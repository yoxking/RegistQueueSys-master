using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;

namespace EntFrm.MainService.Services
{
    public class ITicketService
    { 
        private volatile static ITicketService _instance = null;
        private static readonly object lockHelper = new object();
         
        public static ITicketService CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new ITicketService();
                }
            }
            return _instance;
        }

        private void doClearQueueByCounterNo(string sCounterNo)
        {
            try
            {
                int count = 0;
                string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' And ProcessState In (" + IPublicConsts.PROCSTATE_CALLING +","+ IPublicConsts.PROCSTATE_PROCESSING + ") And ProcessedCounterNo='" + sCounterNo + "' And EnqueueTime Between '" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "' And '" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "'";
                ProcessFlowsBLL pflowBoss = new ProcessFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                ProcessFlowsCollections pflowColl = pflowBoss.GetRecordsByPaging(ref count, 1, 100,sWhere);

                if (pflowColl != null && pflowColl.Count > 0)
                {
                    foreach (ProcessFlows pflow in pflowColl)
                    {
                        pflow.iDataFlag = 1;
                        pflow.dFinishTime = DateTime.Now;
                        pflow.iProcessState = IPublicConsts.PROCSTATE_FINISHED;
                        pflow.dProcessedTime = DateTime.Now;
                        pflow.dModDate = DateTime.Now;

                        if (pflowBoss.UpdateRecord(pflow))
                        {
                            //解锁
                            //doUpdate_DataFlag(pflow.sTFlowNo, 0);
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        private void doClearQueueByStafferNo(string sStafferNo)
        {
            try
            {
                int count = 0;
                string sWhere = "  BranchNo = '" + IUserContext.GetBranchNo() + "' And  ProcessState=" + IPublicConsts.PROCSTATE_CALLING + " And ProcessedStafferNo='" + sStafferNo + "' And EnqueueTime Between '" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "' And '" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00")  + "'";
                ProcessFlowsBLL pflowBoss = new ProcessFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                ProcessFlowsCollections pflowColl = pflowBoss.GetRecordsByPaging(ref count, 1, 100, sWhere);

                if (pflowColl != null && pflowColl.Count > 0)
                {
                    foreach (ProcessFlows pflow in pflowColl)
                    {
                        pflow.iDataFlag = 1;
                        pflow.dFinishTime = DateTime.Now;
                        pflow.iProcessState = IPublicConsts.PROCSTATE_FINISHED;
                        pflow.dProcessedTime = DateTime.Now;
                        pflow.dModDate = DateTime.Now;

                        if (pflowBoss.UpdateRecord(pflow))
                        {
                            //解锁
                            //doUpdate_DataFlag(pflow.sTFlowNo, 0);
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        //登录科室
        //public string doSignIn2(string sCounterNo, string sLoginId, string sPsword)
        public string doSignIn(string sLoginId, string sPsword, string sCounterNo)
        { 
            try
            { 
                int count = 0;
                string sResult = "";
                StafferInfoBLL staffBoss = new StafferInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                StafferInfoCollections staffColl = staffBoss.GetRecordsByPaging(ref count, 1, 1, " BranchNo = '" + IUserContext.GetBranchNo() + "' And LoginId='" + sLoginId + "'");

                CounterInfoBLL counterBoss = new CounterInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                CounterInfo counter = counterBoss.GetRecordByNo(sCounterNo);

                if (staffColl != null && staffColl.Count > 0 && counter != null)
                {
                    StafferInfo staff = staffColl.GetFirstOne();
                    if (staff.sPassword.Equals(sPsword))
                    {
                        counter.iLogonState = 1;
                        counter.sLogonStafferNo = staff.sStafferNo;
                        counter.dModDate = DateTime.Now;

                        counterBoss.UpdateRecord(counter);

                        sResult = staff.sStafferNo;
                         
                        MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "医生(" + sLoginId + ")登录科室(" + counter.sCounterName + ")呼叫器");
                    }
                }

                //////刷新呼叫器  
                //Thread t2 = new Thread(HdcallerService.CreateInstance().doRefreshCaller);
                //t2.Start();
                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
         
        //登出所有科室
        public void doSignoutAll()
        {
            try
            {
                int count = 0;
                CounterInfoBLL counterBoss = new CounterInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                CounterInfoCollections counterList = counterBoss.GetRecordsByPaging(ref count, 1, 100, "  BranchNo = '" + IUserContext.GetBranchNo() + "' ");

                if (counterList != null && counterList.Count > 0)
                {
                    foreach (CounterInfo counter in counterList)
                    {
                        counter.iLogonState = 0;
                        counter.sLogonStafferNo = "";
                        counter.dModDate = DateTime.Now;

                        counterBoss.UpdateRecord(counter).ToString(); 
                    }
                }

                MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "数据清理服务完成...");

                //刷新呼叫器  
                //Thread t2 = new Thread(HdcallerService.CreateInstance().doRefreshCaller);
                //t2.Start(); 
            }
            catch (Exception ex)
            { 
            }
        }

        //登出科室
        public string doSignOut(string sCounterNo)
        {
            try
            {
                string sResult = "false";

                CounterInfoBLL counterBoss = new CounterInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                CounterInfo counter = counterBoss.GetRecordByNo(sCounterNo);

                if (counter != null)
                {
                    counter.iLogonState = 0;
                    counter.sLogonStafferNo = "";
                    counter.dModDate = DateTime.Now;

                    counterBoss.UpdateRecord(counter);
                    sResult = "true";

                    MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "科室(" + counter.sCounterName + ")登出呼叫器");
                }

                //刷新呼叫器  
                //Thread t2 = new Thread(HdcallerService.CreateInstance().doRefreshCaller);
                //t2.Start();

                return sResult;
            }
            catch (Exception ex)
            {
                return "false";
            }
        }

        //科室求助：5
        public string doSeekHelp(string sCounterNo)
        {
            try
            {
                string sResult = "false";
                CounterInfo counter = IPublicHelper.GetCounterByNo(sCounterNo);
                if (counter != null)
                {
                    string sCallVoiceEnable = IUserContext.GetParamValue(IPublicConsts.DEF_CALLVOICEENABLE, "Others");
                    string sCallVoiceFormat = IUserContext.GetParamValue(IPublicConsts.DEF_CALLVOICEFORMAT, "Others");
                    string sCallVoiceStyle = IUserContext.GetParamValue(IPublicConsts.DEF_CALLVOICESTYLE, "Others");
                    string sCallVoiceVolume = IUserContext.GetParamValue(IPublicConsts.DEF_CALLVOICEVOLUME, "Others");
                    string sCallVoiceRate = IUserContext.GetParamValue(IPublicConsts.DEF_CALLVOICERATE, "Others");

                    if (!string.IsNullOrEmpty(sCallVoiceEnable) && sCallVoiceEnable.Equals("1"))
                    {
                        sCallVoiceFormat = sCallVoiceFormat.Replace("[科室名称]", counter.sCounterName);
                        sCallVoiceFormat = sCallVoiceFormat.Replace("[科室别名]", counter.sCounterAlias);

                        SpeechService.CreateInstance().doPlayVoice(sCounterNo,sCallVoiceStyle, int.Parse(sCallVoiceRate), int.Parse(sCallVoiceVolume), sCallVoiceFormat);

                    }
                    sResult = "true";

                    MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "科室(" + counter.sCounterName + ")呼叫求助");
                }
                return sResult;
            }
            catch (Exception ex)
            {
                return "false";
            }
        }

        //科室暂停服务：6
        public string doOutService(string sCounterNo)
        {
            try
            {
                string sResult = "false";
                CounterInfoBLL counterBoss = new CounterInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                CounterInfo counter = IPublicHelper.GetCounterByNo(sCounterNo);
                //LEDDisplay ledinfo =null;

                if (counter != null)
                {
                    counter.iPauseState = 1;
                    counter.dModDate = DateTime.Now;
                    counterBoss.UpdateRecord(counter);

                    //ledinfo = IPublicHelper.GetLEDDisplayByNo(counter.sLedDisplayNo);
                    //if (ledinfo != null)
                    //{
                    //    DisplayService.CreateInstance().doDisplayText(ledinfo, ledinfo.sOnPauseTip);
                    //}
                    sResult = "true";
                    MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "科室(" + counter.sCounterName + ")暂停服务");
                }

                ////刷新呼叫器  
                //Thread t2 = new Thread(HdcallerService.CreateInstance().doRefreshCaller);
                //t2.Start();

                return sResult;
            }
            catch (Exception ex)
            {
                return "false";
            }
        }

        //科室开始服务
        public string doInService(string sCounterNo)
        {
            try
            {
                string sResult = "false";
                CounterInfoBLL counterBoss = new CounterInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                CounterInfo counter = IPublicHelper.GetCounterByNo(sCounterNo);
                //LEDDisplay ledinfo = null;

                if (counter != null)
                {
                    counter.iPauseState = 0;
                    counter.dModDate = DateTime.Now;
                    counterBoss.UpdateRecord(counter);

                    //ledinfo = IPublicHelper.GetLEDDisplayByNo(counter.sLedDisplayNo);
                    //if (ledinfo != null)
                    //{
                    //    DisplayService.CreateInstance().doDisplayText(ledinfo, ledinfo.sPowerOnTip);
                    //}

                    sResult = "true";
                    MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "科室(" + counter.sCounterName + ")开始服务");
                }

                //刷新呼叫器  
                //Thread t2 = new Thread(HdcallerService.CreateInstance().doRefreshCaller);
                //t2.Start();

                return sResult;
            }
            catch (Exception ex)
            {
                return "false";
            }
        }

        //医生/科室叫号：8，9   
        public string doCallNextTicket(string sCounterNo)
        {
            try
            {
                string branchNo = IUserContext.GetBranchNo();
                string callingPFlowNo = "";
                string waitingPFlowNo = "";
                string waitAreaMode = IUserContext.GetParamValue(IPublicConsts.DEF_WAITAREAMODE, "Others");
                //string callingMode = IUserContext.GetParamValue(IPublicConsts.DEF_CALLINGMODE, "Others");
                //int count = int.Parse(waitAreaMode);
                int count = 2;
                WaitArea waitArea = IBusinessHelper.getWaitAreaNoByWAreaIndex(1, branchNo);

                if (waitArea != null)
                {
                    doClearQueueByCounterNo(sCounterNo);
                    CounterInfo counter = IPublicHelper.GetCounterByNo(sCounterNo);
                    if (counter != null)
                    {
                        doClearQueueByStafferNo(counter.sLogonStafferNo);
                    }

                    do
                    {
                        callingPFlowNo = doEnqueue_Calling(sCounterNo);

                        for (int i = 0; i < waitArea.iAreaScale; i++)
                        {
                            string s = doEnqueue_Waiting(sCounterNo, "");
                            if (string.IsNullOrEmpty(s))
                            {
                                break;
                            }
                        }
                        count--;

                    } while (string.IsNullOrEmpty(callingPFlowNo) && count > 0);

                    if (!string.IsNullOrEmpty(callingPFlowNo))
                    { 
                        //播放语音
                        SpeechService.CreateInstance().doPlayVoice(sCounterNo, callingPFlowNo); 
                    }

                    waitingPFlowNo = doEnqueue_Calling(sCounterNo,false);
                    if (!string.IsNullOrEmpty(waitingPFlowNo))
                    {
                        //播放语音
                        SpeechService.CreateInstance().doPlayVoice(sCounterNo, waitingPFlowNo, "waiting");
                    }

                    //刷新呼叫器  
                    //Thread t2 = new Thread(HdcallerService.CreateInstance().doRefreshCaller);
                    //t2.Start();

                    //推送消息 
                    //Thread t3 = new Thread(new ParameterizedThreadStart(IMessageService.CreateInstance().SendMessage));
                    //t3.Start(sCounterNo);

                    MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "科室(" + IPublicHelper.GetCounterNameByNo(sCounterNo) + ")呼叫下一位");
                    return callingPFlowNo;
                }
                return "";
            }
            catch(Exception ex)
            {
                return "";
            }
        }

        //分诊台叫号
        public string doCallSpecTicket(string sPFlowNo)
        {
            return doEnqueue_Waiting("", sPFlowNo);
        }
         
        //指定叫号
        public string doSpecialTicket(string sCounterNo, string sPFlowNo)
        {
            try
            {
                string sResult = "";
                string sStafferNo = IPublicHelper.GetCounterByNo(sCounterNo).sLogonStafferNo;

                doClearQueueByCounterNo(sCounterNo);
                doClearQueueByStafferNo(sStafferNo);

                ProcessFlowsBLL processBoss = new ProcessFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例  
                ProcessFlows processFlow = processBoss.GetRecordByNo(sPFlowNo);
                if (processFlow != null)
                {
                    processFlow.iDataFlag = 0;
                    processFlow.dBeginTime = DateTime.Now;
                    processFlow.iProcessState = IPublicConsts.PROCSTATE_CALLING;
                    processFlow.sProcessedCounterNo = sCounterNo;
                    processFlow.sProcessedStafferNo = sStafferNo;
                    processFlow.dProcessedTime = DateTime.Now;
                    processFlow.dModDate = DateTime.Now;

                    if (processBoss.UpdateRecord(processFlow))
                    { 

                        //播放语音
                        SpeechService.CreateInstance().doPlayVoice(sCounterNo, processFlow.sPFlowNo);
                        sResult = processFlow.sPFlowNo;
                    }


                    ////推送消息 
                    //Thread t3 = new Thread(new ParameterizedThreadStart(IMessageService.CreateInstance().SendMessage));
                    //t3.Start(sCounterNo);

                    MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "科室(" + IPublicHelper.GetCounterNameByNo(sCounterNo) + ")指定呼叫");
                } 

                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        //获取所有医生/业务列表，做转诊使用
        public string getTransferList()
        {
            try
            {
                int count = 0;
                string sResult = "";
                string sWhere = "";
                List<ItemObject> itemList = new List<ItemObject>();
                string workingMode = IUserContext.GetParamValue(IPublicConsts.DEF_WORKINGMODE, "Others");

                if (workingMode.Equals("SERVICE"))
                {
                    sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' ";
                    ServiceInfoBLL infoBoss = new ServiceInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                    ServiceInfoCollections infoColl = infoBoss.GetRecordsByPaging(ref count, 1, 100, sWhere);

                    if (infoColl != null && infoColl.Count > 0)
                    {
                        foreach (ServiceInfo info in infoColl)
                        {
                            itemList.Add(new ItemObject(info.sServiceNo, info.sServiceName));
                        }
                    }
                }
                else if (workingMode.Equals("STAFF"))
                {
                    sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' ";
                    StafferInfoBLL infoBoss = new StafferInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                    StafferInfoCollections infoColl = infoBoss.GetRecordsByPaging(ref count, 1, 100, sWhere);

                    if (infoColl != null && infoColl.Count > 0)
                    {
                        foreach (StafferInfo info in infoColl)
                        {
                            itemList.Add(new ItemObject(info.sStafferNo, info.sStafferName));
                        }
                    }
                }

                sResult = JsonConvert.SerializeObject(itemList);

                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        //病人转移
        public string doTransferTicket(string sTransferNo, string sPFlowNo)
        //public string doTransferTicket(string sPFlowNo,string sTransferNo)
        {
            try
            {
                string sResult = "";
                string workingMode = IUserContext.GetParamValue(IPublicConsts.DEF_WORKINGMODE, "Others");
                

                ProcessFlowsBLL processBoss = new ProcessFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例  
                ProcessFlows processFlow = processBoss.GetRecordByNo(sPFlowNo);
                if (processFlow != null)
                {

                    if (workingMode.Equals("SERVICE"))
                    {
                        string branchNo = IUserContext.GetBranchNo();
                        string counterNos=IPublicHelper.GetCounterNosByServiceNo(sTransferNo, branchNo);
                        processFlow.sServiceNo = sTransferNo;
                        processFlow.sStafferNo = "";
                        processFlow.sCounterNos = counterNos;
                    }
                    else if (workingMode.Equals("STAFF"))
                    {
                        processFlow.sServiceNo = ""; 
                        processFlow.sStafferNo = sTransferNo;
                        processFlow.sCounterNos = "";
                    }
                    processFlow.iWAreaIndex = 0;
                    processFlow.iOrderWeight = 0;
                    processFlow.iProcessState = IPublicConsts.PROCSTATE_EXCHANGE;
                    processFlow.iPriorityType = 0;
                    processFlow.iDelayTimeValue = 0;
                    processFlow.dProcessedTime = DateTime.Now;
                    processFlow.sProcessedCounterNo = "";
                    processFlow.sProcessedStafferNo = "";
                    processFlow.dModDate = DateTime.Now;

                    if (processBoss.UpdateRecord(processFlow))
                    { 
                        sResult = processFlow.sPFlowNo;
                    }

                    //IUserContext.SetStateValue(sTransferNo, "1111111111");

                    MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "病人转诊完成");
                }

                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        
        //重呼：10
        public string doRecallTicket(string sCounterNo)
        {
            try
            {
                string sResult = "";
                string sWhere = "";
                int count = 0;
                DateTime workDate = DateTime.Now.AddMinutes(30);

                ViewTicketFlowsBLL vTicketBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例  
                ViewTicketFlowsCollections vTicketFlows = null;
                ViewTicketFlows vTicket = null;

                if (!string.IsNullOrEmpty(sCounterNo))
                {
                    sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' And ProcessedCounterNo='" + sCounterNo + "' And ProcessState=" + IPublicConsts.PROCSTATE_CALLING + " And  EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";
                     
                    vTicketFlows = vTicketBoss.GetRecordsByPaging(ref count, 1, 1, sWhere); 

                    if (vTicketFlows != null && vTicketFlows.Count > 0)
                    {
                        vTicket = vTicketFlows.GetFirstOne();
                        sResult = vTicket.sTicketNo;

                        //播放语音
                        SpeechService.CreateInstance().doPlayVoice(sCounterNo, vTicket.sPFlowNo);
                    }

                    MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "科室(" + IPublicHelper.GetCounterNameByNo(sCounterNo) + ")进行重呼");
                }

                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        //呼叫（确定） 开始办理业务：11
        public string doStartTicket(string sCounterNo)
        {
            try
            {
                string sResult = "";
                string sWhere = "";
                int count = 0;
                DateTime workDate = DateTime.Now.AddMinutes(30);

                ViewTicketFlowsBLL vTicketBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例 
                ProcessFlowsBLL processBoss = new ProcessFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例    
                ViewTicketFlowsCollections vTicketFlows = null;
                ViewTicketFlows vTicket = null;

                if (!string.IsNullOrEmpty(sCounterNo))
                {
                    sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' And ProcessedCounterNo='" + sCounterNo + "' And ProcessState=" + IPublicConsts.PROCSTATE_CALLING + " And   EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";

                    vTicketFlows = vTicketBoss.GetRecordsByPaging(ref count, 1, 1, sWhere);

                    if (vTicketFlows != null && vTicketFlows.Count > 0)
                    {
                        vTicket = vTicketFlows.GetFirstOne();
                        ProcessFlows processFlow = processBoss.GetRecordByNo(vTicket.sPFlowNo);
                        if (processFlow != null)
                        {
                            processFlow.dBeginTime = DateTime.Now;
                            processFlow.iProcessState = IPublicConsts.PROCSTATE_PROCESSING;
                            processFlow.dProcessedTime = DateTime.Now;
                            processFlow.dModDate = DateTime.Now;

                            if (processBoss.UpdateRecord(processFlow))
                            {
                                sResult = vTicket.sTicketNo;
                            }
                        }

                        MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "科室(" + IPublicHelper.GetCounterNameByNo(sCounterNo) + ")开始就诊");
                    }
                }

                //刷新呼叫器  
                //Thread t2 = new Thread(HdcallerService.CreateInstance().doRefreshCaller);
                //t2.Start();

                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        //呼叫（完成）：12
        public string doFinishTicket(string sCounterNo)
        {
            try
            {
                string sResult = "";
                string sWhere = "";
                int count = 0;
                DateTime workDate = DateTime.Now.AddMinutes(30);

                ViewTicketFlowsBLL vTicketBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例 
                ProcessFlowsBLL processBoss = new ProcessFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例    
                ViewTicketFlowsCollections vTicketFlows = null;
                ViewTicketFlows vTicket = null;

                if (!string.IsNullOrEmpty(sCounterNo))
                {
                    sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' And ProcessedCounterNo='" + sCounterNo + "' And ProcessState In(" + IPublicConsts.PROCSTATE_CALLING + ","+ IPublicConsts.PROCSTATE_PROCESSING + ") And  EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";

                    vTicketFlows = vTicketBoss.GetRecordsByPaging(ref count, 1, 1, sWhere);

                    if (vTicketFlows != null && vTicketFlows.Count > 0)
                    {
                        vTicket = vTicketFlows.GetFirstOne();
                        ProcessFlows processFlow = processBoss.GetRecordByNo(vTicket.sPFlowNo);
                        if (processFlow != null)
                        {  
                            processFlow.iDataFlag = 1;
                            processFlow.dFinishTime = DateTime.Now;
                            processFlow.iProcessState = IPublicConsts.PROCSTATE_FINISHED;
                            processFlow.dProcessedTime = DateTime.Now;
                            processFlow.dModDate = DateTime.Now;

                            if (processBoss.UpdateRecord(processFlow))
                            { 
                                //解锁
                                //doUpdate_DataFlag(processFlow.sTFlowNo, 0);
                                sResult = vTicket.sTicketNo;
                            }
                        }

                        MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "科室(" + IPublicHelper.GetCounterNameByNo(sCounterNo) + ")完成就诊");
                    }
                }

                //刷新呼叫器  
                //Thread t2 = new Thread(HdcallerService.CreateInstance().doRefreshCaller);
                //t2.Start();

                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        
        //未到：15
        public string doNotcomeTicket(string sCounterNo)
        {
            try
            {
                string sResult = "";
                string sWhere = "";
                int count = 0;
                DateTime workDate = DateTime.Now.AddMinutes(30);

                ViewTicketFlowsBLL vTicketBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例 
                ProcessFlowsBLL processBoss = new ProcessFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例    
                ViewTicketFlowsCollections vTicketFlows = null;
                ViewTicketFlows vTicket = null;

                if (!string.IsNullOrEmpty(sCounterNo))
                {
                    sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' And ProcessedCounterNo='" + sCounterNo + "' And ProcessState=" + IPublicConsts.PROCSTATE_CALLING + " And   EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";

                    vTicketFlows = vTicketBoss.GetRecordsByPaging(ref count, 1, 1, sWhere);

                    if (vTicketFlows != null && vTicketFlows.Count > 0)
                    {
                        vTicket = vTicketFlows.GetFirstOne();
                        ProcessFlows processFlow = processBoss.GetRecordByNo(vTicket.sPFlowNo);
                        if (processFlow != null)
                        {
                            processFlow.iDataFlag = 1;
                            processFlow.dFinishTime = DateTime.Now;
                            processFlow.iProcessState = IPublicConsts.PROCSTATE_NONARRIVAL;
                            processFlow.dProcessedTime = DateTime.Now;
                            processFlow.dModDate = DateTime.Now; 

                            if (processBoss.UpdateRecord(processFlow))
                            { 
                                sResult = vTicket.sTicketNo;
                            }
                        }

                        MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "科室(" + IPublicHelper.GetCounterNameByNo(sCounterNo) + ")未到过号");
                    }
                }

                //刷新呼叫器  
                //Thread t2 = new Thread(HdcallerService.CreateInstance().doRefreshCaller);
                //t2.Start();

                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        //未到过号：15
        public string doPassedTicket(string sCounterNo,string sPFlowNo)
        {
            try
            {
                string sResult = ""; 

                ProcessFlowsBLL processBoss = new ProcessFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例  
                ProcessFlows processFlow = processBoss.GetRecordByNo(sPFlowNo);
                if (processFlow != null)
                {
                    processFlow.iDataFlag = 1;
                    processFlow.dFinishTime = DateTime.Now;
                    processFlow.iProcessState = IPublicConsts.PROCSTATE_NONARRIVAL;
                    processFlow.dProcessedTime = DateTime.Now;
                    processFlow.dModDate = DateTime.Now;

                    if (processBoss.UpdateRecord(processFlow))
                    { 
                        sResult = processFlow.sPFlowNo;
                    }

                    MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "科室(" + IPublicHelper.GetCounterNameByNo(sCounterNo) + ")未到过号");
                }

                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        
        //复诊入队：15
        public string doRediagTicket(string sCounterNo, string sPFlowNo)
        {
            try
            {
                string sResult = "";

                ProcessFlowsBLL processBoss = new ProcessFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例  
                ProcessFlows processFlow = processBoss.GetRecordByNo(sPFlowNo);
                if (processFlow != null)
                {  
                    processFlow.iDataFlag = 0;
                    processFlow.sWaitAreaNo = IPublicHelper.GetWaitAreaNoByWAreaIndex(0).sWAreaNo;
                    processFlow.iWAreaIndex = 0;
                    processFlow.iOrderWeight = 2;
                    processFlow.iPriorityType = IPublicConsts.PRIORITY_TYPE0;
                    processFlow.iDelayTimeValue = 0;
                    processFlow.iProcessState = IPublicConsts.PROCSTATE_REDIAGNOSIS;
                    processFlow.dProcessedTime = DateTime.Now;
                    processFlow.dModDate = DateTime.Now;

                    if (processBoss.UpdateRecord(processFlow))
                    {
                        sResult = processFlow.sPFlowNo;
                    }

                    MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "科室(" + IPublicHelper.GetCounterNameByNo(sCounterNo) + ")复诊");
                }

                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public string doCallNextRecipe(string sRFlowNo)
        {
            try
            {
                string branchNo = IUserContext.GetBranchNo();

                ViewRecipeFlowsBLL rflowBLL = new ViewRecipeFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                ViewRecipeFlows recFlow = rflowBLL.GetRecordByNo(sRFlowNo);

                if (recFlow != null)
                {
                    CounterInfo counterInfo = IPublicHelper.GetCounterByNo(recFlow.sCounterNo);
                    VoiceInfo ttsInfo = IPublicHelper.GetVoiceInfoByNo(counterInfo.sVoiceStyleNos.Split(';')[0]);

                    string sPlayingText = IPublicHelper.ReplaceVariables_Recipe(ttsInfo.sFormatCalling, recFlow.sRFlowNo);

                    //播放语音
                    SpeechService.CreateInstance().doPlayVoice(counterInfo.sCounterNo,ttsInfo.sVoice, ttsInfo.iRate, ttsInfo.iVolume, sPlayingText);

                    MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "窗口(" + counterInfo.sCounterAlias + ")呼叫"+ recFlow.sCnName);
                }
                return sRFlowNo;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        
        //插入票号
        public string doAddNewTicket(string sServiceNo, string sStafferNo, string sNewTicketNo, string sUName, string sIdCardNo, string sTelphone, string sWorkDate)
        {
            try
            {
                string sRUserNo = IBusinessHelper.InsertRUserInfo(sUName, sIdCardNo, sTelphone);

                return doAddNewTicket(sServiceNo, sStafferNo, sNewTicketNo, sRUserNo, sWorkDate);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public string doAddNewTicket(string sServiceNo, string sStafferNo, string sNewTicketNo, string sRUserNo, string sWorkDate)
        {
            try
            {
                string sTicketNo = "";
                string sTFlowNo = "";
                string sPFlowNo = "";
                string sCounterNos = "";
                string sWAreaNo = "";
                string sBranchNo = IUserContext.GetBranchNo();
                DateTime workDate = DateTime.Parse(sWorkDate);

                WaitArea wareaInfo = IBusinessHelper.getWaitAreaNoByWAreaIndex(0, sBranchNo);
                if (wareaInfo != null)
                {
                    sWAreaNo = wareaInfo.sWAreaNo;
                }

                if (string.IsNullOrEmpty(sNewTicketNo))
                {
                    ViewTicketFlowsBLL infoBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                    string sWhere = " BranchNo='" + IUserContext.GetBranchNo() + "' And EnqueueTime  Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";
                    sTicketNo = (infoBoss.GetCountByCondition(sWhere) + 1).ToString();
                }
                else
                {
                    sTicketNo = sNewTicketNo;
                }

                if (!string.IsNullOrEmpty(sServiceNo))
                {
                    sCounterNos = IBusinessHelper.getCounterNosByServiceNo(sServiceNo, sBranchNo);
                }

                sTFlowNo = IBusinessHelper.InsertTicketFlow(sTicketNo, sRUserNo, sBranchNo);
                if (!string.IsNullOrEmpty(sTFlowNo))
                {
                    sPFlowNo = IBusinessHelper.InsertProcessFlow(sTFlowNo, sServiceNo, sCounterNos, sStafferNo, IPublicConsts.REGISTETYPE1,sWAreaNo, 0, sBranchNo);
                    if (!string.IsNullOrEmpty(sPFlowNo))
                    {
                        MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "取票(" + sTicketNo + ")成功!");
                        
                        return sTicketNo;
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        
        private string doEnqueue_Calling(string sCounterNo, bool enqueueFlag = true)
        {
            try
            {
                string sWhere = "";
                string sStafferNo = "";
                DateTime workDate = DateTime.Now.AddMinutes(30);
                string workingMode = IUserContext.GetParamValue(IPublicConsts.DEF_WORKINGMODE, "Others");
                //string callingMode = IUserContext.GetParamValue(IPublicConsts.DEF_CALLINGMODE, "Others");
                //string waitAreaMode = IUserContext.GetParamValue(IPublicConsts.DEF_WAITAREAMODE, "Others");

                ProcessFlowsBLL processBoss = new ProcessFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例  
                ProcessFlowsCollections processFlows = null;
                ProcessFlows processFlow = null;

                //获取登录窗口的医生/员工编号
                sStafferNo = IPublicHelper.GetCounterByNo(sCounterNo).sLogonStafferNo;

                if (workingMode.Equals("SERVICE"))
                {
                    sWhere = " DataFlag=0 And BranchNo = '" + IUserContext.GetBranchNo() + "' And CounterNos Like '%" + sCounterNo + "%' And ProcessState Between " + IPublicConsts.PROCSTATE_WAITING + " And " + IPublicConsts.PROCSTATE_WAITAREA9 + " And   EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";

                }
                else if (workingMode.Equals("STAFF"))
                {
                    sWhere = " DataFlag=0 And BranchNo = '" + IUserContext.GetBranchNo() + "' And StafferNo='" + sStafferNo + "' And ProcessState Between " + IPublicConsts.PROCSTATE_WAITING + " And " + IPublicConsts.PROCSTATE_WAITAREA9 + " And  EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";

                }

                SqlModel s_model = new SqlModel();

                s_model.iPageNo = 1;
                s_model.iPageSize = 1;
                s_model.sFields = "*";
                s_model.sCondition = sWhere;
                s_model.sOrderField = " ProcessState Desc,OrderWeight Desc,ProcessedTime Asc,ID ";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "ProcessFlows";

                processFlows = processBoss.GetRecordsByPaging(s_model);

                if (processFlows != null && processFlows.Count > 0)
                {
                    processFlow = processFlows.GetFirstOne();
                }


                if (processFlow != null)
                {
                    if (enqueueFlag)
                    {
                        processFlow.iDataFlag = 0;
                        processFlow.dBeginTime = DateTime.Now;
                        processFlow.iProcessState = IPublicConsts.PROCSTATE_CALLING;
                        processFlow.sProcessedCounterNo = sCounterNo;
                        processFlow.sProcessedStafferNo = sStafferNo;
                        processFlow.dProcessedTime = DateTime.Now;
                        processFlow.dModDate = DateTime.Now;

                        if (processBoss.UpdateRecord(processFlow))
                        {
                            //锁定
                            //doUpdate_DataFlag(processFlow.sTFlowNo,1);
                            return processFlow.sPFlowNo;
                        }
                    }
                    else
                    {
                        return processFlow.sPFlowNo;
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        private string doEnqueue_Waiting(string sCounterNo, string sPFlowNo)
        {
            try
            {
                string sWhere = "";
                string sStafferNo = "";
                string branchNo = IUserContext.GetBranchNo();
                DateTime workDate = DateTime.Now.AddMinutes(30);
                string workingMode = IUserContext.GetParamValue(IPublicConsts.DEF_WORKINGMODE, "Others");
                //string callingMode = IUserContext.GetParamValue(IPublicConsts.DEF_CALLINGMODE, "Others");
                //string waitAreaMode = IUserContext.GetParamValue(IPublicConsts.DEF_WAITAREAMODE, "Others");
                int rediagInterval = int.Parse(IUserContext.GetParamValue(IPublicConsts.DEF_REDIAGNOSISINTERVAL, "Others"));

                ProcessFlowsBLL processBoss = new ProcessFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例  
                CounterInfoBLL counterBoss = new CounterInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例  
                ProcessFlowsCollections processFlows = null;
                ProcessFlows processFlow = null;
                CounterInfo counter = null;
                WaitArea waitArea = null;

                SqlModel s_model = new SqlModel();

                if (!string.IsNullOrEmpty(sPFlowNo))
                {
                    processFlow = processBoss.GetRecordByNo(sPFlowNo);
                }
                else
                {
                    counter = counterBoss.GetRecordByNo(sCounterNo);
                    if (counter != null)
                    {
                        sStafferNo = counter.sLogonStafferNo;

                        //复诊
                        if (counter.iCalledNum >= rediagInterval)
                        {
                            if (workingMode.Equals("SERVICE"))
                            {
                                sWhere = " DataFlag=0 And  BranchNo = '" + IUserContext.GetBranchNo() + "' And CounterNos Like '%" + sCounterNo + "%' And ProcessState =" + IPublicConsts.PROCSTATE_REDIAGNOSIS + " And  EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";
                            }
                            else if (workingMode.Equals("STAFF"))
                            {
                                sWhere = " DataFlag=0 And  BranchNo = '" + IUserContext.GetBranchNo() + "' And StafferNo='" + sStafferNo + "' And ProcessState =" + IPublicConsts.PROCSTATE_REDIAGNOSIS + "  And EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";
                            }

                            s_model.iPageNo = 1;
                            s_model.iPageSize = 1;
                            s_model.sFields = "*";
                            s_model.sCondition = sWhere;
                            s_model.sOrderField = "ProcessedTime ";
                            s_model.sOrderType = "Asc";
                            s_model.sTableName = "ProcessFlows";

                            processFlows = processBoss.GetRecordsByPaging(s_model);
                            if (processFlows != null && processFlows.Count > 0)
                            {
                                processFlow = processFlows.GetFirstOne();
                                counter.iCalledNum = 0;
                            }
                        }

                        if (processFlow == null)
                        {
                            if (workingMode.Equals("SERVICE"))
                            {
                                sWhere = " DataFlag=0 And BranchNo = '" + IUserContext.GetBranchNo() + "' And CounterNos Like '%" + sCounterNo + "%' And ProcessState Between " + IPublicConsts.PROCSTATE_OUTQUEUE + " And " + IPublicConsts.PROCSTATE_DELAY + " And EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";
                            }
                            else if (workingMode.Equals("STAFF"))
                            {
                                sWhere = " DataFlag=0 And BranchNo = '" + IUserContext.GetBranchNo() + "' And StafferNo='" + sStafferNo + "' And ProcessState Between " + IPublicConsts.PROCSTATE_OUTQUEUE + " And " + IPublicConsts.PROCSTATE_DELAY + " And EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";
                            }

                            s_model.iPageNo = 1;
                            s_model.iPageSize = 1;
                            s_model.sFields = "*";
                            s_model.sCondition = sWhere;
                            s_model.sOrderField = " ProcessState Desc,OrderWeight Desc,ProcessedTime Asc,ID ";
                            s_model.sOrderType = "Asc";
                            s_model.sTableName = "ProcessFlows";

                            processFlows = processBoss.GetRecordsByPaging(s_model);
                            if (processFlows != null && processFlows.Count > 0)
                            {
                                processFlow = processFlows.GetFirstOne();
                                counter.iCalledNum++;
                            }
                        }

                        if (processFlow == null)
                        {
                            if (workingMode.Equals("SERVICE"))
                            {
                                sWhere = "  DataFlag=0 And BranchNo = '" + IUserContext.GetBranchNo() + "' And CounterNos Like '%" + sCounterNo + "%' And ProcessState Between " + IPublicConsts.PROCSTATE_OUTQUEUE + " And " + IPublicConsts.PROCSTATE_WAITING + " And EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";
                            }
                            else if (workingMode.Equals("STAFF"))
                            {
                                sWhere = "  DataFlag=0 And BranchNo = '" + IUserContext.GetBranchNo() + "' And StafferNo='" + sStafferNo + "' And ProcessState Between " + IPublicConsts.PROCSTATE_OUTQUEUE + " And " + IPublicConsts.PROCSTATE_WAITING + " And EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";
                            }

                            s_model.iPageNo = 1;
                            s_model.iPageSize = 1;
                            s_model.sFields = "*";
                            s_model.sCondition = sWhere;
                            s_model.sOrderField = " ProcessState Desc,OrderWeight Desc,ProcessedTime Asc,ID ";
                            s_model.sOrderType = "Asc";
                            s_model.sTableName = "ProcessFlows";

                            processFlows = processBoss.GetRecordsByPaging(s_model);
                            if (processFlows != null && processFlows.Count > 0)
                            {
                                processFlow = processFlows.GetFirstOne();
                                counter.iCalledNum++;
                            }
                        }


                        counterBoss.UpdateRecord(counter);//更新叫号次数
                    }
                }

                if (processFlow != null)
                {
                    waitArea = IBusinessHelper.getWaitAreaNoByWAreaIndex(processFlow.iWAreaIndex + 1, branchNo);
                    if (waitArea != null)
                    {
                        int count = 1;
                        if (workingMode.Equals("SERVICE"))
                        {
                            sWhere = " DataFlag=0 And   BranchNo = '" + IUserContext.GetBranchNo() + "' And WaitAreaNo='" + waitArea.sWAreaNo + "' And WAreaIndex=" + waitArea.iAreaIndex + " And CounterNos Like '%" + processFlow.sCounterNos + "%'  And ProcessState Between " + IPublicConsts.PROCSTATE_WAITING + " And " + IPublicConsts.PROCSTATE_WAITAREA9 + " And  EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";
                            count = processBoss.GetCountByCondition(sWhere);
                        }
                        else if (workingMode.Equals("STAFF"))
                        {
                            sWhere = " DataFlag=0 And   BranchNo = '" + IUserContext.GetBranchNo() + "' And WaitAreaNo='" + waitArea.sWAreaNo + "' And WAreaIndex=" + waitArea.iAreaIndex + " And StafferNo='" + processFlow.sStafferNo + "' And ProcessState Between " + IPublicConsts.PROCSTATE_WAITING + " And " + IPublicConsts.PROCSTATE_WAITAREA9 + "  And  EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";
                            count = processBoss.GetCountByCondition(sWhere);
                        }

                        if (count < waitArea.iAreaScale)
                        {
                            processFlow.sWaitAreaNo = waitArea.sWAreaNo;
                            processFlow.iWAreaIndex = waitArea.iAreaIndex;

                            processFlow.iProcessState = IPublicConsts.PROCSTATE_WAITAREA1 + waitArea.iAreaIndex;
                            processFlow.dProcessedTime = DateTime.Now;
                            processFlow.dModDate = DateTime.Now;

                            if (processBoss.UpdateRecord(processFlow))
                            {
                                return processFlow.sPFlowNo;
                            }
                        }
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        private void doUpdate_DataFlag(string sTicketNo, int dataFlag = 0)
        {
            ProcessFlowsBLL processBoss = new ProcessFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例  
            ProcessFlowsCollections processFlows = processBoss.GetRecordsByClassNo(sTicketNo);

            if (processFlows != null && processFlows.Count > 0)
            {
                foreach (ProcessFlows pflow in processFlows)
                {
                    if (pflow.iProcessState < IPublicConsts.PROCSTATE_CALLING)
                    {
                        pflow.iDataFlag = dataFlag;
                        pflow.dModDate = DateTime.Now;

                        processBoss.UpdateRecord(pflow);
                    }
                }
            }
        }
    }
}

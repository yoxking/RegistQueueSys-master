using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;

namespace EntFrm.MainService.Services
{
    public class HdcallerService
    {
        private volatile static HdcallerService _instance = null;
        private static readonly object lockHelper = new object();
        private static Dictionary<int, bool> TransferState = new Dictionary<int, bool>();
        //private static DateTime UpdateTime = DateTime.Parse("1970-01-01");

        public static HdcallerService CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new HdcallerService();

                }
            }
            return _instance;
        }
        
        public void doInitialCaller()
        {
            try
            {
                object obj = null;
                CallerSerialPort sp = null;
                int count = 0;
                string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' ";
                CallerInfoBLL callerBoss = new CallerInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例  

                CallerInfoCollections callerColl = callerBoss.GetRecordsByPaging(ref count, 1, 100, sWhere);
                if (callerColl != null && callerColl.Count > 0)
                {
                    foreach (CallerInfo caller in callerColl)
                    {
                        TransferState.Add(caller.iPhyAddr, false);  //转移状态
                        obj= SeriportService.CreateInstance().getSerialPort(caller.sSerialPort);
                        if (obj==null)
                        {
                            sp = new CallerSerialPort();
                            sp.ReceiveDataEvent += doDataReceived;
                            if (sp.Open(caller.sSerialPort, "9600"))
                            {
                                SeriportService.CreateInstance().setSerialPort(caller.sSerialPort, sp);
                            }
                        } 
                    }
                }

                MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "呼叫器初始化完成...");
            }
            catch (Exception ex)
            { 
                MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "监听呼叫器失败..." + ex.Message);
            }
        }

        public void doRefreshCaller()
        {
            try
            {
                ViewTicketFlows vTicketView = null;
                CounterInfo counter = null;

                string cmdStr = "";
                string waiternum = "0";
                string ticketNo = "aaa";
                object obj = null;
                CallerSerialPort sp = null;

                int count = 0;
                string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' ";
                CallerInfoBLL callerBoss = new CallerInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例  
                CallerInfoCollections callerColl = callerBoss.GetRecordsByPaging(ref count, 1, 100, sWhere);

                if (callerColl != null && callerColl.Count > 0)
                {
                    Thread.Sleep(500);
                    foreach (CallerInfo caller in callerColl)
                    {
                        waiternum = "0";
                        ticketNo = "AAA";
                        counter = IPublicHelper.GetCounterByCallerNo(caller.sCallerNo);
                        if (counter != null && (counter.iLogonState == 1 || counter.iIsAutoLogon == 1))
                        {
                            //waiternum = OpenTicketBusiness.getVTicketCountByCounterNo(counter.sCounterNo, DateTime.Now.ToString(), "0");
                            //vTicketView = OpenTicketBusiness.getVTicketFlowBy(counter.sCounterNo, DateTime.Now.ToString(), "1");

                            if (vTicketView != null)
                            {
                                ticketNo = vTicketView.sTicketNo;
                            }
                        }

                        int inum = int.Parse(waiternum);
                        if (inum < 0 || inum > 99)
                        {
                            inum = 99;
                        }

                        //if (inum == 1)
                        //{
                        //    cmdStr = NCallerConvertUtil.FormatData(new NCallerDataModel(caller.iPhyAddr, 3, "",""));
                        //    ComPortDict[caller.sSerialPort].Send(cmdStr);
                        //    Thread.Sleep(10);
                        //}
                        try
                        {
                            obj = SeriportService.CreateInstance().getSerialPort(caller.sSerialPort);
                            if (obj == null)
                            {
                                sp = new CallerSerialPort();
                                sp.ReceiveDataEvent += doDataReceived;
                                if (sp.Open(caller.sSerialPort, "9600"))
                                {
                                    SeriportService.CreateInstance().setSerialPort(caller.sSerialPort, sp);
                                }
                            }
                            else
                            {
                                if(obj is CallerSerialPort)
                                {
                                    sp = (CallerSerialPort)obj;
                                }
                            }

                            if (sp != null)
                            {
                                cmdStr = NCallerConvertUtil.FormatData(new NCallerDataModel(caller.iPhyAddr, 2, inum.ToString().PadLeft(2, '0') + "A" + ticketNo.Substring(ticketNo.Length - 3, 3), ""));
                                sp.Send(cmdStr);
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "刷新呼叫器时出错，详细：" + ex.Message);
            }
        }

        private void doDataReceived(object sender, SerialPortEventArgs e)
        {
            try
            {
                SerialPort sp = ((CallerSerialPort)sender).sp;
                NCallerDataModel mCommandData = NCallerConvertUtil.ParseData(e.receivedData);

                //frmMainFrame.PrintMessage(e.receivedData);

                if (mCommandData != null)
                {
                    int iPhyAddr = mCommandData.Address;
                    int iFunCode = mCommandData.Funcode;
                    int iCmdCode = int.Parse(mCommandData.Optcode);
                    string sParamData = mCommandData.Optdata;

                    CallerInfoBLL callerBoss = new CallerInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例 
                    CallerInfo caller = IPublicHelper.GetCallerByPhyAddr(sp.PortName, iPhyAddr.ToString());
                    if (caller != null)
                    {
                        TimeSpan timeSpan = DateTime.Now - caller.dUpdateTime;
                        if (timeSpan.TotalSeconds > caller.iTimeoutSec)
                        {
                            //caller.iUpdateFlag = 0;
                            caller.dUpdateTime = DateTime.Now;
                            callerBoss.UpdateRecord(caller);

                            CounterInfo counter = IPublicHelper.GetCounterByCallerNo(caller.sCallerNo);
                            if (counter != null)
                            {
                                //10：重排/开始/欢迎
                                //11：等候/结束/评价
                                //12：呼叫
                                //13：重呼
                                //14：取消
                                //15：功能
                                //0+15：暂停
                                //1+15：警报/一米线
                                //2+15：刷新
                                //3+15：回呼
                                //4+15：退出
                                //5+15：删除
                                //6+15：忽略
                                //7+15：账号
                                //8+15：地址
                                //9+15：统计 

                                //功能
                                if (iFunCode == 1)
                                {
                                    switch (iCmdCode)
                                    {
                                        case 10:  //10：重排/开始/欢迎
                                            if (counter.iIsAutoLogon == 1 || counter.iLogonState == 1)
                                            {
                                                //OpenTicketBusiness.doStartTicket(counter.sCounterNo);
                                                TransferState[iPhyAddr] = false;
                                            }
                                            break;
                                        case 11:  //11：等候/结束/评价    
                                            if (counter.iIsAutoLogon == 1 || counter.iLogonState == 1)
                                            {
                                                //OpenTicketBusiness.doFinishTicket(counter.sCounterNo);
                                                //OpenTicketBusiness.doStartEvalute(counter.sCounterNo);//开始评价
                                                TransferState[iPhyAddr] = false;
                                            }
                                            break;
                                        case 12:  //12：呼叫
                                            if (counter.iIsAutoLogon == 1 || counter.iLogonState == 1)
                                            {
                                                //OpenTicketBusiness.doCallNextTicket(counter.sCounterNo);
                                                TransferState[iPhyAddr] = false;
                                            }
                                            break;
                                        case 13:  //13：重呼
                                            if (counter.iIsAutoLogon == 1 || counter.iLogonState == 1)
                                            {
                                                //OpenTicketBusiness.doRecallTicket(counter.sCounterNo);
                                                TransferState[iPhyAddr] = false;
                                            }
                                            break;
                                        case 14:   //14：取消
                                            TransferState[iPhyAddr] = false;
                                            break;
                                        case 15:  //15：功能   
                                            doSpecialCommand(iPhyAddr, counter.sCounterNo, sParamData);
                                            break;
                                        case 18:  //18：等候   
                                            {
                                            }
                                            break;
                                        case 19:  //19：转移   
                                            {
                                                //OpenTicketBusiness.doTransferTicketByAlias(counter.sCounterNo, sParamData);
                                            }
                                            break;
                                        case 20:  //20：登录   
                                            {
                                                if (sParamData.Length > 1)   //登录
                                                {
                                                    StafferInfo staff = IPublicHelper.GetStaffByLoginId(sParamData);

                                                    if (staff != null)
                                                    {
                                                        //OpenTicketBusiness.doSignIn(counter.sCounterNo,staff.sLoginId, staff.sPassword );
                                                    }
                                                }
                                            }
                                            break;
                                        default:
                                            TransferState[iPhyAddr] = false;
                                            break;
                                    }
                                }
                                //评价
                                else if (iFunCode == 17)
                                {
                                    TransferState[iPhyAddr] = false;
                                    //OpenTicketBusiness.doEvaluTicket(counter.sCounterNo, counter.sStafferNo, iCmdCode.ToString());
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "呼叫器监听时出错，详细：" + ex.Message);
            }
        }

        private void doSpecialCommand(int iPhyAddr, string sCounterNo, string sParam)
        {
            try
            {
                StafferInfoBLL infoBoss = new StafferInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例 

                //string cmdStr = Convert.ToString(iPhyAddr, 16).PadLeft(2, '0') + "-02-0a-0a-0a-0a-0a-0a";

                if (TransferState[iPhyAddr])  //转移到业务
                {
                    //OpenTicketBusiness.doTransferTicketByAlias(sCounterNo, sParam);
                    TransferState[iPhyAddr] = false;
                }
                else
                {
                    if (sParam.Length > 1)   //登录
                    {
                        StafferInfo staff = IPublicHelper.GetStaffByLoginId(sParam);

                        if (staff != null)
                        {
                            //OpenTicketBusiness.doSignIn(staff.sLoginId, staff.sPassword, sCounterNo);
                        }
                    }
                    else
                    {
                        int pValue = int.Parse(sParam);
                        switch (pValue)
                        {
                            case 0:      //0+15：暂停
                                {
                                    //OpenTicketBusiness.doOutService(sCounterNo);
                                }
                                break;
                            case 1:     //1+15：警报/一米线
                                {
                                    //CounterInfo counter = IPublicHelper.GetCounterByNo(sCounterNo);

                                    //EvaluatorBusiness myEval = new EvaluatorBusiness();
                                    //myEval.CallerNo = counter.sCallerNo;
                                    //myEval.LoginId = "";

                                    //Thread t = new Thread(myEval.doWaitingEvaluator);
                                    //t.Start();
                                }
                                break;
                            case 2:      //2+15：刷新  转移
                                TransferState[iPhyAddr] = true;    //进入转移状态
                                break;
                            case 3:      //3+15：回呼
                                break;
                            case 4:      //4+15：退出
                                //OpenTicketBusiness.doSignOut(sCounterNo);
                                break;
                            case 5:      //5+15：删除
                                         //OpenTicketBusiness.doAbortTicket(sCounterNo);
                                //OpenTicketBusiness.doFinishTicket(sCounterNo);
                                break;
                            case 6:      //6+15：忽略
                                break;
                            case 7:      //7+15：账号
                                break;
                            case 8:      //8+15：地址
                                //cmdStr = getReturnString(iPhyAddr, "0", "aaaa", iPhyAddr.ToString());
                                ////cmdStr = "01-02-0a-0a-0a-0a-0a-0a";
                                break;
                            case 9:      //9+15：统计 
                                break;
                            default:
                                break;
                        }
                    }
                }

                return;
            }
            catch (Exception ex)
            {
                return;
            }
        }
    }
} 

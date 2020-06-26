using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.DataAdapter.Business;
using EntFrm.DataAdapter.Entities;
using EntFrm.Framework.Utility;
using FluentScheduler;
using Newtonsoft.Json;
using System;
using System.Threading;

namespace EntFrm.DataAdapter.Services
{
    public class ScheduleService
    {
        private volatile static ScheduleService _instance = null;
        private static readonly object lockHelper = new object(); 

        public static ScheduleService CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new ScheduleService();
                }
            }
            return _instance;
        }

        private ScheduleService() { }


        /// <summary>
        /// 启动定时任务
        /// </summary>
        public void StartSchedule()
        {
            try
            {
                JobManager.Initialize(new ScheduleFactory());
            }
            catch(Exception ex) { }
        }

        /// <summary>
        /// 停止定时任务
        /// </summary>
        public void StopSchedule()
        {
            try
            {
                JobManager.Stop();
            }
            catch(Exception ex)
            {

            }
        }

        internal class ScheduleFactory : Registry
        {
            public ScheduleFactory()
            {
                Schedule<WipeSerivceJob>().ToRunNow().AndEvery(30).Minutes(); //立即执行每30分钟一次的计划任务
                Schedule<MigrateDataJob>().ToRunEvery(1).Days().At(01, 00); //在每天的 1:00 分执行
                // Schedule<ExAuthorizeJob>().ToRunEvery(1).Days().At(02, 00); //在每天的 2:00 分执行 


                DsQuartzInfoBLL quartzBoss = new DsQuartzInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例 

                DsQuartzInfoCollections quartzColl = quartzBoss.GetAllRecords();


                if (quartzColl != null && quartzColl.Count > 0)
                {
                    foreach (DsQuartzInfo quartz in quartzColl)
                    {
                        string[] timeAt = quartz.sCornExp.Split(':');

                        int hours = int.Parse(timeAt[0]);
                        int minutes = int.Parse(timeAt[1]);
                        //int seconds = int.Parse(timeAt[2]);

                        switch (quartz.sJobTask)
                        {
                            case "Shutdown":
                                Schedule<ShutdownJob>().ToRunEvery(1).Days().At(hours, minutes);
                                break;
                            case "Reboot":
                                Schedule<RebootJob>().ToRunEvery(1).Days().At(hours, minutes);
                                break;
                            case "PowerOnOff":
                                Schedule<PowerOnOffJob>().ToRunEvery(1).Days().At(hours, minutes);
                                break;
                            default: break;
                        }
                    }
                }

                //Schedule<WipeSerivceJob>().ToRunEvery(1).Days().At(13, 00); //在每天的下午 1:00 分执行
                //Schedule<BranchInfoJob>().ToRunEvery(1).Days().At(13, 00); //在每天的下午 1:00 分执行
                //Schedule<ServiceInfoJob>().ToRunEvery(1).Days().At(13, 10); //在每天的下午 1:10 分执行
                //Schedule<CounterInfoJob>().ToRunEvery(1).Days().At(13, 20); //在每天的下午 1:10 分执行 
                //Schedule<StaffInfoJob>().ToRunEvery(1).Days().At(13, 20); //在每天的下午 1:10 分执行 
                //Schedule<StaffRotaJob>().ToRunEvery(1).Days().At(13, 30); //在每天的下午 1:10 分执行 
                //Schedule<ServiceRotaJob>().ToRunEvery(1).Days().At(13, 40); //在每天的下午 1:10 分执行 
                //Schedule<RegisteUserJob>().ToRunNow().AndEvery(20).Seconds(); //立即执行每两秒一次的计划任务

                MainFrame.PrintMessage(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "任务调度服务启动完成...");
            }
        }

        internal class WipeSerivceJob : IJob
        {
            void IJob.Execute()
            {
                IAdapterBusiness adapterBoss=AdapterFactory.Create();
                if (adapterBoss!=null&&adapterBoss.wipeHrtbeatFlows())
                {
                    MainFrame.PrintMessage(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "清除心跳信息更新完成...");
                }
                else
                {
                    MainFrame.PrintMessage(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "清除心跳信息更新失败...");
                }
            }
        }

        internal class ShutdownJob : IJob
        {
            void IJob.Execute()
            {
                int count = 0;
                CmmdData command = null;
                string s = "";
                string dtStart = DateTime.Now.AddMinutes(-1).ToString("HH:mm");
                string dtEndit = DateTime.Now.AddMinutes(1).ToString("HH:mm");
                DsQuartzInfoBLL quartzBoss = new DsQuartzInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例 
                DsQuartzInfoCollections quartzColl = quartzBoss.GetRecordsByPaging(ref count, 1, 10, " JobTask='Shutdown' And  CornExp Between '"+ dtStart + "' And '"+ dtEndit + "' ");

                if (quartzColl != null && quartzColl.Count > 0)
                {
                    foreach (DsQuartzInfo quartz in quartzColl)
                    {
                        string[] playerCodes = quartz.sPlayerNos.Split(';');
                        command = new CmmdData();
                        command.cmmdName = "doShutdown"; 
                        command.cmmdType = "MAdapter";
                        command.cmmdArgs = new string[] { "" };

                        s = JsonConvert.SerializeObject(command);

                        foreach (string playerCode in playerCodes) {
                            NettyHostService.CreateInstance().SendCommandData(playerCode, s);
                            Thread.Sleep(200);
                        }
                    }
                }
            }
        }

        internal class RebootJob : IJob
        {
            void IJob.Execute()
            {
                int count = 0;
                CmmdData command = null;
                string s = "";
                string dtStart = DateTime.Now.AddMinutes(-1).ToString("HH:mm");
                string dtEndit = DateTime.Now.AddMinutes(1).ToString("HH:mm");
                DsQuartzInfoBLL quartzBoss = new DsQuartzInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例 
                DsQuartzInfoCollections quartzColl = quartzBoss.GetRecordsByPaging(ref count, 1, 10, " JobTask='Reboot' And  CornExp Between '" + dtStart + "' And '" + dtEndit + "' ");

                if (quartzColl != null && quartzColl.Count > 0)
                {
                    foreach (DsQuartzInfo quartz in quartzColl)
                    {
                        string[] playerCodes = quartz.sPlayerNos.Split(';');
                        command = new CmmdData();
                        command.cmmdName = "doReboot";
                        command.cmmdType = "MAdapter";
                        command.cmmdArgs = new string[] { "" };

                        s = JsonConvert.SerializeObject(command);

                        foreach (string playerCode in playerCodes)
                        {
                            NettyHostService.CreateInstance().SendCommandData(playerCode, s);
                            Thread.Sleep(200);
                        }
                    }
                }
            }
        }

        internal class PowerOnOffJob : IJob
        {
            void IJob.Execute()
            {
                int count = 0;
                CmmdData command = null;
                string s = "";
                int weekDay = CalendarHelper.GetDayOfWeek(DateTime.Now);
                string dtStart = DateTime.Now.AddMinutes(-1).ToString("HH:mm");
                string dtEndit = DateTime.Now.AddMinutes(1).ToString("HH:mm");
                DsQuartzInfoBLL quartzBoss = new DsQuartzInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例 
                DsQuartzInfoCollections quartzColl = quartzBoss.GetRecordsByPaging(ref count, 1, 10, " WeekDay="+ weekDay + " And  JobTask='PowerOnOff' And  CornExp Between '" + dtStart + "' And '" + dtEndit + "' ");

                if (quartzColl != null && quartzColl.Count > 0)
                {
                    foreach (DsQuartzInfo quartz in quartzColl)
                    {
                        string[] playerCodes = quartz.sPlayerNos.Split(';');
                        string[] powerTime = quartz.sComments.Split(':');
                        command = new CmmdData();
                        command.cmmdName = "doPowerOnOff";
                        command.cmmdType = "MAdapter";
                        command.cmmdArgs = new string[] { DateTime.Now.AddDays(1).ToString("yyyy,MM,dd,")+powerTime[0], DateTime.Now.ToString("yyyy,MM,dd,") + powerTime[1] };

                        s = JsonConvert.SerializeObject(command);

                        foreach (string playerCode in playerCodes)
                        {
                            NettyHostService.CreateInstance().SendCommandData(playerCode, s);
                            Thread.Sleep(200);
                        }
                    }
                }
            }
        }

        internal class MigrateDataJob : IJob
        {
            void IJob.Execute()
            {
                try
                {
                    doMigrateProcessFlow();
                    doMigrateRegisteFlow();
                    doMigrateOperateFlow();
                    doMigrateRecipeFlow();

                    MainFrame.PrintMessage(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "数据迁移成功...");
                }
                catch (Exception ex)
                {
                    MainFrame.PrintMessage(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "数据迁移失败," + ex.Message);
                }
            }

            private bool doMigrateProcessFlow()
            {
                try
                {
                    int count = 0;
                    string where = "  ModDate <'" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "' ";
                    ProcessHistoryBLL phistoryBLL = new ProcessHistoryBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                    ViewTicketFlowsBLL ticketsBLL = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                    ProcessFlowsBLL pflowsBLL = new ProcessFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                    TicketFlowsBLL tflowsBLL = new TicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                    ProcessHistory phistory = null;

                    ViewTicketFlowsCollections ticketsColl = ticketsBLL.GetRecordsByPaging(ref count, 1, 1000, where);
                    if (ticketsColl != null && ticketsColl.Count > 0)
                    {
                        foreach (ViewTicketFlows ticket in ticketsColl)
                        {
                            phistory = new ProcessHistory();
                            phistory.sHFlowNo = CommonHelper.Get_New12ByteGuid(); ;
                            phistory.iDataFlag = 1;
                            phistory.sTicketNo = ticket.sTicketNo;
                            phistory.sRUserNo = ticket.sRUserNo;
                            phistory.sCnName = ticket.sCnName;
                            phistory.iAge = ticket.iAge;
                            phistory.iSex = ticket.iSex;
                            phistory.sServiceNo = ticket.sServiceNo;
                            phistory.sCounterNos = ticket.sCounterNos;
                            phistory.sWFlowsNo = ticket.sWorkFlowsNo;
                            phistory.iWFlowsIndex = ticket.iWFlowsIndex;
                            phistory.dEnqueueTime = ticket.dEnqueueTime;
                            phistory.dBeginTime = ticket.dBeginTime;
                            phistory.dFinishTime = ticket.dFinishTime;
                            phistory.iProcessState = ticket.iPauseState;
                            phistory.sProcessFormat = ticket.sProcessFormat;
                            phistory.iProcessIndex = ticket.iProcessIndex;
                            phistory.iPriorityType = ticket.iPriorityType;
                            phistory.iOrderWeight = ticket.iOrderWeight;
                            phistory.iPauseState = ticket.iPauseState;
                            phistory.iDelayType = ticket.iDelayType;
                            phistory.iDelayTimeValue = ticket.iDelayTimeValue;
                            phistory.iDelayStepValue = ticket.iDelayStepValue;
                            phistory.dProcessedTime = ticket.dProcessedTime;
                            phistory.sProcessedCounterNo = ticket.sProcessedCounterNo;
                            phistory.sProcessedStafferNo = ticket.sProcessedStafferNo;

                            phistory.sBranchNo = ticket.sBranchNo;
                            phistory.sAddOptor = ticket.sAddOptor;
                            phistory.dAddDate = ticket.dAddDate;
                            phistory.sModOptor = ticket.sModOptor;
                            phistory.dModDate = ticket.dModDate;
                            phistory.iValidityState = 1;
                            phistory.sComments = ticket.sComments;
                            phistory.sAppCode = IUserContext.GetAppCode() + "; ";

                            phistoryBLL.AddNewRecord(phistory);
                        }

                        //
                        pflowsBLL.HardDeleteByCondition(where);
                        tflowsBLL.HardDeleteByCondition(where);
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            private bool doMigrateRegisteFlow()
            {
                try
                {
                    int count = 0;
                    string where = " ModDate <'" + DateTime.Now.AddDays(-5).ToString("yyyy-MM-dd 00:00:00") + "' ";
                    RegistHistoryBLL rhistoryBLL = new RegistHistoryBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                    ViewRegistFlowsBLL registeBLL = new ViewRegistFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                    RegistFlowsBLL rflowsBLL = new RegistFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                    RegistHistory rhistory = null;

                    ViewRegistFlowsCollections registesColl = registeBLL.GetRecordsByPaging(ref count, 1, 1000, where);
                    if (registesColl != null && registesColl.Count > 0)
                    {
                        foreach (ViewRegistFlows registe in registesColl)
                        {
                            rhistory = new RegistHistory();
                            rhistory.sHFlowNo = CommonHelper.Get_New12ByteGuid();
                            rhistory.iDataFlag = 1;
                            rhistory.sTicketNo = registe.sTicketNo;
                            rhistory.sRUserNo = registe.sRUserNo;
                            rhistory.sCnName = registe.sCnName;
                            rhistory.iAge = registe.iAge;
                            rhistory.iSex = registe.iSex;
                            rhistory.iRegistType = registe.iRegistType;
                            rhistory.sDataFrom = registe.sDataFrom;
                            rhistory.dRegistDate = registe.dRegistDate;
                            rhistory.sServiceNo = registe.sServiceNo;
                            rhistory.sStafferNo = registe.sStafferNo;
                            rhistory.iWorkTime = registe.iWorkTime;
                            rhistory.dStartDate = registe.dStartDate;
                            rhistory.dEnditDate = registe.dEnditDate;
                            rhistory.iRegistState = registe.iRegistState;

                            rhistory.sBranchNo = registe.sBranchNo;
                            rhistory.sAddOptor = registe.sAddOptor;
                            rhistory.dAddDate = registe.dAddDate;
                            rhistory.sModOptor = registe.sModOptor;
                            rhistory.dModDate = registe.dModDate;
                            rhistory.iValidityState = 1;
                            rhistory.sComments = registe.sComments;
                            rhistory.sAppCode = IUserContext.GetAppCode() + "; ";

                            rhistoryBLL.AddNewRecord(rhistory);
                        }

                        //
                        rflowsBLL.HardDeleteByCondition(where);
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            private bool doMigrateOperateFlow()
            {
                try
                {
                    int count = 0;
                    string where = " ModDate <'" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "' ";
                    OperateHistoryBLL ohistoryBLL = new OperateHistoryBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                    ViewOperatFlowsBLL operateBLL = new ViewOperatFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                    OperateFlowsBLL oflowsBLL = new OperateFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                    OperateHistory ohistory = null;

                    ViewOperatFlowsCollections operatesColl = operateBLL.GetRecordsByPaging(ref count, 1, 1000, where);
                    if (operatesColl != null && operatesColl.Count > 0)
                    {
                        foreach (ViewOperatFlows operate in operatesColl)
                        {
                            ohistory = new OperateHistory();
                            ohistory.sHFlowNo = CommonHelper.Get_New12ByteGuid();
                            ohistory.iDataFlag = 1;
                            ohistory.iOperatType = operate.iOperatType;
                            ohistory.sDataFrom = operate.sDataFrom;
                            ohistory.sRUserNo = operate.sRUserNo;
                            ohistory.sCnName = operate.sCnName;
                            ohistory.iAge = operate.iAge;
                            ohistory.iSex = operate.iSex;
                            ohistory.sStafferNo = operate.sStafferNo;
                            ohistory.sStafferName = operate.sStafferNo;
                            ohistory.dRegistDate = operate.dRegistDate;
                            ohistory.dOperatTime = operate.dOperatTime;
                            ohistory.sRoomNo = operate.sRoomNo;
                            ohistory.sRoomName = operate.sRoomName;
                            ohistory.iOptIndex = operate.iOptIndex;
                            ohistory.sOptName = operate.sOptName;
                            ohistory.sOptDesc = operate.sOptDesc;
                            ohistory.sOpter1No = operate.sOpter1No;
                            ohistory.sOpter1Name = operate.sOpter1Name;
                            ohistory.sOpter2No = operate.sOpter2No;
                            ohistory.sOpter2Name = operate.sOpter2Name;
                            ohistory.sOpter3No = operate.sOpter3No;
                            ohistory.sOpter3Name = operate.sOpter3Name;
                            ohistory.sOpter4No = operate.sOpter4No;
                            ohistory.sOpter4Name = operate.sOpter4Name;
                            ohistory.sOpter5No = operate.sOpter5No;
                            ohistory.sOpter5Name = operate.sOpter5Name;
                            ohistory.iOperatState = operate.iOperatState;

                            ohistory.sBranchNo = operate.sBranchNo;
                            ohistory.sAddOptor = operate.sAddOptor;
                            ohistory.dAddDate = operate.dAddDate;
                            ohistory.sModOptor = operate.sModOptor;
                            ohistory.dModDate = operate.dModDate;
                            ohistory.iValidityState = 1;
                            ohistory.sComments = operate.sComments;
                            ohistory.sAppCode = IUserContext.GetAppCode() + "; ";

                            ohistoryBLL.AddNewRecord(ohistory);
                        }

                        //
                        oflowsBLL.HardDeleteByCondition(where);
                    }

                    return true;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            private bool doMigrateRecipeFlow()
            {
                try
                {
                    int count = 0;
                    string where = "  ModDate <'" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "' ";
                    RecipeHistoryBLL rhistoryBLL = new RecipeHistoryBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                    ViewRecipeFlowsBLL recipeBLL = new ViewRecipeFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                    RecipeFlowsBLL rflowsBLL = new RecipeFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                    RecipeHistory rhistory = null;

                    ViewRecipeFlowsCollections recipeColl = recipeBLL.GetRecordsByPaging(ref count, 1, 1000, where);
                    if (recipeColl != null && recipeColl.Count > 0)
                    {
                        foreach (ViewRecipeFlows recipe in recipeColl)
                        {
                            rhistory = new RecipeHistory();
                            rhistory.sHFlowNo = CommonHelper.Get_New12ByteGuid();
                            rhistory.iDataFlag = 1;
                            rhistory.sRegistNo = recipe.sRegistNo;
                            rhistory.sTicketNo = recipe.sTicketNo;
                            rhistory.sRUserNo = recipe.sRUserNo;
                            rhistory.sCnName = recipe.sCnName;
                            rhistory.iAge = recipe.iAge;
                            rhistory.iSex = recipe.iSex;
                            rhistory.sCounterNo = recipe.sCounterNo;
                            rhistory.dEnqueueTime = recipe.dEnqueueTime;
                            rhistory.dBeginTime = recipe.dBeginTime;
                            rhistory.dFinishTime = recipe.dFinishTime;
                            rhistory.iRecipeState = recipe.iRecipeState;
                            rhistory.sRecipeOpter = recipe.sRecipeOpter;
                            rhistory.dRecipeDate = recipe.dRecipeDate;
                            rhistory.iProcessState = recipe.iProcessState;
                            rhistory.dProcessedTime = recipe.dProcessedTime;
                            rhistory.sPrcsCounterNo = recipe.sPrcsCounterNo;
                            rhistory.sDataFrom = recipe.sDataFrom;

                            rhistory.sBranchNo = recipe.sBranchNo;
                            rhistory.sAddOptor = recipe.sAddOptor;
                            rhistory.dAddDate = recipe.dAddDate;
                            rhistory.sModOptor = recipe.sModOptor;
                            rhistory.dModDate = recipe.dModDate;
                            rhistory.iValidityState = 1;
                            rhistory.sComments = recipe.sComments;
                            rhistory.sAppCode = IUserContext.GetAppCode() + "; ";

                            rhistoryBLL.AddNewRecord(rhistory);
                        }

                        //
                        rflowsBLL.HardDeleteByCondition(where);
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        internal class ExAuthorizeJob : IJob
        {
            void IJob.Execute()
            {
                try
                { 
                    string activeDate = DateTime.Now.ToString("yyyy-MM-dd");

                    int ret = IUserContext.doCheck_EncryptDogStatus(ref activeDate);
                    if (ret != 0) 
                    {
                        ret = IUserContext.doCheck_TrialRegistStatus();
                        if (ret <0)
                        {
                            MainFrame.PrintMessage(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "软件授权已过期!");

                            //WebSocketService.CreateInstance().StopWebSocket();
                            NettyHostService.CreateInstance().StopHostService();
                            PgmTaskService.CreateInstance().StopProgramTask();
                            UpdateDataService.CreateInstance().StopUpdateTask();
                        }

                    }
                }
                catch (Exception ex)
                {
                    MainFrame.PrintMessage(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "软件授权失败," + ex.Message);
                }
            }  
        }
    }
}

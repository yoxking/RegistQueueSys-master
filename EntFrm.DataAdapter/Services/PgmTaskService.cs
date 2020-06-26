using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.DataAdapter.Entities;
using Newtonsoft.Json;
using System;
using System.Threading;

namespace EntFrm.DataAdapter.Services
{
    public class PgmTaskService
    {
        private volatile static PgmTaskService _instance = null;
        private static readonly object lockHelper = new object();

        private bool isQuitFlag = false;
        public static PgmTaskService CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new PgmTaskService();
                }
            }
            return _instance;
        }
        private PgmTaskService() { }

        public void StartProgramTask()
        {
            DsDwloadFlowsBLL dloadBLL = new DsDwloadFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
            DsPlayerInfoBLL playerBLL = new DsPlayerInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
            DsProgramInfoBLL programBLL = new DsProgramInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
            DsPublishFlowsBLL publishBLL = new DsPublishFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
            DsDwloadFlowsCollections dloadColl = null;
            DsPlayerInfo player = null;
            DsProgramInfo program = null;
            DsPublishFlows publish = null;
            ProgramData pgmtask = null;

            int count = 0;
            MainFrame.PrintMessage(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "节目发布服务启动完成...");

            while (true)
            {
                if (isQuitFlag)
                {
                    break;
                }

                try
                {
                    string sWhere = " IssueStatus=0 And IFailCount<30 And DSchedule<='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                    dloadColl = dloadBLL.GetRecordsByPaging(ref count, 1, 10, sWhere);
                    if (dloadColl != null && dloadColl.Count > 0)
                    {
                        foreach (DsDwloadFlows dload in dloadColl)
                        {
                            player = playerBLL.GetRecordByNo(dload.sPlayerNo);
                            if (IPublicHelper.getOnlineState(player.sPlayerNo) && NettyChannelMap.containChannel(player.sPlayerCode))
                            {
                                program = programBLL.GetRecordByNo(dload.sProgmNo);
                                publish = publishBLL.GetRecordByNo(dload.sPublishNo);

                                pgmtask = new ProgramData();
                                pgmtask.id = long.Parse(program.sProgmNo);
                                pgmtask.programNo = program.sProgmNo;
                                pgmtask.programName = program.sProgmName;
                                pgmtask.programType = publish.sProgmType;
                                pgmtask.pfilePath = program.sPFilePath;
                                pgmtask.pwebUrl = program.sPWebUrl;
                                pgmtask.pversion = program.iPVersion;
                                pgmtask.duration = program.iDuration + "";
                                pgmtask.resolution = program.sResolution;
                                pgmtask.playMode = publish.sPlayMode;
                                pgmtask.playWeeks = publish.sPlayWeeks;
                                pgmtask.startDate = publish.dStartDate.ToString("yyyy-MM-dd HH:mm:ss");
                                pgmtask.enditDate = publish.dEnditDate.ToString("yyyy-MM-dd HH:mm:ss");
                                pgmtask.startTime = publish.sStartTime;
                                pgmtask.enditTime = publish.sEnditTime;
                                pgmtask.playStatus = "" + 0;
                                pgmtask.playCount = 0;
                                pgmtask.dloadStatus = "" + 0;
                                pgmtask.dloadCount = 0;
                                pgmtask.pComments = "";

                                CmmdData command = new CmmdData();
                                command.cmmdName = "doPublish";
                                command.cmmdType = "MAdapter";
                                command.cmmdArgs = new string[] { JsonConvert.SerializeObject(pgmtask) };

                                string s = JsonConvert.SerializeObject(command);

                                NettyHostService.CreateInstance().SendCommandData(player.sPlayerCode, s);

                                dload.iIssueStatus = 1;
                                dload.dIssueDate = DateTime.Now;
                                dload.iISucCount++;
                                dloadBLL.UpdateRecord(dload);
                            }
                            else
                            {
                                dload.iIssueStatus = 0;
                                dload.iIFailCount++;
                                dloadBLL.UpdateRecord(dload);
                            }

                        }
                    }
                }
                catch (Exception ex) { }
                Thread.Sleep(5000);
            }
        }
         
        public void StopProgramTask()
        {
            isQuitFlag = true;
        }
    }
}

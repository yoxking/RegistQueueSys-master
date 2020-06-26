using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using System;
using System.Collections.Generic;

namespace EntWeb.HDeptConsole
{
    public class PageService
    {

        public static string GetWorkTimeType(int workTime)
        {
            string sResult = "";
            switch (workTime)
            {
                case PublicConsts.WORKTIMETYPE1:
                    sResult = "休息";
                    break;
                case PublicConsts.WORKTIMETYPE2:
                    sResult = "全天";
                    break;
                case PublicConsts.WORKTIMETYPE3:
                    sResult = "上午";
                    break;
                case PublicConsts.WORKTIMETYPE4:
                    sResult = "下午";
                    break;
                default: break;
            }
            return sResult;
        }

        public static List<ItemData> GetWorkTimeList(bool zeroFlag=false)
        {
            List<ItemData> wtimeList = new List<ItemData>();
            if (zeroFlag)
            {
                wtimeList.Add(new ItemData(PublicConsts.WORKTIMETYPE1.ToString(), "休息"));
            }
            wtimeList.Add(new ItemData(PublicConsts.WORKTIMETYPE2.ToString(), "全天"));
            wtimeList.Add(new ItemData(PublicConsts.WORKTIMETYPE3.ToString(), "上午"));
            wtimeList.Add(new ItemData(PublicConsts.WORKTIMETYPE4.ToString(), "下午"));

            return wtimeList;
        }

        public static string GetProcessState(int processState)
        {
            string sResult = "";
            switch (processState)
            {
                case PublicConsts.PROCSTATE_OUTQUEUE:
                    sResult = "未入队";
                    break;
                case PublicConsts.PROCSTATE_DIAGNOSIS:
                    sResult = "初诊";
                    break;
                case PublicConsts.PROCSTATE_TRIAGE:
                    sResult = "分诊";
                    break;
                case PublicConsts.PROCSTATE_EXCHANGE:
                    sResult = "转诊";
                    break;
                case PublicConsts.PROCSTATE_REDIAGNOSIS:
                    sResult = "复诊";
                    break;
                case PublicConsts.PROCSTATE_PASSTICKET:
                    sResult = "过号初诊";
                    break;
                case PublicConsts.PROCSTATE_DELAY:
                    sResult = "延迟";
                    break;
                case PublicConsts.PROCSTATE_WAITING:
                    sResult = "等候";
                    break;
                case PublicConsts.PROCSTATE_WAITAREA1:
                    sResult = "等候中";
                    break;
                case PublicConsts.PROCSTATE_WAITAREA2:
                    sResult = "等候中";
                    break;
                case PublicConsts.PROCSTATE_WAITAREA3:
                    sResult = "等候中";
                    break;
                case PublicConsts.PROCSTATE_CALLING:
                    sResult = "叫号中";
                    break;
                case PublicConsts.PROCSTATE_PROCESSING:
                    sResult = "就诊中";
                    break;
                case PublicConsts.PROCSTATE_FINISHED:
                    sResult = "已就诊";
                    break;
                case PublicConsts.PROCSTATE_NONARRIVAL:
                    sResult = "未到过号";
                    break;
                case PublicConsts.PROCSTATE_HANGUP:
                    sResult = "挂起";
                    break;
                case PublicConsts.PROCSTATE_GREENCHANNEL:
                    sResult = "绿色通道";
                    break;
                case PublicConsts.PROCSTATE_ARCHIVE:
                    sResult = "归档";
                    break;
                default:
                    break;
            }

            return sResult;
        }
        public static string GetPriorityType(int priorityType)
        {
            string sResult = "普通";
            switch (priorityType)
            {
                case PublicConsts.PRIORITY_TYPE1:
                    sResult = "预约";
                    break;
                case PublicConsts.PRIORITY_TYPE2:
                    sResult = "过号";
                    break;
                case PublicConsts.PRIORITY_TYPE3:
                    sResult = "军人";
                    break;
                case PublicConsts.PRIORITY_TYPE4:
                    sResult = "离休";
                    break;
                case PublicConsts.PRIORITY_TYPE5:
                    sResult = "幼儿";
                    break;
                case PublicConsts.PRIORITY_TYPE6:
                    sResult = "老人";
                    break;
                case PublicConsts.PRIORITY_TYPE7:
                    sResult = "急诊";
                    break;
                default:
                    break;
            }

            return sResult;
        }
        public static List<ItemData> GetPriorityList()
        {
            List<ItemData> priorityList = new List<ItemData>(); 
            priorityList.Add(new ItemData(PublicConsts.PRIORITY_TYPE2.ToString(), "过号优先"));
            priorityList.Add(new ItemData(PublicConsts.PRIORITY_TYPE3.ToString(), "军人优先"));
            priorityList.Add(new ItemData(PublicConsts.PRIORITY_TYPE4.ToString(), "离休优先"));
            priorityList.Add(new ItemData(PublicConsts.PRIORITY_TYPE5.ToString(), "幼儿优先"));
            priorityList.Add(new ItemData(PublicConsts.PRIORITY_TYPE6.ToString(), "老人优先"));
            priorityList.Add(new ItemData(PublicConsts.PRIORITY_TYPE7.ToString(), "急诊优先"));

            return priorityList;
        }

        public static string GetRegisteType(int registeType)
        {
            string sResult = "";
            switch (registeType)
            {
                case PublicConsts.REGISTETYPE1:
                    sResult = "现场挂号";
                    break;
                case PublicConsts.REGISTETYPE2:
                    sResult = "预约挂号";
                    break;
                default:
                    break;
            }

            return sResult;
        }

        public static string GetRotaType(int rotaType)
        {
            string sResult = "";
            switch (rotaType)
            {
                case PublicConsts.ROTATYPE1:
                    sResult = "正常排班";
                    break;
                case PublicConsts.ROTATYPE2:
                    sResult = "临时排班";
                    break;
                default:
                    break;
            }

            return sResult;
        }

        public static string GetBranchName(string sNo)
        {
            BranchInfoBLL infoBLL = new BranchInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            return infoBLL.GetRecordNameByNo(sNo);
        }

        public static string GetServiceName(string sNo)
        {
            ServiceInfoBLL infoBLL = new ServiceInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            return infoBLL.GetRecordNameByNo(sNo);
        }

        public static ServiceInfo GetServiceInfo(string sNo)
        {
            ServiceInfoBLL infoBLL = new ServiceInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            return infoBLL.GetRecordByNo(sNo);
        }

        public static string GetCounterName(string sNo)
        {
            CounterInfoBLL infoBLL = new CounterInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            return infoBLL.GetRecordNameByNo(sNo);
        }

        public static string GetCounterNosByServiceNo(string sServiceNo, string sBranchNo)
        {
            try
            {
                int count = 0;
                string sResult = "";
                CounterInfoBLL infoBoss = new CounterInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                CounterInfoCollections infoColl = infoBoss.GetRecordsByPaging(ref count, 1, 10, " BranchNo='" + sBranchNo + "' And  ServiceGroupValue Like '%" + sServiceNo + "%' ");

                if (infoColl != null && infoColl.Count > 0)
                {
                    foreach (CounterInfo info in infoColl)
                    {
                        sResult += info.sCounterNo + ";";
                    }

                    sResult.Trim(';');
                }

                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static CounterInfo GetCounterInfo(string sNo)
        {
            CounterInfoBLL infoBLL = new CounterInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            return infoBLL.GetRecordByNo(sNo);
        }

        public static List<ItemData> GetServiceList(bool bAllFlag = false)
        {
            try
            {
                int pageCount = 1;
                List<ItemData> itemList = new List<ItemData>();
                if (bAllFlag)
                {
                    itemList.Add(new ItemData("99999999", "所有业务"));
                }

                ServiceInfoBLL infoBLL = new ServiceInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ServiceInfoCollections infoColl = infoBLL.GetRecordsByPaging(ref pageCount, 1, 100, " BranchNo='" + PublicHelper.Get_BranchNo() + "' ");

                if (infoColl != null && infoColl.Count > 0)
                {
                    foreach (ServiceInfo info in infoColl)
                    {
                        itemList.Add(new ItemData(info.sServiceNo, info.sServiceName));
                    }
                }

                return itemList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string GetStafferName(string sNo)
        {
            StafferInfoBLL infoBLL = new StafferInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            return infoBLL.GetRecordNameByNo(sNo);
        }

        public static StafferInfo GetStafferInfo(string sNo)
        {
            StafferInfoBLL infoBLL = new StafferInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            return infoBLL.GetRecordByNo(sNo);
        }

        public static List<ItemData> GetStaffList(bool bAllFlag = false)
        {
            try
            {
                int pageCount = 1;
                List<ItemData> itemList = new List<ItemData>();
                if (bAllFlag)
                {
                    itemList.Add(new ItemData("99999999", "所有医生"));
                }

                StafferInfoBLL infoBLL = new StafferInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                StafferInfoCollections infoColl = infoBLL.GetRecordsByPaging(ref pageCount, 1, 1000, " BranchNo='" + PublicHelper.Get_BranchNo() + "' ");

                if (infoColl != null && infoColl.Count > 0)
                {
                    foreach (StafferInfo info in infoColl)
                    {
                        //itemList.Add(new ItemData(info.sStafferNo, info.sStafferName));
                        if (isLogon(info.sStafferNo))
                        {
                            itemList.Add(new ItemData(info.sStafferNo, info.sStafferName));
                        }
                    }
                }

                return itemList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static bool isLogon(string StafferNo)
        {
            CounterInfoBLL infoBLL = new CounterInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            int count=infoBLL.GetCountByCondition(" LogonStafferNo='"+StafferNo+"' ");
            return (count>0?true:false);
        }

        public static string GetRUserName(string sNo)
        {
            RUsersInfoBLL infoBLL = new RUsersInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            return infoBLL.GetRecordNameByNo(sNo);
        }

        public static RUsersInfo GetRUserInfo(string sNo)
        {
            RUsersInfoBLL infoBLL = new RUsersInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            return infoBLL.GetRecordByNo(sNo);
        } 
        public static RUsersInfo GetRUserInfoByRiCardNo(string sRiCardNo)
        {
            int count = 0;
            string sWhere = " RiCardNo='"+sRiCardNo+"' ";
            RUsersInfoBLL infoBLL = new RUsersInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            RUsersInfoCollections infoColl= infoBLL.GetRecordsByPaging(ref count,1,1,sWhere);

            if (infoColl != null && infoColl.Count > 0)
            {
                return infoColl.GetFirstOne();
            }

            return null;
        }
        public static string GetWAreaName(string sNo)
        {
            WaitAreaBLL infoBLL = new WaitAreaBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            return infoBLL.GetRecordNameByNo(sNo);
        }

        public static WaitArea GetWAreaInfo(string sNo)
        {
            WaitAreaBLL infoBLL = new WaitAreaBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            return infoBLL.GetRecordByNo(sNo);
        }

        public static WaitArea GetWaitAreaNoByWAreaIndex(int index)
        {
            try
            {
                int count = 0;
                WaitAreaBLL infoBoss = new WaitAreaBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                WaitAreaCollections infoColl = infoBoss.GetRecordsByPaging(ref count, 1, 1, " BranchNo='" + PublicHelper.Get_BranchNo() + "' And AreaIndex=" + index);

                if (infoColl != null && infoColl.Count > 0)
                {
                    return infoColl.GetFirstOne();
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
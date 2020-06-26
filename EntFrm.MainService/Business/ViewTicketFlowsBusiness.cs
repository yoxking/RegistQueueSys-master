using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using Newtonsoft.Json;
using System;

namespace EntFrm.MainService.Business
{
    public class ViewTicketFlowsBusiness
    {

        private volatile static ViewTicketFlowsBusiness _instance = null;
        private static readonly object lockHelper = new object();
        public static ViewTicketFlowsBusiness CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new ViewTicketFlowsBusiness();
                }
            }
            return _instance;
        }
        public string GetAllRecords()
        {
            try
            {
                string sResult = "";
                int count = 0;
                string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' ";
                ViewTicketFlowsBLL infoBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                ViewTicketFlowsCollections infoColl = infoBoss.GetRecordsByPaging(ref count, 1, 100, sWhere);
                if (infoColl != null)
                {
                    sResult = JsonConvert.SerializeObject(infoColl, Formatting.None);
                }
                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public string GetRecordByNo(string sNo)
        {
            try
            {
                string sResult = "";
                ViewTicketFlowsBLL infoBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                ViewTicketFlows info = infoBoss.GetRecordByNo(sNo);
                if (info != null)
                {
                    sResult = JsonConvert.SerializeObject(info, Formatting.None);
                }
                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public string GetRecordsByPaging(string pageIndex, string pageSize, string condition)
        {
            try
            {
                string sResult = "";
                int count = 0;
                string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' ";
                if (!string.IsNullOrEmpty(condition))
                {
                    sWhere += " And " + condition;
                }
                ViewTicketFlowsBLL infoBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                ViewTicketFlowsCollections infoColl = infoBoss.GetRecordsByPaging(ref count, int.Parse(pageIndex), int.Parse(pageSize), sWhere);
                if (infoColl != null)
                {
                    sResult = JsonConvert.SerializeObject(infoColl, Formatting.None);
                }
                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public string GetCountByCondition(string condition)
        {
            try
            {
                string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' ";
                if (!string.IsNullOrEmpty(condition))
                {
                    sWhere += " And " + condition;
                }
                ViewTicketFlowsBLL infoBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                return infoBoss.GetCountByCondition(sWhere).ToString();
            }
            catch (Exception ex)
            {
                return "0";
            }
        }
         
    }
}


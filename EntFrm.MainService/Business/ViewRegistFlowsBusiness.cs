using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using Newtonsoft.Json;
using System;

namespace EntFrm.MainService.Business
{
    public class ViewRegistFlowsBusiness
    {

    private volatile static ViewRegistFlowsBusiness _instance = null;
    private static readonly object lockHelper = new object();
    public static ViewRegistFlowsBusiness CreateInstance()
    {
    if (_instance == null)
    {
    lock (lockHelper)
    {
    if (_instance == null)
      _instance = new ViewRegistFlowsBusiness();
    }
    }
    return _instance;
    }
        public  string GetAllRecords()
        {
            try
            {
               string sResult = "";
                int count = 0;
                string sWhere = " BranchNo = '"+IUserContext.GetBranchNo()+"' ";
               ViewRegistFlowsBLL infoBoss = new ViewRegistFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
               ViewRegistFlowsCollections infoColl = infoBoss.GetRecordsByPaging(ref count, 1, 100, sWhere);
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
                  ViewRegistFlowsBLL infoBoss = new ViewRegistFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                 ViewRegistFlows info = infoBoss.GetRecordByNo(sNo);
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

        public  string GetRecordsByPaging(string pageIndex,string pageSize,string condition)
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
            ViewRegistFlowsBLL infoBoss = new ViewRegistFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
            ViewRegistFlowsCollections infoColl = infoBoss.GetRecordsByPaging(ref count, int.Parse(pageIndex), int.Parse(pageSize), sWhere);
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

        public  string GetCountByCondition(string condition)
        {
            try
            {
            string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' ";
            if (!string.IsNullOrEmpty(condition))
            {
              sWhere += " And " + condition;
            }
              ViewRegistFlowsBLL infoBoss = new ViewRegistFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
              return infoBoss.GetCountByCondition(sWhere).ToString();
            }
            catch (Exception ex)
            {
                    return "0";
            }
        } 
    }
}


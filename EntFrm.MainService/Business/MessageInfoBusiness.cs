using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using Newtonsoft.Json;
using System;

namespace EntFrm.MainService.Business
{
    public class MessageInfoBusiness
    {

        private volatile static MessageInfoBusiness _instance = null;
        private static readonly object lockHelper = new object();
        public static MessageInfoBusiness CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new MessageInfoBusiness();
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
                MessageInfoBLL infoBoss = new MessageInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                MessageInfoCollections infoColl = infoBoss.GetRecordsByPaging(ref count, 1, 100, sWhere);
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
                MessageInfoBLL infoBoss = new MessageInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                MessageInfo info = infoBoss.GetRecordByNo(sNo);
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
                MessageInfoBLL infoBoss = new MessageInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                MessageInfoCollections infoColl = infoBoss.GetRecordsByPaging(ref count, int.Parse(pageIndex), int.Parse(pageSize), sWhere);
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
                MessageInfoBLL infoBoss = new MessageInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                return infoBoss.GetCountByCondition(sWhere).ToString();
            }
            catch (Exception ex)
            {
                return "0";
            }
        }

        public string PostRecord(string sInfo)
        {
            try
            {
                string sResult = "";
                string sSuNo = "00000000";
                MessageInfo info1 = JsonConvert.DeserializeObject<MessageInfo>(sInfo);
                if (info1 != null)
                {
                    MessageInfoBLL infoBoss = new MessageInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                    MessageInfo info2 = infoBoss.GetRecordByNo(info1.sMessNo);
                    if (info2 == null)
                    {
                        info2 = info1;
                        info2.sMessNo = CommonHelper.Get_New12ByteGuid();
                        info2.sBranchNo = IUserContext.GetBranchNo();
                        info2.sAddOptor = sSuNo;
                        info2.dAddDate = DateTime.Now;
                        info2.sModOptor = sSuNo;
                        info2.dModDate = DateTime.Now;
                        info2.iValidityState = 1;
                        info2.sAppCode = IUserContext.GetAppCode() + "; ";
                        sResult = infoBoss.AddNewRecord(info2).ToString();
                    }
                    else
                    {
                        info2 = info1;
                        info2.sBranchNo = IUserContext.GetBranchNo();
                        info2.sModOptor = sSuNo;
                        info2.dModDate = DateTime.Now;
                        sResult = infoBoss.UpdateRecord(info2).ToString();
                    }
                }
                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}


using EntFrm.Business.DALFactory;
using EntFrm.Business.IDAL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using System;

namespace EntFrm.Business.BLL
{
    public class PermitInfoBLL
    {

        private string connStr;
        private string appCode;

        public void Init()
        {
        }

        public PermitInfoBLL(string sConnStr, string sAppCode)
        {
            this.connStr = sConnStr;
            this.appCode = sAppCode;
        }

        private void ValidateRequired(PermitInfo info)
        {
            /*if (info != null)
            {
                 if (info.sName.Trim().Length == 0)
                 {
                   throw new Exception("出错提示:Name不能为空;");
                 }
            }
            else{
                throw new Exception("出错提示:对象不能为空;");
            }*/
        }

        private void ValidateRepeat(PermitInfo info)
        {
            /*if (info != null)
            {
                 try
                 {
                     IPermitInfo  infoDAL = PermitInfoFactory.Create(this.connStr, this.appCode);
                    if (infoDAL.GetCountByCondition(" Name='" + info.sName.Trim() + "'") > 0)
                    {
                        throw new Exception("Name存在重复记录;");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("出错提示:" + ex.Message);
                }
            }
            else{
                throw new Exception("出错提示:对象不能为空;");
            }*/
        }

        public bool AddNewRecord(PermitInfo info)
        {
            try
            {
                //ValidateRequired(info);
                //ValidateRepeat(info);

                IPermitInfo infoDAL = PermitInfoFactory.Create(this.connStr, this.appCode);
                if (infoDAL.AddRecord(info) > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("出错提示:添加新记录(AddNewRecord|BLL)时出错;" + ex.Message);
            }
        }

        public bool UpdateRecord(PermitInfo info)
        {
            try
            {
                //ValidateRequired(info);
                IPermitInfo infoDAL = PermitInfoFactory.Create(this.connStr, this.appCode);

                if (infoDAL.UpdateRecord(info) > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("出错提示:更新记录(UpdateRecord|BLL)时出错;" + ex.Message);
            }
        }

        public bool SoftDeleteRecord(string sNo)
        {
            if (sNo.Length > 0)
            {
                try
                {
                    IPermitInfo infoDAL = PermitInfoFactory.Create(this.connStr, this.appCode);
                    if (infoDAL.SoftDeleteRecord(sNo) > 0)
                    {
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    throw new Exception("出错提示:软删除记录(SoftDeleteRecord|BLL)时出错;" + ex.Message);
                }
            }
            return false;
        }

        public bool SoftDeleteRecord(string[] sNos)
        {
            if (sNos != null && sNos.Length > 0)
            {
                try
                {
                    bool bResult = true;
                    IPermitInfo infoDAL = PermitInfoFactory.Create(this.connStr, this.appCode);
                    foreach (string sNo in sNos)
                    {
                        if (infoDAL.SoftDeleteRecord(sNo) < 0)
                        {
                            bResult = false;
                            break;
                        }
                    }
                    return bResult;
                }
                catch (Exception ex)
                {
                    throw new Exception("出错提示:软删除多记录(SoftDeleteRecord|BLL)时出错;" + ex.Message);
                }
            }
            return false;
        }

        public bool HardDeleteRecord(string sNo)
        {
            if (sNo.Length > 0)
            {
                try
                {
                    IPermitInfo infoDAL = PermitInfoFactory.Create(this.connStr, this.appCode);
                    if (infoDAL.HardDeleteRecord(sNo) > 0)
                    {
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    throw new Exception("出错提示:硬删除记录(HardDeleteRecord|BLL)时出错;" + ex.Message);
                }
            }
            return false;
        }

        public bool HardDeleteRecord(string[] sNos)
        {
            if (sNos != null && sNos.Length > 0)
            {
                try
                {
                    bool bResult = true;
                    IPermitInfo infoDAL = PermitInfoFactory.Create(this.connStr, this.appCode);
                    foreach (string sNo in sNos)
                    {
                        if (infoDAL.HardDeleteRecord(sNo) < 0)
                        {
                            bResult = false;
                            break;
                        }
                    }
                    return bResult;
                }
                catch (Exception ex)
                {
                    throw new Exception("出错提示:硬删除多记录(HardDeleteRecord|BLL)时出错;" + ex.Message);
                }
            }
            return false;
        }

        public PermitInfoCollections GetAllRecords()
        {
            try
            {
                IPermitInfo infoDAL = PermitInfoFactory.Create(this.connStr, this.appCode);
                return infoDAL.GetAllRecords();
            }
            catch (Exception ex)
            {
                throw new Exception("出错提示:查询所有记录(GetAllRecords|BLL)时出错;" + ex.Message);
            }
        }

        public PermitInfoCollections GetRecordsByClassNo(string sClassNo)
        {
            if (sClassNo.Length > 0)
            {
                try
                {
                    IPermitInfo infoDAL = PermitInfoFactory.Create(this.connStr, this.appCode);
                    return infoDAL.GetRecordsByClassNo(sClassNo);
                }
                catch (Exception ex)
                {
                    throw new Exception("出错提示:通过ClassNo查询记录(GetRecordsByClassNo|BLL)时出错;" + ex.Message);
                }
            }
            return null;
        }

        public PermitInfo GetRecordByCodeNo(string sCodeNo)
        {
            if (sCodeNo.Length > 0)
            {
                try
                {
                    IPermitInfo infoDAL = PermitInfoFactory.Create(this.connStr, this.appCode);
                    return infoDAL.GetRecordsByCodeNo(sCodeNo).GetFirstOne();
                }
                catch (Exception ex)
                {
                    throw new Exception("出错提示:通过CodeNo查询记录(GetRecordsByCodeNo|BLL)时出错;" + ex.Message);
                }
            }
            return null;
        }

        public PermitInfo GetRecordByNo(string sNo)
        {
            if (sNo.Length > 0)
            {
                try
                {
                    IPermitInfo infoDAL = PermitInfoFactory.Create(this.connStr, this.appCode);
                    PermitInfoCollections infoColl = infoDAL.GetRecordsByNo(sNo);
                    if (infoColl != null && infoColl.Count > 0)
                    {
                        return infoDAL.GetRecordsByNo(sNo).GetFirstOne();
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    throw new Exception("出错提示:通过No查询记录(GetRecordsByNo|BLL)时出错;" + ex.Message);
                }
            }
            return null;
        }

        public string GetRecordNameByNo(string sNo)
        {
            if (sNo.Length > 0)
            {
                try
                {
                    IPermitInfo infoDAL = PermitInfoFactory.Create(this.connStr, this.appCode);
                    return infoDAL.GetRecordNameByNo(sNo);
                }
                catch (Exception ex)
                {
                    throw new Exception("出错提示:通过No查询名称(GetRecordNameByNo|BLL)时出错;" + ex.Message);
                }
            }
            return "";
        }

        public PermitInfoCollections GetRecordsByPaging(ref int pageCount, int pageIndex, int pageSize, string condition)
        {
            try
            {
                IPermitInfo infoDAL = PermitInfoFactory.Create(this.connStr, this.appCode);
                int iPageSize = pageSize > 0 ? pageSize : 10;
                int iPageIndex = pageIndex;
                int iRCount = infoDAL.GetCountByCondition(condition);
                int iPageCount = CommonHelper.GetRoundingDevision(iRCount, iPageSize);
                pageCount = iPageCount;

                if (iPageCount < 1)
                {
                    iPageCount = 1;
                }

                if (iPageIndex < 1)
                {
                    iPageIndex = 1;
                }
                else if (iPageIndex > iPageCount)
                {
                    iPageIndex = iPageCount;
                }

                SqlModel s_model = new SqlModel();

                s_model.iPageNo = iPageIndex;
                s_model.iPageSize = iPageSize;
                s_model.sFields = " * ";
                s_model.sCondition = condition;
                s_model.sOrderField = "OrderNo";
                s_model.sOrderType = "asc";
                s_model.sTableName = "PermitInfo";

                return infoDAL.GetRecords_Paging(s_model);
            }
            catch (Exception ex)
            {
                throw new Exception("出错提示:分页查询记录(GetRecords_Paging|BLL)时出错;" + ex.Message);
            }
        }

        public PermitInfoCollections GetRecordsByPaging(SqlModel s_model)
        {
            if (s_model != null)
            {
                try
                {
                    IPermitInfo infoDAL = PermitInfoFactory.Create(this.connStr, this.appCode);
                    return infoDAL.GetRecords_Paging(s_model);
                }
                catch (Exception ex)
                {
                    throw new Exception("出错提示:分页查询记录(GetRecords_Paging|BLL)时出错;" + ex.Message);
                }
            }
            return null;
        }

        public int GetCountByCondition(string sCondition)
        {
            try
            {
                IPermitInfo infoDAL = PermitInfoFactory.Create(this.connStr, this.appCode);
                return infoDAL.GetCountByCondition(sCondition);
            }
            catch (Exception ex)
            {
                throw new Exception("出错提示:计算记录个数(GetCountByCondition|BLL)时出错;" + ex.Message);
            }
        }

        public PermitInfoCollections GetRecordsByUserNo(string sUserNo, string sParentPermitNo)
        {
            try
            {
                if (sUserNo == "" || sParentPermitNo == "")
                {
                    return null;
                }

                IPermitInfo infoDAL = PermitInfoFactory.Create(this.connStr, this.appCode);
                return infoDAL.GetRecordsByUserNo(sUserNo, sParentPermitNo);
            }
            catch (Exception ex)
            {
                throw new Exception(" 通过用户编号检索记录(BLL层)时出错;" + ex.Message);
            }
        }


        public PermitInfoCollections GetAllRecordsByParentNoOrder(string sParentNo, string sSplitStr)
        {
            try
            {
                if (sParentNo == "")
                {
                    return null;
                }

                PermitInfoCollections dInfoColl = new PermitInfoCollections();
                IPermitInfo infoDAL = PermitInfoFactory.Create(this.connStr, this.appCode);
                PermitInfoCollections infoColl = infoDAL.GetRecordsByClassNo(sParentNo);

                GetRecords_By_ParentNo(infoColl, ref dInfoColl, 0, sSplitStr);
                return dInfoColl;
            }
            catch (Exception ex)
            {
                throw new Exception("出错提示:按父编号检索记录(GetAllRecordsByParentNoOrder|BLL)时出错;" + ex.Message);
            }
        }

        private void GetRecords_By_ParentNo(PermitInfoCollections pInfoColl, ref PermitInfoCollections dInfoColl, int iDepth, string sSplitStr)
        {
            try
            {
                if (pInfoColl == null)
                {
                    return;
                }
                else
                {
                    for (int i = 0; i < pInfoColl.Count; i++)
                    {
                        string strEmtpy = "";
                        for (int j = 0; j < iDepth; j++)
                        {
                            strEmtpy += sSplitStr;
                        }

                        pInfoColl[i].sPermitName = strEmtpy + pInfoColl[i].sPermitName;
                        dInfoColl.Add(pInfoColl[i]);
                        IPermitInfo infoDAL = PermitInfoFactory.Create(this.connStr, this.appCode);
                        PermitInfoCollections InfoColl = infoDAL.GetRecordsByClassNo(pInfoColl[i].sPermitCode);
                        GetRecords_By_ParentNo(InfoColl, ref dInfoColl, iDepth + 1, sSplitStr);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("出错提示:按父编号检索记录(GetRecords_By_ParentNo|BLL)时出错;" + ex.Message);
            }
        }
    }
}

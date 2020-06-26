using EntFrm.Framework.Utility;
using EntFrm.Business.DALFactory;
using EntFrm.Business.IDAL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using System;

namespace EntFrm.Business.BLL
{
    public class ServiceInfoBLL
    {

        private string connStr;
        private string appCode;

        public  ServiceInfoBLL(string sConnStr, string sAppCode)
        {
           this.connStr = sConnStr;
           this.appCode = sAppCode;
        }

        private  bool ValidateRepeat(string sNo)
        {
        if (!string.IsNullOrEmpty(sNo))
        {
           try
            {
               IServiceInfo infoDAL = ServiceInfoFactory.Create(this.connStr, this.appCode);
               if (infoDAL.GetCountByCondition(" ServiceNo ='" + sNo + "'") > 0)
               {
                   return true;
               }
               return false;
            }
            catch (Exception ex)
           {
               return false;
           }
        }
        else
        {
           return false;
        }
        }

        public  bool AddNewRecord(ServiceInfo info)
        {
            try
            {
            if (!ValidateRepeat(info.sServiceNo))
            {
                IServiceInfo infoDAL = ServiceInfoFactory.Create(this.connStr, this.appCode);
                if (infoDAL.AddNewRecord(info) > 0)
                {
                    return true;
                }
                return false;
            }
            return false;
            }
            catch (Exception ex)
            {
                throw new Exception("出错提示:添加新记录(AddNewRecord|BLL)时出错;" + ex.Message);
            }
        }

        public  bool UpdateRecord(ServiceInfo info)
        {
            try
            {
                     IServiceInfo  infoDAL = ServiceInfoFactory.Create(this.connStr, this.appCode);

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

        public  bool SoftDeleteRecord(string sNo)
        {
            if (sNo.Length > 0)
            {
                try
                {
                     IServiceInfo  infoDAL = ServiceInfoFactory.Create(this.connStr, this.appCode);
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

        public bool HardDeleteRecord(string sNo)
        {
            if (sNo.Length > 0)
            {
                try
                {
                    IServiceInfo  infoDAL = ServiceInfoFactory.Create(this.connStr, this.appCode);
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

        public bool SoftDeleteRecord(string[] sNos)
        {
            if (sNos != null && sNos.Length > 0)
           {
               try
               {
                   bool bResult = true;
                   IServiceInfo infoDAL = ServiceInfoFactory.Create(this.connStr, this.appCode);
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
                  throw new Exception("出错提示:软删除多记录(SoftDeleteRecord|BLL)时出错; " + ex.Message);
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
                   IServiceInfo infoDAL = ServiceInfoFactory.Create(this.connStr, this.appCode);
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
                  throw new Exception("出错提示:硬删除多记录(HardDeleteRecord|BLL)时出错; " + ex.Message);
              }
          }
           return false;
        }
public bool SoftDeleteByCondition(string sCondition)
{
    try
    {
        IServiceInfo infoDAL = ServiceInfoFactory.Create(this.connStr, this.appCode);
        if (infoDAL.SoftDeleteByCondition(sCondition) > 0)
        {
            return true;
       }
       return false;
    }
    catch (Exception ex)
    {
        throw new Exception("出错提示:软删除记录(SoftDeleteRecord|BLL)时出错; " + ex.Message);
    }
}
public bool HardDeleteByCondition(string sCondition)
{
    try
    {
        IServiceInfo infoDAL = ServiceInfoFactory.Create(this.connStr, this.appCode);
        if (infoDAL.HardDeleteByCondition(sCondition) > 0)
        {
            return true;
        }
        return false;
    }
    catch (Exception ex)
    {
        throw new Exception("出错提示:软删除记录(SoftDeleteRecord|BLL)时出错; " + ex.Message);
    }
}
        public   ServiceInfoCollections GetAllRecords()
        {
            try
            {
                IServiceInfo  infoDAL = ServiceInfoFactory.Create(this.connStr, this.appCode);
                return infoDAL.GetAllRecords();
            }
            catch (Exception ex)
            {
               throw new Exception("出错提示:查询所有记录(GetAllRecords|BLL)时出错;" + ex.Message);
            }
        }

        public ServiceInfoCollections GetRecordsByClassNo(string sClassNo)
        {
            if (sClassNo.Length > 0)
            {
                try
                {
                  IServiceInfo  infoDAL = ServiceInfoFactory.Create(this.connStr, this.appCode);
                    return infoDAL.GetRecordsByClassNo(sClassNo);
                }
                catch (Exception ex)
                {
                    throw new Exception("出错提示:通过ClassNo查询记录(GetRecordsByClassNo|BLL)时出错;" + ex.Message);
                }
            }
            return null ;
        }

        public  ServiceInfo GetRecordByNo(string sNo)
        {
            if (sNo.Length > 0)
            {
                try
                {
                IServiceInfo infoDAL = ServiceInfoFactory.Create(this.connStr, this.appCode);
                ServiceInfoCollections infoList = infoDAL.GetRecordsByNo(sNo);
                if (infoList != null && infoList.Count > 0)
                {
                    return infoList.GetFirstOne();
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

        public  string GetRecordNameByNo(string sNo)
        {
            if (sNo.Length > 0)
            {
                try
                {
                  IServiceInfo  infoDAL = ServiceInfoFactory.Create(this.connStr, this.appCode);
                    return infoDAL.GetRecordNameByNo(sNo);
                }
                catch (Exception ex)
                {
                    throw new Exception("出错提示:通过No查询名称(GetRecordNameByNo|BLL)时出错;" + ex.Message);
                }
            }
            return "";
        }

        public ServiceInfoCollections GetRecordsByPaging(ref int pageCount, int pageIndex, int pageSize, string condition)
        {
           try
           {
               IServiceInfo infoDAL = ServiceInfoFactory.Create(this.connStr, this.appCode);
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
            s_model.sOrderField = "ID";
            s_model.sOrderType = "desc";
             s_model.sTableName = "ServiceInfo";
             return infoDAL.GetRecords_Paging(s_model);
         }
         catch (Exception ex)
         {
             throw new Exception("出错提示:分页查询记录(GetRecords_Paging|BLL)时出错; " + ex.Message);
         }
         }
        public ServiceInfoCollections GetRecordsByPaging(SqlModel s_model)
        {
            if (s_model != null)
            {
                try
               {
                   IServiceInfo infoDAL = ServiceInfoFactory.Create(this.connStr, this.appCode);
                   return infoDAL.GetRecords_Paging(s_model);
                }
               catch (Exception ex)
              {
                  throw new Exception("出错提示:分页查询记录(GetRecords_Paging|BLL)时出错; " + ex.Message);
              }
           }
           return null;
         }
        public  int GetCountByCondition(string sCondition)
        {
            try
            {
                  IServiceInfo  infoDAL = ServiceInfoFactory.Create(this.connStr, this.appCode);
                return infoDAL.GetCountByCondition(sCondition);
            }
            catch (Exception ex)
            {
                throw new Exception("出错提示:计算记录个数(GetCountByCondition|BLL)时出错;" + ex.Message);
            }
        }



        public ServiceInfoCollections GetAllRecordsByParentNoOrder(string sParentNo, string sSplitStr, string sBranchNo)
        {
            try
            {
                if (sParentNo == "")
                {
                    return null;
                }

                SqlModel s_model = new SqlModel();
                s_model.sTableName = "ServiceInfo";
                s_model.iPageNo = 1;
                s_model.iPageSize = 100;
                s_model.sFields = "*";
                s_model.sOrderField = "ID";
                s_model.sOrderType = "Asc";
                s_model.sCondition = "  ParentNo='" + sParentNo + "' And BranchNo='" + sBranchNo + "' ";

                ServiceInfoCollections dInfoColl = new ServiceInfoCollections();

                IServiceInfo infoDAL = ServiceInfoFactory.Create(this.connStr, this.appCode);
                ServiceInfoCollections infoColl = infoDAL.GetRecords_Paging(s_model);

                GetRecordsByParentNo(infoColl, ref dInfoColl, 0, sSplitStr);
                return dInfoColl;
            }
            catch (Exception ex)
            {
                throw new Exception(" 按父编号检索记录(BLL层)时出错;" + ex.Message);
            }
        }

        private void GetRecordsByParentNo(ServiceInfoCollections pInfoColl, ref ServiceInfoCollections dInfoColl, int iDepth, string sSplitStr)
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

                        pInfoColl[i].sServiceName = strEmtpy + pInfoColl[i].sServiceName;
                        dInfoColl.Add(pInfoColl[i]);

                        IServiceInfo infoDAL = ServiceInfoFactory.Create(this.connStr, this.appCode);
                        ServiceInfoCollections InfoColl = infoDAL.GetRecordsByClassNo(pInfoColl[i].sServiceNo);
                        GetRecordsByParentNo(InfoColl, ref dInfoColl, iDepth + 1, sSplitStr);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(" 按父编号检索记录(BLL层)时出错;" + ex.Message);
            }
        }
    }
}


using EntFrm.Framework.Utility;
using EntFrm.Business.DALFactory;
using EntFrm.Business.IDAL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using System;

namespace EntFrm.Business.BLL
{
    public class ContentClassBLL
    {

        private string connStr;
        private string appCode;

        public  ContentClassBLL(string sConnStr, string sAppCode)
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
               IContentClass infoDAL = ContentClassFactory.Create(this.connStr, this.appCode);
               if (infoDAL.GetCountByCondition(" ClassNo ='" + sNo + "'") > 0)
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

        public  bool AddNewRecord(ContentClass info)
        {
            try
            {
            if (!ValidateRepeat(info.sClassNo))
            {
                IContentClass infoDAL = ContentClassFactory.Create(this.connStr, this.appCode);
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

        public  bool UpdateRecord(ContentClass info)
        {
            try
            {
                     IContentClass  infoDAL = ContentClassFactory.Create(this.connStr, this.appCode);

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
                     IContentClass  infoDAL = ContentClassFactory.Create(this.connStr, this.appCode);
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
                    IContentClass  infoDAL = ContentClassFactory.Create(this.connStr, this.appCode);
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
                   IContentClass infoDAL = ContentClassFactory.Create(this.connStr, this.appCode);
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
                   IContentClass infoDAL = ContentClassFactory.Create(this.connStr, this.appCode);
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
        IContentClass infoDAL = ContentClassFactory.Create(this.connStr, this.appCode);
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
        IContentClass infoDAL = ContentClassFactory.Create(this.connStr, this.appCode);
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
        public   ContentClassCollections GetAllRecords()
        {
            try
            {
                IContentClass  infoDAL = ContentClassFactory.Create(this.connStr, this.appCode);
                return infoDAL.GetAllRecords();
            }
            catch (Exception ex)
            {
               throw new Exception("出错提示:查询所有记录(GetAllRecords|BLL)时出错;" + ex.Message);
            }
        }

        public ContentClassCollections GetRecordsByClassNo(string sClassNo)
        {
            if (sClassNo.Length > 0)
            {
                try
                {
                  IContentClass  infoDAL = ContentClassFactory.Create(this.connStr, this.appCode);
                    return infoDAL.GetRecordsByClassNo(sClassNo);
                }
                catch (Exception ex)
                {
                    throw new Exception("出错提示:通过ClassNo查询记录(GetRecordsByClassNo|BLL)时出错;" + ex.Message);
                }
            }
            return null ;
        }

        public  ContentClass GetRecordByNo(string sNo)
        {
            if (sNo.Length > 0)
            {
                try
                {
                IContentClass infoDAL = ContentClassFactory.Create(this.connStr, this.appCode);
                ContentClassCollections infoList = infoDAL.GetRecordsByNo(sNo);
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
                  IContentClass  infoDAL = ContentClassFactory.Create(this.connStr, this.appCode);
                    return infoDAL.GetRecordNameByNo(sNo);
                }
                catch (Exception ex)
                {
                    throw new Exception("出错提示:通过No查询名称(GetRecordNameByNo|BLL)时出错;" + ex.Message);
                }
            }
            return "";
        }

        public ContentClassCollections GetRecordsByPaging(ref int pageCount, int pageIndex, int pageSize, string condition)
        {
           try
           {
               IContentClass infoDAL = ContentClassFactory.Create(this.connStr, this.appCode);
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
             s_model.sTableName = "ContentClass";
             return infoDAL.GetRecords_Paging(s_model);
         }
         catch (Exception ex)
         {
             throw new Exception("出错提示:分页查询记录(GetRecords_Paging|BLL)时出错; " + ex.Message);
         }
         }
        public ContentClassCollections GetRecordsByPaging(SqlModel s_model)
        {
            if (s_model != null)
            {
                try
               {
                   IContentClass infoDAL = ContentClassFactory.Create(this.connStr, this.appCode);
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
                  IContentClass  infoDAL = ContentClassFactory.Create(this.connStr, this.appCode);
                return infoDAL.GetCountByCondition(sCondition);
            }
            catch (Exception ex)
            {
                throw new Exception("出错提示:计算记录个数(GetCountByCondition|BLL)时出错;" + ex.Message);
            }
        }



        public ContentClassCollections GetAllRecordsByParentNoOrder(string sParentNo, string sSplitStr)
        {
            try
            {
                if (sParentNo == "")
                {
                    return null;
                }

                ContentClassCollections dInfoColl = new ContentClassCollections();
                IContentClass infoDAL = ContentClassFactory.Create(this.connStr, this.appCode);
                ContentClassCollections infoColl = infoDAL.GetRecordsByClassNo(sParentNo);

                GetRecords_By_ParentNo(infoColl, ref dInfoColl, 0, sSplitStr);
                return dInfoColl;
            }
            catch (Exception ex)
            {
                throw new Exception("出错提示:按父编号检索记录(GetAllRecordsByParentNoOrder|BLL)时出错;" + ex.Message);
            }
        }

        private void GetRecords_By_ParentNo(ContentClassCollections pInfoColl, ref ContentClassCollections dInfoColl, int iDepth, string sSplitStr)
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

                        pInfoColl[i].sClassName = strEmtpy + pInfoColl[i].sClassName;
                        dInfoColl.Add(pInfoColl[i]);
                        IContentClass infoDAL = ContentClassFactory.Create(this.connStr, this.appCode);
                        ContentClassCollections InfoColl = infoDAL.GetRecordsByClassNo(pInfoColl[i].sClassNo);
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


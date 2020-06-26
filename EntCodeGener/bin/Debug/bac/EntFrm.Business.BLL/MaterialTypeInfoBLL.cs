using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntFrm.Framework.Utility;
using EntFrm.System.Model;
using EntFrm.System.Model.Collections;
using EntFrm.System.IDAL;
using EntFrm.System.DALFactory;

namespace EntFrm.Business.BLL
{
    public class MaterialTypeInfoBLL
    {

        private string connStr;
        private string appCode;

        public  void Init()
        {
        }

        public  MaterialTypeInfoBLL(string sConnStr, string sAppCode)
        {
           this.connStr = sConnStr;
           this.appCode = sAppCode;
        }

        private  void ValidateRequired(MaterialTypeInfo info)
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

        private  void ValidateRepeat(MaterialTypeInfo  info)
        {
            /*if (info != null)
            {
                 try
                 {
                     IMaterialTypeInfo  infoDAL = MaterialTypeInfoFactory.Create(this.connStr, this.appCode);
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

        public  bool AddNewRecord(MaterialTypeInfo info)
        {
            try
            {
                 //ValidateRequired(info);
                 //ValidateRepeat(info);

                IMaterialTypeInfo  infoDAL = MaterialTypeInfoFactory.Create(this.connStr, this.appCode);
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

        public  bool UpdateRecord(MaterialTypeInfo info)
        {
            try
            {
                 //ValidateRequired(info);
                     IMaterialTypeInfo  infoDAL = MaterialTypeInfoFactory.Create(this.connStr, this.appCode);

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
                     IMaterialTypeInfo  infoDAL = MaterialTypeInfoFactory.Create(this.connStr, this.appCode);
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

        public  bool SoftDeleteRecord(string[] sNos)
        {
            if (sNos != null && sNos.Length > 0)
            {
                try
                {
                     bool bResult = true;
                     IMaterialTypeInfo  infoDAL = MaterialTypeInfoFactory.Create(this.connStr, this.appCode);
                     foreach (string sNo in sNos)
                     {
                         if (infoDAL.SoftDeleteRecord(sNo) < 0)
                         {
                             bResult=false;
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
                    IMaterialTypeInfo  infoDAL = MaterialTypeInfoFactory.Create(this.connStr, this.appCode);
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
                    IMaterialTypeInfo  infoDAL = MaterialTypeInfoFactory.Create(this.connStr, this.appCode);
                    foreach (string sNo in sNos)
                    {
                         if (infoDAL.HardDeleteRecord(sNo) < 0)
                         {
                            bResult=false;
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

        public   MaterialTypeInfoCollections GetAllRecords()
        {
            try
            {
                IMaterialTypeInfo  infoDAL = MaterialTypeInfoFactory.Create(this.connStr, this.appCode);
                return infoDAL.GetAllRecords();
            }
            catch (Exception ex)
            {
               throw new Exception("出错提示:查询所有记录(GetAllRecords|BLL)时出错;" + ex.Message);
            }
        }

        public MaterialTypeInfoCollections GetRecordsByClassNo(string sClassNo)
        {
            if (sClassNo.Length > 0)
            {
                try
                {
                  IMaterialTypeInfo  infoDAL = MaterialTypeInfoFactory.Create(this.connStr, this.appCode);
                    return infoDAL.GetRecordsByClassNo(sClassNo);
                }
                catch (Exception ex)
                {
                    throw new Exception("出错提示:通过ClassNo查询记录(GetRecordsByClassNo|BLL)时出错;" + ex.Message);
                }
            }
            return null ;
        }

        public  MaterialTypeInfo GetRecordByNo(string sNo)
        {
            if (sNo.Length > 0)
            {
                try
                {
                  IMaterialTypeInfo  infoDAL = MaterialTypeInfoFactory.Create(this.connStr, this.appCode);
                  MaterialTypeInfoCollections infoColl = infoDAL.GetRecordsByNo(sNo);
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

        public  string GetRecordNameByNo(string sNo)
        {
            if (sNo.Length > 0)
            {
                try
                {
                  IMaterialTypeInfo  infoDAL = MaterialTypeInfoFactory.Create(this.connStr, this.appCode);
                    return infoDAL.GetRecordNameByNo(sNo);
                }
                catch (Exception ex)
                {
                    throw new Exception("出错提示:通过No查询名称(GetRecordNameByNo|BLL)时出错;" + ex.Message);
                }
            }
            return "";
        }

        public  MaterialTypeInfoCollections GetRecordsByPaging(ref int pageCount,int pageIndex,int pageSize,string condition)
        {
          try
          {
             IMaterialTypeInfo  infoDAL = MaterialTypeInfoFactory.Create(this.connStr, this.appCode);
             int iPageSize = pageSize>0?pageSize:10;
             int iPageIndex = pageIndex;
             int iRCount = infoDAL.GetCountByCondition(condition);
             int iPageCount = CommonHelper.GetRoundingDevision(iRCount , iPageSize);
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
           s_model.sTableName = "MaterialTypeInfo";

           return infoDAL.GetRecords_Paging(s_model);
          }
         catch (Exception ex)
         {
              throw new Exception("出错提示:分页查询记录(GetRecords_Paging|BLL)时出错;" + ex.Message);
          }
  }

        public  MaterialTypeInfoCollections GetRecordsByPaging(SqlModel s_model)
        {
            if (s_model!=null)
            {
                try
                {
                  IMaterialTypeInfo  infoDAL = MaterialTypeInfoFactory.Create(this.connStr, this.appCode);
                    return infoDAL.GetRecords_Paging(s_model);
                }
                catch (Exception ex)
                {
                    throw new Exception("出错提示:分页查询记录(GetRecords_Paging|BLL)时出错;" + ex.Message);
                }
            }
            return null;
        }

        public  int GetCountByCondition(string sCondition)
        {
            try
            {
                  IMaterialTypeInfo  infoDAL = MaterialTypeInfoFactory.Create(this.connStr, this.appCode);
                return infoDAL.GetCountByCondition(sCondition);
            }
            catch (Exception ex)
            {
                throw new Exception("出错提示:计算记录个数(GetCountByCondition|BLL)时出错;" + ex.Message);
            }
        }
    }
}


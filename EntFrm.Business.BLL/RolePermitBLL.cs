using EntFrm.Business.DALFactory;
using EntFrm.Business.IDAL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using System;

namespace EntFrm.Business.BLL
{
    public class RolePermitBLL
    {

        private string connStr;
        private string appCode;

        public RolePermitBLL(string sConnStr, string sAppCode)
        {
            this.connStr = sConnStr;
            this.appCode = sAppCode;
        }

        public RolePermitCollections GetRecordsByRoleNo(string sNo)
        {
            try
            {
                if (sNo == "" || sNo == null)
                {
                    return null;
                }

                IRolePermit infoDAL = RolePermitFactory.Create(this.connStr, this.appCode);
                return infoDAL.GetRecordsByRoleNo(sNo);
            }
            catch (Exception ex)
            {
                throw new Exception(" 通过No检索记录(BLL层)时出错;" + ex.Message);
            }
        }

        public RolePermitCollections GetRecordsByPermitNo(string sNo)
        {
            try
            {
                if (sNo == "" || sNo == null)
                {
                    return null;
                }

                IRolePermit infoDAL = RolePermitFactory.Create(this.connStr, this.appCode);
                return infoDAL.GetRecordsByPermitNo(sNo);
            }
            catch (Exception ex)
            {
                throw new Exception(" 通过No检索记录名称(BLL层)时出错;" + ex.Message);
            }
        }

        public RolePermitCollections GetRecordsByRoleNoAndPermitNo(string sRoleNo, string sPermitNo)
        {
            try
            {
                if (sRoleNo == "" || sRoleNo == null || sPermitNo == "" || sPermitNo == null)
                {
                    return null;
                }

                IRolePermit infoDAL = RolePermitFactory.Create(this.connStr, this.appCode);
                return infoDAL.GetRecordsByRoleNoAndPermitNo(sRoleNo, sPermitNo);
            }
            catch (Exception ex)
            {
                throw new Exception(" 通过RoleNo和PermitNo检索记录名称(BLL层)时出错;" + ex.Message);
            }
        }

        //public bool GetRecord_ByRPNo(string sRoleNo, string sPermitNo)
        //{
        //    try
        //    {
        //        if (sRoleNo == "" || sPermitNo == "")
        //        {
        //            return false;
        //        }

        //        IRolePermit infoDAL = RolePermitFactory.Create(this.connStr, this.appCode);
        //        RolePermitCollections infoColl = infoDAL.GetRecordsByRoleNoAndPermitNo(sRoleNo, sPermitNo);

        //        return infoColl == null ? false : true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(" 通过RoleNo和PermitNo检索记录名称(BLL层)时出错;" + ex.Message);
        //    }
        //}

        public int HardDeleteRecord(string sRoleNo, string sPermitNo)
        {
            try
            {
                if (sRoleNo == "" || sRoleNo == null || sPermitNo == "" || sPermitNo == null)
                {
                    return 0;
                }

                IRolePermit infoDAL = RolePermitFactory.Create(this.connStr, this.appCode);
                return infoDAL.HardDeleteRecord(sRoleNo, sPermitNo);
            }
            catch (Exception ex)
            {
                throw new Exception("按RoleNo删除(BLL层)记录时出错;" + ex.Message);
            }
        }

        public int DeleteRecordByRoleNo(string sNo)
        {
            try
            {
                if (sNo == "" || sNo == null)
                {
                    return 0;
                }

                IRolePermit infoDAL = RolePermitFactory.Create(this.connStr, this.appCode);
                return infoDAL.DeleteRecordByRoleNo(sNo);
            }
            catch (Exception ex)
            {
                throw new Exception("按RoleNo删除(BLL层)记录时出错;" + ex.Message);
            }
        }

        public int DeleteRecordByPermitNo(string sNo)
        {
            try
            {
                if (sNo == "" || sNo == null)
                {
                    return 0;
                }

                IRolePermit infoDAL = RolePermitFactory.Create(this.connStr, this.appCode);
                return infoDAL.DeleteRecordByPermitNo(sNo);
            }
            catch (Exception ex)
            {
                throw new Exception("按PermitNo删除(BLL层)记录时出错;" + ex.Message);
            }
        }

        public int AddNewRecord(RolePermit info)
        {
            try
            {

                IRolePermit infoDAL = RolePermitFactory.Create(this.connStr, this.appCode);
                return infoDAL.AddRecord(info);
            }
            catch (Exception ex)
            {
                throw new Exception("出错提示:添加新记录(AddNewRecord|BLL)时出错;" + ex.Message);
            }
        }

        public int UpdateRecord(RolePermit info)
        {
            try
            {

                IRolePermit infoDAL = RolePermitFactory.Create(this.connStr, this.appCode);
                return infoDAL.UpdateRecord(info);
            }
            catch (Exception ex)
            {
                throw new Exception("出错提示:更新记录(UpdateRecord|BLL)时出错;" + ex.Message);
            }
        }

        public RolePermitCollections GetAllRecords()
        {
            try
            {
                IRolePermit infoDAL = RolePermitFactory.Create(this.connStr, this.appCode);
                return infoDAL.GetAllRecords();
            }
            catch (Exception ex)
            {
                throw new Exception("出错提示:检索所有记录(GetAllRecords|BLL)时出错;" + ex.Message);
            }
        }

        public RolePermitCollections GetRecords_Paging(SqlModel s_model)
        {
            if (s_model != null)
            {
                try
                {
                    IRolePermit infoDAL = RolePermitFactory.Create(this.connStr, this.appCode);
                    return infoDAL.GetRecords_Paging(s_model);
                }
                catch (Exception ex)
                {
                    throw new Exception("出错提示:分页检索记录(GetRecords_Paging|BLL)时出错;" + ex.Message);
                }
            }
            return null;
        }

        public int GetCountByCondition(string sCondition)
        {
            try
            {
                IRolePermit infoDAL = RolePermitFactory.Create(this.connStr, this.appCode);
                return infoDAL.GetCountByCondition(sCondition);
            }
            catch (Exception ex)
            {
                throw new Exception("出错提示:分页检索记录(GetRecords_Paging|BLL)时出错;" + ex.Message);
            }
        }
    }
}
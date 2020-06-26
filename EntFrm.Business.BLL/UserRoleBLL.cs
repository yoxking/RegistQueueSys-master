using EntFrm.Business.DALFactory;
using EntFrm.Business.IDAL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using System;

namespace EntFrm.Business.BLL
{
    public class UserRoleBLL
    {

        private string connStr;
        private string appCode;

        public UserRoleBLL(string sConnStr, string sAppCode)
        {
            this.connStr = sConnStr;
            this.appCode = sAppCode;
        }

        public UserRoleCollections GetRecordsByUserNo(string sNo)
        {
            try
            {
                if (sNo == "" || sNo == null)
                {
                    return null;
                }

                IUserRole infoDAL = UserRoleFactory.Create(this.connStr, this.appCode);
                return infoDAL.GetRecordsByUserNo(sNo);
            }
            catch (Exception ex)
            {
                throw new Exception(" 通过No检索记录(BLL层)时出错;" + ex.Message);
            }
        }

        public UserRoleCollections GetRecordsByRoleNo(string sNo)
        {
            try
            {
                if (sNo == "" || sNo == null)
                {
                    return null;
                }

                IUserRole infoDAL = UserRoleFactory.Create(this.connStr, this.appCode);
                return infoDAL.GetRecordsByRoleNo(sNo);
            }
            catch (Exception ex)
            {
                throw new Exception(" 通过No检索记录名称(BLL层)时出错;" + ex.Message);
            }
        }

        public UserRoleCollections GetRecordsByUserNoAndRoleNo(string sUserNo, string sRoleNo)
        {
            try
            {
                if (sRoleNo == "" || sRoleNo == null || sUserNo == "" || sUserNo == null)
                {
                    return null;
                }

                IUserRole infoDAL = UserRoleFactory.Create(this.connStr, this.appCode);
                return infoDAL.GetRecordsByUserNoAndRoleNo(sUserNo, sRoleNo);
            }
            catch (Exception ex)
            {
                throw new Exception(" 通过RoleNo和PermitNo检索记录名称(BLL层)时出错;" + ex.Message);
            }
        }

        public int HardDeleteRecord(string sUserNo, string sRoleNo)
        {
            try
            {
                if (sRoleNo == "" || sRoleNo == null || sUserNo == "" || sUserNo == null)
                {
                    return 0;
                }

                IUserRole infoDAL = UserRoleFactory.Create(this.connStr, this.appCode);
                return infoDAL.HardDeleteRecord(sUserNo, sRoleNo);
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

                IUserRole infoDAL = UserRoleFactory.Create(this.connStr, this.appCode);
                return infoDAL.DeleteRecordByRoleNo(sNo);
            }
            catch (Exception ex)
            {
                throw new Exception("按RoleNo删除(BLL层)记录时出错;" + ex.Message);
            }
        }

        public int DeleteRecordByUserNo(string sNo)
        {
            try
            {
                if (sNo == "" || sNo == null)
                {
                    return 0;
                }

                IUserRole infoDAL = UserRoleFactory.Create(this.connStr, this.appCode);
                return infoDAL.DeleteRecordByUserNo(sNo);
            }
            catch (Exception ex)
            {
                throw new Exception("按PermitNo删除(BLL层)记录时出错;" + ex.Message);
            }
        }

        public int AddNewRecord(UserRole info)
        {
            try
            {
                IUserRole infoDAL = UserRoleFactory.Create(this.connStr, this.appCode);
                return infoDAL.AddRecord(info);
            }
            catch (Exception ex)
            {
                throw new Exception("出错提示:添加新记录(AddNewRecord|BLL)时出错;" + ex.Message);
            }
        }

        public int UpdateRecord(UserRole info)
        {
            try
            {

                IUserRole infoDAL = UserRoleFactory.Create(this.connStr, this.appCode);
                return infoDAL.UpdateRecord(info);
            }
            catch (Exception ex)
            {
                throw new Exception("出错提示:更新记录(UpdateRecord|BLL)时出错;" + ex.Message);
            }
        }

        public UserRoleCollections GetAllRecords()
        {
            try
            {
                IUserRole infoDAL = UserRoleFactory.Create(this.connStr, this.appCode);
                return infoDAL.GetAllRecords();
            }
            catch (Exception ex)
            {
                throw new Exception("出错提示:检索所有记录(GetAllRecords|BLL)时出错;" + ex.Message);
            }
        }

        public UserRoleCollections GetRecords_Paging(SqlModel s_model)
        {
            if (s_model != null)
            {
                try
                {
                    IUserRole infoDAL = UserRoleFactory.Create(this.connStr, this.appCode);
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
                IUserRole infoDAL = UserRoleFactory.Create(this.connStr, this.appCode);
                return infoDAL.GetCountByCondition(sCondition);
            }
            catch (Exception ex)
            {
                throw new Exception("出错提示:分页检索记录(GetRecords_Paging|BLL)时出错;" + ex.Message);
            }
        }
    }
}


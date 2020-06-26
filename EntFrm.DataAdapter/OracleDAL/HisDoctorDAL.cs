using EntFrm.DataAdapter.HisData;
using EntFrm.Framework.Utility;
using EntFrm.Framework.Utility.Dbase;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace EntFrm.DataAdapter.OracleDAL
{
    public class HisDoctorDAL
    {
        #region sql
        private const string SQL_GET_ALL_RECORDS = @"Select *  From view_doctorinfo ";
        private const string SQL_GET_RECORD_BY_NO = @"Select * From view_doctorinfo Where doctor_id=:DocId";
        private const string SQL_GET_NAME_BY_NO = @"Select doctor_name From view_doctorinfo Where doctor_id=:DocId";
        private const string SQL_GET_RECORDS_BY_CLASSNO = @"Select * From view_doctorinfo Where branch_id=:BranchId ";
        private const string SQL_GET_COUNT_BY_CONDITION = @"Select Count(*) From view_doctorinfo Where 1=1 ";
        #endregion

        #region param
        private const string PARAM_DOCID = ":DocId";
        private const string PARAM_BRANCHID = ":BranchId";
        private const string PARAM_DOCNAME = ":DocName";
        private const string PARAM_DOCSEX = ":DocSex";
        private const string PARAM_DOCAGE = ":DocAge";
        private const string PARAM_DOCIDNO = ":DocIdNo";
        private const string PARAM_DOCPHOTO = ":DocPhoto";
        private const string PARAM_DOCRESUME = ":DocResume"; 
        #endregion

        private string connectionStr; 

        public HisDoctorDAL(string sConnectionStr)
        {
            this.connectionStr = sConnectionStr; 
        }

        public List<HisDoctorInfo> GetAllRecords()
        {
            OracleConnection connection = null;
            OracleDataReader reader = null;
            List<HisDoctorInfo> infos = null;
            HisDoctorInfo info = null;

            try
            { 

                connection = OrlHelper.GetConnection(connectionStr);

                reader = OrlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_ALL_RECORDS);

                if (reader.HasRows)
                {
                    infos = new List<HisDoctorInfo>();
                    while (reader.Read())
                    {
                        info = new HisDoctorInfo();
                        // 设置对象属性
                        PutObjectProperty(info, reader);
                        infos.Add(info);
                    }
                }
                return infos;
            }
            catch (Exception ex)
            {
                throw new Exception(" 检索所有记录(DAL层|GetAllRecords)时出错;" + ex.Message);
            }
            finally
            {
                if (reader != null)
                    ((IDisposable)reader).Dispose();
                if (connection != null)
                    connection.Dispose();
            }
        }

        public List<HisDoctorInfo> GetRecordsByClassNo(string sClassNo)
        {
            OracleConnection connection = null;
            OracleDataReader reader = null;
            List<HisDoctorInfo> infos = null;
            HisDoctorInfo info = null;

            try
            {
                OracleParameter[] paras = new OracleParameter[]
                {
                    new OracleParameter(PARAM_BRANCHID,OracleDbType.NVarchar2,50) 
                };
                paras[0].Value = sClassNo; 

                connection = OrlHelper.GetConnection(connectionStr);
                reader = OrlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_CLASSNO, paras);

                if (reader.HasRows)
                {
                    infos = new List<HisDoctorInfo>();
                    while (reader.Read())
                    {
                        info = new HisDoctorInfo();
                        // 设置对象属性
                        PutObjectProperty(info, reader);
                        infos.Add(info);
                    }
                }
                return infos;
            }
            catch (Exception ex)
            {
                throw new Exception(" 通过sClassNo检索记录(DAL层)时出错;" + ex.Message);
            }
            finally
            {
                if (reader != null)
                    ((IDisposable)reader).Dispose();
                if (connection != null)
                    connection.Dispose();
            }
        }

        public HisDoctorInfo GetRecordByNo(string sNo)
        {
            OracleConnection connection = null;
            OracleDataReader reader = null; 
            HisDoctorInfo info = null;

            try
            {
                OracleParameter[] paras = new OracleParameter[]
                {
                    new OracleParameter(PARAM_DOCID,OracleDbType.NVarchar2,50) 
                };
                paras[0].Value = sNo; 

                connection = OrlHelper.GetConnection(connectionStr);
                reader = OrlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORD_BY_NO, paras);

                if (reader.HasRows)
                {
                    reader.Read();

                    info = new HisDoctorInfo();
                        //设置对象属性
                    PutObjectProperty(info, reader); 
                }
                return info;
            }
            catch (Exception ex)
            {
                throw new Exception(" 通过No检索记录(DAL层)时出错;" + ex.Message);
            }
            finally
            {
                if (reader != null)
                    ((IDisposable)reader).Dispose();
                if (connection != null)
                    connection.Dispose();
            }
        }

        public string GetRecordNameByNo(string sNo)
        {
            OracleConnection connection = null;
            try
            {
                OracleParameter[] paras = new OracleParameter[]
                {
                    new OracleParameter(PARAM_DOCID,OracleDbType.NVarchar2,50) 
                };
                paras[0].Value = sNo; 

                connection = OrlHelper.GetConnection(connectionStr);
                return (string)OrlHelper.ExecuteScalar(connection, CommandType.Text, SQL_GET_NAME_BY_NO, paras);
            }
            catch (Exception ex)
            {
                throw new Exception(" 通过No检索记录名称(DAL层)时出错;" + ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Dispose();
            }
        }


        public List<HisDoctorInfo> GetRecordsByPaging(int pageIndex, int pageSize, string condition)
        {

            try
            {
                int iPageSize = pageSize > 0 ? pageSize : 10;
                int iPageIndex = pageIndex;
                int iRCount = GetCountByCondition(condition);
                int iPageCount = CommonHelper.GetRoundingDevision(iRCount, iPageSize);

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
                s_model.sOrderField = "doctor_id";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "view_doctorinfo";

                return GetRecords_Paging(s_model);
            }
            catch (Exception ex)
            {
                throw new Exception("出错提示:分页查询记录(GetRecords_Paging|BLL)时出错; " + ex.Message);
            }
        }

        public List<HisDoctorInfo> GetRecords_Paging(SqlModel s_model)
        {
            OracleConnection connection = null;
            OracleDataReader reader = null;
            List<HisDoctorInfo> infos = null;
            HisDoctorInfo info = null;

            try
            {
                if (!string.IsNullOrEmpty(s_model.sCondition))
                { 
                    s_model.sCondition = " Where   1=1 And  " + s_model.sCondition;
                }
                s_model.sTableName = "view_doctorinfo";

                string strSql = OrlHelper.GetSQL_Paging(s_model);
                connection = OrlHelper.GetConnection(connectionStr);
                reader = OrlHelper.ExecuteReader(connection, CommandType.Text, strSql);
                if (reader.HasRows)
                {
                    infos = new List<HisDoctorInfo>();
                    while (reader.Read())
                    {
                        info = new HisDoctorInfo();
                        // 设置对象属性
                        PutObjectProperty(info, reader);
                        infos.Add(info);
                    }
                }
                return infos;
            }
            catch (Exception ex)
            {
                throw new Exception(" 按页检索(DAL层)记录时出错;;" + ex.Message);
            }
            finally
            {
                if (reader != null)
                    ((IDisposable)reader).Dispose();
                if (connection != null)
                    connection.Dispose();
            }
        }

        public int GetCountByCondition(string sCondition)
        {
            OracleConnection connection = null;

            try
            {
                string strSql = SQL_GET_COUNT_BY_CONDITION;
                if (!string.IsNullOrEmpty(sCondition))
                {
                    strSql += "  And " + sCondition;
                } 

                connection = OrlHelper.GetConnection(connectionStr);
                return Convert.ToInt32(OrlHelper.ExecuteScalar(connection, CommandType.Text, strSql));
            }
            catch (Exception ex)
            {
                throw new Exception(" 计算记录总数(DAL层)时出错;" + ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Dispose();
            }
        }

        #region PutObjectProperty 设置对象属性
        /// <summary>
        /// 从 OracleDataReader 类对象中读取并设置对象属性
        /// </summary>
        /// <param name=" obj_info">主题对象</param>
        /// <param name="dr">读入数据</param>
        internal static void PutObjectProperty(HisDoctorInfo obj_info, OracleDataReader reader)
        {
            obj_info.DocId = reader["doctor_id"].ToString().Trim();
            obj_info.DocName = reader["doctor_name"].ToString().Trim();
            obj_info.DocSex = reader["sex"].ToString().Trim();
            obj_info.DocAge = reader["age"].ToString().Trim();
            //obj_info.DocIdNo = reader["idno"].ToString().Trim();
            obj_info.DocIdNo = "";
            obj_info.DocPhoto = reader["photo"].ToString().Trim();
            obj_info.DocResume = reader["rank"].ToString().Trim();
            obj_info.BranchId = reader["branch_id"].ToString().Trim();
            obj_info.Remark = reader["remark"].ToString().Trim();
        }
        #endregion
    }
}
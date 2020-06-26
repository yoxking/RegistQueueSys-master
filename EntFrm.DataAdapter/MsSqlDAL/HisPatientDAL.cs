using EntFrm.DataAdapter.HisData;
using EntFrm.Framework.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EntFrm.DataAdapter.MsSqlDAL
{
    public class HisPatientDAL
    {
        #region sql
        private const string SQL_GET_ALL_RECORDS = @"Select *  From view_patientinfo ";
        private const string SQL_GET_RECORD_BY_NO = @"Select * From view_patientinfo Where outpatient_id=:RegistId";
        private const string SQL_GET_NAME_BY_NO = @"Select patient_name From view_patientinfo Where outpatient_id=:RegistId";
        private const string SQL_GET_RECORDS_BY_CLASSNO = @"Select * From view_patientinfo Where pay_flag=:PatType ";
        private const string SQL_GET_COUNT_BY_CONDITION = @"Select Count(*) From view_patientinfo Where 1=1 ";
        #endregion

        #region param
        private const string PARAM_GUID = ":Guid";
        private const string PARAM_REGISTID = ":RegistId";
        private const string PARAM_HOSPITID = ":HospitId";
        private const string PARAM_TICKETID = ":TicketId";
        private const string PARAM_PATID = ":PatId";
        private const string PARAM_PATNAME = ":PatName";
        private const string PARAM_PATSEX = ":PatSex";
        private const string PARAM_PATAGE = ":PatAge";
        private const string PARAM_PATPHONE = ":PatPhone";
        private const string PARAM_PATIDNO = ":PatIdNo";
        private const string PARAM_DOCTORID = ":DoctorId";
        private const string PARAM_DOCTORNAME = ":DoctorName";
        private const string PARAM_BRANCHID = ":BranchId";
        private const string PARAM_BRANCHNAME = ":BranchName";
        private const string PARAM_REGISTTIME = ":RegistTime";
        private const string PARAM_PATTYPE = ":PatType";
        private const string PARAM_STATUS = ":Status";
        #endregion

        private string connectionStr;

        public HisPatientDAL(string sConnectionStr)
        {
            this.connectionStr = sConnectionStr;
        }

        public List<HisPatientInfo> GetAllRecords()
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            List<HisPatientInfo> infos = null;
            HisPatientInfo info = null;

            try
            {

                connection = SqlHelper.GetConnection(connectionStr);

                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_ALL_RECORDS);

                if (reader.HasRows)
                {
                    infos = new List<HisPatientInfo>();
                    while (reader.Read())
                    {
                        info = new HisPatientInfo();
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

        public List<HisPatientInfo> GetRecordsByClassNo(string sClassNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            List<HisPatientInfo> infos = null;
            HisPatientInfo info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_PATTYPE,SqlDbType.NVarChar,50)
                };
                paras[0].Value = sClassNo;

                connection = SqlHelper.GetConnection(connectionStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_CLASSNO, paras);

                if (reader.HasRows)
                {
                    infos = new List<HisPatientInfo>();
                    while (reader.Read())
                    {
                        info = new HisPatientInfo();
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

        public HisPatientInfo GetRecordByNo(string sNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            HisPatientInfo info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_REGISTID,SqlDbType.NVarChar,50)
                };
                paras[0].Value = sNo;

                connection = SqlHelper.GetConnection(connectionStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORD_BY_NO, paras);

                if (reader.HasRows)
                {
                    reader.Read();

                    info = new HisPatientInfo();
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
            SqlConnection connection = null;
            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_REGISTID,SqlDbType.NVarChar,50)
                };
                paras[0].Value = sNo;

                connection = SqlHelper.GetConnection(connectionStr);
                return (string)SqlHelper.ExecuteScalar(connection, CommandType.Text, SQL_GET_NAME_BY_NO, paras);
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


        public List<HisPatientInfo> GetRecordsByPaging(int pageIndex, int pageSize, string condition)
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
                s_model.sOrderField = "register_time";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "view_patientinfo";

                return GetRecords_Paging(s_model);
            }
            catch (Exception ex)
            {
                throw new Exception("出错提示:分页查询记录(GetRecords_Paging|BLL)时出错; " + ex.Message);
            }
        }

        public List<HisPatientInfo> GetRecords_Paging(SqlModel s_model)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            List<HisPatientInfo> infos = null;
            HisPatientInfo info = null;

            try
            {
                if (!string.IsNullOrEmpty(s_model.sCondition))
                {
                    s_model.sCondition = " Where   1=1 And  " + s_model.sCondition;
                }
                s_model.sTableName = "view_patientinfo";

                string strSql = SqlHelper.GetSQL_Paging(s_model);
                connection = SqlHelper.GetConnection(connectionStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, strSql);
                if (reader.HasRows)
                {
                    infos = new List<HisPatientInfo>();
                    while (reader.Read())
                    {
                        info = new HisPatientInfo();
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
            SqlConnection connection = null;

            try
            {
                string strSql = SQL_GET_COUNT_BY_CONDITION;
                if (!string.IsNullOrEmpty(sCondition))
                {
                    strSql += "  And " + sCondition;
                }

                connection = SqlHelper.GetConnection(connectionStr);
                return Convert.ToInt32(SqlHelper.ExecuteScalar(connection, CommandType.Text, strSql));
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
        /// 从 SqlDataReader 类对象中读取并设置对象属性
        /// </summary>
        /// <param name=" obj_info">主题对象</param>
        /// <param name="dr">读入数据</param>
        internal static void PutObjectProperty(HisPatientInfo obj_info, SqlDataReader reader)
        {
            obj_info.Guid = reader["outpatient_id"].ToString().Trim();
            obj_info.RegistId = reader["outpatient_id"].ToString().Trim();
            obj_info.HospitId = reader["outpatient_id"].ToString().Trim();
            obj_info.TicketId = reader["ticket_id"].ToString().Trim();
            obj_info.PatId = reader["patient_id"].ToString().Trim();
            obj_info.PatName = reader["patient_name"].ToString().Trim();
            obj_info.PatType = "0";
            obj_info.PatSex = reader["sex"].ToString().Trim();
            obj_info.PatAge = reader["age"].ToString().Trim();
            obj_info.PatPhone = reader["telephone"].ToString().Trim();
            obj_info.PatIdNo = reader["idno"].ToString().Trim();
            obj_info.DoctorId = reader["doctor_id"].ToString().Trim().Trim();
            obj_info.DoctorName = reader["doctor_name"].ToString().Trim();
            obj_info.ServiceId = reader["branch_id"].ToString().Trim();
            obj_info.ServiceName = reader["branch_name"].ToString().Trim();
            obj_info.BranchId = reader["branch_id"].ToString().Trim();
            obj_info.BranchName = reader["branch_name"].ToString().Trim();
            obj_info.RegistTime = reader["register_time"].ToString().Trim();
            obj_info.UpdateTime = reader["update_time"].ToString().Trim();
            obj_info.Status = "Y";
            obj_info.Remark = "";
        }
        #endregion
    }
}
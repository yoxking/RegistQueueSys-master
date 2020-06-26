using EntFrm.DataAdapter.HisData;
using EntFrm.Framework.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EntFrm.DataAdapter.MsSqlDAL
{
    public class HisInspectDAL
    {
        #region sql
        private const string SQL_GET_ALL_RECORDS = @"Select *  From V_EXAMINE_INFO ";
        private const string SQL_GET_RECORDS_BY_NO = @"Select * From V_EXAMINE_INFO Where   EXAM_ID=@EXAM_ID";
        private const string SQL_GET_NAME_BY_NO = @"Select BranchName From V_EXAMINE_INFO Where    EXAM_ID=@EXAM_ID";
         private const string SQL_GET_COUNT_BY_CONDITION = @"Select Count(*) From V_EXAMINE_INFO Where   1=1 ";
        #endregion

        #region param 
        private const string EXAM_ID = "@EXAM_ID"; 
        #endregion

        private string connStr; 

        public HisInspectDAL(string sConnStr)
        {
            this.connStr = sConnStr; 
        }

        public List<HisInspectInfo> GetAllRecords()
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            List<HisInspectInfo> infos = null;
            HisInspectInfo info = null;

            try
            { 

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_ALL_RECORDS);

                if (reader.HasRows)
                {
                    infos = new List<HisInspectInfo>();
                    while (reader.Read())
                    {
                        info = new HisInspectInfo();
                        // 设置对象属性
                        PutObjectProperty(info, reader);
                        infos.Add(info);
                    }
                }
                return infos;
            }
            catch (Exception ex)
            {
                throw new Exception(" 查询所有记录(DAL层|GetAllRecords)时出错;" + ex.Message);
            }
            finally
            {
                if (reader != null)
                    ((IDisposable)reader).Dispose();
                if (connection != null)
                    connection.Dispose();
            }
        } 

        public List<HisInspectInfo> GetRecordsByNo(string sNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            List<HisInspectInfo> infos = null;
            HisInspectInfo info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(EXAM_ID,SqlDbType.NVarChar,20) 
                };
                paras[0].Value = sNo; 

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_NO, paras);

                if (reader.HasRows)
                {
                    infos = new List<HisInspectInfo>();
                    while (reader.Read())
                    {
                        info = new HisInspectInfo();
                        //设置对象属性
                        PutObjectProperty(info, reader);
                        infos.Add(info);
                    }
                }
                return infos;
            }
            catch (Exception ex)
            {
                throw new Exception(" 通过No查询记录(DAL层)时出错;" + ex.Message);
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
                    new SqlParameter(EXAM_ID,SqlDbType.NVarChar,20) 
                };
                paras[0].Value = sNo; 

                connection = SqlHelper.GetConnection(connStr);
                return (string)SqlHelper.ExecuteScalar(connection, CommandType.Text, SQL_GET_NAME_BY_NO, paras);
            }
            catch (Exception ex)
            {
                throw new Exception(" 通过No查询记录名称(DAL层)时出错;" + ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Dispose();
            }
        }
          
        public List<HisInspectInfo> GetRecordsByPaging(int pageIndex,int pageSize, string strWhere)
        { 
              
            SqlModel s_model = new SqlModel();
            s_model.iPageNo = pageIndex;
            s_model.iPageSize = pageSize;
            s_model.sFields = " * ";
            s_model.sCondition = strWhere;
            s_model.sOrderField = "EXAM_ID";
            s_model.sOrderType = "Asc";
            s_model.sTableName = "V_EXAMINE_INFO";

            return GetRecords_Paging(s_model);
        }

        public List<HisInspectInfo> GetRecords_Paging(SqlModel s_model)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            List<HisInspectInfo> infos = null;
            HisInspectInfo info = null;

            try
            {
                if (!string.IsNullOrEmpty(s_model.sCondition))
                { 
                    s_model.sCondition = " Where  " + s_model.sCondition;
                }

                string strSql = SqlHelper.GetSQL_Paging(s_model);
                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, strSql);
                if (reader.HasRows)
                {
                    infos = new List<HisInspectInfo>();
                    while (reader.Read())
                    {
                        info = new HisInspectInfo();
                        // 设置对象属性
                        PutObjectProperty(info, reader);
                        infos.Add(info);
                    }
                }
                return infos;
            }
            catch (Exception ex)
            {
                throw new Exception(" 分页查询(DAL层)记录时出错;;" + ex.Message);
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
                if (sCondition.Length > 0)
                {
                    strSql += "  And " + sCondition;
                } 

                connection = SqlHelper.GetConnection(connStr);
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
        internal static void PutObjectProperty(HisInspectInfo obj_info, SqlDataReader reader)
        {
            obj_info.Guid = reader["table_id"].ToString().Trim();
            obj_info.RegistId = reader["table_id"].ToString().Trim();
            obj_info.HospitId = reader["table_id"].ToString().Trim();
            obj_info.TicketId = reader["ticket_id"].ToString().Trim();
            obj_info.PatId = reader["patient_id"].ToString().Trim();
            obj_info.PatName = reader["patient_name"].ToString().Trim();
            obj_info.PatType = "0";
            obj_info.PatSex = reader["sex"].ToString().Trim();
            obj_info.PatAge = reader["age"].ToString().Trim();
            obj_info.PatPhone = reader["telephone"].ToString().Trim();
            obj_info.PatIdNo = reader["idno"].ToString().Trim();
            obj_info.PatFrom = reader["check_from"].ToString().Trim();
            obj_info.ServiceId = reader["check_id"].ToString().Trim();
            obj_info.ServiceName = reader["check_name"].ToString().Trim();
            obj_info.BranchId = reader["check_id"].ToString().Trim();
            obj_info.BranchName = reader["check_name"].ToString().Trim();
            obj_info.RegistTime = reader["check_time"].ToString().Trim();
            obj_info.UpdateTime = reader["update_time"].ToString().Trim();
            obj_info.Status = "Y";
            obj_info.Remark = "";


            ///////永川人民医院
            //obj_info.Guid = reader["EXAM_ID"].ToString().Trim();
            //obj_info.RegistId = reader["EXAM_ID"].ToString().Trim();
            //obj_info.HospitId = reader["EXAM_ID"].ToString().Trim();
            //obj_info.TicketId = "";
            //obj_info.PatId = "";
            //obj_info.PatName = reader["PAT_CNAME"].ToString();
            //obj_info.PatType = "0";
            //obj_info.PatSex = reader["SEX_NAME"].ToString();
            //obj_info.PatAge = reader["PAT_AGE"].ToString();
            //obj_info.PatPhone = reader["PAT_TELE"].ToString();
            //obj_info.PatIdNo = "";
            //obj_info.PatFrom = reader["EXAM_KIND_NAME"].ToString();
            //obj_info.ServiceId = "";
            //obj_info.ServiceName = reader["EQUI_NAME"].ToString();
            //obj_info.BranchId = "";
            //obj_info.BranchName = "";
            //obj_info.RegistTime = reader["EXAM_DATE"].ToString().Trim();
            //obj_info.UpdateTime = reader["EXAM_DATE"].ToString().Trim();
            //obj_info.Status = "Y";
            //obj_info.Remark = "";
        }
        #endregion
    }
}


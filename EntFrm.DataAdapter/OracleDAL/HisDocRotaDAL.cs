using EntFrm.DataAdapter.HisData;
using EntFrm.Framework.Utility;
using EntFrm.Framework.Utility.Dbase;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace EntFrm.DataAdapter.OracleDAL
{
    public class HisDocRotaDAL
    {
        #region sql
        private const string SQL_GET_ALL_RECORDS = @"Select *  From view_doctorrota ";
        private const string SQL_GET_RECORD_BY_NO = @"Select * From view_doctorrota Where ID=:ID"; 
        private const string SQL_GET_RECORDS_BY_CLASSNO1 = @"Select * From view_doctorrota Where counter_id=:CounterId ";
        private const string SQL_GET_RECORDS_BY_CLASSNO2 = @"Select * From view_doctorrota Where doctor_id=:DoctorId ";
        private const string SQL_GET_COUNT_BY_CONDITION = @"Select Count(*) From view_doctorrota Where 1=1 ";
        #endregion

        #region param
        private const string PARAM_ID = ":ID";
        private const string PARAM_DOCTORID = ":DoctorId";
        private const string PARAM_COUNTERID = ":CounterId";
        private const string PARAM_WEEKDAY1= ":WeekDay1";
        private const string PARAM_WEEKDAY2 = ":WeekDay2";
        private const string PARAM_WEEKDAY3= ":WeekDay3";
        private const string PARAM_WEEKDAY4= ":WeekDay4";
        private const string PARAM_WEEKDAY5 = ":WeekDay5";
        private const string PARAM_WEEKDAY6 = ":WeekDay6";
        private const string PARAM_WEEKDAY7 = ":WeekDay7";
        #endregion

        private string connectionStr;

        public HisDocRotaDAL(string sConnectionStr)
        {
            this.connectionStr = sConnectionStr;
        }

        public List<HisDoctorRota> GetAllRecords()
        {
            OracleConnection connection = null;
            OracleDataReader reader = null;
            List<HisDoctorRota> infos = null;
            HisDoctorRota info = null;

            try
            {

                connection = OrlHelper.GetConnection(connectionStr);

                reader = OrlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_ALL_RECORDS);

                if (reader.HasRows)
                {
                    infos = new List<HisDoctorRota>();
                    while (reader.Read())
                    {
                        info = new HisDoctorRota();
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

        public List<HisDoctorRota> GetRecordsByClassNo1(string sClassNo)
        {
            OracleConnection connection = null;
            OracleDataReader reader = null;
            List<HisDoctorRota> infos = null;
            HisDoctorRota info = null;

            try
            {
                OracleParameter[] paras = new OracleParameter[]
                {
                    new OracleParameter(PARAM_COUNTERID,OracleDbType.NVarchar2,50)
                };
                paras[0].Value = sClassNo;

                connection = OrlHelper.GetConnection(connectionStr);
                reader = OrlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_CLASSNO1, paras);

                if (reader.HasRows)
                {
                    infos = new List<HisDoctorRota>();
                    while (reader.Read())
                    {
                        info = new HisDoctorRota();
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

        public List<HisDoctorRota> GetRecordsByClassNo2(string sClassNo)
        {
            OracleConnection connection = null;
            OracleDataReader reader = null;
            List<HisDoctorRota> infos = null;
            HisDoctorRota info = null;

            try
            {
                OracleParameter[] paras = new OracleParameter[]
                {
                    new OracleParameter(PARAM_DOCTORID,OracleDbType.NVarchar2,50)
                };
                paras[0].Value = sClassNo;

                connection = OrlHelper.GetConnection(connectionStr);
                reader = OrlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_CLASSNO2, paras);

                if (reader.HasRows)
                {
                    infos = new List<HisDoctorRota>();
                    while (reader.Read())
                    {
                        info = new HisDoctorRota();
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

        public HisDoctorRota GetRecordByNo(string sNo)
        {
            OracleConnection connection = null;
            OracleDataReader reader = null;
            HisDoctorRota info = null;

            try
            {
                OracleParameter[] paras = new OracleParameter[]
                {
                    new OracleParameter(PARAM_ID,OracleDbType.NVarchar2,50)
                };
                paras[0].Value = sNo;

                connection = OrlHelper.GetConnection(connectionStr);
                reader = OrlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORD_BY_NO, paras);

                if (reader.HasRows)
                {
                    reader.Read();

                    info = new HisDoctorRota();
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

        public List<HisDoctorRota> GetRecordsByPaging(int pageIndex, int pageSize, string condition)
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
                s_model.sOrderField = "ID";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "view_doctorrota";

                return GetRecords_Paging(s_model);
            }
            catch (Exception ex)
            {
                throw new Exception("出错提示:分页查询记录(GetRecords_Paging|BLL)时出错; " + ex.Message);
            }
        }

        public List<HisDoctorRota> GetRecords_Paging(SqlModel s_model)
        {
            OracleConnection connection = null;
            OracleDataReader reader = null;
            List<HisDoctorRota> infos = null;
            HisDoctorRota info = null;

            try
            {
                if (!string.IsNullOrEmpty(s_model.sCondition))
                {
                    s_model.sCondition = " Where   1=1 And  " + s_model.sCondition;
                }
                s_model.sTableName = "view_doctorrota";

                string strSql = OrlHelper.GetSQL_Paging(s_model);
                connection = OrlHelper.GetConnection(connectionStr);
                reader = OrlHelper.ExecuteReader(connection, CommandType.Text, strSql);
                if (reader.HasRows)
                {
                    infos = new List<HisDoctorRota>();
                    while (reader.Read())
                    {
                        info = new HisDoctorRota();
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
        internal static void PutObjectProperty(HisDoctorRota obj_info, OracleDataReader reader)
        {
            obj_info.Id = int.Parse(reader["id"].ToString().Trim());
            obj_info.DoctorId = reader["doctor_id"].ToString().Trim();
            obj_info.CounterId = reader["counter_id"].ToString().Trim();
            obj_info.WeekDay1 = reader["week_day1"].ToString().Trim();
            obj_info.WeekDay2 = reader["week_day2"].ToString().Trim();
            obj_info.WeekDay3 = reader["week_day3"].ToString().Trim();
            obj_info.WeekDay4 = reader["week_day4"].ToString().Trim();
            obj_info.WeekDay5 = reader["week_day5"].ToString().Trim();
            obj_info.WeekDay6 = reader["week_day6"].ToString().Trim();
            obj_info.WeekDay7 = reader["week_day7"].ToString().Trim();
            obj_info.Remark = "";
        }
        #endregion
    }
}
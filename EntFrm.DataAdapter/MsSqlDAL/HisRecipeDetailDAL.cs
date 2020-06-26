using EntFrm.DataAdapter.HisData;
using EntFrm.Framework.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EntFrm.DataAdapter.MsSqlDAL
{
    public class HisRecipeDetailDAL
    {
        #region sql
        private const string SQL_GET_ALL_RECORDS = @"Select *  From view_recipedetail ";
        private const string SQL_GET_RECORD_BY_NO = @"Select * From view_recipedetail Where table_id=:RegistId";
        private const string SQL_GET_NAME_BY_NO = @"Select patient_name From view_recipedetail Where table_id=:RegistId";
        private const string SQL_GET_RECORDS_BY_CLASSNO = @"Select * From view_recipedetail Where patient_id=:PatId ";
        private const string SQL_GET_COUNT_BY_CONDITION = @"Select Count(*) From view_recipedetail Where 1=1 ";
        #endregion

        #region param
        private const string PARAM_GUID = ":Guid";
        private const string PARAM_REGISTID = ":RegistId";
        private const string PARAM_HOSPITID = ":HospitId";
        private const string PARAM_PATID = ":PatId";
        private const string PARAM_RECIPENAME = ":RecipeName";
        private const string PARAM_RECIPESPEC = ":RecipeSpec";
        private const string PARAM_SERINUMBER = ":SeriNumber";
        private const string PARAM_STANDARD = ":Standard";
        private const string PARAM_PRICE = ":Price";
        private const string PARAM_AMOUNT = ":Amount";
        private const string PARAM_SQUANTITY = ":SQuantity";
        private const string PARAM_TQUANTITY = ":TQuantity";
        private const string PARAM_DIRECTION = ":Direction";
        private const string PARAM_EXPIRYDATE = ":ExpiryDate";
        private const string PARAM_FREQUENCY = ":Frequency";
        #endregion

        private string connectionStr;

        public HisRecipeDetailDAL(string sConnectionStr)
        {
            this.connectionStr = sConnectionStr;
        }

        public List<HisRecipeDetail> GetAllRecords()
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            List<HisRecipeDetail> infos = null;
            HisRecipeDetail info = null;

            try
            {

                connection = SqlHelper.GetConnection(connectionStr);

                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_ALL_RECORDS);

                if (reader.HasRows)
                {
                    infos = new List<HisRecipeDetail>();
                    while (reader.Read())
                    {
                        info = new HisRecipeDetail();
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

        public List<HisRecipeDetail> GetRecordsByClassNo(string sClassNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            List<HisRecipeDetail> infos = null;
            HisRecipeDetail info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_PATID,SqlDbType.NVarChar,50)
                };
                paras[0].Value = sClassNo;

                connection = SqlHelper.GetConnection(connectionStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_CLASSNO, paras);

                if (reader.HasRows)
                {
                    infos = new List<HisRecipeDetail>();
                    while (reader.Read())
                    {
                        info = new HisRecipeDetail();
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

        public HisRecipeDetail GetRecordByNo(string sNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            HisRecipeDetail info = null;

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

                    info = new HisRecipeDetail();
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

        public List<HisRecipeDetail> GetRecordsByPaging(int pageIndex, int pageSize, string condition)
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
                s_model.sOrderField = "table_id";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "view_recipedetail";

                return GetRecords_Paging(s_model);
            }
            catch (Exception ex)
            {
                throw new Exception("出错提示:分页查询记录(GetRecords_Paging|BLL)时出错; " + ex.Message);
            }
        }

        public List<HisRecipeDetail> GetRecords_Paging(SqlModel s_model)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            List<HisRecipeDetail> infos = null;
            HisRecipeDetail info = null;

            try
            {
                if (!string.IsNullOrEmpty(s_model.sCondition))
                {
                    s_model.sCondition = " Where   1=1 And  " + s_model.sCondition;
                }
                s_model.sTableName = "view_recipedetail";

                string strSql = SqlHelper.GetSQL_Paging(s_model);
                connection = SqlHelper.GetConnection(connectionStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, strSql);
                if (reader.HasRows)
                {
                    infos = new List<HisRecipeDetail>();
                    while (reader.Read())
                    {
                        info = new HisRecipeDetail();
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
        internal static void PutObjectProperty(HisRecipeDetail obj_info, SqlDataReader reader)
        {
            obj_info.Guid = reader["table_id"].ToString().Trim();
            obj_info.RegistId = reader["table_id"].ToString().Trim();
            obj_info.HospitId = reader["table_id"].ToString().Trim();
            obj_info.PatId = reader["patient_id"].ToString().Trim();
            obj_info.RecipeName = reader["recipe_name"].ToString().Trim();
            obj_info.RecipeSpec = reader["recipe_spec"].ToString().Trim();
            obj_info.SeriNumber = "";
            obj_info.Standard = reader["standard"].ToString().Trim();
            obj_info.Price = float.Parse(reader["price"].ToString().Trim());
            obj_info.Amount = float.Parse(reader["amount"].ToString().Trim());
            obj_info.SQuantity = reader["squantity"].ToString().Trim();
            obj_info.TQuantity = reader["tquantity"].ToString().Trim() + reader["tquantity_standard"].ToString().Trim();
            obj_info.Direction = reader["direction"].ToString().Trim();
            obj_info.ExpiryDate = "";
            obj_info.Frequency = reader["frequency"].ToString().Trim();
            obj_info.Remark = "";
        }
        #endregion
    }
}
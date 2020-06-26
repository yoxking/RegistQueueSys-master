using EntFrm.Business.IDAL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using System;
using System.Data;
using System.Data.SqlClient;

namespace EntFrm.Business.SQLServerDAL
{
  public class RolePermitDAL: IRolePermit
    {
        #region sql
        private const string SQL_GET_ALL_RECORDS = @"Select *  From RolePermit where  AppCode like @AppCode";
        private const string SQL_GET_RECORDS_BY_ROLENO = @"Select * From RolePermit Where  AppCode like @AppCode And RoleNo=@RoleNo";
        private const string SQL_GET_RECORDS_BY_PERMITNO = @"Select * From RolePermit Where  AppCode like @AppCode And PermitNo=@PermitNo";
        private const string SQL_GET_RECORDS_BY_ROLENO_AND_PERMITNO = @"Select * From RolePermit Where AppCode like @AppCode And  RoleNo=@RoleNo And PermitNo=@PermitNo";
        private const string SQL_ADD_RECORD = @"Insert into RolePermit(RoleNo,PermitNo,AppCode)values (@RoleNo,@PermitNo,@AppCode)";
        private const string SQL_UPDATE_RECORD = @"Update RolePermit set  RoleNo=@RoleNo,PermitNo=@PermitNo ,AppCode = @AppCode  Where  AppCode like @AppCode And ID=@ID";
        private const string SQL_HARD_DELETE_RECORD = @"Delete From RolePermit Where AppCode like @AppCode And  RoleNo=@RoleNo And PermitNo=@PermitNo";
        private const string SQL_DELETE_RECORDS_BY_ROLENO = @"Delete From RolePermit Where AppCode like @AppCode And  RoleNo=@RoleNo";
        private const string SQL_DELETE_RECORDS_BY_PERMITNO = @"Delete From RolePermit Where AppCode like @AppCode And  PermitNo=@PermitNo";
        private const string SQL_GET_COUNT_BY_CONDITION = @"Select Count(*) From RolePermit Where  AppCode like @AppCode ";
        #endregion

        #region param
        private const string PARAM_ID = "@ID";
        private const string PARAM_ROLENO = "@RoleNo";
        private const string PARAM_PERMITNO = "@PermitNo";
        private const string PARAM_APPCODE = "@AppCode";
        #endregion

        private string connStr;
        private string appCode;

        public RolePermitDAL(string sConnStr, string sAppCode)
        {
            this.connStr = sConnStr;
            this.appCode = sAppCode;
        }

        public RolePermitCollections GetAllRecords()
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            RolePermitCollections infos = null;
            RolePermit info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);

                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_ALL_RECORDS, paras);

                if (reader.HasRows)
                {
                    infos = new RolePermitCollections();
                    while (reader.Read())
                    {
                        info = new RolePermit();
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

        public RolePermitCollections GetRecordsByRoleNo(string sNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            RolePermitCollections infos = null;
            RolePermit info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_ROLENO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sNo;
                paras[1].Value = "%" + appCode + ";%";


                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_ROLENO, paras);

                if (reader.HasRows)
                {
                    infos = new RolePermitCollections();
                    while (reader.Read())
                    {
                        info = new RolePermit();
                        //设置对象属性
                        PutObjectProperty(info, reader);
                        infos.Add(info);
                    }
                }
                return infos;
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

        public RolePermitCollections GetRecordsByPermitNo(string sNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            RolePermitCollections infos = null;
            RolePermit info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_PERMITNO,SqlDbType.NVarChar,20),
                   new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sNo;
                paras[1].Value = "%" + appCode + ";%";


                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_PERMITNO, paras);

                if (reader.HasRows)
                {
                    infos = new RolePermitCollections();
                    while (reader.Read())
                    {
                        info = new RolePermit();
                        //设置对象属性
                        PutObjectProperty(info, reader);
                        infos.Add(info);
                    }
                }
                return infos;
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

        public RolePermitCollections GetRecordsByRoleNoAndPermitNo(string sRoleNo, string sPermitNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            RolePermitCollections infos = null;
            RolePermit info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter(PARAM_ROLENO,SqlDbType.NVarChar,20),
                new SqlParameter(PARAM_PERMITNO,SqlDbType.NVarChar,20),
                new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
            };
                paras[0].Value = sRoleNo;
                paras[1].Value = sPermitNo;
                paras[2].Value = "%" + appCode + ";%";


                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_ROLENO_AND_PERMITNO, paras);

                if (reader.HasRows)
                {
                    infos = new RolePermitCollections();
                    while (reader.Read())
                    {
                        info = new RolePermit();
                        //设置对象属性
                        PutObjectProperty(info, reader);
                        infos.Add(info);
                    }
                }
                return infos;
            }
            catch (Exception ex)
            {
                throw new Exception(" 通过PermitNo,RoleNo检索记录(DAL层)时出错;" + ex.Message);
            }
            finally
            {
                if (reader != null)
                    ((IDisposable)reader).Dispose();
                if (connection != null)
                    connection.Dispose();
            }
        }

        public int AddRecord(RolePermit info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter(PARAM_ROLENO,SqlDbType.NVarChar,20),
                new SqlParameter(PARAM_PERMITNO,SqlDbType.NVarChar,20),
                new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
            };
                paras[0].Value = info.sRoleNo;
                paras[1].Value = info.sPermitNo;
                paras[2].Value = info.sAppCode;



                connection = SqlHelper.GetConnection(connStr);
                return SqlHelper.ExecuteNonQuery(connection, CommandType.Text, SQL_ADD_RECORD, paras);
            }
            catch (Exception ex)
            {
                throw new Exception(" 添加(DAL层)记录时出错;" + ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Dispose();
            }
        }

        public int UpdateRecord(RolePermit info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter(PARAM_ID,SqlDbType.Int),
                new SqlParameter(PARAM_ROLENO,SqlDbType.NVarChar,20),
                new SqlParameter(PARAM_PERMITNO,SqlDbType.NVarChar,20),
                new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
            };
                paras[0].Value = info.iID;
                paras[1].Value = info.sRoleNo;
                paras[2].Value = info.sPermitNo;
                paras[3].Value = info.sAppCode;



                connection = SqlHelper.GetConnection(connStr);
                return SqlHelper.ExecuteNonQuery(connection, CommandType.Text, SQL_UPDATE_RECORD, paras);
            }
            catch (Exception ex)
            {
                throw new Exception(" 更新(DAL层)记录时出错;" + ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Dispose();
            }
        }

        public int HardDeleteRecord(string sRoleNo, string sPermitNo)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter(PARAM_ROLENO,SqlDbType.NVarChar,20),
                new SqlParameter(PARAM_PERMITNO,SqlDbType.NVarChar,20),
                new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
            };
                paras[0].Value = sRoleNo;
                paras[1].Value = sPermitNo;
                paras[2].Value = "%" + appCode + ";%";


                connection = SqlHelper.GetConnection(connStr);
                return SqlHelper.ExecuteNonQuery(connection, CommandType.Text, SQL_HARD_DELETE_RECORD, paras);
            }
            catch (Exception ex)
            {
                throw new Exception(" 硬删除(DAL层)记录时出错;" + ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Dispose();
            }
        }

        public int DeleteRecordByRoleNo(string sNo)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_ROLENO,SqlDbType.NVarChar,20),
                new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sNo;
                paras[1].Value = "%" + appCode + ";%";


                connection = SqlHelper.GetConnection(connStr);
                return SqlHelper.ExecuteNonQuery(connection, CommandType.Text, SQL_DELETE_RECORDS_BY_ROLENO, paras);
            }
            catch (Exception ex)
            {
                throw new Exception(" 按RoleNo删除(DAL层)记录时出错;" + ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Dispose();
            }
        }

        public int DeleteRecordByPermitNo(string sNo)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_PERMITNO, SqlDbType.NVarChar, 20),
                new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sNo;
                paras[1].Value = "%" + appCode + ";%";


                connection = SqlHelper.GetConnection(connStr);
                return SqlHelper.ExecuteNonQuery(connection, CommandType.Text, SQL_DELETE_RECORDS_BY_PERMITNO, paras);
            }
            catch (Exception ex)
            {
                throw new Exception(" 按PermitNo删除(DAL层)记录时出错;" + ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Dispose();
            }
        }

        public RolePermitCollections GetRecords_Paging(SqlModel s_model)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            RolePermitCollections infos = null;
            RolePermit info = null;

            try
            {
                if (s_model.sCondition.Length == 0)
                {
                    s_model.sCondition = " Where   AppCode like '%" + appCode + ";%' And ValidityState=1";
                }
                else
                {
                    s_model.sCondition = " Where   AppCode like '%" + appCode + ";%' And ValidityState=1 And " + s_model.sCondition;
                }
                s_model.sTableName = "RoleInfo";

                string strSql = SqlHelper.GetSQL_Paging(s_model);

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, strSql);
                if (reader.HasRows)
                {
                    infos = new RolePermitCollections();
                    while (reader.Read())
                    {
                        info = new RolePermit();
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
                if (sCondition.Length > 0)
                {
                    strSql += "  And " + sCondition;
                }

                SqlParameter[] paras = new SqlParameter[]
                {
                new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                return Convert.ToInt32(SqlHelper.ExecuteScalar(connection, CommandType.Text, strSql, paras));
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
        internal static void PutObjectProperty(RolePermit obj_info, SqlDataReader reader)
        {
            obj_info.iID = int.Parse(reader["ID"].ToString());
            obj_info.sRoleNo = reader["RoleNo"].ToString();
            obj_info.sPermitNo = reader["PermitNo"].ToString();
            obj_info.sAppCode = reader["AppCode"].ToString();
        }
        #endregion
    }
}
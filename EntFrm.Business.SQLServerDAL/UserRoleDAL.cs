using EntFrm.Framework.Utility;
using EntFrm.Business.IDAL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using System;
using System.Data;
using System.Data.SqlClient;

namespace EntFrm.Business.SQLServerDAL
{
  public class UserRoleDAL: IUserRole
  {
        #region sql
        private const string SQL_GET_ALL_RECORDS = @"Select *  From UserRole where  AppCode like @AppCode";
        private const string SQL_GET_RECORDS_BY_USERNO = @"Select * From UserRole Where AppCode like @AppCode And  UserNo=@UserNo";
        private const string SQL_GET_RECORDS_BY_ROLENO = @"Select * From UserRole Where  AppCode like @AppCode And RoleNo=@RoleNo";
        private const string SQL_GET_RECORDS_BY_USERNO_AND_ROLENO = @"Select * From UserRole Where AppCode like @AppCode And  UserNo=@UserNo And RoleNo=@RoleNo";
        private const string SQL_ADD_RECORD = @"Insert into UserRole(UserNo,RoleNo,AppCode)values (@UserNo,@RoleNo,@AppCode)";
        private const string SQL_UPDATE_RECORD = @"Update UserRole set  UserNo=@UserNo,RoleNo=@RoleNo ,AppCode = @AppCode Where  AppCode like @AppCode And ID=@ID";
        private const string SQL_HARD_DELETE_RECORD = @"Delete From UserRole Where  AppCode like @AppCode And UserNo=@UserNo And RoleNo=@RoleNo";
        private const string SQL_DELETE_RECORDS_BY_USERNO = @"Delete From UserRole Where AppCode like @AppCode And  UserNo=@UserNo";
        private const string SQL_DELETE_RECORDS_BY_ROLENO = @"Delete From UserRole Where AppCode like @AppCode And  RoleNo=@RoleNo";
        private const string SQL_GET_COUNT_BY_CONDITION = @"Select Count(*) From UserRole Where  AppCode like @AppCode ";
        #endregion

        #region param
        private const string PARAM_ID = "@ID";
        private const string PARAM_USERNO = "@UserNo";
        private const string PARAM_ROLENO = "@RoleNo";
        private const string PARAM_APPCODE = "@AppCode";
        #endregion

        private string connStr;
        private string appCode;

        public UserRoleDAL(string sConnStr, string sAppCode)
        {
            this.connStr = sConnStr;
            this.appCode = sAppCode;
        }

        public UserRoleCollections GetAllRecords()
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            UserRoleCollections infos = null;
            UserRole info = null;

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
                    infos = new UserRoleCollections();
                    while (reader.Read())
                    {
                        info = new UserRole();
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

        public UserRoleCollections GetRecordsByUserNo(string sNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            UserRoleCollections infos = null;
            UserRole info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_USERNO,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_USERNO, paras);

                if (reader.HasRows)
                {
                    infos = new UserRoleCollections();
                    while (reader.Read())
                    {
                        info = new UserRole();
                        // 设置对象属性
                        PutObjectProperty(info, reader);
                        infos.Add(info);
                    }
                }
                return infos;
            }
            catch (Exception ex)
            {
                throw new Exception(" 通过sUserNo查询记录(DAL层)时出错;" + ex.Message);
            }
            finally
            {
                if (reader != null)
                    ((IDisposable)reader).Dispose();
                if (connection != null)
                    connection.Dispose();
            }
        }

        public UserRoleCollections GetRecordsByRoleNo(string sNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            UserRoleCollections infos = null;
            UserRole info = null;

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
                    infos = new UserRoleCollections();
                    while (reader.Read())
                    {
                        info = new UserRole();
                        // 设置对象属性
                        PutObjectProperty(info, reader);
                        infos.Add(info);
                    }
                }
                return infos;
            }
            catch (Exception ex)
            {
                throw new Exception(" 通过sRoleNo查询记录(DAL层)时出错;" + ex.Message);
            }
            finally
            {
                if (reader != null)
                    ((IDisposable)reader).Dispose();
                if (connection != null)
                    connection.Dispose();
            }
        }

        public UserRoleCollections GetRecordsByUserNoAndRoleNo(string sUserNo, string sRoleNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            UserRoleCollections infos = null;
            UserRole info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter(PARAM_USERNO,SqlDbType.NVarChar,256),
                new SqlParameter(PARAM_ROLENO,SqlDbType.NVarChar,20) ,
                new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
            };
                paras[0].Value = sUserNo;
                paras[1].Value = sRoleNo;
                paras[2].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_USERNO_AND_ROLENO, paras);

                if (reader.HasRows)
                {
                    infos = new UserRoleCollections();
                    while (reader.Read())
                    {
                        info = new UserRole();
                        // 设置对象属性
                        PutObjectProperty(info, reader);
                        infos.Add(info);
                    }
                }
                return infos;
            }
            catch (Exception ex)
            {
                throw new Exception(" 通过sRoleNo查询记录(DAL层)时出错;" + ex.Message);
            }
            finally
            {
                if (reader != null)
                    ((IDisposable)reader).Dispose();
                if (connection != null)
                    connection.Dispose();
            }
        }

        public int AddRecord(UserRole info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter(PARAM_USERNO,SqlDbType.NVarChar,256),
                new SqlParameter(PARAM_ROLENO,SqlDbType.NVarChar,20),
                new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
            };
                paras[0].Value = info.sUserNo;
                paras[1].Value = info.sRoleNo;
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

        public int UpdateRecord(UserRole info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter(PARAM_ID,SqlDbType.Int),
                new SqlParameter(PARAM_USERNO,SqlDbType.NVarChar,256),
                new SqlParameter(PARAM_ROLENO,SqlDbType.NVarChar,20),
                new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
            };
                paras[0].Value = info.iID;
                paras[1].Value = info.sUserNo;
                paras[2].Value = info.sRoleNo;
                paras[3].Value = info.sAppCode;

                connection = SqlHelper.GetConnection(connStr);
                return SqlHelper.ExecuteNonQuery(connection, CommandType.Text, SQL_UPDATE_RECORD, paras);
            }
            catch (Exception ex)
            {
                throw new Exception(" 更新记录(DAL层)时出错;" + ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Dispose();
            }
        }

        public int HardDeleteRecord(string sUserNo, string sRoleNo)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter(PARAM_USERNO,SqlDbType.NVarChar,256),
                new SqlParameter(PARAM_ROLENO,SqlDbType.NVarChar,20),
                new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
            };
                paras[0].Value = sUserNo;
                paras[1].Value = sRoleNo;
                paras[2].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                return SqlHelper.ExecuteNonQuery(connection, CommandType.Text, SQL_HARD_DELETE_RECORD, paras);
            }
            catch (Exception ex)
            {
                throw new Exception(" 硬删除记录(DAL层)时出错;" + ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Dispose();
            }
        }

        public int DeleteRecordByUserNo(string sNo)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_USERNO,SqlDbType.NVarChar,256),
                new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                return SqlHelper.ExecuteNonQuery(connection, CommandType.Text, SQL_DELETE_RECORDS_BY_USERNO, paras);
            }
            catch (Exception ex)
            {
                throw new Exception(" 按UserNo删除记录(DAL层)时出错;" + ex.Message);
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
                throw new Exception(" 按RoleNo删除记录(DAL层)时出错;" + ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Dispose();
            }
        }


        public UserRoleCollections GetRecords_Paging(SqlModel s_model)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            UserRoleCollections infos = null;
            UserRole info = null;

            try
            {
                if (s_model.sCondition.Length == 0)
                {
                    s_model.sCondition = " Where  AppCode like '%" + appCode + ";%' ";
                }
                else
                {
                    s_model.sCondition = " Where   AppCode like '%" + appCode + ";%' And  " + s_model.sCondition;
                }

                string strSql = SqlHelper.GetSQL_Paging(s_model);
                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, strSql);
                if (reader.HasRows)
                {
                    infos = new UserRoleCollections();
                    while (reader.Read())
                    {
                        info = new UserRole();
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
        internal static void PutObjectProperty(UserRole obj_info, SqlDataReader reader)
        {
            obj_info.iID = int.Parse(reader["ID"].ToString());
            obj_info.sUserNo = reader["UserNo"].ToString();
            obj_info.sRoleNo = reader["RoleNo"].ToString();
            obj_info.sAppCode = reader["AppCode"].ToString();
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using EntFrm.Framework.Utility;
using EntFrm.System.Model;
using EntFrm.System.Model.Collections;
using EntFrm.System.IDAL;

namespace EntFrm.Business.SQLServerDAL
{
  public class DeviceReturnInfoDAL: IDeviceReturnInfo
  {
        #region sql
        private const string SQL_GET_ALL_RECORDS = @"Select *  From DeviceReturnInfo Where AppCode like @AppCode And ValidityState=1";
        private const string SQL_GET_RECORDS_BY_NO = @"Select * From DeviceReturnInfo Where   AppCode like @AppCode And   ValidityState=1 And BroNo=@BroNo";
        private const string SQL_GET_NAME_BY_NO = @"Select Name From DeviceReturnInfo Where   AppCode like @AppCode And   ValidityState=1 And BroNo=@BroNo";
        private const string SQL_ADD_RECORD = @"Insert into DeviceReturnInfo
                                              (BroNo,BroPerson,BroTime,PreReTime,Purpose,BroAudit,BroAuditor,RealReTime,RePerson,ReAudit,ReAuditor,IsReturn,AddOptor,AddDate,ModOptor,ModDate,ValidityState,Comments,AppCode)
                                              values(@BroNo,@BroPerson,@BroTime,@PreReTime,@Purpose,@BroAudit,@BroAuditor,@RealReTime,@RePerson,@ReAudit,@ReAuditor,@IsReturn,@AddOptor,@AddDate,@ModOptor,@ModDate,@ValidityState,@Comments,@AppCode)";
        private const string SQL_UPDATE_RECORD = @"Update DeviceReturnInfo set
                                                 BroNo=@BroNo,BroPerson=@BroPerson,BroTime=@BroTime,PreReTime=@PreReTime,Purpose=@Purpose,BroAudit=@BroAudit,BroAuditor=@BroAuditor,RealReTime=@RealReTime,RePerson=@RePerson,ReAudit=@ReAudit,ReAuditor=@ReAuditor,IsReturn=@IsReturn,AddOptor=@AddOptor,AddDate=@AddDate,ModOptor=@ModOptor,ModDate=@ModDate,ValidityState=@ValidityState,Comments=@Comments,AppCode=@AppCode 
                                                 Where  AppCode like @AppCode And   ValidityState=1 And BroNo=@BroNo  And Version=@Version";
        private const string SQL_HARD_DELETE_RECORD = @"Delete From DeviceReturnInfo Where   AppCode like @AppCode And   BroNo=@BroNo ";
        private const string SQL_SOFT_DELETE_RECORD = @"Update DeviceReturnInfo set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 And BroNo=@BroNo";
        private const string SQL_GET_RECORDS_BY_CLASSNO = @"Select * From DeviceReturnInfo Where    AppCode like @AppCode And   ValidityState=1 And ClassNo=@ClassNo";
        private const string SQL_GET_COUNT_BY_CONDITION = @"Select Count(*) From DeviceReturnInfo Where   AppCode like @AppCode  And   ValidityState=1 ";
        #endregion

        #region param
        private const string PARAM_ID = "@ID";
        private const string PARAM_BRONO = "@BroNo";
        private const string PARAM_BROPERSON = "@BroPerson";
        private const string PARAM_BROTIME = "@BroTime";
        private const string PARAM_PRERETIME = "@PreReTime";
        private const string PARAM_PURPOSE = "@Purpose";
        private const string PARAM_BROAUDIT = "@BroAudit";
        private const string PARAM_BROAUDITOR = "@BroAuditor";
        private const string PARAM_REALRETIME = "@RealReTime";
        private const string PARAM_REPERSON = "@RePerson";
        private const string PARAM_REAUDIT = "@ReAudit";
        private const string PARAM_REAUDITOR = "@ReAuditor";
        private const string PARAM_ISRETURN = "@IsReturn";
        private const string PARAM_ADDOPTOR = "@AddOptor";
        private const string PARAM_ADDDATE = "@AddDate";
        private const string PARAM_MODOPTOR = "@ModOptor";
        private const string PARAM_MODDATE = "@ModDate";
        private const string PARAM_VALIDITYSTATE = "@ValidityState";
        private const string PARAM_COMMENTS = "@Comments";
        private const string PARAM_APPCODE = "@AppCode";
        private const string PARAM_VERSION = "@Version";
        #endregion

        private string connStr;
        private string appCode;

        public DeviceReturnInfoDAL(string sConnStr,string sAppCode)
        {
           this.connStr = sConnStr;
           this.appCode = sAppCode;
        }

        public DeviceReturnInfoCollections GetAllRecords()
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            DeviceReturnInfoCollections infos = null;
            DeviceReturnInfo info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                 };
                paras[0].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_ALL_RECORDS,paras);

                if (reader.HasRows)
                {
                    infos = new DeviceReturnInfoCollections();
                    while (reader.Read())
                    {
                        info = new DeviceReturnInfo();
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

        public DeviceReturnInfoCollections GetRecordsByClassNo(string sClassNo)
        {
            /*SqlConnection connection = null;
            SqlDataReader reader = null;
            DeviceReturnInfoCollections infos = null;
            DeviceReturnInfo info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_CLASSNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sClassNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_CLASSNO,paras);

                if (reader.HasRows)
                {
                    infos = new DeviceReturnInfoCollections();
                    while (reader.Read())
                    {
                        info = new DeviceReturnInfo();
                        // 设置对象属性
                        PutObjectProperty(info, reader);
                        infos.Add(info);
                    }
                }
                return infos;
            }
            catch (Exception ex)
            {
                throw new Exception(" 通过sClassNo查询记录(DAL层)时出错;" + ex.Message);
            }
            finally
            {
                if (reader != null)
                    ((IDisposable)reader).Dispose();
                if (connection != null)
                    connection.Dispose();
            }*/
            return null;
        }

        public DeviceReturnInfoCollections GetRecordsByNo(string sNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            DeviceReturnInfoCollections infos = null;
            DeviceReturnInfo info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_BRONO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_NO,paras);

                if (reader.HasRows)
                {
                    infos = new DeviceReturnInfoCollections();
                    while (reader.Read())
                    {
                        info = new DeviceReturnInfo();
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
                    new SqlParameter(PARAM_BRONO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sNo;
                paras[1].Value = "%" + appCode + ";%";

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

        public int AddRecord(DeviceReturnInfo info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_BRONO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_BROPERSON,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_BROTIME,SqlDbType.DateTime),
                    new SqlParameter(PARAM_PRERETIME,SqlDbType.DateTime),
                    new SqlParameter(PARAM_PURPOSE,SqlDbType.NVarChar,1073741823),
                    new SqlParameter(PARAM_BROAUDIT,SqlDbType.Int),
                    new SqlParameter(PARAM_BROAUDITOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_REALRETIME,SqlDbType.DateTime),
                    new SqlParameter(PARAM_REPERSON,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_REAUDIT,SqlDbType.Int),
                    new SqlParameter(PARAM_REAUDITOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ISRETURN,SqlDbType.Int),
                    new SqlParameter(PARAM_ADDOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_MODOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MODDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_VALIDITYSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_COMMENTS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = info.sBroNo;
                paras[1].Value = info.sBroPerson;
                paras[2].Value = info.dBroTime;
                paras[3].Value = info.dPreReTime;
                paras[4].Value = info.sPurpose;
                paras[5].Value = info.iBroAudit;
                paras[6].Value = info.sBroAuditor;
                paras[7].Value = info.dRealReTime;
                paras[8].Value = info.sRePerson;
                paras[9].Value = info.iReAudit;
                paras[10].Value = info.sReAuditor;
                paras[11].Value = info.iIsReturn;
                paras[12].Value = info.sAddOptor;
                paras[13].Value = info.dAddDate;
                paras[14].Value = info.sModOptor;
                paras[15].Value = info.dModDate;
                paras[16].Value = info.iValidityState;
                paras[17].Value = info.sComments;
                paras[18].Value = info.sAppCode;

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

        public int UpdateRecord(DeviceReturnInfo info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_BRONO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_BROPERSON,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_BROTIME,SqlDbType.DateTime),
                    new SqlParameter(PARAM_PRERETIME,SqlDbType.DateTime),
                    new SqlParameter(PARAM_PURPOSE,SqlDbType.NVarChar,1073741823),
                    new SqlParameter(PARAM_BROAUDIT,SqlDbType.Int),
                    new SqlParameter(PARAM_BROAUDITOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_REALRETIME,SqlDbType.DateTime),
                    new SqlParameter(PARAM_REPERSON,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_REAUDIT,SqlDbType.Int),
                    new SqlParameter(PARAM_REAUDITOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ISRETURN,SqlDbType.Int),
                    new SqlParameter(PARAM_ADDOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_MODOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MODDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_VALIDITYSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_COMMENTS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_VERSION,SqlDbType.Timestamp)
                };
                paras[0].Value = info.sBroNo;
                paras[1].Value = info.sBroPerson;
                paras[2].Value = info.dBroTime;
                paras[3].Value = info.dPreReTime;
                paras[4].Value = info.sPurpose;
                paras[5].Value = info.iBroAudit;
                paras[6].Value = info.sBroAuditor;
                paras[7].Value = info.dRealReTime;
                paras[8].Value = info.sRePerson;
                paras[9].Value = info.iReAudit;
                paras[10].Value = info.sReAuditor;
                paras[11].Value = info.iIsReturn;
                paras[12].Value = info.sAddOptor;
                paras[13].Value = info.dAddDate;
                paras[14].Value = info.sModOptor;
                paras[15].Value = info.dModDate;
                paras[16].Value = info.iValidityState;
                paras[17].Value = info.sComments;
                paras[18].Value = info.sAppCode;
                paras[19].Value = StringUtil.ConvertToBytes(info.sVersion);

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

        public int HardDeleteRecord(string sNo)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_BRONO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sNo;
                paras[1].Value = "%" + appCode + ";%";

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

        public int SoftDeleteRecord(string sNo)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_BRONO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                return SqlHelper.ExecuteNonQuery(connection, CommandType.Text, SQL_SOFT_DELETE_RECORD, paras);
            }
            catch (Exception ex)
            {
                throw new Exception(" 软删除记录(DAL层)时出错;" + ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Dispose();
            }
        }

        public DeviceReturnInfoCollections GetRecords_Paging(SqlModel s_model)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            DeviceReturnInfoCollections infos = null;
            DeviceReturnInfo info = null;

            try
            {
                 if (s_model.sCondition.Length==0)
                {
                    s_model.sCondition = " Where  AppCode like '%" + appCode + ";%' And ValidityState=1";
                }
                else
                {
                    s_model.sCondition = " Where   AppCode like '%" + appCode + ";%' And ValidityState=1 And  " + s_model.sCondition;
                }

                string strSql = SqlHelper.GetSQL_Paging(s_model);
                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, strSql);
                if (reader.HasRows)
                {
                    infos = new DeviceReturnInfoCollections();
                    while (reader.Read())
                    {
                        info = new DeviceReturnInfo();
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
                if(sCondition.Length>0)
                {
                    strSql +="  And " + sCondition;
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
        internal static void PutObjectProperty(DeviceReturnInfo obj_info, SqlDataReader reader)
        {
            obj_info.iID= int.Parse(reader["ID"].ToString());
            obj_info.sBroNo= reader["BroNo"].ToString();
            obj_info.sBroPerson= reader["BroPerson"].ToString();
            obj_info.dBroTime= DateTime.Parse(reader["BroTime"].ToString());
            obj_info.dPreReTime= DateTime.Parse(reader["PreReTime"].ToString());
            obj_info.sPurpose= reader["Purpose"].ToString();
            obj_info.iBroAudit= int.Parse(reader["BroAudit"].ToString());
            obj_info.sBroAuditor= reader["BroAuditor"].ToString();
            obj_info.dRealReTime= DateTime.Parse(reader["RealReTime"].ToString());
            obj_info.sRePerson= reader["RePerson"].ToString();
            obj_info.iReAudit= int.Parse(reader["ReAudit"].ToString());
            obj_info.sReAuditor= reader["ReAuditor"].ToString();
            obj_info.iIsReturn= int.Parse(reader["IsReturn"].ToString());
            obj_info.sAddOptor= reader["AddOptor"].ToString();
            obj_info.dAddDate= DateTime.Parse(reader["AddDate"].ToString());
            obj_info.sModOptor= reader["ModOptor"].ToString();
            obj_info.dModDate= DateTime.Parse(reader["ModDate"].ToString());
            obj_info.iValidityState= int.Parse(reader["ValidityState"].ToString());
            obj_info.sComments= reader["Comments"].ToString();
            obj_info.sAppCode= reader["AppCode"].ToString();
            obj_info.sVersion= StringUtil.ConvertToString((byte[])reader["Version"].ToString());
        }
        #endregion
    }
}

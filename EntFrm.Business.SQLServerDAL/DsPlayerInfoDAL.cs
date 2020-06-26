using EntFrm.Business.IDAL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using System;
using System.Data;
using System.Data.SqlClient;

namespace EntFrm.Business.SQLServerDAL
{
  public class DsPlayerInfoDAL: IDsPlayerInfo
  {
        #region sql
        private const string SQL_GET_ALL_RECORDS = @"Select *  From DsPlayerInfo Where AppCode like @AppCode And ValidityState=1";
        private const string SQL_GET_RECORDS_BY_NO = @"Select * From DsPlayerInfo Where   AppCode like @AppCode And   ValidityState=1 And PlayerNo=@PlayerNo";
        private const string SQL_GET_NAME_BY_NO = @"Select PlayerName From DsPlayerInfo Where   AppCode like @AppCode And   ValidityState=1 And PlayerNo=@PlayerNo";
        private const string SQL_ADD_RECORD = @"Insert into DsPlayerInfo
                                              (PlayerNo,PlayerName,PlayerCode,PClassNo,IpAddress,MacAddress,LocalPort,Resolution,OnlineState,OnDuration,OSVersion,ApVersion,ParamsFmt,StartupTime,ShutdownTime,MachineCode,IsAuthorize,CheckState,BranchNo,AddOptor,AddDate,ModOptor,ModDate,ValidityState,Comments,AppCode)
                                              values(@PlayerNo,@PlayerName,@PlayerCode,@PClassNo,@IpAddress,@MacAddress,@LocalPort,@Resolution,@OnlineState,@OnDuration,@OSVersion,@ApVersion,@ParamsFmt,@StartupTime,@ShutdownTime,@MachineCode,@IsAuthorize,@CheckState,@BranchNo,@AddOptor,@AddDate,@ModOptor,@ModDate,@ValidityState,@Comments,@AppCode)";
        private const string SQL_UPDATE_RECORD = @"Update DsPlayerInfo set
                                                 PlayerNo=@PlayerNo,PlayerName=@PlayerName,PlayerCode=@PlayerCode,PClassNo=@PClassNo,IpAddress=@IpAddress,MacAddress=@MacAddress,LocalPort=@LocalPort,Resolution=@Resolution,OnlineState=@OnlineState,OnDuration=@OnDuration,OSVersion=@OSVersion,ApVersion=@ApVersion,ParamsFmt=@ParamsFmt,StartupTime=@StartupTime,ShutdownTime=@ShutdownTime,MachineCode=@MachineCode,IsAuthorize=@IsAuthorize,CheckState=@CheckState,BranchNo=@BranchNo,AddOptor=@AddOptor,AddDate=@AddDate,ModOptor=@ModOptor,ModDate=@ModDate,ValidityState=@ValidityState,Comments=@Comments,AppCode=@AppCode 
                                                 Where  AppCode like @AppCode And   ValidityState=1 And PlayerNo=@PlayerNo  And Version=@Version";
        private const string SQL_HARD_DELETE_RECORD = @"Delete From DsPlayerInfo Where   AppCode like @AppCode And   PlayerNo=@PlayerNo ";
        private const string SQL_SOFT_DELETE_RECORD = @"Update DsPlayerInfo set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 And PlayerNo=@PlayerNo";
        private const string SQL_HARD_DELETE_BY_CONDTION = @"Delete From DsPlayerInfo Where   AppCode like @AppCode ";
        private const string SQL_SOFT_DELETE_BY_CONDTION = @"Update DsPlayerInfo set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 ";
        private const string SQL_GET_RECORDS_BY_CLASSNO = @"Select * From DsPlayerInfo Where    AppCode like @AppCode And   ValidityState=1 And PClassNo=@PClassNo";
        private const string SQL_GET_COUNT_BY_CONDITION = @"Select Count(*) From DsPlayerInfo Where   AppCode like @AppCode  And   ValidityState=1 ";
        #endregion

        #region param
        private const string PARAM_ID = "@ID";
        private const string PARAM_PLAYERNO = "@PlayerNo";
        private const string PARAM_PLAYERNAME = "@PlayerName";
        private const string PARAM_PLAYERCODE = "@PlayerCode";
        private const string PARAM_PCLASSNO = "@PClassNo";
        private const string PARAM_IPADDRESS = "@IpAddress";
        private const string PARAM_MACADDRESS = "@MacAddress";
        private const string PARAM_LOCALPORT = "@LocalPort";
        private const string PARAM_RESOLUTION = "@Resolution";
        private const string PARAM_ONLINESTATE = "@OnlineState";
        private const string PARAM_ONDURATION = "@OnDuration";
        private const string PARAM_OSVERSION = "@OSVersion";
        private const string PARAM_APVERSION = "@ApVersion";
        private const string PARAM_PARAMSFMT = "@ParamsFmt";
        private const string PARAM_STARTUPTIME = "@StartupTime";
        private const string PARAM_SHUTDOWNTIME = "@ShutdownTime";
        private const string PARAM_MACHINECODE = "@MachineCode";
        private const string PARAM_ISAUTHORIZE = "@IsAuthorize";
        private const string PARAM_CHECKSTATE = "@CheckState";
        private const string PARAM_BRANCHNO = "@BranchNo";
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

        public DsPlayerInfoDAL(string sConnStr,string sAppCode)
        {
           this.connStr = sConnStr;
           this.appCode = sAppCode;
        }

        public DsPlayerInfoCollections GetAllRecords()
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            DsPlayerInfoCollections infos = null;
            DsPlayerInfo info = null;

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
                    infos = new DsPlayerInfoCollections();
                    while (reader.Read())
                    {
                        info = new DsPlayerInfo();
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

        public DsPlayerInfoCollections GetRecordsByClassNo(string sClassNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            DsPlayerInfoCollections infos = null;
            DsPlayerInfo info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_PCLASSNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sClassNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_CLASSNO,paras);

                if (reader.HasRows)
                {
                    infos = new DsPlayerInfoCollections();
                    while (reader.Read())
                    {
                        info = new DsPlayerInfo();
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
            }
        }

        public DsPlayerInfoCollections GetRecordsByNo(string sNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            DsPlayerInfoCollections infos = null;
            DsPlayerInfo info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_PLAYERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_NO,paras);

                if (reader.HasRows)
                {
                    infos = new DsPlayerInfoCollections();
                    while (reader.Read())
                    {
                        info = new DsPlayerInfo();
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
                    new SqlParameter(PARAM_PLAYERNO,SqlDbType.NVarChar,20),
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

        public int AddNewRecord(DsPlayerInfo info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_PLAYERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_PLAYERNAME,SqlDbType.NVarChar,200),
                    new SqlParameter(PARAM_PLAYERCODE,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_PCLASSNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_IPADDRESS,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_MACADDRESS,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_LOCALPORT,SqlDbType.Int),
                    new SqlParameter(PARAM_RESOLUTION,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_ONLINESTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_ONDURATION,SqlDbType.Float),
                    new SqlParameter(PARAM_OSVERSION,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_APVERSION,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_PARAMSFMT,SqlDbType.NVarChar,1073741823),
                    new SqlParameter(PARAM_STARTUPTIME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_SHUTDOWNTIME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_MACHINECODE,SqlDbType.NVarChar,500),
                    new SqlParameter(PARAM_ISAUTHORIZE,SqlDbType.Int),
                    new SqlParameter(PARAM_CHECKSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_BRANCHNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_MODOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MODDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_VALIDITYSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_COMMENTS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = info.sPlayerNo;
                paras[1].Value = info.sPlayerName;
                paras[2].Value = info.sPlayerCode;
                paras[3].Value = info.sPClassNo;
                paras[4].Value = info.sIpAddress;
                paras[5].Value = info.sMacAddress;
                paras[6].Value = info.iLocalPort;
                paras[7].Value = info.sResolution;
                paras[8].Value = info.iOnlineState;
                paras[9].Value = info.dOnDuration;
                paras[10].Value = info.sOSVersion;
                paras[11].Value = info.sApVersion;
                paras[12].Value = info.sParamsFmt;
                paras[13].Value = info.sStartupTime;
                paras[14].Value = info.sShutdownTime;
                paras[15].Value = info.sMachineCode;
                paras[16].Value = info.iIsAuthorize;
                paras[17].Value = info.iCheckState;
                paras[18].Value = info.sBranchNo;
                paras[19].Value = info.sAddOptor;
                paras[20].Value = info.dAddDate;
                paras[21].Value = info.sModOptor;
                paras[22].Value = info.dModDate;
                paras[23].Value = info.iValidityState;
                paras[24].Value = info.sComments;
                paras[25].Value = info.sAppCode;

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

        public int UpdateRecord(DsPlayerInfo info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_PLAYERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_PLAYERNAME,SqlDbType.NVarChar,200),
                    new SqlParameter(PARAM_PLAYERCODE,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_PCLASSNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_IPADDRESS,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_MACADDRESS,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_LOCALPORT,SqlDbType.Int),
                    new SqlParameter(PARAM_RESOLUTION,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_ONLINESTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_ONDURATION,SqlDbType.Float),
                    new SqlParameter(PARAM_OSVERSION,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_APVERSION,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_PARAMSFMT,SqlDbType.NVarChar,1073741823),
                    new SqlParameter(PARAM_STARTUPTIME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_SHUTDOWNTIME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_MACHINECODE,SqlDbType.NVarChar,500),
                    new SqlParameter(PARAM_ISAUTHORIZE,SqlDbType.Int),
                    new SqlParameter(PARAM_CHECKSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_BRANCHNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_MODOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MODDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_VALIDITYSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_COMMENTS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_VERSION,SqlDbType.Timestamp)
                };
                paras[0].Value = info.sPlayerNo;
                paras[1].Value = info.sPlayerName;
                paras[2].Value = info.sPlayerCode;
                paras[3].Value = info.sPClassNo;
                paras[4].Value = info.sIpAddress;
                paras[5].Value = info.sMacAddress;
                paras[6].Value = info.iLocalPort;
                paras[7].Value = info.sResolution;
                paras[8].Value = info.iOnlineState;
                paras[9].Value = info.dOnDuration;
                paras[10].Value = info.sOSVersion;
                paras[11].Value = info.sApVersion;
                paras[12].Value = info.sParamsFmt;
                paras[13].Value = info.sStartupTime;
                paras[14].Value = info.sShutdownTime;
                paras[15].Value = info.sMachineCode;
                paras[16].Value = info.iIsAuthorize;
                paras[17].Value = info.iCheckState;
                paras[18].Value = info.sBranchNo;
                paras[19].Value = info.sAddOptor;
                paras[20].Value = info.dAddDate;
                paras[21].Value = info.sModOptor;
                paras[22].Value = info.dModDate;
                paras[23].Value = info.iValidityState;
                paras[24].Value = info.sComments;
                paras[25].Value = info.sAppCode;
                paras[26].Value = StringHelper.ConvertToBytes(info.sVersion);

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
                    new SqlParameter(PARAM_PLAYERNO,SqlDbType.NVarChar,20),
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
                    new SqlParameter(PARAM_PLAYERNO,SqlDbType.NVarChar,20),
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

public int HardDeleteByCondition(string sCondtion)
{
    SqlConnection connection = null;
    try
    {
        string sql = SQL_HARD_DELETE_BY_CONDTION;
        if (!string.IsNullOrEmpty(sCondtion))
        {
           sql += " And " + sCondtion;
       }
       SqlParameter[] paras = new SqlParameter[]
      {
      new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
       };
       paras[0].Value = "%" + appCode + ";%";
      connection = SqlHelper.GetConnection(connStr);
       return SqlHelper.ExecuteNonQuery(connection, CommandType.Text, sql, paras);
  }
  catch (Exception ex)
  {
      throw new Exception("硬删除记录(DAL层)时出错; " + ex.Message);
  }
  finally
  {
      if (connection != null)
          connection.Dispose();
  }
 }
public int SoftDeleteByCondition(string sCondtion)
{
   SqlConnection connection = null;
    try
   {
       string sql = SQL_SOFT_DELETE_BY_CONDTION;
       if (!string.IsNullOrEmpty(sCondtion))
       {
           sql += " And " + sCondtion;
       }
       SqlParameter[] paras = new SqlParameter[]
       {
      new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
      };
      paras[0].Value = "%" + appCode + ";%";
      connection = SqlHelper.GetConnection(connStr);
      return SqlHelper.ExecuteNonQuery(connection, CommandType.Text, sql, paras);
  }
  catch (Exception ex)
 {
     throw new Exception(" 硬删除记录(DAL层)时出错; " + ex.Message);
  }
  finally
  {
     if (connection != null)
         connection.Dispose();
 }
 }
        public DsPlayerInfoCollections GetRecords_Paging(SqlModel s_model)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            DsPlayerInfoCollections infos = null;
            DsPlayerInfo info = null;

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
                    infos = new DsPlayerInfoCollections();
                    while (reader.Read())
                    {
                        info = new DsPlayerInfo();
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
        internal static void PutObjectProperty(DsPlayerInfo obj_info, SqlDataReader reader)
        {
            obj_info.iID= int.Parse(reader["ID"].ToString());
            obj_info.sPlayerNo= reader["PlayerNo"].ToString();
            obj_info.sPlayerName= reader["PlayerName"].ToString();
            obj_info.sPlayerCode= reader["PlayerCode"].ToString();
            obj_info.sPClassNo= reader["PClassNo"].ToString();
            obj_info.sIpAddress= reader["IpAddress"].ToString();
            obj_info.sMacAddress= reader["MacAddress"].ToString();
            obj_info.iLocalPort= int.Parse(reader["LocalPort"].ToString());
            obj_info.sResolution= reader["Resolution"].ToString();
            obj_info.iOnlineState= int.Parse(reader["OnlineState"].ToString());
            obj_info.dOnDuration= double.Parse(reader["OnDuration"].ToString());
            obj_info.sOSVersion= reader["OSVersion"].ToString();
            obj_info.sApVersion= reader["ApVersion"].ToString();
            obj_info.sParamsFmt= reader["ParamsFmt"].ToString();
            obj_info.sStartupTime= reader["StartupTime"].ToString();
            obj_info.sShutdownTime= reader["ShutdownTime"].ToString();
            obj_info.sMachineCode= reader["MachineCode"].ToString();
            obj_info.iIsAuthorize= int.Parse(reader["IsAuthorize"].ToString());
            obj_info.iCheckState= int.Parse(reader["CheckState"].ToString());
            obj_info.sBranchNo= reader["BranchNo"].ToString();
            obj_info.sAddOptor= reader["AddOptor"].ToString();
            obj_info.dAddDate= DateTime.Parse(reader["AddDate"].ToString());
            obj_info.sModOptor= reader["ModOptor"].ToString();
            obj_info.dModDate= DateTime.Parse(reader["ModDate"].ToString());
            obj_info.iValidityState= int.Parse(reader["ValidityState"].ToString());
            obj_info.sComments= reader["Comments"].ToString();
            obj_info.sAppCode= reader["AppCode"].ToString();
            obj_info.sVersion= StringHelper.ConvertToString((byte[])reader["Version"]);
        }
        #endregion
    }
}

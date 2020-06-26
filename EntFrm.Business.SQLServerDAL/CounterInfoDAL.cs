using EntFrm.Business.IDAL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using System;
using System.Data;
using System.Data.SqlClient;

namespace EntFrm.Business.SQLServerDAL
{
  public class CounterInfoDAL: ICounterInfo
  {
        #region sql
        private const string SQL_GET_ALL_RECORDS = @"Select *  From CounterInfo Where AppCode like @AppCode And ValidityState=1";
        private const string SQL_GET_RECORDS_BY_NO = @"Select * From CounterInfo Where   AppCode like @AppCode And   ValidityState=1 And CounterNo=@CounterNo";
        private const string SQL_GET_NAME_BY_NO = @"Select CounterName From CounterInfo Where   AppCode like @AppCode And   ValidityState=1 And CounterNo=@CounterNo";
        private const string SQL_ADD_RECORD = @"Insert into CounterInfo
                                              (CounterNo,CounterName,CounterAlias,ServiceGroupValue,ServiceGroupText,VoiceStyleNos,LedDisplayNo,LedAddress,CallerNo,CallerAddress,IsAutoLogon,LogonState,LogonStafferNo,PauseState,CalledNum,BranchNo,AddOptor,AddDate,ModOptor,ModDate,ValidityState,Comments,AppCode)
                                              values(@CounterNo,@CounterName,@CounterAlias,@ServiceGroupValue,@ServiceGroupText,@VoiceStyleNos,@LedDisplayNo,@LedAddress,@CallerNo,@CallerAddress,@IsAutoLogon,@LogonState,@LogonStafferNo,@PauseState,@CalledNum,@BranchNo,@AddOptor,@AddDate,@ModOptor,@ModDate,@ValidityState,@Comments,@AppCode)";
        private const string SQL_UPDATE_RECORD = @"Update CounterInfo set
                                                 CounterNo=@CounterNo,CounterName=@CounterName,CounterAlias=@CounterAlias,ServiceGroupValue=@ServiceGroupValue,ServiceGroupText=@ServiceGroupText,VoiceStyleNos=@VoiceStyleNos,LedDisplayNo=@LedDisplayNo,LedAddress=@LedAddress,CallerNo=@CallerNo,CallerAddress=@CallerAddress,IsAutoLogon=@IsAutoLogon,LogonState=@LogonState,LogonStafferNo=@LogonStafferNo,PauseState=@PauseState,CalledNum=@CalledNum,BranchNo=@BranchNo,AddOptor=@AddOptor,AddDate=@AddDate,ModOptor=@ModOptor,ModDate=@ModDate,ValidityState=@ValidityState,Comments=@Comments,AppCode=@AppCode 
                                                 Where  AppCode like @AppCode And   ValidityState=1 And CounterNo=@CounterNo  And Version=@Version";
        private const string SQL_HARD_DELETE_RECORD = @"Delete From CounterInfo Where   AppCode like @AppCode And   CounterNo=@CounterNo ";
        private const string SQL_SOFT_DELETE_RECORD = @"Update CounterInfo set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 And CounterNo=@CounterNo";
        private const string SQL_HARD_DELETE_BY_CONDTION = @"Delete From CounterInfo Where   AppCode like @AppCode ";
        private const string SQL_SOFT_DELETE_BY_CONDTION = @"Update CounterInfo set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 ";
        private const string SQL_GET_RECORDS_BY_CLASSNO = @"Select * From CounterInfo Where    AppCode like @AppCode And   ValidityState=1 And ClassNo=@ClassNo";
        private const string SQL_GET_COUNT_BY_CONDITION = @"Select Count(*) From CounterInfo Where   AppCode like @AppCode  And   ValidityState=1 ";
        #endregion

        #region param
        private const string PARAM_ID = "@ID";
        private const string PARAM_COUNTERNO = "@CounterNo";
        private const string PARAM_COUNTERNAME = "@CounterName";
        private const string PARAM_COUNTERALIAS = "@CounterAlias";
        private const string PARAM_SERVICEGROUPVALUE = "@ServiceGroupValue";
        private const string PARAM_SERVICEGROUPTEXT = "@ServiceGroupText";
        private const string PARAM_VOICESTYLENOS = "@VoiceStyleNos";
        private const string PARAM_LEDDISPLAYNO = "@LedDisplayNo";
        private const string PARAM_LEDADDRESS = "@LedAddress";
        private const string PARAM_CALLERNO = "@CallerNo";
        private const string PARAM_CALLERADDRESS = "@CallerAddress";
        private const string PARAM_ISAUTOLOGON = "@IsAutoLogon";
        private const string PARAM_LOGONSTATE = "@LogonState";
        private const string PARAM_LOGONSTAFFERNO = "@LogonStafferNo";
        private const string PARAM_PAUSESTATE = "@PauseState";
        private const string PARAM_CALLEDNUM = "@CalledNum";
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

        public CounterInfoDAL(string sConnStr,string sAppCode)
        {
           this.connStr = sConnStr;
           this.appCode = sAppCode;
        }

        public CounterInfoCollections GetAllRecords()
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            CounterInfoCollections infos = null;
            CounterInfo info = null;

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
                    infos = new CounterInfoCollections();
                    while (reader.Read())
                    {
                        info = new CounterInfo();
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

        public CounterInfoCollections GetRecordsByClassNo(string sClassNo)
        {
            /*SqlConnection connection = null;
            SqlDataReader reader = null;
            CounterInfoCollections infos = null;
            CounterInfo info = null;

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
                    infos = new CounterInfoCollections();
                    while (reader.Read())
                    {
                        info = new CounterInfo();
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

        public CounterInfoCollections GetRecordsByNo(string sNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            CounterInfoCollections infos = null;
            CounterInfo info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_COUNTERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_NO,paras);

                if (reader.HasRows)
                {
                    infos = new CounterInfoCollections();
                    while (reader.Read())
                    {
                        info = new CounterInfo();
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
                    new SqlParameter(PARAM_COUNTERNO,SqlDbType.NVarChar,20),
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

        public int AddNewRecord(CounterInfo info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_COUNTERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_COUNTERNAME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_COUNTERALIAS,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_SERVICEGROUPVALUE,SqlDbType.NVarChar,1024),
                    new SqlParameter(PARAM_SERVICEGROUPTEXT,SqlDbType.NVarChar,1024),
                    new SqlParameter(PARAM_VOICESTYLENOS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_LEDDISPLAYNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_LEDADDRESS,SqlDbType.Int),
                    new SqlParameter(PARAM_CALLERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_CALLERADDRESS,SqlDbType.Int),
                    new SqlParameter(PARAM_ISAUTOLOGON,SqlDbType.Int),
                    new SqlParameter(PARAM_LOGONSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_LOGONSTAFFERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_PAUSESTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_CALLEDNUM,SqlDbType.Int),
                    new SqlParameter(PARAM_BRANCHNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_MODOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MODDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_VALIDITYSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_COMMENTS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = info.sCounterNo;
                paras[1].Value = info.sCounterName;
                paras[2].Value = info.sCounterAlias;
                paras[3].Value = info.sServiceGroupValue;
                paras[4].Value = info.sServiceGroupText;
                paras[5].Value = info.sVoiceStyleNos;
                paras[6].Value = info.sLedDisplayNo;
                paras[7].Value = info.iLedAddress;
                paras[8].Value = info.sCallerNo;
                paras[9].Value = info.iCallerAddress;
                paras[10].Value = info.iIsAutoLogon;
                paras[11].Value = info.iLogonState;
                paras[12].Value = info.sLogonStafferNo;
                paras[13].Value = info.iPauseState;
                paras[14].Value = info.iCalledNum;
                paras[15].Value = info.sBranchNo;
                paras[16].Value = info.sAddOptor;
                paras[17].Value = info.dAddDate;
                paras[18].Value = info.sModOptor;
                paras[19].Value = info.dModDate;
                paras[20].Value = info.iValidityState;
                paras[21].Value = info.sComments;
                paras[22].Value = info.sAppCode;

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

        public int UpdateRecord(CounterInfo info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_COUNTERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_COUNTERNAME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_COUNTERALIAS,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_SERVICEGROUPVALUE,SqlDbType.NVarChar,1024),
                    new SqlParameter(PARAM_SERVICEGROUPTEXT,SqlDbType.NVarChar,1024),
                    new SqlParameter(PARAM_VOICESTYLENOS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_LEDDISPLAYNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_LEDADDRESS,SqlDbType.Int),
                    new SqlParameter(PARAM_CALLERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_CALLERADDRESS,SqlDbType.Int),
                    new SqlParameter(PARAM_ISAUTOLOGON,SqlDbType.Int),
                    new SqlParameter(PARAM_LOGONSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_LOGONSTAFFERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_PAUSESTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_CALLEDNUM,SqlDbType.Int),
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
                paras[0].Value = info.sCounterNo;
                paras[1].Value = info.sCounterName;
                paras[2].Value = info.sCounterAlias;
                paras[3].Value = info.sServiceGroupValue;
                paras[4].Value = info.sServiceGroupText;
                paras[5].Value = info.sVoiceStyleNos;
                paras[6].Value = info.sLedDisplayNo;
                paras[7].Value = info.iLedAddress;
                paras[8].Value = info.sCallerNo;
                paras[9].Value = info.iCallerAddress;
                paras[10].Value = info.iIsAutoLogon;
                paras[11].Value = info.iLogonState;
                paras[12].Value = info.sLogonStafferNo;
                paras[13].Value = info.iPauseState;
                paras[14].Value = info.iCalledNum;
                paras[15].Value = info.sBranchNo;
                paras[16].Value = info.sAddOptor;
                paras[17].Value = info.dAddDate;
                paras[18].Value = info.sModOptor;
                paras[19].Value = info.dModDate;
                paras[20].Value = info.iValidityState;
                paras[21].Value = info.sComments;
                paras[22].Value = info.sAppCode;
                paras[23].Value = StringHelper.ConvertToBytes(info.sVersion);

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
                    new SqlParameter(PARAM_COUNTERNO,SqlDbType.NVarChar,20),
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
                    new SqlParameter(PARAM_COUNTERNO,SqlDbType.NVarChar,20),
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
        public CounterInfoCollections GetRecords_Paging(SqlModel s_model)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            CounterInfoCollections infos = null;
            CounterInfo info = null;

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
                    infos = new CounterInfoCollections();
                    while (reader.Read())
                    {
                        info = new CounterInfo();
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
        internal static void PutObjectProperty(CounterInfo obj_info, SqlDataReader reader)
        {
            obj_info.iID= int.Parse(reader["ID"].ToString());
            obj_info.sCounterNo= reader["CounterNo"].ToString();
            obj_info.sCounterName= reader["CounterName"].ToString();
            obj_info.sCounterAlias= reader["CounterAlias"].ToString();
            obj_info.sServiceGroupValue= reader["ServiceGroupValue"].ToString();
            obj_info.sServiceGroupText= reader["ServiceGroupText"].ToString();
            obj_info.sVoiceStyleNos= reader["VoiceStyleNos"].ToString();
            obj_info.sLedDisplayNo= reader["LedDisplayNo"].ToString();
            obj_info.iLedAddress= int.Parse(reader["LedAddress"].ToString());
            obj_info.sCallerNo= reader["CallerNo"].ToString();
            obj_info.iCallerAddress= int.Parse(reader["CallerAddress"].ToString());
            obj_info.iIsAutoLogon= int.Parse(reader["IsAutoLogon"].ToString());
            obj_info.iLogonState= int.Parse(reader["LogonState"].ToString());
            obj_info.sLogonStafferNo= reader["LogonStafferNo"].ToString();
            obj_info.iPauseState= int.Parse(reader["PauseState"].ToString());
            obj_info.iCalledNum= int.Parse(reader["CalledNum"].ToString());
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

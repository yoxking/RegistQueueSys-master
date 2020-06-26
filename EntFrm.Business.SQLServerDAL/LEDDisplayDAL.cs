using EntFrm.Business.IDAL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using System;
using System.Data;
using System.Data.SqlClient;

namespace EntFrm.Business.SQLServerDAL
{
  public class LEDDisplayDAL: ILEDDisplay
  {
        #region sql
        private const string SQL_GET_ALL_RECORDS = @"Select *  From LEDDisplay Where AppCode like @AppCode And ValidityState=1";
        private const string SQL_GET_RECORDS_BY_NO = @"Select * From LEDDisplay Where   AppCode like @AppCode And   ValidityState=1 And DisplayNo=@DisplayNo";
        private const string SQL_GET_NAME_BY_NO = @"Select DisplayName From LEDDisplay Where   AppCode like @AppCode And   ValidityState=1 And DisplayNo=@DisplayNo";
        private const string SQL_ADD_RECORD = @"Insert into LEDDisplay
                                              (DisplayNo,DisplayName,CounterNos,LedModel,PhyAddr,Protocol,SerialPort,Baudrate,IpAddress,LocalPort,ParamFormat,TimeoutSec,DisplayFormat,DisplayLength,PowerOnTip,InServiceTip,OnPauseTip,TimeoutTip,UpdateFlag,UpdateTime,BranchNo,AddOptor,AddDate,ModOptor,ModDate,ValidityState,Comments,AppCode)
                                              values(@DisplayNo,@DisplayName,@CounterNos,@LedModel,@PhyAddr,@Protocol,@SerialPort,@Baudrate,@IpAddress,@LocalPort,@ParamFormat,@TimeoutSec,@DisplayFormat,@DisplayLength,@PowerOnTip,@InServiceTip,@OnPauseTip,@TimeoutTip,@UpdateFlag,@UpdateTime,@BranchNo,@AddOptor,@AddDate,@ModOptor,@ModDate,@ValidityState,@Comments,@AppCode)";
        private const string SQL_UPDATE_RECORD = @"Update LEDDisplay set
                                                 DisplayNo=@DisplayNo,DisplayName=@DisplayName,CounterNos=@CounterNos,LedModel=@LedModel,PhyAddr=@PhyAddr,Protocol=@Protocol,SerialPort=@SerialPort,Baudrate=@Baudrate,IpAddress=@IpAddress,LocalPort=@LocalPort,ParamFormat=@ParamFormat,TimeoutSec=@TimeoutSec,DisplayFormat=@DisplayFormat,DisplayLength=@DisplayLength,PowerOnTip=@PowerOnTip,InServiceTip=@InServiceTip,OnPauseTip=@OnPauseTip,TimeoutTip=@TimeoutTip,UpdateFlag=@UpdateFlag,UpdateTime=@UpdateTime,BranchNo=@BranchNo,AddOptor=@AddOptor,AddDate=@AddDate,ModOptor=@ModOptor,ModDate=@ModDate,ValidityState=@ValidityState,Comments=@Comments,AppCode=@AppCode 
                                                 Where  AppCode like @AppCode And   ValidityState=1 And DisplayNo=@DisplayNo  And Version=@Version";
        private const string SQL_HARD_DELETE_RECORD = @"Delete From LEDDisplay Where   AppCode like @AppCode And   DisplayNo=@DisplayNo ";
        private const string SQL_SOFT_DELETE_RECORD = @"Update LEDDisplay set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 And DisplayNo=@DisplayNo";
        private const string SQL_HARD_DELETE_BY_CONDTION = @"Delete From LEDDisplay Where   AppCode like @AppCode ";
        private const string SQL_SOFT_DELETE_BY_CONDTION = @"Update LEDDisplay set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 ";
        private const string SQL_GET_RECORDS_BY_CLASSNO = @"Select * From LEDDisplay Where    AppCode like @AppCode And   ValidityState=1 And ClassNo=@ClassNo";
        private const string SQL_GET_COUNT_BY_CONDITION = @"Select Count(*) From LEDDisplay Where   AppCode like @AppCode  And   ValidityState=1 ";
        #endregion

        #region param
        private const string PARAM_ID = "@ID";
        private const string PARAM_DISPLAYNO = "@DisplayNo";
        private const string PARAM_DISPLAYNAME = "@DisplayName";
        private const string PARAM_COUNTERNOS = "@CounterNos";
        private const string PARAM_LEDMODEL = "@LedModel";
        private const string PARAM_PHYADDR = "@PhyAddr";
        private const string PARAM_PROTOCOL = "@Protocol";
        private const string PARAM_SERIALPORT = "@SerialPort";
        private const string PARAM_BAUDRATE = "@Baudrate";
        private const string PARAM_IPADDRESS = "@IpAddress";
        private const string PARAM_LOCALPORT = "@LocalPort";
        private const string PARAM_PARAMFORMAT = "@ParamFormat";
        private const string PARAM_TIMEOUTSEC = "@TimeoutSec";
        private const string PARAM_DISPLAYFORMAT = "@DisplayFormat";
        private const string PARAM_DISPLAYLENGTH = "@DisplayLength";
        private const string PARAM_POWERONTIP = "@PowerOnTip";
        private const string PARAM_INSERVICETIP = "@InServiceTip";
        private const string PARAM_ONPAUSETIP = "@OnPauseTip";
        private const string PARAM_TIMEOUTTIP = "@TimeoutTip";
        private const string PARAM_UPDATEFLAG = "@UpdateFlag";
        private const string PARAM_UPDATETIME = "@UpdateTime";
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

        public LEDDisplayDAL(string sConnStr,string sAppCode)
        {
           this.connStr = sConnStr;
           this.appCode = sAppCode;
        }

        public LEDDisplayCollections GetAllRecords()
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            LEDDisplayCollections infos = null;
            LEDDisplay info = null;

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
                    infos = new LEDDisplayCollections();
                    while (reader.Read())
                    {
                        info = new LEDDisplay();
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

        public LEDDisplayCollections GetRecordsByClassNo(string sClassNo)
        {
            /*SqlConnection connection = null;
            SqlDataReader reader = null;
            LEDDisplayCollections infos = null;
            LEDDisplay info = null;

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
                    infos = new LEDDisplayCollections();
                    while (reader.Read())
                    {
                        info = new LEDDisplay();
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

        public LEDDisplayCollections GetRecordsByNo(string sNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            LEDDisplayCollections infos = null;
            LEDDisplay info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_DISPLAYNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_NO,paras);

                if (reader.HasRows)
                {
                    infos = new LEDDisplayCollections();
                    while (reader.Read())
                    {
                        info = new LEDDisplay();
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
                    new SqlParameter(PARAM_DISPLAYNO,SqlDbType.NVarChar,20),
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

        public int AddNewRecord(LEDDisplay info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_DISPLAYNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_DISPLAYNAME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_COUNTERNOS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_LEDMODEL,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_PHYADDR,SqlDbType.Int),
                    new SqlParameter(PARAM_PROTOCOL,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_SERIALPORT,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_BAUDRATE,SqlDbType.Int),
                    new SqlParameter(PARAM_IPADDRESS,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_LOCALPORT,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_PARAMFORMAT,SqlDbType.NVarChar,1073741823),
                    new SqlParameter(PARAM_TIMEOUTSEC,SqlDbType.Int),
                    new SqlParameter(PARAM_DISPLAYFORMAT,SqlDbType.NVarChar,1073741823),
                    new SqlParameter(PARAM_DISPLAYLENGTH,SqlDbType.Int),
                    new SqlParameter(PARAM_POWERONTIP,SqlDbType.NVarChar,1073741823),
                    new SqlParameter(PARAM_INSERVICETIP,SqlDbType.NVarChar,1073741823),
                    new SqlParameter(PARAM_ONPAUSETIP,SqlDbType.NVarChar,1073741823),
                    new SqlParameter(PARAM_TIMEOUTTIP,SqlDbType.NVarChar,1073741823),
                    new SqlParameter(PARAM_UPDATEFLAG,SqlDbType.Int),
                    new SqlParameter(PARAM_UPDATETIME,SqlDbType.DateTime),
                    new SqlParameter(PARAM_BRANCHNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_MODOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MODDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_VALIDITYSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_COMMENTS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = info.sDisplayNo;
                paras[1].Value = info.sDisplayName;
                paras[2].Value = info.sCounterNos;
                paras[3].Value = info.sLedModel;
                paras[4].Value = info.iPhyAddr;
                paras[5].Value = info.sProtocol;
                paras[6].Value = info.sSerialPort;
                paras[7].Value = info.iBaudrate;
                paras[8].Value = info.sIpAddress;
                paras[9].Value = info.sLocalPort;
                paras[10].Value = info.sParamFormat;
                paras[11].Value = info.iTimeoutSec;
                paras[12].Value = info.sDisplayFormat;
                paras[13].Value = info.iDisplayLength;
                paras[14].Value = info.sPowerOnTip;
                paras[15].Value = info.sInServiceTip;
                paras[16].Value = info.sOnPauseTip;
                paras[17].Value = info.sTimeoutTip;
                paras[18].Value = info.iUpdateFlag;
                paras[19].Value = info.dUpdateTime;
                paras[20].Value = info.sBranchNo;
                paras[21].Value = info.sAddOptor;
                paras[22].Value = info.dAddDate;
                paras[23].Value = info.sModOptor;
                paras[24].Value = info.dModDate;
                paras[25].Value = info.iValidityState;
                paras[26].Value = info.sComments;
                paras[27].Value = info.sAppCode;

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

        public int UpdateRecord(LEDDisplay info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_DISPLAYNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_DISPLAYNAME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_COUNTERNOS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_LEDMODEL,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_PHYADDR,SqlDbType.Int),
                    new SqlParameter(PARAM_PROTOCOL,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_SERIALPORT,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_BAUDRATE,SqlDbType.Int),
                    new SqlParameter(PARAM_IPADDRESS,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_LOCALPORT,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_PARAMFORMAT,SqlDbType.NVarChar,1073741823),
                    new SqlParameter(PARAM_TIMEOUTSEC,SqlDbType.Int),
                    new SqlParameter(PARAM_DISPLAYFORMAT,SqlDbType.NVarChar,1073741823),
                    new SqlParameter(PARAM_DISPLAYLENGTH,SqlDbType.Int),
                    new SqlParameter(PARAM_POWERONTIP,SqlDbType.NVarChar,1073741823),
                    new SqlParameter(PARAM_INSERVICETIP,SqlDbType.NVarChar,1073741823),
                    new SqlParameter(PARAM_ONPAUSETIP,SqlDbType.NVarChar,1073741823),
                    new SqlParameter(PARAM_TIMEOUTTIP,SqlDbType.NVarChar,1073741823),
                    new SqlParameter(PARAM_UPDATEFLAG,SqlDbType.Int),
                    new SqlParameter(PARAM_UPDATETIME,SqlDbType.DateTime),
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
                paras[0].Value = info.sDisplayNo;
                paras[1].Value = info.sDisplayName;
                paras[2].Value = info.sCounterNos;
                paras[3].Value = info.sLedModel;
                paras[4].Value = info.iPhyAddr;
                paras[5].Value = info.sProtocol;
                paras[6].Value = info.sSerialPort;
                paras[7].Value = info.iBaudrate;
                paras[8].Value = info.sIpAddress;
                paras[9].Value = info.sLocalPort;
                paras[10].Value = info.sParamFormat;
                paras[11].Value = info.iTimeoutSec;
                paras[12].Value = info.sDisplayFormat;
                paras[13].Value = info.iDisplayLength;
                paras[14].Value = info.sPowerOnTip;
                paras[15].Value = info.sInServiceTip;
                paras[16].Value = info.sOnPauseTip;
                paras[17].Value = info.sTimeoutTip;
                paras[18].Value = info.iUpdateFlag;
                paras[19].Value = info.dUpdateTime;
                paras[20].Value = info.sBranchNo;
                paras[21].Value = info.sAddOptor;
                paras[22].Value = info.dAddDate;
                paras[23].Value = info.sModOptor;
                paras[24].Value = info.dModDate;
                paras[25].Value = info.iValidityState;
                paras[26].Value = info.sComments;
                paras[27].Value = info.sAppCode;
                paras[28].Value = StringHelper.ConvertToBytes(info.sVersion);

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
                    new SqlParameter(PARAM_DISPLAYNO,SqlDbType.NVarChar,20),
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
                    new SqlParameter(PARAM_DISPLAYNO,SqlDbType.NVarChar,20),
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
        public LEDDisplayCollections GetRecords_Paging(SqlModel s_model)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            LEDDisplayCollections infos = null;
            LEDDisplay info = null;

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
                    infos = new LEDDisplayCollections();
                    while (reader.Read())
                    {
                        info = new LEDDisplay();
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
        internal static void PutObjectProperty(LEDDisplay obj_info, SqlDataReader reader)
        {
            obj_info.iID= int.Parse(reader["ID"].ToString());
            obj_info.sDisplayNo= reader["DisplayNo"].ToString();
            obj_info.sDisplayName= reader["DisplayName"].ToString();
            obj_info.sCounterNos= reader["CounterNos"].ToString();
            obj_info.sLedModel= reader["LedModel"].ToString();
            obj_info.iPhyAddr= int.Parse(reader["PhyAddr"].ToString());
            obj_info.sProtocol= reader["Protocol"].ToString();
            obj_info.sSerialPort= reader["SerialPort"].ToString();
            obj_info.iBaudrate= int.Parse(reader["Baudrate"].ToString());
            obj_info.sIpAddress= reader["IpAddress"].ToString();
            obj_info.sLocalPort= reader["LocalPort"].ToString();
            obj_info.sParamFormat= reader["ParamFormat"].ToString();
            obj_info.iTimeoutSec= int.Parse(reader["TimeoutSec"].ToString());
            obj_info.sDisplayFormat= reader["DisplayFormat"].ToString();
            obj_info.iDisplayLength= int.Parse(reader["DisplayLength"].ToString());
            obj_info.sPowerOnTip= reader["PowerOnTip"].ToString();
            obj_info.sInServiceTip= reader["InServiceTip"].ToString();
            obj_info.sOnPauseTip= reader["OnPauseTip"].ToString();
            obj_info.sTimeoutTip= reader["TimeoutTip"].ToString();
            obj_info.iUpdateFlag= int.Parse(reader["UpdateFlag"].ToString());
            obj_info.dUpdateTime= DateTime.Parse(reader["UpdateTime"].ToString());
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

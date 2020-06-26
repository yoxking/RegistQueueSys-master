using EntFrm.Business.IDAL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using System;
using System.Data;
using System.Data.SqlClient;

namespace EntFrm.Business.SQLServerDAL
{
  public class CallerInfoDAL: ICallerInfo
  {
        #region sql
        private const string SQL_GET_ALL_RECORDS = @"Select *  From CallerInfo Where AppCode like @AppCode And ValidityState=1";
        private const string SQL_GET_RECORDS_BY_NO = @"Select * From CallerInfo Where   AppCode like @AppCode And   ValidityState=1 And CallerNo=@CallerNo";
        private const string SQL_GET_NAME_BY_NO = @"Select CallerName From CallerInfo Where   AppCode like @AppCode And   ValidityState=1 And CallerNo=@CallerNo";
        private const string SQL_ADD_RECORD = @"Insert into CallerInfo
                                              (CallerNo,CallerName,Protocol,SerialPort,CommMode,Baudrate,PhyAddr,EvalorNo,TimeoutSec,UpdateFlag,UpdateTime,CheckState,BranchNo,AddOptor,AddDate,ModOptor,ModDate,ValidityState,Comments,AppCode)
                                              values(@CallerNo,@CallerName,@Protocol,@SerialPort,@CommMode,@Baudrate,@PhyAddr,@EvalorNo,@TimeoutSec,@UpdateFlag,@UpdateTime,@CheckState,@BranchNo,@AddOptor,@AddDate,@ModOptor,@ModDate,@ValidityState,@Comments,@AppCode)";
        private const string SQL_UPDATE_RECORD = @"Update CallerInfo set
                                                 CallerNo=@CallerNo,CallerName=@CallerName,Protocol=@Protocol,SerialPort=@SerialPort,CommMode=@CommMode,Baudrate=@Baudrate,PhyAddr=@PhyAddr,EvalorNo=@EvalorNo,TimeoutSec=@TimeoutSec,UpdateFlag=@UpdateFlag,UpdateTime=@UpdateTime,CheckState=@CheckState,BranchNo=@BranchNo,AddOptor=@AddOptor,AddDate=@AddDate,ModOptor=@ModOptor,ModDate=@ModDate,ValidityState=@ValidityState,Comments=@Comments,AppCode=@AppCode 
                                                 Where  AppCode like @AppCode And   ValidityState=1 And CallerNo=@CallerNo  And Version=@Version";
        private const string SQL_HARD_DELETE_RECORD = @"Delete From CallerInfo Where   AppCode like @AppCode And   CallerNo=@CallerNo ";
        private const string SQL_SOFT_DELETE_RECORD = @"Update CallerInfo set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 And CallerNo=@CallerNo";
        private const string SQL_HARD_DELETE_BY_CONDTION = @"Delete From CallerInfo Where   AppCode like @AppCode ";
        private const string SQL_SOFT_DELETE_BY_CONDTION = @"Update CallerInfo set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 ";
        private const string SQL_GET_RECORDS_BY_CLASSNO = @"Select * From CallerInfo Where    AppCode like @AppCode And   ValidityState=1 And ClassNo=@ClassNo";
        private const string SQL_GET_COUNT_BY_CONDITION = @"Select Count(*) From CallerInfo Where   AppCode like @AppCode  And   ValidityState=1 ";
        #endregion

        #region param
        private const string PARAM_ID = "@ID";
        private const string PARAM_CALLERNO = "@CallerNo";
        private const string PARAM_CALLERNAME = "@CallerName";
        private const string PARAM_PROTOCOL = "@Protocol";
        private const string PARAM_SERIALPORT = "@SerialPort";
        private const string PARAM_COMMMODE = "@CommMode";
        private const string PARAM_BAUDRATE = "@Baudrate";
        private const string PARAM_PHYADDR = "@PhyAddr";
        private const string PARAM_EVALORNO = "@EvalorNo";
        private const string PARAM_TIMEOUTSEC = "@TimeoutSec";
        private const string PARAM_UPDATEFLAG = "@UpdateFlag";
        private const string PARAM_UPDATETIME = "@UpdateTime";
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

        public CallerInfoDAL(string sConnStr,string sAppCode)
        {
           this.connStr = sConnStr;
           this.appCode = sAppCode;
        }

        public CallerInfoCollections GetAllRecords()
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            CallerInfoCollections infos = null;
            CallerInfo info = null;

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
                    infos = new CallerInfoCollections();
                    while (reader.Read())
                    {
                        info = new CallerInfo();
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

        public CallerInfoCollections GetRecordsByClassNo(string sClassNo)
        {
            /*SqlConnection connection = null;
            SqlDataReader reader = null;
            CallerInfoCollections infos = null;
            CallerInfo info = null;

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
                    infos = new CallerInfoCollections();
                    while (reader.Read())
                    {
                        info = new CallerInfo();
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

        public CallerInfoCollections GetRecordsByNo(string sNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            CallerInfoCollections infos = null;
            CallerInfo info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_CALLERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_NO,paras);

                if (reader.HasRows)
                {
                    infos = new CallerInfoCollections();
                    while (reader.Read())
                    {
                        info = new CallerInfo();
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
                    new SqlParameter(PARAM_CALLERNO,SqlDbType.NVarChar,20),
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

        public int AddNewRecord(CallerInfo info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_CALLERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_CALLERNAME,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_PROTOCOL,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_SERIALPORT,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_COMMMODE,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_BAUDRATE,SqlDbType.Int),
                    new SqlParameter(PARAM_PHYADDR,SqlDbType.Int),
                    new SqlParameter(PARAM_EVALORNO,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_TIMEOUTSEC,SqlDbType.Int),
                    new SqlParameter(PARAM_UPDATEFLAG,SqlDbType.Int),
                    new SqlParameter(PARAM_UPDATETIME,SqlDbType.DateTime),
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
                paras[0].Value = info.sCallerNo;
                paras[1].Value = info.sCallerName;
                paras[2].Value = info.sProtocol;
                paras[3].Value = info.sSerialPort;
                paras[4].Value = info.sCommMode;
                paras[5].Value = info.iBaudrate;
                paras[6].Value = info.iPhyAddr;
                paras[7].Value = info.sEvalorNo;
                paras[8].Value = info.iTimeoutSec;
                paras[9].Value = info.iUpdateFlag;
                paras[10].Value = info.dUpdateTime;
                paras[11].Value = info.iCheckState;
                paras[12].Value = info.sBranchNo;
                paras[13].Value = info.sAddOptor;
                paras[14].Value = info.dAddDate;
                paras[15].Value = info.sModOptor;
                paras[16].Value = info.dModDate;
                paras[17].Value = info.iValidityState;
                paras[18].Value = info.sComments;
                paras[19].Value = info.sAppCode;

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

        public int UpdateRecord(CallerInfo info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_CALLERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_CALLERNAME,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_PROTOCOL,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_SERIALPORT,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_COMMMODE,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_BAUDRATE,SqlDbType.Int),
                    new SqlParameter(PARAM_PHYADDR,SqlDbType.Int),
                    new SqlParameter(PARAM_EVALORNO,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_TIMEOUTSEC,SqlDbType.Int),
                    new SqlParameter(PARAM_UPDATEFLAG,SqlDbType.Int),
                    new SqlParameter(PARAM_UPDATETIME,SqlDbType.DateTime),
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
                paras[0].Value = info.sCallerNo;
                paras[1].Value = info.sCallerName;
                paras[2].Value = info.sProtocol;
                paras[3].Value = info.sSerialPort;
                paras[4].Value = info.sCommMode;
                paras[5].Value = info.iBaudrate;
                paras[6].Value = info.iPhyAddr;
                paras[7].Value = info.sEvalorNo;
                paras[8].Value = info.iTimeoutSec;
                paras[9].Value = info.iUpdateFlag;
                paras[10].Value = info.dUpdateTime;
                paras[11].Value = info.iCheckState;
                paras[12].Value = info.sBranchNo;
                paras[13].Value = info.sAddOptor;
                paras[14].Value = info.dAddDate;
                paras[15].Value = info.sModOptor;
                paras[16].Value = info.dModDate;
                paras[17].Value = info.iValidityState;
                paras[18].Value = info.sComments;
                paras[19].Value = info.sAppCode;
                paras[20].Value = StringHelper.ConvertToBytes(info.sVersion);

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
                    new SqlParameter(PARAM_CALLERNO,SqlDbType.NVarChar,20),
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
                    new SqlParameter(PARAM_CALLERNO,SqlDbType.NVarChar,20),
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
        public CallerInfoCollections GetRecords_Paging(SqlModel s_model)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            CallerInfoCollections infos = null;
            CallerInfo info = null;

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
                    infos = new CallerInfoCollections();
                    while (reader.Read())
                    {
                        info = new CallerInfo();
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
        internal static void PutObjectProperty(CallerInfo obj_info, SqlDataReader reader)
        {
            obj_info.iID= int.Parse(reader["ID"].ToString());
            obj_info.sCallerNo= reader["CallerNo"].ToString();
            obj_info.sCallerName= reader["CallerName"].ToString();
            obj_info.sProtocol= reader["Protocol"].ToString();
            obj_info.sSerialPort= reader["SerialPort"].ToString();
            obj_info.sCommMode= reader["CommMode"].ToString();
            obj_info.iBaudrate= int.Parse(reader["Baudrate"].ToString());
            obj_info.iPhyAddr= int.Parse(reader["PhyAddr"].ToString());
            obj_info.sEvalorNo= reader["EvalorNo"].ToString();
            obj_info.iTimeoutSec= int.Parse(reader["TimeoutSec"].ToString());
            obj_info.iUpdateFlag= int.Parse(reader["UpdateFlag"].ToString());
            obj_info.dUpdateTime= DateTime.Parse(reader["UpdateTime"].ToString());
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

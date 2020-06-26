using EntFrm.Business.IDAL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using System;
using System.Data;
using System.Data.SqlClient;

namespace EntFrm.Business.SQLServerDAL
{
  public class LEDMatrixDAL: ILEDMatrix
  {
        #region sql
        private const string SQL_GET_ALL_RECORDS = @"Select *  From LEDMatrix Where AppCode like @AppCode And ValidityState=1";
        private const string SQL_GET_RECORDS_BY_NO = @"Select * From LEDMatrix Where   AppCode like @AppCode And   ValidityState=1 And MatrixNo=@MatrixNo";
        private const string SQL_GET_NAME_BY_NO = @"Select MatrixName From LEDMatrix Where   AppCode like @AppCode And   ValidityState=1 And MatrixNo=@MatrixNo";
        private const string SQL_ADD_RECORD = @"Insert into LEDMatrix
                                              (MatrixNo,MatrixName,MatrixModel,ServiceNos,PhyAddr,Protocol,SerialPort,Baudrate,IpAddress,LocalPort,ParamFormat,TimeoutSec,DisplayRows,DisplayFormat,BranchNo,AddOptor,AddDate,ModOptor,ModDate,ValidityState,Comments,AppCode)
                                              values(@MatrixNo,@MatrixName,@MatrixModel,@ServiceNos,@PhyAddr,@Protocol,@SerialPort,@Baudrate,@IpAddress,@LocalPort,@ParamFormat,@TimeoutSec,@DisplayRows,@DisplayFormat,@BranchNo,@AddOptor,@AddDate,@ModOptor,@ModDate,@ValidityState,@Comments,@AppCode)";
        private const string SQL_UPDATE_RECORD = @"Update LEDMatrix set
                                                 MatrixNo=@MatrixNo,MatrixName=@MatrixName,MatrixModel=@MatrixModel,ServiceNos=@ServiceNos,PhyAddr=@PhyAddr,Protocol=@Protocol,SerialPort=@SerialPort,Baudrate=@Baudrate,IpAddress=@IpAddress,LocalPort=@LocalPort,ParamFormat=@ParamFormat,TimeoutSec=@TimeoutSec,DisplayRows=@DisplayRows,DisplayFormat=@DisplayFormat,BranchNo=@BranchNo,AddOptor=@AddOptor,AddDate=@AddDate,ModOptor=@ModOptor,ModDate=@ModDate,ValidityState=@ValidityState,Comments=@Comments,AppCode=@AppCode 
                                                 Where  AppCode like @AppCode And   ValidityState=1 And MatrixNo=@MatrixNo  And Version=@Version";
        private const string SQL_HARD_DELETE_RECORD = @"Delete From LEDMatrix Where   AppCode like @AppCode And   MatrixNo=@MatrixNo ";
        private const string SQL_SOFT_DELETE_RECORD = @"Update LEDMatrix set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 And MatrixNo=@MatrixNo";
        private const string SQL_HARD_DELETE_BY_CONDTION = @"Delete From LEDMatrix Where   AppCode like @AppCode ";
        private const string SQL_SOFT_DELETE_BY_CONDTION = @"Update LEDMatrix set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 ";
        private const string SQL_GET_RECORDS_BY_CLASSNO = @"Select * From LEDMatrix Where    AppCode like @AppCode And   ValidityState=1 And ClassNo=@ClassNo";
        private const string SQL_GET_COUNT_BY_CONDITION = @"Select Count(*) From LEDMatrix Where   AppCode like @AppCode  And   ValidityState=1 ";
        #endregion

        #region param
        private const string PARAM_ID = "@ID";
        private const string PARAM_MATRIXNO = "@MatrixNo";
        private const string PARAM_MATRIXNAME = "@MatrixName";
        private const string PARAM_MATRIXMODEL = "@MatrixModel";
        private const string PARAM_SERVICENOS = "@ServiceNos";
        private const string PARAM_PHYADDR = "@PhyAddr";
        private const string PARAM_PROTOCOL = "@Protocol";
        private const string PARAM_SERIALPORT = "@SerialPort";
        private const string PARAM_BAUDRATE = "@Baudrate";
        private const string PARAM_IPADDRESS = "@IpAddress";
        private const string PARAM_LOCALPORT = "@LocalPort";
        private const string PARAM_PARAMFORMAT = "@ParamFormat";
        private const string PARAM_TIMEOUTSEC = "@TimeoutSec";
        private const string PARAM_DISPLAYROWS = "@DisplayRows";
        private const string PARAM_DISPLAYFORMAT = "@DisplayFormat";
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

        public LEDMatrixDAL(string sConnStr,string sAppCode)
        {
           this.connStr = sConnStr;
           this.appCode = sAppCode;
        }

        public LEDMatrixCollections GetAllRecords()
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            LEDMatrixCollections infos = null;
            LEDMatrix info = null;

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
                    infos = new LEDMatrixCollections();
                    while (reader.Read())
                    {
                        info = new LEDMatrix();
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

        public LEDMatrixCollections GetRecordsByClassNo(string sClassNo)
        {
            /*SqlConnection connection = null;
            SqlDataReader reader = null;
            LEDMatrixCollections infos = null;
            LEDMatrix info = null;

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
                    infos = new LEDMatrixCollections();
                    while (reader.Read())
                    {
                        info = new LEDMatrix();
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

        public LEDMatrixCollections GetRecordsByNo(string sNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            LEDMatrixCollections infos = null;
            LEDMatrix info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_MATRIXNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_NO,paras);

                if (reader.HasRows)
                {
                    infos = new LEDMatrixCollections();
                    while (reader.Read())
                    {
                        info = new LEDMatrix();
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
                    new SqlParameter(PARAM_MATRIXNO,SqlDbType.NVarChar,20),
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

        public int AddNewRecord(LEDMatrix info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_MATRIXNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MATRIXNAME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_MATRIXMODEL,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_SERVICENOS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_PHYADDR,SqlDbType.Int),
                    new SqlParameter(PARAM_PROTOCOL,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_SERIALPORT,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_BAUDRATE,SqlDbType.Int),
                    new SqlParameter(PARAM_IPADDRESS,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_LOCALPORT,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_PARAMFORMAT,SqlDbType.NVarChar,1073741823),
                    new SqlParameter(PARAM_TIMEOUTSEC,SqlDbType.Int),
                    new SqlParameter(PARAM_DISPLAYROWS,SqlDbType.Int),
                    new SqlParameter(PARAM_DISPLAYFORMAT,SqlDbType.NVarChar,1073741823),
                    new SqlParameter(PARAM_BRANCHNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_MODOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MODDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_VALIDITYSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_COMMENTS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = info.sMatrixNo;
                paras[1].Value = info.sMatrixName;
                paras[2].Value = info.sMatrixModel;
                paras[3].Value = info.sServiceNos;
                paras[4].Value = info.iPhyAddr;
                paras[5].Value = info.sProtocol;
                paras[6].Value = info.sSerialPort;
                paras[7].Value = info.iBaudrate;
                paras[8].Value = info.sIpAddress;
                paras[9].Value = info.sLocalPort;
                paras[10].Value = info.sParamFormat;
                paras[11].Value = info.iTimeoutSec;
                paras[12].Value = info.iDisplayRows;
                paras[13].Value = info.sDisplayFormat;
                paras[14].Value = info.sBranchNo;
                paras[15].Value = info.sAddOptor;
                paras[16].Value = info.dAddDate;
                paras[17].Value = info.sModOptor;
                paras[18].Value = info.dModDate;
                paras[19].Value = info.iValidityState;
                paras[20].Value = info.sComments;
                paras[21].Value = info.sAppCode;

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

        public int UpdateRecord(LEDMatrix info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_MATRIXNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MATRIXNAME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_MATRIXMODEL,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_SERVICENOS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_PHYADDR,SqlDbType.Int),
                    new SqlParameter(PARAM_PROTOCOL,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_SERIALPORT,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_BAUDRATE,SqlDbType.Int),
                    new SqlParameter(PARAM_IPADDRESS,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_LOCALPORT,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_PARAMFORMAT,SqlDbType.NVarChar,1073741823),
                    new SqlParameter(PARAM_TIMEOUTSEC,SqlDbType.Int),
                    new SqlParameter(PARAM_DISPLAYROWS,SqlDbType.Int),
                    new SqlParameter(PARAM_DISPLAYFORMAT,SqlDbType.NVarChar,1073741823),
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
                paras[0].Value = info.sMatrixNo;
                paras[1].Value = info.sMatrixName;
                paras[2].Value = info.sMatrixModel;
                paras[3].Value = info.sServiceNos;
                paras[4].Value = info.iPhyAddr;
                paras[5].Value = info.sProtocol;
                paras[6].Value = info.sSerialPort;
                paras[7].Value = info.iBaudrate;
                paras[8].Value = info.sIpAddress;
                paras[9].Value = info.sLocalPort;
                paras[10].Value = info.sParamFormat;
                paras[11].Value = info.iTimeoutSec;
                paras[12].Value = info.iDisplayRows;
                paras[13].Value = info.sDisplayFormat;
                paras[14].Value = info.sBranchNo;
                paras[15].Value = info.sAddOptor;
                paras[16].Value = info.dAddDate;
                paras[17].Value = info.sModOptor;
                paras[18].Value = info.dModDate;
                paras[19].Value = info.iValidityState;
                paras[20].Value = info.sComments;
                paras[21].Value = info.sAppCode;
                paras[22].Value = StringHelper.ConvertToBytes(info.sVersion);

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
                    new SqlParameter(PARAM_MATRIXNO,SqlDbType.NVarChar,20),
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
                    new SqlParameter(PARAM_MATRIXNO,SqlDbType.NVarChar,20),
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
        public LEDMatrixCollections GetRecords_Paging(SqlModel s_model)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            LEDMatrixCollections infos = null;
            LEDMatrix info = null;

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
                    infos = new LEDMatrixCollections();
                    while (reader.Read())
                    {
                        info = new LEDMatrix();
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
        internal static void PutObjectProperty(LEDMatrix obj_info, SqlDataReader reader)
        {
            obj_info.iID= int.Parse(reader["ID"].ToString());
            obj_info.sMatrixNo= reader["MatrixNo"].ToString();
            obj_info.sMatrixName= reader["MatrixName"].ToString();
            obj_info.sMatrixModel= reader["MatrixModel"].ToString();
            obj_info.sServiceNos= reader["ServiceNos"].ToString();
            obj_info.iPhyAddr= int.Parse(reader["PhyAddr"].ToString());
            obj_info.sProtocol= reader["Protocol"].ToString();
            obj_info.sSerialPort= reader["SerialPort"].ToString();
            obj_info.iBaudrate= int.Parse(reader["Baudrate"].ToString());
            obj_info.sIpAddress= reader["IpAddress"].ToString();
            obj_info.sLocalPort= reader["LocalPort"].ToString();
            obj_info.sParamFormat= reader["ParamFormat"].ToString();
            obj_info.iTimeoutSec= int.Parse(reader["TimeoutSec"].ToString());
            obj_info.iDisplayRows= int.Parse(reader["DisplayRows"].ToString());
            obj_info.sDisplayFormat= reader["DisplayFormat"].ToString();
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

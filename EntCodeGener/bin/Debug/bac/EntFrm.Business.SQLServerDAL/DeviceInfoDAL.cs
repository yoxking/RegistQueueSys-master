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
  public class DeviceInfoDAL: IDeviceInfo
  {
        #region sql
        private const string SQL_GET_ALL_RECORDS = @"Select *  From DeviceInfo Where AppCode like @AppCode And ValidityState=1";
        private const string SQL_GET_RECORDS_BY_NO = @"Select * From DeviceInfo Where   AppCode like @AppCode And   ValidityState=1 And DevNo=@DevNo";
        private const string SQL_GET_NAME_BY_NO = @"Select Name From DeviceInfo Where   AppCode like @AppCode And   ValidityState=1 And DevNo=@DevNo";
        private const string SQL_ADD_RECORD = @"Insert into DeviceInfo
                                              (DevNo,DevTypeNo,DevClassNo,ProviderNo,DevUniteNo,DevName,DevBarcode,DevGuid,DevSpec,DevPrice,MiniLimit,MaxiLimit,AddOptor,AddDate,ModOptor,ModDate,ValidityState,Comments,AppCode)
                                              values(@DevNo,@DevTypeNo,@DevClassNo,@ProviderNo,@DevUniteNo,@DevName,@DevBarcode,@DevGuid,@DevSpec,@DevPrice,@MiniLimit,@MaxiLimit,@AddOptor,@AddDate,@ModOptor,@ModDate,@ValidityState,@Comments,@AppCode)";
        private const string SQL_UPDATE_RECORD = @"Update DeviceInfo set
                                                 DevNo=@DevNo,DevTypeNo=@DevTypeNo,DevClassNo=@DevClassNo,ProviderNo=@ProviderNo,DevUniteNo=@DevUniteNo,DevName=@DevName,DevBarcode=@DevBarcode,DevGuid=@DevGuid,DevSpec=@DevSpec,DevPrice=@DevPrice,MiniLimit=@MiniLimit,MaxiLimit=@MaxiLimit,AddOptor=@AddOptor,AddDate=@AddDate,ModOptor=@ModOptor,ModDate=@ModDate,ValidityState=@ValidityState,Comments=@Comments,AppCode=@AppCode 
                                                 Where  AppCode like @AppCode And   ValidityState=1 And DevNo=@DevNo  And Version=@Version";
        private const string SQL_HARD_DELETE_RECORD = @"Delete From DeviceInfo Where   AppCode like @AppCode And   DevNo=@DevNo ";
        private const string SQL_SOFT_DELETE_RECORD = @"Update DeviceInfo set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 And DevNo=@DevNo";
        private const string SQL_GET_RECORDS_BY_CLASSNO = @"Select * From DeviceInfo Where    AppCode like @AppCode And   ValidityState=1 And ClassNo=@ClassNo";
        private const string SQL_GET_COUNT_BY_CONDITION = @"Select Count(*) From DeviceInfo Where   AppCode like @AppCode  And   ValidityState=1 ";
        #endregion

        #region param
        private const string PARAM_ID = "@ID";
        private const string PARAM_DEVNO = "@DevNo";
        private const string PARAM_DEVTYPENO = "@DevTypeNo";
        private const string PARAM_DEVCLASSNO = "@DevClassNo";
        private const string PARAM_PROVIDERNO = "@ProviderNo";
        private const string PARAM_DEVUNITENO = "@DevUniteNo";
        private const string PARAM_DEVNAME = "@DevName";
        private const string PARAM_DEVBARCODE = "@DevBarcode";
        private const string PARAM_DEVGUID = "@DevGuid";
        private const string PARAM_DEVSPEC = "@DevSpec";
        private const string PARAM_DEVPRICE = "@DevPrice";
        private const string PARAM_MINILIMIT = "@MiniLimit";
        private const string PARAM_MAXILIMIT = "@MaxiLimit";
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

        public DeviceInfoDAL(string sConnStr,string sAppCode)
        {
           this.connStr = sConnStr;
           this.appCode = sAppCode;
        }

        public DeviceInfoCollections GetAllRecords()
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            DeviceInfoCollections infos = null;
            DeviceInfo info = null;

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
                    infos = new DeviceInfoCollections();
                    while (reader.Read())
                    {
                        info = new DeviceInfo();
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

        public DeviceInfoCollections GetRecordsByClassNo(string sClassNo)
        {
            /*SqlConnection connection = null;
            SqlDataReader reader = null;
            DeviceInfoCollections infos = null;
            DeviceInfo info = null;

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
                    infos = new DeviceInfoCollections();
                    while (reader.Read())
                    {
                        info = new DeviceInfo();
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

        public DeviceInfoCollections GetRecordsByNo(string sNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            DeviceInfoCollections infos = null;
            DeviceInfo info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_DEVNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_NO,paras);

                if (reader.HasRows)
                {
                    infos = new DeviceInfoCollections();
                    while (reader.Read())
                    {
                        info = new DeviceInfo();
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
                    new SqlParameter(PARAM_DEVNO,SqlDbType.NVarChar,20),
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

        public int AddRecord(DeviceInfo info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_DEVNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_DEVTYPENO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_DEVCLASSNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_PROVIDERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_DEVUNITENO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_DEVNAME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_DEVBARCODE,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_DEVGUID,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_DEVSPEC,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_DEVPRICE,SqlDbType.Float),
                    new SqlParameter(PARAM_MINILIMIT,SqlDbType.Int),
                    new SqlParameter(PARAM_MAXILIMIT,SqlDbType.Int),
                    new SqlParameter(PARAM_ADDOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_MODOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MODDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_VALIDITYSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_COMMENTS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = info.sDevNo;
                paras[1].Value = info.sDevTypeNo;
                paras[2].Value = info.sDevClassNo;
                paras[3].Value = info.sProviderNo;
                paras[4].Value = info.sDevUniteNo;
                paras[5].Value = info.sDevName;
                paras[6].Value = info.sDevBarcode;
                paras[7].Value = info.sDevGuid;
                paras[8].Value = info.sDevSpec;
                paras[9].Value = info.dDevPrice;
                paras[10].Value = info.iMiniLimit;
                paras[11].Value = info.iMaxiLimit;
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

        public int UpdateRecord(DeviceInfo info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_DEVNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_DEVTYPENO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_DEVCLASSNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_PROVIDERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_DEVUNITENO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_DEVNAME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_DEVBARCODE,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_DEVGUID,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_DEVSPEC,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_DEVPRICE,SqlDbType.Float),
                    new SqlParameter(PARAM_MINILIMIT,SqlDbType.Int),
                    new SqlParameter(PARAM_MAXILIMIT,SqlDbType.Int),
                    new SqlParameter(PARAM_ADDOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_MODOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MODDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_VALIDITYSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_COMMENTS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_VERSION,SqlDbType.Timestamp)
                };
                paras[0].Value = info.sDevNo;
                paras[1].Value = info.sDevTypeNo;
                paras[2].Value = info.sDevClassNo;
                paras[3].Value = info.sProviderNo;
                paras[4].Value = info.sDevUniteNo;
                paras[5].Value = info.sDevName;
                paras[6].Value = info.sDevBarcode;
                paras[7].Value = info.sDevGuid;
                paras[8].Value = info.sDevSpec;
                paras[9].Value = info.dDevPrice;
                paras[10].Value = info.iMiniLimit;
                paras[11].Value = info.iMaxiLimit;
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
                    new SqlParameter(PARAM_DEVNO,SqlDbType.NVarChar,20),
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
                    new SqlParameter(PARAM_DEVNO,SqlDbType.NVarChar,20),
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

        public DeviceInfoCollections GetRecords_Paging(SqlModel s_model)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            DeviceInfoCollections infos = null;
            DeviceInfo info = null;

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
                    infos = new DeviceInfoCollections();
                    while (reader.Read())
                    {
                        info = new DeviceInfo();
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
        internal static void PutObjectProperty(DeviceInfo obj_info, SqlDataReader reader)
        {
            obj_info.iID= int.Parse(reader["ID"].ToString());
            obj_info.sDevNo= reader["DevNo"].ToString();
            obj_info.sDevTypeNo= reader["DevTypeNo"].ToString();
            obj_info.sDevClassNo= reader["DevClassNo"].ToString();
            obj_info.sProviderNo= reader["ProviderNo"].ToString();
            obj_info.sDevUniteNo= reader["DevUniteNo"].ToString();
            obj_info.sDevName= reader["DevName"].ToString();
            obj_info.sDevBarcode= reader["DevBarcode"].ToString();
            obj_info.sDevGuid= reader["DevGuid"].ToString();
            obj_info.sDevSpec= reader["DevSpec"].ToString();
            obj_info.dDevPrice= double.Parse(reader["DevPrice"].ToString());
            obj_info.iMiniLimit= int.Parse(reader["MiniLimit"].ToString());
            obj_info.iMaxiLimit= int.Parse(reader["MaxiLimit"].ToString());
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

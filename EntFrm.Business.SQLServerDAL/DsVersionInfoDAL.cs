using EntFrm.Business.IDAL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using System;
using System.Data;
using System.Data.SqlClient;

namespace EntFrm.Business.SQLServerDAL
{
  public class DsVersionInfoDAL: IDsVersionInfo
  {
        #region sql
        private const string SQL_GET_ALL_RECORDS = @"Select *  From DsVersionInfo Where AppCode like @AppCode And ValidityState=1";
        private const string SQL_GET_RECORDS_BY_NO = @"Select * From DsVersionInfo Where   AppCode like @AppCode And   ValidityState=1 And VerNo=@VerNo";
        private const string SQL_GET_NAME_BY_NO = @"Select VerName From DsVersionInfo Where   AppCode like @AppCode And   ValidityState=1 And VerNo=@VerNo";
        private const string SQL_ADD_RECORD = @"Insert into DsVersionInfo
                                              (VerNo,VerName,VerType,VerCode,Platform,AppStart,VerDesc,UpMode,FileUrl,PlayerNos,DLoadHits,CheckState,AddOptor,AddDate,ModOptor,ModDate,ValidityState,Comments,AppCode)
                                              values(@VerNo,@VerName,@VerType,@VerCode,@Platform,@AppStart,@VerDesc,@UpMode,@FileUrl,@PlayerNos,@DLoadHits,@CheckState,@AddOptor,@AddDate,@ModOptor,@ModDate,@ValidityState,@Comments,@AppCode)";
        private const string SQL_UPDATE_RECORD = @"Update DsVersionInfo set
                                                 VerNo=@VerNo,VerName=@VerName,VerType=@VerType,VerCode=@VerCode,Platform=@Platform,AppStart=@AppStart,VerDesc=@VerDesc,UpMode=@UpMode,FileUrl=@FileUrl,PlayerNos=@PlayerNos,DLoadHits=@DLoadHits,CheckState=@CheckState,AddOptor=@AddOptor,AddDate=@AddDate,ModOptor=@ModOptor,ModDate=@ModDate,ValidityState=@ValidityState,Comments=@Comments,AppCode=@AppCode 
                                                 Where  AppCode like @AppCode And   ValidityState=1 And VerNo=@VerNo  And Version=@Version";
        private const string SQL_HARD_DELETE_RECORD = @"Delete From DsVersionInfo Where   AppCode like @AppCode And   VerNo=@VerNo ";
        private const string SQL_SOFT_DELETE_RECORD = @"Update DsVersionInfo set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 And VerNo=@VerNo";
        private const string SQL_HARD_DELETE_BY_CONDTION = @"Delete From DsVersionInfo Where   AppCode like @AppCode ";
        private const string SQL_SOFT_DELETE_BY_CONDTION = @"Update DsVersionInfo set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 ";
        private const string SQL_GET_RECORDS_BY_CLASSNO = @"Select * From DsVersionInfo Where    AppCode like @AppCode And   ValidityState=1 And VerType=@VerType";
        private const string SQL_GET_COUNT_BY_CONDITION = @"Select Count(*) From DsVersionInfo Where   AppCode like @AppCode  And   ValidityState=1 ";
        #endregion

        #region param
        private const string PARAM_ID = "@ID";
        private const string PARAM_VERNO = "@VerNo";
        private const string PARAM_VERNAME = "@VerName";
        private const string PARAM_VERTYPE = "@VerType";
        private const string PARAM_VERCODE = "@VerCode";
        private const string PARAM_PLATFORM = "@Platform";
        private const string PARAM_APPSTART = "@AppStart";
        private const string PARAM_VERDESC = "@VerDesc";
        private const string PARAM_UPMODE = "@UpMode";
        private const string PARAM_FILEURL = "@FileUrl";
        private const string PARAM_PLAYERNOS = "@PlayerNos";
        private const string PARAM_DLOADHITS = "@DLoadHits";
        private const string PARAM_CHECKSTATE = "@CheckState";
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

        public DsVersionInfoDAL(string sConnStr,string sAppCode)
        {
           this.connStr = sConnStr;
           this.appCode = sAppCode;
        }

        public DsVersionInfoCollections GetAllRecords()
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            DsVersionInfoCollections infos = null;
            DsVersionInfo info = null;

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
                    infos = new DsVersionInfoCollections();
                    while (reader.Read())
                    {
                        info = new DsVersionInfo();
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

        public DsVersionInfoCollections GetRecordsByClassNo(string sClassNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            DsVersionInfoCollections infos = null;
            DsVersionInfo info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_VERTYPE,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sClassNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_CLASSNO,paras);

                if (reader.HasRows)
                {
                    infos = new DsVersionInfoCollections();
                    while (reader.Read())
                    {
                        info = new DsVersionInfo();
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

        public DsVersionInfoCollections GetRecordsByNo(string sNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            DsVersionInfoCollections infos = null;
            DsVersionInfo info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_VERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_NO,paras);

                if (reader.HasRows)
                {
                    infos = new DsVersionInfoCollections();
                    while (reader.Read())
                    {
                        info = new DsVersionInfo();
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
                    new SqlParameter(PARAM_VERNO,SqlDbType.NVarChar,20),
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

        public int AddNewRecord(DsVersionInfo info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_VERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_VERNAME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_VERTYPE,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_VERCODE,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_PLATFORM,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_APPSTART,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_VERDESC,SqlDbType.NVarChar,1073741823),
                    new SqlParameter(PARAM_UPMODE,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_FILEURL,SqlDbType.NVarChar,2000),
                    new SqlParameter(PARAM_PLAYERNOS,SqlDbType.NVarChar,1073741823),
                    new SqlParameter(PARAM_DLOADHITS,SqlDbType.Int),
                    new SqlParameter(PARAM_CHECKSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_ADDOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_MODOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MODDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_VALIDITYSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_COMMENTS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = info.sVerNo;
                paras[1].Value = info.sVerName;
                paras[2].Value = info.sVerType;
                paras[3].Value = info.sVerCode;
                paras[4].Value = info.sPlatform;
                paras[5].Value = info.sAppStart;
                paras[6].Value = info.sVerDesc;
                paras[7].Value = info.sUpMode;
                paras[8].Value = info.sFileUrl;
                paras[9].Value = info.sPlayerNos;
                paras[10].Value = info.iDLoadHits;
                paras[11].Value = info.iCheckState;
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

        public int UpdateRecord(DsVersionInfo info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_VERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_VERNAME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_VERTYPE,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_VERCODE,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_PLATFORM,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_APPSTART,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_VERDESC,SqlDbType.NVarChar,1073741823),
                    new SqlParameter(PARAM_UPMODE,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_FILEURL,SqlDbType.NVarChar,2000),
                    new SqlParameter(PARAM_PLAYERNOS,SqlDbType.NVarChar,1073741823),
                    new SqlParameter(PARAM_DLOADHITS,SqlDbType.Int),
                    new SqlParameter(PARAM_CHECKSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_ADDOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_MODOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MODDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_VALIDITYSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_COMMENTS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_VERSION,SqlDbType.Timestamp)
                };
                paras[0].Value = info.sVerNo;
                paras[1].Value = info.sVerName;
                paras[2].Value = info.sVerType;
                paras[3].Value = info.sVerCode;
                paras[4].Value = info.sPlatform;
                paras[5].Value = info.sAppStart;
                paras[6].Value = info.sVerDesc;
                paras[7].Value = info.sUpMode;
                paras[8].Value = info.sFileUrl;
                paras[9].Value = info.sPlayerNos;
                paras[10].Value = info.iDLoadHits;
                paras[11].Value = info.iCheckState;
                paras[12].Value = info.sAddOptor;
                paras[13].Value = info.dAddDate;
                paras[14].Value = info.sModOptor;
                paras[15].Value = info.dModDate;
                paras[16].Value = info.iValidityState;
                paras[17].Value = info.sComments;
                paras[18].Value = info.sAppCode;
                paras[19].Value = StringHelper.ConvertToBytes(info.sVersion);

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
                    new SqlParameter(PARAM_VERNO,SqlDbType.NVarChar,20),
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
                    new SqlParameter(PARAM_VERNO,SqlDbType.NVarChar,20),
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
        public DsVersionInfoCollections GetRecords_Paging(SqlModel s_model)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            DsVersionInfoCollections infos = null;
            DsVersionInfo info = null;

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
                    infos = new DsVersionInfoCollections();
                    while (reader.Read())
                    {
                        info = new DsVersionInfo();
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
        internal static void PutObjectProperty(DsVersionInfo obj_info, SqlDataReader reader)
        {
            obj_info.iID= int.Parse(reader["ID"].ToString());
            obj_info.sVerNo= reader["VerNo"].ToString();
            obj_info.sVerName= reader["VerName"].ToString();
            obj_info.sVerType= reader["VerType"].ToString();
            obj_info.sVerCode= reader["VerCode"].ToString();
            obj_info.sPlatform= reader["Platform"].ToString();
            obj_info.sAppStart= reader["AppStart"].ToString();
            obj_info.sVerDesc= reader["VerDesc"].ToString();
            obj_info.sUpMode= reader["UpMode"].ToString();
            obj_info.sFileUrl= reader["FileUrl"].ToString();
            obj_info.sPlayerNos= reader["PlayerNos"].ToString();
            obj_info.iDLoadHits= int.Parse(reader["DLoadHits"].ToString());
            obj_info.iCheckState= int.Parse(reader["CheckState"].ToString());
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

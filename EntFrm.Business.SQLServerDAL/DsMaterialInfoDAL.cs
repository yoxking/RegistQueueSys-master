using EntFrm.Business.IDAL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using System;
using System.Data;
using System.Data.SqlClient;

namespace EntFrm.Business.SQLServerDAL
{
  public class DsMaterialInfoDAL: IDsMaterialInfo
  {
        #region sql
        private const string SQL_GET_ALL_RECORDS = @"Select *  From DsMaterialInfo Where AppCode like @AppCode And ValidityState=1";
        private const string SQL_GET_RECORDS_BY_NO = @"Select * From DsMaterialInfo Where   AppCode like @AppCode And   ValidityState=1 And MatNo=@MatNo";
        private const string SQL_GET_NAME_BY_NO = @"Select MatName From DsMaterialInfo Where   AppCode like @AppCode And   ValidityState=1 And MatNo=@MatNo";
        private const string SQL_ADD_RECORD = @"Insert into DsMaterialInfo
                                              (MatNo,MatName,MClassNo,MatPoster,MatType,FilePath,FileSize,Resolution,PlayDuration,IsTemplet,CheckState,AddOptor,AddDate,ModOptor,ModDate,ValidityState,Comments,AppCode)
                                              values(@MatNo,@MatName,@MClassNo,@MatPoster,@MatType,@FilePath,@FileSize,@Resolution,@PlayDuration,@IsTemplet,@CheckState,@AddOptor,@AddDate,@ModOptor,@ModDate,@ValidityState,@Comments,@AppCode)";
        private const string SQL_UPDATE_RECORD = @"Update DsMaterialInfo set
                                                 MatNo=@MatNo,MatName=@MatName,MClassNo=@MClassNo,MatPoster=@MatPoster,MatType=@MatType,FilePath=@FilePath,FileSize=@FileSize,Resolution=@Resolution,PlayDuration=@PlayDuration,IsTemplet=@IsTemplet,CheckState=@CheckState,AddOptor=@AddOptor,AddDate=@AddDate,ModOptor=@ModOptor,ModDate=@ModDate,ValidityState=@ValidityState,Comments=@Comments,AppCode=@AppCode 
                                                 Where  AppCode like @AppCode And   ValidityState=1 And MatNo=@MatNo  And Version=@Version";
        private const string SQL_HARD_DELETE_RECORD = @"Delete From DsMaterialInfo Where   AppCode like @AppCode And   MatNo=@MatNo ";
        private const string SQL_SOFT_DELETE_RECORD = @"Update DsMaterialInfo set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 And MatNo=@MatNo";
        private const string SQL_HARD_DELETE_BY_CONDTION = @"Delete From DsMaterialInfo Where   AppCode like @AppCode ";
        private const string SQL_SOFT_DELETE_BY_CONDTION = @"Update DsMaterialInfo set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 ";
        private const string SQL_GET_RECORDS_BY_CLASSNO = @"Select * From DsMaterialInfo Where    AppCode like @AppCode And   ValidityState=1 And MClassNo=@MClassNo";
        private const string SQL_GET_COUNT_BY_CONDITION = @"Select Count(*) From DsMaterialInfo Where   AppCode like @AppCode  And   ValidityState=1 ";
        #endregion

        #region param
        private const string PARAM_ID = "@ID";
        private const string PARAM_MATNO = "@MatNo";
        private const string PARAM_MATNAME = "@MatName";
        private const string PARAM_MCLASSNO = "@MClassNo";
        private const string PARAM_MATPOSTER = "@MatPoster";
        private const string PARAM_MATTYPE = "@MatType";
        private const string PARAM_FILEPATH = "@FilePath";
        private const string PARAM_FILESIZE = "@FileSize";
        private const string PARAM_RESOLUTION = "@Resolution";
        private const string PARAM_PLAYDURATION = "@PlayDuration";
        private const string PARAM_ISTEMPLET = "@IsTemplet";
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

        public DsMaterialInfoDAL(string sConnStr,string sAppCode)
        {
           this.connStr = sConnStr;
           this.appCode = sAppCode;
        }

        public DsMaterialInfoCollections GetAllRecords()
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            DsMaterialInfoCollections infos = null;
            DsMaterialInfo info = null;

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
                    infos = new DsMaterialInfoCollections();
                    while (reader.Read())
                    {
                        info = new DsMaterialInfo();
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

        public DsMaterialInfoCollections GetRecordsByClassNo(string sClassNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            DsMaterialInfoCollections infos = null;
            DsMaterialInfo info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_MCLASSNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sClassNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_CLASSNO,paras);

                if (reader.HasRows)
                {
                    infos = new DsMaterialInfoCollections();
                    while (reader.Read())
                    {
                        info = new DsMaterialInfo();
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

        public DsMaterialInfoCollections GetRecordsByNo(string sNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            DsMaterialInfoCollections infos = null;
            DsMaterialInfo info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_MATNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_NO,paras);

                if (reader.HasRows)
                {
                    infos = new DsMaterialInfoCollections();
                    while (reader.Read())
                    {
                        info = new DsMaterialInfo();
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
                    new SqlParameter(PARAM_MATNO,SqlDbType.NVarChar,20),
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

        public int AddNewRecord(DsMaterialInfo info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_MATNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MATNAME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_MCLASSNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MATPOSTER,SqlDbType.NVarChar,500),
                    new SqlParameter(PARAM_MATTYPE,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_FILEPATH,SqlDbType.NVarChar,500),
                    new SqlParameter(PARAM_FILESIZE,SqlDbType.Int),
                    new SqlParameter(PARAM_RESOLUTION,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_PLAYDURATION,SqlDbType.Float),
                    new SqlParameter(PARAM_ISTEMPLET,SqlDbType.Int),
                    new SqlParameter(PARAM_CHECKSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_ADDOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_MODOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MODDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_VALIDITYSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_COMMENTS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = info.sMatNo;
                paras[1].Value = info.sMatName;
                paras[2].Value = info.sMClassNo;
                paras[3].Value = info.sMatPoster;
                paras[4].Value = info.sMatType;
                paras[5].Value = info.sFilePath;
                paras[6].Value = info.iFileSize;
                paras[7].Value = info.sResolution;
                paras[8].Value = info.dPlayDuration;
                paras[9].Value = info.iIsTemplet;
                paras[10].Value = info.iCheckState;
                paras[11].Value = info.sAddOptor;
                paras[12].Value = info.dAddDate;
                paras[13].Value = info.sModOptor;
                paras[14].Value = info.dModDate;
                paras[15].Value = info.iValidityState;
                paras[16].Value = info.sComments;
                paras[17].Value = info.sAppCode;

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

        public int UpdateRecord(DsMaterialInfo info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_MATNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MATNAME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_MCLASSNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MATPOSTER,SqlDbType.NVarChar,500),
                    new SqlParameter(PARAM_MATTYPE,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_FILEPATH,SqlDbType.NVarChar,500),
                    new SqlParameter(PARAM_FILESIZE,SqlDbType.Int),
                    new SqlParameter(PARAM_RESOLUTION,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_PLAYDURATION,SqlDbType.Float),
                    new SqlParameter(PARAM_ISTEMPLET,SqlDbType.Int),
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
                paras[0].Value = info.sMatNo;
                paras[1].Value = info.sMatName;
                paras[2].Value = info.sMClassNo;
                paras[3].Value = info.sMatPoster;
                paras[4].Value = info.sMatType;
                paras[5].Value = info.sFilePath;
                paras[6].Value = info.iFileSize;
                paras[7].Value = info.sResolution;
                paras[8].Value = info.dPlayDuration;
                paras[9].Value = info.iIsTemplet;
                paras[10].Value = info.iCheckState;
                paras[11].Value = info.sAddOptor;
                paras[12].Value = info.dAddDate;
                paras[13].Value = info.sModOptor;
                paras[14].Value = info.dModDate;
                paras[15].Value = info.iValidityState;
                paras[16].Value = info.sComments;
                paras[17].Value = info.sAppCode;
                paras[18].Value = StringHelper.ConvertToBytes(info.sVersion);

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
                    new SqlParameter(PARAM_MATNO,SqlDbType.NVarChar,20),
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
                    new SqlParameter(PARAM_MATNO,SqlDbType.NVarChar,20),
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
        public DsMaterialInfoCollections GetRecords_Paging(SqlModel s_model)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            DsMaterialInfoCollections infos = null;
            DsMaterialInfo info = null;

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
                    infos = new DsMaterialInfoCollections();
                    while (reader.Read())
                    {
                        info = new DsMaterialInfo();
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
        internal static void PutObjectProperty(DsMaterialInfo obj_info, SqlDataReader reader)
        {
            obj_info.iID= int.Parse(reader["ID"].ToString());
            obj_info.sMatNo= reader["MatNo"].ToString();
            obj_info.sMatName= reader["MatName"].ToString();
            obj_info.sMClassNo= reader["MClassNo"].ToString();
            obj_info.sMatPoster= reader["MatPoster"].ToString();
            obj_info.sMatType= reader["MatType"].ToString();
            obj_info.sFilePath= reader["FilePath"].ToString();
            obj_info.iFileSize= int.Parse(reader["FileSize"].ToString());
            obj_info.sResolution= reader["Resolution"].ToString();
            obj_info.dPlayDuration= double.Parse(reader["PlayDuration"].ToString());
            obj_info.iIsTemplet= int.Parse(reader["IsTemplet"].ToString());
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

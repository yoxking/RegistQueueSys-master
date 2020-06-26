using EntFrm.Business.IDAL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using System;
using System.Data;
using System.Data.SqlClient;

namespace EntFrm.Business.SQLServerDAL
{
  public class DsProgramInfoDAL: IDsProgramInfo
  {
        #region sql
        private const string SQL_GET_ALL_RECORDS = @"Select *  From DsProgramInfo Where AppCode like @AppCode And ValidityState=1";
        private const string SQL_GET_RECORDS_BY_NO = @"Select * From DsProgramInfo Where   AppCode like @AppCode And   ValidityState=1 And ProgmNo=@ProgmNo";
        private const string SQL_GET_NAME_BY_NO = @"Select ProgmName From DsProgramInfo Where   AppCode like @AppCode And   ValidityState=1 And ProgmNo=@ProgmNo";
        private const string SQL_ADD_RECORD = @"Insert into DsProgramInfo
                                              (ProgmNo,ProgmName,PClassNo,PosterUrl,IsTemplate,PFilePath,PWebUrl,PContent,SlideNum,PVersion,Duration,Resolution,CheckState,CheckOptor,CheckDate,BranchNo,AddOptor,AddDate,ModOptor,ModDate,ValidityState,Comments,AppCode)
                                              values(@ProgmNo,@ProgmName,@PClassNo,@PosterUrl,@IsTemplate,@PFilePath,@PWebUrl,@PContent,@SlideNum,@PVersion,@Duration,@Resolution,@CheckState,@CheckOptor,@CheckDate,@BranchNo,@AddOptor,@AddDate,@ModOptor,@ModDate,@ValidityState,@Comments,@AppCode)";
        private const string SQL_UPDATE_RECORD = @"Update DsProgramInfo set
                                                 ProgmNo=@ProgmNo,ProgmName=@ProgmName,PClassNo=@PClassNo,PosterUrl=@PosterUrl,IsTemplate=@IsTemplate,PFilePath=@PFilePath,PWebUrl=@PWebUrl,PContent=@PContent,SlideNum=@SlideNum,PVersion=@PVersion,Duration=@Duration,Resolution=@Resolution,CheckState=@CheckState,CheckOptor=@CheckOptor,CheckDate=@CheckDate,BranchNo=@BranchNo,AddOptor=@AddOptor,AddDate=@AddDate,ModOptor=@ModOptor,ModDate=@ModDate,ValidityState=@ValidityState,Comments=@Comments,AppCode=@AppCode 
                                                 Where  AppCode like @AppCode And   ValidityState=1 And ProgmNo=@ProgmNo  And Version=@Version";
        private const string SQL_HARD_DELETE_RECORD = @"Delete From DsProgramInfo Where   AppCode like @AppCode And   ProgmNo=@ProgmNo ";
        private const string SQL_SOFT_DELETE_RECORD = @"Update DsProgramInfo set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 And ProgmNo=@ProgmNo";
        private const string SQL_HARD_DELETE_BY_CONDTION = @"Delete From DsProgramInfo Where   AppCode like @AppCode ";
        private const string SQL_SOFT_DELETE_BY_CONDTION = @"Update DsProgramInfo set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 ";
        private const string SQL_GET_RECORDS_BY_CLASSNO = @"Select * From DsProgramInfo Where    AppCode like @AppCode And   ValidityState=1 And PClassNo=@PClassNo";
        private const string SQL_GET_COUNT_BY_CONDITION = @"Select Count(*) From DsProgramInfo Where   AppCode like @AppCode  And   ValidityState=1 ";
        #endregion

        #region param
        private const string PARAM_ID = "@ID";
        private const string PARAM_PROGMNO = "@ProgmNo";
        private const string PARAM_PROGMNAME = "@ProgmName";
        private const string PARAM_PCLASSNO = "@PClassNo";
        private const string PARAM_POSTERURL = "@PosterUrl";
        private const string PARAM_ISTEMPLATE = "@IsTemplate";
        private const string PARAM_PFILEPATH = "@PFilePath";
        private const string PARAM_PWEBURL = "@PWebUrl";
        private const string PARAM_PCONTENT = "@PContent";
        private const string PARAM_SLIDENUM = "@SlideNum";
        private const string PARAM_PVERSION = "@PVersion";
        private const string PARAM_DURATION = "@Duration";
        private const string PARAM_RESOLUTION = "@Resolution";
        private const string PARAM_CHECKSTATE = "@CheckState";
        private const string PARAM_CHECKOPTOR = "@CheckOptor";
        private const string PARAM_CHECKDATE = "@CheckDate";
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

        public DsProgramInfoDAL(string sConnStr,string sAppCode)
        {
           this.connStr = sConnStr;
           this.appCode = sAppCode;
        }

        public DsProgramInfoCollections GetAllRecords()
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            DsProgramInfoCollections infos = null;
            DsProgramInfo info = null;

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
                    infos = new DsProgramInfoCollections();
                    while (reader.Read())
                    {
                        info = new DsProgramInfo();
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

        public DsProgramInfoCollections GetRecordsByClassNo(string sClassNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            DsProgramInfoCollections infos = null;
            DsProgramInfo info = null;

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
                    infos = new DsProgramInfoCollections();
                    while (reader.Read())
                    {
                        info = new DsProgramInfo();
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

        public DsProgramInfoCollections GetRecordsByNo(string sNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            DsProgramInfoCollections infos = null;
            DsProgramInfo info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_PROGMNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_NO,paras);

                if (reader.HasRows)
                {
                    infos = new DsProgramInfoCollections();
                    while (reader.Read())
                    {
                        info = new DsProgramInfo();
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
                    new SqlParameter(PARAM_PROGMNO,SqlDbType.NVarChar,20),
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

        public int AddNewRecord(DsProgramInfo info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_PROGMNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_PROGMNAME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_PCLASSNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_POSTERURL,SqlDbType.NVarChar,500),
                    new SqlParameter(PARAM_ISTEMPLATE,SqlDbType.Int),
                    new SqlParameter(PARAM_PFILEPATH,SqlDbType.NVarChar,500),
                    new SqlParameter(PARAM_PWEBURL,SqlDbType.NVarChar,500),
                    new SqlParameter(PARAM_PCONTENT,SqlDbType.NVarChar,1073741823),
                    new SqlParameter(PARAM_SLIDENUM,SqlDbType.Int),
                    new SqlParameter(PARAM_PVERSION,SqlDbType.Int),
                    new SqlParameter(PARAM_DURATION,SqlDbType.Int),
                    new SqlParameter(PARAM_RESOLUTION,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_CHECKSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_CHECKOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_CHECKDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_BRANCHNO,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_ADDOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_MODOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MODDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_VALIDITYSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_COMMENTS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = info.sProgmNo;
                paras[1].Value = info.sProgmName;
                paras[2].Value = info.sPClassNo;
                paras[3].Value = info.sPosterUrl;
                paras[4].Value = info.iIsTemplate;
                paras[5].Value = info.sPFilePath;
                paras[6].Value = info.sPWebUrl;
                paras[7].Value = info.sPContent;
                paras[8].Value = info.iSlideNum;
                paras[9].Value = info.iPVersion;
                paras[10].Value = info.iDuration;
                paras[11].Value = info.sResolution;
                paras[12].Value = info.iCheckState;
                paras[13].Value = info.sCheckOptor;
                paras[14].Value = info.dCheckDate;
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

        public int UpdateRecord(DsProgramInfo info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_PROGMNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_PROGMNAME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_PCLASSNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_POSTERURL,SqlDbType.NVarChar,500),
                    new SqlParameter(PARAM_ISTEMPLATE,SqlDbType.Int),
                    new SqlParameter(PARAM_PFILEPATH,SqlDbType.NVarChar,500),
                    new SqlParameter(PARAM_PWEBURL,SqlDbType.NVarChar,500),
                    new SqlParameter(PARAM_PCONTENT,SqlDbType.NVarChar,1073741823),
                    new SqlParameter(PARAM_SLIDENUM,SqlDbType.Int),
                    new SqlParameter(PARAM_PVERSION,SqlDbType.Int),
                    new SqlParameter(PARAM_DURATION,SqlDbType.Int),
                    new SqlParameter(PARAM_RESOLUTION,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_CHECKSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_CHECKOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_CHECKDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_BRANCHNO,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_ADDOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_MODOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MODDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_VALIDITYSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_COMMENTS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_VERSION,SqlDbType.Timestamp)
                };
                paras[0].Value = info.sProgmNo;
                paras[1].Value = info.sProgmName;
                paras[2].Value = info.sPClassNo;
                paras[3].Value = info.sPosterUrl;
                paras[4].Value = info.iIsTemplate;
                paras[5].Value = info.sPFilePath;
                paras[6].Value = info.sPWebUrl;
                paras[7].Value = info.sPContent;
                paras[8].Value = info.iSlideNum;
                paras[9].Value = info.iPVersion;
                paras[10].Value = info.iDuration;
                paras[11].Value = info.sResolution;
                paras[12].Value = info.iCheckState;
                paras[13].Value = info.sCheckOptor;
                paras[14].Value = info.dCheckDate;
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
                    new SqlParameter(PARAM_PROGMNO,SqlDbType.NVarChar,20),
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
                    new SqlParameter(PARAM_PROGMNO,SqlDbType.NVarChar,20),
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
        public DsProgramInfoCollections GetRecords_Paging(SqlModel s_model)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            DsProgramInfoCollections infos = null;
            DsProgramInfo info = null;

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
                    infos = new DsProgramInfoCollections();
                    while (reader.Read())
                    {
                        info = new DsProgramInfo();
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
        internal static void PutObjectProperty(DsProgramInfo obj_info, SqlDataReader reader)
        {
            obj_info.iID= int.Parse(reader["ID"].ToString());
            obj_info.sProgmNo= reader["ProgmNo"].ToString();
            obj_info.sProgmName= reader["ProgmName"].ToString();
            obj_info.sPClassNo= reader["PClassNo"].ToString();
            obj_info.sPosterUrl= reader["PosterUrl"].ToString();
            obj_info.iIsTemplate= int.Parse(reader["IsTemplate"].ToString());
            obj_info.sPFilePath= reader["PFilePath"].ToString();
            obj_info.sPWebUrl= reader["PWebUrl"].ToString();
            obj_info.sPContent= reader["PContent"].ToString();
            obj_info.iSlideNum= int.Parse(reader["SlideNum"].ToString());
            obj_info.iPVersion= int.Parse(reader["PVersion"].ToString());
            obj_info.iDuration= int.Parse(reader["Duration"].ToString());
            obj_info.sResolution= reader["Resolution"].ToString();
            obj_info.iCheckState= int.Parse(reader["CheckState"].ToString());
            obj_info.sCheckOptor= reader["CheckOptor"].ToString();
            obj_info.dCheckDate= DateTime.Parse(reader["CheckDate"].ToString());
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

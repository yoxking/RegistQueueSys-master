using EntFrm.Business.IDAL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using System;
using System.Data;
using System.Data.SqlClient;

namespace EntFrm.Business.SQLServerDAL
{
  public class ContentInfoDAL: IContentInfo
  {
        #region sql
        private const string SQL_GET_ALL_RECORDS = @"Select *  From ContentInfo Where AppCode like @AppCode And ValidityState=1";
        private const string SQL_GET_RECORDS_BY_NO = @"Select * From ContentInfo Where   AppCode like @AppCode And   ValidityState=1 And ContNo=@ContNo";
        private const string SQL_GET_NAME_BY_NO = @"Select Title From ContentInfo Where   AppCode like @AppCode And   ValidityState=1 And ContNo=@ContNo";
        private const string SQL_ADD_RECORD = @"Insert into ContentInfo
                                              (ContNo,ClassNo,Title,Author,PublicDate,PostPicture,Abstract,NContent,IsTop,IsHot,IsPop,HitCount,AuditDate,Auditor,CheckState,BranchNos,AddOptor,AddDate,ModOptor,ModDate,ValidityState,Comments,AppCode)
                                              values(@ContNo,@ClassNo,@Title,@Author,@PublicDate,@PostPicture,@Abstract,@NContent,@IsTop,@IsHot,@IsPop,@HitCount,@AuditDate,@Auditor,@CheckState,@BranchNos,@AddOptor,@AddDate,@ModOptor,@ModDate,@ValidityState,@Comments,@AppCode)";
        private const string SQL_UPDATE_RECORD = @"Update ContentInfo set
                                                 ContNo=@ContNo,ClassNo=@ClassNo,Title=@Title,Author=@Author,PublicDate=@PublicDate,PostPicture=@PostPicture,Abstract=@Abstract,NContent=@NContent,IsTop=@IsTop,IsHot=@IsHot,IsPop=@IsPop,HitCount=@HitCount,AuditDate=@AuditDate,Auditor=@Auditor,CheckState=@CheckState,BranchNos=@BranchNos,AddOptor=@AddOptor,AddDate=@AddDate,ModOptor=@ModOptor,ModDate=@ModDate,ValidityState=@ValidityState,Comments=@Comments,AppCode=@AppCode 
                                                 Where  AppCode like @AppCode And   ValidityState=1 And ContNo=@ContNo  And Version=@Version";
        private const string SQL_HARD_DELETE_RECORD = @"Delete From ContentInfo Where   AppCode like @AppCode And   ContNo=@ContNo ";
        private const string SQL_SOFT_DELETE_RECORD = @"Update ContentInfo set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 And ContNo=@ContNo";
        private const string SQL_HARD_DELETE_BY_CONDTION = @"Delete From ContentInfo Where   AppCode like @AppCode ";
        private const string SQL_SOFT_DELETE_BY_CONDTION = @"Update ContentInfo set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 ";
        private const string SQL_GET_RECORDS_BY_CLASSNO = @"Select * From ContentInfo Where    AppCode like @AppCode And   ValidityState=1 And ClassNo=@ClassNo";
        private const string SQL_GET_COUNT_BY_CONDITION = @"Select Count(*) From ContentInfo Where   AppCode like @AppCode  And   ValidityState=1 ";
        #endregion

        #region param
        private const string PARAM_ID = "@ID";
        private const string PARAM_CONTNO = "@ContNo";
        private const string PARAM_CLASSNO = "@ClassNo";
        private const string PARAM_TITLE = "@Title";
        private const string PARAM_AUTHOR = "@Author";
        private const string PARAM_PUBLICDATE = "@PublicDate";
        private const string PARAM_POSTPICTURE = "@PostPicture";
        private const string PARAM_ABSTRACT = "@Abstract";
        private const string PARAM_NCONTENT = "@NContent";
        private const string PARAM_ISTOP = "@IsTop";
        private const string PARAM_ISHOT = "@IsHot";
        private const string PARAM_ISPOP = "@IsPop";
        private const string PARAM_HITCOUNT = "@HitCount";
        private const string PARAM_AUDITDATE = "@AuditDate";
        private const string PARAM_AUDITOR = "@Auditor";
        private const string PARAM_CHECKSTATE = "@CheckState";
        private const string PARAM_BRANCHNOS = "@BranchNos";
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

        public ContentInfoDAL(string sConnStr,string sAppCode)
        {
           this.connStr = sConnStr;
           this.appCode = sAppCode;
        }

        public ContentInfoCollections GetAllRecords()
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            ContentInfoCollections infos = null;
            ContentInfo info = null;

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
                    infos = new ContentInfoCollections();
                    while (reader.Read())
                    {
                        info = new ContentInfo();
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

        public ContentInfoCollections GetRecordsByClassNo(string sClassNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            ContentInfoCollections infos = null;
            ContentInfo info = null;

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
                    infos = new ContentInfoCollections();
                    while (reader.Read())
                    {
                        info = new ContentInfo();
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

        public ContentInfoCollections GetRecordsByNo(string sNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            ContentInfoCollections infos = null;
            ContentInfo info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_CONTNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_NO,paras);

                if (reader.HasRows)
                {
                    infos = new ContentInfoCollections();
                    while (reader.Read())
                    {
                        info = new ContentInfo();
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
                    new SqlParameter(PARAM_CONTNO,SqlDbType.NVarChar,20),
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

        public int AddNewRecord(ContentInfo info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_CONTNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_CLASSNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_TITLE,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_AUTHOR,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_PUBLICDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_POSTPICTURE,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_ABSTRACT,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_NCONTENT,SqlDbType.NVarChar,1073741823),
                    new SqlParameter(PARAM_ISTOP,SqlDbType.Int),
                    new SqlParameter(PARAM_ISHOT,SqlDbType.Int),
                    new SqlParameter(PARAM_ISPOP,SqlDbType.Int),
                    new SqlParameter(PARAM_HITCOUNT,SqlDbType.Int),
                    new SqlParameter(PARAM_AUDITDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_AUDITOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_CHECKSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_BRANCHNOS,SqlDbType.NVarChar,4000),
                    new SqlParameter(PARAM_ADDOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_MODOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MODDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_VALIDITYSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_COMMENTS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = info.sContNo;
                paras[1].Value = info.sClassNo;
                paras[2].Value = info.sTitle;
                paras[3].Value = info.sAuthor;
                paras[4].Value = info.dPublicDate;
                paras[5].Value = info.sPostPicture;
                paras[6].Value = info.sAbstract;
                paras[7].Value = info.sNContent;
                paras[8].Value = info.iIsTop;
                paras[9].Value = info.iIsHot;
                paras[10].Value = info.iIsPop;
                paras[11].Value = info.iHitCount;
                paras[12].Value = info.dAuditDate;
                paras[13].Value = info.sAuditor;
                paras[14].Value = info.iCheckState;
                paras[15].Value = info.sBranchNos;
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

        public int UpdateRecord(ContentInfo info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_CONTNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_CLASSNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_TITLE,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_AUTHOR,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_PUBLICDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_POSTPICTURE,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_ABSTRACT,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_NCONTENT,SqlDbType.NVarChar,1073741823),
                    new SqlParameter(PARAM_ISTOP,SqlDbType.Int),
                    new SqlParameter(PARAM_ISHOT,SqlDbType.Int),
                    new SqlParameter(PARAM_ISPOP,SqlDbType.Int),
                    new SqlParameter(PARAM_HITCOUNT,SqlDbType.Int),
                    new SqlParameter(PARAM_AUDITDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_AUDITOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_CHECKSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_BRANCHNOS,SqlDbType.NVarChar,4000),
                    new SqlParameter(PARAM_ADDOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_MODOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MODDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_VALIDITYSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_COMMENTS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_VERSION,SqlDbType.Timestamp)
                };
                paras[0].Value = info.sContNo;
                paras[1].Value = info.sClassNo;
                paras[2].Value = info.sTitle;
                paras[3].Value = info.sAuthor;
                paras[4].Value = info.dPublicDate;
                paras[5].Value = info.sPostPicture;
                paras[6].Value = info.sAbstract;
                paras[7].Value = info.sNContent;
                paras[8].Value = info.iIsTop;
                paras[9].Value = info.iIsHot;
                paras[10].Value = info.iIsPop;
                paras[11].Value = info.iHitCount;
                paras[12].Value = info.dAuditDate;
                paras[13].Value = info.sAuditor;
                paras[14].Value = info.iCheckState;
                paras[15].Value = info.sBranchNos;
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
                    new SqlParameter(PARAM_CONTNO,SqlDbType.NVarChar,20),
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
                    new SqlParameter(PARAM_CONTNO,SqlDbType.NVarChar,20),
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
        public ContentInfoCollections GetRecords_Paging(SqlModel s_model)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            ContentInfoCollections infos = null;
            ContentInfo info = null;

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
                    infos = new ContentInfoCollections();
                    while (reader.Read())
                    {
                        info = new ContentInfo();
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
        internal static void PutObjectProperty(ContentInfo obj_info, SqlDataReader reader)
        {
            obj_info.iID= int.Parse(reader["ID"].ToString());
            obj_info.sContNo= reader["ContNo"].ToString();
            obj_info.sClassNo= reader["ClassNo"].ToString();
            obj_info.sTitle= reader["Title"].ToString();
            obj_info.sAuthor= reader["Author"].ToString();
            obj_info.dPublicDate= DateTime.Parse(reader["PublicDate"].ToString());
            obj_info.sPostPicture= reader["PostPicture"].ToString();
            obj_info.sAbstract= reader["Abstract"].ToString();
            obj_info.sNContent= reader["NContent"].ToString();
            obj_info.iIsTop= int.Parse(reader["IsTop"].ToString());
            obj_info.iIsHot= int.Parse(reader["IsHot"].ToString());
            obj_info.iIsPop= int.Parse(reader["IsPop"].ToString());
            obj_info.iHitCount= int.Parse(reader["HitCount"].ToString());
            obj_info.dAuditDate= DateTime.Parse(reader["AuditDate"].ToString());
            obj_info.sAuditor= reader["Auditor"].ToString();
            obj_info.iCheckState= int.Parse(reader["CheckState"].ToString());
            obj_info.sBranchNos= reader["BranchNos"].ToString();
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

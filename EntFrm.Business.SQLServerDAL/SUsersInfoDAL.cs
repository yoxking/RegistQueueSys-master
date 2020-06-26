using EntFrm.Business.IDAL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using System;
using System.Data;
using System.Data.SqlClient;

namespace EntFrm.Business.SQLServerDAL
{
  public class SUsersInfoDAL: ISUsersInfo
  {
        #region sql
        private const string SQL_GET_ALL_RECORDS = @"Select *  From SUsersInfo Where AppCode like @AppCode And ValidityState=1";
        private const string SQL_GET_RECORDS_BY_NO = @"Select * From SUsersInfo Where   AppCode like @AppCode And   ValidityState=1 And SuNo=@SuNo";
        private const string SQL_GET_NAME_BY_NO = @"Select TrueName From SUsersInfo Where   AppCode like @AppCode And   ValidityState=1 And SuNo=@SuNo";
        private const string SQL_ADD_RECORD = @"Insert into SUsersInfo
                                              (SuNo,LoginID,Password,DeptNo,TrueName,Sex,UIDType,UIDNo,EMail,Telphone,LockState,AdminFlag,CheckState,BranchNo,AddOptor,AddDate,ModOptor,ModDate,ValidityState,Comments,AppCode)
                                              values(@SuNo,@LoginID,@Password,@DeptNo,@TrueName,@Sex,@UIDType,@UIDNo,@EMail,@Telphone,@LockState,@AdminFlag,@CheckState,@BranchNo,@AddOptor,@AddDate,@ModOptor,@ModDate,@ValidityState,@Comments,@AppCode)";
        private const string SQL_UPDATE_RECORD = @"Update SUsersInfo set
                                                 SuNo=@SuNo,LoginID=@LoginID,Password=@Password,DeptNo=@DeptNo,TrueName=@TrueName,Sex=@Sex,UIDType=@UIDType,UIDNo=@UIDNo,EMail=@EMail,Telphone=@Telphone,LockState=@LockState,AdminFlag=@AdminFlag,CheckState=@CheckState,BranchNo=@BranchNo,AddOptor=@AddOptor,AddDate=@AddDate,ModOptor=@ModOptor,ModDate=@ModDate,ValidityState=@ValidityState,Comments=@Comments,AppCode=@AppCode 
                                                 Where  AppCode like @AppCode And   ValidityState=1 And SuNo=@SuNo  And Version=@Version";
        private const string SQL_HARD_DELETE_RECORD = @"Delete From SUsersInfo Where   AppCode like @AppCode And   SuNo=@SuNo ";
        private const string SQL_SOFT_DELETE_RECORD = @"Update SUsersInfo set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 And SuNo=@SuNo";
        private const string SQL_HARD_DELETE_BY_CONDTION = @"Delete From SUsersInfo Where   AppCode like @AppCode ";
        private const string SQL_SOFT_DELETE_BY_CONDTION = @"Update SUsersInfo set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 ";
        private const string SQL_GET_RECORDS_BY_CLASSNO = @"Select * From SUsersInfo Where    AppCode like @AppCode And   ValidityState=1 And DeptNo=@DeptNo";
        private const string SQL_GET_COUNT_BY_CONDITION = @"Select Count(*) From SUsersInfo Where   AppCode like @AppCode  And   ValidityState=1 ";
        #endregion

        #region param
        private const string PARAM_ID = "@ID";
        private const string PARAM_SUNO = "@SuNo";
        private const string PARAM_LOGINID = "@LoginID";
        private const string PARAM_PASSWORD = "@Password";
        private const string PARAM_DEPTNO = "@DeptNo";
        private const string PARAM_TRUENAME = "@TrueName";
        private const string PARAM_SEX = "@Sex";
        private const string PARAM_UIDTYPE = "@UIDType";
        private const string PARAM_UIDNO = "@UIDNo";
        private const string PARAM_EMAIL = "@EMail";
        private const string PARAM_TELPHONE = "@Telphone";
        private const string PARAM_LOCKSTATE = "@LockState";
        private const string PARAM_ADMINFLAG = "@AdminFlag";
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

        public SUsersInfoDAL(string sConnStr,string sAppCode)
        {
           this.connStr = sConnStr;
           this.appCode = sAppCode;
        }

        public SUsersInfoCollections GetAllRecords()
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            SUsersInfoCollections infos = null;
            SUsersInfo info = null;

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
                    infos = new SUsersInfoCollections();
                    while (reader.Read())
                    {
                        info = new SUsersInfo();
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

        public SUsersInfoCollections GetRecordsByClassNo(string sClassNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            SUsersInfoCollections infos = null;
            SUsersInfo info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_DEPTNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sClassNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_CLASSNO,paras);

                if (reader.HasRows)
                {
                    infos = new SUsersInfoCollections();
                    while (reader.Read())
                    {
                        info = new SUsersInfo();
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

        public SUsersInfoCollections GetRecordsByNo(string sNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            SUsersInfoCollections infos = null;
            SUsersInfo info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_SUNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_NO,paras);

                if (reader.HasRows)
                {
                    infos = new SUsersInfoCollections();
                    while (reader.Read())
                    {
                        info = new SUsersInfo();
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
                    new SqlParameter(PARAM_SUNO,SqlDbType.NVarChar,20),
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

        public int AddNewRecord(SUsersInfo info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_SUNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_LOGINID,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_PASSWORD,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_DEPTNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_TRUENAME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_SEX,SqlDbType.Int),
                    new SqlParameter(PARAM_UIDTYPE,SqlDbType.Int),
                    new SqlParameter(PARAM_UIDNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_EMAIL,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_TELPHONE,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_LOCKSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_ADMINFLAG,SqlDbType.Int),
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
                paras[0].Value = info.sSuNo;
                paras[1].Value = info.sLoginID;
                paras[2].Value = info.sPassword;
                paras[3].Value = info.sDeptNo;
                paras[4].Value = info.sTrueName;
                paras[5].Value = info.iSex;
                paras[6].Value = info.iUIDType;
                paras[7].Value = info.sUIDNo;
                paras[8].Value = info.sEMail;
                paras[9].Value = info.sTelphone;
                paras[10].Value = info.iLockState;
                paras[11].Value = info.iAdminFlag;
                paras[12].Value = info.iCheckState;
                paras[13].Value = info.sBranchNo;
                paras[14].Value = info.sAddOptor;
                paras[15].Value = info.dAddDate;
                paras[16].Value = info.sModOptor;
                paras[17].Value = info.dModDate;
                paras[18].Value = info.iValidityState;
                paras[19].Value = info.sComments;
                paras[20].Value = info.sAppCode;

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

        public int UpdateRecord(SUsersInfo info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_SUNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_LOGINID,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_PASSWORD,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_DEPTNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_TRUENAME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_SEX,SqlDbType.Int),
                    new SqlParameter(PARAM_UIDTYPE,SqlDbType.Int),
                    new SqlParameter(PARAM_UIDNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_EMAIL,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_TELPHONE,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_LOCKSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_ADMINFLAG,SqlDbType.Int),
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
                paras[0].Value = info.sSuNo;
                paras[1].Value = info.sLoginID;
                paras[2].Value = info.sPassword;
                paras[3].Value = info.sDeptNo;
                paras[4].Value = info.sTrueName;
                paras[5].Value = info.iSex;
                paras[6].Value = info.iUIDType;
                paras[7].Value = info.sUIDNo;
                paras[8].Value = info.sEMail;
                paras[9].Value = info.sTelphone;
                paras[10].Value = info.iLockState;
                paras[11].Value = info.iAdminFlag;
                paras[12].Value = info.iCheckState;
                paras[13].Value = info.sBranchNo;
                paras[14].Value = info.sAddOptor;
                paras[15].Value = info.dAddDate;
                paras[16].Value = info.sModOptor;
                paras[17].Value = info.dModDate;
                paras[18].Value = info.iValidityState;
                paras[19].Value = info.sComments;
                paras[20].Value = info.sAppCode;
                paras[21].Value = StringHelper.ConvertToBytes(info.sVersion);

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
                    new SqlParameter(PARAM_SUNO,SqlDbType.NVarChar,20),
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
                    new SqlParameter(PARAM_SUNO,SqlDbType.NVarChar,20),
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
        public SUsersInfoCollections GetRecords_Paging(SqlModel s_model)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            SUsersInfoCollections infos = null;
            SUsersInfo info = null;

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
                    infos = new SUsersInfoCollections();
                    while (reader.Read())
                    {
                        info = new SUsersInfo();
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
        internal static void PutObjectProperty(SUsersInfo obj_info, SqlDataReader reader)
        {
            obj_info.iID= int.Parse(reader["ID"].ToString());
            obj_info.sSuNo= reader["SuNo"].ToString();
            obj_info.sLoginID= reader["LoginID"].ToString();
            obj_info.sPassword= reader["Password"].ToString();
            obj_info.sDeptNo= reader["DeptNo"].ToString();
            obj_info.sTrueName= reader["TrueName"].ToString();
            obj_info.iSex= int.Parse(reader["Sex"].ToString());
            obj_info.iUIDType= int.Parse(reader["UIDType"].ToString());
            obj_info.sUIDNo= reader["UIDNo"].ToString();
            obj_info.sEMail= reader["EMail"].ToString();
            obj_info.sTelphone= reader["Telphone"].ToString();
            obj_info.iLockState= int.Parse(reader["LockState"].ToString());
            obj_info.iAdminFlag= int.Parse(reader["AdminFlag"].ToString());
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

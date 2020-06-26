using EntFrm.Business.IDAL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using System;
using System.Data;
using System.Data.SqlClient;

namespace EntFrm.Business.SQLServerDAL
{
  public class DsDwloadFlowsDAL: IDsDwloadFlows
  {
        #region sql
        private const string SQL_GET_ALL_RECORDS = @"Select *  From DsDwloadFlows Where AppCode like @AppCode And ValidityState=1";
        private const string SQL_GET_RECORDS_BY_NO = @"Select * From DsDwloadFlows Where   AppCode like @AppCode And   ValidityState=1 And DFlowNo=@DFlowNo";
        private const string SQL_GET_NAME_BY_NO = @"Select Name From DsDwloadFlows Where   AppCode like @AppCode And   ValidityState=1 And DFlowNo=@DFlowNo";
        private const string SQL_ADD_RECORD = @"Insert into DsDwloadFlows
                                              (DFlowNo,DataFlag,ProgmNo,PlayerNo,PublishNo,DSchedule,IssueStatus,IssueDate,IFailCount,ISucCount,DloadProgress,DloadStatus,PlayRecord,BranchNo,AddOptor,AddDate,ModOptor,ModDate,ValidityState,Comments,AppCode)
                                              values(@DFlowNo,@DataFlag,@ProgmNo,@PlayerNo,@PublishNo,@DSchedule,@IssueStatus,@IssueDate,@IFailCount,@ISucCount,@DloadProgress,@DloadStatus,@PlayRecord,@BranchNo,@AddOptor,@AddDate,@ModOptor,@ModDate,@ValidityState,@Comments,@AppCode)";
        private const string SQL_UPDATE_RECORD = @"Update DsDwloadFlows set
                                                 DFlowNo=@DFlowNo,DataFlag=@DataFlag,ProgmNo=@ProgmNo,PlayerNo=@PlayerNo,PublishNo=@PublishNo,DSchedule=@DSchedule,IssueStatus=@IssueStatus,IssueDate=@IssueDate,IFailCount=@IFailCount,ISucCount=@ISucCount,DloadProgress=@DloadProgress,DloadStatus=@DloadStatus,PlayRecord=@PlayRecord,BranchNo=@BranchNo,AddOptor=@AddOptor,AddDate=@AddDate,ModOptor=@ModOptor,ModDate=@ModDate,ValidityState=@ValidityState,Comments=@Comments,AppCode=@AppCode 
                                                 Where  AppCode like @AppCode And   ValidityState=1 And DFlowNo=@DFlowNo  And Version=@Version";
        private const string SQL_HARD_DELETE_RECORD = @"Delete From DsDwloadFlows Where   AppCode like @AppCode And   DFlowNo=@DFlowNo ";
        private const string SQL_SOFT_DELETE_RECORD = @"Update DsDwloadFlows set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 And DFlowNo=@DFlowNo";
        private const string SQL_HARD_DELETE_BY_CONDTION = @"Delete From DsDwloadFlows Where   AppCode like @AppCode ";
        private const string SQL_SOFT_DELETE_BY_CONDTION = @"Update DsDwloadFlows set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 ";
        private const string SQL_GET_RECORDS_BY_CLASSNO = @"Select * From DsDwloadFlows Where    AppCode like @AppCode And   ValidityState=1 And PlayerNo=@PlayerNo";
        private const string SQL_GET_COUNT_BY_CONDITION = @"Select Count(*) From DsDwloadFlows Where   AppCode like @AppCode  And   ValidityState=1 ";
        #endregion

        #region param
        private const string PARAM_ID = "@ID";
        private const string PARAM_DFLOWNO = "@DFlowNo";
        private const string PARAM_DATAFLAG = "@DataFlag";
        private const string PARAM_PROGMNO = "@ProgmNo";
        private const string PARAM_PLAYERNO = "@PlayerNo";
        private const string PARAM_PUBLISHNO = "@PublishNo";
        private const string PARAM_DSCHEDULE = "@DSchedule";
        private const string PARAM_ISSUESTATUS = "@IssueStatus";
        private const string PARAM_ISSUEDATE = "@IssueDate";
        private const string PARAM_IFAILCOUNT = "@IFailCount";
        private const string PARAM_ISUCCOUNT = "@ISucCount";
        private const string PARAM_DLOADPROGRESS = "@DloadProgress";
        private const string PARAM_DLOADSTATUS = "@DloadStatus";
        private const string PARAM_PLAYRECORD = "@PlayRecord";
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

        public DsDwloadFlowsDAL(string sConnStr,string sAppCode)
        {
           this.connStr = sConnStr;
           this.appCode = sAppCode;
        }

        public DsDwloadFlowsCollections GetAllRecords()
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            DsDwloadFlowsCollections infos = null;
            DsDwloadFlows info = null;

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
                    infos = new DsDwloadFlowsCollections();
                    while (reader.Read())
                    {
                        info = new DsDwloadFlows();
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

        public DsDwloadFlowsCollections GetRecordsByClassNo(string sClassNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            DsDwloadFlowsCollections infos = null;
            DsDwloadFlows info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_PLAYERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sClassNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_CLASSNO,paras);

                if (reader.HasRows)
                {
                    infos = new DsDwloadFlowsCollections();
                    while (reader.Read())
                    {
                        info = new DsDwloadFlows();
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

        public DsDwloadFlowsCollections GetRecordsByNo(string sNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            DsDwloadFlowsCollections infos = null;
            DsDwloadFlows info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_DFLOWNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_NO,paras);

                if (reader.HasRows)
                {
                    infos = new DsDwloadFlowsCollections();
                    while (reader.Read())
                    {
                        info = new DsDwloadFlows();
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
                    new SqlParameter(PARAM_DFLOWNO,SqlDbType.NVarChar,20),
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

        public int AddNewRecord(DsDwloadFlows info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_DFLOWNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_DATAFLAG,SqlDbType.Int),
                    new SqlParameter(PARAM_PROGMNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_PLAYERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_PUBLISHNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_DSCHEDULE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_ISSUESTATUS,SqlDbType.Int),
                    new SqlParameter(PARAM_ISSUEDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_IFAILCOUNT,SqlDbType.Int),
                    new SqlParameter(PARAM_ISUCCOUNT,SqlDbType.Int),
                    new SqlParameter(PARAM_DLOADPROGRESS,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_DLOADSTATUS,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_PLAYRECORD,SqlDbType.NVarChar,1073741823),
                    new SqlParameter(PARAM_BRANCHNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_MODOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MODDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_VALIDITYSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_COMMENTS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = info.sDFlowNo;
                paras[1].Value = info.iDataFlag;
                paras[2].Value = info.sProgmNo;
                paras[3].Value = info.sPlayerNo;
                paras[4].Value = info.sPublishNo;
                paras[5].Value = info.dDSchedule;
                paras[6].Value = info.iIssueStatus;
                paras[7].Value = info.dIssueDate;
                paras[8].Value = info.iIFailCount;
                paras[9].Value = info.iISucCount;
                paras[10].Value = info.sDloadProgress;
                paras[11].Value = info.sDloadStatus;
                paras[12].Value = info.sPlayRecord;
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

        public int UpdateRecord(DsDwloadFlows info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_DFLOWNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_DATAFLAG,SqlDbType.Int),
                    new SqlParameter(PARAM_PROGMNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_PLAYERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_PUBLISHNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_DSCHEDULE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_ISSUESTATUS,SqlDbType.Int),
                    new SqlParameter(PARAM_ISSUEDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_IFAILCOUNT,SqlDbType.Int),
                    new SqlParameter(PARAM_ISUCCOUNT,SqlDbType.Int),
                    new SqlParameter(PARAM_DLOADPROGRESS,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_DLOADSTATUS,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_PLAYRECORD,SqlDbType.NVarChar,1073741823),
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
                paras[0].Value = info.sDFlowNo;
                paras[1].Value = info.iDataFlag;
                paras[2].Value = info.sProgmNo;
                paras[3].Value = info.sPlayerNo;
                paras[4].Value = info.sPublishNo;
                paras[5].Value = info.dDSchedule;
                paras[6].Value = info.iIssueStatus;
                paras[7].Value = info.dIssueDate;
                paras[8].Value = info.iIFailCount;
                paras[9].Value = info.iISucCount;
                paras[10].Value = info.sDloadProgress;
                paras[11].Value = info.sDloadStatus;
                paras[12].Value = info.sPlayRecord;
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
                    new SqlParameter(PARAM_DFLOWNO,SqlDbType.NVarChar,20),
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
                    new SqlParameter(PARAM_DFLOWNO,SqlDbType.NVarChar,20),
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
        public DsDwloadFlowsCollections GetRecords_Paging(SqlModel s_model)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            DsDwloadFlowsCollections infos = null;
            DsDwloadFlows info = null;

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
                    infos = new DsDwloadFlowsCollections();
                    while (reader.Read())
                    {
                        info = new DsDwloadFlows();
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
        internal static void PutObjectProperty(DsDwloadFlows obj_info, SqlDataReader reader)
        {
            obj_info.iID= int.Parse(reader["ID"].ToString());
            obj_info.sDFlowNo= reader["DFlowNo"].ToString();
            obj_info.iDataFlag= int.Parse(reader["DataFlag"].ToString());
            obj_info.sProgmNo= reader["ProgmNo"].ToString();
            obj_info.sPlayerNo= reader["PlayerNo"].ToString();
            obj_info.sPublishNo= reader["PublishNo"].ToString();
            obj_info.dDSchedule= DateTime.Parse(reader["DSchedule"].ToString());
            obj_info.iIssueStatus= int.Parse(reader["IssueStatus"].ToString());
            obj_info.dIssueDate= DateTime.Parse(reader["IssueDate"].ToString());
            obj_info.iIFailCount= int.Parse(reader["IFailCount"].ToString());
            obj_info.iISucCount= int.Parse(reader["ISucCount"].ToString());
            obj_info.sDloadProgress= reader["DloadProgress"].ToString();
            obj_info.sDloadStatus= reader["DloadStatus"].ToString();
            obj_info.sPlayRecord= reader["PlayRecord"].ToString();
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

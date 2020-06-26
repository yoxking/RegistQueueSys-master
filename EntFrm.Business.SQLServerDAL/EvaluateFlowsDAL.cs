using EntFrm.Business.IDAL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using System;
using System.Data;
using System.Data.SqlClient;

namespace EntFrm.Business.SQLServerDAL
{
  public class EvaluateFlowsDAL: IEvaluateFlows
  {
        #region sql
        private const string SQL_GET_ALL_RECORDS = @"Select *  From EvaluateFlows Where AppCode like @AppCode And ValidityState=1";
        private const string SQL_GET_RECORDS_BY_NO = @"Select * From EvaluateFlows Where   AppCode like @AppCode And   ValidityState=1 And EFlowNo=@EFlowNo";
        private const string SQL_GET_NAME_BY_NO = @"Select Name From EvaluateFlows Where   AppCode like @AppCode And   ValidityState=1 And EFlowNo=@EFlowNo";
        private const string SQL_ADD_RECORD = @"Insert into EvaluateFlows
                                              (EFlowNo,PFlowNo,DataFlag,EvaluateFlag,EvaluateTime,EvalCounterNo,EvalStafferNo,EvalResult,VeryGood,Good,Normal,Bad,Unknown,BranchNo,AddOptor,AddDate,ModOptor,ModDate,ValidityState,Comments,AppCode)
                                              values(@EFlowNo,@PFlowNo,@DataFlag,@EvaluateFlag,@EvaluateTime,@EvalCounterNo,@EvalStafferNo,@EvalResult,@VeryGood,@Good,@Normal,@Bad,@Unknown,@BranchNo,@AddOptor,@AddDate,@ModOptor,@ModDate,@ValidityState,@Comments,@AppCode)";
        private const string SQL_UPDATE_RECORD = @"Update EvaluateFlows set
                                                 EFlowNo=@EFlowNo,PFlowNo=@PFlowNo,DataFlag=@DataFlag,EvaluateFlag=@EvaluateFlag,EvaluateTime=@EvaluateTime,EvalCounterNo=@EvalCounterNo,EvalStafferNo=@EvalStafferNo,EvalResult=@EvalResult,VeryGood=@VeryGood,Good=@Good,Normal=@Normal,Bad=@Bad,Unknown=@Unknown,BranchNo=@BranchNo,AddOptor=@AddOptor,AddDate=@AddDate,ModOptor=@ModOptor,ModDate=@ModDate,ValidityState=@ValidityState,Comments=@Comments,AppCode=@AppCode 
                                                 Where  AppCode like @AppCode And   ValidityState=1 And EFlowNo=@EFlowNo  And Version=@Version";
        private const string SQL_HARD_DELETE_RECORD = @"Delete From EvaluateFlows Where   AppCode like @AppCode And   EFlowNo=@EFlowNo ";
        private const string SQL_SOFT_DELETE_RECORD = @"Update EvaluateFlows set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 And EFlowNo=@EFlowNo";
        private const string SQL_HARD_DELETE_BY_CONDTION = @"Delete From EvaluateFlows Where   AppCode like @AppCode ";
        private const string SQL_SOFT_DELETE_BY_CONDTION = @"Update EvaluateFlows set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 ";
        private const string SQL_GET_RECORDS_BY_CLASSNO = @"Select * From EvaluateFlows Where    AppCode like @AppCode And   ValidityState=1 And PFlowNo=@PFlowNo";
        private const string SQL_GET_COUNT_BY_CONDITION = @"Select Count(*) From EvaluateFlows Where   AppCode like @AppCode  And   ValidityState=1 ";
        #endregion

        #region param
        private const string PARAM_ID = "@ID";
        private const string PARAM_EFLOWNO = "@EFlowNo";
        private const string PARAM_PFLOWNO = "@PFlowNo";
        private const string PARAM_DATAFLAG = "@DataFlag";
        private const string PARAM_EVALUATEFLAG = "@EvaluateFlag";
        private const string PARAM_EVALUATETIME = "@EvaluateTime";
        private const string PARAM_EVALCOUNTERNO = "@EvalCounterNo";
        private const string PARAM_EVALSTAFFERNO = "@EvalStafferNo";
        private const string PARAM_EVALRESULT = "@EvalResult";
        private const string PARAM_VERYGOOD = "@VeryGood";
        private const string PARAM_GOOD = "@Good";
        private const string PARAM_NORMAL = "@Normal";
        private const string PARAM_BAD = "@Bad";
        private const string PARAM_UNKNOWN = "@Unknown";
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

        public EvaluateFlowsDAL(string sConnStr,string sAppCode)
        {
           this.connStr = sConnStr;
           this.appCode = sAppCode;
        }

        public EvaluateFlowsCollections GetAllRecords()
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            EvaluateFlowsCollections infos = null;
            EvaluateFlows info = null;

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
                    infos = new EvaluateFlowsCollections();
                    while (reader.Read())
                    {
                        info = new EvaluateFlows();
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

        public EvaluateFlowsCollections GetRecordsByClassNo(string sClassNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            EvaluateFlowsCollections infos = null;
            EvaluateFlows info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_PFLOWNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sClassNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_CLASSNO,paras);

                if (reader.HasRows)
                {
                    infos = new EvaluateFlowsCollections();
                    while (reader.Read())
                    {
                        info = new EvaluateFlows();
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

        public EvaluateFlowsCollections GetRecordsByNo(string sNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            EvaluateFlowsCollections infos = null;
            EvaluateFlows info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_EFLOWNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_NO,paras);

                if (reader.HasRows)
                {
                    infos = new EvaluateFlowsCollections();
                    while (reader.Read())
                    {
                        info = new EvaluateFlows();
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
                    new SqlParameter(PARAM_EFLOWNO,SqlDbType.NVarChar,20),
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

        public int AddNewRecord(EvaluateFlows info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_EFLOWNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_PFLOWNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_DATAFLAG,SqlDbType.Int),
                    new SqlParameter(PARAM_EVALUATEFLAG,SqlDbType.Int),
                    new SqlParameter(PARAM_EVALUATETIME,SqlDbType.DateTime),
                    new SqlParameter(PARAM_EVALCOUNTERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_EVALSTAFFERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_EVALRESULT,SqlDbType.Int),
                    new SqlParameter(PARAM_VERYGOOD,SqlDbType.Int),
                    new SqlParameter(PARAM_GOOD,SqlDbType.Int),
                    new SqlParameter(PARAM_NORMAL,SqlDbType.Int),
                    new SqlParameter(PARAM_BAD,SqlDbType.Int),
                    new SqlParameter(PARAM_UNKNOWN,SqlDbType.Int),
                    new SqlParameter(PARAM_BRANCHNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_MODOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MODDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_VALIDITYSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_COMMENTS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = info.sEFlowNo;
                paras[1].Value = info.sPFlowNo;
                paras[2].Value = info.iDataFlag;
                paras[3].Value = info.iEvaluateFlag;
                paras[4].Value = info.dEvaluateTime;
                paras[5].Value = info.sEvalCounterNo;
                paras[6].Value = info.sEvalStafferNo;
                paras[7].Value = info.iEvalResult;
                paras[8].Value = info.iVeryGood;
                paras[9].Value = info.iGood;
                paras[10].Value = info.iNormal;
                paras[11].Value = info.iBad;
                paras[12].Value = info.iUnknown;
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

        public int UpdateRecord(EvaluateFlows info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_EFLOWNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_PFLOWNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_DATAFLAG,SqlDbType.Int),
                    new SqlParameter(PARAM_EVALUATEFLAG,SqlDbType.Int),
                    new SqlParameter(PARAM_EVALUATETIME,SqlDbType.DateTime),
                    new SqlParameter(PARAM_EVALCOUNTERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_EVALSTAFFERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_EVALRESULT,SqlDbType.Int),
                    new SqlParameter(PARAM_VERYGOOD,SqlDbType.Int),
                    new SqlParameter(PARAM_GOOD,SqlDbType.Int),
                    new SqlParameter(PARAM_NORMAL,SqlDbType.Int),
                    new SqlParameter(PARAM_BAD,SqlDbType.Int),
                    new SqlParameter(PARAM_UNKNOWN,SqlDbType.Int),
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
                paras[0].Value = info.sEFlowNo;
                paras[1].Value = info.sPFlowNo;
                paras[2].Value = info.iDataFlag;
                paras[3].Value = info.iEvaluateFlag;
                paras[4].Value = info.dEvaluateTime;
                paras[5].Value = info.sEvalCounterNo;
                paras[6].Value = info.sEvalStafferNo;
                paras[7].Value = info.iEvalResult;
                paras[8].Value = info.iVeryGood;
                paras[9].Value = info.iGood;
                paras[10].Value = info.iNormal;
                paras[11].Value = info.iBad;
                paras[12].Value = info.iUnknown;
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
                    new SqlParameter(PARAM_EFLOWNO,SqlDbType.NVarChar,20),
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
                    new SqlParameter(PARAM_EFLOWNO,SqlDbType.NVarChar,20),
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
        public EvaluateFlowsCollections GetRecords_Paging(SqlModel s_model)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            EvaluateFlowsCollections infos = null;
            EvaluateFlows info = null;

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
                    infos = new EvaluateFlowsCollections();
                    while (reader.Read())
                    {
                        info = new EvaluateFlows();
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
        internal static void PutObjectProperty(EvaluateFlows obj_info, SqlDataReader reader)
        {
            obj_info.iID= int.Parse(reader["ID"].ToString());
            obj_info.sEFlowNo= reader["EFlowNo"].ToString();
            obj_info.sPFlowNo= reader["PFlowNo"].ToString();
            obj_info.iDataFlag= int.Parse(reader["DataFlag"].ToString());
            obj_info.iEvaluateFlag= int.Parse(reader["EvaluateFlag"].ToString());
            obj_info.dEvaluateTime= DateTime.Parse(reader["EvaluateTime"].ToString());
            obj_info.sEvalCounterNo= reader["EvalCounterNo"].ToString();
            obj_info.sEvalStafferNo= reader["EvalStafferNo"].ToString();
            obj_info.iEvalResult= int.Parse(reader["EvalResult"].ToString());
            obj_info.iVeryGood= int.Parse(reader["VeryGood"].ToString());
            obj_info.iGood= int.Parse(reader["Good"].ToString());
            obj_info.iNormal= int.Parse(reader["Normal"].ToString());
            obj_info.iBad= int.Parse(reader["Bad"].ToString());
            obj_info.iUnknown= int.Parse(reader["Unknown"].ToString());
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

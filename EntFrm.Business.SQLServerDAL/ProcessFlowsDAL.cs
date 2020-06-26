using EntFrm.Business.IDAL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using System;
using System.Data;
using System.Data.SqlClient;

namespace EntFrm.Business.SQLServerDAL
{
  public class ProcessFlowsDAL: IProcessFlows
  {
        #region sql
        private const string SQL_GET_ALL_RECORDS = @"Select *  From ProcessFlows Where AppCode like @AppCode And ValidityState=1";
        private const string SQL_GET_RECORDS_BY_NO = @"Select * From ProcessFlows Where   AppCode like @AppCode And   ValidityState=1 And PFlowNo=@PFlowNo";
        private const string SQL_GET_NAME_BY_NO = @"Select Name From ProcessFlows Where   AppCode like @AppCode And   ValidityState=1 And PFlowNo=@PFlowNo";
        private const string SQL_ADD_RECORD = @"Insert into ProcessFlows
                                              (PFlowNo,TFlowNo,DataFlag,ServiceNo,CounterNos,StafferNo,WaitAreaNo,WAreaIndex,WorkFlowsNo,WFlowsIndex,EnqueueTime,BeginTime,FinishTime,ProcessState,ProcessFormat,ProcessIndex,PriorityType,OrderWeight,PauseState,DelayType,DelayTimeValue,DelayStepValue,ProcessedTime,ProcessedCounterNo,ProcessedStafferNo,BranchNo,AddOptor,AddDate,ModOptor,ModDate,ValidityState,Comments,AppCode)
                                              values(@PFlowNo,@TFlowNo,@DataFlag,@ServiceNo,@CounterNos,@StafferNo,@WaitAreaNo,@WAreaIndex,@WorkFlowsNo,@WFlowsIndex,@EnqueueTime,@BeginTime,@FinishTime,@ProcessState,@ProcessFormat,@ProcessIndex,@PriorityType,@OrderWeight,@PauseState,@DelayType,@DelayTimeValue,@DelayStepValue,@ProcessedTime,@ProcessedCounterNo,@ProcessedStafferNo,@BranchNo,@AddOptor,@AddDate,@ModOptor,@ModDate,@ValidityState,@Comments,@AppCode)";
        private const string SQL_UPDATE_RECORD = @"Update ProcessFlows set
                                                 PFlowNo=@PFlowNo,TFlowNo=@TFlowNo,DataFlag=@DataFlag,ServiceNo=@ServiceNo,CounterNos=@CounterNos,StafferNo=@StafferNo,WaitAreaNo=@WaitAreaNo,WAreaIndex=@WAreaIndex,WorkFlowsNo=@WorkFlowsNo,WFlowsIndex=@WFlowsIndex,EnqueueTime=@EnqueueTime,BeginTime=@BeginTime,FinishTime=@FinishTime,ProcessState=@ProcessState,ProcessFormat=@ProcessFormat,ProcessIndex=@ProcessIndex,PriorityType=@PriorityType,OrderWeight=@OrderWeight,PauseState=@PauseState,DelayType=@DelayType,DelayTimeValue=@DelayTimeValue,DelayStepValue=@DelayStepValue,ProcessedTime=@ProcessedTime,ProcessedCounterNo=@ProcessedCounterNo,ProcessedStafferNo=@ProcessedStafferNo,BranchNo=@BranchNo,AddOptor=@AddOptor,AddDate=@AddDate,ModOptor=@ModOptor,ModDate=@ModDate,ValidityState=@ValidityState,Comments=@Comments,AppCode=@AppCode 
                                                 Where  AppCode like @AppCode And   ValidityState=1 And PFlowNo=@PFlowNo  And Version=@Version";
        private const string SQL_HARD_DELETE_RECORD = @"Delete From ProcessFlows Where   AppCode like @AppCode And   PFlowNo=@PFlowNo ";
        private const string SQL_SOFT_DELETE_RECORD = @"Update ProcessFlows set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 And PFlowNo=@PFlowNo";
        private const string SQL_HARD_DELETE_BY_CONDTION = @"Delete From ProcessFlows Where   AppCode like @AppCode ";
        private const string SQL_SOFT_DELETE_BY_CONDTION = @"Update ProcessFlows set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 ";
        private const string SQL_GET_RECORDS_BY_CLASSNO = @"Select * From ProcessFlows Where    AppCode like @AppCode And   ValidityState=1 And ServiceNo=@ServiceNo";
        private const string SQL_GET_COUNT_BY_CONDITION = @"Select Count(*) From ProcessFlows Where   AppCode like @AppCode  And   ValidityState=1 ";
        #endregion

        #region param
        private const string PARAM_ID = "@ID";
        private const string PARAM_PFLOWNO = "@PFlowNo";
        private const string PARAM_TFLOWNO = "@TFlowNo";
        private const string PARAM_DATAFLAG = "@DataFlag";
        private const string PARAM_SERVICENO = "@ServiceNo";
        private const string PARAM_COUNTERNOS = "@CounterNos";
        private const string PARAM_STAFFERNO = "@StafferNo";
        private const string PARAM_WAITAREANO = "@WaitAreaNo";
        private const string PARAM_WAREAINDEX = "@WAreaIndex";
        private const string PARAM_WORKFLOWSNO = "@WorkFlowsNo";
        private const string PARAM_WFLOWSINDEX = "@WFlowsIndex";
        private const string PARAM_ENQUEUETIME = "@EnqueueTime";
        private const string PARAM_BEGINTIME = "@BeginTime";
        private const string PARAM_FINISHTIME = "@FinishTime";
        private const string PARAM_PROCESSSTATE = "@ProcessState";
        private const string PARAM_PROCESSFORMAT = "@ProcessFormat";
        private const string PARAM_PROCESSINDEX = "@ProcessIndex";
        private const string PARAM_PRIORITYTYPE = "@PriorityType";
        private const string PARAM_ORDERWEIGHT = "@OrderWeight";
        private const string PARAM_PAUSESTATE = "@PauseState";
        private const string PARAM_DELAYTYPE = "@DelayType";
        private const string PARAM_DELAYTIMEVALUE = "@DelayTimeValue";
        private const string PARAM_DELAYSTEPVALUE = "@DelayStepValue";
        private const string PARAM_PROCESSEDTIME = "@ProcessedTime";
        private const string PARAM_PROCESSEDCOUNTERNO = "@ProcessedCounterNo";
        private const string PARAM_PROCESSEDSTAFFERNO = "@ProcessedStafferNo";
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

        public ProcessFlowsDAL(string sConnStr,string sAppCode)
        {
           this.connStr = sConnStr;
           this.appCode = sAppCode;
        }

        public ProcessFlowsCollections GetAllRecords()
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            ProcessFlowsCollections infos = null;
            ProcessFlows info = null;

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
                    infos = new ProcessFlowsCollections();
                    while (reader.Read())
                    {
                        info = new ProcessFlows();
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

        public ProcessFlowsCollections GetRecordsByClassNo(string sClassNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            ProcessFlowsCollections infos = null;
            ProcessFlows info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_SERVICENO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sClassNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_CLASSNO,paras);

                if (reader.HasRows)
                {
                    infos = new ProcessFlowsCollections();
                    while (reader.Read())
                    {
                        info = new ProcessFlows();
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

        public ProcessFlowsCollections GetRecordsByNo(string sNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            ProcessFlowsCollections infos = null;
            ProcessFlows info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_PFLOWNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_NO,paras);

                if (reader.HasRows)
                {
                    infos = new ProcessFlowsCollections();
                    while (reader.Read())
                    {
                        info = new ProcessFlows();
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
                    new SqlParameter(PARAM_PFLOWNO,SqlDbType.NVarChar,20),
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

        public int AddNewRecord(ProcessFlows info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_PFLOWNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_TFLOWNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_DATAFLAG,SqlDbType.Int),
                    new SqlParameter(PARAM_SERVICENO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_COUNTERNOS,SqlDbType.NVarChar,200),
                    new SqlParameter(PARAM_STAFFERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_WAITAREANO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_WAREAINDEX,SqlDbType.Int),
                    new SqlParameter(PARAM_WORKFLOWSNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_WFLOWSINDEX,SqlDbType.Int),
                    new SqlParameter(PARAM_ENQUEUETIME,SqlDbType.DateTime),
                    new SqlParameter(PARAM_BEGINTIME,SqlDbType.DateTime),
                    new SqlParameter(PARAM_FINISHTIME,SqlDbType.DateTime),
                    new SqlParameter(PARAM_PROCESSSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_PROCESSFORMAT,SqlDbType.NVarChar,2000),
                    new SqlParameter(PARAM_PROCESSINDEX,SqlDbType.Int),
                    new SqlParameter(PARAM_PRIORITYTYPE,SqlDbType.Int),
                    new SqlParameter(PARAM_ORDERWEIGHT,SqlDbType.Int),
                    new SqlParameter(PARAM_PAUSESTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_DELAYTYPE,SqlDbType.Int),
                    new SqlParameter(PARAM_DELAYTIMEVALUE,SqlDbType.Int),
                    new SqlParameter(PARAM_DELAYSTEPVALUE,SqlDbType.Int),
                    new SqlParameter(PARAM_PROCESSEDTIME,SqlDbType.DateTime),
                    new SqlParameter(PARAM_PROCESSEDCOUNTERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_PROCESSEDSTAFFERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_BRANCHNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_MODOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MODDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_VALIDITYSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_COMMENTS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = info.sPFlowNo;
                paras[1].Value = info.sTFlowNo;
                paras[2].Value = info.iDataFlag;
                paras[3].Value = info.sServiceNo;
                paras[4].Value = info.sCounterNos;
                paras[5].Value = info.sStafferNo;
                paras[6].Value = info.sWaitAreaNo;
                paras[7].Value = info.iWAreaIndex;
                paras[8].Value = info.sWorkFlowsNo;
                paras[9].Value = info.iWFlowsIndex;
                paras[10].Value = info.dEnqueueTime;
                paras[11].Value = info.dBeginTime;
                paras[12].Value = info.dFinishTime;
                paras[13].Value = info.iProcessState;
                paras[14].Value = info.sProcessFormat;
                paras[15].Value = info.iProcessIndex;
                paras[16].Value = info.iPriorityType;
                paras[17].Value = info.iOrderWeight;
                paras[18].Value = info.iPauseState;
                paras[19].Value = info.iDelayType;
                paras[20].Value = info.iDelayTimeValue;
                paras[21].Value = info.iDelayStepValue;
                paras[22].Value = info.dProcessedTime;
                paras[23].Value = info.sProcessedCounterNo;
                paras[24].Value = info.sProcessedStafferNo;
                paras[25].Value = info.sBranchNo;
                paras[26].Value = info.sAddOptor;
                paras[27].Value = info.dAddDate;
                paras[28].Value = info.sModOptor;
                paras[29].Value = info.dModDate;
                paras[30].Value = info.iValidityState;
                paras[31].Value = info.sComments;
                paras[32].Value = info.sAppCode;

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

        public int UpdateRecord(ProcessFlows info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_PFLOWNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_TFLOWNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_DATAFLAG,SqlDbType.Int),
                    new SqlParameter(PARAM_SERVICENO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_COUNTERNOS,SqlDbType.NVarChar,200),
                    new SqlParameter(PARAM_STAFFERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_WAITAREANO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_WAREAINDEX,SqlDbType.Int),
                    new SqlParameter(PARAM_WORKFLOWSNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_WFLOWSINDEX,SqlDbType.Int),
                    new SqlParameter(PARAM_ENQUEUETIME,SqlDbType.DateTime),
                    new SqlParameter(PARAM_BEGINTIME,SqlDbType.DateTime),
                    new SqlParameter(PARAM_FINISHTIME,SqlDbType.DateTime),
                    new SqlParameter(PARAM_PROCESSSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_PROCESSFORMAT,SqlDbType.NVarChar,2000),
                    new SqlParameter(PARAM_PROCESSINDEX,SqlDbType.Int),
                    new SqlParameter(PARAM_PRIORITYTYPE,SqlDbType.Int),
                    new SqlParameter(PARAM_ORDERWEIGHT,SqlDbType.Int),
                    new SqlParameter(PARAM_PAUSESTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_DELAYTYPE,SqlDbType.Int),
                    new SqlParameter(PARAM_DELAYTIMEVALUE,SqlDbType.Int),
                    new SqlParameter(PARAM_DELAYSTEPVALUE,SqlDbType.Int),
                    new SqlParameter(PARAM_PROCESSEDTIME,SqlDbType.DateTime),
                    new SqlParameter(PARAM_PROCESSEDCOUNTERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_PROCESSEDSTAFFERNO,SqlDbType.NVarChar,20),
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
                paras[0].Value = info.sPFlowNo;
                paras[1].Value = info.sTFlowNo;
                paras[2].Value = info.iDataFlag;
                paras[3].Value = info.sServiceNo;
                paras[4].Value = info.sCounterNos;
                paras[5].Value = info.sStafferNo;
                paras[6].Value = info.sWaitAreaNo;
                paras[7].Value = info.iWAreaIndex;
                paras[8].Value = info.sWorkFlowsNo;
                paras[9].Value = info.iWFlowsIndex;
                paras[10].Value = info.dEnqueueTime;
                paras[11].Value = info.dBeginTime;
                paras[12].Value = info.dFinishTime;
                paras[13].Value = info.iProcessState;
                paras[14].Value = info.sProcessFormat;
                paras[15].Value = info.iProcessIndex;
                paras[16].Value = info.iPriorityType;
                paras[17].Value = info.iOrderWeight;
                paras[18].Value = info.iPauseState;
                paras[19].Value = info.iDelayType;
                paras[20].Value = info.iDelayTimeValue;
                paras[21].Value = info.iDelayStepValue;
                paras[22].Value = info.dProcessedTime;
                paras[23].Value = info.sProcessedCounterNo;
                paras[24].Value = info.sProcessedStafferNo;
                paras[25].Value = info.sBranchNo;
                paras[26].Value = info.sAddOptor;
                paras[27].Value = info.dAddDate;
                paras[28].Value = info.sModOptor;
                paras[29].Value = info.dModDate;
                paras[30].Value = info.iValidityState;
                paras[31].Value = info.sComments;
                paras[32].Value = info.sAppCode;
                paras[33].Value = StringHelper.ConvertToBytes(info.sVersion);

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
                    new SqlParameter(PARAM_PFLOWNO,SqlDbType.NVarChar,20),
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
                    new SqlParameter(PARAM_PFLOWNO,SqlDbType.NVarChar,20),
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
        public ProcessFlowsCollections GetRecords_Paging(SqlModel s_model)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            ProcessFlowsCollections infos = null;
            ProcessFlows info = null;

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
                    infos = new ProcessFlowsCollections();
                    while (reader.Read())
                    {
                        info = new ProcessFlows();
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
        internal static void PutObjectProperty(ProcessFlows obj_info, SqlDataReader reader)
        {
            obj_info.iID= int.Parse(reader["ID"].ToString());
            obj_info.sPFlowNo= reader["PFlowNo"].ToString();
            obj_info.sTFlowNo= reader["TFlowNo"].ToString();
            obj_info.iDataFlag= int.Parse(reader["DataFlag"].ToString());
            obj_info.sServiceNo= reader["ServiceNo"].ToString();
            obj_info.sCounterNos= reader["CounterNos"].ToString();
            obj_info.sStafferNo= reader["StafferNo"].ToString();
            obj_info.sWaitAreaNo= reader["WaitAreaNo"].ToString();
            obj_info.iWAreaIndex= int.Parse(reader["WAreaIndex"].ToString());
            obj_info.sWorkFlowsNo= reader["WorkFlowsNo"].ToString();
            obj_info.iWFlowsIndex= int.Parse(reader["WFlowsIndex"].ToString());
            obj_info.dEnqueueTime= DateTime.Parse(reader["EnqueueTime"].ToString());
            obj_info.dBeginTime= DateTime.Parse(reader["BeginTime"].ToString());
            obj_info.dFinishTime= DateTime.Parse(reader["FinishTime"].ToString());
            obj_info.iProcessState= int.Parse(reader["ProcessState"].ToString());
            obj_info.sProcessFormat= reader["ProcessFormat"].ToString();
            obj_info.iProcessIndex= int.Parse(reader["ProcessIndex"].ToString());
            obj_info.iPriorityType= int.Parse(reader["PriorityType"].ToString());
            obj_info.iOrderWeight= int.Parse(reader["OrderWeight"].ToString());
            obj_info.iPauseState= int.Parse(reader["PauseState"].ToString());
            obj_info.iDelayType= int.Parse(reader["DelayType"].ToString());
            obj_info.iDelayTimeValue= int.Parse(reader["DelayTimeValue"].ToString());
            obj_info.iDelayStepValue= int.Parse(reader["DelayStepValue"].ToString());
            obj_info.dProcessedTime= DateTime.Parse(reader["ProcessedTime"].ToString());
            obj_info.sProcessedCounterNo= reader["ProcessedCounterNo"].ToString();
            obj_info.sProcessedStafferNo= reader["ProcessedStafferNo"].ToString();
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

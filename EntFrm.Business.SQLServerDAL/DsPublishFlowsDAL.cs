using EntFrm.Business.IDAL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using System;
using System.Data;
using System.Data.SqlClient;

namespace EntFrm.Business.SQLServerDAL
{
  public class DsPublishFlowsDAL: IDsPublishFlows
  {
        #region sql
        private const string SQL_GET_ALL_RECORDS = @"Select *  From DsPublishFlows Where AppCode like @AppCode And ValidityState=1";
        private const string SQL_GET_RECORDS_BY_NO = @"Select * From DsPublishFlows Where   AppCode like @AppCode And   ValidityState=1 And PFlowNo=@PFlowNo";
        private const string SQL_GET_NAME_BY_NO = @"Select Name From DsPublishFlows Where   AppCode like @AppCode And   ValidityState=1 And PFlowNo=@PFlowNo";
        private const string SQL_ADD_RECORD = @"Insert into DsPublishFlows
                                              (PFlowNo,DataFlag,ProgmNo,ProgmType,PlayerNos,PlayMode,PlayWeeks,StartDate,EnditDate,StartTime,EnditTime,PublishState,PublishOptor,PublishDate,CheckState,CheckOptor,CheckDate,BranchNo,AddOptor,AddDate,ModOptor,ModDate,ValidityState,Comments,AppCode)
                                              values(@PFlowNo,@DataFlag,@ProgmNo,@ProgmType,@PlayerNos,@PlayMode,@PlayWeeks,@StartDate,@EnditDate,@StartTime,@EnditTime,@PublishState,@PublishOptor,@PublishDate,@CheckState,@CheckOptor,@CheckDate,@BranchNo,@AddOptor,@AddDate,@ModOptor,@ModDate,@ValidityState,@Comments,@AppCode)";
        private const string SQL_UPDATE_RECORD = @"Update DsPublishFlows set
                                                 PFlowNo=@PFlowNo,DataFlag=@DataFlag,ProgmNo=@ProgmNo,ProgmType=@ProgmType,PlayerNos=@PlayerNos,PlayMode=@PlayMode,PlayWeeks=@PlayWeeks,StartDate=@StartDate,EnditDate=@EnditDate,StartTime=@StartTime,EnditTime=@EnditTime,PublishState=@PublishState,PublishOptor=@PublishOptor,PublishDate=@PublishDate,CheckState=@CheckState,CheckOptor=@CheckOptor,CheckDate=@CheckDate,BranchNo=@BranchNo,AddOptor=@AddOptor,AddDate=@AddDate,ModOptor=@ModOptor,ModDate=@ModDate,ValidityState=@ValidityState,Comments=@Comments,AppCode=@AppCode 
                                                 Where  AppCode like @AppCode And   ValidityState=1 And PFlowNo=@PFlowNo  And Version=@Version";
        private const string SQL_HARD_DELETE_RECORD = @"Delete From DsPublishFlows Where   AppCode like @AppCode And   PFlowNo=@PFlowNo ";
        private const string SQL_SOFT_DELETE_RECORD = @"Update DsPublishFlows set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 And PFlowNo=@PFlowNo";
        private const string SQL_HARD_DELETE_BY_CONDTION = @"Delete From DsPublishFlows Where   AppCode like @AppCode ";
        private const string SQL_SOFT_DELETE_BY_CONDTION = @"Update DsPublishFlows set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 ";
        private const string SQL_GET_RECORDS_BY_CLASSNO = @"Select * From DsPublishFlows Where    AppCode like @AppCode And   ValidityState=1 And ProgmNo=@ProgmNo";
        private const string SQL_GET_COUNT_BY_CONDITION = @"Select Count(*) From DsPublishFlows Where   AppCode like @AppCode  And   ValidityState=1 ";
        #endregion

        #region param
        private const string PARAM_ID = "@ID";
        private const string PARAM_PFLOWNO = "@PFlowNo";
        private const string PARAM_DATAFLAG = "@DataFlag";
        private const string PARAM_PROGMNO = "@ProgmNo";
        private const string PARAM_PROGMTYPE = "@ProgmType";
        private const string PARAM_PLAYERNOS = "@PlayerNos";
        private const string PARAM_PLAYMODE = "@PlayMode";
        private const string PARAM_PLAYWEEKS = "@PlayWeeks";
        private const string PARAM_STARTDATE = "@StartDate";
        private const string PARAM_ENDITDATE = "@EnditDate";
        private const string PARAM_STARTTIME = "@StartTime";
        private const string PARAM_ENDITTIME = "@EnditTime";
        private const string PARAM_PUBLISHSTATE = "@PublishState";
        private const string PARAM_PUBLISHOPTOR = "@PublishOptor";
        private const string PARAM_PUBLISHDATE = "@PublishDate";
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

        public DsPublishFlowsDAL(string sConnStr,string sAppCode)
        {
           this.connStr = sConnStr;
           this.appCode = sAppCode;
        }

        public DsPublishFlowsCollections GetAllRecords()
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            DsPublishFlowsCollections infos = null;
            DsPublishFlows info = null;

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
                    infos = new DsPublishFlowsCollections();
                    while (reader.Read())
                    {
                        info = new DsPublishFlows();
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

        public DsPublishFlowsCollections GetRecordsByClassNo(string sClassNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            DsPublishFlowsCollections infos = null;
            DsPublishFlows info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_PROGMNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sClassNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_CLASSNO,paras);

                if (reader.HasRows)
                {
                    infos = new DsPublishFlowsCollections();
                    while (reader.Read())
                    {
                        info = new DsPublishFlows();
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

        public DsPublishFlowsCollections GetRecordsByNo(string sNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            DsPublishFlowsCollections infos = null;
            DsPublishFlows info = null;

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
                    infos = new DsPublishFlowsCollections();
                    while (reader.Read())
                    {
                        info = new DsPublishFlows();
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

        public int AddNewRecord(DsPublishFlows info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_PFLOWNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_DATAFLAG,SqlDbType.Int),
                    new SqlParameter(PARAM_PROGMNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_PROGMTYPE,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_PLAYERNOS,SqlDbType.NVarChar,1073741823),
                    new SqlParameter(PARAM_PLAYMODE,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_PLAYWEEKS,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_STARTDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_ENDITDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_STARTTIME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_ENDITTIME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_PUBLISHSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_PUBLISHOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_PUBLISHDATE,SqlDbType.DateTime),
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
                paras[0].Value = info.sPFlowNo;
                paras[1].Value = info.iDataFlag;
                paras[2].Value = info.sProgmNo;
                paras[3].Value = info.sProgmType;
                paras[4].Value = info.sPlayerNos;
                paras[5].Value = info.sPlayMode;
                paras[6].Value = info.sPlayWeeks;
                paras[7].Value = info.dStartDate;
                paras[8].Value = info.dEnditDate;
                paras[9].Value = info.sStartTime;
                paras[10].Value = info.sEnditTime;
                paras[11].Value = info.iPublishState;
                paras[12].Value = info.sPublishOptor;
                paras[13].Value = info.dPublishDate;
                paras[14].Value = info.iCheckState;
                paras[15].Value = info.sCheckOptor;
                paras[16].Value = info.dCheckDate;
                paras[17].Value = info.sBranchNo;
                paras[18].Value = info.sAddOptor;
                paras[19].Value = info.dAddDate;
                paras[20].Value = info.sModOptor;
                paras[21].Value = info.dModDate;
                paras[22].Value = info.iValidityState;
                paras[23].Value = info.sComments;
                paras[24].Value = info.sAppCode;

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

        public int UpdateRecord(DsPublishFlows info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_PFLOWNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_DATAFLAG,SqlDbType.Int),
                    new SqlParameter(PARAM_PROGMNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_PROGMTYPE,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_PLAYERNOS,SqlDbType.NVarChar,1073741823),
                    new SqlParameter(PARAM_PLAYMODE,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_PLAYWEEKS,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_STARTDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_ENDITDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_STARTTIME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_ENDITTIME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_PUBLISHSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_PUBLISHOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_PUBLISHDATE,SqlDbType.DateTime),
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
                paras[0].Value = info.sPFlowNo;
                paras[1].Value = info.iDataFlag;
                paras[2].Value = info.sProgmNo;
                paras[3].Value = info.sProgmType;
                paras[4].Value = info.sPlayerNos;
                paras[5].Value = info.sPlayMode;
                paras[6].Value = info.sPlayWeeks;
                paras[7].Value = info.dStartDate;
                paras[8].Value = info.dEnditDate;
                paras[9].Value = info.sStartTime;
                paras[10].Value = info.sEnditTime;
                paras[11].Value = info.iPublishState;
                paras[12].Value = info.sPublishOptor;
                paras[13].Value = info.dPublishDate;
                paras[14].Value = info.iCheckState;
                paras[15].Value = info.sCheckOptor;
                paras[16].Value = info.dCheckDate;
                paras[17].Value = info.sBranchNo;
                paras[18].Value = info.sAddOptor;
                paras[19].Value = info.dAddDate;
                paras[20].Value = info.sModOptor;
                paras[21].Value = info.dModDate;
                paras[22].Value = info.iValidityState;
                paras[23].Value = info.sComments;
                paras[24].Value = info.sAppCode;
                paras[25].Value = StringHelper.ConvertToBytes(info.sVersion);

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
        public DsPublishFlowsCollections GetRecords_Paging(SqlModel s_model)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            DsPublishFlowsCollections infos = null;
            DsPublishFlows info = null;

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
                    infos = new DsPublishFlowsCollections();
                    while (reader.Read())
                    {
                        info = new DsPublishFlows();
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
        internal static void PutObjectProperty(DsPublishFlows obj_info, SqlDataReader reader)
        {
            obj_info.iID= int.Parse(reader["ID"].ToString());
            obj_info.sPFlowNo= reader["PFlowNo"].ToString();
            obj_info.iDataFlag= int.Parse(reader["DataFlag"].ToString());
            obj_info.sProgmNo= reader["ProgmNo"].ToString();
            obj_info.sProgmType= reader["ProgmType"].ToString();
            obj_info.sPlayerNos= reader["PlayerNos"].ToString();
            obj_info.sPlayMode= reader["PlayMode"].ToString();
            obj_info.sPlayWeeks= reader["PlayWeeks"].ToString();
            obj_info.dStartDate= DateTime.Parse(reader["StartDate"].ToString());
            obj_info.dEnditDate= DateTime.Parse(reader["EnditDate"].ToString());
            obj_info.sStartTime= reader["StartTime"].ToString();
            obj_info.sEnditTime= reader["EnditTime"].ToString();
            obj_info.iPublishState= int.Parse(reader["PublishState"].ToString());
            obj_info.sPublishOptor= reader["PublishOptor"].ToString();
            obj_info.dPublishDate= DateTime.Parse(reader["PublishDate"].ToString());
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

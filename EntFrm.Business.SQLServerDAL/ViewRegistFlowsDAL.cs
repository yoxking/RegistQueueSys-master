using EntFrm.Business.IDAL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using System;
using System.Data;
using System.Data.SqlClient;

namespace EntFrm.Business.SQLServerDAL
{
  public class ViewRegistFlowsDAL: IViewRegistFlows
  {
        #region sql
        private const string SQL_GET_ALL_RECORDS = @"Select *  From ViewRegistFlows Where AppCode like @AppCode And ValidityState=1";
        private const string SQL_GET_RECORDS_BY_NO = @"Select * From ViewRegistFlows Where   AppCode like @AppCode And   ValidityState=1 And RFlowNo=@RFlowNo";
        private const string SQL_GET_NAME_BY_NO = @"Select TicketNo From ViewRegistFlows Where   AppCode like @AppCode And   ValidityState=1 And RFlowNo=@RFlowNo";
        private const string SQL_ADD_RECORD = @"Insert into ViewRegistFlows
                                              (RFlowNo,DataFlag,TicketNo,RUserNo,CnName,EnName,Age,Sex,Nation,CardType,IdCardNo,RiCardNo,Telphone,Address,PostCode,HeadPhoto,Summary,ServiceNo,StafferNo,RegistType,DataFrom,RegistDate,WorkTime,StartDate,EnditDate,RegistState,BranchNo,AddOptor,AddDate,ModOptor,ModDate,ValidityState,Comments,AppCode)
                                              values(@RFlowNo,@DataFlag,@TicketNo,@RUserNo,@CnName,@EnName,@Age,@Sex,@Nation,@CardType,@IdCardNo,@RiCardNo,@Telphone,@Address,@PostCode,@HeadPhoto,@Summary,@ServiceNo,@StafferNo,@RegistType,@DataFrom,@RegistDate,@WorkTime,@StartDate,@EnditDate,@RegistState,@BranchNo,@AddOptor,@AddDate,@ModOptor,@ModDate,@ValidityState,@Comments,@AppCode)";
        private const string SQL_UPDATE_RECORD = @"Update ViewRegistFlows set
                                                 RFlowNo=@RFlowNo,DataFlag=@DataFlag,TicketNo=@TicketNo,RUserNo=@RUserNo,CnName=@CnName,EnName=@EnName,Age=@Age,Sex=@Sex,Nation=@Nation,CardType=@CardType,IdCardNo=@IdCardNo,RiCardNo=@RiCardNo,Telphone=@Telphone,Address=@Address,PostCode=@PostCode,HeadPhoto=@HeadPhoto,Summary=@Summary,ServiceNo=@ServiceNo,StafferNo=@StafferNo,RegistType=@RegistType,DataFrom=@DataFrom,RegistDate=@RegistDate,WorkTime=@WorkTime,StartDate=@StartDate,EnditDate=@EnditDate,RegistState=@RegistState,BranchNo=@BranchNo,AddOptor=@AddOptor,AddDate=@AddDate,ModOptor=@ModOptor,ModDate=@ModDate,ValidityState=@ValidityState,Comments=@Comments,AppCode=@AppCode 
                                                 Where  AppCode like @AppCode And   ValidityState=1 And RFlowNo=@RFlowNo  And Version=@Version";
        private const string SQL_HARD_DELETE_RECORD = @"Delete From ViewRegistFlows Where   AppCode like @AppCode And   RFlowNo=@RFlowNo ";
        private const string SQL_SOFT_DELETE_RECORD = @"Update ViewRegistFlows set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 And RFlowNo=@RFlowNo";
        private const string SQL_HARD_DELETE_BY_CONDTION = @"Delete From ViewRegistFlows Where   AppCode like @AppCode ";
        private const string SQL_SOFT_DELETE_BY_CONDTION = @"Update ViewRegistFlows set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 ";
        private const string SQL_GET_RECORDS_BY_CLASSNO = @"Select * From ViewRegistFlows Where    AppCode like @AppCode And   ValidityState=1 And ClassNo=@ClassNo";
        private const string SQL_GET_COUNT_BY_CONDITION = @"Select Count(*) From ViewRegistFlows Where   AppCode like @AppCode  And   ValidityState=1 ";
        #endregion

        #region param
        private const string PARAM_ID = "@ID";
        private const string PARAM_RFLOWNO = "@RFlowNo";
        private const string PARAM_DATAFLAG = "@DataFlag";
        private const string PARAM_TICKETNO = "@TicketNo";
        private const string PARAM_RUSERNO = "@RUserNo";
        private const string PARAM_CNNAME = "@CnName";
        private const string PARAM_ENNAME = "@EnName";
        private const string PARAM_AGE = "@Age";
        private const string PARAM_SEX = "@Sex";
        private const string PARAM_NATION = "@Nation";
        private const string PARAM_CARDTYPE = "@CardType";
        private const string PARAM_IDCARDNO = "@IdCardNo";
        private const string PARAM_RICARDNO = "@RiCardNo";
        private const string PARAM_TELPHONE = "@Telphone";
        private const string PARAM_ADDRESS = "@Address";
        private const string PARAM_POSTCODE = "@PostCode";
        private const string PARAM_HEADPHOTO = "@HeadPhoto";
        private const string PARAM_SUMMARY = "@Summary";
        private const string PARAM_SERVICENO = "@ServiceNo";
        private const string PARAM_STAFFERNO = "@StafferNo";
        private const string PARAM_REGISTTYPE = "@RegistType";
        private const string PARAM_DATAFROM = "@DataFrom";
        private const string PARAM_REGISTDATE = "@RegistDate";
        private const string PARAM_WORKTIME = "@WorkTime";
        private const string PARAM_STARTDATE = "@StartDate";
        private const string PARAM_ENDITDATE = "@EnditDate";
        private const string PARAM_REGISTSTATE = "@RegistState";
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

        public ViewRegistFlowsDAL(string sConnStr,string sAppCode)
        {
           this.connStr = sConnStr;
           this.appCode = sAppCode;
        }

        public ViewRegistFlowsCollections GetAllRecords()
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            ViewRegistFlowsCollections infos = null;
            ViewRegistFlows info = null;

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
                    infos = new ViewRegistFlowsCollections();
                    while (reader.Read())
                    {
                        info = new ViewRegistFlows();
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

        public ViewRegistFlowsCollections GetRecordsByClassNo(string sClassNo)
        {
            /*SqlConnection connection = null;
            SqlDataReader reader = null;
            ViewRegistFlowsCollections infos = null;
            ViewRegistFlows info = null;

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
                    infos = new ViewRegistFlowsCollections();
                    while (reader.Read())
                    {
                        info = new ViewRegistFlows();
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

        public ViewRegistFlowsCollections GetRecordsByNo(string sNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            ViewRegistFlowsCollections infos = null;
            ViewRegistFlows info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_RFLOWNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_NO,paras);

                if (reader.HasRows)
                {
                    infos = new ViewRegistFlowsCollections();
                    while (reader.Read())
                    {
                        info = new ViewRegistFlows();
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
                    new SqlParameter(PARAM_RFLOWNO,SqlDbType.NVarChar,20),
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

        public int AddNewRecord(ViewRegistFlows info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_RFLOWNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_DATAFLAG,SqlDbType.Int),
                    new SqlParameter(PARAM_TICKETNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_RUSERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_CNNAME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_ENNAME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_AGE,SqlDbType.Int),
                    new SqlParameter(PARAM_SEX,SqlDbType.Int),
                    new SqlParameter(PARAM_NATION,SqlDbType.NVarChar,10),
                    new SqlParameter(PARAM_CARDTYPE,SqlDbType.Int),
                    new SqlParameter(PARAM_IDCARDNO,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_RICARDNO,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_TELPHONE,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_ADDRESS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_POSTCODE,SqlDbType.NVarChar,10),
                    new SqlParameter(PARAM_HEADPHOTO,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_SUMMARY,SqlDbType.NVarChar,2000),
                    new SqlParameter(PARAM_SERVICENO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_STAFFERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_REGISTTYPE,SqlDbType.Int),
                    new SqlParameter(PARAM_DATAFROM,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_REGISTDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_WORKTIME,SqlDbType.Int),
                    new SqlParameter(PARAM_STARTDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_ENDITDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_REGISTSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_BRANCHNO,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_ADDOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_MODOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MODDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_VALIDITYSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_COMMENTS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = info.sRFlowNo;
                paras[1].Value = info.iDataFlag;
                paras[2].Value = info.sTicketNo;
                paras[3].Value = info.sRUserNo;
                paras[4].Value = info.sCnName;
                paras[5].Value = info.sEnName;
                paras[6].Value = info.iAge;
                paras[7].Value = info.iSex;
                paras[8].Value = info.sNation;
                paras[9].Value = info.iCardType;
                paras[10].Value = info.sIdCardNo;
                paras[11].Value = info.sRiCardNo;
                paras[12].Value = info.sTelphone;
                paras[13].Value = info.sAddress;
                paras[14].Value = info.sPostCode;
                paras[15].Value = info.sHeadPhoto;
                paras[16].Value = info.sSummary;
                paras[17].Value = info.sServiceNo;
                paras[18].Value = info.sStafferNo;
                paras[19].Value = info.iRegistType;
                paras[20].Value = info.sDataFrom;
                paras[21].Value = info.dRegistDate;
                paras[22].Value = info.iWorkTime;
                paras[23].Value = info.dStartDate;
                paras[24].Value = info.dEnditDate;
                paras[25].Value = info.iRegistState;
                paras[26].Value = info.sBranchNo;
                paras[27].Value = info.sAddOptor;
                paras[28].Value = info.dAddDate;
                paras[29].Value = info.sModOptor;
                paras[30].Value = info.dModDate;
                paras[31].Value = info.iValidityState;
                paras[32].Value = info.sComments;
                paras[33].Value = info.sAppCode;

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

        public int UpdateRecord(ViewRegistFlows info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_RFLOWNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_DATAFLAG,SqlDbType.Int),
                    new SqlParameter(PARAM_TICKETNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_RUSERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_CNNAME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_ENNAME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_AGE,SqlDbType.Int),
                    new SqlParameter(PARAM_SEX,SqlDbType.Int),
                    new SqlParameter(PARAM_NATION,SqlDbType.NVarChar,10),
                    new SqlParameter(PARAM_CARDTYPE,SqlDbType.Int),
                    new SqlParameter(PARAM_IDCARDNO,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_RICARDNO,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_TELPHONE,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_ADDRESS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_POSTCODE,SqlDbType.NVarChar,10),
                    new SqlParameter(PARAM_HEADPHOTO,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_SUMMARY,SqlDbType.NVarChar,2000),
                    new SqlParameter(PARAM_SERVICENO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_STAFFERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_REGISTTYPE,SqlDbType.Int),
                    new SqlParameter(PARAM_DATAFROM,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_REGISTDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_WORKTIME,SqlDbType.Int),
                    new SqlParameter(PARAM_STARTDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_ENDITDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_REGISTSTATE,SqlDbType.Int),
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
                paras[0].Value = info.sRFlowNo;
                paras[1].Value = info.iDataFlag;
                paras[2].Value = info.sTicketNo;
                paras[3].Value = info.sRUserNo;
                paras[4].Value = info.sCnName;
                paras[5].Value = info.sEnName;
                paras[6].Value = info.iAge;
                paras[7].Value = info.iSex;
                paras[8].Value = info.sNation;
                paras[9].Value = info.iCardType;
                paras[10].Value = info.sIdCardNo;
                paras[11].Value = info.sRiCardNo;
                paras[12].Value = info.sTelphone;
                paras[13].Value = info.sAddress;
                paras[14].Value = info.sPostCode;
                paras[15].Value = info.sHeadPhoto;
                paras[16].Value = info.sSummary;
                paras[17].Value = info.sServiceNo;
                paras[18].Value = info.sStafferNo;
                paras[19].Value = info.iRegistType;
                paras[20].Value = info.sDataFrom;
                paras[21].Value = info.dRegistDate;
                paras[22].Value = info.iWorkTime;
                paras[23].Value = info.dStartDate;
                paras[24].Value = info.dEnditDate;
                paras[25].Value = info.iRegistState;
                paras[26].Value = info.sBranchNo;
                paras[27].Value = info.sAddOptor;
                paras[28].Value = info.dAddDate;
                paras[29].Value = info.sModOptor;
                paras[30].Value = info.dModDate;
                paras[31].Value = info.iValidityState;
                paras[32].Value = info.sComments;
                paras[33].Value = info.sAppCode;
                paras[34].Value = StringHelper.ConvertToBytes(info.sVersion);

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
                    new SqlParameter(PARAM_RFLOWNO,SqlDbType.NVarChar,20),
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
                    new SqlParameter(PARAM_RFLOWNO,SqlDbType.NVarChar,20),
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
        public ViewRegistFlowsCollections GetRecords_Paging(SqlModel s_model)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            ViewRegistFlowsCollections infos = null;
            ViewRegistFlows info = null;

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
                    infos = new ViewRegistFlowsCollections();
                    while (reader.Read())
                    {
                        info = new ViewRegistFlows();
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
        internal static void PutObjectProperty(ViewRegistFlows obj_info, SqlDataReader reader)
        {
            obj_info.iID= int.Parse(reader["ID"].ToString());
            obj_info.sRFlowNo= reader["RFlowNo"].ToString();
            obj_info.iDataFlag= int.Parse(reader["DataFlag"].ToString());
            obj_info.sTicketNo= reader["TicketNo"].ToString();
            obj_info.sRUserNo= reader["RUserNo"].ToString();
            obj_info.sCnName= reader["CnName"].ToString();
            obj_info.sEnName= reader["EnName"].ToString();
            obj_info.iAge= int.Parse(reader["Age"].ToString());
            obj_info.iSex= int.Parse(reader["Sex"].ToString());
            obj_info.sNation= reader["Nation"].ToString();
            obj_info.iCardType= int.Parse(reader["CardType"].ToString());
            obj_info.sIdCardNo= reader["IdCardNo"].ToString();
            obj_info.sRiCardNo= reader["RiCardNo"].ToString();
            obj_info.sTelphone= reader["Telphone"].ToString();
            obj_info.sAddress= reader["Address"].ToString();
            obj_info.sPostCode= reader["PostCode"].ToString();
            obj_info.sHeadPhoto= reader["HeadPhoto"].ToString();
            obj_info.sSummary= reader["Summary"].ToString();
            obj_info.sServiceNo= reader["ServiceNo"].ToString();
            obj_info.sStafferNo= reader["StafferNo"].ToString();
            obj_info.iRegistType= int.Parse(reader["RegistType"].ToString());
            obj_info.sDataFrom= reader["DataFrom"].ToString();
            obj_info.dRegistDate= DateTime.Parse(reader["RegistDate"].ToString());
            obj_info.iWorkTime= int.Parse(reader["WorkTime"].ToString());
            obj_info.dStartDate= DateTime.Parse(reader["StartDate"].ToString());
            obj_info.dEnditDate= DateTime.Parse(reader["EnditDate"].ToString());
            obj_info.iRegistState= int.Parse(reader["RegistState"].ToString());
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

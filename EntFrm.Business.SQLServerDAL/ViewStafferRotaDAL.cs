using EntFrm.Business.IDAL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using System;
using System.Data;
using System.Data.SqlClient;

namespace EntFrm.Business.SQLServerDAL
{
  public class ViewStafferRotaDAL: IViewStafferRota
  {
        #region sql
        private const string SQL_GET_ALL_RECORDS = @"Select *  From ViewStafferRota Where AppCode like @AppCode And ValidityState=1";
        private const string SQL_GET_RECORDS_BY_NO = @"Select * From ViewStafferRota Where   AppCode like @AppCode And   ValidityState=1 And RotaNo=@RotaNo";
        private const string SQL_GET_NAME_BY_NO = @"Select Name From ViewStafferRota Where   AppCode like @AppCode And   ValidityState=1 And RotaNo=@RotaNo";
        private const string SQL_ADD_RECORD = @"Insert into ViewStafferRota
                                              (RotaNo,StafferNo,StafferName,LoginId,Password,CounterNo,OrganizNo,OrganizName,StarLevel,HeadPhoto,Summary,RotaType,StartDate,EnditDate,WeekDay1,WeekDay2,WeekDay3,WeekDay4,WeekDay5,WeekDay6,WeekDay7,RotaFormat,RegisteFees,BranchNo,AddOptor,AddDate,ModOptor,ModDate,ValidityState,Comments,AppCode)
                                              values(@RotaNo,@StafferNo,@StafferName,@LoginId,@Password,@CounterNo,@OrganizNo,@OrganizName,@StarLevel,@HeadPhoto,@Summary,@RotaType,@StartDate,@EnditDate,@WeekDay1,@WeekDay2,@WeekDay3,@WeekDay4,@WeekDay5,@WeekDay6,@WeekDay7,@RotaFormat,@RegisteFees,@BranchNo,@AddOptor,@AddDate,@ModOptor,@ModDate,@ValidityState,@Comments,@AppCode)";
        private const string SQL_UPDATE_RECORD = @"Update ViewStafferRota set
                                                 RotaNo=@RotaNo,StafferNo=@StafferNo,StafferName=@StafferName,LoginId=@LoginId,Password=@Password,CounterNo=@CounterNo,OrganizNo=@OrganizNo,OrganizName=@OrganizName,StarLevel=@StarLevel,HeadPhoto=@HeadPhoto,Summary=@Summary,RotaType=@RotaType,StartDate=@StartDate,EnditDate=@EnditDate,WeekDay1=@WeekDay1,WeekDay2=@WeekDay2,WeekDay3=@WeekDay3,WeekDay4=@WeekDay4,WeekDay5=@WeekDay5,WeekDay6=@WeekDay6,WeekDay7=@WeekDay7,RotaFormat=@RotaFormat,RegisteFees=@RegisteFees,BranchNo=@BranchNo,AddOptor=@AddOptor,AddDate=@AddDate,ModOptor=@ModOptor,ModDate=@ModDate,ValidityState=@ValidityState,Comments=@Comments,AppCode=@AppCode 
                                                 Where  AppCode like @AppCode And   ValidityState=1 And RotaNo=@RotaNo  And Version=@Version";
        private const string SQL_HARD_DELETE_RECORD = @"Delete From ViewStafferRota Where   AppCode like @AppCode And   RotaNo=@RotaNo ";
        private const string SQL_SOFT_DELETE_RECORD = @"Update ViewStafferRota set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 And RotaNo=@RotaNo";
        private const string SQL_HARD_DELETE_BY_CONDTION = @"Delete From ViewStafferRota Where   AppCode like @AppCode ";
        private const string SQL_SOFT_DELETE_BY_CONDTION = @"Update ViewStafferRota set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 ";
        private const string SQL_GET_RECORDS_BY_CLASSNO = @"Select * From ViewStafferRota Where    AppCode like @AppCode And   ValidityState=1 And ClassNo=@ClassNo";
        private const string SQL_GET_COUNT_BY_CONDITION = @"Select Count(*) From ViewStafferRota Where   AppCode like @AppCode  And   ValidityState=1 ";
        #endregion

        #region param
        private const string PARAM_ID = "@ID";
        private const string PARAM_ROTANO = "@RotaNo";
        private const string PARAM_STAFFERNO = "@StafferNo";
        private const string PARAM_STAFFERNAME = "@StafferName";
        private const string PARAM_LOGINID = "@LoginId";
        private const string PARAM_PASSWORD = "@Password";
        private const string PARAM_COUNTERNO = "@CounterNo";
        private const string PARAM_ORGANIZNO = "@OrganizNo";
        private const string PARAM_ORGANIZNAME = "@OrganizName";
        private const string PARAM_STARLEVEL = "@StarLevel";
        private const string PARAM_HEADPHOTO = "@HeadPhoto";
        private const string PARAM_SUMMARY = "@Summary";
        private const string PARAM_ROTATYPE = "@RotaType";
        private const string PARAM_STARTDATE = "@StartDate";
        private const string PARAM_ENDITDATE = "@EnditDate";
        private const string PARAM_WEEKDAY1 = "@WeekDay1";
        private const string PARAM_WEEKDAY2 = "@WeekDay2";
        private const string PARAM_WEEKDAY3 = "@WeekDay3";
        private const string PARAM_WEEKDAY4 = "@WeekDay4";
        private const string PARAM_WEEKDAY5 = "@WeekDay5";
        private const string PARAM_WEEKDAY6 = "@WeekDay6";
        private const string PARAM_WEEKDAY7 = "@WeekDay7";
        private const string PARAM_ROTAFORMAT = "@RotaFormat";
        private const string PARAM_REGISTEFEES = "@RegisteFees";
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

        public ViewStafferRotaDAL(string sConnStr,string sAppCode)
        {
           this.connStr = sConnStr;
           this.appCode = sAppCode;
        }

        public ViewStafferRotaCollections GetAllRecords()
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            ViewStafferRotaCollections infos = null;
            ViewStafferRota info = null;

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
                    infos = new ViewStafferRotaCollections();
                    while (reader.Read())
                    {
                        info = new ViewStafferRota();
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

        public ViewStafferRotaCollections GetRecordsByClassNo(string sClassNo)
        {
            /*SqlConnection connection = null;
            SqlDataReader reader = null;
            ViewStafferRotaCollections infos = null;
            ViewStafferRota info = null;

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
                    infos = new ViewStafferRotaCollections();
                    while (reader.Read())
                    {
                        info = new ViewStafferRota();
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

        public ViewStafferRotaCollections GetRecordsByNo(string sNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            ViewStafferRotaCollections infos = null;
            ViewStafferRota info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_ROTANO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_NO,paras);

                if (reader.HasRows)
                {
                    infos = new ViewStafferRotaCollections();
                    while (reader.Read())
                    {
                        info = new ViewStafferRota();
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
                    new SqlParameter(PARAM_ROTANO,SqlDbType.NVarChar,20),
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

        public int AddNewRecord(ViewStafferRota info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_ROTANO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_STAFFERNO,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_STAFFERNAME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_LOGINID,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_PASSWORD,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_COUNTERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ORGANIZNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ORGANIZNAME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_STARLEVEL,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_HEADPHOTO,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_SUMMARY,SqlDbType.NVarChar,1073741823),
                    new SqlParameter(PARAM_ROTATYPE,SqlDbType.Int),
                    new SqlParameter(PARAM_STARTDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_ENDITDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_WEEKDAY1,SqlDbType.Int),
                    new SqlParameter(PARAM_WEEKDAY2,SqlDbType.Int),
                    new SqlParameter(PARAM_WEEKDAY3,SqlDbType.Int),
                    new SqlParameter(PARAM_WEEKDAY4,SqlDbType.Int),
                    new SqlParameter(PARAM_WEEKDAY5,SqlDbType.Int),
                    new SqlParameter(PARAM_WEEKDAY6,SqlDbType.Int),
                    new SqlParameter(PARAM_WEEKDAY7,SqlDbType.Int),
                    new SqlParameter(PARAM_ROTAFORMAT,SqlDbType.NVarChar,1073741823),
                    new SqlParameter(PARAM_REGISTEFEES,SqlDbType.Float),
                    new SqlParameter(PARAM_BRANCHNO,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_ADDOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_MODOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MODDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_VALIDITYSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_COMMENTS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = info.sRotaNo;
                paras[1].Value = info.sStafferNo;
                paras[2].Value = info.sStafferName;
                paras[3].Value = info.sLoginId;
                paras[4].Value = info.sPassword;
                paras[5].Value = info.sCounterNo;
                paras[6].Value = info.sOrganizNo;
                paras[7].Value = info.sOrganizName;
                paras[8].Value = info.sStarLevel;
                paras[9].Value = info.sHeadPhoto;
                paras[10].Value = info.sSummary;
                paras[11].Value = info.iRotaType;
                paras[12].Value = info.dStartDate;
                paras[13].Value = info.dEnditDate;
                paras[14].Value = info.iWeekDay1;
                paras[15].Value = info.iWeekDay2;
                paras[16].Value = info.iWeekDay3;
                paras[17].Value = info.iWeekDay4;
                paras[18].Value = info.iWeekDay5;
                paras[19].Value = info.iWeekDay6;
                paras[20].Value = info.iWeekDay7;
                paras[21].Value = info.sRotaFormat;
                paras[22].Value = info.dRegisteFees;
                paras[23].Value = info.sBranchNo;
                paras[24].Value = info.sAddOptor;
                paras[25].Value = info.dAddDate;
                paras[26].Value = info.sModOptor;
                paras[27].Value = info.dModDate;
                paras[28].Value = info.iValidityState;
                paras[29].Value = info.sComments;
                paras[30].Value = info.sAppCode;

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

        public int UpdateRecord(ViewStafferRota info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_ROTANO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_STAFFERNO,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_STAFFERNAME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_LOGINID,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_PASSWORD,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_COUNTERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ORGANIZNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ORGANIZNAME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_STARLEVEL,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_HEADPHOTO,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_SUMMARY,SqlDbType.NVarChar,1073741823),
                    new SqlParameter(PARAM_ROTATYPE,SqlDbType.Int),
                    new SqlParameter(PARAM_STARTDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_ENDITDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_WEEKDAY1,SqlDbType.Int),
                    new SqlParameter(PARAM_WEEKDAY2,SqlDbType.Int),
                    new SqlParameter(PARAM_WEEKDAY3,SqlDbType.Int),
                    new SqlParameter(PARAM_WEEKDAY4,SqlDbType.Int),
                    new SqlParameter(PARAM_WEEKDAY5,SqlDbType.Int),
                    new SqlParameter(PARAM_WEEKDAY6,SqlDbType.Int),
                    new SqlParameter(PARAM_WEEKDAY7,SqlDbType.Int),
                    new SqlParameter(PARAM_ROTAFORMAT,SqlDbType.NVarChar,1073741823),
                    new SqlParameter(PARAM_REGISTEFEES,SqlDbType.Float),
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
                paras[0].Value = info.sRotaNo;
                paras[1].Value = info.sStafferNo;
                paras[2].Value = info.sStafferName;
                paras[3].Value = info.sLoginId;
                paras[4].Value = info.sPassword;
                paras[5].Value = info.sCounterNo;
                paras[6].Value = info.sOrganizNo;
                paras[7].Value = info.sOrganizName;
                paras[8].Value = info.sStarLevel;
                paras[9].Value = info.sHeadPhoto;
                paras[10].Value = info.sSummary;
                paras[11].Value = info.iRotaType;
                paras[12].Value = info.dStartDate;
                paras[13].Value = info.dEnditDate;
                paras[14].Value = info.iWeekDay1;
                paras[15].Value = info.iWeekDay2;
                paras[16].Value = info.iWeekDay3;
                paras[17].Value = info.iWeekDay4;
                paras[18].Value = info.iWeekDay5;
                paras[19].Value = info.iWeekDay6;
                paras[20].Value = info.iWeekDay7;
                paras[21].Value = info.sRotaFormat;
                paras[22].Value = info.dRegisteFees;
                paras[23].Value = info.sBranchNo;
                paras[24].Value = info.sAddOptor;
                paras[25].Value = info.dAddDate;
                paras[26].Value = info.sModOptor;
                paras[27].Value = info.dModDate;
                paras[28].Value = info.iValidityState;
                paras[29].Value = info.sComments;
                paras[30].Value = info.sAppCode;
                paras[31].Value = StringHelper.ConvertToBytes(info.sVersion);

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
                    new SqlParameter(PARAM_ROTANO,SqlDbType.NVarChar,20),
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
                    new SqlParameter(PARAM_ROTANO,SqlDbType.NVarChar,20),
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
        public ViewStafferRotaCollections GetRecords_Paging(SqlModel s_model)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            ViewStafferRotaCollections infos = null;
            ViewStafferRota info = null;

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
                    infos = new ViewStafferRotaCollections();
                    while (reader.Read())
                    {
                        info = new ViewStafferRota();
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
        internal static void PutObjectProperty(ViewStafferRota obj_info, SqlDataReader reader)
        {
            obj_info.iID= int.Parse(reader["ID"].ToString());
            obj_info.sRotaNo= reader["RotaNo"].ToString();
            obj_info.sStafferNo= reader["StafferNo"].ToString();
            obj_info.sStafferName= reader["StafferName"].ToString();
            obj_info.sLoginId= reader["LoginId"].ToString();
            obj_info.sPassword= reader["Password"].ToString();
            obj_info.sCounterNo= reader["CounterNo"].ToString();
            obj_info.sOrganizNo= reader["OrganizNo"].ToString();
            obj_info.sOrganizName= reader["OrganizName"].ToString();
            obj_info.sStarLevel= reader["StarLevel"].ToString();
            obj_info.sHeadPhoto= reader["HeadPhoto"].ToString();
            obj_info.sSummary= reader["Summary"].ToString();
            obj_info.iRotaType= int.Parse(reader["RotaType"].ToString());
            obj_info.dStartDate= DateTime.Parse(reader["StartDate"].ToString());
            obj_info.dEnditDate= DateTime.Parse(reader["EnditDate"].ToString());
            obj_info.iWeekDay1= int.Parse(reader["WeekDay1"].ToString());
            obj_info.iWeekDay2= int.Parse(reader["WeekDay2"].ToString());
            obj_info.iWeekDay3= int.Parse(reader["WeekDay3"].ToString());
            obj_info.iWeekDay4= int.Parse(reader["WeekDay4"].ToString());
            obj_info.iWeekDay5= int.Parse(reader["WeekDay5"].ToString());
            obj_info.iWeekDay6= int.Parse(reader["WeekDay6"].ToString());
            obj_info.iWeekDay7= int.Parse(reader["WeekDay7"].ToString());
            obj_info.sRotaFormat= reader["RotaFormat"].ToString();
            obj_info.dRegisteFees= double.Parse(reader["RegisteFees"].ToString());
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

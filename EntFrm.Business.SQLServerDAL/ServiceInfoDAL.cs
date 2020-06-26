using EntFrm.Business.IDAL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using System;
using System.Data;
using System.Data.SqlClient;

namespace EntFrm.Business.SQLServerDAL
{
  public class ServiceInfoDAL: IServiceInfo
  {
        #region sql
        private const string SQL_GET_ALL_RECORDS = @"Select *  From ServiceInfo Where AppCode like @AppCode And ValidityState=1";
        private const string SQL_GET_RECORDS_BY_NO = @"Select * From ServiceInfo Where   AppCode like @AppCode And   ValidityState=1 And ServiceNo=@ServiceNo";
        private const string SQL_GET_NAME_BY_NO = @"Select ServiceName From ServiceInfo Where   AppCode like @AppCode And   ValidityState=1 And ServiceNo=@ServiceNo";
        private const string SQL_ADD_RECORD = @"Insert into ServiceInfo
                                              (ServiceNo,ServiceName,ServiceAlias,ServiceType,StartNum,EndNum,AlarmNum,DigitLength,WorkflowValue,WorkflowText,TicketButtonFmt,TicketStyleNo,AMLimit,AMStartTime,AMEndTime,AMTotal,PMLimit,PMStartTime,PMEndTime,PMTotal,WeekLimit,WeekDays,PrintPause,ParentNo,HaveChild,IsShowDialog,ShowDialogName,BranchNo,AddOptor,AddDate,ModOptor,ModDate,ValidityState,Comments,AppCode)
                                              values(@ServiceNo,@ServiceName,@ServiceAlias,@ServiceType,@StartNum,@EndNum,@AlarmNum,@DigitLength,@WorkflowValue,@WorkflowText,@TicketButtonFmt,@TicketStyleNo,@AMLimit,@AMStartTime,@AMEndTime,@AMTotal,@PMLimit,@PMStartTime,@PMEndTime,@PMTotal,@WeekLimit,@WeekDays,@PrintPause,@ParentNo,@HaveChild,@IsShowDialog,@ShowDialogName,@BranchNo,@AddOptor,@AddDate,@ModOptor,@ModDate,@ValidityState,@Comments,@AppCode)";
        private const string SQL_UPDATE_RECORD = @"Update ServiceInfo set
                                                 ServiceNo=@ServiceNo,ServiceName=@ServiceName,ServiceAlias=@ServiceAlias,ServiceType=@ServiceType,StartNum=@StartNum,EndNum=@EndNum,AlarmNum=@AlarmNum,DigitLength=@DigitLength,WorkflowValue=@WorkflowValue,WorkflowText=@WorkflowText,TicketButtonFmt=@TicketButtonFmt,TicketStyleNo=@TicketStyleNo,AMLimit=@AMLimit,AMStartTime=@AMStartTime,AMEndTime=@AMEndTime,AMTotal=@AMTotal,PMLimit=@PMLimit,PMStartTime=@PMStartTime,PMEndTime=@PMEndTime,PMTotal=@PMTotal,WeekLimit=@WeekLimit,WeekDays=@WeekDays,PrintPause=@PrintPause,ParentNo=@ParentNo,HaveChild=@HaveChild,IsShowDialog=@IsShowDialog,ShowDialogName=@ShowDialogName,BranchNo=@BranchNo,AddOptor=@AddOptor,AddDate=@AddDate,ModOptor=@ModOptor,ModDate=@ModDate,ValidityState=@ValidityState,Comments=@Comments,AppCode=@AppCode 
                                                 Where  AppCode like @AppCode And   ValidityState=1 And ServiceNo=@ServiceNo  And Version=@Version";
        private const string SQL_HARD_DELETE_RECORD = @"Delete From ServiceInfo Where   AppCode like @AppCode And   ServiceNo=@ServiceNo ";
        private const string SQL_SOFT_DELETE_RECORD = @"Update ServiceInfo set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 And ServiceNo=@ServiceNo";
        private const string SQL_HARD_DELETE_BY_CONDTION = @"Delete From ServiceInfo Where   AppCode like @AppCode ";
        private const string SQL_SOFT_DELETE_BY_CONDTION = @"Update ServiceInfo set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 ";
        private const string SQL_GET_RECORDS_BY_CLASSNO = @"Select * From ServiceInfo Where    AppCode like @AppCode And   ValidityState=1 And ClassNo=@ClassNo";
        private const string SQL_GET_COUNT_BY_CONDITION = @"Select Count(*) From ServiceInfo Where   AppCode like @AppCode  And   ValidityState=1 ";
        #endregion

        #region param
        private const string PARAM_ID = "@ID";
        private const string PARAM_SERVICENO = "@ServiceNo";
        private const string PARAM_SERVICENAME = "@ServiceName";
        private const string PARAM_SERVICEALIAS = "@ServiceAlias";
        private const string PARAM_SERVICETYPE = "@ServiceType";
        private const string PARAM_STARTNUM = "@StartNum";
        private const string PARAM_ENDNUM = "@EndNum";
        private const string PARAM_ALARMNUM = "@AlarmNum";
        private const string PARAM_DIGITLENGTH = "@DigitLength";
        private const string PARAM_WORKFLOWVALUE = "@WorkflowValue";
        private const string PARAM_WORKFLOWTEXT = "@WorkflowText";
        private const string PARAM_TICKETBUTTONFMT = "@TicketButtonFmt";
        private const string PARAM_TICKETSTYLENO = "@TicketStyleNo";
        private const string PARAM_AMLIMIT = "@AMLimit";
        private const string PARAM_AMSTARTTIME = "@AMStartTime";
        private const string PARAM_AMENDTIME = "@AMEndTime";
        private const string PARAM_AMTOTAL = "@AMTotal";
        private const string PARAM_PMLIMIT = "@PMLimit";
        private const string PARAM_PMSTARTTIME = "@PMStartTime";
        private const string PARAM_PMENDTIME = "@PMEndTime";
        private const string PARAM_PMTOTAL = "@PMTotal";
        private const string PARAM_WEEKLIMIT = "@WeekLimit";
        private const string PARAM_WEEKDAYS = "@WeekDays";
        private const string PARAM_PRINTPAUSE = "@PrintPause";
        private const string PARAM_PARENTNO = "@ParentNo";
        private const string PARAM_HAVECHILD = "@HaveChild";
        private const string PARAM_ISSHOWDIALOG = "@IsShowDialog";
        private const string PARAM_SHOWDIALOGNAME = "@ShowDialogName";
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

        public ServiceInfoDAL(string sConnStr,string sAppCode)
        {
           this.connStr = sConnStr;
           this.appCode = sAppCode;
        }

        public ServiceInfoCollections GetAllRecords()
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            ServiceInfoCollections infos = null;
            ServiceInfo info = null;

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
                    infos = new ServiceInfoCollections();
                    while (reader.Read())
                    {
                        info = new ServiceInfo();
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

        public ServiceInfoCollections GetRecordsByClassNo(string sClassNo)
        {
            /*SqlConnection connection = null;
            SqlDataReader reader = null;
            ServiceInfoCollections infos = null;
            ServiceInfo info = null;

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
                    infos = new ServiceInfoCollections();
                    while (reader.Read())
                    {
                        info = new ServiceInfo();
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

        public ServiceInfoCollections GetRecordsByNo(string sNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            ServiceInfoCollections infos = null;
            ServiceInfo info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_SERVICENO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_NO,paras);

                if (reader.HasRows)
                {
                    infos = new ServiceInfoCollections();
                    while (reader.Read())
                    {
                        info = new ServiceInfo();
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
                    new SqlParameter(PARAM_SERVICENO,SqlDbType.NVarChar,20),
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

        public int AddNewRecord(ServiceInfo info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_SERVICENO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_SERVICENAME,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_SERVICEALIAS,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_SERVICETYPE,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_STARTNUM,SqlDbType.Int),
                    new SqlParameter(PARAM_ENDNUM,SqlDbType.Int),
                    new SqlParameter(PARAM_ALARMNUM,SqlDbType.Int),
                    new SqlParameter(PARAM_DIGITLENGTH,SqlDbType.Int),
                    new SqlParameter(PARAM_WORKFLOWVALUE,SqlDbType.NVarChar,1073741823),
                    new SqlParameter(PARAM_WORKFLOWTEXT,SqlDbType.NVarChar,1073741823),
                    new SqlParameter(PARAM_TICKETBUTTONFMT,SqlDbType.NVarChar,1073741823),
                    new SqlParameter(PARAM_TICKETSTYLENO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_AMLIMIT,SqlDbType.Int),
                    new SqlParameter(PARAM_AMSTARTTIME,SqlDbType.DateTime),
                    new SqlParameter(PARAM_AMENDTIME,SqlDbType.DateTime),
                    new SqlParameter(PARAM_AMTOTAL,SqlDbType.Int),
                    new SqlParameter(PARAM_PMLIMIT,SqlDbType.Int),
                    new SqlParameter(PARAM_PMSTARTTIME,SqlDbType.DateTime),
                    new SqlParameter(PARAM_PMENDTIME,SqlDbType.DateTime),
                    new SqlParameter(PARAM_PMTOTAL,SqlDbType.Int),
                    new SqlParameter(PARAM_WEEKLIMIT,SqlDbType.Int),
                    new SqlParameter(PARAM_WEEKDAYS,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_PRINTPAUSE,SqlDbType.Int),
                    new SqlParameter(PARAM_PARENTNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_HAVECHILD,SqlDbType.Int),
                    new SqlParameter(PARAM_ISSHOWDIALOG,SqlDbType.Int),
                    new SqlParameter(PARAM_SHOWDIALOGNAME,SqlDbType.NVarChar,250),
                    new SqlParameter(PARAM_BRANCHNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_MODOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MODDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_VALIDITYSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_COMMENTS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = info.sServiceNo;
                paras[1].Value = info.sServiceName;
                paras[2].Value = info.sServiceAlias;
                paras[3].Value = info.sServiceType;
                paras[4].Value = info.iStartNum;
                paras[5].Value = info.iEndNum;
                paras[6].Value = info.iAlarmNum;
                paras[7].Value = info.iDigitLength;
                paras[8].Value = info.sWorkflowValue;
                paras[9].Value = info.sWorkflowText;
                paras[10].Value = info.sTicketButtonFmt;
                paras[11].Value = info.sTicketStyleNo;
                paras[12].Value = info.iAMLimit;
                paras[13].Value = info.dAMStartTime;
                paras[14].Value = info.dAMEndTime;
                paras[15].Value = info.iAMTotal;
                paras[16].Value = info.iPMLimit;
                paras[17].Value = info.dPMStartTime;
                paras[18].Value = info.dPMEndTime;
                paras[19].Value = info.iPMTotal;
                paras[20].Value = info.iWeekLimit;
                paras[21].Value = info.sWeekDays;
                paras[22].Value = info.iPrintPause;
                paras[23].Value = info.sParentNo;
                paras[24].Value = info.iHaveChild;
                paras[25].Value = info.iIsShowDialog;
                paras[26].Value = info.sShowDialogName;
                paras[27].Value = info.sBranchNo;
                paras[28].Value = info.sAddOptor;
                paras[29].Value = info.dAddDate;
                paras[30].Value = info.sModOptor;
                paras[31].Value = info.dModDate;
                paras[32].Value = info.iValidityState;
                paras[33].Value = info.sComments;
                paras[34].Value = info.sAppCode;

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

        public int UpdateRecord(ServiceInfo info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_SERVICENO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_SERVICENAME,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_SERVICEALIAS,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_SERVICETYPE,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_STARTNUM,SqlDbType.Int),
                    new SqlParameter(PARAM_ENDNUM,SqlDbType.Int),
                    new SqlParameter(PARAM_ALARMNUM,SqlDbType.Int),
                    new SqlParameter(PARAM_DIGITLENGTH,SqlDbType.Int),
                    new SqlParameter(PARAM_WORKFLOWVALUE,SqlDbType.NVarChar,1073741823),
                    new SqlParameter(PARAM_WORKFLOWTEXT,SqlDbType.NVarChar,1073741823),
                    new SqlParameter(PARAM_TICKETBUTTONFMT,SqlDbType.NVarChar,1073741823),
                    new SqlParameter(PARAM_TICKETSTYLENO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_AMLIMIT,SqlDbType.Int),
                    new SqlParameter(PARAM_AMSTARTTIME,SqlDbType.DateTime),
                    new SqlParameter(PARAM_AMENDTIME,SqlDbType.DateTime),
                    new SqlParameter(PARAM_AMTOTAL,SqlDbType.Int),
                    new SqlParameter(PARAM_PMLIMIT,SqlDbType.Int),
                    new SqlParameter(PARAM_PMSTARTTIME,SqlDbType.DateTime),
                    new SqlParameter(PARAM_PMENDTIME,SqlDbType.DateTime),
                    new SqlParameter(PARAM_PMTOTAL,SqlDbType.Int),
                    new SqlParameter(PARAM_WEEKLIMIT,SqlDbType.Int),
                    new SqlParameter(PARAM_WEEKDAYS,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_PRINTPAUSE,SqlDbType.Int),
                    new SqlParameter(PARAM_PARENTNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_HAVECHILD,SqlDbType.Int),
                    new SqlParameter(PARAM_ISSHOWDIALOG,SqlDbType.Int),
                    new SqlParameter(PARAM_SHOWDIALOGNAME,SqlDbType.NVarChar,250),
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
                paras[0].Value = info.sServiceNo;
                paras[1].Value = info.sServiceName;
                paras[2].Value = info.sServiceAlias;
                paras[3].Value = info.sServiceType;
                paras[4].Value = info.iStartNum;
                paras[5].Value = info.iEndNum;
                paras[6].Value = info.iAlarmNum;
                paras[7].Value = info.iDigitLength;
                paras[8].Value = info.sWorkflowValue;
                paras[9].Value = info.sWorkflowText;
                paras[10].Value = info.sTicketButtonFmt;
                paras[11].Value = info.sTicketStyleNo;
                paras[12].Value = info.iAMLimit;
                paras[13].Value = info.dAMStartTime;
                paras[14].Value = info.dAMEndTime;
                paras[15].Value = info.iAMTotal;
                paras[16].Value = info.iPMLimit;
                paras[17].Value = info.dPMStartTime;
                paras[18].Value = info.dPMEndTime;
                paras[19].Value = info.iPMTotal;
                paras[20].Value = info.iWeekLimit;
                paras[21].Value = info.sWeekDays;
                paras[22].Value = info.iPrintPause;
                paras[23].Value = info.sParentNo;
                paras[24].Value = info.iHaveChild;
                paras[25].Value = info.iIsShowDialog;
                paras[26].Value = info.sShowDialogName;
                paras[27].Value = info.sBranchNo;
                paras[28].Value = info.sAddOptor;
                paras[29].Value = info.dAddDate;
                paras[30].Value = info.sModOptor;
                paras[31].Value = info.dModDate;
                paras[32].Value = info.iValidityState;
                paras[33].Value = info.sComments;
                paras[34].Value = info.sAppCode;
                paras[35].Value = StringHelper.ConvertToBytes(info.sVersion);

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
                    new SqlParameter(PARAM_SERVICENO,SqlDbType.NVarChar,20),
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
                    new SqlParameter(PARAM_SERVICENO,SqlDbType.NVarChar,20),
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
        public ServiceInfoCollections GetRecords_Paging(SqlModel s_model)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            ServiceInfoCollections infos = null;
            ServiceInfo info = null;

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
                    infos = new ServiceInfoCollections();
                    while (reader.Read())
                    {
                        info = new ServiceInfo();
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
        internal static void PutObjectProperty(ServiceInfo obj_info, SqlDataReader reader)
        {
            obj_info.iID= int.Parse(reader["ID"].ToString());
            obj_info.sServiceNo= reader["ServiceNo"].ToString();
            obj_info.sServiceName= reader["ServiceName"].ToString();
            obj_info.sServiceAlias= reader["ServiceAlias"].ToString();
            obj_info.sServiceType= reader["ServiceType"].ToString();
            obj_info.iStartNum= int.Parse(reader["StartNum"].ToString());
            obj_info.iEndNum= int.Parse(reader["EndNum"].ToString());
            obj_info.iAlarmNum= int.Parse(reader["AlarmNum"].ToString());
            obj_info.iDigitLength= int.Parse(reader["DigitLength"].ToString());
            obj_info.sWorkflowValue= reader["WorkflowValue"].ToString();
            obj_info.sWorkflowText= reader["WorkflowText"].ToString();
            obj_info.sTicketButtonFmt= reader["TicketButtonFmt"].ToString();
            obj_info.sTicketStyleNo= reader["TicketStyleNo"].ToString();
            obj_info.iAMLimit= int.Parse(reader["AMLimit"].ToString());
            obj_info.dAMStartTime= DateTime.Parse(reader["AMStartTime"].ToString());
            obj_info.dAMEndTime= DateTime.Parse(reader["AMEndTime"].ToString());
            obj_info.iAMTotal= int.Parse(reader["AMTotal"].ToString());
            obj_info.iPMLimit= int.Parse(reader["PMLimit"].ToString());
            obj_info.dPMStartTime= DateTime.Parse(reader["PMStartTime"].ToString());
            obj_info.dPMEndTime= DateTime.Parse(reader["PMEndTime"].ToString());
            obj_info.iPMTotal= int.Parse(reader["PMTotal"].ToString());
            obj_info.iWeekLimit= int.Parse(reader["WeekLimit"].ToString());
            obj_info.sWeekDays= reader["WeekDays"].ToString();
            obj_info.iPrintPause= int.Parse(reader["PrintPause"].ToString());
            obj_info.sParentNo= reader["ParentNo"].ToString();
            obj_info.iHaveChild= int.Parse(reader["HaveChild"].ToString());
            obj_info.iIsShowDialog= int.Parse(reader["IsShowDialog"].ToString());
            obj_info.sShowDialogName= reader["ShowDialogName"].ToString();
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using EntFrm.Framework.Utility;
using EntFrm.System.Model;
using EntFrm.System.Model.Collections;
using EntFrm.System.IDAL;

namespace EntFrm.Business.SQLServerDAL
{
  public class MaterialIOMasterDAL: IMaterialIOMaster
  {
        #region sql
        private const string SQL_GET_ALL_RECORDS = @"Select *  From MaterialIOMaster Where AppCode like @AppCode And ValidityState=1";
        private const string SQL_GET_RECORDS_BY_NO = @"Select * From MaterialIOMaster Where   AppCode like @AppCode And   ValidityState=1 And MatListNo=@MatListNo";
        private const string SQL_GET_NAME_BY_NO = @"Select Name From MaterialIOMaster Where   AppCode like @AppCode And   ValidityState=1 And MatListNo=@MatListNo";
        private const string SQL_ADD_RECORD = @"Insert into MaterialIOMaster
                                              (MatListNo,MatListType,ProviderNo,MatIOTime,MatIONum,Auditor,Approver,Deliverer,Recipient,Purposer,Sender,AddOptor,AddDate,ModOptor,ModDate,ValidityState,Comments,AppCode)
                                              values(@MatListNo,@MatListType,@ProviderNo,@MatIOTime,@MatIONum,@Auditor,@Approver,@Deliverer,@Recipient,@Purposer,@Sender,@AddOptor,@AddDate,@ModOptor,@ModDate,@ValidityState,@Comments,@AppCode)";
        private const string SQL_UPDATE_RECORD = @"Update MaterialIOMaster set
                                                 MatListNo=@MatListNo,MatListType=@MatListType,ProviderNo=@ProviderNo,MatIOTime=@MatIOTime,MatIONum=@MatIONum,Auditor=@Auditor,Approver=@Approver,Deliverer=@Deliverer,Recipient=@Recipient,Purposer=@Purposer,Sender=@Sender,AddOptor=@AddOptor,AddDate=@AddDate,ModOptor=@ModOptor,ModDate=@ModDate,ValidityState=@ValidityState,Comments=@Comments,AppCode=@AppCode 
                                                 Where  AppCode like @AppCode And   ValidityState=1 And MatListNo=@MatListNo  And Version=@Version";
        private const string SQL_HARD_DELETE_RECORD = @"Delete From MaterialIOMaster Where   AppCode like @AppCode And   MatListNo=@MatListNo ";
        private const string SQL_SOFT_DELETE_RECORD = @"Update MaterialIOMaster set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 And MatListNo=@MatListNo";
        private const string SQL_GET_RECORDS_BY_CLASSNO = @"Select * From MaterialIOMaster Where    AppCode like @AppCode And   ValidityState=1 And ClassNo=@ClassNo";
        private const string SQL_GET_COUNT_BY_CONDITION = @"Select Count(*) From MaterialIOMaster Where   AppCode like @AppCode  And   ValidityState=1 ";
        #endregion

        #region param
        private const string PARAM_ID = "@ID";
        private const string PARAM_MATLISTNO = "@MatListNo";
        private const string PARAM_MATLISTTYPE = "@MatListType";
        private const string PARAM_PROVIDERNO = "@ProviderNo";
        private const string PARAM_MATIOTIME = "@MatIOTime";
        private const string PARAM_MATIONUM = "@MatIONum";
        private const string PARAM_AUDITOR = "@Auditor";
        private const string PARAM_APPROVER = "@Approver";
        private const string PARAM_DELIVERER = "@Deliverer";
        private const string PARAM_RECIPIENT = "@Recipient";
        private const string PARAM_PURPOSER = "@Purposer";
        private const string PARAM_SENDER = "@Sender";
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

        public MaterialIOMasterDAL(string sConnStr,string sAppCode)
        {
           this.connStr = sConnStr;
           this.appCode = sAppCode;
        }

        public MaterialIOMasterCollections GetAllRecords()
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            MaterialIOMasterCollections infos = null;
            MaterialIOMaster info = null;

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
                    infos = new MaterialIOMasterCollections();
                    while (reader.Read())
                    {
                        info = new MaterialIOMaster();
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

        public MaterialIOMasterCollections GetRecordsByClassNo(string sClassNo)
        {
            /*SqlConnection connection = null;
            SqlDataReader reader = null;
            MaterialIOMasterCollections infos = null;
            MaterialIOMaster info = null;

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
                    infos = new MaterialIOMasterCollections();
                    while (reader.Read())
                    {
                        info = new MaterialIOMaster();
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

        public MaterialIOMasterCollections GetRecordsByNo(string sNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            MaterialIOMasterCollections infos = null;
            MaterialIOMaster info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_MATLISTNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_NO,paras);

                if (reader.HasRows)
                {
                    infos = new MaterialIOMasterCollections();
                    while (reader.Read())
                    {
                        info = new MaterialIOMaster();
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
                    new SqlParameter(PARAM_MATLISTNO,SqlDbType.NVarChar,20),
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

        public int AddRecord(MaterialIOMaster info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_MATLISTNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MATLISTTYPE,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_PROVIDERNO,SqlDbType.NVarChar,10),
                    new SqlParameter(PARAM_MATIOTIME,SqlDbType.DateTime),
                    new SqlParameter(PARAM_MATIONUM,SqlDbType.Int),
                    new SqlParameter(PARAM_AUDITOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPROVER,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_DELIVERER,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_RECIPIENT,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_PURPOSER,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_SENDER,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_MODOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MODDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_VALIDITYSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_COMMENTS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = info.sMatListNo;
                paras[1].Value = info.sMatListType;
                paras[2].Value = info.sProviderNo;
                paras[3].Value = info.dMatIOTime;
                paras[4].Value = info.iMatIONum;
                paras[5].Value = info.sAuditor;
                paras[6].Value = info.sApprover;
                paras[7].Value = info.sDeliverer;
                paras[8].Value = info.sRecipient;
                paras[9].Value = info.sPurposer;
                paras[10].Value = info.sSender;
                paras[11].Value = info.sAddOptor;
                paras[12].Value = info.dAddDate;
                paras[13].Value = info.sModOptor;
                paras[14].Value = info.dModDate;
                paras[15].Value = info.iValidityState;
                paras[16].Value = info.sComments;
                paras[17].Value = info.sAppCode;

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

        public int UpdateRecord(MaterialIOMaster info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_MATLISTNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MATLISTTYPE,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_PROVIDERNO,SqlDbType.NVarChar,10),
                    new SqlParameter(PARAM_MATIOTIME,SqlDbType.DateTime),
                    new SqlParameter(PARAM_MATIONUM,SqlDbType.Int),
                    new SqlParameter(PARAM_AUDITOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPROVER,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_DELIVERER,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_RECIPIENT,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_PURPOSER,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_SENDER,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_MODOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MODDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_VALIDITYSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_COMMENTS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_VERSION,SqlDbType.Timestamp)
                };
                paras[0].Value = info.sMatListNo;
                paras[1].Value = info.sMatListType;
                paras[2].Value = info.sProviderNo;
                paras[3].Value = info.dMatIOTime;
                paras[4].Value = info.iMatIONum;
                paras[5].Value = info.sAuditor;
                paras[6].Value = info.sApprover;
                paras[7].Value = info.sDeliverer;
                paras[8].Value = info.sRecipient;
                paras[9].Value = info.sPurposer;
                paras[10].Value = info.sSender;
                paras[11].Value = info.sAddOptor;
                paras[12].Value = info.dAddDate;
                paras[13].Value = info.sModOptor;
                paras[14].Value = info.dModDate;
                paras[15].Value = info.iValidityState;
                paras[16].Value = info.sComments;
                paras[17].Value = info.sAppCode;
                paras[18].Value = StringUtil.ConvertToBytes(info.sVersion);

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
                    new SqlParameter(PARAM_MATLISTNO,SqlDbType.NVarChar,20),
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
                    new SqlParameter(PARAM_MATLISTNO,SqlDbType.NVarChar,20),
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

        public MaterialIOMasterCollections GetRecords_Paging(SqlModel s_model)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            MaterialIOMasterCollections infos = null;
            MaterialIOMaster info = null;

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
                    infos = new MaterialIOMasterCollections();
                    while (reader.Read())
                    {
                        info = new MaterialIOMaster();
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
        internal static void PutObjectProperty(MaterialIOMaster obj_info, SqlDataReader reader)
        {
            obj_info.iID= int.Parse(reader["ID"].ToString());
            obj_info.sMatListNo= reader["MatListNo"].ToString();
            obj_info.sMatListType= reader["MatListType"].ToString();
            obj_info.sProviderNo= reader["ProviderNo"].ToString();
            obj_info.dMatIOTime= DateTime.Parse(reader["MatIOTime"].ToString());
            obj_info.iMatIONum= int.Parse(reader["MatIONum"].ToString());
            obj_info.sAuditor= reader["Auditor"].ToString();
            obj_info.sApprover= reader["Approver"].ToString();
            obj_info.sDeliverer= reader["Deliverer"].ToString();
            obj_info.sRecipient= reader["Recipient"].ToString();
            obj_info.sPurposer= reader["Purposer"].ToString();
            obj_info.sSender= reader["Sender"].ToString();
            obj_info.sAddOptor= reader["AddOptor"].ToString();
            obj_info.dAddDate= DateTime.Parse(reader["AddDate"].ToString());
            obj_info.sModOptor= reader["ModOptor"].ToString();
            obj_info.dModDate= DateTime.Parse(reader["ModDate"].ToString());
            obj_info.iValidityState= int.Parse(reader["ValidityState"].ToString());
            obj_info.sComments= reader["Comments"].ToString();
            obj_info.sAppCode= reader["AppCode"].ToString();
            obj_info.sVersion= StringUtil.ConvertToString((byte[])reader["Version"].ToString());
        }
        #endregion
    }
}

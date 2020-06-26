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
  public class LessonTimesInfoDAL: ILessonTimesInfo
  {
        #region sql
        private const string SQL_GET_ALL_RECORDS = @"Select *  From LessonTimesInfo Where AppCode like @AppCode And ValidityState=1";
        private const string SQL_GET_RECORDS_BY_NO = @"Select * From LessonTimesInfo Where   AppCode like @AppCode And   ValidityState=1 And LessonNo=@LessonNo";
        private const string SQL_GET_NAME_BY_NO = @"Select Name From LessonTimesInfo Where   AppCode like @AppCode And   ValidityState=1 And LessonNo=@LessonNo";
        private const string SQL_ADD_RECORD = @"Insert into LessonTimesInfo
                                              (LessonNo,LessonName,ProfNo,LessonNum,BeginTime,EndTime,TheoryTime,ExperimentTime,LessonType,LsnAbstract,State,AddOptor,AddDate,ModOptor,ModDate,ValidityState,Comments,AppCode)
                                              values(@LessonNo,@LessonName,@ProfNo,@LessonNum,@BeginTime,@EndTime,@TheoryTime,@ExperimentTime,@LessonType,@LsnAbstract,@State,@AddOptor,@AddDate,@ModOptor,@ModDate,@ValidityState,@Comments,@AppCode)";
        private const string SQL_UPDATE_RECORD = @"Update LessonTimesInfo set
                                                 LessonNo=@LessonNo,LessonName=@LessonName,ProfNo=@ProfNo,LessonNum=@LessonNum,BeginTime=@BeginTime,EndTime=@EndTime,TheoryTime=@TheoryTime,ExperimentTime=@ExperimentTime,LessonType=@LessonType,LsnAbstract=@LsnAbstract,State=@State,AddOptor=@AddOptor,AddDate=@AddDate,ModOptor=@ModOptor,ModDate=@ModDate,ValidityState=@ValidityState,Comments=@Comments,AppCode=@AppCode 
                                                 Where  AppCode like @AppCode And   ValidityState=1 And LessonNo=@LessonNo  And Version=@Version";
        private const string SQL_HARD_DELETE_RECORD = @"Delete From LessonTimesInfo Where   AppCode like @AppCode And   LessonNo=@LessonNo ";
        private const string SQL_SOFT_DELETE_RECORD = @"Update LessonTimesInfo set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 And LessonNo=@LessonNo";
        private const string SQL_GET_RECORDS_BY_CLASSNO = @"Select * From LessonTimesInfo Where    AppCode like @AppCode And   ValidityState=1 And ClassNo=@ClassNo";
        private const string SQL_GET_COUNT_BY_CONDITION = @"Select Count(*) From LessonTimesInfo Where   AppCode like @AppCode  And   ValidityState=1 ";
        #endregion

        #region param
        private const string PARAM_ID = "@ID";
        private const string PARAM_LESSONNO = "@LessonNo";
        private const string PARAM_LESSONNAME = "@LessonName";
        private const string PARAM_PROFNO = "@ProfNo";
        private const string PARAM_LESSONNUM = "@LessonNum";
        private const string PARAM_BEGINTIME = "@BeginTime";
        private const string PARAM_ENDTIME = "@EndTime";
        private const string PARAM_THEORYTIME = "@TheoryTime";
        private const string PARAM_EXPERIMENTTIME = "@ExperimentTime";
        private const string PARAM_LESSONTYPE = "@LessonType";
        private const string PARAM_LSNABSTRACT = "@LsnAbstract";
        private const string PARAM_STATE = "@State";
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

        public LessonTimesInfoDAL(string sConnStr,string sAppCode)
        {
           this.connStr = sConnStr;
           this.appCode = sAppCode;
        }

        public LessonTimesInfoCollections GetAllRecords()
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            LessonTimesInfoCollections infos = null;
            LessonTimesInfo info = null;

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
                    infos = new LessonTimesInfoCollections();
                    while (reader.Read())
                    {
                        info = new LessonTimesInfo();
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

        public LessonTimesInfoCollections GetRecordsByClassNo(string sClassNo)
        {
            /*SqlConnection connection = null;
            SqlDataReader reader = null;
            LessonTimesInfoCollections infos = null;
            LessonTimesInfo info = null;

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
                    infos = new LessonTimesInfoCollections();
                    while (reader.Read())
                    {
                        info = new LessonTimesInfo();
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

        public LessonTimesInfoCollections GetRecordsByNo(string sNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            LessonTimesInfoCollections infos = null;
            LessonTimesInfo info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_LESSONNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_NO,paras);

                if (reader.HasRows)
                {
                    infos = new LessonTimesInfoCollections();
                    while (reader.Read())
                    {
                        info = new LessonTimesInfo();
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
                    new SqlParameter(PARAM_LESSONNO,SqlDbType.NVarChar,20),
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

        public int AddRecord(LessonTimesInfo info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_LESSONNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_LESSONNAME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_PROFNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_LESSONNUM,SqlDbType.Int),
                    new SqlParameter(PARAM_BEGINTIME,SqlDbType.DateTime),
                    new SqlParameter(PARAM_ENDTIME,SqlDbType.DateTime),
                    new SqlParameter(PARAM_THEORYTIME,SqlDbType.Int),
                    new SqlParameter(PARAM_EXPERIMENTTIME,SqlDbType.Int),
                    new SqlParameter(PARAM_LESSONTYPE,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_LSNABSTRACT,SqlDbType.NVarChar,1073741823),
                    new SqlParameter(PARAM_STATE,SqlDbType.Int),
                    new SqlParameter(PARAM_ADDOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_MODOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MODDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_VALIDITYSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_COMMENTS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = info.sLessonNo;
                paras[1].Value = info.sLessonName;
                paras[2].Value = info.sProfNo;
                paras[3].Value = info.iLessonNum;
                paras[4].Value = info.dBeginTime;
                paras[5].Value = info.dEndTime;
                paras[6].Value = info.iTheoryTime;
                paras[7].Value = info.iExperimentTime;
                paras[8].Value = info.sLessonType;
                paras[9].Value = info.sLsnAbstract;
                paras[10].Value = info.iState;
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

        public int UpdateRecord(LessonTimesInfo info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_LESSONNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_LESSONNAME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_PROFNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_LESSONNUM,SqlDbType.Int),
                    new SqlParameter(PARAM_BEGINTIME,SqlDbType.DateTime),
                    new SqlParameter(PARAM_ENDTIME,SqlDbType.DateTime),
                    new SqlParameter(PARAM_THEORYTIME,SqlDbType.Int),
                    new SqlParameter(PARAM_EXPERIMENTTIME,SqlDbType.Int),
                    new SqlParameter(PARAM_LESSONTYPE,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_LSNABSTRACT,SqlDbType.NVarChar,1073741823),
                    new SqlParameter(PARAM_STATE,SqlDbType.Int),
                    new SqlParameter(PARAM_ADDOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_MODOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MODDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_VALIDITYSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_COMMENTS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_VERSION,SqlDbType.Timestamp)
                };
                paras[0].Value = info.sLessonNo;
                paras[1].Value = info.sLessonName;
                paras[2].Value = info.sProfNo;
                paras[3].Value = info.iLessonNum;
                paras[4].Value = info.dBeginTime;
                paras[5].Value = info.dEndTime;
                paras[6].Value = info.iTheoryTime;
                paras[7].Value = info.iExperimentTime;
                paras[8].Value = info.sLessonType;
                paras[9].Value = info.sLsnAbstract;
                paras[10].Value = info.iState;
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
                    new SqlParameter(PARAM_LESSONNO,SqlDbType.NVarChar,20),
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
                    new SqlParameter(PARAM_LESSONNO,SqlDbType.NVarChar,20),
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

        public LessonTimesInfoCollections GetRecords_Paging(SqlModel s_model)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            LessonTimesInfoCollections infos = null;
            LessonTimesInfo info = null;

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
                    infos = new LessonTimesInfoCollections();
                    while (reader.Read())
                    {
                        info = new LessonTimesInfo();
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
        internal static void PutObjectProperty(LessonTimesInfo obj_info, SqlDataReader reader)
        {
            obj_info.iID= int.Parse(reader["ID"].ToString());
            obj_info.sLessonNo= reader["LessonNo"].ToString();
            obj_info.sLessonName= reader["LessonName"].ToString();
            obj_info.sProfNo= reader["ProfNo"].ToString();
            obj_info.iLessonNum= int.Parse(reader["LessonNum"].ToString());
            obj_info.dBeginTime= DateTime.Parse(reader["BeginTime"].ToString());
            obj_info.dEndTime= DateTime.Parse(reader["EndTime"].ToString());
            obj_info.iTheoryTime= int.Parse(reader["TheoryTime"].ToString());
            obj_info.iExperimentTime= int.Parse(reader["ExperimentTime"].ToString());
            obj_info.sLessonType= reader["LessonType"].ToString();
            obj_info.sLsnAbstract= reader["LsnAbstract"].ToString();
            obj_info.iState= int.Parse(reader["State"].ToString());
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

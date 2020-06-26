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
  public class MaterialInfoDAL: IMaterialInfo
  {
        #region sql
        private const string SQL_GET_ALL_RECORDS = @"Select *  From MaterialInfo Where AppCode like @AppCode And ValidityState=1";
        private const string SQL_GET_RECORDS_BY_NO = @"Select * From MaterialInfo Where   AppCode like @AppCode And   ValidityState=1 And MatNo=@MatNo";
        private const string SQL_GET_NAME_BY_NO = @"Select Name From MaterialInfo Where   AppCode like @AppCode And   ValidityState=1 And MatNo=@MatNo";
        private const string SQL_ADD_RECORD = @"Insert into MaterialInfo
                                              (MatNo,MatTypeNo,MatClassNo,ProviderNo,MatUniteNo,MatName,MatBarcode,MatGuid,MatSpec,MatPrice,MiniLimit,MaxiLimit,AddOptor,AddDate,ModOptor,ModDate,ValidityState,Comments,AppCode)
                                              values(@MatNo,@MatTypeNo,@MatClassNo,@ProviderNo,@MatUniteNo,@MatName,@MatBarcode,@MatGuid,@MatSpec,@MatPrice,@MiniLimit,@MaxiLimit,@AddOptor,@AddDate,@ModOptor,@ModDate,@ValidityState,@Comments,@AppCode)";
        private const string SQL_UPDATE_RECORD = @"Update MaterialInfo set
                                                 MatNo=@MatNo,MatTypeNo=@MatTypeNo,MatClassNo=@MatClassNo,ProviderNo=@ProviderNo,MatUniteNo=@MatUniteNo,MatName=@MatName,MatBarcode=@MatBarcode,MatGuid=@MatGuid,MatSpec=@MatSpec,MatPrice=@MatPrice,MiniLimit=@MiniLimit,MaxiLimit=@MaxiLimit,AddOptor=@AddOptor,AddDate=@AddDate,ModOptor=@ModOptor,ModDate=@ModDate,ValidityState=@ValidityState,Comments=@Comments,AppCode=@AppCode 
                                                 Where  AppCode like @AppCode And   ValidityState=1 And MatNo=@MatNo  And Version=@Version";
        private const string SQL_HARD_DELETE_RECORD = @"Delete From MaterialInfo Where   AppCode like @AppCode And   MatNo=@MatNo ";
        private const string SQL_SOFT_DELETE_RECORD = @"Update MaterialInfo set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 And MatNo=@MatNo";
        private const string SQL_GET_RECORDS_BY_CLASSNO = @"Select * From MaterialInfo Where    AppCode like @AppCode And   ValidityState=1 And ClassNo=@ClassNo";
        private const string SQL_GET_COUNT_BY_CONDITION = @"Select Count(*) From MaterialInfo Where   AppCode like @AppCode  And   ValidityState=1 ";
        #endregion

        #region param
        private const string PARAM_ID = "@ID";
        private const string PARAM_MATNO = "@MatNo";
        private const string PARAM_MATTYPENO = "@MatTypeNo";
        private const string PARAM_MATCLASSNO = "@MatClassNo";
        private const string PARAM_PROVIDERNO = "@ProviderNo";
        private const string PARAM_MATUNITENO = "@MatUniteNo";
        private const string PARAM_MATNAME = "@MatName";
        private const string PARAM_MATBARCODE = "@MatBarcode";
        private const string PARAM_MATGUID = "@MatGuid";
        private const string PARAM_MATSPEC = "@MatSpec";
        private const string PARAM_MATPRICE = "@MatPrice";
        private const string PARAM_MINILIMIT = "@MiniLimit";
        private const string PARAM_MAXILIMIT = "@MaxiLimit";
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

        public MaterialInfoDAL(string sConnStr,string sAppCode)
        {
           this.connStr = sConnStr;
           this.appCode = sAppCode;
        }

        public MaterialInfoCollections GetAllRecords()
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            MaterialInfoCollections infos = null;
            MaterialInfo info = null;

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
                    infos = new MaterialInfoCollections();
                    while (reader.Read())
                    {
                        info = new MaterialInfo();
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

        public MaterialInfoCollections GetRecordsByClassNo(string sClassNo)
        {
            /*SqlConnection connection = null;
            SqlDataReader reader = null;
            MaterialInfoCollections infos = null;
            MaterialInfo info = null;

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
                    infos = new MaterialInfoCollections();
                    while (reader.Read())
                    {
                        info = new MaterialInfo();
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

        public MaterialInfoCollections GetRecordsByNo(string sNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            MaterialInfoCollections infos = null;
            MaterialInfo info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_MATNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_NO,paras);

                if (reader.HasRows)
                {
                    infos = new MaterialInfoCollections();
                    while (reader.Read())
                    {
                        info = new MaterialInfo();
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
                    new SqlParameter(PARAM_MATNO,SqlDbType.NVarChar,20),
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

        public int AddRecord(MaterialInfo info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_MATNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MATTYPENO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MATCLASSNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_PROVIDERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MATUNITENO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MATNAME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_MATBARCODE,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_MATGUID,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_MATSPEC,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_MATPRICE,SqlDbType.Float),
                    new SqlParameter(PARAM_MINILIMIT,SqlDbType.Int),
                    new SqlParameter(PARAM_MAXILIMIT,SqlDbType.Int),
                    new SqlParameter(PARAM_ADDOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_MODOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MODDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_VALIDITYSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_COMMENTS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = info.sMatNo;
                paras[1].Value = info.sMatTypeNo;
                paras[2].Value = info.sMatClassNo;
                paras[3].Value = info.sProviderNo;
                paras[4].Value = info.sMatUniteNo;
                paras[5].Value = info.sMatName;
                paras[6].Value = info.sMatBarcode;
                paras[7].Value = info.sMatGuid;
                paras[8].Value = info.sMatSpec;
                paras[9].Value = info.dMatPrice;
                paras[10].Value = info.iMiniLimit;
                paras[11].Value = info.iMaxiLimit;
                paras[12].Value = info.sAddOptor;
                paras[13].Value = info.dAddDate;
                paras[14].Value = info.sModOptor;
                paras[15].Value = info.dModDate;
                paras[16].Value = info.iValidityState;
                paras[17].Value = info.sComments;
                paras[18].Value = info.sAppCode;

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

        public int UpdateRecord(MaterialInfo info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_MATNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MATTYPENO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MATCLASSNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_PROVIDERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MATUNITENO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MATNAME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_MATBARCODE,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_MATGUID,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_MATSPEC,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_MATPRICE,SqlDbType.Float),
                    new SqlParameter(PARAM_MINILIMIT,SqlDbType.Int),
                    new SqlParameter(PARAM_MAXILIMIT,SqlDbType.Int),
                    new SqlParameter(PARAM_ADDOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_MODOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MODDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_VALIDITYSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_COMMENTS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_VERSION,SqlDbType.Timestamp)
                };
                paras[0].Value = info.sMatNo;
                paras[1].Value = info.sMatTypeNo;
                paras[2].Value = info.sMatClassNo;
                paras[3].Value = info.sProviderNo;
                paras[4].Value = info.sMatUniteNo;
                paras[5].Value = info.sMatName;
                paras[6].Value = info.sMatBarcode;
                paras[7].Value = info.sMatGuid;
                paras[8].Value = info.sMatSpec;
                paras[9].Value = info.dMatPrice;
                paras[10].Value = info.iMiniLimit;
                paras[11].Value = info.iMaxiLimit;
                paras[12].Value = info.sAddOptor;
                paras[13].Value = info.dAddDate;
                paras[14].Value = info.sModOptor;
                paras[15].Value = info.dModDate;
                paras[16].Value = info.iValidityState;
                paras[17].Value = info.sComments;
                paras[18].Value = info.sAppCode;
                paras[19].Value = StringUtil.ConvertToBytes(info.sVersion);

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
                    new SqlParameter(PARAM_MATNO,SqlDbType.NVarChar,20),
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
                    new SqlParameter(PARAM_MATNO,SqlDbType.NVarChar,20),
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

        public MaterialInfoCollections GetRecords_Paging(SqlModel s_model)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            MaterialInfoCollections infos = null;
            MaterialInfo info = null;

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
                    infos = new MaterialInfoCollections();
                    while (reader.Read())
                    {
                        info = new MaterialInfo();
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
        internal static void PutObjectProperty(MaterialInfo obj_info, SqlDataReader reader)
        {
            obj_info.iID= int.Parse(reader["ID"].ToString());
            obj_info.sMatNo= reader["MatNo"].ToString();
            obj_info.sMatTypeNo= reader["MatTypeNo"].ToString();
            obj_info.sMatClassNo= reader["MatClassNo"].ToString();
            obj_info.sProviderNo= reader["ProviderNo"].ToString();
            obj_info.sMatUniteNo= reader["MatUniteNo"].ToString();
            obj_info.sMatName= reader["MatName"].ToString();
            obj_info.sMatBarcode= reader["MatBarcode"].ToString();
            obj_info.sMatGuid= reader["MatGuid"].ToString();
            obj_info.sMatSpec= reader["MatSpec"].ToString();
            obj_info.dMatPrice= double.Parse(reader["MatPrice"].ToString());
            obj_info.iMiniLimit= int.Parse(reader["MiniLimit"].ToString());
            obj_info.iMaxiLimit= int.Parse(reader["MaxiLimit"].ToString());
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

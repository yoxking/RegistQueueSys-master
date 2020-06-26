using EntFrm.Business.IDAL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using System;
using System.Data;
using System.Data.SqlClient;

namespace EntFrm.Business.SQLServerDAL
{
  public class RecipeDetailsDAL: IRecipeDetails
  {
        #region sql
        private const string SQL_GET_ALL_RECORDS = @"Select *  From RecipeDetails Where AppCode like @AppCode And ValidityState=1";
        private const string SQL_GET_RECORDS_BY_NO = @"Select * From RecipeDetails Where   AppCode like @AppCode And   ValidityState=1 And RecipeNo=@RecipeNo";
        private const string SQL_GET_NAME_BY_NO = @"Select RecipeName From RecipeDetails Where   AppCode like @AppCode And   ValidityState=1 And RecipeNo=@RecipeNo";
        private const string SQL_ADD_RECORD = @"Insert into RecipeDetails
                                              (RecipeNo,RFlowNo,RUserNo,RecipeName,RecipeSpec,SeriNumber,Standard,Price,Amount,SQuantity,TQuantity,Direction,ExpiryDate,Frequency,AddOptor,AddDate,ModOptor,ModDate,ValidityState,Comments,AppCode)
                                              values(@RecipeNo,@RFlowNo,@RUserNo,@RecipeName,@RecipeSpec,@SeriNumber,@Standard,@Price,@Amount,@SQuantity,@TQuantity,@Direction,@ExpiryDate,@Frequency,@AddOptor,@AddDate,@ModOptor,@ModDate,@ValidityState,@Comments,@AppCode)";
        private const string SQL_UPDATE_RECORD = @"Update RecipeDetails set
                                                 RecipeNo=@RecipeNo,RFlowNo=@RFlowNo,RUserNo=@RUserNo,RecipeName=@RecipeName,RecipeSpec=@RecipeSpec,SeriNumber=@SeriNumber,Standard=@Standard,Price=@Price,Amount=@Amount,SQuantity=@SQuantity,TQuantity=@TQuantity,Direction=@Direction,ExpiryDate=@ExpiryDate,Frequency=@Frequency,AddOptor=@AddOptor,AddDate=@AddDate,ModOptor=@ModOptor,ModDate=@ModDate,ValidityState=@ValidityState,Comments=@Comments,AppCode=@AppCode 
                                                 Where  AppCode like @AppCode And   ValidityState=1 And RecipeNo=@RecipeNo  And Version=@Version";
        private const string SQL_HARD_DELETE_RECORD = @"Delete From RecipeDetails Where   AppCode like @AppCode And   RecipeNo=@RecipeNo ";
        private const string SQL_SOFT_DELETE_RECORD = @"Update RecipeDetails set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 And RecipeNo=@RecipeNo";
        private const string SQL_HARD_DELETE_BY_CONDTION = @"Delete From RecipeDetails Where   AppCode like @AppCode ";
        private const string SQL_SOFT_DELETE_BY_CONDTION = @"Update RecipeDetails set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 ";
        private const string SQL_GET_RECORDS_BY_CLASSNO = @"Select * From RecipeDetails Where    AppCode like @AppCode And   ValidityState=1 And RFlowNo=@RFlowNo";
        private const string SQL_GET_COUNT_BY_CONDITION = @"Select Count(*) From RecipeDetails Where   AppCode like @AppCode  And   ValidityState=1 ";
        #endregion

        #region param
        private const string PARAM_ID = "@ID";
        private const string PARAM_RECIPENO = "@RecipeNo";
        private const string PARAM_RFLOWNO = "@RFlowNo";
        private const string PARAM_RUSERNO = "@RUserNo";
        private const string PARAM_RECIPENAME = "@RecipeName";
        private const string PARAM_RECIPESPEC = "@RecipeSpec";
        private const string PARAM_SERINUMBER = "@SeriNumber";
        private const string PARAM_STANDARD = "@Standard";
        private const string PARAM_PRICE = "@Price";
        private const string PARAM_AMOUNT = "@Amount";
        private const string PARAM_SQUANTITY = "@SQuantity";
        private const string PARAM_TQUANTITY = "@TQuantity";
        private const string PARAM_DIRECTION = "@Direction";
        private const string PARAM_EXPIRYDATE = "@ExpiryDate";
        private const string PARAM_FREQUENCY = "@Frequency";
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

        public RecipeDetailsDAL(string sConnStr,string sAppCode)
        {
           this.connStr = sConnStr;
           this.appCode = sAppCode;
        }

        public RecipeDetailsCollections GetAllRecords()
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            RecipeDetailsCollections infos = null;
            RecipeDetails info = null;

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
                    infos = new RecipeDetailsCollections();
                    while (reader.Read())
                    {
                        info = new RecipeDetails();
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

        public RecipeDetailsCollections GetRecordsByClassNo(string sClassNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            RecipeDetailsCollections infos = null;
            RecipeDetails info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_RFLOWNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sClassNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_CLASSNO,paras);

                if (reader.HasRows)
                {
                    infos = new RecipeDetailsCollections();
                    while (reader.Read())
                    {
                        info = new RecipeDetails();
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

        public RecipeDetailsCollections GetRecordsByNo(string sNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            RecipeDetailsCollections infos = null;
            RecipeDetails info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_RECIPENO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_NO,paras);

                if (reader.HasRows)
                {
                    infos = new RecipeDetailsCollections();
                    while (reader.Read())
                    {
                        info = new RecipeDetails();
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
                    new SqlParameter(PARAM_RECIPENO,SqlDbType.NVarChar,20),
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

        public int AddNewRecord(RecipeDetails info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_RECIPENO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_RFLOWNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_RUSERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_RECIPENAME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_RECIPESPEC,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_SERINUMBER,SqlDbType.NVarChar,200),
                    new SqlParameter(PARAM_STANDARD,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_PRICE,SqlDbType.Float),
                    new SqlParameter(PARAM_AMOUNT,SqlDbType.Float),
                    new SqlParameter(PARAM_SQUANTITY,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_TQUANTITY,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_DIRECTION,SqlDbType.NVarChar,500),
                    new SqlParameter(PARAM_EXPIRYDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_FREQUENCY,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_MODOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MODDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_VALIDITYSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_COMMENTS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = info.sRecipeNo;
                paras[1].Value = info.sRFlowNo;
                paras[2].Value = info.sRUserNo;
                paras[3].Value = info.sRecipeName;
                paras[4].Value = info.sRecipeSpec;
                paras[5].Value = info.sSeriNumber;
                paras[6].Value = info.sStandard;
                paras[7].Value = info.dPrice;
                paras[8].Value = info.dAmount;
                paras[9].Value = info.sSQuantity;
                paras[10].Value = info.sTQuantity;
                paras[11].Value = info.sDirection;
                paras[12].Value = info.dExpiryDate;
                paras[13].Value = info.sFrequency;
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

        public int UpdateRecord(RecipeDetails info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_RECIPENO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_RFLOWNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_RUSERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_RECIPENAME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_RECIPESPEC,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_SERINUMBER,SqlDbType.NVarChar,200),
                    new SqlParameter(PARAM_STANDARD,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_PRICE,SqlDbType.Float),
                    new SqlParameter(PARAM_AMOUNT,SqlDbType.Float),
                    new SqlParameter(PARAM_SQUANTITY,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_TQUANTITY,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_DIRECTION,SqlDbType.NVarChar,500),
                    new SqlParameter(PARAM_EXPIRYDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_FREQUENCY,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_MODOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MODDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_VALIDITYSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_COMMENTS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_VERSION,SqlDbType.Timestamp)
                };
                paras[0].Value = info.sRecipeNo;
                paras[1].Value = info.sRFlowNo;
                paras[2].Value = info.sRUserNo;
                paras[3].Value = info.sRecipeName;
                paras[4].Value = info.sRecipeSpec;
                paras[5].Value = info.sSeriNumber;
                paras[6].Value = info.sStandard;
                paras[7].Value = info.dPrice;
                paras[8].Value = info.dAmount;
                paras[9].Value = info.sSQuantity;
                paras[10].Value = info.sTQuantity;
                paras[11].Value = info.sDirection;
                paras[12].Value = info.dExpiryDate;
                paras[13].Value = info.sFrequency;
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
                    new SqlParameter(PARAM_RECIPENO,SqlDbType.NVarChar,20),
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
                    new SqlParameter(PARAM_RECIPENO,SqlDbType.NVarChar,20),
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
        public RecipeDetailsCollections GetRecords_Paging(SqlModel s_model)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            RecipeDetailsCollections infos = null;
            RecipeDetails info = null;

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
                    infos = new RecipeDetailsCollections();
                    while (reader.Read())
                    {
                        info = new RecipeDetails();
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
        internal static void PutObjectProperty(RecipeDetails obj_info, SqlDataReader reader)
        {
            obj_info.iID= int.Parse(reader["ID"].ToString());
            obj_info.sRecipeNo= reader["RecipeNo"].ToString();
            obj_info.sRFlowNo= reader["RFlowNo"].ToString();
            obj_info.sRUserNo= reader["RUserNo"].ToString();
            obj_info.sRecipeName= reader["RecipeName"].ToString();
            obj_info.sRecipeSpec= reader["RecipeSpec"].ToString();
            obj_info.sSeriNumber= reader["SeriNumber"].ToString();
            obj_info.sStandard= reader["Standard"].ToString();
            obj_info.dPrice= double.Parse(reader["Price"].ToString());
            obj_info.dAmount= double.Parse(reader["Amount"].ToString());
            obj_info.sSQuantity= reader["SQuantity"].ToString();
            obj_info.sTQuantity= reader["TQuantity"].ToString();
            obj_info.sDirection= reader["Direction"].ToString();
            obj_info.dExpiryDate= DateTime.Parse(reader["ExpiryDate"].ToString());
            obj_info.sFrequency= reader["Frequency"].ToString();
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

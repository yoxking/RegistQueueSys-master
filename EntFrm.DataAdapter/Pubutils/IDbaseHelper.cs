using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace EntFrm.DataAdapter
{
    public class IDbaseHelper
    {

        public static string GetDataBaseName(string connStr)
        {
            try
            {
               if(!string.IsNullOrEmpty(connStr))
                { 
                    string[] stemp = connStr.Split(';');
                    return stemp[1].Split('=')[1]; 
                }

                return "";
            }
            catch(Exception ex)
            {
                return "";
            }
        }

        /// <summary>
        /// 测试连接数据库是否成功
        /// </summary>
        /// <returns></returns>
        public static bool ConnectionTest(string connStr)
        {
            SqlConnection mySqlConnection;  //mySqlConnection   is   a   SqlConnection   object  
            bool IsCanConnectioned = false;

            //创建连接对象
            mySqlConnection = new SqlConnection(connStr);
            //ConnectionTimeout 在.net 1.x 可以设置 在.net 2.0后是只读属性，则需要在连接字符串设置
            //如：server=.;uid=sa;pwd=;database=PMIS;Integrated Security=SSPI; Connection Timeout=30
            //mySqlConnection.ConnectionTimeout = 1;//设置连接超时的时间
            try
            {
                //Open DataBase
                //打开数据库
                mySqlConnection.Open();
                IsCanConnectioned = true;
            }
            catch
            {
                //Can not Open DataBase
                //打开不成功 则连接不成功
                IsCanConnectioned = false;
            }
            finally
            {
                //Close DataBase
                //关闭数据库连接
                mySqlConnection.Close();
            }
            //mySqlConnection   is   a   SqlConnection   object 
            if (mySqlConnection.State == ConnectionState.Closed || mySqlConnection.State == ConnectionState.Broken)
            {
                //Connection   is   not   available  
                return IsCanConnectioned;
            }
            else
            {
                //Connection   is   available  
                return IsCanConnectioned;
            }
        }

        public static ArrayList GetSqlFile(string sqlFile)
        {
            ArrayList alSql = new ArrayList();
            if (!File.Exists(sqlFile))
            {
                return alSql;
            }
            StreamReader rs = new StreamReader(sqlFile, System.Text.Encoding.Default);//注意编码
            string commandText = "";
            string varLine = "";
            while (rs.Peek() > -1)
            {
                varLine = rs.ReadLine();
                if (varLine == "")
                {
                    continue;
                }
                if (varLine != "GO" && varLine != "go")
                {
                    commandText += varLine;
                    //commandText = commandText.Replace("@database_name=N'dbhr'", string.Format("@database_name=N'{0}'", dbName));
                    commandText += "\r\n";
                }
                else
                {
                    alSql.Add(commandText);
                    commandText = "";
                }
            }

            rs.Close();
            return alSql;
        }

        public static ArrayList GetSqlFile(Stream stm)
        {
            ArrayList alSql = new ArrayList(); 
            StreamReader rs = new StreamReader(stm, System.Text.Encoding.Default);//注意编码

            string commandText = "";
            string varLine = "";
            while (rs.Peek() > -1)
            {
                varLine = rs.ReadLine();
                if (varLine == "")
                {
                    continue;
                }
                if (varLine != "GO" && varLine != "go")
                {
                    commandText += varLine;
                    //commandText = commandText.Replace("@database_name=N'dbhr'", string.Format("@database_name=N'{0}'", dbName));
                    commandText += "\r\n";
                }
                else
                {
                    alSql.Add(commandText);
                    commandText = "";
                }
            }

            rs.Close();
            return alSql;
        }

        public static void ExecuteCmd(ArrayList varSqlList,string connStr)
        {

            SqlConnection MyConnection = new SqlConnection(connStr);
            MyConnection.Open();
            SqlTransaction varTrans = MyConnection.BeginTransaction();
            SqlCommand command = new SqlCommand();
            command.Connection = MyConnection;
            command.Transaction = varTrans;
            try
            {
                foreach (string varcommandText in varSqlList)
                {
                    command.CommandText = varcommandText;
                    command.ExecuteNonQuery();
                }
                varTrans.Commit();
            }
            catch (Exception ex)
            {
                varTrans.Rollback();
                throw ex;
            }
            finally
            {
                MyConnection.Close();
            }
        }

        /// <summary>
        /// 对数据库的备份和恢复操作，Sql语句实现
        /// </summary>
        /// <param name="cmdText">实现备份或恢复的Sql语句</param>
        /// <param name="isBak">该操作是否为备份操作，是为true否，为false</param>
        public static  void BakReductSql(string connStr,string dbaseName,string cmdText, bool isBak)
        {
            SqlCommand cmdBakRst = new SqlCommand();
            SqlConnection conn = new SqlConnection(connStr);
            try
            {
                conn.Open();
                cmdBakRst.Connection = conn;
                cmdBakRst.CommandType = CommandType.Text;
                if (!isBak)     //如果是恢复操作
                {
                    string setOffline = "Alter database "+ dbaseName + " Set Offline With rollback immediate ";
                    string setOnline = " Alter database "+ dbaseName + " Set Online With Rollback immediate";
                    cmdBakRst.CommandText = setOffline + cmdText + setOnline;
                }
                else
                {
                    cmdBakRst.CommandText = cmdText;
                }
                cmdBakRst.ExecuteNonQuery();
                
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                cmdBakRst.Dispose();
                conn.Close();
                conn.Dispose();
            }
        }
    }
}

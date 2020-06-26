using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Threading;

namespace EntCodeGener
{
    public partial class CodeGenerator : Form
    {
        public CodeGenerator()
        {
            InitializeComponent();
        }

        private void CodeGenerator_Load(object sender, EventArgs e)
        {
            txtConnString.Text = @"Server=127.0.0.1;Database=IQueueBusinessSys_Db;User ID=sa;Password=Y789kingadmin";
        }

        private void btConnectDb_Click(object sender, EventArgs e)
        {
            if (txtConnString.Text.Trim().Length == 0)
            {
                MessageBox.Show("数据库连接不能为空.");
            }

            lbResult.Items.Clear();

            SqlConnection con = new SqlConnection(txtConnString.Text.Trim());

            con.Open();
            DataTable dt = con.GetSchema("Tables");
            con.Close();

            DataView dv = dt.DefaultView;
            dv.Sort = "TABLE_NAME Asc"; 

            cbTables.DataSource = dv;
            cbTables.DisplayMember = "TABLE_NAME";
            cbTables.ValueMember = "TABLE_NAME";
        }

        private void btExecute_Click(object sender, EventArgs e)
        {
            bool bSingleMode = rbSingle.Checked;

            string sTableName ="";
            string sFileDir = "";

            string sFileName = "";
            string sNameSpace = txtNameSpace.Text.Trim();

            DataTable dt = new DataTable();

            if (bSingleMode)
            {
                if (cbTables.SelectedIndex < 0)
                {
                    MessageBox.Show("请先选择一个数据库.");
                    return;
                }

                if(cbClassType.SelectedIndex<0)
                {
                    MessageBox.Show("请先选择一个类类型.");
                    return;
                }

                sTableName = cbTables.SelectedValue.ToString();
                sFileDir = Environment.CurrentDirectory.ToString();

                GetTableInfo(sTableName, ref dt);

                switch (cbClassType.SelectedItem.ToString())
                {
                    case "Model":
                        sFileDir += @"\\" + sNameSpace + "Model\\";
                        sFileName = sTableName + ".cs";
                        CreateModelClass(sFileDir, sFileName, sTableName, sNameSpace + "Model", dt);
                        break;
                    case "Collections":
                        sFileDir += @"\\" + sNameSpace + "Model\\Collections\\";
                        sFileName = sTableName + "Collections.cs";
                        CreateCollectionsClass(sFileDir, sFileName, sTableName, sNameSpace + "Model.Collections", dt);
                        break;
                    case "IDAL":
                        sFileDir += @"\\" + sNameSpace + "IDAL\\";
                        sFileName = "I" + sTableName + ".cs";
                        CreateIDALClass(sFileDir, sFileName, sTableName, sNameSpace + "IDAL", dt);
                        break;
                    case "SQLServerDAL":
                        sFileDir += @"\\" + sNameSpace + "SQLServerDAL\\";
                        sFileName = sTableName + "DAL.cs";
                        CreateSQLServerDALClass(sFileDir, sFileName, sTableName, sNameSpace + "SQLServerDAL", dt);
                        break;
                    case "MyCloudDAL":
                        sFileDir += @"\\" + sNameSpace + "MyCloudDAL\\";
                        sFileName = sTableName + "DAL.cs";
                        CreateMyCloudDALClass(sFileDir, sFileName, sTableName, sNameSpace + "MyCloudDAL", dt);
                        break;
                    case "DALFactory":
                        sFileDir += @"\\" + sNameSpace + "DALFactory\\";
                        sFileName = sTableName + "Factory.cs";
                        CreateDALFactoryClass(sFileDir, sFileName, sTableName, sNameSpace + "DALFactory", dt);
                        break; 
                    case "BLL":
                        sFileDir += @"\\" + sNameSpace + "BLL\\";
                        sFileName = sTableName + "BLL.cs";
                        CreateBLLClass(sFileDir, sFileName, sTableName, sNameSpace + "BLL", dt);
                        break;
                    case "IService":
                        sFileDir += @"\\" + sNameSpace + "IService\\";
                        sFileName = sTableName + "_IService.cs";
                        CreateIServiceClass(sFileDir, sFileName, sTableName, sNameSpace + "_IService", dt);
                        break;
                    case "ServiceInstruction":
                        sFileDir += @"\\" + sNameSpace + "ServiceInstruction\\";
                        sFileName = "ServiceInstruction.xml";
                        CreateServiceInstruction(sFileDir, sFileName, sTableName, sNameSpace, dt);
                        break;
                    case "Business":
                        sFileDir += @"\\" + sNameSpace + "Business\\";
                        sFileName = sTableName + "Business.cs";
                        CreateBusiness(sFileDir, sFileName, sTableName, sNameSpace + "Business", dt);
                        break;
                    default: break;
                }
            }
            else
            {
                if (cbClassType.SelectedIndex < 0)
                {
                    MessageBox.Show("请先选择一个类类型.");
                    return;
                }

                for (int i = 0; i < cbTables.Items.Count; i++)
                {
                    cbTables.SelectedIndex = i;
                    sTableName = cbTables.SelectedValue.ToString();
                    sFileDir = Environment.CurrentDirectory.ToString();

                    GetTableInfo(sTableName, ref dt);

                    switch (cbClassType.SelectedItem.ToString())
                    {
                        case "Model":
                            sFileDir += @"\\" + sNameSpace + "Model\\";
                            sFileName = sTableName + ".cs";
                            CreateModelClass(sFileDir, sFileName, sTableName, sNameSpace + "Model", dt);
                            break;
                        case "Collections":
                            sFileDir += @"\\" + sNameSpace + "Model\\Collections\\";
                            sFileName = sTableName + "Collections.cs";
                            CreateCollectionsClass(sFileDir, sFileName, sTableName, sNameSpace + "Model.Collections", dt);
                            break;
                        case "IDAL":
                             sFileDir += @"\\" + sNameSpace + "IDAL\\";
                            sFileName = "I"+sTableName + ".cs";
                            CreateIDALClass(sFileDir, sFileName, sTableName, sNameSpace + "IDAL", dt);
                            break;
                        case "SQLServerDAL":
                            sFileDir += @"\\" + sNameSpace + "SQLServerDAL\\";
                            sFileName = sTableName + "DAL.cs";
                            CreateSQLServerDALClass(sFileDir, sFileName, sTableName, sNameSpace + "SQLServerDAL", dt);
                            break;
                        case "MyCloudDAL":
                            sFileDir += @"\\" + sNameSpace + "MyCloudDAL\\";
                            sFileName = sTableName + "DAL.cs";
                            CreateMyCloudDALClass(sFileDir, sFileName, sTableName, sNameSpace + "MyCloudDAL", dt);
                            break;
                        case "DALFactory":
                            sFileDir += @"\\" + sNameSpace + "DALFactory\\";
                            sFileName = sTableName + "Factory.cs";
                            CreateDALFactoryClass(sFileDir, sFileName, sTableName, sNameSpace + "DALFactory", dt);
                            break;
                        case "BLL":
                            sFileDir += @"\\" + sNameSpace + "BLL\\";
                            sFileName = sTableName + "BLL.cs";
                            CreateBLLClass(sFileDir, sFileName, sTableName, sNameSpace + "BLL", dt);
                            break;
                        case "IService":
                            sFileDir += @"\\" + sNameSpace + "IService\\";
                            sFileName = sTableName + "_IService.cs";
                            CreateIServiceClass(sFileDir, sFileName, sTableName, sNameSpace + "_IService", dt);
                            break;
                        case "ServiceInstruction":
                            sFileDir += @"\\" + sNameSpace + "ServiceInstruction\\";
                            sFileName = "ServiceInstruction.xml";
                            CreateServiceInstruction(sFileDir, sFileName, sTableName, sNameSpace, dt);
                            break;
                        case "Business":
                            sFileDir += @"\\" + sNameSpace + "Business\\";
                            sFileName = sTableName + "Business.cs";
                           CreateBusiness(sFileDir, sFileName, sTableName, sNameSpace + "Business", dt);
                            break;
                        default: break;
                    }
                }
            }

        }

        internal void GenerateCode(string sFileDir, string sFileName, string sCode)
        {
            Directory.CreateDirectory(sFileDir);
            FileStream fs = new FileStream(sFileDir + sFileName, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(sCode);
            sw.Close();
            fs.Close();

            lbResult.Items.Add("生成" + sFileName + "成功."); 
            Thread.Sleep(1000);
        }

        private void GetTableInfo(string sTableName, ref DataTable schema)
        {
            SqlConnection con = new SqlConnection(txtConnString.Text.Trim());

            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROM " + sTableName, con);

            SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            schema = rd.GetSchemaTable();

            con.Close();

        }

        private string CamelCase(string s)
        {
            return s.ToLower()[0].ToString() + s.Substring(1);
        }

        private string GetCSharpType(string sDataType)
        {
            switch (sDataType)
            {
                case "System.Int16":  return "short";
                case "System.Int32": return "int";
                case "System.Single": return "float";
                case "System.Double": return "double";
                case "System.Decimal": return "double";
                case "System.String": return "string";
                case "System.DateTime": return "DateTime";
                case "System.Byte": return "string";
                case "System.Byte[]": return "string";
                case "System.Boolean": return "bool";
                default: throw new Exception("未知的类型.");
            }
        }

        private string GetSQLType(string sDataType,string dataSize)
        {
            switch (sDataType)
            {
                case "System.Int16": return "SqlDbType.Int";
                case "System.Int32": return "SqlDbType.Int";
                case "System.Single": return "SqlDbType.Int";
                case "System.Double": return "SqlDbType.Float";
                case "System.Decimal": return "SqlDbType.Decimal";
                case "System.String": return "SqlDbType.NVarChar,"+dataSize;
                case "System.DateTime": return "SqlDbType.DateTime";
                case "System.Byte": return "SqlDbType.Timestamp";
                case "System.Byte[]": return "SqlDbType.Timestamp";
                case "System.Boolean": return "SqlDbType.Char,1";
                default: throw new Exception("未知的类型.");
            }
        }

        private string GetSqliteType(string sDataType, string dataSize)
        {
            switch (sDataType)
            {
                case "System.Int16": return "DbType.Int32";
                case "System.Int32": return "DbType.Int32";
                case "System.Single": return "DbType.Int32";
                case "System.Double": return "DbType.Decimal";
                case "System.Decimal": return "DbType.Decimal";
                case "System.String": return "DbType.String";
                case "System.DateTime": return "DbType.DateTime"; 
                case "System.Boolean": return "DbType.Boolean";
                default:  return "DbType.String"; 
            }
        }

        private string GetCParseType(string sDataType)
        {
            switch (sDataType)
            {
                case "System.Int16": return "short.Parse(";
                case "System.Int32": return "int.Parse(";
                case "System.Single": return "float.Parse(";
                case "System.Double": return "double.Parse(";
                case "System.Decimal": return "double.Parse(";
                case "System.String":  return "";
                case "System.DateTime": return "DateTime.Parse(";
                case "System.Byte": return "StringHelper.ConvertToString((byte[])";
                case "System.Byte[]": return "StringHelper.ConvertToString((byte[])";
                case "System.Boolean": return "bool.Parse(";
                default:  throw new Exception("未知的类型.");
            }
        }

        private bool GetCParseTypeState(string sDataType)
        {
            bool iscov = true;
            switch (sDataType)
            {
                case "System.Int16": break;
                case "System.Int32": break;
                case "System.Single": break;
                case "System.Double": break;
                case "System.Decimal": break;
                case "System.String": iscov = false; break;
                case "System.DateTime": break;
                case "System.Byte": break;
                case "System.Byte[]": break;
                case "System.Boolean": break;
                default: iscov = false; break;
            }

            return iscov;
        }

        private  void CreateModelClass(string sFileDir, string sFileName, string sTableName,string sNameSpace,DataTable dt)
        {
            StringBuilder sb = new StringBuilder();

            string camelCaseName=CamelCase(sTableName);

            sb.Append("using System;" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("namespace " +sNameSpace+Environment.NewLine);
            sb.Append("{" + Environment.NewLine);

            sb.Append("  public class "+sTableName + Environment.NewLine);
            sb.Append("  {" + Environment.NewLine);

            sb.Append("     public "+sTableName+"(){ }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            string sType="";
            string sColName = "";
            foreach(DataRow dr in dt.Rows)
            {
                sType=GetCSharpType(dr["DataType"].ToString());
                sColName = dr["ColumnName"].ToString();
                sb.Append("       private " + sType + " " + sColName + ";" + Environment.NewLine);
                sb.Append("       public " + sType + " " + sType.ToLower().Substring(0, 1) + sColName + Environment.NewLine);
                sb.Append("       {" + Environment.NewLine);
                sb.Append("           set { this."+sColName+" =value;}"+Environment.NewLine);
                sb.Append("           get { return this."+sColName+";}"+Environment.NewLine);
                sb.Append("        }"+Environment.NewLine);
                sb.Append(Environment.NewLine);
            }
            sb.Append("    }" + Environment.NewLine);
            sb.Append("  }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            GenerateCode(sFileDir, sFileName, sb.ToString());
        }

        private void CreateCollectionsClass(string sFileDir, string sFileName, string sTableName, string sNameSpace, DataTable dt)
        {
            StringBuilder sb = new StringBuilder();

            string camelCaseName = CamelCase(sTableName);
            
            sb.Append("using System.Collections;" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("namespace " + sNameSpace + Environment.NewLine);
            sb.Append("{" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("  public class " + sTableName+"Collections:CollectionBase" + Environment.NewLine);
            sb.Append("  {" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("      public "+sTableName+" this[int index]"+Environment.NewLine);
            sb.Append("      {" + Environment.NewLine);
            sb.Append("         get { return ("+sTableName+")List[index]; }"+Environment.NewLine);
            sb.Append("         set { List[index] = value; }"+Environment.NewLine);
            sb.Append("      }"+Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("      public int Add("+sTableName+" value)"+Environment.NewLine);
            sb.Append("      {"+Environment.NewLine);
            sb.Append("          return (List.Add(value));"+Environment.NewLine);
            sb.Append("     }"+Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("     public int IndexOf(" + sTableName + " value)" + Environment.NewLine);
            sb.Append("     {"+Environment.NewLine);
            sb.Append("         return (List.IndexOf(value));"+Environment.NewLine);
            sb.Append("     }"+Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("      public void Insert(int index, "+sTableName+" value)"+Environment.NewLine);
            sb.Append("     {"+Environment.NewLine);
            sb.Append("         List.Insert(index, value);"+Environment.NewLine);
            sb.Append("      }"+Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("     public void Remove("+sTableName+" value)"+Environment.NewLine);
            sb.Append("    {"+Environment.NewLine);
            sb.Append("         List.Remove(value);"+Environment.NewLine);
            sb.Append("     }"+Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("    public bool Contains("+sTableName+" value)"+Environment.NewLine);
            sb.Append("     {"+Environment.NewLine);
            sb.Append("       return (List.Contains(value));"+Environment.NewLine);
            sb.Append("     }"+Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("     public "+sTableName+" GetFirstOne()"+Environment.NewLine);
            sb.Append("     {"+Environment.NewLine);
            sb.Append("         return this[0];"+Environment.NewLine);
            sb.Append("     }" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("    }" + Environment.NewLine);
            sb.Append("  }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            GenerateCode(sFileDir, sFileName, sb.ToString());
        }

        private void CreateIDALClass(string sFileDir, string sFileName, string sTableName, string sNameSpace, DataTable dt)
        {
            StringBuilder sb = new StringBuilder();

            string camelCaseName = CamelCase(sTableName);
             
            sb.Append("using EntFrm.Business.Model;" + Environment.NewLine);
            sb.Append("using EntFrm.Business.Model.Collections;" + Environment.NewLine);
            sb.Append("using EntFrm.Framework.Utility;" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("namespace " + sNameSpace + Environment.NewLine);
            sb.Append("{" + Environment.NewLine);

            sb.Append("  public interface I" + sTableName + Environment.NewLine);
            sb.Append("  {" + Environment.NewLine);

            sb.Append("        " + sTableName + "Collections GetAllRecords();" + Environment.NewLine);
            sb.Append("        " + sTableName + "Collections GetRecordsByClassNo(string sClassNo);" + Environment.NewLine);
            sb.Append("        " + sTableName + "Collections GetRecordsByNo(string sNo);" + Environment.NewLine);
            sb.Append("        string GetRecordNameByNo(string sNo);" + Environment.NewLine);
            sb.Append("        int AddNewRecord(" + sTableName + " info);" + Environment.NewLine);
            sb.Append("        int UpdateRecord(" + sTableName + " info); " + Environment.NewLine);
            sb.Append("        int HardDeleteRecord(string sNo);" + Environment.NewLine);
            sb.Append("        int SoftDeleteRecord(string sNo);" + Environment.NewLine);
            sb.Append("        int HardDeleteByCondition(string sCondition);" + Environment.NewLine);
            sb.Append("        int SoftDeleteByCondition(string sCondition);" + Environment.NewLine);
            sb.Append("        " + sTableName + "Collections GetRecords_Paging(SqlModel s_model);" + Environment.NewLine);
            sb.Append("        int GetCountByCondition(string sCondition);" + Environment.NewLine);

             
            sb.Append("    }" + Environment.NewLine);
            sb.Append("  }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            GenerateCode(sFileDir, sFileName, sb.ToString());
        }
        
        private void CreateDALFactoryClass(string sFileDir, string sFileName, string sTableName, string sNameSpace, DataTable dt)
        {
            StringBuilder sb = new StringBuilder();

            string camelCaseName = CamelCase(sTableName);

            sb.Append("using EntFrm.Business.IDAL;" + Environment.NewLine); 
            sb.Append("using System;" + Environment.NewLine);
            sb.Append("using System.Configuration;" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("namespace " + sNameSpace + Environment.NewLine);
            sb.Append("{" + Environment.NewLine);

            sb.Append("  public sealed class " + sTableName+"Factory" + Environment.NewLine);
            sb.Append("  {" + Environment.NewLine);

            sb.Append("        public static I" + sTableName + " Create(string sUrl,string sAppCode) " + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("          try" + Environment.NewLine);
            sb.Append("          {" + Environment.NewLine);
            sb.Append("           string path = ConfigurationManager.AppSettings[\"BusinessDAL\"].ToString();" + Environment.NewLine);
            sb.Append("           string className = path + \"." + sTableName + "DAL,\" + path;" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("           Type typeofControl = Type.GetType(className,true,true);" + Environment.NewLine);
            sb.Append("           return (I" + sTableName + ")Activator.CreateInstance(typeofControl, new object[] { sUrl, sAppCode });" + Environment.NewLine); 
            sb.Append("          }" + Environment.NewLine);

            sb.Append("          catch (Exception ex)" + Environment.NewLine);
            sb.Append("          {" + Environment.NewLine);
            sb.Append("              throw new Exception(\" 通过工厂模式创建DAL层时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("           }" + Environment.NewLine);
             
            sb.Append("      }" + Environment.NewLine);
            sb.Append("   }" + Environment.NewLine);
            sb.Append("  }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            GenerateCode(sFileDir, sFileName, sb.ToString());
        }

        private void CreateSQLServerDALClass(string sFileDir, string sFileName, string sTableName, string sNameSpace, DataTable dt)
        {
            StringBuilder sb = new StringBuilder();

            string camelCaseName = CamelCase(sTableName);
             
            sb.Append("using EntFrm.Business.IDAL;" + Environment.NewLine);
            sb.Append("using EntFrm.Business.Model;" + Environment.NewLine);
            sb.Append("using EntFrm.Business.Model.Collections;" + Environment.NewLine);
            sb.Append("using EntFrm.Framework.Utility;" + Environment.NewLine);
            sb.Append("using System;" + Environment.NewLine);
            sb.Append("using System.Data;" + Environment.NewLine);
            sb.Append("using System.Data.SqlClient;" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("namespace " + sNameSpace + Environment.NewLine);
            sb.Append("{" + Environment.NewLine);

            IList<string> sColNames = new List<string>();
            string sColName = "";
            string sVal1 = "";
            string sVal2 = "";
            string sVal3 = "";
            string sKeyField = "";
            foreach (DataRow dr in dt.Rows)
            {
                sColName = dr["ColumnName"].ToString();
                sColNames.Add(sColName);
                sVal1 += sColName + ",";
                sVal2 += "@" + sColName + ",";
                sVal3 += sColName + "=@" + sColName + ",";
            }
            sVal1=sVal1.Substring(3, sVal1.Length - 12);
            sVal2 = sVal2.Substring(4, sVal2.Length - 14);
            sVal3 = sVal3.Substring(7, sVal3.Length - 25);
            sKeyField = sColNames[1];

            sb.Append("  public class " + sTableName + "DAL: I" + sTableName  + Environment.NewLine);
            sb.Append("  {" + Environment.NewLine);
            sb.Append("        #region sql" + Environment.NewLine);
            sb.Append("        private const string SQL_GET_ALL_RECORDS = @\"Select *  From " + sTableName + " Where AppCode like @AppCode And ValidityState=1\";" + Environment.NewLine);
            sb.Append("        private const string SQL_GET_RECORDS_BY_NO = @\"Select * From " + sTableName + " Where   AppCode like @AppCode And   ValidityState=1 And " + sKeyField + "=@" + sKeyField + "\";" + Environment.NewLine);
            sb.Append("        private const string SQL_GET_NAME_BY_NO = @\"Select Name From " + sTableName + " Where   AppCode like @AppCode And   ValidityState=1 And " + sKeyField + "=@" + sKeyField + "\";" + Environment.NewLine);
            sb.Append("        private const string SQL_ADD_RECORD = @\"Insert into " + sTableName  + Environment.NewLine);
            sb.Append("                                              ("+sVal1+")"+Environment.NewLine);
            sb.Append("                                              values(" + sVal2 + ")\";" + Environment.NewLine);
            
            sb.Append("        private const string SQL_UPDATE_RECORD = @\"Update "+sTableName+" set"+Environment.NewLine);
            sb.Append("                                                 " + sVal3 + " " + Environment.NewLine);
            sb.Append("                                                 Where  AppCode like @AppCode And   ValidityState=1 And " + sKeyField + "=@" + sKeyField + "  And Version=@Version\";" + Environment.NewLine);

            sb.Append("        private const string SQL_HARD_DELETE_RECORD = @\"Delete From " + sTableName + " Where   AppCode like @AppCode And   " + sKeyField + "=@" + sKeyField + " \";" + Environment.NewLine);

            sb.Append("        private const string SQL_SOFT_DELETE_RECORD = @\"Update " + sTableName + " set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 And " + sKeyField + "=@" + sKeyField + "\";" + Environment.NewLine);

            sb.Append("        private const string SQL_HARD_DELETE_BY_CONDTION = @\"Delete From " + sTableName + " Where   AppCode like @AppCode \";" + Environment.NewLine);
            sb.Append("        private const string SQL_SOFT_DELETE_BY_CONDTION = @\"Update " + sTableName + " set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 \";" + Environment.NewLine);
            sb.Append("        private const string SQL_GET_RECORDS_BY_CLASSNO = @\"Select * From " + sTableName + " Where    AppCode like @AppCode And   ValidityState=1 And ClassNo=@ClassNo\";" + Environment.NewLine);

            sb.Append("        private const string SQL_GET_COUNT_BY_CONDITION = @\"Select Count(*) From " + sTableName + " Where   AppCode like @AppCode  And   ValidityState=1 \";" + Environment.NewLine);
            sb.Append("        #endregion" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        #region param" + Environment.NewLine);

            sKeyField = sKeyField.ToUpper();
            foreach (DataRow dr in dt.Rows)
            {
                sColName = dr["ColumnName"].ToString();
                sb.Append("        private const string PARAM_" + sColName.ToUpper() + " = \"@" + sColName + "\";" + Environment.NewLine);
            }
            sb.Append("        #endregion" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        private string connStr;" + Environment.NewLine);
            sb.Append("        private string appCode;" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public " + sTableName + "DAL(string sConnStr,string sAppCode)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("           this.connStr = sConnStr;" + Environment.NewLine);
            sb.Append("           this.appCode = sAppCode;" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public " + sTableName + "Collections GetAllRecords()" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            SqlConnection connection = null;" + Environment.NewLine);
            sb.Append("            SqlDataReader reader = null;" + Environment.NewLine);
            sb.Append("            " + sTableName + "Collections infos = null;" + Environment.NewLine);
            sb.Append("            " + sTableName + " info = null;" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                SqlParameter[] paras = new SqlParameter[]" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)" + Environment.NewLine);
            sb.Append("                 };" + Environment.NewLine);
            sb.Append("                paras[0].Value = \"%\" + appCode + \";%\";" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("                connection = SqlHelper.GetConnection(connStr);" + Environment.NewLine);
            sb.Append("                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_ALL_RECORDS,paras);" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("                if (reader.HasRows)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    infos = new " + sTableName + "Collections();" + Environment.NewLine);
            sb.Append("                    while (reader.Read())" + Environment.NewLine);
            sb.Append("                    {" + Environment.NewLine);
            sb.Append("                        info = new " + sTableName + "();" + Environment.NewLine);

            sb.Append("                        // 设置对象属性" + Environment.NewLine);
            sb.Append("                        PutObjectProperty(info, reader);" + Environment.NewLine);

            sb.Append("                        infos.Add(info);" + Environment.NewLine);
            sb.Append("                    }" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);

            sb.Append("                return infos;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                throw new Exception(\" 查询所有记录(DAL层|GetAllRecords)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            finally" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                if (reader != null)" + Environment.NewLine);
            sb.Append("                    ((IDisposable)reader).Dispose();" + Environment.NewLine);

            sb.Append("                if (connection != null)" + Environment.NewLine);
            sb.Append("                    connection.Dispose();" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public " + sTableName + "Collections GetRecordsByClassNo(string sClassNo)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            /*SqlConnection connection = null;" + Environment.NewLine);
            sb.Append("            SqlDataReader reader = null;" + Environment.NewLine);
            sb.Append("            " + sTableName + "Collections infos = null;" + Environment.NewLine);
            sb.Append("            " + sTableName + " info = null;" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                SqlParameter[] paras = new SqlParameter[]" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    new SqlParameter(PARAM_CLASSNO,SqlDbType.NVarChar,20)," + Environment.NewLine);
            sb.Append("                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)" + Environment.NewLine);        
            sb.Append("                };" + Environment.NewLine);
            sb.Append("                paras[0].Value = sClassNo;" + Environment.NewLine);
            sb.Append("                paras[1].Value = \"%\" + appCode + \";%\";" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("                connection = SqlHelper.GetConnection(connStr);" + Environment.NewLine);

            sb.Append("                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_CLASSNO,paras);" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("                if (reader.HasRows)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    infos = new " + sTableName + "Collections();" + Environment.NewLine);
            sb.Append("                    while (reader.Read())" + Environment.NewLine);
            sb.Append("                    {" + Environment.NewLine);
            sb.Append("                        info = new " + sTableName + "();" + Environment.NewLine);

            sb.Append("                        // 设置对象属性" + Environment.NewLine);
            sb.Append("                        PutObjectProperty(info, reader);" + Environment.NewLine);

            sb.Append("                        infos.Add(info);" + Environment.NewLine);
            sb.Append("                    }" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);

            sb.Append("                return infos;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                throw new Exception(\" 通过sClassNo查询记录(DAL层)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            finally" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                if (reader != null)" + Environment.NewLine);
            sb.Append("                    ((IDisposable)reader).Dispose();" + Environment.NewLine);

            sb.Append("                if (connection != null)" + Environment.NewLine);
            sb.Append("                    connection.Dispose();" + Environment.NewLine);
            sb.Append("            }*/" + Environment.NewLine); 
            sb.Append("            return null;" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public " + sTableName + "Collections GetRecordsByNo(string sNo)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            SqlConnection connection = null;" + Environment.NewLine);
            sb.Append("            SqlDataReader reader = null;" + Environment.NewLine);
            sb.Append("            " + sTableName + "Collections infos = null;" + Environment.NewLine);
            sb.Append("            " + sTableName + " info = null;" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                SqlParameter[] paras = new SqlParameter[]" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    new SqlParameter(PARAM_" + sKeyField + ",SqlDbType.NVarChar,20)," + Environment.NewLine);
            sb.Append("                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)" + Environment.NewLine);            
            sb.Append("                };" + Environment.NewLine);
            sb.Append("                paras[0].Value = sNo;" + Environment.NewLine);
            sb.Append("                paras[1].Value = \"%\" + appCode + \";%\";" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("                connection = SqlHelper.GetConnection(connStr);" + Environment.NewLine);
            sb.Append("                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_NO,paras);" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("                if (reader.HasRows)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    infos = new " + sTableName + "Collections();" + Environment.NewLine);
            sb.Append("                    while (reader.Read())" + Environment.NewLine);
            sb.Append("                    {" + Environment.NewLine);
            sb.Append("                        info = new " + sTableName + "();" + Environment.NewLine);

            sb.Append("                        //设置对象属性" + Environment.NewLine);
            sb.Append("                        PutObjectProperty(info, reader);" + Environment.NewLine);

            sb.Append("                        infos.Add(info);" + Environment.NewLine);
            sb.Append("                    }" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);

            sb.Append("                return infos;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                throw new Exception(\" 通过No查询记录(DAL层)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            finally" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                if (reader != null)" + Environment.NewLine);
            sb.Append("                    ((IDisposable)reader).Dispose();" + Environment.NewLine);

            sb.Append("                if (connection != null)" + Environment.NewLine);
            sb.Append("                    connection.Dispose();" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public string GetRecordNameByNo(string sNo)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            SqlConnection connection = null;" + Environment.NewLine);

            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                SqlParameter[] paras = new SqlParameter[]" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    new SqlParameter(PARAM_" + sKeyField + ",SqlDbType.NVarChar,20)," + Environment.NewLine);
            sb.Append("                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)" + Environment.NewLine);          
            sb.Append("                };" + Environment.NewLine);
            sb.Append("                paras[0].Value = sNo;" + Environment.NewLine);
            sb.Append("                paras[1].Value = \"%\" + appCode + \";%\";" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("                connection = SqlHelper.GetConnection(connStr);" + Environment.NewLine);
            sb.Append("                return (string)SqlHelper.ExecuteScalar(connection, CommandType.Text, SQL_GET_NAME_BY_NO, paras);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                throw new Exception(\" 通过No查询记录名称(DAL层)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            finally" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                if (connection != null)" + Environment.NewLine);
            sb.Append("                    connection.Dispose();" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public int AddNewRecord(" + sTableName + " info)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            SqlConnection connection = null;" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                SqlParameter[] paras = new SqlParameter[]" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);

            int pos = 0;
            int rcount = dt.Rows.Count;
            foreach (DataRow dr in dt.Rows)
            {
                if(pos++==0)
                {
                    continue;
                }

                    if(pos==rcount)
                    {
                        break;
                    }
                else if (pos == rcount - 1)
                {
                    sb.Append("                    new SqlParameter(PARAM_" + dr["ColumnName"].ToString().ToUpper() + "," + GetSQLType(dr["DataType"].ToString(), dr["ColumnSize"].ToString()) + ")" + Environment.NewLine);
                }
                else
                {
                    sb.Append("                    new SqlParameter(PARAM_" + dr["ColumnName"].ToString().ToUpper() + "," + GetSQLType(dr["DataType"].ToString(), dr["ColumnSize"].ToString()) + ")," + Environment.NewLine);
                }
            }            
            sb.Append("                };" + Environment.NewLine);

            int index = 0;
            pos = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (pos++ == 0)
                {
                    continue;
                }


                if (pos == rcount)
                {
                    break;
                }
                else
                {
                    sb.Append("                paras[" + index.ToString() + "].Value = info." + GetCSharpType(dr["DataType"].ToString()).ToLower().Substring(0, 1) + dr["ColumnName"].ToString() + ";" + Environment.NewLine);
                }
                index++;

            }
            sb.Append(Environment.NewLine);

            sb.Append("                connection = SqlHelper.GetConnection(connStr);" + Environment.NewLine);
            sb.Append("                return SqlHelper.ExecuteNonQuery(connection, CommandType.Text, SQL_ADD_RECORD, paras);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                throw new Exception(\" 添加(DAL层)记录时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            finally" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                if (connection != null)" + Environment.NewLine);
            sb.Append("                    connection.Dispose();" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public int UpdateRecord(" + sTableName + " info)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            SqlConnection connection = null;" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                SqlParameter[] paras = new SqlParameter[]" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);

            pos = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (pos++ == 0)
                {
                    continue;
                }

                if (pos == rcount )
                {
                    sb.Append("                    new SqlParameter(PARAM_" + dr["ColumnName"].ToString().ToUpper() + "," + GetSQLType(dr["DataType"].ToString(), dr["ColumnSize"].ToString()) + ")" + Environment.NewLine);
                }
                else
                {
                    sb.Append("                    new SqlParameter(PARAM_" + dr["ColumnName"].ToString().ToUpper() + "," + GetSQLType(dr["DataType"].ToString(), dr["ColumnSize"].ToString()) + ")," + Environment.NewLine);
                }
            }
            sb.Append("                };" + Environment.NewLine);

            index = 0;
            pos = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (pos++ == 0)
                {
                    continue;
                }


                if (pos == rcount)
                {
                    sb.Append("                paras[" + index.ToString() + "].Value = StringHelper.ConvertToBytes(info." + GetCSharpType(dr["DataType"].ToString()).ToLower().Substring(0, 1) + dr["ColumnName"].ToString() + ");" + Environment.NewLine);
                }
                else
                {
                    sb.Append("                paras[" + index.ToString() + "].Value = info." + GetCSharpType(dr["DataType"].ToString()).ToLower().Substring(0, 1) + dr["ColumnName"].ToString() + ";" + Environment.NewLine);
                }
                    index++;
            }
            sb.Append(Environment.NewLine);

            sb.Append("                connection = SqlHelper.GetConnection(connStr);" + Environment.NewLine);
            sb.Append("                return SqlHelper.ExecuteNonQuery(connection, CommandType.Text, SQL_UPDATE_RECORD, paras);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                throw new Exception(\" 更新记录(DAL层)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            finally" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                if (connection != null)" + Environment.NewLine);
            sb.Append("                    connection.Dispose();" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public int HardDeleteRecord(string sNo)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            SqlConnection connection = null;" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                SqlParameter[] paras = new SqlParameter[]" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    new SqlParameter(PARAM_" + sKeyField + ",SqlDbType.NVarChar,20)," + Environment.NewLine);
            sb.Append("                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)" + Environment.NewLine);      
            sb.Append("                };" + Environment.NewLine);
            sb.Append("                paras[0].Value = sNo;" + Environment.NewLine);
            sb.Append("                paras[1].Value = \"%\" + appCode + \";%\";" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("                connection = SqlHelper.GetConnection(connStr);" + Environment.NewLine);
            sb.Append("                return SqlHelper.ExecuteNonQuery(connection, CommandType.Text, SQL_HARD_DELETE_RECORD, paras);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                throw new Exception(\" 硬删除记录(DAL层)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            finally" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                if (connection != null)" + Environment.NewLine);
            sb.Append("                    connection.Dispose();" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public int SoftDeleteRecord(string sNo)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            SqlConnection connection = null;" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                SqlParameter[] paras = new SqlParameter[]" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    new SqlParameter(PARAM_" + sKeyField + ",SqlDbType.NVarChar,20)," + Environment.NewLine);
            sb.Append("                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)" + Environment.NewLine); 
            sb.Append("                };" + Environment.NewLine);
            sb.Append("                paras[0].Value = sNo;" + Environment.NewLine);
            sb.Append("                paras[1].Value = \"%\" + appCode + \";%\";" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("                connection = SqlHelper.GetConnection(connStr);" + Environment.NewLine);
            sb.Append("                return SqlHelper.ExecuteNonQuery(connection, CommandType.Text, SQL_SOFT_DELETE_RECORD, paras);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                throw new Exception(\" 软删除记录(DAL层)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            finally" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                if (connection != null)" + Environment.NewLine);
            sb.Append("                    connection.Dispose();" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);


            sb.Append("public int HardDeleteByCondition(string sCondtion)" + Environment.NewLine);
            sb.Append("{" + Environment.NewLine);
            sb.Append("    SqlConnection connection = null;" + Environment.NewLine);

            sb.Append("    try" + Environment.NewLine);
            sb.Append("    {" + Environment.NewLine);
            sb.Append("        string sql = SQL_HARD_DELETE_BY_CONDTION;" + Environment.NewLine);
            sb.Append("        if (!string.IsNullOrEmpty(sCondtion))" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("           sql += \" And \" + sCondtion;" + Environment.NewLine);
            sb.Append("       }" + Environment.NewLine);

            sb.Append("       SqlParameter[] paras = new SqlParameter[]" + Environment.NewLine);
            sb.Append("      {" + Environment.NewLine);
            sb.Append("      new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)" + Environment.NewLine);
            sb.Append("       };" + Environment.NewLine);
            sb.Append("       paras[0].Value = \"%\" + appCode + \";%\";" + Environment.NewLine);

            sb.Append("      connection = SqlHelper.GetConnection(connStr);" + Environment.NewLine);
            sb.Append("       return SqlHelper.ExecuteNonQuery(connection, CommandType.Text, sql, paras);" + Environment.NewLine);
            sb.Append("  }" + Environment.NewLine);
            sb.Append("  catch (Exception ex)" + Environment.NewLine);
            sb.Append("  {" + Environment.NewLine);
            sb.Append("      throw new Exception(\"硬删除记录(DAL层)时出错; \" + ex.Message);" + Environment.NewLine);
            sb.Append("  }" + Environment.NewLine);
            sb.Append("  finally" + Environment.NewLine);
            sb.Append("  {" + Environment.NewLine);
            sb.Append("      if (connection != null)" + Environment.NewLine);
            sb.Append("          connection.Dispose();" + Environment.NewLine);
            sb.Append("  }" + Environment.NewLine);
            sb.Append(" }" + Environment.NewLine);

            sb.Append("public int SoftDeleteByCondition(string sCondtion)" + Environment.NewLine);
            sb.Append("{" + Environment.NewLine);
            sb.Append("   SqlConnection connection = null;" + Environment.NewLine);

            sb.Append("    try" + Environment.NewLine);
            sb.Append("   {" + Environment.NewLine);
            sb.Append("       string sql = SQL_SOFT_DELETE_BY_CONDTION;" + Environment.NewLine);
            sb.Append("       if (!string.IsNullOrEmpty(sCondtion))" + Environment.NewLine);
            sb.Append("       {" + Environment.NewLine);
            sb.Append("           sql += \" And \" + sCondtion;" + Environment.NewLine);
            sb.Append("       }" + Environment.NewLine);

            sb.Append("       SqlParameter[] paras = new SqlParameter[]" + Environment.NewLine);
            sb.Append("       {" + Environment.NewLine);
            sb.Append("      new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)" + Environment.NewLine);
            sb.Append("      };" + Environment.NewLine);
            sb.Append("      paras[0].Value = \"%\" + appCode + \";%\";" + Environment.NewLine);

            sb.Append("      connection = SqlHelper.GetConnection(connStr);" + Environment.NewLine);
            sb.Append("      return SqlHelper.ExecuteNonQuery(connection, CommandType.Text, sql, paras);" + Environment.NewLine);
            sb.Append("  }" + Environment.NewLine);
            sb.Append("  catch (Exception ex)" + Environment.NewLine);
            sb.Append(" {" + Environment.NewLine);
            sb.Append("     throw new Exception(\" 硬删除记录(DAL层)时出错; \" + ex.Message);" + Environment.NewLine);
            sb.Append("  }" + Environment.NewLine);
            sb.Append("  finally" + Environment.NewLine);
            sb.Append("  {" + Environment.NewLine);
            sb.Append("     if (connection != null)" + Environment.NewLine);
            sb.Append("         connection.Dispose();" + Environment.NewLine);
            sb.Append(" }" + Environment.NewLine);
            sb.Append(" }" + Environment.NewLine);

            sb.Append("        public " + sTableName + "Collections GetRecords_Paging(SqlModel s_model)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            SqlConnection connection = null;" + Environment.NewLine);
            sb.Append("            SqlDataReader reader = null;" + Environment.NewLine);
            sb.Append("            " + sTableName + "Collections infos = null;" + Environment.NewLine);
            sb.Append("            " + sTableName + " info = null;" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                 if (s_model.sCondition.Length==0)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    s_model.sCondition = \" Where  AppCode like '%\" + appCode + \";%' And ValidityState=1\";" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("                else" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    s_model.sCondition = \" Where   AppCode like '%\" + appCode + \";%' And ValidityState=1 And  \" + s_model.sCondition;" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            //sb.Append("                s_model.sTableName = \"" + sTableName + "\";" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("                string strSql = SqlHelper.GetSQL_Paging(s_model);" + Environment.NewLine);

            sb.Append("                connection = SqlHelper.GetConnection(connStr);" + Environment.NewLine);

            sb.Append("                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, strSql);" + Environment.NewLine);

            sb.Append("                if (reader.HasRows)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    infos = new " + sTableName + "Collections();" + Environment.NewLine);
            sb.Append("                    while (reader.Read())" + Environment.NewLine);
            sb.Append("                    {" + Environment.NewLine);
            sb.Append("                        info = new " + sTableName + "();" + Environment.NewLine);

            sb.Append("                        // 设置对象属性" + Environment.NewLine);
            sb.Append("                        PutObjectProperty(info, reader);" + Environment.NewLine);

            sb.Append("                        infos.Add(info);" + Environment.NewLine);
            sb.Append("                    }" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);

            sb.Append("                return infos;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                throw new Exception(\" 分页查询(DAL层)记录时出错;;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            finally" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                if (reader != null)" + Environment.NewLine);
            sb.Append("                    ((IDisposable)reader).Dispose();" + Environment.NewLine);

            sb.Append("                if (connection != null)" + Environment.NewLine);
            sb.Append("                    connection.Dispose();" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public int GetCountByCondition(string sCondition)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            SqlConnection connection = null;" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                string strSql = SQL_GET_COUNT_BY_CONDITION;" + Environment.NewLine);
            sb.Append("                if(sCondition.Length>0)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    strSql +=\"  And \" + sCondition;" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("                SqlParameter[] paras = new SqlParameter[]" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                   new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)" + Environment.NewLine);
            sb.Append("                };" + Environment.NewLine);
            sb.Append("                paras[0].Value = \"%\" + appCode + \";%\";" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("                connection = SqlHelper.GetConnection(connStr);" + Environment.NewLine);

            sb.Append("                return Convert.ToInt32(SqlHelper.ExecuteScalar(connection, CommandType.Text, strSql, paras));" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                throw new Exception(\" 计算记录总数(DAL层)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            finally" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                if (connection != null)" + Environment.NewLine);
            sb.Append("                    connection.Dispose();" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        #region PutObjectProperty 设置对象属性" + Environment.NewLine);
            sb.Append("        /// <summary>" + Environment.NewLine);
            sb.Append("        /// 从 SqlDataReader 类对象中读取并设置对象属性" + Environment.NewLine);
            sb.Append("        /// </summary>" + Environment.NewLine);
            sb.Append("       /// <param name=\" obj_info\">主题对象</param>" + Environment.NewLine);
            sb.Append("        /// <param name=\"dr\">读入数据</param>" + Environment.NewLine);
            sb.Append("        internal static void PutObjectProperty(" + sTableName + " obj_info, SqlDataReader reader)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);

            foreach (DataRow dr in dt.Rows)
            {
                sb.Append("            obj_info." + GetCSharpType(dr["DataType"].ToString()).ToLower().Substring(0, 1) + dr["ColumnName"].ToString() + "= " + GetCParseType(dr["DataType"].ToString()) + "reader[\"" + dr["ColumnName"].ToString() + "\"].ToString()"+ (GetCParseTypeState(dr["DataType"].ToString()) ? ")" : "") +";" + Environment.NewLine);
            }
            sb.Append("        }" + Environment.NewLine);
            sb.Append("        #endregion" + Environment.NewLine);
            sb.Append("    }" + Environment.NewLine);
            sb.Append("}" + Environment.NewLine);

            GenerateCode(sFileDir, sFileName, sb.ToString());
        }

        private void CreateMyCloudDALClass(string sFileDir, string sFileName, string sTableName, string sNameSpace, DataTable dt)
        {
            StringBuilder sb = new StringBuilder();

            string camelCaseName = CamelCase(sTableName);

            sb.Append("using System;" + Environment.NewLine);
            sb.Append("using System.Collections.Generic;" + Environment.NewLine);
            sb.Append("using System.Linq;" + Environment.NewLine);
            sb.Append("using System.Text;" + Environment.NewLine);
            sb.Append("using System.Reflection;" + Environment.NewLine);
            sb.Append("using CommonCLib;" + Environment.NewLine);
            sb.Append("using CommonCLib.Soap_Model;" + Environment.NewLine);
            sb.Append("using CommonCLib.Soap_Utils;" + Environment.NewLine);
            sb.Append("using Model;" + Environment.NewLine);
            sb.Append("using Model.Collections;" + Environment.NewLine);
            sb.Append("using IDAL;" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("namespace " + sNameSpace + Environment.NewLine);
            sb.Append("{" + Environment.NewLine);

            sb.Append("  public class " + sTableName + "DAL:I" + sTableName + Environment.NewLine);
            sb.Append("  {" + Environment.NewLine);
            sb.Append("        private string webUrl;" + Environment.NewLine);
            sb.Append("        private string appCode;" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("        private WebSoapInfo soapInfo;" + Environment.NewLine);
            sb.Append("        private EncryptInfo encInfo;" + Environment.NewLine);
            sb.Append(Environment.NewLine);


            sb.Append("        public " + sTableName + "DAL(string sWebUrl,string sAppCode)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("           this.webUrl = sWebUrl;" + Environment.NewLine);
            sb.Append("           this.appCode = sAppCode;" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("           soapInfo = MySoapHelper.Get_WebSoapInfo();" + Environment.NewLine); 
            sb.Append("           soapInfo.WebServiceUrl = webUrl;" + Environment.NewLine);
            sb.Append("           soapInfo.AppCode = appCode;" + Environment.NewLine); 
            sb.Append(Environment.NewLine); 
            sb.Append("           encInfo = MySoapHelper.Get_EncryptInfo();" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public " + sTableName + "Collections GetAllRecords()" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            " + sTableName + "Collections infos = null;" + Environment.NewLine);
            sb.Append("            " + sTableName + " info = null;" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                IWebSoapRequestUtil soapUtil = new IWebSoapRequestUtil(soapInfo);" + Environment.NewLine);
            sb.Append("                if (soapUtil.ExecuteSoapInstruction(\"" + sTableName + "_GetAllRecords\", null, true, true, encInfo))" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                   List<List<string>> infoList = (List<List<string>>)soapUtil.ResultMessage;" + Environment.NewLine);
            sb.Append("                   if (infoList != null && infoList.Count > 0)" + Environment.NewLine);
            sb.Append("                   {" + Environment.NewLine);
            sb.Append("                      infos = new " + sTableName + "Collections();" + Environment.NewLine);
            sb.Append("                      for (int i = 0; i < infoList.Count; i++)" + Environment.NewLine);
            sb.Append("                      {" + Environment.NewLine);
            sb.Append("                         info = new " + sTableName + "();" + Environment.NewLine);
            sb.Append("                         // 设置对象属性" + Environment.NewLine);
            sb.Append("                         PutObjectProperty(info, infoList[i]);" + Environment.NewLine);
            sb.Append("                        infos.Add(info);" + Environment.NewLine);
            sb.Append("                      }" + Environment.NewLine);
            sb.Append("                   }" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);             

            sb.Append("                return infos;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                throw new Exception(\" 查询所有记录(DAL层|GetAllRecords)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public " + sTableName + "Collections GetRecordsByClassNo(string sClassNo)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            " + sTableName + "Collections infos = null;" + Environment.NewLine);
            sb.Append("            " + sTableName + " info = null;" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                IWebSoapRequestUtil soapUtil = new IWebSoapRequestUtil(soapInfo);" + Environment.NewLine);
            sb.Append("                if (soapUtil.ExecuteSoapInstruction(\"" + sTableName + "_GetRecordsByClassNo\", new object[]{sClassNo}, true, true, encInfo))" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                   List<List<string>> infoList = (List<List<string>>)soapUtil.ResultMessage;" + Environment.NewLine);
            sb.Append("                   if (infoList != null && infoList.Count > 0)" + Environment.NewLine);
            sb.Append("                   {" + Environment.NewLine);
            sb.Append("                      infos = new " + sTableName + "Collections();" + Environment.NewLine);
            sb.Append("                      for (int i = 0; i < infoList.Count; i++)" + Environment.NewLine);
            sb.Append("                      {" + Environment.NewLine);
            sb.Append("                         info = new " + sTableName + "();" + Environment.NewLine);
            sb.Append("                         // 设置对象属性" + Environment.NewLine);
            sb.Append("                         PutObjectProperty(info, infoList[i]);" + Environment.NewLine);
            sb.Append("                        infos.Add(info);" + Environment.NewLine);
            sb.Append("                      }" + Environment.NewLine);
            sb.Append("                   }" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);

            sb.Append("                return infos;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                throw new Exception(\" 通过sClassNo查询记录(DAL层)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public " + sTableName + "Collections GetRecordsByNo(string sNo)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            " + sTableName + "Collections infos = null;" + Environment.NewLine);
            sb.Append("            " + sTableName + " info = null;" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                IWebSoapRequestUtil soapUtil = new IWebSoapRequestUtil(soapInfo);" + Environment.NewLine);
            sb.Append("                if (soapUtil.ExecuteSoapInstruction(\"" + sTableName + "_GetRecordsByNo\", new object[]{sNo}, true, true, encInfo))" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                   List<List<string>> infoList = (List<List<string>>)soapUtil.ResultMessage;" + Environment.NewLine);
            sb.Append("                   if (infoList != null && infoList.Count > 0)" + Environment.NewLine);
            sb.Append("                   {" + Environment.NewLine);
            sb.Append("                      infos = new " + sTableName + "Collections();" + Environment.NewLine);
            sb.Append("                      for (int i = 0; i < infoList.Count; i++)" + Environment.NewLine);
            sb.Append("                      {" + Environment.NewLine);
            sb.Append("                         info = new " + sTableName + "();" + Environment.NewLine);
            sb.Append("                         // 设置对象属性" + Environment.NewLine);
            sb.Append("                         PutObjectProperty(info, infoList[i]);" + Environment.NewLine);
            sb.Append("                        infos.Add(info);" + Environment.NewLine);
            sb.Append("                      }" + Environment.NewLine);
            sb.Append("                   }" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);

            sb.Append("                return infos;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                throw new Exception(\" 通过No查询记录(DAL层)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public string GetRecordNameByNo(string sNo)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("          string returnValue = null;" + Environment.NewLine);
            sb.Append("          try" + Environment.NewLine);
            sb.Append("           {" + Environment.NewLine);
            sb.Append("                IWebSoapRequestUtil soapUtil = new IWebSoapRequestUtil(soapInfo);" + Environment.NewLine);
            sb.Append("                if (soapUtil.ExecuteSoapInstruction(\"" + sTableName + "_GetRecordsByNo\", new object[]{sNo}, true, true, encInfo))" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                  List<List<string>> s =  (List<List<string>>)soapUtil.ResultMessage; " + Environment.NewLine);
            sb.Append("                  returnValue = s[0][0].ToString(); " + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("                return returnValue;" + Environment.NewLine);
            sb.Append("             }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                throw new Exception(\" 通过sNo查询记录名称(DAL层)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public int AddRecord(" + sTableName + " info)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("          int returnValue = 0;" + Environment.NewLine);
            sb.Append("          object[] paramValues = GetObjectArray(info);" + Environment.NewLine);
            sb.Append("          try" + Environment.NewLine);
            sb.Append("          {" + Environment.NewLine);
            sb.Append("                IWebSoapRequestUtil soapUtil = new IWebSoapRequestUtil(soapInfo);" + Environment.NewLine);
            sb.Append("                if (soapUtil.ExecuteSoapInstruction(\"" + sTableName + "_AddRecord\", paramValues, true, true, encInfo))" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                  List<List<string>> s =  (List<List<string>>)soapUtil.ResultMessage; " + Environment.NewLine);
            sb.Append("                  returnValue = int.Parse(s[0][0].ToString());" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("                 return returnValue;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                throw new Exception(\" 添加(DAL层)记录时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);             
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public int UpdateRecord(" + sTableName + " info)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("          int returnValue = 0;" + Environment.NewLine);
            sb.Append("          object[] paramValues = GetObjectArray(info);" + Environment.NewLine);
            sb.Append("          try" + Environment.NewLine);
            sb.Append("          {" + Environment.NewLine);
            sb.Append("                IWebSoapRequestUtil soapUtil = new IWebSoapRequestUtil(soapInfo);" + Environment.NewLine);
            sb.Append("                if (soapUtil.ExecuteSoapInstruction(\"" + sTableName + "_UpdateRecord\", paramValues, true, true, encInfo))" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                  List<List<string>> s =  (List<List<string>>)soapUtil.ResultMessage; " + Environment.NewLine);
            sb.Append("                  returnValue = int.Parse(s[0][0].ToString());" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("                 return returnValue;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                throw new Exception(\" 更新(DAL层)记录时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public int HardDeleteRecord(string sNo)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("          int returnValue = 0;" + Environment.NewLine); 
            sb.Append("          try" + Environment.NewLine);
            sb.Append("          {" + Environment.NewLine);
            sb.Append("                IWebSoapRequestUtil soapUtil = new IWebSoapRequestUtil(soapInfo);" + Environment.NewLine);
            sb.Append("                if (soapUtil.ExecuteSoapInstruction(\"" + sTableName + "_HardDeleteRecord\", new object[] { sNo }, true, true, encInfo))" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                  List<List<string>> s =  (List<List<string>>)soapUtil.ResultMessage; " + Environment.NewLine);
            sb.Append("                  returnValue = int.Parse(s[0][0].ToString());" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("                 return returnValue;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                throw new Exception(\" 硬删除(DAL层)记录时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public int SoftDeleteRecord(string sNo)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("          int returnValue = 0;" + Environment.NewLine);
            sb.Append("          try" + Environment.NewLine);
            sb.Append("          {" + Environment.NewLine);
            sb.Append("                IWebSoapRequestUtil soapUtil = new IWebSoapRequestUtil(soapInfo);" + Environment.NewLine);
            sb.Append("                if (soapUtil.ExecuteSoapInstruction(\"" + sTableName + "_SoftDeleteRecord\", new object[] { sNo }, true, true, encInfo))" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                  List<List<string>> s =  (List<List<string>>)soapUtil.ResultMessage; " + Environment.NewLine);
            sb.Append("                  returnValue = int.Parse(s[0][0].ToString());" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("                 return returnValue;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                throw new Exception(\" 软删除(DAL层)记录时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public " + sTableName + "Collections GetRecords_Paging(Common_SqlModel s_model)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            " + sTableName + "Collections infos = null;" + Environment.NewLine);
            sb.Append("            " + sTableName + " info = null;" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("            object[] paramValues = new object[7];" + Environment.NewLine);
            sb.Append("            paramValues[0] = s_model.sTableName;" + Environment.NewLine);
            sb.Append("            paramValues[1] = s_model.sFields;" + Environment.NewLine);
            sb.Append("            paramValues[2] = s_model.iPageNo;" + Environment.NewLine);
            sb.Append("            paramValues[3] = s_model.iPageSize;" + Environment.NewLine);
            sb.Append("            paramValues[4] = s_model.sCondition;" + Environment.NewLine);
            sb.Append("            paramValues[5] = s_model.sOrderField;" + Environment.NewLine);
            sb.Append("            paramValues[6] = s_model.sOrderType;" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                IWebSoapRequestUtil soapUtil = new IWebSoapRequestUtil(soapInfo);" + Environment.NewLine);
            sb.Append("                if (soapUtil.ExecuteSoapInstruction(\"" + sTableName + "_GetRecordsByPaging\", paramValues, true, true, encInfo))" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                   List<List<string>> infoList = (List<List<string>>)soapUtil.ResultMessage;" + Environment.NewLine);
            sb.Append("                   if (infoList != null && infoList.Count > 0)" + Environment.NewLine);
            sb.Append("                   {" + Environment.NewLine);
            sb.Append("                      infos = new " + sTableName + "Collections();" + Environment.NewLine);
            sb.Append("                      for (int i = 0; i < infoList.Count; i++)" + Environment.NewLine);
            sb.Append("                      {" + Environment.NewLine);
            sb.Append("                         info = new " + sTableName + "();" + Environment.NewLine);
            sb.Append("                         // 设置对象属性" + Environment.NewLine);
            sb.Append("                         PutObjectProperty(info, infoList[i]);" + Environment.NewLine);
            sb.Append("                        infos.Add(info);" + Environment.NewLine);
            sb.Append("                      }" + Environment.NewLine);
            sb.Append("                   }" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);

            sb.Append("                return infos;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                throw new Exception(\" 分页查询(DAL层)记录时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public int GetCountByCondition(string sCondition)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("          int returnValue = 0;" + Environment.NewLine);
            sb.Append("          try" + Environment.NewLine);
            sb.Append("          {" + Environment.NewLine);
            sb.Append("                IWebSoapRequestUtil soapUtil = new IWebSoapRequestUtil(soapInfo);" + Environment.NewLine);
            sb.Append("                if (soapUtil.ExecuteSoapInstruction(\"" + sTableName + "_GetCountByCondition\", new object[] { sCondition }, true, true, encInfo))" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                  List<List<string>> s =  (List<List<string>>)soapUtil.ResultMessage; " + Environment.NewLine);
            sb.Append("                  returnValue = int.Parse(s[0][0].ToString());" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("                 return returnValue;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                throw new Exception(\" 计算记录总数(DAL层)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        #region PutObjectProperty 设置对象属性" + Environment.NewLine);
            sb.Append("        /// <summary>" + Environment.NewLine);
            sb.Append("        /// 从 SqlDataReader 类对象中读取并设置对象属性" + Environment.NewLine);
            sb.Append("        /// </summary>" + Environment.NewLine);
            sb.Append("       /// <param name=\" obj_info\">主题对象</param>" + Environment.NewLine);
            sb.Append("        /// <param name=\"dr\">读入数据</param>" + Environment.NewLine);
            sb.Append("        internal static void PutObjectProperty(" + sTableName + " obj_info, List<string> infoList)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            int i = 0;
            int icount = dt.Rows.Count;
            foreach (DataRow dr in dt.Rows)
            {
                if (icount-1 == i)
                {
                    sb.Append("            obj_info." + GetCSharpType(dr["DataType"].ToString()).ToLower().Substring(0, 1) + dr["ColumnName"].ToString() + "= infoList[" + (i++).ToString() + "].ToString();" + Environment.NewLine);
                }
                else
                {
                    sb.Append("            obj_info." + GetCSharpType(dr["DataType"].ToString()).ToLower().Substring(0, 1) + dr["ColumnName"].ToString() + "= " + GetCParseType(dr["DataType"].ToString()) + "infoList[" + (i++).ToString() + "].ToString()" + (GetCParseTypeState(dr["DataType"].ToString()) ? ")" : "") + ";" + Environment.NewLine);
                }
            }
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        internal static object[] GetObjectArray(" + sTableName + "  info)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("          Type t = typeof(" + sTableName + ");" + Environment.NewLine);
            sb.Append("          object[] paramValues = new object[t.GetProperties().Count()-1];" + Environment.NewLine);
            sb.Append( Environment.NewLine);
            int j = -1;
            foreach (DataRow dr in dt.Rows)
            {
                if(j<0)
                {
                    j++;
                    continue;
                }
                sb.Append("            paramValues["+(j++).ToString()+"] = info." + GetCSharpType(dr["DataType"].ToString()).ToLower().Substring(0, 1) + dr["ColumnName"].ToString() +";"+ Environment.NewLine);
            }
            sb.Append(Environment.NewLine);
            sb.Append("        return paramValues;" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append("        #endregion" + Environment.NewLine);
            sb.Append("    }" + Environment.NewLine);
            sb.Append("}" + Environment.NewLine);

            GenerateCode(sFileDir, sFileName, sb.ToString());
        }


        private void CreateBusiness(string sFileDir, string sFileName, string sTableName, string sNameSpace, DataTable dt)
        {
            StringBuilder sb = new StringBuilder();

            string camelCaseName = CamelCase(sTableName);
            
            sb.Append("using System;" + Environment.NewLine);
            sb.Append("using System.Collections.Generic;" + Environment.NewLine);
            sb.Append("using System.Linq;" + Environment.NewLine);
            sb.Append("using System.Text;" + Environment.NewLine);
            sb.Append("using System.Threading.Tasks;" + Environment.NewLine);
            sb.Append("using Model;" + Environment.NewLine);
            sb.Append("using Model.Collections;" + Environment.NewLine);
            sb.Append("using BLL;" + Environment.NewLine);
            sb.Append("using CommonCLib;");
            sb.Append("using EntFrm.Library;");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("namespace " + sNameSpace  + Environment.NewLine);
            sb.Append("{" + Environment.NewLine);
            sb.Append("    public class " + sTableName + "Business" + Environment.NewLine);
            sb.Append("    {" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        private string connStr;" + Environment.NewLine);
            sb.Append("        private string appCode;" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public  void Init()" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public  " + sTableName + "Business(string sConnStr, string sAppCode)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("           this.connStr = sConnStr;" + Environment.NewLine);
            sb.Append("           this.appCode = sAppCode;" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        private  void ValidateRequired(" + sTableName + " info)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            /*if (info != null)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                 if (info.sName.Trim().Length == 0)" + Environment.NewLine);
            sb.Append("                 {" + Environment.NewLine);
            sb.Append("                   throw new Exception(\"出错提示:Name不能为空;\");" + Environment.NewLine);
            sb.Append("                 }" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            else{" + Environment.NewLine);
            sb.Append("                throw new Exception(\"出错提示:对象不能为空;\");" + Environment.NewLine);
            sb.Append("            }*/" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        private  void ValidateRepeat(" + sTableName + "  info)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            /*if (info != null)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                 try" + Environment.NewLine);
            sb.Append("                 {" + Environment.NewLine);
            sb.Append("                     I" + sTableName + "  infoDAL = " + sTableName + "Factory.Create(this.connStr, this.appCode);" + Environment.NewLine);
            sb.Append("                    if (infoDAL.GetCountByCondition(\" Name='\" + info.sName.Trim() + \"'\") > 0)" + Environment.NewLine);
            sb.Append("                    {" + Environment.NewLine);
            sb.Append("                        throw new Exception(\"Name存在重复记录;\");" + Environment.NewLine);
            sb.Append("                    }" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("                catch (Exception ex)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    throw new Exception(\"出错提示:\" + ex.Message);" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            else{" + Environment.NewLine);
            sb.Append("                throw new Exception(\"出错提示:对象不能为空;\");" + Environment.NewLine);
            sb.Append("            }*/" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public  bool AddNewRecord(" + sTableName + " info)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                 //ValidateRequired(info);" + Environment.NewLine);
            sb.Append("                 //ValidateRepeat(info);" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("                " + sTableName + "BLL infoBLL = new " + sTableName + "BLL(connStr, appCode);" + Environment.NewLine);
            sb.Append("                if (infoBLL.AddNewRecord(info) > 0)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                  return true;" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("                  return false;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                throw new Exception(\"出错提示:添加新记录(AddNewRecord|Business)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public  bool UpdateRecord(" + sTableName + " info)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                 //ValidateRequired(info);" + Environment.NewLine);
            sb.Append("                " + sTableName + "BLL infoBLL = new " + sTableName + "BLL(connStr, appCode);" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("                if (infoBLL.UpdateRecord(info) > 0)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                  return true;" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("                  return false;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                throw new Exception(\"出错提示:更新记录(UpdateRecord|Business)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public  bool SoftDeleteRecord(string sNo)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            if (sNo.Length > 0)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                try" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                " + sTableName + "BLL infoBLL = new " + sTableName + "BLL(connStr, appCode);" + Environment.NewLine);
            sb.Append("                 if (infoBLL.SoftDeleteRecord(sNo) > 0)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                  return true;" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("                  return false;" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("                catch (Exception ex)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    throw new Exception(\"出错提示:软删除记录(SoftDeleteRecord|Business)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            return false;" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public bool HardDeleteRecord(string sNo)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            if (sNo.Length > 0)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                try" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                " + sTableName + "BLL infoBLL = new " + sTableName + "BLL(connStr, appCode);" + Environment.NewLine);
            sb.Append("                 if (infoBLL.HardDeleteRecord(sNo) > 0)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                  return true;" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("                  return false;" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("                catch (Exception ex)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    throw new Exception(\"出错提示:硬删除记录(HardDeleteRecord|Business)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            return false;" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public   " + sTableName + "Collections GetAllRecords()" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                " + sTableName + "BLL infoBLL = new " + sTableName + "BLL(connStr, appCode);" + Environment.NewLine);
            sb.Append("                return infoBLL.GetAllRecords();" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("               throw new Exception(\"出错提示:查询所有记录(GetAllRecords|Business)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public " + sTableName + "Collections GetRecordsByClassNo(string sClassNo)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            if (sClassNo.Length > 0)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                try" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    " + sTableName + "BLL infoBLL = new " + sTableName + "BLL(connStr, appCode);" + Environment.NewLine);
            sb.Append("                    return infoBLL.GetRecordsByClassNo(sClassNo);" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("                catch (Exception ex)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    throw new Exception(\"出错提示:通过ClassNo查询记录(GetRecordsByClassNo|Business)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            return null ;" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public  " + sTableName + " GetRecordByNo(string sNo)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            if (sNo.Length > 0)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                try" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                " + sTableName + "BLL infoBLL = new " + sTableName + "BLL(connStr, appCode);" + Environment.NewLine);
            sb.Append("                    return infoBLL.GetRecordByNo(sNo);" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("                catch (Exception ex)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    throw new Exception(\"出错提示:通过No查询记录(GetRecordsByNo|Business)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            return null;" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public  string GetRecordNameByNo(string sNo)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            if (sNo.Length > 0)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                try" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                " + sTableName + "BLL infoBLL = new " + sTableName + "BLL(connStr, appCode);" + Environment.NewLine);
            sb.Append("                    return infoBLL.GetRecordNameByNo(sNo);" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("                catch (Exception ex)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    throw new Exception(\"出错提示:通过No查询名称(GetRecordNameByNo|Business)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            return \"\";" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);


            sb.Append("        public  " + sTableName + "Collections GetPagingRecoreds(ref int pageCount,int pageIndex,string condition)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("           return GetPagingRecoreds(ref pageCount, pageIndex, PublicEntity.PAGESIZE, condition);" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public  " + sTableName + "Collections GetPagingRecoreds(ref int pageCount,int pageIndex,int pageSize,string condition)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("        try" + Environment.NewLine);
            sb.Append("         {" + Environment.NewLine);
            sb.Append("            " + sTableName + "BLL infoBLL = new " + sTableName + "BLL(connStr, appCode);" + Environment.NewLine);
            sb.Append("            int iPageSize = pageSize>0?pageSize:PublicEntity.PAGESIZE;" + Environment.NewLine);
            sb.Append("            int iPageIndex = pageIndex;" + Environment.NewLine);
            sb.Append("            int iRCount = infoBLL.GetCountByCondition(condition);" + Environment.NewLine);
            sb.Append("            int iPageCount = (iRCount / iPageSize) + 1;" + Environment.NewLine);
            sb.Append("            pageCount = iPageCount;" + Environment.NewLine);
            sb.Append("" + Environment.NewLine);
            sb.Append("            if (iPageCount < 1)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("              iPageCount = 1;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("" + Environment.NewLine);
            sb.Append("            if (iPageIndex < 1)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                iPageIndex = 1;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            else if (iPageIndex > iPageCount)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                iPageIndex = iPageCount;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("" + Environment.NewLine);
            sb.Append("          Common_SqlModel s_model = new Common_SqlModel();" + Environment.NewLine);
            sb.Append("" + Environment.NewLine);
            sb.Append("          s_model.iPageNo = iPageIndex;" + Environment.NewLine);
            sb.Append("          s_model.iPageSize = iPageSize;" + Environment.NewLine);
            sb.Append("          s_model.sFields = \"*\";" + Environment.NewLine);
            sb.Append("          s_model.sCondition = condition;" + Environment.NewLine);
            sb.Append("          s_model.sOrderField = \"ID\";" + Environment.NewLine);
            sb.Append("          s_model.sOrderType = \"desc\";" + Environment.NewLine);
            sb.Append("          s_model.sTableName = \"" + sTableName + "\";" + Environment.NewLine);

            sb.Append("          return infoBLL.GetRecords_Paging(s_model);" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("          }" + Environment.NewLine);
            sb.Append("          catch (Exception ex)" + Environment.NewLine);
            sb.Append("          {" + Environment.NewLine);
            sb.Append("              throw new Exception(\"出错提示:分页查询记录(GetRecords_Paging|Business)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("          }" + Environment.NewLine); 
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public  int GetCountByCondition(string sCondition)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                " + sTableName + "BLL infoBLL = new " + sTableName + "BLL(connStr, appCode);" + Environment.NewLine);
            sb.Append("                return infoBLL.GetCountByCondition(sCondition);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                throw new Exception(\"出错提示:计算记录个数(GetCountByCondition|Business)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append("    }" + Environment.NewLine);
            sb.Append("}" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            GenerateCode(sFileDir, sFileName, sb.ToString());
        }

        private void CreateBLLClass(string sFileDir, string sFileName, string sTableName, string sNameSpace, DataTable dt)
        {
            StringBuilder sb = new StringBuilder();

            string camelCaseName = CamelCase(sTableName);
             
            sb.Append("using EntFrm.Framework.Utility;" + Environment.NewLine);
            sb.Append("using EntFrm.Business.DALFactory;" + Environment.NewLine);
            sb.Append("using EntFrm.Business.IDAL;" + Environment.NewLine);
            sb.Append("using EntFrm.Business.Model;" + Environment.NewLine);
            sb.Append("using EntFrm.Business.Model.Collections;" + Environment.NewLine);
            sb.Append("using System;" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("namespace " + sNameSpace  + Environment.NewLine);
            sb.Append("{" + Environment.NewLine);
            sb.Append("    public class " + sTableName + "BLL" + Environment.NewLine);
            sb.Append("    {" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        private string connStr;" + Environment.NewLine);
            sb.Append("        private string appCode;" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            
            sb.Append("        public  " + sTableName + "BLL(string sConnStr, string sAppCode)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("           this.connStr = sConnStr;" + Environment.NewLine);
            sb.Append("           this.appCode = sAppCode;" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);
             
            sb.Append("        private  bool ValidateRepeat(string sNo)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("        if (!string.IsNullOrEmpty(sNo))" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("           try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("               I" + sTableName + " infoDAL = " + sTableName + "Factory.Create(this.connStr, this.appCode);" + Environment.NewLine);
             
            sb.Append("               if (infoDAL.GetCountByCondition(\" "+ dt.Rows[1]["ColumnName"].ToString() + " ='\" + sNo + \"'\") > 0)" + Environment.NewLine);
            sb.Append("               {" + Environment.NewLine);
            sb.Append("                   return true;" + Environment.NewLine);
            sb.Append("               }" + Environment.NewLine);
            sb.Append("               return false;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("           {" + Environment.NewLine);
            sb.Append("               return false;" + Environment.NewLine);
            sb.Append("           }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append("        else" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("           return false;" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public  bool AddNewRecord(" + sTableName + " info)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("            if (!ValidateRepeat(info.s"+ dt.Rows[1]["ColumnName"].ToString() + "))" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                I" + sTableName + " infoDAL = " + sTableName + "Factory.Create(this.connStr, this.appCode);" + Environment.NewLine);
             sb.Append("                if (infoDAL.AddNewRecord(info) > 0)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    return true;" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("                return false;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            return false;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                throw new Exception(\"出错提示:添加新记录(AddNewRecord|BLL)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public  bool UpdateRecord(" + sTableName + " info)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine); 
            sb.Append("                     I" + sTableName + "  infoDAL = " + sTableName + "Factory.Create(this.connStr, this.appCode);" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("                if (infoDAL.UpdateRecord(info) > 0)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    return true;" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("                return false;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                throw new Exception(\"出错提示:更新记录(UpdateRecord|BLL)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public  bool SoftDeleteRecord(string sNo)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            if (sNo.Length > 0)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                try" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                     I" + sTableName + "  infoDAL = " + sTableName + "Factory.Create(this.connStr, this.appCode);" + Environment.NewLine);
            sb.Append("                if (infoDAL.SoftDeleteRecord(sNo) > 0)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    return true;" + Environment.NewLine);
            sb.Append("                 }" + Environment.NewLine);
            sb.Append("                return false;" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("                catch (Exception ex)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    throw new Exception(\"出错提示:软删除记录(SoftDeleteRecord|BLL)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            return false;" + Environment.NewLine); 
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);


            sb.Append("        public bool HardDeleteRecord(string sNo)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            if (sNo.Length > 0)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                try" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    I" + sTableName + "  infoDAL = " + sTableName + "Factory.Create(this.connStr, this.appCode);" + Environment.NewLine);
            sb.Append("                if (infoDAL.HardDeleteRecord(sNo) > 0)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    return true;" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("                return false;" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("                catch (Exception ex)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    throw new Exception(\"出错提示:硬删除记录(HardDeleteRecord|BLL)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            return false;" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);


            sb.Append("        public bool SoftDeleteRecord(string[] sNos)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            if (sNos != null && sNos.Length > 0)" + Environment.NewLine);
            sb.Append("           {" + Environment.NewLine);
            sb.Append("               try" + Environment.NewLine);
            sb.Append("               {" + Environment.NewLine);
            sb.Append("                   bool bResult = true;" + Environment.NewLine);
            sb.Append("                   I" + sTableName + " infoDAL = " + sTableName + "Factory.Create(this.connStr, this.appCode);" + Environment.NewLine);
            sb.Append("                   foreach (string sNo in sNos)" + Environment.NewLine);
            sb.Append("                   {" + Environment.NewLine);
            sb.Append("                       if (infoDAL.SoftDeleteRecord(sNo) < 0)" + Environment.NewLine);
            sb.Append("                       {" + Environment.NewLine);
            sb.Append("                           bResult = false;" + Environment.NewLine);
            sb.Append("                           break;" + Environment.NewLine);
            sb.Append("                       }" + Environment.NewLine);
            sb.Append("                   }" + Environment.NewLine);
            sb.Append("                   return bResult;" + Environment.NewLine);
            sb.Append("               }" + Environment.NewLine);
            sb.Append("               catch (Exception ex)" + Environment.NewLine);
            sb.Append("               {" + Environment.NewLine);
            sb.Append("                  throw new Exception(\"出错提示:软删除多记录(SoftDeleteRecord|BLL)时出错; \" + ex.Message);" + Environment.NewLine);
            sb.Append("              }" + Environment.NewLine);
            sb.Append("           }" + Environment.NewLine);
            sb.Append("            return false;" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);

            sb.Append("        public bool HardDeleteRecord(string[] sNos)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            if (sNos != null && sNos.Length > 0)" + Environment.NewLine);
            sb.Append("           {" + Environment.NewLine);
            sb.Append("               try" + Environment.NewLine);
            sb.Append("               {" + Environment.NewLine);
            sb.Append("                   bool bResult = true;" + Environment.NewLine);
            sb.Append("                   I" + sTableName + " infoDAL = " + sTableName + "Factory.Create(this.connStr, this.appCode);" + Environment.NewLine);
            sb.Append("                   foreach (string sNo in sNos)" + Environment.NewLine);
            sb.Append("                   {" + Environment.NewLine);
            sb.Append("                       if (infoDAL.HardDeleteRecord(sNo) < 0)" + Environment.NewLine);
            sb.Append("                       {" + Environment.NewLine);
            sb.Append("                          bResult = false;" + Environment.NewLine);
            sb.Append("                           break;" + Environment.NewLine);
            sb.Append("                       }" + Environment.NewLine);
            sb.Append("                  }" + Environment.NewLine);
            sb.Append("                   return bResult;" + Environment.NewLine);
            sb.Append("              }" + Environment.NewLine);
            sb.Append("              catch (Exception ex)" + Environment.NewLine);
            sb.Append("              {" + Environment.NewLine);
            sb.Append("                  throw new Exception(\"出错提示:硬删除多记录(HardDeleteRecord|BLL)时出错; \" + ex.Message);" + Environment.NewLine);
            sb.Append("              }" + Environment.NewLine);
            sb.Append("          }" + Environment.NewLine);
            sb.Append("           return false;" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);


            sb.Append("public bool SoftDeleteByCondition(string sCondition)" + Environment.NewLine);
            sb.Append("{" + Environment.NewLine);
            sb.Append("    try" + Environment.NewLine);
            sb.Append("    {" + Environment.NewLine);
            sb.Append("        I" + sTableName + " infoDAL = " + sTableName + "Factory.Create(this.connStr, this.appCode);" + Environment.NewLine);
            sb.Append("        if (infoDAL.SoftDeleteByCondition(sCondition) > 0)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            return true;" + Environment.NewLine);
            sb.Append("       }" + Environment.NewLine);
            sb.Append("       return false;" + Environment.NewLine);
            sb.Append("    }" + Environment.NewLine);
            sb.Append("    catch (Exception ex)" + Environment.NewLine);
            sb.Append("    {" + Environment.NewLine);
            sb.Append("        throw new Exception(\"出错提示:软删除记录(SoftDeleteRecord|BLL)时出错; \" + ex.Message);" + Environment.NewLine);
            sb.Append("    }" + Environment.NewLine);
            sb.Append("}" + Environment.NewLine);


            sb.Append("public bool HardDeleteByCondition(string sCondition)" + Environment.NewLine);
            sb.Append("{" + Environment.NewLine);
            sb.Append("    try" + Environment.NewLine);
            sb.Append("    {" + Environment.NewLine);
            sb.Append("        I" + sTableName + " infoDAL = " + sTableName + "Factory.Create(this.connStr, this.appCode);" + Environment.NewLine);
            sb.Append("        if (infoDAL.HardDeleteByCondition(sCondition) > 0)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            return true;" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append("        return false;" + Environment.NewLine);
            sb.Append("    }" + Environment.NewLine);
            sb.Append("    catch (Exception ex)" + Environment.NewLine);
            sb.Append("    {" + Environment.NewLine);
            sb.Append("        throw new Exception(\"出错提示:软删除记录(SoftDeleteRecord|BLL)时出错; \" + ex.Message);" + Environment.NewLine);
            sb.Append("    }" + Environment.NewLine);
            sb.Append("}" + Environment.NewLine);

            sb.Append("        public   " + sTableName + "Collections GetAllRecords()" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                I" + sTableName + "  infoDAL = " + sTableName + "Factory.Create(this.connStr, this.appCode);" + Environment.NewLine);
            sb.Append("                return infoDAL.GetAllRecords();" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("               throw new Exception(\"出错提示:查询所有记录(GetAllRecords|BLL)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public " + sTableName + "Collections GetRecordsByClassNo(string sClassNo)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            if (sClassNo.Length > 0)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                try" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                  I" + sTableName + "  infoDAL = " + sTableName + "Factory.Create(this.connStr, this.appCode);" + Environment.NewLine);
            sb.Append("                    return infoDAL.GetRecordsByClassNo(sClassNo);" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("                catch (Exception ex)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    throw new Exception(\"出错提示:通过ClassNo查询记录(GetRecordsByClassNo|BLL)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            return null ;" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public  " + sTableName + " GetRecordByNo(string sNo)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            if (sNo.Length > 0)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                try" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                I" + sTableName + " infoDAL = " + sTableName + "Factory.Create(this.connStr, this.appCode);" + Environment.NewLine);
            sb.Append("                " + sTableName + "Collections infoList = infoDAL.GetRecordsByNo(sNo);" + Environment.NewLine);

            sb.Append("                if (infoList != null && infoList.Count > 0)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    return infoList.GetFirstOne();" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("                return null;" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("                catch (Exception ex)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    throw new Exception(\"出错提示:通过No查询记录(GetRecordsByNo|BLL)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            return null;" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public  string GetRecordNameByNo(string sNo)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            if (sNo.Length > 0)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                try" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                  I" + sTableName + "  infoDAL = " + sTableName + "Factory.Create(this.connStr, this.appCode);" + Environment.NewLine);
            sb.Append("                    return infoDAL.GetRecordNameByNo(sNo);" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("                catch (Exception ex)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    throw new Exception(\"出错提示:通过No查询名称(GetRecordNameByNo|BLL)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            return \"\";" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);


            sb.Append("        public " + sTableName + "Collections GetRecordsByPaging(ref int pageCount, int pageIndex, int pageSize, string condition)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("           try" + Environment.NewLine);
            sb.Append("           {" + Environment.NewLine);
            sb.Append("               I" + sTableName + " infoDAL = " + sTableName + "Factory.Create(this.connStr, this.appCode);" + Environment.NewLine);
            sb.Append("               int iPageSize = pageSize > 0 ? pageSize : 10;" + Environment.NewLine);
            sb.Append("               int iPageIndex = pageIndex;" + Environment.NewLine);
            sb.Append("               int iRCount = infoDAL.GetCountByCondition(condition);" + Environment.NewLine);
            sb.Append("               int iPageCount = CommonHelper.GetRoundingDevision(iRCount, iPageSize);" + Environment.NewLine);
            sb.Append("               pageCount = iPageCount;" + Environment.NewLine);

            sb.Append("               if (iPageCount < 1)" + Environment.NewLine);
            sb.Append("               {" + Environment.NewLine);
            sb.Append("                  iPageCount = 1;" + Environment.NewLine);
            sb.Append("               }" + Environment.NewLine);

            sb.Append("              if (iPageIndex < 1)" + Environment.NewLine);
            sb.Append("              {" + Environment.NewLine);
            sb.Append("                  iPageIndex = 1;" + Environment.NewLine);
            sb.Append("              }" + Environment.NewLine);
            sb.Append("              else if (iPageIndex > iPageCount)" + Environment.NewLine);
            sb.Append("              {" + Environment.NewLine);
            sb.Append("                  iPageIndex = iPageCount;" + Environment.NewLine);
            sb.Append("             }" + Environment.NewLine);

            sb.Append("             SqlModel s_model = new SqlModel();" + Environment.NewLine);

            sb.Append("             s_model.iPageNo = iPageIndex;" + Environment.NewLine);
            sb.Append("             s_model.iPageSize = iPageSize;" + Environment.NewLine);
            sb.Append("            s_model.sFields = \" * \";" + Environment.NewLine);
            sb.Append("            s_model.sCondition = condition;" + Environment.NewLine);
            sb.Append("            s_model.sOrderField = \"ID\";" + Environment.NewLine);
            sb.Append("            s_model.sOrderType = \"desc\";" + Environment.NewLine);
            sb.Append("             s_model.sTableName = \"" + sTableName + "\";" + Environment.NewLine);

            sb.Append("             return infoDAL.GetRecords_Paging(s_model);" + Environment.NewLine);
            sb.Append("         }" + Environment.NewLine);
            sb.Append("         catch (Exception ex)" + Environment.NewLine);
            sb.Append("         {" + Environment.NewLine);
            sb.Append("             throw new Exception(\"出错提示:分页查询记录(GetRecords_Paging|BLL)时出错; \" + ex.Message);" + Environment.NewLine);
            sb.Append("         }" + Environment.NewLine);
            sb.Append("         }" + Environment.NewLine);

            sb.Append("        public " + sTableName + "Collections GetRecordsByPaging(SqlModel s_model)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            if (s_model != null)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                try" + Environment.NewLine);
            sb.Append("               {" + Environment.NewLine);
            sb.Append("                   I" + sTableName + " infoDAL = " + sTableName + "Factory.Create(this.connStr, this.appCode);" + Environment.NewLine);
            sb.Append("                   return infoDAL.GetRecords_Paging(s_model);" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("               catch (Exception ex)" + Environment.NewLine);
            sb.Append("              {" + Environment.NewLine);
            sb.Append("                  throw new Exception(\"出错提示:分页查询记录(GetRecords_Paging|BLL)时出错; \" + ex.Message);" + Environment.NewLine);
            sb.Append("              }" + Environment.NewLine);
            sb.Append("           }" + Environment.NewLine);
            sb.Append("           return null;" + Environment.NewLine);
            sb.Append("         }" + Environment.NewLine);

            sb.Append("        public  int GetCountByCondition(string sCondition)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                  I" + sTableName + "  infoDAL = " + sTableName + "Factory.Create(this.connStr, this.appCode);" + Environment.NewLine);
            sb.Append("                return infoDAL.GetCountByCondition(sCondition);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                throw new Exception(\"出错提示:计算记录个数(GetCountByCondition|BLL)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append("    }" + Environment.NewLine);
            sb.Append("}" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            GenerateCode(sFileDir, sFileName, sb.ToString());
        }

        private void CreateIServiceClass(string sFileDir, string sFileName, string sTableName, string sNameSpace, DataTable dt)
        {
            StringBuilder sb = new StringBuilder();

            string camelCaseName = CamelCase(sTableName);

            sb.Append("using System;" + Environment.NewLine);
            sb.Append("using EntFrm.Business.BLL;" + Environment.NewLine);
            sb.Append("using EntFrm.Business.Model;" + Environment.NewLine);
            sb.Append("using EntFrm.Business.Model.Collections;" + Environment.NewLine);
            sb.Append("using EntFrm.Framework.Utility;" + Environment.NewLine);
            sb.Append("using System;" + Environment.NewLine);
            sb.Append("using System.Collections.Generic;" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("namespace  EntWeb.WebService.IBusinessLibs" + Environment.NewLine);
            sb.Append("{" + Environment.NewLine);
            sb.Append("    public class " + sTableName + "_IService" + Environment.NewLine);
            sb.Append("    {" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            
            sb.Append("        public   string " + sTableName + "_GetAllRecords()" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                " + sTableName + "BLL infoBLL = new " + sTableName + "BLL(IPublicHelper.Get_ConnStr(), IPublicHelper.Get_AppCode());" + Environment.NewLine);
            sb.Append("                " + sTableName + "Collections infoColl = infoBLL.GetAllRecords();" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("                List<List<string>> infoList = null;" + Environment.NewLine);
            sb.Append("                if (infoColl != null && infoColl.Count > 0)" + Environment.NewLine);
            sb.Append("                 {" + Environment.NewLine);
            sb.Append("                   infoList = new List<List<string>>();" + Environment.NewLine);
            sb.Append("                    foreach(" + sTableName + " info in infoColl)" + Environment.NewLine);
            sb.Append("                    {" + Environment.NewLine);
            sb.Append("                      infoList.Add(GetObjectList(info));" + Environment.NewLine);
            sb.Append("                     }" + Environment.NewLine);
            sb.Append("                 }" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("                  ReturnInfo retInfo = new ReturnInfo();" + Environment.NewLine);
            sb.Append("                 retInfo.ResultType = \"struct\";" + Environment.NewLine);
            sb.Append("                 retInfo.ResultList = infoList;" + Environment.NewLine);
            sb.Append("                 string strTemp = ISerializerMessageUtil.ConstructReturnInfos(retInfo);" + Environment.NewLine);
            sb.Append("                 return strTemp;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("               throw new Exception(\"出错提示:查询所有记录(GetAllRecords|BLL)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public   string " + sTableName + "_GetRecordsByClassNo(string sClassNo)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                " + sTableName + "BLL infoBLL = new " + sTableName + "BLL(IPublicHelper.Get_ConnStr(), IPublicHelper.Get_AppCode());" + Environment.NewLine);
            sb.Append("                " + sTableName + "Collections infoColl = infoBLL.GetRecordsByClassNo(sClassNo);" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("                List<List<string>> infoList = null;" + Environment.NewLine);
            sb.Append("                if (infoColl != null && infoColl.Count > 0)" + Environment.NewLine);
            sb.Append("                 {" + Environment.NewLine);
            sb.Append("                   infoList = new List<List<string>>();" + Environment.NewLine);
            sb.Append("                    foreach(" + sTableName + " info in infoColl)" + Environment.NewLine);
            sb.Append("                    {" + Environment.NewLine);
            sb.Append("                      infoList.Add(GetObjectList(info));" + Environment.NewLine);
            sb.Append("                     }" + Environment.NewLine);
            sb.Append("                 }" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("                  ReturnInfo retInfo = new ReturnInfo();" + Environment.NewLine);
            sb.Append("                 retInfo.ResultType = \"struct\";" + Environment.NewLine);
            sb.Append("                 retInfo.ResultList = infoList;" + Environment.NewLine);
            sb.Append("                 string strTemp = ISerializerMessageUtil.ConstructReturnInfos(retInfo);" + Environment.NewLine);
            sb.Append("                 return strTemp;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("               throw new Exception(\"出错提示:按sClassNo检索所有记录(GetRecordsByClassNo|BLL)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public   string " + sTableName + "_GetRecordNameByNo(string sNo)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                " + sTableName + "BLL infoBLL = new " + sTableName + "BLL(IPublicHelper.Get_ConnStr(), IPublicHelper.Get_AppCode());" + Environment.NewLine);
            sb.Append("                string sTemp = infoBLL.GetRecordNameByNo(sNo);" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("                List<List<string>> infoList = new List<List<string>>();" + Environment.NewLine);
            sb.Append("                List<string> info=new List<string>();" + Environment.NewLine);
            sb.Append("                info.Add(sTemp);" + Environment.NewLine);
            sb.Append("                infoList.Add(info); " + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("                  ReturnInfo retInfo = new ReturnInfo();" + Environment.NewLine);
            sb.Append("                 retInfo.ResultType = \"string\";" + Environment.NewLine);
            sb.Append("                 retInfo.ResultList = infoList;" + Environment.NewLine);
            sb.Append("                 string strTemp = ISerializerMessageUtil.ConstructReturnInfos(retInfo);" + Environment.NewLine);
            sb.Append("                 return strTemp;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("               throw new Exception(\"出错提示:按sNo检索名称(GetRecordNameByNo|BLL)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public   string " + sTableName + "_GetRecordByNo(string sNo)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                " + sTableName + "BLL infoBLL = new " + sTableName + "BLL(IPublicHelper.Get_ConnStr(), IPublicHelper.Get_AppCode());" + Environment.NewLine);
            sb.Append("               " + sTableName + " infoValue=infoBLL.GetRecordByNo(sNo);" + Environment.NewLine);
            sb.Append("               " + sTableName + "Collections infoColl = new " + sTableName + "Collections();" + Environment.NewLine);
            sb.Append("               infoColl.Add(infoValue);" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("                List<List<string>> infoList = null;" + Environment.NewLine);
            sb.Append("                if (infoColl != null && infoColl.Count > 0)" + Environment.NewLine);
            sb.Append("                 {" + Environment.NewLine);
            sb.Append("                   infoList = new List<List<string>>();" + Environment.NewLine);
            sb.Append("                    foreach(" + sTableName + " info in infoColl)" + Environment.NewLine);
            sb.Append("                    {" + Environment.NewLine);
            sb.Append("                      infoList.Add(GetObjectList(info));" + Environment.NewLine);
            sb.Append("                     }" + Environment.NewLine);
            sb.Append("                 }" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("                  ReturnInfo retInfo = new ReturnInfo();" + Environment.NewLine);
            sb.Append("                 retInfo.ResultType = \"struct\";" + Environment.NewLine);
            sb.Append("                 retInfo.ResultList = infoList;" + Environment.NewLine);
            sb.Append("                 string strTemp = ISerializerMessageUtil.ConstructReturnInfos(retInfo);" + Environment.NewLine);
            sb.Append("                 return strTemp;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("               throw new Exception(\"出错提示:按sNo检索所有记录(GetRecordByNo|BLL)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);


            string paramValues1 = "";
            string paramValues2 = "";
            int pos = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if(pos++==0)
                {
                    continue;
                }
                paramValues1 += "string " + GetCSharpType(dr["DataType"].ToString()).ToLower().Substring(0, 1) + dr["ColumnName"].ToString() + ",";
                paramValues2 +=  GetCSharpType(dr["DataType"].ToString()).ToLower().Substring(0, 1)+ dr["ColumnName"].ToString()+",";
            }
            paramValues1 = paramValues1.Substring(0, paramValues1.Length - 1);
            paramValues2 = paramValues2.Substring(0, paramValues2.Length - 1);

            sb.Append("        public   string " + sTableName + "_AddRecord(" + paramValues1 + ")" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                " + sTableName + "BLL infoBLL = new " + sTableName + "BLL(IPublicHelper.Get_ConnStr(), IPublicHelper.Get_AppCode());" + Environment.NewLine);
            sb.Append("               " + sTableName + "  o=PutObjectProperty(" + paramValues2 + ");" + Environment.NewLine);
            sb.Append("                bool b =  infoBLL.AddNewRecord(o);" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("                List<List<string>> infoList = new List<List<string>>();" + Environment.NewLine);
            sb.Append("                List<string> info=new List<string>();" + Environment.NewLine);
            sb.Append("                info.Add(b.ToString());" + Environment.NewLine);
            sb.Append("                infoList.Add(info); " + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("                 ReturnInfo retInfo = new ReturnInfo();" + Environment.NewLine);
            sb.Append("                 retInfo.ResultType = \"string\";" + Environment.NewLine);
            sb.Append("                 retInfo.ResultList = infoList;" + Environment.NewLine);
            sb.Append("                 string strTemp = ISerializerMessageUtil.ConstructReturnInfos(retInfo);" + Environment.NewLine);
            sb.Append("                 return strTemp;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("               throw new Exception(\"出错提示:添加记录(AddRecord|BLL)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public   string " + sTableName + "_UpdateRecord(" + paramValues1 + ")" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                " + sTableName + "BLL infoBLL = new " + sTableName + "BLL(IPublicHelper.Get_ConnStr(), IPublicHelper.Get_AppCode());" + Environment.NewLine);
            sb.Append("               " + sTableName + "  o=PutObjectProperty(" + paramValues2 + ");" + Environment.NewLine);
            sb.Append("                bool b =  infoBLL.UpdateRecord(o);" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("                List<List<string>> infoList = new List<List<string>>();" + Environment.NewLine);
            sb.Append("                List<string> info=new List<string>();" + Environment.NewLine);
            sb.Append("                info.Add(b.ToString());" + Environment.NewLine);
            sb.Append("                infoList.Add(info); " + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("                 ReturnInfo retInfo = new ReturnInfo();" + Environment.NewLine);
            sb.Append("                 retInfo.ResultType = \"string\";" + Environment.NewLine);
            sb.Append("                 retInfo.ResultList = infoList;" + Environment.NewLine);
            sb.Append("                 string strTemp = ISerializerMessageUtil.ConstructReturnInfos(retInfo);" + Environment.NewLine);
            sb.Append("                 return strTemp;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("               throw new Exception(\"出错提示:添加记录(AddRecord|BLL)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public   string " + sTableName + "_HardDeleteRecord(string sNo)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                " + sTableName + "BLL infoBLL = new " + sTableName + "BLL(IPublicHelper.Get_ConnStr(), IPublicHelper.Get_AppCode());" + Environment.NewLine);
            sb.Append("                bool b =  infoBLL.HardDeleteRecord(sNo);" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("                List<List<string>> infoList = new List<List<string>>();" + Environment.NewLine);
            sb.Append("                List<string> info=new List<string>();" + Environment.NewLine);
            sb.Append("                info.Add(b.ToString());" + Environment.NewLine);
            sb.Append("                infoList.Add(info); " + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("                 ReturnInfo retInfo = new ReturnInfo();" + Environment.NewLine);
            sb.Append("                 retInfo.ResultType = \"string\";" + Environment.NewLine);
            sb.Append("                 retInfo.ResultList = infoList;" + Environment.NewLine);
            sb.Append("                 string strTemp = ISerializerMessageUtil.ConstructReturnInfos(retInfo);" + Environment.NewLine);
            sb.Append("                 return strTemp;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("               throw new Exception(\"出错提示:硬删除记录(HardDeleteRecord|BLL)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public   string " + sTableName + "_SoftDeleteRecord(string sNo)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                " + sTableName + "BLL infoBLL = new " + sTableName + "BLL(IPublicHelper.Get_ConnStr(), IPublicHelper.Get_AppCode());" + Environment.NewLine);
            sb.Append("                bool b =  infoBLL.SoftDeleteRecord(sNo);" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("                List<List<string>> infoList = new List<List<string>>();" + Environment.NewLine);
            sb.Append("                List<string> info=new List<string>();" + Environment.NewLine);
            sb.Append("                info.Add(b.ToString());" + Environment.NewLine);
            sb.Append("                infoList.Add(info); " + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("                 ReturnInfo retInfo = new ReturnInfo();" + Environment.NewLine);
            sb.Append("                 retInfo.ResultType = \"string\";" + Environment.NewLine);
            sb.Append("                 retInfo.ResultList = infoList;" + Environment.NewLine);
            sb.Append("                 string strTemp = ISerializerMessageUtil.ConstructReturnInfos(retInfo);" + Environment.NewLine);
            sb.Append("                 return strTemp;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("               throw new Exception(\"出错提示:软删除记录(HardDeleteRecord|BLL)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public   string " + sTableName + "_GetRecordsByPaging(string sTableName, string sFields, string iPageNo, string iPageSize, string sCondition, string sOrderField, string sOrderType)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("              SqlModel s_model = new SqlModel();" + Environment.NewLine);
            sb.Append("              s_model.sTableName = sTableName;" + Environment.NewLine);
            sb.Append("              s_model.sFields = sFields;" + Environment.NewLine);
            sb.Append("              s_model.iPageNo = int.Parse(iPageNo);" + Environment.NewLine);
            sb.Append("              s_model.iPageSize = int.Parse(iPageSize);" + Environment.NewLine);
            sb.Append("              s_model.sCondition = sCondition;" + Environment.NewLine);
            sb.Append("              s_model.sOrderField = sOrderField;" + Environment.NewLine);
            sb.Append("              s_model.sOrderType = sOrderType;" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("                " + sTableName + "BLL infoBLL = new " + sTableName + "BLL(IPublicHelper.Get_ConnStr(), IPublicHelper.Get_AppCode());" + Environment.NewLine);
            sb.Append("                " + sTableName + "Collections infoColl = infoBLL.GetRecordsByPaging(s_model);" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("                List<List<string>> infoList = null;" + Environment.NewLine);
            sb.Append("                if (infoColl != null && infoColl.Count > 0)" + Environment.NewLine);
            sb.Append("                 {" + Environment.NewLine);
            sb.Append("                   infoList = new List<List<string>>();" + Environment.NewLine);
            sb.Append("                    foreach(" + sTableName + " info in infoColl)" + Environment.NewLine);
            sb.Append("                    {" + Environment.NewLine);
            sb.Append("                      infoList.Add(GetObjectList(info));" + Environment.NewLine);
            sb.Append("                     }" + Environment.NewLine);
            sb.Append("                 }" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("                  ReturnInfo retInfo = new ReturnInfo();" + Environment.NewLine);
            sb.Append("                 retInfo.ResultType = \"struct\";" + Environment.NewLine);
            sb.Append("                 retInfo.ResultList = infoList;" + Environment.NewLine);
            sb.Append("                 string strTemp = ISerializerMessageUtil.ConstructReturnInfos(retInfo);" + Environment.NewLine);
            sb.Append("                 return strTemp;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("               throw new Exception(\"出错提示:按分页检索所有记录(GetRecords_Paging|BLL)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public   string " + sTableName + "_GetCountByCondition(string sCondition)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                " + sTableName + "BLL infoBLL = new " + sTableName + "BLL(IPublicHelper.Get_ConnStr(), IPublicHelper.Get_AppCode());" + Environment.NewLine);
            sb.Append("                int i = infoBLL.GetCountByCondition(sCondition);" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("                List<List<string>> infoList = new List<List<string>>();" + Environment.NewLine);
            sb.Append("                List<string> info=new List<string>();" + Environment.NewLine);
            sb.Append("                info.Add(i.ToString());" + Environment.NewLine);
            sb.Append("                infoList.Add(info); " + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("                 ReturnInfo retInfo = new ReturnInfo();" + Environment.NewLine);
            sb.Append("                 retInfo.ResultType = \"string\";" + Environment.NewLine);
            sb.Append("                 retInfo.ResultList = infoList;" + Environment.NewLine);
            sb.Append("                 string strTemp = ISerializerMessageUtil.ConstructReturnInfos(retInfo);" + Environment.NewLine);
            sb.Append("                 return strTemp;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("               throw new Exception(\"出错提示:计算记录数量(HardDeleteRecord|BLL)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        private   " + sTableName + "  PutObjectProperty("+paramValues1+")" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("             " + sTableName + " info = new " + sTableName + "();" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            string stemmp="";
            pos = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if(pos++==0)
                {
                    continue;
                }
                if (pos == dt.Rows.Count)
                {
                    stemmp = GetCSharpType(dr["DataType"].ToString()).ToLower().Substring(0, 1) + dr["ColumnName"].ToString();
                    sb.Append("              info." + stemmp + "="+stemmp + ";" + Environment.NewLine);
                }
                else
                {
                    stemmp = GetCSharpType(dr["DataType"].ToString()).ToLower().Substring(0, 1) + dr["ColumnName"].ToString();
                    sb.Append("              info." + stemmp + "=" + GetCParseType(dr["DataType"].ToString()) + stemmp + (GetCParseTypeState(dr["DataType"].ToString()) ? ")" : "") + ";" + Environment.NewLine);
                }
            }
            sb.Append(Environment.NewLine);

            sb.Append("             return info;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("               throw ex;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        private  List<string> GetObjectList( " + sTableName + " info)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("              List<string> infos = new List<string>();" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            foreach (DataRow dr in dt.Rows)
            {
                sb.Append("              infos.Add(info." + GetCSharpType(dr["DataType"].ToString()).ToLower().Substring(0, 1) + dr["ColumnName"].ToString() + ".ToString());" + Environment.NewLine);
            }

            sb.Append(Environment.NewLine);
            sb.Append("            return infos;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("               throw ex;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("    }" + Environment.NewLine);
            sb.Append("}" + Environment.NewLine);

            GenerateCode(sFileDir, sFileName, sb.ToString());
        }
        
        private void CreateServiceInstruction(string sFileDir, string sFileName, string sTableName, string sNameSpace, DataTable dt)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<InstructionInfo AssemblyName=\"EntWeb.WebService\" ConstructorName=\"EntWeb.WebService.IBusinessLibs." + sTableName + "_IService\">");
            sb.Append("<InstructionName>" + sTableName + "_GetAllRecords</InstructionName>");
            sb.Append("<InstructionName>" + sTableName + "_GetRecordsByClassNo</InstructionName>");
            sb.Append("<InstructionName>" + sTableName + "_GetRecordNameByNo</InstructionName>");
            sb.Append("<InstructionName>" + sTableName + "_GetRecordByNo</InstructionName>");
            sb.Append("<InstructionName>" + sTableName + "_AddRecord</InstructionName>");
            sb.Append("<InstructionName>" + sTableName + "_UpdateRecord</InstructionName>");
            sb.Append("<InstructionName>" + sTableName + "_HardDeleteRecord</InstructionName>");
            sb.Append("<InstructionName>" + sTableName + "_SoftDeleteRecord</InstructionName>");
            sb.Append("<InstructionName>" + sTableName + "_GetRecordsByPaging</InstructionName>");
            sb.Append("<InstructionName>" + sTableName + "_GetCountByCondition</InstructionName>");
            sb.Append("</InstructionInfo>");
            sb.Append(Environment.NewLine);

            GenerateCode(sFileDir, sFileName, sb.ToString());
        }
        

        private void CreateStoredProcedures(string sFileDir, string sFileName, string sTableName, string sNameSpace, DataTable dt)
        {
            string sCode = "";
            GenerateCode(sFileDir, sFileName, sCode);
        }

        private void CreateEncryptClass(string sFileDir, string sFileName)
        {
            StringBuilder sb = new StringBuilder();

            
            sb.Append("    }" + Environment.NewLine);
            sb.Append("  }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            GenerateCode(sFileDir, sFileName, sb.ToString());
        }

        private void CreateSqlHelperClass(string sFileDir, string sFileName)
        {
            string sCode = "";
            GenerateCode(sFileDir, sFileName, sCode);
        }

        private void CreateSqlModelClass(string sFileDir, string sFileName)
        {
            string sCode = "";
            GenerateCode(sFileDir, sFileName, sCode);
        }

        private void CreateStringHelperClass(string sFileDir, string sFileName)
        {
            string sCode = "";
            GenerateCode(sFileDir, sFileName, sCode);
        }

        private void CreateXmlHelperClass(string sFileDir, string sFileName)
        {
            string sCode = "";
            GenerateCode(sFileDir, sFileName, sCode);
        }

        private void CreateCSVHelperClass(string sFileDir, string sFileName)
        {
            string sCode = "";
            GenerateCode(sFileDir, sFileName, sCode);
        }

        private void CreateFileHelperClass(string sFileDir, string sFileName)
        {
            string sCode = "";
            GenerateCode(sFileDir, sFileName, sCode);
        }

        private void CreateMathHelperClass(string sFileDir, string sFileName)
        {
            string sCode = "";
            GenerateCode(sFileDir, sFileName, sCode);
        }

        private void CreateWebHelperClass(string sFileDir, string sFileName)
        {
            string sCode = "";
            GenerateCode(sFileDir, sFileName, sCode);
        }

        private void CreateExcelHelperClass(string sFileDir, string sFileName)
        {
            string sCode = "";
            GenerateCode(sFileDir, sFileName, sCode);
        }

        private void btnGenSqlite_Click(object sender, EventArgs e)
        {
            string sTableName = cbTables.SelectedValue.ToString();
            string sNameSpace = txtNameSpace.Text.Trim();
            string sFileName = "";

                string sFileDir = Environment.CurrentDirectory.ToString() + @"\\" + sNameSpace + "SqliteAccess\\";
            DataTable dt = new DataTable();

            bool bSingleMode = rbSingle.Checked;

            if (bSingleMode)
            {
                GetTableInfo(sTableName, ref dt); 

                sFileName = sTableName + "Access.cs";
                CreateSqliteDALClass(sFileDir, sFileName, sTableName, sNameSpace + "SqliteAccess", dt);
            }
            else
            {
                for (int i = 0; i < cbTables.Items.Count; i++)
                {
                    cbTables.SelectedIndex = i;
                    sTableName = cbTables.SelectedValue.ToString();
                    GetTableInfo(sTableName, ref dt);

                    sFileName = sTableName + "Access.cs";
                    CreateSqliteDALClass(sFileDir, sFileName, sTableName, sNameSpace + "SqliteAccess", dt);
                }
            }
        }

        private void CreateSqliteDALClass(string sFileDir, string sFileName, string sTableName, string sNameSpace, DataTable dt)
        {
            StringBuilder sb = new StringBuilder();

            string camelCaseName = CamelCase(sTableName);

            sb.Append("using System;" + Environment.NewLine);
            sb.Append("using EntFrm.Common;" + Environment.NewLine);
            sb.Append("using EntFrm.Model;" + Environment.NewLine);
            sb.Append("using EntFrm.Model.Collections;" + Environment.NewLine);
            sb.Append("using System.Data;" + Environment.NewLine);
            sb.Append("using System.Data.Common;" + Environment.NewLine);
            sb.Append("using System.Data.SQLite;" + Environment.NewLine); 
            sb.Append(Environment.NewLine);

            sb.Append("namespace " + sNameSpace + Environment.NewLine);
            sb.Append("{" + Environment.NewLine);

            IList<string> sColNames = new List<string>();
            string sColName = "";
            string sVal1 = "";
            string sVal2 = "";
            string sVal3 = "";
            string sKeyField = "";
            foreach (DataRow dr in dt.Rows)
            {
                sColName = dr["ColumnName"].ToString(); 
                    sColNames.Add(sColName);
                    sVal1 += sColName + ",";
                    sVal2 += "@" + sColName + ",";
                    sVal3 += sColName + "=@" + sColName + ",";
                 
            }
            sVal1 = sVal1.Substring(3, sVal1.Length - 12);
            sVal2 = sVal2.Substring(4, sVal2.Length - 14);
            sVal3 = sVal3.Substring(7, sVal3.Length - 25);
            sKeyField = sColNames[1];

            sb.Append("  public class " + sTableName + "Access" + Environment.NewLine);
            sb.Append("  {" + Environment.NewLine);
            sb.Append("        #region sql" + Environment.NewLine);
            sb.Append("        private const string SQL_GET_ALL_RECORDS = @\"Select *  From " + sTableName + " Where AppCode like @AppCode And ValidityState=1\";" + Environment.NewLine);
            sb.Append("        private const string SQL_GET_RECORDS_BY_NO = @\"Select * From " + sTableName + " Where   AppCode like @AppCode And   ValidityState=1 And " + sKeyField + "=@" + sKeyField + "\";" + Environment.NewLine);
            sb.Append("        private const string SQL_GET_NAME_BY_NO = @\"Select Name From " + sTableName + " Where   AppCode like @AppCode And   ValidityState=1 And " + sKeyField + "=@" + sKeyField + "\";" + Environment.NewLine);
            sb.Append("        private const string SQL_ADD_RECORD = @\"Insert into " + sTableName + Environment.NewLine);
            sb.Append("                                              (" + sVal1 + ")" + Environment.NewLine);
            sb.Append("                                              values(" + sVal2 + ")\";" + Environment.NewLine);

            sb.Append("        private const string SQL_UPDATE_RECORD = @\"Update " + sTableName + " set" + Environment.NewLine);
            sb.Append("                                                 " + sVal3 + " " + Environment.NewLine);
            sb.Append("                                                 Where  AppCode like @AppCode And   ValidityState=1 And " + sKeyField + "=@" + sKeyField + "  And Version=@Version\";" + Environment.NewLine);

            sb.Append("        private const string SQL_HARD_DELETE_RECORD = @\"Delete From " + sTableName + " Where   AppCode like @AppCode And   " + sKeyField + "=@" + sKeyField + " \";" + Environment.NewLine);
            sb.Append("        private const string SQL_HARD_DELETE_RECORDS_BY_CONDITION = @\"Delete From " + sTableName + " Where   AppCode like @AppCode \";" + Environment.NewLine);

            sb.Append("        private const string SQL_SOFT_DELETE_RECORD = @\"Update " + sTableName + " set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 And " + sKeyField + "=@" + sKeyField + "\";" + Environment.NewLine);
            sb.Append("        private const string SQL_SOFT_DELETE_RECORDS_BY_CONDITION = @\"Update " + sTableName + "   set ValidityState=0 Where   AppCode like @AppCode \";" + Environment.NewLine);

            sb.Append("        private const string SQL_GET_RECORDS_BY_CLASSNO = @\"Select * From " + sTableName + " Where    AppCode like @AppCode And   ValidityState=1 And ClassNo=@ClassNo\";" + Environment.NewLine);

            sb.Append("        private const string SQL_GET_COUNT_BY_CONDITION = @\"Select Count(*) From " + sTableName + " Where   AppCode like @AppCode  And   ValidityState=1 \";" + Environment.NewLine);
            sb.Append("        #endregion" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        #region param" + Environment.NewLine);

            sKeyField = sKeyField.ToUpper();
            foreach (DataRow dr in dt.Rows)
            {
                sColName = dr["ColumnName"].ToString();
                if (!sColName.Equals("Version"))
                {
                    sb.Append("        private const string PARAM_" + sColName.ToUpper() + " = \"@" + sColName + "\";" + Environment.NewLine);
                }
            }
            sb.Append("        #endregion" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        private string connStr;" + Environment.NewLine);
            sb.Append("        private string appCode;" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public " + sTableName + "Access(string sConnStr,string sAppCode)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("           this.connStr = sConnStr;" + Environment.NewLine);
            sb.Append("           this.appCode = sAppCode;" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public " + sTableName + "Collections GetAllRecords()" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine); 
            sb.Append("            DbDataReader reader = null;" + Environment.NewLine);
            sb.Append("            " + sTableName + "Collections infos = null;" + Environment.NewLine);
            sb.Append("            " + sTableName + " info = null;" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                SQLiteParameter[] paras = new SQLiteParameter[]" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    new SQLiteParameter(PARAM_APPCODE,DbType.String)" + Environment.NewLine);
            sb.Append("                 };" + Environment.NewLine);
            sb.Append("                paras[0].Value = \"%\" + appCode + \";%\";" + Environment.NewLine);
            sb.Append(Environment.NewLine); 
            sb.Append("                reader = SQLiteHelper.ExecuteReader(connStr, CommandType.Text, SQL_GET_ALL_RECORDS, paras);" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("                if (reader.HasRows)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    infos = new " + sTableName + "Collections();" + Environment.NewLine);
            sb.Append("                    while (reader.Read())" + Environment.NewLine);
            sb.Append("                    {" + Environment.NewLine);
            sb.Append("                        info = new " + sTableName + "();" + Environment.NewLine);

            sb.Append("                        // 设置对象属性" + Environment.NewLine);
            sb.Append("                        PutObjectProperty(info, reader);" + Environment.NewLine);

            sb.Append("                        infos.Add(info);" + Environment.NewLine);
            sb.Append("                    }" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);

            sb.Append("                return infos;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                throw new Exception(\" 查询所有记录(DAL层|GetAllRecords)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            finally" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                if (reader != null)" + Environment.NewLine);
            sb.Append("                    ((IDisposable)reader).Dispose();" + Environment.NewLine);
             
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public " + sTableName + "Collections GetRecordsByClassNo(string sClassNo)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine); 
            sb.Append("            /*DbDataReader reader = null;" + Environment.NewLine);
            sb.Append("            " + sTableName + "Collections infos = null;" + Environment.NewLine);
            sb.Append("            " + sTableName + " info = null;" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                SQLiteParameter[] paras = new SQLiteParameter[]" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    new SQLiteParameter(PARAM_CLASSNO,DbType.String)," + Environment.NewLine);
            sb.Append("                    new SQLiteParameter(PARAM_APPCODE,DbType.String)" + Environment.NewLine);
            sb.Append("                };" + Environment.NewLine);
            sb.Append("                paras[0].Value = sClassNo;" + Environment.NewLine);
            sb.Append("                paras[1].Value = \"%\" + appCode + \";%\";" + Environment.NewLine);
            sb.Append(Environment.NewLine);
             

            sb.Append("                reader = SQLiteHelper.ExecuteReader(connStr, CommandType.Text, SQL_GET_RECORDS_BY_CLASSNO,paras);" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("                if (reader.HasRows)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    infos = new " + sTableName + "Collections();" + Environment.NewLine);
            sb.Append("                    while (reader.Read())" + Environment.NewLine);
            sb.Append("                    {" + Environment.NewLine);
            sb.Append("                        info = new " + sTableName + "();" + Environment.NewLine);

            sb.Append("                        // 设置对象属性" + Environment.NewLine);
            sb.Append("                        PutObjectProperty(info, reader);" + Environment.NewLine);

            sb.Append("                        infos.Add(info);" + Environment.NewLine);
            sb.Append("                    }" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);

            sb.Append("                return infos;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                throw new Exception(\" 通过sClassNo查询记录(DAL层)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            finally" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                if (reader != null)" + Environment.NewLine);
            sb.Append("                    ((IDisposable)reader).Dispose();" + Environment.NewLine); 
            sb.Append("            }*/" + Environment.NewLine);
            sb.Append("            return null;" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public " + sTableName + "Collections GetRecordsByNo(string sNo)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine); 
            sb.Append("            DbDataReader reader = null;" + Environment.NewLine);
            sb.Append("            " + sTableName + "Collections infos = null;" + Environment.NewLine);
            sb.Append("            " + sTableName + " info = null;" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                SQLiteParameter[] paras = new SQLiteParameter[]" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    new SQLiteParameter(PARAM_" + sKeyField + ",DbType.String)," + Environment.NewLine);
            sb.Append("                    new SQLiteParameter(PARAM_APPCODE,DbType.String)" + Environment.NewLine);
            sb.Append("                };" + Environment.NewLine);
            sb.Append("                paras[0].Value = sNo;" + Environment.NewLine);
            sb.Append("                paras[1].Value = \"%\" + appCode + \";%\";" + Environment.NewLine);
            sb.Append(Environment.NewLine);
             
            sb.Append("                reader = SQLiteHelper.ExecuteReader(connStr, CommandType.Text, SQL_GET_RECORDS_BY_NO, paras);" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("                if (reader.HasRows)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    infos = new " + sTableName + "Collections();" + Environment.NewLine);
            sb.Append("                    while (reader.Read())" + Environment.NewLine);
            sb.Append("                    {" + Environment.NewLine);
            sb.Append("                        info = new " + sTableName + "();" + Environment.NewLine);

            sb.Append("                        //设置对象属性" + Environment.NewLine);
            sb.Append("                        PutObjectProperty(info, reader);" + Environment.NewLine);

            sb.Append("                        infos.Add(info);" + Environment.NewLine);
            sb.Append("                    }" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);

            sb.Append("                return infos;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                throw new Exception(\" 通过No查询记录(DAL层)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            finally" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                if (reader != null)" + Environment.NewLine);
            sb.Append("                    ((IDisposable)reader).Dispose();" + Environment.NewLine);
             
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public string GetRecordNameByNo(string sNo)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine); 

            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                SQLiteParameter[] paras = new SQLiteParameter[]" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    new SQLiteParameter(PARAM_" + sKeyField + ",DbType.String)," + Environment.NewLine);
            sb.Append("                    new SQLiteParameter(PARAM_APPCODE,DbType.String)" + Environment.NewLine);
            sb.Append("                };" + Environment.NewLine);
            sb.Append("                paras[0].Value = sNo;" + Environment.NewLine);
            sb.Append("                paras[1].Value = \"%\" + appCode + \";%\";" + Environment.NewLine);
            sb.Append(Environment.NewLine);
             
            sb.Append("                return (string)SQLiteHelper.ExecuteScalar(connStr, CommandType.Text, SQL_GET_NAME_BY_NO, paras);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                throw new Exception(\" 通过No查询记录名称(DAL层)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            finally" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine); 
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public int AddRecord(" + sTableName + " info)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine); 
            sb.Append(Environment.NewLine);

            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                SQLiteParameter[] paras = new SQLiteParameter[]" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);

            int pos = 0;
            int rcount = dt.Rows.Count;
            foreach (DataRow dr in dt.Rows)
            {
                if (pos++ == 0)
                {
                    continue;
                }

                if (pos == rcount)
                {
                    break;
                }
                else if (pos == rcount - 1)
                {
                    sb.Append("                    new SQLiteParameter(PARAM_" + dr["ColumnName"].ToString().ToUpper() + "," + GetSqliteType(dr["DataType"].ToString(), dr["ColumnSize"].ToString()) + ")" + Environment.NewLine);
                }
                else
                {
                    sb.Append("                    new SQLiteParameter(PARAM_" + dr["ColumnName"].ToString().ToUpper() + "," + GetSqliteType(dr["DataType"].ToString(), dr["ColumnSize"].ToString()) + ")," + Environment.NewLine);
                }
            }
            sb.Append("                };" + Environment.NewLine);

            int index = 0;
            pos = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (pos++ == 0)
                {
                    continue;
                }


                if (pos == rcount)
                {
                    break;
                }
                else
                {
                    sb.Append("                paras[" + index.ToString() + "].Value = info." + GetCSharpType(dr["DataType"].ToString()).ToLower().Substring(0, 1) + dr["ColumnName"].ToString() + ";" + Environment.NewLine);
                }
                index++;

            }
            sb.Append(Environment.NewLine);
             
            sb.Append("                return SQLiteHelper.ExecuteNonQuery(connStr, CommandType.Text, SQL_ADD_RECORD, paras);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                throw new Exception(\" 添加(DAL层)记录时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            finally" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine); 
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public int UpdateRecord(" + sTableName + " info)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine); 
            sb.Append(Environment.NewLine);

            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                SQLiteParameter[] paras = new SQLiteParameter[]" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);

            pos = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (pos++ == 0)
                {
                    continue;
                }

                if (pos == rcount)
                {
                    sb.Append("                    new SQLiteParameter(PARAM_" + dr["ColumnName"].ToString().ToUpper() + "," + GetSqliteType(dr["DataType"].ToString(), dr["ColumnSize"].ToString()) + ")" + Environment.NewLine);
                }
                else
                {
                    sb.Append("                    new SQLiteParameter(PARAM_" + dr["ColumnName"].ToString().ToUpper() + "," + GetSqliteType(dr["DataType"].ToString(), dr["ColumnSize"].ToString()) + ")," + Environment.NewLine);
                }
            }
            sb.Append("                };" + Environment.NewLine);

            index = 0;
            pos = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (pos++ == 0)
                {
                    continue;
                }


                if (pos == rcount)
                {
                    sb.Append("                paras[" + index.ToString() + "].Value = Common_StringHelper.ConvertToBytes(info." + GetCSharpType(dr["DataType"].ToString()).ToLower().Substring(0, 1) + dr["ColumnName"].ToString() + ");" + Environment.NewLine);
                }
                else
                {
                    sb.Append("                paras[" + index.ToString() + "].Value = info." + GetCSharpType(dr["DataType"].ToString()).ToLower().Substring(0, 1) + dr["ColumnName"].ToString() + ";" + Environment.NewLine);
                }
                index++;
            }
            sb.Append(Environment.NewLine);
             
            sb.Append("                return SQLiteHelper.ExecuteNonQuery(connStr, CommandType.Text, SQL_UPDATE_RECORD, paras);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                throw new Exception(\" 更新记录(DAL层)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            finally" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine); 
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public int HardDeleteRecord(string sNo)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine); 
            sb.Append(Environment.NewLine);

            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                SQLiteParameter[] paras = new SQLiteParameter[]" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    new SQLiteParameter(PARAM_" + sKeyField + ",DbType.String)," + Environment.NewLine);
            sb.Append("                    new SQLiteParameter(PARAM_APPCODE,DbType.String)" + Environment.NewLine);
            sb.Append("                };" + Environment.NewLine);
            sb.Append("                paras[0].Value = sNo;" + Environment.NewLine);
            sb.Append("                paras[1].Value = \"%\" + appCode + \";%\";" + Environment.NewLine);
            sb.Append(Environment.NewLine);
             
            sb.Append("                return SQLiteHelper.ExecuteNonQuery(connStr, CommandType.Text, SQL_HARD_DELETE_RECORD, paras);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                throw new Exception(\" 硬删除记录(DAL层)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            finally" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine); 
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);


            sb.Append("        public int HardDeleteRecordsBy(string sCondition)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                SQLiteParameter[] paras = new SQLiteParameter[]" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine); 
            sb.Append("                    new SQLiteParameter(PARAM_APPCODE,DbType.String)" + Environment.NewLine);
            sb.Append("                };" + Environment.NewLine); 
            sb.Append("                paras[0].Value = \"%\" + appCode + \";%\";" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("            string strSql = SQL_HARD_DELETE_RECORDS_BY_CONDITION;" + Environment.NewLine);
            sb.Append("            if (sCondition.Length > 0)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("              strSql += \" And \" + sCondition;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);

            sb.Append("                return SQLiteHelper.ExecuteNonQuery(connStr, CommandType.Text, strSql, paras);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                throw new Exception(\" 硬删除多记录(DAL层)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            finally" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);


            sb.Append("        public int SoftDeleteRecord(string sNo)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine); 
            sb.Append(Environment.NewLine);

            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                SQLiteParameter[] paras = new SQLiteParameter[]" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    new SQLiteParameter(PARAM_" + sKeyField + ",DbType.String)," + Environment.NewLine);
            sb.Append("                    new SQLiteParameter(PARAM_APPCODE,DbType.String)" + Environment.NewLine);
            sb.Append("                };" + Environment.NewLine);
            sb.Append("                paras[0].Value = sNo;" + Environment.NewLine);
            sb.Append("                paras[1].Value = \"%\" + appCode + \";%\";" + Environment.NewLine);
            sb.Append(Environment.NewLine);
             
            sb.Append("                return SQLiteHelper.ExecuteNonQuery(connStr, CommandType.Text, SQL_SOFT_DELETE_RECORD, paras);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                throw new Exception(\" 软删除记录(DAL层)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            finally" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine); 
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);



            sb.Append("        public int SoftDeleteRecordsBy(string sCondition)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                SQLiteParameter[] paras = new SQLiteParameter[]" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    new SQLiteParameter(PARAM_APPCODE,DbType.String)" + Environment.NewLine);
            sb.Append("                };" + Environment.NewLine);
            sb.Append("                paras[0].Value = \"%\" + appCode + \";%\";" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("            string strSql = SQL_SOFT_DELETE_RECORDS_BY_CONDITION;" + Environment.NewLine);
            sb.Append("            if (sCondition.Length > 0)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("              strSql += \" And \" + sCondition;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);

            sb.Append("                return SQLiteHelper.ExecuteNonQuery(connStr, CommandType.Text, strSql, paras);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                throw new Exception(\" 软删除多记录(DAL层)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            finally" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public " + sTableName + "Collections GetRecords_Paging(SqlModel s_model)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine); 
            sb.Append("            DbDataReader reader = null;" + Environment.NewLine);
            sb.Append("            " + sTableName + "Collections infos = null;" + Environment.NewLine);
            sb.Append("            " + sTableName + " info = null;" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                 if (s_model.sCondition.Length==0)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    s_model.sCondition = \" Where  AppCode like '%\" + appCode + \";%' And ValidityState=1\";" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("                else" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    s_model.sCondition = \" Where   AppCode like '%\" + appCode + \";%' And ValidityState=1 And  \" + s_model.sCondition;" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            //sb.Append("                s_model.sTableName = \"" + sTableName + "\";" + Environment.NewLine);
            sb.Append(Environment.NewLine); 

            sb.Append("                reader = SQLiteHelper.SelectPaging(connStr, s_model.sTableName, s_model.sFields, s_model.sCondition, s_model.sOrderField,s_model.sOrderType, s_model.iPageSize, s_model.iPageNo);" + Environment.NewLine);

            sb.Append("                if (reader.HasRows)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    infos = new " + sTableName + "Collections();" + Environment.NewLine);
            sb.Append("                    while (reader.Read())" + Environment.NewLine);
            sb.Append("                    {" + Environment.NewLine);
            sb.Append("                        info = new " + sTableName + "();" + Environment.NewLine);

            sb.Append("                        // 设置对象属性" + Environment.NewLine);
            sb.Append("                        PutObjectProperty(info, reader);" + Environment.NewLine);

            sb.Append("                        infos.Add(info);" + Environment.NewLine);
            sb.Append("                    }" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);

            sb.Append("                return infos;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                throw new Exception(\" 分页查询(DAL层)记录时出错;;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            finally" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                if (reader != null)" + Environment.NewLine);
            sb.Append("                    ((IDisposable)reader).Dispose();" + Environment.NewLine);
             
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);


            sb.Append("        public " + sTableName + "Collections GetRecordsByPaging(int pageIndex, int pageSize, string condition)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            DbDataReader reader = null;" + Environment.NewLine);
            sb.Append("            " + sTableName + "Collections infos = null;" + Environment.NewLine);
            sb.Append("            " + sTableName + " info = null;" + Environment.NewLine);
            sb.Append("            SqlModel s_model = null;" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("            s_model = new SqlModel();" + Environment.NewLine);
            sb.Append("            s_model.sTableName = \""+sTableName+"\";" + Environment.NewLine);
            sb.Append("            s_model.sFields = \" * \";" + Environment.NewLine);
            sb.Append("            s_model.sOrderField = \"ID\";" + Environment.NewLine);
            sb.Append("            s_model.sOrderType = \"Asc\";" + Environment.NewLine);
            sb.Append("            s_model.iPageNo = pageIndex;" + Environment.NewLine);
            sb.Append("            s_model.iPageSize = pageSize;" + Environment.NewLine);
            sb.Append("            s_model.sCondition = condition;" + Environment.NewLine);
            sb.Append("            " + Environment.NewLine);
            sb.Append("                 if (s_model.sCondition.Length==0)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    s_model.sCondition = \" Where  AppCode like '%\" + appCode + \";%' And ValidityState=1\";" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("                else" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    s_model.sCondition = \" Where   AppCode like '%\" + appCode + \";%' And ValidityState=1 And  \" + s_model.sCondition;" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            //sb.Append("                s_model.sTableName = \"" + sTableName + "\";" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("                reader = SQLiteHelper.SelectPaging(connStr, s_model.sTableName, s_model.sFields, s_model.sCondition, s_model.sOrderField,s_model.sOrderType, s_model.iPageSize, s_model.iPageNo);" + Environment.NewLine);

            sb.Append("                if (reader.HasRows)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    infos = new " + sTableName + "Collections();" + Environment.NewLine);
            sb.Append("                    while (reader.Read())" + Environment.NewLine);
            sb.Append("                    {" + Environment.NewLine);
            sb.Append("                        info = new " + sTableName + "();" + Environment.NewLine);

            sb.Append("                        // 设置对象属性" + Environment.NewLine);
            sb.Append("                        PutObjectProperty(info, reader);" + Environment.NewLine);

            sb.Append("                        infos.Add(info);" + Environment.NewLine);
            sb.Append("                    }" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);

            sb.Append("                return infos;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                throw new Exception(\" 分页查询(DAL层)记录时出错;;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            finally" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                if (reader != null)" + Environment.NewLine);
            sb.Append("                    ((IDisposable)reader).Dispose();" + Environment.NewLine);

            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public int GetCountByCondition(string sCondition)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine); 
            sb.Append(Environment.NewLine);

            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                string strSql = SQL_GET_COUNT_BY_CONDITION;" + Environment.NewLine);
            sb.Append("                if(sCondition.Length>0)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    strSql +=\"  And \" + sCondition;" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("                SQLiteParameter[] paras = new SQLiteParameter[]" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                   new SQLiteParameter(PARAM_APPCODE,DbType.String)" + Environment.NewLine);
            sb.Append("                };" + Environment.NewLine);
            sb.Append("                paras[0].Value = \"%\" + appCode + \";%\";" + Environment.NewLine);
            sb.Append(Environment.NewLine);
             

            sb.Append("                return Convert.ToInt32(SQLiteHelper.ExecuteScalar(connStr, CommandType.Text, strSql, paras));" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                throw new Exception(\" 计算记录总数(DAL层)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            finally" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine); 
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        #region PutObjectProperty 设置对象属性" + Environment.NewLine);
            sb.Append("        /// <summary>" + Environment.NewLine);
            sb.Append("        /// 从 SqlDataReader 类对象中读取并设置对象属性" + Environment.NewLine);
            sb.Append("        /// </summary>" + Environment.NewLine);
            sb.Append("       /// <param name=\" obj_info\">主题对象</param>" + Environment.NewLine);
            sb.Append("        /// <param name=\"dr\">读入数据</param>" + Environment.NewLine);
            sb.Append("        internal static void PutObjectProperty(" + sTableName + " obj_info, DbDataReader reader)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);

            foreach (DataRow dr in dt.Rows)
            {
                sb.Append("            obj_info." + GetCSharpType(dr["DataType"].ToString()).ToLower().Substring(0, 1) + dr["ColumnName"].ToString() + "= " + GetCParseType(dr["DataType"].ToString()) + "reader[\"" + dr["ColumnName"].ToString() + "\"].ToString()" + (GetCParseTypeState(dr["DataType"].ToString()) ? ")" : "") + ";" + Environment.NewLine);
            }
            sb.Append("        }" + Environment.NewLine);
            sb.Append("        #endregion" + Environment.NewLine);
            sb.Append("    }" + Environment.NewLine);
            sb.Append("}" + Environment.NewLine);

            GenerateCode(sFileDir, sFileName, sb.ToString());
        }

        private void btnGenBuss_Click(object sender, EventArgs e)
        {
            string sTableName = cbTables.SelectedValue.ToString();
            string sNameSpace = txtNameSpace.Text.Trim();
            string sFileName = "";

            string sFileDir = Environment.CurrentDirectory.ToString() + @"\\" + sNameSpace + "Business\\";
            DataTable dt = new DataTable();

            bool bSingleMode = rbSingle.Checked;

            if (bSingleMode)
            {
                GetTableInfo(sTableName, ref dt);
                 
                sFileName = sTableName + "Business.cs";
                CreateSqliteBusiness(sFileDir, sFileName, sTableName, sNameSpace + "Business", dt);
            }
            else
            {
                for (int i = 0; i < cbTables.Items.Count; i++)
                {
                    cbTables.SelectedIndex = i;
                    sTableName = cbTables.SelectedValue.ToString();
                    GetTableInfo(sTableName, ref dt);

                    sFileName = sTableName + "Business.cs";
                    CreateSqliteBusiness(sFileDir, sFileName, sTableName, sNameSpace + "Business", dt);
                }
            }
        }


        private void CreateSqliteBusiness(string sFileDir, string sFileName, string sTableName, string sNameSpace, DataTable dt)
        {
            StringBuilder sb = new StringBuilder();

            string camelCaseName = CamelCase(sTableName);

            sb.Append("using System;" + Environment.NewLine);
            sb.Append("using EntFrm.BLL;" + Environment.NewLine);
            sb.Append("using EntFrm.Common;" + Environment.NewLine);
            sb.Append("using EntFrm.Model;" + Environment.NewLine);
            sb.Append("using EntFrm.Model.Collections;" + Environment.NewLine);
            sb.Append("using EntFrm.SqliteAccess;" + Environment.NewLine);
            sb.Append("using System;" + Environment.NewLine);
            sb.Append(Environment.NewLine); 

            sb.Append("namespace " + sNameSpace + Environment.NewLine);
            sb.Append("{" + Environment.NewLine);
            sb.Append("    public class " + sTableName + "Business" + Environment.NewLine);
            sb.Append("    {" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        private string connStr;" + Environment.NewLine);
            sb.Append("        private string appCode;" + Environment.NewLine);
            sb.Append("        private bool isSqlite;" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public  void Init()" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public  " + sTableName + "Business(string sConnStr, string sAppCode,bool isSqlite=false)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("           this.connStr = sConnStr;" + Environment.NewLine);
            sb.Append("           this.appCode = sAppCode;" + Environment.NewLine);
            sb.Append("           this.isSqlite = isSqlite;" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        private  void ValidateRequired(" + sTableName + " info)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            /*if (info != null)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                 if (info.sName.Trim().Length == 0)" + Environment.NewLine);
            sb.Append("                 {" + Environment.NewLine);
            sb.Append("                   throw new Exception(\"出错提示:Name不能为空;\");" + Environment.NewLine);
            sb.Append("                 }" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            else{" + Environment.NewLine);
            sb.Append("                throw new Exception(\"出错提示:对象不能为空;\");" + Environment.NewLine);
            sb.Append("            }*/" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        private  void ValidateRepeat(" + sTableName + "  info)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            /*if (info != null)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                 try" + Environment.NewLine);
            sb.Append("                 {" + Environment.NewLine);
            sb.Append("                     I" + sTableName + "  infoDAL = " + sTableName + "Factory.Create(this.connStr, this.appCode);" + Environment.NewLine);
            sb.Append("                    if (infoDAL.GetCountByCondition(\" Name='\" + info.sName.Trim() + \"'\") > 0)" + Environment.NewLine);
            sb.Append("                    {" + Environment.NewLine);
            sb.Append("                        throw new Exception(\"Name存在重复记录;\");" + Environment.NewLine);
            sb.Append("                    }" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("                catch (Exception ex)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    throw new Exception(\"出错提示:\" + ex.Message);" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            else{" + Environment.NewLine);
            sb.Append("                throw new Exception(\"出错提示:对象不能为空;\");" + Environment.NewLine);
            sb.Append("            }*/" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public  bool AddNewRecord(" + sTableName + " info)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                 //ValidateRequired(info);" + Environment.NewLine);
            sb.Append("                 //ValidateRepeat(info);" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("                if (isSqlite)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    " + sTableName + "Access infoAccess = new " + sTableName + "Access(connStr, appCode);" + Environment.NewLine);
            sb.Append("                    if (infoAccess.AddRecord(info) > 0)" + Environment.NewLine);
            sb.Append("                    {" + Environment.NewLine);
            sb.Append("                       return true;" + Environment.NewLine);
            sb.Append("                    }" + Environment.NewLine);
            sb.Append("                    return false;" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("                else" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);

            sb.Append("                " + sTableName + "BLL infoBLL = new " + sTableName + "BLL(connStr, appCode);" + Environment.NewLine);
            sb.Append("                if (infoBLL.AddNewRecord(info) > 0)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                  return true;" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("                  return false;" + Environment.NewLine);
                sb.Append("            }" + Environment.NewLine);
                sb.Append("         }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                throw new Exception(\"出错提示:添加新记录(AddNewRecord|Business)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public  bool UpdateRecord(" + sTableName + " info)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                 //ValidateRequired(info);" + Environment.NewLine);

            sb.Append("            if (isSqlite)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("               " + sTableName + "Access infoAccess = new " + sTableName + "Access(connStr, appCode);" + Environment.NewLine);
            sb.Append("               if (infoAccess.UpdateRecord(info) > 0)" + Environment.NewLine);
            sb.Append("               {" + Environment.NewLine);
            sb.Append("                  return true; " + Environment.NewLine);
            sb.Append("              }" + Environment.NewLine);

            sb.Append("               return false;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            else" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);

            sb.Append("                " + sTableName + "BLL infoBLL = new " + sTableName + "BLL(connStr, appCode);" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("                if (infoBLL.UpdateRecord(info) > 0)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                  return true;" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("                  return false;" + Environment.NewLine);

            sb.Append("               }" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                throw new Exception(\"出错提示:更新记录(UpdateRecord|Business)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public  bool SoftDeleteRecord(string sNo)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            if (sNo.Length > 0)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                try" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);

            sb.Append("                if (isSqlite)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    " + sTableName + "Access infoAccess = new " + sTableName + "Access(connStr, appCode);" + Environment.NewLine);
            sb.Append("                    if (infoAccess.SoftDeleteRecord(sNo) > 0)" + Environment.NewLine);
            sb.Append("                    {" + Environment.NewLine);
            sb.Append("                       return true;" + Environment.NewLine);
            sb.Append("                   }" + Environment.NewLine);
            sb.Append("                    return false;" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("                else" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);

            sb.Append("                " + sTableName + "BLL infoBLL = new " + sTableName + "BLL(connStr, appCode);" + Environment.NewLine);
            sb.Append("                 if (infoBLL.SoftDeleteRecord(sNo) > 0)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                  return true;" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("                  return false;" + Environment.NewLine);

            sb.Append("                  }" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("                catch (Exception ex)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    throw new Exception(\"出错提示:软删除记录(SoftDeleteRecord|Business)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            return false;" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public bool HardDeleteRecord(string sNo)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            if (sNo.Length > 0)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                try" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);

            sb.Append("                if (isSqlite)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                   " + sTableName + "Access infoAccess = new " + sTableName + "Access(connStr, appCode);" + Environment.NewLine);
            sb.Append("                    if (infoAccess.HardDeleteRecord(sNo) > 0)" + Environment.NewLine);
            sb.Append("                   {" + Environment.NewLine);
            sb.Append("                       return true;" + Environment.NewLine);
            sb.Append("                   }" + Environment.NewLine);
            sb.Append("                    return false;" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("                else" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);

            sb.Append("                " + sTableName + "BLL infoBLL = new " + sTableName + "BLL(connStr, appCode);" + Environment.NewLine);
            sb.Append("                 if (infoBLL.HardDeleteRecord(sNo) > 0)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                  return true;" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("                  return false;" + Environment.NewLine);

            sb.Append("                  }" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("                catch (Exception ex)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    throw new Exception(\"出错提示:硬删除记录(HardDeleteRecord|Business)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            return false;" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public   " + sTableName + "Collections GetAllRecords()" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("            if (isSqlite)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                " + sTableName + "Access infoAccess = new " + sTableName + "Access(connStr, appCode);" + Environment.NewLine);
            sb.Append("               return infoAccess.GetAllRecords();" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            else" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                " + sTableName + "BLL infoBLL = new " + sTableName + "BLL(connStr, appCode);" + Environment.NewLine);
            sb.Append("                return infoBLL.GetAllRecords();" + Environment.NewLine);
            sb.Append("              }" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("               throw new Exception(\"出错提示:查询所有记录(GetAllRecords|Business)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public " + sTableName + "Collections GetRecordsByClassNo(string sClassNo)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            if (sClassNo.Length > 0)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                try" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                if (isSqlite)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                   " + sTableName + "Access infoAccess = new " + sTableName + "Access(connStr, appCode);" + Environment.NewLine);
            sb.Append("                    return infoAccess.GetRecordsByClassNo(sClassNo);" + Environment.NewLine);
            sb.Append("                 }" + Environment.NewLine);
            sb.Append("                else" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    " + sTableName + "BLL infoBLL = new " + sTableName + "BLL(connStr, appCode);" + Environment.NewLine);
            sb.Append("                    return infoBLL.GetRecordsByClassNo(sClassNo);" + Environment.NewLine);
            sb.Append("                  }" + Environment.NewLine);
            sb.Append("                 }" + Environment.NewLine);
            sb.Append("                catch (Exception ex)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    throw new Exception(\"出错提示:通过ClassNo查询记录(GetRecordsByClassNo|Business)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            return null ;" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public  " + sTableName + " GetRecordByNo(string sNo)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            if (sNo.Length > 0)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                try" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                if (isSqlite)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                   " + sTableName + "Access infoAccess = new " + sTableName + "Access(connStr, appCode);" + Environment.NewLine);
            sb.Append("                   " + sTableName + "Collections infoColl = infoAccess.GetRecordsByNo(sNo);" + Environment.NewLine);

            sb.Append("                   if (infoColl != null && infoColl.Count > 0)" + Environment.NewLine);
            sb.Append("                   {" + Environment.NewLine);
            sb.Append("                      return infoColl.GetFirstOne();" + Environment.NewLine);
            sb.Append("                   }" + Environment.NewLine);
            sb.Append("                   return null;" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("                else" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                " + sTableName + "BLL infoBLL = new " + sTableName + "BLL(connStr, appCode);" + Environment.NewLine);
            sb.Append("                    return infoBLL.GetRecordByNo(sNo);" + Environment.NewLine);
            sb.Append("                 }" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("                catch (Exception ex)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    throw new Exception(\"出错提示:通过No查询记录(GetRecordsByNo|Business)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            return null;" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public  string GetRecordNameByNo(string sNo)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            if (sNo.Length > 0)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                try" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("         if (isSqlite)" + Environment.NewLine);
            sb.Append("         {" + Environment.NewLine);
            sb.Append("             " + sTableName + "Access infoAccess = new " + sTableName + "Access(connStr, appCode);" + Environment.NewLine);
            sb.Append("            return infoAccess.GetRecordNameByNo(sNo);" + Environment.NewLine);
            sb.Append("          }" + Environment.NewLine);
            sb.Append("         else" + Environment.NewLine);
            sb.Append("         {" + Environment.NewLine);
            sb.Append("                " + sTableName + "BLL infoBLL = new " + sTableName + "BLL(connStr, appCode);" + Environment.NewLine);
            sb.Append("                    return infoBLL.GetRecordNameByNo(sNo);" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("          }" + Environment.NewLine);
            sb.Append("                catch (Exception ex)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    throw new Exception(\"出错提示:通过No查询名称(GetRecordNameByNo|Business)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            return \"\";" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);


            sb.Append("        public  " + sTableName + "Collections GetPagingRecoreds(ref int pageCount,int pageIndex,string condition)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("           return GetPagingRecoreds(ref pageCount, pageIndex, PublicEntity.PAGESIZE, condition);" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public  " + sTableName + "Collections GetPagingRecoreds(ref int pageCount,int pageIndex,int pageSize,string condition)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("        try" + Environment.NewLine);
            sb.Append("         {" + Environment.NewLine);
            sb.Append("         if (isSqlite)" + Environment.NewLine);
            sb.Append("         {" + Environment.NewLine);
            sb.Append("             " + sTableName + "Access infoAccess = new " + sTableName + "Access(connStr, appCode);" + Environment.NewLine);
            sb.Append("             pageCount = infoAccess.GetCountByCondition(condition);" + Environment.NewLine);
            sb.Append("             return infoAccess.GetRecordsByPaging(pageIndex, pageSize, condition);" + Environment.NewLine);
            sb.Append("          }" + Environment.NewLine);
            sb.Append("         else" + Environment.NewLine);
            sb.Append("         {" + Environment.NewLine);
            sb.Append("            " + sTableName + "BLL infoBLL = new " + sTableName + "BLL(connStr, appCode);" + Environment.NewLine);
            sb.Append("            int iPageSize = pageSize>0?pageSize:PublicEntity.PAGESIZE;" + Environment.NewLine);
            sb.Append("            int iPageIndex = pageIndex;" + Environment.NewLine);
            sb.Append("            int iRCount = infoBLL.GetCountByCondition(condition);" + Environment.NewLine);
            sb.Append("            int iPageCount = (iRCount / iPageSize) + 1;" + Environment.NewLine);
            sb.Append("            pageCount = iPageCount;" + Environment.NewLine);
            sb.Append("" + Environment.NewLine);
            sb.Append("            if (iPageCount < 1)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("              iPageCount = 1;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("" + Environment.NewLine);
            sb.Append("            if (iPageIndex < 1)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                iPageIndex = 1;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            else if (iPageIndex > iPageCount)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                iPageIndex = iPageCount;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("" + Environment.NewLine);
            sb.Append("          SqlModel s_model = new SqlModel();" + Environment.NewLine);
            sb.Append("" + Environment.NewLine);
            sb.Append("          s_model.iPageNo = iPageIndex;" + Environment.NewLine);
            sb.Append("          s_model.iPageSize = iPageSize;" + Environment.NewLine);
            sb.Append("          s_model.sFields = \"*\";" + Environment.NewLine);
            sb.Append("          s_model.sCondition = condition;" + Environment.NewLine);
            sb.Append("          s_model.sOrderField = \"ID\";" + Environment.NewLine);
            sb.Append("          s_model.sOrderType = \"desc\";" + Environment.NewLine);
            sb.Append("          s_model.sTableName = \"" + sTableName + "\";" + Environment.NewLine);

            sb.Append("          return infoBLL.GetRecords_Paging(s_model);" + Environment.NewLine);
            sb.Append("          }" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("          }" + Environment.NewLine);
            sb.Append("          catch (Exception ex)" + Environment.NewLine);
            sb.Append("          {" + Environment.NewLine);
            sb.Append("              throw new Exception(\"出错提示:分页查询记录(GetRecords_Paging|Business)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("          }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public  int GetCountByCondition(string sCondition)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("            if (isSqlite)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                " + sTableName + "Access infoAccess = new " + sTableName + "Access(connStr, appCode);" + Environment.NewLine);
            sb.Append("                return infoAccess.GetCountByCondition(sCondition);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            else" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                " + sTableName + "BLL infoBLL = new " + sTableName + "BLL(connStr, appCode);" + Environment.NewLine);
            sb.Append("                return infoBLL.GetCountByCondition(sCondition);" + Environment.NewLine);
            sb.Append("             }" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                throw new Exception(\"出错提示:计算记录个数(GetCountByCondition|Business)时出错;\" + ex.Message);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append("    }" + Environment.NewLine);
            sb.Append("}" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            GenerateCode(sFileDir, sFileName, sb.ToString());
        }

        private void btnWBuss_Click(object sender, EventArgs e)
        {
            string sTableName = cbTables.SelectedValue.ToString();
            string sNameSpace = txtNameSpace.Text.Trim();
            string sFileName = "";

            string sFileDir = Environment.CurrentDirectory.ToString() + @"\\" + sNameSpace + "Business\\";
            DataTable dt = new DataTable();

            bool bSingleMode = rbSingle.Checked;

            if (bSingleMode)
            {
                GetTableInfo(sTableName, ref dt);

                sFileName = sTableName + "Business.cs";
                CreateWServiceBusiness(sFileDir, sFileName, sTableName, sNameSpace + "Business", dt);
            }
            else
            {
                for (int i = 0; i < cbTables.Items.Count; i++)
                {
                    cbTables.SelectedIndex = i;
                    sTableName = cbTables.SelectedValue.ToString();
                    GetTableInfo(sTableName, ref dt);

                    sFileName = sTableName + "Business.cs";
                    CreateWServiceBusiness(sFileDir, sFileName, sTableName, sNameSpace + "Business", dt);
                }
            }
        }

        private void CreateWServiceBusiness(string sFileDir, string sFileName, string sTableName, string sNameSpace, DataTable dt)
        {
            StringBuilder sb = new StringBuilder();

            string camelCaseName = CamelCase(sTableName);

            sb.Append("using EntFrm.Common;" + Environment.NewLine);
            sb.Append("using EntFrm.Model;" + Environment.NewLine);
            sb.Append("using EntFrm.Model.Collections;" + Environment.NewLine);
            sb.Append("using System;" + Environment.NewLine);
            sb.Append("using System.Collections.Generic;" + Environment.NewLine);
            sb.Append("using System.Linq;" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("namespace " + sNameSpace + Environment.NewLine);
            sb.Append("{" + Environment.NewLine);
            sb.Append("    public class " + sTableName + "Business" + Environment.NewLine);
            sb.Append("    {" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        private string webUrl;" + Environment.NewLine);
            sb.Append("        private string appCode;" + Environment.NewLine);

            sb.Append("        private WebSoapInfo soapInfo;" + Environment.NewLine);
            sb.Append("        private EncryptInfo encInfo;" + Environment.NewLine); 
            sb.Append(Environment.NewLine); 

            sb.Append("        public  " + sTableName + "Business(string sWebUrl,string sAppCode)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("           this.webUrl = sWebUrl;" + Environment.NewLine);
            sb.Append("           this.appCode = sAppCode;" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("           soapInfo = ISoapHelper.Get_WebSoapInfo();" + Environment.NewLine);
            sb.Append("           soapInfo.WebServiceUrl = webUrl;" + Environment.NewLine);
            sb.Append("           soapInfo.AppCode = appCode;" + Environment.NewLine);
            sb.Append("           encInfo = ISoapHelper.Get_EncryptInfo();" + Environment.NewLine); 
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine); 

            sb.Append("        public  bool AddNewRecord(" + sTableName + " info)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            bool returnValue = false;" + Environment.NewLine);
            sb.Append("            object[] paramValues = GetObjectArray(info);" + Environment.NewLine);
            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                IWebSoapRequestUtil soapUtil = new IWebSoapRequestUtil(soapInfo);" + Environment.NewLine);
            sb.Append("                if (soapUtil.ExecuteSoapInstruction(\"" + sTableName + "_AddRecord\", paramValues, true, true, encInfo))" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                   List <List<string>> s = (List<List<string>>)soapUtil.ResultMessage;" + Environment.NewLine);
            sb.Append("                   returnValue = bool.Parse(s[0][0].ToString());" + Environment.NewLine);
            sb.Append("               }" + Environment.NewLine);
            sb.Append("                return returnValue;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("               throw new Exception(\" 添加(DAL层)记录时出错; \" + ex.Message);" + Environment.NewLine);
            sb.Append("             }" + Environment.NewLine);
             
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public  bool UpdateRecord(" + sTableName + " info)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("        bool returnValue = false;" + Environment.NewLine);
            sb.Append("        object[] paramValues = GetObjectArray(info);" + Environment.NewLine);
            sb.Append("        try" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            IWebSoapRequestUtil soapUtil = new IWebSoapRequestUtil(soapInfo);" + Environment.NewLine);
            sb.Append("            if (soapUtil.ExecuteSoapInstruction(\"" + sTableName + "_UpdateRecord\", paramValues, true, true, encInfo))" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                List <List<string>> s = (List<List<string>>)soapUtil.ResultMessage;" + Environment.NewLine);
            sb.Append("                returnValue = bool.Parse(s[0][0].ToString());" + Environment.NewLine);
            sb.Append("           }" + Environment.NewLine);
            sb.Append("            return returnValue;" + Environment.NewLine);
            sb.Append("         }" + Environment.NewLine);
            sb.Append("        catch (Exception ex)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("           throw new Exception(\" 更新(DAL层)记录时出错; \" + ex.Message);" + Environment.NewLine);
            sb.Append("         }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public  bool SoftDeleteRecord(string sNo)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("        bool returnValue = false;" + Environment.NewLine);
            sb.Append("        try" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("           IWebSoapRequestUtil soapUtil = new IWebSoapRequestUtil(soapInfo);" + Environment.NewLine);
            sb.Append("           if (soapUtil.ExecuteSoapInstruction(\"" + sTableName + "_SoftDeleteRecord\", new object[] { sNo }, true, true, encInfo))" + Environment.NewLine);
            sb.Append("          {" + Environment.NewLine);
            sb.Append("             List <List<string>> s = (List<List<string>>)soapUtil.ResultMessage;" + Environment.NewLine);
            sb.Append("               returnValue = bool.Parse(s[0][0].ToString());" + Environment.NewLine);
            sb.Append("           }" + Environment.NewLine);
            sb.Append("           return returnValue;" + Environment.NewLine);
            sb.Append("         }" + Environment.NewLine);
            sb.Append("        catch (Exception ex)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("           throw new Exception(\" 软删除(DAL层)记录时出错; \" + ex.Message);" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public bool HardDeleteRecord(string sNo)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("        bool returnValue = false;" + Environment.NewLine);
            sb.Append("        try" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            IWebSoapRequestUtil soapUtil = new IWebSoapRequestUtil(soapInfo);" + Environment.NewLine);
            sb.Append("            if (soapUtil.ExecuteSoapInstruction(\"" + sTableName + "_HardDeleteRecord\", new object[] { sNo }, true, true, encInfo))" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("               List <List<string>> s = (List<List<string>>)soapUtil.ResultMessage;" + Environment.NewLine);
            sb.Append("                returnValue = bool.Parse(s[0][0].ToString());" + Environment.NewLine);
            sb.Append("           }" + Environment.NewLine);
            sb.Append("           return returnValue;" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append("        catch (Exception ex)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            throw new Exception(\" 硬删除(DAL层)记录时出错; \" + ex.Message);" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public   " + sTableName + "Collections GetAllRecords()" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine); 
            sb.Append("        " + sTableName + "Collections infos = null;" + Environment.NewLine);
            sb.Append("        " + sTableName + " info = null;" + Environment.NewLine);

            sb.Append("        try" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            IWebSoapRequestUtil soapUtil = new IWebSoapRequestUtil(soapInfo);" + Environment.NewLine);
            sb.Append("            if (soapUtil.ExecuteSoapInstruction(\"" + sTableName + "_GetAllRecords\", null, true, true, encInfo))" + Environment.NewLine);
            sb.Append("           {" + Environment.NewLine);
            sb.Append("               List <List<string>> infoList = (List<List<string>>)soapUtil.ResultMessage;" + Environment.NewLine);
            sb.Append("               if (infoList != null && infoList.Count > 0)" + Environment.NewLine);
            sb.Append("               {" + Environment.NewLine);
            sb.Append("                   if (!infoList[0][0].Equals(\"Null\"))" + Environment.NewLine);
            sb.Append("                   {" + Environment.NewLine);
            sb.Append("                       infos = new " + sTableName + "Collections();" + Environment.NewLine);
            sb.Append("                       for (int i = 0; i < infoList.Count; i++)" + Environment.NewLine);
            sb.Append("                      {" + Environment.NewLine);
            sb.Append("                          info = new " + sTableName + "();" + Environment.NewLine);
            sb.Append("                          // 设置对象属性" + Environment.NewLine);
            sb.Append("                         PutObjectProperty(info, infoList[i]);" + Environment.NewLine);
            sb.Append("                         infos.Add(info);" + Environment.NewLine);
            sb.Append("                     }" + Environment.NewLine);
            sb.Append("                 }" + Environment.NewLine);
            sb.Append("             }" + Environment.NewLine);
            sb.Append("           }" + Environment.NewLine);
            sb.Append("          return infos;" + Environment.NewLine);
            sb.Append("         }" + Environment.NewLine);
            sb.Append("        catch (Exception ex)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            //throw new Exception(\" 查询所有记录(DAL层|GetAllRecords)时出错; \" + ex.Message);" + Environment.NewLine);
            sb.Append("           return null;" + Environment.NewLine);
            sb.Append("         }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public " + sTableName + "Collections GetRecordsByClassNo(string sClassNo)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("         " + sTableName + "Collections infos = null;" + Environment.NewLine);
            sb.Append("         " + sTableName + " info = null;" + Environment.NewLine);

            sb.Append("         try" + Environment.NewLine);
            sb.Append("         {" + Environment.NewLine);
            sb.Append("            IWebSoapRequestUtil soapUtil = new IWebSoapRequestUtil(soapInfo);" + Environment.NewLine);
            sb.Append("            if (soapUtil.ExecuteSoapInstruction(\"" + sTableName + "_GetRecordsByClassNo\", new object[] { sClassNo }, true, true, encInfo))" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("               List <List<string>> infoList = (List<List<string>>)soapUtil.ResultMessage;" + Environment.NewLine);
            sb.Append("                if (infoList != null && infoList.Count > 0)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    if (!infoList[0][0].Equals(\"Null\"))" + Environment.NewLine);
            sb.Append("                    {" + Environment.NewLine);
            sb.Append("                       infos = new " + sTableName + "Collections();" + Environment.NewLine);
            sb.Append("                       for (int i = 0; i < infoList.Count; i++)" + Environment.NewLine);
            sb.Append("                       {" + Environment.NewLine);
            sb.Append("                           info = new " + sTableName + "();" + Environment.NewLine);
            sb.Append("                          // 设置对象属性" + Environment.NewLine);
            sb.Append("                           PutObjectProperty(info, infoList[i]);" + Environment.NewLine);
            sb.Append("                          infos.Add(info);" + Environment.NewLine);
            sb.Append("                       }" + Environment.NewLine);
            sb.Append("                   }" + Environment.NewLine);
            sb.Append("               }" + Environment.NewLine);
            sb.Append("          }" + Environment.NewLine);
            sb.Append("          return infos;" + Environment.NewLine);
            sb.Append("          }" + Environment.NewLine);
            sb.Append("         catch (Exception ex)" + Environment.NewLine);
            sb.Append("         {" + Environment.NewLine);
            sb.Append("             //throw new Exception(\" 通过sClassNo查询记录(DAL层)时出错; \" + ex.Message);" + Environment.NewLine);
            sb.Append("             return null;" + Environment.NewLine);
            sb.Append("          }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public  " + sTableName + " GetRecordByNo(string sNo)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("        " + sTableName + " info = null;" + Environment.NewLine);

            sb.Append("        try" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            IWebSoapRequestUtil soapUtil = new IWebSoapRequestUtil(soapInfo);" + Environment.NewLine);
            sb.Append("            if (soapUtil.ExecuteSoapInstruction(\"" + sTableName + "_GetRecordByNo\", new object[] { sNo }, true, true, encInfo))" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                List <List<string>> infoList = (List<List<string>>)soapUtil.ResultMessage;" + Environment.NewLine);
            sb.Append("               if (infoList != null && infoList.Count > 0)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                   if (!infoList[0][0].Equals(\"Null\"))" + Environment.NewLine);
            sb.Append("                   {" + Environment.NewLine);
            sb.Append("                        info = new " + sTableName + "();" + Environment.NewLine);
            sb.Append("                       // 设置对象属性" + Environment.NewLine);
            sb.Append("                        PutObjectProperty(info, infoList[0]);" + Environment.NewLine);
            sb.Append("                    }" + Environment.NewLine);
            sb.Append("               }" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            return info;" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append("        catch (Exception ex)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            //throw new Exception(\" 通过No查询记录(DAL层)时出错; \" + ex.Message);" + Environment.NewLine);
            sb.Append("            return null;" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public  string GetRecordNameByNo(string sNo)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("        string returnValue = null;" + Environment.NewLine);
            sb.Append("        try" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            IWebSoapRequestUtil soapUtil = new IWebSoapRequestUtil(soapInfo);" + Environment.NewLine);
            sb.Append("           if (soapUtil.ExecuteSoapInstruction(\"" + sTableName + "_GetRecordNameByNo\", new object[] { sNo }, true, true, encInfo))" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                List <List<string>> s = (List<List<string>>)soapUtil.ResultMessage;" + Environment.NewLine);
            sb.Append("                returnValue = s[0][0].ToString();" + Environment.NewLine);
            sb.Append("           }" + Environment.NewLine);
            sb.Append("            return returnValue;" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append("        catch (Exception ex)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("           throw new Exception(\" 通过sNo查询记录名称(DAL层)时出错; \" + ex.Message);" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine); 

            sb.Append("        public " + sTableName + "Collections GetRecordsByPaging(SqlModel s_model)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            " + sTableName + "Collections infos = null;" + Environment.NewLine);
            sb.Append("            " + sTableName + " info = null;" + Environment.NewLine);

            sb.Append("           object[] paramValues = new object[7];" + Environment.NewLine);
            sb.Append("            paramValues[0] = s_model.sTableName;" + Environment.NewLine);
            sb.Append("           paramValues[1] = s_model.sFields;" + Environment.NewLine);
            sb.Append("            paramValues[2] = s_model.iPageNo;" + Environment.NewLine);
            sb.Append("           paramValues[3] = s_model.iPageSize;" + Environment.NewLine);
            sb.Append("           paramValues[4] = s_model.sCondition;" + Environment.NewLine);
            sb.Append("           paramValues[5] = s_model.sOrderField;" + Environment.NewLine);
            sb.Append("           paramValues[6] = s_model.sOrderType;" + Environment.NewLine);

            sb.Append("            try" + Environment.NewLine);
            sb.Append("           {" + Environment.NewLine);
            sb.Append("                IWebSoapRequestUtil soapUtil = new IWebSoapRequestUtil(soapInfo);" + Environment.NewLine);
            sb.Append("                if (soapUtil.ExecuteSoapInstruction(\"" + sTableName + "_GetRecordsByPaging\", paramValues, true, true, encInfo))" + Environment.NewLine);
            sb.Append("               {" + Environment.NewLine);
            sb.Append("                   List <List<string>> infoList = (List<List<string>>)soapUtil.ResultMessage;" + Environment.NewLine);
            sb.Append("                  if (infoList != null && infoList.Count > 0)" + Environment.NewLine);
            sb.Append("                  {" + Environment.NewLine);
            sb.Append("                     if (!infoList[0][0].Equals(\"Null\"))" + Environment.NewLine);
            sb.Append("                     {" + Environment.NewLine);
            sb.Append("                        infos = new " + sTableName + "Collections();" + Environment.NewLine);
            sb.Append("                         for (int i = 0; i < infoList.Count; i++)" + Environment.NewLine);
            sb.Append("                         {" + Environment.NewLine);
            sb.Append("                             info = new " + sTableName + "();" + Environment.NewLine);
            sb.Append("                             // 设置对象属性" + Environment.NewLine);
            sb.Append("                             PutObjectProperty(info, infoList[i]);" + Environment.NewLine);
            sb.Append("                             infos.Add(info);" + Environment.NewLine);
            sb.Append("                         }" + Environment.NewLine);
            sb.Append("                     }" + Environment.NewLine);
            sb.Append("                 }" + Environment.NewLine);
            sb.Append("             }" + Environment.NewLine);
            sb.Append("              return infos;" + Environment.NewLine);
            sb.Append("          }" + Environment.NewLine);
            sb.Append("          catch (Exception ex)" + Environment.NewLine);
            sb.Append("          {" + Environment.NewLine);
            sb.Append("              //throw new Exception(\" 分页查询(DAL层)记录时出错; \" + ex.Message);" + Environment.NewLine);
            sb.Append("              return null;" + Environment.NewLine);
            sb.Append("          }" + Environment.NewLine);
            sb.Append("         }" + Environment.NewLine);

            sb.Append("        public  " + sTableName + "Collections GetRecordsByPaging(int pageIndex, int pageSize, string condition)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("        " + sTableName + "Collections infos = null;" + Environment.NewLine);
            sb.Append("        " + sTableName + " info = null;" + Environment.NewLine);

            sb.Append("        object[] paramValues = new object[3];" + Environment.NewLine);
            sb.Append("        paramValues[0] = pageIndex.ToString();" + Environment.NewLine);
            sb.Append("        paramValues[1] = pageSize.ToString();" + Environment.NewLine);
            sb.Append("        paramValues[2] = condition;" + Environment.NewLine);

            sb.Append("        try" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            IWebSoapRequestUtil soapUtil = new IWebSoapRequestUtil(soapInfo);" + Environment.NewLine);
            sb.Append("            if (soapUtil.ExecuteSoapInstruction(\"" + sTableName + "_GetRecordsByPaging\", paramValues, true, true, encInfo))" + Environment.NewLine);
            sb.Append("           {" + Environment.NewLine);
            sb.Append("               List <List<string>> infoList = (List<List<string>>)soapUtil.ResultMessage;" + Environment.NewLine);
            sb.Append("              if (infoList != null && infoList.Count > 0)" + Environment.NewLine);
            sb.Append("              {" + Environment.NewLine);
            sb.Append("                   if (!infoList[0][0].Equals(\"Null\"))" + Environment.NewLine);
            sb.Append("                  {" + Environment.NewLine);
            sb.Append("                       infos = new " + sTableName + "Collections();" + Environment.NewLine);
            sb.Append("                       for (int i = 0; i < infoList.Count; i++)" + Environment.NewLine);
            sb.Append("                      {" + Environment.NewLine);
            sb.Append("                           info = new " + sTableName + "();" + Environment.NewLine);
            sb.Append("                          // 设置对象属性" + Environment.NewLine);
            sb.Append("                          PutObjectProperty(info, infoList[i]);" + Environment.NewLine);
            sb.Append("                           infos.Add(info);" + Environment.NewLine);
            sb.Append("                      }" + Environment.NewLine);
            sb.Append("                  }" + Environment.NewLine);
            sb.Append("              }" + Environment.NewLine);
            sb.Append("           }" + Environment.NewLine);
            sb.Append("           return infos;" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append("        catch (Exception ex)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            //throw new Exception(\" 分页查询(DAL层)记录时出错; \" + ex.Message);" + Environment.NewLine);
            sb.Append("            return null;" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public  int GetCountByCondition(string sCondition)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("        int returnValue = 0;" + Environment.NewLine);
            sb.Append("        try" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("        IWebSoapRequestUtil soapUtil = new IWebSoapRequestUtil(soapInfo);" + Environment.NewLine);
            sb.Append("        if (soapUtil.ExecuteSoapInstruction(\"" + sTableName + "_GetCountByCondition\", new object[] { sCondition }, true, true, encInfo))" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);

            sb.Append("            List<List<string>> s = (List<List<string>>)soapUtil.ResultMessage;" + Environment.NewLine);

            sb.Append("            returnValue = int.Parse(s[0][0].ToString());" + Environment.NewLine);

            sb.Append("            }" + Environment.NewLine);

            sb.Append("            return returnValue;" + Environment.NewLine);

            sb.Append("         }" + Environment.NewLine);

            sb.Append("        catch (Exception ex)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            throw new Exception(\" 计算记录总数(DAL层)时出错; \" + ex.Message);" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);

            sb.Append("        }" + Environment.NewLine);


            sb.Append("        #region PutObjectProperty 设置对象属性" + Environment.NewLine);
            sb.Append("        /// <summary>" + Environment.NewLine);
            sb.Append("        /// 从 SqlDataReader 类对象中读取并设置对象属性" + Environment.NewLine);
            sb.Append("        /// </summary>" + Environment.NewLine);
            sb.Append("       /// <param name=\" obj_info\">主题对象</param>" + Environment.NewLine);
            sb.Append("        /// <param name=\"dr\">读入数据</param>" + Environment.NewLine);
            sb.Append("        internal static void PutObjectProperty(" + sTableName + " obj_info, List<string> infoList)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            int i = 0;
            int icount = dt.Rows.Count;
            foreach (DataRow dr in dt.Rows)
            {
                if (icount - 1 == i)
                {
                    sb.Append("            obj_info." + GetCSharpType(dr["DataType"].ToString()).ToLower().Substring(0, 1) + dr["ColumnName"].ToString() + "= infoList[" + (i++).ToString() + "].ToString();" + Environment.NewLine);
                }
                else
                {
                    sb.Append("            obj_info." + GetCSharpType(dr["DataType"].ToString()).ToLower().Substring(0, 1) + dr["ColumnName"].ToString() + "= " + GetCParseType(dr["DataType"].ToString()) + "infoList[" + (i++).ToString() + "].ToString()" + (GetCParseTypeState(dr["DataType"].ToString()) ? ")" : "") + ";" + Environment.NewLine);
                }
            }
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        internal static object[] GetObjectArray(" + sTableName + "  info)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("          Type t = typeof(" + sTableName + ");" + Environment.NewLine);
            sb.Append("          object[] paramValues = new object[t.GetProperties().Count()-1];" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            int j = -1;
            foreach (DataRow dr in dt.Rows)
            {
                if (j < 0)
                {
                    j++;
                    continue;
                }
                sb.Append("            paramValues[" + (j++).ToString() + "] = info." + GetCSharpType(dr["DataType"].ToString()).ToLower().Substring(0, 1) + dr["ColumnName"].ToString() + ";" + Environment.NewLine);
            }
            sb.Append(Environment.NewLine);
            sb.Append("        return paramValues;" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append("        #endregion" + Environment.NewLine);

            sb.Append("    }" + Environment.NewLine);
            sb.Append("}" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            GenerateCode(sFileDir, sFileName, sb.ToString());
        }

        private void btnGenBuss2_Click(object sender, EventArgs e)
        {
            string sTableName = cbTables.SelectedValue.ToString();
            string sNameSpace = txtNameSpace.Text.Trim();
            string sFileName = "";

            string sFileDir = Environment.CurrentDirectory.ToString() + @"\\" + sNameSpace + "Business\\";
            DataTable dt = new DataTable();

            bool bSingleMode = rbSingle.Checked;

            if (bSingleMode)
            {
                GetTableInfo(sTableName, ref dt);

                sFileName = sTableName + "Business.cs";
                CreateFrameBusiness(sFileDir, sFileName, sTableName, sNameSpace + "Business", dt);
            }
            else
            {
                for (int i = 0; i < cbTables.Items.Count; i++)
                {
                    cbTables.SelectedIndex = i;
                    sTableName = cbTables.SelectedValue.ToString();
                    GetTableInfo(sTableName, ref dt);

                    sFileName = sTableName + "Business.cs";
                    CreateFrameBusiness(sFileDir, sFileName, sTableName, sNameSpace + "Business", dt);
                }
            }
        }

        private void CreateFrameBusiness(string sFileDir, string sFileName, string sTableName, string sNameSpace, DataTable dt)
        {
            StringBuilder sb = new StringBuilder();

            string camelCaseName = CamelCase(sTableName);

            sb.Append("using EntFrm.Business.BLL;" + Environment.NewLine);
            sb.Append("using EntFrm.Business.Model;" + Environment.NewLine);
            sb.Append("using EntFrm.Business.Model.Collections;" + Environment.NewLine);
            sb.Append("using EntFrm.Framework.Utility;" + Environment.NewLine);
            sb.Append("using Newtonsoft.Json;" + Environment.NewLine); 
            sb.Append("using System;" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("namespace " + sNameSpace + Environment.NewLine);
            sb.Append("{" + Environment.NewLine);
            sb.Append("    public class " + sTableName + "Business" + Environment.NewLine);
            sb.Append("    {" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("    private volatile static " + sTableName + "Business _instance = null;" + Environment.NewLine);
            sb.Append("    private static readonly object lockHelper = new object();" + Environment.NewLine);
            sb.Append("    public static " + sTableName + "Business CreateInstance()" + Environment.NewLine);
            sb.Append("    {" + Environment.NewLine);
            sb.Append("    if (_instance == null)" + Environment.NewLine);
            sb.Append("    {" + Environment.NewLine);
            sb.Append("    lock (lockHelper)" + Environment.NewLine);
            sb.Append("    {" + Environment.NewLine);
            sb.Append("    if (_instance == null)" + Environment.NewLine);
            sb.Append("      _instance = new " + sTableName + "Business();" + Environment.NewLine);
            sb.Append("    }" + Environment.NewLine);
            sb.Append("    }" + Environment.NewLine);
            sb.Append("    return _instance;" + Environment.NewLine);
            sb.Append("    }" + Environment.NewLine);


            sb.Append("        public  string GetAllRecords()" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("               string sResult = \"\";" + Environment.NewLine);
            sb.Append("                int count = 0;" + Environment.NewLine);
            sb.Append("                string sWhere = \" BranchNo = '\"+IUserContext.GetBranchNo()+\"' \";" + Environment.NewLine);
            sb.Append("               " + sTableName + "BLL infoBoss = new " + sTableName + "BLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());" + Environment.NewLine);
            sb.Append("               " + sTableName + "Collections infoColl = infoBoss.GetRecordsByPaging(ref count, 1, 100, sWhere);" + Environment.NewLine);
            sb.Append("               if (infoColl != null)" + Environment.NewLine);
            sb.Append("               {" + Environment.NewLine);
            sb.Append("                  sResult = JsonConvert.SerializeObject(infoColl, Formatting.None);" + Environment.NewLine);
            sb.Append("               }" + Environment.NewLine);
            sb.Append("               return sResult;" + Environment.NewLine);

            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("               return \"\";" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine); 

            sb.Append("        public string GetRecordByNo(string sNo)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("                try" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                  string sResult = \"\";" + Environment.NewLine);
            sb.Append("                  " + sTableName + "BLL infoBoss = new " + sTableName + "BLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());" + Environment.NewLine);
            sb.Append("                 " + sTableName + " info = infoBoss.GetRecordByNo(sNo);" + Environment.NewLine);
            sb.Append("                 if (info != null)" + Environment.NewLine);
            sb.Append("                 {" + Environment.NewLine);
            sb.Append("                    sResult = JsonConvert.SerializeObject(info, Formatting.None);" + Environment.NewLine);
            sb.Append("                 }" + Environment.NewLine);
            sb.Append("                 return sResult;" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("                catch (Exception ex)" + Environment.NewLine);
            sb.Append("                {" + Environment.NewLine);
            sb.Append("                    return \"\";" + Environment.NewLine);
            sb.Append("                }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);  

            sb.Append("        public  string GetRecordsByPaging(string pageIndex,string pageSize,string condition)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("        try" + Environment.NewLine);
            sb.Append("         {" + Environment.NewLine);
            sb.Append("            string sResult = \"\";" + Environment.NewLine);
            sb.Append("            int count = 0;" + Environment.NewLine);
            sb.Append("            string sWhere = \" BranchNo = '\" + IUserContext.GetBranchNo() + \"' \";" + Environment.NewLine);
            sb.Append("            if (!string.IsNullOrEmpty(condition))" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("              sWhere += \" And \" + condition;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            " + sTableName + "BLL infoBoss = new " + sTableName + "BLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());" + Environment.NewLine);
            sb.Append("            " + sTableName + "Collections infoColl = infoBoss.GetRecordsByPaging(ref count, int.Parse(pageIndex), int.Parse(pageSize), sWhere);" + Environment.NewLine);
            sb.Append("            if (infoColl != null)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("              sResult = JsonConvert.SerializeObject(infoColl, Formatting.None);" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("            return sResult;" + Environment.NewLine);

            sb.Append("          }" + Environment.NewLine);
            sb.Append("          catch (Exception ex)" + Environment.NewLine);
            sb.Append("          {" + Environment.NewLine);
            sb.Append("                    return \"\";" + Environment.NewLine);
            sb.Append("          }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("        public  string GetCountByCondition(string condition)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("            string sWhere = \" BranchNo = '\" + IUserContext.GetBranchNo() + \"' \";" + Environment.NewLine);
            sb.Append("            if (!string.IsNullOrEmpty(condition))" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("              sWhere += \" And \" + condition;" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("              " + sTableName + "BLL infoBoss = new " + sTableName + "BLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());" + Environment.NewLine);
            sb.Append("              return infoBoss.GetCountByCondition(sWhere).ToString();" + Environment.NewLine); 

            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                    return \"0\";" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append(Environment.NewLine);



            sb.Append("        public  string PostRecord(string sInfo)" + Environment.NewLine);
            sb.Append("        {" + Environment.NewLine);
            sb.Append("            try" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("              string sResult = \"\";" + Environment.NewLine);
            sb.Append("              string sSuNo = \"00000000\";" + Environment.NewLine);
            sb.Append("              " + sTableName + " info1 = JsonConvert.DeserializeObject<" + sTableName + ">(sInfo);" + Environment.NewLine);
            sb.Append("              if (info1 != null)" + Environment.NewLine);
            sb.Append("              {" + Environment.NewLine);
            sb.Append("                 " + sTableName + "BLL infoBoss = new " + sTableName + "BLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());" + Environment.NewLine);
            sb.Append("                 " + sTableName + " info2 = infoBoss.GetRecordByNo(info1.sCounterNo);" + Environment.NewLine);
            sb.Append("                 if (info2 == null)" + Environment.NewLine);
            sb.Append("                 {" + Environment.NewLine);
            sb.Append("                    info2 = info1;" + Environment.NewLine);
            sb.Append("                    info2.sCounterNo = CommonHelper.Get_New12ByteGuid();" + Environment.NewLine);
            sb.Append("                    info2.sBranchNo = IUserContext.GetBranchNo();" + Environment.NewLine);
            sb.Append("                    info2.sAddOptor = sSuNo;" + Environment.NewLine);
            sb.Append("                    info2.dAddDate = DateTime.Now;" + Environment.NewLine);
            sb.Append("                    info2.sModOptor = sSuNo;" + Environment.NewLine);
            sb.Append("                    info2.dModDate = DateTime.Now;" + Environment.NewLine);
            sb.Append("                    info2.iValidityState = 1;" + Environment.NewLine);
            sb.Append("                    info2.sAppCode = IUserContext.GetAppCode() + \"; \";" + Environment.NewLine);
            sb.Append("                    sResult = infoBoss.AddNewRecord(info2).ToString();" + Environment.NewLine);
            sb.Append("                 }" + Environment.NewLine);
            sb.Append("                 else" + Environment.NewLine);
            sb.Append("                 {" + Environment.NewLine);
            sb.Append("                    info2 = info1;" + Environment.NewLine);
            sb.Append("                    info2.sBranchNo = IUserContext.GetBranchNo();" + Environment.NewLine);
            sb.Append("                    info2.sModOptor = sSuNo;" + Environment.NewLine);
            sb.Append("                    info2.dModDate = DateTime.Now;" + Environment.NewLine);
            sb.Append("                    sResult = infoBoss.UpdateRecord(info2).ToString();" + Environment.NewLine); 
            sb.Append("                 }" + Environment.NewLine);
            sb.Append("              }" + Environment.NewLine);

            sb.Append("              return sResult;" + Environment.NewLine);

            sb.Append("            }" + Environment.NewLine);
            sb.Append("            catch (Exception ex)" + Environment.NewLine);
            sb.Append("            {" + Environment.NewLine);
            sb.Append("                    return \"\";" + Environment.NewLine);
            sb.Append("            }" + Environment.NewLine);
            sb.Append("        }" + Environment.NewLine);
            sb.Append("    }" + Environment.NewLine);
            sb.Append("}" + Environment.NewLine);
            sb.Append(Environment.NewLine);

            GenerateCode(sFileDir, sFileName, sb.ToString());
        }

        private void btnGenPager_Click(object sender, EventArgs e)
        {
            string sTableName = cbTables.SelectedValue.ToString();
            string sNameSpace = txtNameSpace.Text.Trim();
            string sFileName = "";

            string sFileDir = Environment.CurrentDirectory.ToString() + @"\\" + sNameSpace + "Pager\\";
            DataTable dt = new DataTable();

            bool bSingleMode = rbSingle.Checked;

            sFileName = "PagerHelper.cs";
            if (bSingleMode)
            {
                GetTableInfo(sTableName, ref dt);
                 
                CreatePagerHelper(sFileDir, sFileName, sTableName, sNameSpace + "Common", dt);
            }
            else
            { 
                for (int i = 0; i < cbTables.Items.Count; i++)
                {
                    cbTables.SelectedIndex = i;
                    sTableName = cbTables.SelectedValue.ToString();
                    GetTableInfo(sTableName, ref dt);

                    CreatePagerHelper(sFileDir, sFileName, sTableName, sNameSpace + "Common", dt);
                }
            }
        }

        private void CreatePagerHelper(string sFileDir, string sFileName, string sTableName, string sNameSpace, DataTable dt)
        {
            StringBuilder sb = new StringBuilder();

            string camelCaseName = CamelCase(sTableName);

            sb.Append("public static string get" + sTableName + "NameByNo(string sNo)" + Environment.NewLine);
            sb.Append("{ " + Environment.NewLine);
            sb.Append("    try" + Environment.NewLine);
            sb.Append("   { " + Environment.NewLine);
            sb.Append("" + sTableName + "BLL infoBLL = new " + sTableName + "BLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode()); " + Environment.NewLine);
            sb.Append("        return infoBLL.GetRecordNameByNo(sNo); " + Environment.NewLine);
            sb.Append("    }" + Environment.NewLine);
            sb.Append("   catch (Exception ex)" + Environment.NewLine);
            sb.Append("    { " + Environment.NewLine);
            sb.Append("        return \"\"; " + Environment.NewLine);
            sb.Append("    }" + Environment.NewLine);
            sb.Append("}" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("public static " + sTableName + " get" + sTableName + "ByNo(string sNo)" + Environment.NewLine);
            sb.Append("{ " + Environment.NewLine);
            sb.Append("    try" + Environment.NewLine);
            sb.Append("   { " + Environment.NewLine);
            sb.Append("" + sTableName + "BLL infoBLL = new " + sTableName + "BLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode()); " + Environment.NewLine);
            sb.Append("        return infoBLL.GetRecordByNo(sNo); " + Environment.NewLine);
            sb.Append("    }" + Environment.NewLine);
            sb.Append("    catch (Exception ex)" + Environment.NewLine);
            sb.Append("   { " + Environment.NewLine);
            sb.Append("       return null; " + Environment.NewLine);
            sb.Append("   }" + Environment.NewLine);
            sb.Append("}" + Environment.NewLine);

            sb.Append(Environment.NewLine);

            GenerateCode(sFileDir, sFileName, sb.ToString());
        }
    }
}

using EntFrm.Business.BLL;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using System;
using System.Collections;
using System.Windows.Forms;

namespace EntFrm.MainService.Dialogs
{
    public partial class Setting : Form
    {
        public Setting()
        {
            InitializeComponent();
        }

        private void Setting_Load(object sender, EventArgs e)
        {
            DoBindBranchSource(); 

            txtMAdapterIp.Text = IUserContext.GetConfigValue("MAdapterIp");
            txtMAdapterPort.Text = IUserContext.GetConfigValue("MAdapterPort");
            txtServerIp.Text = IUserContext.GetConfigValue("ServerIp");
            txtWTcpPort.Text = IUserContext.GetConfigValue("WTcpPort");
            txtWHttpPort.Text = IUserContext.GetConfigValue("WHttpPort"); 
            dpBranchList.SelectedValue = IUserContext.GetBranchNo();

            txtConnStr.Text = IUserContext.GetConnStr();
        }

        private void DoBindBranchSource()
        {
            try
            {
                BranchInfoBLL infoBoss = new BranchInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                BranchInfoCollections infoColl = infoBoss.GetAllRecords();

                dpBranchList.DataSource = infoColl;
                dpBranchList.ValueMember = "sBranchNo";
                dpBranchList.DisplayMember = "sBranchName";
            }
            catch(Exception ex) { }

        }

        private void btnOk_Click(object sender, EventArgs e)
        {  
            if (txtServerIp.Text.Trim().Length > 0 && txtWTcpPort.Text.Trim().Length > 0)
            {

                string connStr = EnconfigHelper.Encrypt(txtConnStr.Text.Trim());

                IUserContext.SetConfigValue("MAdapterIp", txtMAdapterIp.Text.Trim());
                IUserContext.SetConfigValue("MAdapterPort", txtMAdapterPort.Text.Trim());
                IUserContext.SetConfigValue("ServerIp", txtServerIp.Text.Trim());
                IUserContext.SetConfigValue("WTcpPort", txtWTcpPort.Text.Trim());
                IUserContext.SetConfigValue("WHttpPort", txtWHttpPort.Text.Trim()); 

                IUserContext.SetConfigValue("SqlServer", connStr);
                IUserContext.SetConfigValue("BranchNo", dpBranchList.SelectedValue.ToString());

                MessageBox.Show("系统设置保存成功，请重新启动服务！");
            }
            this.Close();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTestConn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtConnStr.Text.Trim()))
                {
                    if (IDbaseHelper.ConnectionTest(txtConnStr.Text.Trim()))
                    {
                        MessageBox.Show("数据库连接成功!");
                    }
                    else
                    {
                        MessageBox.Show("数据库连接失败!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnInstallDb_Click(object sender, EventArgs e)
        {
            try
            {
                //创建一个对话框对象
                OpenFileDialog ofd = new OpenFileDialog();
                //为对话框设置标题
                ofd.Title = "请选择sql文件";
                //设置筛选的图片格式
                ofd.Filter = "sql格式|*.sql";
                //设置是否允许多选
                ofd.Multiselect = false;
                //如果你点了“确定”按钮
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //获得文件的完整路径（包括名字后后缀）
                    string filePath = ofd.FileName;

                    ArrayList mylist = IDbaseHelper.GetSqlFile(filePath);
                    IDbaseHelper.ExecuteCmd(mylist, txtConnStr.Text.Trim());

                    MessageBox.Show("初始化数据库完成！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("初始化数据库失败！" + ex.Message);
            }
        }


    }
}


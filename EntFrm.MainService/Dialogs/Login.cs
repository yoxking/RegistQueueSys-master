using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntFrm.MainService.Dialogs
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void btnNum_Click(object sender, EventArgs e)
        {
            Button btSender = (Button)sender;

            string sKeyValue = txtPassword.Text;

            sKeyValue += btSender.Text;
            txtPassword.Text = sKeyValue;
            txtPassword.Focus();
            txtPassword.Select(sKeyValue.Length, 0);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string psword = txtPassword.Text.Trim();
            if (!string.IsNullOrEmpty(psword))
            {
                if (psword.Equals("123456"))
                {
                    this.DialogResult = DialogResult.OK; //确定
                    this.Close();
                    return;
                }
            }

            MessageBox.Show("您输入的安全登录密码不正确!");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; //取消
            this.Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (txtPassword.SelectionLength > 0)
            {
                txtPassword.Text = txtPassword.Text.Remove(txtPassword.SelectionStart, txtPassword.SelectionLength);
            }

            string sKeyValue = txtPassword.Text.Trim();
            if (sKeyValue.Length > 0)
            {
                sKeyValue = sKeyValue.Substring(0, sKeyValue.Length - 1);
            }
            txtPassword.Text = sKeyValue;
            txtPassword.Focus();
            txtPassword.Select(sKeyValue.Length, 0);
        }
    }
}



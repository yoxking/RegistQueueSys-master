using EntFrm.Framework.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntFrm.DataAdapter
{
    public partial class RegSoftware : Form
    {
        public RegSoftware()
        {
            InitializeComponent();
        }

        private void RegSoftware_Load(object sender, EventArgs e)
        {
            EncDogModel dogModel = IUserContext.getEncryptDog();
            if (dogModel == null)
            {
                lbStatus.Text = "读取加密锁失败！";
                txtDogID.Text = "";
            }
            else
            {
                lbStatus.Text = "加密锁正常";
                txtDogID.Text = dogModel.EncDogId;
                txtDogType.Text = dogModel.OrganizName;
                txtActiveCount.Text = dogModel.ActiveCount;
            } 
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGenDog_Click(object sender, EventArgs e)
        {  

            string DogID = txtDogID.Text.Trim();
            string DogType = IPublicConsts.DOGTYPE;
            string UpdateDate = DateTime.Now.ToString("yyyy-MM-dd");
            string ActiveDate = DateTime.Now.AddDays(30).ToString("yyyy-MM-dd");
            string ActCount = txtActiveCount.Text.Trim();
            string ActValCode1 = txtActValCode.Text.Trim();

            if (ActValCode1.Length == 32)
            {
                StringBuilder sb1 = new StringBuilder();
                StringBuilder sb2 = new StringBuilder();

                int i = 0;
                int j = 0;
                foreach (char c in ActValCode1)
                {
                    if (i == IPublicConsts.SPACEVAL && j < 8)
                    {
                        sb1.Append(c);
                        i = 0;
                        j++;
                    }
                    else
                    {
                        sb2.Append(c);
                        i++;
                    }
                }

                DateTime dt = DateTime.ParseExact(sb1.ToString(), "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);

                if (dt < DateTime.Now)
                {
                    MessageBox.Show("软件授权有效期已到，请联系管理员！");
                    return;
                }

                ActiveDate = dt.ToString("yyyy-MM-dd");
                ActValCode1 = sb2.ToString();

                if (string.IsNullOrEmpty(DogID) ||  string.IsNullOrEmpty(ActValCode1))
                {
                    MessageBox.Show("加密锁编号和注册码不能为空！");
                    return;
                }

                //初始化我们的操作加密锁的类
                EncryptDogEx encDog = new EncryptDogEx();

                string sCode = DogID + ";" + ActCount + ";" + DogType;
                sCode = encDog.StrEnc(sCode, IPublicConsts.ENCKEY);
                sCode = encDog.getRegisteNum(sCode);

                string ActValCode2 = sCode;

                if (!ActValCode1.Equals(ActValCode2))
                {
                    MessageBox.Show("注册码错误，请重新输入！");
                    return;
                }


                EncDogModel dogCode = new EncDogModel(DogID, DogType, UpdateDate, ActiveDate, ActCount, ActValCode1);
                RegKeyModel regCode = new RegKeyModel(DogID, DogType, UpdateDate, ActiveDate, ActCount, ActValCode1);


                if (IUserContext.setEncryptDog(dogCode) && IUserContext.setRegistryKey(regCode))
                {
                    MessageBox.Show("软件注册成功，请重新启动软件！");
                }
                else
                {
                    MessageBox.Show("软件注册失败！");
                }
            }
            else
            {
                MessageBox.Show("注册码错误，请重新输入！");
            }
        } 
    }
}

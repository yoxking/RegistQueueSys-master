using EntFrm.Framework.Utility;
using System;
using System.Text;
using System.Windows.Forms;

namespace EntFrm.MainService.Dialogs
{
    public partial class RegSoft : Form
    {
        public RegSoft()
        {
            InitializeComponent();
        }

        private void RegSoft_Load(object sender, EventArgs e)
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
                txtOrganizName.Text = dogModel.OrganizName;
                txtActiveCount.Text = dogModel.ActiveCount;
            }
            //RegKeyModel regCode = IUserContext.getRegistryKey();

            //txtDogID.Text = regCode.EncDogId;
            //txtOrganizName.Text = regCode.OrganizName;
            //txtActiveCounter.Text = regCode.ActiveCounter;
            //txtActValCode.Text = regCode.ActiveValCode;
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGenDog_Click(object sender, EventArgs e)
        {
            string encKey = "CABE123F456C7890ABDF123E456D7890";

            string DogID = txtDogID.Text.Trim();
            string OrgzName = txtOrganizName.Text.Trim();
            string UpdateDate = DateTime.Now.ToString("yyyy-MM-dd");
            string ActiveDate = DateTime.Now.AddDays(30).ToString("yyyy-MM-dd");
            string ActCount = txtActiveCount.Text.Trim();
            string ActValCode1 = txtActValCode.Text.Trim();

            if (ActValCode1.Length == 32)
            {
                StringBuilder sb1 = new StringBuilder();
                StringBuilder sb2 = new StringBuilder();

                int i = 1;
                foreach (char c in ActValCode1)
                {
                    if (i % 4 == 0)
                    {
                        sb1.Append(c);
                    }
                    else
                    {
                        sb2.Append(c);
                    }
                    i++;
                }

                DateTime dt = DateTime.ParseExact(sb1.ToString(), "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);

                if (dt < DateTime.Now)
                {
                    MessageBox.Show("软件授权有效期已到，请联系管理员！");
                    return;
                }

                ActiveDate = dt.ToString("yyyy-MM-dd");
                ActValCode1 = sb2.ToString();

                if (string.IsNullOrEmpty(DogID) || string.IsNullOrEmpty(OrgzName) || string.IsNullOrEmpty(ActValCode1))
                {
                    MessageBox.Show("加密锁编号、单位名称和注册码不能为空！");
                    return;
                }

                //初始化我们的操作加密锁的类
                EncryptDogEx encDog = new EncryptDogEx();

                string sCode = DogID + ";" + ActCount + ";" + OrgzName;
                sCode = encDog.StrEnc(sCode, encKey);
                sCode = encDog.getRegisteNum(sCode);

                string ActValCode2 = sCode;

                if (!ActValCode1.Equals(ActValCode2))
                {
                    MessageBox.Show("注册码错误，请重新输入！");
                    return;
                }


                EncDogModel dogCode = new EncDogModel(DogID, OrgzName, UpdateDate, ActiveDate, ActCount, ActValCode1);
                RegKeyModel regCode = new RegKeyModel(DogID, OrgzName, UpdateDate, ActiveDate, ActCount, ActValCode1);


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

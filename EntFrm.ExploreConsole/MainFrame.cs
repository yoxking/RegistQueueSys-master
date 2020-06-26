using EntFrm.ExploreConsole.Dialogs;
using EntFrm.ExploreConsole.Pubutils;
using NetDimension.NanUI;
using System;
using System.Linq;
using System.Windows.Forms;

namespace EntFrm.ExploreConsole
{
    public partial class MainFrame : Formium
    {
        private string riCardNo;
        public MainFrame() 
				: base("http://my.resource.local/Asserts/index.html") {
              
            InitializeComponent(); 

            LoadHandler.OnLoadStart += LoadHandler_OnLoadStart;
            LoadHandler.OnLoadEnd += LoadHandler_OnLoadEnd;

            //分诊台-刷卡报到
            GlobalObject.AddFunction("Staffer_RCardEnqueueDialog").Execute += (_, args) =>
            {
                this.RequireUIThread(() =>
                {
                    var form1 = new RCardEnqueueDlg();
                    form1.WorkingMode = "STAFF";
                    form1.ShowDialog(this);
                });
            };

            //分诊台-刷卡挂号
            GlobalObject.AddFunction("Staffer_RCardRegisteDialog").Execute += (_, args) =>
            {
                this.RequireUIThread(() =>
                {
                    var form2 = new RCardRegisteDlg();
                    form2.WorkingMode = "STAFF";
                    form2.ShowDialog(this);
                });
            };

            //分诊台-扫码报到
            GlobalObject.AddFunction("Staffer_SCodeEnqueueDialog").Execute += (_, args) =>
            {
                this.RequireUIThread(() =>
                {
                    var form1 = new SCodeEnqueueDlg();
                    form1.WorkingMode = "STAFF";
                    form1.ShowDialog(this);
                });
            };

            //分诊台-扫码挂号
            GlobalObject.AddFunction("Staffer_SCodeRegisteDialog").Execute += (_, args) =>
            {
                this.RequireUIThread(() =>
                {
                    var form2 = new SCodeRegisteDlg();
                    form2.WorkingMode = "STAFF";
                    form2.ShowDialog(this);
                });
            };

            //分诊台-刷卡报到
            GlobalObject.AddFunction("Service_RCardEnqueueDialog").Execute += (_, args) =>
            {
                this.RequireUIThread(() =>
                {
                    var form1 = new RCardEnqueueDlg();
                    form1.WorkingMode = "SERVICE";
                    form1.ShowDialog(this);
                });
            };

            //分诊台-刷卡挂号
            GlobalObject.AddFunction("Service_RCardRegisteDialog").Execute += (_, args) =>
            {
                this.RequireUIThread(() =>
                {
                    var form2 = new RCardRegisteDlg();
                    form2.WorkingMode = "SERVICE";
                    form2.ShowDialog(this);
                });
            };

            //分诊台-扫码报到
            GlobalObject.AddFunction("Service_SCodeEnqueueDialog").Execute += (_, args) =>
            {
                this.RequireUIThread(() =>
                {
                    var form1 = new SCodeEnqueueDlg();
                    form1.WorkingMode = "SERVICE";
                    form1.ShowDialog(this);
                });
            };

            //分诊台-扫码挂号
            GlobalObject.AddFunction("Service_SCodeRegisteDialog").Execute += (_, args) =>
            {
                this.RequireUIThread(() =>
                {
                    var form2 = new SCodeRegisteDlg();
                    form2.WorkingMode = "SERVICE";
                    form2.ShowDialog(this);
                });
            };

            //虚拟挂号-刷卡登录
            GlobalObject.AddFunction("RCardSigninDialog").Execute += (_, args) =>
            {
                this.RequireUIThread(() =>
                {
                    var form2 = new RCardSigninDlg();
                    if (form2.ShowDialog(this) == DialogResult.OK)
                    {
                        riCardNo = form2.sStrInput;

                        string homeUrl = PublicHelper.GetHomeUrl();
                        homeUrl = homeUrl+ "/IRTicket/BranchList";
                        this.LoadUrl(homeUrl); 
                    }
                });
            };

            //虚拟挂号-确认挂号
            GlobalObject.AddFunction("RCardConfirmDialog").Execute += (_, args) =>
            {
                this.RequireUIThread(() =>
                {
                    try
                    {
                        string serviceNo = args.Arguments.FirstOrDefault(p => p.IsString).ToString();

                        var form2 = new RCardConfirmDlg();
                        form2.sServiceNo = serviceNo;
                        form2.sStafferNo = "";
                        form2.sRiCardNo = riCardNo;
                        if (form2.ShowDialog(this) == DialogResult.OK)
                        {
                            string homeUrl = PublicHelper.GetHomeUrl();
                            homeUrl = homeUrl + "/IRTicket/Index";
                            this.LoadUrl(homeUrl);
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                });
            };
            
            //药房-取药报到
            GlobalObject.AddFunction("RCardRecipeDialog").Execute += (_, args) =>
            {
                this.RequireUIThread(() =>
                {
                    var form2 = new RCardRecipeDlg();
                    form2.ShowDialog(this);
                });
            };
        }

        private void MainFrame_Load(object sender, EventArgs e)
        {
            string homeUrl = PublicHelper.GetHomeUrl();
            string workMode = PublicHelper.GetConfigValue("WorkMode");
            bool isFull = bool.Parse(PublicHelper.GetIsFull());

            if (workMode.Equals("虚拟挂号模式"))
            {
                if (isFull)
                {
                    this.FormBorderStyle = FormBorderStyle.None;
                    this.WindowState = FormWindowState.Maximized;
                }
                homeUrl += "/IRTicket/Index";
                this.LoadUrl(homeUrl);
            }
            else if (workMode.Equals("取药报到模式"))
            {
                if (isFull)
                {
                    this.FormBorderStyle = FormBorderStyle.None;
                    this.WindowState = FormWindowState.Maximized;
                }
                homeUrl += "/IAdapter/Index";
                this.LoadUrl(homeUrl);
            }
            else    //分诊台模式
            {
                if (isFull)
                {
                    this.WindowState = FormWindowState.Maximized;
                }
                homeUrl += "/";
            }
        }

        private void LoadHandler_OnLoadStart(object sender, Chromium.Event.CfxOnLoadStartEventArgs e)
        {
            #if DEBUG
                        //Chromium.ShowDevTools();
            #endif
        }

        private void LoadHandler_OnLoadEnd(object sender, Chromium.Event.CfxOnLoadEndEventArgs e)
        {
            if (e.Frame.IsMain)
            {
                string homeUrl = PublicHelper.GetHomeUrl(); 
                 
                ExecuteJavascript("loadHomeUrl('" + homeUrl + "')"); 

            }
        }

        private void myNotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Minimized;
                this.Hide();
            }
            else if (this.WindowState == FormWindowState.Minimized)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                this.Activate();
            }
        }

        private void frmMainFrame_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;

            this.WindowState = FormWindowState.Minimized;
            this.Hide();
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            onExitSystem();
        }

        private void showMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        } 

        private void onExitSystem()
        {
            if (MessageBox.Show("你确定要退出程序吗？", "确认", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {  
                this.Close();
                this.Dispose();
                Application.Exit();
            }
        }

        private void settingMenuItem_Click(object sender, EventArgs e)
        {
            SettingDlg dlg = new SettingDlg();
            dlg.Show();
        }
    }
}

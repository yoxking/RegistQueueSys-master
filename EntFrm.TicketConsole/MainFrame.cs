using EntFrm.Business.Model;
using EntFrm.Framework.Utility;
using EntFrm.TicketConsole.MPublicUtils;
using EntFrm.TicketConsole.RegBusiness;
using Newtonsoft.Json;
using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace EntFrm.TicketConsole
{
    public partial class MainFrame : Form
    {
        private BackgroundWorker bkWorker = new BackgroundWorker();
        private DateTime lastTime;
        private int clickTimes=1;
        private bool printTicket = false;
        private bool printRecipe = false;

        #region 
        private string ticketStyle = "";
        private StringReader streamToPrint = null;
        //private StreamReader streamToPrint = null;
        private Font printFont;
        private int iCheckPrint;
        //private int iTicketPrintCount = 1;
        private bool bUse80Printer = true;
        private PrintDocument pdTicket = new PrintDocument();
        private PageSetupDialog psdTicket = new PageSetupDialog();
        private int iPageWidth58 = 228;
        private int iPageWidth80 = 314;
        private int iPageHeight = 1169;
        #endregion
         

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;

                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED    

                if (this.IsXpOr2003 == true)
                {
                    cp.ExStyle |= 0x00080000;  // Turn on WS_EX_LAYERED  
                    this.Opacity = 1;
                }

                return cp;

            }

        }  //防止闪烁  

        private Boolean IsXpOr2003
        {
            get
            {
                OperatingSystem os = Environment.OSVersion;
                Version vs = os.Version;

                if (os.Platform == PlatformID.Win32NT)
                    if ((vs.Major == 5) && (vs.Minor != 0))
                        return true;
                    else
                        return false;
                else
                    return false;
            }
        }

        public MainFrame()
        {
            InitializeComponent();
        }

        private void MainFrame_Load(object sender, EventArgs e)
        {
            printTicket= bool.Parse(IPublicHelper.GetConfigValue("PrintTicket"));
            printRecipe = bool.Parse(IPublicHelper.GetConfigValue("PrintRecipe"));

            Image bgImage = Image.FromFile(Application.StartupPath + "\\AppImages\\BackgroundImage.jpg");
            if (bgImage != null)
            {
                this.pnlContainer.BackgroundImage = bgImage;
                this.pnlContainer.BackgroundImageLayout = ImageLayout.Stretch;
            }

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            this.lbTimeStr.Location = new System.Drawing.Point(this.Width - 300, this.Location.Y + 30);
            this.btnRegistUser.Location = new System.Drawing.Point(this.Width - 310, this.Location.Y + 80);

            //clockTime = 15;

            bkWorker.WorkerReportsProgress = true;
            bkWorker.WorkerSupportsCancellation = true;
            bkWorker.DoWork += new DoWorkEventHandler(DoWork);
            bkWorker.ProgressChanged += new ProgressChangedEventHandler(ProgessChanged);
            bkWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(CompleteWork);

            bkWorker.RunWorkerAsync();
            Init_TicketStyle();
            Init_PrintDocument();
        }

        private void Init_TicketStyle()
        {
            ticketStyle = IUserContext.OnExecuteCommand_Xp("getTicketStyle", new string[] { "" }); 
        }

        private void Init_PrintDocument()
        {
            this.pdTicket.PrintController = new StandardPrintController();
            this.pdTicket.BeginPrint += new PrintEventHandler(this.pdTicket_BeginPrint);
            this.pdTicket.PrintPage += new PrintPageEventHandler(this.pdTicket_PrintPage);
            this.pdTicket.EndPrint += new PrintEventHandler(this.pdTicket_EndPrint);
            this.psdTicket.Document = this.pdTicket;
            this.psdTicket.PageSettings.Margins = new Margins(10, 10, 0, 0);
            this.psdTicket.PageSettings.PaperSize = new PaperSize("paper", this.bUse80Printer ? this.iPageWidth80 : this.iPageWidth58, this.iPageHeight);

        }

        private void pdTicket_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Print the content of RichTextBox. Store the last character printed.
            iCheckPrint = txtTicketStyle.Print(iCheckPrint, txtTicketStyle.TextLength, e);
            //Check for more pages
            if (iCheckPrint < txtTicketStyle.TextLength)
                e.HasMorePages = true;
            else
                e.HasMorePages = false;
        }

        private void pdTicket_BeginPrint(object sender, PrintEventArgs e)
        {
            this.iCheckPrint = 0;
            printFont = txtTicketStyle.Font;//打印使用的字体 
            streamToPrint = new StringReader(txtTicketStyle.Text);//打印richTextBox1.Text 

            //如预览文件改为：
            //streamToPrint=new StreamReader("d:\\new.rtf");
        }

        private void pdTicket_EndPrint(object sender, PrintEventArgs e)
        {
            if (streamToPrint != null) streamToPrint.Close();//释放不用的资源
        }

        private void MainFrame_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = true;
        }

        public void DoWork(object sender, DoWorkEventArgs e)
        {
            // 事件处理，指定处理函数  
            e.Result = ProcessProgress(bkWorker, e);
        }


        public void ProgessChanged(object sender, ProgressChangedEventArgs e)
        {
            this.lbTimeStr.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public void CompleteWork(object sender, RunWorkerCompletedEventArgs e)
        { 
        }

        private int ProcessProgress(object sender, DoWorkEventArgs e)
        {
            //判断是否请求了取消后台操作
            if (bkWorker.CancellationPending)
            {
                e.Cancel = true;
            }
            else
            {
                while (true)
                {
                    bkWorker.ReportProgress(10);
                    Thread.Sleep(1000);
                }
            }

            return -1;
        }

        private void lbTimeStr_Click(object sender, EventArgs e)
        {
            TimeSpan clickInterval = DateTime.Now - lastTime;

            if (clickInterval.Seconds < 4)
            {
                clickTimes++;
            }
            else
            {
                clickTimes = 0;
            }

            if (clickTimes > 2)
            {
                clickTimes = 0;

                ContextDlg dlg = new ContextDlg();
                if (dlg.ShowDialog() == DialogResult.Yes)
                {
                    this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
                }
            }

            lastTime = DateTime.Now;
        }

        private void btnRegistUser_Click(object sender, EventArgs e)
        {
            ScanBcode dlg = new ScanBcode();
            if (dlg.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(dlg.StrInput))
            {
                string sPFlowNo = RegisterFactory.Create().RegisterScanCode(dlg.StrInput);
                //string sPFlowNo = RegisterFactory.Create().RegisterScanCode("201602822317");

            if (!string.IsNullOrEmpty(sPFlowNo))
                {
                    if (printTicket)
                    {
                        //txtTicketStyle.Rtf = IPublicHelper.ReplaceVariables_Recipe(ticketStyle, sPFlowNo);
                        txtTicketStyle.Rtf = IPublicHelper.ReplaceVariables(ticketStyle, sPFlowNo);
                        this.pdTicket.PrinterSettings.PrinterName = IPublicHelper.Get_PrinterName();
                        this.pdTicket.Print();
                    }
                    else
                    {
                        MessageBox.Show("报到成功，请您在等候区等候叫号就诊！");
                    }

                    //打印处方表格
                    if (printRecipe)
                    {
                        PrintRecipe(sPFlowNo);
                    }
                }
                else
                {
                    MessageBox.Show("报到失败，请检查您的挂号单！");
                }
            }
        }

        private void PrintRecipe(string recipeNos)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                string[] recipeArr = recipeNos.Split(',');

                foreach (string recipeNo in recipeArr)
                {

                    string pdfFile = Application.StartupPath + "\\AppTemps\\" + CommonHelper.Get_New12ByteGuid("Pdf") + ".pdf";
                    string ruserName = GetRuserName(recipeNo);
                    double amount = 0;
                    DataTable dt = ConvertToDataTable(recipeNo, ref amount);

                    PdfPrintHelper pdfile = new PdfPrintHelper("A5", 10, 10, 5, 10);
                    pdfile.Open(new FileStream(pdfFile, FileMode.Create));
                    string fontPath = Environment.GetEnvironmentVariable("WINDIR") + "\\FONTS\\SIMSUN.TTC,0";
                    pdfile.SetBaseFont(fontPath);
                    pdfile.AddParagraph("璧山区妇幼保健院用药指导单", 12, 1, 5, 0, 0);
                    pdfile.AddParagraph("编号："+recipeNo+"       患者姓名：" + ruserName, 10, 0, 0, 0, 0);
                    pdfile.AddParagraph(" ", 4, 0, 0, 0, 0);

                    pdfile.AddTable(dt, 6);

                    sb.Clear();
                    sb.Append("总金额：" + amount.ToString("0.00") + "元                             打印时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    pdfile.AddParagraph(sb.ToString(), 8, 0, 0, 0, 0);

                    pdfile.Close();

                    PdfDocument pdf = new PdfDocument();
                    pdf.LoadFromFile(pdfFile); 

                    PrintDocument printDoc = pdf.PrintDocument;
                    printDoc.PrintController = new StandardPrintController();
                    printDoc.PrinterSettings.PrinterName = IPublicHelper.Get_Printer2Name();
                    printDoc.Print();
                    pdf.Close();
                }
            }
            catch (Exception ex)
            { 
            }
        }

        public string GetRuserName(string recipeNo)
        {
           return  IUserContext.OnExecuteCommand_Xp("GetRuserNameByRecipeNo", new string[] { recipeNo });
        }

        public DataTable ConvertToDataTable(string recipeNo,ref double amount)
        {
            try
            {
                ///
                ///将Collection装为DataTable
                ///
                DataTable dt = new DataTable();
                DataColumn dc;
                DataRow dr;
                dc = new DataColumn("药名");
                dt.Columns.Add(dc);
                dc = new DataColumn("规格");
                dt.Columns.Add(dc);
                dc = new DataColumn("数量");
                dt.Columns.Add(dc);
                dc = new DataColumn("频次");
                dt.Columns.Add(dc);
                dc = new DataColumn("用量");
                dt.Columns.Add(dc);
                dc = new DataColumn("用法");
                dt.Columns.Add(dc);

                string s = IUserContext.OnExecuteCommand_Xp("GetRecipeDetailsByRecipeNo", new string[] { recipeNo });

                if (!string.IsNullOrEmpty(s))
                {
                    List<RecipeDetails> recipeList = JsonConvert.DeserializeObject<List<RecipeDetails>>(s);

                    int i = 1;
                    amount = 0;
                    foreach (RecipeDetails info in recipeList)
                    {
                        dr = dt.NewRow();
                        dr["药名"] = info.sRecipeName;
                        dr["规格"] = info.sRecipeSpec;
                        dr["数量"] = info.sSQuantity;
                        dr["频次"] = info.sFrequency;
                        dr["用量"] = info.sTQuantity;
                        dr["用法"] = info.sDirection;
                        dt.Rows.Add(dr);

                        i++;
                        amount += info.dAmount;
                    }
                }

                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
} 
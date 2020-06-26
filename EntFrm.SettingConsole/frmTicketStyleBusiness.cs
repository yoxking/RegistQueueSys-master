using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

namespace EntFrm.SettingConsole
{
    public partial class frmTicketStyleBusiness : Form
    {
        private TicketStyleBLL myBoss;
        private string sCurrentNo;
        private int iSelectedRow;
        private UpdateType updateType = UpdateType.Upt;


        #region  打印设置 
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

        public frmTicketStyleBusiness()
        {
            InitializeComponent();
        }

        private void frmTicketStyleBusiness_Load(object sender, EventArgs e)
        {
            myBoss = new TicketStyleBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
            sCurrentNo = "0";
            iSelectedRow = 0; 
            InfoList.MultiSelect = false;

            DoBindVariableSource(); 
            InitPrintDocument();
            DoBindDataList();
            DoRefreshForm();
        }

        private void InitPrintDocument()
        {
            this.pdTicket.PrintController = new StandardPrintController();
            this.pdTicket.BeginPrint += new PrintEventHandler(this.pdTicket_BeginPrint);
            this.pdTicket.PrintPage += new PrintPageEventHandler(this.pdTicket_PrintPage);
            this.pdTicket.EndPrint += new PrintEventHandler(this.pdTicket_EndPrint);
            this.psdTicket.Document = this.pdTicket;
            this.psdTicket.PageSettings.Margins = new Margins(10, 10, 0, 0);
            this.psdTicket.PageSettings.PaperSize = new PaperSize("paper", this.bUse80Printer ? this.iPageWidth80 : this.iPageWidth58, this.iPageHeight);

        }

        private void DoBindDataList()
        {
            int count = 0;
            string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' ";
            TicketStyleCollections InfoColl = myBoss.GetRecordsByPaging(ref count, 1, 100, sWhere); ;

            if (InfoColl != null && InfoColl.Count > 0 && sCurrentNo == "0")
            {
                sCurrentNo = InfoColl[0].sStyleNo;
            }

           InfoList.AutoGenerateColumns = false;
           InfoList.DataSource = InfoColl;
        }

        private void DoBindVariableSource()
        {
            List<ItemObject> varsList = IPublicConsts.GetPublicVariables();

            dpVariables.DataSource = varsList;
            dpVariables.ValueMember = "Value";
            dpVariables.DisplayMember = "Name";
        }  

        private void DoRefreshForm()
        {
            try
            {
                TicketStyle info = myBoss.GetRecordByNo(sCurrentNo);
                if (info != null)
                {
                    sCurrentNo = info.sStyleNo;

                    txtStyleName.Text = info.sStyleName;
                    txtTicketFormat.Rtf = info.sTicketFormat;
                     
                }
                SetFocusedColumn();

                updateType = UpdateType.Upt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("出错提示:" + ex.Message);
            }
        }

        private void SetFocusedColumn()
        {
            try
            {
                this.InfoList.Rows[iSelectedRow].Selected = true;
                this.InfoList.CurrentCell = this.InfoList.Rows[iSelectedRow].Cells[0];
            }
            catch (Exception ex)
            { }
        }

        private string GetFocusedColumnValue(string columnName)
        {
            try
            {
                string sResult = "";

                if (this.InfoList.SelectedRows.Count > 0)
                {
                    sResult = this.InfoList.SelectedRows[0].Cells[columnName].Value.ToString();
                    iSelectedRow = this.InfoList.SelectedRows[0].Index;
                }
                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        private void InfoList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string sNo = GetFocusedColumnValue("StyleNo");
            if (!sNo.Equals(sCurrentNo))
            {
                sCurrentNo = sNo;
                DoRefreshForm();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int count = myBoss.GetCountByCondition(" BranchNo = '" + IUserContext.GetBranchNo() + "' ") + 1;
                txtStyleName.Text = "样式" + count;

                updateType = UpdateType.Add;
                btnSave_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("出错提示:" + ex.Message);
            }
        } 

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            { 
                if (MessageBox.Show("您确定要删除所选信息?", "确认对话框", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string sNo = GetFocusedColumnValue("StyleNo");
                    if (myBoss.HardDeleteRecord(sNo))
                    {
                        //MessageBox.Show("删除操作成功!");
                        DoBindDataList();
                        DoRefreshForm();
                    }
                    else
                    {
                        MessageBox.Show("删除操作失败!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("出错提示:" + ex.Message);
            }
        }

        private void btnAddVar_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();

            Clipboard.SetDataObject(dpVariables.SelectedValue.ToString(), true);
            txtTicketFormat.Paste();
        } 


        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (updateType == UpdateType.Add)
                {
                    TicketStyle style = new TicketStyle();

                    style.sStyleNo = CommonHelper.Get_New12ByteGuid();
                    style.sStyleName = txtStyleName.Text.Trim();
                    style.iIsTemplet = 0;
                    style.sTicketFormat = txtTicketFormat.Rtf;
                    style.sBranchNo = IUserContext.GetBranchNo();

                    style.sComments = "";
                    style.sAddOptor = "00000000";
                    style.dAddDate = DateTime.Now;
                    style.sModOptor = "00000000";
                    style.dModDate = DateTime.Now;
                    style.iValidityState = 1;
                    style.sAppCode = IUserContext.GetAppCode() + ";";

                    if (myBoss.AddNewRecord(style))
                    {
                        //MessageBox.Show("操作成功!");
                        DoBindDataList();
                        DoRefreshForm();
                        return;
                    }

                    MessageBox.Show("新增操作失败!");
                }
                else
                { 
                    TicketStyle style = myBoss.GetRecordByNo(this.sCurrentNo);
                    if (style != null)
                    {

                        style.sStyleName = txtStyleName.Text.Trim();

                        style.sTicketFormat = txtTicketFormat.Rtf; ;
                        style.dModDate = DateTime.Now;

                        if (myBoss.UpdateRecord(style))
                        {
                            MessageBox.Show("操作成功!");
                            DoBindDataList();
                            DoRefreshForm();
                            return;
                        }
                    }

                    MessageBox.Show("更新操作失败!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("出错提示:" + ex.Message);
            }
        }

        private void btnAddImg_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.Clear();

                OpenFileDialog ofdialog = new OpenFileDialog();
                ofdialog.Filter = "图片文件|*.bmp;*.gif;*.jpeg;*.jpg;*.png";
                if (ofdialog.ShowDialog() == DialogResult.OK)
                {
                    Clipboard.SetDataObject(Image.FromFile(ofdialog.FileName), false);
                    txtTicketFormat.Paste();

                    Clipboard.Clear();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void btnSetFont_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FontDialog fdlg_SendMessage = new FontDialog();
            if (DialogResult.OK == fdlg_SendMessage.ShowDialog())
            {
                txtTicketFormat.SelectionFont = fdlg_SendMessage.Font;
                txtTicketFormat.SelectionColor = fdlg_SendMessage.Color;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //this.pdTicket.PrinterSettings.PrinterName = IPublicHelper.Get_PrinterName();
            this.pdTicket.Print();
        }

        private void pdTicket_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Print the content of RichTextBox. Store the last character printed.
            iCheckPrint = txtTicketFormat.Print(iCheckPrint, txtTicketFormat.TextLength, e);
            //Check for more pages
            if (iCheckPrint < txtTicketFormat.TextLength)
                e.HasMorePages = true;
            else
                e.HasMorePages = false;
        }

        private void pdTicket_BeginPrint(object sender, PrintEventArgs e)
        {
            this.iCheckPrint = 0;
            printFont = txtTicketFormat.Font;//打印使用的字体 
            streamToPrint = new StringReader(txtTicketFormat.Text);//打印richTextBox1.Text 

            //如预览文件改为：
            //streamToPrint = new StreamReader("d:\\new.rtf");
        }

        private void pdTicket_EndPrint(object sender, PrintEventArgs e)
        {
            if (streamToPrint != null) streamToPrint.Close();//释放不用的资源
        }
    }
}

using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using System;
using System.IO;
using System.Windows.Forms;

namespace EntFrm.SettingConsole
{
    public partial class frmStafflistBusiness : Form
    {
        private StafferInfoBLL myBoss;
        private string sCurrentNo;
        private int iSelectedRow;
        private UpdateType updateType = UpdateType.Upt;

        public frmStafflistBusiness()
        {
            InitializeComponent();
        }

        private void frmStafflistBusiness_Load(object sender, EventArgs e)
        { 
            myBoss = new StafferInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
            sCurrentNo = "0";
            iSelectedRow = 0;
            InfoList.MultiSelect = false;
            dpStarLevel.SelectedIndex = 0;

            DoBindDataList();
            DoRefreshForm();
        }

        private void DoBindDataList()
        {
            int count = 0;
            string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' ";
            StafferInfoCollections InfoColl = myBoss.GetRecordsByPaging(ref count, 1, 100, sWhere);

            if (InfoColl != null && InfoColl.Count > 0 )
            {
                if (sCurrentNo == "0")
                {
                sCurrentNo = InfoColl[0].sStafferNo;
                }
                foreach (StafferInfo staff in InfoColl)
                {
                    staff.sCounterNo = IPublicHelper.GetCounterNameByNo(staff.sCounterNo);
                }
            }

            InfoList.AutoGenerateColumns = false;
            InfoList.DataSource = InfoColl;
        }

        //private void DoBindCounterList()
        //{
        //    CounterInfoBusiness counterBoss = new CounterInfoBusiness(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
        //    CounterInfoCollections counterColl = counterBoss.GetAllRecords();

        //    dpCounterList.DataSource = counterColl;
        //    dpCounterList.ValueMember = "sCounterNo";
        //    dpCounterList.DisplayMember = "sCounterName";
        //}

        private void DoRefreshForm()
        {
            try
            {
                StafferInfo info = myBoss.GetRecordByNo(sCurrentNo);
                if(info!=null)
                {
                    txtLoginId.Text = info.sLoginId;
                    txtStafferName.Text = info.sStafferName;
                    txtPsword.Text = info.sPassword;
                    txtOrganizName.Text = info.sOrganizName;
                    //dpCounterList.SelectedValue = info.sCounterNo;
                    dpStarLevel.SelectedItem = info.sStarLevel;
                    txtHeadPhoto.Text = info.sHeadPhoto;
                    txtSummary.Text = info.sSummary;
                }

                SetFocusedColumn();

                updateType = UpdateType.Upt;
            }
            catch(Exception ex)
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
            catch(Exception ex)
            {
                return "";
            }
        }

        private void InfoList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string sNo = GetFocusedColumnValue("StafferNo");
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
                int count = myBoss.GetCountByCondition("  BranchNo = '" + IUserContext.GetBranchNo() + "' ") + 1;

                string temp = count.ToString().PadLeft(3, '0');
                txtLoginId.Text = temp;
                txtStafferName.Text = temp;
                txtPsword.Text = temp;
                txtOrganizName.Text = "";
                txtHeadPhoto.Text = "";

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
                    string sNo = GetFocusedColumnValue("StafferNo");
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (updateType == UpdateType.Add)
                {
                    StafferInfo info = new StafferInfo();
                    info.sStafferNo = CommonHelper.Get_New12ByteGuid();
                    info.sStafferName = txtStafferName.Text.Trim();
                    info.sLoginId = txtLoginId.Text.Trim();
                    info.sPassword = txtPsword.Text.Trim();
                    info.sOrganizNo = "";
                    info.sOrganizName = txtOrganizName.Text.Trim();
                    info.sCounterNo = "";
                    info.sStarLevel = dpStarLevel.SelectedItem.ToString();
                    info.sHeadPhoto = txtHeadPhoto.Text.Trim();
                    info.sRanks = "";
                    info.sPosts = "";
                    info.sSummary = txtSummary.Text.Trim();
                    info.sBranchNo = IUserContext.GetBranchNo();
                    info.sComments = "";

                    info.sAddOptor = "00000000";
                    info.dAddDate = DateTime.Now;
                    info.sModOptor = "00000000";
                    info.dModDate = DateTime.Now;
                    info.iValidityState = 1;
                    info.sAppCode = IUserContext.GetAppCode() + ";";

                    if (myBoss.AddNewRecord(info))
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
                    StafferInfo info = myBoss.GetRecordByNo(this.sCurrentNo);
                    if (info != null)
                    { 
                        info.sStafferName = txtStafferName.Text.Trim();
                        info.sLoginId = txtLoginId.Text.Trim();
                        info.sPassword = txtPsword.Text.Trim();
                        info.sOrganizName = txtOrganizName.Text.Trim();
                        info.sCounterNo = "";
                        info.sStarLevel = dpStarLevel.SelectedItem.ToString();
                        info.sHeadPhoto = txtHeadPhoto.Text.Trim();
                        info.sRanks = "";
                        info.sPosts = "";
                        info.sSummary = txtSummary.Text.Trim();

                        info.sModOptor = "00000000";
                        info.dModDate = DateTime.Now;

                        if (myBoss.UpdateRecord(info))
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

        private void btnChoicePhoto_Click(object sender, EventArgs e)
        {

            try
            {
                  //创建一个对话框对象
                    OpenFileDialog ofd = new OpenFileDialog();
                    //为对话框设置标题
                    ofd.Title = "请选择上传的图片";
                    //设置筛选的图片格式
                    ofd.Filter = "图片格式|*.jpg";
                    //设置是否允许多选
                    ofd.Multiselect = false;
                    //如果你点了“确定”按钮
                    if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        //获得文件的完整路径（包括名字后后缀）
                        string filePath = ofd.FileName;
                        //找到文件名比如“1.jpg”前面的那个“\”的位置
                        int position = filePath.LastIndexOf("\\");
                        //从完整路径中截取出来文件名“1.jpg”
                        string fileName = "\\AppImages\\HeadPhoto" + CommonHelper.Get_New8ByteGuid() + ".jpg";
                        //读取选择的文件，返回一个流
                        using (Stream stream = ofd.OpenFile())
                        {
                            //创建一个流，用来写入得到的文件流（注意：创建一个名为“Images”的文件夹，如果是用相对路径，必须在这个程序的Degug目录下创建
                            //如果是绝对路径，放在那里都行，我用的是相对路径）
                            using (FileStream fs = new FileStream(System.Windows.Forms.Application.StartupPath + fileName, FileMode.CreateNew))
                            {
                                //将得到的文件流复制到写入流中
                                stream.CopyTo(fs);
                                //将写入流中的数据写入到文件中
                                fs.Flush();
                            }
                            //PictrueBOx 显示该图片，此时这个图片已经被复制了一份在Images文件夹下，就相当于上传 
                            //将文件路径显示在文本框中 
                            txtHeadPhoto.Text = fileName;
                        }
                    } 
            }
            catch (Exception ex)
            {

            }
        }

        private void btnUptAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("确认对话框", "您确定要批量修改？", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }

                StafferInfoCollections infoColl = myBoss.GetAllRecords();
                if (infoColl != null && infoColl.Count > 0)
                {
                    foreach (StafferInfo info in infoColl)
                    {
                        info.sStafferName = txtStafferName.Text.Trim();
                        info.sLoginId = txtLoginId.Text.Trim();
                        info.sPassword = txtPsword.Text.Trim();
                        info.sOrganizName = txtOrganizName.Text.Trim();
                        info.sCounterNo = "";
                        info.sStarLevel = dpStarLevel.SelectedItem.ToString();
                        info.sHeadPhoto = txtHeadPhoto.Text.Trim();

                        info.sModOptor = "00000000";
                        info.dModDate = DateTime.Now;

                        myBoss.UpdateRecord(info);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

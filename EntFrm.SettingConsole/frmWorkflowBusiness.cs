using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace EntFrm.SettingConsole
{
    public partial class frmWorkflowBusiness : Form
    {
        private ServiceInfoBLL myBoss;
        private string sCurrentNo;
        private int iSelectedRow;
        private UpdateType updateType = UpdateType.Upt;

        List<ItemObject> listItem = new List<ItemObject>();

        public frmWorkflowBusiness()
        {
            InitializeComponent();
        }

        private void frmWorkflowBusiness_Load(object sender, EventArgs e)
        {
            myBoss = new ServiceInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
            sCurrentNo = "0";
            iSelectedRow = 0;
            InfoList.MultiSelect = false;

            DoBindDataList();
            DoBindServices(); 
            DoRefreshForm();
        }

        private void DoBindDataList()
        {
            int i = 0;
            ServiceInfoCollections InfoColl = myBoss.GetRecordsByPaging(ref i, 1, 100, " BranchNo = '" + IUserContext.GetBranchNo() + "' And HaveChild=0");

            if (InfoColl != null && InfoColl.Count > 0 && sCurrentNo == "0")
             {
                 sCurrentNo = InfoColl[0].sServiceNo;
             }

             InfoList.AutoGenerateColumns = false;
             InfoList.DataSource = InfoColl;
        }

        private void DoBindServices()
        {
            int i = 0;
            ServiceInfoCollections serviceColl = myBoss.GetRecordsByPaging(ref i, 1, 100, " BranchNo = '" + IUserContext.GetBranchNo() + "'  And HaveChild=0");

            dpServiceList.DataSource = serviceColl;
            dpServiceList.DisplayMember = "sServiceName";
            dpServiceList.ValueMember = "sServiceNo"; 
        } 
         

        private void DoRefreshForm()
        {
            try
            {   
                listItem.Clear();
                ServiceInfo wflow = myBoss.GetRecordByNo(this.sCurrentNo);
                if (wflow != null)
                {
                    txtServiceName.Text = wflow.sServiceName;
                    ItemObject item = null;
                    string[] sSteps = wflow.sWorkflowValue.Split(';');
                    foreach (string step in sSteps)
                    {
                        if (!string.IsNullOrEmpty(step))
                        {
                            item = new ItemObject(step, IPublicHelper.GetServiceNameByNo(step));
                            listItem.Add(item);
                        }
                    }
                }

                BindingSource bs = new BindingSource();
                bs.DataSource = listItem;
                lbWorkflows.DataSource = bs;
                lbWorkflows.ValueMember = "KeyName";
                lbWorkflows.DisplayMember = "Value";

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
            string sNo = GetFocusedColumnValue("ServiceNo");
            if (!sNo.Equals(sCurrentNo))
            {
                sCurrentNo = sNo; 
                DoRefreshForm();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        { 
            //try
            //{
            //    txtWFlowName.Text="";

            //    listItem.Clear();
            //    BindingSource bs = new BindingSource();
            //    bs.DataSource = listItem;

            //    lbWorkflows.DataSource = bs;

            //    updateType = UpdateType.Add;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("出错提示:" + ex.Message);
            //}
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (MessageBox.Show("您确定要删除所选信息?", "确认对话框", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //    {
            //        string sNo = GetFocusedColumnValue("ServiceNo");
            //        if (myBoss.HardDeleteRecord(sNo))
            //        {
            //            //MessageBox.Show("删除操作成功!");
            //            DoBindDataList();
            //            DoRefreshForm();
            //        }
            //        else
            //        {
            //            MessageBox.Show("删除操作失败!");
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("出错提示:" + ex.Message);
            //}
        }

        private string getWorkFlow_Text()
        {
            StringBuilder sb=new StringBuilder();
            if(listItem.Count>0)
            {
                foreach(ItemObject item in listItem)
                {
                    sb.Append( "->"+item.Value);
                }
            }
             
            return sb.ToString(); 
        }

        private string getWorkFlow_Value()
        {
            StringBuilder sb = new StringBuilder();
            if (listItem.Count > 0)
            {
                foreach (ItemObject item in listItem)
                {
                    sb.Append(";"+item.Name);
                }
            }

            string s = sb.ToString();
            if (!string.IsNullOrEmpty(s))
            {
                s = s.Substring(1);
            }
            return s;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtServiceName.Text.Trim()))
                {
                    MessageBox.Show("流程名称不能为空！");
                    return;
                }

                if (updateType == UpdateType.Upt)
                {
                    ServiceInfo info = myBoss.GetRecordByNo(this.sCurrentNo);
                    if (info != null)
                    { 
                        info.sWorkflowValue = getWorkFlow_Value();
                        info.sWorkflowText = getWorkFlow_Text();

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

        private void btnAddService_Click(object sender, EventArgs e)
        {
            int i=lbWorkflows.SelectedIndex;
            string s = dpServiceList.SelectedValue.ToString(); 
            ItemObject item=new ItemObject(s, IPublicHelper.GetServiceNameByNo(s));

            if ( i> -1)
            {
                listItem.Insert(i+1, item);

            }
            else
            {
                i = 0;
                listItem.Add(item);
            }

            BindingSource bs = new BindingSource();
            bs.DataSource = listItem;

            lbWorkflows.DataSource = bs;

            lbWorkflows.SetSelected(i, true);       
        }

        private void btnDelService_Click(object sender, EventArgs e)
        { 
            int i = lbWorkflows.SelectedIndex;
            if (i > -1)
            {
                string sValue = lbWorkflows.SelectedValue.ToString();
                listItem.RemoveAt(lbWorkflows.SelectedIndex);

                BindingSource bs = new BindingSource();
                bs.DataSource = listItem;

                lbWorkflows.DataSource = bs;

                if (i > 0)
                {
                    lbWorkflows.SetSelected(i - 1, true);
                }
            } 
        }  
         
    }
}

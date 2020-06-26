using EntFrm.Framework.Utility;
using System;
using System.IO;
using System.Windows.Forms;

namespace EntFrm.SettingConsole
{
    public partial class frmEvaluatorBusiness : Form
    {
        public frmEvaluatorBusiness()
        {
            InitializeComponent();
        }

        private void frmEvaluatorBusiness_Load(object sender, EventArgs e)
        {
            try
            {
                string sEvaTitle = IUserContext.GetParamValue(IPublicConsts.DEF_EVATITLE, IPublicConsts.TYPE_EVALUATOR);
                string sEvaBulletin = IUserContext.GetParamValue(IPublicConsts.DEF_EVABULLETIN, IPublicConsts.TYPE_EVALUATOR);
                string sEvaImages = IUserContext.GetParamValue(IPublicConsts.DEF_EVAIMAGES, IPublicConsts.TYPE_EVALUATOR);

                string sSlideTitle = IUserContext.GetParamValue(IPublicConsts.DEF_SLIDETITLE, "Others");
                string sSlideBulletin = IUserContext.GetParamValue(IPublicConsts.DEF_SLIDEBULLETIN, "Others");
                string sSlideHtml = IUserContext.GetParamValue(IPublicConsts.DEF_SLIDEHTML, "Others");
                string sSlideImgs = IUserContext.GetParamValue(IPublicConsts.DEF_SLIDEIMAGES, "Others");

                txtEvaTitle.Text = sEvaTitle;
                txtEvaBulletin.Text = sEvaBulletin;
                ltEvaImages.Items.Clear();

                if (!string.IsNullOrEmpty(sEvaImages))
                {
                    string[] imageList = sEvaImages.Split(';');
                    foreach (string img in imageList)
                    {
                        ltEvaImages.Items.Add(img);
                    }
                }
                 
            }
            catch (Exception ex)
            { }
        }

        private void btnAddImg_Click(object sender, EventArgs e)
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
                    string fileName = "\\AppImages\\SlideImage" + CommonHelper.Get_New8ByteGuid() + ".jpg";
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
                        ltEvaImages.Items.Add(fileName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelImg_Click(object sender, EventArgs e)
        {
            try
            {
                if (ltEvaImages.Items.Count > 0 && ltEvaImages.SelectedItems.Count > 0)
                {
                    ltEvaImages.Items.RemoveAt(ltEvaImages.SelectedIndex);
                }
            }
            catch(Exception ex)
            { }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string sEvaTitle = txtEvaTitle.Text.Trim();
                string sEvaBulletin = txtEvaBulletin.Text.Trim();
                string sEvaImages = "";
                if(ltEvaImages.Items.Count>0)
                {
                    for(int i=0;i<ltEvaImages.Items.Count;i++)
                    {
                        sEvaImages += ltEvaImages.Items[i].ToString()+";";
                    }
                    sEvaImages = sEvaImages.Substring(0, sEvaImages.Length - 1);
                }

                 IUserContext.SetParamValue(IPublicConsts.DEF_EVATITLE,sEvaTitle, IPublicConsts.TYPE_EVALUATOR);
                 IUserContext.SetParamValue(IPublicConsts.DEF_EVABULLETIN, sEvaBulletin,IPublicConsts.TYPE_EVALUATOR);
                 IUserContext.SetParamValue(IPublicConsts.DEF_EVAIMAGES,sEvaImages, IPublicConsts.TYPE_EVALUATOR);

                 MessageBox.Show("保存成功！");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
         
    }
}

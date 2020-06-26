using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = "adsdgdsfgsdfgdsfg(张三阿斯蒂芬)";
            string realName = "";
            if (name.IndexOf('(') < 0)
            {
                realName = name.Trim();
                if ( realName.Length > 1)
                {
                    realName = realName.Substring(0, 1) + "*" + realName.Substring(realName.Length - 1, 1);
                }

                MessageBox.Show(realName);
            }
            else
            {
                realName = name.Trim().Substring(0, name.IndexOf('('));
                string extension = name.Trim().Substring(name.IndexOf('('));
                if (realName.Length > 1)
                {
                    realName = realName.Substring(0, 1) + "*" + realName.Substring(realName.Length - 1, 1);
                }

                MessageBox.Show(realName + extension);
            }
             
        }
    }
}

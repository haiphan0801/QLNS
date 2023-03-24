using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataLayer;
using BusinessLayer;

namespace QLNhanSu
{
    public partial class ThongTinCaNhan : Form
    {
        public ThongTinCaNhan()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
        }
        private void ThongTinCaNhan_Load(object sender, EventArgs e)
        {
          
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
        
                switch (tabControl1.SelectedIndex)
                {
                    case 0:
                        DanToc form1 = new DanToc();
                        form1.TopLevel = false;
                        form1.FormBorderStyle = FormBorderStyle.None;
                        form1.Dock = DockStyle.Fill;
                        tabPage1.Controls.Add(form1);
                        form1.Show();
                        break;
                    case 1:
                        TonGiao form2 = new TonGiao();
                        form2.TopLevel = false;
                        form2.FormBorderStyle = FormBorderStyle.None;
                        form2.Dock = DockStyle.Fill;
                        tabPage2.Controls.Add(form2);
                        form2.Show();
                        break;
                    case 2:
                        TrinhDo form3 = new TrinhDo();
                        form3.TopLevel = false;
                        form3.FormBorderStyle = FormBorderStyle.None;
                        form3.Dock = DockStyle.Fill;
                        tabPage4.Controls.Add(form3);
                        form3.Show();
                        break;
                    default:
                        break;
                }
            
        }
    }
}

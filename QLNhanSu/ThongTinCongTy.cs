using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNhanSu
{
    public partial class ThongTinCongTy : Form
    {
        public ThongTinCongTy()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void ThongTinCongTy_Load(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    CongTy form1 = new CongTy();
                    form1.TopLevel = false;
                    form1.FormBorderStyle = FormBorderStyle.None;
                    form1.Dock = DockStyle.Fill;
                    tabPage1.Controls.Add(form1);
                    form1.Show();
                    break;
                case 1:
                    PhongBan form2 = new PhongBan();
                    form2.TopLevel = false;
                    form2.FormBorderStyle = FormBorderStyle.None;
                    form2.Dock = DockStyle.Fill;
                    tabPage2.Controls.Add(form2);
                    form2.Show();
                    break;
                case 2:
                    BoPhan form3 = new BoPhan();
                    form3.TopLevel = false;
                    form3.FormBorderStyle = FormBorderStyle.None;
                    form3.Dock = DockStyle.Fill;
                    tabPage3.Controls.Add(form3);
                    form3.Show();
                    break;
                case 3:
                    ChucVu form4 = new ChucVu();
                    form4.TopLevel = false;
                    form4.FormBorderStyle = FormBorderStyle.None;
                    form4.Dock = DockStyle.Fill;
                    tabPage4.Controls.Add(form4);
                    form4.Show();
                    break;
                default:
                    break;
            }
        }
    }
}

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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            ThongTinCaNhan form1 = new ThongTinCaNhan();
            form1.TopLevel = false;
            form1.Dock = DockStyle.Fill;
            panel2.Controls.Add(form1);
            form1.Show();
        }

        private void thôngTinCôngTyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            ThongTinCongTy form2 = new ThongTinCongTy();
            form2.TopLevel = false;
            form2.Dock = DockStyle.Fill;
            panel2.Controls.Add(form2);
            form2.Show();
        }
    }
}

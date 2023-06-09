﻿using System;
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

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void khenThưởngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            KhenThuong form2 = new KhenThuong();
            form2.TopLevel = false;
            form2.Dock = DockStyle.Fill;
            panel2.Controls.Add(form2);
            form2.Show();
        }

        private void nhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            NhanVien form2 = new NhanVien();
            form2.TopLevel = false;
            form2.Dock = DockStyle.Fill;
            panel2.Controls.Add(form2);
            form2.Show();
        }

        private void hợpĐồngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            HopDong form2 = new HopDong();
            form2.TopLevel = false;
            form2.Dock = DockStyle.Fill;
            panel2.Controls.Add(form2);
            form2.Show();
        }

        private void kỉLuậtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            KiLuat form2 = new KiLuat();
            form2.TopLevel = false;
            form2.Dock = DockStyle.Fill;
            panel2.Controls.Add(form2);
            form2.Show();
        }

        private void điềuChuyểnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            DieuChuyen form2 = new DieuChuyen();
            form2.TopLevel = false;
            form2.Dock = DockStyle.Fill;
            panel2.Controls.Add(form2);
            form2.Show();
        }

        private void thôiViệcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            ThoiViec form2 = new ThoiViec();
            form2.TopLevel = false;
            form2.Dock = DockStyle.Fill;
            panel2.Controls.Add(form2);
            form2.Show();
        }

        private void bảngCôngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            BangCong form2 = new BangCong();
            form2.TopLevel = false;
            form2.Dock = DockStyle.Fill;
            panel2.Controls.Add(form2);
            form2.Show();
        }

        private void bảngCôngChiTiếtToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void loạiCaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            LoaiCa form2 = new LoaiCa();
            form2.TopLevel = false;
            form2.Dock = DockStyle.Fill;
            panel2.Controls.Add(form2);
            form2.Show();
        }

        private void loạiCôngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            LoaiCong form2 = new LoaiCong();
            form2.TopLevel = false;
            form2.Dock = DockStyle.Fill;
            panel2.Controls.Add(form2);
            form2.Show();
        }
    }
}

using BusinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNhanSu
{
    public partial class TangCa : Form
    {
        public TangCa()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            InitializeComponent();
        }

        TANGCA _tangca;
        NHANVIEN _nhanvien;
        LOAICA _loaica;
        SYS_CONFIG _config;
        bool _them;
        int _id;

        private void TangCa_Load(object sender, EventArgs e)
        {
            _them = false;
            _tangca = new TANGCA();
            _loaica = new LOAICA();
            _nhanvien = new NHANVIEN();
            _config = new SYS_CONFIG();
            _showHide(true);
            loadData();
            loadNhanVien();
            loadLoaiCa();
            panel2.Visible = false;
            dataGridView1.Columns["MANV"].Visible = false;
            dataGridView1.Columns["IDLOAICA"].Visible = false;
            dataGridView1.Columns["CREATED_BY"].Visible = false;
            dataGridView1.Columns["CREATED_DATE"].Visible = false;
            dataGridView1.Columns["UPDATED_BY"].Visible = false;
            dataGridView1.Columns["UPDATED_DATE"].Visible = false;
            dataGridView1.Columns["DELETED_BY"].Visible = false;
            dataGridView1.Columns["DELETED_DATE"].Visible = false;
        }

        void _showHide(bool kt)
        {
            btnLuu.Enabled = !kt;
            btnHuy.Enabled = !kt;
            btnThem.Enabled = kt;
            btnSua.Enabled = kt;
            btnXoa.Enabled = kt;
            btnIn.Enabled = kt;
            btnDong.Enabled = kt;
            txtGhichu.Enabled = !kt;
            nudSoGio.Enabled = !kt;
            cboLoaiCa.Enabled = !kt;
            cboNhanVien.Enabled = !kt;
        }

        void loadNhanVien()
        {
            cboNhanVien.DataSource = _nhanvien.getList();
            cboNhanVien.DisplayMember = "HOTEN";
            cboNhanVien.ValueMember = "MANV";
        }

        void loadLoaiCa()
        {
            cboLoaiCa.DataSource = _loaica.getList();
            cboLoaiCa.DisplayMember = "TENLOAICA";
            cboLoaiCa.ValueMember = "IDLOAICA";
        }

        void loadData()
        {
            dataGridView1.DataSource = _tangca.getListFull();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            _showHide(false);
            _them = true;
            txtGhichu.Text = string.Empty;
            //cboLoaiCa.SelectedIndex = 0;
            //cboNhanVien.SelectedIndex = 0;
            nudSoGio.Value = 0;
            panel2.Visible = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            _them = false;
            _showHide(false);
            panel2.Visible = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xoá hay không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                _tangca.Delete(_id, 1);
                loadData();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            SaveData();
            loadData();
            _them = false;
            _showHide(true);
            panel2.Visible = false;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            _them = false;
            _showHide(true);
            panel2.Visible = false;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {

        }
        void SaveData()
        {
            if (_them)
            {
                tb_TANGCA tc = new tb_TANGCA();
                tc.IDLOAICA = int.Parse(cboLoaiCa.SelectedValue.ToString());
                tc.SOGIO = double.Parse(nudSoGio.Value.ToString());
                tc.MANV = int.Parse(cboNhanVien.SelectedValue.ToString());
                tc.GHICHU = txtGhichu.Text;
                tc.NGAY = DateTime.Now.Day;
                tc.THANG = DateTime.Now.Month;
                tc.NAM = DateTime.Now.Year;
                var lc = _loaica.getItem(int.Parse(cboLoaiCa.SelectedValue.ToString()));
                var cg = _config.getItem("Tăng ca");
                tc.SOTIEN = tc.SOGIO * lc.HESO * int.Parse(cg.VALUES);
                tc.CREATED_BY = 1;
                tc.CREATED_DATE = DateTime.Now;
                _tangca.Add(tc);
            }
            else
            {
                var tc = _tangca.getItem(_id);
                tc.IDLOAICA = int.Parse(cboLoaiCa.SelectedValue.ToString());
                tc.SOGIO = double.Parse(nudSoGio.Value.ToString());
                tc.MANV = int.Parse(cboNhanVien.SelectedValue.ToString());
                tc.GHICHU = txtGhichu.Text;
                
                tc.NGAY = DateTime.Now.Day;
                tc.THANG = DateTime.Now.Month;
                tc.NAM = DateTime.Now.Year;
                var lc = _loaica.getItem(int.Parse(cboLoaiCa.SelectedValue.ToString()));
                var cg = _config.getItem("Tăng ca");
                tc.SOTIEN = tc.SOGIO * lc.HESO * int.Parse(cg.VALUES);
                tc.UPDATED_BY = 1;
                tc.UPDATED_DATE = DateTime.Now;
                _tangca.Update(tc);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                _id = Convert.ToInt32(row.Cells["ID"].Value.ToString());
                var tc = _tangca.getItem(_id);
                cboNhanVien.SelectedValue = tc.MANV;
                cboLoaiCa.SelectedValue = tc.IDLOAICA;
                nudSoGio.Text = tc.SOGIO.ToString(); 
                txtGhichu.Text = tc.GHICHU;
            }
        }
    }
}

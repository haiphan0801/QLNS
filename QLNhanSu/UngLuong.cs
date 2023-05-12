using BusinessLayer;
using DataLayer;
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
    public partial class UngLuong : Form
    {
        public UngLuong()
        {
            InitializeComponent();
        }
        UNGLUONG _ungluong;
        NHANVIEN _nhanvien;
        bool _them;
        int _id;

        private void UngLuong_Load(object sender, EventArgs e)
        {
            _them = false;
            _ungluong = new UNGLUONG();
            _nhanvien = new NHANVIEN();
            _showHide(true);
            loadData();
            loadNhanVien();
            panel2.Visible = false;
            dataGridView1.Columns["MANV"].Visible = false;
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
            cboNhanVien.Enabled = !kt;
            txtSoTien.Enabled = !kt;
        }

        void loadNhanVien()
        {
            cboNhanVien.DataSource = _nhanvien.getListFull();
            cboNhanVien.DisplayMember = "HOTEN";
            cboNhanVien.ValueMember = "MANV";
        }
        void loadData()
        {
            dataGridView1.DataSource = _ungluong.getListFull();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            _showHide(false);
            _them = true;
            txtGhichu.Text = string.Empty;
            //cboNhanVien.SelectedIndex = 0;
            txtSoTien.Text = string.Empty;
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
                _ungluong.Delete(_id, 1);
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
                tb_UNGLUONG ul = new tb_UNGLUONG();
                
                ul.MANV = int.Parse(cboNhanVien.SelectedValue.ToString());
                ul.SOTIEN = double.Parse(txtSoTien.Text);
                ul.GHICHU = txtGhichu.Text;
                ul.NGAY = DateTime.Now.Day;
                ul.THANG = DateTime.Now.Month;
                ul.NAM = DateTime.Now.Year;
                ul.CREATED_BY = 1;
                ul.CREATED_DATE = DateTime.Now;
                _ungluong.Add(ul);
            }
            else
            {
                var ul = _ungluong.getItem(_id);
                
                ul.MANV = int.Parse(cboNhanVien.SelectedValue.ToString());
                ul.SOTIEN = double.Parse(txtSoTien.Text);
                ul.GHICHU = txtGhichu.Text;
                ul.NGAY = DateTime.Now.Day;
                ul.THANG = DateTime.Now.Month;
                ul.NAM = DateTime.Now.Year;
                
                ul.UPDATED_BY = 1;
                ul.UPDATED_DATE = DateTime.Now;
                _ungluong.Update(ul);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                _id = Convert.ToInt32(row.Cells["ID"].Value.ToString());
                var ul = _ungluong.getItem(_id);
                txtGhichu.Text = ul.GHICHU;
                cboNhanVien.SelectedValue = ul.MANV;
                txtSoTien.Text = ul.SOTIEN.ToString();
            }
        }
    }
}

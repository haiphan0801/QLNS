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
    public partial class ThoiViec : Form
    {
        public ThoiViec()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
        }
        bool _them;
        string _soQD;
        NHANVIEN_THOIVIEC _nvtv;
        NHANVIEN _nv;
        PHONGBAN _pb;
        private void ThoiViec_Load(object sender, EventArgs e)
        {
            _them = false;
            _nvtv = new NHANVIEN_THOIVIEC();
            _nv = new NHANVIEN();
            _showHide(true);
            loadNhanVien();
            loadData();
            dataGridView1.CellClick += dataGridView1_CellClick;
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                if (col.Name.StartsWith("tb_"))
                {
                    col.Visible = false;
                }
            }
            dataGridView1.Columns["CREATED_BY"].Visible = false;
            dataGridView1.Columns["CREATED_DATE"].Visible = false;
            dataGridView1.Columns["UPDATED_BY"].Visible = false;
            dataGridView1.Columns["UPDATED_DATE"].Visible = false;
            dataGridView1.Columns["DELETED_BY"].Visible = false;
            dataGridView1.Columns["DELETED_DATE"].Visible = false;

            dataGridView1.Columns["GHICHU"].Width = 220;
            dataGridView1.Columns["LYDO"].Width = 220;
            dataGridView1.Columns["NGAYNOPDON"].Width = 160;
            dataGridView1.Columns["NGAYNGHI"].Width = 160;
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
            txtLyDo.Enabled = !kt;
            txtGhiChu.Enabled = !kt;
            txtSoQD.Enabled = !kt;
            cboNhanVien.Enabled = !kt;
            dtNgayNghi.Enabled = !kt;
            dtNgayNopDon.Enabled = !kt;
        }
        private void _reset()
        {
            txtSoQD.Text = string.Empty;
            txtLyDo.Text = string.Empty;
            txtGhiChu.Text = string.Empty;
            dtNgayNopDon.Value = DateTime.Now;
            dtNgayNghi.Value = dtNgayNopDon.Value.AddDays(45);
        }
        void loadNhanVien()
        {
            cboNhanVien.DataSource = _nv.getList();
            cboNhanVien.DisplayMember = "HOTEN";
            cboNhanVien.ValueMember = "MANV";
        }

        void loadData()
        {
            dataGridView1.DataSource = _nvtv.getListFull();

        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            _showHide(false);
            _them = true;
            _reset();
            panel2.Visible = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            _showHide(false);
            _them = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xoá hay không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                _nvtv.Delete(_soQD, 1);
                loadData();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            SaveData();
            loadData();
            _them = false;
            _showHide(true);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            _them = false;
            _showHide(true);
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
            tb_NHANVIEN_THOIVIEC tv;
            if (_them)
            {
                var maxSoQD = _nvtv.MaxSoQuyetDinh();
                int so = int.Parse(maxSoQD.Substring(0, 5)) + 1;

                tv = new tb_NHANVIEN_THOIVIEC();
                tv.SOQD = so.ToString("00000") + @"/" + DateTime.Now.Year.ToString() + @"QDTV";
                tv.LYDO = txtLyDo.Text;
                tv.NGAYNOPDON = dtNgayNopDon.Value;
                tv.NGAYNGHI = dtNgayNghi.Value;
                tv.GHICHU = txtGhiChu.Text;
                tv.MANV = int.Parse(cboNhanVien.SelectedValue.ToString());
                tv.CREATED_BY = 1;
                tv.CREATED_DATE = DateTime.Now;
                _nvtv.Add(tv);
            }
            else
            {
                tv = _nvtv.getItem(_soQD);
                tv.LYDO = txtLyDo.Text;
                tv.NGAYNGHI = dtNgayNghi.Value;
                tv.NGAYNOPDON = dtNgayNopDon.Value;
                tv.GHICHU = txtGhiChu.Text;
                tv.MANV = int.Parse(cboNhanVien.SelectedValue.ToString());
                tv.UPDATED_BY = 1;
                tv.UPDATED_DATE = DateTime.Now;
                _nvtv.Update(tv);
            }
            var nv = _nv.getItem(tv.MANV.Value);
            nv.DATHOIVIEC = true;
            _nv.Update(nv);
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                _soQD = row.Cells["SOQD"].Value.ToString();
                var tv = _nvtv.getItem(_soQD);
                txtSoQD.Text = _soQD;
                txtGhiChu.Text = tv.GHICHU;
                cboNhanVien.SelectedValue = tv.MANV;
                txtLyDo.Text = tv.LYDO;
                dtNgayNghi.Value = tv.NGAYNGHI.Value;
                dtNgayNopDon.Value = tv.NGAYNOPDON.Value;
            }
        }

        private void dtNgayNopDon_ValueChanged(object sender, EventArgs e)
        {
            dtNgayNghi.Value = dtNgayNopDon.Value.AddDays(45);
        }
    }
}

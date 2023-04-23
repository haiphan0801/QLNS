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
    public partial class DieuChuyen : Form
    {
        public DieuChuyen()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            InitializeComponent();
        }
        bool _them;
        string _soQD;
        NHANVIEN_DIEUCHUYEN _nvdc;
        NHANVIEN _nv;
        PHONGBAN _pb;
        private void DieuChuyen_Load(object sender, EventArgs e)
        {
            _them = false;
            _nvdc = new NHANVIEN_DIEUCHUYEN();
            _nv = new NHANVIEN();
            _pb = new PHONGBAN();
            _showHide(true);
            loadNhanVien();
            loadDonViDen();
            loadData();
            dataGridView1.CellClick += dataGridView1_CellClick;
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                if (col.Name.StartsWith("tb_"))
                {
                    col.Visible = false;
                }
            }

            dataGridView1.Columns["MAPB"].Visible = false;
            dataGridView1.Columns["MAPB2"].Visible = false;
            dataGridView1.Columns["CREATED_BY"].Visible = false;
            dataGridView1.Columns["CREATED_DATE"].Visible = false;
            dataGridView1.Columns["UPDATED_BY"].Visible = false;
            dataGridView1.Columns["UPDATED_DATE"].Visible = false;
            dataGridView1.Columns["DELETED_BY"].Visible = false;
            dataGridView1.Columns["DELETED_DATE"].Visible = false;

            dataGridView1.Columns["GHICHU"].Width = 250;
            dataGridView1.Columns["LYDO"].Width = 250;
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
            cboDonViDen.Enabled = !kt;
        }
        private void _reset()
        {
            txtSoQD.Text = string.Empty;
            txtLyDo.Text = string.Empty;
            txtGhiChu.Text = string.Empty;
        }
        void loadNhanVien()
        {
            cboNhanVien.DataSource = _nv.getList();
            cboNhanVien.DisplayMember = "HOTEN";
            cboNhanVien.ValueMember = "MANV";
        }
        void loadDonViDen()
        {
            cboDonViDen.DataSource = _pb.getList();
            cboDonViDen.DisplayMember = "TENPB";
            cboDonViDen.ValueMember = "IDPB";
        }
        void loadData()
        {
            dataGridView1.DataSource = _nvdc.getListFull();

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
                _nvdc.Delete(_soQD, 1);
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
            tb_NHANVIEN_DIEUCHUYEN dc;
            if (_them)
            {
                var maxSoQD = _nvdc.MaxSoQuyetDinh();
                int so = int.Parse(maxSoQD.Substring(0, 5)) + 1;

                dc = new tb_NHANVIEN_DIEUCHUYEN();
                dc.SOQD = so.ToString("00000") + @"/" + DateTime.Now.Year.ToString() + @"QDDC";
                dc.LYDO = txtLyDo.Text;
                dc.NGAY = dtNgay.Value;
                dc.GHICHU = txtGhiChu.Text;
                dc.MANV = int.Parse(cboNhanVien.SelectedValue.ToString());
                dc.MAPB = _nv.getItem(int.Parse(cboNhanVien.SelectedValue.ToString())).IDPB;
                dc.MAPB2 = int.Parse(cboDonViDen.SelectedValue.ToString());
                dc.CREATED_BY = 1;
                dc.CREATED_DATE = DateTime.Now;
                _nvdc.Add(dc);
            }
            else
            {
                dc = _nvdc.getItem(_soQD);
                dc.LYDO = txtLyDo.Text;
                dc.NGAY = dtNgay.Value;
                dc.GHICHU = txtGhiChu.Text;
                dc.MANV = int.Parse(cboNhanVien.SelectedValue.ToString());
                dc.MAPB2 = int.Parse(cboDonViDen.SelectedValue.ToString());
                dc.UPDATED_BY = 1;
                dc.UPDATED_DATE = DateTime.Now;
                _nvdc.Update(dc);
            }
            var nv = _nv.getItem(dc.MANV.Value);
            nv.IDPB = dc.MAPB2;
            _nv.Update(nv);
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                _soQD = row.Cells["SOQD"].Value.ToString();
                var dc = _nvdc.getItem(_soQD);
                txtSoQD.Text = _soQD;
                txtGhiChu.Text = dc.GHICHU;
                cboNhanVien.SelectedValue = dc.MANV;
                cboDonViDen.SelectedValue = dc.MAPB2;
                txtLyDo.Text = dc.LYDO;
                dtNgay.Value = dc.NGAY.Value;
            }
        }
    }
}

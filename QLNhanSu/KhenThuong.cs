using BusinessLayer;
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
using System.Security.Cryptography;

namespace QLNhanSu
{
    public partial class KhenThuong : Form
    {
        public KhenThuong()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            InitializeComponent();
        }
        bool _them;
        string _soQD;
        KHENTHUONG_KYLUAT _ktkl;
        NHANVIEN _nv;
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void KhenThuong_Load(object sender, EventArgs e)
        {
            _them = false;
            _ktkl = new KHENTHUONG_KYLUAT();
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
            dataGridView1.Columns["LOAI"].Visible = false;
            dataGridView1.Columns["TUNGAY"].Visible = false;
            dataGridView1.Columns["DENNGAY"].Visible = false;

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
            txtNoiDung.Enabled = !kt;
            txtSoQD.Enabled = !kt;
            cboNhanVien.Enabled = !kt;
        }
        private void _reset()
        {
            txtSoQD.Text = string.Empty;
            txtLyDo.Text = string.Empty;
            txtNoiDung.Text = string.Empty;
        }
        void loadNhanVien()
        {
            cboNhanVien.DataSource = _nv.getList();
            cboNhanVien.DisplayMember = "HOTEN";
            cboNhanVien.ValueMember = "MANV";
        }
        void loadData()
        {
            dataGridView1.DataSource = _ktkl.getListFull(1);

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
                _ktkl.Delete(_soQD, 1);
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
            if (_them)
            {
                var maxSoQD = _ktkl.MaxSoQuyetDinh(1);
                int so = int.Parse(maxSoQD.Substring(0, 5)) + 1;

                tb_KHENTHUONG_KYLUAT kt = new tb_KHENTHUONG_KYLUAT();
                kt.SOQUYETDINH = so.ToString("00000") + @"/2023/QDKT";
                kt.LYDO = txtLyDo.Text;
                kt.NGAY = dtNgay.Value;
                kt.NOIDUNG = txtNoiDung.Text;
                kt.MANV = int.Parse(cboNhanVien.SelectedValue.ToString());
                kt.LOAI = 1;
                kt.CREATED_BY = 1;
                kt.CREATED_DATE = DateTime.Now;
                _ktkl.Add(kt);
            }
            else
            {
                var kt = _ktkl.getItem(_soQD);
                kt.LYDO = txtLyDo.Text;
                kt.NGAY = dtNgay.Value;
                kt.NOIDUNG = txtNoiDung.Text;
                kt.MANV = int.Parse(cboNhanVien.SelectedValue.ToString());
                kt.CREATED_BY = 1;
                kt.CREATED_DATE = DateTime.Now;
                _ktkl.Update(kt);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                _soQD = row.Cells["SOQUYETDINH"].Value.ToString();
                var kt = _ktkl.getItem(_soQD);
                txtSoQD.Text = _soQD;
                txtNoiDung.Text = kt.NOIDUNG;
                cboNhanVien.SelectedValue = kt.MANV;
                txtLyDo.Text = kt.LYDO;
                dtNgay.Value = kt.NGAY.Value;
            }
        }

        
    }
}

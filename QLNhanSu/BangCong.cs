using BusinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace QLNhanSu
{
    public partial class BangCong : Form
    {
        
        public BangCong()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            
        }

        KYCONG _kycong;
        bool _them;
        int _makycong;
        private BangCongChiTiet _formChiTiet;
        private void BangCong_Load(object sender, EventArgs e)
        {
            _formChiTiet = new BangCongChiTiet();
            _formChiTiet.TopLevel = false;
            _formChiTiet.Dock = DockStyle.Fill;
            this.Parent.Controls.Add(_formChiTiet);
            _them = false;
            _kycong = new KYCONG();
            _showHide(true);
            loadData();
            dataGridView1.CellClick += dataGridView1_CellContentClick;
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                if (col.Name.StartsWith("tb_"))
                {
                    col.Visible = false;
                }
            }
            dataGridView1.Columns["MACTY"].Visible = false;
            dataGridView1.Columns["CREATED_BY"].Visible = false;
            dataGridView1.Columns["CREATED_DATE"].Visible = false;
            dataGridView1.Columns["UPDATED_BY"].Visible = false;
            dataGridView1.Columns["UPDATED_DATE"].Visible = false;
            dataGridView1.Columns["DELETED_BY"].Visible = false;
            dataGridView1.Columns["DELETED_DATE"].Visible = false;
            dataGridView1.Columns["NGAYCONGTRONGTHANG"].Width = 180;
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

        }
        void loadData()
        {
            dataGridView1.DataSource = _kycong.getList();

        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            _showHide(false);
            _them = true;
            cboNam.Text = DateTime.Now.Year.ToString();
            cboThang.Text = DateTime.Now.Month.ToString();
            checkBoxKhoa.Checked = false;
            checkBoxTrangthai.Checked = false;
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
                _kycong.Delete(_makycong, 1);
                loadData();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            SaveData(GetCboThang());
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

        public ComboBox GetCboThang()
        {
            return cboThang;
        }

        void SaveData(ComboBox cboThang)
        {
            if (_them)
            {
                tb_KYCONG kc = new tb_KYCONG();
                kc.MAKYCONG = int.Parse(cboNam.Text) * 100 + int.Parse(cboThang.Text);
                kc.NAM = int.Parse(cboNam.Text);
                kc.THANG = int.Parse(cboThang.Text);
                kc.KHOA = checkBoxKhoa.Checked;
                kc.TRANGTHAI = checkBoxTrangthai.Checked;
                kc.MACTY = 1;
                kc.NGAYCONGTRONGTHANG = Functions.DemSoNgayLamViecTrongThang(int.Parse(cboThang.Text), int.Parse(cboNam.Text));
                kc.NGAYTINHCONG = DateTime.Now;
                kc.CREATED_BY = 1;
                kc.CREATED_DATE = DateTime.Now;
                _kycong.Add(kc);
            }
            else
            {
                var kc = _kycong.getItem(_makycong);
                //kc.MAKYCONG = int.Parse(cboNam.Text) * 100 + int.Parse(cboThang.Text);
                kc.NAM = int.Parse(cboNam.Text);
                kc.THANG = int.Parse(cboThang.Text);
                kc.KHOA = checkBoxKhoa.Checked;
                kc.TRANGTHAI = checkBoxTrangthai.Checked;
                kc.NGAYCONGTRONGTHANG = Functions.DemSoNgayLamViecTrongThang(int.Parse(cboThang.Text), int.Parse(cboNam.Text));
                kc.NGAYTINHCONG = DateTime.Now;
                kc.UPDATED_BY = 1;
                kc.UPDATED_DATE = DateTime.Now;
                _kycong.Update(kc);
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                _makycong = Convert.ToInt32(row.Cells["MAKYCONG"].Value.ToString());
                cboNam.Text = row.Cells["NAM"].Value.ToString();
                cboThang.Text = row.Cells["THANG"].Value.ToString();
                checkBoxKhoa.Checked = Convert.ToBoolean(row.Cells["KHOA"].Value);
                checkBoxTrangthai.Checked = Convert.ToBoolean(row.Cells["TRANGTHAI"].Value);
            }
        }

        private void btnXemBangCong_Click(object sender, EventArgs e)
        {
            BangCongChiTiet form = new BangCongChiTiet();
            form._makycong = _makycong;
            form._thang = int.Parse(cboThang.Text);
            form._nam = int.Parse(cboNam.Text);
            form._macty = 1;
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            this.Parent.Controls.Add(form);
            form.Show();
            this.Hide();
        }
    }
}

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
    public partial class LoaiCong : Form
    {
        public LoaiCong()
        {
            InitializeComponent();
        }

        LOAICONG _loaicong;
        bool _them;
        int _id;
        private void LoaiCong_Load(object sender, EventArgs e)
        {
            _them = false;
            _loaicong = new LOAICONG();
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
            txtTen.Enabled = !kt;
            nudHeSo.Enabled = !kt;
        }
        void loadData()
        {
            dataGridView1.DataSource = _loaicong.getList();

        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            _showHide(false);
            _them = true;
            txtTen.Text = string.Empty;
            nudHeSo.Value = 1;
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
                _loaicong.Delete(_id, 1);
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
                tb_LOAICONG lc = new tb_LOAICONG();
                lc.TENLC = txtTen.Text;
                lc.HESO = double.Parse(nudHeSo.Value.ToString());
                lc.CREATED_BY = 1;
                lc.CREATED_DATE = DateTime.Now;
                _loaicong.Add(lc);
            }
            else
            {
                var lc = _loaicong.getItem(_id);
                lc.TENLC = txtTen.Text;
                lc.HESO = double.Parse(nudHeSo.Value.ToString());
                lc.UPDATED_BY = 1;
                lc.UPDATED_DATE = DateTime.Now;
                _loaicong.Update(lc);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                _id = Convert.ToInt32(row.Cells["IDLC"].Value.ToString());
                txtTen.Text = row.Cells["TENLC"].Value.ToString();
                nudHeSo.Text = row.Cells["HESO"].Value.ToString();
            }
        }
    }
}

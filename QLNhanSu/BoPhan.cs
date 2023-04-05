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
    public partial class BoPhan : Form
    {
        public BoPhan()
        {
            InitializeComponent();
        }

        BOPHAN _bophan;
        bool _them;
        int _id;
        private void BoPhan_Load(object sender, EventArgs e)
        {
            _them = false;
            _bophan = new BOPHAN();
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
            dataGridView1.Columns["IDBP"].Width = 150;
            dataGridView1.Columns["TENBP"].Width = 500;
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
        }
        void loadData()
        {
            dataGridView1.DataSource = _bophan.getList();
            

        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            _showHide(false);
            _them = true;
            txtTen.Text = string.Empty;
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
                _bophan.Delete(_id);
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

        void SaveData()
        {
            if (_them)
            {
                tb_BOPHAN dt = new tb_BOPHAN();
                dt.TENBP = txtTen.Text;
                _bophan.Add(dt);
            }
            else
            {
                var dt = _bophan.getItem(_id);
                dt.TENBP = txtTen.Text;
                _bophan.Update(dt);
            }
        }
        private void btnIn_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                _id = Convert.ToInt32(row.Cells["IDBP"].Value.ToString());
                txtTen.Text = row.Cells["TENBP"].Value.ToString();
            }
        }
    }
}

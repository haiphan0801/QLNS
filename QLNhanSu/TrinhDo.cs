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
namespace QLNhanSu
{
    public partial class TrinhDo : Form
    {
        public TrinhDo()
        {
            InitializeComponent();
        }
        TRINHDO _trinhdo;
        bool _them;
        int _id;
        private void TrinhDo_Load(object sender, EventArgs e)
        {
            _them = false;
            _trinhdo = new TRINHDO();
            _showHide(true);
            loadData();
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.Columns["IDTD"].Width = 150;
            dataGridView1.Columns["TENTD"].Width = 500;
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
            dataGridView1.DataSource = _trinhdo.getList();

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
                _trinhdo.Delete(_id);
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

        }

        private void btnIn_Click(object sender, EventArgs e)
        {

        }
        void SaveData()
        {
            if (_them)
            {
                tb_TRINHDO tg = new tb_TRINHDO();
                tg.TENTD = txtTen.Text;
                _trinhdo.Add(tg);
            }
            else
            {
                var tg = _trinhdo.getItem(_id);
                tg.TENTD = txtTen.Text;
                _trinhdo.Update(tg);
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                _id = Convert.ToInt32(row.Cells["IDTD"].Value.ToString());
                txtTen.Text = row.Cells["TENTD"].Value.ToString();
            }
        }

       
    }
}

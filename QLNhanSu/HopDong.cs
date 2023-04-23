using BusinessLayer;
using BusinessLayer.ClassXuLy;
using DataLayer;
using Microsoft.Office.Interop.Excel;
using Application = Microsoft.Office.Interop.Word;
using Microsoft.Vbe.Interop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Microsoft.Office.Interop.Word;

namespace QLNhanSu
{
    public partial class HopDong : Form
    {
        QuanlynhansuEntities _db = new QuanlynhansuEntities();
        public HopDong()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            InitializeComponent();
        }

        HOPDONG _hd;
        NHANVIEN _nv;
        bool _them;
        string _soHD;
        string _maSoHD;
        private void HopDong_Load(object sender, EventArgs e)
        {
            _hd = new HOPDONG();
            _nv = new NHANVIEN();
            _them = false;
            _showHide(true);
            loadData();
            loadNhanVien();
            dataGridView1.CellClick += dataGridView1_CellClick;
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                if (col.Name.StartsWith("tb_"))
                {
                    col.Visible = false;
                }
            }
            dataGridView1.Columns["NOIDUNG"].Visible = false;
            dataGridView1.Columns["CREATED_BY"].Visible = false;
            dataGridView1.Columns["CREATED_DATE"].Visible = false;
            dataGridView1.Columns["UPDATED_BY"].Visible = false;
            dataGridView1.Columns["UPDATED_DATE"].Visible = false;
            dataGridView1.Columns["DELETED_BY"].Visible = false;
            dataGridView1.Columns["DELETED_DATE"].Visible = false;
            panel2.Visible = false;
        }

        void _showHide(bool kt)
        {
            btnLuu.Enabled = !kt;
            btnHuy.Enabled = !kt;
            btnThem.Enabled = kt;
            btnSua.Enabled = kt;
            btnXoa.Enabled = kt;
            btnIn.Enabled = !kt;
            btnDong.Enabled = kt;
            dataGridView1.Enabled = kt;
            txtSoHD.Enabled = kt;
            dtNgayBatDau.Enabled = !kt;
            dtNgayKetThuc.Enabled = !kt;
            dtNgayKy.Enabled = !kt;
            nudHeSoLuong.Enabled = !kt;
            nudLanKy.Enabled = !kt;
            cboNhanVien.Enabled = !kt;
        }
        void _reset()
        {
            txtSoHD.Text = string.Empty;
            dtNgayBatDau.Value = DateTime.Now;
            dtNgayKetThuc.Value = dtNgayBatDau.Value.AddMonths(6);
            dtNgayKy.Value = DateTime.Now;
            nudLanKy.Text = "1";
            nudHeSoLuong.Text = "1";
            richTextBoxNoiDung.Text = string.Empty;
        }
        void loadData()
        {
            dataGridView1.DataSource = _hd.getListFull();
        }

        void loadNhanVien()
        {
            cboNhanVien.DataSource = _nv.getList();
            cboNhanVien.DisplayMember = "CCCD";
            cboNhanVien.DisplayMember = "HOTEN";
            cboNhanVien.ValueMember = "MANV";
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
            panel2.Visible = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xoá hay không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                _hd.Delete(_soHD, 1);
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
            string sohd = dataGridView1.CurrentRow.Cells["SOHD"].Value.ToString();
            tb_HOPDONG hopDong = _db.tb_HOPDONG.Single(p => p.SOHD == sohd);
           
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("dd", DateTime.Parse(hopDong.NGAYKY.ToString()).Day.ToString());
            dic.Add("MM", DateTime.Parse(hopDong.NGAYKY.ToString()).Month.ToString());
            dic.Add("yyyy", DateTime.Parse(hopDong.NGAYKY.ToString()).Year.ToString());
            dic.Add("SOHD", hopDong.SOHD);
            dic.Add("HOTEN", hopDong.tb_NHANVIEN.HOTEN);
            dic.Add("CCCD", hopDong.tb_NHANVIEN.CCCD);
            dic.Add("DIACHI", hopDong.tb_NHANVIEN.DIACHI);
            dic.Add("NGAYBATDAU", DateTime.Parse(hopDong.NGAYBATDAU.ToString()).ToString());
            dic.Add("NGAYKETTHUC", DateTime.Parse(hopDong.NGAYKETTHUC.ToString()).ToString());
        
            string fileName = @"C:\Users\Admin\Desktop\công nghệ phần mềm\hopdong.docx";

            WordUltil wd = new WordUltil(fileName, true);
            wd.WriteFields(dic);
        }

        void SaveData()
        {
            if (_them)
            {
                var maxSoHD = _hd.MaSoHopDong();
                int so = int.Parse(maxSoHD.Substring(0, 5)) + 1;

                tb_HOPDONG hd = new tb_HOPDONG();
                hd.SOHD = so.ToString("00000") + @"/2023/HĐLĐ";
                hd.NGAYBATDAU = dtNgayBatDau.Value;
                hd.NGAYKETTHUC = dtNgayKetThuc.Value;
                hd.NGAYKY = dtNgayKy.Value;
                hd.THOIHAN = cboThoiHan.Text;
                hd.HESOLUONG = double.Parse(nudHeSoLuong.Value.ToString());
                hd.LANKY = int.Parse(nudLanKy.Value.ToString());
                hd.MANV = int.Parse(cboNhanVien.SelectedValue.ToString());
                hd.NOIDUNG = richTextBoxNoiDung.Rtf;
                hd.MACTY = 1;
                hd.CREATED_BY = 1;
                hd.CREATED_DATE = DateTime.Now;
                _hd.Add(hd);
            }
            else
            {
                var hd = _hd.getItem(_soHD);
                hd.NGAYBATDAU = dtNgayBatDau.Value;
                hd.NGAYKETTHUC = dtNgayKetThuc.Value;
                hd.NGAYKY = dtNgayKy.Value;
                hd.THOIHAN = cboThoiHan.Text;
                hd.HESOLUONG = double.Parse(nudHeSoLuong.Value.ToString());
                hd.LANKY = int.Parse(nudLanKy.Value.ToString());
                hd.MANV = int.Parse(cboNhanVien.SelectedValue.ToString());
                hd.NOIDUNG = richTextBoxNoiDung.Rtf;
                hd.MACTY = 1;
                hd.CREATED_BY = 1;
                hd.CREATED_DATE = DateTime.Now;
                _hd.Update(hd);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                _soHD = row.Cells["SOHD"].Value.ToString();
                var hd = _hd.getItem(_soHD);
                txtSoHD.Text = _soHD;
                dtNgayBatDau.Value = hd.NGAYBATDAU.Value;
                dtNgayKetThuc.Value = hd.NGAYKETTHUC.Value;
                dtNgayKy.Value = hd.NGAYKY.Value;
                cboThoiHan.Text = hd.THOIHAN;
                nudHeSoLuong.Text = hd.HESOLUONG.ToString();
                nudLanKy.Text = hd.LANKY.ToString();
                cboNhanVien.Text = hd.MANV.ToString();
                richTextBoxNoiDung.Rtf = hd.NOIDUNG;
            }
        }
    }
}

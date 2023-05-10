using BusinessLayer;
using DataLayer;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace QLNhanSu
{
    public partial class BangCongChiTiet : Form
    {
        public BangCongChiTiet()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
        }
        NHANVIEN _nhanvien;
        KYCONG _kc;
        KYCONGCHITIET _kcct;
        BANG_CONG_NHAN_VIEN_CHI_TIET _bangcongCT;
        public int _makycong;
        public int _macty;
        public int _thang;
        public int _nam;

        private void BangCongChiTiet_Load(object sender, EventArgs e)
        {
            _kc = new KYCONG();
            _kcct = new KYCONGCHITIET();
            _nhanvien = new NHANVIEN();
            _bangcongCT = new BANG_CONG_NHAN_VIEN_CHI_TIET();
            dataGridView1.DataSource = _kcct.getList(_makycong);
            CustomView();
            cboThang.Text = _thang.ToString();
            cboNam.Text = _nam.ToString();
        }
        public void loadBangCong()
        {
            _kcct = new KYCONGCHITIET();
            dataGridView1.DataSource = _kcct.getList(int.Parse(cboNam.Text) * 100 + int.Parse(cboThang.Text));
            CustomView();
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
        }
        private void btnPhatSinhKyCong_Click(object sender, EventArgs e)
        {
            Waiting myForm = new Waiting(); 
            myForm.ShowDialog();
            if (_kc.KiemTraPhatSinhKyCong(int.Parse(cboNam.Text) * 100 + int.Parse(cboThang.Text)))
            {
                MessageBox.Show("Kỳ công đã được phát sinh.", "Thông báo");
                myForm.Close();
                return;
            }
            List<tb_NHANVIEN> lstNhanVien = _nhanvien.getList();
            _kcct.phatSinhKyCongChiTiet(_macty,int.Parse(cboThang.Text), int.Parse(cboNam.Text));
            foreach(var item in lstNhanVien)
            {
                for (int i = 1; i <= GetDayNumber(int.Parse(cboThang.Text), int.Parse(cboNam.Text)); i++)
                {
                    tb_BANG_CONG_NHAN_VIEN_CHI_TIET bcct = new tb_BANG_CONG_NHAN_VIEN_CHI_TIET();
                    bcct.MANV = item.MANV;
                    bcct.MACTY = item.MACTY;
                    bcct.HOTEN = item.HOTEN;
                    bcct.GIOVAO = "00:00";
                    bcct.GIORA = "17:00";
                    bcct.NGAY = DateTime.Parse(cboNam.Text + " - " + cboThang.Text + " - " + i.ToString());
                    bcct.THU = Functions.layThuTrongTuan(int.Parse(cboNam.Text), int.Parse(cboThang.Text),  i);
                    bcct.NGAYPHEP = 0;
                    bcct.CONGNGAYLE = 0;
                    bcct.CONGCHUNHAT = 0;
                    if (bcct.THU == "Chủ nhật")
                    {
                        bcct.KYHIEU = "CN";
                        bcct.NGAYCONG = 0;
                    }
                    else
                    {
                        bcct.KYHIEU = "X";
                        bcct.NGAYCONG = 1;
                    }
                    bcct.MAKYCONG = _makycong;
                    bcct.CREATED_DATE = DateTime.Now;
                    bcct.CREATED_BY = 1;
                    _bangcongCT.Add(bcct);
                }
            }
            
            var kc = _kc.getItem(int.Parse(cboNam.Text) * 100 + int.Parse(cboThang.Text));
            kc.TRANGTHAI = true;
            _kc.Update(kc);
            myForm.Close();
            loadBangCong();
        }

        private void btnXemBangCong_Click(object sender, EventArgs e)
        {
            loadBangCong();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            loadBangCong();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void CustomView()
        {
            int i;
            // Thiết lập các thuộc tính cho các cột trong DataGridView
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                if (column.Name == "HOTEN") continue;

                DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = column.Name;
                textColumn.Width = 30;
                textColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                textColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                textColumn.DefaultCellStyle.BackColor = Color.White;
                textColumn.DefaultCellStyle.ForeColor = Color.Black;
                column.CellTemplate = textColumn.CellTemplate;
            }

            // Thiết lập các thuộc tính cho các cột ngày trong DataGridView
            for (i = 1; i <= GetDayNumber(_thang, _nam); i++)
            {
                DateTime newDate = new DateTime(_nam, _thang, i);
                string columnName = "D" + i;

                DataGridViewColumn column = dataGridView1.Columns[columnName];
                column.HeaderText = newDate.DayOfWeek.ToString().Substring(0, 2).ToUpper() + Environment.NewLine + i;
                column.Width = 30;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.DefaultCellStyle.BackColor = GetCellColor(newDate.DayOfWeek);
                column.DefaultCellStyle.ForeColor = Color.Black;
            }

            // Thiết lập lại tiêu đề cho cột HOTEN
            DataGridViewColumn hoTenColumn = dataGridView1.Columns["HOTEN"];
            hoTenColumn.HeaderText = "HỌ TÊN";
            hoTenColumn.Width = 100;
            hoTenColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            hoTenColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            hoTenColumn.DefaultCellStyle.BackColor = Color.White;
            hoTenColumn.DefaultCellStyle.ForeColor = Color.Black;
            hoTenColumn.ReadOnly = true;

            // Ẩn các cột thừa
            for (i = GetDayNumber(_thang, _nam) + 2; i <= 32; i++)
            {
                dataGridView1.Columns[i].Visible = false;
            }
            while (i <= 31)
            {
                dataGridView1.Columns[i + 1].Visible = false;
                i++;
            }
        }

        // Trả về màu nền cho ô DataGridViewCell tương ứng với ngày trong tuần
        private Color GetCellColor(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday:
                case DayOfWeek.Tuesday:
                case DayOfWeek.Wednesday:
                case DayOfWeek.Thursday:
                case DayOfWeek.Friday:
                    return Color.White;
                case DayOfWeek.Saturday:
                    return Color.Khaki;
                case DayOfWeek.Sunday:
                    return Color.Orange;
                default:
                    return Color.White;
            }
        }
        private int GetDayNumber(int thang, int nam)
        {
            int dayNumber = 0;
            switch (thang)
            {
                case 2:
                    dayNumber = (nam % 4 == 0 && nam % 100 != 0) || nam % 400 == 0 ? 29 : 28;
                    break;
                case 4:
                case 6:
                case 9:
                case 11:
                    dayNumber = 30;
                    break;
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    dayNumber = 31;
                    break;
            }
            return dayNumber;
        }

        private void mnCapNhatNgayCong_Click(object sender, EventArgs e)
        {
            CapnhatNgayCong frm = new CapnhatNgayCong();
            frm._makycong = _makycong;
            frm._manv = int.Parse(dataGridView1.CurrentRow.Cells["MANV"].Value.ToString());
            frm._hoten = dataGridView1.CurrentRow.Cells["HOTEN"].Value.ToString();
            //frm._ngay = gvBangCongChiTiet.FocusColum.FieldName.ToString();
            frm._ngay = dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].Name;
            frm.ShowDialog();
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.Value == null)
            {

            }
            else
            {
                if (e.Value.ToString() == "VR")
                {
                    e.CellStyle.BackColor = Color.DarkGreen;
                    e.CellStyle.ForeColor = Color.White;
                }
                if (e.Value.ToString() == "P")
                {
                    e.CellStyle.BackColor = Color.LightBlue;
                }
                if (e.Value.ToString() == "V")
                {
                    e.CellStyle.BackColor = Color.IndianRed;
                    e.CellStyle.ForeColor = Color.White;
                }
            }
        }
    }
}

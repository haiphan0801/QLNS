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
    public partial class CapnhatNgayCong : Form
    {
        public CapnhatNgayCong()
        {
            InitializeComponent();
        }
        public int _manv;
        public string _hoten;
        public int _makycong;
        public string _ngay;
        public int _cNgay;
        KYCONGCHITIET _kcct;
        BANG_CONG_NHAN_VIEN_CHI_TIET _bcct_nv;
        BangCongChiTiet frmBCCT = (BangCongChiTiet) Application.OpenForms["BangCongChiTiet"];
        private void CapnhatNgayCong_Load(object sender, EventArgs e)
        {
            _kcct = new KYCONGCHITIET();
            _bcct_nv = new BANG_CONG_NHAN_VIEN_CHI_TIET();
            lblID.Text = _manv.ToString();
            lblHoTen.Text = _hoten;
            string nam = _makycong.ToString().Substring(0, 4);
            string thang = _makycong.ToString().Substring(4);
            string ngay = _ngay.Substring(1);
            DateTime _d = DateTime.Parse(nam + " - " + thang + " - " + ngay);
            cldNgayCong.SetDate(_d);
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
          
            string _valueChamCong = rdgChamCong.Controls.OfType<RadioButton>().FirstOrDefault(rb => rb.Checked)?.Tag.ToString();
            string _valueNgayNghi = rdgNgayNghi.Controls.OfType<RadioButton>().FirstOrDefault(rb => rb.Checked)?.Tag.ToString();
            string fieldName = "D" + _cNgay.ToString();
            var kcct = _kcct.getItem(_makycong, _manv);

            //double? tongngaycong = kcct.TONGNGAYCONG;
            //double? tongngayphep = kcct.NGAYPHEP;
            //double? tongngaykhongphep = kcct.NGAYKHONGPHEP;
            //double? tongngayle = kcct.CONGNGAYLE;
            if (cldNgayCong.SelectionRange.Start.Year * 100 + cldNgayCong.SelectionRange.Start.Month!=+_makycong)
            {
                MessageBox.Show("Thực hiện chấm công không đúng kỳ công. Vui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //Cập nhật KYCONGCHITIET=>cập nhật BANGCONGNHANVIENCHITIET
            Functions.execQuery("UPDATE tb_KYCONGCHITIET SET " + fieldName + "='" + _valueChamCong + "'WHERE MAKYCONG=" + _makycong + "AND MANV=" + _manv);

            tb_BANG_CONG_NHAN_VIEN_CHI_TIET bcctnv = _bcct_nv.getItem(_makycong, _manv, cldNgayCong.SelectionStart.Day);
            bcctnv.KYHIEU = _valueChamCong;
            
            switch (_valueChamCong)
            {
                case "P":
                    if (_valueNgayNghi == "NN")
                    {
                        bcctnv.NGAYPHEP = 1;
                        bcctnv.NGAYCONG = 0;
                    }
                    else
                    {
                        bcctnv.NGAYPHEP = 0.5;
                        bcctnv.NGAYCONG = 0.5;
                    }
                    break;
                case "CT":
                    if (_valueNgayNghi == "NN")
                    {
                        bcctnv.NGAYCONG = 1;
                    }
                    else
                    {
                        bcctnv.NGAYCONG = 0.5;
                        bcctnv.NGAYPHEP = 0.5;
                    }
                    break;
                case "V":
                    if (_valueNgayNghi == "NN")
                    {
                        bcctnv.NGAYPHEP = 1;
                        bcctnv.NGAYCONG = 0;
                    }
                    else
                    {
                        bcctnv.NGAYPHEP = 0.5;
                        bcctnv.NGAYCONG = 0.5;
                    }
                    break;
                case "VR":
                    if (_valueNgayNghi == "NN")
                    {
                        bcctnv.NGAYPHEP = 1;
                        bcctnv.NGAYCONG = 0;
                    }
                    else
                    {
                        bcctnv.NGAYPHEP = 0.5;
                        bcctnv.NGAYCONG = 0.5;
                    }
                    break;
                default:
                    break;
            }
            _bcct_nv.Update(bcctnv);

            //Tính lại tổng các ngày: phép, công, vắng....
            double tongngaycong = _bcct_nv.tongNgayCong(_makycong, _manv);
            double tongngayphep = _bcct_nv.tongNgayPhep(_makycong, _manv);
            kcct.NGAYPHEP = tongngayphep;
            kcct.TONGNGAYCONG = tongngaycong;
            _kcct.Update(kcct);
            frmBCCT.loadBangCong();
            //MessageBox.Show(_valueChamCong + " -- " + _valueNgayNghi);
        }

        
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cldNgayCong_DateSelected(object sender, DateRangeEventArgs e)
        {
            _cNgay = cldNgayCong.SelectionRange.Start.Day;
        }
    }
}

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
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;


namespace QLNhanSu
{
    public partial class NhanVien : Form
    {
        public NhanVien()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            InitializeComponent();
        }
        NHANVIEN _nhanvien;
        PHONGBAN _phongban;
        BOPHAN _bophan;
        CHUCVU _chucvu;
        TONGIAO _tongiao;
        DANTOC _dantoc;
        TRINHDO _trinhdo;

        bool _them;
        int _id;
        private void NhanVien_Load(object sender, EventArgs e)
        {
            _them = false;
            _nhanvien = new NHANVIEN();
            _phongban = new PHONGBAN();
            _bophan = new BOPHAN();
            _chucvu = new CHUCVU();
            _tongiao = new TONGIAO();
            _dantoc = new DANTOC();
            _trinhdo = new TRINHDO();
            _showHide(true);
            loadData();
            loadCombo();
            dataGridView1.CellClick += dataGridView1_CellClick;
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                if (col.Name.StartsWith("tb_"))
                {
                    col.Visible = false;
                }
                if (col.Name.StartsWith("ID"))
                {
                    col.Visible = false;
                }
                dataGridView1.Columns["MACTY"].Visible =false;

            }
            panel2.Visible = false;
        }
        void loadCombo()
        {
            
            cboPhongBan.DataSource = _phongban.getList();
            cboPhongBan.DisplayMember = "TENPB";
            cboPhongBan.ValueMember = "IDPB";

            cboBoPhan.DataSource = _bophan.getList();
            cboBoPhan.DisplayMember = "TENBP";
            cboBoPhan.ValueMember = "IDBP";

            cboChucVu.DataSource = _chucvu.getList();
            cboChucVu.DisplayMember = "TENCV";
            cboChucVu.ValueMember = "IDCV";

            cboTrinhDo.DataSource = _trinhdo.getList();
            cboTrinhDo.DisplayMember = "TENTD";
            cboTrinhDo.ValueMember = "IDTD";

            cboDanToc.DataSource = _dantoc.getList();
            cboDanToc.DisplayMember = "TENDT";
            cboDanToc.ValueMember = "ID";

            cboTonGiao.DataSource = _tongiao.getList();
            cboTonGiao.DisplayMember = "TENTG";
            cboTonGiao.ValueMember = "ID";
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
            txtHoTen.Enabled = !kt;
            ckcGioiTinh.Enabled = !kt;
            dtNgaySinh.Enabled = !kt;
            txtCCCD.Enabled = !kt;
            txtDienThoai.Enabled = !kt;
            txtDiaChi.Enabled = !kt;
            cboChucVu.Enabled = !kt;
            cboTrinhDo.Enabled = !kt;
            cboPhongBan.Enabled = !kt;
            cboBoPhan.Enabled = !kt;
            cboDanToc.Enabled = !kt;
            cboTonGiao.Enabled = !kt;
        }
        void _reset()
        {
            txtHoTen.Text = string.Empty;
            txtCCCD.Text = string.Empty;
            txtDienThoai.Text = string.Empty;
            txtDiaChi.Text = string.Empty;
            ckcGioiTinh.Checked = false;
        }
        void loadData()
        {
            dataGridView1.DataSource = _nhanvien.getListFull();

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
                _nhanvien.Delete(_id);
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
        void SaveData()
        {
            if (_them)
            {
                tb_NHANVIEN nv = new tb_NHANVIEN();
                nv.HOTEN = txtHoTen.Text;
                nv.GIOITINH = ckcGioiTinh.Checked;
                nv.NGAYSINH = dtNgaySinh.Value;
                nv.DIENTHOAI = txtDienThoai.Text;
                nv.CCCD = txtCCCD.Text;
                nv.DIACHI = txtDiaChi.Text;
                nv.HINHANH = ImageToBase64(picHinhAnh.Image, picHinhAnh.Image.RawFormat);
                nv.IDPB = int.Parse(cboPhongBan.SelectedValue.ToString());
                nv.IDBP = int.Parse(cboBoPhan.SelectedValue.ToString());
                nv.IDCV = int.Parse(cboChucVu.SelectedValue.ToString());
                nv.IDTD = int.Parse(cboTrinhDo.SelectedValue.ToString());
                nv.IDDT = int.Parse(cboDanToc.SelectedValue.ToString());
                nv.IDTG = int.Parse(cboTonGiao.SelectedValue.ToString());
                nv.MACTY = 1;
                _nhanvien.Add(nv);
            }
            else
            {
                var nv = _nhanvien.getItem(_id);
                nv.HOTEN = txtHoTen.Text;
                nv.GIOITINH = ckcGioiTinh.Checked;
                nv.NGAYSINH = dtNgaySinh.Value;
                nv.DIENTHOAI = txtDienThoai.Text;
                nv.CCCD = txtCCCD.Text;
                nv.DIACHI = txtDiaChi.Text;
                nv.HINHANH = ImageToBase64(picHinhAnh.Image, picHinhAnh.Image.RawFormat);
                nv.IDPB = int.Parse(cboPhongBan.SelectedValue.ToString());
                nv.IDBP = int.Parse(cboBoPhan.SelectedValue.ToString());
                nv.IDCV = int.Parse(cboChucVu.SelectedValue.ToString());
                nv.IDTD = int.Parse(cboTrinhDo.SelectedValue.ToString());
                nv.IDDT = int.Parse(cboDanToc.SelectedValue.ToString());
                nv.IDTG = int.Parse(cboTonGiao.SelectedValue.ToString());
                nv.MACTY = 1;
                _nhanvien.Update(nv);
            }
        }
        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void ExportFile(DataTable dataTable, string sheetName, string title)
        {
            Microsoft.Office.Interop.Excel.Application oExcel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbooks oBooks;
            Microsoft.Office.Interop.Excel.Sheets oSheets;
            Microsoft.Office.Interop.Excel.Workbook oBook;
            Microsoft.Office.Interop.Excel.Worksheet oSheet;

            oExcel.Visible = true;
            oExcel.DisplayAlerts = false;
            oExcel.Application.SheetsInNewWorkbook = 1;
            oBooks = oExcel.Workbooks;
            oBook = (Microsoft.Office.Interop.Excel.Workbook)(oExcel.Workbooks.Add(Type.Missing));
            oSheets = oBook.Worksheets;
            oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oSheets.get_Item(1);
            oSheet.Name = sheetName;

            Microsoft.Office.Interop.Excel.Range head = oSheet.get_Range("A1", "N1");
            head.MergeCells = true;
            head.Value2 = title;
            head.Font.Bold = true;
            head.Font.Name = "Times New Roman";
            head.Font.Size = "20";
            head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            Microsoft.Office.Interop.Excel.Range cl1 = oSheet.get_Range("A3", "A3");
            cl1.Value2 = "Mã nhân viên";
            cl1.ColumnWidth = 12;

            Microsoft.Office.Interop.Excel.Range cl2 = oSheet.get_Range("B3", "B3");
            cl2.Value2 = "Họ tên nhân viên";
            cl2.ColumnWidth = 25;

            Microsoft.Office.Interop.Excel.Range cl3 = oSheet.get_Range("C3", "C3");
            cl3.Value2 = "Giới tính";
            cl3.ColumnWidth = 18;

            Microsoft.Office.Interop.Excel.Range cl4 = oSheet.get_Range("D3", "D3");
            cl4.Value2 = "Ngày sinh";
            cl4.ColumnWidth = 25;

            Microsoft.Office.Interop.Excel.Range cl5 = oSheet.get_Range("E3", "E3");
            cl5.Value2 = "Điện thoại";
            cl5.ColumnWidth = 25;

            Microsoft.Office.Interop.Excel.Range cl6 = oSheet.get_Range("F3", "F3");
            cl6.Value2 = "CCCD";
            cl6.ColumnWidth = 25;

            Microsoft.Office.Interop.Excel.Range cl7 = oSheet.get_Range("G3", "G3");
            cl7.Value2 = "Địa chỉ";
            cl7.ColumnWidth = 35;

            Microsoft.Office.Interop.Excel.Range cl8 = oSheet.get_Range("H3", "H3");
            cl8.Value2 = "Phòng ban";
            cl8.ColumnWidth = 20;

            Microsoft.Office.Interop.Excel.Range cl9 = oSheet.get_Range("I3", "I3");
            cl9.Value2 = "Bộ phận";
            cl9.ColumnWidth = 25;

            Microsoft.Office.Interop.Excel.Range cl10 = oSheet.get_Range("J3", "J3");
            cl10.Value2 = "Chức vụ";
            cl10.ColumnWidth = 20;

            Microsoft.Office.Interop.Excel.Range cl11 = oSheet.get_Range("K3", "K3");
            cl11.Value2 = "Trình độ";
            cl11.ColumnWidth = 20;

            Microsoft.Office.Interop.Excel.Range cl12 = oSheet.get_Range("L3", "L3");
            cl12.Value2 = "Dân tộc";
            cl12.ColumnWidth = 20;

            Microsoft.Office.Interop.Excel.Range cl13 = oSheet.get_Range("M3", "M3");
            cl13.Value2 = "Tôn giáo";
            cl13.ColumnWidth = 20;

            Microsoft.Office.Interop.Excel.Range cl14 = oSheet.get_Range("N3", "N3");
            cl14.Value2 = "Tên công ty";
            cl14.ColumnWidth = 20;


            Microsoft.Office.Interop.Excel.Range rowHead = oSheet.get_Range("A3", "N3");
            rowHead.Font.Bold = true;

            rowHead.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;

            rowHead.Interior.ColorIndex = 6;
            rowHead.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            object[,] arr = new object[dataTable.Rows.Count, dataTable.Columns.Count];

            for(int row = 0; row < dataTable.Rows.Count; row++)
            {
                DataRow dataRow = dataTable.Rows[row];
                for (int col = 0; col < dataTable.Columns.Count; col++)
                {
                    arr[row, col] = dataRow[col];
                }
            }

            int rowStart = 4;
            int columnStart = 1;
            int rowEnd = rowStart + dataTable.Rows.Count - 1;
            int columnEnd = dataTable.Columns.Count;

            Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowStart, columnStart];
            Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, columnEnd];
            Microsoft.Office.Interop.Excel.Range range = oSheet.get_Range(c1, c2);
            range.Value2 = arr;

            range.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;

            oSheet.get_Range(c1, c2).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();

            DataColumn col1 = new DataColumn("MANV");
            DataColumn col2 = new DataColumn("HOTEN");
            DataColumn col3 = new DataColumn("GIOITINH");
            DataColumn col4 = new DataColumn("NGAYSINH");
            DataColumn col5 = new DataColumn("DIENTHOAI");
            DataColumn col6 = new DataColumn("CCCD");
            DataColumn col7 = new DataColumn("DIACHI");
            DataColumn col9 = new DataColumn("TENPB");
            DataColumn col10 = new DataColumn("TENBP");
            DataColumn col11 = new DataColumn("TENCV");
            DataColumn col12 = new DataColumn("TENTD");
            DataColumn col13 = new DataColumn("TENDT");
            DataColumn col14 = new DataColumn("TENTG");
            DataColumn col15 = new DataColumn("TENCTY");

            dataTable.Columns.Add(col1);
            dataTable.Columns.Add(col2);
            dataTable.Columns.Add(col3);
            dataTable.Columns.Add(col4);
            dataTable.Columns.Add(col5);
            dataTable.Columns.Add(col6);
            dataTable.Columns.Add(col7);
            dataTable.Columns.Add(col9);
            dataTable.Columns.Add(col10);
            dataTable.Columns.Add(col11);
            dataTable.Columns.Add(col12);
            dataTable.Columns.Add(col13);
            dataTable.Columns.Add(col14);
            dataTable.Columns.Add(col15);

            foreach (DataGridViewRow dtgvRow in dataGridView1.Rows)
            {
                DataRow dtrow = dataTable.NewRow();

                dtrow[0] = dtgvRow.Cells[0].Value;
                dtrow[1] = dtgvRow.Cells[1].Value;
                dtrow[2] = dtgvRow.Cells[2].Value;
                dtrow[3] = dtgvRow.Cells[3].Value;
                dtrow[4] = dtgvRow.Cells[4].Value;
                dtrow[5] = dtgvRow.Cells[5].Value;
                dtrow[6] = dtgvRow.Cells[6].Value;
                dtrow[7] = dtgvRow.Cells[9].Value;
                dtrow[8] = dtgvRow.Cells[11].Value;
                dtrow[9] = dtgvRow.Cells[13].Value;
                dtrow[10] = dtgvRow.Cells[15].Value;
                dtrow[11] = dtgvRow.Cells[17].Value;
                dtrow[12] = dtgvRow.Cells[19].Value;
                dtrow[13] = dtgvRow.Cells[21].Value;
                

                dataTable.Rows.Add(dtrow);
            }

            ExportFile(dataTable, "Danh sach", "DANH SÁCH SINH VIÊN");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                _id = Convert.ToInt32(row.Cells["MANV"].Value.ToString());
                var nv = _nhanvien.getItem(_id);
                txtHoTen.Text = nv.HOTEN;
                ckcGioiTinh.Checked = nv.GIOITINH.Value;
                dtNgaySinh.Value = nv.NGAYSINH.Value;
                txtDienThoai.Text = nv.DIENTHOAI;
                txtCCCD.Text = nv.CCCD;
                txtDiaChi.Text = nv.DIACHI;
                picHinhAnh.Image = Base64ToImage(nv.HINHANH);
                picHinhAnh.SizeMode = PictureBoxSizeMode.StretchImage;
                cboPhongBan.SelectedValue = nv.IDPB;
                cboBoPhan.SelectedValue = nv.IDBP;
                cboChucVu.SelectedValue = nv.IDCV;
                cboTrinhDo.SelectedValue = nv.IDTD;
                cboDanToc.SelectedValue = nv.IDDT;
                cboTonGiao.SelectedValue = nv.IDTG;
               
            }
        }

        public byte[] ImageToBase64(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();
                return imageBytes;
            }
        }

        public Image Base64ToImage(byte[] imageBytes)
        {
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }

        private void btnHinhAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Picture file (.png, .jpg)|*.png;*.jpg";
            openFile.Title = "Chọn hình ảnh";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                picHinhAnh.Image = Image.FromFile(openFile.FileName);
                picHinhAnh.SizeMode = PictureBoxSizeMode.StretchImage;
                
            }
        }

        
    }
}

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
    public partial class Waiting : Form
    {
        private Timer timer;
        private int maxCount = 100;

        public Waiting()
        {

            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            // Thiết lập giá trị mặc định cho ProgressBar và Label
            progressBar1.Minimum = 0;
            progressBar1.Maximum = maxCount;
            progressBar1.Value = 0;
            label3.Text = "0%";

            // Tạo Timer với thời gian là 5 giây
            timer = new Timer();
            timer.Interval = 20; // đơn vị là miliseconds
            timer.Tick += new EventHandler(timer_Tick);

            // Khởi động Timer khi Form được load
            this.Load += new EventHandler(Waiting_Load);
        }

        void Waiting_Load(object sender, EventArgs e)
        {
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            // Tính toán giá trị tương ứng cho ProgressBar và Label
            int percent = (int)Math.Round((double)progressBar1.Value / (double)progressBar1.Maximum * 100);

            // Cập nhật giá trị ProgressBar và Label
            if (percent < 100)
            {
                progressBar1.Value++;
                label3.Text = $"{percent + 1}%";
            }
            else
            {
                // Dừng Timer và đóng Form
                timer.Stop();
                this.Close();
            }
        }
    }
}

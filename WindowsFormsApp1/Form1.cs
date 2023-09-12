using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class frmTimer : Form
    {
        public frmTimer()
        {
            InitializeComponent();
        }

        private string Getformatted(int interval, TimeSpan timeSpan)
        {
            return $"{interval - timeSpan.TotalSeconds % interval:N1}";
        }

        private string Getformatted(DateTime startOn, int interval)
        {
            var escaped = DateTime.Now - startOn;
            return interval != 0 ? $"{interval - escaped.TotalSeconds % interval:N0}" : $"{escaped.ToString(@"hh\:mm\:ss")}";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.lblNow.Text = DateTime.Now.ToString("HH:mm:ss");

            if (item1Enabled)
            {
                //var item1t = DateTime.Now - item1StartOn;
                this.lblInterval1.Text = Getformatted(item1StartOn, item1Interval);// $"{item1Interval - item1t.TotalSeconds % item1Interval:N0}";
            }

            if (item2Enabled)
            {
                //var item2t = DateTime.Now - item2StartOn;
                this.lblInterval2.Text = Getformatted(item2StartOn, item2Interval);// $"{item2Interval - item2t.TotalSeconds % item2Interval:N0}";
            }

            if (item3Enabled)
            {
                //var item3t = DateTime.Now - item3StartOn;
                this.lblInterval3.Text = Getformatted(item3StartOn, item3Interval);// $"{item3Interval - item3t.TotalSeconds % item3Interval:N0}";
            }

            if (item4Enabled)
            {
                //var item4t = DateTime.Now - item4StartOn;
                this.lblInterval4.Text = Getformatted(item4StartOn, item4Interval);// $"{item4Interval - item4t.TotalSeconds % item4Interval:N0}";
            }

            if (item5Enabled)
            {
                //var item5t = DateTime.Now - item5StartOn;
                this.lblInterval5.Text = Getformatted(item5StartOn, item5Interval);// $"{item5Interval - item5t.TotalSeconds % item5Interval:N0}";
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.lblInterval1.Text = this.lblInterval2.Text = this.lblInterval3.Text = this.lblInterval4.Text = this.lblInterval5.Text = "";
            //this.Text = "Timer";
            this.timer1.Start();
            //this.timer1.Interval = 100;
        }

        private bool item1Enabled;
        private bool item2Enabled;
        private bool item3Enabled;
        private bool item4Enabled;
        private bool item5Enabled;

        private DateTime item1StartOn;
        private DateTime item2StartOn;
        private DateTime item3StartOn;
        private DateTime item4StartOn;
        private DateTime item5StartOn;

        private int item1Interval;
        private int item2Interval;
        private int item3Interval;
        private int item4Interval;
        private int item5Interval;

        private void btnXiaotie_Click(object sender, EventArgs e)
        {
            Reset();
            this.Text = $"{btnXiaotie.Text}";

            this.btnStart1.Text = $"群破甲(20)";
            item1Interval = 20;

            this.btnStart2.Text = $"巨力(30)";
            item2Interval = 30;

            this.btnStart3.Text = $"大群(20)";
            item3Interval = 20;

            this.btnStart4.Text = "";
            item4Interval = 0;
            this.btnStart4.Enabled = false;

            this.btnStart5.Text = "";
            item5Interval = 0;
            this.btnStart5.Enabled = false;

            this.richTextBox1.Text = "破甲群 无利齿320W 真256W 假272W\n" +
                "利刃 无利齿200W计 真160W 假170W\n" +
                "大群 无利齿120W计 真90W 假102W";
        }

        private void btnZichun_Click(object sender, EventArgs e)
        {
            Reset();
            this.Text = $"{btnZichun.Text}";

            this.btnStart1.Text = $"封印(20)";
            item1Interval = 20;

            this.btnStart2.Text = $"群晕(20)";
            item2Interval = 20;

            this.btnStart3.Text = $"流血(30)";
            item3Interval = 30;

            this.btnStart4.Text = "";
            item4Interval = 0;
            this.btnStart4.Enabled = false;

            this.btnStart5.Text = "";
            item5Interval = 0;
            this.btnStart5.Enabled = false;

            this.richTextBox1.Text = "封印 无利齿450W 真360W 假382.5W\n" +
                "群晕 无利齿300W 真240W 假255W\n" +
                "吸蓝流血 无利齿150W 真120W 假127.5W";
        }

        private void btnCangli_Click(object sender, EventArgs e)
        {
            Reset();
            this.Text = $"{btnCangli.Text}";

            this.btnStart1.Text = $"群流血(30)";
            item1Interval = 30;

            this.btnStart2.Text = $"群晕(20)";
            item2Interval = 20;

            this.btnStart3.Text = $"大群(20)";
            item3Interval = 20;

            this.btnStart4.Enabled = true;
            this.btnStart4.Text = $"大群(20)";
            item4Interval = 20;

            this.btnStart5.Text = "";
            item5Interval = 0;
            this.btnStart5.Enabled = false;

            this.richTextBox1.Text = "群流血 无利齿450W 真360W 假382.5W\n" +
                "群晕 无利齿250W 真200W 假212.5W\n" +
                "大群 无利齿150W 真120W 假127.5W\n" +
                "大群 150W 125W会刷新大群时间，清仇恨\n" +
                "最好是160W等晕后20秒内直接龙打到125W以下\n"
                ;
        }

        private void btnTiandi_Click(object sender, EventArgs e)
        {
            Reset();
            this.Text = $"{btnTiandi.Text}";

            this.btnStart1.Text = $"群驱(60)";
            item1Interval = 60;

            this.btnStart2.Text = $"群吸元(45)";
            item2Interval = 45;

            this.btnStart3.Text = $"群木毒(35)";
            item3Interval = 35;

            this.btnStart4.Enabled = true;
            this.btnStart4.Text = $"群晕(30)";
            item4Interval = 30;

            this.btnStart5.Enabled = true;
            this.btnStart5.Text = $"大群(55)";
            item5Interval = 55;

            this.richTextBox1.Text = "群驱 无利齿540W 真432W 假459W\n" +
                "群吸元 无利齿420W 真336W 假357W\n" +
                "群木毒 无利齿300W 真240W 假255W\n" +
                "群晕 无利齿180W 真144W 假153W\n" +
                "大群 无利齿150W 真120W 假127.5W";
        }

        private void Reset()
        {
            item1Enabled = false;
            lblInterval1.Text = "";

            item2Enabled = false;
            lblInterval2.Text = "";

            item3Enabled = false;
            lblInterval3.Text = "";

            item4Enabled = false;
            lblInterval4.Text = "";

            item5Enabled = false;
            lblInterval5.Text = "";

            item1Interval = item2Interval = item3Interval = item4Interval = item5Interval = 0;
            btnStart1.Text = btnStart2.Text = btnStart3.Text = btnStart4.Text = btnStart5.Text = "开始";
            btnStart1.Enabled = btnStart2.Enabled = btnStart3.Enabled = btnStart4.Enabled = btnStart5.Enabled = true;
        }

        private void btnStart1_Click(object sender, EventArgs e)
        {
            item1Enabled = true;
            item1StartOn = DateTime.Now;
        }

        private void btnClear1_Click(object sender, EventArgs e)
        {
            item1Enabled = false;
            this.lblInterval1.Text = "";
        }

        private void btnStart2_Click(object sender, EventArgs e)
        {
            item2Enabled = true;
            item2StartOn = DateTime.Now;
        }

        private void btnClear2_Click(object sender, EventArgs e)
        {
            item2Enabled = false;
            this.lblInterval2.Text = "";
        }

        private void btnStart3_Click(object sender, EventArgs e)
        {
            item3Enabled = true;
            item3StartOn = DateTime.Now;
        }

        private void btnClear3_Click(object sender, EventArgs e)
        {
            item3Enabled = false;
            this.lblInterval3.Text = "";
        }

        private void btnStart4_Click(object sender, EventArgs e)
        {
            item4Enabled = true;
            item4StartOn = DateTime.Now;
        }

        private void btnClear4_Click(object sender, EventArgs e)
        {
            item4Enabled = false;
            this.lblInterval4.Text = "";
        }

        private void btnStart5_Click(object sender, EventArgs e)
        {
            item5Enabled = true;
            item5StartOn = DateTime.Now;
        }

        private void btnClear5_Click(object sender, EventArgs e)
        {
            item5Enabled = false;
            this.lblInterval5.Text = "";
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            this.Text = "计时器";
            Reset();
        }

        private void lblInterval1_Click(object sender, EventArgs e)
        {
            var label = (Label)sender;
            label.Text = "";
        }

        private void lblInterval_Click(object sender, EventArgs e)
        {
            var label = (Label)sender;
            label.Text = "";
        }

        private void lblInterval1_Click_1(object sender, EventArgs e)
        {
            item1Enabled = false;
            this.lblInterval1.Text = "";
        }

        private void lblInterval2_Click(object sender, EventArgs e)
        {
            item2Enabled = false;
            this.lblInterval2.Text = "";
        }

        private void lblInterval3_Click(object sender, EventArgs e)
        {
            item3Enabled = false;
            this.lblInterval3.Text = "";
        }

        private void lblInterval4_Click(object sender, EventArgs e)
        {
            item4Enabled = false;
            this.lblInterval4.Text = "";
        }

        private void lblInterval5_Click(object sender, EventArgs e)
        {
            item5Enabled = false;
            this.lblInterval5.Text = "";
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

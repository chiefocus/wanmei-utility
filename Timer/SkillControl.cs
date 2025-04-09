using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Timer.Models;

namespace Timer
{
    public partial class SkillControl : UserControl
    {
        public bool Enabled { get; set; }
        public Skill RowData { get; set; }
        public DateTime StartOn { get; set; }
        public int Interval { get; set; }
        public string Description { get; set; }
        public int Flag { get; set; }
        public bool Clickable { get; set; }
        public System.Windows.Forms.Timer Timer1 { get; set; }

        private Point _stopwatchControlOriginalLocation;

        private Stopwatch _stopwatch;

        public SkillControl()
        {
        }

        public SkillControl(Skill row, System.Windows.Forms.Timer timer)
        {
            InitializeComponent();

            this.Dock = DockStyle.Top;

            _stopwatchControlOriginalLocation = this.stopwatchControl1.Location;

            this.stopwatchControl1.Visible = false;

            this.label1.Visible = false; //后浪专用计时
            this.label2.Visible = false; //Plus
            this.label3.Visible = false; //Minus

            Timer1 = timer;
            Timer1.Tick += new System.EventHandler(this.timer1_Tick);

            this.RowData = row;
            this.button1.Text = RowData.Interval == 0 ? RowData.Name : $"{RowData.Name}";//row.Button1Text;
            this.button2.Text = row.Reset;
            this.textBox1.Text = row.Description;
            this.textBox2.Text = row.Interval == 0 ? "" : $"{row.Interval}";
            this.Interval = row.Interval;
            this.Flag = row.Flag;
            this.Clickable = row.Clickable;
            this.Tag = row.Interval;
            this.Description = row.Description;

            toolTip1.SetToolTip(this.button1, row.Description);
        }

        public void UpdateLabel()
        {
            if (Enabled)
            {
                if (Interval != 0)
                {
                    var escaped = DateTime.Now - StartOn;
                    var ts = TimeSpan.FromSeconds(Interval - escaped.TotalSeconds % Interval);

                    this.stopwatchControl1.Seconds = (int)ts.TotalSeconds;
                    this.stopwatchControl1.Milliseconds = ts.Milliseconds / 100;
                }
                else
                {
                    this.label1.Text = $"{_stopwatch.Elapsed.Hours:00}:{_stopwatch.Elapsed.Minutes:00}:{_stopwatch.Elapsed.Seconds:00}";
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateLabel();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Clickable)
            {
                OnClick(this);
            }
        }

        public void OnClick(SkillControl skillControl)
        {
            skillControl.Enabled = true;
            skillControl.textBox1.Visible = false;

            if (Interval != 0)
            {
                skillControl.StartOn = DateTime.Now;
                skillControl.stopwatchControl1.Visible = true;
                skillControl.stopwatchControl1.Seconds = Interval;
                this.label1.Visible = false;
                this.label2.Visible = Timer.Profile.PlusFlag;
                this.label3.Visible = Timer.Profile.MinusFlag;

                if (Interval >= 100)
                {
                    this.stopwatchControl1.Location = new Point(_stopwatchControlOriginalLocation.X + 12,
                        _stopwatchControlOriginalLocation.Y);
                }
            }

            if (Interval == 0)
            {
                _stopwatch = Stopwatch.StartNew();
                skillControl.stopwatchControl1.Visible = false;
                this.label1.Visible = true;
                this.label2.Visible = false;
                this.label3.Visible = false;
                this.label1.Text = $"{_stopwatch.Elapsed.Hours:00}:{_stopwatch.Elapsed.Minutes:00}:{_stopwatch.Elapsed.Seconds:00}";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            this.stopwatchControl1.Visible = false;
            this.textBox1.Visible = true;
            this.textBox1.Text = this.Description;
            this.label1.Visible = false;
            this.label2.Visible = false;
            this.label3.Visible = false;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.StartOn = StartOn.AddMilliseconds(Timer.Profile.Offset);
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.StartOn = StartOn.AddMilliseconds(-Timer.Profile.Offset);
        }
    }
}

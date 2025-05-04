using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Timer.Models;

namespace Timer
{
    public partial class SkillControl : UserControl
    {
        public bool Enabled { get; set; }
        public Skill Skill { get; set; }
        public DateTime StartOn { get; set; }
        public int Interval { get; set; }
        public string Description { get; set; }
        public int Flag { get; set; }
        public bool Clickable { get; set; }
        public System.Windows.Forms.Timer Timer1 { get; set; }

        private Point _stopwatchControlOriginalLocation;

        public SkillControl(Skill skill, System.Windows.Forms.Timer timer)
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

            this.Skill = skill;
            this.button1.Text = Skill.Interval == 0 ? Skill.Name : $"{Skill.Name}";//row.Button1Text;
            this.button2.Text = skill.Reset;
            this.textBox1.Text = skill.Description;
            this.textBox2.Text = skill.Interval == 0 ? "" : $"{skill.Interval}";
            this.Interval = skill.Interval;
            this.Flag = skill.Flag;
            this.Clickable = skill.Clickable;
            this.Tag = skill.Interval;
            this.Description = skill.Description;

            toolTip1.SetToolTip(this.button1, skill.Description);
        }

        public void UpdateLabel()
        {
            var interval = GetInterval(this.textBox2.Text);

            if (Enabled)
            {
                if (interval != 0)
                {
                    var escaped = DateTime.Now - StartOn;
                    var ts = TimeSpan.FromSeconds(interval - escaped.TotalSeconds % interval);

                    this.stopwatchControl1.Seconds = (int)ts.TotalSeconds;
                    this.stopwatchControl1.Milliseconds = ts.Milliseconds / 100;

                    if (interval >= 100 && ts.TotalSeconds < 100)
                    {
                        this.stopwatchControl1.Location = _stopwatchControlOriginalLocation;
                    }
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

            var interval = GetInterval(this.textBox2.Text);

            if (interval != 0)
            {
                skillControl.StartOn = DateTime.Now;
                skillControl.stopwatchControl1.Visible = true;
                skillControl.stopwatchControl1.Seconds = GetInterval(this.textBox2.Text);
                this.label1.Visible = false;
                this.label2.Visible = Timer.Profile.PlusFlag;
                this.label3.Visible = Timer.Profile.MinusFlag;

                if (interval >= 100)
                {
                    this.stopwatchControl1.Location = new Point(_stopwatchControlOriginalLocation.X + 10,
                        _stopwatchControlOriginalLocation.Y);
                }
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

        private static int GetInterval(string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                int r = 0;
                int.TryParse(s, out r);
                return r;
            }
            return 0;
        }
    }
}

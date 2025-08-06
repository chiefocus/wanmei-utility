using System;
using System.Diagnostics;
using System.Windows.Forms;
using TimerUtility.Models;

namespace TimerUtility
{
    public partial class SkillControl : UserControl
    {
        public bool Enabled { get; set; }
        public Skill Skill { get; set; }
        public int Interval { get; set; }
        public string Description { get; set; }
        public int Flag { get; set; }
        public bool Clickable { get; set; }
        public System.Windows.Forms.Timer Timer1 { get; set; }

        private Stopwatch _stopwatch;
        private double _offsetMilliseconds = 0.0;

        public SkillControl(Skill skill, System.Windows.Forms.Timer timer)
        {
            InitializeComponent();

            this.Dock = DockStyle.Top;
            this.stopwatchDisplay1.Visible = false;

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

            _stopwatch = new Stopwatch();
        }

        public void UpdateLabel()
        {
            if (Enabled && Interval > 0)
            {
                var elapsedSeconds = _stopwatch.Elapsed.TotalSeconds - _offsetMilliseconds / 1000;
                var remaining = Math.Max(0, Interval - (elapsedSeconds % Interval));

                this.stopwatchDisplay1.Seconds = (int)remaining;
                this.stopwatchDisplay1.Milliseconds = (int)(remaining * 10) % 10;
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
            _stopwatch.Restart();
            _offsetMilliseconds = 0.0;

            skillControl.Enabled = true;
            skillControl.textBox1.Visible = false;

            if (Interval > 0)
            {
                skillControl.stopwatchDisplay1.Visible = true;
                skillControl.stopwatchDisplay1.Seconds = Interval;
                this.label2.Visible = Timer.Profile.PlusFlag;
                this.label3.Visible = Timer.Profile.MinusFlag;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            this.stopwatchDisplay1.Visible = false;
            this.textBox1.Visible = true;
            this.textBox1.Text = this.Description;
            this.label2.Visible = false;
            this.label3.Visible = false;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            _offsetMilliseconds += Timer.Profile.Offset;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            _offsetMilliseconds -= Timer.Profile.Offset;
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            this.Interval = GetInterval(textBox2.Text);
        }

        private void stopwatchDisplay1_DoubleClick(object sender, EventArgs e)
        {
            Timer.Profile.MillisecondsFlag = !Timer.Profile.MillisecondsFlag;
        }
    }
}

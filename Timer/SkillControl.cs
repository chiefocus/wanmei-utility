using System;
using System.Diagnostics;
using System.Linq;
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
        public Timer Timer1 { get; set; }

        private Stopwatch _stopwatch;
        private double _offsetMilliseconds = 0.0;

        public SkillControl(Skill skill, Timer timer)
        {
            InitializeComponent();

            this.Dock = DockStyle.Top;
            this.stopwatchDisplay1.Visible = false;

            this.label2.Visible = false; //Plus
            this.label3.Visible = false; //Minus

            Timer1 = timer;
            Timer1.Tick += new EventHandler(this.timer1_Tick);

            this.Skill = skill;
            this.button1.Text = Skill.Interval == 0 ? Skill.Name : $"{Skill.Name}";
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
                this.label2.Visible = WanmeiTimer.Settings.Profile.PlusFlag;
                this.label3.Visible = WanmeiTimer.Settings.Profile.MinusFlag;
            }

            if (!string.IsNullOrEmpty(Skill.Affiliate))
            {
                var control = WanmeiTimer.SkillControls.FirstOrDefault(s => s.Skill.Name.Equals(Skill.Affiliate));
                if (control != null && !control.Enabled)
                {
                    control.button1.PerformClick();
                }
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
            _offsetMilliseconds += WanmeiTimer.Settings.Profile.Offset;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            _offsetMilliseconds -= WanmeiTimer.Settings.Profile.Offset;
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

            if (targetSkill != null && targetSkill.Interval != this.Interval)
            {
                targetSkill.Interval = this.Interval;
                WanmeiTimer.SettingsChanged = true;
            }
        }

        private void stopwatchDisplay1_DoubleClick(object sender, EventArgs e)
        {
            WanmeiTimer.Settings.Profile.MillisecondsFlag = !WanmeiTimer.Settings.Profile.MillisecondsFlag;
            WanmeiTimer.SettingsChanged = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (targetSkill != null && targetSkill.Description != this.textBox1.Text)
            {
                targetSkill.Description = this.textBox1.Text;
                WanmeiTimer.SettingsChanged = true;
            }
        }

        private Skill targetSkill =>
            WanmeiTimer.Settings.Instances.FirstOrDefault(i => i.Name.Equals(Skill.InstanceName))
            ?.Bosses.FirstOrDefault(b => b.Name.Equals(Skill.BossName))
            ?.Skills.FirstOrDefault(s => s.Name.Equals(Skill.Name))
            ?? WanmeiTimer.Settings.UserDefinedBoss.Skills.FirstOrDefault(s => s.Name.Equals(Skill.Name));

    }
}

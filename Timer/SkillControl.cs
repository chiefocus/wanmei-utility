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

            Dock = DockStyle.Top;
            stopwatchDisplay1.Visible = false;

            label2.Visible = false; //Plus
            label3.Visible = false; //Minus

            Timer1 = timer;
            Timer1.Tick += new EventHandler(timer1_Tick);

            Skill = skill;
            button1.Text = Skill.Interval == 0 ? Skill.Name : $"{Skill.Name}";
            button2.Text = skill.Reset;
            textBox1.Text = skill.Description;
            textBox2.Text = skill.Interval == 0 ? "" : $"{skill.Interval}";
            Interval = skill.Interval;
            Flag = skill.Flag;
            Clickable = skill.Clickable;
            Tag = skill.Interval;
            Description = skill.Description;

            toolTip1.SetToolTip(button1, skill.Description);

            _stopwatch = new Stopwatch();
        }

        public void UpdateLabel()
        {
            if (Enabled && Interval > 0)
            {
                var elapsedSeconds = _stopwatch.Elapsed.TotalSeconds - _offsetMilliseconds / 1000;
                var remaining = Math.Max(0, Interval - (elapsedSeconds % Interval));

                stopwatchDisplay1.Seconds = (int)remaining;
                stopwatchDisplay1.Milliseconds = (int)(remaining * 10) % 10;
            }
        }

        private void timer1_Tick(object sender, EventArgs e) => UpdateLabel();

        private void button1_Click(object sender, EventArgs e)
        {
            var control = sender as Control;
            Console.WriteLine(control.Text);

            if (Clickable && Interval > 0) OnClick();
        }

        private void OnClick()
        {
            Enabled = true;

            if (!string.IsNullOrEmpty(Skill.Affiliate))
            {
                var affiliate = WanmeiTimer.SkillControls.FirstOrDefault(s => s.Skill.Name.Equals(Skill.Affiliate));
                if (affiliate != null && !affiliate.Enabled)
                {
                    Console.WriteLine("affiliate");
                    affiliate.BeginInvoke(new Action(() => affiliate.button1.PerformClick()));
                }
            }

            _stopwatch.Restart();
            _offsetMilliseconds = 0.0;

            textBox1.Visible = false;
            stopwatchDisplay1.Visible = true;
            stopwatchDisplay1.Seconds = Interval;
            label2.Visible = WanmeiTimer.Settings.Profile.PlusFlag;
            label3.Visible = WanmeiTimer.Settings.Profile.MinusFlag;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Enabled = false;
            stopwatchDisplay1.Visible = false;
            textBox1.Visible = true;
            textBox1.Text = Description;
            label2.Visible = false;
            label3.Visible = false;
        }

        private void label2_Click(object sender, EventArgs e) => _offsetMilliseconds += WanmeiTimer.Settings.Profile.Offset;
        private void label3_Click(object sender, EventArgs e) => _offsetMilliseconds -= WanmeiTimer.Settings.Profile.Offset;

        private static int ParseInterval(string s) => int.TryParse(s, out var r) ? r : 0;

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Interval = ParseInterval(textBox2.Text);
            if (targetSkill != null && targetSkill.Interval != Interval)
            {
                targetSkill.Interval = Interval;
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
            if (targetSkill != null && targetSkill.Description != textBox1.Text)
            {
                targetSkill.Description = textBox1.Text;
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

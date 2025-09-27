using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using TimerUtility.Models;

namespace TimerUtility
{
    public partial class SkillControl : UserControl
    {
        public bool Enabled { get; set; }
        public Skill Skill { get; set; }
        public Timer Timer1 { get; set; }
        public List<SkillControl> AffiliateSkills { get; set; } = new List<SkillControl>();

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

            toolTip1.SetToolTip(button1, skill.Description);

            _stopwatch = new Stopwatch();
        }

        private void UpdateLabel()
        {
            if (Enabled && Skill.Interval > 0)
            {
                var elapsedSeconds = _stopwatch.Elapsed.TotalSeconds - _offsetMilliseconds / 1000;
                var remaining = Math.Max(0, Skill.Interval - (elapsedSeconds % Skill.Interval));

                stopwatchDisplay1.Seconds = (int)remaining;
                stopwatchDisplay1.Milliseconds = (int)(remaining * 10) % 10;
            }
        }

        private void timer1_Tick(object sender, EventArgs e) => UpdateLabel();

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var skill in AffiliateSkills)
            {
                skill.Start();
            }
        }

        public void Start()
        {
            if (!Skill.Clickable || Skill.Interval <= 0) return;

            Enabled = true;
            _stopwatch.Restart();
            _offsetMilliseconds = 0.0;

            textBox1.Visible = false;
            stopwatchDisplay1.Visible = true;
            stopwatchDisplay1.Seconds = Skill.Interval;
            label2.Visible = WanmeiTimer.Settings.Profile.PlusFlag;
            label3.Visible = WanmeiTimer.Settings.Profile.MinusFlag;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Enabled = false;
            stopwatchDisplay1.Visible = false;
            textBox1.Visible = true;
            textBox1.Text = Skill.Description;
            label2.Visible = false;
            label3.Visible = false;
        }

        private void label2_Click(object sender, EventArgs e) => _offsetMilliseconds += WanmeiTimer.Settings.Profile.Offset;
        private void label3_Click(object sender, EventArgs e) => _offsetMilliseconds -= WanmeiTimer.Settings.Profile.Offset;

        private static int ParseInterval(string s) => int.TryParse(s, out var r) ? r : 0;

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            var interval = ParseInterval(textBox2.Text);
            if (targetSkill != null && targetSkill.Interval != interval)
            {
                targetSkill.Interval = interval;
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

        private Skill targetSkill => WanmeiTimer.Settings.UserDefinedBoss.SkillDic.GetValue(Skill.Id) ??
            WanmeiTimer.Settings.InstanceDic.GetValue(Skill.InstanceId)
            ?.BossDic.GetValue(Skill.BossId)?.SkillDic.GetValue(Skill.Id);
    }
}

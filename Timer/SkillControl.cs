using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using TimerUtility.Models;

namespace TimerUtility
{
    public partial class SkillControl : UserControl
    {
        public Skill Skill { get; set; }
        public List<SkillControl> AffiliateSkills { get; set; } = new List<SkillControl>();

        private Timer timer;
        private readonly Stopwatch stopwatch;
        private bool isRunning;
        private int offsetMilliseconds = 0;
        private int intervalMilliseconds = 0;

        public SkillControl(Skill skill, Timer timer)
        {
            InitializeComponent();

            Dock = DockStyle.Top;
            stopwatchDisplay1.Skill = skill;
            stopwatchDisplay1.Visible = false;

            label2.Visible = false; //Plus
            label3.Visible = false; //Minus

            this.timer = timer;
            timer.Tick += new EventHandler(timer1_Tick);

            Skill = skill;
            button1.Text = skill.Name;
            button2.Text = skill.Reset;
            textBox1.Text = skill.Description;
            textBox2.Text = skill.Interval == 0 ? "" : $"{skill.Interval}";

            stopwatch = new Stopwatch();
            intervalMilliseconds = Skill.Interval * 1000;
        }

        private void RefreshDisplay()
        {
            if (isRunning && Skill.Interval > 0)
            {
                int elapsed = (int)(stopwatch.Elapsed.TotalMilliseconds - offsetMilliseconds);
                int remaining = intervalMilliseconds - elapsed % intervalMilliseconds;

                stopwatchDisplay1.Seconds = remaining / 1000;
                stopwatchDisplay1.Milliseconds = remaining / 100 % 10;
            }
        }

        private void timer1_Tick(object sender, EventArgs e) => RefreshDisplay();

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

            isRunning = true;
            stopwatch.Restart();
            offsetMilliseconds = 0;

            textBox1.Visible = false;
            stopwatchDisplay1.Visible = true;
            stopwatchDisplay1.Seconds = Skill.Interval;
            label2.Visible = WanmeiTimer.Settings.Profile.PlusFlag;
            label3.Visible = WanmeiTimer.Settings.Profile.MinusFlag;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            isRunning = false;
            stopwatchDisplay1.Visible = false;
            textBox1.Visible = true;
            textBox1.Text = Skill.Description;
            label2.Visible = false;
            label3.Visible = false;
        }

        private void label2_Click(object sender, EventArgs e) => offsetMilliseconds += WanmeiTimer.Settings.Profile.Offset;
        private void label3_Click(object sender, EventArgs e) => offsetMilliseconds -= WanmeiTimer.Settings.Profile.Offset;

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox2.Text.Trim(), out var interval);
            if (targetSkill != null && targetSkill.Interval != interval)
            {
                targetSkill.Interval = interval;
                Skill.Interval = interval;
                intervalMilliseconds = interval * 1000;
                WanmeiTimer.SettingsChanged = true;
                button2.PerformClick();
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
                Skill.Description = textBox1.Text;
                WanmeiTimer.SettingsChanged = true;
            }
        }

        private Skill targetSkill => WanmeiTimer.Settings.UserDefinedBoss.SkillDic.GetValue(Skill.Id) ??
            WanmeiTimer.Settings.InstanceDic.GetValue(Skill.InstanceId)
            ?.BossDic.GetValue(Skill.BossId)?.SkillDic.GetValue(Skill.Id);
    }
}

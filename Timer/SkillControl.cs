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

        private readonly Timer timer;
        private readonly Stopwatch stopwatch;
        private bool isRunning;
        private int offsetMilliseconds = 0;

        private int IntervalMilliseconds => Skill.Interval * 1000;

        public SkillControl(Skill skill, Timer timer)
        {
            InitializeComponent();

            Dock = DockStyle.Top;
            stopwatchDisplay1.Visible = false;
            stopwatchDisplay1.Skill = skill;
            label2.Visible = false; //Plus
            label3.Visible = false; //Minus

            this.timer = timer;
            Skill = skill;
            button1.Text = skill.Name;
            button2.Text = skill.Reset;
            textBox1.Text = skill.Description;
            textBox2.Text = skill.Interval == 0 ? "" : $"{skill.Interval}";

            stopwatch = new Stopwatch();
        }

        private void RefreshDisplay()
        {
            if (!isRunning)
                return;

            var elapsed = (int)(stopwatch.Elapsed.TotalMilliseconds - offsetMilliseconds);
            var remaining = IntervalMilliseconds - elapsed % IntervalMilliseconds;

            stopwatchDisplay1.Seconds = remaining / 1000;
            stopwatchDisplay1.Milliseconds = remaining / 100 % 10;
        }

        private void timer1_Tick(object sender, EventArgs e) => RefreshDisplay();

        private void button1_Click(object sender, EventArgs e)
        {
            StartSkills();
        }

        public void StartSkills()
        {
            AffiliateSkills.ForEach(skill => skill.Start());
        }

        private void Start()
        {
            if (!Skill.Clickable || Skill.Interval <= 0)
                return;

            UpdateControls(true);
            timer.Tick -= timer1_Tick;
            timer.Tick += timer1_Tick;
            stopwatch.Restart();
        }

        private void ResetSkills()
        {
            AffiliateSkills.ForEach(skill => skill.Reset());
        }

        private void Reset()
        {
            UpdateControls(false);
            timer.Tick -= timer1_Tick;
        }

        private void UpdateControls(bool isRunning)
        {
            offsetMilliseconds = 0;

            this.isRunning = isRunning;
            textBox1.Visible = !isRunning;
            stopwatchDisplay1.Visible = isRunning;
            label2.Visible = isRunning && WanmeiTimer.Settings.Profile.PlusFlag;
            label3.Visible = isRunning && WanmeiTimer.Settings.Profile.MinusFlag;

            if (isRunning)
            {
                stopwatchDisplay1.Seconds = Skill.Interval;
            }
            else
            {
                textBox1.Text = Skill.Description;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ResetSkills();
        }

        private void label2_Click(object sender, EventArgs e) => offsetMilliseconds += WanmeiTimer.Settings.Profile.Offset;
        private void label3_Click(object sender, EventArgs e) => offsetMilliseconds -= WanmeiTimer.Settings.Profile.Offset;

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox2.Text.Trim(), out var interval);
            if (TargetSkill != null && TargetSkill.Interval != interval)
            {
                TargetSkill.Interval = interval;
                Skill.Interval = interval;
                WanmeiTimer.SettingsChanged = true;
                Reset();
            }
        }

        private void stopwatchDisplay1_DoubleClick(object sender, EventArgs e)
        {
            WanmeiTimer.Settings.Profile.MillisecondsFlag = !WanmeiTimer.Settings.Profile.MillisecondsFlag;
            WanmeiTimer.SettingsChanged = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (TargetSkill != null && TargetSkill.Description != textBox1.Text)
            {
                TargetSkill.Description = textBox1.Text;
                Skill.Description = textBox1.Text;
                WanmeiTimer.SettingsChanged = true;
            }
        }

        private Skill TargetSkill => WanmeiTimer.Settings.UserDefinedBoss.SkillDic.GetValue(Skill.Id)
            ?? WanmeiTimer.Settings.InstanceDic.GetValue(Skill.InstanceId)
            ?.BossDic.GetValue(Skill.BossId)?.SkillDic.GetValue(Skill.Id);
    }
}

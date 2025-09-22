using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TimerUtility.Models;

namespace TimerUtility
{
    public class ButtonControl : RadioButton
    {
        public ButtonType Type { get; set; } //标识是副本还是boss

        public Boss Boss { get; set; }

        public Instance Instance { get; set; }

        public Control Bosses { get; set; }

        public Control Skills { get; set; }

        public System.Windows.Forms.Timer Timer { get; set; }

        public WanmeiTimer RootForm { get; set; }

        public ButtonControl(string text)
        {
            Text = text;
            Appearance = Appearance.Button;
            Font = new Font("SimHei", 11.25F);
            Margin = new Padding(1);
            Size = new Size(93, 32);
            TextAlign = ContentAlignment.MiddleCenter;

            Click += new EventHandler(button1_Click);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Type == ButtonType.Instance && Instance?.Bosses?.Count > 0)
            {
                var originalHeight = Bosses.Height;
                Bosses.Controls.Clear();

                foreach (var boss in Instance.Bosses)
                {
                    var bossBtn = new ButtonControl(boss.Name)
                    {
                        Type = ButtonType.Boss,
                        Instance = Instance,
                        Boss = boss,
                        Skills = Skills,
                        Timer = Timer,
                        RootForm = RootForm
                    };
                    Bosses.Controls.Add(bossBtn);
                }

                var newHeight = Bosses.Height;
                //RootForm.Height += newHeight - originalHeight;

                var preInstance = WanmeiTimer.ActiveInstanceControl?.Instance;
                if (preInstance != null)
                {
                    WanmeiTimer.Settings.InstanceDic[preInstance.Id].Default = false;
                    WanmeiTimer.SettingsChanged = preInstance.Id != Instance.Id;
                }
                WanmeiTimer.ActiveInstanceControl = this;
                WanmeiTimer.Settings.InstanceDic[Instance.Id].Default = true;

                var bossButtons = Bosses.Controls.OfType<ButtonControl>();
                var defaultBossButton = bossButtons.FirstOrDefault(b => b.Boss.Id == WanmeiTimer.defaultBoss.Id) ?? bossButtons.FirstOrDefault();
                defaultBossButton.PerformClick();
            }

            if (Type == ButtonType.Boss && Boss?.Skills?.Count > 0)
            {
                var preBoss = WanmeiTimer.ActiveBossControl?.Boss;
                if (preBoss != null)
                {
                    WanmeiTimer.Settings.InstanceDic[preBoss.InstanceId].BossDic[preBoss.Id].Default = false;
                    WanmeiTimer.SettingsChanged = preBoss.Id != Boss.Id;
                }
                WanmeiTimer.ActiveBossControl = this;
                WanmeiTimer.Settings.InstanceDic[Instance.Id].BossDic[Boss.Id].Default = true;

                RootForm.flowLayoutPanel1.Controls.OfType<ButtonControl>().FirstOrDefault(i => i.Instance.Id.Equals(Instance.Id)).Checked = true;
                DrawSkills();
            }
        }

        private void DrawSkills()
        {
            WanmeiTimer.SkillControls.Clear();
            RootForm.Text = $"{Instance?.Name} - {Boss?.Name}";
            Skills.Controls.Clear();

            foreach (var skill in Boss.Skills)
            {
                skill.InstanceName = Instance.Name;
                skill.BossName = Boss.Name;
                var skillControl = new SkillControl(skill, Timer);
                Skills.Controls.Add(skillControl);
                WanmeiTimer.SkillControls.Add(skillControl);
            }

            RootForm.RegisterAllHotKeys();
        }
    }

    public enum ButtonType
    {
        Unknown,
        Instance,
        Boss
    }
}
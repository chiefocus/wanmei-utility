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

        public Timer RootForm { get; set; }

        public ButtonControl(string text)
        {
            this.Text = text;
            this.Appearance = Appearance.Button;
            this.Font = new Font("SimHei", 11.25F);
            this.Margin = new Padding(0);
            this.Size = new Size(93, 32);
            this.TextAlign = ContentAlignment.MiddleCenter;

            this.Click += new EventHandler(this.button1_Click);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Type == ButtonType.Instance && Instance?.Bosses?.Count > 0)
            {
                this.Bosses.Controls.Clear();

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
                    this.Bosses.Controls.Add(bossBtn);
                }

                this.Boss = Instance.Bosses.First();
                this.Bosses.Controls.OfType<RadioButton>().FirstOrDefault().Checked = true;
                ToDrawSkills(this.Boss);
            }

            if (Type == ButtonType.Boss && Boss?.Skills?.Count > 0)
            {
                RootForm.flowLayoutPanel1.Controls.OfType<RadioButton>()
                    .FirstOrDefault(i => i.Text.Equals(this.Instance.Name)).Checked = true;
                ToDrawSkills(this.Boss);
            }
        }

        private void ToDrawSkills(Boss boss)
        {
            RootForm.SkillControls.Clear();
            this.RootForm.Text = $"{Instance?.Name} - {Boss?.Name}";
            this.Skills.Controls.Clear();

            foreach (var skill in boss.Skills)
            {
                skill.InstanceName = Instance.Name;
                skill.BossName = boss.Name;
                var skillControl = new SkillControl(skill, Timer);
                this.Skills.Controls.Add(skillControl);
                RootForm.SkillControls.Add(skillControl);
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
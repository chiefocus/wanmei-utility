using System;
using System.Linq;
using System.Windows.Forms;
using TimerUtility.Models;

namespace TimerUtility
{
    public partial class ButtonControl : UserControl
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
            InitializeComponent();
            this.button1.Text = text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Type == ButtonType.Boss && Instance?.Bosses?.Count > 0)
            {
                this.Bosses.Controls.Clear();

                foreach (var boss in Instance.Bosses)
                {
                    var bossBtn = new ButtonControl(boss.Name)
                    {
                        Type = ButtonType.Skill,
                        Instance = Instance,
                        Boss = boss,
                        Skills = Skills,
                        Timer = Timer,
                        RootForm = RootForm
                    };
                    this.Bosses.Controls.Add(bossBtn);
                }

                this.Boss = Instance.Bosses.First();
                ToDrawSkills(this.Boss);
            }

            if (Type == ButtonType.Skill && Boss?.Skills?.Count > 0)
            {
                ToDrawSkills(Boss);
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
        Boss,
        Skill
    }
}
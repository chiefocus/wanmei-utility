using System;
using System.Linq;
using System.Windows.Forms;
using Timer.Models;

namespace Timer
{
    public partial class ButtonControl : UserControl
    {
        public DrawType Type { get; set; } //标识是副本还是boss

        public Boss Boss { get; set; }

        public Instance Instance { get; set; }

        public Control DrawBosses { get; set; }

        public Control DrawSkills { get; set; }

        public System.Windows.Forms.Timer Timer { get; set; }

        public Timer RootForm { get; set; }

        public ButtonControl()
        {
            InitializeComponent();
        }
        public ButtonControl(string text)
        {
            InitializeComponent();
            this.button1.Text = text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.button1
            if (Type == DrawType.Boss && Instance?.Bosses != null)
            {
                this.DrawBosses.Controls.Clear();

                foreach (var boss in Instance.Bosses)
                {
                    var bossBtn = new ButtonControl(boss.Name)
                    {
                        Type = DrawType.Skill,
                        Instance = Instance,
                        Boss = boss,
                        DrawSkills = DrawSkills,
                        Timer = Timer,
                        RootForm = RootForm
                    };
                    this.DrawBosses.Controls.Add(bossBtn);
                }

                this.Boss = Instance.Bosses.First();
                ToDrawSkills(this.Boss);
            }

            if (Type == DrawType.Skill && Boss?.Skills != null)
            {
                ToDrawSkills(Boss);
            }
        }
        private void ToDrawSkills(Boss boss)
        {
            RootForm.SkillControls.Clear();
            this.RootForm.Text = $"{Instance?.Name} -- {Boss?.Name}";
            this.DrawSkills.Controls.Clear();

            foreach (var skill in boss.Skills)
            {
                var skillBtn = new SkillControl(skill, Timer);
                this.DrawSkills.Controls.Add(skillBtn);

                if (skill.Flag == 0)
                {
                    RootForm.SkillControls.Add(skillBtn);
                }
            }
        }
    }

    public enum DrawType
    {
        Unknown,
        Boss,
        Skill
    }
}

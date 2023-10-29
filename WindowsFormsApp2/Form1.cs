using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using WindowsFormsApp2.Models;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public List<SkillControl> SkillControls { get; set; } = new List<SkillControl>();

        public static readonly List<Instance> _instances = new List<Instance>();

        private static void InitInstances()
        {
            var xml = File.ReadAllText("wmapp.dat");
            var xmlRoot = XElement.Parse(xml);

            var hs = xmlRoot.Elements("h");
            _instances.Clear();

            foreach (var h in hs)
            {
                var instance = new Instance()
                {
                    Name = h.Attribute("n").Value
                };

                var bs = h.Elements("b");
                foreach (var b in bs)
                {
                    var boss = new Boss()
                    {
                        InstanceName = instance.Name,
                        Name = b.Attribute("n").Value
                    };
                    var ss = b.Elements("s");
                    foreach (var s in ss)
                    {
                        var skill = new Skill()
                        {
                            InstanceName = instance.Name,
                            BossName = boss.Name,
                            Name = s.Attribute("n").Value,
                            Interval = int.Parse(s.Attribute("i").Value),
                            Description = s.Attribute("d").Value,
                            Flag = int.Parse(s.Attribute("f")?.Value ?? "1"),
                        };
                        boss.Skills.Add(skill);
                    }
                    instance.Bosses.Add(boss);
                }

                _instances.Add(instance);
            }
        }

        //public

        private void Form1_Load(object sender, EventArgs e)
        {
            InitInstances();

            this.timer1.Start();

            foreach (var instance in _instances)
            {
                var instanceBtn = new ButtonControl($"{instance.Name} >>")
                {
                    Type = DrawType.Boss,
                    DrawBosses = this.flowLayoutPanel2,
                    Instance = instance,
                    DrawSkills = this.panel2,
                    Timer = timer1,
                    RootForm = this,
                    BackColor = Color.OrangeRed
                };
                this.flowLayoutPanel1.Controls.Add(instanceBtn);
            }

            Reset();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.textBox1.Text = DateTime.Now.ToString("HH:mm:ss");
            
            this.button1.Visible = SkillControls.Any();
        }

        private void Reset()
        {
            this.panel2.Controls.Clear();
            this.flowLayoutPanel2.Controls.Clear();

            var skill = new Skill();
            var skillrow1 = new SkillControl(skill, timer1);
            var skillrow2 = new SkillControl(skill, timer1);
            var skillrow3 = new SkillControl(skill, timer1);
            var skillrow4 = new SkillControl(skill, timer1);
            var skillrow5 = new SkillControl(skill, timer1);

            this.panel2.Controls.Add(skillrow1);
            this.panel2.Controls.Add(skillrow2);
            this.panel2.Controls.Add(skillrow3);
            this.panel2.Controls.Add(skillrow4);
            this.panel2.Controls.Add(skillrow5);

            var instance = _instances.First();
            foreach (var boss in instance.Bosses)
            {
                var bossBtn = new ButtonControl(boss.Name)
                {
                    Type = DrawType.Skill,
                    DrawBosses = this.flowLayoutPanel2,
                    Instance = instance,
                    Boss = boss,
                    DrawSkills = this.panel2,
                    Timer = this.timer1,
                    RootForm = this
                };
                this.flowLayoutPanel2.Controls.Add(bossBtn);
            }

            this.Text = "计时器v2";
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var skill in SkillControls)
            {
                skill.OnClick(skill);
            }
        }
    }
}

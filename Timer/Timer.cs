using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using Timer.Models;

namespace Timer
{
    public partial class Timer : Form
    {
        public Timer()
        {
            InitializeComponent();
            Rectangle res = Screen.PrimaryScreen.Bounds;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(res.Right - this.Width, res.Height / 2);
        }

        private static string _instancesXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><r><h n=\"黄3\"><b n=\"小铁\"><s n=\"破甲\" i=\"20\" d=\"320 仙256 魔272\"/><s n=\"巨力\" i=\"30\" d=\"200 仙160 魔170\"/><s n=\"大群\" i=\"20\" d=\"120 仙96 魔102\"/></b><b n=\"子纯\"><s n=\"封印\" i=\"20\" d=\"450 仙360 魔382.5\"/><s n=\"群晕\" i=\"20\" d=\"300 仙240 魔255\"/><s n=\"流血\" i=\"20\" d=\"150 仙120 魔127.5\"/></b><b n=\"仓力\"><s n=\"流血\" i=\"30\" d=\"450 仙360 魔382.5\"/><s n=\"群晕\" i=\"20\" d=\"250 仙200 魔212.5\"/><s n=\"大群1\" i=\"20\" d=\"150 仙120 魔127.5\"/><s n=\"大群2\" i=\"20\" d=\"125\"/></b><b n=\"天地\"><s n=\"驱逐\" i=\"60\" d=\"540 仙432 魔459\"/><s n=\"吸元\" i=\"45\" d=\"420 仙336 魔357\"/><s n=\"木毒\" i=\"35\" d=\"300 仙240 魔255\"/><s n=\"群晕\" i=\"30\" d=\"180 仙144 魔153\"/><s n=\"大群\" i=\"55\" d=\"150 仙120 魔127.5\"/></b></h><h n=\"黄2\"><b n=\"神武罗\"><s n=\"固伤\" i=\"30\" d=\"开打计时\" f=\"0\"/></b><b n=\"猴子\"><s n=\"扇形大\" i=\"45\" d=\"开打计时\" f=\"0\"/><s n=\"巨力\" i=\"60\" d=\"开打计时\" f=\"0\"/></b><b n=\"狗\"><s n=\"群晕\" i=\"0\" d=\"每掉25%血群晕\"/><s n=\"吸蓝\" i=\"20\" d=\"开打计时\" f=\"0\"/></b><b n=\"十方\"><s n=\"流血\" i=\"45\" d=\"250 仙200 魔212.5\"/><s n=\"群减血\" i=\"30\" d=\"开打计时\" f=\"0\"/></b><b n=\"罗刹\"><s n=\"单体封印\" i=\"45\" d=\"50%血开始计时\"/><s n=\"单体落雷\" i=\"35\" d=\"开打计时\" f=\"0\"/><s n=\"金系群攻\" i=\"15\" d=\"开打计时\" f=\"0\"/></b></h></r>";

        public List<SkillControl> SkillControls { get; set; } = new List<SkillControl>();

        public static readonly List<Instance> _instances = new List<Instance>();

        private static void InitInstances()
        {
            if (File.Exists("wmapp.dat"))
            {
                _instancesXml = File.ReadAllText("wmapp.dat");
            }

            var xmlRoot = XElement.Parse(_instancesXml);
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
                    boss.Skills.Reverse();

                    instance.Bosses.Add(boss);
                }

                _instances.Add(instance);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.timer1.Start();

            this.BeginInvoke(new Action(() =>
            {
                InitInstances();

                foreach (var instance in _instances)
                {
                    var instanceBtn = new ButtonControl($"{instance.Name}")
                    {
                        Type = DrawType.Boss,
                        DrawBosses = this.flowLayoutPanel2,
                        Instance = instance,
                        DrawSkills = this.panel2,
                        Timer = timer1,
                        RootForm = this,
                        ForeColor = Color.DarkMagenta,
                    };
                    this.flowLayoutPanel1.Controls.Add(instanceBtn);
                }

                Reset();
            }));
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

            this.Text = "计时器 - HL";
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

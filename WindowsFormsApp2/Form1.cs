using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Windows.Forms;
using System.Xml.Linq;
using WindowsFormsApp2.Models;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        readonly KeyboardHook hook = new KeyboardHook();

        public Form1()
        {
            InitializeComponent();

            // register the event that is fired after the key press.
            hook.KeyPressed += new EventHandler<KeyPressedEventArgs>(hook_KeyPressed);
            // register the control + alt + F12 combination as hot key.
            //hook.RegisterHotKey(WindowsFormsApp2.ModifierKeys.Control | WindowsFormsApp2.ModifierKeys.Alt, Keys.F12);
            hook.RegisterHotKey(WindowsFormsApp2.ModifierKeys.None, Keys.NumPad1);
        }

        private void hook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            // show the keys pressed in a label.
            this.Text = e.Modifier.ToString() + " + " + e.Key.ToString();
        }

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

            //this.panel3.Visible = false;
            //this.panel3.Enabled = false;
            //this.panel3.Height = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.textBox1.Text = DateTime.Now.ToString("HH:mm:ss");
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

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml.Linq;
using TimerUtility.Models;

namespace TimerUtility
{
    public partial class Timer : Form
    {
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private const uint MOD_ALT = 0x0001;

        public Timer()
        {
            InitializeComponent();
            Rectangle res = Screen.PrimaryScreen.Bounds;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(res.Right - this.Width, res.Height / 2 - 200);

            Application.ApplicationExit += OnAppExit;
        }

        private static readonly string DataFile = "wmapp.dat";
        private static string InstancesXml = "<r><u p=\"0\" m=\"0\" ms=\"0\" o=\"1000\" s=\"0\"/><h n=\"黄3\"><b n=\"圣母\"><s n=\"减攻速\" i=\"30\" d=\"280 仙224 魔238\"/><s n=\"减吟唱\" i=\"20\" d=\"175 仙140 魔148.75\"/><s n=\"群攻\" i=\"20\" d=\"175 仙140 魔148.75\"/></b><b n=\"小铁\"><s n=\"破甲\" i=\"20\" d=\"320 仙256 魔272\"/><s n=\"巨力\" i=\"30\" d=\"200 仙160 魔170\"/><s n=\"大群\" i=\"20\" d=\"120 仙96 魔102\"/></b><b n=\"子纯\"><s n=\"封印\" i=\"20\" d=\"450 仙360 魔382.5\"/><s n=\"群晕\" i=\"20\" d=\"300 仙240 魔255\"/><s n=\"流血\" i=\"20\" d=\"150 仙120 魔127.5\"/></b><b n=\"仓力\"><s n=\"流血\" i=\"30\" d=\"450 仙360 魔382.5\"/><s n=\"群晕\" i=\"20\" d=\"250 仙200 魔212.5\"/><s n=\"大群1\" i=\"20\" d=\"150 仙120 魔127.5\"/><s n=\"大群2\" i=\"20\" d=\"125\"/></b><b n=\"天地\"><s n=\"驱逐\" i=\"60\" d=\"540 仙432 魔459\"/><s n=\"吸元\" i=\"45\" d=\"420 仙336 魔357\"/><s n=\"木毒\" i=\"35\" d=\"300 仙240 魔255\"/><s n=\"群晕\" i=\"30\" d=\"180 仙144 魔153\"/><s n=\"大群\" i=\"55\" d=\"150 仙120 魔127.5\"/></b></h><h n=\"黄2\"><b n=\"神武罗\"><s n=\"固伤\" i=\"30\" d=\"开打计时\" f=\"0\"/></b><b n=\"猴子\"><s n=\"扇形大\" i=\"45\" d=\"开打计时\" f=\"0\"/><s n=\"巨力\" i=\"60\" d=\"开打计时\" f=\"0\"/></b><b n=\"狗\"><s n=\"群晕\" i=\"0\" d=\"每掉25%血群晕\"/><s n=\"吸蓝\" i=\"20\" d=\"开打计时\" f=\"0\"/></b><b n=\"十方\"><s n=\"流血\" i=\"45\" d=\"250 仙200 魔212.5\"/><s n=\"群减血\" i=\"30\" d=\"开打计时\" f=\"0\"/></b><b n=\"罗刹\"><s n=\"单体封印\" i=\"45\" d=\"50%血开始计时\"/><s n=\"单体落雷\" i=\"35\" d=\"开打计时\" f=\"0\"/><s n=\"金系群攻\" i=\"15\" d=\"开打计时\" f=\"0\"/></b></h><h n=\"黄1\"><b n=\"鼓神\"><s n=\"封印\" i=\"0\" d=\"身上有减唱可立马T封印\" c=\"0\"/><s n=\"全屏攻击\" i=\"600\" d=\"开打计时，每600秒\" f=\"0\"/><s n=\"近战群晕\" i=\"20\" d=\"开打计时\" f=\"0\"/></b><b n=\"古蛇\"><s n=\"单大毒\" i=\"20\" d=\"开打计时\" f=\"0\"/><s n=\"群小毒\" i=\"15\" d=\"开打计时\" f=\"0\"/></b><b n=\"圣金甲\"><s n=\"6K固伤\" i=\"60\" d=\"开打计时\" f=\"0\"/><s n=\"乱仇恨\" i=\"15\" d=\"开打计时\" f=\"0\"/></b><b n=\"怒目\"><s n=\"狂暴\" i=\"30\" d=\"50%血开始计时\"/><s n=\"近战群晕\" i=\"35\" d=\"开打计时\" f=\"0\"/><s n=\"扇形攻击\" i=\"15\" d=\"开打计时\" f=\"0\"/></b></h><udb n=\"自定义\"><s n=\"计时1\" i=\"15\" d=\"\" f=\"0\"/><s n=\"计时2\" i=\"20\" d=\"\" f=\"0\"/><s n=\"计时3\" i=\"30\" d=\"\" f=\"0\"/><s n=\"计时4\" i=\"45\" d=\"\" f=\"0\"/><s n=\"计时5\" i=\"60\" d=\"\" f=\"0\"/></udb></r>";

        public List<SkillControl> SkillControls { get; set; } = new List<SkillControl>();

        public static Settings Settings = new Settings();

        private static void InitInstances()
        {
            try
            {
                if (File.Exists(DataFile))
                {
                    InstancesXml = File.ReadAllText(DataFile);
                }

                var xmlRoot = XElement.Parse(InstancesXml);
                Settings = xmlRoot.Deserialize<Settings>();

                foreach (var instance in Settings.Instances)
                {
                    foreach (var boss in instance.Bosses)
                    {
                        int vkIndex = 0;
                        foreach (var skill in boss.Skills)
                        {
                            int key = 49 + vkIndex;
                            skill.Id = key;
                            skill.VirtualKey = (uint)key;
                            vkIndex++;
                        }
                        boss.Skills.Reverse();
                    }
                }

                int vki = 0;
                foreach (var skill in Settings.UserDefinedBoss.Skills)
                {
                    int key = 49 + vki;
                    skill.Id = key;
                    skill.VirtualKey = (uint)key;
                    vki++;
                }
                Settings.UserDefinedBoss.Skills.Reverse();
            }
            catch { }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.timer1.Start();

            this.BeginInvoke(new Action(() =>
            {
                InitInstances();

                foreach (var instance in Settings.Instances)
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

                LoadDefaultBoss(Settings.Instances.First(), Settings.Instances.First().Bosses.First());
            }));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.textBox1.Text = DateTime.Now.ToString("HH:mm:ss");
            this.button1.Visible = SkillControls.Any(s => s.Skill.Flag == 0);
        }

        private void LoadDefaultBoss(Instance defaultInstance, Boss defaultBoss)
        {
            this.panel2.Controls.Clear();
            this.SkillControls.Clear();

            foreach (var skill in defaultBoss.Skills)
            {
                skill.InstanceName = defaultInstance?.Name ?? Settings.UserDefinedBoss?.Name;
                skill.BossName = defaultBoss.Name;
                var skillControl = new SkillControl(skill, timer1);
                this.panel2.Controls.Add(skillControl);
                this.SkillControls.Add(skillControl);
            }
            this.Text = $"{Settings.UserDefinedBoss?.Name} - 后浪专用";

            RegisterAllHotKeys();

            if (defaultInstance == null)
            {
                return;
            }

            this.Text = $"{defaultInstance?.Name} - {defaultBoss?.Name}";
            this.flowLayoutPanel2.Controls.Clear();

            foreach (var boss in defaultInstance.Bosses)
            {
                boss.InstanceName = defaultInstance.Name;
                var bossBtn = new ButtonControl(boss.Name)
                {
                    Type = DrawType.Skill,
                    DrawBosses = this.flowLayoutPanel2,
                    Instance = defaultInstance,
                    Boss = boss,
                    DrawSkills = this.panel2,
                    Timer = this.timer1,
                    RootForm = this
                };
                this.flowLayoutPanel2.Controls.Add(bossBtn);
                this.flowLayoutPanel2.Controls[0].Focus();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            LoadDefaultBoss(null, Settings.UserDefinedBoss);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var skillControls = SkillControls
                .Where(s => s.Skill.Flag == 0);

            foreach (var skill in skillControls)
            {
                skill.OnClick(skill);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            var keys = SkillControls.Select(s => s.Skill.Id);
            UnregisterAllHotKeys(keys);
            base.OnFormClosing(e);
        }

        private void OnAppExit(object sender, EventArgs e)
        {
            var keys = SkillControls.Select(s => s.Skill.Id);
            UnregisterAllHotKeys(keys);
            SaveSettings();
        }

        private void SaveSettings()
        {
            if (Settings.Profile.Preservable)
            {
                foreach (var instance in Settings.Instances)
                {
                    foreach (var boss in instance.Bosses)
                    {
                        boss.Skills.Reverse();
                    }
                }
                Settings.UserDefinedBoss.Skills.Reverse();
                var settingsXml = Settings.SerializeToString();
                File.WriteAllText(DataFile, settingsXml);
            }
        }

        private void UnregisterAllHotKeys(IEnumerable<int> keys)
        {
            foreach (var key in keys)
            {
                try
                {
                    UnregisterHotKey(this.Handle, key);
                }
                catch { }
            }
        }

        public void RegisterAllHotKeys()
        {
            var keys = SkillControls.Select(s => s.Skill.Id);
            UnregisterAllHotKeys(keys);
            foreach (var key in keys)
            {
                try
                {
                    RegisterHotKey(this.Handle, key, MOD_ALT, (uint)key);
                }
                catch { }
            }
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;
            if (m.Msg == WM_HOTKEY)
            {
                int id = m.WParam.ToInt32();
                var skillControl = SkillControls.FirstOrDefault(s => s.Skill.Id == id);
                skillControl?.button1.PerformClick();
            }
            base.WndProc(ref m);
        }
    }
}

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
        private static string InstancesXml = "<r><u p=\"1\" m=\"1\" ms=\"0\" o=\"1000\" s=\"1\" k=\"1\" /><h n=\"黄3\" df=\"1\"><b n=\"圣母\" df=\"1\"><s n=\"减攻速\" d=\"280 仙224 魔235.2\" i=\"30\" /><s n=\"减吟唱\" d=\"175 仙140 魔147\" i=\"20\" /><s n=\"群攻\" d=\"175 仙140 魔147\" i=\"20\" /></b><b n=\"小铁\"><s n=\"破甲\" d=\"320 仙256 魔268.8\" i=\"20\" /><s n=\"巨力\" d=\"200 仙160 魔168\" i=\"30\" /><s n=\"大群\" d=\"120 仙96 魔100.8\" i=\"20\" /></b><b n=\"子纯\"><s n=\"封印\" d=\"450 仙360 魔378\" i=\"20\" /><s n=\"群晕\" d=\"300 仙240 魔252\" i=\"20\" /><s n=\"流血\" d=\"150 仙120 魔126\" i=\"20\" /></b><b n=\"仓力\"><s n=\"流血\" d=\"450 仙360 魔378\" i=\"30\" /><s n=\"群晕\" d=\"250 仙200 魔210\" i=\"20\" /><s n=\"清仇恨\" d=\"250 仙200 魔210\" i=\"90\" /><s n=\"大群1\" d=\"150 仙120 魔126\" i=\"20\" /><s n=\"大群2\" d=\"125 仙100 魔105\" i=\"20\" /></b><b n=\"天地\"><s n=\"驱逐\" d=\"540 仙432 魔453.6\" i=\"60\" /><s n=\"吸元\" d=\"420 仙336 魔352.8\" i=\"45\" /><s n=\"木毒\" d=\"300 仙240 魔252\" i=\"35\" /><s n=\"群晕\" d=\"180 仙144 魔151.2\" i=\"30\" /><s n=\"大群\" d=\"150 仙120 魔126\" i=\"55\" /></b></h><h n=\"黄2\"><b n=\"神武罗\"><s n=\"固伤\" d=\"开打计时\" i=\"30\" f=\"0\" /></b><b n=\"猴子\"><s n=\"扇形大\" d=\"开打计时\" i=\"45\" f=\"0\" /><s n=\"巨力\" d=\"开打计时\" i=\"60\" f=\"0\" /></b><b n=\"狗\"><s n=\"群晕\" d=\"每掉25%血群晕\" i=\"0\" c=\"0\" /><s n=\"吸蓝\" d=\"开打计时\" i=\"20\" f=\"0\" /></b><b n=\"十方\"><s n=\"流血\" d=\"250 仙200 魔210\" i=\"45\" /><s n=\"群减血\" d=\"开打计时\" i=\"30\" f=\"0\" /></b><b n=\"罗刹\"><s n=\"单体封印\" d=\"50%血开始计时\" i=\"45\" /><s n=\"单体落雷\" d=\"开打计时\" i=\"35\" f=\"0\" /><s n=\"金系群攻\" d=\"开打计时\" i=\"15\" f=\"0\" /></b></h><h n=\"黄1\"><b n=\"鼓神\"><s n=\"封印\" d=\"身上有减唱可立马T封印\" i=\"0\" c=\"0\" /><s n=\"全屏攻击\" d=\"开打计时，每600秒\" i=\"600\" f=\"0\" /><s n=\"近战群晕\" d=\"开打计时\" i=\"20\" f=\"0\" /></b><b n=\"古蛇\"><s n=\"单大毒\" d=\"开打计时\" i=\"20\" f=\"0\" /><s n=\"群小毒\" d=\"开打计时\" i=\"15\" f=\"0\" /></b><b n=\"圣金甲\"><s n=\"固伤6000\" d=\"开打计时\" i=\"60\" f=\"0\" /><s n=\"乱仇恨\" d=\"开打计时\" i=\"15\" f=\"0\" /></b><b n=\"怒目\"><s n=\"狂暴\" d=\"50%血开始计时\" i=\"30\" /><s n=\"近战群晕\" d=\"开打计时\" i=\"35\" f=\"0\" /><s n=\"扇形攻击\" d=\"开打计时\" i=\"15\" f=\"0\" /></b></h><udb n=\"自定义\"><s n=\"计时1\" d=\"\" i=\"15\" f=\"0\" /><s n=\"计时2\" d=\"\" i=\"20\" f=\"0\" /><s n=\"计时3\" d=\"\" i=\"30\" f=\"0\" /><s n=\"计时4\" d=\"\" i=\"45\" f=\"0\" /><s n=\"计时5\" d=\"\" i=\"60\" f=\"0\" /></udb></r>";

        public static List<SkillControl> SkillControls { get; set; } = new List<SkillControl>();

        public static Settings Settings = new Settings();
        public static bool SettingsChanged = false;

        private static Instance defaultInstance;
        private static Boss defaultBoss;

        private static void InitInstances()
        {
            try
            {
                if (File.Exists(DataFile))
                {
                    InstancesXml = File.ReadAllText(DataFile);
                }

                var xmlRoot = XElement.Parse(InstancesXml);

                {
                    Settings = xmlRoot.Deserialize<Settings>();
                    defaultInstance = Settings.Instances.FirstOrDefault(i => i.Default) ?? Settings.Instances.FirstOrDefault();
                    defaultBoss = defaultInstance.Bosses.FirstOrDefault(b => b.Default) ?? defaultInstance.Bosses.FirstOrDefault();
                }

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
                        Type = ButtonType.Instance,
                        Bosses = this.flowLayoutPanel2,
                        Instance = instance,
                        Skills = this.panel2,
                        Timer = timer1,
                        RootForm = this,
                        ForeColor = Color.DarkMagenta,
                    };
                    this.flowLayoutPanel1.Controls.Add(instanceBtn);
                }

                LoadDefaultBoss(defaultInstance, defaultBoss);
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
            SkillControls.Clear();

            foreach (var skill in defaultBoss.Skills)
            {
                skill.InstanceName = defaultInstance?.Name ?? Settings.UserDefinedBoss?.Name;
                skill.BossName = defaultBoss.Name;
                var skillControl = new SkillControl(skill, timer1);
                this.panel2.Controls.Add(skillControl);
                SkillControls.Add(skillControl);
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
                    Type = ButtonType.Boss,
                    Bosses = this.flowLayoutPanel2,
                    Instance = defaultInstance,
                    Boss = boss,
                    Skills = this.panel2,
                    Timer = this.timer1,
                    RootForm = this
                };
                this.flowLayoutPanel2.Controls.Add(bossBtn);
            }
            this.flowLayoutPanel1.Controls.OfType<RadioButton>()
                .FirstOrDefault(b => defaultInstance.Name.Equals(b.Text)).Checked = true;
            this.flowLayoutPanel2.Controls.OfType<RadioButton>()
                .FirstOrDefault(b => defaultBoss.Name.Equals(b.Text)).Checked = true;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            LoadDefaultBoss(null, Settings.UserDefinedBoss);
            foreach (var control in this.flowLayoutPanel1.Controls)
            {
                if (control is RadioButton rb)
                    rb.Checked = false;
            }
            foreach (var control in this.flowLayoutPanel2.Controls)
            {
                if (control is RadioButton rb)
                    rb.Checked = false;
            }
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
            if (Settings.Profile.Preservable && SettingsChanged)
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
            if (!Settings.Profile.Shortcutable)
                return;

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
            if (!Settings.Profile.Shortcutable)
                return;

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

        const int WM_HOTKEY = 0x0312;
        const int WM_NCLBUTTONDBLCLK = 0x00A3;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_HOTKEY && Settings.Profile.Shortcutable)
            {
                int id = m.WParam.ToInt32();
                var skillControl = SkillControls.FirstOrDefault(s => s.Skill.Id == id);
                skillControl?.button1.PerformClick();
            }

            if (m.Msg == WM_NCLBUTTONDBLCLK)
            {
                const int HTCAPTION = 2;
                int hitTest = m.WParam.ToInt32();
                if (hitTest == HTCAPTION)
                {
                    MessageBox.Show("兽丶神作品，特供后浪！！！", "完美计时器", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            base.WndProc(ref m);
        }

        private void Timer_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                Timer.Settings.Profile.Preservable = true;
            }
        }
    }
}

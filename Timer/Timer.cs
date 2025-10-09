using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using TimerUtility.Models;

namespace TimerUtility
{
    public partial class WanmeiTimer : Form
    {
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private const uint MOD_ALT = 0x0001;

        public WanmeiTimer()
        {
            InitializeComponent();
            var exePath = Assembly.GetEntryAssembly().Location;
            Icon = Icon.ExtractAssociatedIcon(exePath);
            MaximumSize = new Size(785, 515);
            MinimumSize = new Size(312, 310);
            Application.ApplicationExit += OnAppExit;
        }

        private static readonly string DataFile = "wmapp.dat";
        private static readonly string InstancesXml = "<r><u p=\"1\" m=\"1\" ms=\"0\" o=\"1000\" s=\"1\" k=\"1\"/><h n=\"黄3\"><b n=\"圣母\" df=\"1\"><s n=\"减攻速\" d=\"280 仙224 魔235.2\" i=\"30\"/><s n=\"减吟唱\" d=\"175 仙140 魔147\" i=\"20\" a=\"群攻\"/><s n=\"群攻\" d=\"175 仙140 魔147\" i=\"20\" a=\"减吟唱\"/></b><b n=\"小铁\"><s n=\"破甲\" d=\"320 仙256 魔268.8\" i=\"20\"/><s n=\"巨力\" d=\"200 仙160 魔168\" i=\"30\"/><s n=\"大群\" d=\"120 仙96 魔100.8\" i=\"20\"/></b><b n=\"子纯\"><s n=\"封印\" d=\"450 仙360 魔378\" i=\"20\"/><s n=\"群晕\" d=\"300 仙240 魔252\" i=\"20\"/><s n=\"流血\" d=\"150 仙120 魔126\" i=\"20\"/></b><b n=\"仓力\"><s n=\"流血\" d=\"450 仙360 魔378\" i=\"30\"/><s n=\"群晕\" d=\"250 仙200 魔210\" i=\"20\" a=\"清仇恨\"/><s n=\"清仇恨\" d=\"250 仙200 魔210\" i=\"90\" a=\"群晕\"/><s n=\"大群1\" d=\"150 仙120 魔126\" i=\"20\"/><s n=\"大群2\" d=\"125 仙100 魔105\" i=\"20\"/></b><b n=\"天地\"><s n=\"驱逐\" d=\"540 仙432 魔453.6\" i=\"60\"/><s n=\"吸元\" d=\"420 仙336 魔352.8\" i=\"45\"/><s n=\"木毒\" d=\"300 仙240 魔252\" i=\"35\"/><s n=\"群晕\" d=\"180 仙144 魔151.2\" i=\"30\"/><s n=\"大群\" d=\"150 仙120 魔126\" i=\"55\"/></b></h><h n=\"黄2\"><b n=\"神武罗\"><s n=\"固伤\" d=\"开打计时\" i=\"30\" f=\"0\"/></b><b n=\"猴子\"><s n=\"扇形大\" d=\"开打计时\" i=\"45\" f=\"0\"/><s n=\"巨力\" d=\"开打计时\" i=\"60\" f=\"0\"/></b><b n=\"狗\"><s n=\"群晕\" d=\"每掉25%血群晕\" i=\"0\" c=\"0\"/><s n=\"吸蓝\" d=\"开打计时\" i=\"20\" f=\"0\"/></b><b n=\"十方\"><s n=\"流血\" d=\"250 仙200 魔210\" i=\"45\"/><s n=\"群减血\" d=\"开打计时\" i=\"30\" f=\"0\"/></b><b n=\"罗刹\"><s n=\"单体封印\" d=\"50%血开始计时\" i=\"45\"/><s n=\"单体落雷\" d=\"开打计时\" i=\"35\" f=\"0\"/><s n=\"金系群攻\" d=\"开打计时\" i=\"15\" f=\"0\"/></b></h><h n=\"黄1\"><b n=\"鼓神\"><s n=\"封印\" d=\"身上有减唱可立马T封印\" i=\"0\" c=\"0\"/><s n=\"全屏攻击\" d=\"开打计时，每600秒\" i=\"600\" f=\"0\"/><s n=\"近战群晕\" d=\"开打计时\" i=\"20\" f=\"0\"/></b><b n=\"古蛇\"><s n=\"单大毒\" d=\"开打计时\" i=\"20\" f=\"0\"/><s n=\"群小毒\" d=\"开打计时\" i=\"15\" f=\"0\"/></b><b n=\"圣金甲\"><s n=\"6000固伤\" d=\"开打计时\" i=\"60\" f=\"0\"/><s n=\"乱仇恨\" d=\"开打计时\" i=\"15\" f=\"0\"/></b><b n=\"怒目\"><s n=\"狂暴\" d=\"50%血开始计时\" i=\"30\"/><s n=\"近战群晕\" d=\"开打计时\" i=\"35\" f=\"0\"/><s n=\"扇形攻击\" d=\"开打计时\" i=\"15\" f=\"0\"/></b></h><udb n=\"自定义\"><s n=\"计时1\" d=\"\" i=\"15\" f=\"0\"/><s n=\"计时2\" d=\"\" i=\"20\" f=\"0\"/><s n=\"计时3\" d=\"\" i=\"30\" f=\"0\"/><s n=\"计时4\" d=\"\" i=\"45\" f=\"0\"/><s n=\"计时5\" d=\"\" i=\"60\" f=\"0\"/></udb></r>";

        public static Config Settings = new Config();
        public static bool SettingsChanged = false;

        private List<SkillControl> skillControls = new List<SkillControl>();
        private Boss activeBoss;

        private void InitInstances(string file)
        {
            try
            {
                var content = File.ReadAllText(file);
                var xmlRoot = XElement.Parse(content);
                Settings = xmlRoot.Deserialize<Config>();
            }
            catch
            {
                var xmlRoot = XElement.Parse(InstancesXml);
                Settings = xmlRoot.Deserialize<Config>();
            }

            foreach (var instance in Settings.Instances)
            {
                foreach (var boss in instance.Bosses)
                {
                    boss.InstanceId = instance.Id;
                    boss.InstanceName = instance.Name;
                    int vkIndex = 0;
                    foreach (var skill in boss.Skills)
                    {
                        skill.InstanceId = instance.Id;
                        skill.BossId = boss.Id;

                        int key = 49 + vkIndex;
                        skill.Key = key;
                        skill.VirtualKey = (uint)key;
                        vkIndex++;
                    }

                    if (boss.Default)
                    {
                        activeBoss = boss;
                    }

                    boss.Skills.Reverse();
                }

                int vki = 0;
                foreach (var skill in Settings.UserDefinedBoss.Skills)
                {
                    int key = 49 + vki;
                    skill.Key = key;
                    skill.VirtualKey = (uint)key;
                    vki++;
                }
                Settings.UserDefinedBoss.Skills.Reverse();
            }
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await Init(DataFile);
        }

        private async Task Init(string file)
        {
            await Task.Run(() => { InitInstances(file); });

            timer1.Start();

            if (Settings.Preference.Location != null)
                Location = Settings.Preference.Location.Value;

            if (Settings.Preference.ClientSize != null)
                ClientSize = Settings.Preference.ClientSize.Value;

            LoadInstances();
            activeBoss = activeBoss ?? Settings.Instances.FirstOrDefault().Bosses.FirstOrDefault();
            var activeInstance = Settings.InstanceDic.GetValue(activeBoss.InstanceId);
            LoadBosses(activeInstance);
            activeBoss = null;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox1.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void LoadInstances()
        {
            flowLayoutPanel1.Controls.Clear();
            foreach (var instance in Settings.Instances)
            {
                var instanceControl = new ButtonControl<Instance>(instance, instance.Name)
                {
                    ForeColor = Color.DarkMagenta,
                };
                instanceControl.ButtonClicked += i => LoadBosses(i);
                flowLayoutPanel1.Controls.Add(instanceControl);
            }
        }

        public void LoadBosses(Instance instance)
        {
            var controls = new List<ButtonControl<Boss>>(flowLayoutPanel2.Controls.Cast<ButtonControl<Boss>>());
            foreach (var control in controls)
            {
                control.Dispose();
            }
            flowLayoutPanel2.Controls.Clear();
            foreach (var boss in instance.Bosses)
            {
                var bossControl = new ButtonControl<Boss>(boss, boss.Name);
                bossControl.ButtonClicked += b => LoadSkills(b);
                flowLayoutPanel2.Controls.Add(bossControl);
            }
            var loadingBoss = activeBoss ?? instance.Bosses.FirstOrDefault();
            LoadSkills(loadingBoss);
        }

        public void LoadSkills(Boss boss)
        {
            UnregisterAllHotKeys();

            Text = string.IsNullOrWhiteSpace(boss.InstanceName) ? boss.Name : $"{boss.InstanceName} - {boss.Name}";
            btnReset.Checked = boss.Id == Settings.UserDefinedBoss.Id;
            foreach (var control in skillControls)
            {
                control.Dispose();
            }
            skillControls.Clear();
            panel2.Controls.Clear();
            button1.Visible = boss.Skills.Any(s => s.Flag);

            foreach (var skill in boss.Skills)
            {
                skill.InstanceName = boss.InstanceName;
                skill.BossName = boss.Name;
                var skillControl = new SkillControl(skill, timer1);
                panel2.Controls.Add(skillControl);
                skillControls.Add(skillControl);
            }

            var checkedBoss = flowLayoutPanel2.Controls.OfType<ButtonControl<Boss>>().FirstOrDefault(i => i.Data.Id == boss.Id);
            if (checkedBoss != null) { checkedBoss.Checked = true; }

            var checkedInstance = flowLayoutPanel1.Controls.OfType<ButtonControl<Instance>>().FirstOrDefault(i => i.Data.Id == boss.InstanceId);
            if (checkedInstance != null) { checkedInstance.Checked = true; }

            foreach (var skillControl in skillControls)
            {
                skillControl.AffiliateSkills.Add(skillControl);
                if (!string.IsNullOrEmpty(skillControl.Skill.Affiliate))
                {
                    foreach (var affiliate in skillControl.Skill.Affiliate.Split(','))
                    {
                        var affiliateControl = skillControls.FirstOrDefault(s => s.Skill.Name.Equals(affiliate));
                        skillControl.AffiliateSkills.Add(affiliateControl);
                    }
                }
            }

            RegisterAllHotKeys();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            LoadSkills(Settings.UserDefinedBoss);

            var checkedInstance = flowLayoutPanel1.Controls.OfType<ButtonControl<Instance>>().Where(c => c.Checked).FirstOrDefault();
            if (checkedInstance != null)
            {
                checkedInstance.Checked = false;
            }

            var checkedBoss = flowLayoutPanel2.Controls.OfType<ButtonControl<Boss>>().Where(c => c.Checked).FirstOrDefault();
            if (checkedBoss != null)
            {
                checkedBoss.Checked = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var skillControls = this.skillControls.Where(s => s.Skill.Flag);
            foreach (var skill in skillControls)
            {
                skill.Start();
            }
        }

        private void OnAppExit(object sender, EventArgs e)
        {
            UnregisterAllHotKeys();
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

        private void UnregisterAllHotKeys()
        {
            if (!Settings.Profile.Shortcutable) return;

            foreach (var skillControl in skillControls)
            {
                try
                {
                    UnregisterHotKey(Handle, skillControl.Skill.Key);
                }
                catch { }
            }
        }

        public void RegisterAllHotKeys()
        {
            if (!Settings.Profile.Shortcutable) return;

            foreach (var skillControl in skillControls)
            {
                try
                {
                    var key = skillControl.Skill.Key;
                    RegisterHotKey(Handle, key, MOD_ALT, (uint)key);
                }
                catch { }
            }
        }

        private const int WM_HOTKEY = 0x0312;
        private const int WM_NCLBUTTONDBLCLK = 0x00A3;
        private const int WM_EXITSIZEMOVE = 0x0232;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_HOTKEY && Settings.Profile.Shortcutable)
            {
                int id = m.WParam.ToInt32();
                var skillControl = skillControls.FirstOrDefault(s => s.Skill.Key == id);
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

            if (m.Msg == WM_EXITSIZEMOVE)
            {
                HandleMoveOrResizeEnd();
            }

            base.WndProc(ref m);
        }

        private void HandleMoveOrResizeEnd()
        {
            Settings.Preference.Location = Location;
            Settings.Preference.ClientSize = ClientSize;
            SettingsChanged = true;
        }

        private async void panel1_DoubleClick(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "所有文件 (*.*)|*.*",
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var file = openFileDialog.FileName;
                await Init(file);
                SettingsChanged = true;
            }
        }
    }
}

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
            MaximumSize = new Size(1000, 800);
            MinimumSize = new Size(308, 310);
            Application.ApplicationExit += OnAppExit;
        }

        private static readonly string DataFile = "wmapp.dat";
        private static readonly string InstancesXml = Properties.Resources.wmappdata;

        public static Config Settings = new Config();
        public static bool SettingsChanged = false;

        private readonly List<SkillControl> skillControls = new List<SkillControl>();
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
                }

                int vki = 0;
                foreach (var skill in Settings.UserDefinedBoss.Skills)
                {
                    int key = 49 + vki;
                    skill.Key = key;
                    skill.VirtualKey = (uint)key;
                    vki++;
                }
            }
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            timer.Start();
            await Init(DataFile);
        }

        private void ResizeForm()
        {
            var totalHeight = 0;
            foreach (Control c in this.Controls)
            {
                if (c.Visible)
                    totalHeight += c.Height + c.Margin.Horizontal;
            }
            var borderHeight = this.Height - this.ClientSize.Height;
            this.Height = totalHeight + borderHeight;
        }

        private async Task Init(string file)
        {
            await Task.Run(() => { InitInstances(file); });

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

        private void timer_Tick(object sender, EventArgs e)
        {
            nowLabel.Text = DateTime.Now.ToString("HH:mm:ss yyyy-M-d ddd");
        }

        private void LoadInstances()
        {
            instancePanel.Controls.Clear();
            foreach (var instance in Settings.Instances)
            {
                var instanceControl = new ButtonControl<Instance>(instance, instance.Name)
                {
                    ForeColor = Color.DarkMagenta,
                };
                instanceControl.ButtonClicked += i => LoadBosses(i);
                instancePanel.Controls.Add(instanceControl);
            }
        }

        public void LoadBosses(Instance instance)
        {
            var controls = new List<ButtonControl<Boss>>(bossPanel.Controls.Cast<ButtonControl<Boss>>());
            foreach (var control in controls)
            {
                control.Dispose();
            }
            bossPanel.Controls.Clear();
            foreach (var boss in instance.Bosses)
            {
                var bossControl = new ButtonControl<Boss>(boss, boss.Name);
                bossControl.ButtonClicked += b => LoadSkills(b);
                bossPanel.Controls.Add(bossControl);
            }

            var udfBossControl = new ButtonControl<Boss>(Settings.UserDefinedBoss, Settings.UserDefinedBoss.Name);
            udfBossControl.ButtonClicked += b => LoadSkills(b);
            bossPanel.Controls.Add(udfBossControl);

            var loadingBoss = activeBoss ?? instance.Bosses.FirstOrDefault();
            LoadSkills(loadingBoss);
        }

        public void LoadSkills(Boss boss)
        {
            UnregisterAllHotKeys();

            Text = string.IsNullOrEmpty(boss.InstanceName) ? boss.Name : $"{boss.InstanceName} - {boss.Name}";
            skillControls.ForEach(c => c.Dispose());
            skillControls.Clear();
            skillPanel.Controls.Clear();

            foreach (var skill in boss.Skills)
            {
                skill.InstanceName = boss.InstanceName;
                skill.BossName = boss.Name;
                var skillControl = new SkillControl(skill, timer);
                skillPanel.Controls.Add(skillControl);
                skillControls.Add(skillControl);
            }

            foreach (var b in bossPanel.Controls.OfType<ButtonControl<Boss>>())
                b.Checked = b.Data.Id == boss.Id;

            foreach (var i in instancePanel.Controls.OfType<ButtonControl<Instance>>())
                i.Checked = i.Data.Id == boss.InstanceId;

            LinkAffiliateSkills();
            RegisterAllHotKeys();
            ResizeForm();
        }

        private void LinkAffiliateSkills()
        {
            foreach (var control in skillControls)
            {
                control.AffiliateSkills.Clear();

                if (control.Skill.Flag)
                {
                    control.AffiliateSkills.AddRange(skillControls.Where(c => c.Skill.Flag));
                }

                if (!string.IsNullOrEmpty(control.Skill.Affiliate))
                {
                    foreach (var name in control.Skill.Affiliate.Split(','))
                    {
                        var aff = skillControls.FirstOrDefault(c => c.Skill.Name.Equals(name));
                        if (aff != null)
                            control.AffiliateSkills.Add(aff);
                    }
                }

                if (control.AffiliateSkills.Contains(control))
                {
                    control.AffiliateSkills.Remove(control);
                }
            }
        }

        private void OnAppExit(object sender, EventArgs e)
        {
            UnregisterAllHotKeys();
            SaveSettings();
        }

        private void SaveSettings()
        {
            if (!Settings.Profile.Preservable || !SettingsChanged)
                return;
            var settingsXml = Settings.SerializeToString();
            File.WriteAllText(DataFile, settingsXml);
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
                skillControl?.StartSkills();
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

        private async void DoubleClick(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "所有文件 (*.*)|*.*",
                InitialDirectory = Application.StartupPath
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var file = openFileDialog.FileName;
                await Init(file);
                SettingsChanged = true;
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }
    }
}

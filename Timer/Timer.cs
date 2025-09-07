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
        private static string InstancesXml = Properties.Resources.Settings;

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

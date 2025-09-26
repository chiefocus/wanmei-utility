using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WinformTimerGroups
{
    public partial class MainForm : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private void panelTitle_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private Config config;
        private Timer uiTimer;

        public MainForm()
        {
            InitializeComponent();
            this.Load += MainForm_Load;
            //this.Icon = Properties.Resources.timer;
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            flowCategories.Enabled = false;

            // 异步加载 XML
            config = await System.Threading.Tasks.Task.Run(() => ConfigLoader.Load("config.xml"));

            flowCategories.Enabled = true;

            uiTimer = new Timer { Interval = 50 };
            uiTimer.Start();

            LoadCategories();
        }

        private void LoadCategories()
        {
            flowCategories.Controls.Clear();
            foreach (var cat in config.Categories)
            {
                var btn = new CategoryButton(cat, config.Style.CategoryButton);
                btn.CategoryClicked += (c) => LoadGroups(c);
                flowCategories.Controls.Add(btn);
            }
        }

        private void LoadGroups(Category cat)
        {
            flowGroups.Controls.Clear();
            foreach (var grp in cat.Groups)
            {
                var btn = new GroupButton(grp, config.Style.GroupButton);
                btn.GroupClicked += (g) => LoadTimers(g);
                flowGroups.Controls.Add(btn);
            }
        }


        private void LoadTimers(Group grp)
        {
            flowTimers.Controls.Clear();
            var timerControls = new List<TimerControl>();

            // 创建 TimerControl
            foreach (var t in grp.Timers)
            {
                var ctl = new TimerControl(t, uiTimer, config.Style.TimerButton);
                flowTimers.Controls.Add(ctl);
                timerControls.Add(ctl);
            }

            // 建立 StartWith 关联
            for (int i = 0; i < grp.Timers.Count; i++)
            {
                var startWith = grp.Timers[i].StartWith;
                if (!string.IsNullOrEmpty(startWith))
                {
                    var names = startWith.Split(',');
                    foreach (var name in names)
                    {
                        var linked = timerControls.Find(c => c.ButtonText == name.Trim());
                        if (linked != null) timerControls[i].AddLinkedTimer(linked);
                    }
                }
            }
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

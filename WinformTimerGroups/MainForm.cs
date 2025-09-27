using System;
using System.Collections.Generic;
using System.Drawing;
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
            this.FormBorderStyle = FormBorderStyle.None;
            this.Load += MainForm_Load;
            panelTitle.MouseDown += panelTitle_MouseDown;
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.Icon;

            // 异步加载 XML
            config = await System.Threading.Tasks.Task.Run(() => ConfigLoader.Load("config.xml"));
            LoadCategories();

            CreateResizeGrips();

            uiTimer = new Timer { Interval = 100 };
            uiTimer.Start();
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

        private const int GripSize = 3;

        private void CreateResizeGrips()
        {
            // 左
            var left = new Panel { Dock = DockStyle.Left, Width = GripSize, Cursor = Cursors.SizeWE };
            left.MouseDown += (s, e) => StartResize(10); // HTLEFT
            this.Controls.Add(left);

            // 右
            var right = new Panel { Dock = DockStyle.Right, Width = GripSize, Cursor = Cursors.SizeWE };
            right.MouseDown += (s, e) => StartResize(11); // HTRIGHT
            this.Controls.Add(right);

            // 顶
            var top = new Panel { Dock = DockStyle.Top, Height = GripSize, Cursor = Cursors.SizeNS };
            top.MouseDown += (s, e) => StartResize(12); // HTTOP
            this.Controls.Add(top);

            // 底
            var bottom = new Panel { Dock = DockStyle.Bottom, Height = GripSize, Cursor = Cursors.SizeNS };
            bottom.MouseDown += (s, e) => StartResize(15); // HTBOTTOM
            this.Controls.Add(bottom);

            // 四角
            var topLeft = new Panel { Size = new Size(GripSize, GripSize), Cursor = Cursors.SizeNWSE };
            topLeft.Location = new Point(0, 0);
            topLeft.MouseDown += (s, e) => StartResize(13); // HTTOPLEFT
            this.Controls.Add(topLeft);

            var topRight = new Panel { Size = new Size(GripSize, GripSize), Cursor = Cursors.SizeNESW };
            topRight.Location = new Point(this.ClientSize.Width - GripSize, 0);
            topRight.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            topRight.MouseDown += (s, e) => StartResize(14); // HTTOPRIGHT
            this.Controls.Add(topRight);

            var bottomLeft = new Panel { Size = new Size(GripSize, GripSize), Cursor = Cursors.SizeNESW };
            bottomLeft.Location = new Point(0, this.ClientSize.Height - GripSize);
            bottomLeft.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            bottomLeft.MouseDown += (s, e) => StartResize(16); // HTBOTTOMLEFT
            this.Controls.Add(bottomLeft);

            var bottomRight = new Panel { Size = new Size(GripSize, GripSize), Cursor = Cursors.SizeNWSE };
            bottomRight.Location = new Point(this.ClientSize.Width - GripSize, this.ClientSize.Height - GripSize);
            bottomRight.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            bottomRight.MouseDown += (s, e) => StartResize(17); // HTBOTTOMRIGHT
            this.Controls.Add(bottomRight);
        }


        private void StartResize(int hitTest)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_NCLBUTTONDOWN, hitTest, 0);
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x84;
            const int HTLEFT = 10;
            const int HTRIGHT = 11;
            const int HTTOP = 12;
            const int HTTOPLEFT = 13;
            const int HTTOPRIGHT = 14;
            const int HTBOTTOM = 15;
            const int HTBOTTOMLEFT = 16;
            const int HTBOTTOMRIGHT = 17;
            const int HTCAPTION = 2;
            const int grip = 10; // 边缘抓手宽度，可根据需要调节

            if (m.Msg == WM_NCHITTEST)
            {
                Point pos = PointToClient(Cursor.Position);

                bool left = pos.X <= grip;
                bool right = pos.X >= ClientSize.Width - grip;
                bool top = pos.Y <= grip;
                bool bottom = pos.Y >= ClientSize.Height - grip;

                // 四角优先
                if (left && top) { m.Result = (IntPtr)HTTOPLEFT; return; }
                if (left && bottom) { m.Result = (IntPtr)HTBOTTOMLEFT; return; }
                if (right && top) { m.Result = (IntPtr)HTTOPRIGHT; return; }
                if (right && bottom) { m.Result = (IntPtr)HTBOTTOMRIGHT; return; }

                // 四边
                if (left) { m.Result = (IntPtr)HTLEFT; return; }
                if (right) { m.Result = (IntPtr)HTRIGHT; return; }
                if (top) { m.Result = (IntPtr)HTTOP; return; }
                if (bottom) { m.Result = (IntPtr)HTBOTTOM; return; }

                // 标题栏拖动
                if (panelTitle.Bounds.Contains(pos))
                {
                    m.Result = (IntPtr)HTCAPTION;
                    return;
                }

                // 默认客户端区域
                m.Result = (IntPtr)1; // HTCLIENT
                return;
            }

            base.WndProc(ref m);
        }



        private void lblMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

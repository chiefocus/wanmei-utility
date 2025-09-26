using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinformTimerGroups
{
    public class TimerControl : UserControl
    {
        public TimerInfo Data { get; private set; }
        public string ButtonText => Data.ButtonText;
        private Stopwatch stopwatch;
        private TimeSpan initialTime;
        private Timer uiTimer;
        private List<TimerControl> linkedTimers = new List<TimerControl>();

        private Button btnStart;
        private Label lblTime;
        private Label lblDescription;

        public TimerControl(TimerInfo data, Timer sharedTimer, ButtonStyle style)
        {
            Data = data;
            uiTimer = sharedTimer;
            initialTime = TimeSpan.FromSeconds(data.CountdownSeconds / 10.0); // 缩短 10 倍
            stopwatch = new Stopwatch();

            InitUI(style);

            uiTimer.Tick += (s, e) => RefreshDisplay();
        }

        private void InitUI(ButtonStyle style)
        {
            this.Height = style.Height + 10;
            this.Width = 450;
            this.Margin = new Padding(5);

            btnStart = new Button
            {
                Text = Data.ButtonText,
                Width = style.Width,
                Height = style.Height,
                //BackColor = ColorTranslator.FromHtml(style.BackColor),
                Font = new Font(style.FontName, style.FontSize),
                Location = new Point(0, 0)
            };
            btnStart.Click += (s, e) =>
            {
                StartCountdown();
                foreach (var t in linkedTimers) t.StartCountdown();
            };
            this.Controls.Add(btnStart);

            lblTime = new Label
            {
                Text = initialTime.ToString(@"mm\:ss\.f"),
                Location = new Point(style.Width + 5, 5),
                Width = 80
            };
            this.Controls.Add(lblTime);

            lblDescription = new Label
            {
                Text = Data.Description,
                Location = new Point(style.Width + 90, 5),
                Width = 250
            };
            this.Controls.Add(lblDescription);
        }

        private void RefreshDisplay()
        {
            if (!stopwatch.IsRunning) return;
            var remaining = initialTime - stopwatch.Elapsed;
            if (remaining <= TimeSpan.Zero)
            {
                stopwatch.Restart();
                remaining = initialTime;
            }
            lblTime.Text = remaining.ToString(@"mm\:ss\.f");
        }

        public void StartCountdown()
        {
            stopwatch.Reset();
            stopwatch.Start();
            lblTime.Text = initialTime.ToString(@"mm\:ss\.f");
        }

        public void AddLinkedTimer(TimerControl ctl)
        {
            if (!linkedTimers.Contains(ctl)) linkedTimers.Add(ctl);
        }
    }
}

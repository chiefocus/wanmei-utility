using System;
using System.Drawing;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimerUtility.Models;

namespace TimerUtility
{
    public class StopwatchDisplay : UserControl
    {
        private const int WARNING_SECONDS = 5;

        public Color ForeColorMain { get; set; }
        public Font FontMain { get; set; }
        public Font FontSub { get; set; }
        public int Seconds
        {
            set
            {
                mainText = $"{value}";

                if (value == WARNING_SECONDS && !isPlaying)
                {
                    isPlaying = true;
                    try { soundPlayer?.Play(); } catch { }
                }

                if (value > WARNING_SECONDS)
                {
                    ForeColorMain = Color.Blue;
                    isPlaying = false;
                }
                else
                {
                    ForeColorMain = Color.Red;
                }
            }
        }

        public int Milliseconds
        {
            set
            {
                subText = $".{value}";
                Invalidate();
            }
        }

        public Skill Skill
        {
            set
            {
                if (!string.IsNullOrEmpty(value?.Voice))
                {
                    try
                    {
                        soundPlayer = new SoundPlayer(value.Voice);
                        soundPlayer.LoadAsync();
                    }
                    catch { }
                }
            }
        }

        private string mainText;
        private string subText;
        private SoundPlayer soundPlayer;
        private bool isPlaying = false;

        public StopwatchDisplay()
        {
            AutoScaleMode = AutoScaleMode.Dpi;
            DoubleBuffered = true;
            ResizeRedraw = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.Clear(BackColor);

            Size mainSize = TextRenderer.MeasureText(mainText, FontMain);
            TextRenderer.DrawText(e.Graphics, mainText, FontMain, new Point(0, 0), ForeColorMain, TextFormatFlags.NoPadding);

            var millisecondsFlag = WanmeiTimer.Settings?.Profile?.MillisecondsFlag ?? false;
            if (millisecondsFlag && !string.IsNullOrEmpty(subText))
            {
                Size subSize = TextRenderer.MeasureText(subText, FontSub);
                int subX = mainSize.Width - subSize.Width / 3 - 5;
                int subY = mainSize.Height - subSize.Height - 1;
                TextRenderer.DrawText(e.Graphics, subText, FontSub, new Point(subX, subY), ForeColorMain, TextFormatFlags.NoPadding);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                soundPlayer?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

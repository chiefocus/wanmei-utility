using System.Drawing;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimerUtility.Models;

namespace TimerUtility
{
    public class StopwatchDisplay : UserControl
    {
        public Color ForeColorMain { get; set; }
        public Font FontMain { get; set; }
        public Font FontSub { get; set; }
        public int Seconds
        {
            set
            {
                mainText = $"{value}";
                ForeColorMain = value > 5 ? Color.Blue : Color.Red;

                if (value == 5 && !isPlaying)
                {
                    isPlaying = true;
                    Task.Run(() => soundPlayer?.Play());
                }

                if (value > 5) isPlaying = false;
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
                skill = value;
                if (!string.IsNullOrEmpty(skill?.Voice))
                {
                    soundPlayer = new SoundPlayer(skill.Voice);
                }
            }
        }

        private Skill skill;
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

            Size mainSize = TextRenderer.MeasureText(mainText, FontMain, Size.Empty, TextFormatFlags.NoPadding);
            TextRenderer.DrawText(e.Graphics, mainText, FontMain, new Point(0, 0), ForeColorMain,
                TextFormatFlags.NoPadding | TextFormatFlags.NoClipping);

            var millisecondsFlag = WanmeiTimer.Settings?.Profile?.MillisecondsFlag;
            if (millisecondsFlag.HasValue && millisecondsFlag.Value)
            {
                Size subSize = TextRenderer.MeasureText(subText, FontSub, Size.Empty, TextFormatFlags.NoPadding);
                int subX = mainSize.Width - subSize.Width / 3 - 5;
                int subY = mainSize.Height - subSize.Height - 1;
                TextRenderer.DrawText(e.Graphics, subText, FontSub, new Point(subX, subY), ForeColorMain,
                TextFormatFlags.NoPadding | TextFormatFlags.NoClipping);
            }
        }
    }
}

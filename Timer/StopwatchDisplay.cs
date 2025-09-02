using System.Drawing;
using System.Windows.Forms;

namespace TimerUtility
{
    public class StopwatchDisplay : UserControl
    {
        public int Seconds
        {
            set
            {
                this.mainText = $"{value}";
            }
        }

        public int Milliseconds
        {
            set
            {
                this.subText = $".{value}";
                this.Invalidate();
            }
        }

        private string mainText;

        private string subText;

        public Color ForeColorMain { get; set; } = Color.Red;
        public Font FontMain { get; set; } = Control.DefaultFont;
        public Font FontSub { get; set; } = Control.DefaultFont;

        public StopwatchDisplay()
        {
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.Clear(this.BackColor);

            Size mainSize = TextRenderer.MeasureText(mainText, FontMain, Size.Empty, TextFormatFlags.NoPadding);
            TextRenderer.DrawText(e.Graphics, mainText, FontMain, new Point(0, 0), ForeColorMain,
                TextFormatFlags.NoPadding | TextFormatFlags.NoClipping);

            var millisecondsFlag = Timer.Settings?.Profile?.MillisecondsFlag;
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

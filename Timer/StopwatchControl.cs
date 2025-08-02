using System.Drawing;
using System.Windows.Forms;

namespace Timer
{
    public partial class StopwatchControl : UserControl
    {
        private static readonly int ALERT_SEC = 7;

        public int Seconds
        {
            set
            {
                this.label1.ForeColor = value < ALERT_SEC ? Color.Red : _secondsOriginalColor;
                this.label1.Text = $"{value}";
            }
        }
        public int Milliseconds
        {
            set
            {
                this.label2.ForeColor = int.Parse(this.label1.Text) < ALERT_SEC ? Color.Red : _millisecondsOriginalColor;
                this.label2.Text = value.ToString();
            }
        }

        private Color _secondsOriginalColor;
        private Color _millisecondsOriginalColor;

        private Font _secondsOriginalFont;

        public StopwatchControl()
        {
            InitializeComponent();
            this.label2.Text = "0";
            _secondsOriginalColor = this.label1.ForeColor;
            _millisecondsOriginalColor = this.label2.ForeColor;

            _secondsOriginalFont = this.label1.Font;

            this.label2.Visible = Timer.Profile.MillisecondsFlag;
        }
    }
}

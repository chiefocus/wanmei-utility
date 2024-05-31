using System;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Timer.Models;

namespace Timer
{
    public partial class SkillControl : UserControl
    {
        public bool Enabled { get; set; }
        public Skill RowData { get; set; }
        public DateTime StartOn { get; set; }
        public int Interval { get; set; }
        public string Description { get; set; }
        public int Flag { get; set; }
        public System.Windows.Forms.Timer Timer1 { get; set; }

        public SkillControl()
        {
        }

        public SkillControl(Skill row, System.Windows.Forms.Timer timer)
        {
            InitializeComponent();

            this.Dock = DockStyle.Top;

            Timer1 = timer;
            Timer1.Tick += new System.EventHandler(this.timer1_Tick);

            this.RowData = row;
            this.button1.Text = RowData.Interval == 0 ? RowData.Name : $"{RowData.Name}({RowData.Interval})";//row.Button1Text;
            this.button2.Text = row.Reset;
            this.textBox1.Text = row.Description;
            this.Interval = row.Interval;
            this.Flag = row.Flag;
            this.Tag = row.Interval;
            this.Description = row.Description;

            toolTip1.SetToolTip(this.button1, row.Description);
        }

        private string GetLabelText()
        {
            var escaped = DateTime.Now - StartOn;
            return Interval != 0 ? $"{Interval - escaped.TotalSeconds % Interval:N0}" : $"{escaped.Hours:00}:{escaped.Minutes:00}:{escaped.Seconds:00}";
        }

        public void UpdateLabel()
        {
            if (Enabled)
            {
                this.label1.Text = GetLabelText();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateLabel();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OnClick(this);
        }

        public void OnClick(SkillControl skillControl)
        {
            skillControl.StartOn = DateTime.Now;
            skillControl.Enabled = true;
            skillControl.textBox1.Visible = false;
            skillControl.label1.Text = Interval != 0 ? $"{Interval}" : "00:00:00";
            skillControl.label1.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            this.label1.Visible = false;
            this.textBox1.Visible = true;
            this.textBox1.Text = this.Description;
        }
    }
}

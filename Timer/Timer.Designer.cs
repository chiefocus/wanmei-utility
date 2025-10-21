using System.Windows.Forms.VisualStyles;

namespace TimerUtility
{
    partial class WanmeiTimer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.instancePanel = new System.Windows.Forms.FlowLayoutPanel();
            this.bossPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.skillPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.panel = new System.Windows.Forms.FlowLayoutPanel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.nowLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // instancePanel
            // 
            this.instancePanel.AutoSize = true;
            this.instancePanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.instancePanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.instancePanel.Margin = new System.Windows.Forms.Padding(2);
            this.instancePanel.Name = "instancePanel";
            // 
            // bossPanel
            // 
            this.bossPanel.AutoSize = true;
            this.bossPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bossPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bossPanel.Margin = new System.Windows.Forms.Padding(2);
            this.bossPanel.Name = "bossPanel";
            // 
            // skillPanel
            // 
            this.skillPanel.AutoSize = true;
            this.skillPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.skillPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.skillPanel.Margin = new System.Windows.Forms.Padding(2);
            this.skillPanel.MinimumSize = new System.Drawing.Size(289, 160);
            this.skillPanel.Name = "skillPanel";
            // 
            // panel
            // 
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Visible = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nowLabel});
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.DoubleClick += new System.EventHandler(this.DoubleClick);
            // 
            // nowLabel
            // 
            this.nowLabel.Font = new System.Drawing.Font("SimHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nowLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.nowLabel.Margin = new System.Windows.Forms.Padding(0);
            this.nowLabel.Name = "nowLabel";
            this.nowLabel.Text = "{now}";
            // 
            // WanmeiTimer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel);
            this.Controls.Add(this.instancePanel);
            this.Controls.Add(this.bossPanel);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.skillPanel);
            this.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "WanmeiTimer";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "计时器";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.FlowLayoutPanel instancePanel;
        private System.Windows.Forms.FlowLayoutPanel bossPanel;
        private System.Windows.Forms.FlowLayoutPanel skillPanel;
        private System.Windows.Forms.FlowLayoutPanel panel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel nowLabel;
    }
}


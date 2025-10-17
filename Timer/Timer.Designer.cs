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
            this.titlePanel = new System.Windows.Forms.Panel();
            this.udfButton = new System.Windows.Forms.RadioButton();
            this.nowText = new System.Windows.Forms.TextBox();
            this.instancePanel = new System.Windows.Forms.FlowLayoutPanel();
            this.bossPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.skillPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.titlePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // titlePanel
            // 
            this.titlePanel.AutoScroll = true;
            this.titlePanel.Controls.Add(this.udfButton);
            this.titlePanel.Controls.Add(this.nowText);
            this.titlePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.titlePanel.Location = new System.Drawing.Point(3, 3);
            this.titlePanel.Margin = new System.Windows.Forms.Padding(2);
            this.titlePanel.Name = "titlePanel";
            this.titlePanel.Size = new System.Drawing.Size(278, 32);
            this.titlePanel.TabIndex = 0;
            this.titlePanel.DoubleClick += new System.EventHandler(this.titlePanel_DoubleClick);
            // 
            // udfButton
            // 
            this.udfButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.udfButton.Font = new System.Drawing.Font("SimHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.udfButton.Location = new System.Drawing.Point(106, 2);
            this.udfButton.Margin = new System.Windows.Forms.Padding(0);
            this.udfButton.Name = "udfButton";
            this.udfButton.Size = new System.Drawing.Size(51, 30);
            this.udfButton.TabIndex = 2;
            this.udfButton.Text = "自定义";
            this.udfButton.UseVisualStyleBackColor = true;
            this.udfButton.Click += new System.EventHandler(this.udfButton_Click);
            // 
            // nowText
            // 
            this.nowText.BackColor = System.Drawing.SystemColors.Control;
            this.nowText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.nowText.Cursor = System.Windows.Forms.Cursors.No;
            this.nowText.Font = new System.Drawing.Font("SimHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nowText.ForeColor = System.Drawing.Color.Green;
            this.nowText.Location = new System.Drawing.Point(4, 7);
            this.nowText.Margin = new System.Windows.Forms.Padding(2);
            this.nowText.Name = "nowText";
            this.nowText.ReadOnly = true;
            this.nowText.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.nowText.Size = new System.Drawing.Size(85, 23);
            this.nowText.TabIndex = 9999;
            this.nowText.TabStop = false;
            this.nowText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // instancePanel
            // 
            this.instancePanel.AutoSize = true;
            this.instancePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.instancePanel.Location = new System.Drawing.Point(3, 198);
            this.instancePanel.Margin = new System.Windows.Forms.Padding(2);
            this.instancePanel.Name = "instancePanel";
            this.instancePanel.Size = new System.Drawing.Size(278, 0);
            this.instancePanel.TabIndex = 10;
            // 
            // bossPanel
            // 
            this.bossPanel.AutoSize = true;
            this.bossPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.bossPanel.Location = new System.Drawing.Point(3, 198);
            this.bossPanel.Margin = new System.Windows.Forms.Padding(2);
            this.bossPanel.Name = "bossPanel";
            this.bossPanel.Size = new System.Drawing.Size(278, 0);
            this.bossPanel.TabIndex = 20;
            // 
            // skillPanel
            // 
            this.skillPanel.AutoSize = true;
            this.skillPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.skillPanel.Location = new System.Drawing.Point(3, 35);
            this.skillPanel.Margin = new System.Windows.Forms.Padding(2);
            this.skillPanel.MinimumSize = new System.Drawing.Size(289, 163);
            this.skillPanel.Name = "skillPanel";
            this.skillPanel.Size = new System.Drawing.Size(289, 163);
            this.skillPanel.TabIndex = 0;
            // 
            // WanmeiTimer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(284, 305);
            this.Controls.Add(this.bossPanel);
            this.Controls.Add(this.instancePanel);
            this.Controls.Add(this.skillPanel);
            this.Controls.Add(this.titlePanel);
            this.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "WanmeiTimer";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "计时器";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form_Load);
            this.titlePanel.ResumeLayout(false);
            this.titlePanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Panel titlePanel;
        public System.Windows.Forms.FlowLayoutPanel instancePanel;
        private System.Windows.Forms.FlowLayoutPanel bossPanel;
        private System.Windows.Forms.FlowLayoutPanel skillPanel;
        private System.Windows.Forms.TextBox nowText;
        private System.Windows.Forms.RadioButton udfButton;
    }
}


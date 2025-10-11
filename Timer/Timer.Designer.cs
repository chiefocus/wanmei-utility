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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WanmeiTimer));
            timer = new System.Windows.Forms.Timer(components);
            titlePanel = new System.Windows.Forms.Panel();
            startButton = new System.Windows.Forms.Button();
            udfButton = new System.Windows.Forms.RadioButton();
            nowText = new System.Windows.Forms.TextBox();
            instancePanel = new System.Windows.Forms.FlowLayoutPanel();
            bossPanel = new System.Windows.Forms.FlowLayoutPanel();
            skillPanel = new System.Windows.Forms.FlowLayoutPanel();
            titlePanel.SuspendLayout();
            SuspendLayout();
            // 
            // timer
            // 
            timer.Tick += new System.EventHandler(timer_Tick);
            // 
            // titlePanel
            // 
            titlePanel.AutoScroll = true;
            titlePanel.Controls.Add(startButton);
            titlePanel.Controls.Add(udfButton);
            titlePanel.Controls.Add(nowText);
            titlePanel.Dock = System.Windows.Forms.DockStyle.Top;
            titlePanel.Location = new System.Drawing.Point(3, 3);
            titlePanel.Margin = new System.Windows.Forms.Padding(2);
            titlePanel.Name = "titlePanel";
            titlePanel.Size = new System.Drawing.Size(289, 32);
            titlePanel.TabIndex = 0;
            titlePanel.DoubleClick += new System.EventHandler(panel1_DoubleClick);
            // 
            // startButton
            // 
            startButton.Font = new System.Drawing.Font("SimHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            startButton.Location = new System.Drawing.Point(162, 2);
            startButton.Margin = new System.Windows.Forms.Padding(0);
            startButton.Name = "startButton";
            startButton.Size = new System.Drawing.Size(64, 30);
            startButton.TabIndex = 3;
            startButton.Text = "开打计时";
            startButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            startButton.UseVisualStyleBackColor = true;
            startButton.Visible = false;
            startButton.Click += new System.EventHandler(startButton_Click);
            // 
            // udfButton
            // 
            udfButton.Appearance = System.Windows.Forms.Appearance.Button;
            udfButton.Font = new System.Drawing.Font("SimHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            udfButton.Location = new System.Drawing.Point(110, 2);
            udfButton.Margin = new System.Windows.Forms.Padding(0);
            udfButton.Name = "udfButton";
            udfButton.Size = new System.Drawing.Size(51, 30);
            udfButton.TabIndex = 2;
            udfButton.TabStop = false;
            udfButton.Text = "自定义";
            udfButton.UseVisualStyleBackColor = true;
            udfButton.Click += new System.EventHandler(udfButton_Click);
            // 
            // nowText
            // 
            nowText.BackColor = System.Drawing.SystemColors.Control;
            nowText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            nowText.Cursor = System.Windows.Forms.Cursors.No;
            nowText.Font = new System.Drawing.Font("SimHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            nowText.ForeColor = System.Drawing.Color.Green;
            nowText.Location = new System.Drawing.Point(4, 7);
            nowText.Margin = new System.Windows.Forms.Padding(2);
            nowText.Name = "nowText";
            nowText.ReadOnly = true;
            nowText.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            nowText.Size = new System.Drawing.Size(85, 23);
            nowText.TabIndex = 9999;
            nowText.TabStop = false;
            nowText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // instancePanel
            // 
            instancePanel.AutoSize = true;
            instancePanel.Dock = System.Windows.Forms.DockStyle.Top;
            instancePanel.Location = new System.Drawing.Point(3, 198);
            instancePanel.Margin = new System.Windows.Forms.Padding(2);
            instancePanel.Name = "instancePanel";
            instancePanel.Size = new System.Drawing.Size(289, 0);
            instancePanel.TabIndex = 10;
            // 
            // bossPanel
            // 
            bossPanel.AutoSize = true;
            bossPanel.Dock = System.Windows.Forms.DockStyle.Top;
            bossPanel.Location = new System.Drawing.Point(3, 198);
            bossPanel.Margin = new System.Windows.Forms.Padding(2);
            bossPanel.Name = "bossPanel";
            bossPanel.Size = new System.Drawing.Size(289, 0);
            bossPanel.TabIndex = 20;
            // 
            // skillPanel
            // 
            skillPanel.Dock = System.Windows.Forms.DockStyle.Top;
            skillPanel.Location = new System.Drawing.Point(3, 35);
            skillPanel.Margin = new System.Windows.Forms.Padding(2);
            skillPanel.Name = "skillPanel";
            skillPanel.MinimumSize = new System.Drawing.Size(289, 163);
            skillPanel.TabIndex = 0;
            skillPanel.AutoSize = true;
            // 
            // WanmeiTimer
            // 
            AutoSize = true;
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(bossPanel);
            Controls.Add(instancePanel);
            Controls.Add(skillPanel);
            Controls.Add(titlePanel);
            Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Margin = new System.Windows.Forms.Padding(2);
            MaximizeBox = false;
            Name = "WanmeiTimer";
            Padding = new System.Windows.Forms.Padding(3);
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "计时器";
            TopMost = true;
            Load += new System.EventHandler(Form_Load);
            titlePanel.ResumeLayout(false);
            titlePanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Panel titlePanel;
        public System.Windows.Forms.FlowLayoutPanel instancePanel;
        private System.Windows.Forms.FlowLayoutPanel bossPanel;
        private System.Windows.Forms.FlowLayoutPanel skillPanel;
        private System.Windows.Forms.TextBox nowText;
        private System.Windows.Forms.RadioButton udfButton;
        public System.Windows.Forms.Button startButton;
    }
}


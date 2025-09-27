namespace WinformTimerGroups
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panelTitle = new System.Windows.Forms.Panel();
            this.lblClose = new System.Windows.Forms.Label();
            this.lblMin = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.titleIcon = new System.Windows.Forms.PictureBox();
            this.flowCategories = new System.Windows.Forms.FlowLayoutPanel();
            this.flowGroups = new System.Windows.Forms.FlowLayoutPanel();
            this.flowTimers = new System.Windows.Forms.FlowLayoutPanel();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.titleIcon)).BeginInit();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTitle
            // 
            this.panelTitle.BackColor = System.Drawing.Color.Snow;
            this.panelTitle.Controls.Add(this.lblClose);
            this.panelTitle.Controls.Add(this.lblMin);
            this.panelTitle.Controls.Add(this.lblTitle);
            this.panelTitle.Controls.Add(this.titleIcon);
            this.panelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitle.Location = new System.Drawing.Point(0, 0);
            this.panelTitle.Name = "panelTitle";
            this.panelTitle.Padding = new System.Windows.Forms.Padding(3);
            this.panelTitle.Size = new System.Drawing.Size(784, 32);
            this.panelTitle.TabIndex = 0;
            // 
            // lblClose
            // 
            this.lblClose.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblClose.AutoSize = true;
            this.lblClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClose.Location = new System.Drawing.Point(1283, 6);
            this.lblClose.Name = "lblClose";
            this.lblClose.Size = new System.Drawing.Size(25, 20);
            this.lblClose.TabIndex = 3;
            this.lblClose.Text = "✖︎";
            this.lblClose.Click += new System.EventHandler(this.lblClose_Click);
            // 
            // lblMin
            // 
            this.lblMin.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblMin.AutoSize = true;
            this.lblMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMin.Location = new System.Drawing.Point(1250, 6);
            this.lblMin.Name = "lblMin";
            this.lblMin.Size = new System.Drawing.Size(25, 20);
            this.lblMin.TabIndex = 2;
            this.lblMin.Text = "➖";
            this.lblMin.Click += new System.EventHandler(this.lblMin_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(39, 5);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(51, 20);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "label1";
            // 
            // titleIcon
            // 
            this.titleIcon.Image = ((System.Drawing.Image)(resources.GetObject("titleIcon.Image")));
            this.titleIcon.Location = new System.Drawing.Point(1, 1);
            this.titleIcon.Name = "titleIcon";
            this.titleIcon.Size = new System.Drawing.Size(30, 30);
            this.titleIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.titleIcon.TabIndex = 0;
            this.titleIcon.TabStop = false;
            // 
            // flowCategories
            // 
            this.flowCategories.AutoSize = true;
            this.flowCategories.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowCategories.Location = new System.Drawing.Point(0, 32);
            this.flowCategories.Name = "flowCategories";
            this.flowCategories.Padding = new System.Windows.Forms.Padding(3);
            this.flowCategories.Size = new System.Drawing.Size(784, 6);
            this.flowCategories.TabIndex = 1;
            // 
            // flowGroups
            // 
            this.flowGroups.AutoSize = true;
            this.flowGroups.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowGroups.Location = new System.Drawing.Point(0, 38);
            this.flowGroups.Name = "flowGroups";
            this.flowGroups.Padding = new System.Windows.Forms.Padding(3);
            this.flowGroups.Size = new System.Drawing.Size(784, 6);
            this.flowGroups.TabIndex = 2;
            // 
            // flowTimers
            // 
            this.flowTimers.AutoSize = true;
            this.flowTimers.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowTimers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowTimers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowTimers.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowTimers.Location = new System.Drawing.Point(0, 44);
            this.flowTimers.Name = "flowTimers";
            this.flowTimers.Padding = new System.Windows.Forms.Padding(3);
            this.flowTimers.Size = new System.Drawing.Size(784, 365);
            this.flowTimers.TabIndex = 3;
            this.flowTimers.WrapContents = false;
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.label1);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 409);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Padding = new System.Windows.Forms.Padding(100);
            this.panelBottom.Size = new System.Drawing.Size(784, 40);
            this.panelBottom.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(784, 449);
            this.Controls.Add(this.flowTimers);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.flowGroups);
            this.Controls.Add(this.flowCategories);
            this.Controls.Add(this.panelTitle);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.panelTitle.ResumeLayout(false);
            this.panelTitle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.titleIcon)).EndInit();
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTitle;
        private System.Windows.Forms.FlowLayoutPanel flowCategories;
        private System.Windows.Forms.FlowLayoutPanel flowGroups;
        private System.Windows.Forms.FlowLayoutPanel flowTimers;
        private System.Windows.Forms.PictureBox titleIcon;
        private System.Windows.Forms.Label lblClose;
        private System.Windows.Forms.Label lblMin;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Label label1;
    }
}
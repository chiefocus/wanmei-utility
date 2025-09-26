using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WinformTimerGroups
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private FlowLayoutPanel flowCategories;
        private FlowLayoutPanel flowGroups;
        private FlowLayoutPanel flowTimers;

        /// <summary>
        /// 清理资源
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.flowCategories = new System.Windows.Forms.FlowLayoutPanel();
            this.flowGroups = new System.Windows.Forms.FlowLayoutPanel();
            this.flowTimers = new System.Windows.Forms.FlowLayoutPanel();
            this.panelTitle = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // flowCategories
            // 
            this.flowCategories.AutoScroll = true;
            this.flowCategories.Location = new System.Drawing.Point(279, 249);
            this.flowCategories.Name = "flowCategories";
            this.flowCategories.Size = new System.Drawing.Size(800, 50);
            this.flowCategories.TabIndex = 2;
            // 
            // flowGroups
            // 
            this.flowGroups.AutoScroll = true;
            this.flowGroups.Location = new System.Drawing.Point(279, 348);
            this.flowGroups.Name = "flowGroups";
            this.flowGroups.Size = new System.Drawing.Size(800, 50);
            this.flowGroups.TabIndex = 1;
            // 
            // flowTimers
            // 
            this.flowTimers.AutoScroll = true;
            this.flowTimers.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowTimers.Location = new System.Drawing.Point(354, 465);
            this.flowTimers.Name = "flowTimers";
            this.flowTimers.Size = new System.Drawing.Size(283, 202);
            this.flowTimers.TabIndex = 0;
            this.flowTimers.WrapContents = false;
            // 
            // panelTitle
            // 
            this.panelTitle.Controls.Add(this.pictureBox1);
            this.panelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitle.Location = new System.Drawing.Point(0, 0);
            this.panelTitle.Margin = new System.Windows.Forms.Padding(0);
            this.panelTitle.Name = "panelTitle";
            this.panelTitle.Size = new System.Drawing.Size(1439, 181);
            this.panelTitle.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(7, 3);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Padding = new System.Windows.Forms.Padding(3);
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(1439, 1046);
            this.Controls.Add(this.panelTitle);
            this.Controls.Add(this.flowTimers);
            this.Controls.Add(this.flowGroups);
            this.Controls.Add(this.flowCategories);
            this.Name = "MainForm";
            this.Text = "循环倒计时多层计时器";
            this.panelTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panelTitle;
        private PictureBox pictureBox1;
    }
}

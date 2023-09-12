namespace WindowsFormsApp1
{
    partial class frmTimer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTimer));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblNow = new System.Windows.Forms.Label();
            this.lblInterval1 = new System.Windows.Forms.Label();
            this.lblInterval2 = new System.Windows.Forms.Label();
            this.btnStart1 = new System.Windows.Forms.Button();
            this.btnStart2 = new System.Windows.Forms.Button();
            this.btnClear1 = new System.Windows.Forms.Button();
            this.btnClear2 = new System.Windows.Forms.Button();
            this.btnClear3 = new System.Windows.Forms.Button();
            this.btnStart3 = new System.Windows.Forms.Button();
            this.lblInterval3 = new System.Windows.Forms.Label();
            this.btnXiaotie = new System.Windows.Forms.Button();
            this.btnZichun = new System.Windows.Forms.Button();
            this.btnCangli = new System.Windows.Forms.Button();
            this.btnTiandi = new System.Windows.Forms.Button();
            this.btnClear4 = new System.Windows.Forms.Button();
            this.btnStart4 = new System.Windows.Forms.Button();
            this.lblInterval4 = new System.Windows.Forms.Label();
            this.btnClear5 = new System.Windows.Forms.Button();
            this.btnStart5 = new System.Windows.Forms.Button();
            this.lblInterval5 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblNow
            // 
            this.lblNow.AutoSize = true;
            this.lblNow.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNow.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblNow.Location = new System.Drawing.Point(5, 259);
            this.lblNow.Name = "lblNow";
            this.lblNow.Size = new System.Drawing.Size(143, 37);
            this.lblNow.TabIndex = 0;
            this.lblNow.Text = "00:00.00";
            // 
            // lblInterval1
            // 
            this.lblInterval1.AutoSize = true;
            this.lblInterval1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInterval1.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblInterval1.Location = new System.Drawing.Point(175, 2);
            this.lblInterval1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInterval1.Name = "lblInterval1";
            this.lblInterval1.Size = new System.Drawing.Size(92, 32);
            this.lblInterval1.TabIndex = 1;
            this.lblInterval1.Text = "label1";
            this.lblInterval1.Click += new System.EventHandler(this.lblInterval1_Click_1);
            // 
            // lblInterval2
            // 
            this.lblInterval2.AutoSize = true;
            this.lblInterval2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInterval2.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblInterval2.Location = new System.Drawing.Point(175, 34);
            this.lblInterval2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInterval2.Name = "lblInterval2";
            this.lblInterval2.Size = new System.Drawing.Size(92, 32);
            this.lblInterval2.TabIndex = 2;
            this.lblInterval2.Text = "label2";
            this.lblInterval2.Click += new System.EventHandler(this.lblInterval2_Click);
            // 
            // btnStart1
            // 
            this.btnStart1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart1.Location = new System.Drawing.Point(7, 4);
            this.btnStart1.Margin = new System.Windows.Forms.Padding(2);
            this.btnStart1.Name = "btnStart1";
            this.btnStart1.Size = new System.Drawing.Size(94, 28);
            this.btnStart1.TabIndex = 3;
            this.btnStart1.Text = "开始";
            this.btnStart1.UseVisualStyleBackColor = true;
            this.btnStart1.Click += new System.EventHandler(this.btnStart1_Click);
            // 
            // btnStart2
            // 
            this.btnStart2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart2.Location = new System.Drawing.Point(7, 36);
            this.btnStart2.Margin = new System.Windows.Forms.Padding(2);
            this.btnStart2.Name = "btnStart2";
            this.btnStart2.Size = new System.Drawing.Size(94, 28);
            this.btnStart2.TabIndex = 4;
            this.btnStart2.Text = "开始";
            this.btnStart2.UseVisualStyleBackColor = true;
            this.btnStart2.Click += new System.EventHandler(this.btnStart2_Click);
            // 
            // btnClear1
            // 
            this.btnClear1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear1.Location = new System.Drawing.Point(103, 2);
            this.btnClear1.Margin = new System.Windows.Forms.Padding(2);
            this.btnClear1.Name = "btnClear1";
            this.btnClear1.Size = new System.Drawing.Size(68, 28);
            this.btnClear1.TabIndex = 7;
            this.btnClear1.Text = "清除";
            this.btnClear1.UseVisualStyleBackColor = true;
            this.btnClear1.Click += new System.EventHandler(this.btnClear1_Click);
            // 
            // btnClear2
            // 
            this.btnClear2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear2.Location = new System.Drawing.Point(103, 34);
            this.btnClear2.Margin = new System.Windows.Forms.Padding(2);
            this.btnClear2.Name = "btnClear2";
            this.btnClear2.Size = new System.Drawing.Size(68, 28);
            this.btnClear2.TabIndex = 8;
            this.btnClear2.Text = "清除";
            this.btnClear2.UseVisualStyleBackColor = true;
            this.btnClear2.Click += new System.EventHandler(this.btnClear2_Click);
            // 
            // btnClear3
            // 
            this.btnClear3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear3.Location = new System.Drawing.Point(103, 66);
            this.btnClear3.Margin = new System.Windows.Forms.Padding(2);
            this.btnClear3.Name = "btnClear3";
            this.btnClear3.Size = new System.Drawing.Size(68, 28);
            this.btnClear3.TabIndex = 12;
            this.btnClear3.Text = "清除";
            this.btnClear3.UseVisualStyleBackColor = true;
            this.btnClear3.Click += new System.EventHandler(this.btnClear3_Click);
            // 
            // btnStart3
            // 
            this.btnStart3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart3.Location = new System.Drawing.Point(7, 68);
            this.btnStart3.Margin = new System.Windows.Forms.Padding(2);
            this.btnStart3.Name = "btnStart3";
            this.btnStart3.Size = new System.Drawing.Size(94, 28);
            this.btnStart3.TabIndex = 10;
            this.btnStart3.Text = "开始";
            this.btnStart3.UseVisualStyleBackColor = true;
            this.btnStart3.Click += new System.EventHandler(this.btnStart3_Click);
            // 
            // lblInterval3
            // 
            this.lblInterval3.AutoSize = true;
            this.lblInterval3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInterval3.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblInterval3.Location = new System.Drawing.Point(175, 65);
            this.lblInterval3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInterval3.Name = "lblInterval3";
            this.lblInterval3.Size = new System.Drawing.Size(92, 32);
            this.lblInterval3.TabIndex = 9;
            this.lblInterval3.Text = "label3";
            this.lblInterval3.Click += new System.EventHandler(this.lblInterval3_Click);
            // 
            // btnXiaotie
            // 
            this.btnXiaotie.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXiaotie.Location = new System.Drawing.Point(7, 196);
            this.btnXiaotie.Margin = new System.Windows.Forms.Padding(2);
            this.btnXiaotie.Name = "btnXiaotie";
            this.btnXiaotie.Size = new System.Drawing.Size(94, 28);
            this.btnXiaotie.TabIndex = 13;
            this.btnXiaotie.Text = "小铁";
            this.btnXiaotie.UseVisualStyleBackColor = true;
            this.btnXiaotie.Click += new System.EventHandler(this.btnXiaotie_Click);
            // 
            // btnZichun
            // 
            this.btnZichun.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnZichun.Location = new System.Drawing.Point(103, 194);
            this.btnZichun.Margin = new System.Windows.Forms.Padding(2);
            this.btnZichun.Name = "btnZichun";
            this.btnZichun.Size = new System.Drawing.Size(68, 28);
            this.btnZichun.TabIndex = 14;
            this.btnZichun.Text = "子纯";
            this.btnZichun.UseVisualStyleBackColor = true;
            this.btnZichun.Click += new System.EventHandler(this.btnZichun_Click);
            // 
            // btnCangli
            // 
            this.btnCangli.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCangli.Location = new System.Drawing.Point(7, 228);
            this.btnCangli.Margin = new System.Windows.Forms.Padding(2);
            this.btnCangli.Name = "btnCangli";
            this.btnCangli.Size = new System.Drawing.Size(94, 28);
            this.btnCangli.TabIndex = 15;
            this.btnCangli.Text = "仓力";
            this.btnCangli.UseVisualStyleBackColor = true;
            this.btnCangli.Click += new System.EventHandler(this.btnCangli_Click);
            // 
            // btnTiandi
            // 
            this.btnTiandi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTiandi.Location = new System.Drawing.Point(103, 226);
            this.btnTiandi.Margin = new System.Windows.Forms.Padding(2);
            this.btnTiandi.Name = "btnTiandi";
            this.btnTiandi.Size = new System.Drawing.Size(68, 28);
            this.btnTiandi.TabIndex = 17;
            this.btnTiandi.Text = "天地";
            this.btnTiandi.UseVisualStyleBackColor = true;
            this.btnTiandi.Click += new System.EventHandler(this.btnTiandi_Click);
            // 
            // btnClear4
            // 
            this.btnClear4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear4.Location = new System.Drawing.Point(103, 98);
            this.btnClear4.Margin = new System.Windows.Forms.Padding(2);
            this.btnClear4.Name = "btnClear4";
            this.btnClear4.Size = new System.Drawing.Size(68, 28);
            this.btnClear4.TabIndex = 20;
            this.btnClear4.Text = "清除";
            this.btnClear4.UseVisualStyleBackColor = true;
            this.btnClear4.Click += new System.EventHandler(this.btnClear4_Click);
            // 
            // btnStart4
            // 
            this.btnStart4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart4.Location = new System.Drawing.Point(7, 100);
            this.btnStart4.Margin = new System.Windows.Forms.Padding(2);
            this.btnStart4.Name = "btnStart4";
            this.btnStart4.Size = new System.Drawing.Size(94, 28);
            this.btnStart4.TabIndex = 19;
            this.btnStart4.Text = "开始";
            this.btnStart4.UseVisualStyleBackColor = true;
            this.btnStart4.Click += new System.EventHandler(this.btnStart4_Click);
            // 
            // lblInterval4
            // 
            this.lblInterval4.AutoSize = true;
            this.lblInterval4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInterval4.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblInterval4.Location = new System.Drawing.Point(175, 97);
            this.lblInterval4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInterval4.Name = "lblInterval4";
            this.lblInterval4.Size = new System.Drawing.Size(92, 32);
            this.lblInterval4.TabIndex = 18;
            this.lblInterval4.Text = "label4";
            this.lblInterval4.Click += new System.EventHandler(this.lblInterval4_Click);
            // 
            // btnClear5
            // 
            this.btnClear5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear5.Location = new System.Drawing.Point(103, 130);
            this.btnClear5.Margin = new System.Windows.Forms.Padding(2);
            this.btnClear5.Name = "btnClear5";
            this.btnClear5.Size = new System.Drawing.Size(68, 28);
            this.btnClear5.TabIndex = 23;
            this.btnClear5.Text = "清除";
            this.btnClear5.UseVisualStyleBackColor = true;
            this.btnClear5.Click += new System.EventHandler(this.btnClear5_Click);
            // 
            // btnStart5
            // 
            this.btnStart5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart5.Location = new System.Drawing.Point(7, 132);
            this.btnStart5.Margin = new System.Windows.Forms.Padding(2);
            this.btnStart5.Name = "btnStart5";
            this.btnStart5.Size = new System.Drawing.Size(94, 28);
            this.btnStart5.TabIndex = 22;
            this.btnStart5.Text = "开始";
            this.btnStart5.UseVisualStyleBackColor = true;
            this.btnStart5.Click += new System.EventHandler(this.btnStart5_Click);
            // 
            // lblInterval5
            // 
            this.lblInterval5.AutoSize = true;
            this.lblInterval5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInterval5.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblInterval5.Location = new System.Drawing.Point(175, 130);
            this.lblInterval5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInterval5.Name = "lblInterval5";
            this.lblInterval5.Size = new System.Drawing.Size(92, 32);
            this.lblInterval5.TabIndex = 21;
            this.lblInterval5.Text = "label5";
            this.lblInterval5.Click += new System.EventHandler(this.lblInterval5_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(0, 294);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(2);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(347, 134);
            this.richTextBox1.TabIndex = 24;
            this.richTextBox1.Text = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnClearAll);
            this.panel1.Controls.Add(this.btnStart1);
            this.panel1.Controls.Add(this.lblNow);
            this.panel1.Controls.Add(this.btnClear5);
            this.panel1.Controls.Add(this.lblInterval1);
            this.panel1.Controls.Add(this.btnStart5);
            this.panel1.Controls.Add(this.lblInterval2);
            this.panel1.Controls.Add(this.lblInterval5);
            this.panel1.Controls.Add(this.btnStart2);
            this.panel1.Controls.Add(this.btnClear4);
            this.panel1.Controls.Add(this.btnClear1);
            this.panel1.Controls.Add(this.btnStart4);
            this.panel1.Controls.Add(this.btnClear2);
            this.panel1.Controls.Add(this.lblInterval4);
            this.panel1.Controls.Add(this.lblInterval3);
            this.panel1.Controls.Add(this.btnTiandi);
            this.panel1.Controls.Add(this.btnStart3);
            this.panel1.Controls.Add(this.btnCangli);
            this.panel1.Controls.Add(this.btnClear3);
            this.panel1.Controls.Add(this.btnZichun);
            this.panel1.Controls.Add(this.btnXiaotie);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(347, 294);
            this.panel1.TabIndex = 25;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // btnClearAll
            // 
            this.btnClearAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PageTabList;
            this.btnClearAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearAll.Location = new System.Drawing.Point(7, 162);
            this.btnClearAll.Margin = new System.Windows.Forms.Padding(2);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(164, 28);
            this.btnClearAll.TabIndex = 24;
            this.btnClearAll.Text = "重置";
            this.btnClearAll.UseVisualStyleBackColor = true;
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // frmTimer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 428);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmTimer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "H3计时器";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblNow;
        private System.Windows.Forms.Label lblInterval1;
        private System.Windows.Forms.Label lblInterval2;
        private System.Windows.Forms.Button btnStart1;
        private System.Windows.Forms.Button btnStart2;
        private System.Windows.Forms.Button btnClear1;
        private System.Windows.Forms.Button btnClear2;
        private System.Windows.Forms.Button btnClear3;
        private System.Windows.Forms.Button btnStart3;
        private System.Windows.Forms.Label lblInterval3;
        private System.Windows.Forms.Button btnXiaotie;
        private System.Windows.Forms.Button btnZichun;
        private System.Windows.Forms.Button btnCangli;
        private System.Windows.Forms.Button btnTiandi;
        private System.Windows.Forms.Button btnClear4;
        private System.Windows.Forms.Button btnStart4;
        private System.Windows.Forms.Label lblInterval4;
        private System.Windows.Forms.Button btnClear5;
        private System.Windows.Forms.Button btnStart5;
        private System.Windows.Forms.Label lblInterval5;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnClearAll;
    }
}


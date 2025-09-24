using TimerUtility;

namespace TimerUtility
{
    partial class SkillControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            button1 = new System.Windows.Forms.Button();
            button2 = new System.Windows.Forms.Button();
            textBox1 = new System.Windows.Forms.TextBox();
            toolTip1 = new System.Windows.Forms.ToolTip(components);
            textBox2 = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            stopwatchDisplay1 = new TimerUtility.StopwatchDisplay();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Font = new System.Drawing.Font("SimHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            button1.Location = new System.Drawing.Point(0, 1);
            button1.Margin = new System.Windows.Forms.Padding(0);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(82, 32);
            button1.TabIndex = 0;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += new System.EventHandler(button1_Click);
            // 
            // button2
            // 
            button2.Font = new System.Drawing.Font("SimHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            button2.Location = new System.Drawing.Point(110, 1);
            button2.Margin = new System.Windows.Forms.Padding(2);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(51, 32);
            button2.TabIndex = 1;
            button2.Text = "button";
            button2.UseVisualStyleBackColor = true;
            button2.Click += new System.EventHandler(button2_Click);
            // 
            // textBox1
            // 
            textBox1.BackColor = System.Drawing.SystemColors.Control;
            textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBox1.Font = new System.Drawing.Font("SimHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            textBox1.ForeColor = System.Drawing.Color.DarkGreen;
            textBox1.Location = new System.Drawing.Point(165, 11);
            textBox1.Margin = new System.Windows.Forms.Padding(0);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(297, 15);
            textBox1.TabIndex = 3;
            textBox1.TextChanged += new System.EventHandler(textBox1_TextChanged);
            // 
            // textBox2
            // 
            textBox2.BackColor = System.Drawing.SystemColors.Control;
            textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBox2.Font = new System.Drawing.Font("SimHei", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            textBox2.ForeColor = System.Drawing.Color.DarkGreen;
            textBox2.Location = new System.Drawing.Point(83, 11);
            textBox2.Margin = new System.Windows.Forms.Padding(1);
            textBox2.MaxLength = 3;
            textBox2.Name = "textBox2";
            textBox2.Size = new System.Drawing.Size(22, 15);
            textBox2.TabIndex = 4;
            textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            textBox2.TextChanged += new System.EventHandler(textBox2_TextChanged);
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label2.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            label2.Location = new System.Drawing.Point(213, 3);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(31, 24);
            label2.TabIndex = 5;
            label2.Text = "➕";
            label2.Click += new System.EventHandler(label2_Click);
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label3.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            label3.Location = new System.Drawing.Point(242, 3);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(31, 24);
            label3.TabIndex = 6;
            label3.Text = "➖";
            label3.Click += new System.EventHandler(label3_Click);
            // 
            // stopwatchDisplay1
            // 
            stopwatchDisplay1.FontMain = new System.Drawing.Font("SimHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            stopwatchDisplay1.FontSub = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            stopwatchDisplay1.ForeColorMain = System.Drawing.Color.Red;
            stopwatchDisplay1.Location = new System.Drawing.Point(165, 5);
            stopwatchDisplay1.Name = "stopwatchDisplay1";
            stopwatchDisplay1.Size = new System.Drawing.Size(45, 26);
            stopwatchDisplay1.TabIndex = 8;
            stopwatchDisplay1.DoubleClick += new System.EventHandler(stopwatchDisplay1_DoubleClick);
            // 
            // SkillControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(stopwatchDisplay1);
            Controls.Add(button2);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(textBox2);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Margin = new System.Windows.Forms.Padding(0);
            Name = "SkillControl";
            Size = new System.Drawing.Size(481, 35);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private StopwatchDisplay stopwatchDisplay1;
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimerUtility2
{
    public partial class StealthForm : Form
    {
        // 引入Windows API
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_TOOLWINDOW = 0x00000080;
        private const int WS_EX_APPWINDOW = 0x00040000;

        //protected override void OnHandleCreated(EventArgs e)
        //{
        //    base.OnHandleCreated(e);
        //    // 关键API调用：将窗体标记为“工具窗口”
        //    SetWindowLong(this.Handle, GWL_EXSTYLE,
        //        GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TOOLWINDOW);
        //}

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;

                // 加上 WS_EX_TOOLWINDOW（0x80）标志
                cp.ExStyle |= 0x80;

                // 移除 WS_EX_APPWINDOW（0x40000）标志（如果有）
                cp.ExStyle &= ~0x40000;

                return cp;
            }
        }

        public StealthForm()
        {
            //this.ShowInTaskbar = false;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.label1.Text = DateTime.Now.ToString();
        }
    }
}

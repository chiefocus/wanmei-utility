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

namespace Timer2
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

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            // 关键API调用：将窗体标记为“工具窗口”
            SetWindowLong(this.Handle, GWL_EXSTYLE,
                GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TOOLWINDOW);
        }

        public StealthForm()
        {
            this.ShowInTaskbar = false;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.label1.Text = DateTime.Now.ToString();
        }
    }
}

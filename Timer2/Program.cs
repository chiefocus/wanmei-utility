using System;
using System.Windows.Forms;

namespace TimerUtility2
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //var form1 = new Form1();
            //form1.ShowInTaskbar = false;

            //var subForm = new Timer();
            //subForm.Owner = form1;

            Application.Run(new StealthForm());
        }
    }
}

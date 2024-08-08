using System;
using System.Threading;
using System.Windows.Forms;

namespace Timer
{
    internal static class Program
    {
        private static readonly string appGuid = "B54548B9-FE81-4242-B551-174D808FBE36";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (Mutex mutex = new Mutex(false, "Global\\" + appGuid))
            {
                if (!mutex.WaitOne(0, false))
                {
                    //MessageBox.Show("Instance already running");
                    return;
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Timer());
            }
        }
    }
}

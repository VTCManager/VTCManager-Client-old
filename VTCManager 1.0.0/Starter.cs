using System;
using System.Threading;
using System.Windows.Forms;

namespace VTCManager_1._0._0
{
    internal static class Starter
    {
        [STAThread]

        private static void Main()
        {
            System.Threading.Thread.Sleep(1000);
            bool createdNew;
            Mutex mutex = new Mutex(true, "VTCManager", out createdNew);
            if (createdNew)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run((Form)new Login());
                
            }
            else
            {
                int num = (int)MessageBox.Show("VTCManager is already running!", "VTCManager");
            }

        }
    }
}


using System;
using System.Threading;
using System.Windows.Forms;

namespace VTCManager_1._0._0
{
    internal static class Starter
    {
        [STAThread]

        private static void Main(string[] args)
        {
            System.Threading.Thread.Sleep(1000);
            bool createdNew;
            Mutex mutex = new Mutex(true, "VTCManager", out createdNew);
            if (createdNew)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                if (args.Length == 1)
                {
                    if (args[0] == "DEBUG")
                    {
                        Application.Run((Form)new Login(true));
                    }
                    else
                    {
                        Application.Run((Form)new Login(false));
                    }
                }
                else
                {
                    Application.Run((Form)new Login(false));
                }
                
            }
            else
            {
                int num = (int)MessageBox.Show("VTCManager is already running!", "VTCManager");
            }

        }
    }
}



//do not touch:for GUI System    
namespace System.Windows.Forms
    {
        using System;
        using System.Runtime.CompilerServices;
        using System.Runtime.InteropServices;

        public static class Extensions
        {
            private const uint SW_RESTORE = 9;

            public static void Restore(this Form form)
            {
                ShowWindow(form.Handle, 9);
            }

            [DllImport("user32.dll")]
            private static extern int ShowWindow(IntPtr hWnd, uint Msg);
        }
    }

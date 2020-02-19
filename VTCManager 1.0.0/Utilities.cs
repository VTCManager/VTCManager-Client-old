using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows;

namespace VTCManager_1._0._0
{
    class Utilities
    {
        //Fields
        private static long _lastCheckTime;
        private static bool _cachedRunningFlag;
        private static string _chachedGame;
        private static string _max;
        private static bool _DiscordRunningFlag;

        //Properties
        public static string LastRunningGameName { get; set; }


        internal static string BytesToString(byte[] bytes) =>
    ((bytes != null) ? Encoding.UTF8.GetString(bytes, 0, Array.FindIndex<byte>(bytes, b => b == 0)) : string.Empty);
        internal static DateTime MinutesToDate(int minutes) =>
    SecondsToDate(minutes * 60);
        internal static DateTime SecondsToDate(int seconds)
        {
            if (seconds < 0)
            {
                seconds = 0;
            }
            return new DateTime(seconds * 0x98_9680L, DateTimeKind.Utc);
        }

        public static string Sha256(string randomString)
        {
            StringBuilder builder = new StringBuilder();
            foreach (byte num2 in new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(randomString), 0, Encoding.UTF8.GetByteCount(randomString)))
            {
                builder.Append(num2.ToString("x2"));
            }
            return builder.ToString();
        }

        public static bool IsGameRunning
        {
            get
            {
                if ((DateTime.Now - new DateTime(Interlocked.Read(ref _lastCheckTime))) > TimeSpan.FromSeconds(3.0))
                {
                    Interlocked.Exchange(ref _lastCheckTime, DateTime.Now.Ticks);
                    Process[] processes = Process.GetProcesses();
                    int index = 0;
                    while (true)
                    {
                        if (index < processes.Length)
                        {
                            Process process = processes[index];
                            try
                            {
                                if ((process.MainWindowTitle.StartsWith("Euro Truck Simulator 2") || (process.ProcessName == "eurotrucks2")))
                                {
                                    _cachedRunningFlag = true;
                                    if (process.ProcessName == "eurotrucks2")
                                    {
                                        LastRunningGameName = "ETS2";
                                    }
                                    return _cachedRunningFlag;
                                }
                                if ((process.MainWindowTitle.StartsWith("American Truck Simulator") || (process.ProcessName == "amtrucks.exe")))
                                {
                                    _cachedRunningFlag = true;
                                    if (process.ProcessName == "amtrucks")
                                    {
                                        LastRunningGameName = "ATS";
                                    }
                                    return _cachedRunningFlag;
                                }

                            }
                            catch
                            {
                            }
                            index++;
                            continue;
                        }
                        else
                        {
                            _cachedRunningFlag = false;
                        }
                        break;
                    }
                }
                return _cachedRunningFlag;
            }
        }

        public static bool IsDiscordRunning
        {
            get
            {
                if ((DateTime.Now - new DateTime(Interlocked.Read(ref _lastCheckTime))) > TimeSpan.FromSeconds(3.0))
                {
                    Interlocked.Exchange(ref _lastCheckTime, DateTime.Now.Ticks);
                    Process[] processes = Process.GetProcesses();
                    int index = 0;
                    while (true)
                    {
                        if (index < processes.Length)
                        {
                            Process process = processes[index];
                            try
                            {
                              

                                if (process.ProcessName == "Discord")
                                {
                                    _DiscordRunningFlag = true;
      
                                    return _DiscordRunningFlag;
                                }

                            }
                            catch
                            {
                            }
                            index++;
                            continue;
                        }
                        else
                        {

                            _DiscordRunningFlag = false;
                        }
                        break;
                    }
                }
                return _DiscordRunningFlag;
            }
        }

        public static string WhichGameIsRunning
        {
            get
            {
                foreach (Process process in Process.GetProcesses())
                {
                    try
                    {
                        if (process.MainWindowTitle.StartsWith("Euro Truck Simulator 2") && (process.ProcessName == "eurotrucks2"))
                        {
                            _chachedGame = "ets2";
                        }
                        if (process.MainWindowTitle.StartsWith("American Truck Simulator") && (process.ProcessName == "amtrucks"))
                        {
                            _chachedGame = "ats";
                        }

                    }
                    catch
                    {
                        _chachedGame = "none";
                    }
                }
                return _chachedGame;
            }
        }




        // Edit by Thommy
        public void Reg_Schreiben(string name, string wert)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", true);
            key.CreateSubKey("VTCManager");
            key = key.OpenSubKey("VTCManager", true);
            key.CreateSubKey("TruckersMP_Autorun");
            key = key.OpenSubKey("TruckersMP_Autorun", true);
            key.SetValue(name, wert);

        }
        public string Reg_Lesen(string ordner, string value)
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\VTCManager\" + ordner);
                return key.GetValue(value).ToString();
            } catch {
                return null;
            }


        }






    }
}

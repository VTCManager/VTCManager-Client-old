using DiscordRPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace VTCManager_1._0._0
{
    class Discord
    {
        public DiscordRpcClient client;

        public Discord()
        {
            if (Utilities.IsDiscordRunning == true)
            {

                client = new DiscordRpcClient("659036297561767948");
                client.Initialize();
                client.SetPresence(new RichPresence()
                {
                    Details = "Starte...",
                    Assets = new Assets()
                    {
                        LargeImageKey = "truck-icon",
                        LargeImageText = "Beyond the limits",
                        SmallImageKey = "vtcm-logo"
                    }

                });
                Timer t1 = new Timer(); // Timer anlegen
			t1.Interval = 100; // Intervall festlegen, hier 100 ms
			t1.Elapsed += new ElapsedEventHandler(t1_Tick); // Eventhandler ezeugen der beim Timerablauf aufgerufen wird
			t1.Start(); // Timer starten

			void t1_Tick(object sender, EventArgs e)
			{
				// dieser Code wird ausgeführt, wenn der Timer abgelaufen ist
				client.Invoke();
			}
            }
        }
        public void onTour(string destination, string depature, string freight, string weight)
        {
            RichPresence rpc = new RichPresence()
            {
                Details = "Fracht: "+freight+"("+weight+"t)",
                State = "von "+depature+" nach "+destination,

                Assets = new Assets()
                {
                    LargeImageKey = "truck-icon",
                    LargeImageText = "Beyond the limits",
                    SmallImageKey = "vtcm-logo"
                }
            };
            rpc = rpc.WithTimestamps(Timestamps.Now);
            client.SetPresence(rpc);
            client.Invoke();
        }
        public void noTour()
        {
            RichPresence rpc = new RichPresence()
            {
                Details = "Frei wie der Wind",

                Assets = new Assets()
                {
                    LargeImageKey = "truck-icon",
                    LargeImageText = "Beyond the limits",
                    SmallImageKey = "vtcm-logo"
                }
            };
            client.SetPresence(rpc);
            client.Invoke();
        }
    }
}

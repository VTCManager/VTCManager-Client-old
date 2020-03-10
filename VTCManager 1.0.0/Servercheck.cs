using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;



namespace VTCManager_1._0._0
{
    
    class Servercheck
    {
        public bool WS_Check()
        {
                PingReply pingReply;
                using (var ping = new Ping())
                    pingReply = ping.Send("vtc.northwestvideo.de");
                return pingReply.Status == IPStatus.Success;

        }

        public bool DB_Check()
        {
            PingReply pingReply;
            using (var ping = new Ping())
                pingReply = ping.Send("51.89.14.36");
            return pingReply.Status == IPStatus.Success;

        }
    }
}

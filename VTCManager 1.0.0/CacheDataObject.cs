using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTCManager_1._0._0
{
    [Serializable]
    public class CacheDataObject
    {
        private string m_oSaveJobID;
        private string m_ofirst_start;
        private string m_otruckersmp_server;
        private string m_ospeed_mode;

        public string SaveJobID
        {
            get =>
                this.m_oSaveJobID;
            set =>
                this.m_oSaveJobID = value;
        }
        public string first_start
        {
            get =>
                this.m_ofirst_start;
            set =>
                this.m_ofirst_start = value;
        }
        public string truckersmp_server
        {
            get =>
                this.m_otruckersmp_server;
            set =>
                this.m_otruckersmp_server = value;
        }
        public string speed_mode
        {
            get =>
                this.m_ospeed_mode;
            set =>
                this.m_ospeed_mode = value;
        }
    }
}

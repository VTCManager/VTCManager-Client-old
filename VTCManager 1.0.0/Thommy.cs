using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTCManager_1._0._0
{
    class Thommy
    {
        public static string MeineVersion;
        public string Aktuelle_Version_lesen()
        {
            Utilities util = new Utilities();;
            MeineVersion = util.Reg_Lesen("TruckersMP_Autorun", "Version");
            return MeineVersion;
        }



    }
}

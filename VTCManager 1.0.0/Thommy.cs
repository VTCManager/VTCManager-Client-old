using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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



        public void Sende_TollGate(string authcode, long payment, int tournummer)
        {
            var request = (HttpWebRequest)WebRequest.Create("http://vtc.zwpc.de/tollgate.php");
            var postData = "authcode=" + authcode;
            postData += "&payment=" + payment;
            postData += "&tourid=" + tournummer;

            var data = Encoding.ASCII.GetBytes(postData);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
        }

    }
}

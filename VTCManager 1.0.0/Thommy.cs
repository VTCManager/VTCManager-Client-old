using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VTCManager_1._0._0
{
    class Thommy
    {
        public static string MeineVersion;
        private API api = new API();

        public string Aktuelle_Version_lesen()
        {
            Utilities util = new Utilities();
            MeineVersion = util.Reg_Lesen("TruckersMP_Autorun", "Version");
            return MeineVersion;
        }



        public void Sende_TollGate(string authcode, float payment, int tournummer)
        {
            var request = (HttpWebRequest)WebRequest.Create(this.api.api_server + this.api.tollgate_path);
            var postData = "authcode=" + authcode.ToString();
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

        public void Sende_Refuel(string authcode, float payment, string tournummer)
        {
            var request = (HttpWebRequest)WebRequest.Create("http://vtc.zwpc.de/tankkosten.php");
            var postData = "authcode=" + authcode.ToString();
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


        public void Sende_Faehre(string authcode, float payment, int tournummer)
        {

        }

    }
}

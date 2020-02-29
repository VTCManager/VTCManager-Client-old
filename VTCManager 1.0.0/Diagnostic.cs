using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VTCManager_1._0._0
{
    public partial class Diagnostic : Form
    {
        public Diagnostic()
        {
            InitializeComponent();
        }

        private void Diagnostic_Load(object sender, EventArgs e)
        {
            SendEMail();
        }


        public void SendEMail()
        {
            try
            {
                string _to = "vtc_diag@web.de";
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                message.To.Add(_to);
                message.Subject = "DiagnosticFile von ";
                message.From = new System.Net.Mail.MailAddress("vtc_diag@web.de");
                message.Body = "Test Nachricht";
                message.Body += listAll();
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.web.de")
                {
                    Credentials = new NetworkCredential("vtc_diag", "VtcDiagnistic")
                };
                smtp.Send(message);

                Console.WriteLine("Nachricht wurde versand an (" + _to + ")");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Nachricht konnte nicht versendet werden: " + ex);
            }
        }



        private static string listAll()
        {
            Utilities util = new Utilities();
            string startPfad_ETS = util.Reg_Lesen("TruckersMP_Autorun", "ETS2_Pfad");
            //string startPfad_ATS = util.Reg_Lesen("TruckersMP_Autorun", "ATS_Pfad");
            
            try
            {
                foreach (string file in Directory.EnumerateDirectories(startPfad_ETS, "*.*", SearchOption.AllDirectories))
                {
                    return file;
                }
            } catch (Exception ex)
            {
                return ex.Message;
            }
            return null;
            
        }


    }
}

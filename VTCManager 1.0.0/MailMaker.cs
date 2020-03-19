using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;


namespace VTCManager_1._0._0
{
    class MailMaker
    {
        public MailMaker()
        {
        }

        /// <summary>
        /// Stellt die Mail-Adresse des Absenders dar
        /// </summary>        
        public string Absender
        {
            get;
            set;
        }

        /// <summary>
        /// Stellt eine Auflistung der Empfänger dar
        /// </summary>
        public List<string> Empfänger
        {
            get;
            set;
        }

        /// <summary>
        /// Stellt eine Auflistung der Empfänger dar, die die Mail als Kopie erhalten
        /// </summary>
        public List<string> Kopie
        {
            get;
            set;
        }

        /// <summary>
        /// Stellt eine Auflistung der Empfänger dar, die die Mail als Blindkopie erhalten
        /// </summary>
        public List<string> Blindkopie
        {
            get;
            set;
        }

        /// <summary>
        /// Stellt eine Auflistung von den Dateinamen der Anhänge dar
        /// </summary>
        public List<Attachment> Anhänge
        {
            get;
            set;
        }

        /// <summary>
        /// Stellt den Betreff der Mail dar
        /// </summary>
        public string Betreff
        {
            get;
            set;
        }

        /// <summary>
        /// Stellt den Text der Nachricht dar
        /// </summary>
        public string Nachricht
        {
            get;
            set;
        }

        /// <summary>
        /// Stellt den Usernamen dar
        /// </summary>        
        public string Username
        {
            get;
            set;
        }

        /// <summary>
        /// Stellt das Passwort dar
        /// </summary>
        public string Passwort
        {
            private get;
            set;
        }

        /// <summary>
        /// Stellt den Namen des SMTP-Servers dar
        /// </summary>
        public string Servername
        {
            get;
            set;
        }

        /// <summary>
        /// Stellt den Port für die Mail-Übermittlung dar
        /// </summary>
        public string Port
        {
            get;
            set;
        }

        /// <summary>
        /// Versendet die Mail mit den festgelegten Eigenschaften in der Klasse
        /// </summary>
        public void Send()
        {
            MailMessage Email = new MailMessage();

            MailAddress Sender = new MailAddress(Absender);
            Email.From = Sender; // Absender einstellen

            // Empfänger hinzufügen
            foreach (string empf in Empfänger)
                Email.To.Add(empf);

            // Kopie-Empfänger hinzufügen (wenn vorhanden)
            if (Kopie.Count != 0)
                foreach (string kopie in Kopie)
                    Email.CC.Add(kopie);

            // Blindkopie-Empfänger hinzufügen (wenn vorhanden)
            if (Blindkopie.Count != 0)
                foreach (string blindkopie in Blindkopie)
                    Email.Bcc.Add(blindkopie);

            // Anhänge hinzufügen (wenn vorhanden)
            if (Anhänge.Count != 0)
                foreach (Attachment anhang in Anhänge)
                    Email.Attachments.Add(anhang);

            Email.Subject = Betreff; // Betreff hinzufügen

            Email.Body = Nachricht; // Nachrichtentext hinzufügen

            SmtpClient MailClient = new SmtpClient(Servername, int.Parse(Port)); // Postausgangsserver definieren

            string UserName = Username;
            string Password = Passwort;
            System.Net.NetworkCredential Credentials = new System.Net.NetworkCredential(UserName, Password);

            MailClient.Credentials = Credentials; // Anmeldeinformationen setzen

            MailClient.Send(Email); // Email senden
        }
    }
}

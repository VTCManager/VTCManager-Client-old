using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Net;
using System.Windows.Forms;
using System.IO;

namespace VTCManager_1._0._0
{

    public class Login : Form
    {
        private API api = new API();
        private SettingsManager preferences = new SettingsManager();
        private Dictionary<string, string> settingsDictionary = new Dictionary<string, string>();
        public string userID = "0";
        public string userCompany = "0";
        public string jobID = "0";
        public string authCode = "false";
        public Dictionary<string, string> lastJobDictionary = new Dictionary<string, string>();
        private Panel login_panel;
        private Label label2;
        private Label label1;
        private TextBox password_input;
        private TextBox email_input;
        private Button submit_login;
        private Label version_text;
        private int driven_tours;
        private int bank_balance;
        private String username;
        private string profile_picture;
        private int version_int;
        private ProgressBar progressBardownload;
        private string downloaddirectory;
        private string userFolder;
        private string conv_fileName;
        private int fileName_int;
        private Translation translation;
        private SettingsManager settings;
        private String first_start;
        // Edit by Thommy
        // Auf Öffentlichkeit prüfen || true = Öffentlich || false = keine Prüfung
        private bool oeffentlich = false;


        public Login() {
            
            CultureInfo ci = CultureInfo.InstalledUICulture;
            this.translation = new Translation(ci.DisplayName);
            this.InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(this.Main_FormClosing);
        }
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
        private void InitializeComponent()
        {
            this.settings = new SettingsManager();
            this.settings.CreateCache();
            this.settings.LoadJobID();
            if (this.settings.Cache.first_start == "true" || string.IsNullOrEmpty(this.settings.Cache.first_start) == true) {
                this.first_start = "false";
                this.settings.Cache.first_start = "false";
                this.settings.SaveJobID();
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;
                result = MessageBox.Show(translation.update_message, translation.update_caption, buttons);
            }
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(Main));
            this.login_panel = new Panel();
            this.submit_login = new Button();
            this.label2 = new Label(); //Email Label
            this.label1 = new Label(); //Passwirt label
            this.password_input = new TextBox();
            this.email_input = new TextBox();
            this.version_text = new Label();
            this.progressBardownload = new ProgressBar();
            this.login_panel.SuspendLayout();
            this.SuspendLayout();
            //this.login_panel.Controls.Add((Control)this.logo);
            this.login_panel.Controls.Add((Control)this.submit_login);
            this.login_panel.Controls.Add((Control)this.label2);
            this.login_panel.Controls.Add((Control)this.label1);
            this.login_panel.Controls.Add((Control)this.password_input);
            this.login_panel.Controls.Add((Control)this.email_input);
            this.login_panel.Location = new Point(4, 9);
            this.login_panel.Name = "login_panel";
            this.login_panel.Size = new Size(375, 377);
            this.login_panel.TabIndex = 6;
            //this.logo.BackgroundImage = (Image)Resources.logo;
            //this.logo.BackgroundImageLayout = ImageLayout.Zoom;
            //this.logo.Location = new Point(60, 75);
            // this.logo.Name = "logo";
            //this.logo.Size = new Size(250, 66);
            //this.logo.TabIndex = 6;
            //this.logo.TabStop = false;
            this.submit_login.FlatAppearance.BorderColor = Color.FromArgb(204, 204, 204);
            this.submit_login.FlatAppearance.MouseDownBackColor = Color.FromArgb(150, 150, 150);
            this.submit_login.FlatAppearance.MouseOverBackColor = Color.FromArgb(204, 204, 204);
            this.submit_login.FlatStyle = FlatStyle.Flat;
            this.submit_login.Location = new Point(60, 274);
            this.submit_login.Name = "submit_login";
            this.submit_login.Size = new Size(250, 25);
            this.submit_login.TabIndex = 4;
            this.submit_login.Text = translation.login;
            this.submit_login.UseVisualStyleBackColor = true;
            this.submit_login.Click += new EventHandler(this.submit_login_Click);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(57, 151);
            this.label2.Name = "label2";
            this.label2.Size = new Size(53, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = translation.password;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(57, 100);
            this.label1.Name = "label1";
            this.label1.Size = new Size(131, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = this.translation.login_username;
            this.password_input.Location = new Point(60, 167);
            this.password_input.Name = "password_input";
            this.password_input.Size = new Size(250, 22);
            this.password_input.TabIndex = 1;
            this.password_input.UseSystemPasswordChar = true;
            this.password_input.KeyUp += new KeyEventHandler(this.password_input_KeyPress);
            this.email_input.Location = new Point(60, 116);
            this.email_input.Name = "email_input";
            this.email_input.Size = new Size(250, 22);
            this.email_input.TabIndex = 0;
            this.version_text.AutoSize = true;
            this.version_text.Location = new Point(280, 3);
            this.version_text.Name = "version_text";
            this.version_text.Size = new Size(101, 13);
            this.version_text.TabIndex = 7;
            this.version_text.Text = "Version: 1.1.1 Alpha";
            this.version_text.TextAlign = ContentAlignment.MiddleRight;
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.ClientSize = new Size(384, 411);
            this.Controls.Add((Control)this.login_panel);
            this.Font = new Font("Segoe UI", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            //this.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
            this.MaximizeBox = false;
            this.Name = nameof(Main);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "VTCManager";
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Load += new EventHandler(this.Main_Load);
            this.Resize += new EventHandler(this.Main_Resize);
            this.login_panel.ResumeLayout(false);
            this.login_panel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        public static string Authenticate(string email, string password)
        {
            API api = new API();
            return api.HTTPSRequestPost(api.api_server + api.login_path, new Dictionary<string, string>()
      {
        {
          "username",
          email
        },
        {
          "password",
          password
        }
      }, true).ToString();
        }
        private void submit_login_Click(object sender, EventArgs e)
        {
            this.login(this.email_input.Text, Utilities.Sha256(this.password_input.Text));
        }
        private void password_input_KeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Return)
                return;
            this.login(this.email_input.Text, Utilities.Sha256(this.password_input.Text));
            e.Handled = true;
        }
        private void login(string theEmail, string thePassword)
        {
            this.authCode = Authenticate(theEmail, thePassword);
            if (this.authCode.Equals("Error: PIN_Invalid") || this.authCode.Equals("Error: User_Invalid") || this.authCode.Equals("Error: Serverside"))
            {
                this.login_panel.Visible = true;
                int num = (int)MessageBox.Show(translation.login_failed);
            }
            else
            {
                this.preferences.Config.SaveLoginData = "yes";
                this.preferences.Config.Account = theEmail;
                this.preferences.Config.Password = thePassword;
                this.preferences.SaveConfig();
                this.login_panel.Visible = false;
                string[] strArray = this.api.HTTPSRequestPost(this.api.api_server + this.api.load_data_path, new Dictionary<string, string>()
            {
          {
            "authcode",
            this.authCode
          }
        }, true).ToString().Split(',');
                if (this.authCode.Equals("Error: PIN_Invalid") || this.authCode.Equals("Error: User_Invalid") || this.authCode.Equals("Error: Serverside"))
                {
                    this.login_panel.Visible = true;
                    MessageBox.Show(translation.login_failed);
                }
                if (string.IsNullOrEmpty(this.authCode))
                {
                    Application.Exit();
                }

                this.userID = strArray[0];
                this.userCompany = strArray[1];
                this.username = strArray[2];
                this.profile_picture = strArray[3];
                this.driven_tours = Convert.ToInt32(strArray[4]);
                this.bank_balance = Convert.ToInt32(strArray[5]);
                this.Hide();

                Main Mainwindow = new Main(this.authCode, this.username, this.driven_tours, this.bank_balance, false, this.userCompany);
                Mainwindow.ShowDialog();
            }
        }
        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate {
                double bytesIn = double.Parse(e.BytesReceived.ToString());
                double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
                double percentage = bytesIn / totalBytes * 100;
                label2.Text = "Downloaded " + e.BytesReceived + " of " + e.TotalBytesToReceive;
                progressBardownload.Value = int.Parse(Math.Truncate(percentage).ToString());
            });
        }
        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate {
                label2.Text = "Completed";
            });
        }

        private void Main_Load(object sender, EventArgs e)
        {
            /*
            this.version_text.Text = "Version: 1.1.1";
            version_int = 111;
            string fileName = this.api.HTTPSRequestPost("https://vtc.northwestvideo.de/api/app/download.php", new Dictionary<string, string>()
      {
        {
          "version",
          "1.1.1"
        }
      }, true);
            if (string.IsNullOrEmpty(fileName))
            {
                Application.Exit();
            }
            conv_fileName = fileName.Replace(".", string.Empty);
            fileName_int = System.Convert.ToInt32(conv_fileName);

            // Prüfen ob es eine Öffentliche Version ist:
            if (oeffentlich == true)
            {
                if (fileName_int != version_int)
                {
                    switch (MessageBox.Show(translation.update_part1 + fileName + translation.update_part2, translation.update_avail_window, MessageBoxButtons.YesNo))
                    {
                        case DialogResult.Yes:
                            this.first_start = "false";
                            this.settings.Cache.first_start = this.first_start;
                            this.settings.SaveJobID();
                            this.userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                            this.downloaddirectory = Path.Combine(userFolder, ".vtcmanager");
                            WebClient webClient = new WebClient();
                            webClient.DownloadFile("http://vtc.northwestvideo.de/api/app/VTCMInstaller-latest.exe", this.downloaddirectory + "/VTCMInstaller-latest.exe");
                            Process ExternalProcess = new Process();
                            ExternalProcess.StartInfo.FileName = this.downloaddirectory + "/VTCMInstaller-latest.exe";
                            ExternalProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                            ExternalProcess.Start();
                            Application.Exit();
                            break;
                        case DialogResult.No:
                            Application.Exit();
                            break;
                    }
                }
            }
            */

            //this.preferences.CreateConfig();
            this.preferences.LoadConfig();
            if (!(this.preferences.Config.SaveLoginData == "yes"))
                return;
            this.login(this.preferences.Config.Account, this.preferences.Config.Password);
            
        }
        private void Main_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
            else
            {
                if (this.WindowState != FormWindowState.Normal)
                    return;
            }
        }
    }
}

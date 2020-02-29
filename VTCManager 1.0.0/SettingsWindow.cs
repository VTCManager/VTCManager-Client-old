using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VTCManager_1._0._0
{
    class SettingsWindow : Form
    {
        private ComboBox comboBox1;
        private Label label1;
        private SettingsManager data;
        private Translation translation;
        private CheckBox speed_setup_box;
        private Button save_button;
        private GroupBox groupBox1;
        private GroupBox btn_TruckersMP_suchen;
        private Label label3;
        private Button button1;
        private string selected_server_tm;
        private System.Windows.Forms.OpenFileDialog tmp_Trucker;
        private TextBox truckersMP_Pfad_TextBox;
        //private System.Windows.Forms.OpenFileDialog openFileDialog1;

        public SettingsWindow() {
            this.data = new SettingsManager();
            this.data.LoadJobID();
            CultureInfo ci = CultureInfo.InstalledUICulture;
            this.translation = new Translation(ci.DisplayName);

            this.InitializeComponent();
            this.comboBox1.Text = this.data.Cache.truckersmp_server;
            this.speed_setup_box.Text = translation.speed_setup_box;
            this.Text = translation.settings_window;
            if (this.data.Cache.speed_mode == "mph")
            {
                this.speed_setup_box.Checked = true;
            }
            else
            {
                this.speed_setup_box.Checked = false;
            }
        }
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsWindow));
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.speed_setup_box = new System.Windows.Forms.CheckBox();
            this.save_button = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_TruckersMP_suchen = new System.Windows.Forms.GroupBox();
            this.truckersMP_Pfad_TextBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tmp_Trucker = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            this.btn_TruckersMP_suchen.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Simulation 1",
            "Simulation 2",
            "Arcade",
            "EU Promods 1",
            "EU Promods 2"});
            this.comboBox1.Location = new System.Drawing.Point(26, 42);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.Text = "Simulation 1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "TruckersMP-Server:";
            // 
            // speed_setup_box
            // 
            this.speed_setup_box.AutoSize = true;
            this.speed_setup_box.Location = new System.Drawing.Point(26, 69);
            this.speed_setup_box.Name = "speed_setup_box";
            this.speed_setup_box.Size = new System.Drawing.Size(144, 17);
            this.speed_setup_box.TabIndex = 2;
            this.speed_setup_box.Text = "Geschwindigkeit in mph?";
            this.speed_setup_box.UseVisualStyleBackColor = true;
            // 
            // save_button
            // 
            this.save_button.Location = new System.Drawing.Point(469, 546);
            this.save_button.Name = "save_button";
            this.save_button.Size = new System.Drawing.Size(149, 41);
            this.save_button.TabIndex = 3;
            this.save_button.Text = "Einstellungen Speichern...";
            this.save_button.UseVisualStyleBackColor = true;
            this.save_button.Click += new System.EventHandler(this.save_button_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.speed_setup_box);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(271, 111);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Server Einstellungen";
            // 
            // btn_TruckersMP_suchen
            // 
            this.btn_TruckersMP_suchen.Controls.Add(this.truckersMP_Pfad_TextBox);
            this.btn_TruckersMP_suchen.Controls.Add(this.button1);
            this.btn_TruckersMP_suchen.Controls.Add(this.label3);
            this.btn_TruckersMP_suchen.Location = new System.Drawing.Point(12, 134);
            this.btn_TruckersMP_suchen.Name = "btn_TruckersMP_suchen";
            this.btn_TruckersMP_suchen.Size = new System.Drawing.Size(270, 84);
            this.btn_TruckersMP_suchen.TabIndex = 6;
            this.btn_TruckersMP_suchen.TabStop = false;
            this.btn_TruckersMP_suchen.Text = "TruckersMP Einstellungen";
            // 
            // truckersMP_Pfad_TextBox
            // 
            this.truckersMP_Pfad_TextBox.Location = new System.Drawing.Point(7, 37);
            this.truckersMP_Pfad_TextBox.Name = "truckersMP_Pfad_TextBox";
            this.truckersMP_Pfad_TextBox.Size = new System.Drawing.Size(228, 20);
            this.truckersMP_Pfad_TextBox.TabIndex = 10;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(241, 36);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(25, 22);
            this.button1.TabIndex = 9;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Pfad zu TruckersMP:";
            // 
            // tmp_Trucker
            // 
            this.tmp_Trucker.FileName = "T";
            // 
            // SettingsWindow
            // 
            this.ClientSize = new System.Drawing.Size(630, 599);
            this.Controls.Add(this.btn_TruckersMP_suchen);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.save_button);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Einstellungen";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SettingsWindow_FormClosed);
            this.Load += new System.EventHandler(this.SettingsWindow_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.btn_TruckersMP_suchen.ResumeLayout(false);
            this.btn_TruckersMP_suchen.PerformLayout();
            this.ResumeLayout(false);

        }

        private void save_button_Click(object sender, EventArgs e)
        {
            Utilities util = new Utilities();

            if (this.comboBox1.Text == "Simulation 1")
            {
                this.selected_server_tm = "sim1";
                this.data.Cache.truckersmp_server = this.selected_server_tm;
                // Edit by Thommy

                util.Reg_Schreiben("verkehr_SERVER", "sim1");

            }
            else if (this.comboBox1.Text == "Simulation 2")
            {
                this.selected_server_tm = "sim2";
                this.data.Cache.truckersmp_server = this.selected_server_tm;
                util.Reg_Schreiben("verkehr_SERVER", "sim2");

            }
            else if (this.comboBox1.Text == "Arcade")
            {
                this.selected_server_tm = "arc1";
                this.data.Cache.truckersmp_server = this.selected_server_tm;
                util.Reg_Schreiben("verkehr_SERVER", "arc1");
            }
            else if (this.comboBox1.Text == "EU Promods 1")
            {
                this.selected_server_tm = "eupromods1";
                this.data.Cache.truckersmp_server = this.selected_server_tm;
                util.Reg_Schreiben("verkehr_SERVER", "eupromods1");
            }
            else if (this.comboBox1.Text == "EU Promods 2")
            {
                this.selected_server_tm = "eupromods2";
                this.data.Cache.truckersmp_server = this.selected_server_tm;
                util.Reg_Schreiben("verkehr_SERVER", "eupromods2");
            }
            if (speed_setup_box.Checked)
            {
                this.data.Cache.speed_mode = "mph";
                util.Reg_Schreiben("speed_MODE", "mph");
            }
            else
            {
                this.data.Cache.speed_mode = "kmh";
                util.Reg_Schreiben("speed_MODE", "kmh");
            }



            this.data.SaveJobID();

            //MessageBox.Show(translation.save_info);
            this.Close();
        }

        private void SettingsWindow_Load(object sender, EventArgs e)
        {
      
            Utilities util2 = new Utilities();
            var test = util2.Reg_Lesen("TruckersMP_Autorun", "TruckersMP_Pfad");
            if (test == "") {
                MessageBox.Show("Es fehlt der Pfad zu TruckersMP" + Environment.NewLine + "Bitte Korrigiere die Angabe dem folgenden Fenster!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 

            string wert27 = util2.Reg_Lesen("TruckersMP_Autorun", "verkehr_SERVER");
            string wert28 = util2.Reg_Lesen("TruckersMP_Autorun", "TruckersMP_Pfad");

            if (wert28 != null)
            {
                truckersMP_Pfad_TextBox.Text = wert28;
                truckersMP_Pfad_TextBox.Enabled = false;
            } else
            {
                truckersMP_Pfad_TextBox.Text = "";
            }

            // Server COMBO vorauswahl
            if(wert27 == null) { comboBox1.Text = "Simulation 1"; util2.Reg_Schreiben("verkehr_SERVER", "sim1"); }
            if (wert27 == "sim1") { comboBox1.Text = "Simulation 1"; }
            if (wert27 == "sim2") { comboBox1.Text = "Simulation 2"; }
            if (wert27 == "arc1") { comboBox1.Text = "Arcade 1"; }
            if (wert27 == "eupromods1") { comboBox1.Text = "EU Promods 1"; }
            if (wert27 == "eupromods2") { comboBox1.Text = "EU Promods 2"; }

            // Variablen abrufen von Main
            var link = Main.truckersMP_Link;

        }





        private void SettingsWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
       
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tmp_Trucker = new System.Windows.Forms.OpenFileDialog();
            tmp_Trucker.InitialDirectory =
            tmp_Trucker.Filter = "TruckersMP Launcher (launcher.exe)|launcher.exe|Alle Dateien (*.*)|*.*";
            if (tmp_Trucker.ShowDialog() == DialogResult.OK)
            {
                Utilities util = new Utilities();
                util.Reg_Schreiben("TruckersMP_Pfad", tmp_Trucker.FileName);
                truckersMP_Pfad_TextBox.Text = tmp_Trucker.FileName.ToString();
            }
                
        }

        private void checkBoxShowOverlay_CheckedChanged(object sender, EventArgs e)
        {
   
        }
    }
}

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
        private GroupBox groupBox2;
        private Label label2;
        private CheckBox checkBox1;
        private GroupBox groupBox3;
        private TextBox truckersMP_Pfad_TextBox;
        private Label label3;
        private FolderBrowserDialog trucker_MP_Browser_Dialog;
        private CheckBox truckers_autorun;
        private CheckBox truckersMP_Button_anzeigen;
        private Label label4;
        private string selected_server_tm;

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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.truckers_autorun = new System.Windows.Forms.CheckBox();
            this.truckersMP_Button_anzeigen = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.truckersMP_Pfad_TextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.trucker_MP_Browser_Dialog = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
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
            this.save_button.Location = new System.Drawing.Point(289, 537);
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
            this.groupBox1.Size = new System.Drawing.Size(222, 111);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Server Einstellungen";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Location = new System.Drawing.Point(12, 139);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(222, 100);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Design Einstellungen";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "(In Vorbereitung)";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(26, 28);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(79, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Dark-Mode";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.truckers_autorun);
            this.groupBox3.Controls.Add(this.truckersMP_Button_anzeigen);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.truckersMP_Pfad_TextBox);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(13, 246);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(370, 156);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "TruckersMP Einstellungen";
            // 
            // truckers_autorun
            // 
            this.truckers_autorun.AutoSize = true;
            this.truckers_autorun.Location = new System.Drawing.Point(13, 118);
            this.truckers_autorun.Name = "truckers_autorun";
            this.truckers_autorun.Size = new System.Drawing.Size(230, 17);
            this.truckers_autorun.TabIndex = 4;
            this.truckers_autorun.Text = "Truckers MP beim Start automatisch öffnen";
            this.truckers_autorun.UseVisualStyleBackColor = true;
            this.truckers_autorun.CheckedChanged += new System.EventHandler(this.truckers_autorun_CheckedChanged);
            // 
            // truckersMP_Button_anzeigen
            // 
            this.truckersMP_Button_anzeigen.AutoSize = true;
            this.truckersMP_Button_anzeigen.Location = new System.Drawing.Point(13, 94);
            this.truckersMP_Button_anzeigen.Name = "truckersMP_Button_anzeigen";
            this.truckersMP_Button_anzeigen.Size = new System.Drawing.Size(167, 17);
            this.truckersMP_Button_anzeigen.TabIndex = 3;
            this.truckersMP_Button_anzeigen.Text = "Truckers MP Button anzeigen";
            this.truckersMP_Button_anzeigen.UseVisualStyleBackColor = true;
            this.truckersMP_Button_anzeigen.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(151, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "(Wird automatisch eingefügt !) ";
            // 
            // truckersMP_Pfad_TextBox
            // 
            this.truckersMP_Pfad_TextBox.Location = new System.Drawing.Point(10, 36);
            this.truckersMP_Pfad_TextBox.Name = "truckersMP_Pfad_TextBox";
            this.truckersMP_Pfad_TextBox.Size = new System.Drawing.Size(354, 20);
            this.truckersMP_Pfad_TextBox.TabIndex = 1;
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
            // trucker_MP_Browser_Dialog
            // 
            this.trucker_MP_Browser_Dialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // SettingsWindow
            // 
            this.ClientSize = new System.Drawing.Size(744, 599);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.save_button);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Einstellungen";
            this.Load += new System.EventHandler(this.SettingsWindow_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        private void save_button_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.Text == "Simulation 1")
            {
                this.selected_server_tm = "sim1";
                this.data.Cache.truckersmp_server = this.selected_server_tm;
                this.label2.Text = "Simulation 1";
            }
            else if (this.comboBox1.Text == "Simulation 2")
            {
                this.selected_server_tm = "sim2";
                this.data.Cache.truckersmp_server = this.selected_server_tm;
                

            }
            else if (this.comboBox1.Text == "Arcade")
            {
                this.selected_server_tm = "arc1";
                this.data.Cache.truckersmp_server = this.selected_server_tm;
            }
            else if (this.comboBox1.Text == "EU Promods 1")
            {
                this.selected_server_tm = "eupromods1";
                this.data.Cache.truckersmp_server = this.selected_server_tm;
            }
            else if (this.comboBox1.Text == "EU Promods 2")
            {
                this.selected_server_tm = "eupromods2";
                this.data.Cache.truckersmp_server = this.selected_server_tm;
            }
            if (speed_setup_box.Checked)
            {
                this.data.Cache.speed_mode = "mph";
            }
            else
            {
                this.data.Cache.speed_mode = "kmh";
            }
            this.data.SaveJobID();
            MessageBox.Show(translation.save_info);

            // Edit by Thommy



            this.Close();
        }

        private void SettingsWindow_Load(object sender, EventArgs e)
        {
            Utilities util2 = new Utilities();
            string wert99 = util2.Reg_Lesen("TruckersMP_Autorun", "autorun");
            string wert98 = util2.Reg_Lesen("TruckersMP_Autorun", "button_show");
            if(wert99 == "1")
            {
                truckers_autorun.CheckState = CheckState.Checked;
            } else
            {
                truckers_autorun.CheckState = CheckState.Unchecked;
                
            }


            // Button anzeigen vorauswahl
            if (wert98 == "1")
            {
                truckersMP_Button_anzeigen.CheckState = CheckState.Checked;
            }
            else
            {
                truckersMP_Button_anzeigen.CheckState = CheckState.Unchecked;
            }


            // Variablen abrufen von Main
            var link = Main.truckersMP_Link;

            // Ausgabe in Settings
            truckersMP_Pfad_TextBox.Text = link;


            // Checkbox autorun laden


        }

        private void truckers_autorun_CheckedChanged(object sender, EventArgs e)
        {
            if(truckers_autorun.CheckState == CheckState.Checked)
            {
                Utilities util = new Utilities();
                util.Reg_Schreiben("button_show", "1");
            } else
            {
                Utilities util = new Utilities();
                util.Reg_Schreiben("button_show", "0");
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (truckersMP_Button_anzeigen.CheckState == CheckState.Checked)
            {
                Utilities util = new Utilities();
                util.Reg_Schreiben("autorun", "1");
            }
            else
            {
                Utilities util = new Utilities();
                util.Reg_Schreiben("autorun", "0");
            }
        }
    }
}

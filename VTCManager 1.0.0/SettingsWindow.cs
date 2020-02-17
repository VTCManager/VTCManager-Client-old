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
        private CheckBox truckers_autorun;
        private GroupBox group_Overlay;
        private ComboBox combo_Bildschirme;
        private Label label6;
        private NumericUpDown num_Overlay_Transparenz;
        private Label label5;
        private GroupBox groupBox4;
        private CheckBox chk_GANG;
        private CheckBox chk_KUPPLUNG;
        private CheckBox chk_BREMSE;
        private CheckBox chk_GAS;
        private CheckBox chk_RPM_ANZEIGE;
        private Button button1;
        private string selected_server_tm;
        private System.Windows.Forms.OpenFileDialog tmp_Trucker;
        private TextBox truckersMP_Pfad_TextBox;
        private Label label4;
        private NumericUpDown traffic_Update_Intervall;
        private GroupBox groupBox_AntiAFK;
        private TextBox txt_Anti_AFK_Text;
        private CheckBox chk_antiafk_on_off;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;

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
            this.truckers_autorun = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.group_Overlay = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.num_Overlay_Transparenz = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.combo_Bildschirme = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.traffic_Update_Intervall = new System.Windows.Forms.NumericUpDown();
            this.chk_RPM_ANZEIGE = new System.Windows.Forms.CheckBox();
            this.chk_GANG = new System.Windows.Forms.CheckBox();
            this.chk_KUPPLUNG = new System.Windows.Forms.CheckBox();
            this.chk_BREMSE = new System.Windows.Forms.CheckBox();
            this.chk_GAS = new System.Windows.Forms.CheckBox();
            this.tmp_Trucker = new System.Windows.Forms.OpenFileDialog();
            this.groupBox_AntiAFK = new System.Windows.Forms.GroupBox();
            this.txt_Anti_AFK_Text = new System.Windows.Forms.TextBox();
            this.chk_antiafk_on_off = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.btn_TruckersMP_suchen.SuspendLayout();
            this.group_Overlay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_Overlay_Transparenz)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.traffic_Update_Intervall)).BeginInit();
            this.groupBox_AntiAFK.SuspendLayout();
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
            this.btn_TruckersMP_suchen.Controls.Add(this.truckers_autorun);
            this.btn_TruckersMP_suchen.Controls.Add(this.label3);
            this.btn_TruckersMP_suchen.Location = new System.Drawing.Point(12, 134);
            this.btn_TruckersMP_suchen.Name = "btn_TruckersMP_suchen";
            this.btn_TruckersMP_suchen.Size = new System.Drawing.Size(270, 143);
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
            // truckers_autorun
            // 
            this.truckers_autorun.AutoSize = true;
            this.truckers_autorun.Location = new System.Drawing.Point(6, 63);
            this.truckers_autorun.Name = "truckers_autorun";
            this.truckers_autorun.Size = new System.Drawing.Size(129, 17);
            this.truckers_autorun.TabIndex = 4;
            this.truckers_autorun.Text = "TruckersMP Autostart";
            this.truckers_autorun.UseVisualStyleBackColor = true;
            this.truckers_autorun.CheckedChanged += new System.EventHandler(this.truckers_autorun_CheckedChanged);
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
            // group_Overlay
            // 
            this.group_Overlay.Controls.Add(this.label6);
            this.group_Overlay.Controls.Add(this.num_Overlay_Transparenz);
            this.group_Overlay.Controls.Add(this.label5);
            this.group_Overlay.Controls.Add(this.combo_Bildschirme);
            this.group_Overlay.Location = new System.Drawing.Point(289, 12);
            this.group_Overlay.Name = "group_Overlay";
            this.group_Overlay.Size = new System.Drawing.Size(321, 111);
            this.group_Overlay.TabIndex = 7;
            this.group_Overlay.TabStop = false;
            this.group_Overlay.Text = "Overlay Einstellungen";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(41, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Transparenz:";
            // 
            // num_Overlay_Transparenz
            // 
            this.num_Overlay_Transparenz.Location = new System.Drawing.Point(116, 56);
            this.num_Overlay_Transparenz.Name = "num_Overlay_Transparenz";
            this.num_Overlay_Transparenz.Size = new System.Drawing.Size(59, 20);
            this.num_Overlay_Transparenz.TabIndex = 2;
            this.num_Overlay_Transparenz.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.num_Overlay_Transparenz.ValueChanged += new System.EventHandler(this.num_Overlay_Transparenz_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Gaming-Monitor:";
            // 
            // combo_Bildschirme
            // 
            this.combo_Bildschirme.FormattingEnabled = true;
            this.combo_Bildschirme.Location = new System.Drawing.Point(116, 28);
            this.combo_Bildschirme.Name = "combo_Bildschirme";
            this.combo_Bildschirme.Size = new System.Drawing.Size(195, 21);
            this.combo_Bildschirme.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.traffic_Update_Intervall);
            this.groupBox4.Controls.Add(this.chk_RPM_ANZEIGE);
            this.groupBox4.Controls.Add(this.chk_GANG);
            this.groupBox4.Controls.Add(this.chk_KUPPLUNG);
            this.groupBox4.Controls.Add(this.chk_BREMSE);
            this.groupBox4.Controls.Add(this.chk_GAS);
            this.groupBox4.Location = new System.Drawing.Point(289, 134);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(321, 143);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Anzeige-Einstellungen";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(163, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Reload Verkehr nach Sekunden:";
            // 
            // traffic_Update_Intervall
            // 
            this.traffic_Update_Intervall.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.traffic_Update_Intervall.Location = new System.Drawing.Point(186, 100);
            this.traffic_Update_Intervall.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.traffic_Update_Intervall.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.traffic_Update_Intervall.Name = "traffic_Update_Intervall";
            this.traffic_Update_Intervall.Size = new System.Drawing.Size(65, 26);
            this.traffic_Update_Intervall.TabIndex = 5;
            this.traffic_Update_Intervall.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.traffic_Update_Intervall.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.traffic_Update_Intervall.ValueChanged += new System.EventHandler(this.traffic_Update_Intervall_ValueChanged);
            // 
            // chk_RPM_ANZEIGE
            // 
            this.chk_RPM_ANZEIGE.AutoSize = true;
            this.chk_RPM_ANZEIGE.Location = new System.Drawing.Point(158, 44);
            this.chk_RPM_ANZEIGE.Name = "chk_RPM_ANZEIGE";
            this.chk_RPM_ANZEIGE.Size = new System.Drawing.Size(96, 17);
            this.chk_RPM_ANZEIGE.TabIndex = 4;
            this.chk_RPM_ANZEIGE.Text = "R/PM Anzeige";
            this.chk_RPM_ANZEIGE.UseVisualStyleBackColor = true;
            this.chk_RPM_ANZEIGE.CheckedChanged += new System.EventHandler(this.chk_RPM_ANZEIGE_CheckedChanged);
            // 
            // chk_GANG
            // 
            this.chk_GANG.AutoSize = true;
            this.chk_GANG.Location = new System.Drawing.Point(158, 23);
            this.chk_GANG.Name = "chk_GANG";
            this.chk_GANG.Size = new System.Drawing.Size(93, 17);
            this.chk_GANG.TabIndex = 3;
            this.chk_GANG.Text = "Gang-Anzeige";
            this.chk_GANG.UseVisualStyleBackColor = true;
            this.chk_GANG.CheckedChanged += new System.EventHandler(this.chk_GANG_CheckedChanged);
            // 
            // chk_KUPPLUNG
            // 
            this.chk_KUPPLUNG.AutoSize = true;
            this.chk_KUPPLUNG.Location = new System.Drawing.Point(22, 69);
            this.chk_KUPPLUNG.Name = "chk_KUPPLUNG";
            this.chk_KUPPLUNG.Size = new System.Drawing.Size(102, 17);
            this.chk_KUPPLUNG.TabIndex = 2;
            this.chk_KUPPLUNG.Text = "Kupplungspedal";
            this.chk_KUPPLUNG.UseVisualStyleBackColor = true;
            this.chk_KUPPLUNG.CheckedChanged += new System.EventHandler(this.chk_KUPPLUNG_CheckedChanged);
            // 
            // chk_BREMSE
            // 
            this.chk_BREMSE.AutoSize = true;
            this.chk_BREMSE.Location = new System.Drawing.Point(22, 46);
            this.chk_BREMSE.Name = "chk_BREMSE";
            this.chk_BREMSE.Size = new System.Drawing.Size(81, 17);
            this.chk_BREMSE.TabIndex = 1;
            this.chk_BREMSE.Text = "Bremspedal";
            this.chk_BREMSE.UseVisualStyleBackColor = true;
            this.chk_BREMSE.CheckedChanged += new System.EventHandler(this.chk_BREMSE_CheckedChanged);
            // 
            // chk_GAS
            // 
            this.chk_GAS.AutoSize = true;
            this.chk_GAS.Location = new System.Drawing.Point(22, 23);
            this.chk_GAS.Name = "chk_GAS";
            this.chk_GAS.Size = new System.Drawing.Size(71, 17);
            this.chk_GAS.TabIndex = 0;
            this.chk_GAS.Text = "Gaspedal";
            this.chk_GAS.UseVisualStyleBackColor = true;
            this.chk_GAS.CheckedChanged += new System.EventHandler(this.chk_GAS_CheckedChanged);
            // 
            // tmp_Trucker
            // 
            this.tmp_Trucker.FileName = "T";
            // 
            // groupBox_AntiAFK
            // 
            this.groupBox_AntiAFK.Controls.Add(this.chk_antiafk_on_off);
            this.groupBox_AntiAFK.Controls.Add(this.txt_Anti_AFK_Text);
            this.groupBox_AntiAFK.Location = new System.Drawing.Point(12, 284);
            this.groupBox_AntiAFK.Name = "groupBox_AntiAFK";
            this.groupBox_AntiAFK.Size = new System.Drawing.Size(598, 79);
            this.groupBox_AntiAFK.TabIndex = 9;
            this.groupBox_AntiAFK.TabStop = false;
            this.groupBox_AntiAFK.Text = "Anti - AFK";
            // 
            // txt_Anti_AFK_Text
            // 
            this.txt_Anti_AFK_Text.Location = new System.Drawing.Point(10, 20);
            this.txt_Anti_AFK_Text.Name = "txt_Anti_AFK_Text";
            this.txt_Anti_AFK_Text.Size = new System.Drawing.Size(578, 20);
            this.txt_Anti_AFK_Text.TabIndex = 0;
            // 
            // chk_antiafk_on_off
            // 
            this.chk_antiafk_on_off.AutoSize = true;
            this.chk_antiafk_on_off.Location = new System.Drawing.Point(10, 47);
            this.chk_antiafk_on_off.Name = "chk_antiafk_on_off";
            this.chk_antiafk_on_off.Size = new System.Drawing.Size(106, 17);
            this.chk_antiafk_on_off.TabIndex = 1;
            this.chk_antiafk_on_off.Text = "Anti AFK An/Aus";
            this.chk_antiafk_on_off.UseVisualStyleBackColor = true;
            // 
            // SettingsWindow
            // 
            this.ClientSize = new System.Drawing.Size(630, 599);
            this.Controls.Add(this.groupBox_AntiAFK);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.group_Overlay);
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
            this.group_Overlay.ResumeLayout(false);
            this.group_Overlay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_Overlay_Transparenz)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.traffic_Update_Intervall)).EndInit();
            this.groupBox_AntiAFK.ResumeLayout(false);
            this.groupBox_AntiAFK.PerformLayout();
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

            // ANTI_AFK
            util.Reg_Schreiben("ANTI_AFK", txt_Anti_AFK_Text.Text);
           if(chk_antiafk_on_off.CheckState == CheckState.Checked)
            {
                if(txt_Anti_AFK_Text.Text == "")
                {
                    util.Reg_Schreiben("ANTI_AFK", "Der neue VTC-Manager wünscht Gute und Sichere Fahrt !");
                }
                util.Reg_Schreiben("ANTI_AFK_AN", "1");
            } else
            {
                util.Reg_Schreiben("ANTI_AFK_AN","0");
            }


            this.data.SaveJobID();

            //MessageBox.Show(translation.save_info);

            // Edit by Thommy
            this.Close();
        }

        private void SettingsWindow_Load(object sender, EventArgs e)
        {
            group_Overlay.Visible = false;

            Utilities util2 = new Utilities();

            string wert99 = util2.Reg_Lesen("TruckersMP_Autorun", "autorun");
            string wert22 = util2.Reg_Lesen("TruckersMP_Autorun", "show_GAS");
            string wert23 = util2.Reg_Lesen("TruckersMP_Autorun", "show_BREMSE");
            string wert24 = util2.Reg_Lesen("TruckersMP_Autorun", "show_KUPPLUNG");
            string wert25 = util2.Reg_Lesen("TruckersMP_Autorun", "show_GANG");
            string wert26 = util2.Reg_Lesen("TruckersMP_Autorun", "show_RPM_ANZEIGE");
            string wert27 = util2.Reg_Lesen("TruckersMP_Autorun", "verkehr_SERVER");
            string wert28 = util2.Reg_Lesen("TruckersMP_Autorun", "TruckersMP_Pfad");
            string wert29 = util2.Reg_Lesen("TruckersMP_Autorun", "Reload_Traffic_Sekunden");
            string wert30 = util2.Reg_Lesen("TruckersMP_Autorun", "ANTI_AFK_AN");
            string wert31 = util2.Reg_Lesen("TruckersMP_Autorun", "ANTI_AFK");


            if (wert22 == null)
            {
                wert22 = "1";
            }
            if (wert23 == null)
            {
                wert23 = "1";
            }
            if (wert24 == null)
            {
                wert24 = "1";
            }
            if (wert25 == null)
            {
                wert25 = "1";
            }
            if (wert26 == null)
            {
                wert26 = "1";
            }



            if (wert29 == null)
            {
                traffic_Update_Intervall.Value = 5;
            } else
            {
                traffic_Update_Intervall.Value = Convert.ToDecimal(wert29);
            }


            


            if (wert28.ToString() != null)
            {
                truckersMP_Pfad_TextBox.Text = wert28;
            } else
            {
                truckersMP_Pfad_TextBox.Text = "";
            }

            if (wert99 == "1")
            {
                truckers_autorun.CheckState = CheckState.Checked;
            } else
            {
                truckers_autorun.CheckState = CheckState.Unchecked;
            }

            // Server COMBO vorauswahl
            if(wert27 == "") { wert27 = "Simulation 1"; }
            if (wert27 == "sim1") { comboBox1.Text = "Simulation 1"; }
            if (wert27 == "sim2") { comboBox1.Text = "Simulation 2"; }
            if (wert27 == "arc1") { comboBox1.Text = "Arcade 1"; }
            if (wert27 == "eupromods1") { comboBox1.Text = "EU Promods 1"; }
            if (wert27 == "eupromods2") { comboBox1.Text = "EU Promods 2"; }



            // CHECKBOXEN FÜLLEN FÜR PROGRESS BARS
            if (wert22 == "1") { 
                chk_GAS.CheckState = CheckState.Checked; } else { 
                chk_GAS.CheckState = CheckState.Unchecked;  }
            if (wert23 == "1") {
                chk_BREMSE.CheckState = CheckState.Checked; } else {
                chk_BREMSE.CheckState = CheckState.Unchecked;
            }
            if (wert24 == "1") {
                chk_KUPPLUNG.CheckState = CheckState.Checked; } else {
                chk_KUPPLUNG.CheckState = CheckState.Unchecked;
            }
            if (wert25 == "1") {
                chk_GANG.CheckState = CheckState.Checked; } else {
                chk_GANG.CheckState = CheckState.Unchecked;
            }
            if (wert26 == "1") { 
                chk_RPM_ANZEIGE.CheckState = CheckState.Checked; } else {
                chk_RPM_ANZEIGE.CheckState = CheckState.Unchecked;
            }

            // ANTI_AFK
            chk_antiafk_on_off.CheckState = (wert30 == "1") ? CheckState.Checked : CheckState.Unchecked;
            txt_Anti_AFK_Text.Text = (wert31 != null) ? wert31.ToString() : "";

            // Listbox mit Bildschirmen füllen

            combo_Bildschirme.Items.Clear();
            combo_Bildschirme.Items.Add(Screen.AllScreens.GetUpperBound(0));
            combo_Bildschirme.Items.Add(Screen.AllScreens[0].DeviceName);
            if (Screen.AllScreens.GetUpperBound(0) == 1)
            {
                combo_Bildschirme.Items.Add(Screen.AllScreens[1].DeviceName);
            }
            if (Screen.AllScreens.GetUpperBound(0) == 2)
            {
                combo_Bildschirme.Items.Add(Screen.AllScreens[2].DeviceName);
            }
            if (Screen.AllScreens.GetUpperBound(0) == 3)
            {
                combo_Bildschirme.Items.Add(Screen.AllScreens[3].DeviceName);
            }

            // Variablen abrufen von Main
            var link = Main.truckersMP_Link;

        }

        private void truckers_autorun_CheckedChanged(object sender, EventArgs e)
        {
            if(truckers_autorun.CheckState == CheckState.Checked)
            {
                Utilities util = new Utilities();
                util.Reg_Schreiben("autorun", "1");
            } else
            {
                Utilities util = new Utilities();
                util.Reg_Schreiben("autorun", "0");
            }
        }


        private void num_Overlay_Transparenz_ValueChanged(object sender, EventArgs e)
        {
            double anz = (double)num_Overlay_Transparenz.Value;
            Main.over.Opacity = anz/100;
            Main.overlay_ist_offen = 1;
            Main.over.Update();
            Console.WriteLine(anz/100);
        }

        private void chk_GAS_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_GAS.CheckState == CheckState.Checked)
            {
                Utilities util = new Utilities();
                util.Reg_Schreiben("show_GAS", "1");
            }
            else
            {
                Utilities util = new Utilities();
                util.Reg_Schreiben("show_GAS", "0");
            }
        }

        private void chk_BREMSE_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_BREMSE.CheckState == CheckState.Checked)
            {
                Utilities util = new Utilities();
                util.Reg_Schreiben("show_BREMSE", "1");
            }
            else
            {
                Utilities util = new Utilities();
                util.Reg_Schreiben("show_BREMSE", "0");
            }
        }

        private void chk_KUPPLUNG_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_KUPPLUNG.CheckState == CheckState.Checked)
            {
                Utilities util = new Utilities();
                util.Reg_Schreiben("show_KUPPLUNG", "1");
            }
            else
            {
                Utilities util = new Utilities();
                util.Reg_Schreiben("show_KUPPLUNG", "0");
            }
        }

        private void chk_GANG_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_GANG.CheckState == CheckState.Checked)
            {
                Utilities util = new Utilities();
                util.Reg_Schreiben("show_GANG", "1");
            }
            else
            {
                Utilities util = new Utilities();
                util.Reg_Schreiben("show_GANG", "0");
            }
        }

        private void chk_RPM_ANZEIGE_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_RPM_ANZEIGE.CheckState == CheckState.Checked)
            {
                Utilities util = new Utilities();
                util.Reg_Schreiben("show_RPM_ANZEIGE", "1");
            }
            else
            {
                Utilities util = new Utilities();
                util.Reg_Schreiben("show_RPM_ANZEIGE", "0");
            }
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

        private void traffic_Update_Intervall_ValueChanged(object sender, EventArgs e)
        {
            Utilities util = new Utilities();
            util.Reg_Schreiben("Reload_Traffic_Sekunden", traffic_Update_Intervall.Value.ToString());
            
        }

    }
}

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VTCManager_1._0._0
{
    class SettingsWindow : Form
    {
        private Utilities utils = new Utilities();
        private ComboBox comboBox1;
        private Label label1;
        private SettingsManager data;
        private Translation translation;
        private Button save_button;
        private GroupBox groupBox1;
        private GroupBox btn_TruckersMP_suchen;
        private Label label3;
        private GroupBox group_Overlay;
        private ComboBox combo_Bildschirme;
        private Label label6;
        private NumericUpDown num_Overlay_Transparenz;
        private Label label5;
        private Button button1;
        private string selected_server_tm;
        private System.Windows.Forms.OpenFileDialog tmp_Trucker;
        private TextBox truckersMP_Pfad_TextBox;
        private GroupBox groupBox_AntiAFK;
        private TextBox txt_Anti_AFK_Text;
        private CheckBox chk_antiafk_on_off;
        private Button Ats_Suche;
        private Label label4;
        private TextBox ATS_Pfad_Textbox;
        private Button Ets_Suche;
        private Label label2;
        private TextBox ETS2_Pfad_Textbox;
        private Label label8;
        private Label label7;
        private NumericUpDown reload_antiafk;
        private PictureBox pictureBox1;
        private Label label9;
        private Label Settings_Windows_Label_Settings;
        private FolderBrowserDialog ATS_folderDialog;
        private FolderBrowserDialog ETS2_folderDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        

        public SettingsWindow(Translation translation) {
            this.data = new SettingsManager();
            this.data.LoadJobID();

            this.InitializeComponent();
            this.save_button.Text = translation.settings_window_save_button;
            this.groupBox1.Text = translation.settings_window_groupBox1text;
            this.btn_TruckersMP_suchen.Text = translation.btn_TruckersMP_suchentext;
            this.label3.Text = translation.settings_window_label3text;
            this.comboBox1.Text = this.data.Cache.truckersmp_server;

        }
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsWindow));
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.save_button = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_TruckersMP_suchen = new System.Windows.Forms.GroupBox();
            this.Ats_Suche = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.ATS_Pfad_Textbox = new System.Windows.Forms.TextBox();
            this.Ets_Suche = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ETS2_Pfad_Textbox = new System.Windows.Forms.TextBox();
            this.truckersMP_Pfad_TextBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.group_Overlay = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.num_Overlay_Transparenz = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.combo_Bildschirme = new System.Windows.Forms.ComboBox();
            this.tmp_Trucker = new System.Windows.Forms.OpenFileDialog();
            this.groupBox_AntiAFK = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.reload_antiafk = new System.Windows.Forms.NumericUpDown();
            this.chk_antiafk_on_off = new System.Windows.Forms.CheckBox();
            this.txt_Anti_AFK_Text = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Settings_Windows_Label_Settings = new System.Windows.Forms.Label();
            this.ATS_folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.ETS2_folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1.SuspendLayout();
            this.btn_TruckersMP_suchen.SuspendLayout();
            this.group_Overlay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_Overlay_Transparenz)).BeginInit();
            this.groupBox_AntiAFK.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.reload_antiafk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            this.comboBox1.Location = new System.Drawing.Point(171, 23);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(136, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.Text = "Simulation 1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "TruckersMP-Server:";
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
            this.groupBox1.Location = new System.Drawing.Point(288, 129);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(321, 62);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Server Einstellungen";
            // 
            // btn_TruckersMP_suchen
            // 
            this.btn_TruckersMP_suchen.Controls.Add(this.Ats_Suche);
            this.btn_TruckersMP_suchen.Controls.Add(this.label4);
            this.btn_TruckersMP_suchen.Controls.Add(this.ATS_Pfad_Textbox);
            this.btn_TruckersMP_suchen.Controls.Add(this.Ets_Suche);
            this.btn_TruckersMP_suchen.Controls.Add(this.label2);
            this.btn_TruckersMP_suchen.Controls.Add(this.ETS2_Pfad_Textbox);
            this.btn_TruckersMP_suchen.Controls.Add(this.truckersMP_Pfad_TextBox);
            this.btn_TruckersMP_suchen.Controls.Add(this.button1);
            this.btn_TruckersMP_suchen.Controls.Add(this.label3);
            this.btn_TruckersMP_suchen.Location = new System.Drawing.Point(12, 129);
            this.btn_TruckersMP_suchen.Name = "btn_TruckersMP_suchen";
            this.btn_TruckersMP_suchen.Size = new System.Drawing.Size(270, 186);
            this.btn_TruckersMP_suchen.TabIndex = 6;
            this.btn_TruckersMP_suchen.TabStop = false;
            this.btn_TruckersMP_suchen.Text = "Game und Multiplayer";
            // 
            // Ats_Suche
            // 
            this.Ats_Suche.Location = new System.Drawing.Point(241, 128);
            this.Ats_Suche.Name = "Ats_Suche";
            this.Ats_Suche.Size = new System.Drawing.Size(25, 22);
            this.Ats_Suche.TabIndex = 16;
            this.Ats_Suche.Text = "...";
            this.Ats_Suche.UseVisualStyleBackColor = true;
            this.Ats_Suche.Click += new System.EventHandler(this.Ats_Suche_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Pfad zu ATS:";
            // 
            // ATS_Pfad_Textbox
            // 
            this.ATS_Pfad_Textbox.Location = new System.Drawing.Point(7, 130);
            this.ATS_Pfad_Textbox.Name = "ATS_Pfad_Textbox";
            this.ATS_Pfad_Textbox.Size = new System.Drawing.Size(228, 20);
            this.ATS_Pfad_Textbox.TabIndex = 14;
            // 
            // Ets_Suche
            // 
            this.Ets_Suche.Location = new System.Drawing.Point(241, 81);
            this.Ets_Suche.Name = "Ets_Suche";
            this.Ets_Suche.Size = new System.Drawing.Size(25, 22);
            this.Ets_Suche.TabIndex = 13;
            this.Ets_Suche.Text = "...";
            this.Ets_Suche.UseVisualStyleBackColor = true;
            this.Ets_Suche.Click += new System.EventHandler(this.Ets_Suche_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Pfad zu ETS 2:";
            // 
            // ETS2_Pfad_Textbox
            // 
            this.ETS2_Pfad_Textbox.Location = new System.Drawing.Point(7, 83);
            this.ETS2_Pfad_Textbox.Name = "ETS2_Pfad_Textbox";
            this.ETS2_Pfad_Textbox.Size = new System.Drawing.Size(228, 20);
            this.ETS2_Pfad_Textbox.TabIndex = 11;
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
            // group_Overlay
            // 
            this.group_Overlay.Controls.Add(this.label6);
            this.group_Overlay.Controls.Add(this.num_Overlay_Transparenz);
            this.group_Overlay.Controls.Add(this.label5);
            this.group_Overlay.Controls.Add(this.combo_Bildschirme);
            this.group_Overlay.Location = new System.Drawing.Point(22, 413);
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
            // tmp_Trucker
            // 
            this.tmp_Trucker.FileName = "T";
            // 
            // groupBox_AntiAFK
            // 
            this.groupBox_AntiAFK.Controls.Add(this.label9);
            this.groupBox_AntiAFK.Controls.Add(this.label8);
            this.groupBox_AntiAFK.Controls.Add(this.label7);
            this.groupBox_AntiAFK.Controls.Add(this.reload_antiafk);
            this.groupBox_AntiAFK.Controls.Add(this.chk_antiafk_on_off);
            this.groupBox_AntiAFK.Controls.Add(this.txt_Anti_AFK_Text);
            this.groupBox_AntiAFK.Location = new System.Drawing.Point(288, 198);
            this.groupBox_AntiAFK.Name = "groupBox_AntiAFK";
            this.groupBox_AntiAFK.Size = new System.Drawing.Size(321, 95);
            this.groupBox_AntiAFK.TabIndex = 9;
            this.groupBox_AntiAFK.TabStop = false;
            this.groupBox_AntiAFK.Text = "Anti - AFK";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 19);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "Text:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(268, 69);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "in Min.";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(168, 69);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Reload:";
            // 
            // reload_antiafk
            // 
            this.reload_antiafk.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.reload_antiafk.Location = new System.Drawing.Point(214, 65);
            this.reload_antiafk.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.reload_antiafk.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.reload_antiafk.Name = "reload_antiafk";
            this.reload_antiafk.Size = new System.Drawing.Size(52, 20);
            this.reload_antiafk.TabIndex = 2;
            this.reload_antiafk.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.reload_antiafk.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.reload_antiafk.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // chk_antiafk_on_off
            // 
            this.chk_antiafk_on_off.AutoSize = true;
            this.chk_antiafk_on_off.Location = new System.Drawing.Point(10, 66);
            this.chk_antiafk_on_off.Name = "chk_antiafk_on_off";
            this.chk_antiafk_on_off.Size = new System.Drawing.Size(106, 17);
            this.chk_antiafk_on_off.TabIndex = 1;
            this.chk_antiafk_on_off.Text = "Anti AFK An/Aus";
            this.chk_antiafk_on_off.UseVisualStyleBackColor = true;
            // 
            // txt_Anti_AFK_Text
            // 
            this.txt_Anti_AFK_Text.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Anti_AFK_Text.Enabled = false;
            this.txt_Anti_AFK_Text.Location = new System.Drawing.Point(10, 33);
            this.txt_Anti_AFK_Text.Name = "txt_Anti_AFK_Text";
            this.txt_Anti_AFK_Text.Size = new System.Drawing.Size(301, 20);
            this.txt_Anti_AFK_Text.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::VTCManager_1._0._0.Properties.Resources.einstellungen;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 100);
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // Settings_Windows_Label_Settings
            // 
            this.Settings_Windows_Label_Settings.AutoSize = true;
            this.Settings_Windows_Label_Settings.Font = new System.Drawing.Font("Verdana", 27.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Settings_Windows_Label_Settings.Location = new System.Drawing.Point(118, 33);
            this.Settings_Windows_Label_Settings.Name = "Settings_Windows_Label_Settings";
            this.Settings_Windows_Label_Settings.Size = new System.Drawing.Size(59, 45);
            this.Settings_Windows_Label_Settings.TabIndex = 11;
            this.Settings_Windows_Label_Settings.Text = "...";
            // 
            // ATS_folderDialog
            // 
            this.ATS_folderDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.ATS_folderDialog.ShowNewFolderButton = false;
            // 
            // ETS2_folderDialog
            // 
            this.ETS2_folderDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.ETS2_folderDialog.ShowNewFolderButton = false;
            // 
            // SettingsWindow
            // 
            this.ClientSize = new System.Drawing.Size(630, 599);
            this.Controls.Add(this.Settings_Windows_Label_Settings);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox_AntiAFK);
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
            this.groupBox_AntiAFK.ResumeLayout(false);
            this.groupBox_AntiAFK.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.reload_antiafk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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


            // ANTI_AFK
            // TODO-- SPENDER 


            util.Reg_Schreiben("ANTI_AFK", txt_Anti_AFK_Text.Text);
           if(chk_antiafk_on_off.CheckState == CheckState.Checked)
           {
                if(txt_Anti_AFK_Text.Text == "")
                {
                    util.Reg_Schreiben("ANTI_AFK", "VTCManager wünscht Gute und Sichere Fahrt!");
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
            // Settings_Windows_Label_Settings.Text = translation.settings_window_titel_text; ######### GEHT NICHT ############
            Settings_Windows_Label_Settings.Text = "Einstellungen";

            string test = utils.Reg_Lesen("TruckersMP_Autorun", "TruckersMP_Pfad");
            if (string.IsNullOrEmpty(test)) {
                MessageBox.Show("Der Pfad zu TruckersMP stimmt nicht" + Environment.NewLine + "Bitte korrigiere diesen im folgenden Fenster", "Fehler TruckersMP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 

            string wert27 = utils.Reg_Lesen("TruckersMP_Autorun", "verkehr_SERVER");
            string wert28 = utils.Reg_Lesen("TruckersMP_Autorun", "TruckersMP_Pfad");
            string wert30 = utils.Reg_Lesen("TruckersMP_Autorun", "ANTI_AFK_AN");
            string wert31 = utils.Reg_Lesen("TruckersMP_Autorun", "ANTI_AFK");
            ETS2_Pfad_Textbox.Text = utils.Reg_Lesen("TruckersMP_Autorun", "ETS2_Pfad");
            ToolTip tt1 = new ToolTip();
            tt1.SetToolTip(ETS2_Pfad_Textbox, ETS2_Pfad_Textbox.Text);

            ATS_Pfad_Textbox.Text = utils.Reg_Lesen("TruckersMP_Autorun", "ATS_Pfad");
            ToolTip tt2 = new ToolTip();
            tt2.SetToolTip(ATS_Pfad_Textbox, ATS_Pfad_Textbox.Text);

            if (wert28 != null)
            {
                truckersMP_Pfad_TextBox.Text = wert28;
                truckersMP_Pfad_TextBox.Enabled = false;
            } else
            {
                truckersMP_Pfad_TextBox.Text = "";
            }

            // Server COMBO vorauswahl
            if(wert27 == null) { comboBox1.Text = "Simulation 1"; utils.Reg_Schreiben("verkehr_SERVER", "sim1"); }
            if (wert27 == "sim1") { comboBox1.Text = "Simulation 1"; }
            if (wert27 == "sim2") { comboBox1.Text = "Simulation 2"; }
            if (wert27 == "arc1") { comboBox1.Text = "Arcade 1"; }
            if (wert27 == "eupromods1") { comboBox1.Text = "EU Promods 1"; }
            if (wert27 == "eupromods2") { comboBox1.Text = "EU Promods 2"; }


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

        private void Ets_Suche_Click(object sender, EventArgs e)
        {
            if (ETS2_folderDialog.ShowDialog() == DialogResult.OK)
            {
                ETS2_Pfad_Textbox.Text = ETS2_folderDialog.SelectedPath.ToString();
                ETS2_Pfad_Textbox.Enabled = false;
                utils.Reg_Schreiben("ETS2_Pfad", ETS2_folderDialog.SelectedPath.ToString());
                if (!Directory.Exists(ETS2_folderDialog.SelectedPath.ToString() + @"\bin\win_x64\plugins"))
                {
                    Directory.CreateDirectory(ETS2_folderDialog.SelectedPath.ToString() + @"\bin\win_x64\plugins");
                    File.Copy(Application.StartupPath + @"\Resources\scs-telemetry.dll", ETS2_folderDialog.SelectedPath.ToString() + @"\bin\win_x64\plugins\scs-telemetry.dll");
                }
            }
            else
            {
                if(string.IsNullOrEmpty(ETS2_Pfad_Textbox.Text))
                MessageBox.Show("Das angegebene Verzeichnis von ETS2 ist falsch!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Ats_Suche_Click(object sender, EventArgs e)
        {
            if (ATS_folderDialog.ShowDialog() == DialogResult.OK)
            {
                ATS_Pfad_Textbox.Text = ATS_folderDialog.SelectedPath.ToString();
                ATS_Pfad_Textbox.Enabled = false;
                utils.Reg_Schreiben("ATS_Pfad", ATS_folderDialog.SelectedPath.ToString());
                if (!Directory.Exists(ATS_folderDialog.SelectedPath.ToString() + @"\bin\win_x64\plugins"))
                {
                    Directory.CreateDirectory(ATS_folderDialog.SelectedPath.ToString() + @"\bin\win_x64\plugins");
                    File.Copy(Application.StartupPath + @"\Resources\scs-telemetry.dll", ATS_folderDialog.SelectedPath.ToString() + @"\bin\win_x64\plugins\scs-telemetry.dll");
                }
            }
            else
            {
                if(string.IsNullOrEmpty(ATS_Pfad_Textbox.Text))
                MessageBox.Show("Das angegebene Verzeichnis von ATS ist falsch!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            utils.Reg_Schreiben("ANTI_AFK_RELOAD", reload_antiafk.Value.ToString());

        }
    }
}

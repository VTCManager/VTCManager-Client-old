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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.speed_setup_box = new System.Windows.Forms.CheckBox();
            this.save_button = new System.Windows.Forms.Button();
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
            this.comboBox1.Location = new System.Drawing.Point(12, 25);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.Text = "Simulation 1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "TruckersMP-Server:";
            // 
            // speed_setup_box
            // 
            this.speed_setup_box.AutoSize = true;
            this.speed_setup_box.Location = new System.Drawing.Point(12, 52);
            this.speed_setup_box.Name = "speed_setup_box";
            this.speed_setup_box.Size = new System.Drawing.Size(144, 17);
            this.speed_setup_box.TabIndex = 2;
            this.speed_setup_box.Text = "Geschwindigkeit in mph?";
            this.speed_setup_box.UseVisualStyleBackColor = true;
            // 
            // save_button
            // 
            this.save_button.Location = new System.Drawing.Point(363, 555);
            this.save_button.Name = "save_button";
            this.save_button.Size = new System.Drawing.Size(75, 23);
            this.save_button.TabIndex = 3;
            this.save_button.Text = "Speichern...";
            this.save_button.UseVisualStyleBackColor = true;
            this.save_button.Click += new System.EventHandler(this.save_button_Click);
            // 
            // SettingsWindow
            // 
            this.ClientSize = new System.Drawing.Size(744, 599);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Controls.Add(this.save_button);
            this.Controls.Add(this.speed_setup_box);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Name = "SettingsWindow";
            this.Text = "Einstellungen";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void save_button_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.Text == "Simulation 1")
            {
                this.selected_server_tm = "sim1";
                this.data.Cache.truckersmp_server = this.selected_server_tm;
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
            this.Dispose();
        }
    }
}

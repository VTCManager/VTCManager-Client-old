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
        private string selected_server_tm;

        public SettingsWindow() {
            this.data = new SettingsManager();
            this.data.LoadJobID();
            CultureInfo ci = CultureInfo.InstalledUICulture;
            this.translation = new Translation(ci.DisplayName);

            this.InitializeComponent();
        }
        private void InitializeComponent()
        {
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(12, 40);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.Items.Add("Simulation 1");
            this.comboBox1.Items.Add("Simulation 2");
            this.comboBox1.Items.Add("Arcade");
            this.comboBox1.Items.Add("EU Promods 1");
            this.comboBox1.Items.Add("EU Promods 2");
            this.comboBox1.Text = this.data.Cache.truckersmp_server;
            comboBox1.SelectedIndexChanged += new System.EventHandler(ComboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "TruckersMP-Server:";
            // 
            // SettingsWindow
            // 
            this.ClientSize = new System.Drawing.Size(744, 599);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Name = "SettingsWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private void ComboBox1_SelectedIndexChanged(object sender,
System.EventArgs e)
        {
            if (this.comboBox1.Text == "Simulation 1")
            {
                this.selected_server_tm = "sim1";
                this.data.Cache.truckersmp_server = this.selected_server_tm;
                this.data.SaveJobID();
                MessageBox.Show(translation.save_info);
            } else if (this.comboBox1.Text == "Simulation 2") {
                this.selected_server_tm = "sim2";
                this.data.Cache.truckersmp_server = this.selected_server_tm;
                this.data.SaveJobID();
                MessageBox.Show(translation.save_info);
            }
            else if (this.comboBox1.Text == "Arcade")
            {
                this.selected_server_tm = "arc1";
                this.data.Cache.truckersmp_server = this.selected_server_tm;
                this.data.SaveJobID();
                MessageBox.Show(translation.save_info);
            }
            else if (this.comboBox1.Text == "EU Promods 1")
            {
                this.selected_server_tm = "eupromods1";
                this.data.Cache.truckersmp_server = this.selected_server_tm;
                this.data.SaveJobID();
                MessageBox.Show(translation.save_info);
            }
            else if (this.comboBox1.Text == "EU Promods 2")
            {
                this.selected_server_tm = "eupromods2";
                this.data.Cache.truckersmp_server = this.selected_server_tm;
                this.data.SaveJobID();
                MessageBox.Show(translation.save_info);
            }
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VTCManager_1._0._0
{
    public partial class ETS2_Pfad_Window : Form
    {
        public ETS2_Pfad_Window()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void ETS2_Pfad_Window_Load(object sender, EventArgs e)
        {
           
            Utilities util = new Utilities();
            ats_pfad.Text = util.Reg_Lesen("TruckersMP_Autorun", "ATS_Pfad");
            ets_pfad.Text = util.Reg_Lesen("TruckersMP_Autorun", "ETS2_Pfad");

        }

        private void btn_Suche_ETS_Click(object sender, EventArgs e)
        {
            var pfad_suchen = folderBrowserDialog_ETS.ShowDialog();
            if (pfad_suchen == DialogResult.OK)
            {
                Utilities util = new Utilities();
                util.Reg_Schreiben("ETS2_Pfad", folderBrowserDialog_ETS.SelectedPath + @"\");
                ets_pfad.Text = folderBrowserDialog_ETS.SelectedPath.ToString();

                // Telemetry kopieren
                Utilities util2 = new Utilities();
                string dest_Path = util2.Reg_Lesen("TruckersMP_Autorun", "ETS2_Pfad") + @"plugins\";
                try
                {
                    File.Copy(Application.StartupPath + @"\Resources\ets2-telemetry.dll", dest_Path + @"ets2-telemetry.dll");

                }
                catch { }




            }
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Suche_ATS_Click(object sender, EventArgs e)
        {
            var pfad_suchen = folderBrowserDialog_ATS.ShowDialog();
            if (pfad_suchen == DialogResult.OK)
            {
                Utilities util = new Utilities();
                util.Reg_Schreiben("ATS_Pfad", folderBrowserDialog_ATS.SelectedPath + @"\");
                ats_pfad.Text = folderBrowserDialog_ATS.SelectedPath.ToString();

                // Telemetry kopieren
                Utilities util2 = new Utilities();
                string dest_Path = util2.Reg_Lesen("TruckersMP_Autorun", "ATS_Pfad") + @"plugins\";
                try
                {
                    File.Copy(Application.StartupPath + @"\Resources\ets2-telemetry.dll", dest_Path + @"ets2-telemetry.dll");
                } catch { }
                

            }
        }
    }
}

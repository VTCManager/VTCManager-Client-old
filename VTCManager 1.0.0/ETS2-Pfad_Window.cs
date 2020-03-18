﻿using System;
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

        private Utilities utils = new Utilities();

        public ETS2_Pfad_Window()
        {
            

            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void ETS2_Pfad_Window_Load(object sender, EventArgs e)
        {
            
            ats_pfad.Text = utils.Reg_Lesen("TruckersMP_Autorun", "ATS_Pfad");
            ets_pfad.Text = utils.Reg_Lesen("TruckersMP_Autorun", "ETS2_Pfad");

        }

        private void btn_Suche_ETS_Click(object sender, EventArgs e)
        {
            var pfad_suchen = folderBrowserDialog_ETS.ShowDialog();
            if (pfad_suchen == DialogResult.OK)
            {
                utils.Reg_Schreiben("ETS2_Pfad", folderBrowserDialog_ETS.SelectedPath + @"\");
                ets_pfad.Text = folderBrowserDialog_ETS.SelectedPath.ToString();

                // Telemetry kopierens();
                string dest_leer = utils.Reg_Lesen("TruckersMP_Autorun", "ETS2_Pfad");
                if (!Directory.Exists(dest_leer + @"bin\win_x64\plugins")) { Directory.CreateDirectory(dest_leer + @"bin\win_x64\plugins"); }

                string dest_Path = utils.Reg_Lesen("TruckersMP_Autorun", "ETS2_Pfad") + @"bin\win_x64\plugins\";
                try
                {
                    File.Copy(Application.StartupPath + @"\Resources\scs-telemetry.dll", dest_Path + @"\scs-telemetry.dll");

                }
                catch {
       
                }
           
            }
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
                string dest_leer = util2.Reg_Lesen("TruckersMP_Autorun", "ATS_Pfad");
                if (!Directory.Exists(dest_leer + @"bin\win_x64\plugins")) { Directory.CreateDirectory(dest_leer + @"bin\win_x64\plugins"); }

                string dest_Path = util2.Reg_Lesen("TruckersMP_Autorun", "ATS_Pfad") + @"bin\win_x64\plugins\";
              
                try
                {
                    File.Copy(Application.StartupPath + @"\Resources\scs-telemetry.dll", dest_Path + @"scs-telemetry.dll");
                } catch {



                }


            }
        }


        private void button_ok_Click(object sender, EventArgs e)
        {
            Utilities util2 = new Utilities();
            ets_pfad.Text = util2.Reg_Lesen("TruckersMP_Autorun", "ETS2_Pfad");
            ats_pfad.Text = util2.Reg_Lesen("TruckersMP_Autorun", "ATS_Pfad");

            if (!File.Exists(ets_pfad.Text + @"bin\win_x64\eurotrucks2.exe")) { MessageBox.Show("Der Pfad von ETS ist falsch ! " + Environment.NewLine + "Bitte gib den richtigen Pfad an!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }



            //if (!File.Exists(ats_pfad.Text + @"\amtrucks.exe")) { MessageBox.Show("In diesem Ordner ist keine Spieldatei von ATS ! " + Environment.NewLine + "Bitte gibt den Pfad bis: win_x64 an!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }


            this.Dispose();
            this.Close();
        }

        private void ETS2_Pfad_Window_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ets_pfad.Text == "") { Application.Exit(); }
            if (ats_pfad.Text == "") { Application.Exit(); }
        }


    }
}

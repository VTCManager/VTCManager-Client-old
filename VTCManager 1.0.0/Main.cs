using SCSSdkClient.Object;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Media;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;
using SCSSdkClient;
using Newtonsoft.Json;
using System.Text;
using System.Collections;

namespace VTCManager_1._0._0
{
    public class Main : Form
    {
        private API api = new API();
        private Utilities utils = new Utilities();
        public string userID = "0";
        public string userCompanyID = "0";
        public string jobID = "0";
        private SettingsManager settings;
        public string authCode = "false";
        public Dictionary<string, string> lastJobDictionary = new Dictionary<string, string>();
        public SCSSdkTelemetry Telemetry;
        public bool jobStarted;
        public bool jobRunning;
        private float fuelatend;
        private float fuelconsumption;
        public bool jobFinished;
        public bool locationUpdate;
        public int totalDistance;
        public int invertedDistance;
        public int lastNotZeroDistance;
        public float lastCargoDamage;
        public double currentPercentage;
        public int updatedPercentage;
        public int fuelValue;
        public bool ownTrailerAttached;
        public bool stillTheSameJob;
        private IContainer components;
        private System.Timers.Timer send_tour_status;
        private Panel panel2;
        private Timer send_location;
        private Timer send_speedo;
        public MenuStrip menuStrip1;
        private ToolStripMenuItem einstellungenToolStripMenuItem;
        private ToolStripMenuItem beendenToolStripMenuItem;
        private ToolStripMenuItem topMenuAccount;
        private ToolStripMenuItem topmenuwebsite;
        private Panel panel4;
        private System.Windows.Forms.Label speed_lb;
        private System.Windows.Forms.Label cargo_lb;
        private System.Windows.Forms.Label depature_lb;
        private System.Windows.Forms.Label destination_lb;
        private ToolStripMenuItem dateiToolStripMenuItem;
        private System.Windows.Forms.Label truck_lb;
        private Label label1;
        public Label label2;
        private Translation translation;
        private TableLayoutPanel tableLayoutPanel1;
        private string traffic_response;
        public string username;
        public int driven_tours;
        public int act_bank_balance;
        public SoundPlayer notification_sound_tour_start;
        public SoundPlayer notification_sound_success;
        public SoundPlayer notification_sound_fail;
        private Label status_jb_canc_lb;
        public SoundPlayer notification_sound_tour_end;


        /// <summary>
        /// SPEED LABEL
        /// </summary>


        private string speed;
        private int rpm;
        private double CoordinateX;
        private double CoordinateZ;
        private double rotation;
        private double num1;
        private double num2;
        public string userCompany;
        private Label version_lb;
        private ToolStripMenuItem MenuAbmeldenButton;
        private float fuelatstart;
        private ToolStripMenuItem creditsToolStripMenuItem;
        public bool discordRPCalreadrunning;
        public string CityDestination;
        public string CitySource;
        private NotifyIcon TaskBar_Icon;
        private ContextMenuStrip contextTaskbar;
        private ToolStripMenuItem öffnenToolStripMenuItem;
        private ToolStripMenuItem einstellungenToolStripMenuItem1;
        private ToolStripMenuItem webseiteToolStripMenuItem;
        private ToolStripMenuItem überToolStripMenuItem;
        private ToolStripMenuItem beendenToolStripMenuItem1;
        private LinkLabel linkLabel1;
        private ToolStripMenuItem GUI_SIZE_BUTTON;
        private GroupBox groupStatistiken;
        private Label user_company_lb;
        private Label statistic_panel_topic;
        private Label act_bank_balance_lb;
        private Label driven_tours_lb;
        private GroupBox groupVerkehr;
        private Button truckersMP_Button;
        private ToolStripMenuItem eventsToolStripMenuItem;

        // GUI by Thommy
        public int GUI_SIZE = 1;
        public static string truckersMP_Link;
        private ToolStripMenuItem lbl_Overlay;
        public static int truckersMP_autorun;
        public static int overlay_ist_offen = 0;
        private ToolStripMenuItem darkToolStripMenuItem;
        public static int overlay_Opacity;
        public Timer updateTraffic;
        private Label lbl_Reload_Time;
        public int Is_DarkMode_On;
        public Label lbl_Revision;
        private ToolStripMenuItem serverstatusToolStripMenuItem;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel WebServer_Status_label;
        private ToolStripStatusLabel Label_DB_Server;
        public int reload;
        public Timer anti_AFK_TIMER;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem oldCar1ToolStripMenuItem;
        private ToolStripMenuItem oldCar2ToolStripMenuItem;
        private ToolStripMenuItem oldCar3ToolStripMenuItem;
        private ToolStripMenuItem oldCar4ToolStripMenuItem;
        private ToolStripMenuItem keinsToolStripMenuItem;
        public int anti_afk_on_off;
        private Label label3;
        private PictureBox ets2_button;
        private PictureBox ats_button;
        public static string labelRevision;
        private string meins;
        public bool Tollgate;
        public float Tollgate_Payment;
        public bool Ferry;
        public bool Train;
        private Label label_prozent;
        private Label label_gefahren;
        public int GameRuns;
        public string Spiel;
        public string Refuel_Amount;
        public string Strafe;
        public string Faehre;
        public string FaehreKosten;
        public string labelkmh;
        public bool refuel_beendet;
        private int jobrunningcounter;
        private Discord discord;
        private string uid;


        // Get a handle to an application window.
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName,
            string lpWindowName);

        // Activate an application window.
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);


        public Main(string newauthcode, string username, int driven_tours, int act_bank_balance, bool last_job_canceled, string company)
        {
            // Revision

            if (File.Exists(Environment.CurrentDirectory + @"\Ressources\insight.wav"))
            {
                this.notification_sound_success = new SoundPlayer(Environment.CurrentDirectory + @"\Ressources\insight.wav");
            }
            if (File.Exists(Environment.CurrentDirectory + @"\Ressources\time-is-now.wav.wav"))
            {
                this.notification_sound_fail = new SoundPlayer(Environment.CurrentDirectory + @"\Ressources\time-is-now.wav");
            }
            if (File.Exists(Environment.CurrentDirectory + @"\Ressources\AutopilotStart_fx.wav"))
            {
                this.notification_sound_tour_start = new SoundPlayer(Environment.CurrentDirectory + @"\Ressources\AutopilotStart_fx.wav");
            }
            if (File.Exists(Environment.CurrentDirectory + @"\Ressources\AutopilotEnd_fx.wav"))
            {
                this.notification_sound_tour_end = new SoundPlayer(Environment.CurrentDirectory + @"\Ressources\AutopilotEnd_fx.wav");
            }


            
            this.username = username;
            this.driven_tours = driven_tours;
            this.act_bank_balance = act_bank_balance;
            CultureInfo ci = CultureInfo.InstalledUICulture;
            this.translation = new Translation(ci.DisplayName);
            if (company == "0")
            {
                this.userCompany = translation.no_company_text;
            }
            else
            {
                this.userCompany = company;
            }
            if (last_job_canceled == true)
            {
                this.status_jb_canc_lb.Text = translation.jb_canc_lb;
            }
            this.settings = new SettingsManager();
            this.settings.CreateCache();
            this.settings.LoadJobID();
            if (string.IsNullOrEmpty(this.settings.Cache.speed_mode) == true)
            {
                this.settings.Cache.speed_mode = "kmh";
                this.settings.SaveJobID(); ;
            }
            this.authCode = newauthcode;
            this.InitializeComponent();
            this.InitializeTranslation();
            try
            {
                this.load_traffic();
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception: Getting traffic data from TruckyAPI");
            }
            this.FormClosing += new FormClosingEventHandler(this.Main_FormClosing);
            this.Telemetry = new SCSSdkTelemetry();
            this.Telemetry.Data += this.Telemetry_Data;
            this.Telemetry.JobStarted += this.TelemetryOnJobStarted;

            this.Telemetry.JobCancelled += this.TelemetryJobCancelled;
            this.Telemetry.JobDelivered += this.TelemetryJobDelivered;
            //this.Telemetry.Fined += this.TelemetryFined;
            this.Telemetry.Tollgate += this.TelemetryTollgate;
            this.Telemetry.Ferry += this.TelemetryFerry;
            this.Telemetry.Train += this.TelemetryTrain;
            this.Telemetry.Refuel += this.TelemetryRefuel;
            //this.Telemetry.RefuelEnd += TelemetryRefuelEnd;
            //this.Telemetry.RefuelPayed += TelemetryRefuelPayed;


            if (this.Telemetry.Error == null)
                return;
            int num = (int)MessageBox.Show("Fehler beim Ausführen von:" + this.Telemetry.Map + "\r\n" + this.Telemetry.Error.Message + "\r\n\r\nStacktrace:\r\n" + this.Telemetry.Error.StackTrace);

            
            
        }



        private void InitializeDiscord(int mode)
        {
            /*  DISABLED WEIL FEHLER WENN DISCORD AUS IST !!
             *  
                this.client = new DiscordRpcClient("659036297561767948");
                this.client.Logger = new ConsoleLogger() { Level = LogLevel.Warning };
                this.client.OnReady += (sender, e) =>
                {
                    Console.WriteLine("Received Ready from user {0}", e.User.Username);
                };
                this.client.OnPresenceUpdate += (sender, e) =>
                {
                    Console.WriteLine("Received Update! {0}", e.Presence);
                };
                this.client.Initialize();
                client.Invoke();
            */
        }



        private void InitializeTranslation()
        {
            this.label1.Text = translation.traffic_main_lb;
            this.einstellungenToolStripMenuItem.Text = translation.settings_lb;
            this.beendenToolStripMenuItem.Text = translation.exit_lb;
            this.topMenuAccount.Text = translation.topmenuaccount_lb;
            this.statistic_panel_topic.Text = translation.statistic_panel_topic + this.username.ToUpper();
            this.driven_tours_lb.Text = translation.driven_tours_lb + this.driven_tours;
            this.act_bank_balance_lb.Text = translation.act_bank_balance + this.act_bank_balance + "€";
            this.user_company_lb.Text = translation.user_company_lb + this.userCompany;
            //this.version_lb.Text = translation.version;
            this.MenuAbmeldenButton.Text = translation.logout;


        }

        private void load_traffic()
        {
            

            string server;
          

            if (string.IsNullOrEmpty(utils.Reg_Lesen("TruckersMP_Autorun", "verkehr_SERVER")) == true)
            {
                this.settings.Cache.truckersmp_server = "sim1";
                server = "sim1";
            } else
            {
                server = utils.Reg_Lesen("TruckersMP_Autorun", "verkehr_SERVER");
            }

            this.tableLayoutPanel1.Visible = false;

            //Console.WriteLine(server);

            this.tableLayoutPanel1.Controls.Clear();
            this.tableLayoutPanel1.RowStyles.Clear();
            this.tableLayoutPanel1.ColumnCount = 2;
            // this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.18533F));
            // this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.81467F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(13, 78);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";

            this.tableLayoutPanel1.RowCount = 9;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(518, 179);
            this.tableLayoutPanel1.TabIndex = 4;
            API api = new API();
            Dictionary<string, string> postParameters = new Dictionary<string, string>();
            postParameters.Add("server", server);
            postParameters.Add("game", "ets2");

            this.traffic_response = this.api.HTTPSRequestGet(this.api.trucky_api_server + this.api.get_traffic_path, postParameters).ToString();
            var truckyTopTraffic = TruckyTopTraffic.FromJson(this.traffic_response);
            this.AddItem(truckyTopTraffic.Response[0].Name, truckyTopTraffic.Response[0].Players.ToString());
            this.AddItem(truckyTopTraffic.Response[1].Name, truckyTopTraffic.Response[1].Players.ToString());
            this.AddItem(truckyTopTraffic.Response[2].Name, truckyTopTraffic.Response[2].Players.ToString());
            this.AddItem(truckyTopTraffic.Response[3].Name, truckyTopTraffic.Response[3].Players.ToString());
            this.AddItem(truckyTopTraffic.Response[4].Name, truckyTopTraffic.Response[4].Players.ToString());
            this.AddItem(truckyTopTraffic.Response[5].Name, truckyTopTraffic.Response[5].Players.ToString());
            this.AddItem(truckyTopTraffic.Response[6].Name, truckyTopTraffic.Response[6].Players.ToString());
            this.AddItem(truckyTopTraffic.Response[7].Name, truckyTopTraffic.Response[7].Players.ToString());
            this.AddItem(truckyTopTraffic.Response[8].Name, truckyTopTraffic.Response[8].Players.ToString());
            this.tableLayoutPanel1.Visible = true;
            // Verkehr Label aktualisieren

            if (server == "sim1") { label2.Text = "Server: Simulation 1"; }
            if (server == "sim2") { label2.Text = "Server: Simulation 2"; }
            if (server == "arc1") { label2.Text = "Server: Arcade 1"; }
            if (server == "eupromods1") { label2.Text = "Server: ProMods 1"; }
            if (server == "eupromods2") { label2.Text = "Server: ProMods 2"; }


        }
        private void AddItem(string road, string traffic)
        {
            //get a reference to the previous existent 
            RowStyle temp = tableLayoutPanel1.RowStyles[tableLayoutPanel1.RowCount - 1];
            //increase panel rows count by one
            tableLayoutPanel1.RowCount++;
            //add a new RowStyle as a copy of the previous one
            tableLayoutPanel1.RowStyles.Add(new RowStyle(temp.SizeType, temp.Height));
            //add your three controls
            tableLayoutPanel1.Controls.Add(new Label() { Text = road }, 0, tableLayoutPanel1.RowCount - 1);
            tableLayoutPanel1.Controls.Add(new Label() { Text = traffic }, 1, tableLayoutPanel1.RowCount - 1);
        }

        public bool CancelTour()
        {
            notification_sound_fail.Play();
            this.settings.Cache.SaveJobID = "0";
            this.settings.SaveJobID();
            API api = new API();
            api.HTTPSRequestPost(api.api_server + api.canceltourpath, new Dictionary<string, string>()
      {
        {
          "authcode",
          this.authCode
        },
        {
          "job_id",
          this.jobID
        }
      }, true).ToString();
            this.totalDistance = 0;
            this.currentPercentage = 0;
            this.invertedDistance = 0;
            this.lastNotZeroDistance = 0;
            this.lastCargoDamage = 0.0f;
            this.jobID = "0";
            this.InitializeDiscord(0);
            return true;
        }

        private void TelemetryOnJobFinished(object sender, EventArgs args)
        {
            this.send_tour_status.Enabled = false;
            this.jobFinished = true;
        }

        private void TelemetryOnJobStarted(object sender, EventArgs e)
        {
            this.jobStarted = true;
        }



        private void Telemetry_Data(SCSTelemetry data, bool updated)
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke(new TelemetryData(Telemetry_Data), data, updated);
                    return;
                }
                else
                {
                    int time = Telemetry.UpdateInterval;
                    float num1;
                    if (data.SdkActive)
                    {
                        CoordinateX = data.TruckValues.CurrentValues.PositionValue.Position.X;
                        CoordinateZ = data.TruckValues.CurrentValues.PositionValue.Position.Y;
                        Spiel = data.Game.ToString();


                        // EIN - AUSBLENDEN JE NACH PAUSENSTATUS
                            //label_gefahren.Visible = (data.Paused) ? false : true;
                            //label_prozent.Visible = (data.Paused) ? false : true;
                        //truck_lb.Visible = (data.Paused) ? false : true;
                        this.truck_lb.Visible = true;
                        destination_lb.Visible = (data.Paused) ? false : true;
                            depature_lb.Visible = (data.Paused) ? false : true;
                        //cargo_lb.Visible = (data.Paused) ? false : true;
                        this.cargo_lb.Visible = true;
                            Tollgate_Payment = data.GamePlay.TollgateEvent.PayAmount;



                        if (data.Paused == false)
                        {
                            // PROZENTBERECHNUNG ANFANG
                            label_prozent.Text = "Gesamt: " + data.JobValues.PlannedDistanceKm.ToString() + " KM";
                            label_gefahren.Text = "Reststrecke: " + Convert.ToInt32(data.NavigationValues.NavigationDistance / 1000) + " KM";
                            // PROZENTBERECHNUNG ENDE

                           this.currentPercentage = (((((double)data.NavigationValues.NavigationDistance / 1000) / (double)data.JobValues.PlannedDistanceKm) * 100)-100)*-1;

                            // SPEED LABEL - TRUCK LABEL
                            if (data.Game.ToString() == "Ets2") { labelkmh = " KM/H";  } else { labelkmh = " mp/h"; }
                            speed_lb.Text = (int)data.TruckValues.CurrentValues.DashboardValues.Speed.Kph + labelkmh;
                            truck_lb.Text = "Dein Truck: " + data.TruckValues.ConstantsValues.Brand + ", Modell: " + data.TruckValues.ConstantsValues.Name;

                            if (data.JobValues.CargoLoaded == false)
                            {
                                this.discord.noTour();
                                cargo_lb.Text = "Leerfahrt";
                                destination_lb.Visible = false;
                                depature_lb.Text = "";
                            }
                            else
                            {
                                if (this.jobrunningcounter == 30)
                                {
                                    Console.WriteLine("tiick");
                                    this.api.HTTPSRequestPost(this.api.api_server + this.api.job_update_path, new Dictionary<string, string>()
                                    {
                                        { "authcode", this.authCode },
                                        { "job_id", this.jobID },
                                        { "percentage", this.currentPercentage.ToString() }
                                    }, false).ToString();
                                    this.jobrunningcounter = 0;
                                }
                                this.jobrunningcounter++;
                            }
                                
                            if (this.discordRPCalreadrunning == false)
                            {
                                this.InitializeDiscord(0); //ot working uff cant update RPC
                                this.discordRPCalreadrunning = true;
                            }
                        }
                        else
                        {
                            
                        }
                        bool flag;
                        using (Dictionary<string, string>.Enumerator enumerator = this.lastJobDictionary.GetEnumerator())
                            flag = !enumerator.MoveNext();

                    }
                    else
                    {
                        this.truck_lb.Visible = false;
                        this.cargo_lb.Visible = false;
                        this.speed_lb.Text = translation.waiting_for_ets;
                    }
  
                label_25:
                    double num2;
                    if (this.jobStarted)
                    {
                        Console.WriteLine("///STARTE TOUR_START_DEBUGGER///");
                        bool flag;
                        using (Dictionary<string, string>.Enumerator enumerator = this.lastJobDictionary.GetEnumerator())
                            flag = !enumerator.MoveNext();
                        if (flag)
                        {
                            Console.WriteLine("FLAG IST TRUE!!!");
                            this.uid = data.JobValues.CitySourceId.ToString() + data.JobValues.CargoValues.Id.ToString() + data.JobValues.CargoValues.Mass.ToString();
                        
                            Console.WriteLine("UNIQUE TOUR ID: " + this.uid);
                            Console.WriteLine("OLD UID: " + this.lastJobDictionary["uid"]);
                            Console.WriteLine("UNIQUE ID BUILDER:" + data.JobValues.CitySourceId.ToString() + " " + data.JobValues.CargoValues.Id.ToString() + " " + data.JobValues.CargoValues.Mass.ToString());
                       
                            if ((double)data.NavigationValues.NavigationDistance >= 0.1)
                            {
                                Console.WriteLine("NAVIGATION DISTANCE CHECK IST TRUE!!!");
                                
                                if (this.lastJobDictionary["uid"] != this.uid)
                                {
                                    Console.WriteLine("UID CHECK IST TRUE!!!");
                                    this.lastJobDictionary.Clear();
                                    notification_sound_tour_start.Play();
                                    this.totalDistance = (int)data.NavigationValues.NavigationDistance;
                                    num2 = (double)data.JobValues.Income * 0.15;
                                    this.cargo_lb.Text = "Deine Fracht: " + ((int)Math.Round((double)data.JobValues.CargoValues.Mass, 0) / 1000).ToString() + " Tonnen " + data.JobValues.CargoValues.Name;
                                    this.depature_lb.Text = "Von: " + data.JobValues.CitySource + " ( " + data.JobValues.CompanySource + " ) nach: " + data.JobValues.CityDestination + " ( " + data.JobValues.CompanyDestination + " )";
                                    this.fuelatstart = data.TruckValues.ConstantsValues.CapacityValues.Fuel;

                                    Dictionary<string, string> postParameters = new Dictionary<string, string>();
                                    postParameters.Add("authcode", this.authCode);
                                    postParameters.Add("cargo", data.JobValues.CargoValues.Name);
                                    postParameters.Add("weight", ((int)Math.Round((double)data.JobValues.CargoValues.Mass, 0) / 1000).ToString());
                                    postParameters.Add("depature", data.JobValues.CitySource);
                                    postParameters.Add("depature_company", data.JobValues.CompanySource);
                                    postParameters.Add("destination_company", data.JobValues.CompanyDestination);
                                    postParameters.Add("destination", data.JobValues.CityDestination);
                                    postParameters.Add("truck_manufacturer", data.TruckValues.ConstantsValues.Brand);
                                    postParameters.Add("truck_model", data.TruckValues.ConstantsValues.Name);
                                    postParameters.Add("distance", data.JobValues.PlannedDistanceKm.ToString());
                                    this.jobID = this.api.HTTPSRequestPost(this.api.api_server + this.api.new_job_path, postParameters, true).ToString();


                                    utils.Reg_Schreiben("jobID", this.jobID);

                                    //this.settings.Cache.SaveJobID = this.jobID;
                                    //this.settings.SaveJobID();
                                    

                                    Dictionary<string, string> lastJobDictionary = this.lastJobDictionary;
                                    this.lastJobDictionary.Add("uid", this.uid);

                                    this.discord.onTour(data.JobValues.CityDestination, data.JobValues.CitySource, data.JobValues.CargoValues.Name, ((int)Math.Round((double)data.JobValues.CargoValues.Mass, 0) / 1000).ToString());

                                    //if(this.lastJobDictionary["mass"] == Convert.ToString(data.Job.Mass)) { MessageBox.Show("SELEBE!"); }
                                    this.CitySource = data.JobValues.CitySource;
                                    this.CityDestination = data.JobValues.CityDestination;
                                    this.InitializeDiscord(1);
                                    this.send_tour_status.Enabled = true;
                                    this.send_tour_status.Start();
                                    this.jobStarted = false;
                                }
                            }
                        }

                    }


                    if (this.jobRunning)
                    {
                                this.jobRunning = false;
                                    
                    }



                    if (this.jobFinished)
                    {
                        this.uid = data.JobValues.CitySourceId.ToString() + data.JobValues.CargoValues.Id.ToString() + data.JobValues.CargoValues.Mass.ToString();
                        if (this.lastJobDictionary["uid"] == this.uid)
                        {

                            Console.WriteLine("jobfinsiehed");
                            notification_sound_tour_end.Play();
                            this.send_tour_status.Enabled = false;
                            this.jobRunning = false;
                            this.fuelatend = (float)data.TruckValues.ConstantsValues.CapacityValues.Fuel;
                            this.fuelconsumption = this.fuelatstart - this.fuelatend;
                            Console.WriteLine(this.fuelconsumption);
                            Dictionary<string, string> postParameters = new Dictionary<string, string>();
                            postParameters.Add("authcode", this.authCode);
                            postParameters.Add("job_id", this.jobID);
                            Dictionary<string, string> dictionary2 = postParameters;
                            num2 = Math.Floor((double)data.TruckValues.CurrentValues.DamageValues.Transmission * 100.0 / 1.0);
                            string str3 = num2.ToString();
                            dictionary2.Add("trailer_damage", str3);
                            postParameters.Add("income", data.JobValues.Income.ToString());
                            if (this.fuelconsumption > data.TruckValues.ConstantsValues.CapacityValues.Fuel)
                            {
                                postParameters.Add("refueled", "true");
                            }
                            postParameters.Add("fuelconsumption", this.fuelconsumption.ToString());

                            Console.WriteLine(this.api.HTTPSRequestPost(this.api.api_server + this.api.finishjob_path, postParameters, true).ToString());
                            this.InitializeDiscord(0);
                            this.totalDistance = 0;
                            this.invertedDistance = 0;
                            this.currentPercentage = 0;
                            this.lastNotZeroDistance = 0;
                            this.lastCargoDamage = 0.0f;
                            this.jobID = "0";
                            this.jobID = null;
                            this.destination_lb.Text = "";
                            this.depature_lb.Text = "";
                            //this.cargo_lb.Text = translation.no_cargo_lb;
                            
                            this.jobFinished = false;
                        }
                    }
                    this.invertedDistance = this.totalDistance - (int)Math.Round((double)data.NavigationValues.NavigationDistance, 0);
                }
            }
            catch { }
        }

        string get_unique_string(int string_length)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var bit_count = (string_length * 6);
                var byte_count = ((bit_count + 7) / 8); // rounded up
                var bytes = new byte[byte_count];
                rng.GetBytes(bytes);
                return Convert.ToBase64String(bytes);
            }
        }

        private void send_tour_status_Tick(object sender, EventArgs e)
        {
            this.jobRunning = true;
            this.locationupdate();
            
        }

        private void locationupdate()
        {

                double num3 = this.rotation;
                Dictionary<string, string> postParameters = new Dictionary<string, string>();
                Dictionary<string, string> dictionary1 = postParameters;


                num1 = CoordinateX;
                string str1 = num1.ToString();
                dictionary1.Add("coordinate_x", str1);
                Dictionary<string, string> dictionary2 = postParameters;
                num1 = CoordinateZ;
                string str2 = num1.ToString();
                dictionary2.Add("coordinate_y", str2);
                Dictionary<string, string> dictionary3 = postParameters;
                num2 = -(num3 * 180.0 / Math.PI);
                string str3 = num2.ToString();
                dictionary3.Add("rotation", str3);
                postParameters.Add("authcode", this.authCode);
                postParameters.Add("percentage", this.currentPercentage.ToString());
                postParameters.Add("game", this.Spiel);

                this.api.HTTPSRequestPost(this.api.api_server + this.api.loc_update_path, postParameters, false).ToString();

           
        }
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
            TaskBar_Icon.Dispose();
        }

        private void send_location_Tick(object sender, EventArgs e)
        {
            this.locationUpdate = true;
        }
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.send_tour_status = new System.Timers.Timer();
            this.send_location = new System.Windows.Forms.Timer(this.components);
            this.send_speedo = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dateiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.einstellungenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.creditsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.beendenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serverstatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.topMenuAccount = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuAbmeldenButton = new System.Windows.Forms.ToolStripMenuItem();
            this.topmenuwebsite = new System.Windows.Forms.ToolStripMenuItem();
            this.eventsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GUI_SIZE_BUTTON = new System.Windows.Forms.ToolStripMenuItem();
            this.lbl_Overlay = new System.Windows.Forms.ToolStripMenuItem();
            this.darkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.oldCar1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oldCar2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oldCar3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oldCar4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.keinsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label_gefahren = new System.Windows.Forms.Label();
            this.label_prozent = new System.Windows.Forms.Label();
            this.status_jb_canc_lb = new System.Windows.Forms.Label();
            this.truck_lb = new System.Windows.Forms.Label();
            this.destination_lb = new System.Windows.Forms.Label();
            this.depature_lb = new System.Windows.Forms.Label();
            this.cargo_lb = new System.Windows.Forms.Label();
            this.speed_lb = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.version_lb = new System.Windows.Forms.Label();
            this.TaskBar_Icon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextTaskbar = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.öffnenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.einstellungenToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.webseiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.überToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.beendenToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.groupStatistiken = new System.Windows.Forms.GroupBox();
            this.ats_button = new System.Windows.Forms.PictureBox();
            this.ets2_button = new System.Windows.Forms.PictureBox();
            this.truckersMP_Button = new System.Windows.Forms.Button();
            this.user_company_lb = new System.Windows.Forms.Label();
            this.statistic_panel_topic = new System.Windows.Forms.Label();
            this.act_bank_balance_lb = new System.Windows.Forms.Label();
            this.driven_tours_lb = new System.Windows.Forms.Label();
            this.groupVerkehr = new System.Windows.Forms.GroupBox();
            this.lbl_Reload_Time = new System.Windows.Forms.Label();
            this.updateTraffic = new System.Windows.Forms.Timer(this.components);
            this.lbl_Revision = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.WebServer_Status_label = new System.Windows.Forms.ToolStripStatusLabel();
            this.Label_DB_Server = new System.Windows.Forms.ToolStripStatusLabel();
            this.anti_AFK_TIMER = new System.Windows.Forms.Timer(this.components);
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.send_tour_status)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.contextTaskbar.SuspendLayout();
            this.groupStatistiken.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ats_button)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ets2_button)).BeginInit();
            this.groupVerkehr.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // send_tour_status
            // 
            this.send_tour_status.Enabled = true;
            this.send_tour_status.Interval = 15000D;
            this.send_tour_status.SynchronizingObject = this;
            this.send_tour_status.Elapsed += new System.Timers.ElapsedEventHandler(this.send_tour_status_Tick);
            // 
            // send_location
            // 
            this.send_location.Enabled = true;
            this.send_location.Interval = 15000;
            this.send_location.Tick += new System.EventHandler(this.send_location_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateiToolStripMenuItem,
            this.topMenuAccount,
            this.topmenuwebsite,
            this.eventsToolStripMenuItem,
            this.GUI_SIZE_BUTTON,
            this.lbl_Overlay,
            this.darkToolStripMenuItem,
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1388, 32);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // dateiToolStripMenuItem
            // 
            this.dateiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.einstellungenToolStripMenuItem,
            this.creditsToolStripMenuItem,
            this.beendenToolStripMenuItem,
            this.serverstatusToolStripMenuItem});
            this.dateiToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateiToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("dateiToolStripMenuItem.Image")));
            this.dateiToolStripMenuItem.Name = "dateiToolStripMenuItem";
            this.dateiToolStripMenuItem.Size = new System.Drawing.Size(36, 28);
            this.dateiToolStripMenuItem.ToolTipText = "Hauptmenü";
            // 
            // einstellungenToolStripMenuItem
            // 
            this.einstellungenToolStripMenuItem.Name = "einstellungenToolStripMenuItem";
            this.einstellungenToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.einstellungenToolStripMenuItem.Text = "Einstellungen";
            this.einstellungenToolStripMenuItem.Click += new System.EventHandler(this.einstellungenToolStripMenuItemClick);
            // 
            // creditsToolStripMenuItem
            // 
            this.creditsToolStripMenuItem.Name = "creditsToolStripMenuItem";
            this.creditsToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.creditsToolStripMenuItem.Text = "Über...";
            this.creditsToolStripMenuItem.Click += new System.EventHandler(this.CreditsToolStripMenuItem_Click);
            // 
            // beendenToolStripMenuItem
            // 
            this.beendenToolStripMenuItem.Name = "beendenToolStripMenuItem";
            this.beendenToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.beendenToolStripMenuItem.Text = "Beenden";
            this.beendenToolStripMenuItem.Click += new System.EventHandler(this.beendenToolStripMenuItemClick);
            // 
            // serverstatusToolStripMenuItem
            // 
            this.serverstatusToolStripMenuItem.Name = "serverstatusToolStripMenuItem";
            this.serverstatusToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.serverstatusToolStripMenuItem.Text = "Serverstatus (Inaktiv)";
            this.serverstatusToolStripMenuItem.Click += new System.EventHandler(this.serverstatusToolStripMenuItem_Click);
            // 
            // topMenuAccount
            // 
            this.topMenuAccount.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuAbmeldenButton});
            this.topMenuAccount.Image = ((System.Drawing.Image)(resources.GetObject("topMenuAccount.Image")));
            this.topMenuAccount.Name = "topMenuAccount";
            this.topMenuAccount.Size = new System.Drawing.Size(102, 28);
            this.topMenuAccount.Text = "Account";
            this.topMenuAccount.ToolTipText = "Client an/abmelden";
            // 
            // MenuAbmeldenButton
            // 
            this.MenuAbmeldenButton.Name = "MenuAbmeldenButton";
            this.MenuAbmeldenButton.Size = new System.Drawing.Size(151, 26);
            this.MenuAbmeldenButton.Text = "Abmelden";
            this.MenuAbmeldenButton.Click += new System.EventHandler(this.MenuAbmeldenButton_Click);
            // 
            // topmenuwebsite
            // 
            this.topmenuwebsite.Image = ((System.Drawing.Image)(resources.GetObject("topmenuwebsite.Image")));
            this.topmenuwebsite.Name = "topmenuwebsite";
            this.topmenuwebsite.Size = new System.Drawing.Size(101, 28);
            this.topmenuwebsite.Text = "Website";
            this.topmenuwebsite.ToolTipText = "Gehe zu unserer Homepage";
            this.topmenuwebsite.Click += new System.EventHandler(this.topMenuWebsiteClick);
            // 
            // eventsToolStripMenuItem
            // 
            this.eventsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("eventsToolStripMenuItem.Image")));
            this.eventsToolStripMenuItem.Name = "eventsToolStripMenuItem";
            this.eventsToolStripMenuItem.Size = new System.Drawing.Size(91, 28);
            this.eventsToolStripMenuItem.Text = "Events";
            this.eventsToolStripMenuItem.ToolTipText = "Zeige aktuelle Events (in Bearbeitung)";
            this.eventsToolStripMenuItem.Visible = false;
            // 
            // GUI_SIZE_BUTTON
            // 
            this.GUI_SIZE_BUTTON.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.GUI_SIZE_BUTTON.Image = ((System.Drawing.Image)(resources.GetObject("GUI_SIZE_BUTTON.Image")));
            this.GUI_SIZE_BUTTON.Name = "GUI_SIZE_BUTTON";
            this.GUI_SIZE_BUTTON.Size = new System.Drawing.Size(36, 28);
            this.GUI_SIZE_BUTTON.Text = "Button_Groesse";
            this.GUI_SIZE_BUTTON.ToolTipText = "Ansicht verkleinern / vergrößern";
            this.GUI_SIZE_BUTTON.Click += new System.EventHandler(this.buttonGroesseToolStripMenuItem_Click);
            // 
            // lbl_Overlay
            // 
            this.lbl_Overlay.Name = "lbl_Overlay";
            this.lbl_Overlay.Size = new System.Drawing.Size(76, 28);
            this.lbl_Overlay.Text = "Overlay";
            this.lbl_Overlay.Visible = false;
            this.lbl_Overlay.Click += new System.EventHandler(this.overlayToolStripMenuItem_Click);
            // 
            // darkToolStripMenuItem
            // 
            this.darkToolStripMenuItem.Image = global::VTCManager_1._0._0.Properties.Resources.icons8_film_noir_50;
            this.darkToolStripMenuItem.Name = "darkToolStripMenuItem";
            this.darkToolStripMenuItem.Size = new System.Drawing.Size(36, 28);
            this.darkToolStripMenuItem.ToolTipText = "Komm auf die Dunkle Seite";
            this.darkToolStripMenuItem.Click += new System.EventHandler(this.darkToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.oldCar1ToolStripMenuItem,
            this.oldCar2ToolStripMenuItem,
            this.oldCar3ToolStripMenuItem,
            this.oldCar4ToolStripMenuItem,
            this.keinsToolStripMenuItem});
            this.toolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem1.Image")));
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(36, 28);
            // 
            // oldCar1ToolStripMenuItem
            // 
            this.oldCar1ToolStripMenuItem.Name = "oldCar1ToolStripMenuItem";
            this.oldCar1ToolStripMenuItem.Size = new System.Drawing.Size(146, 26);
            this.oldCar1ToolStripMenuItem.Text = "Old Car 1";
            this.oldCar1ToolStripMenuItem.Click += new System.EventHandler(this.oldCar1ToolStripMenuItem_Click);
            // 
            // oldCar2ToolStripMenuItem
            // 
            this.oldCar2ToolStripMenuItem.Name = "oldCar2ToolStripMenuItem";
            this.oldCar2ToolStripMenuItem.Size = new System.Drawing.Size(146, 26);
            this.oldCar2ToolStripMenuItem.Text = "Old Car 2";
            this.oldCar2ToolStripMenuItem.Click += new System.EventHandler(this.oldCar2ToolStripMenuItem_Click);
            // 
            // oldCar3ToolStripMenuItem
            // 
            this.oldCar3ToolStripMenuItem.Name = "oldCar3ToolStripMenuItem";
            this.oldCar3ToolStripMenuItem.Size = new System.Drawing.Size(146, 26);
            this.oldCar3ToolStripMenuItem.Text = "Old Car 3";
            this.oldCar3ToolStripMenuItem.Click += new System.EventHandler(this.oldCar3ToolStripMenuItem_Click);
            // 
            // oldCar4ToolStripMenuItem
            // 
            this.oldCar4ToolStripMenuItem.Name = "oldCar4ToolStripMenuItem";
            this.oldCar4ToolStripMenuItem.Size = new System.Drawing.Size(146, 26);
            this.oldCar4ToolStripMenuItem.Text = "Old Car 4";
            this.oldCar4ToolStripMenuItem.Click += new System.EventHandler(this.oldCar4ToolStripMenuItem_Click);
            // 
            // keinsToolStripMenuItem
            // 
            this.keinsToolStripMenuItem.Name = "keinsToolStripMenuItem";
            this.keinsToolStripMenuItem.Size = new System.Drawing.Size(146, 26);
            this.keinsToolStripMenuItem.Text = "Keins";
            this.keinsToolStripMenuItem.Click += new System.EventHandler(this.keinsToolStripMenuItem_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(342, 342);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(145, 13);
            this.linkLabel1.TabIndex = 5;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "(powered by Truckyapp.com)";
            this.linkLabel1.VisitedLinkColor = System.Drawing.Color.Blue;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 30);
            this.label1.TabIndex = 1;
            this.label1.Text = "Verkehr";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label2.Location = new System.Drawing.Point(13, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "...";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.label_gefahren);
            this.panel2.Controls.Add(this.label_prozent);
            this.panel2.Controls.Add(this.status_jb_canc_lb);
            this.panel2.Controls.Add(this.truck_lb);
            this.panel2.Controls.Add(this.destination_lb);
            this.panel2.Controls.Add(this.depature_lb);
            this.panel2.Controls.Add(this.cargo_lb);
            this.panel2.Controls.Add(this.speed_lb);
            this.panel2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.panel2.Location = new System.Drawing.Point(540, 28);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(551, 582);
            this.panel2.TabIndex = 2;
            // 
            // label_gefahren
            // 
            this.label_gefahren.AutoSize = true;
            this.label_gefahren.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_gefahren.Location = new System.Drawing.Point(59, 281);
            this.label_gefahren.Name = "label_gefahren";
            this.label_gefahren.Size = new System.Drawing.Size(51, 20);
            this.label_gefahren.TabIndex = 8;
            this.label_gefahren.Text = "label4";
            // 
            // label_prozent
            // 
            this.label_prozent.AutoSize = true;
            this.label_prozent.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_prozent.Location = new System.Drawing.Point(59, 261);
            this.label_prozent.Name = "label_prozent";
            this.label_prozent.Size = new System.Drawing.Size(51, 20);
            this.label_prozent.TabIndex = 7;
            this.label_prozent.Text = "label4";
            // 
            // status_jb_canc_lb
            // 
            this.status_jb_canc_lb.AutoSize = true;
            this.status_jb_canc_lb.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.status_jb_canc_lb.Location = new System.Drawing.Point(148, 245);
            this.status_jb_canc_lb.Name = "status_jb_canc_lb";
            this.status_jb_canc_lb.Size = new System.Drawing.Size(0, 19);
            this.status_jb_canc_lb.TabIndex = 6;
            // 
            // truck_lb
            // 
            this.truck_lb.AutoSize = true;
            this.truck_lb.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.truck_lb.Location = new System.Drawing.Point(47, 110);
            this.truck_lb.Name = "truck_lb";
            this.truck_lb.Size = new System.Drawing.Size(48, 19);
            this.truck_lb.TabIndex = 5;
            this.truck_lb.Text = "Truck: ";
            // 
            // destination_lb
            // 
            this.destination_lb.AutoSize = true;
            this.destination_lb.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.destination_lb.Location = new System.Drawing.Point(47, 166);
            this.destination_lb.Name = "destination_lb";
            this.destination_lb.Size = new System.Drawing.Size(0, 19);
            this.destination_lb.TabIndex = 3;
            // 
            // depature_lb
            // 
            this.depature_lb.AutoSize = true;
            this.depature_lb.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.depature_lb.Location = new System.Drawing.Point(47, 149);
            this.depature_lb.Name = "depature_lb";
            this.depature_lb.Size = new System.Drawing.Size(78, 19);
            this.depature_lb.TabIndex = 2;
            this.depature_lb.Text = "Departure: ";
            // 
            // cargo_lb
            // 
            this.cargo_lb.AutoSize = true;
            this.cargo_lb.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cargo_lb.Location = new System.Drawing.Point(47, 129);
            this.cargo_lb.Name = "cargo_lb";
            this.cargo_lb.Size = new System.Drawing.Size(55, 19);
            this.cargo_lb.TabIndex = 1;
            this.cargo_lb.Text = "Freight:";
            // 
            // speed_lb
            // 
            this.speed_lb.AutoSize = true;
            this.speed_lb.BackColor = System.Drawing.Color.Transparent;
            this.speed_lb.Font = new System.Drawing.Font("Verdana", 26.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.speed_lb.Location = new System.Drawing.Point(186, 51);
            this.speed_lb.Name = "speed_lb";
            this.speed_lb.Size = new System.Drawing.Size(0, 42);
            this.speed_lb.TabIndex = 0;
            this.speed_lb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(39, 63);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 9;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(470, 179);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.Location = new System.Drawing.Point(1097, 28);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(284, 582);
            this.panel4.TabIndex = 4;
            // 
            // version_lb
            // 
            this.version_lb.AutoSize = true;
            this.version_lb.Location = new System.Drawing.Point(1287, 9);
            this.version_lb.Name = "version_lb";
            this.version_lb.Size = new System.Drawing.Size(0, 13);
            this.version_lb.TabIndex = 5;
            // 
            // TaskBar_Icon
            // 
            this.TaskBar_Icon.BalloonTipText = "VTC-Manager läuft im Hintergrund";
            this.TaskBar_Icon.BalloonTipTitle = "VTC-Manager";
            this.TaskBar_Icon.ContextMenuStrip = this.contextTaskbar;
            this.TaskBar_Icon.Icon = ((System.Drawing.Icon)(resources.GetObject("TaskBar_Icon.Icon")));
            this.TaskBar_Icon.Text = "VTC-Manager";
            this.TaskBar_Icon.Visible = true;
            // 
            // contextTaskbar
            // 
            this.contextTaskbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.öffnenToolStripMenuItem,
            this.einstellungenToolStripMenuItem1,
            this.webseiteToolStripMenuItem,
            this.überToolStripMenuItem,
            this.beendenToolStripMenuItem1});
            this.contextTaskbar.Name = "contextTaskbar";
            this.contextTaskbar.Size = new System.Drawing.Size(146, 114);
            // 
            // öffnenToolStripMenuItem
            // 
            this.öffnenToolStripMenuItem.Name = "öffnenToolStripMenuItem";
            this.öffnenToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.öffnenToolStripMenuItem.Text = "Öffnen";
            // 
            // einstellungenToolStripMenuItem1
            // 
            this.einstellungenToolStripMenuItem1.Name = "einstellungenToolStripMenuItem1";
            this.einstellungenToolStripMenuItem1.Size = new System.Drawing.Size(145, 22);
            this.einstellungenToolStripMenuItem1.Text = "Einstellungen";
            this.einstellungenToolStripMenuItem1.Click += new System.EventHandler(this.einstellungenToolStripMenuItem1_Click);
            // 
            // webseiteToolStripMenuItem
            // 
            this.webseiteToolStripMenuItem.Name = "webseiteToolStripMenuItem";
            this.webseiteToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.webseiteToolStripMenuItem.Text = "Webseite";
            // 
            // überToolStripMenuItem
            // 
            this.überToolStripMenuItem.Name = "überToolStripMenuItem";
            this.überToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.überToolStripMenuItem.Text = "Über...";
            // 
            // beendenToolStripMenuItem1
            // 
            this.beendenToolStripMenuItem1.Name = "beendenToolStripMenuItem1";
            this.beendenToolStripMenuItem1.Size = new System.Drawing.Size(145, 22);
            this.beendenToolStripMenuItem1.Text = "Beenden";
            // 
            // groupStatistiken
            // 
            this.groupStatistiken.BackColor = System.Drawing.Color.Transparent;
            this.groupStatistiken.Controls.Add(this.ats_button);
            this.groupStatistiken.Controls.Add(this.ets2_button);
            this.groupStatistiken.Controls.Add(this.truckersMP_Button);
            this.groupStatistiken.Controls.Add(this.user_company_lb);
            this.groupStatistiken.Controls.Add(this.statistic_panel_topic);
            this.groupStatistiken.Controls.Add(this.act_bank_balance_lb);
            this.groupStatistiken.Controls.Add(this.driven_tours_lb);
            this.groupStatistiken.Location = new System.Drawing.Point(0, 35);
            this.groupStatistiken.Name = "groupStatistiken";
            this.groupStatistiken.Size = new System.Drawing.Size(534, 178);
            this.groupStatistiken.TabIndex = 6;
            this.groupStatistiken.TabStop = false;
            // 
            // ats_button
            // 
            this.ats_button.Image = global::VTCManager_1._0._0.Properties.Resources.ats2l;
            this.ats_button.Location = new System.Drawing.Point(17, 124);
            this.ats_button.Name = "ats_button";
            this.ats_button.Size = new System.Drawing.Size(100, 54);
            this.ats_button.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ats_button.TabIndex = 8;
            this.ats_button.TabStop = false;
            this.ats_button.Visible = false;
            this.ats_button.Click += new System.EventHandler(this.ats_button_Click);
            // 
            // ets2_button
            // 
            this.ets2_button.Image = global::VTCManager_1._0._0.Properties.Resources._280px_Ets2_logo;
            this.ets2_button.Location = new System.Drawing.Point(225, 124);
            this.ets2_button.Name = "ets2_button";
            this.ets2_button.Size = new System.Drawing.Size(100, 54);
            this.ets2_button.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ets2_button.TabIndex = 7;
            this.ets2_button.TabStop = false;
            this.ets2_button.Visible = false;
            this.ets2_button.Click += new System.EventHandler(this.ets2_button_Click);
            // 
            // truckersMP_Button
            // 
            this.truckersMP_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.truckersMP_Button.BackColor = System.Drawing.Color.Transparent;
            this.truckersMP_Button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("truckersMP_Button.BackgroundImage")));
            this.truckersMP_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.truckersMP_Button.FlatAppearance.BorderSize = 0;
            this.truckersMP_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.truckersMP_Button.Location = new System.Drawing.Point(450, 124);
            this.truckersMP_Button.Margin = new System.Windows.Forms.Padding(0);
            this.truckersMP_Button.Name = "truckersMP_Button";
            this.truckersMP_Button.Size = new System.Drawing.Size(84, 54);
            this.truckersMP_Button.TabIndex = 6;
            this.truckersMP_Button.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.truckersMP_Button.UseVisualStyleBackColor = false;
            this.truckersMP_Button.Click += new System.EventHandler(this.truckersMP_Button_Click);
            // 
            // user_company_lb
            // 
            this.user_company_lb.AutoSize = true;
            this.user_company_lb.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.user_company_lb.Location = new System.Drawing.Point(16, 86);
            this.user_company_lb.Name = "user_company_lb";
            this.user_company_lb.Size = new System.Drawing.Size(178, 19);
            this.user_company_lb.TabIndex = 5;
            this.user_company_lb.Text = "angestellt bei: Selbstständig";
            // 
            // statistic_panel_topic
            // 
            this.statistic_panel_topic.AutoSize = true;
            this.statistic_panel_topic.BackColor = System.Drawing.Color.Transparent;
            this.statistic_panel_topic.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statistic_panel_topic.Location = new System.Drawing.Point(12, 22);
            this.statistic_panel_topic.Name = "statistic_panel_topic";
            this.statistic_panel_topic.Size = new System.Drawing.Size(174, 30);
            this.statistic_panel_topic.TabIndex = 2;
            this.statistic_panel_topic.Text = "User\'s  Statistiken";
            this.statistic_panel_topic.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // act_bank_balance_lb
            // 
            this.act_bank_balance_lb.AutoSize = true;
            this.act_bank_balance_lb.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.act_bank_balance_lb.Location = new System.Drawing.Point(16, 67);
            this.act_bank_balance_lb.Name = "act_bank_balance_lb";
            this.act_bank_balance_lb.Size = new System.Drawing.Size(139, 19);
            this.act_bank_balance_lb.TabIndex = 4;
            this.act_bank_balance_lb.Text = "aktueller Kontostand:";
            // 
            // driven_tours_lb
            // 
            this.driven_tours_lb.AutoSize = true;
            this.driven_tours_lb.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.driven_tours_lb.Location = new System.Drawing.Point(16, 48);
            this.driven_tours_lb.Name = "driven_tours_lb";
            this.driven_tours_lb.Size = new System.Drawing.Size(119, 19);
            this.driven_tours_lb.TabIndex = 3;
            this.driven_tours_lb.Text = "gefahrene Touren:";
            // 
            // groupVerkehr
            // 
            this.groupVerkehr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupVerkehr.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupVerkehr.BackColor = System.Drawing.Color.Transparent;
            this.groupVerkehr.Controls.Add(this.lbl_Reload_Time);
            this.groupVerkehr.Controls.Add(this.tableLayoutPanel1);
            this.groupVerkehr.Controls.Add(this.label1);
            this.groupVerkehr.Controls.Add(this.linkLabel1);
            this.groupVerkehr.Controls.Add(this.label2);
            this.groupVerkehr.Location = new System.Drawing.Point(0, 243);
            this.groupVerkehr.Name = "groupVerkehr";
            this.groupVerkehr.Size = new System.Drawing.Size(537, 367);
            this.groupVerkehr.TabIndex = 7;
            this.groupVerkehr.TabStop = false;
            // 
            // lbl_Reload_Time
            // 
            this.lbl_Reload_Time.AutoSize = true;
            this.lbl_Reload_Time.Location = new System.Drawing.Point(12, 345);
            this.lbl_Reload_Time.Name = "lbl_Reload_Time";
            this.lbl_Reload_Time.Size = new System.Drawing.Size(16, 13);
            this.lbl_Reload_Time.TabIndex = 6;
            this.lbl_Reload_Time.Text = "...";
            // 
            // updateTraffic
            // 
            this.updateTraffic.Enabled = true;
            this.updateTraffic.Interval = 30000;
            this.updateTraffic.Tick += new System.EventHandler(this.updateTraffic_Tick);
            // 
            // lbl_Revision
            // 
            this.lbl_Revision.AutoSize = true;
            this.lbl_Revision.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Revision.Location = new System.Drawing.Point(1312, 9);
            this.lbl_Revision.Name = "lbl_Revision";
            this.lbl_Revision.Size = new System.Drawing.Size(16, 13);
            this.lbl_Revision.TabIndex = 8;
            this.lbl_Revision.Text = "...";
            this.lbl_Revision.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.Transparent;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.WebServer_Status_label,
            this.Label_DB_Server});
            this.statusStrip1.Location = new System.Drawing.Point(0, 620);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1388, 22);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // WebServer_Status_label
            // 
            this.WebServer_Status_label.Name = "WebServer_Status_label";
            this.WebServer_Status_label.Size = new System.Drawing.Size(10, 17);
            this.WebServer_Status_label.Text = ".";
            this.WebServer_Status_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label_DB_Server
            // 
            this.Label_DB_Server.Name = "Label_DB_Server";
            this.Label_DB_Server.Size = new System.Drawing.Size(10, 17);
            this.Label_DB_Server.Text = ".";
            // 
            // anti_AFK_TIMER
            // 
            this.anti_AFK_TIMER.Enabled = true;
            this.anti_AFK_TIMER.Interval = 10000;
            this.anti_AFK_TIMER.Tick += new System.EventHandler(this.anti_AFK_TIMER_Tick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(1261, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Revision:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Main
            // 
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1388, 642);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lbl_Revision);
            this.Controls.Add(this.groupVerkehr);
            this.Controls.Add(this.groupStatistiken);
            this.Controls.Add(this.version_lb);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VTC-Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing_1);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.send_tour_status)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.contextTaskbar.ResumeLayout(false);
            this.groupStatistiken.ResumeLayout(false);
            this.groupStatistiken.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ats_button)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ets2_button)).EndInit();
            this.groupVerkehr.ResumeLayout(false);
            this.groupVerkehr.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void topMenuWebsiteClick(object sender, EventArgs e)
        {
            Process.Start("https://vtc.northwestvideo.de/");
        }


        private void beendenToolStripMenuItemClick(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void einstellungenToolStripMenuItemClick(object sender, EventArgs e)
        {
            SettingsWindow Settingswindow = new SettingsWindow();
            Settingswindow.FormClosing += new FormClosingEventHandler(ChildFormClosing);
            Settingswindow.ShowDialog();
        }

        private void ChildFormClosing(object sender, FormClosingEventArgs e)
        {


        }

        private void MenuAbmeldenButton_Click(object sender, EventArgs e)
        {
            this.settings.DeleteConfig();
            Application.Restart();
        }

        private void CreditsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ueber ueber = new Ueber();
            ueber.ShowDialog();
        }


        // Edit by Thommy
        private void Main_Load(object sender, EventArgs e)
        {


            this.discord = new Discord();
            lbl_Revision.Text = "1207";
            labelRevision = lbl_Revision.Text;

            // Check auf REGISTR
            utils.Reg_Schreiben("Reload_Traffic_Sekunden", "20");

            // Prüfen ob ETS2 und ATS Pfade angegeben sind. Wenn nicht -> Dialog
            if (utils.Reg_Lesen("TruckersMP_Autorun", "ETS2_Pfad") == "")
            {
                ETS2_Pfad_Window win = new ETS2_Pfad_Window();
                win.Show();
                win.Focus();
                this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            } else
            {
                ets2_button.Visible = true;
                ToolTip tt = new ToolTip();
                tt.SetToolTip(this.ets2_button, "Starte ETS2 im Singleplayer !");
            }

            if (utils.Reg_Lesen("TruckersMP_Autorun", "ATS_Pfad") != "")
            {
                ats_button.Visible = true;
                ToolTip tt = new ToolTip();
                tt.SetToolTip(this.ats_button, "Starte ATS im Singleplayer !");
            }


            // Back Test
            string hintergrund = utils.Reg_Lesen("TruckersMP_Autorun", "Background");
            if (hintergrund.ToString() == "oldcar1") { this.BackgroundImage = Properties.Resources.oldcar1; }
            else if (hintergrund == "oldcar2") { this.BackgroundImage = Properties.Resources.oldcar2; }
            else if (hintergrund == "oldcar3") { this.BackgroundImage = Properties.Resources.oldcar3; }
            else if (hintergrund == "oldcar4") { this.BackgroundImage = Properties.Resources.oldcar4; }
            else { this.BackgroundImage = null; }

            try
            {
                reload = Convert.ToInt32(utils.Reg_Lesen("TruckersMP_Autorun", "Reload_Traffic_Sekunden"));
            } catch
            {
                utils.Reg_Schreiben("Reload_Traffic_Sekunden", "7");
            }


            lbl_Reload_Time.Text = "Reload-Interval: " + reload + " Sek.";

            if (utils.Reg_Lesen("TruckersMP_Autorun", "TruckersMP_Pfad") != "")
            {
                truckersMP_Button.Visible = true;
            }
            else
            {
                truckersMP_Button.Visible = false;
            }

            // TMP Button anzeigen wenn Pfad in den Settings
            truckersMP_Button.Visible = (utils.Reg_Lesen("TruckersMP_Autorun", "TruckersMP_Pfad") != "" ? true : false);

            if(Utilities.IsGameRunning == false)
            {
                speed_lb.Text = "Warte auf ETS2...";
                label_gefahren.Visible = false;
                label_prozent.Visible = false;
                truck_lb.Visible = false;
                destination_lb.Visible = false;
                depature_lb.Visible = false;
                cargo_lb.Visible = false;
            } else
            {
                
                label_gefahren.Visible = true;
                label_prozent.Visible = true;
                truck_lb.Visible = true;
                destination_lb.Visible = true;
                depature_lb.Visible = true;
                cargo_lb.Visible = true;
            }



        }


        private void truckersMP_Button_Click(object sender, EventArgs e)
        {
            truckersMP_Link = utils.Reg_Lesen("TruckersMP_Autorun", "TruckersMP_Pfad");
            if (truckersMP_Link != null)
            {
                Process.Start(truckersMP_Link);
            } else
            {
                MessageBox.Show("Kein Link zu Truckers-MP angegeben!\nBitte schaue in den Einstellungen nach.", "Kein Link!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://truckyapp.com/");
        }

        private void einstellungenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form sw = new SettingsWindow();
            sw.ShowDialog();

        }


        private static Image GetImageFromURL(string url)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            HttpWebResponse httpWebReponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Stream stream = httpWebReponse.GetResponseStream();
            return Image.FromStream(stream);
        }

        private void buttonGroesseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GUI_SIZE == 1)
            {
                GUI_SIZE = 0;
                this.groupStatistiken.Visible = false;
                this.groupVerkehr.Visible = false;
                this.Size = new Size(581, 661);
                this.panel2.Location = new Point(5, 28);
                GUI_SIZE_BUTTON.Image = GetImageFromURL("https://zwpc.de/icons/expand.png");
                // COMMIT - eventuell die beiden Bilder über Ressourcen laden
                this.BackgroundImage = null;
            }
            else
            {
                GUI_SIZE = 1;
                this.groupStatistiken.Visible = true;
                this.groupVerkehr.Visible = true;
                this.Size = new Size(1404, 681);
                this.panel2.Location = new Point(540, 28);
                GUI_SIZE_BUTTON.Image = GetImageFromURL("https://zwpc.de/icons/komprimieren.png");
                // COMMIT - eventuell die beiden Bilder über Ressourcen laden
                string hintergrund = utils.Reg_Lesen("TruckersMP_Autorun", "Background");
                if (hintergrund.ToString() == "oldcar1") { this.BackgroundImage = Properties.Resources.oldcar1; }
                else if (hintergrund == "oldcar2") { this.BackgroundImage = Properties.Resources.oldcar2; }
                else if (hintergrund == "oldcar3") { this.BackgroundImage = Properties.Resources.oldcar3; }
                else if (hintergrund == "oldcar4") { this.BackgroundImage = Properties.Resources.oldcar4; }
                else { this.BackgroundImage = null; }
            }


        }

        private void overlayToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Main_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            TaskBar_Icon.Dispose();
        }

        private void darkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Is_DarkMode_On == 0)
            {
                Is_DarkMode_On = 1;
                this.BackgroundImage = null;
                menuStrip1.BackColor = System.Drawing.Color.FromArgb(46, 46, 46);
                menuStrip1.ForeColor = System.Drawing.Color.Gray;
                BackColor = System.Drawing.Color.FromArgb(46, 46, 46);
                ForeColor = System.Drawing.Color.LightGray;
            } else
            {
                Is_DarkMode_On = 0;
                string hintergrund = utils.Reg_Lesen("TruckersMP_Autorun", "Background");
                if (hintergrund.ToString() == "oldcar1") { this.BackgroundImage = Properties.Resources.oldcar1; }
                else if (hintergrund == "oldcar2") { this.BackgroundImage = Properties.Resources.oldcar2; }
                else if (hintergrund == "oldcar3") { this.BackgroundImage = Properties.Resources.oldcar3; }
                else if (hintergrund == "oldcar4") { this.BackgroundImage = Properties.Resources.oldcar4; }
                else { this.BackgroundImage = null; }

                menuStrip1.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                menuStrip1.ForeColor = System.Drawing.Color.Gray;
                BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                ForeColor = System.Drawing.Color.Black;
            }
        }

        private void updateTraffic_Tick(object sender, EventArgs e)
        {
            int wert = Convert.ToInt32(utils.Reg_Lesen("TruckersMP_Autorun", "Reload_Traffic_Sekunden"));
            updateTraffic.Interval = wert * 1000;
            lbl_Reload_Time.Text = "Reload-Interval: " + wert + " Sek.";
            this.load_traffic();

            // Serverstatus in Statusleiste anzeigen
            Servercheck sc = new Servercheck();
            var green = new Bitmap(Properties.Resources.iconfinder_bulled_green_1930264);
            var red = new Bitmap(Properties.Resources.iconfinder_bullet_red_84435);
            // Webserver-Check
            try
            {
                WebServer_Status_label.Text = "←Webserver";
                WebServer_Status_label.Image = (sc.WS_Check() == true) ? green : red;
            } catch (Exception Fehler_Server)
            {
                MessageBox.Show("Keine Verbindung zum Webserver\n" + Fehler_Server.Message, "Fehler Verbindung", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // DB-Check
            try
            {
                Label_DB_Server.Text = "←Datenbank";
                Label_DB_Server.Image = (sc.DB_Check() == true) ? green : red;
            }
            catch (Exception Fehler_Server)
            {
                MessageBox.Show("Keine Verbindung zum Datenbankserver\n" + Fehler_Server.Message, "Fehler Verbindung", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void serverstatusToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        private void anti_AFK_TIMER_Tick(object sender, EventArgs e)
        {

        }

        private void oldCar1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources.oldcar1;
            utils.Reg_Schreiben("Background", "oldcar1");
        }

        private void oldCar2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources.oldcar2;
            utils.Reg_Schreiben("Background", "oldcar2");
        }

        private void oldCar3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources.oldcar3;
            utils.Reg_Schreiben("Background", "oldcar3");
        }

        private void oldCar4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources.oldcar4;
            utils.Reg_Schreiben("Background", "oldcar4");
        }

        private void keinsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = null;
            utils.Reg_Schreiben("Background", "");
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e) =>
            TaskBar_Icon.Dispose();
       

        private void ets2_button_Click(object sender, EventArgs e) =>
            Process.Start(utils.Reg_Lesen("TruckersMP_Autorun", "ETS2_Pfad") +  @"\bin\win_X64\eurotrucks2.exe");
       

        private void ats_button_Click(object sender, EventArgs e) =>
            Process.Start(utils.Reg_Lesen("TruckersMP_Autorun", "ATS_Pfad") + @"\bin\win_X64\amtrucks.exe");
       


        private void TelemetryJobCancelled(object sender, EventArgs e)
        {
            this.jobStarted = false;
            this.jobRunning = false;
            CancelTour();
        }

        

        private void TelemetryJobDelivered(object sender, EventArgs e) =>
            this.jobFinished = true;

        private void TelemetryFined(object sender, EventArgs e) =>
            MessageBox.Show("Fined");
        private void TelemetryTollgate(object sender, EventArgs e)
        {
            Thommy th3 = new Thommy(); 
            th3.Sende_TollGate(this.authCode, this.Tollgate_Payment, 1);
        
        }
            

        private void TelemetryFerry(object sender, EventArgs e)
        {
            this.Ferry = true;
        }


        private void TelemetryTrain(object sender, EventArgs e) =>
        this.Train = true;

        private void TelemetryRefuel(object sender, EventArgs e) 
        {
                Thommy th3 = new Thommy();
                th3.Sende_Refuel(this.authCode, this.Tollgate_Payment, this.jobID);

        }



    }


}


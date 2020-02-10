using DiscordRPC;
using DiscordRPC.Logging;
using Ets2SdkClient;
using RJCP.IO.Ports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Media;
using System.Net;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;


namespace VTCManager_1._0._0
{
    public class Main : Form
    {
        private API api = new API();
        public string userID = "0";
        public string userCompanyID = "0";
        public string jobID = "0";
        private SettingsManager settings;
        public string authCode = "false";
        public Dictionary<string, string> lastJobDictionary = new Dictionary<string, string>();
        public Ets2SdkTelemetry Telemetry;
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
        public int currentPercentage;
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
        public System.Windows.Forms.ProgressBar progressBar1;
        private ToolStripMenuItem dateiToolStripMenuItem;
        private System.Windows.Forms.Label truck_lb;
        private System.Windows.Forms.Label notavaiblepanel4;
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
        private float speed;
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
        public DiscordRpcClient client;
        private ToolStripMenuItem creditsToolStripMenuItem;
        public bool discordRPCalreadrunning;
        public string CityDestination;
        public string CitySource;
        private SerialPortStream s;
        private bool serial_start = false;
        private bool first_run_speedo;
        private NotifyIcon TaskBar_Icon;
        private ContextMenuStrip contextTaskbar;
        private ToolStripMenuItem öffnenToolStripMenuItem;
        private ToolStripMenuItem einstellungenToolStripMenuItem1;
        private ToolStripMenuItem webseiteToolStripMenuItem;
        private ToolStripMenuItem überToolStripMenuItem;
        private ToolStripMenuItem beendenToolStripMenuItem1;
        private LinkLabel linkLabel1;
        private ToolStripMenuItem GUI_SIZE_BUTTON;
        private int blinker_int;
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
        public static Form over = new Overlay_1();
        private Label label6;
        public Label label_GAS;
        public Label lbl_BREMSE;
        public ProgressBar progressBar_BREMSE;
        public ProgressBar progressBar_GAS;
        public Label lbl_RPM;
        public ProgressBar progressBar_RPM;
        public Label lbl_GANG;
        public Label lbl_KUPPLUNG;
        public ProgressBar progressBar_KUPPLUNG;
        private ToolStripMenuItem darkToolStripMenuItem;
        public static int overlay_Opacity;
        public Timer updateTraffic;
        private Label lbl_Reload_Time;
        public int Is_DarkMode_On;


        public Main(string newauthcode, string username, int driven_tours, int act_bank_balance, bool last_job_canceled, string company)
        {

            over.Opacity = 0;
            over.Show();

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
            this.Telemetry = new Ets2SdkTelemetry();
            this.Telemetry.Data += new TelemetryData(this.Telemetry_Data);
            this.Telemetry.JobFinished += new EventHandler(this.TelemetryOnJobFinished);
            this.Telemetry.JobStarted += new EventHandler(this.TelemetryOnJobStarted);
            if (this.Telemetry.Error == null)
                return;
            int num = (int)MessageBox.Show("Fehler beim Ausführen von:" + this.Telemetry.Map + "\r\n" + this.Telemetry.Error.Message + "\r\n\r\nStacktrace:\r\n" + this.Telemetry.Error.StackTrace);
        }

        private void InitializeDiscord(int mode)
        {
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
            this.version_lb.Text = translation.version;
            this.MenuAbmeldenButton.Text = translation.logout;


        }

        private void load_traffic()
        {
            Utilities utils = new Utilities();
            string server = utils.Reg_Lesen("TruckersMP_Autorun", "verkehr_SERVER");

            if (string.IsNullOrEmpty(server) == true)
            {
                this.settings.Cache.truckersmp_server = "sim1";
            }

            Console.WriteLine(server);

            if (this.GUI_SIZE == 1)
            {
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

                // Verkehr Label aktualisieren

                if (server == "sim1") { label2.Text = "Server: Simulation 1"; }
                if (server == "sim2") { label2.Text = "Server: Simulation 2"; }
                if (server == "arc1") { label2.Text = "Server: Arcade 1"; }
                if (server == "eupromods1") { label2.Text = "Server: ProMods 1"; }
                if (server == "eupromods2") { label2.Text = "Server: ProMods 2"; }
            }

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

        public void Telemetry_Data(Ets2Telemetry data, bool updated)
        {
            try
            {

                if (this.InvokeRequired)
                {
                    this.Invoke((Delegate)new TelemetryData(this.Telemetry_Data), (object)data, (object)updated);
                }
                else
                {
                    int time = (int)data.Time;
                    float num1;
                    if (Utilities.IsGameRunning)
                    {
                        this.progressBar1.Style = ProgressBarStyle.Continuous;
                        if ((double)data.Job.NavigationDistanceLeft != 0.0)
                            this.lastNotZeroDistance = (int)Math.Round((double)data.Job.NavigationDistanceLeft, 0);
                        if (data.Truck != "")
                        {
                            double kupp1 = Convert.ToDouble(data.Controls.UserClutch.ToString()) * 100;
                            double gas1 = Convert.ToDouble(data.Controls.UserThrottle.ToString()) * 100;
                            double brems1 = Convert.ToDouble(data.Controls.UserBrake.ToString()) * 100;
                            double rpm1 = Convert.ToDouble(data.Drivetrain.EngineRpm.ToString());
                            double rpm_max = Convert.ToDouble(data.Drivetrain.EngineRpmMax.ToString());

                            progressBar_KUPPLUNG.Value = Convert.ToInt32(kupp1);
                            progressBar_GAS.Value = Convert.ToInt32(gas1);
                            progressBar_BREMSE.Value = Convert.ToInt32(brems1);
                            progressBar_RPM.Maximum = Convert.ToInt32(rpm_max);
                            progressBar_RPM.Value = Convert.ToInt32(rpm1);
                            progressBar_RPM.Refresh();

                            if(data.Drivetrain.Gear.ToString() == "-1")
                            {
                                lbl_GANG.Text = "R";
                            } else
                            {
                                lbl_GANG.Text = data.Drivetrain.Gear.ToString();
                            }
                            


                            if (data.Truck == "Extra_D" || data.Truck == "Superb")
                            {
                                this.truck_lb.Text = translation.car_lb + "Škoda" + " Superb";
                            }
                            else
                            {
                                this.truck_lb.Text = translation.truck_lb + data.Manufacturer + " " + data.Truck;
                            }
                            this.truck_lb.Visible = true;
                            this.destination_lb.Visible = true;
                            this.depature_lb.Visible = true;
                            this.cargo_lb.Visible = true;
                            if (this.settings.Cache.speed_mode == "mph")
                            {
                                this.speed_lb.Text = Math.Round((double)data.Drivetrain.SpeedMph).ToString().Replace("-", "") + " mph";
                            }
                            else
                            {
                                this.speed_lb.Text = Math.Round((double)data.Drivetrain.SpeedKmh).ToString().Replace("-", "") + " km/h";
                            }
                            if (this.serial_start == false)
                            {
                                this.serial_start = true;
                            }
                            this.speed = data.Drivetrain.SpeedKmh;
                            this.rpm = (int)data.Drivetrain.EngineRpm;
                            if (data.Lights.BlinkerLeftActive == true && data.Lights.BlinkerRightActive == true)
                            {
                                this.blinker_int = 3;
                            }
                            else
                            {
                                if (data.Lights.BlinkerLeftActive)
                                {
                                    this.blinker_int = 1;
                                }
                                else if (data.Lights.BlinkerRightActive)
                                {
                                    this.blinker_int = 2;
                                }
                                else
                                {
                                    this.blinker_int = 0;
                                }
                            }

                            /*this.s.Write("0"); //ABS
                            this.s.Write("0"); //Handbrake
                            if (data.Axilliary.AirPressureEmergency == true)
                            {
                                this.s.Write("1");
                            }
                            else
                            {
                                this.s.Write("0");
                            }
                            if (data.Axilliary.BatteryVoltageWarning == true)
                            {
                                this.s.Write("1");
                            }
                            else
                            {
                                this.s.Write("0");
                            }
                            this.s.Write("1"); //fog_light
                            if (data.Lights.HighBeams == true)
                            {
                                this.s.Write("1");
                            }
                            else
                            {
                                this.s.Write("0");
                            }*/

                            if (!File.Exists("test"))
                            {

                            }
                            this.CoordinateX = (double)data.Physics.CoordinateX;
                            this.CoordinateZ = (double)data.Physics.CoordinateZ;
                            this.rotation = (double)data.Physics.RotationX * Math.PI * 2.0;
                            if (data.Job.Cargo == "")
                            {
                                this.cargo_lb.Text = translation.no_cargo_lb;
                                this.depature_lb.Text = "";
                                this.destination_lb.Text = "";
                                this.progressBar1.Visible = false;
                            }
                            if (this.discordRPCalreadrunning == false)
                            {
                                this.InitializeDiscord(0); //ot working uff cant update RPC
                                this.discordRPCalreadrunning = true;
                            }
                        }
                        else
                        {
                            this.progressBar1.Style = ProgressBarStyle.Marquee;
                            this.truck_lb.Visible = false;
                            this.destination_lb.Visible = false;
                            this.depature_lb.Visible = false;
                            this.cargo_lb.Visible = false;
                            this.speed_lb.Text = translation.wait_ets2_is_ready;
                        }
                        bool flag;
                        using (Dictionary<string, string>.Enumerator enumerator = this.lastJobDictionary.GetEnumerator())
                            flag = !enumerator.MoveNext();
                        if (!flag)
                        {
                            if (this.lastJobDictionary["cargo"] == data.Job.Cargo && this.lastJobDictionary["source"] == data.Job.CitySource && this.lastJobDictionary["destination"] == data.Job.CityDestination)
                            {
                                string lastJob = this.lastJobDictionary["weight"];
                                num1 = data.Job.Mass;
                                string str = num1.ToString();
                                if (lastJob == str)
                                {
                                    this.stillTheSameJob = true;
                                    goto label_25;
                                }
                            }
                            this.stillTheSameJob = false;
                        }
                    }
                    else
                    {
                        this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
                        this.truck_lb.Visible = false;
                        this.destination_lb.Visible = false;
                        this.depature_lb.Visible = false;
                        this.cargo_lb.Visible = false;
                        this.speed_lb.Text = translation.waiting_for_ets;


                    }
                label_25:
                    double num2;
                    if (this.jobStarted)
                    {
                        bool flag;
                        using (Dictionary<string, string>.Enumerator enumerator = this.lastJobDictionary.GetEnumerator())
                            flag = !enumerator.MoveNext();
                        if (flag)
                        {
                            if ((double)data.Job.NavigationDistanceLeft != 0.0 && data.Job.CityDestination != "")
                            {

                                notification_sound_tour_start.Play();
                                this.totalDistance = (int)data.Job.NavigationDistanceLeft;
                                num2 = (double)data.Job.Income * 0.15;
                                this.cargo_lb.Text = translation.freight_lb + data.Job.Cargo + " (" + ((int)Math.Round((double)data.Job.Mass, 0) / 1000).ToString() + "t)";
                                this.depature_lb.Text = translation.depature_lb + data.Job.CitySource + " ( " + data.Job.CompanySource + " ) ";
                                this.destination_lb.Text = translation.destination_lb + data.Job.CityDestination + " ( " + data.Job.CompanyDestination + " )";
                                this.progressBar1.Visible = true;
                                this.fuelatstart = data.Drivetrain.Fuel;
                                Dictionary<string, string> postParameters = new Dictionary<string, string>();
                                postParameters.Add("authcode", this.authCode);
                                postParameters.Add("cargo", data.Job.Cargo);
                                postParameters.Add("weight", ((int)Math.Round((double)data.Job.Mass, 0) / 1000).ToString());
                                postParameters.Add("depature", data.Job.CitySource);
                                postParameters.Add("depature_company", data.Job.CompanySource);
                                postParameters.Add("destination_company", data.Job.CompanyDestination);
                                postParameters.Add("destination", data.Job.CityDestination);
                                postParameters.Add("truck_manufacturer", data.Manufacturer);
                                postParameters.Add("truck_model", data.Truck);
                                postParameters.Add("distance", this.totalDistance.ToString());

                                this.jobID = this.api.HTTPSRequestPost(this.api.api_server + this.api.new_job_path, postParameters, true).ToString();
                                this.settings.Cache.SaveJobID = "this.jobID";
                                this.settings.SaveJobID();
                                this.lastJobDictionary.Add("cargo", data.Job.Cargo);
                                this.lastJobDictionary.Add("source", data.Job.CitySource);
                                this.lastJobDictionary.Add("destination", data.Job.CityDestination);
                                Dictionary<string, string> lastJobDictionary = this.lastJobDictionary;
                                num1 = data.Job.Mass;
                                string str2 = num1.ToString();



                                lastJobDictionary.Add("weight", str2);
                                this.CitySource = data.Job.CitySource;
                                this.CityDestination = data.Job.CityDestination;
                                this.InitializeDiscord(1);
                                this.send_tour_status.Enabled = true;
                                this.send_tour_status.Start();
                                this.jobStarted = false;
                            }
                        }

                    }
                    if (this.jobRunning)
                    {
                        if (this.lastJobDictionary["cargo"] == data.Job.Cargo && this.lastJobDictionary["source"] == data.Job.CitySource && this.lastJobDictionary["destination"] == data.Job.CityDestination)
                        {
                            if (Utilities.IsGameRunning)
                            {
                                this.jobRunning = false;
                                if (this.currentPercentage > 0)
                                {
                                    if (this.totalDistance == 0 || this.totalDistance < 0)
                                        this.totalDistance = (int)data.Job.NavigationDistanceLeft;
                                    this.progressBar1.Value = this.currentPercentage;
                                    this.InitializeDiscord(1);
                                    this.api.HTTPSRequestPost(this.api.api_server + this.api.job_update_path, new Dictionary<string, string>()

                    {
                    {
                      "authcode",
                      this.authCode
                    },
                    {
                      "job_id",
                      this.jobID
                    },
                    {
                      "percentage",
                      this.currentPercentage.ToString()
                    }
                  }, false).ToString();
                                }
                            }
                        }
                        this.jobRunning = false;
                    }
                    if (this.jobFinished)
                    {
                        if (this.lastJobDictionary["cargo"] == data.Job.Cargo && this.lastJobDictionary["source"] == data.Job.CitySource && this.lastJobDictionary["destination"] == data.Job.CityDestination)
                        {
                            string lastJob = this.lastJobDictionary["weight"];
                            num1 = data.Job.Mass;
                            string str1 = num1.ToString();
                            if (lastJob == str1)
                            {
                                if (this.lastNotZeroDistance <= 2000 && this.currentPercentage > 90)
                                {
                                    Console.WriteLine(this.lastNotZeroDistance);
                                    notification_sound_tour_end.Play();
                                    this.send_tour_status.Enabled = false;
                                    this.jobRunning = false;
                                    this.fuelatend = data.Drivetrain.Fuel;
                                    this.fuelconsumption = this.fuelatstart - this.fuelatend;
                                    Console.WriteLine(this.fuelconsumption);
                                    Dictionary<string, string> postParameters = new Dictionary<string, string>();
                                    postParameters.Add("authcode", this.authCode);
                                    postParameters.Add("job_id", this.jobID);
                                    Dictionary<string, string> dictionary2 = postParameters;
                                    num2 = Math.Floor((double)data.Damage.WearTrailer * 100.0 / 1.0);
                                    string str3 = num2.ToString();
                                    dictionary2.Add("trailer_damage", str3);
                                    postParameters.Add("income", data.Job.Income.ToString());
                                    if (this.fuelconsumption > data.Drivetrain.FuelMax)
                                    {
                                        postParameters.Add("refueled", "true");
                                    }
                                    postParameters.Add("fuelconsumption", this.fuelconsumption.ToString());
                                    this.api.HTTPSRequestPost(this.api.api_server + this.api.finishjob_path, postParameters, true).ToString();
                                    this.InitializeDiscord(0);
                                    this.totalDistance = 0;
                                    this.invertedDistance = 0;
                                    this.currentPercentage = 0;
                                    this.lastNotZeroDistance = 0;
                                    this.lastCargoDamage = 0.0f;
                                    this.jobID = "0";
                                    this.destination_lb.Text = "";
                                    this.depature_lb.Text = "";
                                    //this.cargo_lb.Text = translation.no_cargo_lb;
                                    this.lastJobDictionary.Clear();
                                }
                                else
                                {
                                    this.send_tour_status.Enabled = false;
                                    this.jobRunning = false;
                                    this.CancelTour();
                                    this.lastJobDictionary.Clear();
                                }
                            }
                        }
                        Console.WriteLine(this.s.ToString());
                        this.jobFinished = false;
                    }
                    this.invertedDistance = this.totalDistance - (int)Math.Round((double)data.Job.NavigationDistanceLeft, 0);
                    try
                    {
                        this.currentPercentage = 100 * this.invertedDistance / this.totalDistance;
                    }
                    catch { }

                    this.progressBar1.Value = this.currentPercentage;
                }
            }
            catch
            {

            }
        }

        private void send_tour_status_Tick(object sender, EventArgs e)
        {
            this.jobRunning = true;
            this.locationupdate();
        }

        private void locationupdate()
        {
            if (Utilities.WhichGameIsRunning == "ets2" && ((double)this.CoordinateX != 0.0 || (double)this.CoordinateZ != 0.0))
            {
                double num3 = this.rotation;
                Dictionary<string, string> postParameters = new Dictionary<string, string>();
                Dictionary<string, string> dictionary1 = postParameters;
                num1 = this.CoordinateX;
                string str1 = num1.ToString();
                dictionary1.Add("coordinate_x", str1);
                Dictionary<string, string> dictionary2 = postParameters;
                num1 = this.CoordinateZ;
                string str2 = num1.ToString();
                dictionary2.Add("coordinate_y", str2);
                Dictionary<string, string> dictionary3 = postParameters;
                num2 = -(num3 * 180.0 / Math.PI);
                string str3 = num2.ToString();
                dictionary3.Add("rotation", str3);
                postParameters.Add("authcode", this.authCode);
                this.api.HTTPSRequestPost(this.api.api_server + this.api.loc_update_path, postParameters, false).ToString();
            }
        }
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
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
            this.topMenuAccount = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuAbmeldenButton = new System.Windows.Forms.ToolStripMenuItem();
            this.topmenuwebsite = new System.Windows.Forms.ToolStripMenuItem();
            this.eventsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GUI_SIZE_BUTTON = new System.Windows.Forms.ToolStripMenuItem();
            this.lbl_Overlay = new System.Windows.Forms.ToolStripMenuItem();
            this.darkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label_GAS = new System.Windows.Forms.Label();
            this.progressBar_GAS = new System.Windows.Forms.ProgressBar();
            this.lbl_KUPPLUNG = new System.Windows.Forms.Label();
            this.progressBar_KUPPLUNG = new System.Windows.Forms.ProgressBar();
            this.lbl_GANG = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_BREMSE = new System.Windows.Forms.Label();
            this.progressBar_BREMSE = new System.Windows.Forms.ProgressBar();
            this.lbl_RPM = new System.Windows.Forms.Label();
            this.progressBar_RPM = new System.Windows.Forms.ProgressBar();
            this.status_jb_canc_lb = new System.Windows.Forms.Label();
            this.truck_lb = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.destination_lb = new System.Windows.Forms.Label();
            this.depature_lb = new System.Windows.Forms.Label();
            this.cargo_lb = new System.Windows.Forms.Label();
            this.speed_lb = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.notavaiblepanel4 = new System.Windows.Forms.Label();
            this.version_lb = new System.Windows.Forms.Label();
            this.TaskBar_Icon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextTaskbar = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.öffnenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.einstellungenToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.webseiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.überToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.beendenToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.groupStatistiken = new System.Windows.Forms.GroupBox();
            this.truckersMP_Button = new System.Windows.Forms.Button();
            this.user_company_lb = new System.Windows.Forms.Label();
            this.statistic_panel_topic = new System.Windows.Forms.Label();
            this.act_bank_balance_lb = new System.Windows.Forms.Label();
            this.driven_tours_lb = new System.Windows.Forms.Label();
            this.groupVerkehr = new System.Windows.Forms.GroupBox();
            this.updateTraffic = new System.Windows.Forms.Timer(this.components);
            this.lbl_Reload_Time = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.send_tour_status)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.contextTaskbar.SuspendLayout();
            this.groupStatistiken.SuspendLayout();
            this.groupVerkehr.SuspendLayout();
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
            // send_speedo
            // 
            this.send_speedo.Enabled = true;
            this.send_speedo.Interval = 30;
            this.send_speedo.Tick += new System.EventHandler(this.send_speedo_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateiToolStripMenuItem,
            this.topMenuAccount,
            this.topmenuwebsite,
            this.eventsToolStripMenuItem,
            this.GUI_SIZE_BUTTON,
            this.lbl_Overlay,
            this.darkToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1458, 32);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // dateiToolStripMenuItem
            // 
            this.dateiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.einstellungenToolStripMenuItem,
            this.creditsToolStripMenuItem,
            this.beendenToolStripMenuItem});
            this.dateiToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateiToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("dateiToolStripMenuItem.Image")));
            this.dateiToolStripMenuItem.Name = "dateiToolStripMenuItem";
            this.dateiToolStripMenuItem.Size = new System.Drawing.Size(36, 28);
            this.dateiToolStripMenuItem.ToolTipText = "Hauptmenü";
            // 
            // einstellungenToolStripMenuItem
            // 
            this.einstellungenToolStripMenuItem.Name = "einstellungenToolStripMenuItem";
            this.einstellungenToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.einstellungenToolStripMenuItem.Text = "Einstellungen";
            this.einstellungenToolStripMenuItem.Click += new System.EventHandler(this.einstellungenToolStripMenuItemClick);
            // 
            // creditsToolStripMenuItem
            // 
            this.creditsToolStripMenuItem.Name = "creditsToolStripMenuItem";
            this.creditsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.creditsToolStripMenuItem.Text = "Über...";
            this.creditsToolStripMenuItem.Click += new System.EventHandler(this.CreditsToolStripMenuItem_Click);
            // 
            // beendenToolStripMenuItem
            // 
            this.beendenToolStripMenuItem.Name = "beendenToolStripMenuItem";
            this.beendenToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.beendenToolStripMenuItem.Text = "Beenden";
            this.beendenToolStripMenuItem.Click += new System.EventHandler(this.beendenToolStripMenuItemClick);
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
            this.panel2.Controls.Add(this.label_GAS);
            this.panel2.Controls.Add(this.progressBar_GAS);
            this.panel2.Controls.Add(this.lbl_KUPPLUNG);
            this.panel2.Controls.Add(this.progressBar_KUPPLUNG);
            this.panel2.Controls.Add(this.lbl_GANG);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.lbl_BREMSE);
            this.panel2.Controls.Add(this.progressBar_BREMSE);
            this.panel2.Controls.Add(this.lbl_RPM);
            this.panel2.Controls.Add(this.progressBar_RPM);
            this.panel2.Controls.Add(this.status_jb_canc_lb);
            this.panel2.Controls.Add(this.truck_lb);
            this.panel2.Controls.Add(this.progressBar1);
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
            // label_GAS
            // 
            this.label_GAS.AutoSize = true;
            this.label_GAS.Location = new System.Drawing.Point(26, 419);
            this.label_GAS.Name = "label_GAS";
            this.label_GAS.Size = new System.Drawing.Size(26, 13);
            this.label_GAS.TabIndex = 13;
            this.label_GAS.Text = "Gas";
            // 
            // progressBar_GAS
            // 
            this.progressBar_GAS.ForeColor = System.Drawing.Color.DarkRed;
            this.progressBar_GAS.Location = new System.Drawing.Point(55, 414);
            this.progressBar_GAS.Name = "progressBar_GAS";
            this.progressBar_GAS.Size = new System.Drawing.Size(493, 23);
            this.progressBar_GAS.Step = 1;
            this.progressBar_GAS.TabIndex = 10;
            // 
            // lbl_KUPPLUNG
            // 
            this.lbl_KUPPLUNG.AutoSize = true;
            this.lbl_KUPPLUNG.Location = new System.Drawing.Point(1, 448);
            this.lbl_KUPPLUNG.Name = "lbl_KUPPLUNG";
            this.lbl_KUPPLUNG.Size = new System.Drawing.Size(52, 13);
            this.lbl_KUPPLUNG.TabIndex = 17;
            this.lbl_KUPPLUNG.Text = "Kupplung";
            // 
            // progressBar_KUPPLUNG
            // 
            this.progressBar_KUPPLUNG.Location = new System.Drawing.Point(55, 443);
            this.progressBar_KUPPLUNG.Name = "progressBar_KUPPLUNG";
            this.progressBar_KUPPLUNG.Size = new System.Drawing.Size(493, 23);
            this.progressBar_KUPPLUNG.Step = 1;
            this.progressBar_KUPPLUNG.TabIndex = 16;
            // 
            // lbl_GANG
            // 
            this.lbl_GANG.AutoSize = true;
            this.lbl_GANG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_GANG.Font = new System.Drawing.Font("Verdana", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_GANG.Location = new System.Drawing.Point(429, 295);
            this.lbl_GANG.Name = "lbl_GANG";
            this.lbl_GANG.Size = new System.Drawing.Size(120, 118);
            this.lbl_GANG.TabIndex = 15;
            this.lbl_GANG.Text = "1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(217, 540);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Streckenverlauf";
            // 
            // lbl_BREMSE
            // 
            this.lbl_BREMSE.AutoSize = true;
            this.lbl_BREMSE.Location = new System.Drawing.Point(11, 480);
            this.lbl_BREMSE.Name = "lbl_BREMSE";
            this.lbl_BREMSE.Size = new System.Drawing.Size(42, 13);
            this.lbl_BREMSE.TabIndex = 12;
            this.lbl_BREMSE.Text = "Bremse";
            // 
            // progressBar_BREMSE
            // 
            this.progressBar_BREMSE.Location = new System.Drawing.Point(55, 475);
            this.progressBar_BREMSE.Name = "progressBar_BREMSE";
            this.progressBar_BREMSE.Size = new System.Drawing.Size(493, 23);
            this.progressBar_BREMSE.TabIndex = 11;
            // 
            // lbl_RPM
            // 
            this.lbl_RPM.AutoSize = true;
            this.lbl_RPM.Location = new System.Drawing.Point(22, 512);
            this.lbl_RPM.Name = "lbl_RPM";
            this.lbl_RPM.Size = new System.Drawing.Size(31, 13);
            this.lbl_RPM.TabIndex = 9;
            this.lbl_RPM.Text = "RPM";
            // 
            // progressBar_RPM
            // 
            this.progressBar_RPM.Location = new System.Drawing.Point(55, 507);
            this.progressBar_RPM.Maximum = 10000;
            this.progressBar_RPM.Name = "progressBar_RPM";
            this.progressBar_RPM.Size = new System.Drawing.Size(493, 23);
            this.progressBar_RPM.TabIndex = 8;
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
            this.truck_lb.Location = new System.Drawing.Point(47, 185);
            this.truck_lb.Name = "truck_lb";
            this.truck_lb.Size = new System.Drawing.Size(48, 19);
            this.truck_lb.TabIndex = 5;
            this.truck_lb.Text = "Truck: ";
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.progressBar1.Location = new System.Drawing.Point(3, 556);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(545, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 4;
            this.progressBar1.UseWaitCursor = true;
            // 
            // destination_lb
            // 
            this.destination_lb.AutoSize = true;
            this.destination_lb.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.destination_lb.Location = new System.Drawing.Point(48, 166);
            this.destination_lb.Name = "destination_lb";
            this.destination_lb.Size = new System.Drawing.Size(86, 19);
            this.destination_lb.TabIndex = 3;
            this.destination_lb.Text = "Destination: ";
            // 
            // depature_lb
            // 
            this.depature_lb.AutoSize = true;
            this.depature_lb.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.depature_lb.Location = new System.Drawing.Point(47, 147);
            this.depature_lb.Name = "depature_lb";
            this.depature_lb.Size = new System.Drawing.Size(78, 19);
            this.depature_lb.TabIndex = 2;
            this.depature_lb.Text = "Departure: ";
            // 
            // cargo_lb
            // 
            this.cargo_lb.AutoSize = true;
            this.cargo_lb.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cargo_lb.Location = new System.Drawing.Point(47, 128);
            this.cargo_lb.Name = "cargo_lb";
            this.cargo_lb.Size = new System.Drawing.Size(55, 19);
            this.cargo_lb.TabIndex = 1;
            this.cargo_lb.Text = "Freight:";
            // 
            // speed_lb
            // 
            this.speed_lb.AutoSize = true;
            this.speed_lb.BackColor = System.Drawing.Color.Transparent;
            this.speed_lb.Font = new System.Drawing.Font("Segoe UI", 25F);
            this.speed_lb.Location = new System.Drawing.Point(144, 38);
            this.speed_lb.Name = "speed_lb";
            this.speed_lb.Size = new System.Drawing.Size(114, 46);
            this.speed_lb.TabIndex = 0;
            this.speed_lb.Text = "Speed";
            this.speed_lb.TextAlign = System.Drawing.ContentAlignment.TopCenter;
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
            this.panel4.Controls.Add(this.notavaiblepanel4);
            this.panel4.Location = new System.Drawing.Point(1097, 28);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(349, 582);
            this.panel4.TabIndex = 4;
            // 
            // notavaiblepanel4
            // 
            this.notavaiblepanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.notavaiblepanel4.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.notavaiblepanel4.Location = new System.Drawing.Point(0, 0);
            this.notavaiblepanel4.Name = "notavaiblepanel4";
            this.notavaiblepanel4.Size = new System.Drawing.Size(349, 582);
            this.notavaiblepanel4.TabIndex = 0;
            this.notavaiblepanel4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // version_lb
            // 
            this.version_lb.AutoSize = true;
            this.version_lb.Location = new System.Drawing.Point(1349, 9);
            this.version_lb.Name = "version_lb";
            this.version_lb.Size = new System.Drawing.Size(102, 13);
            this.version_lb.TabIndex = 5;
            this.version_lb.Text = "Version: 1.1.1 Alpha";
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
            // truckersMP_Button
            // 
            this.truckersMP_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.truckersMP_Button.BackColor = System.Drawing.Color.Silver;
            this.truckersMP_Button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("truckersMP_Button.BackgroundImage")));
            this.truckersMP_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.truckersMP_Button.FlatAppearance.BorderSize = 0;
            this.truckersMP_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.truckersMP_Button.Location = new System.Drawing.Point(447, 121);
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
            this.groupVerkehr.Controls.Add(this.lbl_Reload_Time);
            this.groupVerkehr.Controls.Add(this.tableLayoutPanel1);
            this.groupVerkehr.Controls.Add(this.label1);
            this.groupVerkehr.Controls.Add(this.linkLabel1);
            this.groupVerkehr.Controls.Add(this.label2);
            this.groupVerkehr.Location = new System.Drawing.Point(0, 223);
            this.groupVerkehr.Name = "groupVerkehr";
            this.groupVerkehr.Size = new System.Drawing.Size(537, 367);
            this.groupVerkehr.TabIndex = 7;
            this.groupVerkehr.TabStop = false;
            // 
            // updateTraffic
            // 
            this.updateTraffic.Enabled = true;
            this.updateTraffic.Interval = 30000;
            this.updateTraffic.Tick += new System.EventHandler(this.updateTraffic_Tick);
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
            // Main
            // 
            this.ClientSize = new System.Drawing.Size(1458, 622);
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
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.send_tour_status)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.contextTaskbar.ResumeLayout(false);
            this.groupStatistiken.ResumeLayout(false);
            this.groupStatistiken.PerformLayout();
            this.groupVerkehr.ResumeLayout(false);
            this.groupVerkehr.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void send_speedo_Tick(object sender, EventArgs e)
        {

            /*if (first_run_speedo == false)
            {
                this.s = new SerialPortStream("COM3", 9600, 8, Parity.None, StopBits.One);
                this.s.Open();
                Console.WriteLine("Verbindung");
                System.Threading.Thread.Sleep(10000);
                first_run_speedo = true;
            }
            //this.s.WriteLine(((int)this.speed).ToString());
            //this.s.WriteLine(this.blinker_int.ToString());
            this.s.WriteLine(this.rpm.ToString());*/
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
            Utilities regi = new Utilities();
            // Progresses und Labels aktualisieren auf FORM1
            if (regi.Reg_Lesen("TruckersMP_Autorun", "show_GAS") == "1")
            {
                progressBar_GAS.Visible = true;
                label_GAS.Visible = true;
            }
            else
            {
                label_GAS.Visible = false;
                progressBar_GAS.Visible = false;
            }
            if (regi.Reg_Lesen("TruckersMP_Autorun", "show_BREMSE") == "1")
            {
                lbl_BREMSE.Visible = true;
                progressBar_BREMSE.Visible = true;
            }
            else
            {
                lbl_BREMSE.Visible = false;
                progressBar_BREMSE.Visible = false;
            }
            if (regi.Reg_Lesen("TruckersMP_Autorun", "show_KUPPLUNG") == "1")
            {
                lbl_KUPPLUNG.Visible = true;
                progressBar_KUPPLUNG.Visible = true;
            }
            else
            {
                lbl_KUPPLUNG.Visible = false;
                progressBar_KUPPLUNG.Visible = false;
            }
            if (regi.Reg_Lesen("TruckersMP_Autorun", "show_RPM_ANZEIGE") == "1")
            {
                lbl_RPM.Visible = true;
                progressBar_RPM.Visible = true;
            }
            else
            {
                lbl_RPM.Visible = false;
                progressBar_RPM.Visible = false;
            }
            if (regi.Reg_Lesen("TruckersMP_Autorun", "show_GANG") == "1")
            {
                lbl_GANG.Visible = true;
            }
            else
            {
                lbl_GANG.Visible = false;
            }
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
            Utilities util3 = new Utilities();

            int reload = Convert.ToInt32(util3.Reg_Lesen("TruckersMP_Autorun", "Reload_Traffic_Sekunden"));
            lbl_Reload_Time.Text = "Reload-Interval: " + reload + " Sek.";

            if (util3.Reg_Lesen("TruckersMP_Autorun", "TruckersMP_Pfad") != "") 
            {
                truckersMP_Button.Visible = true;
            }
            else
            {
                truckersMP_Button.Visible = false;
            }

            

            Utilities util = new Utilities();
            // TMP Button anzeigen wenn Pfad in den Settings
           truckersMP_Button.Visible = (util.Reg_Lesen("TruckersMP_Autorun", "TruckersMP_Pfad") != "" ? true : false);

            // GAS_PROGRESS_AUSBLENDEN WENN 0
            progressBar_GAS.Visible = (util.Reg_Lesen("TruckersMP_Autorun", "show_GAS") == "1" ? true : false);
            label_GAS.Visible = (util.Reg_Lesen("TruckersMP_Autorun", "show_GAS") == "1" ? true : false);
            // BREMSE AUSBLENDEN WENN 0
            progressBar_BREMSE.Visible = (util.Reg_Lesen("TruckersMP_Autorun", "show_BREMSE") == "1" ? true : false);
            lbl_BREMSE.Visible = (util.Reg_Lesen("TruckersMP_Autorun", "show_BREMSE") == "1" ? true : false);
            // KUPPLUNG AUSBLENDEN WENN 0
            progressBar_KUPPLUNG.Visible = (util.Reg_Lesen("TruckersMP_Autorun", "show_KUPPLUNG") == "1" ? true : false);
            lbl_KUPPLUNG.Visible = (util.Reg_Lesen("TruckersMP_Autorun", "show_KUPPLUNG") == "1" ? true : false);
            // RPM AUSBLENDEN WENN 0
            progressBar_RPM.Visible = (util.Reg_Lesen("TruckersMP_Autorun", "show_RPM_ANZEIGE") == "1" ? true : false);
            lbl_RPM.Visible = (util.Reg_Lesen("TruckersMP_Autorun", "show_RPM_ANZEIGE") == "1" ? true : false);
            // GANG AUSBLENDEN WENN 0
            lbl_GANG.Visible = (util.Reg_Lesen("TruckersMP_Autorun", "show_GANG") == "1" ? true : false);


            // Autostart von TruckersMP 
            if (util.Reg_Lesen("TruckersMP_Autorun", "autorun") == "1")
            {
                Process.Start(truckersMP_Link);
            }


        }


        private void truckersMP_Button_Click(object sender, EventArgs e)
        {
            if(truckersMP_Link != null)
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
            }
            else
            {
                GUI_SIZE = 1;
                this.groupStatistiken.Visible = true;
                this.groupVerkehr.Visible = true;
                this.Size = new Size(1474, 661);
                this.panel2.Location = new Point(540, 28);
                GUI_SIZE_BUTTON.Image = GetImageFromURL("https://zwpc.de/icons/komprimieren.png");
                // COMMIT - eventuell die beiden Bilder über Ressourcen laden
            }


        }

        private void overlayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (overlay_ist_offen == 0)
            {
                Main.over.Opacity = 1;
                overlay_ist_offen = 1;
            }
            else
            {
                Main.over.Opacity = 0;
                overlay_ist_offen = 0;
            }
        }

        private void Main_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            TaskBar_Icon.Dispose();
        }

        private void darkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(Is_DarkMode_On == 0)
            {
                Is_DarkMode_On = 1;
                menuStrip1.BackColor = System.Drawing.Color.FromArgb(46, 46, 46);
                menuStrip1.ForeColor = System.Drawing.Color.Gray;
                BackColor = System.Drawing.Color.FromArgb(46, 46, 46);
                ForeColor = System.Drawing.Color.LightGray;

                lbl_RPM.Visible = false;
                label_GAS.Visible = false;
                lbl_BREMSE.Visible = false;
                lbl_GANG.Visible = false;
                lbl_KUPPLUNG.Visible = false;

                progressBar_BREMSE.Visible = false;
                progressBar_GAS.Visible = false;
                progressBar_KUPPLUNG.Visible = false;
                progressBar_RPM.Visible = false;
                lbl_GANG.Visible = false;

            } else
            {
                Is_DarkMode_On = 0;
                menuStrip1.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                menuStrip1.ForeColor = System.Drawing.Color.Gray;
                BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                ForeColor = System.Drawing.Color.Black;

                lbl_RPM.Visible = true;
                label_GAS.Visible = true;
                lbl_BREMSE.Visible = true;
                lbl_GANG.Visible = true;
                lbl_KUPPLUNG.Visible = true;

                progressBar_BREMSE.Visible = true;
                progressBar_GAS.Visible = true;
                progressBar_KUPPLUNG.Visible = true;
                progressBar_RPM.Visible = true;
                lbl_GANG.Visible = true;
            }
        }

        private void updateTraffic_Tick(object sender, EventArgs e)
        {
            Utilities util3 = new Utilities();
            int wert = Convert.ToInt32(util3.Reg_Lesen("TruckersMP_Autorun", "Reload_Traffic_Sekunden"));
            updateTraffic.Interval = wert * 1000;
            lbl_Reload_Time.Text = "Reload-Interval: " + wert + " Sek.";
            this.load_traffic();
        }
    }
}

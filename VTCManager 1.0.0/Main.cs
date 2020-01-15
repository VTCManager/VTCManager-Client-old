using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;
using Ets2SdkClient;
using System.Media;
using Timer = System.Windows.Forms.Timer;
using DiscordRPC;
using DiscordRPC.Logging;
using RJCP.IO.Ports;

namespace VTCManager_1._0._0
{
    public class Main : Form
    {
        private API api = new API();
        private SettingsManager preferences = new SettingsManager();
        private Dictionary<string, string> settingsDictionary = new Dictionary<string, string>();
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

        //new GUI
        private MenuStrip menuStrip1;
        private ToolStripMenuItem einstellungenToolStripMenuItem;
        private ToolStripMenuItem beendenToolStripMenuItem;
        private ToolStripMenuItem topMenuAccount;
        private ToolStripMenuItem topmenuwebsite;
        private Panel panel1;
        private Panel panel3;
        private Panel panel4;
        private System.Windows.Forms.Label speed_lb;
        private System.Windows.Forms.Label cargo_lb;
        private System.Windows.Forms.Label depature_lb;
        private System.Windows.Forms.Label destination_lb;
        public System.Windows.Forms.ProgressBar progressBar1;
        private ToolStripMenuItem dateiToolStripMenuItem;
        private System.Windows.Forms.Label truck_lb;
        private System.Windows.Forms.Label notavaiblepanel1;
        private System.Windows.Forms.Label notavaiblepanel3;
        private System.Windows.Forms.Label notavaiblepanel4;
        private Label label1;
        private Label label2;
        private Translation translation;
        private TableLayoutPanel tableLayoutPanel1;
        private string traffic_response;
        private Label statistic_panel_topic;
        private Label act_bank_balance_lb;
        private Label driven_tours_lb;
        private Label user_company_lb;
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
        private int blinker_int;

        public Main(string newauthcode, string username, int driven_tours, int act_bank_balance, bool last_job_canceled, string company)
        {
            this.notification_sound_success = new SoundPlayer(Environment.CurrentDirectory + @"\Ressources\insight.wav");
            this.notification_sound_fail = new SoundPlayer(Environment.CurrentDirectory + @"\Ressources\time-is-now.wav");
            this.notification_sound_tour_start = new SoundPlayer(Environment.CurrentDirectory + @"\Ressources\AutopilotStart_fx.wav");
            this.notification_sound_tour_end = new SoundPlayer(Environment.CurrentDirectory + @"\Ressources\AutopilotEnd_fx.wav");
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
                this.settings.SaveJobID();;
            }
            this.authCode = newauthcode;
            this.InitializeComponent();
            this.InitializeTranslation();
            try
            {
                this.load_traffic();
            } catch (Exception e)
            {

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
            this.statistic_panel_topic.Text = this.username + translation.statistic_panel_topic;
            this.driven_tours_lb.Text = translation.driven_tours_lb + this.driven_tours;
            this.act_bank_balance_lb.Text = translation.act_bank_balance + this.act_bank_balance + "€";
            this.user_company_lb.Text = translation.user_company_lb + this.userCompany;
            this.version_lb.Text = translation.version;
            this.MenuAbmeldenButton.Text = translation.logout;
            if (this.settings.Cache.truckersmp_server == "sim1") {
                this.label2.Text = "Server: Simulation 1";
            } else if (this.settings.Cache.truckersmp_server == "sim1")
            {
                this.label2.Text = "Server: Simulation 2";
            }
            else if (this.settings.Cache.truckersmp_server == "arc1")
            {
                this.label2.Text = "Server: Arcade 1";
            }
            else if (this.settings.Cache.truckersmp_server == "eupromods1")
            {
                this.label2.Text = "Server: ProMods 1";
            }
            else if (this.settings.Cache.truckersmp_server == "eupromods2")
            {
                this.label2.Text = "Server: ProMods 2";
            }
        }

        private void load_traffic()
        {
            if (string.IsNullOrEmpty(this.settings.Cache.truckersmp_server) == true) {
                this.settings.Cache.truckersmp_server = "sim1";
                    }
            Console.WriteLine(this.settings.Cache.truckersmp_server);
            this.tableLayoutPanel1.Controls.Clear();
            this.tableLayoutPanel1.RowStyles.Clear();
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 78.18533F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.81467F));
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
            postParameters.Add("server", this.settings.Cache.truckersmp_server);
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
                            if (data.Truck == "Extra_D" || data.Truck == "Superb") {
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
                            if(this.serial_start == false)
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
                            this.CoordinateX = (double)data.Physics.CoordinateX;
                            this.CoordinateZ = (double)data.Physics.CoordinateZ;
                            this.rotation = (double)data.Physics.RotationX * Math.PI * 2.0;
                            if (data.Job.Cargo == "") {
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
                                    this.cargo_lb.Text = translation.no_cargo_lb;
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
                        this.jobFinished = false;
                    }

                    this.invertedDistance = this.totalDistance - (int)Math.Round((double)data.Job.NavigationDistanceLeft, 0);
                    this.currentPercentage = 100 * this.invertedDistance / this.totalDistance;
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
            try
            {
                this.load_traffic();
            }
            catch (Exception)
            {

            }
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
            this.beendenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.topMenuAccount = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuAbmeldenButton = new System.Windows.Forms.ToolStripMenuItem();
            this.topmenuwebsite = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.user_company_lb = new System.Windows.Forms.Label();
            this.act_bank_balance_lb = new System.Windows.Forms.Label();
            this.driven_tours_lb = new System.Windows.Forms.Label();
            this.statistic_panel_topic = new System.Windows.Forms.Label();
            this.notavaiblepanel1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.status_jb_canc_lb = new System.Windows.Forms.Label();
            this.truck_lb = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.destination_lb = new System.Windows.Forms.Label();
            this.depature_lb = new System.Windows.Forms.Label();
            this.cargo_lb = new System.Windows.Forms.Label();
            this.speed_lb = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.notavaiblepanel3 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.notavaiblepanel4 = new System.Windows.Forms.Label();
            this.version_lb = new System.Windows.Forms.Label();
            this.creditsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.send_tour_status)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
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
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateiToolStripMenuItem,
            this.topMenuAccount,
            this.topmenuwebsite});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1458, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // dateiToolStripMenuItem
            // 
            this.dateiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.einstellungenToolStripMenuItem,
            this.creditsToolStripMenuItem,
            this.beendenToolStripMenuItem});
            this.dateiToolStripMenuItem.Name = "dateiToolStripMenuItem";
            this.dateiToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.dateiToolStripMenuItem.Text = "VTCManager";
            // 
            // einstellungenToolStripMenuItem
            // 
            this.einstellungenToolStripMenuItem.Name = "einstellungenToolStripMenuItem";
            this.einstellungenToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.einstellungenToolStripMenuItem.Text = "Settings";
            this.einstellungenToolStripMenuItem.Click += new System.EventHandler(this.einstellungenToolStripMenuItemClick);
            // 
            // beendenToolStripMenuItem
            // 
            this.beendenToolStripMenuItem.Name = "beendenToolStripMenuItem";
            this.beendenToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.beendenToolStripMenuItem.Text = "Exit";
            this.beendenToolStripMenuItem.Click += new System.EventHandler(this.beendenToolStripMenuItemClick);
            // 
            // topMenuAccount
            // 
            this.topMenuAccount.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuAbmeldenButton});
            this.topMenuAccount.Name = "topMenuAccount";
            this.topMenuAccount.Size = new System.Drawing.Size(64, 20);
            this.topMenuAccount.Text = "Account";
            // 
            // MenuAbmeldenButton
            // 
            this.MenuAbmeldenButton.Name = "MenuAbmeldenButton";
            this.MenuAbmeldenButton.Size = new System.Drawing.Size(129, 22);
            this.MenuAbmeldenButton.Text = "Abmelden";
            this.MenuAbmeldenButton.Click += new System.EventHandler(this.MenuAbmeldenButton_Click);
            // 
            // topmenuwebsite
            // 
            this.topmenuwebsite.Name = "topmenuwebsite";
            this.topmenuwebsite.Size = new System.Drawing.Size(61, 20);
            this.topmenuwebsite.Text = "Website";
            this.topmenuwebsite.Click += new System.EventHandler(this.topMenuWebsiteClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.user_company_lb);
            this.panel1.Controls.Add(this.act_bank_balance_lb);
            this.panel1.Controls.Add(this.driven_tours_lb);
            this.panel1.Controls.Add(this.statistic_panel_topic);
            this.panel1.Controls.Add(this.notavaiblepanel1);
            this.panel1.Location = new System.Drawing.Point(0, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(533, 294);
            this.panel1.TabIndex = 1;
            // 
            // user_company_lb
            // 
            this.user_company_lb.AutoSize = true;
            this.user_company_lb.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.user_company_lb.Location = new System.Drawing.Point(12, 77);
            this.user_company_lb.Name = "user_company_lb";
            this.user_company_lb.Size = new System.Drawing.Size(178, 19);
            this.user_company_lb.TabIndex = 5;
            this.user_company_lb.Text = "angestellt bei: Selbstständig";
            // 
            // act_bank_balance_lb
            // 
            this.act_bank_balance_lb.AutoSize = true;
            this.act_bank_balance_lb.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.act_bank_balance_lb.Location = new System.Drawing.Point(12, 58);
            this.act_bank_balance_lb.Name = "act_bank_balance_lb";
            this.act_bank_balance_lb.Size = new System.Drawing.Size(139, 19);
            this.act_bank_balance_lb.TabIndex = 4;
            this.act_bank_balance_lb.Text = "aktueller Kontostand:";
            // 
            // driven_tours_lb
            // 
            this.driven_tours_lb.AutoSize = true;
            this.driven_tours_lb.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.driven_tours_lb.Location = new System.Drawing.Point(12, 39);
            this.driven_tours_lb.Name = "driven_tours_lb";
            this.driven_tours_lb.Size = new System.Drawing.Size(119, 19);
            this.driven_tours_lb.TabIndex = 3;
            this.driven_tours_lb.Text = "gefahrene Touren:";
            // 
            // statistic_panel_topic
            // 
            this.statistic_panel_topic.AutoSize = true;
            this.statistic_panel_topic.BackColor = System.Drawing.Color.Transparent;
            this.statistic_panel_topic.Font = new System.Drawing.Font("Segoe UI", 20F);
            this.statistic_panel_topic.Location = new System.Drawing.Point(139, 1);
            this.statistic_panel_topic.Name = "statistic_panel_topic";
            this.statistic_panel_topic.Size = new System.Drawing.Size(222, 37);
            this.statistic_panel_topic.TabIndex = 2;
            this.statistic_panel_topic.Text = "User\'s  Statistiken";
            this.statistic_panel_topic.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // notavaiblepanel1
            // 
            this.notavaiblepanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.notavaiblepanel1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.notavaiblepanel1.Location = new System.Drawing.Point(0, 0);
            this.notavaiblepanel1.Name = "notavaiblepanel1";
            this.notavaiblepanel1.Size = new System.Drawing.Size(533, 294);
            this.notavaiblepanel1.TabIndex = 0;
            this.notavaiblepanel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
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
            // panel3
            // 
            this.panel3.Controls.Add(this.tableLayoutPanel1);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.notavaiblepanel3);
            this.panel3.Location = new System.Drawing.Point(0, 327);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(534, 283);
            this.panel3.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 78.18533F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.81467F));
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
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label2.Location = new System.Drawing.Point(12, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "Server: Simulation 1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20F);
            this.label1.Location = new System.Drawing.Point(79, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(351, 37);
            this.label1.TabIndex = 1;
            this.label1.Text = "Verkehr (powered by Trucky)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // notavaiblepanel3
            // 
            this.notavaiblepanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.notavaiblepanel3.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.notavaiblepanel3.Location = new System.Drawing.Point(0, 0);
            this.notavaiblepanel3.Name = "notavaiblepanel3";
            this.notavaiblepanel3.Size = new System.Drawing.Size(534, 283);
            this.notavaiblepanel3.TabIndex = 0;
            this.notavaiblepanel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.version_lb.Size = new System.Drawing.Size(97, 13);
            this.version_lb.TabIndex = 5;
            this.version_lb.Text = "Version: 1.0.0 Beta";
            // 
            // creditsToolStripMenuItem
            // 
            this.creditsToolStripMenuItem.Name = "creditsToolStripMenuItem";
            this.creditsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.creditsToolStripMenuItem.Text = "Credits";
            this.creditsToolStripMenuItem.Click += new System.EventHandler(this.CreditsToolStripMenuItem_Click);
            // 
            // Main
            // 
            this.ClientSize = new System.Drawing.Size(1458, 622);
            this.Controls.Add(this.version_lb);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.Text = "VTCManager";
            ((System.ComponentModel.ISupportInitialize)(this.send_tour_status)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
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
            Settingswindow.ShowDialog();
        }

        private void MenuAbmeldenButton_Click(object sender, EventArgs e)
        {
            this.settings.DeleteConfig();
            Application.Restart();
        }

        private void CreditsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("Credits: \n" +
                "developed by Joschua Haß @NorthWestMedia \n" +
                "telemetry sytem based on nlhans");
        }
    }
}

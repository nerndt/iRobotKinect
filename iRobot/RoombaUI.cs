using System;
using System.IO;
using System.Xml.Serialization;
using System.IO.Ports;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;

using RoombaSCI;
using Logging;

namespace iRobotKinect
{
    [Serializable]
    public class RoombaUI
    {
        #region Constants
        public bool Init = true; // NGE05192014 Start in Drive Mode
        public bool DriveMode = true; // NGE05192014 Start in Drive Mode

        protected const string c_sLogStart = "Log Start\r\n\r\n";
        protected const string c_sRoombaUI = "RoombaUI";
        protected const string c_sStartRoomba = "Start Roomba";
        protected const string c_sMacroStart = "Macro Start";

        #endregion

        //Property Flash_Connection
        #region Member Variables

        private Config_Settings p_csConfig = null;
        public Config_Settings Config
        {
            get
            {
                return p_csConfig;
            }
            set
            {
                p_csConfig = value;
            }
        }

        private Statistics p_sStatistics = null;
        public Statistics Statistics
        {
            get
            {
                return p_sStatistics;
            }
            set
            {
                p_sStatistics = value;
            }
        }

        //Later, this will be a list of Roombas.
        private Roomba_Poller p_rCurrentRoomba = null;
        public Roomba_Poller CurrentRoomba
        {
            get
            {
                return p_rCurrentRoomba;
            }
            set
            {
                p_rCurrentRoomba = value;
            }
        }

        private List<Line2D> p_lCharts = null;
        public List<Line2D> Charts
        {
            get
            {
                return p_lCharts;
            }
            set
            {
                p_lCharts = value;
            }
        }

        private bool p_bStarted = false;
        public bool Started
        {
            get
            {
                return p_bStarted;
            }
            set
            {
                p_bStarted = value;
            }
        }

        private bool p_bDebugMode = false;
        public bool DebugMode
        {
            get
            {
                return p_bDebugMode;
            }
            set
            {
                p_bDebugMode = value;
            }
        }

        private bool p_bOpen_For_Restart = false;
        public bool Open_For_Restart
        {
            get
            {
                return p_bOpen_For_Restart;
            }
            set
            {
                p_bOpen_For_Restart = value;
            }
        }

        private bool p_bSuspended = false;
        public bool Suspended
        {
            get
            {
                return p_bSuspended;
            }
            set
            {
                p_bSuspended = value;
            }
        }

        private string p_sLogPath;
        public string LogPath
        {
            get
            {
                return p_sLogPath;
            }
            set
            {
                p_sLogPath = value;
            }
        }

        private string p_sMacroPath;
        public string MacroPath
        {
            get
            {
                return p_sMacroPath;
            }
            set
            {
                p_sMacroPath = value;
            }
        }

        private MainForm startForm = null;
        public MainForm StartForm
        {
            get
            {
                return startForm;
            }
            set
            {
                startForm = value;
            }
        }

        private frmDrive driveForm = null;
        public frmDrive DriveForm
        {
            get
            {
                return driveForm;
            }
            set
            {
                driveForm = value;
            }
        }

        private frmPacket packetForm = null;
        public frmPacket PacketForm
        {
            get
            {
                return packetForm;
            }
            set
            {
                packetForm = value;
            }
        }

        private frmGraph graphForm = null;
        public frmGraph GraphForm
        {
            get
            {
                return graphForm;
            }
            set
            {
                graphForm = value;
            }
        }

        private frmConfig configForm = null;
        public frmConfig ConfigForm
        {
            get
            {
                return configForm;
            }
            set
            {
                configForm = value;
            }
        }

        private frmSensors sensorsForm = null;
        public frmSensors SensorsForm
        {
            get
            {
                return sensorsForm;
            }
            set
            {
                sensorsForm = value;
            }
        }

        private frmCommand commandForm = null;
        public frmCommand CommandForm
        {
            get
            {
                return commandForm;
            }
            set
            {
                commandForm = value;
            }
        }

        private frmMacro macroForm = null;
        public frmMacro MacroForm
        {
            get
            {
                return macroForm;
            }
            set
            {
                macroForm = value;
            }
        }

        #endregion

        public RoombaUI()
        {
            this.Config = new Config_Settings();
            this.Statistics = new Statistics();
        }

        public void Start_Connection(bool setupLog, bool setupMacro, string appVersion)
        {
            //string sCOMM_PORT;
            Log.This("Start Connection", c_sRoombaUI, this.Config.Log.RoombaUI);

            if (this.CurrentRoomba != null)
            {
                // sCOMM_PORT = //Whatever it was set at.
            }

            Log.This("Creating New Roomba Class", c_sRoombaUI, this.Config.Log.RoombaUI);

            //Creating the Roomba Object requires you to set the Log path
            this.CurrentRoomba = new Roomba_Poller(new SerialPort(), this.Config.Polling.Frequency, this.Config.Log.Path);

            if (setupLog) { this.Setup_Log(appVersion); }
            if (setupMacro) { this.Setup_Macro(appVersion); }

            this.CurrentRoomba.Macro.FilePath = Program.UI.MacroPath; //you can get away with not setting this, but then macros won't work until you do...
            this.CurrentRoomba.Do_Not_Parse_RCV = false;

            //this.CurrentRoomba.COMM Port = sCOMM_PORT

            this.CurrentRoomba.LogSCICommands = Program.UI.Config.Log.LogSCICommands;
            this.CurrentRoomba.LogPacketData = Program.UI.Config.Log.Roomba_PacketData;
            this.CurrentRoomba.LogIO = Program.UI.Config.Log.Roomba_IO;

            this.CurrentRoomba.Poller_LogPermission = Program.UI.Config.Log.Roomba_Poller;

            Log.This(c_sStartRoomba, c_sRoombaUI, this.Config.Log.RoombaUI);
            this.CurrentRoomba.Start(this.Config.COMM.ConnectedTo, this.Config.Polling.Sensors);

            //These may not be needed, cause RoombaSCI does all the recieving of Data.

            //'Rope handlers to Roomba
            //'AddHandler Me.CurrentRoomba.IO_Handler, AddressOf Me.Roomba_DataReceived 'Me.CurrentRoomba.IO_Handler += New Roomba.Roomba_IO_Handler(Me.Roomba_DataReceived)

            //This second line may go away
            //AddHandler this.CurrentRoomba.Text_IO_Handler, AddressOf Me.Roomba_Text_DataReceived  //Me.CurrentRoomba.Text_IO_Handler += New Roomba.Roomba_Text_IO_Handler(Me.Roomba_Text_DataReceived)

            //Start up Roomba's Polling
            if (this.CurrentRoomba != null)
            {
                this.CurrentRoomba.Automatic_Polling = this.Config.Polling.Sensors; //Roomba Poller will pick this up & Start Roomba
            }

            this.Started = true;
        }

        public void Setup_Macro(string appVersion)
        {
            //I put this here in the UI because we don't always need a macro set up with every Roomba you make..
            //This is also intended to set up "defaults".  You will nood to change the filename & path from here, but at least we have a default.
            //Lets write the first entry manually, in case the log won't initialize

            //Set Unique Path & Record it in the macro & the program.UI
            Program.UI.CurrentRoomba.Macro = new Macro(Program.UI.CurrentRoomba);
            Program.UI.MacroPath = Program.UI.CurrentRoomba.Macro.FilePath = Application.ExecutablePath + " Macro " + Log.GetTimeStamp(false) + ".txt";

            System.IO.StreamWriter swLogWriter = new System.IO.StreamWriter(Program.UI.MacroPath);
            swLogWriter.Write(c_sMacroStart + " " + appVersion + "\r\n\r\n");
            swLogWriter.Close();

            Log.This("MacroWriter Initialized.", this.GetType().ToString(), true);
        }

        public void Setup_Log(string appVersion)
        {
            //Lets write the first entry manually, in case the log won't initialize

            //Set Unique Path & Record it in the macro & the program.UI
            Log.Path = Program.UI.LogPath = Application.ExecutablePath + " Log " + Log.GetTimeStamp(false) + ".txt";
            System.IO.StreamWriter swLogWriter = new System.IO.StreamWriter(Program.UI.LogPath);
            swLogWriter.Write(c_sLogStart);
            swLogWriter.Close();

            Log.This("LogWriter Initialized.", this.GetType().ToString(), true);
        }

        public void Stop_Connection(bool bDestroyRoombaObj)
        {
            Log.This("Stop Connection", c_sRoombaUI, this.Config.Log.RoombaUI);

            this.Started = false;

            if (this.CurrentRoomba != null)
            {
                if (this.CurrentRoomba.ConnectionTime != null)
                {
                    this.CurrentRoomba.ConnectionTime.Stop();
                }

                this.CurrentRoomba.Automatic_Polling = false;
                if (this.CurrentRoomba.IO != null && this.CurrentRoomba.IO.IsOpen == true)
                    this.CurrentRoomba.SetMode(SCI_Mode.Off);

                if (this.CurrentRoomba.IO != null)
                {
                    this.CurrentRoomba.IO.Close();
                    this.CurrentRoomba.IO = null;
                }

                if (bDestroyRoombaObj)
                {
                    this.CurrentRoomba = null;
                }
            }

        }

        public void SetPictureBox(bool bSensor, PictureBox pBox)
        {
            if (bSensor)
            {
                pBox.BackColor = Color.Green;
            }
            else
            {
                pBox.BackColor = Color.Transparent;
            }
        }

        private string GetTimeStamp(bool bSplit)
        {
            string sTimeStamp = DateTime.Now.Year.ToString() +
                                           DateTime.Now.Month.ToString() +
                                           DateTime.Now.Day.ToString() +
                                           DateTime.Now.Hour.ToString() +
                                           DateTime.Now.Minute.ToString() +
                                           DateTime.Now.Second.ToString() +
                                           DateTime.Now.Millisecond.ToString();

            if (bSplit)
            {
                sTimeStamp = DateTime.Now.Month.ToString() + "/" +
                                      DateTime.Now.Day.ToString() + "/" +
                                      DateTime.Now.Year.ToString() + "   " +
                      DateTime.Now.Hour.ToString() + ":" +
                      DateTime.Now.Minute.ToString() + ":" +
                      DateTime.Now.Second.ToString() + "." +
                      DateTime.Now.Millisecond.ToString();
            }

            return sTimeStamp;

        }

        public void CopyMyMenu(frmMenu FormToBorgify)
        {
            foreach (IntPtr pCurrent in Program.Menu_Cache)
            {
                frmMenu frmGrabbedForm = (frmMenu)Form.FromHandle(pCurrent);

                foreach (ToolStripMenuItem tsTop in frmGrabbedForm.MainMenuStrip.Items)
                {
                    if (tsTop.Name == "formToolStripMenuItem")
                    {
                        foreach (ToolStripMenuItem grabbedFormMenuItem in tsTop.DropDownItems)
                        {
                            foreach (ToolStripMenuItem originalFormToolItem in FormToBorgify.formToolStripMenuItem.DropDownItems)
                            {
                                if (grabbedFormMenuItem.Name == originalFormToolItem.Name) { grabbedFormMenuItem.Enabled = originalFormToolItem.Enabled; }
                            }
                        }
                    }
                }
            }
        }

        public bool IsOpen(string Form_Name)
        {
            bool bIsOpen = false;

            foreach (IntPtr pCurrent in Program.Menu_Cache)
            {
                frmMenu frmGrabbedForm = (frmMenu)Form.FromHandle(pCurrent);
                if (frmGrabbedForm.Name == Form_Name)
                {
                    bIsOpen = true;
                    break;

                }
                frmGrabbedForm = null;
            }

            return bIsOpen;
        }

        #region Forms to Open

        public void Open_Packet_Form(frmMenu frmMenu, Point location)
        {
            bool isNull = false;
            if (packetForm == null)
            {
                isNull = true;
                packetForm = new frmPacket();
            }

            Program.Menu_Cache.Add(PacketForm.Handle);

            //Now, set all of this new form's menu control settings to be the same as mine.
            Program.UI.CopyMyMenu(frmMenu);

            if (isNull == true)
            {
                packetForm.Location = location;
            }
            PacketForm.Show();
        }

        public void Open_Graph_Form(frmMenu frmMenu, Point location)
        {
            bool isNull = false;
            if (graphForm == null)
            {
                isNull = true;
                graphForm = new frmGraph();
            }

            Program.Menu_Cache.Add(GraphForm.Handle);
            Program.UI.CopyMyMenu(frmMenu);

            if (isNull == true)
            {
                graphForm.Location = location;
            }
            GraphForm.Show();
        }

        public void Open_Config_Form(frmMenu frmMenu, string sSelectedTab, Point location)
        {
            bool isNull = false;
            if (configForm == null)
            {
                isNull = true;
                configForm = new frmConfig();
            }

            Program.Menu_Cache.Add(ConfigForm.Handle);

            //Now, set all of this new form's menu control settings to be the same as mine.
            Program.UI.CopyMyMenu(frmMenu);

            switch (sSelectedTab)
            {
                case "tabPolling":
                    ConfigForm.tcConfig.SelectedTab = ConfigForm.tcConfig.SelectedTab = ConfigForm.tabPolling;
                    break;

                case "tabCOMM":
                    ConfigForm.tcConfig.SelectedTab = ConfigForm.tcConfig.SelectedTab = ConfigForm.tabCOMM;
                    break;

                default:
                    break;
            }

            if (isNull == true)
            {
                configForm.Location = location;
            }
            ConfigForm.Show();
        }

        public void Open_Drive_Form(frmMenu frmMenu, Point location)
        {
            bool isNull = false;
            if (driveForm == null)
            {
                isNull = true;
                driveForm = new frmDrive();
            }

            Program.Menu_Cache.Add(DriveForm.Handle);

            //Now, set all of this new form's menu control settings to be the same as mine.
            Program.UI.CopyMyMenu(frmMenu);

            if (isNull == true)
            {
                driveForm.Location = location;
            }
            DriveForm.Show();
        }

        public void Open_Sensors_Form(frmMenu frmMenu, Point location)
        {
            bool isNull = false;
            if (sensorsForm == null)
            {
                isNull = true;
                sensorsForm = new frmSensors();
            }

            Program.Menu_Cache.Add(SensorsForm.Handle);

            //Now, set all of this new form's menu control settings to be the same as mine.
            Program.UI.CopyMyMenu(frmMenu);

            if (isNull == true)
            {
                sensorsForm.Location = location;
            }
            SensorsForm.Show();
        }

        public void Open_Command_Form(frmMenu frmMenu, Point location)
        {
            bool isNull = false;
            if (commandForm == null)
            {
                isNull = true;
                commandForm = new frmCommand();
            }

            Program.Menu_Cache.Add(CommandForm.Handle);

            //Now, set all of this new form's menu control settings to be the same as mine.
            Program.UI.CopyMyMenu(frmMenu);

            if (isNull == true)
            {
                commandForm.Location = location;
            }
            CommandForm.Show();
        }

        public void Open_Macro_Form(frmMenu frmMenu, Point location)
        {
            bool isNull = false;
            if (macroForm == null)
            {
                isNull = true;
                macroForm = new frmMacro();
            }
            
            Program.Menu_Cache.Add(MacroForm.Handle);

            //Now, set all of this new form's menu control settings to be the same as mine.
            Program.UI.CopyMyMenu(frmMenu);

            if (isNull == true)
            {
                macroForm.Location = location;
            }
            MacroForm.Show();
        }

        #endregion

        public void Check_Battery(Sensors sSensorPollToCheck)
        {

            //Changes a property on this form if things get too bad

            //rsCurrentSensorPoll.Packet.Voltage.ToString();
            //rsCurrentSensorPoll.Packet.Current.ToString();
            //rsCurrentSensorPoll.Packet.Temperature.ToString();
            //Convert.ToString(Convert.ToInt32(this.lblTemp.Text) * 9 / 5 + 32);
            //rsCurrentSensorPoll.Packet.Charge.ToString();
            //rsCurrentSensorPoll.Packet.Capacity.ToString();
        }

        public List<string> GetPorts()
        {
            //this.cCOM_Port.Items.Clear();
            //this.cCOM_Port.Text = "";
            //this.cCOM_Port.Items.AddRange(this.GetPorts().ToArray);

            List<string> lAvailablePorts = new List<string>();
            string sPortsAvailable = "";

            try
            {

                lAvailablePorts.AddRange(SerialPort.GetPortNames());

                //Log.This("Scanning for Ports")
                foreach (string sPort in lAvailablePorts)
                {
                    sPortsAvailable += " " + sPort;
                }

                //Log.This("Ports Available: " + sPortsAvailable)
            }
            catch
            {

            }

            return lAvailablePorts;

        }

        public void Suspend_Comm(bool Suspend)
        {
            //this.Started = !Suspend;
            this.Suspended = Suspend;

            if (!this.Suspended)
            {
                this.Open_For_Restart = !Suspend;
            }
        }

        public static void ShowFile(string sFilePath)
        {
            bool bFileExists = File.Exists(sFilePath);

            if (bFileExists)
            {
                try
                {
                    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                    proc.EnableRaisingEvents = false;
                    proc.StartInfo.FileName = sFilePath;
                    proc.Start();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}







        ////These generic UI Functions  separate dll - Kevin_UI
        //public void RecursiveEnable(frmMenu Form, ToolStripMenuItem mControl, bool bEnabled)
        //{
        //    ToolStripMenuItem mCallingFormMenuItem = null;

        //    if (mControl != null)
        //    {
        //        mControl.Enabled = bEnabled;

        //        if (Form.CallingForm != null)
        //        {
        //            foreach (ToolStripMenuItem tsTop in Form.CallingForm.Menu.Items)
        //            {
        //                if (tsTop.Name == "formToolStripMenuItem")
        //                {
        //                    foreach (ToolStripMenuItem x in tsTop.DropDownItems)
        //                    {
        //                        if (x.Name == mControl.Name)
        //                        {
        //                            mCallingFormMenuItem = x;
        //                            goto ExitFor;
        //                        }
        //                    }
        //                }
        //            }

        //        ExitFor:
        //            this.RecursiveEnable(Form.CallingForm, mCallingFormMenuItem, bEnabled);
        //        }
        //    }
        //}

        //public void CloneMyMenu(frmMenu oldForm, frmMenu newForm)
        //{

        //    foreach (ToolStripMenuItem tsTop in newForm.Menu.Items)
        //    {
        //        if (tsTop.Name == "formToolStripMenuItem")
        //        {
        //            foreach (ToolStripMenuItem x in tsTop.DropDownItems)
        //            {
        //                foreach (ToolStripMenuItem y in oldForm.formToolStripMenuItem.DropDownItems)
        //                {
        //                    if (y.Name == x.Name)
        //                    {
        //                        x.Enabled = y.Enabled;
        //                    }
        //                }

        //            }
        //        }
        //    }
        //}

#region example C code to test roomba
//#include <SoftwareSerial.h>

//int rxPin = 3;
//int txPin = 4;
//int ledPin = 13;

//SoftwareSerial Roomba(rxPin,txPin);

//#define bumpright (sensorbytes[0] & 0x01)
//#define bumpleft  (sensorbytes[0] & 0x02)

//void setup() {
//  pinMode(ledPin, OUTPUT);   // sets the pins as output
//  Serial.begin(115200);
//  Roomba.begin(115200);  
//  digitalWrite(ledPin, HIGH); // say we're alive
//  Serial.println ("Sending start command...");
//  delay (1000);
//   // set up ROI to receive commands  
//  Roomba.write(128);  // START
//  delay(150);
//  Serial.println ("Sending Safe Mode command...");
//  delay (1000);
//  Roomba.write(131);  // CONTROL
//  delay(150);
//  digitalWrite(ledPin, LOW);  // say we've finished setup
//  Serial.println ("Ready to go!");
//  delay (5000);
//}

//void loop() {
//  digitalWrite(ledPin, HIGH); // say we're starting loop
//  Serial.println ("Go Forward");
//  goForward();
//  delay (500);
//  Serial.println ("Halt!");
//  halt();
//  Serial.println ("Go Backwards");
//  delay (500);
//  goBackward();
//  delay (500);
//  Serial.println ("Halt!");
//  halt();
//  while(1) { } // Stop program
//}

//void goForward() {
//  Roomba.write(137);   // DRIVE
//  Roomba.write((byte)0x00);   // 0x00c8 == 200
//  Roomba.write(0xc8);
//  Roomba.write(0x80);
//  Roomba.write((byte)0x00);
//}
//void goBackward() {
//  Roomba.write(137);   // DRIVE
//  Roomba.write(0xff);   // 0xff38 == -200
//  Roomba.write(0x38);
//  Roomba.write(0x80);
//  Roomba.write((byte)0x00);
//}

//void halt(){
// byte j = 0x00;
// Roomba.write(137);   
// Roomba.write(j);   
// Roomba.write(j);
// Roomba.write(j);
// Roomba.write(j);
//}
#endregion
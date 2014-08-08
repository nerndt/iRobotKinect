using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading;

using System.Xml;
using System.Xml.Serialization;

using RoombaSCI;
using Logging;

//Whats Left:
// Finish hooking up Roomba's Forms,
// Add Logging & Macro Generation

namespace iRobotKinect
{
    public partial class frmStart : iRobotKinect.frmMenu
    {
        #region Constants
        protected const char c_sDot = '.';
        protected const string c_sVoltage = " Voltage: ";
        protected const string c_sCurrent = " Current: ";
        protected const string c_sChargeState = "Charge State: ";
        protected const string c_sIsCurrent = "Roomba.IsCurrent = ";
        protected const string c_sFinished = "Finished";
        protected const string c_sOff = "Off";
        protected const string c_sTick = "Tick.."; //+ " Interval: ";
        protected const string c_sNoRead = "No Read";
        protected const string c_sStopped = "Stopped";
        protected const string c_sSuspended = "Suspended"; //Means that another process in Roomba-Term has stopped polling for its own needs
        protected const string c_sRecording = "Recording Macro";
        protected const string c_sNotRecording = "Not Recording";
        protected const string c_sExecuting = "Executing";
        protected const string c_sNotExecuting = "NotExecuting";
        protected const string c_sRoombaTerm = "Roomba-Term: ";
        protected const string c_sLoadingConfig = "Loading Config File: ";


        #endregion

        #region Member Variables

        static int m_iFormUpdated_DisplayLag = 0;
        static bool m_bPluggedInFlasher = false;
        static bool m_bRecordingFlasher = false;
        static bool m_bExecutingFlasher = false;
        static List<string> comPorts = new List<string>();

        #endregion

        public frmStart()
        {
            InitializeComponent();
            Program.UI = new RoombaUI();
        }

        public double p_dFormUpdated;

        public double FormUpdated
        {
            get
            {
                return (this.p_dFormUpdated);
            }
            set
            {
                this.p_dFormUpdated = value;
            }
        }

        #region Events

        #region Form

        private void frmStart_Load(object sender, EventArgs e)
        {
            Program.UI.CurrentRoomba = new Roomba_Poller();
            this.LoadForm();

            // Try to connect and start the power on for the rumba if it is not already on
            if (comPorts.Count > 0)
            {
                this._Start();
                Program.UI.DriveForm.SetToFullMode();
            }
        }

        private void frmStart_FormClosing(object sender, FormClosingEventArgs e)
        {
            this._Stop(false, true);
        }

        #region Buttons

        private void btnShutDown_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        #endregion

        #region Timers

        private void tForm_Timer_Tick(object sender, EventArgs e)
        {
            this.Tick();
        }

        #endregion

        #region MenuItems

        private void explanationOfThisFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string explanation;

            explanation = "This form is the startup & current status form. It  must be running for the other forms to work. \r\n";
            explanation += "This app is presented as a sample application that uses the C# RoombaSCI framework created by Kevin Gabbert. \r\n\r\n";

            explanation += "Double-Click on the boxes in the upper right of the Start Form to start/Stop communication to Roomba. \r\n\r\n";
            explanation += "You can Double-Click on any of the boxes on this Form to start/Stop their primary function. \r\n\r\n";

            explanation += "The colored boxes in the upper right are there to give you feedback on this application's connection with Roomba.  \r\n";
            explanation += "Red is obvious. something is not working. Green shows you that something is working. Light green means things are \r\n";
            explanation += "working too, but it shows where this program is making an assumption. For example, If Roomba keeps giving this app back \r\n";
            explanation += "the same thing over and over again, then for speed purposes, this program will not do any evaluation or parsing of new data that is similar. \r\n";
            explanation += "In that case, light green is shown. I thought it helpful to show it to you so that you can know what is going on. When a box is the same color \r\n";
            explanation += "as the form, that means nothing is happening  there. It may be that this program has nothing to parse, or there is no data being recieved. \r\n\r\n";
            explanation += "The blue text in the parse box means that the data it is parsing is not new. Black text = new data. The time taken to parse is expressed in Ticks. \r\n\r\n";

            explanation += "This form uses COM1 by default. You will need to change the port if Roomba is set up to a different COM Port. \r\n";
            explanation += "The logs & macro filenames are automatically generated with a unique name in the application folder, or bin dir. Use the menu to open the latest.";

            MessageBox.Show(explanation);
        }

        private void restartConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._Start();
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._Stop(true, true);
        }

        private void startTimerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log.This("Starting Form Timer via Menu", this.Name, Program.UI.Config.Log.StartForm);
            this.tForm_Timer.Start();
        }

        private void stopTimerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log.This("Stopping Form Timer via Menu", this.Name, Program.UI.Config.Log.StartForm);
            this.tForm_Timer.Stop();
        }

        private void configureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sOpen = "tabPolling";
            Log.This("Open Form via Menu: " + sOpen, this.Name, Program.UI.Config.Log.StartForm);
            Program.UI.Open_Config_Form(this, sOpen, new Point(0,0));
        }
        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Log.This("Open Log via Menu", this.Name, Program.UI.Config.Log.StartForm);
            RoombaUI.ShowFile(Program.UI.LogPath);
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int iDeletedFiles = 0;

            Log.This("Clear Form Logs Requested", this.Name, Program.UI.Config.Log.StartForm);

            string[] sLogFiles = Directory.GetFiles(Path.GetDirectoryName(Application.ExecutablePath), "roomba-term.EXE Log*");

            Log.This(sLogFiles.Length + " Log Files Found.", this.Name, Program.UI.Config.Log.StartForm);

            foreach (string sCurrent in sLogFiles)
            {
                try
                {
                    //Don't shoot the food!
                    if (sCurrent != Program.UI.LogPath)
                    {
                        File.Delete(sCurrent);
                        Log.This(sCurrent + " Deleted.", this.Name, Program.UI.Config.Log.StartForm);
                        iDeletedFiles += 1;
                    }
                    else
                    {
                        Log.This("This log file ignored: " + sCurrent, this.Name, Program.UI.Config.Log.StartForm);
                    }
                }
                catch (Exception ex)
                {
                    //Keep going
                    Log.This("Delete File Error: " + ex.Message, this.Name, Program.UI.Config.Log.StartForm);
                }
            }

            string sLogFilesDeleted = iDeletedFiles.ToString() + " Log Files Deleted.";

            Log.This(sLogFilesDeleted, this.Name, Program.UI.Config.Log.StartForm);
            MessageBox.Show(sLogFilesDeleted);
        }

        private void openCurrentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log.This("Open Macro via Menu", this.Name, Program.UI.Config.Log.StartForm);
            RoombaUI.ShowFile(Program.UI.MacroPath);
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log.This("Execute Saved Macro Requested", this.Name, Program.UI.Config.Log.StartForm);

            //Create a new ShowOpenFile Dialog
            DialogResult result = loadMacro.ShowDialog();
            string selectedFile = loadMacro.FileName;

            //Execute will ignole any badly formatted Macro commands, but the form will let you know that there are errors 
            //by checking a property.
            Program.UI.CurrentRoomba.Macro.Execute(selectedFile);

            //lblMacroExecuting will flash green on every successful command & red on a failed command.
            //While executing, it will display the line number of the currently executing line. (gets line# from Macro object)
            //"Executing Line: 232"
        }

        #endregion

        private void llSet_COMM_Port_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string sOpen = "tabCOMM";
            Log.This("Open Form via Menu: " + sOpen, this.Name, Program.UI.Config.Log.StartForm);
            Program.UI.Open_Config_Form(this, sOpen, new Point(0, 0));
        }

        #region labels

        private void insetControls_DoubleClick(object sender, EventArgs e)
        {
            if (!Program.UI.Started)
            {
                Log.This("Double-Click Start", this.Name, Program.UI.Config.Log.StartForm);
                this._Start();
            }
            else
            {
                Log.This("Double-Click Stop", this.Name, Program.UI.Config.Log.StartForm);
                this._Stop(true, true);

                if (Program.UI.CurrentRoomba == null)
                {
                    Program.UI.CurrentRoomba = new Roomba_Poller();
                }
            }
        }

        private void lblRecording_DoubleClick(object sender, EventArgs e)
        {
            this.ToggleRecordMacro();
        }

        private void lblMacroExecuting_DoubleClick(object sender, EventArgs e)
        {
            this.ToggleExecuteMacro();
        }

        private void lblRecording_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.lblRecording, "double-click to Start/Stop Recording Macro");
        }

        private void lblMacroExecuting_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.lblRecording, "double-click to Start/Stop Macro Execution");
        }

        private void lblChargeState_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.lblChargeState, "This control isn't perfect yet. it falsely reports sometimes.");
        }

        private void lblPoller_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.lblChargeState, "If Roomba won't communicate, the battery might be low.");
        }

        #endregion

        #endregion

        #endregion

        private void LoadForm()
        {
            //this.Location = new Point(2000, 0); // NGE07152014 Make it off screen // this.Location = new Point(0, 0); // Put Startup form in top left of screen
            this.Location = new Point(0, 0); // NGE07152014 Make it off screen // this.Location = new Point(0, 0); // Put Startup form in top left of screen

            //Program.UI.CurrentRoomba.HookUp(); //this is where we would hook up events if we chose to use them.
            //If this is the "first run" of the app (no XMLConfig file) then pop open frmConfig and put
            //a blinkie next to the comm port. Tell user to close form when done. and start Roomba

            //Read in settings from XML.  If no XML, then use defaults.
            //On first close of config form, then a new settings file will be made.

            loadToolStripMenuItem.Enabled = false;

            Program.UI.Setup_Log(this.lblVersion.Text);

            Log.DebugMode = Program.UI.DebugMode = this.chkAppDebugMode.Checked;
            Log.This(c_sRoombaTerm + this.lblVersion.Text, this.Name, true);

            this.tForm_Timer.Interval = (Program.UI.Config.Forms.StartForm.Timer);

            Log.This("Starting Form Timer - Form_Load", this.Name, true);
            this.tForm_Timer.Start();

            string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string sConfig_File = path + @"\config.xml";

            Log.This(c_sLoadingConfig + sConfig_File, this.Name, true);

            //MessageBox.Show("Before Config File");

            if (File.Exists(sConfig_File))
            {
                //MessageBox.Show("Found Config File");
                //iRobotKinect.Config_Settings csSavedData = Config_Settings.Deserialize("Resources/config"); // "Resources/config.xml"

                XmlSerializer s = new XmlSerializer(typeof(iRobotKinect.Config_Settings));
                TextReader trRead = new StreamReader(sConfig_File);
                iRobotKinect.Config_Settings csSavedData = (iRobotKinect.Config_Settings)s.Deserialize(trRead); // "Resources/config.xml"
                trRead.Close();

                Program.UI.Config = csSavedData;

                Log.This("Config File loaded.", this.Name, Program.UI.Config.Log.StartForm);

                comPorts.AddRange(Program.UI.GetPorts().ToArray());

                string comFoundResult = Program.UI.Config.COMM.ConnectedTo;
                if (comPorts.Count > 0)
                {
                    //MessageBox.Show("Found Com Port " + comPorts[0]);
                    comFoundResult = comPorts.Find(item => item == Program.UI.Config.COMM.ConnectedTo);
                    if (comFoundResult != null)
                    {
                        this.lblCOMM_Port_Used.Text = Program.UI.Config.COMM.ConnectedTo;
                    }
                    else
                    {
                        Program.UI.Config.COMM.ConnectedTo = comPorts[0];
                        this.lblCOMM_Port_Used.Text = comPorts[0];
                        // At some point need to save!!!!!!! s.Serialize(csSavedData, sConfig_File);
                    }
                }
                else
                {
                    this.lblCOMM_Port_Used.Text = "NONE";
                }
                //MessageBox.Show("Com Port Used " + this.lblCOMM_Port_Used.Text);

                #region Open all windows at startup
                Show();
                Program.UI.StartForm = this;
                Program.UI.Open_Config_Form(this, "", new Point(2000, 0)); // Offscreen // NGE07172014 Program.UI.Open_Config_Form(this, "", new Point(0, Location.Y + Size.Height));
                Program.UI.Open_Drive_Form(this, new Point(2000, 0)); // Offscreen // NGE07172014 Program.UI.Open_Drive_Form(this, new Point(Location.X + Size.Width, 0));
                Program.UI.Open_Packet_Form(this, new Point(Program.UI.DriveForm.Location.X, Program.UI.DriveForm.Location.Y + Program.UI.DriveForm.Size.Height));
                Program.UI.Open_Sensors_Form(this, new Point(Program.UI.DriveForm.Location.X + Program.UI.DriveForm.Size.Width, 0));
                // NGE07162014 Program.UI.Open_Config_Form(this, "", new Point(0, Location.Y + Size.Height));
                // NGE07162014 Program.UI.Open_Drive_Form(this, new Point(0, 0)); // NGE07152014 Program.UI.Open_Drive_Form(this, new Point(Location.X + Size.Width, 0));
                // NGE07152014 Program.UI.Open_Packet_Form(this, new Point(Program.UI.DriveForm.Location.X, Program.UI.DriveForm.Location.Y + Program.UI.DriveForm.Size.Height));
                // NGE07152014 Program.UI.Open_Sensors_Form(this, new Point(Program.UI.DriveForm.Location.X + Program.UI.DriveForm.Size.Width, 0));

                // NGE07172014 Program.UI.DriveForm.Focus();
                // NGE07172014 Program.UI.DriveForm.Update();
                if (Program.UI.DriveForm != null)
                {
                    Program.UI.DriveForm.Visible = false;
                }
                if (Program.UI.PacketForm != null)
                {
                    Program.UI.PacketForm.Visible = false;
                }
                if (Program.UI.ConfigForm != null)
                {
                    Program.UI.ConfigForm.Visible = false;
                }
                if (Program.UI.MacroForm != null)
                {
                    Program.UI.MacroForm.Visible = false;
                }
                if (Program.UI.SensorsForm != null)
                {
                    Program.UI.SensorsForm.Visible = false;
                }
                //if (this != null)
                //{
                //    this.Visible = false;
                //    Update();
                //}
                #endregion Open all windows at startup
            }
        }

        public void Tick()
        {
            tForm_Timer.Stop();

            bool bSuspended = (Program.UI.Suspended);
            Log.This(c_sTick, this.Name, Program.UI.Config.Log.StartForm_Timer);

            //Is COMM Port on the UI object set?
            //is value in "this.Config.ConnectedTo" in Program.UI.CurrentRoomba.GetPorts().ToArray()?

            //if not then pop open Config Form for user to set it.

            //Update Status Lights
            if (Program.UI.CurrentRoomba != null)
            {
                this.Check_If_Recording();
                this.Check_If_Executing();

                this.Check_Roomba_Object();

                if (!Program.UI.Suspended)
                {
                    this.Check_Form_Connection();

                    //If frmConfig.Config.Do Battery Check is checked...

                    if (Program.UI.Config.Forms.StartForm.Battery_Check)
                    {
                        Program.UI.Check_Battery(Program.UI.CurrentRoomba.Sensors);
                    }
                }
            }

            //My one & only Star Trek reference.
            //I feel that this is needed so that i can see how fast this timer is going. Its also feedback. "Is that program DOING anything??"
            this.lblFormTimer.Text = lblFormTimer.Text + c_sDot;
            if (this.lblFormTimer.Text.Length > 19)
            {
                this.lblFormTimer.Text = "";
            }

            if (bSuspended)
            {
                this._Suspend();
            }
            else
            {
                if (Program.UI.Open_For_Restart)
                {
                    this._Start();

                    Program.UI.Open_For_Restart = false;
                }
            }

            this.tForm_Timer.Interval = (Program.UI.Config.Forms.StartForm.Timer);
            tForm_Timer.Start();

            if (Program.UI.Init == true && Program.UI.DriveMode == true)
            {
                Update();

                Program.UI.Init = false;
            }
        }

        public void _Start()
        {
            this._Stop(false, false);

            try
            {
                this.lblFormUpdated.Text = "Starting..";
                this.lblPoller.Text = "Starting..";
                this.lblCOMM_Port_Used.Text = Program.UI.Config.COMM.ConnectedTo;
                Program.UI.Start_Connection(false, true, this.lblVersion.Text); //we set up the log earlier so we can capture form events.
                this.lblCOMM_Port_Used.Text = Program.UI.CurrentRoomba.IO.PortName;

                //'Start Form Timer
                this.tForm_Timer.Interval = (Program.UI.Config.Polling.Frequency);

                Log.This("Restarting Form Timer Interval: " + Program.UI.Config.Polling.Frequency.ToString(), this.Name, Program.UI.Config.Log.StartForm);
                this.tForm_Timer.Start();

                Program.UI.Statistics.ConnectedTime = new TimeSpan();

                startToolStripMenuItem.Enabled = false;
                stopToolStripMenuItem.Enabled = true;

                loadToolStripMenuItem.Enabled = true; //Load Macro

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            GC.Collect();

            //if (this.chkAutoOpen.Checked)
            //{
            //    if(! Program.UI.IsOpen("frmPacket"))
            //    {
            //        sensorPacketToolStripMenuItem.Enabled = false;
            //        Program.UI.Open_Packet_Form(this);
            //    }
            //}

        }
        public void _Stop(bool bUpdate_Indicator, bool bDestroyRoombaObj)
        {
            loadToolStripMenuItem.Enabled = false; //Load Macro

            Log.This("Stop Roomba. ", this.Name, Program.UI.Config.Log.StartForm);

            Program.UI.Stop_Connection(bDestroyRoombaObj);

            if (bUpdate_Indicator)
            {
                this.lblIsCurrent.Text = frmStart.c_sStopped;
                this.lblIsCurrent.BackColor = Color.Transparent;

                this.lblFormUpdated.BackColor = Color.Transparent;
                this.lblFormUpdated.Text = c_sStopped;

                this.lblPoller.BackColor = Color.Transparent;
                this.lblPoller.Text = "No Obj";
            }

            GC.Collect();
        }
        public void _Suspend()
        {
            if (Program.UI.CurrentRoomba.Automatic_Polling) { Program.UI.CurrentRoomba.Automatic_Polling = false; };

            this.lblIsCurrent.Text = frmStart.c_sSuspended;
            this.lblIsCurrent.BackColor = Color.Yellow;

            this.lblFormUpdated.BackColor = Color.Yellow;
            this.lblFormUpdated.Text = c_sSuspended;
        }

        public void Check_Form_Connection()
        {
            //if this stays red, then that means that Automatic Polling is off

            if (Program.UI.CurrentRoomba.Sensors != null)
            {
                DateTime dtNow = DateTime.Now;
                DateTime dtLastUpdated = Program.UI.CurrentRoomba.Sensors.LastUpdated;
                TimeSpan tsResult = dtNow - dtLastUpdated;
                double dForm_Updated = (dtNow - dtLastUpdated).TotalMilliseconds;

                this.FormUpdated = dForm_Updated;

                Log.This("FormUpdated: " + this.FormUpdated.ToString(), this.Name, Program.UI.Config.Log.StartForm_Timer);

                if ((this.FormUpdated > 0) & (this.FormUpdated < 10000))
                {
                    this.lblFormUpdated.Text = this.FormUpdated.ToString();
                    m_iFormUpdated_DisplayLag = 0;

                    //TODO: These numbers could be boxed on the config form
                    if (this.FormUpdated < 500)
                    {
                        if (Program.UI.CurrentRoomba.New_State)
                        {
                            this.lblFormUpdated.BackColor = Color.Green;
                        }
                        else
                        {
                            this.lblFormUpdated.BackColor = Color.LightGreen;
                        }
                    }

                    if (this.FormUpdated > 500) { lblFormUpdated.BackColor = Color.Transparent; }

                    Program.UI.Started = true;
                }
                else
                {
                    m_iFormUpdated_DisplayLag++;
                    if (m_iFormUpdated_DisplayLag > Program.UI.Config.Forms.StartForm.FormUpdated_DisplayLag)
                    {
                        this.lblFormUpdated.Text = frmStart.c_sNoRead;
                        this.lblFormUpdated.BackColor = Color.Red;
                        this.lblChargeState.BackColor = Color.Transparent;
                        this.lblChargeState.Text = null;

                        Program.UI.Started = false;

                        // NGE04102014 try to restart the Roomba
                        //Thread.Sleep(2000);
                        //restartConnectionToolStripMenuItem_Click
                    }
                }
            }
        }
        public void Check_Roomba_Object()
        {
            if (Program.UI.CurrentRoomba.New_State)
            {
                this.lblIsCurrent.ForeColor = Color.Black;
            }
            else
            {
                this.lblIsCurrent.ForeColor = Color.Blue;
            }

            Sensors snRoomba = Program.UI.CurrentRoomba.Sensors; //This is how we get our info from Roomba. Events are too bulky.

            //TODO: We may want to add in a check here to see if the battery is too low. If it is, then change lblPoller to Brown or something to show that it is polling, but has issues.

            if (Program.UI.CurrentRoomba.Polling)
            {
                this.lblPoller.BackColor = Color.Green;

                lblPoller.Text = Program.UI.CurrentRoomba.PollTicks.ToString();

                if (Program.UI.Started)
                {
                    this.Check_If_Plugged_In(Program.UI.CurrentRoomba.Sensors);
                }
            }
            else
            {
                lblPoller.BackColor = Color.Red;
                lblPoller.Text = c_sOff;
            }

            if (snRoomba != null)
            {
                Log.This(c_sIsCurrent + snRoomba.IsCurrent.ToString() + "  " + this.FormUpdated.ToString(), this.Name, Program.UI.Config.Log.StartForm_Timer);

                if (snRoomba.IsCurrent)
                {
                    if (snRoomba.LastUpdated != new DateTime())
                    {
                        this.SetIsCurrentConnected(snRoomba);
                    }
                    else
                    {
                        //Transparent basically means "Old Data is persisting in the Sensors Object". Nothing new was recieved and parsed. 
                        this.toolTip1.SetToolTip(lblIsCurrent, null);
                        lblIsCurrent.BackColor = Color.Transparent;
                    }
                }
                else
                {
                    lblIsCurrent.Text = frmStart.c_sNoRead;
                    lblIsCurrent.BackColor = Color.Red;
                }
            }
        }

        private void SetIsCurrentConnected(Sensors snRoomba)
        {
            this.toolTip1.SetToolTip(lblIsCurrent, null);
            this.lblIsCurrent.Text = snRoomba.ParseTime.Ticks.ToString();

            if (Program.UI.CurrentRoomba.New_State)
            {
                this.lblIsCurrent.BackColor = Color.Green;
            }
            else
            {
                this.lblIsCurrent.BackColor = Color.LightGreen;
            }

            this.toolTip1.SetToolTip(lblIsCurrent, snRoomba.LastUpdated.ToString());
            this.lblLastCurrent.Text = snRoomba.LastUpdated.ToString();
        }

        private void RecordFlasher(bool bRecording)
        {
            if (bRecording)
            {
                this.lblMacroExecuting.Enabled = false;
                this.lblRecording.Text = c_sRecording;

                m_bRecordingFlasher = !m_bRecordingFlasher;
                if (m_bRecordingFlasher) { this.lblRecording.BackColor = Color.Green; } else { this.lblRecording.BackColor = Color.Transparent; }
            }
            else
            {
                this.lblMacroExecuting.Enabled = true;
            }
        }
        private void ExecuteFlasher(bool bExecuting)
        {
            if (bExecuting)
            {
                this.lblRecording.Enabled = false;
                this.lblMacroExecuting.Text = c_sExecuting;

                m_bExecutingFlasher = !m_bExecutingFlasher;
                if (m_bExecutingFlasher) { this.lblMacroExecuting.BackColor = Color.Green; } else { this.lblMacroExecuting.BackColor = Color.Transparent; }
            }
            else
            {
                this.lblRecording.Enabled = true;
                this.lblMacroExecuting.BackColor = Color.Transparent;
            }
        }

        private void ToggleRecordMacro()
        {
            //If path & everything else is set up, then toggle Recording
            this.lblRecording.BackColor = Color.Transparent;
            this.lblRecording.Text = "";

            if (Program.UI.CurrentRoomba != null)
            {
                if (!Program.UI.CurrentRoomba.Macro.Recording)
                {
                    try
                    {
                        Program.UI.CurrentRoomba.Macro.Record();

                        this.lblRecording.BackColor = Color.Green;
                        this.lblRecording.Text = c_sRecording;
                    }
                    catch (Exception ex)
                    {
                        if (ex.GetType() == typeof(RoombaSCI.MacroException))
                        {
                            this.lblRecording.Text = ex.Message;
                            this.lblRecording.BackColor = Color.Red;
                        }
                    }
                }
                else
                {
                    Program.UI.CurrentRoomba.Macro.Stop();
                    this.lblRecording.BackColor = Color.Transparent;
                    this.lblRecording.Text = c_sNotRecording;
                }

                Log.This("Macro Recording " + Program.UI.CurrentRoomba.Macro.Recording.ToString(), this.Name, Program.UI.Config.Log.StartForm);
            }
        }
        private void ToggleExecuteMacro()
        {
            //If path & everything else is set up, then toggle Recording
            this.lblMacroExecuting.BackColor = Color.Transparent;
            this.lblMacroExecuting.Text = "";

            if (!Program.UI.CurrentRoomba.Macro.Executing)
            {
                try
                {
                    Program.UI.CurrentRoomba.Macro.Execute();

                    this.lblMacroExecuting.BackColor = Color.Green;
                    this.lblMacroExecuting.Text = c_sExecuting;
                }
                catch (Exception ex)
                {
                    if (ex.GetType() == typeof(RoombaSCI.MacroException))
                    {
                        this.lblMacroExecuting.Text = ex.Message;
                        this.lblMacroExecuting.BackColor = Color.Red;
                    }
                }
            }
            else
            {
                Program.UI.CurrentRoomba.Macro.Stop();
                this.lblMacroExecuting.BackColor = Color.Transparent;
                this.lblMacroExecuting.Text = c_sNotExecuting;
            }

            Log.This("Macro Recording " + Program.UI.CurrentRoomba.Macro.Recording.ToString(), this.Name, Program.UI.Config.Log.StartForm);
        }

        private void PluggedIn(bool bPluggedIn)
        {
            if (bPluggedIn)
            {
                m_bPluggedInFlasher = !m_bPluggedInFlasher;
                if (m_bPluggedInFlasher) { this.lblChargeState.BackColor = Color.Yellow; } else { this.lblChargeState.BackColor = Color.Transparent; }
            }
            else
            {
                this.lblChargeState.BackColor = Color.Transparent;
            }
        }
        private void PluggedIn(bool bPluggedIn, bool noFlash)
        {
            if (bPluggedIn)
            {
                if (!noFlash)
                {
                    m_bPluggedInFlasher = !m_bPluggedInFlasher;
                }
                else
                {
                    m_bPluggedInFlasher = bPluggedIn;
                }

                if (m_bPluggedInFlasher) { this.lblChargeState.BackColor = Color.Yellow; } else { this.lblChargeState.BackColor = Color.Transparent; }
            }
            else
            {
                this.lblChargeState.BackColor = Color.Transparent;
                this.lblChargeState.Text = Charge_State_Description.Indeterminate;
            }
        }

        private void Check_If_Plugged_In(Sensors rsCurrentSensorPoll)
        {
            //Check sensors to see if current is negative. 
            //If it is, then disable the motor buttons & flash "Plugged In" in Bold Red in time with the sensor polls.

            //If Roomba is charging, then flash that in Red as well, in time with the sensor polls.
            //Program.UI.Started = false;
            byte chargeState = rsCurrentSensorPoll.Packet.Charging_State;
            this.PopulateChargeState(chargeState, rsCurrentSensorPoll);

            Log.This(c_sChargeState + this.lblChargeState.Text + c_sVoltage + rsCurrentSensorPoll.Packet.Voltage.ToString() + c_sCurrent + rsCurrentSensorPoll.Packet.Current.ToString(),
                         this.Name, Program.UI.Config.Log.StartForm_Charging);
        }

        private void Check_If_Recording()
        {
            if (Program.UI.CurrentRoomba.Macro != null)
            {
                this.RecordFlasher(Program.UI.CurrentRoomba.Macro.Recording);
            }
            else
            {
                this.RecordFlasher(false);
            }
        }
        private void Check_If_Executing()
        {
            if (Program.UI.CurrentRoomba.Macro != null)
            {
                this.ExecuteFlasher(Program.UI.CurrentRoomba.Macro.Executing);
                if (Program.UI.CurrentRoomba.Macro.Finished)
                {
                    this.lblMacroExecuting.Text = c_sFinished;
                    this.ExecuteFlasher(false);
                }
            }
            else
            {
                this.ExecuteFlasher(false);
            }
        }

        public void PopulateChargeState(byte byChargingState, Sensors rsCurrentSensorPoll)
        {
            bool bPluggedIn = false;

            Application.DoEvents();

            if (byChargingState == Charging_State.Not_Charging)
            {
                this.lblChargeState.Text = Charge_State_Description.Not_Charging;
                bPluggedIn = false;
            }
            else if (byChargingState == Charging_State.Charging_Recovery)
            {
                this.lblChargeState.Text = Charge_State_Description.Charging_Recovery;
                bPluggedIn = true;
            }
            else if (byChargingState == Charging_State.Charging)
            {
                //After you unplug Roomba, Roomba's OS still thinks it is charging, so you will continue to get this indicator.
                //The best thing to do to reset things os to start & stop Roomba
                this.lblChargeState.Text = Charge_State_Description.Charging;
                bPluggedIn = true;
            }
            else if (byChargingState == Charging_State.Trickle_Charging)
            {
                this.lblChargeState.Text = Charge_State_Description.Trickle_Charging;
                bPluggedIn = true;
            }
            else if (byChargingState == Charging_State.Waiting)
            {
                this.lblChargeState.Text = Charge_State_Description.Waiting;
                bPluggedIn = true;
            }
            else if (byChargingState == Charging_State.Charging_Error)
            {
                this.lblChargeState.Text = Charge_State_Description.Charging_Error;
                bPluggedIn = true;
            }
            else
            {
                this.lblChargeState.Text = "Error";
                bPluggedIn = false;
            }

            this.PluggedIn(bPluggedIn);

            if (rsCurrentSensorPoll.Packet.Current < 0)
            {
                this.lblChargeState.Text = Charge_State_Description.Plugged_In;
                this.PluggedIn(true, true);
            }

        }

        #region Drive Commands // NGE !!!!!!!!!!!!

        /// <summary>
        /// Capture all events and decide what to do with them (arrow keys for moving robot, space bar for stopping).
        /// </summary>
        /// <param name="m"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message m, Keys keyData)
        {
            if (this.WindowState == FormWindowState.Minimized) // Do not process control keys when minimized
            {
                return base.ProcessCmdKey(ref m, keyData);
            }
            bool blnProcess = false;

            if (Program.UI.DriveForm != null)
            {
                if (keyData == Keys.Up)
                {
                    Program.UI.DriveForm.HandleKeys(keyData);
                    blnProcess = true; // Process the keystroke

                }
                else if (keyData == Keys.Down)
                {
                    Program.UI.DriveForm.HandleKeys(keyData);
                    blnProcess = true; // Process the keystroke
                }
                else if (keyData == Keys.Left)
                {
                    Program.UI.DriveForm.HandleKeys(keyData);
                    blnProcess = true; // Process the keystroke
                }
                else if (keyData == Keys.Right)
                {
                    Program.UI.DriveForm.HandleKeys(keyData);
                    blnProcess = true; // Process the keystroke
                }
                else if (keyData == Keys.Space)
                {
                    Program.UI.DriveForm.HandleKeys(keyData);
                    blnProcess = true; // Process the keystroke
                }
            }

            if (blnProcess == true)
                return true;
            else
                return base.ProcessCmdKey(ref m, keyData);

        }

        private void buttonResetAll_Click(object sender, EventArgs e)
        {
            Program.UI.DriveForm.ResetAll();
        }

        private void buttonResetPosition_Click(object sender, EventArgs e)
        {
            Program.UI.DriveForm.ResetPosition();
        }

        private void buttonResetDirection_Click(object sender, EventArgs e)
        {
            Program.UI.DriveForm.ResetDirection();
        }

        #endregion Drive Commands

        public static void ShowFormInContainerControl(Control ctl, Form frm)
        {
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            frm.Visible = true;
            ctl.Controls.Add(frm);
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            // Close the application
            this.Close();
        }
    }
}

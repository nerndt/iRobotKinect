using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Diagnostics;

using Logging;
using RoombaSCI;

//TODO: Fix buttons so that multiple button presses are supported. - Fixed 04092014

namespace iRobotKinect
{
    public partial class frmDrive : iRobotKinect.frmMenu
    {
        static public PointF RobotPosition = new Point(0, 0); // Keep trackof the robot's position based on time, velocity, and angle
        static public float RobotDirection = 0; // Keep trackof the robot's direction based on time, velocity, and angle
        static public double RobotSpeed = 0; // Keep trackof the robot's speed based on time, velocity, and angle
        // xPos = time * cos(angle) * velocity
        // yPos = time * sin(angle) * velocity
        static public long ElapsedTimeMilliseconds = 0;
        static public Stopwatch stopWatchStart = new Stopwatch();
        static public Stopwatch stopWatch = new Stopwatch();
        static public bool MotionStarted = false; // Keep track of if motion has began

        static public bool Moving = false; // Keep track of motion
        static public bool Rotating = false; // Keep track of direction

        // This delegate enables asynchronous calls for setting
        // the text property on a TextBox control.
        //delegate void SetTextCallback(string text);
        delegate void SetTextCallback();

        // This thread is used to demonstrate both thread-safe and
        // unsafe ways to call a Windows Forms control.
        private Thread timerThread = null;

        public frmDrive()
        {
            InitializeComponent();
            string directions = "KEYS" + Environment.NewLine + "Up-Forwards" + Environment.NewLine + "Down-Reverse" + Environment.NewLine + "Right/Left-Rotate" + Environment.NewLine + "+/- Change Speed" + Environment.NewLine + "Spacebar Stop";
            labelInstructions.Text = directions;
            this.numericBaseSpeed.Value = m_iDefaultCurrentSpeed;
            this.numericRotationBaseSpeed.Value = m_iDefaultCurrentRotationSpeed;
            this.lrSpeed.Value = m_iDefaultCurrentRotationSpeed;
        }

        #region Member variables

        public static int m_iDefaultCurrentSpeed = 20;
        public static int m_iDefaultCurrentRotationSpeed = 20;
        private int m_iCurrentSpeed = 0;
        private int m_iCurrentRotationSpeed = 20;
        private int m_iCurrentAngle = 32768;
        private bool m_bCurrentRotationDirectionPositive = true;
            
        //private bool m_bPluggedInFlasher = false;

        #endregion
        #region Properties

            private bool p_bDebugMode;
            public bool DebugMode
            {
                get
                {
                    return (this.p_bDebugMode);
                }
                set
                {
                    this.p_bDebugMode = value;
                }
            }

        #endregion

        #region Event Handlers

            #region Form

                private void frmDrive_Load(object sender, EventArgs e)
                {
                    this.lblError.Visible = chkShowErrors.Checked;

                    this.Form_Timer = new RoombaSCI.Timer();
                    this.Form_Timer.Period = (int)this.udFormDisplay.Value; 

                    // Hook up the Elapsed event for the timer.
                    this.Form_Timer.Tick += new EventHandler(OnTimedEvent);
                    this.Form_Timer.Start();

                    this.PASSIVE(true);
                    this.KeyPreview = true;

                    chkAccurateSensors.Checked = Program.UI.Config.Forms.DriveForm.Accurate_Sensor_Display;
                }

                private void frmDrive_FormClosing(object sender, FormClosingEventArgs e)
                {
                    this.Form_Timer.Stop();
                    Application.DoEvents();

                    this.driveToolStripMenuItem.Enabled = true;
                    Program.Menu_Cache.Remove(this.Handle);
                    Program.UI.CopyMyMenu(this);

                    Program.UI.DriveForm = null;
                }

                private void frmDrive_KeyDown(object sender, KeyEventArgs e)
                {
                    this.GetKey(e);
                }

            #endregion

            #region Timers

                private void OnTimedEvent(object sender, System.EventArgs e)
                {
                    this.Form_Timer.Stop();
                    this.Display_SensorInfo();
                    this.Form_Timer.Start();
                }

            #endregion

            #region CheckBoxes

                private void chkDebugConnection_CheckedChanged(object sender, EventArgs e)
                {
                    chkDebugConnection.Enabled = false;
                    this.DebugMode = this.chkDebugConnection.Checked;
                    chkDebugConnection.Enabled = true;
                }

                private void chkShowErrors_CheckedChanged(object sender, EventArgs e)
                {
                    chkShowErrors.Enabled = false;
                    this.lblError.Visible = chkShowErrors.Checked;
                    chkShowErrors.Enabled = true;
                }

                private void tMain_Brush_CheckedChanged(object sender, EventArgs e)
                {
                    tMain_Brush.Enabled = false;

                    //Log.This("User Request Main Brush: " + this.tMain_Brush.Checked.ToString());

                    byte bMotorSettings;
                    if (!tMain_Brush.Checked)
                    {
                        bMotorSettings = Convert.ToByte(Motor.Main_Brush.Off);
                    }
                    else
                    {
                        bMotorSettings = Convert.ToByte(Motor.Main_Brush.On);
                    }

                    bool bSuccess = Program.UI.CurrentRoomba.Motor_Action(bMotorSettings);
                    //this.tlStatus.Text = "Motor_Action Success : " + bSuccess.ToString() + "  " + this.GetTimeStamp(true);

                    tMain_Brush.Enabled = true;
                }

                private void tVacuum_CheckedChanged(object sender, EventArgs e)
                {
                    tVacuum.Enabled = false;

                    //Log.This("User Request Vacuum: " + this.tVacuum.Checked.ToString());

                    byte bMotorSettings;
                    if (!this.tVacuum.Checked)
                    {
                        bMotorSettings = Convert.ToByte(Motor.Vacuum.Off);
                    }
                    else
                    {
                        bMotorSettings = Convert.ToByte(Motor.Vacuum.On);
                    }

                    bool bSuccess = Program.UI.CurrentRoomba.Motor_Action(bMotorSettings);
                    //this.tlStatus.Text = "Motor_Action Success : " + bSuccess.ToString() + "  " + this.GetTimeStamp(true);

                    tVacuum.Enabled = true;
                }

                private void tSideBrush_CheckedChanged(object sender, EventArgs e)
                {
                    tSideBrush.Enabled = false;

                    //Log.This("User Request Side Brush: " + this.tSideBrush.Checked.ToString());

                    byte bMotorSettings;
                    if (!this.tSideBrush.Checked)
                    {
                        bMotorSettings = Convert.ToByte(Motor.Side_Brush.Off);
                    }
                    else
                    {
                        bMotorSettings = Convert.ToByte(Motor.Side_Brush.On);
                    }

                    bool bSuccess = Program.UI.CurrentRoomba.Motor_Action(bMotorSettings);
                    //this.tlStatus.Text = "Motor_Action Success : " + bSuccess.ToString() + "  " + this.GetTimeStamp(true);

                    tSideBrush.Enabled = true;
                }

            #endregion

            #region MenuItems

                private void restartConnectionToolStripMenuItem_Click(object sender, EventArgs e)
                {
                    this.Form_Timer.Start();
                }

                private void stopToolStripMenuItem_Click(object sender, EventArgs e)
                {
                    this.Form_Timer.Stop();
                }

                private void clearToolStripMenuItem_Click(object sender, EventArgs e)
                {
                    this.Clear();
                }

                private void PASSIVEToolStripMenuItem_Click(object sender, EventArgs e)
                {
                    PASSIVEToolStripMenuItem.Enabled = false;
                    this.PASSIVE(true);
                    PASSIVEToolStripMenuItem.Enabled = true;
                }

                private void SAFEToolStripMenuItem_Click(object sender, EventArgs e)
                {
                    SAFEToolStripMenuItem.Enabled = false;
                    this.SAFE(true);
                    SAFEToolStripMenuItem.Enabled = true;
                }

                private void FULLToolStripMenuItem_Click(object sender, EventArgs e)
                {
                    FULLToolStripMenuItem.Enabled = false;
                    this.FULL(true);
                    FULLToolStripMenuItem.Enabled = true;
                }

            #endregion

            #region Numeric UpDowns

                private void udSpeed_MouseDoubleClick(object sender, MouseEventArgs e)
                {
                    if (this.DebugMode)
                    {
                        try
                        {
                            udSpeed.Enabled = false;
                            Drive((int)udSpeed.Value, (int)udRotate.Value); // NGE07182014 
                            // NGE07182014 Program.UI.CurrentRoomba.Drive((Velocity)udSpeed.Value, (int)udRotate.Value);
                            udSpeed.Enabled = true;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                private void udFormDisplay_ValueChanged(object sender, EventArgs e)
                {
                    udFormDisplay.Enabled = false;
                    this.Form_Timer.Period = (int)this.udFormDisplay.Value; //put this on Packet>Config tab
                    udFormDisplay.Enabled = true;
                }

            #endregion

            #region Labels

                public void SetToPassiveMode()
                {
                    lPassive.Enabled = false;
                    this.PASSIVE(true);
                    lPassive.Enabled = false;
                }

                private void lPassive_DoubleClick(object sender, EventArgs e)
                {
                    SetToPassiveMode();
                }

                public void SetToSafeMode()
                {
                    lSafe.Enabled = false;
                    this.SAFE(true);
                    lSafe.Enabled = true;
                }

                private void lSafe_DoubleClick(object sender, EventArgs e)
                {
                    SetToSafeMode();
                }

                public void SetToFullMode()
                {
                    lFull.Enabled = false;
                    this.FULL(true);
                    lFull.Enabled = true;
                }

                private void lFull_DoubleClick(object sender, EventArgs e)
                {
                    SetToFullMode();
                }

                private void lPassive_MouseHover(object sender, EventArgs e)
                {
                    this.lPassive.Font = new Font("Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold);
                }

                private void lSafe_MouseHover(object sender, EventArgs e)
                {
                    this.lSafe.Font = new Font("Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold);
                }

                private void lFull_MouseHover(object sender, EventArgs e)
                {
                    this.lFull.Font = new Font("Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold);
                }

                private void lPassive_MouseLeave(object sender, EventArgs e)
                {
                    this.lPassive.Font = new Font("Microsoft Sans Serif", 8, System.Drawing.FontStyle.Regular);
                }

                private void lSafe_MouseLeave(object sender, EventArgs e)
                {
                    this.lSafe.Font = new Font("Microsoft Sans Serif", 8, System.Drawing.FontStyle.Regular);
                }

                private void lFull_MouseLeave(object sender, EventArgs e)
                {
                    this.lFull.Font = new Font("Microsoft Sans Serif", 8, System.Drawing.FontStyle.Regular);
                }

            #endregion

            #region Buttons

                private void btnApply_Click(object sender, EventArgs e)
                {
                    btnApply.Enabled = false;
                    Program.UI.Config.Save();
                    btnApply.Enabled = true;
                }

                #region Hardware UI
                    public void TurnOnRumba()
                    {
                        //Here is a Python code fragment that illustrates this method 
                        //(Device Detect is connected to the PC’s RTS line via a level 
                        //shifter):
                        //ser = serial.Serial(0, baudrate=19200, 
                        //timeout=0.1)
                        //ser.open()
                        //# wake up robot 
                        //ser.setRTS (0) 
                        //time.sleep (0.1) 
                        //ser.setRTS (1) 
                        //time.sleep (2)
                        //# pulse device-detect three times 
                        //for i in range (3): 
                        // ser.setRTS (0) 
                        // time.sleep (0.25) 
                        // ser.setRTS (1) 
                        // time.sleep (0.25)
                        btnPower.Enabled = false;
                        //Log.This("User clicked Power Button");

                        //Serial.println("Sending start command...");
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

                        //Thread.Sleep(1000);
                        //Program.UI.CurrentRoomba.Execute(OpCode.Start);
                        //Thread.Sleep(150);
                        //Program.UI.CurrentRoomba.Execute(OpCode.Safe_Mode);
                        //Thread.Sleep(150);                       

                        Program.UI.CurrentRoomba.Execute(OpCode.Power);
                        btnPower.Enabled = true;
                    }

                    private void btnPower_Click(object sender, EventArgs e)
                    {
                        TurnOnRumba();
                    }

                    private void btnSpot_Click(object sender, EventArgs e)
                    {
                        btnSpot.Enabled = false;
                        //Log.This("User clicked Spot Button");
                        bool bSuccess = Program.UI.CurrentRoomba.Execute(OpCode.Spot);
                        btnSpot.Enabled = true;
                    }

                    private void btnClean_Click(object sender, EventArgs e)
                    {
                        btnClean.Enabled = false;
                        //Log.This("User clicked Clean Button");
                        bool bSuccess = Program.UI.CurrentRoomba.Execute(OpCode.Clean);
                        btnClean.Enabled = true;
                    }

                    private void btnMax_Click(object sender, EventArgs e)
                    {
                        btnMax.Enabled = false;
                        //Log.This("User clicked Max Button");
                        bool bSuccess = Program.UI.CurrentRoomba.Execute(OpCode.Max);
                        btnMax.Enabled = true;
                    }

                    private void btnHome_Click(object sender, EventArgs e)
                    {
                        /// <summary>
                        /// Turns on Roomba's force-seeking dock mode, which causes the robot to immediately attempt to dock during its cleaning cycle.<br></br>
                        /// if it encounters the docking beams from its home base. (note, however, that if the robot was not active in a clean, spot, or max cycle<br></br>
                        /// it will not attempt to execute the docking.)  Normally, the robot attempts to dock only if the cleaning cycle has completed or the battery<br></br>
                        /// is nearing depletion. The command cam be sent anytime, but the mode will be cancelled if the robot turns off, begins charging, or is<br></br>
                        /// commanded to <b>safe</b> or <b>full</b> mode.
                        /// OpCode = 143<br></br>
                        /// </summary>
                        // public const byte Force_Seeking_Dock = 143;

                        btnHome.Enabled = false;
                        //Log.This("User clicked Home Button");
                        bool bSuccess = Program.UI.CurrentRoomba.Execute(OpCode.Force_Seeking_Dock);
                        btnHome.Enabled = true;
                    }

                #endregion

                #region Button BugFix

                    //I found an interesting issue. I have set the buttons not to be tab stops since the cursor keys also will cycle between tab stops, and
                    //cycling through the buttons would mess up my cursor keys drive method. 
                    //yet, if you mouseDown on a button, you can still 
                    //hit the tab key & cycle among the buttons, even though they are no longer set as tab stops. it's a nice bug.  Well that screws with my
                    //cursor driving method by shifting the focus away from my code & back to the buttons.  
                    //The code below is designed to throw the focus somewhere else to get out of that dastardly loop.

                    //The side effect is that it will eat the first cursor keydown, but that I can live with, as we humans tend to smack the same key 
                    //multiple times if it doesn't work on the first try.  If you are the odd sort that hits the button only once & gives up, then you have this note to read..

                    //Of course this could all be silly if there is some kind of setting to tell the form to not use the cursor keys to jump between tab stops, but I don't know what that is.
                    //Until then.. Monkey, meet wrench.
                    private void tSideBrush_Leave(object sender, EventArgs e)
                    {
                        tSideBrush.Enabled = false;
                        tabControl1.Select(); //Set focus to a control that won't eat my cursor keydown events
                        Application.DoEvents();
                        tSideBrush.Enabled = true;
                    }

                    private void tMain_Brush_Leave(object sender, EventArgs e)
                    {
                        tMain_Brush.Enabled = false;
                        tabControl1.Select(); //Set focus to a control that won't eat my cursor keydown events
                        Application.DoEvents();
                        tMain_Brush.Enabled = true;
                    }

                    private void tVacuum_Leave(object sender, EventArgs e)
                    {
                        tVacuum.Enabled = false;
                        tabControl1.Select(); //Set focus to a control that won't eat my cursor keydown events
                        Application.DoEvents();
                        tVacuum.Enabled = true;
                    }

                #endregion

            #endregion

        #endregion

        public void OFF(bool setMode)
        {
                if (setMode) { Program.UI.CurrentRoomba.SetMode(SCI_Mode.Off);}

                this.lOff.BackColor = Color.Yellow;

                this.lSafe.BackColor = Color.Transparent;
                this.lPassive.BackColor = Color.Transparent;
                this.lFull.BackColor = Color.Transparent;

                this.EnableButtons(false);
        }

        public void PASSIVE(bool setMode)
        {
            if (Program.UI.CurrentRoomba != null)
            {
                if (setMode) { Program.UI.CurrentRoomba.SetMode(SCI_Mode.Passive); }

                this.EnableButtons(true);
                this.Set_Buttons(false);

                this.lPassive.BackColor = Color.Cyan;
                this.lSafe.BackColor = Color.Transparent;
                this.lFull.BackColor = Color.Transparent;
                this.lOff.BackColor = Color.Transparent;
            }
        }
        
        public void SAFE(bool setMode)
        {
             if(Program.UI.CurrentRoomba != null)
            {
                if (setMode) { Program.UI.CurrentRoomba.SetMode(SCI_Mode.Passive); }

                Application.DoEvents();
                if (setMode) { Program.UI.CurrentRoomba.SetMode(SCI_Mode.Safe); }

                this.EnableButtons(true);
                this.Set_Buttons(false);

                this.lSafe.BackColor = Color.Cyan;

                this.lFull.BackColor = Color.Transparent;
                this.lPassive.BackColor = Color.Transparent;
                this.lOff.BackColor = Color.Transparent;
            }
        }
        
        public void FULL(bool setMode)
        {
            if (Program.UI.CurrentRoomba != null)
            {
                if (setMode) { Program.UI.CurrentRoomba.SetMode(SCI_Mode.Passive); }

                Application.DoEvents();
                if (setMode) { Program.UI.CurrentRoomba.SetMode(SCI_Mode.Full); }

                this.EnableButtons(true);
                this.Set_Buttons(false);

                this.lFull.BackColor = Color.Cyan;

                this.lSafe.BackColor = Color.Transparent;
                this.lPassive.BackColor = Color.Transparent;
                this.lOff.BackColor = Color.Transparent;
            }
        }

        public void HandleKeys(Keys kDown)
        {
            //TODO: Make separate functions from these cases

            this.pRotateLeft.BackColor = Color.Transparent;
            this.pRotateRight.BackColor = Color.Transparent;
            this.pFWD.BackColor = Color.Transparent;
            this.pBack.BackColor = Color.Transparent;

            try
            {
                int speed = 0, angle = 0;
                switch (kDown)
                {
                    case Keys.Right:
                        this.pRotateRight.BackColor = Color.Blue;
                        if (m_iCurrentAngle == 32768) { m_iCurrentAngle = 0; }

                        if ((m_iCurrentAngle - (int)this.udRotateStep.Value) > Radius.Maximum_Right)
                        {
                            if (m_iCurrentRotationSpeed >= 0)
                            {
                                m_iCurrentAngle -= (int)this.udRotateStep.Value;
                                m_bCurrentRotationDirectionPositive = false;
                            }
                            else
                            {
                                m_iCurrentAngle += (int)this.udRotateStep.Value;
                                m_bCurrentRotationDirectionPositive = true;
                            }
                        }
                        angle = m_iCurrentAngle;
                        speed = m_iCurrentRotationSpeed;
                        break;

                    case Keys.Left:
                        this.pRotateLeft.BackColor = Color.Blue;
                        if (m_iCurrentAngle == 32768) { m_iCurrentAngle = 0; }

                        if (m_iCurrentRotationSpeed >= 0)
                        {
                            m_iCurrentAngle += (int)this.udRotateStep.Value;
                            m_bCurrentRotationDirectionPositive = true;
                        }
                        else
                        {
                            m_iCurrentAngle -= (int)this.udRotateStep.Value;
                            m_bCurrentRotationDirectionPositive = false;
                        }

                        angle = m_iCurrentAngle;
                        speed = m_iCurrentRotationSpeed;
                        break;

                    case Keys.Up:
                        this.pFWD.BackColor = Color.Blue; //later, darken as we go faster.

                        if ((m_iCurrentSpeed + (int)this.udSpeedStep.Value) < Velocity.Maximum_Forward)
                        {
                            m_iCurrentSpeed += (int)this.udSpeedStep.Value;
                        }

                        if (chkAutoStraighten.Checked)
                        {
                            m_iCurrentAngle = 32768;
                        }

                        angle = m_iCurrentAngle;
                        speed = m_iCurrentSpeed;
                        break;

                    case Keys.Down:
                        this.pBack.BackColor = Color.Blue;

                        if ((m_iCurrentSpeed - (int)this.udSpeedStep.Value) > Velocity.Maximum_Reverse)
                        {
                            this.udSpeed.Font = new Font("Microsoft Sans Serif", 8, System.Drawing.FontStyle.Regular);
                            m_iCurrentSpeed -= (int)this.udSpeedStep.Value;
                        }
                        else
                        {
                            this.udSpeed.Font = new Font("Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold);
                        }

                        if (chkAutoStraighten.Checked)
                        {
                            m_iCurrentAngle = 32768;
                        }

                        angle = m_iCurrentAngle;
                        speed = m_iCurrentSpeed;
                        break;

                    case Keys.Add:
                        if (this.pBack.BackColor == Color.Blue)
                        {
                            // stop
                            m_iCurrentAngle = 32768;
                            Drive((int)0, (int)m_iCurrentAngle); // NGE07182014 
                            // NGE07182014 Program.UI.CurrentRoomba.Drive((Velocity)0, (Radius)(m_iCurrentAngle));
                            this.pBack.BackColor = Color.Transparent;
                        }
                        else
                        {
                            // If switching from forwards to backwards, slow down and then go the other direction
                            m_iCurrentSpeed = m_iDefaultCurrentSpeed;
                        }
                        
                        this.pFWD.BackColor = Color.Blue; //later, darken as we go faster.

                        //if ((m_iCurrentSpeed + (int)this.udSpeedStep.Value) < Velocity.Maximum_Forward)
                        //{
                        //    m_iCurrentSpeed += (int)this.udSpeedStep.Value;
                        //}

                        //if (chkAutoStraighten.Checked)
                        //{
                        //    m_iCurrentAngle = 32768;
                        //}

                        angle = m_iCurrentAngle;
                        speed = m_iCurrentSpeed;
                        break;

                    case Keys.Subtract:
                        if (this.pFWD.BackColor == Color.Blue)
                        {
                            // stop
                            m_iCurrentAngle = 32768;
                            Drive((int)0, (int)m_iCurrentAngle); // NGE07182014 
                            // NGE07182014 Program.UI.CurrentRoomba.Drive((Velocity)0, (Radius)(m_iCurrentAngle));
                            this.pFWD.BackColor = Color.Transparent;
                        }
                        else
                        {
                            // If switching from forwards to backwards, slow down and then go the other direction
                            m_iCurrentSpeed = -m_iDefaultCurrentSpeed;
                        }

                        this.pBack.BackColor = Color.Blue;

                        //if ((m_iCurrentSpeed - (int)this.udSpeedStep.Value) > Velocity.Maximum_Reverse)
                        //{
                        //    this.udSpeed.Font = new Font("Microsoft Sans Serif", 8, System.Drawing.FontStyle.Regular);
                        //    m_iCurrentSpeed -= (int)this.udSpeedStep.Value;
                        //}
                        //else
                        //{
                        //    this.udSpeed.Font = new Font("Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold);
                        //}

                        //if (chkAutoStraighten.Checked)
                        //{
                        //    m_iCurrentAngle = 32768;
                        //}

                        angle = m_iCurrentAngle;
                        speed = m_iCurrentSpeed;
                        break;

                    case Keys.Space:
                        m_iCurrentSpeed = 0;

                        if (chkAutoStraighten.Checked)
                        {
                            m_iCurrentAngle = 32768;
                        }

                        angle = m_iCurrentAngle;
                        speed = m_iCurrentSpeed;
                        break;
                }

                this.udSpeed.Value = m_iCurrentSpeed;
                this.lrSpeed.Value = m_iCurrentRotationSpeed;
                this.udRotate.Value = m_iCurrentAngle;

                Log.This("Drive Form: Velocity: " + m_iCurrentSpeed.ToString() + "  Rotation Speed: " + m_iCurrentRotationSpeed.ToString() + "  Radius: " + m_iCurrentAngle.ToString(), this.Name, Program.UI.Config.Log.DriveForm);
                Drive((int)speed, (int)angle); // NGE07182014 Drive((int)m_iCurrentSpeed, (int)m_iCurrentAngle); // NGE07182014 
                // NGE07182014 Program.UI.CurrentRoomba.Drive((Velocity)m_iCurrentSpeed, (Radius)(m_iCurrentAngle));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        public void GetKey(KeyEventArgs e)
        {
            if (Program.UI.Started)
            {
                Keys kDown = e.KeyCode;
                this.HandleKeys(kDown);
                e.Handled = true;
            }
            else
            {
                // this.BackColor = Color.Red;
            }
        }

        public void LockDown_Form(bool Lock)
        {
            this.EnableButtons(Lock);
        }
        
        public void EnableButtons(bool bEnabled)
        {
            //Hardware buttons are available in certain modes

            this.tSideBrush.Enabled = bEnabled;
            this.tMain_Brush.Enabled = bEnabled;
            this.tVacuum.Enabled = bEnabled;

            //this.udRotate.Enabled = bEnabled;
            //this.udSpeed.Enabled = bEnabled;
        }
        
        public void Set_Buttons(bool bChecked)
        {
            this.tSideBrush.Checked = bChecked;
            this.tMain_Brush.Checked = bChecked;
            this.tVacuum.Checked = bChecked;
        }

        public void Display_SensorInfo()
        {
            this.timerThread = null;

            //Poll only if We've started Roomba
            if (Program.UI.CurrentRoomba != null)
            {
                //and if his sensors are running.
                if (Program.UI.CurrentRoomba.Sensors != null)
                {
                    if (Program.UI.CurrentRoomba.Sensors.LastUpdated != new DateTime())
                    {
                        this.timerThread = new Thread(new ThreadStart(this.UpdateForm));
                        this.timerThread.Start();
                    }
                    else
                    {
                        //Flash sensors in time with polls
                        if (chkAccurateSensors.Checked)
                        {
                            this.Clear();
                        }
                    }
                }
                else
                {
                    //No Sensors to poll
                    lOff.BackColor = Color.Purple;
                    this.lSafe.BackColor = Color.Transparent;
                    this.lPassive.BackColor = Color.Transparent;
                    this.lFull.BackColor = Color.Transparent;
                }
            }
            else
            {
                //No Roomba Object. 
                lOff.BackColor = Color.Red;
                this.lSafe.BackColor = Color.Transparent;
                this.lPassive.BackColor = Color.Transparent;
                this.lFull.BackColor = Color.Transparent;
            }

        }
       
        private void UpdateForm()
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.InvokeRequired)
            {
                try
                {
                    SetTextCallback d = new SetTextCallback(UpdateForm);
                    this.Invoke(d, new object[] { });
                }
                catch (Exception ex)
                {
                    string exMessage = ex.Message;
                    //This usually errors when you are shutting things down.
                }
            }
            else
            {
                if (Program.UI.CurrentRoomba != null)
                {
                    if (Program.UI.CurrentRoomba.Sensors.LastUpdated != new DateTime())
                    {

                        if (Program.UI.CurrentRoomba.Sensors.IsCurrent)
                        {
                            if (chkDebugConnection.Checked)
                            {
                                this.lblSensorParse.Text = lblSensorParse.Text + "c";
                                this.lblError.Text = "";
                            }

                            this.Check_If_Plugged_In(Program.UI.CurrentRoomba.Sensors);
                        }
                        else
                        {
                            if (chkDebugConnection.Checked)
                            {
                                this.lblSensorParse.Text = lblSensorParse.Text + "x";
                                this.lblError.Text = "Packet Data not Current";
                            }
                        }

                        this.Show_SensorData(Program.UI.CurrentRoomba.Sensors);

                        UpdateRobotPositionAndDirection(); // Keep track of robot position and direction

                        // For now if I hit something or may go off of a cliff; STOP
                        StopIfBumpersOrCliffSensorsAreTrue();

                        this.CheckMode(Program.UI.CurrentRoomba);
                        
                    }
                    else
                    {
                        if (chkDebugConnection.Checked)
                        {
                            this.lblSensorParse.Text = lblSensorParse.Text + "-";
                            this.lblError.Text = "No new Packet Data";
                        }

                        //Maybe this should happen after a delay...
                        //this.OFF();

                        //this.lOff.BackColor = Color.Red;
                        //this.lSafe.BackColor = Color.Red;
                        //this.lPassive.BackColor = Color.Red;
                        //this.lFull.BackColor = Color.Red;

                    }
                }
                else
                {
                    if (chkDebugConnection.Checked)
                    {
                        this.lblSensorParse.Text = lblSensorParse.Text + "X";
                        this.lblError.Text = "No Roomba Started or Connected";
                    }
                }

                if (this.lblSensorParse.Text.Length > 35)
                {
                    this.lblSensorParse.Text = this.lblSensorParse.Text.Substring(1,  this.lblSensorParse.Text.Length - 1); 
                }

            }
        }

        private void StopIfBumpersOrCliffSensorsAreTrue()
        {
            bool bSafe = Program.UI.CurrentRoomba.Mode == SCI_Mode.Safe;
            bool bFull = Program.UI.CurrentRoomba.Mode == SCI_Mode.Full;

            if (bSafe == true || bFull == true)
            {
                Sensors rSensors = Program.UI.CurrentRoomba.Sensors;
                if (rSensors.Packet.Bump.Left == true || rSensors.Packet.Bump.Right == true ||
                    rSensors.Packet.Cliff.FrontLeft == true || rSensors.Packet.Cliff.FrontRight == true ||
                    rSensors.Packet.Cliff.Left == true || rSensors.Packet.Cliff.Right == true)
                {
                    // STOP!!!
                    Log.This("BumpersOrCliffSensors hit while driving");
                    Drive((int)0, (int)0); // NGE07182014 
                    // NGE07182014 Program.UI.CurrentRoomba.Drive((Velocity)0, (Radius)(0));

                    if (this.pFWD.BackColor == Color.Blue)
                    {
                        // Go forwards for a little
                        m_iCurrentAngle = 32768;
                        m_iCurrentSpeed = m_iDefaultCurrentSpeed; // Go Forward again

                        Drive((int)-150, (int)m_iCurrentAngle); // NGE07182014 
                        // NGE07182014 Program.UI.CurrentRoomba.Drive((Velocity)(-150), (Radius)(m_iCurrentAngle));
                        Thread.Sleep(500);
                        Drive((int)0, (int)m_iCurrentAngle); // NGE07182014 
                        // NGE07182014 Program.UI.CurrentRoomba.Drive((Velocity)0, (Radius)(m_iCurrentAngle));
                    }
                    else if (this.pBack.BackColor == Color.Blue)
                    {
                        m_iCurrentAngle = 32768;
                        m_iCurrentSpeed = m_iDefaultCurrentSpeed; // Go Forward again
                        
                        // Go forwards for a little
                        Drive((int)150, (int)m_iCurrentAngle); // NGE07182014 
                        // NGE07182014 Program.UI.CurrentRoomba.Drive((Velocity)(150), (Radius)(m_iCurrentAngle));
                        Thread.Sleep(500);
                        Drive((int)0, (int)m_iCurrentAngle); // NGE07182014 
                        // NGE07182014 Program.UI.CurrentRoomba.Drive((Velocity)0, (Radius)(m_iCurrentAngle));
                    }
                }
            }
        }

        private void CheckMode(Roomba_Poller roomba_Poller)
        {
            switch (roomba_Poller.Current_Mode)
            {
                case SCI_Mode.Off:
                    this.OFF(false);
                    break;
                
                case SCI_Mode.Safe:
                    this.SAFE(false);
                    break;
                
                case SCI_Mode.Full:
                    this.FULL(false);
                    break;

                case SCI_Mode.Passive:
                    this.PASSIVE(false);
                    break;
            }
        }

        private void Check_If_Plugged_In(RoombaSCI.Sensors rsCurrentSensorPoll)
        {
            //Check sensors to see if current is negative. 
            //If it is, then disable the motor buttons & flash "Plugged In" in Bold Red in time with the sensor polls.

            //If Roomba is charging, then flash that in Red as well, in time with the sensor polls.

            byte chargeState = rsCurrentSensorPoll.Packet.Charging_State;
            this.PopulateChargeState(chargeState);
        }

        //Populates all sensor indicators on the form from Roomba's Sensor structure
        private void Show_SensorData(Sensors rsCurrentSensorPoll)
        {
            try
            {
                Program.UI.SetPictureBox(rsCurrentSensorPoll.Packet.Buttons.Power, this.pPower);
                Program.UI.SetPictureBox(rsCurrentSensorPoll.Packet.Buttons.Spot, this.pSpot);
                Program.UI.SetPictureBox(rsCurrentSensorPoll.Packet.Buttons.Clean, this.pClean);
                Program.UI.SetPictureBox(rsCurrentSensorPoll.Packet.Buttons.Max, this.pMax);
                Program.UI.SetPictureBox(rsCurrentSensorPoll.Packet.Buttons.Home, this.pHome);

                Program.UI.SetPictureBox(rsCurrentSensorPoll.Packet.Cliff.Left, this.pCliffLeft);
                Program.UI.SetPictureBox(rsCurrentSensorPoll.Packet.Cliff.Right, this.pCliffRight);
                Program.UI.SetPictureBox(rsCurrentSensorPoll.Packet.Cliff.FrontLeft, this.pCliffFrontLeft);
                Program.UI.SetPictureBox(rsCurrentSensorPoll.Packet.Cliff.FrontRight, this.pCliffFrontRight);

                Program.UI.SetPictureBox(rsCurrentSensorPoll.Packet.Bump.Left, this.pBump_Left);
                Program.UI.SetPictureBox(rsCurrentSensorPoll.Packet.Bump.Right, this.pBump_Right);

                Program.UI.SetPictureBox(rsCurrentSensorPoll.Packet.WheelDrop.Left, this.pWheelDrop_Left);
                Program.UI.SetPictureBox(rsCurrentSensorPoll.Packet.WheelDrop.Right, this.pWheelDrop_Right);
                Program.UI.SetPictureBox(rsCurrentSensorPoll.Packet.WheelDrop.Caster, this.pWheelDrop_Caster);

                Program.UI.SetPictureBox(rsCurrentSensorPoll.Packet.OverCurrent.Left_Wheel, this.pDriveLeft_Overcurrent);
                Program.UI.SetPictureBox(rsCurrentSensorPoll.Packet.OverCurrent.Right_Wheel, this.pDriveRight_OverCurrent);
                Program.UI.SetPictureBox(rsCurrentSensorPoll.Packet.OverCurrent.Main_Brush, this.pMainBrush_Overcurrent);
                Program.UI.SetPictureBox(rsCurrentSensorPoll.Packet.OverCurrent.Side_Brush, this.pSideBrush_Overcurrent);
                Program.UI.SetPictureBox(rsCurrentSensorPoll.Packet.OverCurrent.Vacuum, this.pVacuum_OverCurrent);

                Program.UI.SetPictureBox(rsCurrentSensorPoll.Packet.Virtual_Wall, this.pVirtual_Wall);
                Program.UI.SetPictureBox(rsCurrentSensorPoll.Packet.Wall, this.pWallDetect);

                Program.UI.SetPictureBox(rsCurrentSensorPoll.Packet.Buttons.Power, this.pPower);
                Program.UI.SetPictureBox(rsCurrentSensorPoll.Packet.Buttons.Spot, this.pSpot);
                Program.UI.SetPictureBox(rsCurrentSensorPoll.Packet.Buttons.Clean, this.pClean);
                Program.UI.SetPictureBox(rsCurrentSensorPoll.Packet.Buttons.Max, this.pMax);
                Program.UI.SetPictureBox(rsCurrentSensorPoll.Packet.Buttons.Home, this.pHome);
            }
            catch (Exception ex)
            {
                this.lblError.Text = ex.Message;
                //Log.This(ex.Message + "  " + MethodBase.GetCurrentMethod().ToString());
            }
         }
 
        public void PopulateChargeState(byte byChargingState)
        {

            if (byChargingState == Charging_State.Not_Charging)
            {
                //this.lblChargeState.Text = Charge_State_Description.Not_Charging;
                this.PluggedIn(false);
            }
            else if (byChargingState == Charging_State.Charging_Recovery)
            {
                //this.lblChargeState.Text = Charge_State_Description.Charging_Recovery;
                this.PluggedIn(true);
            }
            else if (byChargingState == Charging_State.Charging)
            {
                //this.lblChargeState.Text = Charge_State_Description.Charging;
                this.PluggedIn(true);
            }
            else if (byChargingState == Charging_State.Trickle_Charging)
            {
                //this.lblChargeState.Text = Charge_State_Description.Trickle_Charging;
                this.PluggedIn(true);
            }
            else if (byChargingState == Charging_State.Waiting)
            {
                //this.lblChargeState.Text = Charge_State_Description.Waiting;
                this.PluggedIn(true);
            }
            else if (byChargingState == Charging_State.Charging_Error)
            {
                //this.lblChargeState.Text = Charge_State_Description.Charging_Error;
                this.PluggedIn(true);
            }
            else
            {
                //this.lblChargeState.Text = "Error";
            }
        }

        private void PluggedIn(bool bPluggedIn)
        {
            if (!(Program.UI.CurrentRoomba.Current_Mode == SCI_Mode.Off))
            {
                this.EnableButtons(!bPluggedIn);
            }

            //this.lblPluggedIn.Visible = bPluggedIn;

            //if (bPluggedIn)
            //{
            //    m_bPluggedInFlasher = !m_bPluggedInFlasher;
            //    if (m_bPluggedInFlasher) { this.lblPluggedIn.BackColor = Color.Yellow; } else { this.lblPluggedIn.BackColor = Color.Transparent; }
            //}
        }
        
        private void Clear()
        {
            this.pBump_Left.BackColor = Color.Transparent;
            this.pBump_Right.BackColor = Color.Transparent;

            this.pCliffLeft.BackColor = Color.Transparent;
            this.pCliffFrontLeft.BackColor = Color.Transparent;
            this.pCliffFrontRight.BackColor = Color.Transparent;
            this.pCliffRight.BackColor = Color.Transparent;

            this.pVirtual_Wall.BackColor = Color.Transparent;
            this.pWallDetect.BackColor = Color.Transparent;

            this.pWheelDrop_Caster.BackColor = Color.Transparent;
            this.pWheelDrop_Left.BackColor = Color.Transparent;
            this.pWheelDrop_Right.BackColor = Color.Transparent;

            this.pDriveLeft_Overcurrent.BackColor = Color.Transparent;
            this.pDriveRight_OverCurrent.BackColor = Color.Transparent;
            
            this.lblDirt_Detect_Left.BackColor = Color.Transparent;
            this.lblDirt_Detect_Right.BackColor = Color.Transparent;

            this.pMainBrush_Overcurrent.BackColor = Color.Transparent;
            this.pSideBrush_Overcurrent.BackColor = Color.Transparent;
            this.pVacuum_OverCurrent.BackColor = Color.Transparent;
        }

        private void chkAccurateSensors_CheckedChanged(object sender, EventArgs e)
        {
            chkAccurateSensors.Enabled = false;
            Program.UI.Config.Forms.DriveForm.Accurate_Sensor_Display = chkAccurateSensors.Checked;
            chkAccurateSensors.Enabled = true;
        }

        private void numericBaseSpeed_ValueChanged(object sender, EventArgs e)
        {
            m_iDefaultCurrentSpeed = (int)numericBaseSpeed.Value;
        }

        private void numericRotationBaseSpeed_ValueChanged(object sender, EventArgs e)
        {
            m_iDefaultCurrentRotationSpeed = (int)numericRotationBaseSpeed.Value;
            lrSpeed.Value = m_iDefaultCurrentRotationSpeed;
            m_iCurrentRotationSpeed = m_iDefaultCurrentRotationSpeed;
        }

        #region tracking robot position
        public void Drive(int speed, int rotate)
        {
            double angle = rotate; //32768
            if (angle == 32768) // quirk of iRobot where this means 0
            {
                angle = 0;
            }
            
            //UpdateRobotPositionAndDirection();

            if (MotionStarted == false)
            {
                MotionStarted = true;
                stopWatch.Start(); // begin keeping trak of position and direction
                stopWatchStart.Start(); // begin keeping trak of position and direction
            }
            Program.UI.CurrentRoomba.Drive((Velocity)speed, (Radius)rotate);
        }

        public void ResetPosition()
        {
            RobotPosition = new PointF(0, 0);
        }

        public void ResetDirection()
        {
            RobotDirection = 0;
        }
        
        public void ResetAll()
        {
            ResetPosition();
            ResetDirection();
        }

        public void UpdateRobotPositionAndDirection()
        {
            if (MotionStarted == false) return;
            stopWatch.Stop();
            long totalElapsedTimeMilliseconds = stopWatchStart.ElapsedMilliseconds / 1000;
            ElapsedTimeMilliseconds = stopWatch.ElapsedMilliseconds;
            stopWatch.Restart(); // Resets time to 0 and then calls start
            // Convert m_iCurrentSpeed to meters/second
            double ConversionOfIRobotUnitsToMetersPerSecond = 1.0 / 1100.0; // m_iCurrentSpeed = 600 will go 1 meter in 1 sec // m_iCurrentSpeed = 20 takes 55 seconds to go 1 meter
            double ConversionOfIRobotUnitsToDegreesPerSecond = 0.014; // m_iCurrentRotationSpeed = 20 will rotate 10 degrees in 1 sec // m_iCurrentRotationSpeed = 20 takes 9 seconds to go 90 degrees
            double speed = m_iCurrentSpeed * ConversionOfIRobotUnitsToMetersPerSecond; // meters/sec
            double currentAngle = m_iCurrentAngle;
            if (m_iCurrentAngle == 32768)
            {
                currentAngle = 0;
            }
            double angle = m_iCurrentRotationSpeed * ConversionOfIRobotUnitsToDegreesPerSecond; // degrees/sec
            if (m_bCurrentRotationDirectionPositive == false)
            {
                angle *= -1.0;
            }
            if (currentAngle != 0)
            {
                RobotDirection += (float)(angle);
                RobotDirection = RobotDirection % 360.0f;
            }

            double degrees = Math.Abs(RobotDirection);
            if (degrees > 90)
            {
                degrees -= 90;
            }
            double radians = 0.0174532925 * degrees;
            double dSin = Math.Sin(radians);
            double dCos = Math.Cos(radians);

            double time = ElapsedTimeMilliseconds / 1000.0; // seconds
            double distance = speed * time; // meters

            RobotSpeed = speed;
            RobotPosition.X += (float)(distance * dCos);
            RobotPosition.Y += (float)(distance * dSin);

            Program.UI.StartForm.labelPositionXValue.Text = MathTools.RoundToSignificantFigures(RobotPosition.X, 3).ToString();
            Program.UI.StartForm.labelPositionYValue.Text = MathTools.RoundToSignificantFigures(RobotPosition.Y, 3).ToString();
            Program.UI.StartForm.labelDirectionValue.Text = MathTools.RoundToSignificantFigures(RobotDirection, 3).ToString();
            Program.UI.StartForm.labelSpeedValue.Text = MathTools.RoundToSignificantFigures(RobotSpeed, 3).ToString();
            Program.UI.StartForm.labelElapsedTimeValue.Text = MathTools.RoundToSignificantFigures(totalElapsedTimeMilliseconds, 3).ToString(); //ElapsedTimeMilliseconds.ToString();
            
            // Draw the location of the robot on a 10m by10m bitmap
           // Program.UI.StartForm.pictureBoxLocationMap.BackgroundImage = Bitm
        }

        #endregion tracking robot position
    }
}


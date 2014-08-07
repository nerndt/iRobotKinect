using System;
using System.Threading;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using RoombaSCI;

namespace iRobotKinect
{
    public partial class frmSensors : iRobotKinect.frmMenu
    {
        // This delegate enables asynchronous calls for setting
        // the text property on a TextBox control.
        //delegate void SetTextCallback(string text);
        delegate void SetTextCallback();

        // This thread is used to demonstrate both thread-safe and
        // unsafe ways to call a Windows Forms control.
        private Thread timerThread = null;

        public frmSensors()
        {
            InitializeComponent();
        }

        public const string c_sBytesRCVD = " Bytes RCVD";

        #region Properties

            private RoombaSCI.Timer p_tForm_Timer;

            /// <summary>
            /// 
            /// </summary>
            new public RoombaSCI.Timer Form_Timer
            {
                get
                {
                    return (this.p_tForm_Timer);
                }
                set
                {
                    this.p_tForm_Timer = value;
                }
            }

        #endregion

        #region Event Handlers

            #region Form

                private void frmSensors_Load(object sender, EventArgs e)
                {
                    this.Form_Timer = new RoombaSCI.Timer();
                    this.Form_Timer.Period = (int)this.udFormDisplay.Value; //put this on Packet>Config tab

                    // Hook up the Elapsed event for the timer.
                    this.Form_Timer.Tick += new EventHandler(OnTimedEvent);
                    this.Form_Timer.Start();
                }
                private void frmSensors_FormClosing(object sender, FormClosingEventArgs e)
                {
                    this.Form_Timer.Stop();
                    Application.DoEvents();

                    this.sensorsToolStripMenuItem.Enabled = true;
                    Program.Menu_Cache.Remove(this.Handle);
                    Program.UI.CopyMyMenu(this);

                    Program.UI.SensorsForm = null;
                }

                #region Timers

                    private void OnTimedEvent(object sender, System.EventArgs e)
                    {
                        this.Form_Timer.Stop();
                        this.Display_SensorInfo();
                        this.Form_Timer.Start();
                    }

                #endregion
                #region MenuItems

                    private void clearToolStripMenuItem_Click(object sender, EventArgs e)
                    {
                        this.Clear();
                    }

                #endregion

            #endregion
        #endregion

        public void Display_SensorInfo()
        {
            this.timerThread = null;

            //Poll only if We've started Roomba
            if (Program.UI.CurrentRoomba != null)
            {
                //and if his sensors are running.
                if (Program.UI.CurrentRoomba.Sensors != null)
                {

                    this.timerThread = new Thread(new ThreadStart(this.UpdateBytes));
                    this.timerThread.Start();

                    //{
                    //    if (Program.UI.CurrentRoomba.Sensors.LastUpdated != new DateTime())
                    //    {
                    //        this.timerThread = new Thread(new ThreadStart(this.UpdateBytes));
                    //        this.timerThread.Start();
                    //    }
                    //}

                }
                else
                {
                    //No Sensors to poll
                }
            }
            else
            {
                //No Roomba Object. 
            }
        }
        private void UpdateBytes()
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.InvokeRequired)
            {
                try
                {
                    SetTextCallback d = new SetTextCallback(UpdateBytes);
                    this.Invoke(d, new object[] { });
                }
                catch (Exception ex)
                {
                    string exMessage = ex.Message;
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
                            //this.lblError.Text = "";
                        }
                        else
                        {
                            //this.lblError.Text = "Packet Data not Current";
                        }

                        this.Display_SensorInfo2(Program.UI.CurrentRoomba.Sensors);
                    }
                    else
                    {
                        //this.lblError.Text = "No new Packet Data";
                    }


                    if (chkPersist.Checked)
                    {

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
                    //this.lblError.Text = "No Roomba Started or Connected";
                }

                if (this.lblSensorParse.Text.Length > 25)
                {
                    this.lblSensorParse.Text = this.lblSensorParse.Text.Substring(1, this.lblSensorParse.Text.Length - 1);
                }

               // NGE05192014 if (Program.UI.Init == true && Program.UI.DriveMode == true)
               // NGE05192014 {
               // NGE05192014     Update();
               // NGE05192014 
               // NGE05192014     Program.UI.Init = false;
               // NGE05192014     // Set the active window to the Drive Window
               // NGE05192014     Program.UI.DriveForm.Focus();
               // NGE05192014 }
            }
        }

        public void Display_SensorInfo2(RoombaSCI.Sensors rsCurrentSensorPoll)
        {
            try
            {

                this.lblBytesRCVD.Text = rsCurrentSensorPoll.Raw_Bytes.Count.ToString() + c_sBytesRCVD;
                this.lblBumps_WheelDropsRaw.Text = rsCurrentSensorPoll.Sync_Bytes[0].ToString();
                this.lblBumps_WheelDrops.Text = this.GetBumpsWheelDrops(rsCurrentSensorPoll);

                this.lblWall.Text = rsCurrentSensorPoll.Packet.Wall.ToString();
                this.lblCliff_Left.Text = rsCurrentSensorPoll.Packet.Cliff.Left.ToString();
                this.lblCliff_Front_Left.Text = rsCurrentSensorPoll.Packet.Cliff.FrontLeft.ToString();
                this.lblCliff_Front_Right.Text = rsCurrentSensorPoll.Packet.Cliff.FrontRight.ToString();
                this.lblCliff_Right.Text = rsCurrentSensorPoll.Packet.Cliff.Right.ToString();
                this.lblVirtual_Wall.Text = rsCurrentSensorPoll.Packet.Virtual_Wall.ToString();

                this.lblDirt_Detector_Left.Text = rsCurrentSensorPoll.Packet.Dirt_Detector.Left.ToString();
                this.lblDirt_Detector_Right.Text = rsCurrentSensorPoll.Packet.Dirt_Detector.Right.ToString();
                this.lblButtonsRaw.Text = rsCurrentSensorPoll.Sync_Bytes[11].ToString();
                this.lblButtons.Text = this.GetButtons(rsCurrentSensorPoll);

                this.lblRemote.Text = rsCurrentSensorPoll.Packet.Remote_Control_Command.ToString();
                this.lblDistance_Traveled.Text = rsCurrentSensorPoll.Packet.Distance.ToString();
                this.lblAngle_Traveled.Text = rsCurrentSensorPoll.Packet.Angle.ToString();

                this.lblChargeStateRaw.Text = rsCurrentSensorPoll.Sync_Bytes[16].ToString();
                this.PopulateChargeState(rsCurrentSensorPoll.Packet.Charging_State);

                this.lblVoltage.Text = rsCurrentSensorPoll.Packet.Voltage.ToString();
                this.lblCurrent.Text = rsCurrentSensorPoll.Packet.Current.ToString();
                this.lblTemp.Text = rsCurrentSensorPoll.Packet.Temperature.ToString();
                this.lblTempF.Text = Convert.ToString(Convert.ToInt32(this.lblTemp.Text) * 9 / 5 + 32);
                this.lblCharge.Text = rsCurrentSensorPoll.Packet.Charge.ToString();
                this.lblCapacity.Text = rsCurrentSensorPoll.Packet.Capacity.ToString();

                this.lblMotorOvercurrentsRaw.Text = rsCurrentSensorPoll.Sync_Bytes[7].ToString();
                this.lblMotorOvercurrents.Text = this.GetOverCurrents(rsCurrentSensorPoll);
            }
            catch
            {
            }
        }

        public string GetButtons(Sensors rsSensorPoll)
        {
            string value = null;

            if (rsSensorPoll.Packet.Buttons.Clean)
            {
                value += " Clean";
            }

            if (rsSensorPoll.Packet.Buttons.Max)
            {
                value += " Max";
            }

            if (rsSensorPoll.Packet.Buttons.Home)
            {
                value += " Home";
            }

            if (rsSensorPoll.Packet.Buttons.Power)
            {
                value += " Power";
            }

            if (rsSensorPoll.Packet.Buttons.Spot)
            {
                value += " Spot";
            }

            return value;
        }

        public string GetBumpsWheelDrops(Sensors rsSensorPoll)
        {
            string value = null;

            if (rsSensorPoll.Packet.Bump.Left)
            {
                value += " B. Left";
            }

            if (rsSensorPoll.Packet.Bump.Right)
            {
                value += " B. Right";
            }

            if (rsSensorPoll.Packet.WheelDrop.Left)
            {
                value += " WD Left";
            }

            if (rsSensorPoll.Packet.WheelDrop.Right)
            {
                value += " WD Right";
            }

            if (rsSensorPoll.Packet.WheelDrop.Caster)
            {
                value += " WD Caster";
            }

            return value;
        }
        public string GetOverCurrents(Sensors rsSensorPoll)
        {
            string value = null;

            if (rsSensorPoll.Packet.OverCurrent.Left_Wheel)
            {
                value += " Left Wheel";
            }

            if (rsSensorPoll.Packet.OverCurrent.Right_Wheel)
            {
                value += " Right Wheel";
            }

            if (rsSensorPoll.Packet.OverCurrent.Side_Brush)
            {
                value += " Side Brush";
            }

            if (rsSensorPoll.Packet.OverCurrent.Main_Brush)
            {
                value += " Main Brush";
            }

            if (rsSensorPoll.Packet.OverCurrent.Vacuum)
            {
                value += " Vacuum";
            }

            return value;
        }

        public void PopulateChargeState(byte byChargingState)
        {

            if (byChargingState == Charging_State.Not_Charging)
            {
                this.lblChargeState.Text = Charge_State_Description.Not_Charging;
            }
            else if (byChargingState == Charging_State.Charging_Recovery)
            {
                this.lblChargeState.Text = Charge_State_Description.Charging_Recovery;
            }
            else if (byChargingState == Charging_State.Charging)
            {
                this.lblChargeState.Text = Charge_State_Description.Charging;
            }
            else if (byChargingState == Charging_State.Trickle_Charging)
            {
                this.lblChargeState.Text = Charge_State_Description.Trickle_Charging;
            }
            else if (byChargingState == Charging_State.Waiting)
            {
                this.lblChargeState.Text = Charge_State_Description.Waiting;
            }
            else if (byChargingState == Charging_State.Charging_Error)
            {
                this.lblChargeState.Text = Charge_State_Description.Charging_Error;
            }
            else
            {
                this.lblChargeState.Text = "Error";
            }
        }

        public void Clear()
        {
            this.lblBytesRCVD.Text = "";
            this.lblBumps_WheelDropsRaw.Text = "";
            this.lblCliff_Left.Text = "";
            this.lblCliff_Front_Left.Text = "";
            this.lblCliff_Front_Right.Text = "";
            this.lblCliff_Right.Text = "";
            this.lblVirtual_Wall.Text = "";
            this.lblButtons.Text = "";
            this.lblRemote.Text = "";
            this.lblDistance_Traveled.Text = "";
            this.lblAngle_Traveled.Text = "";
            //this.lblDirt_Detect_Left.Text  = "";
            //this.lblDirt_Detect_Right.Text = "";
            this.lblVoltage.Text = "";
            this.lblCurrent.Text = "";
            this.lblTemp.Text = "";
            this.lblTempF.Text = "";
            this.lblCharge.Text = "";
            this.lblCapacity.Text = "";
        }

    }
}


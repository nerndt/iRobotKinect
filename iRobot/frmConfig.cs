using System;
using System.Reflection;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

using System.Drawing;

namespace iRobotKinect
{

    //This form  will write to config, and tell it to save
    public partial class frmConfig : iRobotKinect.frmMenu
    {
        public frmConfig()
        {
            InitializeComponent();
        }

        #region Events

            #region Form

                private void Config_Load(object sender, EventArgs e)
                {
                    this.cCOM_Port.Items.Clear();
                    this.cCOM_Port.Text = "";
                    this.cCOM_Port.Items.AddRange(Program.UI.GetPorts().ToArray());

                    //Grab ID of Roomba located in cCOM_Port.Text (from Roomba Object)
                    //-Do a readExisting to get it
                    //Make it happen here, then file it as a new function in Roomba.cs

                    Program.UI.Config.Forms.StartForm.Battery_Check = this.chkStart_Form_Battery_Check.Checked;

                    this.SyncToFile();

                    //TODO: add a new prop:  Active. this form will update if Roomba obj is active.
                    //then go fix all the code when you leave a control that saves it to the active roomba object

                }
                private void frmConfig_FormClosing(object sender, FormClosingEventArgs e)
                {
                    //Do an XML Save - or Set property on XML Object (Save Request)
                    Program.UI.ConfigForm = null;
                }
                private void frmConfig_FormClosed(object sender, FormClosedEventArgs e)
                {
                    this.configToolStripMenuItem.Enabled = true;
                    Program.Menu_Cache.Remove(this.Handle);
                    Program.UI.CopyMyMenu(this);
                }

            #endregion
            #region Comboboxes

                private void cCOM_Port_SelectedIndexChanged(object sender, EventArgs e)
                {
                    //If Roomba is running, then stop him

                    //Set Roomba object to use the port selected
                    Program.UI.Config.COMM.ConnectedTo = this.cCOM_Port.Text;

                    //Start Roomba
                    //Program.UI.CurrentRoomba.Start(this.cCOM_Port.Text, this.chkPollSensors.Checked);
                }
            #endregion
            #region Checkboxes

                private void chkReadSensorObj_CheckedChanged(object sender, EventArgs e)
                {
                    // chkReadSensorObj
                }
                private void chkPollSensors_CheckedChanged(object sender, EventArgs e)
                {
                    //TODO: Mke sure that shutting this off indeed stops RoombaObj's *internal* poller, not the form.

                    Program.UI.Config.Polling.Sensors = this.chkPollSensors.Checked;
                    Program.UI.CurrentRoomba.Automatic_Polling = this.chkPollSensors.Checked;

                    this.udPollFrequency.Enabled = this.chkPollSensors.Checked;

                }
                private void chkStart_Form_Battery_Check_CheckedChanged(object sender, EventArgs e)
                {
                    Program.UI.Config.Forms.StartForm.Battery_Check = this.chkStart_Form_Battery_Check.Checked;
                }

                //Logging
                private void chkRoombaUILog_CheckedChanged(object sender, EventArgs e)
                {
                    Program.UI.Config.Log.RoombaUI = this.chkRoombaUILog.Checked;
                }
                private void chkStartFormTimerLog_CheckedChanged(object sender, EventArgs e)
                {
                    Program.UI.Config.Log.StartForm_Timer = this.chkStartFormTimerLog.Checked;
                }
                private void chkStartFormChargeLog_CheckedChanged(object sender, EventArgs e)
                {
                    Program.UI.Config.Log.StartForm_Charging = this.chkStartFormChargeLog.Checked;
                }
                private void chkLogDriveActions_CheckedChanged(object sender, EventArgs e)
                {
                    Program.UI.Config.Log.DriveForm = this.chkLogDriveActions.Checked;
                }
                private void chkRoombaPollerLog_CheckedChanged(object sender, EventArgs e)
                {
                    Program.UI.Config.Log.Roomba_Poller = chkRoombaPollerLog.Checked;
                    Program.UI.CurrentRoomba.Poller_LogPermission = Program.UI.Config.Log.Roomba_Poller;
                }
                private void chkRoombaSCILog_CheckedChanged(object sender, EventArgs e)
                {
                    Program.UI.Config.Log.LogSCICommands = chkRoombaSCILog.Checked;
                    Program.UI.CurrentRoomba.LogSCICommands = Program.UI.Config.Log.LogSCICommands;
                }
                private void chkLogRoombaIO_CheckedChanged(object sender, EventArgs e)
                {
                    Program.UI.Config.Log.Roomba_IO = this.chkLogRoombaIO.Checked;
                    Program.UI.CurrentRoomba.LogIO = Program.UI.Config.Log.Roomba_IO;
                }
                private void chkLogPacketData_CheckedChanged(object sender, EventArgs e)
                {
                    Program.UI.Config.Log.Roomba_PacketData = this.chkLogPacketData.Checked;
                    Program.UI.CurrentRoomba.LogPacketData = Program.UI.Config.Log.Roomba_PacketData;
                }

            #endregion
            #region Buttons

                private void btnApply_Click(object sender, EventArgs e)
                {
                    Program.UI.Config.COMM.ConnectedTo = this.cCOM_Port.Text;
                    Program.UI.Config.Save();
                    this.Close();
                }

            #endregion
            #region UD Controls

                private void udStartForm_Timer_ValueChanged(object sender, EventArgs e)
                {
                    Program.UI.Config.Forms.StartForm.Timer = (int)udStartForm_Timer.Value;
                }
                private void udPollFrequency_ValueChanged(object sender, EventArgs e)
                {
                    Program.UI.Config.Polling.Frequency = (int)this.udPollFrequency.Value;

                    if (Program.UI.CurrentRoomba.ConnectionTime != null)
                    {
                        Program.UI.CurrentRoomba.Sensors.PollingInterval = Program.UI.Config.Polling.Frequency;
                    }
                }
                private void udIsCurrent_Threshold_ValueChanged(object sender, EventArgs e)
                {
                    if (Program.UI.CurrentRoomba.ConnectionTime != null)
                    {
                        Program.UI.Config.Sensors.IsCurrent_Threshold = (int)this.udIsCurrent_Threshold.Value;
                        Program.UI.CurrentRoomba.Sensors.IsCurrent_Threshold = Program.UI.Config.Sensors.IsCurrent_Threshold;
                    }
                }

            #endregion
            #region MenuItems

                private void openToolStripMenuItem_Click(object sender, EventArgs e)
                {

                }

            #endregion

        #endregion

        private void SyncToFile()
        {
            //Sync all controls to our config file:
            this.udPollFrequency.Value = Program.UI.Config.Polling.Frequency;
            this.cCOM_Port.Text = Program.UI.Config.COMM.ConnectedTo;
            this.udStartForm_Timer.Value = Program.UI.Config.Forms.StartForm.Timer;

            this.chkRoombaUILog.Checked = Program.UI.Config.Log.RoombaUI;
            this.chkRoombaPollerLog.Checked = Program.UI.Config.Log.Roomba_Poller;
            this.chkRoombaSCILog.Checked = Program.UI.Config.Log.LogSCICommands;
            this.chkLogRoombaIO.Checked = Program.UI.Config.Log.Roomba_IO;

            this.chkStartFormTimerLog.Checked = Program.UI.Config.Log.StartForm_Timer;
            this.chkStartFormChargeLog.Checked = Program.UI.Config.Log.StartForm_Charging;
            this.chkLogDriveActions.Checked = Program.UI.Config.Log.DriveForm;
            this.chkLogPacketData.Checked = Program.UI.Config.Log.Roomba_PacketData;
        }



        // this.CurrentRoomba.DebugMode = Me.chkRoombaDebug.Checked;

        //move this to config form.  Read Setting
        //Me.cCOM_Port.Items.Clear()
        //Me.cCOM_Port.Items.AddRange(Me.GetPorts().ToArray)

        //If (Me.chkUseFirstPort.Checked) Then
        //    If (Me.cCOM_Port.Text IsNot Nothing) Then Me.cCOM_Port.Text = Me.cCOM_Port.Items(0).ToString() 'Read UI Combobox Value. At the moment, I am pulling the first COM Port that I find
        //Else
        //    If (Me.cCOM_Port.Text Is Nothing) Then MessageBox.Show("Set COM Port (On Config Tab)") : Exit Sub
        //    If (Me.cCOM_Port.Text.Length = 0) Then MessageBox.Show("Set COM Port (On Config Tab)") : Exit Sub
        //End If

    }
}


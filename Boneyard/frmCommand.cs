using System;
using System.IO.Ports;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

using RoombaSCI;

namespace iRobotKinect
{
    public partial class frmCommand : iRobotKinect.frmMenu
    {
        // This delegate enables asynchronous calls for setting
        // the text property on a TextBox control.
        //delegate void SetTextCallback(string text);
        delegate void SetTextCallback();

        // This thread is used to demonstrate both thread-safe and
        // unsafe ways to call a Windows Forms control.
        private Thread timerThread = null;

        protected static string m_sReturn = "";
        protected static int m_iBytesToRead = -1;
        protected static bool m_bRCV_Err = false;

        public frmCommand()
        {
            InitializeComponent();
        }

        //may be needed for text
        //RemoveHandler Me.CurrentRoomba.Text_IO_Handler, AddressOf Me.Roomba_Text_DataReceived  'Me.CurrentRoomba.Text_IO_Handler += New Roomba.Roomba_Text_IO_Handler(Me.Roomba_Text_DataReceived)

        //All errors will be piped out to SendRecieve, but in Bold

        #region Events

            #region Form

                private void frmCommand_Load(object sender, EventArgs e)
                {

                    //I say "Allow" because this checkbox will just drop a marker, and start form's timer will come along
                    //and see that it needs to shut off or reconnect.  Suspending is really just turning off the poller, not
                    //destroying the roomba object.
                    if (!this.chkAllowPolling.Checked) { Program.UI.Suspend_Comm(true); }

                    this.Form_Timer = new RoombaSCI.Timer();
                    this.Form_Timer.Period = (int)this.udFormDisplay.Value;

                    // Hook up the Elapsed event for the timer.
                    this.Form_Timer.Tick += new EventHandler(OnTimedEvent);
                    this.Form_Timer.Start();


                   //Program.UI.CurrentRoomba = new Roomba_Poller();
                    //Program.UI.CurrentRoomba.IO = new SerialPort();

                    //Program.UI..CurrentRoomba.DebugMode = this.chkRoombaDebug.Checked;
                    //Program.UI..CurrentRoomba.LogPath = this.LogPath; //In this case, Roomba and this form could share 1 log..

                    //Program.UI.CurrentRoomba.IO_Handler += new Roomba.Roomba_IO_Handler(this.Roomba_DataReceived);
                    //Program.UI.CurrentRoomba.Text_IO_Handler += new Roomba.Roomba_Text_IO_Handler(this.Roomba_Text_DataReceived);

                    Program.UI.CurrentRoomba.IO.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.Roomba_DataReceived);
                    Program.UI.CurrentRoomba.IO.ErrorReceived += new System.IO.Ports.SerialErrorReceivedEventHandler(this.Roomba_DataErrorRecieved);

                }
                private void frmCommand_FormClosing(object sender, FormClosingEventArgs e)
                {
                    this.Form_Timer.Stop();
                    Application.DoEvents();

                    //this.commandToolStripMenuItem.Enabled = true;
                    Program.Menu_Cache.Remove(this.Handle);
                    Program.UI.CopyMyMenu(this);
                }

            #endregion
            #region Timers

                private void OnTimedEvent(object sender, System.EventArgs e)
                {

                    try
                    {
                        this.timerThread = new Thread(new ThreadStart(this.UpdateForm));
                        this.timerThread.Start();
                    }
                    catch (Exception ex)
                    {
                        string exMessage = ex.Message;
                        this.timerThread.Abort();
                    }

                    //MessageBox.Show(m_sReturn);
                    //Application.DoEvents();
                }

            #endregion

            private void chkAllowPolling_CheckedChanged(object sender, EventArgs e)
            {

                 Program.UI.Suspend_Comm(!this.chkAllowPolling.Checked);
            }

            #region Buttons

                private void btnSendOpCode_Click(object sender, EventArgs e)
                {
                    Program.UI.CurrentRoomba.IO.Close();
                    
                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    Program.UI.CurrentRoomba.IO.Open();

                    //Log.This("User executed a manual SEND ");

                   //Program.UI.

                    byte[] b = new byte[5];
                    b[0] = Convert.ToByte(this.cOtherByteCommands.Text);

                    if (this.txtByte1.Text.Length > 0)
                    {
                        b[1] = Convert.ToByte(this.txtByte1.Text);
                        if (this.txtByte2.Text.Length > 0)
                        {
                            b[2] = Convert.ToByte(this.txtByte2.Text);
                            if (this.txtByte3.Text.Length > 0)
                            {
                                b[3] = Convert.ToByte(this.txtByte3.Text);
                                if (this.txtByte4.Text.Length > 0)
                                {
                                    b[4] = Convert.ToByte(this.txtByte4.Text);
                                }
                            }
                        }
                    }


                    this.txtSendRecieve.Text = "Sent: [" + b[0].ToString() + "][" + b[1].ToString() + "]" + "\r\n" + this.txtSendRecieve.Text;
                    //Log.This("Sent: [" + b[0].ToString() + "][" + b[1].ToString() + "]" + "\r\n");

                    Program.UI.CurrentRoomba.Execute(b[0], b[1]);
                    //Program.UI.CurrentRoomba.IO.Write(b, 0, b.Length);

                    this.timerThread = new Thread(new ThreadStart(this.UpdateForm));
                    this.timerThread.Start();

                }
                private void btnSendCommand_Click(object sender, EventArgs e)
                {
                    if (this.cboCommand.Text == "Poll Sensors")
                    {
                        //Program.UI.CurrentRoomba.IO.RtsEnable = this.chkRTSEnable.Checked;
                        Program.UI.CurrentRoomba.SetMode(SCI_Mode.Passive);
                        //Program.UI.CurrentRoomba.Execute(OpCode.Sensors, 0);

                        Program.UI.CurrentRoomba.Poll_Sensors(0);

                    }

                }

            #endregion
            #region Groupboxes

                private void rUserControlOff_CheckedChanged(object sender, EventArgs e)
                {
                    //Log.This("User Control OFF checked");
                    bool bSuccess = Program.UI.CurrentRoomba.Execute(OpCode.Start);

                    if (bSuccess)
                    {

                        this.pUserControl.BackColor = Color.Transparent;
                        this.gMode.Enabled = false;
                    }

                }
                private void rUserControlOn_CheckedChanged(object sender, EventArgs e)
                {
                    //Log.This("User Control ON Checked");
                    //this.eErrorBlinky.SetError(this.rUserControlOn, "");

                    bool bSuccess = Program.UI.CurrentRoomba.SetMode(SCI_Mode.Passive);
                    this.chkRTSEnable.Checked = Program.UI.CurrentRoomba.IO.RtsEnable; //Roomba is smart enough to disable RTS himself

                    //At this point, lets request a sample packet from Roomba.  If Roomba responds, then we have verified communication
                    //Make this test request as a sample property on Roomba. i.e. this property is set to true when communciations have been verified.

                    try
                    {

                        //this.tlStatus.Text = "SetMode Success : " + bSuccess.ToString();
                        //Log.This(this.tlStatus.Text);

                        if (bSuccess)
                        {
                            Program.UI.CurrentRoomba.SetLED(LED.Dirt_Detect.On);

                            this.pUserControl.BackColor = Color.Blue;
                            this.gMode.Enabled = true;

                            this.rFull.Enabled = true;

                            //At this point, lets request a sample packet from Roomba.  If Roomba responds, then we have verified communication
                            

                            //Use checkboxes to make all these verifications optional

                            //this.tForm_Timer.Interval = (int)this.udPollFrequency.Value;
                            //this.tForm_Timer.Start();

                            this.rFull.Checked = true;

                            //Show that Roomba is Ready to Go.
                            Program.UI.CurrentRoomba.Motor_Action(Motor.Side_Brush.On);
                            for (int i = 0; i < 10000; i++)
                            {
                                Application.DoEvents();
                            }

                            Program.UI.CurrentRoomba.Motor_Action(Motor.Side_Brush.Off);

                        }
                        else
                        {
                            this.pUserControl.BackColor = Color.Red;
                            this.gMode.Enabled = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        string exMessage = ex.Message;
                        //Log.This(ex.Message);
                    }

                }

                private void rSafe_CheckedChanged(object sender, EventArgs e)
                {

                    bool bSuccess = Program.UI.CurrentRoomba.SetMode(SCI_Mode.Safe);
                    //this.tlStatus.Text = "SetMode Success (Safe) : " + bSuccess.ToString();
                    //Log.This(this.tlStatus.Text);

                }
                private void rPassive_CheckedChanged(object sender, EventArgs e)
                {

                    bool bSuccess = Program.UI.CurrentRoomba.SetMode(SCI_Mode.Passive);
                    this.chkRTSEnable.Checked = Program.UI.CurrentRoomba.IO.RtsEnable; //Roomba is smart enough to disable RTS himself

                    //this.tlStatus.Text = "SetMode Success (Passive) : " + bSuccess.ToString();
                    //Log.This(this.tlStatus.Text);

                }
                private void rFull_CheckedChanged(object sender, EventArgs e)
                {

                    bool bSuccess = Program.UI.CurrentRoomba.SetMode(SCI_Mode.Full);

                    //this.tlStatus.Text = "SetMode Success (Full) : " + bSuccess.ToString();
                    //Log.This(this.tlStatus.Text);

                }

            #endregion

            private void Roomba_Text_DataReceived(string sRecievedText)
            {
                m_sReturn = sRecievedText;

            }

            //Refactor this to Roomba Obj
            private void Roomba_DataReceived(object sender, SerialDataReceivedEventArgs e)
            {
                m_bRCV_Err = false;

                //m_sReturn = e.ToString() + DateTime.Now.ToString();
                //m_iBytesToRead = Program.UI.CurrentRoomba.IO.BytesToRead;
                //m_sReturn = Program.UI.CurrentRoomba.IO.ReadExisting();

                MessageBox.Show(m_sReturn);

                this.UpdateForm();

                //this.pSensorRCV.BackColor = Color.Green;
                //if (this.chkFlashStatus.Checked)
                //{
                //    this.m_bTextRecieved = true;
                //    this.SetCommStatus();
                //}; //Feedback to Roomba to show that we have communication

                //g_sRCV  = this.CurrentRoomba.IO.ReadExisting();
                ////int iBytesToRead = this.CurrentRoomba.IO.BytesToRead;

                //this.SetText();

                //Application.DoEvents();
            }
            private void Roomba_DataErrorRecieved(object sender, SerialErrorReceivedEventArgs e)
            {
                string m_sReturn = Program.UI.CurrentRoomba.IO.ReadExisting();
                m_bRCV_Err = true;
            }

        #endregion


        private void UpdateForm()
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(UpdateForm);
                this.Invoke(d, new object[] { });
            }
            else
            {
                if (Program.UI.CurrentRoomba != null)
                {
                    //Do a ReadExisting on Roomba's IO
                    
                    //Grab data that was gathered by Roomba_DataRecieved
                    
                    //string sReturn = Program.UI.CurrentRoomba.IO.ReadExisting();

                    m_sReturn = Program.UI.CurrentRoomba.IO.ReadExisting();

                    if (m_sReturn.Length > 0)
                    {
                        this.txtSendRecieve.Text = "\r\n RCV'd " + m_iBytesToRead.ToString() + " Bytes. " + DateTime.Now.ToLongTimeString() + "  Error = " + frmCommand.m_bRCV_Err.ToString() + this.txtSendRecieve.Text;
                        this.txtSendRecieve.Text = "\r\n.." + m_sReturn + this.txtSendRecieve.Text;
                    }
                }
            }
        }
        }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using RoombaSCI;

namespace iRobotKinect
{
    public partial class frmPacket : iRobotKinect.frmMenu
    {
        // This delegate enables asynchronous calls for setting
        // the text property on a TextBox control.
        //delegate void SetTextCallback(string text);
        delegate void SetTextCallback();

        // This thread is used to demonstrate both thread-safe and
        // unsafe ways to call a Windows Forms control.
        private Thread timerThread = null;

        public frmPacket()
        {
            InitializeComponent();
        }

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

        #region "Event Handlers"

            #region Form

                private void frmPacket_Load(object sender, EventArgs e)
                {
                    pRawBytes.Enabled = this.chkShowRawBytes.Checked;
                    pSyncBytes.Enabled = this.chkShowSyncBytes.Checked;
                    this.lblError.Visible = chkShowErrors.Checked;

                    this.Form_Timer = new RoombaSCI.Timer();
                    this.Form_Timer.Period = (int)this.udFormDisplay.Value; //put this on Packet>Config tab

                    // Hook up the Elapsed event for the timer.
                    this.Form_Timer.Tick += new EventHandler(OnTimedEvent);
                    this.Form_Timer.Start();
                }
                private void frmPacket_FormClosing(object sender, FormClosingEventArgs e)
                {
                    this.Form_Timer.Stop();
                    Application.DoEvents();

                    this.sensorPacketToolStripMenuItem.Enabled = true;
                    Program.Menu_Cache.Remove(this.Handle);
                    Program.UI.CopyMyMenu(this);

                    Program.UI.PacketForm = null;
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
            #region CheckBoxen

                private void chkShowRawBytes_CheckedChanged(object sender, EventArgs e)
                {
                    pRawBytes.Enabled = this.chkShowRawBytes.Checked;
                }
                private void chkShowSyncBytes_CheckedChanged(object sender, EventArgs e)
                {
                    this.pSyncBytes.Enabled = this.chkShowSyncBytes.Checked;
                }
                private void chkShowErrors_CheckedChanged(object sender, EventArgs e)
                {
                    this.lblError.Visible = chkShowErrors.Checked;
                }

            #endregion
            #region MenuItems

                private void restartConnectionToolStripMenuItem_Click(object sender, EventArgs e)
                {
                    this.Form_Timer.Start();
                    this.pRawBytes.Enabled = true;
                    this.pSyncBytes.Enabled = true;
                }
                private void stopToolStripMenuItem_Click(object sender, EventArgs e)
                {
                    this.Form_Timer.Stop();
                    this.pRawBytes.Enabled = false;
                    this.pSyncBytes.Enabled = false;
                }
                private void clearToolStripMenuItem_Click(object sender, EventArgs e)
                {
                    this.Clear();
                }

            #endregion

            private void udFormDisplay_ValueChanged(object sender, EventArgs e)
            {
                this.Form_Timer.Period = (int)this.udFormDisplay.Value; //put this on Packet>Config tab
            }

        #endregion

        public void Clear()
        {
            this.lblRaw0.Text = null;
            this.lblRaw1.Text = null;
            this.lblRaw2.Text = null;
            this.lblRaw3.Text = null;
            this.lblRaw4.Text = null;
            this.lblRaw5.Text = null;
            this.lblRaw6.Text = null;
            this.lblRaw7.Text = null;
            this.lblRaw8.Text = null;
            this.lblRaw9.Text = null;
            this.lblRaw10.Text = null;
            this.lblRaw11.Text = null;
            this.lblRaw12.Text = null;
            this.lblRaw13.Text = null;
            this.lblRaw14.Text = null;
            this.lblRaw15.Text = null;
            this.lblRaw16.Text = null;
            this.lblRaw17.Text = null;
            this.lblRaw18.Text = null;
            this.lblRaw19.Text = null;
            this.lblRaw20.Text = null;
            this.lblRaw21.Text = null;
            this.lblRaw22.Text = null;
            this.lblRaw23.Text = null;
            this.lblRaw24.Text = null;
            this.lblRaw25.Text = null;

            this.lblSync0.Text = null;
            this.lblSync1.Text = null;
            this.lblSync2.Text = null;
            this.lblSync3.Text = null;
            this.lblSync4.Text = null;
            this.lblSync5.Text = null;
            this.lblSync6.Text = null;
            this.lblSync7.Text = null;
            this.lblSync8.Text = null;
            this.lblSync9.Text = null;
            this.lblSync10.Text = null;
            this.lblSync11.Text = null;
            this.lblSync12.Text = null;
            this.lblSync13.Text = null;
            this.lblSync14.Text = null;
            this.lblSync15.Text = null;
            this.lblSync16.Text = null;
            this.lblSync17.Text = null;
            this.lblSync18.Text = null;
            this.lblSync19.Text = null;
            this.lblSync20.Text = null;
            this.lblSync21.Text = null;
            this.lblSync22.Text = null;
            this.lblSync23.Text = null;
            this.lblSync24.Text = null;
            this.lblSync25.Text = null;

            this.lblError.Text = null;
            this.lblSensorParse.Text = null;

        }
        public void ShowRawBytes(Sensors rsCurrentSensorPoll)
        {
            if (rsCurrentSensorPoll.IsCurrent || this.chkIgnore_IsCurrent.Checked)
            {
                if (this.chkShowRawBytes.Checked)
                {
                    try
                    {
                        if (rsCurrentSensorPoll.Raw_Bytes != null)
                        {
                            this.lblRaw0.Text = rsCurrentSensorPoll.Raw_Bytes[0].ToString();
                            this.lblRaw1.Text = rsCurrentSensorPoll.Raw_Bytes[1].ToString();
                            this.lblRaw2.Text = rsCurrentSensorPoll.Raw_Bytes[2].ToString();
                            this.lblRaw3.Text = rsCurrentSensorPoll.Raw_Bytes[3].ToString();
                            this.lblRaw4.Text = rsCurrentSensorPoll.Raw_Bytes[4].ToString();
                            this.lblRaw5.Text = rsCurrentSensorPoll.Raw_Bytes[5].ToString();
                            this.lblRaw6.Text = rsCurrentSensorPoll.Raw_Bytes[6].ToString();
                            this.lblRaw7.Text = rsCurrentSensorPoll.Raw_Bytes[7].ToString();
                            this.lblRaw8.Text = rsCurrentSensorPoll.Raw_Bytes[8].ToString();
                            this.lblRaw9.Text = rsCurrentSensorPoll.Raw_Bytes[9].ToString();
                            this.lblRaw10.Text = rsCurrentSensorPoll.Raw_Bytes[10].ToString();
                            this.lblRaw11.Text = rsCurrentSensorPoll.Raw_Bytes[11].ToString();
                            this.lblRaw12.Text = rsCurrentSensorPoll.Raw_Bytes[12].ToString();
                            this.lblRaw13.Text = rsCurrentSensorPoll.Raw_Bytes[13].ToString();
                            this.lblRaw14.Text = rsCurrentSensorPoll.Raw_Bytes[14].ToString();
                            this.lblRaw15.Text = rsCurrentSensorPoll.Raw_Bytes[15].ToString();
                            this.lblRaw16.Text = rsCurrentSensorPoll.Raw_Bytes[16].ToString();
                            this.lblRaw17.Text = rsCurrentSensorPoll.Raw_Bytes[17].ToString();
                            this.lblRaw18.Text = rsCurrentSensorPoll.Raw_Bytes[18].ToString();
                            this.lblRaw19.Text = rsCurrentSensorPoll.Raw_Bytes[19].ToString();
                            this.lblRaw20.Text = rsCurrentSensorPoll.Raw_Bytes[20].ToString();
                            this.lblRaw21.Text = rsCurrentSensorPoll.Raw_Bytes[21].ToString();
                            this.lblRaw22.Text = rsCurrentSensorPoll.Raw_Bytes[22].ToString();
                            this.lblRaw23.Text = rsCurrentSensorPoll.Raw_Bytes[23].ToString();
                            this.lblRaw24.Text = rsCurrentSensorPoll.Raw_Bytes[24].ToString();
                            this.lblRaw25.Text = rsCurrentSensorPoll.Raw_Bytes[25].ToString();

                            lblError.Text = null;
                        }
                        else
                        {
                            lblError.Text = "No Data";
                        }
                    }
                    catch (Exception ex)
                    {
                        lblError.Text = ex.Message;
                    }
                }
            }
            else
            {
                lblError.Text = "No Data";
            }
        }
        public void ShowSyncBytes(Sensors rsCurrentSensorPoll)
        {
             if (this.chkShowSyncBytes.Checked)
             {
                   try 
                   {
                     this.lblSync0.Text = rsCurrentSensorPoll.Sync_Bytes[0].ToString();
                     this.lblSync1.Text = rsCurrentSensorPoll.Sync_Bytes[1].ToString();
                     this.lblSync2.Text = rsCurrentSensorPoll.Sync_Bytes[2].ToString();
                     this.lblSync3.Text = rsCurrentSensorPoll.Sync_Bytes[3].ToString();
                     this.lblSync4.Text = rsCurrentSensorPoll.Sync_Bytes[4].ToString();
                     this.lblSync5.Text = rsCurrentSensorPoll.Sync_Bytes[5].ToString();
                     this.lblSync6.Text = rsCurrentSensorPoll.Sync_Bytes[6].ToString();
                     this.lblSync7.Text = rsCurrentSensorPoll.Sync_Bytes[7].ToString();
                     this.lblSync8.Text = rsCurrentSensorPoll.Sync_Bytes[8].ToString();
                     this.lblSync9.Text = rsCurrentSensorPoll.Sync_Bytes[9].ToString();
                     this.lblSync10.Text = rsCurrentSensorPoll.Sync_Bytes[10].ToString();
                     this.lblSync11.Text = rsCurrentSensorPoll.Sync_Bytes[11].ToString();
                     this.lblSync12.Text = rsCurrentSensorPoll.Sync_Bytes[12].ToString();
                     this.lblSync13.Text = rsCurrentSensorPoll.Sync_Bytes[13].ToString();
                     this.lblSync14.Text = rsCurrentSensorPoll.Sync_Bytes[14].ToString();
                     this.lblSync15.Text = rsCurrentSensorPoll.Sync_Bytes[15].ToString();
                     this.lblSync16.Text = rsCurrentSensorPoll.Sync_Bytes[16].ToString();
                     this.lblSync17.Text = rsCurrentSensorPoll.Sync_Bytes[17].ToString();
                     this.lblSync18.Text = rsCurrentSensorPoll.Sync_Bytes[18].ToString();
                     this.lblSync19.Text = rsCurrentSensorPoll.Sync_Bytes[19].ToString();
                     this.lblSync20.Text = rsCurrentSensorPoll.Sync_Bytes[20].ToString();
                     this.lblSync21.Text = rsCurrentSensorPoll.Sync_Bytes[21].ToString();
                     this.lblSync22.Text = rsCurrentSensorPoll.Sync_Bytes[22].ToString();
                     this.lblSync23.Text = rsCurrentSensorPoll.Sync_Bytes[23].ToString();
                     this.lblSync24.Text = rsCurrentSensorPoll.Sync_Bytes[24].ToString();
                     this.lblSync25.Text = rsCurrentSensorPoll.Sync_Bytes[25].ToString();
                   } 
                   catch 
                   {          
                   }
             }
        }

        //Test function
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
                        this.timerThread = new Thread(new ThreadStart(this.UpdateBytes));
                        this.timerThread.Start();
                    }
                }
            }

        }
        private void UpdateBytes()
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(UpdateBytes);
                this.Invoke(d, new object[] { });
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
                                this.lblError.Text = null;
                            }
                        }
                        else
                        {
                            if (chkDebugConnection.Checked)
                            {
                                this.lblSensorParse.Text = lblSensorParse.Text + "x";
                                this.lblError.Text = "Packet Data not Current";
                            }
                        }
                        
                        this.ShowRawBytes(Program.UI.CurrentRoomba.Sensors);
                        this.ShowSyncBytes(Program.UI.CurrentRoomba.Sensors);
                    }
                    else
                    {
                        if (chkDebugConnection.Checked)
                        {
                            this.lblSensorParse.Text = lblSensorParse.Text + "-";
                            this.lblError.Text = "No new Packet Data";
                        }
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

                if (this.lblSensorParse.Text.Length > 55)
                {
                    this.lblSensorParse.Text = this.lblSensorParse.Text.Substring(1,  this.lblSensorParse.Text.Length - 1); 
                }

            }
        }

        private void explanationOfThisFormToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            string explanation;

            explanation = "This form contains a 26 byte mask, or template in which to view output from Roomba. \r\n";
            explanation += "When the 'Raw Bytes' tab is selected, then you will see the first 26 bytes of data as recieved from Roomba. \r\n\r\n";
            explanation += "If there are additional bytes, or data corruption, that data would show up here. \r\n";
            explanation += "When the 'Sync Bytes' tab is selected, you should see a proper Roomba packet, minus any possible data corruption that this program knows how to handle. \r\n\r\n";
            explanation += "This program uses the 'Sync Bytes' for all of its operations.  The 'Raw Bytes' tab is included here to show exactly what is being recieved for troubleshooting purposes. ";

             MessageBox.Show(explanation);
        }
    }
}


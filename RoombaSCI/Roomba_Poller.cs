using System;
using System.IO;
using System.Text;
using System.IO.Ports;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;

using Logging;

namespace RoombaSCI
{
    [Serializable]
    public class Roomba_Poller: Roomba
    {
        #region Member Variables

            Stopwatch m_wPollTime = new Stopwatch();

        #endregion
        #region Constants

            protected const string c_sRoomba_Poller = "Roomba_Poller";
            protected const string c_sSensorPoll = "Sensor Poll: ";
            protected const string c_sSettingIO = "Setting IO Port";

        #endregion

        public Roomba_Poller()
        {
        }

        public Roomba_Poller(SerialPort IO, double Sensor_Polling_Interval, string sLogPath) :base(IO, Sensor_Polling_Interval, sLogPath) 
        {

            this.AutoPollingCheck = new RoombaSCI.Timer();
            this.AutoPollingCheck.Period = 200;

            // Hook up the Elapsed event for the timer.
            this.AutoPollingCheck.Tick += new EventHandler(OnTimedEvent); 
            this.AutoPollingCheck.Start();

            this.Macro =  new RoombaSCI.Macro(this);

            //this.ConnectionTime = new RoombaSCI.Timer();
            //this.ConnectionTime.Period = 100; //default

            //// Hook up the Elapsed event for the timer.
            ////this.ConnectionTime.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            //this.ConnectionTime.Start();

        }

        public void HookUp()
        {
            this.IO = new System.IO.Ports.SerialPort();

            if (this.IO != null)
            {
              //  this.IO.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.Roomba_DataReceived);
               // this.IO.ErrorReceived += new System.IO.Ports.SerialErrorReceivedEventHandler(this.Roomba_DataErrorRecieved);
            }
        }
   
        #region Properties

            protected Thread p_tPollThread = null;
            public Thread PollThread
            {
                get
                {
                    return (this.p_tPollThread);
                }
                set
                {
                    this.p_tPollThread = value;
                }
            }

            private RoombaSCI.Timer p_tAutoPollingCheck;
            public RoombaSCI.Timer AutoPollingCheck
            {
                get
                {
                    return (this.p_tAutoPollingCheck);
                }
                set
                {
                    this.p_tAutoPollingCheck = value;
                }
            }

            private Timer p_tConnectionTime;
            public RoombaSCI.Timer ConnectionTime
            {
                get
                {
                    return (this.p_tConnectionTime);
                }
                set
                {
                    this.p_tConnectionTime = value;
                }
            }

            private bool p_bPolling;
            public bool Polling
            {
                get
                {
                    return (this.p_bPolling);
                }
                set
                {
                    this.p_bPolling = value;
                }
            }

            private long p_lPollTicks;
            public long PollTicks
            {
                get
                {
                    return (this.p_lPollTicks);
                }
                set
                {
                    this.p_lPollTicks = value;
                }
            }

            private bool p_bPoller_LogPermission;
            public bool Poller_LogPermission
            {
                get
                {
                    return (this.p_bPoller_LogPermission);
                }
                set
                {
                    this.p_bPoller_LogPermission = value;
                }
            }

        #endregion
        #region Event Handlers

            public void Roomba_DataReceived(object sender, SerialDataReceivedEventArgs e)
            {
                //m_bRCV_Err = false;


                //Read existing here when not polling
                string m_sReturn = e.ToString() + DateTime.Now.ToString();
                //m_iBytesToRead = Program.UI.CurrentRoomba.IO.BytesToRead;
                //m_sReturn = Program.UI.CurrentRoomba.IO.ReadExisting();

                //MessageBox.Show(m_sReturn);

                //this.UpdateForm();

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
            public void Roomba_DataErrorRecieved(object sender, SerialErrorReceivedEventArgs e)
            {
                string m_sReturn = this.IO.ReadExisting();
                //m_bRCV_Err = true;
            }

            private void OnTimedEvent(object sender, System.EventArgs e)
                {

                    if (this.Automatic_Polling)
                    {
                        if (this.PollThread == null)
                        {
                            this.Start_Automatic_Polling();
                        }
                        else
                        {
                            m_wPollTime.Stop();

                            this.PollTicks = m_wPollTime.ElapsedMilliseconds;
                            this.Polling = true;

                            m_wPollTime.Reset();
                            m_wPollTime.Start();
                        }

                       // this.AutoPollingCheck.Period = this.Config
                    }
                    else
                    {
                        m_wPollTime.Stop();

                        this.PollTicks = m_wPollTime.ElapsedTicks;
                        this.Polling = false;

                        if (this.PollThread != null){this.Stop_Automatic_Polling(); }
                    }
                }

        #endregion

        #region Functions

            #region Polling

                /// <summary>
                /// Starts the Roomba class Automatic polling routine in a new thread. As of v1.01, this function currently polls all sensors
                /// </summary>
                public void Start_Automatic_Polling()
                {
                    //Yank off the taps into COMM Port
                    //this.IO.DataReceived -= new System.IO.Ports.SerialDataReceivedEventHandler(this.Roomba_DataReceived);
                    //this.IO.ErrorReceived -= new System.IO.Ports.SerialErrorReceivedEventHandler(this.Roomba_DataErrorRecieved);

                    this.PollThread = new Thread(new ThreadStart(this.PollMe));
                    this.PollThread.Start();
                    this.Macro.SetAction("Start_Automatic_Polling");
                }

                /// <summary>
                /// Stops the Roomba class Automatic polling routine (aborts thread)
                /// </summary>
                protected void Stop_Automatic_Polling()
                {
                    if (this.PollThread != null)
                    {
                        this.PollThread.Abort();
                        this.Macro.SetAction("Stop_Automatic_Polling");
                    }

                    //If we are not polling, then we can set up to recieve Text from Roomba
                    //if (this.IO != null)
                    //{
                    //    this.IO.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.Roomba_DataReceived);
                    //    this.IO.ErrorReceived += new System.IO.Ports.SerialErrorReceivedEventHandler(this.Roomba_DataErrorRecieved);
                    //}
                }

                /// <summary>
                /// supporting function for the Automatic polling routine
                /// </summary>
                private void PollMe()
                {
                    if (!this.Sensors.Lock)
                    {
                        do
                        {
                            this.Poll_Sensors(0);
                            Thread.Sleep((int)this.Sensors.PollingInterval);

                        } while (!this.Sensors.StopPolling);
                    }
                }

                /// <summary>
                ///  The Command to Poll Roomba's sensors
                /// </summary>
                /// <param name="byPollType"></param>
                /// <returns></returns>
                public bool Poll_Sensors(byte byPollType)
                {
                    bool bSuccess = false;
                    
                    byte[] b = new byte[2];
                    b[0] = OpCode.Sensors;
                    b[1] = byPollType;

                    try
                    {
                        this.IO.RtsEnable = true;
                        this.Sensors.LastUpdated = new DateTime();

                        if (this.Mode != SCI_Mode.Off)
                        {
                            Log.This(c_sSensorPoll, c_sRoomba_Poller, this.LogIO);
                            this.IO.Write(b, 0, b.Length);
                        }

                        //For debugging purposes
                        //int iBytesInBuffer = this.IO.BytesToRead;
                        //this.LogThis("Bytes Found in Buffer: " + iBytesInBuffer.ToString());

                        //if (iBytesInBuffer > 0)
                        //{
                        //    string sRCV = this.IO.ReadExisting().ToString();
                        //}

                        bSuccess = true;
                    }
                    catch (Exception ex)
                    {
                        Log.This("Sensor Poll Fail: " + ex.Message, c_sRoomba_Poller, this.LogIO); //This may not always be an error, someone could have shut Roomba Off
                    }

                    return bSuccess;

                }

                public void Start(string Port_Name, bool Start_Polling)
                {

                    Log.This(c_sSettingIO, c_sRoomba_Poller, this.LogIO);

                    this.IO = new SerialPort();
                    this.IO.PortName = Port_Name;
                    this.IO.BaudRate = 57600;
                    this.IO.DataBits = 8;
                    this.IO.DtrEnable = false;
                    this.IO.StopBits = StopBits.One;
                    this.IO.Handshake = Handshake.None;
                    this.IO.Parity = Parity.None;
                    this.IO.RtsEnable = false;
                    this.IO.Close();
                    this.IO.Open();

                    Log.This("IO Port: " + Port_Name + " Open:", c_sRoomba_Poller, this.LogIO);

                    if (Start_Polling)
                    {
                        GC.Collect();

                        this.SetMode(SCI_Mode.Passive);

                        if (this.PollThread != null)
                        {
                            try { this.PollThread.Abort(); }
                            catch { }
                            finally { this.PollThread = null; }
                        }

                        this.Automatic_Polling = true; //A timer will come along and check the property & will start  //this.Start_Automatic_Polling();
                        this.Macro.SetAction("Start");
                    }
                }

            #endregion

        #endregion
        
        }
}

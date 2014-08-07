using System;
using System.IO.Ports;
using System.Reflection;
using System.Collections.Generic;
using Logging;

namespace RoombaSCI
{
    public class RoombaException : System.ApplicationException
    {
        public RoombaException(string message) : base(message)
        {
        }
    }

    /// <summary>
    ///  This class is is a representation of the various tasks that Roomba can carry out as specified in the Roomba SCI
    /// </summary>
    /// 
    [Serializable]
    public class Roomba 
    {
        #region Constants

            private const char c_cOpen = '[';
            private const char c_cClose = ']';

            private const string c_sBytesToRead = "Bytes to Read: ";
            private const string c_sBytesRetrieved = " Sensor Byte(s) Retrieved from IO: ";

            private const string c_sExecuteAction = "Execute Action: ";

            private const string c_sParsingData = "Parsing Sensor Data";
            private const string c_sIgnored = "Not enough data to parse, Ignored.";

            private const string c_sRoomba = "Roomba";
            private const string c_sExecuteActionFail = "Execute Action Fail: ";
            private const string c_sPacketData = "Packet Data";
            private const string c_sIO_DataRecieved = "IO Data Received";
            private const string c_sIO_PinChanged = "IO PinChanged";

        #endregion

        #region Events

            /// <summary>
            /// 
            /// </summary>
            /// <param name="RecievedBytes"></param>
            public delegate void Roomba_IO_Handler(byte[] RecievedBytes);
            public delegate void Roomba_Text_IO_Handler(string sRecievedText);

            //TODO: We might want to conditionally add these.  I'm worried about overhead
            public delegate void Roomba_Motor_Action_Handler(byte bSend_Motor_Byte, bool bSendResult);

            /// <summary>
            /// 
            /// </summary>
            public event Roomba_IO_Handler IO_Handler;
            public event Roomba_Text_IO_Handler Text_IO_Handler;

            //public event Roomba_Motor_Action_Handler Before_Motor_Action_Send;
            //public event Roomba_Motor_Action_Handler After_Motor_Action_Send;

        #endregion

        public Roomba()
        {

        }

        /// <summary>
        /// Constructor for Roomba Class. The Motors & Sensors objects are initialized here as well
        /// </summary>
        /// <param name="IO"></param>
        /// <param name="Sensor_Polling_Interval"></param>
        /// <param name="Sync_Battery_Capacity1">Default should be 10</param>
        /// <param name="Sync_Battery_Capacity2">Default should be 142</param>
        /// <param name="sLogPath"></param>
        public Roomba(SerialPort IO, double Sensor_Polling_Interval, string sLogPath)
        {
            try
            {

                this.LogPath = sLogPath;

                Log.This("Initializing Sensors", c_sRoomba, this.LogSCICommands);

                this.Sensors = new Sensors(Sensor_Polling_Interval);
                this.IO_Buffer = new List<byte>();

                Log.This("Initialize Complete", c_sRoomba, this.LogSCICommands);

                //for now, I will assume that the caller of this object will Set Roomba's log.  It may change later.
            }
            catch (Exception ex)
            {
                throw new RoombaException("Error during Roomba Initialize: " + ex.Message);
            }

        }

        #region Events

        //public event EventHandler IO_DataReceived
        //{
        //    add { Events.AddHandler("IO_DataReceived", value); }
        //    remove { Events.RemoveHandler("IO_DataReceived", value); }
        //}

        #endregion
        #region Event Handlers

            /// <summary>
            /// 
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            public void IO_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
            {

                Log.This(c_sIO_DataRecieved, c_sRoomba, this.LogIO);

                //Read all bytes from InputBuffer into a Managed List & give it to the Sensor structure to parse into itself
                if (!this.Do_Not_Parse_RCV) { this.Parse_Sensor_Data(); };
            }
            public void IO_PinChanged(object sender, System.IO.Ports.SerialPinChangedEventArgs e)
            {
                Log.This(c_sIO_PinChanged, c_sRoomba, this.LogIO);
                 this.Sensors.IsCurrent = false;
             }

        #endregion

        #region Properties

            private SerialPort p_spIO = null;

            /// <summary>
            /// 
            /// </summary>
            public SerialPort IO
            {

                get
                {
                    return p_spIO;
                }
                set
                {
                    p_spIO = value;

                    if (this.IO != null)
                    {
                        this.IO.DataReceived -= new System.IO.Ports.SerialDataReceivedEventHandler(this.IO_DataReceived);
                        this.IO.PinChanged -= new SerialPinChangedEventHandler(this.IO_PinChanged);

                        Log.This("Serial Port Set: " + p_spIO.PortName, c_sRoomba, this.LogIO);
                        this.IO.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.IO_DataReceived);
                        this.IO.PinChanged += new SerialPinChangedEventHandler(this.IO_PinChanged);
                    }

                }

            }

            //These props are read only.  Later, I may add a 'set', but until then, call the function
            private Velocity p_vVelocity;

            /// <summary>
            /// 
            /// </summary>
            public Velocity Velocity
            {
                
                get
                {
                    return (this.p_vVelocity);
                }

            }

            private Radius p_rRadius;

            /// <summary>
            /// 
            /// </summary>
            public Radius Radius
            {

                get
                {
                    return (this.p_rRadius);
                }

            }

            private Sensors p_sSensors;

            /// <summary>
            /// 
            /// </summary>
            public Sensors Sensors
            {
                get
                {
                    return p_sSensors;
                }
                set
                {
                    p_sSensors = value;
                    Log.This("Sensors Set: ", c_sRoomba, this.LogSCICommands);
                }
            }

           private Macro p_mMacro = new Macro();

            /// <summary>
            /// 
            /// </summary>
            public Macro Macro
            {
                get
                {
                    return p_mMacro;
                }
                set
                {
                    p_mMacro = value;
                }
            }
  
            private byte p_bcBaud_Rate;

            /// <summary>
            /// 
            /// </summary>
            public byte Baud_Rate
            {

                get
                {
                    return (this.p_bcBaud_Rate);
                }

            }

            private byte p_byMode;

            /// <summary>
            /// 
            /// </summary>
            public byte Mode
            {

                get
                {
                    return p_byMode;
                }
                set
                {
                    try
                    {
                        bool bSuccess = this.SetMode(value); //item logged here
                        if (bSuccess) {p_byMode = value; };
                    }
                    catch (Exception ex)
                    {
                        throw new RoombaException("An Error has occurred while setting mode: " + ex.Message);
                    }
                }

            }

            private string p_sLogPath = "";

            /// <summary>
            /// This is the path and filename of the log used by RoombaSCI
            /// </summary>
            public string LogPath
            {

                get
                {
                    return (this.p_sLogPath);
                }
                set
                {
                    this.p_sLogPath = value;
                }


            }

            //A primitive form of Errorhandler
            private string p_sErrorText = "";

            /// <summary>
            /// Experimental property.  This should be updated whenever RoombaSCI encouters an error
            /// </summary>
            public string ErrorText
            {

                get
                {
                    return (this.p_sErrorText);
                }
                set
                {
                    this.p_sErrorText = value;
                }


            }

            public bool p_bDebugMode = true; //default ON

            /// <summary>
            /// 
            /// </summary>
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

            public bool p_bAutomatic_Polling = false; 

            /// <summary>
            /// 
            /// </summary>
            public bool Automatic_Polling
            {

                get
                {
                    return (this.p_bAutomatic_Polling);
                }
                set
                {
                    this.p_bAutomatic_Polling = value;
                }
            }

            public bool p_bLogIO = false; //default OFF

            /// <summary>
            /// 
            /// </summary>
            public bool LogIO
            {

                get
                {
                    return (this.p_bLogIO);
                }
                set
                {
                    this.p_bLogIO = value;
                }
            }

            public bool p_bLogPacketData = false; //default OFF

            /// <summary>
            /// 
            /// </summary>
            public bool LogPacketData
            {

                get
                {
                    return (this.p_bLogPacketData);
                }
                set
                {
                    this.p_bLogPacketData = value;
                }
            }

            public bool p_bDo_Not_Parse_RCV = false;

            /// <summary>
            ///  This command is for when you want to parse Roomba's IO yourself, and you don't want the Roomba object to waste valuable time populating its sensor object.
            /// </summary>
            public bool Do_Not_Parse_RCV
            {

                get
                {
                    return (this.p_bDo_Not_Parse_RCV);
                }
                set
                {
                    this.p_bDo_Not_Parse_RCV = value;
                }
            }

            private bool p_bLogSCICommands;
            public bool LogSCICommands
            {
                get
                {
                    return (this.p_bLogSCICommands);
                }
                set
                {
                    this.p_bLogSCICommands = value;
                }
            }

            private List<byte> p_bIO_Buffer;
            public List<byte> IO_Buffer
            {
                get
                {
                    return (this.p_bIO_Buffer);
                }
                set
                {
                    this.p_bIO_Buffer = value;
                }
            }

            private bool p_bNew_State;
            public bool New_State
            {
                get
                {
                    return (this.p_bNew_State);
                }
                set
                {
                    this.p_bNew_State = value;
                }
            }

            private byte p_byCurrent_Mode;
            public byte Current_Mode
            {
                get
                {
                    return (this.p_byCurrent_Mode);
                }
                set
                {
                    this.p_byCurrent_Mode = value;
                }
            }

        #endregion

        #region Functions

            #region IO

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

            #endregion

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public bool Wake()
            {
                this.IO.RtsEnable = true;

                for (int i = 0; i < 600; i++)
                {

                }

                this.IO.RtsEnable = false;

                this.Macro.SetAction("WAKE");

                return true;
            }

            /// <summary>
            ///  When a radius value passed, (between -2000 - 2000) this command will cause Roomba to spin in place. <br></br>
            ///  negative numbers will cause Roomba to turn clockwise, positive numbers = counter-clockwise
            /// </summary>
            /// <param name="rAngle"></param>
            /// <returns></returns>
            public bool Spin(Radius rAngle)
            {
                this.ErrorText = "";

                bool bSuccess = false;

                byte[] byRadius = BitConverter.GetBytes(rAngle.ToInt);

                byte[] b = new byte[5];
                b[0] = OpCode.Drive;
                b[1] = byRadius[0];
                b[2] = byRadius[1];
                b[3] = 0;
                b[4] = 0;

                string sDebugSend = "[" + b[0].ToString() + "][" + b[1].ToString() + "][" + b[2].ToString() + "][" + b[3].ToString() + "]";

                try
                {
                    this.IO.RtsEnable = false; //otherwise, roomba will ignore you
                    this.IO.Write(b, 0, b.Length);
                    this.p_rRadius = rAngle;
 
                    bSuccess = true;
                    Log.This("Spin Action Executed: ", c_sRoomba, this.LogSCICommands);
                    this.Macro.SetAction("SPIN " + rAngle as string);
                }
                catch (Exception ex)
                {
                    Log.This("Spin Fail: " + ex.Message, c_sRoomba, this.LogSCICommands);
                }

                this.Macro.SetAction("SPIN " + rAngle);

                return bSuccess;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public bool SpinLeft()
            {
                return this.Spin(-1);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public bool SpinRight()
            {
               return this.Spin(-2);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="bySCI_Mode"></param>
            /// <returns></returns>
            public bool SetMode(byte bySCI_Mode)
            {
                this.ErrorText = "";

                bool bSuccess = false;

                try
                {
                    this.IO.RtsEnable = false;

                    switch (bySCI_Mode)
                    {

                        case SCI_Mode.Off:
                            bSuccess = this.Execute(OpCode.Power);
                            this.p_byMode = SCI_Mode.Off;
                            Log.This("Mode Set to Off", c_sRoomba, this.LogSCICommands);
                            this.Macro.SetAction("SETMODE\tOff");
                            this.Current_Mode = SCI_Mode.Off;
                            break;

                        case SCI_Mode.Passive:
                            bSuccess = this.Execute(OpCode.Start);
                            this.p_byMode = SCI_Mode.Passive;
                            Log.This("Mode Set to Passive", c_sRoomba, this.LogSCICommands);
                            this.Current_Mode = SCI_Mode.Passive;
                            System.Threading.Thread.Sleep(20);

                            bSuccess &= this.Execute(OpCode.Enable_User_Control);
                            Log.This("Enable User Control Set", c_sRoomba, this.LogSCICommands);
                            this.Macro.SetAction("SETMODE\tPassive");

                            break;
                        case SCI_Mode.Safe:
                            bSuccess = this.Execute(OpCode.Safe_Mode);
                            this.p_byMode = SCI_Mode.Safe;
                            Log.This("Mode Set to Safe", c_sRoomba, this.LogSCICommands);
                            this.Macro.SetAction("SETMODE\tSafe");
                            this.Current_Mode = SCI_Mode.Safe;
                            break;

                        case SCI_Mode.Full:
                            bSuccess = this.Execute(OpCode.Full_Mode);
                            this.p_byMode = SCI_Mode.Full;
                            Log.This("Mode Set to Full", c_sRoomba, this.LogSCICommands);
                            this.Macro.SetAction("SETMODE\tFull");
                            this.Current_Mode = SCI_Mode.Full;
                            break;
                    }

                    if (bSuccess) { this.p_byMode = bySCI_Mode; }

                    System.Threading.Thread.Sleep(20);

                }
                catch (Exception ex)
                {
                    Log.This("Set Mode Fail: " + ex.Message, c_sRoomba, this.LogSCICommands);
                }

                return bSuccess;

            }

            public void EnforceSafeMode(Sensors rsCurrentSensorPoll)
            {
                if ((rsCurrentSensorPoll.Packet.WheelDrop.Left ||
                    rsCurrentSensorPoll.Packet.WheelDrop.Caster ||
                    rsCurrentSensorPoll.Packet.WheelDrop.Right ||
                    rsCurrentSensorPoll.Packet.Cliff.Left ||
                    rsCurrentSensorPoll.Packet.Cliff.FrontLeft ||
                    rsCurrentSensorPoll.Packet.Cliff.FrontRight ||
                    rsCurrentSensorPoll.Packet.Cliff.Right) && (!(this.Current_Mode == SCI_Mode.Full)))
                {
                    this.SetMode(SCI_Mode.Passive);
                }
            }

            #region Actions

                /// <summary>
                /// 
                /// </summary>
                /// <param name="bps"></param>
                /// <returns></returns>
                public bool Change_Baud_Rate(byte bps)
                {

                    this.ErrorText = ""; 

                    bool bSuccess = false;

                    try
                    {

                        Log.This("Baud Rate Change: " + bps.ToString(), c_sRoomba, this.LogSCICommands);
                        this.Execute(OpCode.Baud, bps);
                        p_bcBaud_Rate = bps;
                        this.Macro.SetAction("BAUD_RATE " + bps.ToString());

                        bSuccess = true;
                    
                    }
                    catch (Exception ex)
                    {
                        Log.This("Baud Rate Change Fail: " + ex.Message, c_sRoomba, this.LogSCICommands);
                    }

                    return bSuccess;
                }

                /// <summary>
                /// 
                /// </summary>
                /// <param name="bSend_Motor_Byte"></param>
                /// <returns></returns>
                public bool Motor_Action(byte bSend_Motor_Byte)
                {

                    this.ErrorText = "";

                    bool bSuccess = false;

                    //byte Test_Setting1 = Motor.Main_Brush.On;
                    //byte Test_Setting2 = Convert.ToByte(Motor.Side_Brush.On & Motor.Side_Brush.Off);
                    //byte Test_Setting3 = Convert.ToByte(Motor.Side_Brush.On & Motor.Side_Brush.On & Motor.Vacuum.On);

                    //Get & Add to the existing motor settings
                    ////////byte bExistingSettings = 0;

                    //TODO: We need to look up our existing settings, carry on existing settings, and reset only the ones passed. AND should do it.
                    //bGotSettings = this.CurrentRoomba.CurrentMotorSettings(ref bExistingSettings)?;

                    //Truth Table:
                    //Roomba             1  1  0  0 
                    //New Command   1  0  1  0
                    //------------------------------------
                    //Result                1  0   1  0

                    try
                    {
                        //TODO: Do a check here. If this is not a motor byte, then throw an exception

                        byte bSend = bSend_Motor_Byte;  //bExistingSettings +

                        this.IO.RtsEnable = false; //otherwise, roomba will ignore you

                        Log.This("Motor Action Requested: ", c_sRoomba, this.LogSCICommands);
                        bSuccess = this.Execute(OpCode.Motors, bSend);

                        this.SendMotorBytesToMacro(bSend_Motor_Byte);
                    }
                    catch (Exception ex)
                    {
                        Log.This("Motor Action Fail: " + ex.Message, c_sRoomba, this.LogSCICommands);
                    }

                    return bSuccess;
                }

                public void Drive(byte[] driveAction)
                {
                    //this function is new, for those who know what they want, and have it ready in advance - KRG 1-18-07
                    //as of 1-18-07, this function is the Drive function that is executed by the Macro object.

                    string sDebugSend = "[" + driveAction[0].ToString() + "][" + driveAction[1].ToString() + "][" + driveAction[2].ToString() + "][" + driveAction[3].ToString() + "][" + driveAction[4].ToString() + "]";

                    try
                    {
                        this.IO.RtsEnable = false;
                        this.IO.Write(driveAction, 0, driveAction.Length);
                        this.Macro.SetAction("DRIVE\t" + sDebugSend);

                        Log.This("Drive Action Success " + sDebugSend, c_sRoomba, this.LogSCICommands);
                    }
                    catch (Exception ex)
                    {
                        Log.This("Drive Action Fail: " + ex.Message + " Debug: " + sDebugSend, c_sRoomba, this.LogSCICommands);
                    }

                }
                
                /// <summary>
                /// 
                /// </summary>
                /// <param name="vSpeed"></param>
                /// <param name="rAngle"></param>
                /// <param name="bySCI_Mode"></param>
                /// <returns></returns>
                public bool Drive(Velocity vSpeed, Radius rAngle, byte bySCI_Mode)
                {
                    bool bDriveSuccess;
                    this.Mode = bySCI_Mode;
                    bDriveSuccess = this.Drive(vSpeed, rAngle);

                    return bDriveSuccess;
                }

                /// <summary>
                /// 
                /// </summary>
                /// <param name="vSpeed"></param>
                /// <param name="rAngle"></param>
                /// <returns></returns>
                public bool Drive(Velocity vSpeed, Radius rAngle)
                {
                    this.ErrorText = "";

                    bool bSafe = this.Mode == SCI_Mode.Safe;
                    bool bFull = this.Mode == SCI_Mode.Full;
                    bool bError = (!bSafe) & (!bFull);

                    //Push back at the user
                    if (bError)
                    {
                        throw new RoombaException("Roomba must be in Safe or Full mode before running Calling the Drive Function: ");
                    }

                    //Sample from the SCI Spec:
                    //to drive in reverse at a velocity of -200 mm/s while turning at a radius of 500mm, you would send the serial byte sequence
                    //[137] [255] [56] [1] [244]
                    //[137] [Velocity High Byte] [Velocity Low Byte] [Radius High Byte] [Radius Low Byte]

                    //divide up vSpeed & rAngle into 2 bytes ea
                    int num = rAngle.ToInt;
                    byte byAngleHi = (byte)(num >> 8);
                    byte byAngleLo = (byte)(num & 255);

                    num = vSpeed.ToInt;
                    byte bySpeedHi = (byte)(num >> 8);
                    byte bySpeedLo = (byte)(num & 255); 

                    bool bSuccess = false;

                    List<byte> lSend = new List<byte>();
                    lSend.Add(OpCode.Drive);
                    lSend.Add(bySpeedHi);
                    lSend.Add(bySpeedLo);
                    lSend.Add(byAngleHi);
                    lSend.Add(byAngleLo);
                    
                    string sDebugSend = "[" + lSend[0].ToString() + "][" + lSend[1].ToString() + "][" + lSend[2].ToString() + "][" + lSend[3].ToString() + "]["  + lSend[4].ToString() + "]";

                    try
                    {
                        Log.This("Drive Action: " + sDebugSend, c_sRoomba, this.LogSCICommands);

                        this.IO.RtsEnable = false;
                        this.IO.Write(lSend.ToArray(), 0, lSend.Count);
                        this.Macro.SetAction("DRIVE\t" + sDebugSend);

                        this.p_vVelocity = vSpeed;
                        this.p_rRadius = rAngle;

                        bSuccess = true;
                        Log.This("Drive Action Success " + bSuccess.ToString() + " Velocity: " + this.p_vVelocity.ToString() + " Radius: " + this.p_rRadius.ToString(), c_sRoomba, this.LogSCICommands);
                    }
                    catch (Exception ex)
                    {
                        Log.This("Drive Action Fail: " + ex.Message, c_sRoomba, this.LogSCICommands);
                    }

                    return bSuccess;
                }

                /// <summary>
                /// 
                /// </summary>
                /// <param name="bSetting"></param>
                /// <returns></returns>
                public bool SetLED(byte bSetting)
                {
                    this.ErrorText = ""; 

                    bool bSuccess = false;

                    try
                    {

                        byte[] b = new byte[4];
                        b[0] = OpCode.LEDs;
                        b[1] = bSetting;
                        b[2] = 0;
                        b[3] = 0;

                        this.IO.RtsEnable = false; //otherwise, roomba will ignore you
                        this.IO.Write(b, 0, b.Length);

                        bSuccess = true;
                        Log.This("Set LED Success: ", c_sRoomba, this.LogSCICommands);
                        this.Macro.SetAction("SET_LED " + b.ToString());

                    }
                    catch (Exception ex)
                    {
                        Log.This("Set LED Fail: " + ex.Message, c_sRoomba, this.LogSCICommands);
                    }

                    return bSuccess;

                }

                /// <summary>
                /// 
                /// </summary>
                /// <param name="bLED_Bits"></param>
                /// <param name="bPWR_Color"></param>
                /// <param name="bPWR_Intensity"></param>
                /// <returns></returns>
                public bool SetLEDs(byte bLED_Bits, byte bPWR_Color, byte bPWR_Intensity)
                {
                    this.ErrorText = ""; 

                    bool bSuccess = false;

                    List<byte> b = new List<byte>();

                    try
                    {
                        b.Add(OpCode.LEDs);
                        b.Add(bLED_Bits);
                        b.Add(bPWR_Color);
                        b.Add(bPWR_Intensity);

                        //write something here to enforce the values
                        this.IO.RtsEnable = false; //otherwise, roomba will ignore you
                        this.IO.Write(b.ToArray(), 0, b.Count);

                        bSuccess = true;
                        Log.This("Set LEDs Success", c_sRoomba, this.LogSCICommands);
                        this.Macro.SetAction("SET_LED " + b.ToString());

                    }
                    catch (Exception ex)
                    {
                        Log.This("Set LEDs Fail: " + ex.Message, c_sRoomba, this.LogSCICommands);
                    }

                    return bSuccess;

                }

            #endregion

        #endregion
        #region Supporting Functions

            /// <summary>
            ///  When this command is called, will parse all available data in Roomba's input buffer into the Roomba class sensor structure.
            /// </summary>
            /// <returns></returns>
            public bool Parse_Sensor_Data()
            {
                bool bSuccess = false;
                bool bState_Changed = false;

                try
                {
                    //List<byte> bylRecievedSensorData;
                    byte[] byString;

                    this.Get_Packet(out byString); //out bylRecievedSensorData,

                    this.LogSensorData(this.IO_Buffer); //bylRecievedSensorData

                    //Convert our bytes to a string
                    //Text_IO_Handler(Encoding.ASCII.GetString(byString)); //For Roomba's Text Responses

                    this.Set_Packet(this.IO_Buffer, out bSuccess,  out bState_Changed, byString);//

                }
                catch (Exception ex)
                {
                    Log.This("Parse Sensor Data Fail: " + ex.Message, c_sRoomba, this.LogSCICommands);
                }

                return bSuccess;

            }

            private void LogSensorData(List<byte> bylRecievedSensorData)
            {
                string sDebug = null;

                foreach (byte byCurrent in bylRecievedSensorData)
                {
                    sDebug += c_cOpen + byCurrent.ToString() + c_cClose;
                }

                //Roll through List & build as text string
                Log.This(c_sPacketData + "  " + sDebug, c_sRoomba, this.LogPacketData);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="bylRecievedSensorData"></param>
            /// <param name="byString"></param>
            private void Get_Packet(out byte[] byString) //out List<byte> bylRecievedSensorData,
            {
                int iBytesToRead = this.IO.BytesToRead;
                int iBytesLeft = iBytesToRead;

                if (this.LogIO)
                {
                    Log.This(c_sBytesToRead + iBytesToRead.ToString(), c_sRoomba, this.LogIO);
                }

                //bylRecievedSensorData = new List<byte>();
                byte byCurrent;

                int iCount = 0;

                this.Sensors.Previous_Bytes =  this.Sensors.Raw_Bytes = new List<byte>();

                //funny. If I don't clear this out now, then when I assign this.IO_Buffer below, it goes into this construct too. It is like I have a ref somewhere.
                this.Sensors.Raw_Bytes = new List<byte>();

                //Load our managed list..
                while (iBytesLeft > 0)
                {
                    
                    byCurrent = (byte)this.IO.ReadByte();
                    iCount++;

                    //GC.Collect();

                    this.IO_Buffer.Add(byCurrent);

                    iBytesLeft = this.IO.BytesToRead;
                }

                if (this.LogIO)
                {
                    Log.This(this.IO_Buffer.Count.ToString() + c_sBytesRetrieved + " bytes left: " + iBytesLeft.ToString() + " iCount: " + iCount.ToString(), c_sRoomba, this.LogIO);//bylRecievedSensorData
                }

                byString = this.IO_Buffer.ToArray(); //bylRecievedSensorData
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="bSuccess"></param>
            /// <param name="bState_Changed"></param>
            /// <param name="bylRecievedSensorData"></param>
            /// <param name="byString"></param>
            private void Set_Packet(List<byte> bylRecievedSensorData, out bool bSuccess, out bool bState_Changed, byte[] byString)//,
            {
                bSuccess = false;

                bState_Changed = !EqualByteArrays(this.Sensors.Previous_Bytes, bylRecievedSensorData); //
                this.New_State = bState_Changed;

                if (bState_Changed)
                {
                    this.Sensors.Raw_Bytes = bylRecievedSensorData;
                    //this.Sensors.Raw_Bytes = bylRecievedSensorData; //This is where our newly parsed bytes go to live..

                    //Make this configurable, I might want to look at just 26, or I will even parse > that since Sensor.Parse can line up the packet..
                    if (bylRecievedSensorData.Count >= Packet.Full) //TODO: this # will change. It will be the size of the data packet expected
                    {
                        Log.This(c_sParsingData, c_sRoomba, this.LogIO);
                        bSuccess = this.Sensors.Parse();

                        this.EnforceSafeMode(this.Sensors);

                        //TODO: If we recognize the packet at this point, then yank off only the value that we need to, in case of overlap.

                        this.IO_Buffer = new List<byte>();
                        Log.This(" - Resetting Buffer - " + c_sBytesRetrieved, c_sRoomba, this.LogIO);
                    }
                    else
                    {
                        Log.This(c_sIgnored, c_sRoomba, this.LogIO);
                    }

                    if (IO_Handler != null)
                    {
                        Log.This("Broadcast", c_sRoomba, this.LogIO);

                        //Tell the world in case they want it to divvy up themselves
                        this.IO_Handler(byString);

                        Log.This("After Broadcast", c_sRoomba, this.LogIO);
                    }
                }
                else
                {
                    //We still need to update this indicator, since technically, if we even got this far, Roomba gave us new information. 
                    if (bylRecievedSensorData.Count >= Packet.Full) //TODO: this # will change. It will be the size of the data packet expected
                    {
                        this.Sensors.LastUpdated = DateTime.Now;
                    }

                    Log.This("Roomba State unchanged.", c_sRoomba, this.LogIO);
                }
            }
            
            /// <summary>
            /// 
            /// </summary>
            /// <param name="data1"></param>
            /// <param name="data2"></param>
            /// <returns></returns>
            public static bool EqualByteArrays(List<byte> data1, List<byte> data2)
            {
                // If both are null, they're equal
                if (data1 == null && data2 == null) {return true;}

                // If either but not both are null, they're not equal
                if (data1 == null || data2 == null) { return false; }

                if (data1.Count != data2.Count) { return false; }

                for (int i = 0; i < data1.Count; i++)
                {
                    if (data1[i] != data2[i]){ return false; }
                }

                return true;
            }
            public bool ReadExisting()
            {
                int iBytesToRead = this.IO.BytesToRead;
                int iBytesLeft = iBytesToRead;

                string sRoombaText = "";

                //while (iBytesLeft > 0)
                //{

                    sRoombaText = this.IO.ReadExisting();
                    Text_IO_Handler(sRoombaText); //For Roomba's Text Responses

                    iBytesLeft = this.IO.BytesToRead;
                //}

                return true;

            }

            /// <summary>
            /// Sends a command to Roomba that requires a single OpCode and a single data byte
            /// </summary>
            /// <param name="bOpCode"></param>
            /// <param name="bSendByte"></param>
            /// <returns></returns>
            public bool Execute(byte bOpCode, byte bSendByte)
            {

                this.ErrorText = ""; 

                bool bSuccess = false;
                byte[] b = new byte[2];
                b[0] = bOpCode;
                b[1] = bSendByte;

                string sDebugSend = c_cOpen + b[0].ToString() + "][" + b[1].ToString() + c_cClose;

                try
                {

                    Log.This(c_sExecuteAction + sDebugSend, c_sRoomba, this.LogIO);
                    this.IO.Write(b, 0, b.Length);

                    bSuccess = true; //Command sent without error

                }
                catch (Exception ex)
                {
                    Log.This(c_sExecuteActionFail + ex.Message, c_sRoomba, this.LogIO);
                }

                return bSuccess;
            }
            
            /// <summary>
            /// Sends a command to Roomba that requires a single OpCode and no data bytes
            /// </summary>
            /// <param name="bOpCode"></param>
            /// <returns></returns>
            public bool Execute(byte bOpCode)
            {
                this.ErrorText = ""; 

                bool bSuccess = false;
                byte[] b = new byte[1];
                b[0] = bOpCode;

                string sDebugSend = c_cOpen + b[0].ToString() + c_cClose;

                try
                {
                    Log.This(c_sExecuteAction + sDebugSend, c_sRoomba, this.LogIO);
                    this.IO.Write(b, 0, b.Length);

                    bSuccess = true;
                }
                catch (Exception ex)
                {
                    Log.This("Execute Action Fail: " + ex.Message, c_sRoomba, this.LogIO);
                }

                return bSuccess;
            }

            /// <summary>
            /// Sends any command that is not in this framework.
            /// If you want to parse Roomba's IO yourself, and you don't want the Roomba object to waste valuable time populating its sensor object
            /// then set   Do_Not_Parse_RCV = True
            /// </summary>
            /// <param name="sendBytes"></param>
            /// <returns></returns>
            public bool Execute(List<byte> sendBytes) //Untested as of 3.3.07
            {
                this.ErrorText = "";

                bool bSuccess = false;

                byte[] b = sendBytes.ToArray();
                //byte[] b = new byte[4];
                //b[0] = 140;
                //b[1] = 1;
                //b[2] = 1;
                //b[3] = 81;
                //b[4] = 120;

                try
                {
                    this.IO.RtsEnable = false; //otherwise, roomba will ignore you
                    this.IO.Write(b, 0, b.Length);

                    bSuccess = true;
                    Log.This(c_sExecuteAction, c_sRoomba, this.LogSCICommands);
                }
                catch (Exception ex)
                {
                    Log.This(c_sExecuteAction + " fail ~ " + ex.Message, c_sRoomba, this.LogSCICommands);
                }

                return bSuccess;

                }

            protected void SendMotorBytesToMacro(byte bSend_Motor_Byte)
            {
                //string byteText = "";

                //all this is for getting the above executed info to the Macro Object in a text form..
                List<byte> motorBytes = new List<byte>();
                motorBytes.Add(bSend_Motor_Byte);

                //byte[] byteArray = motorBytes.ToArray();

                //foreach (byte b in byteArray)
                //{
                //    byteText += "[" + b + "]";
                //}

                this.Macro.SetAction("MOTORS\t" + bSend_Motor_Byte.ToString()); //This might need to be translated into english first
            }

        #endregion
    }
}




#region "these may be used later"

//#region "Logging & Error Handling"

//    protected bool LogThis(string sValue)
//    {

//        bool bSuccess = false;

//        if (this.DebugMode)
//        {
//            try
//            {
//                if (this.LogPath.Length > 0)
//                {

//                    System.IO.StreamWriter swLogWriter = new System.IO.StreamWriter(this.LogPath, true);
//                    swLogWriter.WriteLine("Roomba: " + this.GetTimeStamp(true) + "\t" + sValue + "\r\n");
//                    swLogWriter.Close();

//                    bSuccess = true;
//                }

//            }
//            catch (System.Exception ex)
//            {
//                throw; // new RoombaException(ex.Message);
//            }
//        }

//        return bSuccess;

//    }

//    private bool LogThis(string sValue, string sCallingFunction)
//    {
//        bool bSuccess = false;

//        if (this.DebugMode)
//        {
//            try
//            {
//                if (this.LogPath.Length > 0)
//                {
//                    System.IO.StreamWriter swLogWriter = new System.IO.StreamWriter(this.LogPath, true);
//                    swLogWriter.WriteLine("Roomba: " + this.GetTimeStamp(true) + "\t" + sValue + "\t" + sCallingFunction + "\r\n");
//                    swLogWriter.Close();

//                    bSuccess = true;
//                }

//            }
//            catch (System.Exception ex)
//            {
//                throw new RoombaException(ex.Message);
//            }
//        }

//        return bSuccess;

//    }

//    private bool LogError(string sErrorValue, string sCallingFunction)
//    {
//        bool bSuccess = false;

//        this.ErrorText = sErrorValue;

//        bSuccess = this.LogThis(sErrorValue, sCallingFunction);

//        return bSuccess;

//    }

//    /// <summary>
//    /// 
//    /// </summary>
//    /// <param name="bSplit"></param>
//    /// <returns></returns>
//    private string GetTimeStamp(bool bSplit)
//    {
//        string sTimeStamp = DateTime.Now.Year.ToString() +
//                                       DateTime.Now.Month.ToString() +
//                                       DateTime.Now.Day.ToString() +
//                                       DateTime.Now.Hour.ToString() +
//                                       DateTime.Now.Minute.ToString() +
//                                       DateTime.Now.Second.ToString() +
//                                       DateTime.Now.Millisecond.ToString();

//        if (bSplit)
//        {
//            sTimeStamp = DateTime.Now.Month.ToString() + "/" +
//                                  DateTime.Now.Day.ToString() + "/" +
//                                  DateTime.Now.Year.ToString() + "   " +
//                  DateTime.Now.Hour.ToString() + ":" +
//                  DateTime.Now.Minute.ToString() + ":" +
//                  DateTime.Now.Second.ToString() + "." +
//                  DateTime.Now.Millisecond.ToString();
//        }

//        return sTimeStamp;

//    }

//#endregion

//private bool SetBytes(byte bSetThis)
//{
//    //I am sure there is an operator to do this, but I am tired and dense

//    //Truth Table:
//    //Roomba             1  1  0  0 
//    //New Command   1  0  1  0
//    //------------------------------------
//    //Result                1  0   1  0

//    byte[] myBytes = new byte[1] { bSetThis };
//    BitArray myBA3 = new BitArray(myBytes);

//    //System.Console.Debugger.PrintValues(myBA3, 8);
//    bool[] bBitsToSet = new bool[8];
//    bool[] bBitsInRoomba = new bool[8];
//    bool[] bSetBytes = new bool[8];

//    GetBools(myBA3, 8, ref bBitsToSet);
//    //GetRoombaBools(ref bBitsInRoomba);

//    for (int i = 0; i < 8; i++)
//    {
//        if (bBitsInRoomba[i] == true && bBitsToSet[i] == true)
//        {
//            bSetBytes[i] = true;
//        }

//        if (bBitsInRoomba[i] == true && bBitsToSet[i] == false)
//        {
//            bSetBytes[i] = false;
//        }

//        if (bBitsInRoomba[i] == false && bBitsToSet[i] == true)
//        {
//            bSetBytes[i] = true;
//        }

//        if (bBitsInRoomba[i] == false && bBitsToSet[i] == false)
//        {
//            bSetBytes[i] = false;
//        }

//    }

//    //Send bSetBytes to Roomba
//    //this.Motor_Action((byte)bSetBytes);

//    return true;

//}
//private static void PrintValues(IEnumerable myList, int myWidth)
//{
//    int i = myWidth;
//    foreach (Object obj in myList)
//    {
//        if (i <= 0)
//        {
//            i = myWidth;
//            Console.WriteLine();
//        }
//        i--;
//        Console.Write("{0,8}", obj);
//    }
//    Console.WriteLine();
//}
//private static void GetBools(IEnumerable myList, int myWidth, ref bool[] myBools)
//{

//    int i = 0;
//    foreach (Object obj in myList)
//    {
//        myBools[i] = (bool)obj;
//        i++;
//    }
//    //for (int i = 0; i > myWidth; i++)
//    //{
//    //    myBools[i] = (bool)obj;
//    //}
//}

#endregion

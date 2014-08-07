using System;
using System.Collections;
using System.Collections.Generic;

namespace RoombaSCI
{
    public class SensorsException : System.ApplicationException
    {
        public SensorsException(string message) : base(message)
        {
        }
    }

    /// <summary>
    /// This structure is intended to be a representation of Roomba's SCI Sensor Packets.  This structure is set up as a group of property bags
    /// that are loaded every time Roomba's Sensors are polled by <b>Sensors.Parse()</b>
    /// </summary>
    public class Sensors
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Sensor_Polling_Interval"></param>
        public Sensors(double Sensor_Polling_Interval)
        {

            try
            {
                //Set HistoryLog Defaults
                //this.History.Log_Flags =  Sensor_Log_Flag.Sensor_Update  & Sensor_Log_Flag.Sensor_Parse;
                //this.History.Size = 10;

                this.PollingInterval = Sensor_Polling_Interval;
                //this.m_byCapacity1 = Sync_Battery_Capacity1;
                //this.m_byCapacity2 = Sync_Battery_Capacity2;

            }
            catch (Exception ex)
            {
                throw new SensorsException("An Error has occurred while initializing Sensors " + ex.Message);
            }

        }

        #region Constants

            protected string c_sAngle = "Angle";
            protected string c_sBumpBoth = "Bump.Both";
            protected string c_sBumpNone = "Bump.None";
            protected string c_sBumpRight = "Bump.Right";
            protected string c_sBumpLeft = "Bump.Left";
            protected string c_sClean = "Buttons.Clean";
            protected string c_sMax = "Buttons.Max";
            protected string c_sPower = "Buttons.Power";
            protected string c_sSpot = "Buttons.Spot";
            protected string c_sCapacity = "Capacity";
            protected string c_sCharge = "Charge";
            protected string c_sChargingState = "Charging_State";
            protected string c_sCliffFrontLeft = "Cliff.FrontLeft";
            protected string c_sCliffFrontRight = "Cliff.FrontRight";
            protected string c_sCliffLeft = "Cliff.Left";
            protected string c_sCliffRight = "Cliff.Right";
            protected string c_sCurrent = "Current";
            protected string c_sDirtDetectorLeft = "Dirt_Detector.Left";
            protected string c_sDirtDetectorRight = "Dirt_Detector_Right";
            protected string c_sDistance = "Distance";
            protected string c_sOverCurrentLeft = "OverCurrent.Left_Wheel";
            protected string c_sOverCurrentRight = "OverCurrent.Right_Wheel";
            protected string c_sOverCurrentMAIN = "OverCurrent.Main_Brush";
            protected string c_sOverCurrentSide = "OverCurrent.Side_Brush";
            protected string c_sOverCurrentVacuum = "OverCurrent.Vacuum";
            protected string c_sRemote = "Remote_Control_Command";
            protected string c_sTemperature = "Temperature";
            protected string c_sVirtualWall = "Virtual_Wall";
            protected string c_sVoltage = "Voltage";
            protected string c_sWall = "Wall";
            protected string c_sWheelDropCaster = "WheelDrop.Caster";
            protected string c_sWheelDropLeft = "WheelDrop.Left";
            protected string c_sWheelDropLeftAndCaster = "WheelDrop.LeftAndCaster";
            protected string c_sWheelDropLeftAndRight = "WheelDrop.LeftAndRight";
            protected string c_sWheelDropNone = "WheelDrop.None";
            protected string c_sWheelDropRight = "WheelDrop.Right";
            protected string c_sWheelDropRightAndCaster = "WheelDrop.RightAndCaster";

        #endregion
        #region Local Variables

        /// <summary>
            /// This is the value of the remote control default value.  Since the packets can jump around a little bit in the polling stream, this is used to sync things back up.
            /// </summary>
            private const byte c_byPacket_Sync = 255;

            private byte m_byCapacity1 = 10;
            private byte m_byCapacity2 = 142;

        #endregion

        #region Properties

            private double p_dPollingInterval;

            /// <summary>
            /// The interval (in milliseconds) that Roomba's Sensors will be polled
            /// </summary>
            public double PollingInterval
            {
                get
                {
                    return p_dPollingInterval;
                }
                set
                {
                    p_dPollingInterval = value;
                }
            }

            private double p_dPolling;

            /// <summary>
            /// 
            /// </summary>
            public double Polling
            {

                get
                {
                    return p_dPolling;
                }
                set
                {
                    bool bSuccess = this.Set_Polling_Interval(value);
                    if (bSuccess) { p_dPolling = value; };
                }

            }

            public List<byte> p_lBytes;

            /// <summary>
            /// Generic List of of the <b>raw</b> bytes that are returned from a sensor poll
            /// </summary>
            public List<byte> Raw_Bytes
            {

                get
                {
                    return p_lBytes;
                }
                set
                {
                    p_lBytes = value;
                }

            }

            public List<byte> p_lprBytes;

            /// <summary>
            /// Generic List of of the <b>raw</b> bytes that are returned from a sensor poll
            /// </summary>
            public List<byte> Previous_Bytes
            {

                get
                {
                    return p_lprBytes;
                }
                set
                {
                    p_lprBytes = value;
                }

            }

            public List<byte> p_lSync_Bytes;

            /// <summary>
            /// Generic List of of the bytes that are returned from a sensor poll that are assumed to be Roomba's sensor packet.
            /// </summary>
            public List<byte> Sync_Bytes
            {

                get
                {
                    return p_lSync_Bytes;
                }
                set
                {
                    p_lSync_Bytes = value;
                }

            }

            private bool p_bStopPolling;

            /// <summary>
            ///  When <b>true</b>, then polling of Roomba's sensors will cease
            /// </summary>
            public bool StopPolling
            {

                get
                {
                    return p_bStopPolling;
                }
                set
                {
                    p_bStopPolling = value;
                }

            }

            private bool p_bLock;

            /// <summary>
            /// Experimental property.  If Lock = true, then Sensor polling is ignored. Lock will keep the Sensors object from being repopulated
            /// </summary>
            public bool Lock
            {

                get
                {
                    return p_bLock;
                }
                set
                {
                    p_bLock = value;
                }

            }

            private DateTime p_dtLastUpdated;

            /// <summary>
            /// 
            /// </summary>
            public DateTime LastUpdated
            {

                get
                {
                    return p_dtLastUpdated;
                }
                set
                {
                    p_dtLastUpdated = value;
                }

            }

            private TimeSpan p_tsUpdateResponse;
            public TimeSpan UpdateResponse
            {

                get
                {
                    return p_tsUpdateResponse;
                }
                set
                {
                    p_tsUpdateResponse = value;
                }

            }

            private TimeSpan p_tsParseTime;
            public TimeSpan ParseTime
            {

                get
                {
                    return p_tsParseTime;
                }
                set
                {
                    p_tsParseTime = value;
                }

            }

            private bool p_bIsCurrent = false;

            /// <summary>
            /// Experimental property. Intended to reveal that the data sitting in the Sensors object is not old data. This property is set to True upon evaluation that this sensors object is being populated on a routine basis.
            /// </summary>
            public bool IsCurrent
            {

                get
                {
                    return p_bIsCurrent;
                }
                set
                {
                    p_bIsCurrent = value;
                }

            }

            private int p_iIsCurrent_Threshold = 300; //default value

            /// <summary>
            /// .
            /// </summary>
            public int IsCurrent_Threshold
            {

                get
                {
                    return p_iIsCurrent_Threshold;
                }
                set
                {
                    p_iIsCurrent_Threshold = value;
                }

            }

            private int p_iDefaultDataPoints = 200;
            public int DefaultDataPoints
            {

                get
                {
                    return p_iDefaultDataPoints;
                }
                set
                {
                    p_iDefaultDataPoints = value;
                }

            }

            private long p_iSensor_Parse = 200;
            public long Sensor_Parse
            {

                get
                {
                    return p_iSensor_Parse;
                }
                set
                {
                    p_iSensor_Parse = value;
                }

            }

            private bool p_bIsText = false;

            /// <summary>
            /// Experimental property. Set to True when it is determined that Roomba is returning Text (and not sensor packets) in response to sensor polling
            /// </summary>
            public bool IsText
            {

                get
                {
                    return p_bIsText;
                }
                set
                {
                    p_bIsText = value;
                }

            }

            private bool p_sSensorText = false;

            /// <summary>
            /// Experimental property. 
            /// </summary>
            public bool SensorText
            {

                get
                {
                    return p_sSensorText;
                }
                set
                {
                    p_sSensorText = value;
                }

            }

            private Sensor_Packet p_pPacket = new Sensor_Packet();

            /// <summary>
            /// Property bag<br></br>
            /// encopsulates the Sensor Packets Sent from Roomba in response to a sensor poll
            /// </summary>
            public Sensor_Packet Packet
            {

                get
                {
                    return p_pPacket;
                }
                set
                {
                    p_pPacket = value;
                }

            }


            private bool p_bAutoExport;

            /// <summary>
            ///  This is used by the Macro object, or anyone else wanting to refer to the Sensors structure this way.
            /// </summary>
            public bool AutoExport
            {

                get
                {
                    return p_bAutoExport;
                }
                set
                {
                    p_bAutoExport = value;
                }

            }

            private Hashtable p_hHashtable;

            /// <summary>
            ///  This is used by the Macro object, or anyone else wanting to refer to the Sensors structure this way.
            /// </summary>
            public Hashtable Hashtable
            {
                get
                {
                    return p_hHashtable;
                }
                set
                {
                    p_hHashtable = value;
                }
            }

        #endregion
        #region Functions

            /// <summary>
            ///  This is the function that divvies up Roomba's Raw bytes into this sensor structure
            /// </summary>
            /// <returns></returns>
            public bool Parse()
            {
                bool bSuccess = false;
                bool bBytesSyncd = false;
                int iPacketBytes = 0;

                System.Diagnostics.Stopwatch m_wParseTime = new System.Diagnostics.Stopwatch();

                try
                {
                    m_wParseTime.Start();

                    this.IsCurrent = false;

                    bBytesSyncd = this.Sync_Raw_Bytes2();

                    if (bBytesSyncd)
                    {
                        //What kind of packet do we have? Is it Text?
                        iPacketBytes = this.Sync_Bytes.Count;

                        //if we have a non-text sensor pull
                        if (iPacketBytes > 6)
                        {
                            this.Parse_BumpAndWheelDrops();
                            this.Parse_CliffSensors();
                            this.Parse_OverCurrents();
                            this.Parse_ChargingState();
                            this.Parse_Voltage();
                            this.Parse_Current();
                            this.Parse_Temperature();
                            this.Parse_Charge();
                            this.Parse_Capacity();
                            this.Parse_Wall();
                            this.Parse_Virtual_Wall();
                            this.Parse_Dirt_Detect();
                            this.Parse_Distance();
                            this.Parse_Angle();
                            this.Parse_Buttons();
                            this.LastUpdated = DateTime.Now;

                            this.Set_IsCurrent();
                            if (this.AutoExport) { this.Hashtable = this.ExportSensors(this.Packet); }
                        }
                    }
                    else
                    {
                        this.IsCurrent = false;
                    }

                    bSuccess = true;
                }
                catch (Exception ex)
                {
                    string sError = ex.Message; //for debug purposes.
                }
                finally
                {
                    m_wParseTime.Stop();
                    TimeSpan tsElapsed = m_wParseTime.Elapsed;

                    this.ParseTime = tsElapsed;
                    this.Sensor_Parse = m_wParseTime.ElapsedTicks;

                    m_wParseTime.Reset();
                }

                return bSuccess;
            }

            #region "Supporting Parse Functions"

                #region Sync'ing bytes

                    private bool Sync_Raw_Bytes2()
                    {
                        //Roomba sometimes includes some extra "undocumented bytes" at the start of the packet. I am not sure what they are, but one
                        //of my roombas does it on occasion. This code is my way of filtering it out.  I use the remote default of 255 as a way to deduce the start
                        //of packet.  Failing that, I set out and count boolean entries in the packet to find out where I am.

                        bool bSuccess = false;
                        int iStart_Of_Packet = 0;
                        //Loop through Raw bytes

                        bool bFoundIt;

                        //what we are looking for is the remote control's default value of 255
                        bFoundIt = this.Found_Remote_Marker(this.Raw_Bytes, ref iStart_Of_Packet); //The easy way..

                        //If we didn't find it, then that is because some extra bytes got shoved in at the front of the packet. and we have > 26 bytes. (logic elsewhere keeps us from ever seeing less than 26 bytes)
                        if (!bFoundIt)
                        {
                            //We are looking for 8 entries in a row that are Zero or One. This is the method that I use to locate where in the stream Roomba's packet is..
                            bFoundIt = this.Found_8_Bools(this.Raw_Bytes, ref iStart_Of_Packet); //the harder way - which may also mean that someone is currently sending a remote command. this feature is not tested.
                        }

                        bool bCreate_Sync_Bytes = this.Create_Sync_Bytes(iStart_Of_Packet);

                        bSuccess = bCreate_Sync_Bytes; //so what happens if they don't sync? I wouldn't want to log the volume of errors we would get

                        return bSuccess;
                    }
                    private bool Found_8_Bools(List<byte> Bytes_To_Search, ref int iStart_Of_Packet)
                    {
                        bool bSuccess = false;
                        List<bool> FoundBools = new List<bool>();
                        bool bCurrentMatch = false;

                        //Find 7 bools in Bytes_To_Search in which the previous bool examined is also a bool. Total = 8 Bools
                        int iBoolCount = 0;
                        int i = 1;

                        try
                        {

                            //Set BoolCount + 1 if current item == 0 or 1 and previous == 0 or 1
                            //I would prefor to use BinarySearch, but these must be consecutive
                            for (i = 0; i < Bytes_To_Search.Count; i++) //skip the first byte. That will never be our first bool.
                            {
                                bCurrentMatch = (Bytes_To_Search[i] == 0) || (Bytes_To_Search[i] == 1);
                                if (bCurrentMatch)
                                {
                                    FoundBools.Add(bCurrentMatch);
                                }
                                else
                                {
                                    //ok, so we did not find a bool. this could only mean 1 thing: We aren't in a string of bools no more..
                                    if (iBoolCount < 8)
                                    {
                                        FoundBools.Clear();
                                        FoundBools = new List<bool>();
                                    };
                                }

                                iBoolCount = FoundBools.Count;
                                if (iBoolCount == 8) { break; };

                            }

                            bSuccess = (iBoolCount == 8);
                            if (bSuccess) { iStart_Of_Packet = (i + 1) - (iBoolCount); };

                        }
                        catch
                        {
                        }

                        return bSuccess;
                    }
                    private bool Found_Remote_Marker(List<byte> Bytes_To_Search, ref int iStart_Of_Packet)
                    {
                        //I'm not gonna spend all day looking for it. its either here or its not. Bytes_To_Search[11]'s range is 0-15. 
                        //Seeing a 255 there is a sure fire way of telling that those undocumented bytes are sneaking in..
                        bool bFoundMarker = (Bytes_To_Search[10] == 255) & (Bytes_To_Search[11] < 16);

                        if (bFoundMarker) { iStart_Of_Packet = 0; }

                        return bFoundMarker;
                    }

                    private bool Get_Sync_Bytes(ref int iRemote_Default, ref int iCapacity1, ref int iCapacity2)
                    {
                        bool bSuccess = false;

                        try
                        {
                            iRemote_Default = this.Raw_Bytes.FindIndex(0, IsRemote_Sync);
                            iCapacity1 = this.Raw_Bytes.FindIndex(0, IsCapacity1);
                            iCapacity2 = this.Raw_Bytes.FindIndex(0, IsCapacity2);

                            bSuccess = true;

                        }
                        catch
                        {

                        }

                        return bSuccess;
                    }
                    public bool Create_Sync_Bytes(int iStart_Of_Packet)
                    {
                        bool bSuccess = false;

                        try
                        {
                            this.Sync_Bytes = new List<byte>();

                            this.Sync_Bytes.Add(this.Raw_Bytes[iStart_Of_Packet]);
                            this.Sync_Bytes.Add(this.Raw_Bytes[iStart_Of_Packet + 1]);
                            this.Sync_Bytes.Add(this.Raw_Bytes[iStart_Of_Packet + 2]);
                            this.Sync_Bytes.Add(this.Raw_Bytes[iStart_Of_Packet + 3]);
                            this.Sync_Bytes.Add(this.Raw_Bytes[iStart_Of_Packet + 4]);
                            this.Sync_Bytes.Add(this.Raw_Bytes[iStart_Of_Packet + 5]);
                            this.Sync_Bytes.Add(this.Raw_Bytes[iStart_Of_Packet + 6]);
                            this.Sync_Bytes.Add(this.Raw_Bytes[iStart_Of_Packet + 7]);
                            this.Sync_Bytes.Add(this.Raw_Bytes[iStart_Of_Packet + 8]);
                            this.Sync_Bytes.Add(this.Raw_Bytes[iStart_Of_Packet + 9]);
                            this.Sync_Bytes.Add(this.Raw_Bytes[iStart_Of_Packet + 10]);
                            this.Sync_Bytes.Add(this.Raw_Bytes[iStart_Of_Packet + 11]);
                            this.Sync_Bytes.Add(this.Raw_Bytes[iStart_Of_Packet + 12]);
                            this.Sync_Bytes.Add(this.Raw_Bytes[iStart_Of_Packet + 13]);
                            this.Sync_Bytes.Add(this.Raw_Bytes[iStart_Of_Packet + 14]);
                            this.Sync_Bytes.Add(this.Raw_Bytes[iStart_Of_Packet + 15]);
                            this.Sync_Bytes.Add(this.Raw_Bytes[iStart_Of_Packet + 16]);
                            this.Sync_Bytes.Add(this.Raw_Bytes[iStart_Of_Packet + 17]);
                            this.Sync_Bytes.Add(this.Raw_Bytes[iStart_Of_Packet + 18]);
                            this.Sync_Bytes.Add(this.Raw_Bytes[iStart_Of_Packet + 19]);
                            this.Sync_Bytes.Add(this.Raw_Bytes[iStart_Of_Packet + 20]);
                            this.Sync_Bytes.Add(this.Raw_Bytes[iStart_Of_Packet + 21]);
                            this.Sync_Bytes.Add(this.Raw_Bytes[iStart_Of_Packet + 22]);
                            this.Sync_Bytes.Add(this.Raw_Bytes[iStart_Of_Packet + 23]);
                            this.Sync_Bytes.Add(this.Raw_Bytes[iStart_Of_Packet + 24]);
                            this.Sync_Bytes.Add(this.Raw_Bytes[iStart_Of_Packet + 25]);

                            bSuccess = true;
                        }
                        catch
                        {

                        }

                        return bSuccess;
                    }

                    //Temporary, till i find a cleaner way of doing this.
                    private bool IsRemote_Sync(byte b)
                    {
                        if (b == Sensors.c_byPacket_Sync)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    private bool IsCapacity1(byte b)
                    {
                        if (b == this.m_byCapacity1)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    private bool IsCapacity2(byte b)
                    {
                        if (b == this.m_byCapacity2)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }

                #endregion

                /// <summary>
                /// 
                /// </summary>
                private void Parse_BumpAndWheelDrops()
                {
                    if (this.Sync_Bytes.Count > 0)
                    {

                        byte byBumpAndWheelDrops = this.Sync_Bytes[0];

                        //Bumps
                        this.Packet.Bump.Left = (Convert.ToByte(byBumpAndWheelDrops & Sensor_Code.Bump_Left) != 0);
                        this.Packet.Bump.Right = (Convert.ToByte(byBumpAndWheelDrops & Sensor_Code.Bump_Right) != 0);

                        //WheelDrops
                        this.Packet.WheelDrop.Left = (Convert.ToByte(byBumpAndWheelDrops & Sensor_Code.WheelDrop_Left) != 0);
                        this.Packet.WheelDrop.Right = (Convert.ToByte(byBumpAndWheelDrops & Sensor_Code.WheelDrop_Right) != 0);
                        this.Packet.WheelDrop.Caster = (Convert.ToByte(byBumpAndWheelDrops & Sensor_Code.WheelDrop_Caster) != 0);

                        this.Packet.WheelDrop.None = (this.Packet.WheelDrop.Left) & (this.Packet.WheelDrop.Right) & (this.Packet.WheelDrop.Caster) == false;
                    }
                }
                private void Parse_Wall()
                {
                    if (this.Sync_Bytes.Count > 1)
                    {
                        byte byWall = this.Sync_Bytes[1];

                        this.Packet.Wall = (byWall == 1);
                    }
                    else
                    {

                    }
                }

                private void Parse_CliffSensors()
                {
                    try
                    {

                        if (this.Sync_Bytes.Count > 5)
                        {
                            byte byCliffLeft = this.Sync_Bytes[2];
                            byte byCliffFrontLeft = this.Sync_Bytes[3];
                            byte byCliffFrontRight = this.Sync_Bytes[4];
                            byte byCliffRight = this.Sync_Bytes[5];

                            this.Packet.Cliff.Left = (byCliffLeft == 1);
                            this.Packet.Cliff.FrontLeft = (byCliffFrontLeft == 1);
                            this.Packet.Cliff.FrontRight = (byCliffFrontRight == 1);
                            this.Packet.Cliff.Right = (byCliffRight == 1);
                        }
                        else
                        {

                        }

                    }
                    catch (Exception ex)
                    {
                        string sError = ex.Message;
                    }

                }
                private void Parse_OverCurrents()
                {

                    if (this.Sync_Bytes.Count > 6)
                    {
                        byte byOverCurrent = this.Sync_Bytes[7];

                        //OverCurrent
                        this.Packet.OverCurrent.Left_Wheel = (Convert.ToByte(byOverCurrent & Sensor_Code.OverCurrent_Drive_Left) != 0);
                        this.Packet.OverCurrent.Right_Wheel = (Convert.ToByte(byOverCurrent & Sensor_Code.OverCurrent_Drive_Right) != 0);
                        this.Packet.OverCurrent.Side_Brush = (Convert.ToByte(byOverCurrent & Sensor_Code.OverCurrent_Side_Brush) != 0);
                        this.Packet.OverCurrent.Main_Brush = (Convert.ToByte(byOverCurrent & Sensor_Code.OverCurrent_Main_Brush) != 0);
                        this.Packet.OverCurrent.Vacuum = (Convert.ToByte(byOverCurrent & Sensor_Code.OverCurrent_Vacuum) != 0);
                    }
                }

                private void Parse_Dirt_Detect()
                {
                    if (this.Sync_Bytes.Count > 7)
                    {
                        this.Packet.Dirt_Detector.Left = this.Sync_Bytes[8];
                        this.Packet.Dirt_Detector.Right = this.Sync_Bytes[9];
                    }
                }
                private void Parse_Virtual_Wall()
                {
                    if (this.Sync_Bytes.Count > 5)
                    {
                        this.Packet.Virtual_Wall = this.Sync_Bytes[6] != 0;
                    }
                }
                private void Parse_Remote_Control()
                {
                    if (this.Sync_Bytes.Count > 9)
                    {
                        this.Packet.Remote_Control_Command = this.Sync_Bytes[10];
                    }
                }
                private void Parse_Angle()
                {
                    if (this.Sync_Bytes.Count > 13)
                    {
                        short difference = (short)((Sync_Bytes[14] << 8) | Sync_Bytes[15]);
                       
                        if (difference > 360) difference -= 360; // Limit to +-360
                        if (difference < -360) difference += 360;

                        this.Packet.Angle = difference; // (short)((2 * difference) / 258); //radians
                    }
                }
                private void Parse_Distance()
                {
                    if (this.Sync_Bytes.Count > 11)
                    {
                        try
                        {
                            byte[] byDistance = { this.Sync_Bytes[12], this.Sync_Bytes[13] };
                            this.Packet.Distance = (short)((Sync_Bytes[12] << 8) | Sync_Bytes[13]); 
                        }
                        catch (Exception ex)
                        {
                            string exMessage = ex.Message;
                            //Throw error of some sort
                        }
                    }
                }
                private void Parse_Buttons()
                {
                    if (this.Sync_Bytes.Count > 10)
                    {
                        byte byButtons = this.Sync_Bytes[11];

                        //Buttons
                        this.Packet.Buttons.Power = (Convert.ToByte(byButtons & Sensor_Code.Buttons_Power) != 0);
                        this.Packet.Buttons.Spot = (Convert.ToByte(byButtons & Sensor_Code.Buttons_Spot) != 0);
                        this.Packet.Buttons.Clean = (Convert.ToByte(byButtons & Sensor_Code.Buttons_Clean) != 0);
                        this.Packet.Buttons.Max = (Convert.ToByte(byButtons & Sensor_Code.Buttons_Max) != 0);
                        this.Packet.Buttons.Home = (Convert.ToByte(byButtons & Sensor_Code.Buttons_Home) != 0);
                    }

                }
                private void Parse_ChargingState()
                {
                    if (this.Sync_Bytes.Count > 15)
                    {
                        byte byChargingState = this.Sync_Bytes[16];

                        this.Packet.Charging_State = RoombaSCI.Charging_State.Indeterminate;
                        switch (byChargingState)
                        {
                            case 0:
                                this.Packet.Charging_State = RoombaSCI.Charging_State.Not_Charging;
                                break;
                            case 1:
                                this.Packet.Charging_State = RoombaSCI.Charging_State.Charging_Recovery;
                                break;
                            case 2:
                                this.Packet.Charging_State = RoombaSCI.Charging_State.Charging;
                                break;
                            case 3:
                                this.Packet.Charging_State = RoombaSCI.Charging_State.Trickle_Charging;
                                break;
                            case 4:
                                this.Packet.Charging_State = RoombaSCI.Charging_State.Waiting;
                                break;
                            case 5:
                                this.Packet.Charging_State = RoombaSCI.Charging_State.Charging_Error;
                                break;
                        }
                    }

                }
                private void Parse_Voltage()
                {
                    if (this.Sync_Bytes.Count > 16)
                    {
                        byte[] byVoltage = { this.Sync_Bytes[17], this.Sync_Bytes[18] };
                        this.Packet.Voltage = BitConverter.ToUInt16(byVoltage, 0);
                    }
                }
                private void Parse_Current()
                {
                    if (this.Sync_Bytes.Count > 18)
                    {
                        byte[] byCurrent = { this.Sync_Bytes[19], this.Sync_Bytes[20] };
                        this.Packet.Current = BitConverter.ToInt16(byCurrent, 0);
                    }
                }
                private void Parse_Temperature()
                {
                    if (this.Sync_Bytes.Count > 20)
                    {
                        this.Packet.Temperature = this.Sync_Bytes[21];
                    }
                }
                private void Parse_Charge()
                {
                    if (this.Sync_Bytes.Count > 21)
                    {
                        byte[] byCharge = { this.Sync_Bytes[22], this.Sync_Bytes[23] };
                        this.Packet.Charge = BitConverter.ToUInt16(byCharge, 0);
                    }
                }
                private void Parse_Capacity()
                {
                    if (this.Sync_Bytes.Count > 23)
                    {
                        byte[] byCapacity = { this.Sync_Bytes[24], this.Sync_Bytes[25] };
                        this.Packet.Capacity = BitConverter.ToUInt16(byCapacity, 0);
                    }
                }

            #endregion

            /// <summary>
            /// 
            /// </summary>
            /// <param name="lParse_Into_Sensor_Structure"></param>
            /// <returns></returns>
            public bool Parse(List<byte> lParse_Into_Sensor_Structure)
            {
                return false;
            }

            private void Set_IsCurrent()
            {
                double iPollInterval = this.PollingInterval;

                DateTime dtNow = DateTime.Now;
                DateTime dtLastUpdated = this.LastUpdated;
                TimeSpan tsResult = dtNow - dtLastUpdated;
                TimeSpan tsCalculation = tsResult.Add(TimeSpan.FromMilliseconds(this.PollingInterval + this.PollingInterval));
                TimeSpan tsCalcResult = tsCalculation.Subtract(tsResult);

                this.UpdateResponse = tsCalcResult;

                double dResult = (dtNow - dtLastUpdated).TotalMilliseconds;

                this.IsCurrent = (dResult < Convert.ToDouble(this.IsCurrent_Threshold));
                if (this.IsCurrent)
                {
                    this.LastUpdated = dtNow;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="interval"></param>
            /// <returns></returns>
            public bool Set_Polling_Interval(double interval)
            {

                try
                {
                    if (interval > 0)
                    {
                        //this.m_tPolling = new Timer(interval);
                        this.PollingInterval = interval;

                        //this.m_tPolling.Elapsed += new System.Timers.ElapsedEventHandler(this.m_tPolling_IntervalElapsed);
                    }
                    else
                    {
                        this.PollingInterval = 0;
                    }
                }
                catch (Exception ex)
                {
                    throw new SensorsException("An Error has occurred setting polling interval " + ex.Message);
                }
                return true;
            }

            //Move this to Sensors Object..
            public Hashtable ExportSensors(RoombaSCI.Sensor_Packet sensors)
            {
                Hashtable hLookup = new Hashtable();

                hLookup.Add(c_sAngle, sensors.Angle);
                hLookup.Add(c_sBumpRight, sensors.Bump.Right);
                hLookup.Add(c_sBumpLeft, sensors.Bump.Left);
                hLookup.Add(c_sClean, sensors.Buttons.Clean);
                hLookup.Add(c_sMax, sensors.Buttons.Max);
                hLookup.Add(c_sMax, sensors.Buttons.Home);
                hLookup.Add(c_sPower, sensors.Buttons.Power);
                hLookup.Add(c_sSpot, sensors.Buttons.Spot);
                hLookup.Add(c_sCapacity, sensors.Capacity);
                hLookup.Add(c_sCharge, sensors.Charge);
                hLookup.Add(c_sChargingState, sensors.Charging_State);
                hLookup.Add(c_sCliffFrontLeft, sensors.Cliff.FrontLeft);
                hLookup.Add(c_sCliffFrontRight, sensors.Cliff.FrontRight);
                hLookup.Add(c_sCliffLeft, sensors.Cliff.Left);
                hLookup.Add(c_sCliffRight, sensors.Cliff.Right);
                hLookup.Add(c_sCurrent, sensors.Current);
                hLookup.Add(c_sDirtDetectorLeft, sensors.Dirt_Detector.Left);
                hLookup.Add(c_sDirtDetectorRight, sensors.Dirt_Detector.Right);
                hLookup.Add(c_sDistance, sensors.Distance);
                hLookup.Add(c_sOverCurrentLeft, sensors.OverCurrent.Left_Wheel);
                hLookup.Add(c_sOverCurrentRight, sensors.OverCurrent.Right_Wheel);
                hLookup.Add(c_sOverCurrentMAIN, sensors.OverCurrent.Main_Brush);
                hLookup.Add(c_sOverCurrentSide, sensors.OverCurrent.Side_Brush);
                hLookup.Add(c_sOverCurrentVacuum, sensors.OverCurrent.Vacuum);
                hLookup.Add(c_sRemote, sensors.Remote_Control_Command);
                hLookup.Add(c_sTemperature, sensors.Temperature);
                hLookup.Add(c_sVirtualWall, sensors.Virtual_Wall);
                hLookup.Add(c_sVoltage, sensors.Voltage);
                hLookup.Add(c_sWall, sensors.Wall);
                hLookup.Add(c_sWheelDropCaster, sensors.WheelDrop.Caster);
                hLookup.Add(c_sWheelDropLeft, sensors.WheelDrop.Left);
                hLookup.Add(c_sWheelDropNone, sensors.WheelDrop.None);
                hLookup.Add(c_sWheelDropRight, sensors.WheelDrop.Right);

                //This is how you look up..
                //string x = hLookup["Angle"].ToString();

                return hLookup;
            }

        #endregion

    }
}

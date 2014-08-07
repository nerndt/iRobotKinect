using System;
using System.Timers;
using System.Collections.Generic;

namespace RoombaSCI
{
    /// <summary>
    /// The commands in which to control Roomba. Each command consists of a one byte code.  Some commands must also be followed by data bytes<br></br>
    /// Roomba will not respond to any commands while asleep.
    /// </summary>
    public static class OpCode
    {
        /// <summary>
        /// Starts the SCI.  The Start command must be sent before any other SCI Commands.  <i>This command puts Roomba in <b>passive</b> mode.</i><br></br>
        /// Data Bytes: 0<br></br>
        /// OpCode = 128<br></br>
        /// </summary>
        public const byte Start = 128;

        /// <summary>
        /// Sets the baud rate (in bps) at which SCI commands and data are sent.  The default baud rate at power up is 57600 bps<br></br>
        /// Once the baud rate is changed, it will persist until Roomba is power cycled by removing the battery or the battery is dead.<br></br>
        /// You must wait 100ms after sending this command before sending additional commands at the new baud rate.<br></br>
        /// <i>Roomba must be in <b>passive</b>, <b>safe</b>, or <b>full</b> mode to accept this command</i><br></br>
        /// <i>this command puts Roomba in <b>passive</b> mode</i><br></br>
        /// Data Bytes: 1  (0 - 11)<br></br>
        /// OpCode = 129<br></br>
        /// </summary>
        public const byte Baud = 129;

        /// <summary>
        ///  Enables User Control of Roomba. This command must be sent after the <i>start</i> command and before any control commands are sent.<br></br>
        ///  <i>Roomba must be in <b>passive</b> mode to accept this command</i><br></br>
        ///  <i>This command puts Roomba into <b>safe</b> mode</i><br></br>
        /// Data Bytes: 0<br></br>
        /// OpCode = 130<br></br>
        /// </summary>
        public const byte Enable_User_Control = 130;

        /// <summary>
        /// Enables a restricted mode of operation for Roomba. Roomba will react to all sensor input (such as cliff and bumper sensors). as appropriate.
        ///  <i>This command puts Roomba into <b>safe</b> mode</i><br></br>
        /// Roomba must be in <b>full</b> mode to accept this command<br></br>
        /// Data Bytes: 0<br></br>
        /// OpCode = 131<br></br>
        /// </summary>
        public const byte Safe_Mode = 131;

        /// <summary>
        /// Enables unrestricted control of Roomba.
        ///  <i>This command puts Roomba into <b>full</b> mode</i><br></br>
        /// Roomba must be in <b>safe</b> mode to accept this command<br></br>
        /// Data Bytes: 0<br></br>
        /// OpCode = 132<br></br>
        /// </summary>
        public const byte Full_Mode = 132;

        /// <summary>
        /// Puts Roomba to sleep, the same as the normal "power" button press.  To wake,  use the <b>wake</b> function, or set Roomba.IO.RtsEnable Off, then on for >500 MS
        ///  <i>This command puts Roomba into <b>passive</b> mode</i><br></br>
        /// Roomba must be in <b>safe</b> or <b>full</b> mode to accept this command<br></br>
        /// Data Bytes: 0<br></br>
        /// OpCode = 133<br></br>
        /// </summary>
        public const byte Power = 133;

        /// <summary>
        /// Starts a spot cleaning cycle. the same as a normal "spot" button press.
        ///  <i>This command puts Roomba into <b>passive</b> mode</i><br></br>
        /// Roomba must be in <b>safe</b> or <b>full</b> mode to accept this command<br></br>
        /// Data Bytes: 0<br></br>
        /// OpCode = 134<br></br>
        /// </summary>
        public const byte Spot = 134;

        /// <summary>
        /// Starts a normal cleaning cycle. the same as a normal "clean" button press.
        ///  <i>This command puts Roomba into <b>passive</b> mode</i><br></br>
        /// Roomba must be in <b>safe</b> or <b>full</b> mode to accept this command<br></br>
        /// Data Bytes: 0<br></br>
        /// OpCode = 135<br></br>
        /// </summary>
        public const byte Clean = 135;

        /// <summary>
        /// Starts a maximum cleaning cycle. the same as a normal "max" button press.
        ///  <i>This command puts Roomba into <b>passive</b> mode</i><br></br>
        /// Roomba must be in <b>safe</b> or <b>full</b> mode to accept this command<br></br>
        /// Data Bytes: 0<br></br>
        /// OpCode = 136<br></br>
        /// </summary>
        public const byte Max = 136;

        /// <summary>
        /// Controls Roomba's drive wheels. This command takes 4 data bytes.<br></br>
        /// These data bytes are interpreted as two 16 bit values using twos-compliment.<br></br>
        /// The first two bytes specify the average velocity of the drive wheels in millimeters per second (mm/s)<br></br>
        /// The next 2 bytes specify the radius, in millimeters, in which Roomba should turn.  The longer radii make Roomba drive straighter; shorter radii make Roomba turn more.<br></br>
        /// A drive command with a positive velocity and a positive radius will make Roomba drive forward while turning toward the left.<br></br>
        /// A negative radius will make Roomba turn toward the right.<br></br>
        /// Special cases for the radius make Roomba turn in place or drive straight.<br></br>
        /// <br></br>
        /// Note: The robot system and its environment impose restrictions that may prevent the robot from accurately carrying out some drive commands. For example, it may not be possible to drive in a large arc with a large radius of curvature.<br></br>
        /// <br></br>
        /// Data Bytes: 4<br></br>
        /// Data Bytes 1 and 2: Velocity (-500 - 500 mm/s)<br></br>
        /// Data Bytes 3 and 4: Radius (-2000 - 2000 mm)<br></br>
        /// Special cases for Radius: 32768 = straight, -1 = turn in place clockwise, 1 = Turn in place counter-clockwise<br></br>
        /// <br></br>
        /// C# usage example:<br></br>
        /// To drive in reverse at a velocity of -200mm/s while turning at a radius of 500mm:<br></br>
        /// myRoombaObj.Drive(-200, 500);<br></br>
        /// <br></br>
        ///  <i>This command does not change Roomba's mode</i><br></br>
        /// Roomba must be in <b>safe</b> or <b>full</b> mode to accept this command<br></br>
        /// OpCode = 137<br></br>
        /// </summary>
        public const byte Drive = 137;

        /// <summary>
        /// Controls Roomba's cleaning motors.
        /// This OpCode is used in conjunction with the RoombaSCI.Motor class
        /// 
        ///  <i>This command does not change Roomba's mode</i><br></br>
        /// Roomba must be in <b>safe</b> or <b>full</b> mode to accept this command<br></br>
        /// OpCode = 138<br></br>
        /// </summary>
        public const byte Motors = 138;

        /// <summary>
        /// Controls Roomba's LEDs.  This Opcode is used in conjunction with Roombas LED class
        ///  <i>This command does not change Roomba's mode</i><br></br>
        /// Roomba must be in <b>safe</b> or <b>full</b> mode to accept this command<br></br>
        /// OpCode = 139<br></br>
        /// </summary>
        public const byte LEDs = 139;

        /// <summary>
        /// Specifies a song to be played later. This opcode curretly has minimal support by RoombaSCI (as of 5/24/06)<br></br>
        ///  <i>This command does not change Roomba's mode</i><br></br>
        /// Roomba must be in <b>passive</b>, <b>safe</b> or <b>full</b> mode to accept this command<br></br>
        /// Stay Tuned!<br></br>
        /// OpCode = 140<br></br>
        /// </summary>
        public const byte Song = 140;

        /// <summary>
        /// Plays one of 16 Songs. This opcode curretly has minimal support by RoombaSCI (as of 5/24/06)<br></br>
        /// If th requested song has not been specified yet (with OpCode 140 - OpCode.Song), then the Play command does nothing<br></br>
        ///  <i>This command does not change Roomba's mode</i><br></br>
        /// Roomba must be in <b>safe</b> or <b>full</b> mode to accept this command<br></br>
        /// OpCode = 141<br></br>
        /// Stay Tuned!<br></br>
        /// </summary>
        public const byte Play = 141;

        /// <summary>
        /// Requests a packet of Sensor Data Bytes. The user can select one of 4 different Sensor packets.
        /// This Command is used in conjunction with the <b>Sensors</b> class
        ///  <i>This command does not change Roomba's mode</i><br></br>
        /// Roomba must be in <b>passive</b>, <b>safe</b> or <b>full</b> mode to accept this command<br></br>
        /// OpCode = 142<br></br>
        /// </summary>
        public const byte Sensors = 142;

        /// <summary>
        /// Turns on Roomba's force-seeking dock mode, which causes the robot to immediately attempt to dock during its cleaning cycle.<br></br>
        /// if it encounters the docking beams from its home base. (note, however, that if the robot was not active in a clean, spot, or max cycle<br></br>
        /// it will not attempt to execute the docking.)  Normally, the robot attempts to dock only if the cleaning cycle has completed or the battery<br></br>
        /// is nearing depletion. The command cam be sent anytime, but the mode will be cancelled if the robot turns off, begins charging, or is<br></br>
        /// commanded to <b>safe</b> or <b>full</b> mode.
        /// OpCode = 143<br></br>
        /// </summary>
        public const byte Force_Seeking_Dock = 143;

    }

    /// <summary>
    /// 
    /// </summary>
    public static class Sensor_Code
    {
        /// <summary>
        /// 
        /// </summary>
        public const byte Bump_Right = 1;

        /// <summary>
        /// 
        /// </summary>
        public const byte Bump_Left = 2;

        /// <summary>
        /// 
        /// </summary>
        public const byte WheelDrop_Right = 4;

        /// <summary>
        /// 
        /// </summary>
        public const byte WheelDrop_Left = 8;

        /// <summary>
        /// 
        /// </summary>
        public const byte WheelDrop_Caster = 16;

        ///// <summary>
        ///// 
        ///// </summary>
        //public const byte WheelDrop_LeftAndRight = WheelDrop_Left & WheelDrop_Right; //Calculated, not checked

        ///// <summary>
        ///// 
        ///// </summary>
        //public const byte WheelDrop_LeftAndCaster = 24; 

        ///// <summary>
        ///// 
        ///// </summary>
        //public const byte WheelDrop_RightAndCaster = 20; 

        /// <summary>
        /// 
        /// </summary>
        public const byte OverCurrent_Side_Brush = 1;

        /// <summary>
        /// 
        /// </summary>
        public const byte OverCurrent_Vacuum = 2;

        /// <summary>
        /// 
        /// </summary>
        public const byte OverCurrent_Main_Brush = 4;

        /// <summary>
        /// 
        /// </summary>
        public const byte OverCurrent_Drive_Right = 8;

        /// <summary>
        /// 
        /// </summary>
        public const byte OverCurrent_Drive_Left = 16;

        /// <summary>
        /// 
        /// </summary>
        public const byte Buttons_Max = 1;

        /// <summary>
        /// 
        /// </summary>
        public const byte Buttons_Clean = 2;

        /// <summary>
        /// 
        /// </summary>
        public const byte Buttons_Spot = 4;

        /// <summary>
        /// 
        /// </summary>
        public const byte Buttons_Home = 5;

        /// <summary>
        /// 
        /// </summary>
        public const byte Buttons_Power = 8;

    }

    /// <summary>
    /// Property bag that encapsulates the sensor packet sent by Roomba when polled.
    /// </summary>
    public class Sensor_Packet
    {
        
        #region "Packet Subset 1 (10 bytes)"

            //Range: 0-31
            private Bump_Sensor p_bpBump = new Bump_Sensor(); // Bump + WheelDrops = 1 byte, unsigned

            /// <summary>
            /// Roomba SCI sensor packet subset 1<br></br>
            /// 1 byte, unsigned<br></br>
            ///  Range  0-1<br></br>
            /// <i>Property bag</i><br></br>
            /// When any of this property's members are <b>true</b>, denotes that a bump has occurred at the appropriate sensor on Roomba's front bumper 
            /// </summary>
            public Bump_Sensor Bump
            {
                get
                {
                    return p_bpBump;
                }
                set
                {
                    p_bpBump = value;
                }
            }

            private WheelDrop p_wWheelDrop = new WheelDrop();

            /// <summary>
            /// 
            /// </summary>
            public WheelDrop WheelDrop
            {
                get
                {
                    return p_wWheelDrop;
                }
                set
                {
                    p_wWheelDrop = value;
                }
            }

            //Range: 0-1
            private bool p_bWall; //1 byte, unsigned

            /// <summary>
            /// 
            /// </summary>
            public bool Wall
            {
                get
                {
                    return p_bWall;
                }
                set
                {
                    p_bWall = value;
                }
            }

            //Range: 0-1 on each Cliff Sensor
            private Cliff_Sensor p_csCliff = new Cliff_Sensor(); //1 byte, unsigned

            /// <summary>
            /// 
            /// </summary>
            public Cliff_Sensor Cliff
            {
                get
                {
                    return p_csCliff;
                }
                set
                {
                    p_csCliff = value;
                }
            }

            //Range: 0-1
            private bool p_bVirtual_Wall; //1 byte, unsigned

            /// <summary>
            /// 
            /// </summary>
            public bool Virtual_Wall
            {
                get
                {
                    return p_bVirtual_Wall;
                }
                set
                {
                    p_bVirtual_Wall = value;
                }
            }

            //Range: 0-31
            private OverCurrent p_oOverCurrent = new OverCurrent(); //Range: 0-31

            /// <summary>
            /// 
            /// </summary>
            public OverCurrent OverCurrent
            {
                get
                {
                    return p_oOverCurrent;
                }
                set
                {
                    p_oOverCurrent = value;
                }
            }

            //Range: 0-255
            private Dirt_Detector p_ddDirt_Detector = new Dirt_Detector(); //1 byte, unsigned

            /// <summary>
            /// 
            /// </summary>
            public Dirt_Detector Dirt_Detector
            {
                get
                {
                    return p_ddDirt_Detector;
                }
                set
                {
                    p_ddDirt_Detector = value;
                }
            }

        #endregion
        #region "Packet Subset 2 (6 Bytes)"

            //Range 0-255
            private ushort p_uRemote_Control_Command; //1 byte, unsigned

            /// <summary>
            /// 
            /// </summary>
            public ushort Remote_Control_Command
            {
                get
                {
                    return p_uRemote_Control_Command;
                }
                set
                {
                    p_uRemote_Control_Command = value;
                }
            } 

            //Range 0-15
            private Buttons p_buButtons = new Buttons();  //1 byte, unsigned

            /// <summary>
            /// 
            /// </summary>
            public Buttons Buttons
            {
                get
                {
                    return p_buButtons;
                }
                set
                {
                    p_buButtons = value;
                }
            }

            //Range -32768 - 32767
            private short p_uiDistance; //2 bytes, signed

            /// <summary>
            /// 
            /// </summary>
            public short Distance
            {
                get
                {
                    return p_uiDistance;
                }
                set
                {
                    p_uiDistance = value;
                }
            }

            //Range -32768 - 32767
            private short p_uiAngle; //2 bytes, signed

            /// <summary>
            /// 
            /// </summary>
            public short Angle
            {
                get
                {
                    return p_uiAngle;
                }
                set
                {
                    p_uiAngle = value;
                }
            }

        #endregion
        #region "Packet Subset 3 (10 Bytes)"

            //Range 0-5
            private byte p_byCharging_State;  //1 byte, unsigned

            /// <summary>
            /// 
            /// </summary>
            public byte Charging_State
            {
                get
                {
                    return p_byCharging_State;
                }
                set
                {
                    p_byCharging_State = value;
                }
            }

            //Range 0 - 65535
            private ushort p_usVoltage; //2 bytes, unsigned

            /// <summary>
            /// 
            /// </summary>
            public ushort Voltage
            {
                get
                {
                    return p_usVoltage;
                }
                set
                {
                    p_usVoltage = value;
                }
            }

            //Range -32768 - 32767
            private short p_shCurrent; //2 bytes, signed

            /// <summary>
            /// 
            /// </summary>
            public short Current
            {
                get
                {
                    return p_shCurrent;
                }
                set
                {
                    p_shCurrent = value;
                }
            }

            //Range -128 - 127
            private byte p_byTemperature; //1 byte, signed

            /// <summary>
            /// 
            /// </summary>
            public byte Temperature
            {
                get
                {
                    return p_byTemperature;
                }
                set
                {
                    p_byTemperature = value;
                }
            }

            //Range 0 - 65535
            private UInt16 p_uiCharge; //2 bytes, unsigned

            /// <summary>
            /// 
            /// </summary>
            public UInt16 Charge
            {
                get
                {
                    return p_uiCharge;
                }
                set
                {
                    p_uiCharge = value;
                }
            }

            //Range 0 - 65535
            private UInt16 p_uiCapacity; //2 bytes, unsigned

            /// <summary>
            /// 
            /// </summary>
            public UInt16 Capacity
            {
                get
                {
                    return p_uiCapacity;
                }
                set
                {
                    p_uiCapacity = value;
                }
            }

        #endregion

    }

    /// <summary>
    /// These constants represent Roomba's 4 Operating Modes. 
    /// </summary>
    public static class SCI_Mode
    {

        /// <summary>
        /// 
        /// </summary>
        public const byte Off = 0;

        /// <summary>
        /// 
        /// </summary>
        public const byte Passive = 1;

        /// <summary>
        /// 
        /// </summary>
        public const byte Safe = 2;

        /// <summary>
        /// 
        /// </summary>
        public const byte Full = 3;

    }

    /// <summary>
    /// 
    /// </summary>
    public static class Motor
    {

        /// <summary>
        /// 
        /// </summary>
        public static class Vacuum
        {

            /// <summary>
            /// 
            /// </summary>
            public const byte On = 2;

            /// <summary>
            /// 
            /// </summary>
            public const byte Off = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        public static class Main_Brush
        {

            /// <summary>
            /// 
            /// </summary>
            public const byte On = 4;

            /// <summary>
            /// 
            /// </summary>
            public const byte Off = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        public static class Side_Brush
        {

            /// <summary>
            /// 
            /// </summary>
            public const byte On = 1;

            /// <summary>
            /// 
            /// </summary>
            public const byte Off = 0;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class LED
    {

        /// <summary>
        /// 
        /// </summary>
        public static class Dirt_Detect
        {
            /// <summary>
            /// 
            /// </summary>
            public const byte On = 1;

            /// <summary>
            /// 
            /// </summary>
            public const byte Off = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        public static class Max
        {

            /// <summary>
            /// 
            /// </summary>
            public const byte On = 2;

            /// <summary>
            /// 
            /// </summary>
            public const byte Off = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        public static class Clean
        {

            /// <summary>
            /// 
            /// </summary>
            public const byte On = 4;

            /// <summary>
            /// 
            /// </summary>
            public const byte Off = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        public static class Spot
        {

            /// <summary>
            /// 
            /// </summary>
            public const byte On = 8;

            /// <summary>
            /// 
            /// </summary>
            public const byte Off = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        public static class Status
        {

            /// <summary>
            /// 
            /// </summary>
            public const byte Red = 16;

            /// <summary>
            /// 
            /// </summary>
            public const byte Green = 32;

            /// <summary>
            /// 
            /// </summary>
            public const byte BrightRed = 48;

            /// <summary>
            /// 
            /// </summary>
            public const byte Off = 0;
        }

    }

    /// <summary>
    /// 
    /// </summary>
    public static class BaudCode
    {

        /// <summary>
        /// 
        /// </summary>
        public const byte x300 = 0;

        /// <summary>
        /// 
        /// </summary>
        public const byte x600 = 1;

        /// <summary>
        /// 
        /// </summary>
        public const byte x1200 = 2;

        /// <summary>
        /// 
        /// </summary>
        public const byte x2400 = 3;

        /// <summary>
        /// 
        /// </summary>
        public const byte x4800 = 4;

        /// <summary>
        /// 
        /// </summary>
        public const byte x9600 = 5;

        /// <summary>
        /// 
        /// </summary>
        public const byte x14400 = 6;

        /// <summary>
        /// 
        /// </summary>
        public const byte x19200 = 7;

        /// <summary>
        /// 
        /// </summary>
        public const byte x28800 = 8;

        /// <summary>
        /// 
        /// </summary>
        public const byte x38400 = 9;

        /// <summary>
        /// 
        /// </summary>
        public const byte x57600 = 10;

        /// <summary>
        /// 
        /// </summary>
        public const byte x115200 = 11;

    }

    /// <summary>
    /// This structure is used to read and set Roomba's velocity. This structure is designed to be used as a variable.
    /// This structure also serves to keep any assigned variables within the limits of the SCI spec
    /// The limits are: -500mm/s - 500 mm/s
    /// 
    /// </summary>
    /// <example>
    ///  Velocity x = 250; //if the programmer sets this to a value > 500, then x will automatically set itself to 500
    ///  Radius y = -400;
    ///  this.CurrentRoomba.Drive(x, y);
    /// </example>
    public struct Velocity
    {
        private readonly int m_iValue;

        public const int Maximum_Forward = 500;
        public const int Maximum_Reverse = -500;

        public static implicit operator Velocity(int iValue)
        {
            return new Velocity(iValue);
        }
        public Velocity(int iSpeed)
        {
            if (iSpeed > 500) { iSpeed = 500; };
            if (iSpeed < -500) { iSpeed = -500; };

            this.m_iValue = iSpeed;
        }
        public int ToInt
        {
            get
            {
                return this.m_iValue;
            }
        }

    }

    /// <summary>
    /// 
    /// </summary>
    public struct Radius
    {
        private readonly int m_iValue;
        public const int Straight = 32768;

        public const int Maximum_Right = -2000;
        public const int Maximum_Left = 2000;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iValue"></param>
        /// <returns></returns>
        public static implicit operator Radius(int iValue)
        {
            return new Radius(iValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iAngle"></param>
        public Radius(int iAngle)
        {
            if ((iAngle > 2000) & (iAngle != 32768)) { iAngle = 2000; }
            if (iAngle < -2000) { iAngle = -2000; }

            this.m_iValue = iAngle;
        }

        /// <summary>
        /// 
        /// </summary>
        public int ToInt
        {
            get
            {
                return this.m_iValue;
            }
        }

    }

    public static class Packet
    {
        public const byte Full = 26;
    }

    #region Property Objects

        /// <summary>
        /// Roomba SCI sensor packet subset 1<br></br>
        /// 1 byte, unsigned<br></br>
        ///  Range  0-1<br></br>
        /// <i>Property bag</i><br></br>
        /// When any of the class members are <b>true</b>, denotes that a cliff is present at the appropriate sensor on the bottom of Roomba's front bumper 
        /// </summary>
        public class Cliff_Sensor
        {
            /// <summary>
            /// When <b>true</b>, denotes that a cliff is present at the <b>leftmost</b> cliff sensor on the bottom of  Roomba's front bumper 
            /// </summary>
            public bool Left;

            /// <summary>
            ///When <b>true</b>, denotes that a cliff is present at the <b>front leftt</b> cliff sensor on the bottom of Roomba's front bumper 
            /// </summary>
            public bool FrontLeft;

            /// <summary>
            ///When <b>true</b>, denotes that a cliff is present at the <b>rightmost</b> cliff sensor on the bottom of Roomba's front bumper 
            /// </summary>
            public bool Right;

            /// <summary>
            ///When <b>true</b>, denotes that a cliff is present at the <b>front right</b> cliff sensor on the bottom of Roomba's front bumper 
            /// </summary>
            public bool FrontRight;
        }

        /// <summary>
        /// Roomba SCI sensor packet subset 1<br></br>
        /// 1 byte, unsigned<br></br>
        ///  Range  0-1<br></br>
        /// <i>Property bag</i><br></br>
        /// When any of the class members are <b>true</b>, denotes that a bump has occurred at the appropriate sensor on Roomba's front bumper 
        /// </summary>
        public class Bump_Sensor
        {
            /// <summary>
            /// When <b>true</b>, denotes that a bump has occurred at the <b>left</b> bump sensor on Roomba's front bumper 
            /// </summary>
            public bool Left;

            /// <summary>
            /// When <b>true</b>, denotes that a bump has occurred at the <b>right</b> bump sensor on Roomba's front bumper 
            /// </summary>
            public bool Right;
        }

        /// <summary>
        /// Roomba SCI sensor packet subset 1<br></br>
        /// 1 byte, unsigned<br></br>
        /// Range  0-1<br></br>
        /// <i>Property bag</i><br></br>
        /// When any of the class members are <b>true</b>, denotes a that one of Roomba's wheels has dropped, or a combination thereof
        /// </summary>
        public class WheelDrop
        {
            /// <summary>
            /// When <b>true</b>, denotes that no wheel drops are occurring
            /// </summary>
            public bool None;

            /// <summary>
            /// When <b>true</b>, denotes that Roomba's <b>left</b> wheel has dropped
            /// </summary>
            public bool Left;

            /// <summary>
            /// When <b>true</b>, denotes that Roomba's <b>right</b> wheel has dropped
            /// </summary>
            public bool Right;

            /// <summary>
            /// When <b>true</b>, denotes that Roomba's <b>center</b> wheel/caster has dropped
            /// </summary>
            public bool Caster;
        }

        /// <summary>
        /// Roomba SCI sensor packet subset 1<br></br>
        /// 1 byte, unsigned (for each detector)<br></br>
        /// Range  0-255<br></br>
        /// <i>Property bag</i><br></br>
        /// For each member, a value of 0 indicates that no dirt is detected. Higher values indicate higher levels of dirt detected. (255 Max)
        /// </summary>
        public class Dirt_Detector
        {
            /// <summary>
            /// For the <b>left</b> detector, value of 0 indicates that no dirt is detected. Higher values indicate higher levels of dirt detected.
            /// </summary>
            public UInt16 Left;

            /// <summary>
            /// For the <b>right</b> detector, value of 0 indicates that no dirt is detected. Higher values indicate higher levels of dirt detected.
            /// </summary>
            public UInt16 Right;
        }

        /// <summary>
        /// Roomba SCI sensor packet subset 2<br></br>
        /// 1 byte, unsigned<br></br>
        /// Range 0-31<br></br>
        /// <i>Property bag</i><br></br>
        /// When any of the class members are <b>true</b>, denotes an overcurrent state in one of Roomba's 5 motors.
        /// </summary>
        public class OverCurrent
        {
            /// <summary>
            /// When <b>true</b>, denotes an overcurrent state in Roomba's <b>left wheel</b> motor.
            /// </summary>
            public bool Left_Wheel;

            /// <summary>
            /// When <b>true</b>, denotes an overcurrent state in Roomba's <b>right wheel</b> motor.
            /// </summary>
            public bool Right_Wheel;

            /// <summary>
            /// When <b>true</b>, denotes an overcurrent state in Roomba's <b>main brush</b> motor.
            /// </summary>
            public bool Main_Brush;

            /// <summary>
            /// When <b>true</b>, denotes an overcurrent state in Roomba's <b>side brush</b> motor.
            /// </summary>
            public bool Side_Brush;

            /// <summary>
            /// When <b>true</b>, denotes an overcurrent state in Roomba's <b>vacuum</b> motor.
            /// </summary>
            public bool Vacuum;
        }

        /// <summary>
        /// Roomba SCI sensor packet subset 2<br></br>
        /// 1 byte, unsigned<br></br>
        /// Range 1-15<br></br>
        /// <i>Property bag</i><br></br>
        /// When any of the class members are <b>true</b>, denotes a that one of Roomba's buttons have been pressed, or a combination thereof
        /// </summary>
        public class Buttons
        {
            /// <summary>
            ///  When <b>true</b>, denotes that Roomba's <b>power</b> button has been pressed.
            /// </summary>
            public bool Power;

            /// <summary>
            ///  When <b>true</b>, denotes that Roomba's <b>spot</b> button has been pressed.
            /// </summary>
            public bool Spot;

            /// <summary>
            ///  When <b>true</b>, denotes that Roomba's <b>clean</b> button has been pressed.
            /// </summary>
            public bool Clean;

            /// <summary>
            ///  When <b>true</b>, denotes that Roomba's <b>max</b> button has been pressed.
            /// </summary>
            public bool Max;
            
            /// <summary>
            ///  When <b>true</b>, denotes that Roomba's <b>home</b> button has been pressed. Forces docking
            /// </summary>
            public bool Home;
        }

        /// <summary>
        /// Roomba SCI sensor packet subset 3<br></br>
        /// 1 byte, unsigned<br></br>
        /// Range 0-5<br></br>
        /// <i>Property bag</i><br></br>
        /// When any of the class members are <b>true</b>, denotes a that Roomba is in the specified charging state.
        /// </summary>
        public class Charging_State
        {
            /// <summary>
            /// 
            /// </summary>
            public const byte Not_Charging = 0;

            /// <summary>
            ///  When <b>true</b>, denotes that Roomba's battery is not being charged.
            /// </summary>
            public const byte Charging_Recovery = 1;

            /// <summary>
            /// 
            /// </summary>
            public const byte Charging = 2;

            /// <summary>
            /// 
            /// </summary>
            public const byte Trickle_Charging = 3;

            /// <summary>
            /// 
            /// </summary>
            public const byte Waiting = 4;

            /// <summary>
            /// 
            /// </summary>
            public const byte Charging_Error = 5;

            /// <summary>
            /// 
            /// </summary>
            public const byte Indeterminate = 6;

        }

        /// <summary>
        /// 
        /// </summary>
        public class Charge_State_Description
        {

            public const string Not_Charging = "Not Charging";
            public const string Charging_Recovery = "Charging Recovery";
            public const string Charging = "Charging";
            public const string Trickle_Charging = "Trickle Charging";
            public const string Waiting = "Waiting";
            public const string Charging_Error = "Charging Error";
            public const string Indeterminate = "Indeterminate";

            public const string Plugged_In = "Plugged In";
        }

    #endregion

}
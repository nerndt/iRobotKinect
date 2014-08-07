using System;
using System.IO;
using System.Xml;
using System.IO.Ports;
using System.Xml.Serialization;
using System.Collections.Generic;

using RoombaSCI;


//Serialization example
//ShoppingList myList = new ShoppingList();
//myList.AddItem( new Item( "eggs",1.49 ) );
//myList.AddItem( new Item( "ground beef",3.69 ) );
//myList.AddItem( new Item( "bread",0.89 ) );

//// Serialization
//XmlSerializer s = new XmlSerializer( typeof( ShoppingList ) );
//TextWriter w = new StreamWriter( @"c:\list.xml" );
//s.Serialize( w, myList );
//w.Close();

//// Deserialization
//ShoppingList newList;
//TextReader r = new StreamReader( "list.xml" );
//newList = (ShoppingList)s.Deserialize( r );
//r.Close();

namespace iRobotKinect
{
    #region Config Classes

        public class Config_Polling
        {
            //Polling
            private bool p_bSensors = true;
            public bool Sensors
            {
                get
                {
                    return p_bSensors;
                }
                set
                {
                    p_bSensors = value;
                }
            }

            //Polling.Frequency
            private int p_iFrequency = 500; // NGE07212014 lowered rate so that Kinect has more cycels to work 100; //default value
            public int Frequency
            {
                get
                {
                    return p_iFrequency;
                }
                set
                {
                    p_iFrequency = value;
                }
            }
        }
        public class Config_Sensors
        {
            //Sensors
            private int p_iIsCurrent_Threshold = 300; //default value
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

        }
        public class Config_COMM
        {
            //COMM
            private string p_sConnectedTo = "COM1"; //default value
            public string ConnectedTo
            {
                get
                {
                    return p_sConnectedTo;
                }
                set
                {
                    p_sConnectedTo = value;
                }
            }

            private byte p_sBaudRate = BaudCode.x57600; //default value
            public byte BaudRate
            {
                get
                {
                    return p_sBaudRate;
                }
                set
                {
                    p_sBaudRate = value;
                }
            }

            private byte p_iDataBits = 8; //default value
            public byte DataBits
            {
                get
                {
                    return p_iDataBits;
                }
                set
                {
                    p_iDataBits = value;
                }
            }

            private bool p_bDTR_Enable = false; //default value
            public bool DTR_Enable
            {
                get
                {
                    return p_bDTR_Enable;
                }
                set
                {
                    p_bDTR_Enable = value;
                }
            }

            private StopBits p_sbStopBits = StopBits.One; //default value
            public StopBits StopBits
            {
                get
                {
                    return p_sbStopBits;
                }
                set
                {
                    p_sbStopBits = value;
                }
            }

            private Handshake p_hHandshake = Handshake.None; //default value
            public Handshake Handshake
            {
                get
                {
                    return p_hHandshake;
                }
                set
                {
                    p_hHandshake = value;
                }
            }

            private Parity p_pParity = Parity.None; //default value
            public Parity Parity
            {
                get
                {
                    return p_pParity;
                }
                set
                {
                    p_pParity = value;
                }
            }

            private bool p_bRTS_Enabled = false; //default value
            public bool RTS_Enabled
            {
                get
                {
                    return p_bRTS_Enabled;
                }
                set
                {
                    p_bRTS_Enabled = value;
                }
            }

        }
        public class Config_Logging
        {
            private string p_sPath = "";
            public string Path
            {
                get
                {
                    return p_sPath;
                }
                set
                {
                    p_sPath = value;
                }
            }

            private bool p_bRoombaUI;
            public bool RoombaUI
            {
                get
                {
                    return p_bRoombaUI;
                }
                set
                {
                    p_bRoombaUI = value;
                }
            }

            private bool p_bLogSCICommands;
            public bool LogSCICommands
            {
                get
                {
                    return p_bLogSCICommands;
                }
                set
                {
                    p_bLogSCICommands = value;
                }
            }

            private bool p_bRoomba_IO;
            public bool Roomba_IO
            {
                get
                {
                    return p_bRoomba_IO;
                }
                set
                {
                    p_bRoomba_IO = value;
                }
            }

            private bool p_bRoomba_PacketData;
            public bool Roomba_PacketData
            {
                get
                {
                    return p_bRoomba_PacketData;
                }
                set
                {
                    p_bRoomba_PacketData = value;
                }
            }

            private bool p_bRoomba_Poller;
            public bool Roomba_Poller
            {
                get
                {
                    return p_bRoomba_Poller;
                }
                set
                {
                    p_bRoomba_Poller = value;
                }
            }

            private bool p_sStartForm;
            public bool StartForm
            {
                get
                {
                    return p_sStartForm;
                }
                set
                {
                    p_sStartForm = value;
                }
            }

            private bool p_sDriveForm;
            public bool DriveForm
            {
                get
                {
                    return p_sDriveForm;
                }
                set
                {
                    p_sDriveForm = value;
                }
            }

            private bool p_sStartForm_Timer;
            public bool StartForm_Timer
            {
                get
                {
                    return p_sStartForm_Timer;
                }
                set
                {
                    p_sStartForm_Timer = value;
                }
            }

            private bool p_sStartForm_Charging;
            public bool StartForm_Charging
            {
                get
                {
                    return p_sStartForm_Charging;
                }
                set
                {
                    p_sStartForm_Charging = value;
                }
            }

        }
        public class Config_Packet
        {

        }
        public class Config_Graph
        {

        }
        public class Config_Drive
        {

        }
        public class Config_Command
        {

        }
        public class Config_Macro
        {

        }
        public class Config_Song
        {

        }
        public class Config_Statistics
        {

        }
        public class Config_Forms
        {
            private Config_StartForm p_cStartForm = new Config_StartForm();
            public Config_StartForm StartForm
            {
                get
                {
                    return (this.p_cStartForm);
                }
                set
                {
                    this.p_cStartForm = value;
                }
            }

            private Config_DriveForm p_cDriveForm = new Config_DriveForm();
            public Config_DriveForm DriveForm
            {
                get
                {
                    return (this.p_cDriveForm);
                }
                set
                {
                    this.p_cDriveForm = value;
                }
            }
        }
        public class Config_StartForm
        {
            private int p_iLastUpdated_DisplayLag = 25; //default value
            public int LastUpdated_DisplayLag
            {
                get
                {
                    return p_iLastUpdated_DisplayLag;
                }
                set
                {
                    p_iLastUpdated_DisplayLag = value;
                }
            }

            private int p_iFormUpdated_DisplayLag = 25; //default value
            public int FormUpdated_DisplayLag
            {
                get
                {
                    return p_iFormUpdated_DisplayLag;
                }
                set
                {
                    p_iFormUpdated_DisplayLag = value;
                }
            }

            private bool p_bBattery_Check = true;
            public bool Battery_Check
            {
                get
                {
                    return p_bBattery_Check;
                }
                set
                {
                    p_bBattery_Check = value;
                }
            }

            private int p_iTimer = 500; // NGE07212014 100;
            public int Timer
            {
                get
                {
                    return p_iTimer;
                }
                set
                {
                    p_iTimer = value;
                }
            }

        }
        public class Config_DriveForm
        {
            private bool p_bAccurate_Sensor_Display = false;
            public bool Accurate_Sensor_Display
            {
                get
                {
                    return p_bAccurate_Sensor_Display;
                }
                set
                {
                    p_bAccurate_Sensor_Display = value;
                }
            }
        }

    #endregion

    [Serializable]
    public class Config_Settings
    {
        //Property Bag that can load and save itself to/From an XML file.
        //slap a timer on here to do periodic saves.

        private Config_Polling p_cPolling = new Config_Polling();
        public Config_Polling Polling
        {
            get
            {
                return (this.p_cPolling);
            }
            set
            {
                this.p_cPolling = value;
            }
        }

        private Config_Sensors p_cSensors = new Config_Sensors();
        public Config_Sensors Sensors
        {
            get
            {
                return (this.p_cSensors);
            }
            set
            {
                this.p_cSensors = value;
            }
        }

        private Config_COMM p_cCOMM = new Config_COMM();
        public Config_COMM COMM
        {
            get
            {
                return (this.p_cCOMM);
            }
            set
            {
                this.p_cCOMM = value;
            }
        }

        private Config_Forms p_cForms = new Config_Forms();
        public Config_Forms Forms
        {
            get
            {
                return (this.p_cForms);
            }
            set
            {
                this.p_cForms = value;
            }
        }

        private Config_Logging p_cLog = new Config_Logging();
        public Config_Logging Log
        {
            get
            {
                return (this.p_cLog);
            }
            set
            {
                this.p_cLog = value;
            }
        }

        public void Load()
        {
        }
        public void Save()
        {
            string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            XmlSerializer xsApply = new XmlSerializer(typeof(iRobotKinect.Config_Settings));
            TextWriter twWriteFile = new StreamWriter(path + @"\config.xml");
            xsApply.Serialize(twWriteFile, Program.UI.Config);
            twWriteFile.Close();
        }

    }

}

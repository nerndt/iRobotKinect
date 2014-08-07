using System;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System.Collections;

using Logging;

namespace RoombaSCI
{
    public class MacroException : System.ApplicationException
    {
        public MacroException(string message): base(message)
        {
        }
    }

    public class Macro_Common
    {
        protected const string c_sRoomba = "Roomba";
        protected const string c_sPathNotFound = "Macro file path not found";
        protected const string c_sRoombaNotFound = "CurrentRoomba not set";

        #region Properties

            string p_sFilePath;
            public string FilePath
            {
                get
                {
                    return (this.p_sFilePath);
                }
                set
                {
                    this.p_sFilePath = value;
                }
            }

            public bool p_bPathExists = false;
            public bool PathExists
            {
                get
                {
                    return (this.p_bPathExists);
                }
                set
                {
                    this.p_bPathExists = value;
                }
            }

        #endregion

        protected bool VerifyPath()
        {
            this.PathExists = System.IO.File.Exists(this.FilePath);
            return this.PathExists;
        }
    }

    //This class is used by Roomba/Roomba_Poller
    //The Macro evaluator is simple: If a comparison is TRUE then it keeps executing, if it is false,
    //then it jumps to the appropriate END construct
    public class Macro : Macro_Common
    {
        #region Constants
            protected const string c_sWait = "WAIT";

        #endregion
        #region Properties

            public bool p_bRecording = false;
            public bool Recording
        {

            get
            {
                return (this.p_bRecording);
            }
            set
            {
                this.p_bRecording = value;
            }
        }

            public bool p_bExecuting = false;
            public bool Executing
            {
                get
                {
                    return (this.p_bExecuting);
                }
                set
                {
                    this.p_bExecuting = value;
                }
            }

            public bool p_bFinished = true;
            public bool Finished
            {
                get
                {
                    return (this.p_bFinished);
                }
                set
                {
                    this.p_bFinished = value;
                }
            }

            public bool p_bMacroError = false;
            public bool MacroError
            {
                get
                {
                    return (this.p_bMacroError);
                }
                set
                {
                    this.p_bMacroError = value;
                    //raise ErrorEvent
                }
            }

            public int p_bExecutingLine = -1;
            public int ExecutingLine
            {
                get
                {
                    return (this.p_bExecutingLine);
                }
                set
                {
                    this.p_bExecutingLine = value;
                    //raise ExecutingLine Event
                }
            }

            public int p_iWaitTime = 100;
            public int WaitTime
            {
                get
                {
                    return (this.p_iWaitTime);
                }
                set
                {
                    this.p_iWaitTime = value;
                }
            }

            public Roomba_Poller p_rCurrentRoomba;
            public Roomba_Poller CurrentRoomba
            {
                get
                {
                    return (this.p_rCurrentRoomba);
                }
                set
                {
                    this.p_rCurrentRoomba = value;
                }
            }

            public RoombaSCI.Timer p_tWaitTimer;
            public RoombaSCI.Timer WaitTimer
            {
                get
                {
                    return (this.p_tWaitTimer);
                }
                set
                {
                    this.p_tWaitTimer = value;
                }
            }

        #endregion

        public Macro()
        {

        }
        public Macro(Roomba_Poller macroFor)
        {
            this.CurrentRoomba = macroFor;
            this.WaitTimer = new RoombaSCI.Timer();
            this.WaitTimer.Tick += new EventHandler(OnTimedEvent);
            this.WaitTimer.Period = 1;
        }

        #region Events

            private void OnTimedEvent(object sender, System.EventArgs e)
            {
                this.WaitTime += 1;
            }

        #endregion

        public void Record()
        {
            //Verify Path
            this.Recording = this.VerifyPath();

            if (this.PathExists == false)
            {
                throw new MacroException(c_sPathNotFound);
            }
        }
        
        public void SetAction(string action)
        {
            if (this.Recording && !this.Executing)
            {
                if (this.PathExists)
                {
                    this.WaitTimer.Stop();

                    int timeSinceLastAction = this.WaitTime;

                    this.Store(c_sWait + "\t" + timeSinceLastAction.ToString());

                    //TODO: If Wait is 0, then we need to store a minimum of x MS that is pulled from the config page. 20 is a likely default
                    this.Store(action);

                    this.WaitTime = 0;
                    this.WaitTimer.Start();
                }
                else
                {
                    throw new MacroException(c_sPathNotFound);
                }
            }
        }
        public void Stop()
        {
            this.Recording = false;
            this.Executing = false;
        }

        public void Store(string sValue)
        {
            if (this.PathExists)
            {
                try
                {
                    System.IO.StreamWriter swLogWriter = new System.IO.StreamWriter(this.FilePath, true);
                    swLogWriter.WriteLine(Log.GetTimeStamp(true) + "\t" + sValue);
                    swLogWriter.Close();
                }
                catch (Exception ex)
                {
                    throw new MacroException(ex.Message);
                }
            }
            else
            {
                throw new MacroException(c_sPathNotFound);
            }

        }

        public void Execute()
        {
            this.Execute(this.FilePath);
        }

        public void Execute(string filePath)
        {
            //Chop file into lines, then words & execute them in order.
            //The intent of the Execute functionality is not to enforce any rules. Execute if recognized, ignore if not..

            string currentLine = null;
            string currentCommand = null;
            string commandParameter = null;
            byte[] currentcommandByte = null;

            System.IO.StreamReader srMacro = new System.IO.StreamReader(filePath);

            while ((currentLine = srMacro.ReadLine()) != null)
            {
                if (currentLine.Length > 0)
                {
                    //We are expecting 1 command per line.
                    this.Parse(currentLine, out currentCommand, out commandParameter, out currentcommandByte);

                    if ((currentcommandByte != null) | (commandParameter != null))
                    {

                        //IF
                        //ENDIF
                        //LOOPIF
                        //ENDLOOP
                        
                    //case "IF":

                    //    //This is how you look up..
                    //    //string x = hLookup["Angle"].ToString();

                    //    //this.CurrentRoomba.Sensors.Hashtable[action]
                    //   break;

                        this.Execute_Action(currentCommand, currentcommandByte, commandParameter);
                    }
                }
            }

            this.Executing = false;
            this.Finished = true;
        }
        
        public void Execute_Action(string action, byte[] actionParam, string commandParameter)
        {
            //If commands do not meet the format we are looking for, then raise MacroError flag, 

            //Add support in here for a wait command, and an IF structure. (that will check sensors)
            //so user can say in Macro "IF (Sensors.Distance = 50) {Motors.Vacuum.On}else{Motors.SideBrush.On} 

            if (this.CurrentRoomba != null)
            {
                switch (action.ToUpper())
                {
                    case "WAIT":
                        Thread.Sleep(Convert.ToInt32(commandParameter));
                        break;

                    case "SETMODE":

                        this.Execute_SetMode(commandParameter);
                        break;

                    //case "STARTPOLLING":
                    //    this.CurrentRoomba.Start_Automatic_Polling();
                    //    break;

                    case "MOTORS":
                        if (commandParameter != null)
                        {
                            this.CurrentRoomba.Motor_Action(Convert.ToByte(commandParameter));
                        }
                        break;

                    case "DRIVE":
                        //actionParam[0] = OpCode.Drive; //replace our placeholder with the appropriate OpCode
                        this.CurrentRoomba.Drive(actionParam); //heck, you could write straight to the I/O here with what you have. why use Drive?
                        break;

                    //default:
                    //    //do nothing. This is not the most smart command interpreter.
                    //    break;
                }
            }
            else
            {
                throw new MacroException(c_sRoombaNotFound);
            }

        }
        
        public void Execute_SetMode(string commandParameter)
        {
            switch (commandParameter.ToUpper())
            {
                case "SAFE":
                    this.CurrentRoomba.SetMode(SCI_Mode.Safe);
                    break;

                case "FULL":
                    this.CurrentRoomba.SetMode(SCI_Mode.Full);
                    break;

                case "PASSIVE":
                    this.CurrentRoomba.SetMode(SCI_Mode.Passive);
                    break;

                case "OFF":
                    this.CurrentRoomba.SetMode(SCI_Mode.Off);
                    break;
            }
        }

        protected void Parse(string currentLine, out string command, out string parameter, out byte[] commandByte)
        {
            //string[] fullCommand = null;
            command = "";
            commandByte = null;
            parameter = null;

            string[] items = currentLine.Split('\t');

            if (items.Length > 1)
            {
                //fullCommand = items[1].Split(' ');
                command = items[1];  //Ex: WAIT  or DRIVE
                parameter = items[2];

                if (parameter.StartsWith("[")) //Ex. 0  or  [137][0][20][128][0] 
                {
                    commandByte = GetCommandBytes(parameter);
                }
            }
        }
        
        protected byte[] GetCommandBytes(string currentItem)
        {
            //expecting something like this: [137][0][20][128][0] 

            char[] x = { '[', ']' };

            string[] items = currentItem.Split(x, StringSplitOptions.RemoveEmptyEntries);

            //Convert to this:


            //This List might need to be passed in with the OPCode already set
            List<byte> lSend = new List<byte>();
            //lSend.Add(0); //Blank entry to be replaced later by the correct OpCode

            foreach (string current in items)
            {
                byte iCurrent = Convert.ToByte(current);
                lSend.Add(iCurrent);
            }

            return lSend.ToArray();
        }
    }
}



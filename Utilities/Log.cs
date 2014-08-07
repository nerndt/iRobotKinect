using System;
using System.IO;

namespace Logging
{
    public class LoggingException : System.ApplicationException
    {
        public LoggingException(string message): base(message)
        {
        }
    }

    public static class Log
    {

        #region Properties

            private static bool p_bDebugMode = false; //default ON

            /// <summary>
            /// 
            /// </summary>
            public static bool DebugMode
            {

                get
                {
                    return (Log.p_bDebugMode);
                }
                set
                {
                    Log.p_bDebugMode = value;
                }
            }

            private static string p_sPath = "";

            /// <summary>
            /// This is the path and filename of the log
            /// </summary>
            public static string Path
            {

                get
                {
                    return (Log.p_sPath);
                }
                set
                {
                    Log.p_sPath = value;
                }


            }

            private static string p_sBuffer;
            public static string Buffer
            {
                get
                {
                    string sReturn = Log.p_sBuffer;

                    // p_sBuffer = "";
                    return (sReturn);
                }
                set
                {
                    Log.p_sBuffer = value;
                }
            }

        #endregion

            ///// <summary>
            ///// 
            ///// </summary>
            ///// <param name="sValue"></param>
            ///// <param name="sCallingFunction"></param>
            ///// <returns></returns>
            //public static bool This(string sValue, string sCallingFunction)
            //{
            //    bool bSuccess = false;

            //    if (Log.DebugMode)
            //    {
            //        try
            //        {
            //            if (Log.Path.Length > 0)
            //            {
            //                StreamWriter swLogWriter = new StreamWriter(Log.Path, true);
            //                swLogWriter.WriteLine("Roomba: " + Log.GetTimeStamp(true) + "\t" + sValue + "\t" + sCallingFunction + "\r\n");
            //                swLogWriter.Close();

            //                bSuccess = true;
            //            }
            //            else
            //            {
            //                throw new LoggingException("No Path");
            //            }

            //        }
            //        catch (System.Exception ex)
            //        {
            //            throw new LoggingException(ex.Message);
            //        }
            //    }

            //    return bSuccess;

            //}

        public static void This(string sValue)
        {
            This(sValue, "", true);    
        }
        public static bool This(string sValue, string CallingObject, bool LogPermission)
        {
            bool bSuccess = false;

            if (LogPermission)
            {
                try
                {
                    if (Log.Path.Length > 0)
                    {

                        Write(sValue, CallingObject);

                        //Thread x = new Thread(new ThreadStart(Log.Write()));
                        //x.Start();

                        Log.Buffer = "";

                        bSuccess = true;
                    }
                    else
                    {
                        throw new LoggingException("No Path");
                    }

                }
                catch (System.Exception ex)
                {
                    p_sBuffer += Log.GetTimeStamp(true) + "\t" + CallingObject + "\t" + sValue + "\r\n";
                    //throw new LoggingException(ex.Message);
                }
            }

            return bSuccess;

        }

        private static void Write(string sValue, string CallingObject)
        {
            System.IO.StreamWriter swLogWriter = new System.IO.StreamWriter(Log.Path, true);
            swLogWriter.WriteLine(Log.Buffer + Log.GetTimeStamp(true) + "\t" + CallingObject + "\t" + sValue); // + "\r\n"
            swLogWriter.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bSplit"></param>
        /// <returns></returns>
        public static string GetTimeStamp(bool bSplit)
        {
            char fill = '0';
            char dot = '.';
            char col = ':';
            char sla = '/';

            string sMin = DateTime.Now.Minute.ToString();
            string sSec = DateTime.Now.Second.ToString();
            string sMil = DateTime.Now.Millisecond.ToString();

            string sTimeStamp = DateTime.Now.Year.ToString() +
                                           DateTime.Now.Month.ToString() +
                                           DateTime.Now.Day.ToString() +
                                           DateTime.Now.Hour.ToString() +
                                           sMin +
                                           sSec + 
                                           sMil;

            if (bSplit)
            {
                sTimeStamp = DateTime.Now.Month.ToString() + sla +
                                      DateTime.Now.Day.ToString() + sla +
                                      DateTime.Now.Year.ToString() + " " +
                      DateTime.Now.Hour.ToString() + col +
                      sMin.PadLeft(2, fill) + col +
                      sSec.PadLeft(2, fill) + dot +
                      sMil.PadRight(3, fill);
            }

            return sTimeStamp;

        }
    }
}

//[22:24] x_something_wicked_x: right you put your log function in a class
//[22:24] kevin.gabbert: yup
//[22:25] x_something_wicked_x: then you create a thread using the addressof the log function
//[22:25] x_something_wicked_x: not the name of the log function alone
//[22:25] x_something_wicked_x: I don't know what the AddressOf operator is in c#
//[22:25] kevin.gabbert: right, if the Log function takes parameters it will complain
//[22:25] x_something_wicked_x: there is a way around that in the article
//[22:26] x_something_wicked_x: you wrap your log function in a function
//[22:26] x_something_wicked_x: then pass the address of the wrapper 
//[22:26] x_something_wicked_x: to the thread
//[22:26] kevin.gabbert: so if my log function is: Log(1,2), how do I wrap it?
//[22:27] x_something_wicked_x: public x,yPublic Sub LogWrapper()   Log(x,y)end Sub
//[22:28] kevin.gabbert: alas, that is the problem
//[22:28] kevin.gabbert: public x, & y have to be set by someone
//[22:28] kevin.gabbert: if you have multiple someones tripping all over each other, then you are back at square 1
//[22:29] kevin.gabbert: public x & y might get overwritten by the next cockroach in the motel
//[22:30] x_something_wicked_x: put them as props on your class that contains the log function
//[22:30] x_something_wicked_x: you're going to create a new instance of that class for each thread anyways
//[22:30] kevin.gabbert: which brings us back to what i said above
//[22:30] x_something_wicked_x: logclass.x = 1; logclass.y = 2; logclass.log()
//[22:30] kevin.gabbert: each person that calls the class/function will need to do more than just a function call
//[22:31] kevin.gabbert: lets see.
//[22:31] kevin.gabbert: i could wrap *that*
//[22:31] kevin.gabbert: in the local class.
//[22:31] kevin.gabbert: egad
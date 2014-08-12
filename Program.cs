using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CMDLine;
using ExceptionHandling;
using System.Drawing;

using System.Runtime.Serialization;
using System.Threading;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;

namespace iRobotKinect 
{
    static class Program
    {
        public static Mutex ApplicationMutex;
        public static bool SplashCurrentlyShown = false; // Needed to track if Splash Screen is up
        public const string DefaultMutexName = "iRobotKinectMutex";

        public static List<IntPtr> Menu_Cache = new List<IntPtr>();

        private static RoombaUI p_fUI = new RoombaUI();
        public static RoombaUI UI
        {
            get
            {
                return p_fUI;
            }
            set
            {
                p_fUI = value;
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            UnhandledExceptionManager.AddHandler(); // This allows ability to catch all unhandled exceptions in the program!

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //create parser
            CMDLineParser parser = new CMDLineParser();
            parser.throwInvalidOptionsException = false;

            //add Options
            #region addOptions
            //add default help "-help",..
            parser.AddHelpOption();
            CMDLineParser.Option TouchInputFilenameOpt = parser.AddStringParameter("-l", "Left", false);
            CMDLineParser.Option TouchInputDirectoryOpt = parser.AddStringParameter("-r", "Right", false);
            CMDLineParser.Option TouchOutputFilenameOpt = parser.AddStringParameter("-f", "Forwards", false);
            CMDLineParser.Option TouchOutputReportFileOpt = parser.AddStringParameter("-b", "Backwards", false);
            #endregion

            if (args.Length > 0)
            {
                try
                {
                    parser.Parse(args);
                }
                catch (CMDLineParser.CMDLineParserException e)
                {
                    Console.WriteLine("Error: " + e.Message);
                    parser.HelpMessage();
                }

                //parser.Debug();

                #region Section dealing with Multiple Instances of iRobotKinect
                if (ApplicationMutex != null && MainForm.MainFormMutex == ApplicationMutex) // This is sent to us by another copy of the program! Assume it is the qlp file to load
                {
                    MainForm.CRMainForm.TopMost = true;
                    MessageBox.Show("Args passed to iRobotKinect by another instance are:" + args[0], "iRobotKinect Remote Message"); // NGE07252013
                    if (args != null)
                    {
                        if (args.Count() == 1)
                        {
                            MainForm.CRMainForm.CommandLineDrivenOpenPlate(args[0]);
                        }
                        else if (parser._matchedSwitches.Count == 0 && parser._unmatchedArgs.Count == 2 && System.IO.Path.GetFileName((string)args[0]) == "iRobotKinect.exe") // Assume it is the file to load               
                        {
                            MainForm.CRMainForm.CommandLineDrivenOpenPlate(args[1]);
                        }
                    }
                #endregion Section dealing with Multiple Instances of iRobotKinect

                    return;
                }

                Application.EnableVisualStyles(); // NGE07242014 // Changes default way buttons and controls are shown - commented out makes basic 3d button shading
                Application.SetCompatibleTextRenderingDefault(false);
                // NGE07252013 if (args != null)
                // NGE07252013     MessageBox.Show("A Application.Run(CRMainForm = new MainForm(args, parser))" + string.Join("\t", args));
                // NGE07252013 else
                // NGE07252013     MessageBox.Show("A Application.Run(CRMainForm = new MainForm(args, parser))");

                CRMainForm = new MainForm();
                Program.Menu_Cache.Add(CRMainForm.Handle);
                Application.Run(CRMainForm);
            }
            else
            {
                #region Section dealing with Multiple Instances of iRobotKinect
                if (MainForm.AllowForMultipleInstancesOfiRobotKinect == false)
                {
                    bool MutexOwner = false;

                    // This application wants initial ownership of the "iRobotKinect", if not, then iRobotKinect is already running
                    ApplicationMutex = new Mutex(true, DefaultMutexName, out MutexOwner);

                    int tryNumber = 0;
                    while (MutexOwner == false && tryNumber < 5)
                    {
                        Thread.Sleep(3000); // Sleep for 3 seconds to see if other instance has finished exiting
                        // Try 5 times, waiting 3 seconds between each try
                        ApplicationMutex = new Mutex(true, DefaultMutexName, out MutexOwner);
                        tryNumber++;
                    }

                    if (MutexOwner == false)
                    {
                        MessageBox.Show("iRobotKinect software is already running.", "Error - Unable To Run iRobotKinect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    bool TooManyInstancesRunning = false;
                    string currentMutexName = "", newMutexName = "";
                    if (RuniRobotKinectDueToMutexCondition(true, out TooManyInstancesRunning, ref currentMutexName, ref newMutexName) == false)
                        return;

                    string mutexName = "";
                    bool bRet = IsiRobotKinectAlreadyRunning(ref mutexName);

                    bool MutexOwner = false;
                    if (newMutexName == "" || currentMutexName == mutexName) // The initial instance of iRobotKinect
                    {
                        if (newMutexName == "")
                            MainForm.InitialInstanceMutexName = currentMutexName;
                        ApplicationMutex = new Mutex(true, currentMutexName, out MutexOwner);
                    }
                    else
                        ApplicationMutex = null;
                }
                #endregion Section dealing with Multiple Instances of iRobotKinect

                Application.EnableVisualStyles(); // NGE07242014 // Changes default way buttons and controls are shown - commented out makes basic 3d button shading
                Application.SetCompatibleTextRenderingDefault(false);
                SetMessageFiltering();
              
                SplashCurrentlyShown = false; // NGE12262011 Set to True if showing Splash Screen
                // Show the splash ASAP
                // NGE11202013 SplashScreen.ShowSplash();

                try
                {
                    CRMainForm = new MainForm();
                    Program.Menu_Cache.Add(CRMainForm.Handle);
                    Application.Run(CRMainForm);
                }
                catch (System.IO.FileNotFoundException excep)
                {
                    MessageBox.Show("Missing file is : " + excep.FileName);
                }
                //catch (Exception ex)
                //{
                //    string errorString = ex.Message;
                //    if (MainForm.ShowApplication) MessageBox.Show(errorString);
                //}
            }
        }

        #region Methods dealing with Multiple Instances of iRobotKinect

        public static bool IsiRobotKinectAlreadyRunning(ref string mutexName)
        {
            bool MutexOwner = false;
            int numberOfiRobotKinectInstances = 1;
            string tempMutexName = DefaultMutexName;
            while (MutexOwner == false)
            {
                MainForm.MainFormMutex = new Mutex(true, tempMutexName, out MutexOwner); // See if it already exists but checking if MutexOwner == false
                if (MutexOwner == false) // yes we have one
                {
                    mutexName = tempMutexName;
                    return true;
                }
                else // not yet - keep searching
                {
                    tempMutexName = DefaultMutexName + numberOfiRobotKinectInstances.ToString();
                    numberOfiRobotKinectInstances += 1;
                    if (numberOfiRobotKinectInstances >= 10) // only check for 10 instances of the program!! 10 instances is all that we allow
                    {
                        mutexName = "";
                        return false;
                    }
                }
            }
            return false;
        }

        public static bool RuniRobotKinectDueToMutexCondition(bool showMessageBox, out bool TooManyInstancesRunning, ref string currentMutexName, ref string newMutexName)
        {
            // Only allow the first instance of iRobotKinect to run the instrument
            bool MutexOwner = false;
            TooManyInstancesRunning = false;

            MainForm.MutexName = DefaultMutexName;
            // This application wants initial ownership of the "iRobotKinectMutex", if not, then iRobotKinect is already running
            MainForm.MainFormMutex = new Mutex(true, MainForm.MutexName, out MutexOwner);

            if (MainForm.AllowForMultipleInstancesOfiRobotKinect == false)
            {
                if (MutexOwner == false)
                {
                    currentMutexName = MainForm.MutexName;
                    if (showMessageBox == true)
                        MessageBox.Show("iRobotKinect software is already running.", "Error - Unable To Run iRobotKinect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    newMutexName = currentMutexName = MainForm.MutexName;
                    return false;
                }
            }
            else
            {
                int numberOfiRobotKinectInstances = 1;
                currentMutexName = MainForm.MutexName;
                if (MutexOwner == true)
                    return true;
                string tempMutexName = DefaultMutexName;
                while (MutexOwner == false)
                {
                    MainForm.MainFormMutex = new Mutex(true, tempMutexName, out MutexOwner);
                    if (MutexOwner == false)
                    {
                        newMutexName = tempMutexName;
                        tempMutexName = DefaultMutexName + numberOfiRobotKinectInstances.ToString();
                        numberOfiRobotKinectInstances += 1;
                        if (numberOfiRobotKinectInstances >= 10) // only allow 10 instances of the program!!
                        {
                            if (showMessageBox == true)
                                MessageBox.Show("Only 10 instances of iRobotKinect are allowed to run.", "Error - Unable To Run iRobotKinect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            TooManyInstancesRunning = true;
                            newMutexName = "";
                            MainForm.MainFormMutex = new Mutex(true, MainForm.MutexName, out MutexOwner); // Set Mutex to use first instance
                            return false;
                        }
                    }
                    else
                    {
                        newMutexName = tempMutexName;
                        MainForm.MainFormMutex = new Mutex(true, tempMutexName, out MutexOwner); // Set Mutex to use first instance
                        break;
                    }
                }
            }
            return true;
        }

        #endregion Methods dealing with Multiple Instances of iRobotKinect

        static MainForm CRMainForm;

        public static void SetMessageFiltering()
        {
            Application.AddMessageFilter(new MouseMessageFilter());
            MouseMessageFilter.MouseMove += new MouseEventHandler(OnGlobalMouseMove);
            MouseMessageFilter.MouseUp += new MouseEventHandler(OnGlobalMouseUp);
            MouseMessageFilter.MouseDown += new MouseEventHandler(OnGlobalMouseDown);
        }

        static void OnGlobalMouseMove(object sender, MouseEventArgs e) 
        {
            if (MainForm.CRMainForm != null && MainForm.CRMainForm.ActiveControl != null)
            {
                if (MainForm.CRMainForm.ActiveControl.GetType() == MainForm.CRMainForm.GetType()) // Not working properly!!  || MainForm.CRMainForm.ActiveControl.GetType() == MainForm.CRMainForm.splitContainerMain1.GetType())
                {
                    MainForm.CRMainForm.MainForm_MouseMove(sender, e);
                }
            }
        }

        static void OnGlobalMouseUp(object sender, MouseEventArgs e)
        {
            if (MainForm.CRMainForm != null && MainForm.CRMainForm.ActiveControl != null)
            {
                if (MainForm.CRMainForm.ActiveControl.GetType() == MainForm.CRMainForm.GetType()) // Not working properly!!  || MainForm.CRMainForm.ActiveControl.GetType() == MainForm.CRMainForm.splitContainerMain1.GetType())
                {
                    MainForm.CRMainForm.MainForm_MouseUp(sender, e);
                }
            }
        }

        static void OnGlobalMouseDown(object sender, MouseEventArgs e)
        {
            if (MainForm.CRMainForm != null && MainForm.CRMainForm.ActiveControl != null)
            {
                if (MainForm.CRMainForm.ActiveControl.GetType() == MainForm.CRMainForm.GetType()) // Not working properly!! || MainForm.CRMainForm.ActiveControl.GetType() == MainForm.CRMainForm.splitContainerMain1.GetType())
                {
                    MainForm.CRMainForm.MainForm_MouseDown(sender, e);
                }
            }
        }

        public static Control FindFocusedControl(Control control)
        {
            var container = control as ContainerControl;
            while (container != null)
            {
                control = container.ActiveControl;
                container = control as ContainerControl;
            }
            return control;
        }

    }

    class MouseMessageFilter : IMessageFilter
    {
        public static event MouseEventHandler MouseMove = delegate { };
        public static event MouseEventHandler MouseUp = delegate { };
        public static event MouseEventHandler MouseDown = delegate { };
        const int WM_MOUSEMOVE = 0x0200;
        const int WM_LBUTTONDOWN = 0x0201;
        const int WM_LBUTTONUP = 0x0202;
        const int WM_LBUTTONDBLCLK = 0x0203;
        const int WM_RBUTTONDOWN = 0x0204;
        const int WM_RBUTTONUP = 0x0205;
        const int WM_RBUTTONDBLCLK = 0x0206;
        const int WM_MBUTTONDOWN = 0x0207;
        const int WM_MBUTTONUP = 0x0208;
        const int WM_MBUTTONDBLCLK = 0x0209;
        const int WM_MOUSEWHEEL = 0x020A;
        const int WM_XBUTTONDOWN = 0x020B;
        const int WM_XBUTTONUP = 0x020C;
        const int WM_XBUTTONDBLCLK = 0x020D;
        const int WM_MOUSEHWHEEL = 0x020E;

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == WM_MOUSEMOVE)
            {
                Point mousePosition = Control.MousePosition;
                MouseMove(null, new MouseEventArgs(
                    MouseButtons.None, 0, mousePosition.X, mousePosition.Y, 0));
            }
            else if (m.Msg == WM_LBUTTONUP)
            {
                Point mousePosition = Control.MousePosition;
                MouseUp(null, new MouseEventArgs(
                    MouseButtons.None, 0, mousePosition.X, mousePosition.Y, 0));
            }
            else if (m.Msg == WM_LBUTTONDOWN)
            {
                Point mousePosition = Control.MousePosition;
                MouseDown(null, new MouseEventArgs(
                    MouseButtons.None, 0, mousePosition.X, mousePosition.Y, 0));
            }
            else if (m.Msg == WM_RBUTTONUP)
            {
                Point mousePosition = Control.MousePosition;
                MouseUp(null, new MouseEventArgs(
                    MouseButtons.None, 0, mousePosition.X, mousePosition.Y, 0));
            }
            else if (m.Msg == WM_RBUTTONDOWN)
            {
                Point mousePosition = Control.MousePosition;
                MouseDown(null, new MouseEventArgs(
                    MouseButtons.None, 0, mousePosition.X, mousePosition.Y, 0));
            }
            return false;
        }
    }

    /*  
     * Found at http://stackoverflow.com/questions/2063974/how-do-i-capture-the-mouse-mouse-move-event-in-my-winform-application
     * The class MouseMessageFilter and the method OnGlobalMouseMove above capture the mouse move event in the main form. 
     * I am ensured that I always have the mouse position even with mouse moves above other controls.
     */

}
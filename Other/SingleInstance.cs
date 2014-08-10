using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Threading;

using System.Windows.Forms;

namespace iRobotKinect
{
    // Code below allows one instance of iRobotKinect to send arguments to an already running instance of iRobotKinect
    public class SingleInstance
    {
        public Mutex TheMutex;
        private IChannel IpcChannel;
        public bool First;

        public SingleInstance(MainForm form, string[] args, string currentMutexName)
        {
            try
            {
                string name = currentMutexName;

                if (Program.ApplicationMutex != null)
                {
                    MainForm.MainFormMutex = TheMutex = Program.ApplicationMutex;
                    First = true;
                }
                else
                {
                    MainForm.MainFormMutex = TheMutex = new Mutex(true, name, out First);
                }

                string objectName = "iRobotKinectSingleInstanceProxy";
                string objectUri = "ipc://" + name + "/" + objectName;

                // NGE07252013 string result = "";
                // NGE07252013 if (args != null)
                // NGE07252013 {
                // NGE07252013     if (First)
                // NGE07252013         result += "First\t" + string.Join("\t", args);
                // NGE07252013     else
                // NGE07252013         result += "Second\t" + string.Join("\t", args);
                // NGE07252013 }

                if (First)
                {
                    // NGE07252013 MessageBox.Show(result);
                    IpcChannel = new IpcServerChannel(name);
                    ChannelServices.RegisterChannel(IpcChannel, false);
                    RemotingConfiguration.RegisterWellKnownServiceType(typeof(IpcObject), objectName, WellKnownObjectMode.Singleton);

                    IpcObject obj = new IpcObject(new NewInstanceHandler(form.SecondInstanceStarted));

                    RemotingServices.Marshal(obj, objectName);
                }
                else
                {
                    // NGE07252013 MessageBox.Show(result);
                    IpcChannel = new IpcClientChannel();
                    ChannelServices.RegisterChannel(IpcChannel, false);

                    IpcObject obj = Activator.GetObject(typeof(IpcObject), objectUri) as IpcObject;

                    obj.SignalNewInstance(args);
                }
            }
            catch (Exception ex)
            {
                string exMessage = ex.Message;
            }
        }
    }

    public delegate void NewInstanceHandler(string[] args);

    public class IpcObject : MarshalByRefObject
    {
        event NewInstanceHandler NewInstance;

        public IpcObject(NewInstanceHandler handler)
        {
            NewInstance += handler;
        }

        public void SignalNewInstance(string[] args)
        {
            NewInstance(args);
        }

        // Make sure the object exists "forever"
        public override object InitializeLifetimeService()
        {
            return null;
        }
    }

    // And in the form load function do

    //    public partial class MainForm : Form
    //{
    //    SingleInstance Instance;

    //    public MainForm(string[] args)
    //    {
    //        InitializeComponent();

    //        Instance = new SingleInstance(this, args);

    //        if (!Instance.First)
    //            return;
    //    }

    //    public void SecondInstanceStarted(string[] args)
    //    {
    //        if (InvokeRequired)
    //        {
    //            BeginInvoke((MethodInvoker)delegate() { SecondInstanceStarted(args); });
    //            return;
    //        }

    //        // add code here to safely handle arguments given to 2nd instance
    //    }
    //}

}

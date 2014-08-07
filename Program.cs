using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace iRobotKinect
{
    static class Program
    {
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
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            frmStart x = new frmStart();
            Program.Menu_Cache.Add(x.Handle);
            Application.Run(x);
        }
    }
}
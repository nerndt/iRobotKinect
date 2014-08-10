using System;
using System.Windows.Forms;
using System.Threading;

namespace iRobotKinect
{
    public class GuiHelper
    {
        /// <summary>
        /// Efficient Wait function for GUI elements that need to process events (much better than Thread.Sleep)
        /// </summary>
        /// <param name="millisecondsTimeout"></param>
        public static void Wait(int millisecondsTimeout)
        {
            // Minimum Wait Time
            DateTime minTime = DateTime.Now.AddMilliseconds(millisecondsTimeout);

            // Execute at least once
            // Wait for the minimum time while processing events and yielding the thread
            do
            {
                // Process Events (Note: This is only good for WinForms projects)
                Application.DoEvents();

                // Yield Thread
                Thread.Yield();
            } while (DateTime.Now < minTime);
        }

    }
}

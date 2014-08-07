using System;
using System.Collections.Generic;
using System.Text;

namespace iRobotKinect
{
    [Serializable]
    public class Statistics
    {

        //This might need to go into a statistics class
        private TimeSpan p_iConnectedTime;
        public TimeSpan ConnectedTime
        {
            get
            {
                return p_iConnectedTime;
            }
            set
            {
                p_iConnectedTime = value;
            }
        }

    }
}

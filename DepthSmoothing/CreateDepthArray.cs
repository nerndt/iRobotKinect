using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Drawing.Imaging;
using Microsoft.Kinect;

namespace iRobotKinect
{
    public partial class KinectForm : Form
    {
        private short[] CreateDepthArray(Bitmap image)
        {
            // When creating a depth array, it will have half the number of indexes than the original depth image
            // This is because the depth image uses two bytes to represent depth.  These values must then be 
            // transformed to a single value per iRobotKinect of the final image that represents depth
            // for purposes of smoothing prior to rendering.

            short[] returnArray = new short[image.Width * image.Height];
            // NGE05212014 For some reason this Method does NOT work properly!!!!! byte[] depthFrame = BitmapManipulator.ConvertBitmapToByteArray(image);
            byte[] depthFrame = BitmapManipulator.BitmapToByteArray(image);
            // byte[] depthFrame = image.Bits;

            Parallel.For(0, 240, depthImageRowIndex =>
            {
                for (int depthImageColumnIndex = 0; depthImageColumnIndex < 640; depthImageColumnIndex += 2)
                {
                    var depthIndex = depthImageColumnIndex + (depthImageRowIndex * 640);
                    var index = depthIndex / 2;

                    returnArray[index] = CalculateDistanceFromDepth(depthFrame[depthIndex], depthFrame[depthIndex + 1]);
                }
            });

            return returnArray;
        }
    }
}

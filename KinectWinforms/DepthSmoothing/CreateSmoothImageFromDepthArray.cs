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

namespace KinectWinforms
{
    public partial class MainWindow : Form
    {
        private Bitmap CreateSmoothImageFromDepthArray(Bitmap image)
        {
            int width = image.Width;
            int height = image.Height;

            // We first want to create a simple array where each index represents a single pixel of depth information.
            // This will make it easier to work with the data to filter and average it for smoothing.
            short[] depthArray = CreateDepthArray(image);

            // Users can decide from the UI if this feature is applied
            if (useFiltering)
                depthArray = CreateFilteredDepthArray(depthArray, width, height);

            // Users can decide from the UI if this feature is applied
            if (useAverage)
                depthArray = CreateAverageDepthArray(depthArray);

            // After we have processed the data, we can transform it into color channels for final rendering.
            byte[] colorBytes = CreateColorBytesFromDepthArray(depthArray, width, height);
            Bitmap bitmapFrame = ArrayToBitmap(colorBytes, width, height, PixelFormat.Format32bppRgb);
            return bitmapFrame;
        }

        private byte[] CreateColorBytesFromDepthArray(short[] depthArray, int width, int height)
        {
            // We multiply the product of width and height by 4 because each byte
            // will represent a different color channel per pixel in the final iamge.
            byte[] colorFrame = new byte[width * height * 4];

            // Process each row in parallel
            Parallel.For(0, 240, depthArrayRowIndex =>
            {
                // Process each pixel in the row
                for (int depthArrayColumnIndex = 0; depthArrayColumnIndex < 320; depthArrayColumnIndex++)
                {
                    var distanceIndex = depthArrayColumnIndex + (depthArrayRowIndex * 320);

                    // Because the colorFrame we are creating has four times as many bytes representing
                    // a pixel in the final image, we set the index to for times of the depth index.
                    var index = distanceIndex * 4;

                    // Map the distance to an intesity that can be represented in RGB
                    var intensity = CalculateIntensityFromDistance(depthArray[distanceIndex]);

                    // Apply the intensity to the color channels
                    colorFrame[index + BlueIndex] = intensity;
                    colorFrame[index + GreenIndex] = intensity;
                    colorFrame[index + RedIndex] = intensity;
                }
                
            });

            return colorFrame;
        }
    }
}

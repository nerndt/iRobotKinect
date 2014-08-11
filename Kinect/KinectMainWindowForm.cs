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
using System.IO;
using Emgu.CV;
using Emgu.CV.Structure;

using System.Windows;
using System.Windows.Media.Media3D;
using System.Windows.Media.Imaging;

//using iRobotKinect;

//Manuell hinzugefügt
using Microsoft.Kinect;
//using Microsoft.Kinect.Toolkit;
//using Microsoft.Kinect.Toolkit.Fusion;

using System.Drawing.Imaging;
using System.Runtime.InteropServices;
//using System.Windows;
//using System.Windows.Media.Media3D;

//using OpenTK;
//using OpenTK.Platform;

namespace iRobotKinect
{
    public partial class KinectForm : Form
    {
        /// Active Kinect sensor
        private KinectSensor sensor;
        private short fpsEnd = 1;
        private MyWrite MyFile;
        Bitmap tempColorFrame;
        bool windowClosing = false;
        int initFrames = 1;

        int MinDepth; // Min Image Depth to collect
        int MaxDepth; // Max Image Depth to collect

        #region Private Fields

        // Pattern finding 
        int objectsFound = 0; // number of blobs in kinect field of view

        // color divisors for tinting depth pixels
        private static readonly int[] IntensityShiftByPlayerR = { 1, 2, 0, 2, 0, 0, 2, 0 };
        private static readonly int[] IntensityShiftByPlayerG = { 1, 2, 2, 0, 2, 0, 0, 1 };
        private static readonly int[] IntensityShiftByPlayerB = { 1, 0, 2, 2, 0, 2, 0, 2 };

        //private byte[] depthFrame32 = new byte[640 * 480 * 4];

        // Fields used to adjust the filtering feature
        private bool useFiltering = true; //checkBoxDepthUseFiltering.Checked;
        // Will specify how many non-zero pixels within a 1 pixel band
        // around the origin there should be before a filter is applied
        private int innerBandThreshold;
        // Will specify how many non-zero pixels within a 2 pixel band
        // around the origin there should be before a filter is applied
        private int outerBandThreshold;

        // Fields used to adjust the averaging feature
        private bool useAverage = true; //checkBoxDepthUseAverage.Checked;
        // Will specify how many frames to hold in the Queue for averaging
        private int averageFrameCount;
        // The actual Queue that will hold all of the frames to be averaged
        private Queue<short[]> averageQueue = new Queue<short[]>();

        // Fields used for FPS calculations
        private int totalFrames;
        private int lastFrames;
        private DateTime lastTime;

        // Constants used to address the individual color pixels for generating images
        private const int RedIndex = 2;
        private const int GreenIndex = 1;
        private const int BlueIndex = 0;

        // Constants used to map value ranges for distance to pixel intensity conversions
        private const int MaxDepthDistance = 4000;
        private const int MinDepthDistance = 850;
        private const int MaxDepthDistanceOffset = 3150;

        private int[,] Heatmap = new int[64, 4]; // Heatmap

        // We'll use a lookup table so that we don't have to repeat the math over and over
        float[] DepthLookUp = new float[2048];

        Bitmap depthBmp = null;

        private SpeechRecognizer mySpeechRecognizer;

        private GestureController _gestureController;

        #endregion Private Fields

        public KinectForm()
        {
            InitializeComponent();
            if (!this.DesignMode)
            {
                Init();

                Cursor.Current = Cursors.WaitCursor;
                start();
            }
        }

        private void Init()
        {
            ColorMapExt cmap = new ColorMapExt();
            Heatmap = cmap.Heatmap();

            useFiltering = checkBoxDepthUseFiltering.Checked;
            useAverage = checkBoxDepthUseAverage.Checked;
            innerBandThreshold = (int)trackBarDepthInnerBand.Value;
            outerBandThreshold = (int)trackBarDepthOuterBand.Value;
            averageFrameCount = (int)trackBarDepthFramesToAverage.Value;

            // Sets the camera elevation angle
            //this.sensor.ElevationAngle = Convert.ToInt32(this.textboxElevationAngle.Text);

            // Lookup table for all possible depth values (0 - 2047)
            for (int i = 0; i < DepthLookUp.Length; i++)
            {
                DepthLookUp[i] = rawDepthToMeters(i);
            }
        }

        private void button_start_Click(object sender, EventArgs e)
        {
            start();
        }

        private void start()
        {
            if (!this.DesignMode)
            {

                if (sensor == null)
                {
                    // Look through all sensors and start the first connected one.
                    // This requires that a Kinect is connected at the time of app startup.
                    // To make your app robust against plug/unplug, 
                    // it is recommended to use KinectSensorChooser provided in Microsoft.Kinect.Toolkit (See components in Toolkit Browser).
                    foreach (var potentialSensor in KinectSensor.KinectSensors)
                    {
                        this.textBox_sensorStatus.Text = "Searching...";
                        if (potentialSensor.Status == KinectStatus.Connected)
                        {
                            this.sensor = potentialSensor;
                            break;
                        }
                    }

                    if (null != this.sensor)
                    {
                        TransformSmoothParameters smoothingParam = new TransformSmoothParameters();

                        if (radioButton_smoothDefault.Checked)
                        {
                            // Some smoothing with little latency (defaults).
                            // Only filters out small jitters.
                            // Good for gesture recognition in games.
                            smoothingParam = new TransformSmoothParameters();
                            {
                                smoothingParam.Smoothing = 0.5f;
                                smoothingParam.Correction = 0.5f;
                                smoothingParam.Prediction = 0.5f;
                                smoothingParam.JitterRadius = 0.05f;
                                smoothingParam.MaxDeviationRadius = 0.04f;
                            };
                        }
                        else if (radioButton_smoothModerate.Checked)
                        {

                            // Smoothed with some latency.
                            // Filters out medium jitters.
                            // Good for a menu system that needs to be smooth but
                            // doesn't need the reduced latency as much as gesture recognition does.
                            smoothingParam = new TransformSmoothParameters();
                            {
                                smoothingParam.Smoothing = 0.5f;
                                smoothingParam.Correction = 0.1f;
                                smoothingParam.Prediction = 0.5f;
                                smoothingParam.JitterRadius = 0.1f;
                                smoothingParam.MaxDeviationRadius = 0.1f;
                            };
                        }
                        else if (radioButton_smoothIntense.Checked)
                        {
                            // Very smooth, but with a lot of latency.
                            // Filters out large jitters.
                            // Good for situations where smooth data is absolutely required
                            // and latency is not an issue.
                            smoothingParam = new TransformSmoothParameters();
                            {
                                smoothingParam.Smoothing = 0.7f;
                                smoothingParam.Correction = 0.3f;
                                smoothingParam.Prediction = 1.0f;
                                smoothingParam.JitterRadius = 1.0f;
                                smoothingParam.MaxDeviationRadius = 1.0f;
                            };
                        }
                        groupBox_smooth.Enabled = false;

                        // Turn on the stream to receive frames
                        this.sensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
                        this.sensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
                        this.sensor.SkeletonStream.Enable(smoothingParam);
                        this.sensor.SkeletonStream.TrackingMode = SkeletonTrackingMode.Default; // SkeletonTrackingMode.Seated; // SkeletonTrackingMode.Default standing
                        this.sensor.SkeletonStream.EnableTrackingInNearRange = true;

                        // event handler to be called whenever there is new frame data
                        this.sensor.AllFramesReady += sensor_allFramesReady;

                        // Start the sensor!
                        try
                        {
                            this.sensor.Start();
                            this.textBox_sensorStatus.Text = "Stream started";

                            // Start speech recognizer after KinectSensor started successfully.
                            this.mySpeechRecognizer = SpeechRecognizer.Create();

                            if (null != this.mySpeechRecognizer)
                            {
                                this.mySpeechRecognizer.SaidSomething += this.RecognizerSaidSomething;
                                this.mySpeechRecognizer.Start(sensor.AudioSource);
                            }

                            _gestureController = new GestureController(GestureType.All);
                            _gestureController.GestureRecognized += GestureController_GestureRecognized;


                        }
                        catch (Exception)
                        {
                            this.sensor = null;
                            this.textBox_sensorStatus.Text = "Stream not started";
                        }
                    }
                    else
                    {
                        this.textBox_sensorStatus.Text = "Kinect not found";
                    }
                }
            }
        }

        private void button_stop_Click(object sender, EventArgs e)
        {
            if (MyFile != null)
            {
                this.textBox_sensorStatus.Text = "First Recording stopped!";
            }
            else
            {
                if (sensor != null)
                {
                    StopKinect(sensor);
                    this.textBox_sensorStatus.Text = "Stream ended";
                    this.pictureBox_skeleton.Image = null;
                    groupBox_smooth.Enabled = true;
                }
            }
        }

        #region this code  is from CoordinateMappingBasics in Kinect Developer Toolkit v1.8
        /// <summary>
        /// Format we will use for the depth stream
        /// </summary>
        private const DepthImageFormat DepthFormat = DepthImageFormat.Resolution320x240Fps30;

        /// <summary>
        /// Format we will use for the color stream
        /// </summary>
        private const ColorImageFormat ColorFormat = ColorImageFormat.RgbResolution640x480Fps30;
        
        /// <summary>
        /// Bitmap that will hold color information
        /// </summary>
        private Bitmap colorBitmap;  // private WriteableBitmap colorBitmap;

        /// <summary>
        /// Bitmap that will hold opacity mask information
        /// </summary>
        private Bitmap playerOpacityMaskImage = null; // private WriteableBitmap playerOpacityMaskImage = null;

        /// <summary>
        /// Intermediate storage for the depth data received from the sensor
        /// </summary>
        private DepthImagePixel[] depthPixels;

        /// <summary>
        /// Intermediate storage for the color data received from the camera
        /// </summary>
        private byte[] colorPixels;

        /// <summary>
        /// Intermediate storage for the player opacity mask
        /// </summary>
        private int[] playerPixelData;

        /// <summary>
        /// Intermediate storage for the depth to color mapping
        /// </summary>
        private ColorImagePoint[] colorCoordinates;

        /// <summary>
        /// Inverse scaling factor between color and depth
        /// </summary>
        private int colorToDepthDivisor;

        /// <summary>
        /// Width of the depth image
        /// </summary>
        private int depthWidth;

        /// <summary>
        /// Height of the depth image
        /// </summary>
        private int depthHeight;

        /// <summary>
        /// Indicates opaque in an opacity mask
        /// </summary>
        private int opaquePixelValue = -1;

        public static Bitmap BytesToBitmap(byte[] byteArray)
        {
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                Bitmap img = (Bitmap)Image.FromStream(ms);
                return img;
            }
        }

        #region Testing
        ///// <summary>
        ///// Event handler for Kinect sensor's DepthFrameReady event
        ///// </summary>
        ///// <param name="sender">object sending the event</param>
        ///// <param name="e">event arguments</param>
        //private void SensorAllFramesReady(object sender, AllFramesReadyEventArgs e)
        //{
        //    // in the middle of shutting down, so nothing to do
        //    if (null == this.sensor)
        //    {
        //        return;
        //    }

        //    bool depthReceived = false;
        //    bool colorReceived = false;

        //    using (DepthImageFrame depthFrame = e.OpenDepthImageFrame())
        //    {
        //        if (null != depthFrame)
        //        {
        //            // Copy the pixel data from the image to a temporary array
        //            depthFrame.CopyDepthImagePixelDataTo(this.depthPixels);

        //            depthReceived = true;
        //        }
        //    }

        //    using (ColorImageFrame colorFrame = e.OpenColorImageFrame())
        //    {
        //        if (null != colorFrame)
        //        {
        //            // Copy the pixel data from the image to a temporary array
        //            colorFrame.CopyPixelDataTo(this.colorPixels);

        //            colorReceived = true;
        //        }
        //    }

        //    // do our processing outside of the using block
        //    // so that we return resources to the kinect as soon as possible
        //    if (true == depthReceived)
        //    {
        //        this.sensor.CoordinateMapper.MapDepthFrameToColorFrame(
        //            DepthFormat,
        //            this.depthPixels,
        //            ColorFormat,
        //            this.colorCoordinates);

        //        Array.Clear(this.playerPixelData, 0, this.playerPixelData.Length);

        //        #region  loop over each row and column of the depth
        //        for (int y = 0; y < this.depthHeight; ++y)
        //        {
        //            for (int x = 0; x < this.depthWidth; ++x)
        //            {
        //                // calculate index into depth array
        //                int depthIndex = x + (y * this.depthWidth);

        //                DepthImagePixel depthPixel = this.depthPixels[depthIndex];

        //                int player = depthPixel.PlayerIndex;

        //                // if we're tracking a player for the current pixel, sets it opacity to full
        //                if (player > 0)
        //                {
        //                    // retrieve the depth to color mapping for the current depth pixel
        //                    ColorImagePoint colorImagePoint = this.colorCoordinates[depthIndex];

        //                    // scale color coordinates to depth resolution
        //                    int colorInDepthX = colorImagePoint.X / this.colorToDepthDivisor;
        //                    int colorInDepthY = colorImagePoint.Y / this.colorToDepthDivisor;

        //                    // make sure the depth pixel maps to a valid point in color space
        //                    // check y > 0 and y < depthHeight to make sure we don't write outside of the array
        //                    // check x > 0 instead of >= 0 since to fill gaps we set opaque current pixel plus the one to the left
        //                    // because of how the sensor works it is more correct to do it this way than to set to the right
        //                    if (colorInDepthX > 0 && colorInDepthX < this.depthWidth && colorInDepthY >= 0 && colorInDepthY < this.depthHeight)
        //                    {
        //                        // calculate index into the player mask pixel array
        //                        int playerPixelIndex = colorInDepthX + (colorInDepthY * this.depthWidth);

        //                        // set opaque
        //                        this.playerPixelData[playerPixelIndex] = opaquePixelValue;

        //                        // compensate for depth/color not corresponding exactly by setting the pixel 
        //                        // to the left to opaque as well
        //                        this.playerPixelData[playerPixelIndex - 1] = opaquePixelValue;
        //                    }
        //                }
        //            }
        //        }
        //        #endregion  loop over each row and column of the depth
        //    }

        //    // do our processing outside of the using block
        //    // so that we return resources to the kinect as soon as possible
        //    if (true == colorReceived)
        //    {
        //        this.colorBitmap = BytesToBitmap(this.colorPixels);
        //        //this.colorBitmap.
        //        //// Write the pixel data into our bitmap
        //        //this.colorBitmap.WritePixels(
        //        //    new Int32Rect(0, 0, this.colorBitmap.PixelWidth, this.colorBitmap.PixelHeight),
        //        //    this.colorPixels,
        //        //    this.colorBitmap.PixelWidth * sizeof(int),
        //        //    0);

        //        if (this.playerOpacityMaskImage == null)
        //        {
        //           this.playerOpacityMaskImage = new Bitmap(
        //                this.depthWidth,
        //                this.depthHeight,
        //                PixelFormat.Format32bppArgb);

        //            //this.playerOpacityMaskImage = new WriteableBitmap(
        //            //    this.depthWidth,
        //            //    this.depthHeight,
        //            //    96,
        //            //    96,
        //            //    PixelFormats.Bgra32,
        //            //    null);

        //            MaskedColor.OpacityMask = new ImageBrush { ImageSource = this.playerOpacityMaskImage };
        //        }

        //        // Convert playerPixelData to bytes
        //        for (int y = 0; y < this.depthHeight; ++y)
        //        {
        //            for (int x = 0; x < this.depthWidth; ++x)
        //            {
        //            }
        //        }

        //        this.playerOpacityMaskImage = BytesToBitmap(this.playerPixelData);

        //        this.playerOpacityMaskImage.WritePixels(
        //            new Int32Rect(0, 0, this.depthWidth, this.depthHeight),
        //            this.playerPixelData,
        //            this.depthWidth * ((this.playerOpacityMaskImage.Format.BitsPerPixel + 7) / 8),
        //            0);
        //    }
        //}
        #endregion Testing

        #endregion this callback is from CoordinateMappingBasics in Kinect Developer Toolkit v1.8

        void sensor_allFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }
            
            if (windowClosing)
            {
                return;
            }

            int value;
            if (int.TryParse(this.textBox_init.Text, out value))
            {
                initFrames = value;
            }

            #region set FramesPerSecond
            if (fpsEnd == 1 && this.dropDown_fps.Text != "")
            {
                //FPS Suggestion. Bei niedrigen Frameraten werden empfangene Frames übersprungen (nicht angezeigt)
                Int16 fps = Convert.ToInt16(this.dropDown_fps.Text);
                switch (fps)
                {
                    case 30:
                        fpsEnd = 1;
                        break;
                    case 15:
                        fpsEnd = 2;
                        break;
                    case 10:
                        fpsEnd = 3;
                        break;
                    case 5:
                        fpsEnd = 6;
                        break;
                    case 4:
                        fpsEnd = 8;
                        break;
                    case 3:
                        fpsEnd = 10;
                        break;
                    case 2:
                        fpsEnd = 15;
                        break;
                    case 1:
                        fpsEnd = 30;
                        break;
                }
                #endregion set FramesPerSecond

                tempColorFrame = null;
                #region ColorImage
                using (ColorImageFrame colorFrame = e.OpenColorImageFrame())
                {
                    if (colorFrame != null)
                    {
                        // Kinect Color Frame to Bitmap
                        tempColorFrame = (Bitmap)ColorImageFrameToBitmap(colorFrame); // BitmapManipulator.MirrorXBitmap((Bitmap)ColorImageFrameToBitmap(colorFrame));
                        
                        // Too slow if not scaled down by 10x
                        //this.pictureBox_colorPic.BackgroundImage = BitmapManipulator.ConvertImageToHeatmapBitMap(BitmapManipulator.ScaleBitmap(tempColorFrame, 0.1, 0.1, System.Drawing.Drawing2D.InterpolationMode.Default), Heatmap, false);
                    }
                }
                #endregion ColorImage

                #region Depth
                using (DepthImageFrame depthFrame = e.OpenDepthImageFrame())
                {
                    if (depthFrame != null)
                    {
                        // Kinect Depth Frame to Bitmap
                        Bitmap tempDepthFrame = DepthImageFrameToBitmap(depthFrame);
                        this.pictureBox_depthPic.BackgroundImage = tempDepthFrame; // this.pictureBox_depthPic.Image = new Bitmap(tempDepthFrame, this.pictureBox_depthPic.Width, this.pictureBox_depthPic.Height);

                        objectsFound = 0;

                        depthBmp = ImageHelpers.SliceDepthImage(depthFrame, (int)trackBarDepthMinDistance.Value, (int)trackBarDepthMaxDistance.Value);

                        try
                        {
                            Image<Bgr, Byte> openCVImg = new Image<Bgr, byte>(depthBmp);
                            Image<Gray, byte> gray_image = openCVImg.Convert<Gray, byte>();

                            using (MemStorage stor = new MemStorage())
                            {
                                //Find contours with no holes try CV_RETR_EXTERNAL to find holes
                                Contour<System.Drawing.Point> contours = gray_image.FindContours(
                                 Emgu.CV.CvEnum.CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_SIMPLE,
                                 Emgu.CV.CvEnum.RETR_TYPE.CV_RETR_EXTERNAL,
                                 stor);

                                for (int i = 0; contours != null; contours = contours.HNext)
                                {
                                    i++;

                                    if ((contours.Area > Math.Pow(trackBarObjectMinSize.Value, 2)) && (contours.Area < Math.Pow(trackBarObjectMaxSize.Value, 2)))
                                    {
                                        MCvBox2D box = contours.GetMinAreaRect();
                                        openCVImg.Draw(box, new Bgr(System.Drawing.Color.Red), 2);
                                        objectsFound++;
                                    }
                                }
                            }

                            pictureBox_depthPicSmoothed.BackgroundImage = openCVImg.Bitmap;
                            richTextBoxObjectFound.Text = objectsFound.ToString();
                            // The Smoothed Image will apply both a filter and a weighted moving average over the
                            // depth data from the Kinect (depending on UI selections)
                            //pictureBox_depthPicSmoothed.BackgroundImage = CreateSmoothImageFromDepthArray(tempDepthFrame); // pictureBox_depthPicSmoothed.Image = CreateSmoothImageFromDepthArray(tempDepthFrame);

                            // Create a pseudo color of the depth map
                            // Too slow for 30 frames per second if not scaled down by 10x
                            //this.pictureBox_depthPicSmoothed.BackgroundImage = BitmapManipulator.ConvertImageToHeatmapBitMap(BitmapManipulator.ScaleBitmap(tempDepthFrame, 0.1, 0.1, System.Drawing.Drawing2D.InterpolationMode.Default), Heatmap, false);

                            // Create a byte arrya of the depth data
                            //ConvertDepthFrameData(sender, depthFrame);

                            DepthImagePixel[] depth = depthFrame.GetRawPixelData();
                            pictureBox_colorPic.BackgroundImage = iRobotKinect.DepthExtensions.ToBitmap(depthFrame, PixelFormat.Format32bppRgb, iRobotKinect.DepthImageMode.Colors);
                        }
                        catch (Exception ex)
                        {
                            string exMessage = ex.Message;
                        }
                    }
                }
                #endregion Depth

                #region Skeleton
                bool isSkeletonShown = false;
                using (SkeletonFrame skelFrame = e.OpenSkeletonFrame())
                {
                    if (skelFrame != null)
                    {
                        Image tempSkeletonFrame = null; // new Bitmap(this.pictureBox_skeleton.Width, this.pictureBox_skeleton.Height);
                        // make the background black if there is no image
                        this.pictureBox_skeleton.BackColor = Color.Black;
                        this.pictureBox_skeleton.BackgroundImage = null;

                        if (checkBox_colorCam.Checked && tempColorFrame != null)
                        {
                            tempSkeletonFrame = new Bitmap(tempColorFrame);
                        }
                        else
                        {
                            tempSkeletonFrame = new Bitmap(this.pictureBox_skeleton.Width, this.pictureBox_skeleton.Height);
                        }

                        if (this.checkBoxShowSkeleton.Checked)
                        {
                            Skeleton[] skeletons = new Skeleton[skelFrame.SkeletonArrayLength];
                            skelFrame.CopySkeletonDataTo(skeletons);
                            if (skeletons.Length != 0)
                            {
                                foreach (Skeleton skel in skeletons)
                                {
                                    if (skel.TrackingState == SkeletonTrackingState.Tracked)
                                    {
                                        DrawSkeletons(tempSkeletonFrame, skel);

                                        if (skel != null)
                                        {
                                            // Update skeleton gestures.
                                            _gestureController.Update(skel);

                                            double height = iRobotKinect.SkeletonExtensions.Height(skel);
                                        }

                                        if (MyFile != null)
                                        {
                                            if (MyFile.isRecording == true && MyFile.isInitializing == true)
                                            {
                                                MyFile.Entry(skel);

                                                if (MyFile.intializingCounter > initFrames)
                                                {
                                                    MyFile.startWritingEntry();
                                                }

                                            }

                                            if (MyFile.isRecording == true && MyFile.isInitializing == false)
                                            {
                                                MyFile.Motion(skel);
                                                this.textBox_sensorStatus.Text = "Record";
                                                this.textBox_sensorStatus.BackColor = Color.Green;
                                            }
                                        }
                                    }
                                }
                            }
                            tempSkeletonFrame = BitmapManipulator.MirrorXBitmap((Bitmap)tempSkeletonFrame); // Flip the bitmap

                            this.pictureBox_skeleton.BackgroundImage = tempSkeletonFrame; // this.pictureBox_skeleton.Image = tempSkeletonFrame;
                            //this.pictureBox_skeleton.Image = new Bitmap(tempSkeletonFrame, this.pictureBox_skeleton.Width, this.pictureBox_skeleton.Height);
                            isSkeletonShown = true;
                        }
                    }
                }
                #endregion Skeleton
                if (tempColorFrame != null && isSkeletonShown == false)
                {
                    this.pictureBox_skeleton.BackgroundImage = BitmapManipulator.MirrorXBitmap((Bitmap)tempColorFrame); // Flip the bitmap
                }
            }
            else
            {
                fpsEnd -= 1;
            }
            UpdateFps();
        }

        // Converts a 16-bit grayscale depth frame which includes player indexes into a 32-bit frame
        // that displays different players in different colors
        private static byte[] ConvertDepthFrame(short[] depthFrame, DepthImageStream depthStream, int depthFrame32Length)
        {
            int tooNearDepth = depthStream.TooNearDepth;
            int tooFarDepth = depthStream.TooFarDepth;
            int unknownDepth = depthStream.UnknownDepth;
            byte[] depthFrame32 = new byte[depthFrame32Length];

            for (int i16 = 0, i32 = 0; i16 < depthFrame.Length && i32 < depthFrame32.Length; i16++, i32 += 4)
            {
                int player = depthFrame[i16] & DepthImageFrame.PlayerIndexBitmask;
                int realDepth = depthFrame[i16] >> DepthImageFrame.PlayerIndexBitmaskWidth;

                // transform 13-bit depth information into an 8-bit intensity appropriate
                // for display (we disregard information in most significant bit)
                byte intensity = (byte)(~(realDepth >> 4));

                if (player == 0 && realDepth == 0)
                {
                    // white 
                    depthFrame32[i32 + RedIndex] = 255;
                    depthFrame32[i32 + GreenIndex] = 255;
                    depthFrame32[i32 + BlueIndex] = 255;
                }
                else if (player == 0 && realDepth == tooFarDepth)
                {
                    // dark purple
                    depthFrame32[i32 + RedIndex] = 66;
                    depthFrame32[i32 + GreenIndex] = 0;
                    depthFrame32[i32 + BlueIndex] = 66;
                }
                else if (player == 0 && realDepth == unknownDepth)
                {
                    // dark brown
                    depthFrame32[i32 + RedIndex] = 66;
                    depthFrame32[i32 + GreenIndex] = 66;
                    depthFrame32[i32 + BlueIndex] = 33;
                }
                else
                {
                    // tint the intensity by dividing by per-player values
                    depthFrame32[i32 + RedIndex] = (byte)(intensity >> IntensityShiftByPlayerR[player]);
                    depthFrame32[i32 + GreenIndex] = (byte)(intensity >> IntensityShiftByPlayerG[player]);
                    depthFrame32[i32 + BlueIndex] = (byte)(intensity >> IntensityShiftByPlayerB[player]);
                }
            }

            return depthFrame32;
        }

        private static byte[] ConvertDepthFrameData(object sender, DepthImageFrame depthImageFrame)
        {
            short[] pixelsFromFrame = new short[depthImageFrame.PixelDataLength];
 
            depthImageFrame.CopyPixelDataTo(pixelsFromFrame);
            return ConvertDepthFrame(pixelsFromFrame, ((KinectSensor)sender).DepthStream, 640 * 480 * 4);
        }
        
        private void UpdateFps()
        {
            totalFrames++;
            DateTime current = DateTime.Now;
            if (lastTime == DateTime.MaxValue || current.Subtract(lastTime) > TimeSpan.FromSeconds(1))
            {
                textBoxActualFramesPerSecond.Text = (totalFrames - lastFrames).ToString();
                lastFrames = totalFrames;
                lastTime = current;
            }
        }

        private Bitmap ColorImageFrameToBitmap(ColorImageFrame colorFrame)
        {
            byte[] pixelBuffer = new byte[colorFrame.PixelDataLength];
            colorFrame.CopyPixelDataTo(pixelBuffer);
            Bitmap bitmapFrame = ArrayToBitmap(pixelBuffer, colorFrame.Width, colorFrame.Height, PixelFormat.Format32bppRgb);
            return bitmapFrame;
        }

        private Bitmap DepthImageFrameToBitmap(DepthImageFrame depthFrame)
        {
            DepthImagePixel[] depthPixels = new DepthImagePixel[depthFrame.PixelDataLength];
            byte[] colorPixels = new byte[depthFrame.PixelDataLength * 4];
            depthFrame.CopyDepthImagePixelDataTo(depthPixels);

            // Get the min and max reliable depth for the current frame
            int minDepth = depthFrame.MinDepth;
            int maxDepth = depthFrame.MaxDepth;

            // Convert the depth to RGB
            int colorPixelIndex = 0;
            for (int i = 0; i < depthPixels.Length; ++i)
            {
                // Get the depth for this pixel
                short depth = depthPixels[i].Depth;

                // To convert to a byte, we're discarding the most-significant
                // rather than least-significant bits.
                // We're preserving detail, although the intensity will "wrap."
                // Values outside the reliable depth range are mapped to 0 (black).

                // NOTE: Using conditionals in this loop could degrade performance.
                // Consider using a lookup table instead when writing production code.
                // See the KinectDepthViewer class used by the KinectExplorer sample
                // for a lookup table example.
                byte intensity = (byte)(depth >= minDepth && depth <= maxDepth ? depth : 0);

                // Write out blue byte
                colorPixels[colorPixelIndex++] = intensity;

                // Write out green byte
                colorPixels[colorPixelIndex++] = intensity;

                // Write out red byte                        
                colorPixels[colorPixelIndex++] = intensity;

                // We're outputting BGR, the last byte in the 32 bits is unused so skip it
                // If we were outputting BGRA, we would write alpha here.
                ++colorPixelIndex;
            }
            Bitmap bitmapFrame = ArrayToBitmap(colorPixels, depthFrame.Width, depthFrame.Height, PixelFormat.Format32bppRgb);
            return bitmapFrame;
        }

        private void DrawSkeletons(Image backgroundImage, Skeleton skel)
        {
            Graphics graphicBox = Graphics.FromImage(backgroundImage);
            float width = (float)(backgroundImage.Width / 640F);
            float height = (float)(backgroundImage.Height / 480F);
            graphicBox.ScaleTransform(width, height);
            this.DrawBonesAndJoints(skel, graphicBox);
        }

        private static short CalculateDistanceFromDepth(byte first, byte second)
        {
            // Please note that this would be different if you use Depth and User tracking rather than just depth
            return (short)(first | second << 8);
        }

        private static byte CalculateIntensityFromDistance(int distance)
        {
            // This will map a distance value to a 0 - 255 range
            // for the purposes of applying the resulting value
            // to RGB pixels.
            int newMax = distance - MinDepthDistance;
            if (newMax > 0)
                return (byte)(255 - (255 * newMax
                / (MaxDepthDistanceOffset)));
            else
                return (byte)255;
        }

        private void button_rec_Click(object sender, EventArgs e)
        {
            if (MyFile == null && sensor != null)
            {
                this.textBox_sensorStatus.Text = "Initialize";
                this.textBox_sensorStatus.BackColor = Color.Yellow;
                DateTime thisDay = DateTime.UtcNow;
                string txtFileName = thisDay.ToString("dd.MM.yyyy_HH.mm");
                MyFile = new MyWrite(txtFileName);
                MyFile.setTextFeld(textFields1);
            }
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            windowClosing = true;
            StopKinect(sensor);
        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            StopKinect(sensor);
        }

        //TODO: evtl folgende FUnktionen auslagern
        private void DrawBonesAndJoints(Skeleton skeleton, Graphics graphicBox)
        {
            /// Brush used to draw skeleton center point
            Brush centerPointBrush = Brushes.Blue;

            /// Brush used for drawing joints that are currently tracked
            Pen trackedJointPen = new Pen(Color.GreenYellow);

            /// Brush used for drawing joints that are currently inferred      
            Pen inferredJointPen = new Pen(Color.Yellow);

            // gefundene Punkte als Kreise malen
            foreach (Joint joint in skeleton.Joints)
            {
                Pen drawPen = null;

                if (joint.TrackingState == JointTrackingState.Tracked)
                {
                    drawPen = trackedJointPen;
                }
                else if (joint.TrackingState == JointTrackingState.Inferred)
                {
                    drawPen = inferredJointPen;
                }

                if (drawPen != null)
                {
                    graphicBox.DrawEllipse(drawPen, new Rectangle(this.SkeletonPointToScreen(joint.Position), new Size(10, 10)));
                }
            }

            //Verbindungen zwischen Punkten malen
            // Render Torso
            this.DrawBone(skeleton, graphicBox, JointType.Head, JointType.ShoulderCenter);
            this.DrawBone(skeleton, graphicBox, JointType.ShoulderCenter, JointType.ShoulderLeft);
            this.DrawBone(skeleton, graphicBox, JointType.ShoulderCenter, JointType.ShoulderRight);
            this.DrawBone(skeleton, graphicBox, JointType.ShoulderCenter, JointType.Spine);
            this.DrawBone(skeleton, graphicBox, JointType.Spine, JointType.HipCenter);
            this.DrawBone(skeleton, graphicBox, JointType.HipCenter, JointType.HipLeft);
            this.DrawBone(skeleton, graphicBox, JointType.HipCenter, JointType.HipRight);

            // Left Arm
            this.DrawBone(skeleton, graphicBox, JointType.ShoulderLeft, JointType.ElbowLeft);
            this.DrawBone(skeleton, graphicBox, JointType.ElbowLeft, JointType.WristLeft);
            this.DrawBone(skeleton, graphicBox, JointType.WristLeft, JointType.HandLeft);

            // Right Arm
            this.DrawBone(skeleton, graphicBox, JointType.ShoulderRight, JointType.ElbowRight);
            this.DrawBone(skeleton, graphicBox, JointType.ElbowRight, JointType.WristRight);
            this.DrawBone(skeleton, graphicBox, JointType.WristRight, JointType.HandRight);

            // Left Leg
            this.DrawBone(skeleton, graphicBox, JointType.HipLeft, JointType.KneeLeft);
            this.DrawBone(skeleton, graphicBox, JointType.KneeLeft, JointType.AnkleLeft);
            this.DrawBone(skeleton, graphicBox, JointType.AnkleLeft, JointType.FootLeft);

            // Right Leg
            this.DrawBone(skeleton, graphicBox, JointType.HipRight, JointType.KneeRight);
            this.DrawBone(skeleton, graphicBox, JointType.KneeRight, JointType.AnkleRight);
            this.DrawBone(skeleton, graphicBox, JointType.AnkleRight, JointType.FootRight);

            //Kopf malen
            if (skeleton.Joints[JointType.Head].TrackingState == JointTrackingState.Tracked)
            {
                graphicBox.DrawEllipse(new Pen(Color.GreenYellow), this.SkeletonPointToScreen(skeleton.Joints[JointType.Head].Position).X - 50,
                    this.SkeletonPointToScreen(skeleton.Joints[JointType.Head].Position).Y - 50, 100, 100);
            }


            return;
        }

        private void DrawBone(Skeleton skeleton, Graphics graphicBox, JointType jointType0, JointType jointType1)
        {
            /// Pen used for drawing bones that are currently tracked
            Pen trackedBonePen = new Pen(Brushes.Green, 6);

            /// Pen used for drawing bones that are currently inferred      
            Pen inferredBonePen = new Pen(Brushes.Gray, 1);

            Joint joint0 = skeleton.Joints[jointType0];
            Joint joint1 = skeleton.Joints[jointType1];

            // If we can't find either of these joints, exit
            if (joint0.TrackingState == JointTrackingState.NotTracked ||
                joint1.TrackingState == JointTrackingState.NotTracked)
            {
                return;
            }

            // Don't draw if both points are inferred
            if (joint0.TrackingState == JointTrackingState.Inferred &&
                joint1.TrackingState == JointTrackingState.Inferred)
            {
                return;
            }

            // We assume all drawn bones are inferred unless BOTH joints are tracked
            Pen drawPen = inferredBonePen;
            if (joint0.TrackingState == JointTrackingState.Tracked && joint1.TrackingState == JointTrackingState.Tracked)
            {
                drawPen = trackedBonePen;
            }


            Point startPixel = SkeletonPointToScreen(joint0.Position);
            Point endPixel = SkeletonPointToScreen(joint1.Position);
            double distanceBtw2Joints = Math.Round(calcDistanceBtw2Points(joint0.Position, joint1.Position) * 100) / 100;

            //Linie zwischen 2 Joints wird gezeichnet
            graphicBox.DrawLine(drawPen, startPixel, endPixel);

            //Länge des Bones wird daneben geschrieben
            int textPosPixelX = Convert.ToInt32(Math.Abs(Math.Round(0.5 * (startPixel.X + endPixel.X))));
            int textPosPixelY = Convert.ToInt32(Math.Abs(Math.Round(0.5 * (startPixel.Y + endPixel.Y))));
            PointF textPos = new PointF(textPosPixelX, textPosPixelY);


            //graphicBox.DrawString(distanceBtw2Joints.ToString(), new Font("Arial", 20), new SolidBrush(Color.White), textPos);

            return;
        }

        private Point SkeletonPointToScreen(SkeletonPoint skelpoint)
        {
            // Convert point to depth space.  
            // We are not using depth directly, but we do want the points in our output resolution.
            DepthImagePoint depthPoint = this.sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(skelpoint, DepthImageFormat.Resolution640x480Fps30);
            return new Point(depthPoint.X, depthPoint.Y);
        }

        private double calcDistanceBtw2Points(SkeletonPoint Joint1, SkeletonPoint Joint2)
        {
            double distanceBtwJoints = Math.Sqrt(Math.Pow(Joint1.X - Joint2.X, 2) + Math.Pow(Joint1.Y - Joint2.Y, 2) + Math.Pow(Joint1.Z - Joint2.Z, 2));
            return distanceBtwJoints;
        }

        Bitmap ArrayToBitmap(byte[] array, int width, int height, PixelFormat pixelFormat)
        {
            Bitmap bitmapFrame = new Bitmap(width, height, pixelFormat);

            BitmapData bitmapData = bitmapFrame.LockBits(new Rectangle(0, 0,
            width, height), ImageLockMode.WriteOnly, bitmapFrame.PixelFormat);

            IntPtr intPointer = bitmapData.Scan0;
            Marshal.Copy(array, 0, intPointer, array.Length);

            bitmapFrame.UnlockBits(bitmapData);
            return bitmapFrame;
        }

        private void button_recStop_Click(object sender, EventArgs e)
        {
            if (MyFile != null)
            {
                MyFile.MyCloseFile();
                this.textBox_sensorStatus.Text = "Recording captured";
                this.textBox_sensorStatus.BackColor = Color.White;
                MyFile = null;
            }
        }

        private void StopKinect(KinectSensor sensor)
        {
            if (sensor != null)
            {
                if (sensor.IsRunning)
                {
                    //stop sensor 
                    sensor.Stop();
                }
            }
        }

        // These functions come from: http://graphics.stanford.edu/~mdfisher/Kinect.html
        float rawDepthToMeters(int depthValue)
        {
            if (depthValue < 2047)
            {
                return (float)(1.0 / ((double)(depthValue) * -0.0030711016 + 3.3309495161));
            }
            return 0.0f;
        }

        Point3D depthToWorld(int x, int y, int depthValue)
        {
            const double fx_d = 1.0 / 5.9421434211923247e+02;
            const double fy_d = 1.0 / 5.9104053696870778e+02;
            const double cx_d = 3.3930780975300314e+02;
            const double cy_d = 2.4273913761751615e+02;

            Point3D result = new Point3D();
            double depth = DepthLookUp[depthValue];//rawDepthToMeters(depthValue);
            result.X = (float)((x - cx_d) * depth * fx_d);
            result.Y = (float)((y - cy_d) * depth * fy_d);
            result.Z = (float)(depth);
            return result;
        }

        #region OpenTK section
        private void CreateOpenTK3D()
        {
            //IWindowInfo m_wi;
            //IGraphicsContext m_context;
            //m_wi = Utilities.CreateWindowsWindowInfo(pictureBox1.Handle);
            //// Construct a new IGraphicsContext using the IWindowInfo from above.
            //m_context = new GraphicsContext(GraphicsMode.Default, m_wi);
            //m_context.MakeCurrent(m_wi);
            //(m_context as IGraphicsContextInternal).LoadAll();
            //GL.Clear(ClearBufferMask.ColorBufferBit); //"NullReferenceException was unhandled" is thrown here
            //GL.MatrixMode(MatrixMode.Projection);
            //GL.LoadIdentity();
            //GL.Ortho(0, pictureBox1.Width, pictureBox1.Height, 0, -1, 1);
            //TexUtil.InitTexturing();
            //m_textureID = TexUtil.CreateTextureFromFile("Untitled.png");
            //GL.BindTexture(TextureTarget.Texture2D, m_textureID);
            //GL.Begin(BeginMode.Quads);
            //GL.TexCoord2(0, 0); GL.Vertex2(0, 0);
            //GL.TexCoord2(1, 0); GL.Vertex2(pictureBox1.Width, 0);
            //GL.TexCoord2(1, 1); GL.Vertex2(pictureBox1.Width, pictureBox1.Height);
            //GL.TexCoord2(0, 1); GL.Vertex2(0, pictureBox1.Height);
            //GL.End();
            //m_context.SwapBuffers();
        }
        #endregion OpenTK section

        private void trackBarDepthInnerBand_ValueChanged(object sender, EventArgs e)
        {
            innerBandThreshold = (int)trackBarDepthInnerBand.Value;
        }

        private void trackBarDepthOuterBand_ValueChanged(object sender, EventArgs e)
        {
            outerBandThreshold = (int)trackBarDepthOuterBand.Value;
        }

        private void trackBarDepthFramesToAverage_ValueChanged(object sender, EventArgs e)
        {
            averageFrameCount = (int)trackBarDepthFramesToAverage.Value;
        }

        private void trackBarDepthThresholdMin_ValueChanged(object sender, EventArgs e)
        {
            MinDepth = (int)trackBarDepthThresholdMin.Value;
        }

        private void trackBarDepthThresholdMax_ValueChanged(object sender, EventArgs e)
        {
            MaxDepth = (int)trackBarDepthThresholdMax.Value;
        }

        private void trackBarVolumeMaxIntegrationRate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void trackBarVolumeVoxelsPerMeter_ValueChanged(object sender, EventArgs e)
        {

        }

        private void trackBarXAxis_ValueChanged(object sender, EventArgs e)
        {

        }

        private void trackBarYAxis_ValueChanged(object sender, EventArgs e)
        {

        }

        private void trackBarZAxis_ValueChanged(object sender, EventArgs e)
        {

        }

        #region Kinect Coordinate Space Info
        //Coordinate Spaces

        //12 out of 21 rated this helpful - Rate this topic
        //Kinect for Windows 1.5, 1.6, 1.7, 1.8
        //A Kinect streams out color, depth, and skeleton data one frame at a time. This section briefly describes the coordinate spaces for each data type and the API support for transforming data from one space to another.
        //Color Space
        //Each frame, the color sensor captures a color image of everything visible in the field of view of the color sensor. A frame is made up of pixels. The number of pixels depends on the frame size, which is specified by NUI_IMAGE_RESOLUTION Enumeration. Each pixel contains the red, green, and blue value of a single pixel at a particular (x, y) coordinate in the color image.
        //Depth Space
        //Each frame, the depth sensor captures a grayscale image of everything visible in the field of view of the depth sensor. A frame is made up of pixels, whose size is once again specified by NUI_IMAGE_RESOLUTION Enumeration. Each pixel contains the Cartesian distance, in millimeters, from the camera plane to the nearest object at that particular (x, y) coordinate, as shown in Figure 1. The (x, y) coordinates of a depth frame do not represent physical units in the room; instead, they represent the location of a pixel in the depth frame.
        //Figure 1.  Depth stream values
        //Hh973078.k4w_nui_5(en-us,IEB.10).png
        //When the depth stream has been opened with the NUI_IMAGE_STREAM_FLAG_DISTINCT_OVERFLOW_VALUES flag, there are three values that indicate the depth could not be reliably measured at a location. The "too near" value means an object was detected, but it is too near to the sensor to provide a reliable distance measurement. The "too far" value means an object was detected, but too far to reliably measure. The "unknown" value means no object was detected. In C++, when the NUI_IMAGE_STREAM_FLAG_DISTINCT_OVERFLOW_DEPTH_VALUES flag is not specified, all of the overflow values are reported as a depth value of "0".
        //Depth Space Range
        //The depth sensor has two depth ranges: the default range and the near range (shown in the DepthRange Enumeration). This image illustrates the sensor depth ranges in meters. The default range is available in both the Kinect for Windows sensor and the Kinect for Xbox 360 sensor; the near range is available only in the Kinect for Windows sensor.
        //Hh973078.k4w_sensor_ranges(en-us,IEB.10).png
        //This diagram applies to the values returned by the managed API, or by the native API with the NUI_IMAGE_STREAM_FLAG_DISTINCT_OVERFLOW_DEPTH_VALUES turned on. With the flag turned off, out of range data is returned as zero.
        //This table lists the depth values for out of range readings.
        //Out of range depth data	Has this value
        //too near	0x0000
        //too far	0x0FFF
        //unknown	 0x1FFF
        //The same values apply in managed code, but with a small twist: Because the managed values are signed, the 13-bit value (0xFFF8 >> 3) sign-extends to -1, rather than 8191.
        //Skeleton Space
        //Each frame, the depth image captured is processed by the Kinect runtime into skeleton data. Skeleton data contains 3D position data for human skeletons for up to two people who are visible in the depth sensor. The position of a skeleton and each of the skeleton joints (if active tracking is enabled) are stored as (x, y, z) coordinates. Unlike depth space, skeleton space coordinates are expressed in meters. The x, y, and z-axes are the body axes of the depth sensor as shown below.
        //Figure 2.  Skeleton space
        //Hh973078.k4w_nui_6(en-us,IEB.10).png
        //This is a right-handed coordinate system that places a Kinect at the origin with the positive z-axis extending in the direction in which the Kinect is pointed. The positive y-axis extends upward, and the positive x-axis extends to the left. Placing a Kinect on a surface that is not level (or tilting the sensor) to optimize the sensor's field of view can generate skeletons that appear to lean instead of be standing upright.
        //Floor Determination
        //Each skeleton frame also contains a floor-clipping-plane vector, which contains the coefficients of an estimated floor-plane equation. The skeleton tracking system updates this estimate for each frame and uses it as a clipping plane for removing the background and segmenting players. The general plane equation is:
        //        Ax + By + Cz + D = 0

        //where:
        //        A = vFloorClipPlane.x
        //        B = vFloorClipPlane.y
        //        C = vFloorClipPlane.z
        //        D = vFloorClipPlane.w

        //The equation is normalized so that the physical interpretation of D is the height of the camera from the floor, in meters. Note that the floor might not always be visible or detectable. In this case, the floor clipping plane is a zero vector.
        //The floor clipping plane is used in the vFloorClipPlane member of the NUI_SKELETON_FRAME structure (for C++) and in the FloorClipPlane property in managed code.
        //Skeletal Mirroring
        //By default, the skeleton system mirrors the user who is being tracked. That is, a person facing the sensor is considered to be looking in the -z direction in skeleton space. This accommodates an application that uses an avatar to represent the user since the avatar will be shown facing into the screen. However, if the avatar faces the user, mirroring would present the avatar as backwards. If needed, use a transformation matrix to flip the z-coordinates of the skeleton positions to orient the skeleton as necessary for your application.
        //Converting Coordinates between Spaces
        //The following APIs are designed to convert data from one coordinate space to the other:
        //Method	Converts
        //NuiTransformSkeletonToDepthImage(Vector4, LONG*, LONG*, USHORT*)	Skeleton to depth
        //NuiTransformSkeletonToDepthImage(Vector4, FLOAT*, FLOAT*, NUI_IMAGE_RESOLUTION)	Skeleton to depth
        //NuiTransformSkeletonToDepthImage(Vector4, FLOAT*, FLOAT*)	Skeleton to depth
        //NuiTransformDepthImageToSkeleton(LONG, LONG, USHORT)	Depth to skeleton
        //NuiTransformDepthImageToSkeleton(LONG, LONG, USHORT, NUI_IMAGE_RESOLUTION)	Depth to skeleton
        //NuiImageGetColorPixelCoordinatesFromDepthPixelAtResolution	Depth to color
        //NuiImageGetColorPixelCoordinatesFromDepthPixel	Depth to color
        //NuiDepthPixelToPlayerIndex	Depth to player index
        //NuiDepthPixelToDepth	Packed format depth pixel to unpacked format depth pixel
        #endregion Kinect Coordinate Space Info

        #region 3D Modelling Code

        #region commented out

        float PrincipalPointX, PrincipalPointY, FocalLengthX, FocalLengthY;
        private DepthImagePixel[] depthImagePixels;

        //public void addTriangleToMesh(Point3D p0, Point3D p1, Point3D p2, MeshGeometry3D mesh, bool combine_vertices)
        //{
        //    Vector3D normal = CalculateNormal(p0, p1, p2);

        //    if (combine_vertices)
        //    {
        //        addPointCombined(p0, mesh, normal);
        //        addPointCombined(p1, mesh, normal);
        //        addPointCombined(p2, mesh, normal);
        //    }
        //    else
        //    {
        //        mesh.Positions.Add(p0);
        //        mesh.Positions.Add(p1);
        //        mesh.Positions.Add(p2);
        //        //mesh.TriangleIndices.Add(mesh.TriangleIndices.Count);
        //        // mesh.TriangleIndices.Add(mesh.TriangleIndices.Count);
        //        // mesh.TriangleIndices.Add(mesh.TriangleIndices.Count);
        //        mesh.Normals.Add(normal);
        //        mesh.Normals.Add(normal);
        //        mesh.Normals.Add(normal);
        //    }
        //}

        public Vector3D CalculateNormal(Point3D P0, Point3D P1, Point3D P2)   //static
        {
            Vector3D v0 = new Vector3D(P1.X - P0.X, P1.Y - P0.Y, P1.Z - P0.Z);

            Vector3D v1 = new Vector3D(P2.X - P1.X, P2.Y - P1.Y, P2.Z - P1.Z);

            return Vector3D.CrossProduct(v0, v1);
        }

        private void trackBarDepthMinDistance_Scroll(object sender, EventArgs e)
        {
            this.labelDepthMinDistanceValue.Text = trackBarDepthMinDistance.Value.ToString(); ;
        }

        private void trackBarDepthMaxDistance_Scroll(object sender, EventArgs e)
        {
            this.labelDepthMaxDistanceValue.Text = trackBarDepthMaxDistance.Value.ToString(); ;
        }

        private void trackBarObjectMinSize_Scroll(object sender, EventArgs e)
        {
            this.labelObjectMinSizeValue.Text = trackBarObjectMinSize.Value.ToString(); ;
        }

        private void trackBarObjectMaxSize_Scroll(object sender, EventArgs e)
        {
            this.labelObjectMaxSizeValue.Text = trackBarObjectMaxSize.Value.ToString(); ;
        }

        //public void addPointCombined(Point3D point, MeshGeometry3D mesh, Vector3D normal)
        //{
            //bool found = false;
            //int i = 0;
            //foreach (Point3D p in mesh.Positions)
            //{
            //    if (p.Equals(point))
            //    {
            //        found = true;
            //        mesh.TriangleIndices.Add(i);
            //        mesh.Positions.Add(point);
            //        mesh.Normals.Add(normal);
            //        break;
            //    }

            //    i++;
            //}

            //if (!found)
            //{
            //    mesh.Positions.Add(point);
            //    mesh.TriangleIndices.Add(mesh.TriangleIndices.Count);
            //    mesh.Normals.Add(normal);
            //}
        //}

        //private void viewModel(Point3DCollection points)
        //{
            //DirectionalLight DirLight1 = new DirectionalLight();
            //DirLight1.Color = System.Windows.Media.Colors.White;
            //DirLight1.Direction = new Vector3D(1, 1, 1);

            //PerspectiveCamera Camera1 = new PerspectiveCamera();
            //Camera1.FarPlaneDistance = 8000;
            ////Camera1.NearPlaneDistance = 100; //close object will not be displayed with this option
            //Camera1.FieldOfView = 10;
            ////Camera1.Position = new Point3D(0, 0, 1);
            ////Camera1.LookDirection = new Vector3D(-1, -1, -1);
            //Camera1.Position = new Point3D(0, 0, 10);
            //Camera1.LookDirection = new Point3D(0, 0, 0) - Camera1.Position; //focus camera on real center of your model (0,0,0) in this case
            //Camera1.UpDirection = new Vector3D(0, 1, 0);
            ////you can use constructor to create Camera instead of assigning its properties like:
            ////PerspectiveCamera Camera1 = new PerspectiveCamera(new Point3D(0,0,10), new Vector3D(0,0,-1), new Vector3D(0,1,0), 10);


            //bool combinedvertices = true;
            //System.Windows.Media.Media3D.GeometryModel3D Triatomesh = new System.Windows.Media.Media3D.GeometryModel3D(); //  TriangleModel();
            //MeshGeometry3D tmesh = new MeshGeometry3D();
            //GeometryModel3D msheet = new GeometryModel3D();
            //Model3DGroup modelGroup = new Model3DGroup();
            //ModelVisual3D modelsVisual = new ModelVisual3D();
            //Viewport3D myViewport = new Viewport3D();

            //for (int i = 0; i < points.Count; i += 3)
            //{
            //    Triatomesh.addTriangleToMesh(points[i + 2], points[i + 1], points[i], tmesh, combinedvertices);
            //    //I did swap order of vertexes you may try both options with your model               
            //}

            //msheet.Geometry = tmesh;
            //msheet.Material = new DiffuseMaterial(new SolidColorBrush(System.Windows.Media.Colors.White));
            ////you can use constructor to create GeometryModel3D instead of assigning its properties like:
            ////msheet = new GeometryModel3D(tmesh, new DiffuseMaterial(new SolidColorBrush(Colors.White)));             

            //modelGroup.Children.Add(msheet);
            ////use AMbientLIght instead of directional
            //modelGroup.Children.Add(new AmbientLight(System.Windows.Media.Colors.White));

            //modelsVisual.Content = modelGroup;
            //myViewport.IsHitTestVisible = false;

            //myViewport.Camera = Camera1;

            //myViewport.Children.Add(modelsVisual);

            //canvas1.Children.Add(myViewport);
            //myViewport.Height = canvas1.Height;
            //myViewport.Width = canvas1.Width;
            //Canvas.SetTop(myViewport, 0);
            //Canvas.SetLeft(myViewport, 0);
        //}

        //private void display3dView()
        //{
        //    bool loop_run = true;
        //    while (loop_run)
        //    {
        //        using (DepthImageFrame depthFrame = sensor.DepthStream.OpenNextFrame(1000))
        //        {
        //            if (depthFrame == null) continue;

        //            Point3DCollection PointCloud;

        //            depthFrame.CopyDepthImagePixelDataTo(this.depthImagePixels);

        //            float[,] ImageArray = new float[320, 240];

        //            short[,] depth = new short[240, 320];

        //            for (int i = 0; i < 240; i++)
        //            {
        //                for (int j = 0; j < 320; j++)
        //                {
        //                    depth[i, j] = depthImagePixels[j + i * 320].Depth;

        //                    ImageArray[i, j] = (float)depth[i, j] / (float)1000;
        //                }
        //            }
        //            PointCloud = Calculate_PointCloud(ImageArray);

        //            viewModel(PointCloud);
        //        }
        //    }
        //}

        //private Point3DCollection Calculate_PointCloud(float[,] ImageArray)
        //{
        //    Point3DCollection PointCloud = new Point3DCollection();

        //    float x_coodinate;
        //    float y_coordinate;
        //    float z_coordinate;
        //    float thresholdvalue = 2.0f;

        //    for (int i = 0; i < 239; ++i)
        //    {
        //        for (int j = 0; j < 319; ++j)
        //        {
        //            if (Math.Abs(ImageArray[i, j] - ImageArray[i, j + 1]) < thresholdvalue && Math.Abs(ImageArray[i, j] - ImageArray[i + 1, j]) < thresholdvalue && Math.Abs(ImageArray[i, j + 1] - ImageArray[i + 1, j]) < thresholdvalue)
        //            {

        //                z_coordinate = ImageArray[i, j];
        //                x_coodinate = ((j - this.PrincipalPointX) * z_coordinate) / FocalLengthX;
        //                y_coordinate = ((i - this.PrincipalPointY) * z_coordinate) / FocalLengthY;
        //                Point3D point1 = new Point3D(x_coodinate, y_coordinate, z_coordinate);
        //                PointCloud.Add(point1);

        //                z_coordinate = ImageArray[i, j + 1];
        //                x_coodinate = (((j + 1) - this.PrincipalPointX) * z_coordinate) / FocalLengthX;
        //                y_coordinate = ((i - this.PrincipalPointY) * z_coordinate) / FocalLengthY;
        //                Point3D point2 = new Point3D(x_coodinate, y_coordinate, z_coordinate);
        //                PointCloud.Add(point2);

        //                z_coordinate = ImageArray[i + 1, j];
        //                x_coodinate = ((j - this.PrincipalPointX) * z_coordinate) / FocalLengthX;
        //                y_coordinate = (((i + 1) - this.PrincipalPointY) * z_coordinate) / FocalLengthY;
        //                Point3D point3 = new Point3D(x_coodinate, y_coordinate, z_coordinate);
        //                PointCloud.Add(point3);

        //            }
        //        }
        //    }
        //    return PointCloud;
        //}

        #endregion commented out

        #endregion 3D Modelling Code

        #region Kinect Fusion Members

        #region commented out
        ///// <summary>
        ///// Format of depth image to use, either standard 640x480 resolution or 320x240 in this sample.
        ///// </summary>
        //private const DepthImageFormat DepthFormat = DepthImageFormat.Resolution640x480Fps30;

        ///// <summary>
        ///// Format of color frame to use - here we can use either standard 640x480 resolution Rgb at 30fps
        ///// or the high resolution 1280x960 Rgb at 12fps.
        ///// </summary>
        //private const ColorImageFormat ColorFormat = ColorImageFormat.RgbResolution640x480Fps30;

        ///// <summary>
        ///// The reconstruction volume processor type. This parameter sets whether AMP or CPU processing
        ///// is used. Note that CPU processing will likely be too slow for real-time processing.
        ///// </summary>
        //private const ReconstructionProcessor ProcessorType = ReconstructionProcessor.Amp;

        ///// <summary>
        ///// The zero-based device index to choose for reconstruction processing if the 
        ///// ReconstructionProcessor AMP options are selected.
        ///// Here we automatically choose a device to use for processing by passing -1, 
        ///// </summary>
        //private const int DeviceToUse = -1;

        ///// <summary>
        ///// If set true, will automatically reset the reconstruction when MaxTrackingErrors have occurred
        ///// </summary>
        //private const bool AutoResetReconstructionWhenLost = false;

        ///// <summary>
        ///// Max tracking error count, will reset the reconstruction if tracking errors
        ///// reach the number
        ///// </summary>
        //private const int MaxTrackingErrors = 100;

        ///// <summary>
        ///// If set true, will automatically reset the reconstruction when the timestamp changes by
        ///// ResetOnTimeStampSkippedMillisecondsGPU or ResetOnTimeStampSkippedMillisecondsCPU for the 
        ///// different processor types respectively. This is useful for automatically resetting when
        ///// scrubbing through a .xed file or on loop of a .xed file during playback. Note that setting
        ///// this true may cause constant resets on slow machines that cannot process frames in less
        ///// time that the reset threshold. If this occurs, set to false or increase the timeout.
        ///// </summary>
        ///// <remarks>
        ///// We now try to find the camera pose, however, setting this false will no longer auto reset on .xed file playback
        ///// </remarks>
        //private const bool AutoResetReconstructionOnTimeSkip = false;

        ///// <summary>
        ///// Time threshold to reset the reconstruction if tracking can't be restored within it.
        ///// This value is valid if GPU is used
        ///// </summary>
        //private const int ResetOnTimeStampSkippedMillisecondsGPU = 2000;

        ///// <summary>
        ///// Time threshold to reset the reconstruction if tracking can't be restored within it.
        ///// This value is valid if CPU is used
        ///// </summary>
        //private const int ResetOnTimeStampSkippedMillisecondsCPU = 6000;

        ///// <summary>
        ///// Event interval for FPS timer
        ///// </summary>
        //private const int FpsInterval = 5;

        ///// <summary>
        ///// Event interval for status bar timer
        ///// </summary>
        //private const int StatusBarInterval = 1;

        ///// <summary>
        ///// Force a point cloud calculation and render at least every 100ms
        ///// </summary>
        //private const int RenderIntervalMilliseconds = 100;

        ///// <summary>
        ///// The frame interval where we integrate color.
        ///// Capturing color has an associated processing cost, so we do not have to capture every frame here.
        ///// </summary>
        //private const int ColorIntegrationInterval = 1;

        ///// <summary>
        ///// Frame interval we calculate the deltaFromReferenceFrame 
        ///// </summary>
        //private const int DeltaFrameCalculationInterval = 2;

        ///// <summary>
        ///// Volume Cube and WPF3D Origin coordinate cross axis 3D graphics line thickness in screen pixels
        ///// </summary>
        //private const int LineThickness = 2;

        ///// <summary>
        ///// WPF3D Origin coordinate cross 3D graphics axis size in m
        ///// </summary>
        //private const float OriginCoordinateCrossAxisSize = 0.1f;

        ///// <summary>
        ///// Frame interval we update the camera pose finder database.
        ///// </summary>
        //private const int CameraPoseFinderProcessFrameCalculationInterval = 5;

        ///// <summary>
        ///// How many frames after starting tracking will will wait before starting to store
        ///// image frames to the pose finder database. Here we set 45 successful frames (1.5s).
        ///// </summary>
        //private const int MinSuccessfulTrackingFramesForCameraPoseFinder = 45;

        ///// <summary>
        ///// How many frames after starting tracking will will wait before starting to store
        ///// image frames to the pose finder database. Here we set 200 successful frames (~7s).
        ///// </summary>
        //private const int MinSuccessfulTrackingFramesForCameraPoseFinderAfterFailure = 200;

        ///// <summary>
        ///// Here we set a high limit on the maximum residual alignment energy where we consider the tracking
        ///// to have succeeded. Typically this value would be around 0.2f to 0.3f.
        ///// (Lower residual alignment energy after tracking is considered better.)
        ///// </summary>
        //private const float MaxAlignToReconstructionEnergyForSuccess = 0.27f;

        ///// <summary>
        ///// Here we set a low limit on the residual alignment energy, below which we reject a tracking
        ///// success report and believe it to have failed. Typically this value would be around 0.005f, as
        ///// values below this (i.e. close to 0 which is perfect alignment) most likely come from frames
        ///// where the majority of the image is obscured (i.e. 0 depth) or mis-matched (i.e. similar depths
        ///// but different scene or camera pose).
        ///// </summary>
        //private const float MinAlignToReconstructionEnergyForSuccess = 0.005f;

        ///// <summary>
        ///// Here we set a high limit on the maximum residual alignment energy where we consider the tracking
        ///// with AlignPointClouds to have succeeded. Typically this value would be around 0.005f to 0.006f.
        ///// (Lower residual alignment energy after relocalization is considered better.)
        ///// </summary>
        //private const float MaxAlignPointCloudsEnergyForSuccess = 0.006f;

        ///// <summary>
        ///// Here we set a low limit on the residual alignment energy, below which we reject a tracking
        ///// success report from AlignPointClouds and believe it to have failed. This can typically be around 0.
        ///// </summary>
        //private const float MinAlignPointCloudsEnergyForSuccess = 0.0f;

        ///// <summary>
        ///// The maximum number of matched poseCount we consider when finding the camera pose. 
        ///// Although the matches are ranked, so we look at the highest probability match first, a higher 
        ///// value has a greater chance of finding a good match overall, but has the trade-off of being 
        ///// slower. Typically we test up to around the 5 best matches, after which is may be better just
        ///// to try again with the next input depth frame if no good match is found.
        ///// </summary>
        //private const int MaxCameraPoseFinderPoseTests = 5;

        ///// <summary>
        ///// CameraPoseFinderDistanceThresholdReject is a threshold used following the minimum distance 
        ///// calculation between the input frame and the camera pose finder database. This calculated value
        ///// between 0 and 1.0f must be less than or equal to the threshold in order to run the pose finder,
        ///// as the input must at least be similar to the pose finder database for a correct pose to be
        ///// matched.
        ///// </summary>
        //private const float CameraPoseFinderDistanceThresholdReject = 1.0f; // a value of 1.0 means no rejection

        ///// <summary>
        ///// CameraPoseFinderDistanceThresholdAccept is a threshold passed to the ProcessFrame 
        ///// function in the camera pose finder interface. The minimum distance between the input frame and
        ///// the pose finder database must be greater than or equal to this value for a new pose to be 
        ///// stored in the database, which regulates how close together poseCount are stored in the database.
        ///// </summary>
        //private const float CameraPoseFinderDistanceThresholdAccept = 0.1f;

        ///// <summary>
        ///// Maximum residual alignment energy where tracking is still considered successful
        ///// </summary>
        //private const int SmoothingKernelWidth = 1; // 0=just copy, 1=3x3, 2=5x5, 3=7x7, here we create a 3x3 kernel

        ///// <summary>
        ///// Maximum residual alignment energy where tracking is still considered successful
        ///// </summary>
        //private const float SmoothingDistanceThreshold = 0.04f; // 4cm, could use up to around 0.1f;

        ///// <summary>
        ///// Maximum translation threshold between successive poses when using AlingPointClouds
        ///// </summary>
        //private const float MaxTranslationDeltaAlignPointClouds = 0.3f; // 0.15 - 0.3m per frame typical

        ///// <summary>
        ///// Maximum rotation threshold between successive poses when using AlingPointClouds
        ///// </summary>
        //private const float MaxRotationDeltaAlignPointClouds = 20.0f; // 10-20 degrees per frame typical

        ///// <summary>
        ///// The factor to downsample the depth image by for AlignPointClouds
        ///// </summary>
        //private const int DownsampleFactor = 2;

        ///// <summary>
        ///// Volume Cube 3D graphics line color
        ///// </summary>
        //private static System.Windows.Media.Color volumeCubeLineColor = System.Windows.Media.Color.FromArgb(200, 0, 200, 0);   // Green, partly transparent

        ///// <summary>
        ///// Track whether Dispose has been called
        ///// </summary>
        //private bool disposed;

        ///// <summary>
        ///// Saving mesh flag
        ///// </summary>
        //private bool savingMesh;

        ///// <summary>
        ///// To display shaded surface normals frame instead of shaded surface frame
        ///// </summary>
        //private bool displayNormals;

        ///// <summary>
        ///// Capture, integrate and display color when true
        ///// </summary>
        //private bool captureColor;

        ///// <summary>
        ///// Pause or resume image integration
        ///// </summary>
        //private bool pauseIntegration;

        ///// <summary>
        ///// Depth image is mirrored
        ///// </summary>
        //private bool mirrorDepth;

        ///// <summary>
        ///// If near mode is enabled
        ///// </summary>
        //private bool nearMode;

        ///// <summary>
        ///// Whether render from the live Kinect camera pose or virtual camera pose
        ///// </summary>
        //private bool kinectView = true;

        ///// <summary>
        ///// Whether render the volume 3D graphics overlay
        ///// </summary>
        //private bool volumeGraphics;

        ///// <summary>
        ///// Image Width of depth frame
        ///// </summary>
        //private int depthWidth = 0;

        ///// <summary>
        ///// Image height of depth frame
        ///// </summary>
        //private int depthHeight = 0;

        ///// <summary>
        ///// Count of pixels in the depth frame
        ///// </summary>
        //private int depthPixelCount = 0;

        ///// <summary>
        ///// Image width of color frame
        ///// </summary>
        //private int colorWidth = 0;

        ///// <summary>
        ///// Image height of color frame
        ///// </summary>
        //private int colorHeight = 0;

        ///// <summary>
        ///// Count of pixels in the color frame
        ///// </summary>
        //private int colorPixelCount = 0;

        ///// <summary>
        ///// The width of the downsampled images for AlignPointClouds
        ///// </summary>
        //private int downsampledWidth;

        ///// <summary>
        ///// The height of the downsampled images for AlignPointClouds
        ///// </summary>
        //private int downsampledHeight;

        ///// <summary>
        ///// The counter for image process failures
        ///// </summary>
        //private int trackingErrorCount = 0;

        ///// <summary>
        ///// Set true when tracking fails
        ///// </summary>
        //private bool trackingFailed;

        ///// <summary>
        ///// Set true when tracking fails and stays false until integration resumes.
        ///// </summary>
        //private bool trackingHasFailedPreviously;

        ///// <summary>
        ///// Set true when the camera pose finder has stored frames in its database and is able to match camera frames.
        ///// </summary>
        //private bool cameraPoseFinderAvailable;

        ///// <summary>
        ///// The counter for image process successes
        ///// </summary>
        //private int successfulFrameCount;

        ///// <summary>
        ///// The counter for frames that have been processed
        ///// </summary>
        //private int processedFrameCount = 0;

        ///// <summary>
        ///// Timestamp of last depth frame in milliseconds
        ///// </summary>
        //private long lastFrameTimestamp = 0;

        ///// <summary>
        ///// Timer to count FPS
        ///// </summary>
        //private System.Threading.Timer fpsTimer; // NGE04232014 private DispatcherTimer fpsTimer;

        ///// <summary>
        ///// Timer stamp of last computation of FPS
        ///// </summary>
        //private DateTime lastFPSTimestamp = DateTime.UtcNow;

        ///// <summary>
        ///// Timer stamp of last raycast and render
        ///// </summary>
        //private DateTime lastRenderTimestamp = DateTime.UtcNow;

        ///// <summary>
        ///// Timer used for ensuring status bar message will be displayed at least one second
        ///// </summary>
        //private System.Threading.Timer statusBarTimer; // NGE04232014 private DispatcherTimer statusBarTimer;

        ///// <summary>
        ///// Timer stamp of last update of status message
        ///// </summary>
        //private DateTime lastStatusTimestamp;

        ///// <summary>
        ///// A high priority message queue for status message
        ///// </summary>
        //private Queue<string> statusMessageQueue = new Queue<string>();

        ///// <summary>
        ///// Active Kinect sensor
        ///// </summary>
        //// NGE04232014 private KinectSensor sensor = null;

        ///// <summary>
        ///// Kinect sensor chooser object
        ///// </summary>
        //private KinectSensorChooser sensorChooser;

        ///// <summary>
        ///// Intermediate storage for the extended depth data received from the camera in the current frame
        ///// </summary>
        //private DepthImagePixel[] depthImagePixels;

        ///// <summary>
        ///// Intermediate storage for the color data received from the camera in 32bit color
        ///// </summary>
        //private byte[] colorImagePixels;

        ///// <summary>
        ///// Intermediate storage for the color data received from the camera in 32bit color, re-sampled to depth image size
        ///// </summary>
        //private int[] resampledColorImagePixels;

        ///// <summary>
        ///// Intermediate storage for the color data downsampled from depth image size and used in AlignPointClouds
        ///// </summary>
        //private int[] downsampledDeltaFromReferenceColorPixels;

        ///// <summary>
        ///// The Kinect Fusion volume, enabling color reconstruction
        ///// </summary>
        //private ColorReconstruction volume;

        ///// <summary>
        ///// Intermediate storage for the depth float data converted from depth image frame
        ///// </summary>
        //private FusionFloatImageFrame depthFloatFrame;

        ///// <summary>
        ///// Intermediate storage for the smoothed depth float image frame
        ///// </summary>
        //private FusionFloatImageFrame smoothDepthFloatFrame;

        ///// <summary>
        ///// Kinect color re-sampled to be the same size as the depth frame
        ///// </summary>
        //private FusionColorImageFrame resampledColorFrame;

        ///// <summary>
        ///// Kinect color mapped into depth frame
        ///// </summary>
        //private FusionColorImageFrame resampledColorFrameDepthAligned;

        ///// <summary>
        ///// Per-pixel alignment values
        ///// </summary>
        //private FusionFloatImageFrame deltaFromReferenceFrame;

        ///// <summary>
        ///// Shaded surface frame from shading point cloud frame
        ///// </summary>
        //private FusionColorImageFrame shadedSurfaceFrame;

        ///// <summary>
        ///// Shaded surface normals frame from shading point cloud frame
        ///// </summary>
        //private FusionColorImageFrame shadedSurfaceNormalsFrame;

        ///// <summary>
        ///// Calculated point cloud frame from image integration
        ///// </summary>
        //private FusionPointCloudImageFrame raycastPointCloudFrame;

        ///// <summary>
        ///// Calculated point cloud frame from input depth
        ///// </summary>
        //private FusionPointCloudImageFrame depthPointCloudFrame;

        ///// <summary>
        ///// Intermediate storage for the depth float data converted from depth image frame
        ///// </summary>
        //private FusionFloatImageFrame downsampledDepthFloatFrame;

        ///// <summary>
        ///// Intermediate storage for the depth float data following smoothing
        ///// </summary>
        //private FusionFloatImageFrame downsampledSmoothDepthFloatFrame;

        ///// <summary>
        ///// Calculated point cloud frame from image integration
        ///// </summary>
        //private FusionPointCloudImageFrame downsampledRaycastPointCloudFrame;

        ///// <summary>
        ///// Calculated point cloud frame from input depth
        ///// </summary>
        //private FusionPointCloudImageFrame downsampledDepthPointCloudFrame;

        ///// <summary>
        ///// Kinect color delta from reference frame data from AlignPointClouds
        ///// </summary>
        //private FusionColorImageFrame downsampledDeltaFromReferenceFrameColorFrame;

        ///// <summary>
        ///// Bitmap contains depth float frame data for rendering
        ///// </summary>
        //private WriteableBitmap depthFloatFrameBitmap;

        ////// <summary>
        ////// Bitmap contains delta from reference frame data for rendering
        ////// </summary>
        //private WriteableBitmap deltaFromReferenceFrameBitmap;

        ///// <summary>
        ///// Bitmap contains shaded surface frame data for rendering
        ///// </summary>
        //private WriteableBitmap shadedSurfaceFrameBitmap;

        ///// <summary>
        ///// Pixel buffer of depth float frame with pixel data in float format
        ///// </summary>
        //private float[] depthFloatFrameDepthPixels;

        ///// <summary>
        ///// Pixel buffer of delta from reference frame with pixel data in float format
        ///// </summary>
        //private float[] deltaFromReferenceFrameFloatPixels;

        ///// <summary>
        ///// Pixel buffer of depth float frame with pixel data in 32bit color
        ///// </summary>
        //private int[] depthFloatFramePixelsArgb;

        ////// <summary>
        ////// Pixel buffer of delta from reference frame in 32bit color
        ////// </summary>
        //private int[] deltaFromReferenceFramePixelsArgb;

        ///// <summary>
        ///// Pixels buffer of shaded surface frame in 32bit color
        ///// </summary>
        //private int[] shadedSurfaceFramePixelsArgb;

        ///// <summary>
        ///// Mapping of depth pixels into color image
        ///// </summary>
        //private ColorImagePoint[] colorCoordinates;

        ///// <summary>
        ///// Mapped color pixels in depth frame of reference
        ///// </summary>
        //private int[] resampledColorImagePixelsAlignedToDepth;

        ///// <summary>
        ///// Pixel buffer of depth float frame with pixel data in float format, downsampled for AlignPointClouds
        ///// </summary>
        //private float[] downsampledDepthImagePixels;

        ///// <summary>
        ///// The coordinate mapper to convert between depth and color frames of reference
        ///// </summary>
        //private CoordinateMapper mapper;

        ///// <summary>
        ///// Alignment energy from AlignDepthFloatToReconstruction for current frame 
        ///// </summary>
        //private float alignmentEnergy;

        ///// <summary>
        ///// The worker thread to process the depth and color data
        ///// </summary>
        //private Thread workerThread = null;

        ///// <summary>
        ///// Event to stop worker thread
        ///// </summary>
        //private ManualResetEvent workerThreadStopEvent;

        ///// <summary>
        ///// Event to notify that depth data is ready for process
        ///// </summary>
        //private ManualResetEvent depthReadyEvent;

        ///// <summary>
        ///// Event to notify that color data is ready for process
        ///// </summary>
        //private ManualResetEvent colorReadyEvent;

        ///// <summary>
        ///// Lock object for raw depth pixel access
        ///// </summary>
        //private object depthLock = new object();

        ///// <summary>
        ///// Lock object for raw color pixel access
        ///// </summary>
        //private object colorLock = new object();

        ///// <summary>
        ///// Lock object for volume re-creation and meshing
        ///// </summary>
        //private object volumeLock = new object();

        ///// <summary>
        ///// The volume cube 3D graphical representation
        ///// </summary>
        //private ScreenSpaceLines3D volumeCube;

        ///// <summary>
        ///// The volume cube 3D graphical representation
        ///// </summary>
        //private ScreenSpaceLines3D volumeCubeAxisX;

        ///// <summary>
        ///// The volume cube 3D graphical representation
        ///// </summary>
        //private ScreenSpaceLines3D volumeCubeAxisY;

        ///// <summary>
        ///// The volume cube 3D graphical representation
        ///// </summary>
        //private ScreenSpaceLines3D volumeCubeAxisZ;

        ///// <summary>
        ///// The axis-aligned coordinate cross X axis
        ///// </summary>
        //private ScreenSpaceLines3D axisX;

        ///// <summary>
        ///// The axis-aligned coordinate cross Y axis
        ///// </summary>
        //private ScreenSpaceLines3D axisY;

        ///// <summary>
        ///// The axis-aligned coordinate cross Z axis
        ///// </summary>
        //private ScreenSpaceLines3D axisZ;

        ///// <summary>
        ///// Indicate whether the 3D view port has added the volume cube
        ///// </summary>
        //private bool haveAddedVolumeCube = false;

        ///// <summary>
        ///// Indicate whether the 3D view port has added the origin coordinate cross
        ///// </summary>
        //private bool haveAddedCoordinateCross = false;

        ///// <summary>
        ///// Flag boolean set true to force the reconstruction visualization to be updated after graphics camera movements
        ///// </summary>
        //private bool viewChanged = true;

        ///// <summary>
        ///// The virtual 3rd person camera view that can be controlled by the mouse
        ///// </summary>
        //private GraphicsCamera virtualCamera;

        ///// <summary>
        ///// The virtual 3rd person camera view that can be controlled by the mouse - start rotation
        ///// </summary>
        //private Quaternion virtualCameraStartRotation = Quaternion.Identity;

        ///// <summary>
        ///// The virtual 3rd person camera view that can be controlled by the mouse - start translation
        ///// </summary>
        //private Point3D virtualCameraStartTranslation = new Point3D();  // 0,0,0

        ///// <summary>
        ///// Flag to signal to worker thread to reset the reconstruction
        ///// </summary>
        //private bool resetReconstruction = false;

        ///// <summary>
        ///// Flag to signal to worker thread to re-create the reconstruction
        ///// </summary>
        //private bool recreateReconstruction = false;

        ///// <summary>
        ///// The transformation between the world and camera view coordinate system
        ///// </summary>
        //private Matrix4 worldToCameraTransform;

        ///// <summary>
        ///// The default transformation between the world and volume coordinate system
        ///// </summary>
        //private Matrix4 defaultWorldToVolumeTransform;

        ///// <summary>
        ///// Minimum depth distance threshold in meters. Depth pixels below this value will be
        ///// returned as invalid (0). Min depth must be positive or 0.
        ///// </summary>
        //private float minDepthClip = FusionDepthProcessor.DefaultMinimumDepth;

        ///// <summary>
        ///// Maximum depth distance threshold in meters. Depth pixels above this value will be
        ///// returned as invalid (0). Max depth must be greater than 0.
        ///// </summary>
        //private float maxDepthClip = FusionDepthProcessor.DefaultMaximumDepth;

        ///// <summary>
        ///// Image integration weight
        ///// </summary>
        //private short integrationWeight = FusionDepthProcessor.DefaultIntegrationWeight;

        ///// <summary>
        ///// The reconstruction volume voxel density in voxels per meter (vpm)
        ///// 1000mm / 256vpm = ~3.9mm/voxel
        ///// </summary>
        //private float voxelsPerMeter = 256.0f;

        ///// <summary>
        ///// The reconstruction volume voxel resolution in the X axis
        ///// At a setting of 256vpm the volume is 512 / 256 = 2m wide
        ///// </summary>
        //private int voxelsX = 512;

        ///// <summary>
        ///// The reconstruction volume voxel resolution in the Y axis
        ///// At a setting of 256vpm the volume is 384 / 256 = 1.5m high
        ///// </summary>
        //private int voxelsY = 384;

        ///// <summary>
        ///// The reconstruction volume voxel resolution in the Z axis
        ///// At a setting of 256vpm the volume is 512 / 256 = 2m deep
        ///// </summary>
        //private int voxelsZ = 512;

        ///// <summary>
        ///// Parameter to translate the reconstruction based on the minimum depth setting. When set to
        ///// false, the reconstruction volume +Z axis starts at the camera lens and extends into the scene.
        ///// Setting this true in the constructor will move the volume forward along +Z away from the
        ///// camera by the minimum depth threshold to enable capture of very small reconstruction volume
        ///// by setting a non-identity world-volume transformation in the ResetReconstruction call.
        ///// Small volumes should be shifted, as the Kinect hardware has a minimum sensing limit of ~0.35m,
        ///// inside which no valid depth is returned, hence it is difficult to initialize and track robustly  
        ///// when the majority of a small volume is inside this distance.
        ///// </summary>
        //private bool translateResetPoseByMinDepthThreshold = true;

        ///// <summary>
        ///// The color mapping of the rendered reconstruction visualization. 
        ///// </summary>
        //private Matrix4 worldToBGRTransform = new Matrix4();

        ///// <summary>
        ///// The virtual camera pose - updated whenever the user interacts and moves the virtual camera.
        ///// </summary>
        //private Matrix4 virtualCameraWorldToCameraMatrix4 = new Matrix4();

        ///// <summary>
        ///// Flag set true if at some point color has been captured. 
        ///// Used when writing .Ply mesh files to output vertex color.
        ///// </summary>
        //private bool colorCaptured;

        ///// <summary>
        ///// A camera pose finder to store image frames and poseCount to a database then match the input frames
        ///// when tracking is lost to help us recover tracking.
        ///// </summary>
        //private CameraPoseFinder cameraPoseFinder;

        ///// <summary>
        ///// Parameter to enable automatic finding of camera pose when lost. This searches back through
        ///// the camera pose history where key-frames and camera poseCount have been stored in the camera
        ///// pose finder database to propose the most likely pose matches for the current camera input.
        ///// </summary>
        //private bool autoFindCameraPoseWhenLost = true;

        #endregion commented out
        
        #endregion Kinect Fusion Members

        #region Kinect Speech processing
        private void RecognizerSaidSomething(object sender, SpeechRecognizer.SaidSomethingEventArgs e)
        {
            Program.UI.StartForm.labelSpeechCommand.Text = e.Matched;
            //FlyingText.NewFlyingText(this.screenRect.Width / 30, new Point(this.screenRect.Width / 2, this.screenRect.Height / 2), e.Matched);
            switch (e.Verb)
            {
                case SpeechRecognizer.Verbs.Faster:
                    if (frmDrive.RobotSpeed >= 0)
                    {
                        Program.UI.DriveForm.HandleKeys(Keys.Up);
                    }
                    else
                    {
                        Program.UI.DriveForm.HandleKeys(Keys.Down);
                    }
                    break;
                case SpeechRecognizer.Verbs.Slower:
                    if (frmDrive.RobotSpeed >= 0)
                    {
                        Program.UI.DriveForm.HandleKeys(Keys.Down);
                    }
                    else
                    {
                        Program.UI.DriveForm.HandleKeys(Keys.Up);
                    }
                    break;
                case SpeechRecognizer.Verbs.Left:
                    Program.UI.DriveForm.HandleKeys(Keys.Left);
                    break;
                case SpeechRecognizer.Verbs.Right:
                    Program.UI.DriveForm.HandleKeys(Keys.Right);
                    break;
                case SpeechRecognizer.Verbs.Forwards:
                case SpeechRecognizer.Verbs.Start:
                case SpeechRecognizer.Verbs.Go:
                    Program.UI.DriveForm.HandleKeys(Keys.Up);
                    break;
                case SpeechRecognizer.Verbs.Back:
                case SpeechRecognizer.Verbs.Backwards:
                case SpeechRecognizer.Verbs.Reverse:
                     Program.UI.DriveForm.HandleKeys(Keys.Down);
                   break;
                case SpeechRecognizer.Verbs.Stop:
                case SpeechRecognizer.Verbs.Halt:
                case SpeechRecognizer.Verbs.End:
                case SpeechRecognizer.Verbs.Reset:
                   Program.UI.DriveForm.HandleKeys(Keys.Space);
                   break;

                case SpeechRecognizer.Verbs.Pause:
                   SpeechRecognizer.paused = true;
                   MainForm.CRMainForm.checkBoxSpeech.Checked = false;
                   break;
                
                case SpeechRecognizer.Verbs.Resume:
                   SpeechRecognizer.paused = false;
                   MainForm.CRMainForm.checkBoxSpeech.Checked = true;
                   break;

                //case SpeechRecognizer.Verbs.Pause:
                //    this.myFallingThings.SetDropRate(0);
                //    this.myFallingThings.SetGravity(0);
                //    break;
                //case SpeechRecognizer.Verbs.Resume:
                //    this.myFallingThings.SetDropRate(this.dropRate);
                //    this.myFallingThings.SetGravity(this.dropGravity);
                //    break;
                //case SpeechRecognizer.Verbs.Reset:
                //    this.dropRate = DefaultDropRate;
                //    this.dropSize = DefaultDropSize;
                //    this.dropGravity = DefaultDropGravity;
                //    this.myFallingThings.SetPolies(PolyType.All);
                //    this.myFallingThings.SetDropRate(this.dropRate);
                //    this.myFallingThings.SetGravity(this.dropGravity);
                //    this.myFallingThings.SetSize(this.dropSize);
                //    this.myFallingThings.SetShapesColor(System.Windows.Media.Color.FromRgb(0, 0, 0), true);
                //    this.myFallingThings.Reset();
                //    break;
                //case SpeechRecognizer.Verbs.DoShapes:
                //    this.myFallingThings.SetPolies(e.Shape);
                //    break;
                //case SpeechRecognizer.Verbs.RandomColors:
                //    this.myFallingThings.SetShapesColor(System.Windows.Media.Color.FromRgb(0, 0, 0), true);
                //    break;
                //case SpeechRecognizer.Verbs.Colorize:
                //    this.myFallingThings.SetShapesColor(e.RgbColor, false);
                //    break;
                //case SpeechRecognizer.Verbs.ShapesAndColors:
                //    this.myFallingThings.SetPolies(e.Shape);
                //    this.myFallingThings.SetShapesColor(e.RgbColor, false);
                //    break;
                //case SpeechRecognizer.Verbs.More:
                //    this.dropRate *= 1.5;
                //    this.myFallingThings.SetDropRate(this.dropRate);
                //    break;
                //case SpeechRecognizer.Verbs.Fewer:
                //    this.dropRate /= 1.5;
                //    this.myFallingThings.SetDropRate(this.dropRate);
                //    break;
                //case SpeechRecognizer.Verbs.Bigger:
                //    this.dropSize *= 1.5;
                //    if (this.dropSize > MaxShapeSize)
                //    {
                //        this.dropSize = MaxShapeSize;
                //    }

                //    this.myFallingThings.SetSize(this.dropSize);
                //    break;
                //case SpeechRecognizer.Verbs.Biggest:
                //    this.dropSize = MaxShapeSize;
                //    this.myFallingThings.SetSize(this.dropSize);
                //    break;
                //case SpeechRecognizer.Verbs.Smaller:
                //    this.dropSize /= 1.5;
                //    if (this.dropSize < MinShapeSize)
                //    {
                //        this.dropSize = MinShapeSize;
                //    }

                //    this.myFallingThings.SetSize(this.dropSize);
                //    break;
                //case SpeechRecognizer.Verbs.Smallest:
                //    this.dropSize = MinShapeSize;
                //    this.myFallingThings.SetSize(this.dropSize);
                //    break;
                //case SpeechRecognizer.Verbs.Faster:
                //    this.dropGravity *= 1.25;
                //    if (this.dropGravity > 4.0)
                //    {
                //        this.dropGravity = 4.0;
                //    }

                //    this.myFallingThings.SetGravity(this.dropGravity);
                //    break;
                //case SpeechRecognizer.Verbs.Slower:
                //    this.dropGravity /= 1.25;
                //    if (this.dropGravity < 0.25)
                //    {
                //        this.dropGravity = 0.25;
                //    }

                //    this.myFallingThings.SetGravity(this.dropGravity);
                //    break;
            }
        }

        private void EnableAecChecked(object sender, RoutedEventArgs e)
        {
            var enableAecCheckBox = (CheckBox)sender;
            this.UpdateEchoCancellation(enableAecCheckBox);
        }

        private void UpdateEchoCancellation(CheckBox aecCheckBox)
        {
            if (aecCheckBox != null)
            {
                this.mySpeechRecognizer.EchoCancellationMode = aecCheckBox.Checked
                    ? EchoCancellationMode.CancellationAndSuppression
                    : EchoCancellationMode.None;
            }
        }

        #endregion Kinect Speech processing

        #region Kinect Gesture processing
        void GestureController_GestureRecognized(object sender, GestureEventArgs e)
        {
            // Display the gesture type.
            Program.UI.StartForm.labelGestureCommand.Text = e.Name;
            // NGE08082014 !!!!!!! tblGestures.Text = e.Name;

            // Do something according to the type of the gesture.
            switch (e.Type)
            {
                case GestureType.JoinedHands:
                   Program.UI.DriveForm.HandleKeys(Keys.Space);
                    break;
                case GestureType.Menu:
                   Program.UI.DriveForm.HandleKeys(Keys.Space);
                    break;
                case GestureType.SwipeDown:
                   Program.UI.DriveForm.HandleKeys(Keys.Space);
                    break;
                case GestureType.SwipeLeft:
                    Program.UI.DriveForm.HandleKeys(Keys.Left);
                    break;
                case GestureType.SwipeRight:
                    Program.UI.DriveForm.HandleKeys(Keys.Right);
                    break;
                case GestureType.SwipeUp:
                    Program.UI.DriveForm.HandleKeys(Keys.Up);
                   break;
                case GestureType.WaveLeft:
                    Program.UI.DriveForm.HandleKeys(Keys.Left);
                    break;
                case GestureType.WaveRight:
                    Program.UI.DriveForm.HandleKeys(Keys.Right);
                    break;
                case GestureType.ZoomIn:
                    Program.UI.DriveForm.HandleKeys(Keys.Up);
                    break;
                case GestureType.ZoomOut:
                     Program.UI.DriveForm.HandleKeys(Keys.Down);
                    break;
                default:
                    break;
            }
        }
        #endregion Kinect Gesture processing
    
    }

    #region Image Utils

    public static class ImageHelpers
    {

        private const int MaxDepthDistance = 4000;
        private const int MinDepthDistance = 850;
        private const int MaxDepthDistanceOffset = 3150;

        public static Bitmap SliceDepthImage(this DepthImageFrame image, int min = 20, int max = 1000)
        {
            int width = image.Width;
            int height = image.Height;

            //var depthFrame = image.Image.Bits;
            short[] rawDepthData = new short[image.PixelDataLength];
            image.CopyPixelDataTo(rawDepthData);

            byte[] pixels = new byte[height * width * 4];

            const int BlueIndex = 0;
            const int GreenIndex = 1;
            const int RedIndex = 2;

            for (int depthIndex = 0, colorIndex = 0;
                depthIndex < rawDepthData.Length && colorIndex < pixels.Length;
                depthIndex++, colorIndex += 4)
            {

                // Calculate the distance represented by the two depth bytes
                int depth = rawDepthData[depthIndex] >> DepthImageFrame.PlayerIndexBitmaskWidth;

                // Map the distance to an intesity that can be represented in RGB
                var intensity = ImageHelpers.CalculateIntensityFromDistance(depth);

                if (depth > min && depth < max)
                {
                    // Apply the intensity to the color channels
                    pixels[colorIndex + BlueIndex] = intensity; //blue
                    pixels[colorIndex + GreenIndex] = intensity; //green
                    pixels[colorIndex + RedIndex] = intensity; //red                    
                }
            }

            return BitmapManipulator.ByteArrayToBitmap(pixels, width, height, PixelFormat.Format32bppArgb);
        }

        public static byte CalculateIntensityFromDistance(int distance)
        {
            // This will map a distance value to a 0 - 255 range
            // for the purposes of applying the resulting value
            // to RGB pixels.
            int newMax = distance - MinDepthDistance;
            if (newMax > 0)
                return (byte)(255 - (255 * newMax
                / (MaxDepthDistanceOffset)));
            else
                return (byte)255;
        }

    }

    #endregion Image Utils

}
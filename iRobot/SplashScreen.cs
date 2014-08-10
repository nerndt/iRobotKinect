//SplashScreen.cs

using System;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;

namespace iRobotKinect
{
    public class SplashScreen : Form
    {
        private static Thread _splashLauncher;
        private static SplashScreen _splashScreen = null;
        private Label CopyrightLabel;
        private Label labelForResearchOnly;
        private Label labelInitializingInstrument;

        public static bool ShowInitializingInstrumentMessage = false;
        public static bool ShowSplashScreen = true;
        private System.Windows.Forms.Timer timer1;

        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Static property for checking if the splash screen exists
        /// </summary>
        public static bool DoesSplashExist
        {
            get { return (_splashScreen != null); }
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (_splashScreen != null)
            {
                Application.ExitThread();
                _splashScreen = null;
            }

            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreen));
            this.CopyrightLabel = new System.Windows.Forms.Label();
            this.labelForResearchOnly = new System.Windows.Forms.Label();
            this.labelInitializingInstrument = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // CopyrightLabel
            // 
            this.CopyrightLabel.BackColor = System.Drawing.Color.Transparent;
            this.CopyrightLabel.Font = new System.Drawing.Font("Arial", 12.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CopyrightLabel.ForeColor = System.Drawing.Color.LightGray;
            this.CopyrightLabel.Location = new System.Drawing.Point(12, 496);
            this.CopyrightLabel.Name = "CopyrightLabel";
            this.CopyrightLabel.Size = new System.Drawing.Size(600, 30);
            this.CopyrightLabel.TabIndex = 0;
            this.CopyrightLabel.Text = "© 2013 Cellular Research, Inc.   All rights reserved.";
            this.CopyrightLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelForResearchOnly
            // 
            this.labelForResearchOnly.BackColor = System.Drawing.Color.Transparent;
            this.labelForResearchOnly.Font = new System.Drawing.Font("Arial", 12.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelForResearchOnly.ForeColor = System.Drawing.Color.LightGray;
            this.labelForResearchOnly.Location = new System.Drawing.Point(12, 482);
            this.labelForResearchOnly.Name = "labelForResearchOnly";
            this.labelForResearchOnly.Size = new System.Drawing.Size(584, 14);
            this.labelForResearchOnly.TabIndex = 1;
            this.labelForResearchOnly.Text = "For research use only.   Not for use in diagnostic procedures.";
            this.labelForResearchOnly.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelInitializingInstrument
            // 
            this.labelInitializingInstrument.BackColor = System.Drawing.Color.White;
            this.labelInitializingInstrument.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInitializingInstrument.ForeColor = System.Drawing.Color.Black;
            this.labelInitializingInstrument.Location = new System.Drawing.Point(258, 57);
            this.labelInitializingInstrument.Name = "labelInitializingInstrument";
            this.labelInitializingInstrument.Size = new System.Drawing.Size(258, 42);
            this.labelInitializingInstrument.TabIndex = 2;
            this.labelInitializingInstrument.Text = "Initializing - Please wait...";
            this.labelInitializingInstrument.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelInitializingInstrument.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 250;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // SplashScreen
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(740, 530);
            this.Controls.Add(this.labelInitializingInstrument);
            this.Controls.Add(this.labelForResearchOnly);
            this.Controls.Add(this.CopyrightLabel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SplashScreen";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Click += new System.EventHandler(this.SplashScreen_Click);
            this.ResumeLayout(false);

        }

        #endregion

        private SplashScreen()
        {
            TopMost = true;
            InitializeComponent();
            CreateControl();    // Forces the creation of the handle

            // Analyze current graphics resolution
            Graphics g = this.CreateGraphics();

            if (g != null)
            {
                if (g.DpiX != 96 || g.DpiY != 96)
                {
                    MainForm.scaleRatioX = 96f / g.DpiX;
                    MainForm.scaleRatioY = 96f / g.DpiY;
                }

                g.Dispose();
            }

            timer1.Enabled = true;

            // Adjust label in SplashScreen
            //MainForm.AdjustTextSizeForAllControls(this, true);
        }


        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Do not paint background
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (BackgroundImage != null)
            {
                e.Graphics.DrawImage(BackgroundImage, 0, 0);
            }
        }

        public static bool ShowSplash()
        {
            // Show the form in a new thread
            _splashLauncher = new Thread(new ThreadStart(LaunchSplash));
            _splashLauncher.SetApartmentState(ApartmentState.STA);
            _splashLauncher.IsBackground = true;
            _splashLauncher.Start();

            // Wait for up to 5 seconds for the SplashScreen Handle to be created
            // Max Wait Time = 5 seconds
            DateTime maxTime = DateTime.Now.AddSeconds(5);

            while (_splashScreen == null && DateTime.Now < maxTime)
                GuiHelper.Wait(0);  // Allow the background thread some time

            return (_splashScreen != null);
        }

        public static void ShowInitializingInstrumentLabel()
        {
            ShowInitializingInstrumentMessage = true;
        }

        public static bool IsSplashUp()
        {
            return (_splashScreen != null);
        }

        private static void LaunchSplash()
        {
            _splashScreen = new SplashScreen();

            // Create new message pump
            Application.Run(_splashScreen);
        }

        private static void CloseSplashDown()
        {
            Application.ExitThread();
            _splashScreen = null;
        }

        public static void CloseSplash()
        {
            // Need to get the thread that launched the form, so we need to use Invoke.
            if (_splashScreen != null)
            {
                MethodInvoker mi = new MethodInvoker(CloseSplashDown);
                _splashScreen.BeginInvoke(mi);

                // Wait for Splash Screen to not exist
                while (_splashScreen != null)
                    GuiHelper.Wait(0);
            }

        }

        private void SplashScreen_Click(object sender, EventArgs e)
        {
            // Close the Splash Screen right away
            CloseSplashDown();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TopMost = true; // Make sure it stays on top!!
            if (ShowInitializingInstrumentMessage == true)
            {
                this.labelInitializingInstrument.Visible = true;
            }
        }
    }
}


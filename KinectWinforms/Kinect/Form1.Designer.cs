namespace KinectWinforms
{
    partial class MainWindow
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_start = new System.Windows.Forms.Button();
            this.button_stop = new System.Windows.Forms.Button();
            this.textBox_sensorStatus = new System.Windows.Forms.TextBox();
            this.dropDown_fps = new System.Windows.Forms.ComboBox();
            this.txtLabel_fps = new System.Windows.Forms.Label();
            this.pictureBox_skeleton = new System.Windows.Forms.PictureBox();
            this.button_rec = new System.Windows.Forms.Button();
            this.button_recStop = new System.Windows.Forms.Button();
            this.checkBox_colorCam = new System.Windows.Forms.CheckBox();
            this.groupBox_smooth = new System.Windows.Forms.GroupBox();
            this.radioButton_smoothIntense = new System.Windows.Forms.RadioButton();
            this.radioButton_smoothModerate = new System.Windows.Forms.RadioButton();
            this.radioButton_smoothDefault = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_init = new System.Windows.Forms.TextBox();
            this.checkBoxShowSkeleton = new System.Windows.Forms.CheckBox();
            this.groupBoxActions = new System.Windows.Forms.GroupBox();
            this.buttonResetVirtualCamera = new System.Windows.Forms.Button();
            this.buttonResetReconstruction = new System.Windows.Forms.Button();
            this.checkBoxUseCameraPoseFinder = new System.Windows.Forms.CheckBox();
            this.groupBoxCreateMesh = new System.Windows.Forms.GroupBox();
            this.radioButtonPly = new System.Windows.Forms.RadioButton();
            this.radioButtonObj = new System.Windows.Forms.RadioButton();
            this.radioButtonSTL = new System.Windows.Forms.RadioButton();
            this.groupBoxImageOptions = new System.Windows.Forms.GroupBox();
            this.checkBoxVolumeGraphics = new System.Windows.Forms.CheckBox();
            this.checkBoxMirrorDepth = new System.Windows.Forms.CheckBox();
            this.checkBoxNearMode = new System.Windows.Forms.CheckBox();
            this.checkBoxKinectView = new System.Windows.Forms.CheckBox();
            this.checkBoxPauseIntegration = new System.Windows.Forms.CheckBox();
            this.checkBoxCaptureColor = new System.Windows.Forms.CheckBox();
            this.groupBoxDepthThreshold = new System.Windows.Forms.GroupBox();
            this.labelDepthThresholdMax = new System.Windows.Forms.Label();
            this.labelDepthThresholdMin = new System.Windows.Forms.Label();
            this.trackBarDepthThresholdMax = new System.Windows.Forms.TrackBar();
            this.trackBarDepthThresholdMin = new System.Windows.Forms.TrackBar();
            this.groupBoxVoxelInfo = new System.Windows.Forms.GroupBox();
            this.labelZAxis = new System.Windows.Forms.Label();
            this.trackBarZAxis = new System.Windows.Forms.TrackBar();
            this.labelYAxis = new System.Windows.Forms.Label();
            this.trackBarYAxis = new System.Windows.Forms.TrackBar();
            this.labelXAxis = new System.Windows.Forms.Label();
            this.trackBarXAxis = new System.Windows.Forms.TrackBar();
            this.labelVolumeVoxelsResolution = new System.Windows.Forms.Label();
            this.labelVolumeVoxelsPerMeter = new System.Windows.Forms.Label();
            this.trackBarVolumeVoxelsPerMeter = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.trackBarVolumeMaxIntegrationRate = new System.Windows.Forms.TrackBar();
            this.pictureBox_depthPic = new System.Windows.Forms.PictureBox();
            this.pictureBox_colorPic = new System.Windows.Forms.PictureBox();
            this.pictureBox_depthPicSmoothed = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxDepthUseAverage = new System.Windows.Forms.CheckBox();
            this.checkBoxDepthUseFiltering = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.trackBarDepthFramesToAverage = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.trackBarDepthOuterBand = new System.Windows.Forms.TrackBar();
            this.trackBarDepthInnerBand = new System.Windows.Forms.TrackBar();
            this.textBoxActualFramesPerSecond = new System.Windows.Forms.TextBox();
            this.labelActualFramesPerSecond = new System.Windows.Forms.Label();
            this.textFields1 = new KinectWinforms.TextFields();
            this.groupBoxObjectSize = new System.Windows.Forms.GroupBox();
            this.labelObjectMaxSizeValue = new System.Windows.Forms.Label();
            this.labelObjectMinSizeValue = new System.Windows.Forms.Label();
            this.labelDepthMaxDistanceValue = new System.Windows.Forms.Label();
            this.labelDepthMinDistanceValue = new System.Windows.Forms.Label();
            this.labelDepthMaxDistance = new System.Windows.Forms.Label();
            this.labelDepthMinDistance = new System.Windows.Forms.Label();
            this.trackBarDepthMaxDistance = new System.Windows.Forms.TrackBar();
            this.trackBarDepthMinDistance = new System.Windows.Forms.TrackBar();
            this.labelObjectSizeMax = new System.Windows.Forms.Label();
            this.labelObjectSizeMin = new System.Windows.Forms.Label();
            this.trackBarObjectMaxSize = new System.Windows.Forms.TrackBar();
            this.trackBarObjectMinSize = new System.Windows.Forms.TrackBar();
            this.richTextBoxObjectFound = new System.Windows.Forms.RichTextBox();
            this.labelObjectsFound = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_skeleton)).BeginInit();
            this.groupBox_smooth.SuspendLayout();
            this.groupBoxActions.SuspendLayout();
            this.groupBoxCreateMesh.SuspendLayout();
            this.groupBoxImageOptions.SuspendLayout();
            this.groupBoxDepthThreshold.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDepthThresholdMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDepthThresholdMin)).BeginInit();
            this.groupBoxVoxelInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarZAxis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarYAxis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarXAxis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVolumeVoxelsPerMeter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVolumeMaxIntegrationRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_depthPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_colorPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_depthPicSmoothed)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDepthFramesToAverage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDepthOuterBand)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDepthInnerBand)).BeginInit();
            this.groupBoxObjectSize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDepthMaxDistance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDepthMinDistance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarObjectMaxSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarObjectMinSize)).BeginInit();
            this.SuspendLayout();
            // 
            // button_start
            // 
            this.button_start.Location = new System.Drawing.Point(678, 307);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(75, 23);
            this.button_start.TabIndex = 2;
            this.button_start.Text = "Start";
            this.button_start.UseVisualStyleBackColor = true;
            this.button_start.Click += new System.EventHandler(this.button_start_Click);
            // 
            // button_stop
            // 
            this.button_stop.Location = new System.Drawing.Point(678, 336);
            this.button_stop.Name = "button_stop";
            this.button_stop.Size = new System.Drawing.Size(75, 23);
            this.button_stop.TabIndex = 3;
            this.button_stop.Text = "Stop";
            this.button_stop.UseVisualStyleBackColor = true;
            this.button_stop.Click += new System.EventHandler(this.button_stop_Click);
            // 
            // textBox_sensorStatus
            // 
            this.textBox_sensorStatus.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_sensorStatus.Location = new System.Drawing.Point(678, 423);
            this.textBox_sensorStatus.Name = "textBox_sensorStatus";
            this.textBox_sensorStatus.Size = new System.Drawing.Size(75, 20);
            this.textBox_sensorStatus.TabIndex = 4;
            this.textBox_sensorStatus.Text = "Status";
            this.textBox_sensorStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dropDown_fps
            // 
            this.dropDown_fps.Cursor = System.Windows.Forms.Cursors.Default;
            this.dropDown_fps.FormattingEnabled = true;
            this.dropDown_fps.Items.AddRange(new object[] {
            "30",
            "15",
            "10",
            "5",
            "1"});
            this.dropDown_fps.Location = new System.Drawing.Point(705, 12);
            this.dropDown_fps.Name = "dropDown_fps";
            this.dropDown_fps.Size = new System.Drawing.Size(75, 21);
            this.dropDown_fps.TabIndex = 7;
            this.dropDown_fps.Text = "10";
            // 
            // txtLabel_fps
            // 
            this.txtLabel_fps.AutoSize = true;
            this.txtLabel_fps.Location = new System.Drawing.Point(675, 15);
            this.txtLabel_fps.Name = "txtLabel_fps";
            this.txtLabel_fps.Size = new System.Drawing.Size(24, 13);
            this.txtLabel_fps.TabIndex = 9;
            this.txtLabel_fps.Text = "fps:";
            // 
            // pictureBox_skeleton
            // 
            this.pictureBox_skeleton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pictureBox_skeleton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox_skeleton.Location = new System.Drawing.Point(12, 12);
            this.pictureBox_skeleton.Name = "pictureBox_skeleton";
            this.pictureBox_skeleton.Size = new System.Drawing.Size(320, 240);
            this.pictureBox_skeleton.TabIndex = 10;
            this.pictureBox_skeleton.TabStop = false;
            // 
            // button_rec
            // 
            this.button_rec.Location = new System.Drawing.Point(678, 365);
            this.button_rec.Name = "button_rec";
            this.button_rec.Size = new System.Drawing.Size(75, 23);
            this.button_rec.TabIndex = 12;
            this.button_rec.Text = "Record";
            this.button_rec.UseVisualStyleBackColor = true;
            this.button_rec.Click += new System.EventHandler(this.button_rec_Click);
            // 
            // button_recStop
            // 
            this.button_recStop.Location = new System.Drawing.Point(678, 394);
            this.button_recStop.Name = "button_recStop";
            this.button_recStop.Size = new System.Drawing.Size(75, 23);
            this.button_recStop.TabIndex = 13;
            this.button_recStop.Text = "Record stop";
            this.button_recStop.UseVisualStyleBackColor = true;
            this.button_recStop.Click += new System.EventHandler(this.button_recStop_Click);
            // 
            // checkBox_colorCam
            // 
            this.checkBox_colorCam.AutoSize = true;
            this.checkBox_colorCam.Checked = true;
            this.checkBox_colorCam.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_colorCam.Location = new System.Drawing.Point(678, 95);
            this.checkBox_colorCam.Name = "checkBox_colorCam";
            this.checkBox_colorCam.Size = new System.Drawing.Size(89, 17);
            this.checkBox_colorCam.TabIndex = 27;
            this.checkBox_colorCam.Text = "Video Stream";
            this.checkBox_colorCam.UseVisualStyleBackColor = true;
            // 
            // groupBox_smooth
            // 
            this.groupBox_smooth.Controls.Add(this.radioButton_smoothIntense);
            this.groupBox_smooth.Controls.Add(this.radioButton_smoothModerate);
            this.groupBox_smooth.Controls.Add(this.radioButton_smoothDefault);
            this.groupBox_smooth.Location = new System.Drawing.Point(669, 133);
            this.groupBox_smooth.Name = "groupBox_smooth";
            this.groupBox_smooth.Size = new System.Drawing.Size(111, 86);
            this.groupBox_smooth.TabIndex = 29;
            this.groupBox_smooth.TabStop = false;
            this.groupBox_smooth.Text = "Smooth";
            // 
            // radioButton_smoothIntense
            // 
            this.radioButton_smoothIntense.AutoSize = true;
            this.radioButton_smoothIntense.Location = new System.Drawing.Point(6, 65);
            this.radioButton_smoothIntense.Name = "radioButton_smoothIntense";
            this.radioButton_smoothIntense.Size = new System.Drawing.Size(60, 17);
            this.radioButton_smoothIntense.TabIndex = 2;
            this.radioButton_smoothIntense.Text = "Intense";
            this.radioButton_smoothIntense.UseVisualStyleBackColor = true;
            // 
            // radioButton_smoothModerate
            // 
            this.radioButton_smoothModerate.AutoSize = true;
            this.radioButton_smoothModerate.Location = new System.Drawing.Point(6, 42);
            this.radioButton_smoothModerate.Name = "radioButton_smoothModerate";
            this.radioButton_smoothModerate.Size = new System.Drawing.Size(70, 17);
            this.radioButton_smoothModerate.TabIndex = 1;
            this.radioButton_smoothModerate.Text = "Moderate";
            this.radioButton_smoothModerate.UseVisualStyleBackColor = true;
            // 
            // radioButton_smoothDefault
            // 
            this.radioButton_smoothDefault.AutoSize = true;
            this.radioButton_smoothDefault.Checked = true;
            this.radioButton_smoothDefault.Location = new System.Drawing.Point(6, 19);
            this.radioButton_smoothDefault.Name = "radioButton_smoothDefault";
            this.radioButton_smoothDefault.Size = new System.Drawing.Size(59, 17);
            this.radioButton_smoothDefault.TabIndex = 0;
            this.radioButton_smoothDefault.TabStop = true;
            this.radioButton_smoothDefault.Text = "Default";
            this.radioButton_smoothDefault.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(666, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "Initialization:";
            // 
            // textBox_init
            // 
            this.textBox_init.Location = new System.Drawing.Point(730, 65);
            this.textBox_init.Name = "textBox_init";
            this.textBox_init.Size = new System.Drawing.Size(50, 20);
            this.textBox_init.TabIndex = 31;
            this.textBox_init.Text = "100";
            this.textBox_init.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // checkBoxShowSkeleton
            // 
            this.checkBoxShowSkeleton.AutoSize = true;
            this.checkBoxShowSkeleton.Checked = true;
            this.checkBoxShowSkeleton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShowSkeleton.Location = new System.Drawing.Point(678, 115);
            this.checkBoxShowSkeleton.Name = "checkBoxShowSkeleton";
            this.checkBoxShowSkeleton.Size = new System.Drawing.Size(113, 17);
            this.checkBoxShowSkeleton.TabIndex = 32;
            this.checkBoxShowSkeleton.Text = "Skeleton Tracking";
            this.checkBoxShowSkeleton.UseVisualStyleBackColor = true;
            // 
            // groupBoxActions
            // 
            this.groupBoxActions.Controls.Add(this.buttonResetVirtualCamera);
            this.groupBoxActions.Controls.Add(this.buttonResetReconstruction);
            this.groupBoxActions.Controls.Add(this.checkBoxUseCameraPoseFinder);
            this.groupBoxActions.Controls.Add(this.groupBoxCreateMesh);
            this.groupBoxActions.Location = new System.Drawing.Point(721, 468);
            this.groupBoxActions.Name = "groupBoxActions";
            this.groupBoxActions.Size = new System.Drawing.Size(178, 174);
            this.groupBoxActions.TabIndex = 34;
            this.groupBoxActions.TabStop = false;
            this.groupBoxActions.Text = "Actions";
            // 
            // buttonResetVirtualCamera
            // 
            this.buttonResetVirtualCamera.Location = new System.Drawing.Point(16, 135);
            this.buttonResetVirtualCamera.Name = "buttonResetVirtualCamera";
            this.buttonResetVirtualCamera.Size = new System.Drawing.Size(153, 23);
            this.buttonResetVirtualCamera.TabIndex = 38;
            this.buttonResetVirtualCamera.Text = "Reset Virtual Camera";
            this.buttonResetVirtualCamera.UseVisualStyleBackColor = true;
            // 
            // buttonResetReconstruction
            // 
            this.buttonResetReconstruction.Location = new System.Drawing.Point(16, 79);
            this.buttonResetReconstruction.Name = "buttonResetReconstruction";
            this.buttonResetReconstruction.Size = new System.Drawing.Size(153, 23);
            this.buttonResetReconstruction.TabIndex = 37;
            this.buttonResetReconstruction.Text = "Reset Reconstruction";
            this.buttonResetReconstruction.UseVisualStyleBackColor = true;
            // 
            // checkBoxUseCameraPoseFinder
            // 
            this.checkBoxUseCameraPoseFinder.AutoSize = true;
            this.checkBoxUseCameraPoseFinder.Location = new System.Drawing.Point(16, 112);
            this.checkBoxUseCameraPoseFinder.Name = "checkBoxUseCameraPoseFinder";
            this.checkBoxUseCameraPoseFinder.Size = new System.Drawing.Size(143, 17);
            this.checkBoxUseCameraPoseFinder.TabIndex = 1;
            this.checkBoxUseCameraPoseFinder.Text = "Use Camera Pose Finder";
            this.checkBoxUseCameraPoseFinder.UseVisualStyleBackColor = true;
            // 
            // groupBoxCreateMesh
            // 
            this.groupBoxCreateMesh.Controls.Add(this.radioButtonPly);
            this.groupBoxCreateMesh.Controls.Add(this.radioButtonObj);
            this.groupBoxCreateMesh.Controls.Add(this.radioButtonSTL);
            this.groupBoxCreateMesh.Location = new System.Drawing.Point(16, 26);
            this.groupBoxCreateMesh.Name = "groupBoxCreateMesh";
            this.groupBoxCreateMesh.Size = new System.Drawing.Size(153, 46);
            this.groupBoxCreateMesh.TabIndex = 0;
            this.groupBoxCreateMesh.TabStop = false;
            this.groupBoxCreateMesh.Text = "Create Mesh";
            // 
            // radioButtonPly
            // 
            this.radioButtonPly.AutoSize = true;
            this.radioButtonPly.Location = new System.Drawing.Point(111, 19);
            this.radioButtonPly.Name = "radioButtonPly";
            this.radioButtonPly.Size = new System.Drawing.Size(39, 17);
            this.radioButtonPly.TabIndex = 4;
            this.radioButtonPly.TabStop = true;
            this.radioButtonPly.Text = "Ply";
            this.radioButtonPly.UseVisualStyleBackColor = true;
            // 
            // radioButtonObj
            // 
            this.radioButtonObj.AutoSize = true;
            this.radioButtonObj.Location = new System.Drawing.Point(64, 19);
            this.radioButtonObj.Name = "radioButtonObj";
            this.radioButtonObj.Size = new System.Drawing.Size(41, 17);
            this.radioButtonObj.TabIndex = 2;
            this.radioButtonObj.TabStop = true;
            this.radioButtonObj.Text = "Obj";
            this.radioButtonObj.UseVisualStyleBackColor = true;
            // 
            // radioButtonSTL
            // 
            this.radioButtonSTL.AutoSize = true;
            this.radioButtonSTL.Checked = true;
            this.radioButtonSTL.Location = new System.Drawing.Point(6, 19);
            this.radioButtonSTL.Name = "radioButtonSTL";
            this.radioButtonSTL.Size = new System.Drawing.Size(45, 17);
            this.radioButtonSTL.TabIndex = 0;
            this.radioButtonSTL.TabStop = true;
            this.radioButtonSTL.Text = "STL";
            this.radioButtonSTL.UseVisualStyleBackColor = true;
            // 
            // groupBoxImageOptions
            // 
            this.groupBoxImageOptions.Controls.Add(this.checkBoxVolumeGraphics);
            this.groupBoxImageOptions.Controls.Add(this.checkBoxMirrorDepth);
            this.groupBoxImageOptions.Controls.Add(this.checkBoxNearMode);
            this.groupBoxImageOptions.Controls.Add(this.checkBoxKinectView);
            this.groupBoxImageOptions.Controls.Add(this.checkBoxPauseIntegration);
            this.groupBoxImageOptions.Controls.Add(this.checkBoxCaptureColor);
            this.groupBoxImageOptions.Location = new System.Drawing.Point(1190, 373);
            this.groupBoxImageOptions.Name = "groupBoxImageOptions";
            this.groupBoxImageOptions.Size = new System.Drawing.Size(240, 92);
            this.groupBoxImageOptions.TabIndex = 35;
            this.groupBoxImageOptions.TabStop = false;
            this.groupBoxImageOptions.Text = "Image Options";
            // 
            // checkBoxVolumeGraphics
            // 
            this.checkBoxVolumeGraphics.AutoSize = true;
            this.checkBoxVolumeGraphics.Location = new System.Drawing.Point(124, 65);
            this.checkBoxVolumeGraphics.Name = "checkBoxVolumeGraphics";
            this.checkBoxVolumeGraphics.Size = new System.Drawing.Size(106, 17);
            this.checkBoxVolumeGraphics.TabIndex = 5;
            this.checkBoxVolumeGraphics.Text = "Volume Graphics";
            this.checkBoxVolumeGraphics.UseVisualStyleBackColor = true;
            // 
            // checkBoxMirrorDepth
            // 
            this.checkBoxMirrorDepth.AutoSize = true;
            this.checkBoxMirrorDepth.Location = new System.Drawing.Point(124, 42);
            this.checkBoxMirrorDepth.Name = "checkBoxMirrorDepth";
            this.checkBoxMirrorDepth.Size = new System.Drawing.Size(84, 17);
            this.checkBoxMirrorDepth.TabIndex = 4;
            this.checkBoxMirrorDepth.Text = "Mirror Depth";
            this.checkBoxMirrorDepth.UseVisualStyleBackColor = true;
            // 
            // checkBoxNearMode
            // 
            this.checkBoxNearMode.AutoSize = true;
            this.checkBoxNearMode.Location = new System.Drawing.Point(124, 19);
            this.checkBoxNearMode.Name = "checkBoxNearMode";
            this.checkBoxNearMode.Size = new System.Drawing.Size(79, 17);
            this.checkBoxNearMode.TabIndex = 3;
            this.checkBoxNearMode.Text = "Near Mode";
            this.checkBoxNearMode.UseVisualStyleBackColor = true;
            // 
            // checkBoxKinectView
            // 
            this.checkBoxKinectView.AutoSize = true;
            this.checkBoxKinectView.Location = new System.Drawing.Point(9, 65);
            this.checkBoxKinectView.Name = "checkBoxKinectView";
            this.checkBoxKinectView.Size = new System.Drawing.Size(82, 17);
            this.checkBoxKinectView.TabIndex = 2;
            this.checkBoxKinectView.Text = "Kinect View";
            this.checkBoxKinectView.UseVisualStyleBackColor = true;
            // 
            // checkBoxPauseIntegration
            // 
            this.checkBoxPauseIntegration.AutoSize = true;
            this.checkBoxPauseIntegration.Location = new System.Drawing.Point(9, 42);
            this.checkBoxPauseIntegration.Name = "checkBoxPauseIntegration";
            this.checkBoxPauseIntegration.Size = new System.Drawing.Size(109, 17);
            this.checkBoxPauseIntegration.TabIndex = 1;
            this.checkBoxPauseIntegration.Text = "Pause Integration";
            this.checkBoxPauseIntegration.UseVisualStyleBackColor = true;
            // 
            // checkBoxCaptureColor
            // 
            this.checkBoxCaptureColor.AutoSize = true;
            this.checkBoxCaptureColor.Location = new System.Drawing.Point(9, 19);
            this.checkBoxCaptureColor.Name = "checkBoxCaptureColor";
            this.checkBoxCaptureColor.Size = new System.Drawing.Size(90, 17);
            this.checkBoxCaptureColor.TabIndex = 0;
            this.checkBoxCaptureColor.Text = "Capture Color";
            this.checkBoxCaptureColor.UseVisualStyleBackColor = true;
            // 
            // groupBoxDepthThreshold
            // 
            this.groupBoxDepthThreshold.Controls.Add(this.labelDepthThresholdMax);
            this.groupBoxDepthThreshold.Controls.Add(this.labelDepthThresholdMin);
            this.groupBoxDepthThreshold.Controls.Add(this.trackBarDepthThresholdMax);
            this.groupBoxDepthThreshold.Controls.Add(this.trackBarDepthThresholdMin);
            this.groupBoxDepthThreshold.Location = new System.Drawing.Point(1190, 471);
            this.groupBoxDepthThreshold.Name = "groupBoxDepthThreshold";
            this.groupBoxDepthThreshold.Size = new System.Drawing.Size(240, 97);
            this.groupBoxDepthThreshold.TabIndex = 36;
            this.groupBoxDepthThreshold.TabStop = false;
            this.groupBoxDepthThreshold.Text = "Depth Threshold";
            // 
            // labelDepthThresholdMax
            // 
            this.labelDepthThresholdMax.AutoSize = true;
            this.labelDepthThresholdMax.Location = new System.Drawing.Point(6, 63);
            this.labelDepthThresholdMax.Name = "labelDepthThresholdMax";
            this.labelDepthThresholdMax.Size = new System.Drawing.Size(27, 13);
            this.labelDepthThresholdMax.TabIndex = 6;
            this.labelDepthThresholdMax.Text = "Max";
            // 
            // labelDepthThresholdMin
            // 
            this.labelDepthThresholdMin.AutoSize = true;
            this.labelDepthThresholdMin.Location = new System.Drawing.Point(6, 23);
            this.labelDepthThresholdMin.Name = "labelDepthThresholdMin";
            this.labelDepthThresholdMin.Size = new System.Drawing.Size(24, 13);
            this.labelDepthThresholdMin.TabIndex = 5;
            this.labelDepthThresholdMin.Text = "Min";
            // 
            // trackBarDepthThresholdMax
            // 
            this.trackBarDepthThresholdMax.LargeChange = 150;
            this.trackBarDepthThresholdMax.Location = new System.Drawing.Point(27, 58);
            this.trackBarDepthThresholdMax.Margin = new System.Windows.Forms.Padding(0);
            this.trackBarDepthThresholdMax.Maximum = 800;
            this.trackBarDepthThresholdMax.Minimum = 35;
            this.trackBarDepthThresholdMax.Name = "trackBarDepthThresholdMax";
            this.trackBarDepthThresholdMax.Size = new System.Drawing.Size(206, 45);
            this.trackBarDepthThresholdMax.SmallChange = 2;
            this.trackBarDepthThresholdMax.TabIndex = 2;
            this.trackBarDepthThresholdMax.TickFrequency = 10;
            this.trackBarDepthThresholdMax.Value = 35;
            this.trackBarDepthThresholdMax.ValueChanged += new System.EventHandler(this.trackBarDepthThresholdMax_ValueChanged);
            // 
            // trackBarDepthThresholdMin
            // 
            this.trackBarDepthThresholdMin.LargeChange = 150;
            this.trackBarDepthThresholdMin.Location = new System.Drawing.Point(27, 23);
            this.trackBarDepthThresholdMin.Margin = new System.Windows.Forms.Padding(0);
            this.trackBarDepthThresholdMin.Maximum = 800;
            this.trackBarDepthThresholdMin.Minimum = 35;
            this.trackBarDepthThresholdMin.Name = "trackBarDepthThresholdMin";
            this.trackBarDepthThresholdMin.Size = new System.Drawing.Size(206, 45);
            this.trackBarDepthThresholdMin.SmallChange = 2;
            this.trackBarDepthThresholdMin.TabIndex = 1;
            this.trackBarDepthThresholdMin.Value = 35;
            this.trackBarDepthThresholdMin.ValueChanged += new System.EventHandler(this.trackBarDepthThresholdMin_ValueChanged);
            // 
            // groupBoxVoxelInfo
            // 
            this.groupBoxVoxelInfo.Controls.Add(this.labelZAxis);
            this.groupBoxVoxelInfo.Controls.Add(this.trackBarZAxis);
            this.groupBoxVoxelInfo.Controls.Add(this.labelYAxis);
            this.groupBoxVoxelInfo.Controls.Add(this.trackBarYAxis);
            this.groupBoxVoxelInfo.Controls.Add(this.labelXAxis);
            this.groupBoxVoxelInfo.Controls.Add(this.trackBarXAxis);
            this.groupBoxVoxelInfo.Controls.Add(this.labelVolumeVoxelsResolution);
            this.groupBoxVoxelInfo.Controls.Add(this.labelVolumeVoxelsPerMeter);
            this.groupBoxVoxelInfo.Controls.Add(this.trackBarVolumeVoxelsPerMeter);
            this.groupBoxVoxelInfo.Controls.Add(this.label2);
            this.groupBoxVoxelInfo.Controls.Add(this.trackBarVolumeMaxIntegrationRate);
            this.groupBoxVoxelInfo.Location = new System.Drawing.Point(944, 373);
            this.groupBoxVoxelInfo.Name = "groupBoxVoxelInfo";
            this.groupBoxVoxelInfo.Size = new System.Drawing.Size(240, 238);
            this.groupBoxVoxelInfo.TabIndex = 36;
            this.groupBoxVoxelInfo.TabStop = false;
            this.groupBoxVoxelInfo.Text = "Voxel Info";
            // 
            // labelZAxis
            // 
            this.labelZAxis.AutoSize = true;
            this.labelZAxis.Location = new System.Drawing.Point(10, 192);
            this.labelZAxis.Name = "labelZAxis";
            this.labelZAxis.Size = new System.Drawing.Size(36, 13);
            this.labelZAxis.TabIndex = 16;
            this.labelZAxis.Text = "Z Axis";
            // 
            // trackBarZAxis
            // 
            this.trackBarZAxis.LargeChange = 1;
            this.trackBarZAxis.Location = new System.Drawing.Point(54, 192);
            this.trackBarZAxis.Margin = new System.Windows.Forms.Padding(0);
            this.trackBarZAxis.Maximum = 640;
            this.trackBarZAxis.Minimum = 128;
            this.trackBarZAxis.Name = "trackBarZAxis";
            this.trackBarZAxis.Size = new System.Drawing.Size(180, 45);
            this.trackBarZAxis.TabIndex = 15;
            this.trackBarZAxis.Value = 128;
            this.trackBarZAxis.ValueChanged += new System.EventHandler(this.trackBarZAxis_ValueChanged);
            // 
            // labelYAxis
            // 
            this.labelYAxis.AutoSize = true;
            this.labelYAxis.Location = new System.Drawing.Point(8, 157);
            this.labelYAxis.Name = "labelYAxis";
            this.labelYAxis.Size = new System.Drawing.Size(36, 13);
            this.labelYAxis.TabIndex = 14;
            this.labelYAxis.Text = "Y Axis";
            // 
            // trackBarYAxis
            // 
            this.trackBarYAxis.LargeChange = 1;
            this.trackBarYAxis.Location = new System.Drawing.Point(52, 157);
            this.trackBarYAxis.Margin = new System.Windows.Forms.Padding(0);
            this.trackBarYAxis.Maximum = 640;
            this.trackBarYAxis.Minimum = 128;
            this.trackBarYAxis.Name = "trackBarYAxis";
            this.trackBarYAxis.Size = new System.Drawing.Size(180, 45);
            this.trackBarYAxis.TabIndex = 13;
            this.trackBarYAxis.Value = 128;
            this.trackBarYAxis.ValueChanged += new System.EventHandler(this.trackBarYAxis_ValueChanged);
            // 
            // labelXAxis
            // 
            this.labelXAxis.AutoSize = true;
            this.labelXAxis.Location = new System.Drawing.Point(6, 125);
            this.labelXAxis.Name = "labelXAxis";
            this.labelXAxis.Size = new System.Drawing.Size(36, 13);
            this.labelXAxis.TabIndex = 12;
            this.labelXAxis.Text = "X Axis";
            // 
            // trackBarXAxis
            // 
            this.trackBarXAxis.LargeChange = 1;
            this.trackBarXAxis.Location = new System.Drawing.Point(50, 125);
            this.trackBarXAxis.Margin = new System.Windows.Forms.Padding(0);
            this.trackBarXAxis.Maximum = 640;
            this.trackBarXAxis.Minimum = 128;
            this.trackBarXAxis.Name = "trackBarXAxis";
            this.trackBarXAxis.Size = new System.Drawing.Size(180, 45);
            this.trackBarXAxis.TabIndex = 11;
            this.trackBarXAxis.Value = 128;
            this.trackBarXAxis.ValueChanged += new System.EventHandler(this.trackBarXAxis_ValueChanged);
            // 
            // labelVolumeVoxelsResolution
            // 
            this.labelVolumeVoxelsResolution.AutoSize = true;
            this.labelVolumeVoxelsResolution.Location = new System.Drawing.Point(75, 109);
            this.labelVolumeVoxelsResolution.Name = "labelVolumeVoxelsResolution";
            this.labelVolumeVoxelsResolution.Size = new System.Drawing.Size(129, 13);
            this.labelVolumeVoxelsResolution.TabIndex = 10;
            this.labelVolumeVoxelsResolution.Text = "Volume Voxels Resolution";
            // 
            // labelVolumeVoxelsPerMeter
            // 
            this.labelVolumeVoxelsPerMeter.AutoSize = true;
            this.labelVolumeVoxelsPerMeter.Location = new System.Drawing.Point(75, 61);
            this.labelVolumeVoxelsPerMeter.Name = "labelVolumeVoxelsPerMeter";
            this.labelVolumeVoxelsPerMeter.Size = new System.Drawing.Size(125, 13);
            this.labelVolumeVoxelsPerMeter.TabIndex = 9;
            this.labelVolumeVoxelsPerMeter.Text = "Volume Voxels Per Meter";
            // 
            // trackBarVolumeVoxelsPerMeter
            // 
            this.trackBarVolumeVoxelsPerMeter.LargeChange = 10;
            this.trackBarVolumeVoxelsPerMeter.Location = new System.Drawing.Point(6, 77);
            this.trackBarVolumeVoxelsPerMeter.Margin = new System.Windows.Forms.Padding(0);
            this.trackBarVolumeVoxelsPerMeter.Maximum = 768;
            this.trackBarVolumeVoxelsPerMeter.Minimum = 128;
            this.trackBarVolumeVoxelsPerMeter.Name = "trackBarVolumeVoxelsPerMeter";
            this.trackBarVolumeVoxelsPerMeter.Size = new System.Drawing.Size(224, 45);
            this.trackBarVolumeVoxelsPerMeter.TabIndex = 8;
            this.trackBarVolumeVoxelsPerMeter.Value = 128;
            this.trackBarVolumeVoxelsPerMeter.ValueChanged += new System.EventHandler(this.trackBarVolumeVoxelsPerMeter_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(75, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Volume Max Integration Rate";
            // 
            // trackBarVolumeMaxIntegrationRate
            // 
            this.trackBarVolumeMaxIntegrationRate.LargeChange = 50;
            this.trackBarVolumeMaxIntegrationRate.Location = new System.Drawing.Point(6, 26);
            this.trackBarVolumeMaxIntegrationRate.Margin = new System.Windows.Forms.Padding(0);
            this.trackBarVolumeMaxIntegrationRate.Maximum = 1000;
            this.trackBarVolumeMaxIntegrationRate.Minimum = 1;
            this.trackBarVolumeMaxIntegrationRate.Name = "trackBarVolumeMaxIntegrationRate";
            this.trackBarVolumeMaxIntegrationRate.Size = new System.Drawing.Size(224, 45);
            this.trackBarVolumeMaxIntegrationRate.SmallChange = 10;
            this.trackBarVolumeMaxIntegrationRate.TabIndex = 6;
            this.trackBarVolumeMaxIntegrationRate.Value = 1;
            this.trackBarVolumeMaxIntegrationRate.ValueChanged += new System.EventHandler(this.trackBarVolumeMaxIntegrationRate_ValueChanged);
            // 
            // pictureBox_depthPic
            // 
            this.pictureBox_depthPic.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pictureBox_depthPic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox_depthPic.Location = new System.Drawing.Point(12, 261);
            this.pictureBox_depthPic.Name = "pictureBox_depthPic";
            this.pictureBox_depthPic.Size = new System.Drawing.Size(320, 240);
            this.pictureBox_depthPic.TabIndex = 37;
            this.pictureBox_depthPic.TabStop = false;
            // 
            // pictureBox_colorPic
            // 
            this.pictureBox_colorPic.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pictureBox_colorPic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox_colorPic.Location = new System.Drawing.Point(343, 12);
            this.pictureBox_colorPic.Name = "pictureBox_colorPic";
            this.pictureBox_colorPic.Size = new System.Drawing.Size(320, 240);
            this.pictureBox_colorPic.TabIndex = 38;
            this.pictureBox_colorPic.TabStop = false;
            // 
            // pictureBox_depthPicSmoothed
            // 
            this.pictureBox_depthPicSmoothed.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pictureBox_depthPicSmoothed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox_depthPicSmoothed.Location = new System.Drawing.Point(343, 261);
            this.pictureBox_depthPicSmoothed.Name = "pictureBox_depthPicSmoothed";
            this.pictureBox_depthPicSmoothed.Size = new System.Drawing.Size(320, 240);
            this.pictureBox_depthPicSmoothed.TabIndex = 39;
            this.pictureBox_depthPicSmoothed.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxDepthUseAverage);
            this.groupBox1.Controls.Add(this.checkBoxDepthUseFiltering);
            this.groupBox1.Location = new System.Drawing.Point(669, 220);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(111, 67);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Depth";
            // 
            // checkBoxDepthUseAverage
            // 
            this.checkBoxDepthUseAverage.AutoSize = true;
            this.checkBoxDepthUseAverage.Checked = true;
            this.checkBoxDepthUseAverage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDepthUseAverage.Location = new System.Drawing.Point(9, 42);
            this.checkBoxDepthUseAverage.Name = "checkBoxDepthUseAverage";
            this.checkBoxDepthUseAverage.Size = new System.Drawing.Size(88, 17);
            this.checkBoxDepthUseAverage.TabIndex = 29;
            this.checkBoxDepthUseAverage.Text = "Use Average";
            this.checkBoxDepthUseAverage.UseVisualStyleBackColor = true;
            // 
            // checkBoxDepthUseFiltering
            // 
            this.checkBoxDepthUseFiltering.AutoSize = true;
            this.checkBoxDepthUseFiltering.Checked = true;
            this.checkBoxDepthUseFiltering.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDepthUseFiltering.Location = new System.Drawing.Point(9, 19);
            this.checkBoxDepthUseFiltering.Name = "checkBoxDepthUseFiltering";
            this.checkBoxDepthUseFiltering.Size = new System.Drawing.Size(84, 17);
            this.checkBoxDepthUseFiltering.TabIndex = 28;
            this.checkBoxDepthUseFiltering.Text = "Use Filtering";
            this.checkBoxDepthUseFiltering.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.trackBarDepthFramesToAverage);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.trackBarDepthOuterBand);
            this.groupBox2.Controls.Add(this.trackBarDepthInnerBand);
            this.groupBox2.Location = new System.Drawing.Point(1070, 617);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(360, 128);
            this.groupBox2.TabIndex = 37;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Depth Smoothing";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(145, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Number ofFrames to Average";
            // 
            // trackBarDepthFramesToAverage
            // 
            this.trackBarDepthFramesToAverage.LargeChange = 1;
            this.trackBarDepthFramesToAverage.Location = new System.Drawing.Point(144, 83);
            this.trackBarDepthFramesToAverage.Margin = new System.Windows.Forms.Padding(0);
            this.trackBarDepthFramesToAverage.Maximum = 12;
            this.trackBarDepthFramesToAverage.Minimum = 2;
            this.trackBarDepthFramesToAverage.Name = "trackBarDepthFramesToAverage";
            this.trackBarDepthFramesToAverage.Size = new System.Drawing.Size(206, 45);
            this.trackBarDepthFramesToAverage.TabIndex = 7;
            this.trackBarDepthFramesToAverage.Value = 5;
            this.trackBarDepthFramesToAverage.ValueChanged += new System.EventHandler(this.trackBarDepthFramesToAverage_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(80, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Outer Band";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(80, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 5;
            this.label4.Tag = "";
            this.label4.Text = "Inner Band";
            // 
            // trackBarDepthOuterBand
            // 
            this.trackBarDepthOuterBand.LargeChange = 1;
            this.trackBarDepthOuterBand.Location = new System.Drawing.Point(144, 54);
            this.trackBarDepthOuterBand.Margin = new System.Windows.Forms.Padding(0);
            this.trackBarDepthOuterBand.Maximum = 16;
            this.trackBarDepthOuterBand.Minimum = 1;
            this.trackBarDepthOuterBand.Name = "trackBarDepthOuterBand";
            this.trackBarDepthOuterBand.Size = new System.Drawing.Size(206, 45);
            this.trackBarDepthOuterBand.TabIndex = 2;
            this.trackBarDepthOuterBand.Value = 5;
            this.trackBarDepthOuterBand.ValueChanged += new System.EventHandler(this.trackBarDepthOuterBand_ValueChanged);
            // 
            // trackBarDepthInnerBand
            // 
            this.trackBarDepthInnerBand.LargeChange = 1;
            this.trackBarDepthInnerBand.Location = new System.Drawing.Point(144, 23);
            this.trackBarDepthInnerBand.Margin = new System.Windows.Forms.Padding(0);
            this.trackBarDepthInnerBand.Maximum = 8;
            this.trackBarDepthInnerBand.Minimum = 1;
            this.trackBarDepthInnerBand.Name = "trackBarDepthInnerBand";
            this.trackBarDepthInnerBand.Size = new System.Drawing.Size(206, 45);
            this.trackBarDepthInnerBand.TabIndex = 1;
            this.trackBarDepthInnerBand.Value = 2;
            this.trackBarDepthInnerBand.ValueChanged += new System.EventHandler(this.trackBarDepthInnerBand_ValueChanged);
            // 
            // textBoxActualFramesPerSecond
            // 
            this.textBoxActualFramesPerSecond.Location = new System.Drawing.Point(730, 42);
            this.textBoxActualFramesPerSecond.Name = "textBoxActualFramesPerSecond";
            this.textBoxActualFramesPerSecond.Size = new System.Drawing.Size(50, 20);
            this.textBoxActualFramesPerSecond.TabIndex = 42;
            this.textBoxActualFramesPerSecond.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelActualFramesPerSecond
            // 
            this.labelActualFramesPerSecond.AutoSize = true;
            this.labelActualFramesPerSecond.Location = new System.Drawing.Point(666, 45);
            this.labelActualFramesPerSecond.Name = "labelActualFramesPerSecond";
            this.labelActualFramesPerSecond.Size = new System.Drawing.Size(63, 13);
            this.labelActualFramesPerSecond.TabIndex = 41;
            this.labelActualFramesPerSecond.Text = "Actual FPS:";
            // 
            // textFields1
            // 
            this.textFields1.Location = new System.Drawing.Point(9, 547);
            this.textFields1.Margin = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.textFields1.Name = "textFields1";
            this.textFields1.setTextBoxAngles = "";
            this.textFields1.setTextBoxCapturedFrames = "";
            this.textFields1.setTextBoxElapsedTime = "";
            this.textFields1.setTextBoxFrameRate = "";
            this.textFields1.setTextBoxLength = "";
            this.textFields1.setTextPosition = "";
            this.textFields1.Size = new System.Drawing.Size(703, 209);
            this.textFields1.TabIndex = 28;
            // 
            // groupBoxObjectSize
            // 
            this.groupBoxObjectSize.Controls.Add(this.labelObjectsFound);
            this.groupBoxObjectSize.Controls.Add(this.richTextBoxObjectFound);
            this.groupBoxObjectSize.Controls.Add(this.labelObjectMaxSizeValue);
            this.groupBoxObjectSize.Controls.Add(this.labelObjectMinSizeValue);
            this.groupBoxObjectSize.Controls.Add(this.labelDepthMaxDistanceValue);
            this.groupBoxObjectSize.Controls.Add(this.labelDepthMinDistanceValue);
            this.groupBoxObjectSize.Controls.Add(this.labelDepthMaxDistance);
            this.groupBoxObjectSize.Controls.Add(this.labelDepthMinDistance);
            this.groupBoxObjectSize.Controls.Add(this.trackBarDepthMaxDistance);
            this.groupBoxObjectSize.Controls.Add(this.trackBarDepthMinDistance);
            this.groupBoxObjectSize.Controls.Add(this.labelObjectSizeMax);
            this.groupBoxObjectSize.Controls.Add(this.labelObjectSizeMin);
            this.groupBoxObjectSize.Controls.Add(this.trackBarObjectMaxSize);
            this.groupBoxObjectSize.Controls.Add(this.trackBarObjectMinSize);
            this.groupBoxObjectSize.Location = new System.Drawing.Point(803, 15);
            this.groupBoxObjectSize.Name = "groupBoxObjectSize";
            this.groupBoxObjectSize.Size = new System.Drawing.Size(329, 211);
            this.groupBoxObjectSize.TabIndex = 43;
            this.groupBoxObjectSize.TabStop = false;
            this.groupBoxObjectSize.Text = "Object Tracking";
            // 
            // labelObjectMaxSizeValue
            // 
            this.labelObjectMaxSizeValue.AutoSize = true;
            this.labelObjectMaxSizeValue.Location = new System.Drawing.Point(287, 142);
            this.labelObjectMaxSizeValue.Name = "labelObjectMaxSizeValue";
            this.labelObjectMaxSizeValue.Size = new System.Drawing.Size(34, 13);
            this.labelObjectMaxSizeValue.TabIndex = 14;
            this.labelObjectMaxSizeValue.Text = "Value";
            // 
            // labelObjectMinSizeValue
            // 
            this.labelObjectMinSizeValue.AutoSize = true;
            this.labelObjectMinSizeValue.Location = new System.Drawing.Point(287, 102);
            this.labelObjectMinSizeValue.Name = "labelObjectMinSizeValue";
            this.labelObjectMinSizeValue.Size = new System.Drawing.Size(34, 13);
            this.labelObjectMinSizeValue.TabIndex = 13;
            this.labelObjectMinSizeValue.Text = "Value";
            // 
            // labelDepthMaxDistanceValue
            // 
            this.labelDepthMaxDistanceValue.AutoSize = true;
            this.labelDepthMaxDistanceValue.Location = new System.Drawing.Point(287, 56);
            this.labelDepthMaxDistanceValue.Name = "labelDepthMaxDistanceValue";
            this.labelDepthMaxDistanceValue.Size = new System.Drawing.Size(34, 13);
            this.labelDepthMaxDistanceValue.TabIndex = 12;
            this.labelDepthMaxDistanceValue.Text = "Value";
            // 
            // labelDepthMinDistanceValue
            // 
            this.labelDepthMinDistanceValue.AutoSize = true;
            this.labelDepthMinDistanceValue.Location = new System.Drawing.Point(286, 16);
            this.labelDepthMinDistanceValue.Name = "labelDepthMinDistanceValue";
            this.labelDepthMinDistanceValue.Size = new System.Drawing.Size(34, 13);
            this.labelDepthMinDistanceValue.TabIndex = 11;
            this.labelDepthMinDistanceValue.Text = "Value";
            // 
            // labelDepthMaxDistance
            // 
            this.labelDepthMaxDistance.AutoSize = true;
            this.labelDepthMaxDistance.Location = new System.Drawing.Point(6, 56);
            this.labelDepthMaxDistance.Name = "labelDepthMaxDistance";
            this.labelDepthMaxDistance.Size = new System.Drawing.Size(72, 13);
            this.labelDepthMaxDistance.TabIndex = 10;
            this.labelDepthMaxDistance.Text = "Max Distance";
            // 
            // labelDepthMinDistance
            // 
            this.labelDepthMinDistance.AutoSize = true;
            this.labelDepthMinDistance.Location = new System.Drawing.Point(6, 16);
            this.labelDepthMinDistance.Name = "labelDepthMinDistance";
            this.labelDepthMinDistance.Size = new System.Drawing.Size(69, 13);
            this.labelDepthMinDistance.TabIndex = 9;
            this.labelDepthMinDistance.Text = "Min Distance";
            // 
            // trackBarDepthMaxDistance
            // 
            this.trackBarDepthMaxDistance.LargeChange = 1000;
            this.trackBarDepthMaxDistance.Location = new System.Drawing.Point(77, 51);
            this.trackBarDepthMaxDistance.Margin = new System.Windows.Forms.Padding(0);
            this.trackBarDepthMaxDistance.Maximum = 6000;
            this.trackBarDepthMaxDistance.Minimum = 900;
            this.trackBarDepthMaxDistance.Name = "trackBarDepthMaxDistance";
            this.trackBarDepthMaxDistance.Size = new System.Drawing.Size(206, 45);
            this.trackBarDepthMaxDistance.SmallChange = 100;
            this.trackBarDepthMaxDistance.TabIndex = 8;
            this.trackBarDepthMaxDistance.TickFrequency = 100;
            this.trackBarDepthMaxDistance.Value = 1270;
            this.trackBarDepthMaxDistance.Scroll += new System.EventHandler(this.trackBarDepthMaxDistance_Scroll);
            // 
            // trackBarDepthMinDistance
            // 
            this.trackBarDepthMinDistance.LargeChange = 50;
            this.trackBarDepthMinDistance.Location = new System.Drawing.Point(77, 16);
            this.trackBarDepthMinDistance.Margin = new System.Windows.Forms.Padding(0);
            this.trackBarDepthMinDistance.Maximum = 900;
            this.trackBarDepthMinDistance.Minimum = 300;
            this.trackBarDepthMinDistance.Name = "trackBarDepthMinDistance";
            this.trackBarDepthMinDistance.Size = new System.Drawing.Size(206, 45);
            this.trackBarDepthMinDistance.SmallChange = 5;
            this.trackBarDepthMinDistance.TabIndex = 7;
            this.trackBarDepthMinDistance.TickFrequency = 10;
            this.trackBarDepthMinDistance.Value = 750;
            this.trackBarDepthMinDistance.Scroll += new System.EventHandler(this.trackBarDepthMinDistance_Scroll);
            // 
            // labelObjectSizeMax
            // 
            this.labelObjectSizeMax.AutoSize = true;
            this.labelObjectSizeMax.Location = new System.Drawing.Point(6, 142);
            this.labelObjectSizeMax.Name = "labelObjectSizeMax";
            this.labelObjectSizeMax.Size = new System.Drawing.Size(50, 13);
            this.labelObjectSizeMax.TabIndex = 6;
            this.labelObjectSizeMax.Text = "Max Size";
            // 
            // labelObjectSizeMin
            // 
            this.labelObjectSizeMin.AutoSize = true;
            this.labelObjectSizeMin.Location = new System.Drawing.Point(6, 102);
            this.labelObjectSizeMin.Name = "labelObjectSizeMin";
            this.labelObjectSizeMin.Size = new System.Drawing.Size(47, 13);
            this.labelObjectSizeMin.TabIndex = 5;
            this.labelObjectSizeMin.Text = "Min Size";
            // 
            // trackBarObjectMaxSize
            // 
            this.trackBarObjectMaxSize.LargeChange = 50;
            this.trackBarObjectMaxSize.Location = new System.Drawing.Point(77, 137);
            this.trackBarObjectMaxSize.Margin = new System.Windows.Forms.Padding(0);
            this.trackBarObjectMaxSize.Maximum = 500;
            this.trackBarObjectMaxSize.Minimum = 1;
            this.trackBarObjectMaxSize.Name = "trackBarObjectMaxSize";
            this.trackBarObjectMaxSize.Size = new System.Drawing.Size(206, 45);
            this.trackBarObjectMaxSize.TabIndex = 2;
            this.trackBarObjectMaxSize.TickFrequency = 10;
            this.trackBarObjectMaxSize.Value = 110;
            this.trackBarObjectMaxSize.Scroll += new System.EventHandler(this.trackBarObjectMaxSize_Scroll);
            // 
            // trackBarObjectMinSize
            // 
            this.trackBarObjectMinSize.Location = new System.Drawing.Point(77, 102);
            this.trackBarObjectMinSize.Margin = new System.Windows.Forms.Padding(0);
            this.trackBarObjectMinSize.Maximum = 50;
            this.trackBarObjectMinSize.Minimum = 1;
            this.trackBarObjectMinSize.Name = "trackBarObjectMinSize";
            this.trackBarObjectMinSize.Size = new System.Drawing.Size(206, 45);
            this.trackBarObjectMinSize.TabIndex = 1;
            this.trackBarObjectMinSize.Value = 10;
            this.trackBarObjectMinSize.Scroll += new System.EventHandler(this.trackBarObjectMinSize_Scroll);
            // 
            // richTextBoxObjectFound
            // 
            this.richTextBoxObjectFound.Location = new System.Drawing.Point(127, 173);
            this.richTextBoxObjectFound.Name = "richTextBoxObjectFound";
            this.richTextBoxObjectFound.Size = new System.Drawing.Size(163, 26);
            this.richTextBoxObjectFound.TabIndex = 44;
            this.richTextBoxObjectFound.Text = "";
            // 
            // labelObjectsFound
            // 
            this.labelObjectsFound.AutoSize = true;
            this.labelObjectsFound.Location = new System.Drawing.Point(42, 182);
            this.labelObjectsFound.Name = "labelObjectsFound";
            this.labelObjectsFound.Size = new System.Drawing.Size(79, 13);
            this.labelObjectsFound.TabIndex = 7;
            this.labelObjectsFound.Text = "Objects Found:";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1435, 757);
            this.Controls.Add(this.groupBoxObjectSize);
            this.Controls.Add(this.textBoxActualFramesPerSecond);
            this.Controls.Add(this.labelActualFramesPerSecond);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox_depthPicSmoothed);
            this.Controls.Add(this.pictureBox_colorPic);
            this.Controls.Add(this.pictureBox_depthPic);
            this.Controls.Add(this.groupBoxVoxelInfo);
            this.Controls.Add(this.groupBoxDepthThreshold);
            this.Controls.Add(this.groupBoxImageOptions);
            this.Controls.Add(this.groupBoxActions);
            this.Controls.Add(this.checkBoxShowSkeleton);
            this.Controls.Add(this.textBox_init);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox_smooth);
            this.Controls.Add(this.pictureBox_skeleton);
            this.Controls.Add(this.textFields1);
            this.Controls.Add(this.checkBox_colorCam);
            this.Controls.Add(this.button_recStop);
            this.Controls.Add(this.button_rec);
            this.Controls.Add(this.txtLabel_fps);
            this.Controls.Add(this.dropDown_fps);
            this.Controls.Add(this.textBox_sensorStatus);
            this.Controls.Add(this.button_stop);
            this.Controls.Add(this.button_start);
            this.Name = "MainWindow";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Kinect Main Window";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWindow_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_skeleton)).EndInit();
            this.groupBox_smooth.ResumeLayout(false);
            this.groupBox_smooth.PerformLayout();
            this.groupBoxActions.ResumeLayout(false);
            this.groupBoxActions.PerformLayout();
            this.groupBoxCreateMesh.ResumeLayout(false);
            this.groupBoxCreateMesh.PerformLayout();
            this.groupBoxImageOptions.ResumeLayout(false);
            this.groupBoxImageOptions.PerformLayout();
            this.groupBoxDepthThreshold.ResumeLayout(false);
            this.groupBoxDepthThreshold.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDepthThresholdMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDepthThresholdMin)).EndInit();
            this.groupBoxVoxelInfo.ResumeLayout(false);
            this.groupBoxVoxelInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarZAxis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarYAxis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarXAxis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVolumeVoxelsPerMeter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVolumeMaxIntegrationRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_depthPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_colorPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_depthPicSmoothed)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDepthFramesToAverage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDepthOuterBand)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDepthInnerBand)).EndInit();
            this.groupBoxObjectSize.ResumeLayout(false);
            this.groupBoxObjectSize.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDepthMaxDistance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDepthMinDistance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarObjectMaxSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarObjectMinSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.Button button_stop;
        private System.Windows.Forms.TextBox textBox_sensorStatus;
        private System.Windows.Forms.ComboBox dropDown_fps;
        private System.Windows.Forms.Label txtLabel_fps;
        private System.Windows.Forms.PictureBox pictureBox_skeleton;
        private System.Windows.Forms.Button button_rec;
        private System.Windows.Forms.Button button_recStop;
        private System.Windows.Forms.CheckBox checkBox_colorCam;
        private TextFields textFields1;
        private System.Windows.Forms.GroupBox groupBox_smooth;
        private System.Windows.Forms.RadioButton radioButton_smoothIntense;
        private System.Windows.Forms.RadioButton radioButton_smoothModerate;
        private System.Windows.Forms.RadioButton radioButton_smoothDefault;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_init;
        private System.Windows.Forms.CheckBox checkBoxShowSkeleton;
        private System.Windows.Forms.GroupBox groupBoxActions;
        private System.Windows.Forms.Button buttonResetVirtualCamera;
        private System.Windows.Forms.Button buttonResetReconstruction;
        private System.Windows.Forms.CheckBox checkBoxUseCameraPoseFinder;
        private System.Windows.Forms.GroupBox groupBoxCreateMesh;
        private System.Windows.Forms.RadioButton radioButtonPly;
        private System.Windows.Forms.RadioButton radioButtonObj;
        private System.Windows.Forms.RadioButton radioButtonSTL;
        private System.Windows.Forms.GroupBox groupBoxImageOptions;
        private System.Windows.Forms.CheckBox checkBoxVolumeGraphics;
        private System.Windows.Forms.CheckBox checkBoxMirrorDepth;
        private System.Windows.Forms.CheckBox checkBoxNearMode;
        private System.Windows.Forms.CheckBox checkBoxKinectView;
        private System.Windows.Forms.CheckBox checkBoxPauseIntegration;
        private System.Windows.Forms.CheckBox checkBoxCaptureColor;
        private System.Windows.Forms.GroupBox groupBoxDepthThreshold;
        private System.Windows.Forms.Label labelDepthThresholdMax;
        private System.Windows.Forms.Label labelDepthThresholdMin;
        private System.Windows.Forms.TrackBar trackBarDepthThresholdMax;
        private System.Windows.Forms.TrackBar trackBarDepthThresholdMin;
        private System.Windows.Forms.GroupBox groupBoxVoxelInfo;
        private System.Windows.Forms.Label labelZAxis;
        private System.Windows.Forms.TrackBar trackBarZAxis;
        private System.Windows.Forms.Label labelYAxis;
        private System.Windows.Forms.TrackBar trackBarYAxis;
        private System.Windows.Forms.Label labelXAxis;
        private System.Windows.Forms.TrackBar trackBarXAxis;
        private System.Windows.Forms.Label labelVolumeVoxelsResolution;
        private System.Windows.Forms.Label labelVolumeVoxelsPerMeter;
        private System.Windows.Forms.TrackBar trackBarVolumeVoxelsPerMeter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar trackBarVolumeMaxIntegrationRate;
        private System.Windows.Forms.PictureBox pictureBox_depthPic;
        private System.Windows.Forms.PictureBox pictureBox_colorPic;
        private System.Windows.Forms.PictureBox pictureBox_depthPicSmoothed;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxDepthUseFiltering;
        private System.Windows.Forms.CheckBox checkBoxDepthUseAverage;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar trackBarDepthFramesToAverage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar trackBarDepthOuterBand;
        private System.Windows.Forms.TrackBar trackBarDepthInnerBand;
        private System.Windows.Forms.TextBox textBoxActualFramesPerSecond;
        private System.Windows.Forms.Label labelActualFramesPerSecond;
        private System.Windows.Forms.GroupBox groupBoxObjectSize;
        private System.Windows.Forms.Label labelObjectSizeMax;
        private System.Windows.Forms.Label labelObjectSizeMin;
        private System.Windows.Forms.TrackBar trackBarObjectMaxSize;
        private System.Windows.Forms.TrackBar trackBarObjectMinSize;
        private System.Windows.Forms.RichTextBox richTextBoxObjectFound;
        private System.Windows.Forms.Label labelObjectsFound;
        private System.Windows.Forms.Label labelDepthMaxDistance;
        private System.Windows.Forms.Label labelDepthMinDistance;
        private System.Windows.Forms.TrackBar trackBarDepthMaxDistance;
        private System.Windows.Forms.TrackBar trackBarDepthMinDistance;
        private System.Windows.Forms.Label labelObjectMaxSizeValue;
        private System.Windows.Forms.Label labelObjectMinSizeValue;
        private System.Windows.Forms.Label labelDepthMaxDistanceValue;
        private System.Windows.Forms.Label labelDepthMinDistanceValue;
    }
}


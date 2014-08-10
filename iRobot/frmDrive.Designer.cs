namespace iRobotKinect
{
    partial class frmDrive
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDrive));
            this.pBack = new System.Windows.Forms.PictureBox();
            this.pRotateRight = new System.Windows.Forms.PictureBox();
            this.pRotateLeft = new System.Windows.Forms.PictureBox();
            this.pFWD = new System.Windows.Forms.PictureBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblDirt_Detect_Right = new System.Windows.Forms.Label();
            this.lblDirt_Detect_Left = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.pVirtual_Wall = new System.Windows.Forms.PictureBox();
            this.pDriveLeft_Overcurrent = new System.Windows.Forms.PictureBox();
            this.pDriveRight_OverCurrent = new System.Windows.Forms.PictureBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.pWheelDrop_Caster = new System.Windows.Forms.PictureBox();
            this.label23 = new System.Windows.Forms.Label();
            this.pWheelDrop_Right = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pWheelDrop_Left = new System.Windows.Forms.PictureBox();
            this.pBump_Right = new System.Windows.Forms.PictureBox();
            this.label25 = new System.Windows.Forms.Label();
            this.pBump_Left = new System.Windows.Forms.PictureBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tSideBrush = new System.Windows.Forms.CheckBox();
            this.tVacuum = new System.Windows.Forms.CheckBox();
            this.tMain_Brush = new System.Windows.Forms.CheckBox();
            this.pCliffFrontRight = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pCliffRight = new System.Windows.Forms.PictureBox();
            this.pCliffFrontLeft = new System.Windows.Forms.PictureBox();
            this.pWallDetect = new System.Windows.Forms.PictureBox();
            this.pCliffLeft = new System.Windows.Forms.PictureBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.pMainBrush_Overcurrent = new System.Windows.Forms.PictureBox();
            this.pVacuum_OverCurrent = new System.Windows.Forms.PictureBox();
            this.pSideBrush_Overcurrent = new System.Windows.Forms.PictureBox();
            this.gMode = new System.Windows.Forms.GroupBox();
            this.lOff = new System.Windows.Forms.Label();
            this.lFull = new System.Windows.Forms.Label();
            this.lSafe = new System.Windows.Forms.Label();
            this.lPassive = new System.Windows.Forms.Label();
            this.udSpeed = new System.Windows.Forms.NumericUpDown();
            this.udRotate = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabDriveDisplay = new System.Windows.Forms.TabPage();
            this.lrSpeed = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.numericRotationBaseSpeed = new System.Windows.Forms.NumericUpDown();
            this.labelBaseRotationSpeed = new System.Windows.Forms.Label();
            this.labelBaseSpeed = new System.Windows.Forms.Label();
            this.numericBaseSpeed = new System.Windows.Forms.NumericUpDown();
            this.labelInstructions = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.chkAutoStraighten = new System.Windows.Forms.CheckBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.tabDriveSettings = new System.Windows.Forms.TabPage();
            this.gHardwareUI = new System.Windows.Forms.GroupBox();
            this.btnHome = new System.Windows.Forms.Button();
            this.pHome = new System.Windows.Forms.PictureBox();
            this.pMax = new System.Windows.Forms.PictureBox();
            this.pClean = new System.Windows.Forms.PictureBox();
            this.pSpot = new System.Windows.Forms.PictureBox();
            this.pPower = new System.Windows.Forms.PictureBox();
            this.btnMax = new System.Windows.Forms.Button();
            this.btnClean = new System.Windows.Forms.Button();
            this.btnSpot = new System.Windows.Forms.Button();
            this.btnPower = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.udRotateStep = new System.Windows.Forms.NumericUpDown();
            this.label19 = new System.Windows.Forms.Label();
            this.udSpeedStep = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chkDebugConnection = new System.Windows.Forms.CheckBox();
            this.udFormDisplay = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.chkAccurateSensors = new System.Windows.Forms.CheckBox();
            this.chkShowErrors = new System.Windows.Forms.CheckBox();
            this.lblError = new System.Windows.Forms.Label();
            this.lblSensorParse = new System.Windows.Forms.Label();
            this.cmRightClick = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.changeModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PASSIVEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SAFEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FULLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnApply = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pBack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pRotateRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pRotateLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pFWD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pVirtual_Wall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pDriveLeft_Overcurrent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pDriveRight_OverCurrent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pWheelDrop_Caster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pWheelDrop_Right)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pWheelDrop_Left)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBump_Right)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBump_Left)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pCliffFrontRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pCliffRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pCliffFrontLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pWallDetect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pCliffLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pMainBrush_Overcurrent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pVacuum_OverCurrent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pSideBrush_Overcurrent)).BeginInit();
            this.gMode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udRotate)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabDriveDisplay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lrSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericRotationBaseSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericBaseSpeed)).BeginInit();
            this.tabDriveSettings.SuspendLayout();
            this.gHardwareUI.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pHome)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pClean)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pSpot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pPower)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udRotateStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udSpeedStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udFormDisplay)).BeginInit();
            this.cmRightClick.SuspendLayout();
            this.SuspendLayout();
            // 
            // pBack
            // 
            this.pBack.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pBack.Location = new System.Drawing.Point(168, 249);
            this.pBack.Margin = new System.Windows.Forms.Padding(2);
            this.pBack.Name = "pBack";
            this.pBack.Size = new System.Drawing.Size(43, 11);
            this.pBack.TabIndex = 109;
            this.pBack.TabStop = false;
            // 
            // pRotateRight
            // 
            this.pRotateRight.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pRotateRight.Location = new System.Drawing.Point(222, 249);
            this.pRotateRight.Margin = new System.Windows.Forms.Padding(2);
            this.pRotateRight.Name = "pRotateRight";
            this.pRotateRight.Size = new System.Drawing.Size(43, 11);
            this.pRotateRight.TabIndex = 108;
            this.pRotateRight.TabStop = false;
            // 
            // pRotateLeft
            // 
            this.pRotateLeft.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pRotateLeft.Location = new System.Drawing.Point(115, 249);
            this.pRotateLeft.Margin = new System.Windows.Forms.Padding(2);
            this.pRotateLeft.Name = "pRotateLeft";
            this.pRotateLeft.Size = new System.Drawing.Size(43, 11);
            this.pRotateLeft.TabIndex = 107;
            this.pRotateLeft.TabStop = false;
            // 
            // pFWD
            // 
            this.pFWD.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pFWD.Location = new System.Drawing.Point(168, 220);
            this.pFWD.Margin = new System.Windows.Forms.Padding(2);
            this.pFWD.Name = "pFWD";
            this.pFWD.Size = new System.Drawing.Size(43, 11);
            this.pFWD.TabIndex = 106;
            this.pFWD.TabStop = false;
            // 
            // label17
            // 
            this.label17.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label17.Location = new System.Drawing.Point(211, 232);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(67, 13);
            this.label17.TabIndex = 105;
            this.label17.Text = "Rotate Right";
            // 
            // label16
            // 
            this.label16.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label16.Location = new System.Drawing.Point(107, 232);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(60, 13);
            this.label16.TabIndex = 104;
            this.label16.Text = "Rotate Left";
            // 
            // label15
            // 
            this.label15.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label15.Location = new System.Drawing.Point(173, 232);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(35, 13);
            this.label15.TabIndex = 103;
            this.label15.Text = "BACK";
            // 
            // label13
            // 
            this.label13.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label13.Location = new System.Drawing.Point(175, 205);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(32, 13);
            this.label13.TabIndex = 101;
            this.label13.Text = "FWD";
            // 
            // lblDirt_Detect_Right
            // 
            this.lblDirt_Detect_Right.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDirt_Detect_Right.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDirt_Detect_Right.Location = new System.Drawing.Point(204, 154);
            this.lblDirt_Detect_Right.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDirt_Detect_Right.Name = "lblDirt_Detect_Right";
            this.lblDirt_Detect_Right.Size = new System.Drawing.Size(51, 11);
            this.lblDirt_Detect_Right.TabIndex = 99;
            // 
            // lblDirt_Detect_Left
            // 
            this.lblDirt_Detect_Left.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDirt_Detect_Left.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDirt_Detect_Left.Location = new System.Drawing.Point(121, 154);
            this.lblDirt_Detect_Left.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDirt_Detect_Left.Name = "lblDirt_Detect_Left";
            this.lblDirt_Detect_Left.Size = new System.Drawing.Size(51, 11);
            this.lblDirt_Detect_Left.TabIndex = 83;
            // 
            // label28
            // 
            this.label28.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label28.AutoSize = true;
            this.label28.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label28.Location = new System.Drawing.Point(156, 17);
            this.label28.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(69, 13);
            this.label28.TabIndex = 95;
            this.label28.Text = "Virtual Wall ^";
            // 
            // pVirtual_Wall
            // 
            this.pVirtual_Wall.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pVirtual_Wall.Location = new System.Drawing.Point(44, 4);
            this.pVirtual_Wall.Margin = new System.Windows.Forms.Padding(2);
            this.pVirtual_Wall.Name = "pVirtual_Wall";
            this.pVirtual_Wall.Size = new System.Drawing.Size(324, 10);
            this.pVirtual_Wall.TabIndex = 94;
            this.pVirtual_Wall.TabStop = false;
            // 
            // pDriveLeft_Overcurrent
            // 
            this.pDriveLeft_Overcurrent.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pDriveLeft_Overcurrent.Location = new System.Drawing.Point(63, 250);
            this.pDriveLeft_Overcurrent.Margin = new System.Windows.Forms.Padding(2);
            this.pDriveLeft_Overcurrent.Name = "pDriveLeft_Overcurrent";
            this.pDriveLeft_Overcurrent.Size = new System.Drawing.Size(25, 10);
            this.pDriveLeft_Overcurrent.TabIndex = 90;
            this.pDriveLeft_Overcurrent.TabStop = false;
            // 
            // pDriveRight_OverCurrent
            // 
            this.pDriveRight_OverCurrent.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pDriveRight_OverCurrent.Location = new System.Drawing.Point(300, 256);
            this.pDriveRight_OverCurrent.Margin = new System.Windows.Forms.Padding(2);
            this.pDriveRight_OverCurrent.Name = "pDriveRight_OverCurrent";
            this.pDriveRight_OverCurrent.Size = new System.Drawing.Size(25, 10);
            this.pDriveRight_OverCurrent.TabIndex = 89;
            this.pDriveRight_OverCurrent.TabStop = false;
            // 
            // label27
            // 
            this.label27.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label27.AutoSize = true;
            this.label27.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label27.Location = new System.Drawing.Point(190, 139);
            this.label27.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(86, 13);
            this.label27.TabIndex = 88;
            this.label27.Text = "Dirt Detect Right";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label21
            // 
            this.label21.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label21.AutoSize = true;
            this.label21.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label21.Location = new System.Drawing.Point(109, 138);
            this.label21.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(79, 13);
            this.label21.TabIndex = 86;
            this.label21.Text = "Dirt Detect Left";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pWheelDrop_Caster
            // 
            this.pWheelDrop_Caster.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pWheelDrop_Caster.Location = new System.Drawing.Point(178, 116);
            this.pWheelDrop_Caster.Margin = new System.Windows.Forms.Padding(2);
            this.pWheelDrop_Caster.Name = "pWheelDrop_Caster";
            this.pWheelDrop_Caster.Size = new System.Drawing.Size(25, 12);
            this.pWheelDrop_Caster.TabIndex = 83;
            this.pWheelDrop_Caster.TabStop = false;
            // 
            // label23
            // 
            this.label23.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label23.AutoSize = true;
            this.label23.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label23.Location = new System.Drawing.Point(292, 95);
            this.label23.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(52, 13);
            this.label23.TabIndex = 79;
            this.label23.Text = "Cliff Right";
            // 
            // pWheelDrop_Right
            // 
            this.pWheelDrop_Right.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pWheelDrop_Right.Location = new System.Drawing.Point(300, 239);
            this.pWheelDrop_Right.Margin = new System.Windows.Forms.Padding(2);
            this.pWheelDrop_Right.Name = "pWheelDrop_Right";
            this.pWheelDrop_Right.Size = new System.Drawing.Size(25, 16);
            this.pWheelDrop_Right.TabIndex = 80;
            this.pWheelDrop_Right.TabStop = false;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label5.Location = new System.Drawing.Point(282, 199);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 29);
            this.label5.TabIndex = 79;
            this.label5.Text = "WheelDrop Right";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pWheelDrop_Left
            // 
            this.pWheelDrop_Left.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pWheelDrop_Left.Location = new System.Drawing.Point(63, 233);
            this.pWheelDrop_Left.Margin = new System.Windows.Forms.Padding(2);
            this.pWheelDrop_Left.Name = "pWheelDrop_Left";
            this.pWheelDrop_Left.Size = new System.Drawing.Size(25, 16);
            this.pWheelDrop_Left.TabIndex = 78;
            this.pWheelDrop_Left.TabStop = false;
            // 
            // pBump_Right
            // 
            this.pBump_Right.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pBump_Right.Location = new System.Drawing.Point(249, 48);
            this.pBump_Right.Margin = new System.Windows.Forms.Padding(2);
            this.pBump_Right.Name = "pBump_Right";
            this.pBump_Right.Size = new System.Drawing.Size(43, 11);
            this.pBump_Right.TabIndex = 73;
            this.pBump_Right.TabStop = false;
            // 
            // label25
            // 
            this.label25.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label25.AutoSize = true;
            this.label25.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label25.Location = new System.Drawing.Point(243, 31);
            this.label25.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(62, 13);
            this.label25.TabIndex = 72;
            this.label25.Text = "Bump Right";
            // 
            // pBump_Left
            // 
            this.pBump_Left.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pBump_Left.Location = new System.Drawing.Point(76, 48);
            this.pBump_Left.Margin = new System.Windows.Forms.Padding(2);
            this.pBump_Left.Name = "pBump_Left";
            this.pBump_Left.Size = new System.Drawing.Size(43, 11);
            this.pBump_Left.TabIndex = 71;
            this.pBump_Left.TabStop = false;
            // 
            // label24
            // 
            this.label24.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label24.AutoSize = true;
            this.label24.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label24.Location = new System.Drawing.Point(238, 65);
            this.label24.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(79, 13);
            this.label24.TabIndex = 77;
            this.label24.Text = "Cliff Front Right";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label7.Location = new System.Drawing.Point(84, 66);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 13);
            this.label7.TabIndex = 75;
            this.label7.Text = "Cliff Front Left";
            // 
            // label22
            // 
            this.label22.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label22.AutoSize = true;
            this.label22.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label22.Location = new System.Drawing.Point(52, 95);
            this.label22.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(45, 13);
            this.label22.TabIndex = 73;
            this.label22.Text = "Cliff Left";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label6.Location = new System.Drawing.Point(39, 199);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 29);
            this.label6.TabIndex = 49;
            this.label6.Text = "WheelDrop Left";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tSideBrush
            // 
            this.tSideBrush.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tSideBrush.Appearance = System.Windows.Forms.Appearance.Button;
            this.tSideBrush.AutoSize = true;
            this.tSideBrush.Location = new System.Drawing.Point(291, 129);
            this.tSideBrush.Margin = new System.Windows.Forms.Padding(2);
            this.tSideBrush.Name = "tSideBrush";
            this.tSideBrush.Size = new System.Drawing.Size(68, 23);
            this.tSideBrush.TabIndex = 0;
            this.tSideBrush.TabStop = false;
            this.tSideBrush.Text = "Side Brush";
            this.tSideBrush.UseVisualStyleBackColor = true;
            this.tSideBrush.CheckedChanged += new System.EventHandler(this.tSideBrush_CheckedChanged);
            this.tSideBrush.Leave += new System.EventHandler(this.tSideBrush_Leave);
            // 
            // tVacuum
            // 
            this.tVacuum.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tVacuum.Appearance = System.Windows.Forms.Appearance.Button;
            this.tVacuum.Location = new System.Drawing.Point(155, 278);
            this.tVacuum.Margin = new System.Windows.Forms.Padding(2);
            this.tVacuum.Name = "tVacuum";
            this.tVacuum.Size = new System.Drawing.Size(75, 20);
            this.tVacuum.TabIndex = 0;
            this.tVacuum.TabStop = false;
            this.tVacuum.Text = "Vacuum";
            this.tVacuum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.tVacuum.UseVisualStyleBackColor = true;
            this.tVacuum.CheckedChanged += new System.EventHandler(this.tVacuum_CheckedChanged);
            this.tVacuum.Leave += new System.EventHandler(this.tVacuum_Leave);
            // 
            // tMain_Brush
            // 
            this.tMain_Brush.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tMain_Brush.Appearance = System.Windows.Forms.Appearance.Button;
            this.tMain_Brush.Location = new System.Drawing.Point(154, 170);
            this.tMain_Brush.Margin = new System.Windows.Forms.Padding(2);
            this.tMain_Brush.Name = "tMain_Brush";
            this.tMain_Brush.Size = new System.Drawing.Size(75, 21);
            this.tMain_Brush.TabIndex = 0;
            this.tMain_Brush.TabStop = false;
            this.tMain_Brush.Text = "Main Brush";
            this.tMain_Brush.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.tMain_Brush.UseVisualStyleBackColor = true;
            this.tMain_Brush.CheckedChanged += new System.EventHandler(this.tMain_Brush_CheckedChanged);
            this.tMain_Brush.Leave += new System.EventHandler(this.tMain_Brush_Leave);
            // 
            // pCliffFrontRight
            // 
            this.pCliffFrontRight.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pCliffFrontRight.Location = new System.Drawing.Point(250, 81);
            this.pCliffFrontRight.Margin = new System.Windows.Forms.Padding(2);
            this.pCliffFrontRight.Name = "pCliffFrontRight";
            this.pCliffFrontRight.Size = new System.Drawing.Size(43, 11);
            this.pCliffFrontRight.TabIndex = 43;
            this.pCliffFrontRight.TabStop = false;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label4.Location = new System.Drawing.Point(74, 31);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 40;
            this.label4.Text = "Bump Left";
            // 
            // pCliffRight
            // 
            this.pCliffRight.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pCliffRight.Location = new System.Drawing.Point(296, 110);
            this.pCliffRight.Margin = new System.Windows.Forms.Padding(2);
            this.pCliffRight.Name = "pCliffRight";
            this.pCliffRight.Size = new System.Drawing.Size(43, 11);
            this.pCliffRight.TabIndex = 44;
            this.pCliffRight.TabStop = false;
            // 
            // pCliffFrontLeft
            // 
            this.pCliffFrontLeft.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pCliffFrontLeft.Location = new System.Drawing.Point(101, 82);
            this.pCliffFrontLeft.Margin = new System.Windows.Forms.Padding(2);
            this.pCliffFrontLeft.Name = "pCliffFrontLeft";
            this.pCliffFrontLeft.Size = new System.Drawing.Size(43, 11);
            this.pCliffFrontLeft.TabIndex = 46;
            this.pCliffFrontLeft.TabStop = false;
            // 
            // pWallDetect
            // 
            this.pWallDetect.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pWallDetect.Location = new System.Drawing.Point(248, 107);
            this.pWallDetect.Margin = new System.Windows.Forms.Padding(2);
            this.pWallDetect.Name = "pWallDetect";
            this.pWallDetect.Size = new System.Drawing.Size(19, 8);
            this.pWallDetect.TabIndex = 48;
            this.pWallDetect.TabStop = false;
            // 
            // pCliffLeft
            // 
            this.pCliffLeft.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pCliffLeft.Location = new System.Drawing.Point(54, 110);
            this.pCliffLeft.Margin = new System.Windows.Forms.Padding(2);
            this.pCliffLeft.Name = "pCliffLeft";
            this.pCliffLeft.Size = new System.Drawing.Size(43, 11);
            this.pCliffLeft.TabIndex = 47;
            this.pCliffLeft.TabStop = false;
            // 
            // label26
            // 
            this.label26.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label26.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label26.Location = new System.Drawing.Point(161, 85);
            this.label26.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(60, 29);
            this.label26.TabIndex = 82;
            this.label26.Text = "WheelDrop Caster";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label8.Location = new System.Drawing.Point(244, 93);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(28, 13);
            this.label8.TabIndex = 84;
            this.label8.Text = "Wall";
            // 
            // pMainBrush_Overcurrent
            // 
            this.pMainBrush_Overcurrent.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pMainBrush_Overcurrent.Location = new System.Drawing.Point(154, 190);
            this.pMainBrush_Overcurrent.Margin = new System.Windows.Forms.Padding(2);
            this.pMainBrush_Overcurrent.Name = "pMainBrush_Overcurrent";
            this.pMainBrush_Overcurrent.Size = new System.Drawing.Size(75, 10);
            this.pMainBrush_Overcurrent.TabIndex = 93;
            this.pMainBrush_Overcurrent.TabStop = false;
            // 
            // pVacuum_OverCurrent
            // 
            this.pVacuum_OverCurrent.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pVacuum_OverCurrent.Location = new System.Drawing.Point(155, 297);
            this.pVacuum_OverCurrent.Margin = new System.Windows.Forms.Padding(2);
            this.pVacuum_OverCurrent.Name = "pVacuum_OverCurrent";
            this.pVacuum_OverCurrent.Size = new System.Drawing.Size(75, 11);
            this.pVacuum_OverCurrent.TabIndex = 92;
            this.pVacuum_OverCurrent.TabStop = false;
            // 
            // pSideBrush_Overcurrent
            // 
            this.pSideBrush_Overcurrent.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pSideBrush_Overcurrent.Location = new System.Drawing.Point(291, 153);
            this.pSideBrush_Overcurrent.Margin = new System.Windows.Forms.Padding(2);
            this.pSideBrush_Overcurrent.Name = "pSideBrush_Overcurrent";
            this.pSideBrush_Overcurrent.Size = new System.Drawing.Size(65, 11);
            this.pSideBrush_Overcurrent.TabIndex = 91;
            this.pSideBrush_Overcurrent.TabStop = false;
            // 
            // gMode
            // 
            this.gMode.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.gMode.Controls.Add(this.lOff);
            this.gMode.Controls.Add(this.lFull);
            this.gMode.Controls.Add(this.lSafe);
            this.gMode.Controls.Add(this.lPassive);
            this.gMode.Location = new System.Drawing.Point(369, 177);
            this.gMode.Margin = new System.Windows.Forms.Padding(2);
            this.gMode.Name = "gMode";
            this.gMode.Padding = new System.Windows.Forms.Padding(2);
            this.gMode.Size = new System.Drawing.Size(70, 89);
            this.gMode.TabIndex = 310;
            this.gMode.TabStop = false;
            this.gMode.Text = "Mode";
            this.toolTip1.SetToolTip(this.gMode, "Double click a mode to change mode");
            // 
            // lOff
            // 
            this.lOff.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lOff.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lOff.Location = new System.Drawing.Point(4, 18);
            this.lOff.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lOff.Name = "lOff";
            this.lOff.Size = new System.Drawing.Size(64, 14);
            this.lOff.TabIndex = 311;
            this.lOff.Text = "OFF";
            this.lOff.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lFull
            // 
            this.lFull.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lFull.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lFull.Location = new System.Drawing.Point(4, 67);
            this.lFull.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lFull.Name = "lFull";
            this.lFull.Size = new System.Drawing.Size(64, 14);
            this.lFull.TabIndex = 312;
            this.lFull.Text = "FULL";
            this.lFull.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lFull.DoubleClick += new System.EventHandler(this.lFull_DoubleClick);
            this.lFull.MouseLeave += new System.EventHandler(this.lFull_MouseLeave);
            this.lFull.MouseHover += new System.EventHandler(this.lFull_MouseHover);
            // 
            // lSafe
            // 
            this.lSafe.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lSafe.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lSafe.Location = new System.Drawing.Point(4, 51);
            this.lSafe.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lSafe.Name = "lSafe";
            this.lSafe.Size = new System.Drawing.Size(64, 14);
            this.lSafe.TabIndex = 314;
            this.lSafe.Text = "SAFE";
            this.lSafe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lSafe.DoubleClick += new System.EventHandler(this.lSafe_DoubleClick);
            this.lSafe.MouseLeave += new System.EventHandler(this.lSafe_MouseLeave);
            this.lSafe.MouseHover += new System.EventHandler(this.lSafe_MouseHover);
            // 
            // lPassive
            // 
            this.lPassive.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lPassive.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lPassive.Location = new System.Drawing.Point(4, 35);
            this.lPassive.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lPassive.Name = "lPassive";
            this.lPassive.Size = new System.Drawing.Size(64, 14);
            this.lPassive.TabIndex = 313;
            this.lPassive.Text = "PASSIVE";
            this.lPassive.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lPassive.DoubleClick += new System.EventHandler(this.lPassive_DoubleClick);
            this.lPassive.MouseLeave += new System.EventHandler(this.lPassive_MouseLeave);
            this.lPassive.MouseHover += new System.EventHandler(this.lPassive_MouseHover);
            // 
            // udSpeed
            // 
            this.udSpeed.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.udSpeed.Enabled = false;
            this.udSpeed.Location = new System.Drawing.Point(418, 313);
            this.udSpeed.Margin = new System.Windows.Forms.Padding(2);
            this.udSpeed.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.udSpeed.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            -2147483648});
            this.udSpeed.Name = "udSpeed";
            this.udSpeed.Size = new System.Drawing.Size(39, 20);
            this.udSpeed.TabIndex = 61;
            this.udSpeed.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.udSpeed_MouseDoubleClick);
            // 
            // udRotate
            // 
            this.udRotate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.udRotate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.udRotate.Enabled = false;
            this.udRotate.Location = new System.Drawing.Point(396, 369);
            this.udRotate.Margin = new System.Windows.Forms.Padding(2);
            this.udRotate.Maximum = new decimal(new int[] {
            32768,
            0,
            0,
            0});
            this.udRotate.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.udRotate.Name = "udRotate";
            this.udRotate.Size = new System.Drawing.Size(62, 20);
            this.udRotate.TabIndex = 60;
            this.udRotate.Value = new decimal(new int[] {
            32768,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label10.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label10.Location = new System.Drawing.Point(374, 15);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 128);
            this.label10.TabIndex = 100;
            this.label10.Text = "if Roomba keeps snapping to OFF mode, reposition and make sure all sensors are cl" +
    "ear";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabDriveDisplay);
            this.tabControl1.Controls.Add(this.tabDriveSettings);
            this.tabControl1.Location = new System.Drawing.Point(3, 25);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(469, 447);
            this.tabControl1.TabIndex = 297;
            // 
            // tabDriveDisplay
            // 
            this.tabDriveDisplay.Controls.Add(this.lrSpeed);
            this.tabDriveDisplay.Controls.Add(this.label12);
            this.tabDriveDisplay.Controls.Add(this.numericRotationBaseSpeed);
            this.tabDriveDisplay.Controls.Add(this.labelBaseRotationSpeed);
            this.tabDriveDisplay.Controls.Add(this.labelBaseSpeed);
            this.tabDriveDisplay.Controls.Add(this.numericBaseSpeed);
            this.tabDriveDisplay.Controls.Add(this.labelInstructions);
            this.tabDriveDisplay.Controls.Add(this.label10);
            this.tabDriveDisplay.Controls.Add(this.label30);
            this.tabDriveDisplay.Controls.Add(this.chkAutoStraighten);
            this.tabDriveDisplay.Controls.Add(this.label18);
            this.tabDriveDisplay.Controls.Add(this.label14);
            this.tabDriveDisplay.Controls.Add(this.pBack);
            this.tabDriveDisplay.Controls.Add(this.gMode);
            this.tabDriveDisplay.Controls.Add(this.pRotateRight);
            this.tabDriveDisplay.Controls.Add(this.pRotateLeft);
            this.tabDriveDisplay.Controls.Add(this.udRotate);
            this.tabDriveDisplay.Controls.Add(this.pFWD);
            this.tabDriveDisplay.Controls.Add(this.label17);
            this.tabDriveDisplay.Controls.Add(this.udSpeed);
            this.tabDriveDisplay.Controls.Add(this.tMain_Brush);
            this.tabDriveDisplay.Controls.Add(this.label16);
            this.tabDriveDisplay.Controls.Add(this.label24);
            this.tabDriveDisplay.Controls.Add(this.label7);
            this.tabDriveDisplay.Controls.Add(this.label15);
            this.tabDriveDisplay.Controls.Add(this.pBump_Left);
            this.tabDriveDisplay.Controls.Add(this.label13);
            this.tabDriveDisplay.Controls.Add(this.label22);
            this.tabDriveDisplay.Controls.Add(this.label25);
            this.tabDriveDisplay.Controls.Add(this.label6);
            this.tabDriveDisplay.Controls.Add(this.lblDirt_Detect_Right);
            this.tabDriveDisplay.Controls.Add(this.pBump_Right);
            this.tabDriveDisplay.Controls.Add(this.pSideBrush_Overcurrent);
            this.tabDriveDisplay.Controls.Add(this.tSideBrush);
            this.tabDriveDisplay.Controls.Add(this.lblDirt_Detect_Left);
            this.tabDriveDisplay.Controls.Add(this.pWheelDrop_Left);
            this.tabDriveDisplay.Controls.Add(this.pVacuum_OverCurrent);
            this.tabDriveDisplay.Controls.Add(this.tVacuum);
            this.tabDriveDisplay.Controls.Add(this.label28);
            this.tabDriveDisplay.Controls.Add(this.label5);
            this.tabDriveDisplay.Controls.Add(this.pMainBrush_Overcurrent);
            this.tabDriveDisplay.Controls.Add(this.pCliffFrontRight);
            this.tabDriveDisplay.Controls.Add(this.pVirtual_Wall);
            this.tabDriveDisplay.Controls.Add(this.pWheelDrop_Right);
            this.tabDriveDisplay.Controls.Add(this.label8);
            this.tabDriveDisplay.Controls.Add(this.label4);
            this.tabDriveDisplay.Controls.Add(this.pDriveLeft_Overcurrent);
            this.tabDriveDisplay.Controls.Add(this.label23);
            this.tabDriveDisplay.Controls.Add(this.label26);
            this.tabDriveDisplay.Controls.Add(this.pCliffRight);
            this.tabDriveDisplay.Controls.Add(this.pDriveRight_OverCurrent);
            this.tabDriveDisplay.Controls.Add(this.pWheelDrop_Caster);
            this.tabDriveDisplay.Controls.Add(this.pCliffLeft);
            this.tabDriveDisplay.Controls.Add(this.pCliffFrontLeft);
            this.tabDriveDisplay.Controls.Add(this.label27);
            this.tabDriveDisplay.Controls.Add(this.label21);
            this.tabDriveDisplay.Controls.Add(this.pWallDetect);
            this.tabDriveDisplay.Location = new System.Drawing.Point(4, 22);
            this.tabDriveDisplay.Margin = new System.Windows.Forms.Padding(2);
            this.tabDriveDisplay.Name = "tabDriveDisplay";
            this.tabDriveDisplay.Padding = new System.Windows.Forms.Padding(2);
            this.tabDriveDisplay.Size = new System.Drawing.Size(461, 421);
            this.tabDriveDisplay.TabIndex = 0;
            this.tabDriveDisplay.Text = "Display";
            this.tabDriveDisplay.UseVisualStyleBackColor = true;
            // 
            // lrSpeed
            // 
            this.lrSpeed.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lrSpeed.Enabled = false;
            this.lrSpeed.Location = new System.Drawing.Point(417, 341);
            this.lrSpeed.Margin = new System.Windows.Forms.Padding(2);
            this.lrSpeed.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.lrSpeed.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            -2147483648});
            this.lrSpeed.Name = "lrSpeed";
            this.lrSpeed.Size = new System.Drawing.Size(39, 20);
            this.lrSpeed.TabIndex = 327;
            this.lrSpeed.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label12.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label12.Location = new System.Drawing.Point(312, 341);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(104, 17);
            this.label12.TabIndex = 326;
            this.label12.Text = "ROTATION SPEED";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericRotationBaseSpeed
            // 
            this.numericRotationBaseSpeed.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.numericRotationBaseSpeed.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericRotationBaseSpeed.Location = new System.Drawing.Point(253, 338);
            this.numericRotationBaseSpeed.Margin = new System.Windows.Forms.Padding(2);
            this.numericRotationBaseSpeed.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericRotationBaseSpeed.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.numericRotationBaseSpeed.Name = "numericRotationBaseSpeed";
            this.numericRotationBaseSpeed.Size = new System.Drawing.Size(52, 20);
            this.numericRotationBaseSpeed.TabIndex = 325;
            this.numericRotationBaseSpeed.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.numericRotationBaseSpeed.ValueChanged += new System.EventHandler(this.numericRotationBaseSpeed_ValueChanged);
            // 
            // labelBaseRotationSpeed
            // 
            this.labelBaseRotationSpeed.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelBaseRotationSpeed.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelBaseRotationSpeed.Location = new System.Drawing.Point(137, 339);
            this.labelBaseRotationSpeed.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelBaseRotationSpeed.Name = "labelBaseRotationSpeed";
            this.labelBaseRotationSpeed.Size = new System.Drawing.Size(113, 18);
            this.labelBaseRotationSpeed.TabIndex = 324;
            this.labelBaseRotationSpeed.Text = "Base Rotation Speed";
            this.labelBaseRotationSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelBaseSpeed
            // 
            this.labelBaseSpeed.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelBaseSpeed.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelBaseSpeed.Location = new System.Drawing.Point(183, 314);
            this.labelBaseSpeed.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelBaseSpeed.Name = "labelBaseSpeed";
            this.labelBaseSpeed.Size = new System.Drawing.Size(66, 18);
            this.labelBaseSpeed.TabIndex = 323;
            this.labelBaseSpeed.Text = "Base Speed";
            this.labelBaseSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericBaseSpeed
            // 
            this.numericBaseSpeed.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.numericBaseSpeed.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericBaseSpeed.Location = new System.Drawing.Point(253, 313);
            this.numericBaseSpeed.Margin = new System.Windows.Forms.Padding(2);
            this.numericBaseSpeed.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericBaseSpeed.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.numericBaseSpeed.Name = "numericBaseSpeed";
            this.numericBaseSpeed.Size = new System.Drawing.Size(52, 20);
            this.numericBaseSpeed.TabIndex = 322;
            this.numericBaseSpeed.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numericBaseSpeed.ValueChanged += new System.EventHandler(this.numericBaseSpeed_ValueChanged);
            // 
            // labelInstructions
            // 
            this.labelInstructions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelInstructions.Location = new System.Drawing.Point(4, 310);
            this.labelInstructions.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelInstructions.Name = "labelInstructions";
            this.labelInstructions.Size = new System.Drawing.Size(109, 96);
            this.labelInstructions.TabIndex = 321;
            this.labelInstructions.Text = "KEYS: Up-Forwards  Down-Back   Right   + Increase speed   Down decrease speed    " +
    "Space Bar to stop";
            this.labelInstructions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label30
            // 
            this.label30.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label30.AutoSize = true;
            this.label30.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label30.Location = new System.Drawing.Point(152, 263);
            this.label30.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(90, 13);
            this.label30.TabIndex = 318;
            this.label30.Text = "Spacebar to Stop";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkAutoStraighten
            // 
            this.chkAutoStraighten.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAutoStraighten.Checked = true;
            this.chkAutoStraighten.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoStraighten.Enabled = false;
            this.chkAutoStraighten.Location = new System.Drawing.Point(335, 401);
            this.chkAutoStraighten.Margin = new System.Windows.Forms.Padding(2);
            this.chkAutoStraighten.Name = "chkAutoStraighten";
            this.chkAutoStraighten.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkAutoStraighten.Size = new System.Drawing.Size(108, 18);
            this.chkAutoStraighten.TabIndex = 317;
            this.chkAutoStraighten.Text = "Auto-straighten";
            this.chkAutoStraighten.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            this.label18.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label18.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label18.Location = new System.Drawing.Point(331, 371);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(64, 14);
            this.label18.TabIndex = 316;
            this.label18.Text = "ROTATION";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label14.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label14.Location = new System.Drawing.Point(362, 316);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(54, 14);
            this.label14.TabIndex = 315;
            this.label14.Text = "SPEED";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabDriveSettings
            // 
            this.tabDriveSettings.Controls.Add(this.gHardwareUI);
            this.tabDriveSettings.Controls.Add(this.label9);
            this.tabDriveSettings.Controls.Add(this.label29);
            this.tabDriveSettings.Controls.Add(this.label20);
            this.tabDriveSettings.Controls.Add(this.udRotateStep);
            this.tabDriveSettings.Controls.Add(this.label19);
            this.tabDriveSettings.Controls.Add(this.udSpeedStep);
            this.tabDriveSettings.Controls.Add(this.label3);
            this.tabDriveSettings.Controls.Add(this.label2);
            this.tabDriveSettings.Controls.Add(this.chkDebugConnection);
            this.tabDriveSettings.Controls.Add(this.udFormDisplay);
            this.tabDriveSettings.Controls.Add(this.label1);
            this.tabDriveSettings.Controls.Add(this.chkAccurateSensors);
            this.tabDriveSettings.Location = new System.Drawing.Point(4, 22);
            this.tabDriveSettings.Margin = new System.Windows.Forms.Padding(2);
            this.tabDriveSettings.Name = "tabDriveSettings";
            this.tabDriveSettings.Padding = new System.Windows.Forms.Padding(2);
            this.tabDriveSettings.Size = new System.Drawing.Size(461, 421);
            this.tabDriveSettings.TabIndex = 1;
            this.tabDriveSettings.Text = "Settings";
            this.tabDriveSettings.UseVisualStyleBackColor = true;
            // 
            // gHardwareUI
            // 
            this.gHardwareUI.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.gHardwareUI.Controls.Add(this.btnHome);
            this.gHardwareUI.Controls.Add(this.pHome);
            this.gHardwareUI.Controls.Add(this.pMax);
            this.gHardwareUI.Controls.Add(this.pClean);
            this.gHardwareUI.Controls.Add(this.pSpot);
            this.gHardwareUI.Controls.Add(this.pPower);
            this.gHardwareUI.Controls.Add(this.btnMax);
            this.gHardwareUI.Controls.Add(this.btnClean);
            this.gHardwareUI.Controls.Add(this.btnSpot);
            this.gHardwareUI.Controls.Add(this.btnPower);
            this.gHardwareUI.Location = new System.Drawing.Point(22, 180);
            this.gHardwareUI.Margin = new System.Windows.Forms.Padding(2);
            this.gHardwareUI.Name = "gHardwareUI";
            this.gHardwareUI.Padding = new System.Windows.Forms.Padding(2);
            this.gHardwareUI.Size = new System.Drawing.Size(84, 147);
            this.gHardwareUI.TabIndex = 325;
            this.gHardwareUI.TabStop = false;
            this.gHardwareUI.Text = "UI";
            // 
            // btnHome
            // 
            this.btnHome.Location = new System.Drawing.Point(20, 115);
            this.btnHome.Margin = new System.Windows.Forms.Padding(2);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(60, 20);
            this.btnHome.TabIndex = 101;
            this.btnHome.Text = "Home";
            this.btnHome.UseVisualStyleBackColor = true;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // pHome
            // 
            this.pHome.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pHome.Location = new System.Drawing.Point(13, 115);
            this.pHome.Margin = new System.Windows.Forms.Padding(2);
            this.pHome.Name = "pHome";
            this.pHome.Size = new System.Drawing.Size(8, 20);
            this.pHome.TabIndex = 100;
            this.pHome.TabStop = false;
            // 
            // pMax
            // 
            this.pMax.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pMax.Location = new System.Drawing.Point(13, 91);
            this.pMax.Margin = new System.Windows.Forms.Padding(2);
            this.pMax.Name = "pMax";
            this.pMax.Size = new System.Drawing.Size(8, 20);
            this.pMax.TabIndex = 99;
            this.pMax.TabStop = false;
            // 
            // pClean
            // 
            this.pClean.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pClean.Location = new System.Drawing.Point(13, 67);
            this.pClean.Margin = new System.Windows.Forms.Padding(2);
            this.pClean.Name = "pClean";
            this.pClean.Size = new System.Drawing.Size(8, 20);
            this.pClean.TabIndex = 98;
            this.pClean.TabStop = false;
            // 
            // pSpot
            // 
            this.pSpot.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pSpot.Location = new System.Drawing.Point(13, 42);
            this.pSpot.Margin = new System.Windows.Forms.Padding(2);
            this.pSpot.Name = "pSpot";
            this.pSpot.Size = new System.Drawing.Size(8, 20);
            this.pSpot.TabIndex = 97;
            this.pSpot.TabStop = false;
            // 
            // pPower
            // 
            this.pPower.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pPower.Location = new System.Drawing.Point(13, 18);
            this.pPower.Margin = new System.Windows.Forms.Padding(2);
            this.pPower.Name = "pPower";
            this.pPower.Size = new System.Drawing.Size(8, 20);
            this.pPower.TabIndex = 96;
            this.pPower.TabStop = false;
            // 
            // btnMax
            // 
            this.btnMax.Location = new System.Drawing.Point(20, 91);
            this.btnMax.Margin = new System.Windows.Forms.Padding(2);
            this.btnMax.Name = "btnMax";
            this.btnMax.Size = new System.Drawing.Size(60, 20);
            this.btnMax.TabIndex = 38;
            this.btnMax.Text = "Max";
            this.btnMax.UseVisualStyleBackColor = true;
            this.btnMax.Click += new System.EventHandler(this.btnMax_Click);
            // 
            // btnClean
            // 
            this.btnClean.Location = new System.Drawing.Point(20, 66);
            this.btnClean.Margin = new System.Windows.Forms.Padding(2);
            this.btnClean.Name = "btnClean";
            this.btnClean.Size = new System.Drawing.Size(60, 20);
            this.btnClean.TabIndex = 37;
            this.btnClean.Text = "Clean";
            this.btnClean.UseVisualStyleBackColor = true;
            this.btnClean.Click += new System.EventHandler(this.btnClean_Click);
            // 
            // btnSpot
            // 
            this.btnSpot.Location = new System.Drawing.Point(20, 42);
            this.btnSpot.Margin = new System.Windows.Forms.Padding(2);
            this.btnSpot.Name = "btnSpot";
            this.btnSpot.Size = new System.Drawing.Size(60, 20);
            this.btnSpot.TabIndex = 36;
            this.btnSpot.Text = "Spot";
            this.btnSpot.UseVisualStyleBackColor = true;
            this.btnSpot.Click += new System.EventHandler(this.btnSpot_Click);
            // 
            // btnPower
            // 
            this.btnPower.Location = new System.Drawing.Point(20, 17);
            this.btnPower.Margin = new System.Windows.Forms.Padding(2);
            this.btnPower.Name = "btnPower";
            this.btnPower.Size = new System.Drawing.Size(60, 20);
            this.btnPower.TabIndex = 29;
            this.btnPower.Text = "Power";
            this.btnPower.UseVisualStyleBackColor = true;
            this.btnPower.Click += new System.EventHandler(this.btnPower_Click);
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label9.Location = new System.Drawing.Point(49, 102);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(309, 13);
            this.label9.TabIndex = 100;
            this.label9.Text = "Roomba needs to apply Rotation AND speed for ALL movement";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label9.Visible = false;
            // 
            // label29
            // 
            this.label29.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label29.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label29.Location = new System.Drawing.Point(286, 287);
            this.label29.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(110, 14);
            this.label29.TabIndex = 324;
            this.label29.Text = "KEYBOARD DRIVING";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label20
            // 
            this.label20.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label20.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label20.Location = new System.Drawing.Point(257, 330);
            this.label20.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(91, 14);
            this.label20.TabIndex = 323;
            this.label20.Text = "Rotate Increment";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // udRotateStep
            // 
            this.udRotateStep.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.udRotateStep.Location = new System.Drawing.Point(351, 328);
            this.udRotateStep.Margin = new System.Windows.Forms.Padding(2);
            this.udRotateStep.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.udRotateStep.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            -2147483648});
            this.udRotateStep.Name = "udRotateStep";
            this.udRotateStep.Size = new System.Drawing.Size(52, 20);
            this.udRotateStep.TabIndex = 322;
            this.udRotateStep.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label19
            // 
            this.label19.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label19.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label19.Location = new System.Drawing.Point(256, 309);
            this.label19.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(91, 14);
            this.label19.TabIndex = 321;
            this.label19.Text = "Speed Increment";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // udSpeedStep
            // 
            this.udSpeedStep.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.udSpeedStep.Location = new System.Drawing.Point(351, 307);
            this.udSpeedStep.Margin = new System.Windows.Forms.Padding(2);
            this.udSpeedStep.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.udSpeedStep.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            -2147483648});
            this.udSpeedStep.Name = "udSpeedStep";
            this.udSpeedStep.Size = new System.Drawing.Size(52, 20);
            this.udSpeedStep.TabIndex = 320;
            this.udSpeedStep.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label3.Location = new System.Drawing.Point(22, 145);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(369, 13);
            this.label3.TabIndex = 304;
            this.label3.Text = "TODO: Create option to generate Macro from that log, or log Macro functions";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label3.Visible = false;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label2.Location = new System.Drawing.Point(53, 124);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(275, 13);
            this.label2.TabIndex = 100;
            this.label2.Text = "TODO: Create option for This form to log all of its actions ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.Visible = false;
            // 
            // chkDebugConnection
            // 
            this.chkDebugConnection.AutoSize = true;
            this.chkDebugConnection.Location = new System.Drawing.Point(162, 7);
            this.chkDebugConnection.Margin = new System.Windows.Forms.Padding(2);
            this.chkDebugConnection.Name = "chkDebugConnection";
            this.chkDebugConnection.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkDebugConnection.Size = new System.Drawing.Size(115, 17);
            this.chkDebugConnection.TabIndex = 303;
            this.chkDebugConnection.Text = "Debug Connection";
            this.chkDebugConnection.UseVisualStyleBackColor = true;
            this.chkDebugConnection.CheckedChanged += new System.EventHandler(this.chkDebugConnection_CheckedChanged);
            // 
            // udFormDisplay
            // 
            this.udFormDisplay.Location = new System.Drawing.Point(111, 6);
            this.udFormDisplay.Margin = new System.Windows.Forms.Padding(2);
            this.udFormDisplay.Maximum = new decimal(new int[] {
            32768,
            0,
            0,
            0});
            this.udFormDisplay.Name = "udFormDisplay";
            this.udFormDisplay.Size = new System.Drawing.Size(34, 20);
            this.udFormDisplay.TabIndex = 301;
            this.udFormDisplay.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.udFormDisplay.ValueChanged += new System.EventHandler(this.udFormDisplay_ValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 8);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 300;
            this.label1.Text = "Form Display Speed";
            // 
            // chkAccurateSensors
            // 
            this.chkAccurateSensors.Location = new System.Drawing.Point(8, 26);
            this.chkAccurateSensors.Margin = new System.Windows.Forms.Padding(2);
            this.chkAccurateSensors.Name = "chkAccurateSensors";
            this.chkAccurateSensors.Size = new System.Drawing.Size(356, 50);
            this.chkAccurateSensors.TabIndex = 297;
            this.chkAccurateSensors.Text = "Accurate sensor Display (Means indicators will flash since some polls are dropped" +
    " or never recieved. This is technically the most accurate display of sensor data" +
    ")";
            this.chkAccurateSensors.UseVisualStyleBackColor = true;
            this.chkAccurateSensors.CheckedChanged += new System.EventHandler(this.chkAccurateSensors_CheckedChanged);
            // 
            // chkShowErrors
            // 
            this.chkShowErrors.AutoSize = true;
            this.chkShowErrors.Location = new System.Drawing.Point(4, 476);
            this.chkShowErrors.Margin = new System.Windows.Forms.Padding(2);
            this.chkShowErrors.Name = "chkShowErrors";
            this.chkShowErrors.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkShowErrors.Size = new System.Drawing.Size(83, 17);
            this.chkShowErrors.TabIndex = 309;
            this.chkShowErrors.Text = "Show Errors";
            this.chkShowErrors.UseVisualStyleBackColor = true;
            this.chkShowErrors.CheckedChanged += new System.EventHandler(this.chkShowErrors_CheckedChanged);
            // 
            // lblError
            // 
            this.lblError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblError.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblError.Location = new System.Drawing.Point(91, 477);
            this.lblError.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(312, 35);
            this.lblError.TabIndex = 308;
            // 
            // lblSensorParse
            // 
            this.lblSensorParse.BackColor = System.Drawing.Color.Transparent;
            this.lblSensorParse.Location = new System.Drawing.Point(86, 2);
            this.lblSensorParse.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSensorParse.Name = "lblSensorParse";
            this.lblSensorParse.Size = new System.Drawing.Size(342, 20);
            this.lblSensorParse.TabIndex = 307;
            this.lblSensorParse.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmRightClick
            // 
            this.cmRightClick.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeModeToolStripMenuItem});
            this.cmRightClick.Name = "cmRightClick";
            this.cmRightClick.Size = new System.Drawing.Size(150, 26);
            // 
            // changeModeToolStripMenuItem
            // 
            this.changeModeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PASSIVEToolStripMenuItem,
            this.SAFEToolStripMenuItem,
            this.FULLToolStripMenuItem});
            this.changeModeToolStripMenuItem.Name = "changeModeToolStripMenuItem";
            this.changeModeToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.changeModeToolStripMenuItem.Text = "Change Mode";
            // 
            // PASSIVEToolStripMenuItem
            // 
            this.PASSIVEToolStripMenuItem.Name = "PASSIVEToolStripMenuItem";
            this.PASSIVEToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.PASSIVEToolStripMenuItem.Text = "PASSIVE";
            this.PASSIVEToolStripMenuItem.Click += new System.EventHandler(this.PASSIVEToolStripMenuItem_Click);
            // 
            // SAFEToolStripMenuItem
            // 
            this.SAFEToolStripMenuItem.Name = "SAFEToolStripMenuItem";
            this.SAFEToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.SAFEToolStripMenuItem.Text = "SAFE";
            this.SAFEToolStripMenuItem.Click += new System.EventHandler(this.SAFEToolStripMenuItem_Click);
            // 
            // FULLToolStripMenuItem
            // 
            this.FULLToolStripMenuItem.Name = "FULLToolStripMenuItem";
            this.FULLToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.FULLToolStripMenuItem.Text = "FULL";
            this.FULLToolStripMenuItem.Click += new System.EventHandler(this.FULLToolStripMenuItem_Click);
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnApply.Location = new System.Drawing.Point(411, 477);
            this.btnApply.Margin = new System.Windows.Forms.Padding(2);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(53, 22);
            this.btnApply.TabIndex = 326;
            this.btnApply.Text = "Apply";
            this.toolTip1.SetToolTip(this.btnApply, "Click to save configuration");
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Visible = false;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // frmDrive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(472, 517);
            this.ContextMenuStrip = this.cmRightClick;
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.lblSensorParse);
            this.Controls.Add(this.chkShowErrors);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(357, 0);
            this.Name = "frmDrive";
            this.ShowInTaskbar = true;
            this.Text = "Drive";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDrive_FormClosing);
            this.Load += new System.EventHandler(this.frmDrive_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmDrive_KeyDown);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.Controls.SetChildIndex(this.lblError, 0);
            this.Controls.SetChildIndex(this.chkShowErrors, 0);
            this.Controls.SetChildIndex(this.lblSensorParse, 0);
            this.Controls.SetChildIndex(this.btnApply, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pBack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pRotateRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pRotateLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pFWD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pVirtual_Wall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pDriveLeft_Overcurrent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pDriveRight_OverCurrent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pWheelDrop_Caster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pWheelDrop_Right)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pWheelDrop_Left)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBump_Right)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBump_Left)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pCliffFrontRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pCliffRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pCliffFrontLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pWallDetect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pCliffLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pMainBrush_Overcurrent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pVacuum_OverCurrent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pSideBrush_Overcurrent)).EndInit();
            this.gMode.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.udSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udRotate)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabDriveDisplay.ResumeLayout(false);
            this.tabDriveDisplay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lrSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericRotationBaseSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericBaseSpeed)).EndInit();
            this.tabDriveSettings.ResumeLayout(false);
            this.tabDriveSettings.PerformLayout();
            this.gHardwareUI.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pHome)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pClean)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pSpot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pPower)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udRotateStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udSpeedStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udFormDisplay)).EndInit();
            this.cmRightClick.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDirt_Detect_Right;
        private System.Windows.Forms.Label lblDirt_Detect_Left;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.PictureBox pVirtual_Wall;
        private System.Windows.Forms.PictureBox pDriveLeft_Overcurrent;
        private System.Windows.Forms.PictureBox pDriveRight_OverCurrent;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.PictureBox pWheelDrop_Caster;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.PictureBox pWheelDrop_Right;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pWheelDrop_Left;
        private System.Windows.Forms.PictureBox pBump_Right;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.PictureBox pBump_Left;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.NumericUpDown udSpeed;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown udRotate;
        private System.Windows.Forms.CheckBox tSideBrush;
        private System.Windows.Forms.CheckBox tVacuum;
        private System.Windows.Forms.CheckBox tMain_Brush;
        private System.Windows.Forms.PictureBox pCliffFrontRight;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pCliffRight;
        private System.Windows.Forms.PictureBox pCliffFrontLeft;
        private System.Windows.Forms.PictureBox pWallDetect;
        private System.Windows.Forms.PictureBox pCliffLeft;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pMainBrush_Overcurrent;
        private System.Windows.Forms.PictureBox pVacuum_OverCurrent;
        private System.Windows.Forms.PictureBox pSideBrush_Overcurrent;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabDriveDisplay;
        private System.Windows.Forms.TabPage tabDriveSettings;
        private System.Windows.Forms.CheckBox chkAccurateSensors;
        private System.Windows.Forms.NumericUpDown udFormDisplay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkShowErrors;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Label lblSensorParse;
        private System.Windows.Forms.GroupBox gMode;
        private System.Windows.Forms.CheckBox chkDebugConnection;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.PictureBox pBack;
        private System.Windows.Forms.PictureBox pRotateRight;
        private System.Windows.Forms.PictureBox pRotateLeft;
        private System.Windows.Forms.PictureBox pFWD;
        private System.Windows.Forms.Label lSafe;
        private System.Windows.Forms.Label lPassive;
        private System.Windows.Forms.Label lFull;
        private System.Windows.Forms.Label lOff;
        private System.Windows.Forms.ContextMenuStrip cmRightClick;
        private System.Windows.Forms.ToolStripMenuItem changeModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PASSIVEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SAFEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FULLToolStripMenuItem;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox chkAutoStraighten;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.NumericUpDown udRotateStep;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.NumericUpDown udSpeedStep;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.GroupBox gHardwareUI;
        private System.Windows.Forms.PictureBox pMax;
        private System.Windows.Forms.PictureBox pClean;
        private System.Windows.Forms.PictureBox pSpot;
        private System.Windows.Forms.PictureBox pPower;
        private System.Windows.Forms.Button btnMax;
        private System.Windows.Forms.Button btnClean;
        private System.Windows.Forms.Button btnSpot;
        private System.Windows.Forms.Button btnPower;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label labelInstructions;
        private System.Windows.Forms.Label labelBaseSpeed;
        private System.Windows.Forms.NumericUpDown numericBaseSpeed;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.PictureBox pHome;
        private System.Windows.Forms.NumericUpDown lrSpeed;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown numericRotationBaseSpeed;
        private System.Windows.Forms.Label labelBaseRotationSpeed;
    }
}

namespace iRobotKinect
{
    partial class frmSensors
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSensors));
            this.gSensors = new System.Windows.Forms.GroupBox();
            this.cmSensorMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.displayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.outputPacketDataToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.lblMotorOvercurrentsRaw = new System.Windows.Forms.Label();
            this.lblChargeStateRaw = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblButtonsRaw = new System.Windows.Forms.Label();
            this.lblMotorOvercurrents = new System.Windows.Forms.Label();
            this.lblBumps_WheelDrops = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label76 = new System.Windows.Forms.Label();
            this.lblButtons = new System.Windows.Forms.Label();
            this.label68 = new System.Windows.Forms.Label();
            this.lblVirtual_Wall = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.lblCliff_Right = new System.Windows.Forms.Label();
            this.label72 = new System.Windows.Forms.Label();
            this.lblCliff_Front_Right = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.lblCliff_Front_Left = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.lblCliff_Left = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.lblWall = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.lblBumps_WheelDropsRaw = new System.Windows.Forms.Label();
            this.lblTempF = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblChargeState = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.lblVoltage = new System.Windows.Forms.Label();
            this.lblCurrent = new System.Windows.Forms.Label();
            this.lblTemp = new System.Windows.Forms.Label();
            this.lblCharge = new System.Windows.Forms.Label();
            this.lblCapacity = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.lblDistance_Traveled = new System.Windows.Forms.Label();
            this.lblAngle_Traveled = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.lblRemote = new System.Windows.Forms.Label();
            this.label78 = new System.Windows.Forms.Label();
            this.lblDirt_Detector_Right = new System.Windows.Forms.Label();
            this.label80 = new System.Windows.Forms.Label();
            this.lblDirt_Detector_Left = new System.Windows.Forms.Label();
            this.lblBytesRCVD = new System.Windows.Forms.Label();
            this.lblSensorParse = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabDisplay = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabConfig = new System.Windows.Forms.TabPage();
            this.chkPersist = new System.Windows.Forms.CheckBox();
            this.chkAccurateSensors = new System.Windows.Forms.CheckBox();
            this.udFormDisplay = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.gSensors.SuspendLayout();
            this.cmSensorMenu.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabDisplay.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udFormDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // gSensors
            // 
            this.gSensors.BackColor = System.Drawing.Color.Transparent;
            this.gSensors.ContextMenuStrip = this.cmSensorMenu;
            this.gSensors.Controls.Add(this.lblMotorOvercurrentsRaw);
            this.gSensors.Controls.Add(this.lblChargeStateRaw);
            this.gSensors.Controls.Add(this.label8);
            this.gSensors.Controls.Add(this.lblButtonsRaw);
            this.gSensors.Controls.Add(this.lblMotorOvercurrents);
            this.gSensors.Controls.Add(this.lblBumps_WheelDrops);
            this.gSensors.Controls.Add(this.label6);
            this.gSensors.Controls.Add(this.label5);
            this.gSensors.Controls.Add(this.label4);
            this.gSensors.Controls.Add(this.label2);
            this.gSensors.Controls.Add(this.label1);
            this.gSensors.Controls.Add(this.label76);
            this.gSensors.Controls.Add(this.lblButtons);
            this.gSensors.Controls.Add(this.label68);
            this.gSensors.Controls.Add(this.lblVirtual_Wall);
            this.gSensors.Controls.Add(this.label70);
            this.gSensors.Controls.Add(this.lblCliff_Right);
            this.gSensors.Controls.Add(this.label72);
            this.gSensors.Controls.Add(this.lblCliff_Front_Right);
            this.gSensors.Controls.Add(this.label45);
            this.gSensors.Controls.Add(this.lblCliff_Front_Left);
            this.gSensors.Controls.Add(this.label41);
            this.gSensors.Controls.Add(this.lblCliff_Left);
            this.gSensors.Controls.Add(this.label37);
            this.gSensors.Controls.Add(this.lblWall);
            this.gSensors.Controls.Add(this.label35);
            this.gSensors.Controls.Add(this.lblBumps_WheelDropsRaw);
            this.gSensors.Controls.Add(this.lblTempF);
            this.gSensors.Controls.Add(this.label3);
            this.gSensors.Controls.Add(this.lblChargeState);
            this.gSensors.Controls.Add(this.label38);
            this.gSensors.Controls.Add(this.lblVoltage);
            this.gSensors.Controls.Add(this.lblCurrent);
            this.gSensors.Controls.Add(this.lblTemp);
            this.gSensors.Controls.Add(this.lblCharge);
            this.gSensors.Controls.Add(this.lblCapacity);
            this.gSensors.Controls.Add(this.label32);
            this.gSensors.Controls.Add(this.lblDistance_Traveled);
            this.gSensors.Controls.Add(this.lblAngle_Traveled);
            this.gSensors.Controls.Add(this.label34);
            this.gSensors.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gSensors.Location = new System.Drawing.Point(4, 2);
            this.gSensors.Margin = new System.Windows.Forms.Padding(2);
            this.gSensors.Name = "gSensors";
            this.gSensors.Padding = new System.Windows.Forms.Padding(2);
            this.gSensors.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.gSensors.Size = new System.Drawing.Size(168, 436);
            this.gSensors.TabIndex = 295;
            this.gSensors.TabStop = false;
            this.gSensors.Text = "Sensors";
            // 
            // cmSensorMenu
            // 
            this.cmSensorMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.displayToolStripMenuItem,
            this.outputPacketDataToFileToolStripMenuItem});
            this.cmSensorMenu.Name = "cmConnectionMenu";
            this.cmSensorMenu.Size = new System.Drawing.Size(148, 48);
            // 
            // displayToolStripMenuItem
            // 
            this.displayToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem1,
            this.stopToolStripMenuItem,
            this.clearToolStripMenuItem});
            this.displayToolStripMenuItem.Name = "displayToolStripMenuItem";
            this.displayToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.displayToolStripMenuItem.Text = "Display";
            // 
            // startToolStripMenuItem1
            // 
            this.startToolStripMenuItem1.Name = "startToolStripMenuItem1";
            this.startToolStripMenuItem1.Size = new System.Drawing.Size(101, 22);
            this.startToolStripMenuItem1.Text = "Start";
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // outputPacketDataToFileToolStripMenuItem
            // 
            this.outputPacketDataToFileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem2,
            this.stopToolStripMenuItem1});
            this.outputPacketDataToFileToolStripMenuItem.Name = "outputPacketDataToFileToolStripMenuItem";
            this.outputPacketDataToFileToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.outputPacketDataToFileToolStripMenuItem.Text = "Output to File";
            // 
            // startToolStripMenuItem2
            // 
            this.startToolStripMenuItem2.Name = "startToolStripMenuItem2";
            this.startToolStripMenuItem2.Size = new System.Drawing.Size(98, 22);
            this.startToolStripMenuItem2.Text = "Start";
            // 
            // stopToolStripMenuItem1
            // 
            this.stopToolStripMenuItem1.Name = "stopToolStripMenuItem1";
            this.stopToolStripMenuItem1.Size = new System.Drawing.Size(98, 22);
            this.stopToolStripMenuItem1.Text = "Stop";
            // 
            // lblMotorOvercurrentsRaw
            // 
            this.lblMotorOvercurrentsRaw.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMotorOvercurrentsRaw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMotorOvercurrentsRaw.Location = new System.Drawing.Point(133, 288);
            this.lblMotorOvercurrentsRaw.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMotorOvercurrentsRaw.Name = "lblMotorOvercurrentsRaw";
            this.lblMotorOvercurrentsRaw.Size = new System.Drawing.Size(28, 15);
            this.lblMotorOvercurrentsRaw.TabIndex = 107;
            // 
            // lblChargeStateRaw
            // 
            this.lblChargeStateRaw.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblChargeStateRaw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblChargeStateRaw.Location = new System.Drawing.Point(122, 251);
            this.lblChargeStateRaw.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblChargeStateRaw.Name = "lblChargeStateRaw";
            this.lblChargeStateRaw.Size = new System.Drawing.Size(40, 16);
            this.lblChargeStateRaw.TabIndex = 230;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(2, 287);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 13);
            this.label8.TabIndex = 106;
            this.label8.Text = "Motor Overcurrents";
            // 
            // lblButtonsRaw
            // 
            this.lblButtonsRaw.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblButtonsRaw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblButtonsRaw.Location = new System.Drawing.Point(122, 163);
            this.lblButtonsRaw.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblButtonsRaw.Name = "lblButtonsRaw";
            this.lblButtonsRaw.Size = new System.Drawing.Size(40, 16);
            this.lblButtonsRaw.TabIndex = 229;
            // 
            // lblMotorOvercurrents
            // 
            this.lblMotorOvercurrents.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMotorOvercurrents.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMotorOvercurrents.Location = new System.Drawing.Point(4, 306);
            this.lblMotorOvercurrents.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMotorOvercurrents.Name = "lblMotorOvercurrents";
            this.lblMotorOvercurrents.Size = new System.Drawing.Size(157, 39);
            this.lblMotorOvercurrents.TabIndex = 105;
            this.lblMotorOvercurrents.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBumps_WheelDrops
            // 
            this.lblBumps_WheelDrops.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBumps_WheelDrops.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBumps_WheelDrops.Location = new System.Drawing.Point(5, 39);
            this.lblBumps_WheelDrops.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblBumps_WheelDrops.Name = "lblBumps_WheelDrops";
            this.lblBumps_WheelDrops.Size = new System.Drawing.Size(158, 35);
            this.lblBumps_WheelDrops.TabIndex = 228;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1, 415);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 227;
            this.label6.Text = "Capacity";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 398);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 226;
            this.label5.Text = "Charge";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 381);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 225;
            this.label4.Text = "Temp";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 366);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 224;
            this.label2.Text = "Current";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 349);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 223;
            this.label1.Text = "Voltage";
            // 
            // label76
            // 
            this.label76.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label76.AutoSize = true;
            this.label76.Location = new System.Drawing.Point(78, 163);
            this.label76.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(43, 13);
            this.label76.TabIndex = 106;
            this.label76.Text = "Buttons";
            // 
            // lblButtons
            // 
            this.lblButtons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblButtons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblButtons.Location = new System.Drawing.Point(5, 181);
            this.lblButtons.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblButtons.Name = "lblButtons";
            this.lblButtons.Size = new System.Drawing.Size(156, 32);
            this.lblButtons.TabIndex = 105;
            // 
            // label68
            // 
            this.label68.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label68.AutoSize = true;
            this.label68.Location = new System.Drawing.Point(61, 145);
            this.label68.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(60, 13);
            this.label68.TabIndex = 98;
            this.label68.Text = "Virtual Wall";
            // 
            // lblVirtual_Wall
            // 
            this.lblVirtual_Wall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVirtual_Wall.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVirtual_Wall.Location = new System.Drawing.Point(122, 145);
            this.lblVirtual_Wall.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblVirtual_Wall.Name = "lblVirtual_Wall";
            this.lblVirtual_Wall.Size = new System.Drawing.Size(40, 15);
            this.lblVirtual_Wall.TabIndex = 97;
            // 
            // label70
            // 
            this.label70.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label70.AutoSize = true;
            this.label70.Location = new System.Drawing.Point(69, 128);
            this.label70.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(52, 13);
            this.label70.TabIndex = 96;
            this.label70.Text = "Cliff Right";
            // 
            // lblCliff_Right
            // 
            this.lblCliff_Right.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCliff_Right.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCliff_Right.Location = new System.Drawing.Point(122, 128);
            this.lblCliff_Right.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCliff_Right.Name = "lblCliff_Right";
            this.lblCliff_Right.Size = new System.Drawing.Size(40, 15);
            this.lblCliff_Right.TabIndex = 95;
            // 
            // label72
            // 
            this.label72.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label72.AutoSize = true;
            this.label72.Location = new System.Drawing.Point(41, 112);
            this.label72.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(79, 13);
            this.label72.TabIndex = 94;
            this.label72.Text = "Cliff Front Right";
            // 
            // lblCliff_Front_Right
            // 
            this.lblCliff_Front_Right.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCliff_Front_Right.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCliff_Front_Right.Location = new System.Drawing.Point(122, 112);
            this.lblCliff_Front_Right.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCliff_Front_Right.Name = "lblCliff_Front_Right";
            this.lblCliff_Front_Right.Size = new System.Drawing.Size(40, 15);
            this.lblCliff_Front_Right.TabIndex = 93;
            // 
            // label45
            // 
            this.label45.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(48, 95);
            this.label45.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(72, 13);
            this.label45.TabIndex = 92;
            this.label45.Text = "Cliff Front Left";
            // 
            // lblCliff_Front_Left
            // 
            this.lblCliff_Front_Left.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCliff_Front_Left.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCliff_Front_Left.Location = new System.Drawing.Point(122, 95);
            this.lblCliff_Front_Left.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCliff_Front_Left.Name = "lblCliff_Front_Left";
            this.lblCliff_Front_Left.Size = new System.Drawing.Size(40, 15);
            this.lblCliff_Front_Left.TabIndex = 91;
            // 
            // label41
            // 
            this.label41.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(71, 78);
            this.label41.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(48, 13);
            this.label41.TabIndex = 90;
            this.label41.Text = "Cliff Left ";
            // 
            // lblCliff_Left
            // 
            this.lblCliff_Left.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCliff_Left.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCliff_Left.Location = new System.Drawing.Point(122, 78);
            this.lblCliff_Left.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCliff_Left.Name = "lblCliff_Left";
            this.lblCliff_Left.Size = new System.Drawing.Size(40, 15);
            this.lblCliff_Left.TabIndex = 89;
            // 
            // label37
            // 
            this.label37.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(6, 138);
            this.label37.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(28, 13);
            this.label37.TabIndex = 88;
            this.label37.Text = "Wall";
            // 
            // lblWall
            // 
            this.lblWall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWall.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblWall.Location = new System.Drawing.Point(6, 155);
            this.lblWall.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWall.Name = "lblWall";
            this.lblWall.Size = new System.Drawing.Size(40, 15);
            this.lblWall.TabIndex = 87;
            // 
            // label35
            // 
            this.label35.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(5, 18);
            this.label35.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(103, 13);
            this.label35.TabIndex = 86;
            this.label35.Text = "Bumps/WheelDrops";
            // 
            // lblBumps_WheelDropsRaw
            // 
            this.lblBumps_WheelDropsRaw.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBumps_WheelDropsRaw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBumps_WheelDropsRaw.Location = new System.Drawing.Point(122, 20);
            this.lblBumps_WheelDropsRaw.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblBumps_WheelDropsRaw.Name = "lblBumps_WheelDropsRaw";
            this.lblBumps_WheelDropsRaw.Size = new System.Drawing.Size(40, 16);
            this.lblBumps_WheelDropsRaw.TabIndex = 85;
            // 
            // lblTempF
            // 
            this.lblTempF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTempF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTempF.Location = new System.Drawing.Point(123, 382);
            this.lblTempF.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTempF.Name = "lblTempF";
            this.lblTempF.Size = new System.Drawing.Size(40, 15);
            this.lblTempF.TabIndex = 82;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(104, 381);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 13);
            this.label3.TabIndex = 81;
            this.label3.Text = "(F)";
            // 
            // lblChargeState
            // 
            this.lblChargeState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblChargeState.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblChargeState.Location = new System.Drawing.Point(5, 269);
            this.lblChargeState.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblChargeState.Name = "lblChargeState";
            this.lblChargeState.Size = new System.Drawing.Size(156, 15);
            this.lblChargeState.TabIndex = 68;
            // 
            // label38
            // 
            this.label38.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(52, 251);
            this.label38.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(69, 13);
            this.label38.TabIndex = 67;
            this.label38.Text = "Charge State";
            // 
            // lblVoltage
            // 
            this.lblVoltage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVoltage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVoltage.Location = new System.Drawing.Point(52, 349);
            this.lblVoltage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblVoltage.Name = "lblVoltage";
            this.lblVoltage.Size = new System.Drawing.Size(51, 15);
            this.lblVoltage.TabIndex = 70;
            // 
            // lblCurrent
            // 
            this.lblCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCurrent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCurrent.Location = new System.Drawing.Point(52, 366);
            this.lblCurrent.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCurrent.Name = "lblCurrent";
            this.lblCurrent.Size = new System.Drawing.Size(51, 15);
            this.lblCurrent.TabIndex = 72;
            // 
            // lblTemp
            // 
            this.lblTemp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTemp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTemp.Location = new System.Drawing.Point(52, 382);
            this.lblTemp.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTemp.Name = "lblTemp";
            this.lblTemp.Size = new System.Drawing.Size(51, 15);
            this.lblTemp.TabIndex = 74;
            // 
            // lblCharge
            // 
            this.lblCharge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCharge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCharge.Location = new System.Drawing.Point(52, 399);
            this.lblCharge.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCharge.Name = "lblCharge";
            this.lblCharge.Size = new System.Drawing.Size(51, 15);
            this.lblCharge.TabIndex = 76;
            // 
            // lblCapacity
            // 
            this.lblCapacity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCapacity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCapacity.Location = new System.Drawing.Point(52, 416);
            this.lblCapacity.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCapacity.Name = "lblCapacity";
            this.lblCapacity.Size = new System.Drawing.Size(51, 15);
            this.lblCapacity.TabIndex = 78;
            // 
            // label32
            // 
            this.label32.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(34, 216);
            this.label32.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(49, 13);
            this.label32.TabIndex = 42;
            this.label32.Text = "Distance";
            // 
            // lblDistance_Traveled
            // 
            this.lblDistance_Traveled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDistance_Traveled.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDistance_Traveled.Location = new System.Drawing.Point(87, 215);
            this.lblDistance_Traveled.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDistance_Traveled.Name = "lblDistance_Traveled";
            this.lblDistance_Traveled.Size = new System.Drawing.Size(75, 15);
            this.lblDistance_Traveled.TabIndex = 43;
            // 
            // lblAngle_Traveled
            // 
            this.lblAngle_Traveled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAngle_Traveled.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAngle_Traveled.Location = new System.Drawing.Point(87, 232);
            this.lblAngle_Traveled.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAngle_Traveled.Name = "lblAngle_Traveled";
            this.lblAngle_Traveled.Size = new System.Drawing.Size(75, 15);
            this.lblAngle_Traveled.TabIndex = 66;
            // 
            // label34
            // 
            this.label34.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(5, 232);
            this.label34.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(82, 13);
            this.label34.TabIndex = 65;
            this.label34.Text = "Angle (Radians)";
            // 
            // label29
            // 
            this.label29.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(45, 50);
            this.label29.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(86, 13);
            this.label29.TabIndex = 40;
            this.label29.Text = "Remote Control: ";
            // 
            // lblRemote
            // 
            this.lblRemote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRemote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRemote.Location = new System.Drawing.Point(135, 50);
            this.lblRemote.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRemote.Name = "lblRemote";
            this.lblRemote.Size = new System.Drawing.Size(28, 15);
            this.lblRemote.TabIndex = 41;
            // 
            // label78
            // 
            this.label78.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label78.AutoSize = true;
            this.label78.Location = new System.Drawing.Point(37, 33);
            this.label78.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(95, 13);
            this.label78.TabIndex = 104;
            this.label78.Text = "Dirt Detector Right";
            // 
            // lblDirt_Detector_Right
            // 
            this.lblDirt_Detector_Right.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDirt_Detector_Right.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDirt_Detector_Right.Location = new System.Drawing.Point(135, 33);
            this.lblDirt_Detector_Right.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDirt_Detector_Right.Name = "lblDirt_Detector_Right";
            this.lblDirt_Detector_Right.Size = new System.Drawing.Size(28, 15);
            this.lblDirt_Detector_Right.TabIndex = 103;
            // 
            // label80
            // 
            this.label80.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label80.AutoSize = true;
            this.label80.Location = new System.Drawing.Point(44, 16);
            this.label80.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(88, 13);
            this.label80.TabIndex = 102;
            this.label80.Text = "Dirt Detector Left";
            // 
            // lblDirt_Detector_Left
            // 
            this.lblDirt_Detector_Left.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDirt_Detector_Left.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDirt_Detector_Left.Location = new System.Drawing.Point(135, 16);
            this.lblDirt_Detector_Left.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDirt_Detector_Left.Name = "lblDirt_Detector_Left";
            this.lblDirt_Detector_Left.Size = new System.Drawing.Size(28, 15);
            this.lblDirt_Detector_Left.TabIndex = 101;
            // 
            // lblBytesRCVD
            // 
            this.lblBytesRCVD.Location = new System.Drawing.Point(86, 3);
            this.lblBytesRCVD.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblBytesRCVD.Name = "lblBytesRCVD";
            this.lblBytesRCVD.Size = new System.Drawing.Size(92, 14);
            this.lblBytesRCVD.TabIndex = 222;
            this.lblBytesRCVD.Text = "Bytes RCVD";
            this.lblBytesRCVD.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSensorParse
            // 
            this.lblSensorParse.BackColor = System.Drawing.Color.Transparent;
            this.lblSensorParse.Location = new System.Drawing.Point(86, 2);
            this.lblSensorParse.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSensorParse.Name = "lblSensorParse";
            this.lblSensorParse.Size = new System.Drawing.Size(95, 20);
            this.lblSensorParse.TabIndex = 307;
            this.lblSensorParse.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabDisplay);
            this.tabControl1.Controls.Add(this.tabConfig);
            this.tabControl1.Location = new System.Drawing.Point(2, 21);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(182, 538);
            this.tabControl1.TabIndex = 296;
            // 
            // tabDisplay
            // 
            this.tabDisplay.Controls.Add(this.groupBox1);
            this.tabDisplay.Controls.Add(this.gSensors);
            this.tabDisplay.Location = new System.Drawing.Point(4, 22);
            this.tabDisplay.Margin = new System.Windows.Forms.Padding(2);
            this.tabDisplay.Name = "tabDisplay";
            this.tabDisplay.Padding = new System.Windows.Forms.Padding(2);
            this.tabDisplay.Size = new System.Drawing.Size(174, 512);
            this.tabDisplay.TabIndex = 0;
            this.tabDisplay.Text = "Display";
            this.tabDisplay.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.ContextMenuStrip = this.cmSensorMenu;
            this.groupBox1.Controls.Add(this.lblDirt_Detector_Right);
            this.groupBox1.Controls.Add(this.label80);
            this.groupBox1.Controls.Add(this.label78);
            this.groupBox1.Controls.Add(this.lblDirt_Detector_Left);
            this.groupBox1.Controls.Add(this.label29);
            this.groupBox1.Controls.Add(this.lblRemote);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox1.Location = new System.Drawing.Point(3, 438);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupBox1.Size = new System.Drawing.Size(170, 71);
            this.groupBox1.TabIndex = 296;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Roomba Only";
            // 
            // tabConfig
            // 
            this.tabConfig.Controls.Add(this.chkPersist);
            this.tabConfig.Controls.Add(this.chkAccurateSensors);
            this.tabConfig.Controls.Add(this.udFormDisplay);
            this.tabConfig.Controls.Add(this.label7);
            this.tabConfig.Location = new System.Drawing.Point(4, 22);
            this.tabConfig.Margin = new System.Windows.Forms.Padding(2);
            this.tabConfig.Name = "tabConfig";
            this.tabConfig.Padding = new System.Windows.Forms.Padding(2);
            this.tabConfig.Size = new System.Drawing.Size(174, 512);
            this.tabConfig.TabIndex = 1;
            this.tabConfig.Text = "Config";
            this.tabConfig.UseVisualStyleBackColor = true;
            // 
            // chkPersist
            // 
            this.chkPersist.Checked = true;
            this.chkPersist.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPersist.Location = new System.Drawing.Point(4, 128);
            this.chkPersist.Margin = new System.Windows.Forms.Padding(2);
            this.chkPersist.Name = "chkPersist";
            this.chkPersist.Size = new System.Drawing.Size(170, 51);
            this.chkPersist.TabIndex = 303;
            this.chkPersist.Text = "Persist Last Sensor Reading (Do not clear if no data recieved)";
            this.chkPersist.UseVisualStyleBackColor = true;
            // 
            // chkAccurateSensors
            // 
            this.chkAccurateSensors.Location = new System.Drawing.Point(4, 28);
            this.chkAccurateSensors.Margin = new System.Windows.Forms.Padding(2);
            this.chkAccurateSensors.Name = "chkAccurateSensors";
            this.chkAccurateSensors.Size = new System.Drawing.Size(170, 93);
            this.chkAccurateSensors.TabIndex = 302;
            this.chkAccurateSensors.Text = "Accurate sensor Display (Means indicators will flash since some polls are dropped" +
    " or never recieved. This is technically the most accurate display of sensor data" +
    ")";
            this.chkAccurateSensors.UseVisualStyleBackColor = true;
            // 
            // udFormDisplay
            // 
            this.udFormDisplay.Location = new System.Drawing.Point(116, 5);
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
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 6);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 13);
            this.label7.TabIndex = 300;
            this.label7.Text = "Form Display Speed";
            // 
            // frmSensors
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(186, 561);
            this.ContextMenuStrip = this.cmSensorMenu;
            this.Controls.Add(this.lblBytesRCVD);
            this.Controls.Add(this.lblSensorParse);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(845, 0);
            this.Name = "frmSensors";
            this.ShowInTaskbar = false;
            this.Text = "Sensors";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSensors_FormClosing);
            this.Load += new System.EventHandler(this.frmSensors_Load);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.Controls.SetChildIndex(this.lblSensorParse, 0);
            this.Controls.SetChildIndex(this.lblBytesRCVD, 0);
            this.gSensors.ResumeLayout(false);
            this.gSensors.PerformLayout();
            this.cmSensorMenu.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabDisplay.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabConfig.ResumeLayout(false);
            this.tabConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udFormDisplay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gSensors;
        internal System.Windows.Forms.Label lblBytesRCVD;
        private System.Windows.Forms.Label label76;
        private System.Windows.Forms.Label lblButtons;
        private System.Windows.Forms.Label label78;
        private System.Windows.Forms.Label lblDirt_Detector_Right;
        private System.Windows.Forms.Label label80;
        private System.Windows.Forms.Label lblDirt_Detector_Left;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.Label lblVirtual_Wall;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.Label lblCliff_Right;
        private System.Windows.Forms.Label label72;
        private System.Windows.Forms.Label lblCliff_Front_Right;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label lblCliff_Front_Left;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label lblCliff_Left;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label lblWall;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label lblBumps_WheelDropsRaw;
        private System.Windows.Forms.Label lblTempF;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblChargeState;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label lblVoltage;
        private System.Windows.Forms.Label lblCurrent;
        private System.Windows.Forms.Label lblTemp;
        private System.Windows.Forms.Label lblCharge;
        private System.Windows.Forms.Label lblCapacity;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label lblRemote;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label lblDistance_Traveled;
        private System.Windows.Forms.Label lblAngle_Traveled;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSensorParse;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabDisplay;
        private System.Windows.Forms.TabPage tabConfig;
        private System.Windows.Forms.NumericUpDown udFormDisplay;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ContextMenuStrip cmSensorMenu;
        private System.Windows.Forms.ToolStripMenuItem displayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem outputPacketDataToFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem1;
        private System.Windows.Forms.CheckBox chkAccurateSensors;
        private System.Windows.Forms.CheckBox chkPersist;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblMotorOvercurrents;
        private System.Windows.Forms.Label lblBumps_WheelDrops;
        private System.Windows.Forms.Label lblMotorOvercurrentsRaw;
        private System.Windows.Forms.Label lblChargeStateRaw;
        private System.Windows.Forms.Label lblButtonsRaw;
    }
}

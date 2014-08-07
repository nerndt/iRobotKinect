namespace iRobotKinect
{
    partial class frmCommand
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
            this.txtByte3 = new System.Windows.Forms.TextBox();
            this.cOtherByteCommands = new System.Windows.Forms.ComboBox();
            this.btnSendOpCode = new System.Windows.Forms.Button();
            this.txtByte1 = new System.Windows.Forms.TextBox();
            this.txtByte2 = new System.Windows.Forms.TextBox();
            this.chkRTSEnable = new System.Windows.Forms.CheckBox();
            this.txtByte4 = new System.Windows.Forms.TextBox();
            this.txtSendRecieve = new System.Windows.Forms.TextBox();
            this.gMode = new System.Windows.Forms.GroupBox();
            this.rFull = new System.Windows.Forms.RadioButton();
            this.rPassive = new System.Windows.Forms.RadioButton();
            this.rSafe = new System.Windows.Forms.RadioButton();
            this.rOff = new System.Windows.Forms.RadioButton();
            this.gUserControl = new System.Windows.Forms.GroupBox();
            this.pUserControl = new System.Windows.Forms.PictureBox();
            this.rUserControlOn = new System.Windows.Forms.RadioButton();
            this.rUserControlOff = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabSNDRCV = new System.Windows.Forms.TabPage();
            this.tabConfig = new System.Windows.Forms.TabPage();
            this.chkAllowPolling = new System.Windows.Forms.CheckBox();
            this.udFormDisplay = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboCommand = new System.Windows.Forms.ComboBox();
            this.btnSendCommand = new System.Windows.Forms.Button();
            this.gMode.SuspendLayout();
            this.gUserControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pUserControl)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabSNDRCV.SuspendLayout();
            this.tabConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udFormDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // txtByte3
            // 
            this.txtByte3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtByte3.Location = new System.Drawing.Point(588, 6);
            this.txtByte3.Name = "txtByte3";
            this.txtByte3.Size = new System.Drawing.Size(36, 22);
            this.txtByte3.TabIndex = 113;
            // 
            // cOtherByteCommands
            // 
            this.cOtherByteCommands.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cOtherByteCommands.FormattingEnabled = true;
            this.cOtherByteCommands.Items.AddRange(new object[] {
            "129",
            "137",
            "138",
            "139",
            "141",
            "142"});
            this.cOtherByteCommands.Location = new System.Drawing.Point(422, 5);
            this.cOtherByteCommands.Name = "cOtherByteCommands";
            this.cOtherByteCommands.Size = new System.Drawing.Size(76, 24);
            this.cOtherByteCommands.TabIndex = 111;
            // 
            // btnSendOpCode
            // 
            this.btnSendOpCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendOpCode.Location = new System.Drawing.Point(672, 5);
            this.btnSendOpCode.Name = "btnSendOpCode";
            this.btnSendOpCode.Size = new System.Drawing.Size(73, 24);
            this.btnSendOpCode.TabIndex = 110;
            this.btnSendOpCode.Text = "Send OpCode";
            this.btnSendOpCode.UseVisualStyleBackColor = true;
            this.btnSendOpCode.Click += new System.EventHandler(this.btnSendOpCode_Click);
            // 
            // txtByte1
            // 
            this.txtByte1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtByte1.Location = new System.Drawing.Point(504, 6);
            this.txtByte1.Name = "txtByte1";
            this.txtByte1.Size = new System.Drawing.Size(36, 22);
            this.txtByte1.TabIndex = 112;
            // 
            // txtByte2
            // 
            this.txtByte2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtByte2.Location = new System.Drawing.Point(546, 6);
            this.txtByte2.Name = "txtByte2";
            this.txtByte2.Size = new System.Drawing.Size(36, 22);
            this.txtByte2.TabIndex = 114;
            // 
            // chkRTSEnable
            // 
            this.chkRTSEnable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkRTSEnable.AutoSize = true;
            this.chkRTSEnable.Location = new System.Drawing.Point(284, 6);
            this.chkRTSEnable.Name = "chkRTSEnable";
            this.chkRTSEnable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkRTSEnable.Size = new System.Drawing.Size(103, 21);
            this.chkRTSEnable.TabIndex = 116;
            this.chkRTSEnable.Text = "RTS Enable";
            this.chkRTSEnable.UseVisualStyleBackColor = true;
            // 
            // txtByte4
            // 
            this.txtByte4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtByte4.Location = new System.Drawing.Point(630, 6);
            this.txtByte4.Name = "txtByte4";
            this.txtByte4.Size = new System.Drawing.Size(36, 22);
            this.txtByte4.TabIndex = 115;
            // 
            // txtSendRecieve
            // 
            this.txtSendRecieve.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSendRecieve.Location = new System.Drawing.Point(7, 86);
            this.txtSendRecieve.Multiline = true;
            this.txtSendRecieve.Name = "txtSendRecieve";
            this.txtSendRecieve.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSendRecieve.Size = new System.Drawing.Size(724, 178);
            this.txtSendRecieve.TabIndex = 107;
            this.txtSendRecieve.WordWrap = false;
            // 
            // gMode
            // 
            this.gMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.gMode.Controls.Add(this.rFull);
            this.gMode.Controls.Add(this.rPassive);
            this.gMode.Controls.Add(this.rSafe);
            this.gMode.Controls.Add(this.rOff);
            this.gMode.Enabled = false;
            this.gMode.Location = new System.Drawing.Point(481, 267);
            this.gMode.Name = "gMode";
            this.gMode.Size = new System.Drawing.Size(256, 39);
            this.gMode.TabIndex = 295;
            this.gMode.TabStop = false;
            this.gMode.Text = "Mode";
            // 
            // rFull
            // 
            this.rFull.AutoSize = true;
            this.rFull.Location = new System.Drawing.Point(199, 16);
            this.rFull.Name = "rFull";
            this.rFull.Size = new System.Drawing.Size(48, 21);
            this.rFull.TabIndex = 32;
            this.rFull.Text = "Full";
            this.rFull.UseVisualStyleBackColor = true;
            // 
            // rPassive
            // 
            this.rPassive.AutoSize = true;
            this.rPassive.Location = new System.Drawing.Point(118, 16);
            this.rPassive.Name = "rPassive";
            this.rPassive.Size = new System.Drawing.Size(75, 21);
            this.rPassive.TabIndex = 31;
            this.rPassive.Text = "Passive";
            this.rPassive.UseVisualStyleBackColor = true;
            // 
            // rSafe
            // 
            this.rSafe.AutoSize = true;
            this.rSafe.Location = new System.Drawing.Point(57, 16);
            this.rSafe.Name = "rSafe";
            this.rSafe.Size = new System.Drawing.Size(55, 21);
            this.rSafe.TabIndex = 30;
            this.rSafe.Text = "Safe";
            this.rSafe.UseVisualStyleBackColor = true;
            // 
            // rOff
            // 
            this.rOff.AutoSize = true;
            this.rOff.Checked = true;
            this.rOff.Location = new System.Drawing.Point(6, 16);
            this.rOff.Name = "rOff";
            this.rOff.Size = new System.Drawing.Size(45, 21);
            this.rOff.TabIndex = 29;
            this.rOff.TabStop = true;
            this.rOff.Text = "Off";
            this.rOff.UseVisualStyleBackColor = true;
            // 
            // gUserControl
            // 
            this.gUserControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gUserControl.Controls.Add(this.pUserControl);
            this.gUserControl.Controls.Add(this.rUserControlOn);
            this.gUserControl.Controls.Add(this.rUserControlOff);
            this.gUserControl.Location = new System.Drawing.Point(7, 266);
            this.gUserControl.Name = "gUserControl";
            this.gUserControl.Size = new System.Drawing.Size(168, 40);
            this.gUserControl.TabIndex = 296;
            this.gUserControl.TabStop = false;
            this.gUserControl.Text = "User Control";
            // 
            // pUserControl
            // 
            this.pUserControl.Location = new System.Drawing.Point(107, 17);
            this.pUserControl.Name = "pUserControl";
            this.pUserControl.Size = new System.Drawing.Size(54, 20);
            this.pUserControl.TabIndex = 31;
            this.pUserControl.TabStop = false;
            // 
            // rUserControlOn
            // 
            this.rUserControlOn.AutoSize = true;
            this.rUserControlOn.Location = new System.Drawing.Point(57, 17);
            this.rUserControlOn.Name = "rUserControlOn";
            this.rUserControlOn.Size = new System.Drawing.Size(45, 21);
            this.rUserControlOn.TabIndex = 30;
            this.rUserControlOn.TabStop = true;
            this.rUserControlOn.Text = "On";
            this.rUserControlOn.UseVisualStyleBackColor = true;
            this.rUserControlOn.CheckedChanged += new System.EventHandler(this.rUserControlOn_CheckedChanged);
            // 
            // rUserControlOff
            // 
            this.rUserControlOff.AutoSize = true;
            this.rUserControlOff.Location = new System.Drawing.Point(6, 17);
            this.rUserControlOff.Name = "rUserControlOff";
            this.rUserControlOff.Size = new System.Drawing.Size(45, 21);
            this.rUserControlOff.TabIndex = 29;
            this.rUserControlOff.TabStop = true;
            this.rUserControlOff.Text = "Off";
            this.rUserControlOff.UseVisualStyleBackColor = true;
            this.rUserControlOff.CheckedChanged += new System.EventHandler(this.rUserControlOff_CheckedChanged);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(73, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(580, 17);
            this.label2.TabIndex = 297;
            this.label2.Text = "This form need to be highly explanatory with tooltips everywhere and intellisense" +
                " if possible";
            // 
            // checkBox2
            // 
            this.checkBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Enabled = false;
            this.checkBox2.Location = new System.Drawing.Point(101, 182);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBox2.Size = new System.Drawing.Size(393, 21);
            this.checkBox2.TabIndex = 299;
            this.checkBox2.Text = "Insert (and wait for) Commands between Start Form\'s polls";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabSNDRCV);
            this.tabControl1.Controls.Add(this.tabConfig);
            this.tabControl1.Location = new System.Drawing.Point(3, 29);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(748, 341);
            this.tabControl1.TabIndex = 300;
            // 
            // tabSNDRCV
            // 
            this.tabSNDRCV.Controls.Add(this.cboCommand);
            this.tabSNDRCV.Controls.Add(this.btnSendCommand);
            this.tabSNDRCV.Controls.Add(this.label3);
            this.tabSNDRCV.Controls.Add(this.txtSendRecieve);
            this.tabSNDRCV.Controls.Add(this.checkBox2);
            this.tabSNDRCV.Controls.Add(this.gUserControl);
            this.tabSNDRCV.Controls.Add(this.gMode);
            this.tabSNDRCV.Controls.Add(this.label2);
            this.tabSNDRCV.Location = new System.Drawing.Point(4, 25);
            this.tabSNDRCV.Name = "tabSNDRCV";
            this.tabSNDRCV.Padding = new System.Windows.Forms.Padding(3);
            this.tabSNDRCV.Size = new System.Drawing.Size(740, 312);
            this.tabSNDRCV.TabIndex = 0;
            this.tabSNDRCV.Text = "Send/Recieve";
            this.tabSNDRCV.UseVisualStyleBackColor = true;
            // 
            // tabConfig
            // 
            this.tabConfig.Controls.Add(this.udFormDisplay);
            this.tabConfig.Controls.Add(this.label1);
            this.tabConfig.Controls.Add(this.chkAllowPolling);
            this.tabConfig.Location = new System.Drawing.Point(4, 25);
            this.tabConfig.Name = "tabConfig";
            this.tabConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabConfig.Size = new System.Drawing.Size(740, 312);
            this.tabConfig.TabIndex = 1;
            this.tabConfig.Text = "Config";
            this.tabConfig.UseVisualStyleBackColor = true;
            // 
            // chkAllowPolling
            // 
            this.chkAllowPolling.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chkAllowPolling.AutoSize = true;
            this.chkAllowPolling.Location = new System.Drawing.Point(17, 17);
            this.chkAllowPolling.Name = "chkAllowPolling";
            this.chkAllowPolling.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkAllowPolling.Size = new System.Drawing.Size(188, 21);
            this.chkAllowPolling.TabIndex = 299;
            this.chkAllowPolling.Text = "Allow Start Form\'s polling.";
            this.chkAllowPolling.UseVisualStyleBackColor = true;
            this.chkAllowPolling.CheckedChanged += new System.EventHandler(this.chkAllowPolling_CheckedChanged);
            // 
            // udFormDisplay
            // 
            this.udFormDisplay.Location = new System.Drawing.Point(152, 58);
            this.udFormDisplay.Maximum = new decimal(new int[] {
            32768,
            0,
            0,
            0});
            this.udFormDisplay.Name = "udFormDisplay";
            this.udFormDisplay.Size = new System.Drawing.Size(46, 22);
            this.udFormDisplay.TabIndex = 303;
            this.udFormDisplay.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 17);
            this.label1.TabIndex = 302;
            this.label1.Text = "Form Display Speed";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(140, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(379, 17);
            this.label3.TabIndex = 300;
            this.label3.Text = "All errors will be piped out to SendRecieve, but in Red Bold";
            // 
            // cboCommand
            // 
            this.cboCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboCommand.FormattingEnabled = true;
            this.cboCommand.Items.AddRange(new object[] {
            "Poll Sensors"});
            this.cboCommand.Location = new System.Drawing.Point(401, 25);
            this.cboCommand.Name = "cboCommand";
            this.cboCommand.Size = new System.Drawing.Size(174, 24);
            this.cboCommand.TabIndex = 302;
            // 
            // btnSendCommand
            // 
            this.btnSendCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendCommand.Location = new System.Drawing.Point(581, 25);
            this.btnSendCommand.Name = "btnSendCommand";
            this.btnSendCommand.Size = new System.Drawing.Size(73, 24);
            this.btnSendCommand.TabIndex = 301;
            this.btnSendCommand.Text = "Send OpCode";
            this.btnSendCommand.UseVisualStyleBackColor = true;
            this.btnSendCommand.Click += new System.EventHandler(this.btnSendCommand_Click);
            // 
            // frmCommand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(753, 373);
            this.Controls.Add(this.cOtherByteCommands);
            this.Controls.Add(this.txtByte1);
            this.Controls.Add(this.txtByte2);
            this.Controls.Add(this.btnSendOpCode);
            this.Controls.Add(this.txtByte4);
            this.Controls.Add(this.txtByte3);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.chkRTSEnable);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmCommand";
            this.Text = "Command";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCommand_FormClosing);
            this.Load += new System.EventHandler(this.frmCommand_Load);
            this.Controls.SetChildIndex(this.chkRTSEnable, 0);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.Controls.SetChildIndex(this.txtByte3, 0);
            this.Controls.SetChildIndex(this.txtByte4, 0);
            this.Controls.SetChildIndex(this.btnSendOpCode, 0);
            this.Controls.SetChildIndex(this.txtByte2, 0);
            this.Controls.SetChildIndex(this.txtByte1, 0);
            this.Controls.SetChildIndex(this.cOtherByteCommands, 0);
            this.gMode.ResumeLayout(false);
            this.gMode.PerformLayout();
            this.gUserControl.ResumeLayout(false);
            this.gUserControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pUserControl)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabSNDRCV.ResumeLayout(false);
            this.tabSNDRCV.PerformLayout();
            this.tabConfig.ResumeLayout(false);
            this.tabConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udFormDisplay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtByte3;
        private System.Windows.Forms.ComboBox cOtherByteCommands;
        private System.Windows.Forms.Button btnSendOpCode;
        private System.Windows.Forms.TextBox txtByte1;
        private System.Windows.Forms.TextBox txtByte2;
        private System.Windows.Forms.CheckBox chkRTSEnable;
        private System.Windows.Forms.TextBox txtByte4;
        private System.Windows.Forms.TextBox txtSendRecieve;
        private System.Windows.Forms.GroupBox gMode;
        private System.Windows.Forms.RadioButton rFull;
        private System.Windows.Forms.RadioButton rPassive;
        private System.Windows.Forms.RadioButton rSafe;
        private System.Windows.Forms.RadioButton rOff;
        private System.Windows.Forms.GroupBox gUserControl;
        private System.Windows.Forms.PictureBox pUserControl;
        private System.Windows.Forms.RadioButton rUserControlOn;
        private System.Windows.Forms.RadioButton rUserControlOff;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabSNDRCV;
        private System.Windows.Forms.TabPage tabConfig;
        private System.Windows.Forms.CheckBox chkAllowPolling;
        private System.Windows.Forms.NumericUpDown udFormDisplay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboCommand;
        private System.Windows.Forms.Button btnSendCommand;
    }
}

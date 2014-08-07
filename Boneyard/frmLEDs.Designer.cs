namespace iRobotKinect
{
    partial class frmLEDs
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
            this.plLED = new System.Windows.Forms.Panel();
            this.label16 = new System.Windows.Forms.Label();
            this.tgStatus = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.udLED_Bits = new System.Windows.Forms.NumericUpDown();
            this.hsPWR_Color = new System.Windows.Forms.HScrollBar();
            this.udPower_Intensity = new System.Windows.Forms.NumericUpDown();
            this.hsPWR_Intensity = new System.Windows.Forms.HScrollBar();
            this.udPower_Color = new System.Windows.Forms.NumericUpDown();
            this.hsLED_Bits = new System.Windows.Forms.HScrollBar();
            this.label9 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.tgMax = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tgDirt_Detect = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tgClean = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tgSpot = new System.Windows.Forms.CheckBox();
            this.plLED.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udLED_Bits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPower_Intensity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPower_Color)).BeginInit();
            this.SuspendLayout();
            // 
            // plLED
            // 
            this.plLED.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.plLED.Controls.Add(this.label16);
            this.plLED.Controls.Add(this.tgStatus);
            this.plLED.Controls.Add(this.label15);
            this.plLED.Controls.Add(this.udLED_Bits);
            this.plLED.Controls.Add(this.hsPWR_Color);
            this.plLED.Controls.Add(this.udPower_Intensity);
            this.plLED.Controls.Add(this.hsPWR_Intensity);
            this.plLED.Controls.Add(this.udPower_Color);
            this.plLED.Controls.Add(this.hsLED_Bits);
            this.plLED.Controls.Add(this.label9);
            this.plLED.Controls.Add(this.label13);
            this.plLED.Controls.Add(this.tgMax);
            this.plLED.Controls.Add(this.label14);
            this.plLED.Controls.Add(this.tgDirt_Detect);
            this.plLED.Controls.Add(this.label12);
            this.plLED.Controls.Add(this.tgClean);
            this.plLED.Controls.Add(this.label10);
            this.plLED.Controls.Add(this.tgSpot);
            this.plLED.Enabled = false;
            this.plLED.Location = new System.Drawing.Point(4, 31);
            this.plLED.Name = "plLED";
            this.plLED.Size = new System.Drawing.Size(513, 246);
            this.plLED.TabIndex = 78;
            // 
            // label16
            // 
            this.label16.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(8, 202);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(96, 17);
            this.label16.TabIndex = 76;
            this.label16.Text = "PWR Intensity";
            // 
            // tgStatus
            // 
            this.tgStatus.Appearance = System.Windows.Forms.Appearance.Button;
            this.tgStatus.AutoSize = true;
            this.tgStatus.Location = new System.Drawing.Point(11, 32);
            this.tgStatus.Name = "tgStatus";
            this.tgStatus.Size = new System.Drawing.Size(58, 27);
            this.tgStatus.TabIndex = 56;
            this.tgStatus.Text = "Status";
            this.tgStatus.ThreeState = true;
            this.tgStatus.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(8, 163);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(77, 17);
            this.label15.TabIndex = 75;
            this.label15.Text = "PWR Color";
            // 
            // udLED_Bits
            // 
            this.udLED_Bits.Location = new System.Drawing.Point(11, 90);
            this.udLED_Bits.Maximum = new decimal(new int[] {
            63,
            0,
            0,
            0});
            this.udLED_Bits.Name = "udLED_Bits";
            this.udLED_Bits.Size = new System.Drawing.Size(51, 22);
            this.udLED_Bits.TabIndex = 3;
            // 
            // hsPWR_Color
            // 
            this.hsPWR_Color.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.hsPWR_Color.Location = new System.Drawing.Point(8, 180);
            this.hsPWR_Color.Maximum = 110;
            this.hsPWR_Color.Name = "hsPWR_Color";
            this.hsPWR_Color.Size = new System.Drawing.Size(492, 18);
            this.hsPWR_Color.TabIndex = 74;
            // 
            // udPower_Intensity
            // 
            this.udPower_Intensity.Location = new System.Drawing.Point(166, 90);
            this.udPower_Intensity.Name = "udPower_Intensity";
            this.udPower_Intensity.Size = new System.Drawing.Size(51, 22);
            this.udPower_Intensity.TabIndex = 4;
            // 
            // hsPWR_Intensity
            // 
            this.hsPWR_Intensity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.hsPWR_Intensity.Location = new System.Drawing.Point(8, 219);
            this.hsPWR_Intensity.Maximum = 110;
            this.hsPWR_Intensity.Name = "hsPWR_Intensity";
            this.hsPWR_Intensity.Size = new System.Drawing.Size(492, 18);
            this.hsPWR_Intensity.TabIndex = 73;
            // 
            // udPower_Color
            // 
            this.udPower_Color.Location = new System.Drawing.Point(83, 90);
            this.udPower_Color.Name = "udPower_Color";
            this.udPower_Color.Size = new System.Drawing.Size(51, 22);
            this.udPower_Color.TabIndex = 5;
            // 
            // hsLED_Bits
            // 
            this.hsLED_Bits.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.hsLED_Bits.Location = new System.Drawing.Point(8, 142);
            this.hsLED_Bits.Maximum = 72;
            this.hsLED_Bits.Name = "hsLED_Bits";
            this.hsLED_Bits.Size = new System.Drawing.Size(492, 18);
            this.hsLED_Bits.TabIndex = 72;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 70);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 17);
            this.label9.TabIndex = 6;
            this.label9.Text = "LED Bits";
            // 
            // label13
            // 
            this.label13.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 125);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(62, 17);
            this.label13.TabIndex = 71;
            this.label13.Text = "LED Bits";
            // 
            // tgMax
            // 
            this.tgMax.Appearance = System.Windows.Forms.Appearance.Button;
            this.tgMax.AutoSize = true;
            this.tgMax.Location = new System.Drawing.Point(189, 32);
            this.tgMax.Name = "tgMax";
            this.tgMax.Size = new System.Drawing.Size(43, 27);
            this.tgMax.TabIndex = 57;
            this.tgMax.Text = "Max";
            this.tgMax.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(81, 70);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(77, 17);
            this.label14.TabIndex = 67;
            this.label14.Text = "PWR Color";
            // 
            // tgDirt_Detect
            // 
            this.tgDirt_Detect.Appearance = System.Windows.Forms.Appearance.Button;
            this.tgDirt_Detect.AutoSize = true;
            this.tgDirt_Detect.Location = new System.Drawing.Point(238, 32);
            this.tgDirt_Detect.Name = "tgDirt_Detect";
            this.tgDirt_Detect.Size = new System.Drawing.Size(85, 27);
            this.tgDirt_Detect.TabIndex = 59;
            this.tgDirt_Detect.Text = "Dirt Detect";
            this.tgDirt_Detect.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(166, 70);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(96, 17);
            this.label12.TabIndex = 65;
            this.label12.Text = "PWR Intensity";
            // 
            // tgClean
            // 
            this.tgClean.Appearance = System.Windows.Forms.Appearance.Button;
            this.tgClean.AutoSize = true;
            this.tgClean.Location = new System.Drawing.Point(128, 32);
            this.tgClean.Name = "tgClean";
            this.tgClean.Size = new System.Drawing.Size(54, 27);
            this.tgClean.TabIndex = 60;
            this.tgClean.Text = "Clean";
            this.tgClean.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(11, 12);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 17);
            this.label10.TabIndex = 63;
            this.label10.Text = "LED Macros";
            // 
            // tgSpot
            // 
            this.tgSpot.Appearance = System.Windows.Forms.Appearance.Button;
            this.tgSpot.AutoSize = true;
            this.tgSpot.Location = new System.Drawing.Point(75, 32);
            this.tgSpot.Name = "tgSpot";
            this.tgSpot.Size = new System.Drawing.Size(47, 27);
            this.tgSpot.TabIndex = 61;
            this.tgSpot.Text = "Spot";
            this.tgSpot.UseVisualStyleBackColor = true;
            // 
            // LEDs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(524, 281);
            this.Controls.Add(this.plLED);
            this.Name = "LEDs";
            this.Text = "LEDs";
            this.Controls.SetChildIndex(this.plLED, 0);
            this.plLED.ResumeLayout(false);
            this.plLED.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udLED_Bits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPower_Intensity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPower_Color)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel plLED;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.CheckBox tgStatus;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.NumericUpDown udLED_Bits;
        private System.Windows.Forms.HScrollBar hsPWR_Color;
        private System.Windows.Forms.NumericUpDown udPower_Intensity;
        private System.Windows.Forms.HScrollBar hsPWR_Intensity;
        private System.Windows.Forms.NumericUpDown udPower_Color;
        private System.Windows.Forms.HScrollBar hsLED_Bits;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox tgMax;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox tgDirt_Detect;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox tgClean;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox tgSpot;
    }
}

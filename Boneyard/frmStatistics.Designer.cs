namespace iRobotKinect
{
    partial class frmStatistics
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
            this.lblSensorParseTimeCaption = new System.Windows.Forms.Label();
            this.lblSensorParseTime = new System.Windows.Forms.Label();
            this.Label24 = new System.Windows.Forms.Label();
            this.lblFormUpdated = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.lblConnected = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.lblTotalConnectionLength = new System.Windows.Forms.Label();
            this.Label11 = new System.Windows.Forms.Label();
            this.lblConnectionLength = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.lblLatestUpdate = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.Label10 = new System.Windows.Forms.Label();
            this.udExpireWarning = new System.Windows.Forms.NumericUpDown();
            this.Label26 = new System.Windows.Forms.Label();
            this.Label30 = new System.Windows.Forms.Label();
            this.lblOldHistory = new System.Windows.Forms.Label();
            this.Label27 = new System.Windows.Forms.Label();
            this.lblRoombaObjHistory = new System.Windows.Forms.Label();
            this.Label14 = new System.Windows.Forms.Label();
            this.lblRead_Roomba_Sensors = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.udExpireWarning)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSensorParseTimeCaption
            // 
            this.lblSensorParseTimeCaption.AutoSize = true;
            this.lblSensorParseTimeCaption.Location = new System.Drawing.Point(463, 57);
            this.lblSensorParseTimeCaption.Name = "lblSensorParseTimeCaption";
            this.lblSensorParseTimeCaption.Size = new System.Drawing.Size(259, 17);
            this.lblSensorParseTimeCaption.TabIndex = 313;
            this.lblSensorParseTimeCaption.Text = "Roomba Obj Sensor Parse Time (Ticks)";
            // 
            // lblSensorParseTime
            // 
            this.lblSensorParseTime.BackColor = System.Drawing.Color.Transparent;
            this.lblSensorParseTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSensorParseTime.Location = new System.Drawing.Point(724, 56);
            this.lblSensorParseTime.Name = "lblSensorParseTime";
            this.lblSensorParseTime.Size = new System.Drawing.Size(76, 20);
            this.lblSensorParseTime.TabIndex = 312;
            this.lblSensorParseTime.Text = "Current";
            this.lblSensorParseTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label24
            // 
            this.Label24.AutoSize = true;
            this.Label24.Location = new System.Drawing.Point(524, 78);
            this.Label24.Name = "Label24";
            this.Label24.Size = new System.Drawing.Size(196, 17);
            this.Label24.TabIndex = 311;
            this.Label24.Text = "Form Sensor Read Time (MS)";
            // 
            // lblFormUpdated
            // 
            this.lblFormUpdated.BackColor = System.Drawing.Color.Transparent;
            this.lblFormUpdated.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFormUpdated.Location = new System.Drawing.Point(724, 78);
            this.lblFormUpdated.Name = "lblFormUpdated";
            this.lblFormUpdated.Size = new System.Drawing.Size(76, 20);
            this.lblFormUpdated.TabIndex = 310;
            this.lblFormUpdated.Text = "Current";
            this.lblFormUpdated.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(618, 121);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(76, 17);
            this.Label2.TabIndex = 308;
            this.Label2.Text = "Connected";
            // 
            // lblConnected
            // 
            this.lblConnected.Location = new System.Drawing.Point(697, 121);
            this.lblConnected.Name = "lblConnected";
            this.lblConnected.Size = new System.Drawing.Size(103, 20);
            this.lblConnected.TabIndex = 309;
            this.lblConnected.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(454, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(268, 17);
            this.label1.TabIndex = 307;
            this.label1.Text = "Roomba Obj Sensor Update Time (Ticks)";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(724, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 20);
            this.label3.TabIndex = 306;
            this.label3.Text = "Current";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(641, 162);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(74, 17);
            this.Label7.TabIndex = 304;
            this.Label7.Text = "Total Polls";
            // 
            // lblTotalConnectionLength
            // 
            this.lblTotalConnectionLength.Location = new System.Drawing.Point(722, 161);
            this.lblTotalConnectionLength.Name = "lblTotalConnectionLength";
            this.lblTotalConnectionLength.Size = new System.Drawing.Size(78, 20);
            this.lblTotalConnectionLength.TabIndex = 305;
            this.lblTotalConnectionLength.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(602, 141);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(113, 17);
            this.Label11.TabIndex = 302;
            this.Label11.Text = "Connection Polls";
            // 
            // lblConnectionLength
            // 
            this.lblConnectionLength.Location = new System.Drawing.Point(721, 141);
            this.lblConnectionLength.Name = "lblConnectionLength";
            this.lblConnectionLength.Size = new System.Drawing.Size(79, 20);
            this.lblConnectionLength.TabIndex = 303;
            this.lblConnectionLength.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(509, 101);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(97, 17);
            this.Label8.TabIndex = 300;
            this.Label8.Text = "Latest Update";
            // 
            // lblLatestUpdate
            // 
            this.lblLatestUpdate.Location = new System.Drawing.Point(613, 101);
            this.lblLatestUpdate.Name = "lblLatestUpdate";
            this.lblLatestUpdate.Size = new System.Drawing.Size(187, 20);
            this.lblLatestUpdate.TabIndex = 301;
            this.lblLatestUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(9, 35);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.checkBox1.Size = new System.Drawing.Size(230, 21);
            this.checkBox1.TabIndex = 323;
            this.checkBox1.Text = "Use Form based History Method";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(9, 63);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(104, 17);
            this.Label10.TabIndex = 322;
            this.Label10.Text = "Expire Warning";
            // 
            // udExpireWarning
            // 
            this.udExpireWarning.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.udExpireWarning.Location = new System.Drawing.Point(118, 61);
            this.udExpireWarning.Maximum = new decimal(new int[] {
            500000,
            0,
            0,
            0});
            this.udExpireWarning.Name = "udExpireWarning";
            this.udExpireWarning.Size = new System.Drawing.Size(59, 22);
            this.udExpireWarning.TabIndex = 317;
            this.udExpireWarning.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.udExpireWarning.Value = new decimal(new int[] {
            850,
            0,
            0,
            0});
            // 
            // Label26
            // 
            this.Label26.AutoSize = true;
            this.Label26.BackColor = System.Drawing.Color.Transparent;
            this.Label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label26.Location = new System.Drawing.Point(9, 100);
            this.Label26.Name = "Label26";
            this.Label26.Size = new System.Drawing.Size(245, 17);
            this.Label26.TabIndex = 330;
            this.Label26.Text = "Program function Timing: (Ticks)";
            // 
            // Label30
            // 
            this.Label30.AutoSize = true;
            this.Label30.BackColor = System.Drawing.Color.Transparent;
            this.Label30.Location = new System.Drawing.Point(230, 141);
            this.Label30.Name = "Label30";
            this.Label30.Size = new System.Drawing.Size(198, 17);
            this.Label30.TabIndex = 329;
            this.Label30.Text = "Form based History Overhead";
            // 
            // lblOldHistory
            // 
            this.lblOldHistory.BackColor = System.Drawing.Color.Transparent;
            this.lblOldHistory.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblOldHistory.Location = new System.Drawing.Point(430, 141);
            this.lblOldHistory.Name = "lblOldHistory";
            this.lblOldHistory.Size = new System.Drawing.Size(76, 20);
            this.lblOldHistory.TabIndex = 328;
            this.lblOldHistory.Text = "Current";
            this.lblOldHistory.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label27
            // 
            this.Label27.AutoSize = true;
            this.Label27.BackColor = System.Drawing.Color.Transparent;
            this.Label27.Location = new System.Drawing.Point(226, 120);
            this.Label27.Name = "Label27";
            this.Label27.Size = new System.Drawing.Size(202, 17);
            this.Label27.TabIndex = 327;
            this.Label27.Text = "Roomba Obj History Overhead";
            // 
            // lblRoombaObjHistory
            // 
            this.lblRoombaObjHistory.BackColor = System.Drawing.Color.Transparent;
            this.lblRoombaObjHistory.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRoombaObjHistory.Location = new System.Drawing.Point(430, 120);
            this.lblRoombaObjHistory.Name = "lblRoombaObjHistory";
            this.lblRoombaObjHistory.Size = new System.Drawing.Size(76, 20);
            this.lblRoombaObjHistory.TabIndex = 326;
            this.lblRoombaObjHistory.Text = "Current";
            this.lblRoombaObjHistory.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label14
            // 
            this.Label14.AutoSize = true;
            this.Label14.BackColor = System.Drawing.Color.Transparent;
            this.Label14.Location = new System.Drawing.Point(265, 99);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(165, 17);
            this.Label14.TabIndex = 325;
            this.Label14.Text = "Read Roomba Sensors()";
            // 
            // lblRead_Roomba_Sensors
            // 
            this.lblRead_Roomba_Sensors.BackColor = System.Drawing.Color.Transparent;
            this.lblRead_Roomba_Sensors.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRead_Roomba_Sensors.Location = new System.Drawing.Point(430, 99);
            this.lblRead_Roomba_Sensors.Name = "lblRead_Roomba_Sensors";
            this.lblRead_Roomba_Sensors.Size = new System.Drawing.Size(76, 20);
            this.lblRead_Roomba_Sensors.TabIndex = 324;
            this.lblRead_Roomba_Sensors.Text = "Current";
            this.lblRead_Roomba_Sensors.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(30, 223);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(517, 22);
            this.textBox1.TabIndex = 331;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(555, 222);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(32, 24);
            this.button1.TabIndex = 332;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 196);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 17);
            this.label4.TabIndex = 333;
            this.label4.Text = "Save these Metrics";
            // 
            // frmStatistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(806, 304);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Label26);
            this.Controls.Add(this.Label30);
            this.Controls.Add(this.lblOldHistory);
            this.Controls.Add(this.Label27);
            this.Controls.Add(this.lblRoombaObjHistory);
            this.Controls.Add(this.Label14);
            this.Controls.Add(this.lblRead_Roomba_Sensors);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.Label10);
            this.Controls.Add(this.udExpireWarning);
            this.Controls.Add(this.lblSensorParseTimeCaption);
            this.Controls.Add(this.lblSensorParseTime);
            this.Controls.Add(this.Label24);
            this.Controls.Add(this.lblFormUpdated);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.lblConnected);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.lblTotalConnectionLength);
            this.Controls.Add(this.Label11);
            this.Controls.Add(this.lblConnectionLength);
            this.Controls.Add(this.Label8);
            this.Controls.Add(this.lblLatestUpdate);
            this.Name = "frmStatistics";
            this.Text = "Statistics";
            this.Controls.SetChildIndex(this.lblLatestUpdate, 0);
            this.Controls.SetChildIndex(this.Label8, 0);
            this.Controls.SetChildIndex(this.lblConnectionLength, 0);
            this.Controls.SetChildIndex(this.Label11, 0);
            this.Controls.SetChildIndex(this.lblTotalConnectionLength, 0);
            this.Controls.SetChildIndex(this.Label7, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.lblConnected, 0);
            this.Controls.SetChildIndex(this.Label2, 0);
            this.Controls.SetChildIndex(this.lblFormUpdated, 0);
            this.Controls.SetChildIndex(this.Label24, 0);
            this.Controls.SetChildIndex(this.lblSensorParseTime, 0);
            this.Controls.SetChildIndex(this.lblSensorParseTimeCaption, 0);
            this.Controls.SetChildIndex(this.udExpireWarning, 0);
            this.Controls.SetChildIndex(this.Label10, 0);
            this.Controls.SetChildIndex(this.checkBox1, 0);
            this.Controls.SetChildIndex(this.lblRead_Roomba_Sensors, 0);
            this.Controls.SetChildIndex(this.Label14, 0);
            this.Controls.SetChildIndex(this.lblRoombaObjHistory, 0);
            this.Controls.SetChildIndex(this.Label27, 0);
            this.Controls.SetChildIndex(this.lblOldHistory, 0);
            this.Controls.SetChildIndex(this.Label30, 0);
            this.Controls.SetChildIndex(this.Label26, 0);
            this.Controls.SetChildIndex(this.textBox1, 0);
            this.Controls.SetChildIndex(this.button1, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            ((System.ComponentModel.ISupportInitialize)(this.udExpireWarning)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSensorParseTimeCaption;
        internal System.Windows.Forms.Label lblSensorParseTime;
        private System.Windows.Forms.Label Label24;
        internal System.Windows.Forms.Label lblFormUpdated;
        private System.Windows.Forms.Label Label2;
        private System.Windows.Forms.Label lblConnected;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label Label7;
        private System.Windows.Forms.Label lblTotalConnectionLength;
        private System.Windows.Forms.Label Label11;
        private System.Windows.Forms.Label lblConnectionLength;
        private System.Windows.Forms.Label Label8;
        private System.Windows.Forms.Label lblLatestUpdate;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label Label10;
        private System.Windows.Forms.NumericUpDown udExpireWarning;
        private System.Windows.Forms.Label Label26;
        private System.Windows.Forms.Label Label30;
        internal System.Windows.Forms.Label lblOldHistory;
        private System.Windows.Forms.Label Label27;
        internal System.Windows.Forms.Label lblRoombaObjHistory;
        private System.Windows.Forms.Label Label14;
        internal System.Windows.Forms.Label lblRead_Roomba_Sensors;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
    }
}

namespace iRobotKinect
{
    partial class frmGraph
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.vSubtract = new System.Windows.Forms.VScrollBar();
            this.pChart = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.udIgnore = new System.Windows.Forms.NumericUpDown();
            this.Label20 = new System.Windows.Forms.Label();
            this.udSubtract = new System.Windows.Forms.NumericUpDown();
            this.Label19 = new System.Windows.Forms.Label();
            this.udHeight = new System.Windows.Forms.NumericUpDown();
            this.Label17 = new System.Windows.Forms.Label();
            this.udWidth = new System.Windows.Forms.NumericUpDown();
            this.Label18 = new System.Windows.Forms.Label();
            this.udXSlice = new System.Windows.Forms.NumericUpDown();
            this.Label16 = new System.Windows.Forms.Label();
            this.udYSlice = new System.Windows.Forms.NumericUpDown();
            this.Label15 = new System.Windows.Forms.Label();
            this.udDataPoints = new System.Windows.Forms.NumericUpDown();
            this.udIncrement = new System.Windows.Forms.NumericUpDown();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pChart)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udIgnore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udSubtract)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udXSlice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udYSlice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udDataPoints)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udIncrement)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(3, 28);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(399, 363);
            this.tabControl1.TabIndex = 295;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.vSubtract);
            this.tabPage1.Controls.Add(this.pChart);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(391, 334);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Graph";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // vSubtract
            // 
            this.vSubtract.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.vSubtract.Location = new System.Drawing.Point(369, 3);
            this.vSubtract.Maximum = 700;
            this.vSubtract.Name = "vSubtract";
            this.vSubtract.Size = new System.Drawing.Size(22, 326);
            this.vSubtract.TabIndex = 269;
            this.vSubtract.Value = 50;
            // 
            // pChart
            // 
            this.pChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pChart.Location = new System.Drawing.Point(3, 2);
            this.pChart.Name = "pChart";
            this.pChart.Size = new System.Drawing.Size(365, 327);
            this.pChart.TabIndex = 270;
            this.pChart.TabStop = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.udIgnore);
            this.tabPage2.Controls.Add(this.Label20);
            this.tabPage2.Controls.Add(this.udSubtract);
            this.tabPage2.Controls.Add(this.Label19);
            this.tabPage2.Controls.Add(this.udHeight);
            this.tabPage2.Controls.Add(this.Label17);
            this.tabPage2.Controls.Add(this.udWidth);
            this.tabPage2.Controls.Add(this.Label18);
            this.tabPage2.Controls.Add(this.udXSlice);
            this.tabPage2.Controls.Add(this.Label16);
            this.tabPage2.Controls.Add(this.udYSlice);
            this.tabPage2.Controls.Add(this.Label15);
            this.tabPage2.Controls.Add(this.udDataPoints);
            this.tabPage2.Controls.Add(this.udIncrement);
            this.tabPage2.Controls.Add(this.Label4);
            this.tabPage2.Controls.Add(this.Label6);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(391, 334);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Config";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // udIgnore
            // 
            this.udIgnore.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.udIgnore.Location = new System.Drawing.Point(192, 59);
            this.udIgnore.Maximum = new decimal(new int[] {
            500000,
            0,
            0,
            0});
            this.udIgnore.Name = "udIgnore";
            this.udIgnore.Size = new System.Drawing.Size(87, 22);
            this.udIgnore.TabIndex = 294;
            this.udIgnore.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.udIgnore.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // Label20
            // 
            this.Label20.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Label20.AutoSize = true;
            this.Label20.Location = new System.Drawing.Point(189, 36);
            this.Label20.Name = "Label20";
            this.Label20.Size = new System.Drawing.Size(157, 17);
            this.Label20.TabIndex = 295;
            this.Label20.Text = "Ignore Data Points over";
            // 
            // udSubtract
            // 
            this.udSubtract.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.udSubtract.Location = new System.Drawing.Point(55, 150);
            this.udSubtract.Maximum = new decimal(new int[] {
            500000,
            0,
            0,
            0});
            this.udSubtract.Name = "udSubtract";
            this.udSubtract.Size = new System.Drawing.Size(59, 22);
            this.udSubtract.TabIndex = 292;
            this.udSubtract.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.udSubtract.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // Label19
            // 
            this.Label19.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Label19.AutoSize = true;
            this.Label19.Location = new System.Drawing.Point(52, 127);
            this.Label19.Name = "Label19";
            this.Label19.Size = new System.Drawing.Size(61, 17);
            this.Label19.TabIndex = 293;
            this.Label19.Text = "Subtract";
            // 
            // udHeight
            // 
            this.udHeight.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.udHeight.Location = new System.Drawing.Point(94, 40);
            this.udHeight.Maximum = new decimal(new int[] {
            500000,
            0,
            0,
            0});
            this.udHeight.Name = "udHeight";
            this.udHeight.Size = new System.Drawing.Size(59, 22);
            this.udHeight.TabIndex = 290;
            this.udHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.udHeight.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // Label17
            // 
            this.Label17.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Label17.AutoSize = true;
            this.Label17.Location = new System.Drawing.Point(91, 17);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(49, 17);
            this.Label17.TabIndex = 291;
            this.Label17.Text = "Height";
            // 
            // udWidth
            // 
            this.udWidth.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.udWidth.Location = new System.Drawing.Point(93, 92);
            this.udWidth.Maximum = new decimal(new int[] {
            500000,
            0,
            0,
            0});
            this.udWidth.Name = "udWidth";
            this.udWidth.Size = new System.Drawing.Size(59, 22);
            this.udWidth.TabIndex = 288;
            this.udWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.udWidth.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // Label18
            // 
            this.Label18.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Label18.AutoSize = true;
            this.Label18.Location = new System.Drawing.Point(90, 69);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(44, 17);
            this.Label18.TabIndex = 289;
            this.Label18.Text = "Width";
            // 
            // udXSlice
            // 
            this.udXSlice.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.udXSlice.Location = new System.Drawing.Point(20, 38);
            this.udXSlice.Maximum = new decimal(new int[] {
            500000,
            0,
            0,
            0});
            this.udXSlice.Name = "udXSlice";
            this.udXSlice.Size = new System.Drawing.Size(59, 22);
            this.udXSlice.TabIndex = 286;
            this.udXSlice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.udXSlice.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // Label16
            // 
            this.Label16.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Label16.AutoSize = true;
            this.Label16.Location = new System.Drawing.Point(17, 15);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(51, 17);
            this.Label16.TabIndex = 287;
            this.Label16.Text = "X Slice";
            // 
            // udYSlice
            // 
            this.udYSlice.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.udYSlice.Location = new System.Drawing.Point(19, 90);
            this.udYSlice.Maximum = new decimal(new int[] {
            500000,
            0,
            0,
            0});
            this.udYSlice.Name = "udYSlice";
            this.udYSlice.Size = new System.Drawing.Size(59, 22);
            this.udYSlice.TabIndex = 284;
            this.udYSlice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.udYSlice.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // Label15
            // 
            this.Label15.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Label15.AutoSize = true;
            this.Label15.Location = new System.Drawing.Point(16, 67);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(51, 17);
            this.Label15.TabIndex = 285;
            this.Label15.Text = "Y Slice";
            // 
            // udDataPoints
            // 
            this.udDataPoints.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.udDataPoints.Location = new System.Drawing.Point(284, 124);
            this.udDataPoints.Maximum = new decimal(new int[] {
            500000,
            0,
            0,
            0});
            this.udDataPoints.Name = "udDataPoints";
            this.udDataPoints.Size = new System.Drawing.Size(59, 22);
            this.udDataPoints.TabIndex = 282;
            this.udDataPoints.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.udDataPoints.Value = new decimal(new int[] {
            250,
            0,
            0,
            0});
            // 
            // udIncrement
            // 
            this.udIncrement.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.udIncrement.Location = new System.Drawing.Point(199, 124);
            this.udIncrement.Maximum = new decimal(new int[] {
            500000,
            0,
            0,
            0});
            this.udIncrement.Name = "udIncrement";
            this.udIncrement.Size = new System.Drawing.Size(59, 22);
            this.udIncrement.TabIndex = 280;
            this.udIncrement.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.udIncrement.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // Label4
            // 
            this.Label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(196, 101);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(70, 17);
            this.Label4.TabIndex = 281;
            this.Label4.Text = "Increment";
            // 
            // Label6
            // 
            this.Label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(271, 101);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(81, 17);
            this.Label6.TabIndex = 283;
            this.Label6.Text = "Data Points";
            // 
            // frmGraph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(403, 394);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "frmGraph";
            this.Text = "Graph";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmGraph_FormClosed);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pChart)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udIgnore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udSubtract)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udXSlice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udYSlice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udDataPoints)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udIncrement)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        internal System.Windows.Forms.VScrollBar vSubtract;
        internal System.Windows.Forms.PictureBox pChart;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.NumericUpDown udIgnore;
        private System.Windows.Forms.Label Label20;
        private System.Windows.Forms.NumericUpDown udSubtract;
        private System.Windows.Forms.Label Label19;
        private System.Windows.Forms.NumericUpDown udHeight;
        private System.Windows.Forms.Label Label17;
        private System.Windows.Forms.NumericUpDown udWidth;
        private System.Windows.Forms.Label Label18;
        private System.Windows.Forms.NumericUpDown udXSlice;
        private System.Windows.Forms.Label Label16;
        private System.Windows.Forms.NumericUpDown udYSlice;
        private System.Windows.Forms.Label Label15;
        private System.Windows.Forms.NumericUpDown udDataPoints;
        private System.Windows.Forms.NumericUpDown udIncrement;
        private System.Windows.Forms.Label Label4;
        private System.Windows.Forms.Label Label6;
    }
}

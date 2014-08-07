namespace iRobotKinect
{
    partial class frmVirtual
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
            this.pVirtual = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pVirtual)).BeginInit();
            this.SuspendLayout();
            // 
            // pVirtual
            // 
            this.pVirtual.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pVirtual.Location = new System.Drawing.Point(1, 3);
            this.pVirtual.Name = "pVirtual";
            this.pVirtual.Size = new System.Drawing.Size(497, 490);
            this.pVirtual.TabIndex = 0;
            this.pVirtual.TabStop = false;
            // 
            // frmVirtual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 493);
            this.Controls.Add(this.pVirtual);
            this.Name = "frmVirtual";
            this.Text = "frmVirtual";
            ((System.ComponentModel.ISupportInitialize)(this.pVirtual)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pVirtual;
    }
}
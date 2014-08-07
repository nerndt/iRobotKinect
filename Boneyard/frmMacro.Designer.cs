namespace iRobotKinect
{
    partial class frmMacro
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.gRecord = new System.Windows.Forms.GroupBox();
            this.label185 = new System.Windows.Forms.Label();
            this.comboBox6 = new System.Windows.Forms.ComboBox();
            this.pRecord = new System.Windows.Forms.PictureBox();
            this.rRecord_On = new System.Windows.Forms.RadioButton();
            this.rRecord_Off = new System.Windows.Forms.RadioButton();
            this.label183 = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.btnGetLoadDir = new System.Windows.Forms.Button();
            this.label180 = new System.Windows.Forms.Label();
            this.txtSaveMacroDir = new System.Windows.Forms.TextBox();
            this.btnGetSaveDir = new System.Windows.Forms.Button();
            this.txtLoadMacro = new System.Windows.Forms.TextBox();
            this.label165 = new System.Windows.Forms.Label();
            this.btnExecute = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.gRecord.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pRecord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(4, 275);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(609, 206);
            this.dataGridView1.TabIndex = 305;
            // 
            // gRecord
            // 
            this.gRecord.Controls.Add(this.label185);
            this.gRecord.Controls.Add(this.comboBox6);
            this.gRecord.Controls.Add(this.pRecord);
            this.gRecord.Controls.Add(this.rRecord_On);
            this.gRecord.Controls.Add(this.rRecord_Off);
            this.gRecord.Location = new System.Drawing.Point(11, 30);
            this.gRecord.Name = "gRecord";
            this.gRecord.Size = new System.Drawing.Size(168, 99);
            this.gRecord.TabIndex = 304;
            this.gRecord.TabStop = false;
            this.gRecord.Text = "Record";
            // 
            // label185
            // 
            this.label185.AutoSize = true;
            this.label185.Location = new System.Drawing.Point(6, 48);
            this.label185.Name = "label185";
            this.label185.Size = new System.Drawing.Size(97, 17);
            this.label185.TabIndex = 22;
            this.label185.Text = "Add To Macro";
            this.label185.Visible = false;
            // 
            // comboBox6
            // 
            this.comboBox6.Enabled = false;
            this.comboBox6.FormattingEnabled = true;
            this.comboBox6.Items.AddRange(new object[] {
            "On",
            "Off"});
            this.comboBox6.Location = new System.Drawing.Point(5, 68);
            this.comboBox6.Name = "comboBox6";
            this.comboBox6.Size = new System.Drawing.Size(156, 24);
            this.comboBox6.TabIndex = 32;
            this.comboBox6.Visible = false;
            // 
            // pRecord
            // 
            this.pRecord.Location = new System.Drawing.Point(107, 21);
            this.pRecord.Name = "pRecord";
            this.pRecord.Size = new System.Drawing.Size(54, 20);
            this.pRecord.TabIndex = 31;
            this.pRecord.TabStop = false;
            // 
            // rRecord_On
            // 
            this.rRecord_On.AutoSize = true;
            this.rRecord_On.Location = new System.Drawing.Point(57, 21);
            this.rRecord_On.Name = "rRecord_On";
            this.rRecord_On.Size = new System.Drawing.Size(45, 21);
            this.rRecord_On.TabIndex = 30;
            this.rRecord_On.TabStop = true;
            this.rRecord_On.Text = "On";
            this.rRecord_On.UseVisualStyleBackColor = true;
            this.rRecord_On.CheckedChanged += new System.EventHandler(this.rRecord_On_CheckedChanged);
            // 
            // rRecord_Off
            // 
            this.rRecord_Off.AutoSize = true;
            this.rRecord_Off.Location = new System.Drawing.Point(6, 21);
            this.rRecord_Off.Name = "rRecord_Off";
            this.rRecord_Off.Size = new System.Drawing.Size(45, 21);
            this.rRecord_Off.TabIndex = 29;
            this.rRecord_Off.TabStop = true;
            this.rRecord_Off.Text = "Off";
            this.rRecord_Off.UseVisualStyleBackColor = true;
            this.rRecord_Off.CheckedChanged += new System.EventHandler(this.rRecord_Off_CheckedChanged);
            // 
            // label183
            // 
            this.label183.AutoSize = true;
            this.label183.Location = new System.Drawing.Point(194, 241);
            this.label183.Name = "label183";
            this.label183.Size = new System.Drawing.Size(99, 17);
            this.label183.TabIndex = 303;
            this.label183.Text = "Macro Loaded";
            // 
            // pictureBox5
            // 
            this.pictureBox5.Location = new System.Drawing.Point(296, 246);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(56, 10);
            this.pictureBox5.TabIndex = 302;
            this.pictureBox5.TabStop = false;
            // 
            // btnGetLoadDir
            // 
            this.btnGetLoadDir.Location = new System.Drawing.Point(576, 212);
            this.btnGetLoadDir.Name = "btnGetLoadDir";
            this.btnGetLoadDir.Size = new System.Drawing.Size(31, 25);
            this.btnGetLoadDir.TabIndex = 301;
            this.btnGetLoadDir.Text = "...";
            this.btnGetLoadDir.UseVisualStyleBackColor = true;
            // 
            // label180
            // 
            this.label180.AutoSize = true;
            this.label180.Location = new System.Drawing.Point(8, 144);
            this.label180.Name = "label180";
            this.label180.Size = new System.Drawing.Size(157, 17);
            this.label180.TabIndex = 300;
            this.label180.Text = "Save Macro at this path";
            // 
            // txtSaveMacroDir
            // 
            this.txtSaveMacroDir.Location = new System.Drawing.Point(8, 165);
            this.txtSaveMacroDir.Name = "txtSaveMacroDir";
            this.txtSaveMacroDir.Size = new System.Drawing.Size(558, 22);
            this.txtSaveMacroDir.TabIndex = 299;
            // 
            // btnGetSaveDir
            // 
            this.btnGetSaveDir.Location = new System.Drawing.Point(576, 164);
            this.btnGetSaveDir.Name = "btnGetSaveDir";
            this.btnGetSaveDir.Size = new System.Drawing.Size(31, 25);
            this.btnGetSaveDir.TabIndex = 298;
            this.btnGetSaveDir.Text = "...";
            this.btnGetSaveDir.UseVisualStyleBackColor = true;
            // 
            // txtLoadMacro
            // 
            this.txtLoadMacro.Location = new System.Drawing.Point(8, 213);
            this.txtLoadMacro.Name = "txtLoadMacro";
            this.txtLoadMacro.Size = new System.Drawing.Size(559, 22);
            this.txtLoadMacro.TabIndex = 297;
            // 
            // label165
            // 
            this.label165.AutoSize = true;
            this.label165.Location = new System.Drawing.Point(8, 193);
            this.label165.Name = "label165";
            this.label165.Size = new System.Drawing.Size(83, 17);
            this.label165.TabIndex = 296;
            this.label165.Text = "Load Macro";
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(385, 241);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(71, 25);
            this.btnExecute.TabIndex = 295;
            this.btnExecute.Text = "Execute";
            this.btnExecute.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(264, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(233, 17);
            this.label1.TabIndex = 306;
            this.label1.Text = "make sure metrics are recorded too";
            // 
            // frmMacro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(621, 490);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.gRecord);
            this.Controls.Add(this.label183);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.btnGetLoadDir);
            this.Controls.Add(this.label180);
            this.Controls.Add(this.txtSaveMacroDir);
            this.Controls.Add(this.btnGetSaveDir);
            this.Controls.Add(this.txtLoadMacro);
            this.Controls.Add(this.label165);
            this.Controls.Add(this.btnExecute);
            this.Name = "frmMacro";
            this.Text = "Macro";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMacro_FormClosing);
            this.Load += new System.EventHandler(this.frmMacro_Load);
            this.Controls.SetChildIndex(this.btnExecute, 0);
            this.Controls.SetChildIndex(this.label165, 0);
            this.Controls.SetChildIndex(this.txtLoadMacro, 0);
            this.Controls.SetChildIndex(this.btnGetSaveDir, 0);
            this.Controls.SetChildIndex(this.txtSaveMacroDir, 0);
            this.Controls.SetChildIndex(this.label180, 0);
            this.Controls.SetChildIndex(this.btnGetLoadDir, 0);
            this.Controls.SetChildIndex(this.pictureBox5, 0);
            this.Controls.SetChildIndex(this.label183, 0);
            this.Controls.SetChildIndex(this.gRecord, 0);
            this.Controls.SetChildIndex(this.dataGridView1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.gRecord.ResumeLayout(false);
            this.gRecord.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pRecord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox gRecord;
        private System.Windows.Forms.Label label185;
        private System.Windows.Forms.ComboBox comboBox6;
        private System.Windows.Forms.PictureBox pRecord;
        private System.Windows.Forms.RadioButton rRecord_On;
        private System.Windows.Forms.RadioButton rRecord_Off;
        private System.Windows.Forms.Label label183;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Button btnGetLoadDir;
        private System.Windows.Forms.Label label180;
        private System.Windows.Forms.TextBox txtSaveMacroDir;
        private System.Windows.Forms.Button btnGetSaveDir;
        private System.Windows.Forms.TextBox txtLoadMacro;
        private System.Windows.Forms.Label label165;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Label label1;
    }
}

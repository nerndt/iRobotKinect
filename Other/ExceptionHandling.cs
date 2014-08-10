// User Friendly Exception Handling
// http://www.codeproject.com/Articles/7482/User-Friendly-Exception-Handling

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ExceptionHandling;
using System.Collections.Specialized;
using System.Configuration;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Net;
using System.Net.Sockets;
using System.Text;


///*****************************************************************************************/
///* ***************************************************************************************
// * Program.cs
// * ***************************************************************************************/
///*****************************************************************************************/
////'--
////'--
////'-- Jeff Atwood
////'-- http://www.codinghorror.com
////'--
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Windows.Forms;
//using ExceptionHandling;
 
//namespace WindowsFormsApplication1
//{
//    static class Program
//    {
//        /// <summary>
//        /// The main entry point for the application.
//        /// </summary>
//        [STAThread]
//        static void Main()
//        {
//            UnhandledExceptionManager.AddHandler();
//            Application.EnableVisualStyles();
//            Application.SetCompatibleTextRenderingDefault(false);
//            Application.Run(new Form1());
//        }
//    }
//}
  
 /*****************************************************************************************/
/* ***************************************************************************************
 * Form1.cs
 * ***************************************************************************************/
/*****************************************************************************************/
//'--
//'--
//'-- Jeff Atwood
//'-- http://www.codinghorror.com
//'--
 
namespace ExceptionHandling
{
	public class Form1 : System.Windows.Forms.Form
	{
		#region " Windows Form Designer generated code "
 
		public Form1()
			: base()
		{
			Load += Form1_Load;
 
			//This call is required by the Windows Form Designer.
			InitializeComponent();
 
			//Add any initialization after the InitializeComponent() call
		}
 
		//Form overrides dispose to clean up the component list.
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if ((components != null))
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
 
		//Required by the Windows Form Designer

		private System.ComponentModel.IContainer components = null;
		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.
		//Do not modify it using the code editor.
		internal System.Windows.Forms.TabControl TabControl1;
		internal System.Windows.Forms.TabPage TabPage1;
		internal System.Windows.Forms.TabPage TabPage2;
		internal System.Windows.Forms.Button Button3;
		internal System.Windows.Forms.Label Label3;
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.TextBox txtWhatUserCanDo;
		internal System.Windows.Forms.TextBox txtWhatHappened;
		internal System.Windows.Forms.TextBox txtHowUserAffected;
		internal System.Windows.Forms.Button Button2;
		internal System.Windows.Forms.Button Button1;
		internal System.Windows.Forms.GroupBox GroupBox1;
		internal System.Windows.Forms.GroupBox GroupBox2;
		internal System.Windows.Forms.RadioButton radioAbortRetryIgnore;
		internal System.Windows.Forms.RadioButton radioOK;
		internal System.Windows.Forms.RadioButton radioOKCancel;
		internal System.Windows.Forms.RadioButton radioRetryCancel;
		internal System.Windows.Forms.RadioButton radioYesNo;
		internal System.Windows.Forms.RadioButton radioYesNoCancel;
		internal System.Windows.Forms.RadioButton radioError;
		internal System.Windows.Forms.RadioButton radioExclamation;
		internal System.Windows.Forms.RadioButton radioQuestion;
		internal System.Windows.Forms.RadioButton radioInformation;
		internal System.Windows.Forms.GroupBox GroupBox3;
		internal System.Windows.Forms.RadioButton radioButtonDefault;
		internal System.Windows.Forms.RadioButton radioButton3;
		internal System.Windows.Forms.RadioButton radioButton2;
		internal System.Windows.Forms.RadioButton radioButton1;
		internal System.Windows.Forms.CheckBox checkEmailHandledException;
		internal System.Windows.Forms.CheckBox CheckBox1;
		internal System.Windows.Forms.Label Label5;
		internal System.Windows.Forms.TextBox txtMoreInfo;
		internal System.Windows.Forms.TextBox txtSMTPPort;
		internal System.Windows.Forms.Label Label6;
		internal System.Windows.Forms.TextBox txtSMTPServer;
		internal System.Windows.Forms.Label Label4;
		internal System.Windows.Forms.TabPage TabPage3;
		internal System.Windows.Forms.Label Label8;
		internal System.Windows.Forms.TextBox txtSMTPDomain;
		internal System.Windows.Forms.Label Label9;
		internal System.Windows.Forms.Label Label10;
		internal System.Windows.Forms.Label Label11;
		internal System.Windows.Forms.TextBox txtEmailTo;
		internal System.Windows.Forms.Label Label12;
		internal System.Windows.Forms.TextBox txtContactInfo;
		internal System.Windows.Forms.PictureBox PictureBox1;
 
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.TabControl1 = new System.Windows.Forms.TabControl();
			this.TabPage3 = new System.Windows.Forms.TabPage();
			this.PictureBox1 = new System.Windows.Forms.PictureBox();
			this.txtContactInfo = new System.Windows.Forms.TextBox();
			this.Label12 = new System.Windows.Forms.Label();
			this.txtEmailTo = new System.Windows.Forms.TextBox();
			this.Label11 = new System.Windows.Forms.Label();
			this.Label10 = new System.Windows.Forms.Label();
			this.Label9 = new System.Windows.Forms.Label();
			this.txtSMTPDomain = new System.Windows.Forms.TextBox();
			this.Label8 = new System.Windows.Forms.Label();
			this.txtSMTPPort = new System.Windows.Forms.TextBox();
			this.Label6 = new System.Windows.Forms.Label();
			this.txtSMTPServer = new System.Windows.Forms.TextBox();
			this.Label4 = new System.Windows.Forms.Label();
			this.TabPage1 = new System.Windows.Forms.TabPage();
			this.txtMoreInfo = new System.Windows.Forms.TextBox();
			this.Label5 = new System.Windows.Forms.Label();
			this.CheckBox1 = new System.Windows.Forms.CheckBox();
			this.checkEmailHandledException = new System.Windows.Forms.CheckBox();
			this.GroupBox3 = new System.Windows.Forms.GroupBox();
			this.radioButtonDefault = new System.Windows.Forms.RadioButton();
			this.radioButton3 = new System.Windows.Forms.RadioButton();
			this.radioButton2 = new System.Windows.Forms.RadioButton();
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			this.GroupBox2 = new System.Windows.Forms.GroupBox();
			this.radioInformation = new System.Windows.Forms.RadioButton();
			this.radioQuestion = new System.Windows.Forms.RadioButton();
			this.radioExclamation = new System.Windows.Forms.RadioButton();
			this.radioError = new System.Windows.Forms.RadioButton();
			this.GroupBox1 = new System.Windows.Forms.GroupBox();
			this.radioYesNoCancel = new System.Windows.Forms.RadioButton();
			this.radioYesNo = new System.Windows.Forms.RadioButton();
			this.radioRetryCancel = new System.Windows.Forms.RadioButton();
			this.radioOKCancel = new System.Windows.Forms.RadioButton();
			this.radioOK = new System.Windows.Forms.RadioButton();
			this.radioAbortRetryIgnore = new System.Windows.Forms.RadioButton();
			this.Button3 = new System.Windows.Forms.Button();
			this.Label3 = new System.Windows.Forms.Label();
			this.Label2 = new System.Windows.Forms.Label();
			this.Label1 = new System.Windows.Forms.Label();
			this.txtWhatUserCanDo = new System.Windows.Forms.TextBox();
			this.txtWhatHappened = new System.Windows.Forms.TextBox();
			this.txtHowUserAffected = new System.Windows.Forms.TextBox();
			this.Button2 = new System.Windows.Forms.Button();
			this.TabPage2 = new System.Windows.Forms.TabPage();
			this.Button1 = new System.Windows.Forms.Button();
			this.TabControl1.SuspendLayout();
			this.TabPage3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
			this.TabPage1.SuspendLayout();
			this.GroupBox3.SuspendLayout();
			this.GroupBox2.SuspendLayout();
			this.GroupBox1.SuspendLayout();
			this.TabPage2.SuspendLayout();
			this.SuspendLayout();
			//
			// TabControl1
			//
			this.TabControl1.Controls.Add(this.TabPage3);
			this.TabControl1.Controls.Add(this.TabPage1);
			this.TabControl1.Controls.Add(this.TabPage2);
			this.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TabControl1.Location = new System.Drawing.Point(0, 0);
			this.TabControl1.Name = "TabControl1";
			this.TabControl1.SelectedIndex = 0;
			this.TabControl1.Size = new System.Drawing.Size(708, 501);
			this.TabControl1.TabIndex = 0;
			//
			// TabPage3
			//
			this.TabPage3.Controls.Add(this.PictureBox1);
			this.TabPage3.Controls.Add(this.txtContactInfo);
			this.TabPage3.Controls.Add(this.Label12);
			this.TabPage3.Controls.Add(this.txtEmailTo);
			this.TabPage3.Controls.Add(this.Label11);
			this.TabPage3.Controls.Add(this.Label10);
			this.TabPage3.Controls.Add(this.Label9);
			this.TabPage3.Controls.Add(this.txtSMTPDomain);
			this.TabPage3.Controls.Add(this.Label8);
			this.TabPage3.Controls.Add(this.txtSMTPPort);
			this.TabPage3.Controls.Add(this.Label6);
			this.TabPage3.Controls.Add(this.txtSMTPServer);
			this.TabPage3.Controls.Add(this.Label4);
			this.TabPage3.Location = new System.Drawing.Point(4, 22);
			this.TabPage3.Name = "TabPage3";
			this.TabPage3.Size = new System.Drawing.Size(700, 475);
			this.TabPage3.TabIndex = 2;
			this.TabPage3.Text = "SMTP Settings";
			//
			// PictureBox1
			//
			this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
			this.PictureBox1.Location = new System.Drawing.Point(16, 152);
			this.PictureBox1.Name = "PictureBox1";
			this.PictureBox1.Size = new System.Drawing.Size(673, 163);
			this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.PictureBox1.TabIndex = 13;
			this.PictureBox1.TabStop = false;
			//
			// txtContactInfo
			//
			this.txtContactInfo.Enabled = false;
			this.txtContactInfo.Location = new System.Drawing.Point(256, 348);
			this.txtContactInfo.Name = "txtContactInfo";
			this.txtContactInfo.Size = new System.Drawing.Size(280, 20);
			this.txtContactInfo.TabIndex = 12;
			//
			// Label12
			//
			this.Label12.Location = new System.Drawing.Point(12, 352);
			this.Label12.Name = "Label12";
			this.Label12.Size = new System.Drawing.Size(216, 16);
			this.Label12.TabIndex = 11;
			this.Label12.Text = "UnhandledExceptionManager/ContactInfo";
			//
			// txtEmailTo
			//
			this.txtEmailTo.Enabled = false;
			this.txtEmailTo.Location = new System.Drawing.Point(256, 324);
			this.txtEmailTo.Name = "txtEmailTo";
			this.txtEmailTo.Size = new System.Drawing.Size(280, 20);
			this.txtEmailTo.TabIndex = 10;
			//
			// Label11
			//
			this.Label11.Location = new System.Drawing.Point(12, 328);
			this.Label11.Name = "Label11";
			this.Label11.Size = new System.Drawing.Size(200, 16);
			this.Label11.TabIndex = 9;
			this.Label11.Text = "UnhandledExceptionManager/EmailTo";
			//
			// Label10
			//
            this.Label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Label10.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label10.Location = new System.Drawing.Point(16, 128);
			this.Label10.Name = "Label10";
			this.Label10.Size = new System.Drawing.Size(284, 16);
			this.Label10.TabIndex = 8;
			this.Label10.Text = "Don\'t forget to set the mailto: address in App.Config!";
			//
			// Label9
			//
            this.Label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Label9.Location = new System.Drawing.Point(12, 12);
			this.Label9.Name = "Label9";
			this.Label9.Size = new System.Drawing.Size(460, 16);
			this.Label9.TabIndex = 7;
			this.Label9.Text = "Edit the default values in SimpleMail.vb to match your preferred outgoing mail se" +
	"rver.";
			//
			// txtSMTPDomain
			//
			this.txtSMTPDomain.Enabled = false;
			this.txtSMTPDomain.Location = new System.Drawing.Point(256, 36);
			this.txtSMTPDomain.Name = "txtSMTPDomain";
			this.txtSMTPDomain.Size = new System.Drawing.Size(256, 20);
			this.txtSMTPDomain.TabIndex = 6;
			//
			// Label8
			//
			this.Label8.Location = new System.Drawing.Point(12, 36);
			this.Label8.Name = "Label8";
			this.Label8.Size = new System.Drawing.Size(144, 16);
			this.Label8.TabIndex = 5;
			this.Label8.Text = "SimpleMail.DefaultDomain";
			//
			// txtSMTPPort
			//
			this.txtSMTPPort.Enabled = false;
			this.txtSMTPPort.Location = new System.Drawing.Point(256, 88);
			this.txtSMTPPort.MaxLength = 4;
			this.txtSMTPPort.Name = "txtSMTPPort";
			this.txtSMTPPort.Size = new System.Drawing.Size(56, 20);
			this.txtSMTPPort.TabIndex = 3;
			//
			// Label6
			//
			this.Label6.Location = new System.Drawing.Point(12, 92);
			this.Label6.Name = "Label6";
			this.Label6.Size = new System.Drawing.Size(88, 16);
			this.Label6.TabIndex = 2;
			this.Label6.Text = "SimpleMail.Port";
			//
			// txtSMTPServer
			//
			this.txtSMTPServer.Enabled = false;
			this.txtSMTPServer.Location = new System.Drawing.Point(256, 60);
			this.txtSMTPServer.Name = "txtSMTPServer";
			this.txtSMTPServer.Size = new System.Drawing.Size(256, 20);
			this.txtSMTPServer.TabIndex = 1;
			//
			// Label4
			//
			this.Label4.Location = new System.Drawing.Point(12, 64);
			this.Label4.Name = "Label4";
			this.Label4.Size = new System.Drawing.Size(100, 16);
			this.Label4.TabIndex = 0;
			this.Label4.Text = "SimpleMail.Server";
			//
			// TabPage1
			//
			this.TabPage1.Controls.Add(this.txtMoreInfo);
			this.TabPage1.Controls.Add(this.Label5);
			this.TabPage1.Controls.Add(this.CheckBox1);
			this.TabPage1.Controls.Add(this.checkEmailHandledException);
			this.TabPage1.Controls.Add(this.GroupBox3);
			this.TabPage1.Controls.Add(this.GroupBox2);
			this.TabPage1.Controls.Add(this.GroupBox1);
			this.TabPage1.Controls.Add(this.Button3);
			this.TabPage1.Controls.Add(this.Label3);
			this.TabPage1.Controls.Add(this.Label2);
			this.TabPage1.Controls.Add(this.Label1);
			this.TabPage1.Controls.Add(this.txtWhatUserCanDo);
			this.TabPage1.Controls.Add(this.txtWhatHappened);
			this.TabPage1.Controls.Add(this.txtHowUserAffected);
			this.TabPage1.Controls.Add(this.Button2);
			this.TabPage1.Location = new System.Drawing.Point(4, 22);
			this.TabPage1.Name = "TabPage1";
			this.TabPage1.Size = new System.Drawing.Size(700, 475);
			this.TabPage1.TabIndex = 0;
			this.TabPage1.Text = "Handled Exception";
			//
			// txtMoreInfo
			//
			this.txtMoreInfo.Enabled = false;
			this.txtMoreInfo.Location = new System.Drawing.Point(32, 304);
			this.txtMoreInfo.Multiline = true;
			this.txtMoreInfo.Name = "txtMoreInfo";
			this.txtMoreInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtMoreInfo.Size = new System.Drawing.Size(512, 60);
			this.txtMoreInfo.TabIndex = 7;
			//
			// Label5
			//
            this.Label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Label5.Location = new System.Drawing.Point(8, 288);
			this.Label5.Name = "Label5";
			this.Label5.Size = new System.Drawing.Size(104, 16);
			this.Label5.TabIndex = 6;
			this.Label5.Text = "More information:";
			//
			// CheckBox1
			//
			this.CheckBox1.Checked = true;
			this.CheckBox1.CheckState = System.Windows.Forms.CheckState.Checked;
			this.CheckBox1.Location = new System.Drawing.Point(32, 372);
			this.CheckBox1.Name = "CheckBox1";
			this.CheckBox1.Size = new System.Drawing.Size(312, 20);
			this.CheckBox1.TabIndex = 8;
			this.CheckBox1.Text = "Use the actual exception for the more information text";
			this.CheckBox1.CheckedChanged += new System.EventHandler(this.CheckBox1_CheckedChanged);
			//
			// checkEmailHandledException
			//
			this.checkEmailHandledException.Checked = true;
			this.checkEmailHandledException.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkEmailHandledException.Location = new System.Drawing.Point(16, 420);
			this.checkEmailHandledException.Name = "checkEmailHandledException";
			this.checkEmailHandledException.Size = new System.Drawing.Size(288, 16);
			this.checkEmailHandledException.TabIndex = 12;
			this.checkEmailHandledException.Text = "Send an email notification for this handled exception";
			//
			// GroupBox3
			//
			this.GroupBox3.Controls.Add(this.radioButtonDefault);
			this.GroupBox3.Controls.Add(this.radioButton3);
			this.GroupBox3.Controls.Add(this.radioButton2);
			this.GroupBox3.Controls.Add(this.radioButton1);
			this.GroupBox3.Location = new System.Drawing.Point(556, 324);
			this.GroupBox3.Name = "GroupBox3";
			this.GroupBox3.Size = new System.Drawing.Size(132, 104);
			this.GroupBox3.TabIndex = 11;
			this.GroupBox3.TabStop = false;
			this.GroupBox3.Text = "Default Button";
			//
			// radioButtonDefault
			//
			this.radioButtonDefault.Checked = true;
			this.radioButtonDefault.Location = new System.Drawing.Point(12, 80);
			this.radioButtonDefault.Name = "radioButtonDefault";
			this.radioButtonDefault.Size = new System.Drawing.Size(60, 16);
			this.radioButtonDefault.TabIndex = 3;
			this.radioButtonDefault.TabStop = true;
			this.radioButtonDefault.Text = "Default";
			//
			// radioButton3
			//
			this.radioButton3.Location = new System.Drawing.Point(12, 60);
			this.radioButton3.Name = "radioButton3";
			this.radioButton3.Size = new System.Drawing.Size(64, 16);
			this.radioButton3.TabIndex = 2;
			this.radioButton3.Text = "Button3";
			//
			// radioButton2
			//
			this.radioButton2.Location = new System.Drawing.Point(12, 40);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new System.Drawing.Size(64, 16);
			this.radioButton2.TabIndex = 1;
			this.radioButton2.Text = "Button2";
			//
			// radioButton1
			//
			this.radioButton1.Location = new System.Drawing.Point(12, 20);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new System.Drawing.Size(64, 16);
			this.radioButton1.TabIndex = 0;
			this.radioButton1.Text = "Button1";
			//
			// GroupBox2
			//
			this.GroupBox2.Controls.Add(this.radioInformation);
			this.GroupBox2.Controls.Add(this.radioQuestion);
			this.GroupBox2.Controls.Add(this.radioExclamation);
			this.GroupBox2.Controls.Add(this.radioError);
			this.GroupBox2.Location = new System.Drawing.Point(556, 192);
			this.GroupBox2.Name = "GroupBox2";
			this.GroupBox2.Size = new System.Drawing.Size(132, 124);
			this.GroupBox2.TabIndex = 10;
			this.GroupBox2.TabStop = false;
			this.GroupBox2.Text = "Icon";
			//
			// radioInformation
			//
			this.radioInformation.Location = new System.Drawing.Point(12, 96);
			this.radioInformation.Name = "radioInformation";
			this.radioInformation.Size = new System.Drawing.Size(84, 20);
			this.radioInformation.TabIndex = 3;
			this.radioInformation.Text = "Information";
			//
			// radioQuestion
			//
			this.radioQuestion.Location = new System.Drawing.Point(12, 72);
			this.radioQuestion.Name = "radioQuestion";
			this.radioQuestion.Size = new System.Drawing.Size(72, 20);
			this.radioQuestion.TabIndex = 2;
			this.radioQuestion.Text = "Question";
			//
			// radioExclamation
			//
			this.radioExclamation.Location = new System.Drawing.Point(12, 48);
			this.radioExclamation.Name = "radioExclamation";
			this.radioExclamation.Size = new System.Drawing.Size(88, 20);
			this.radioExclamation.TabIndex = 1;
			this.radioExclamation.Text = "Exclamation";
			//
			// radioError
			//
			this.radioError.Checked = true;
			this.radioError.Location = new System.Drawing.Point(12, 24);
			this.radioError.Name = "radioError";
			this.radioError.Size = new System.Drawing.Size(48, 20);
			this.radioError.TabIndex = 0;
			this.radioError.TabStop = true;
			this.radioError.Text = "Error";
			//
			// GroupBox1
			//
			this.GroupBox1.Controls.Add(this.radioYesNoCancel);
			this.GroupBox1.Controls.Add(this.radioYesNo);
			this.GroupBox1.Controls.Add(this.radioRetryCancel);
			this.GroupBox1.Controls.Add(this.radioOKCancel);
			this.GroupBox1.Controls.Add(this.radioOK);
			this.GroupBox1.Controls.Add(this.radioAbortRetryIgnore);
			this.GroupBox1.Location = new System.Drawing.Point(556, 12);
			this.GroupBox1.Name = "GroupBox1";
			this.GroupBox1.Size = new System.Drawing.Size(132, 172);
			this.GroupBox1.TabIndex = 9;
			this.GroupBox1.TabStop = false;
			this.GroupBox1.Text = "Buttons";
			//
			// radioYesNoCancel
			//
			this.radioYesNoCancel.Location = new System.Drawing.Point(12, 140);
			this.radioYesNoCancel.Name = "radioYesNoCancel";
			this.radioYesNoCancel.Size = new System.Drawing.Size(96, 20);
			this.radioYesNoCancel.TabIndex = 5;
			this.radioYesNoCancel.Text = "YesNoCancel";
			//
			// radioYesNo
			//
			this.radioYesNo.Location = new System.Drawing.Point(12, 116);
			this.radioYesNo.Name = "radioYesNo";
			this.radioYesNo.Size = new System.Drawing.Size(60, 20);
			this.radioYesNo.TabIndex = 4;
			this.radioYesNo.Text = "YesNo";
			//
			// radioRetryCancel
			//
			this.radioRetryCancel.Location = new System.Drawing.Point(12, 92);
			this.radioRetryCancel.Name = "radioRetryCancel";
			this.radioRetryCancel.Size = new System.Drawing.Size(96, 20);
			this.radioRetryCancel.TabIndex = 3;
			this.radioRetryCancel.Text = "RetryCancel";
			//
			// radioOKCancel
			//
			this.radioOKCancel.Location = new System.Drawing.Point(12, 68);
			this.radioOKCancel.Name = "radioOKCancel";
			this.radioOKCancel.Size = new System.Drawing.Size(76, 20);
			this.radioOKCancel.TabIndex = 2;
			this.radioOKCancel.Text = "OKCancel";
			//
			// radioOK
			//
			this.radioOK.Checked = true;
			this.radioOK.Location = new System.Drawing.Point(12, 44);
			this.radioOK.Name = "radioOK";
			this.radioOK.Size = new System.Drawing.Size(44, 20);
			this.radioOK.TabIndex = 1;
			this.radioOK.TabStop = true;
			this.radioOK.Text = "OK";
			//
			// radioAbortRetryIgnore
			//
			this.radioAbortRetryIgnore.Location = new System.Drawing.Point(12, 20);
			this.radioAbortRetryIgnore.Name = "radioAbortRetryIgnore";
			this.radioAbortRetryIgnore.Size = new System.Drawing.Size(112, 20);
			this.radioAbortRetryIgnore.TabIndex = 0;
			this.radioAbortRetryIgnore.Text = "AbortRetryIgnore";
			//
			// Button3
			//
			this.Button3.Location = new System.Drawing.Point(508, 436);
			this.Button3.Name = "Button3";
			this.Button3.Size = new System.Drawing.Size(180, 28);
			this.Button3.TabIndex = 14;
			this.Button3.Text = "Handled Exception (defaults)";
			this.Button3.Click += new System.EventHandler(this.Button3_Click);
			//
			// Label3
			//
            this.Label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Label3.Location = new System.Drawing.Point(12, 196);
			this.Label3.Name = "Label3";
			this.Label3.Size = new System.Drawing.Size(128, 16);
			this.Label3.TabIndex = 4;
			this.Label3.Text = "What the user can do:";
			//
			// Label2
			//
            this.Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Label2.Location = new System.Drawing.Point(8, 104);
			this.Label2.Name = "Label2";
			this.Label2.Size = new System.Drawing.Size(164, 16);
			this.Label2.TabIndex = 2;
			this.Label2.Text = "How the user will be affected:";
			//
			// Label1
			//
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Label1.Location = new System.Drawing.Point(12, 12);
			this.Label1.Name = "Label1";
			this.Label1.Size = new System.Drawing.Size(112, 16);
			this.Label1.TabIndex = 0;
			this.Label1.Text = "What Happened:";
			//
			// txtWhatUserCanDo
			//
			this.txtWhatUserCanDo.Location = new System.Drawing.Point(32, 220);
			this.txtWhatUserCanDo.Multiline = true;
			this.txtWhatUserCanDo.Name = "txtWhatUserCanDo";
			this.txtWhatUserCanDo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtWhatUserCanDo.Size = new System.Drawing.Size(512, 60);
			this.txtWhatUserCanDo.TabIndex = 5;
			this.txtWhatUserCanDo.Text = "List anything the user can do to resolve this problem or condition, including con" +
	"tacting (contact) or perhaps visiting http://www.codinghorror.com";
			//
			// txtWhatHappened
			//
			this.txtWhatHappened.Location = new System.Drawing.Point(32, 36);
			this.txtWhatHappened.Multiline = true;
			this.txtWhatHappened.Name = "txtWhatHappened";
			this.txtWhatHappened.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtWhatHappened.Size = new System.Drawing.Size(508, 60);
			this.txtWhatHappened.TabIndex = 1;
			this.txtWhatHappened.Text = "Describe what happened to (app) in plain, non technical terms";
			//
			// txtHowUserAffected
			//
			this.txtHowUserAffected.Location = new System.Drawing.Point(32, 128);
			this.txtHowUserAffected.Multiline = true;
			this.txtHowUserAffected.Name = "txtHowUserAffected";
			this.txtHowUserAffected.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtHowUserAffected.Size = new System.Drawing.Size(508, 60);
			this.txtHowUserAffected.TabIndex = 3;
			this.txtHowUserAffected.Text = "Describe how the user will be affected by this problem or condition; be specific." +
	"";
			//
			// Button2
			//
			this.Button2.Location = new System.Drawing.Point(320, 436);
			this.Button2.Name = "Button2";
			this.Button2.Size = new System.Drawing.Size(180, 28);
			this.Button2.TabIndex = 13;
			this.Button2.Text = "Handled Exception (customized)";
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			//
			// TabPage2
			//
			this.TabPage2.Controls.Add(this.Button1);
			this.TabPage2.Location = new System.Drawing.Point(4, 22);
			this.TabPage2.Name = "TabPage2";
			this.TabPage2.Size = new System.Drawing.Size(700, 475);
			this.TabPage2.TabIndex = 1;
			this.TabPage2.Text = "Unhandled Exception";
			//
			// Button1
			//
			this.Button1.Location = new System.Drawing.Point(262, 223);
			this.Button1.Name = "Button1";
			this.Button1.Size = new System.Drawing.Size(176, 28);
			this.Button1.TabIndex = 1;
			this.Button1.Text = "Generate Unhandled Exception";
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			//
			// Form1
			//
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(708, 501);
			this.Controls.Add(this.TabControl1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.TabControl1.ResumeLayout(false);
			this.TabPage3.ResumeLayout(false);
			this.TabPage3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
			this.TabPage1.ResumeLayout(false);
			this.TabPage1.PerformLayout();
			this.GroupBox3.ResumeLayout(false);
			this.GroupBox2.ResumeLayout(false);
			this.GroupBox1.ResumeLayout(false);
			this.TabPage2.ResumeLayout(false);
			this.ResumeLayout(false);
		}
 
		#endregion " Windows Form Designer generated code "
 
		private void GenerateException()
		{
			//-- just a simple "object not initialized" exception (should read "as new")
			System.Collections.Specialized.NameValueCollection x = null;
			MessageBox.Show(x.Count.ToString());
		}
 
		private void Button1_Click(System.Object sender, System.EventArgs e)
		{
			GenerateException();
		}
 
		private void Button2_Click(System.Object sender, System.EventArgs e)
		{
			try
			{
				GenerateException();
			}
			catch (Exception ex)
			{
				HandledExceptionManager.EmailError = checkEmailHandledException.Checked;
				if (CheckBox1.Checked)
				{
					//-- use exception as "more"
					HandledExceptionManager.ShowDialog(txtWhatHappened.Text, txtHowUserAffected.Text, txtWhatUserCanDo.Text, ex, GetButtonType(), GetIconType(), GetDefaultButton());
				}
				else
				{
					//-- use custom text as "more"
					HandledExceptionManager.ShowDialog(txtWhatHappened.Text, txtHowUserAffected.Text, txtWhatUserCanDo.Text, txtMoreInfo.Text, GetButtonType(), GetIconType(), GetDefaultButton());
				}
			}
		}
 
		private void Button3_Click(System.Object sender, System.EventArgs e)
		{
			try
			{
				GenerateException();
			}
			catch (Exception ex)
			{
				HandledExceptionManager.EmailError = checkEmailHandledException.Checked;
				//-- minimal form, no extra params
				HandledExceptionManager.ShowDialog(txtWhatHappened.Text, txtHowUserAffected.Text, txtWhatUserCanDo.Text, ex);
			}
		}
 
		private HandledExceptionManager.UserErrorDefaultButton GetDefaultButton()
		{
			if (radioButton1.Checked)
			{
				return HandledExceptionManager.UserErrorDefaultButton.Button1;
			}
			if (radioButton2.Checked)
			{
				return HandledExceptionManager.UserErrorDefaultButton.Button2;
			}
			if (radioButton3.Checked)
			{
				return HandledExceptionManager.UserErrorDefaultButton.Button3;
			}
			return HandledExceptionManager.UserErrorDefaultButton.Default;
		}
 
		private MessageBoxIcon GetIconType()
		{
			if (radioExclamation.Checked)
			{
				return MessageBoxIcon.Exclamation;
			}
			if (radioQuestion.Checked)
			{
				return MessageBoxIcon.Question;
			}
			if (radioInformation.Checked)
			{
				return MessageBoxIcon.Information;
			}
			return MessageBoxIcon.Error;
		}
 
		private MessageBoxButtons GetButtonType()
		{
			if (radioAbortRetryIgnore.Checked)
			{
				return MessageBoxButtons.AbortRetryIgnore;
			}
			if (radioOKCancel.Checked)
			{
				return MessageBoxButtons.OKCancel;
			}
			if (radioRetryCancel.Checked)
			{
				return MessageBoxButtons.RetryCancel;
			}
			if (radioYesNo.Checked)
			{
				return MessageBoxButtons.YesNo;
			}
			if (radioYesNoCancel.Checked)
			{
				return MessageBoxButtons.YesNoCancel;
			}
			return MessageBoxButtons.OK;
		}
 
		private void CheckBox1_CheckedChanged(System.Object sender, System.EventArgs e)
		{
			if (CheckBox1.Checked == true)
			{
				if (!string.IsNullOrEmpty(txtMoreInfo.Text))
				{
					txtMoreInfo.Tag = txtMoreInfo.Text;
					txtMoreInfo.Text = "";
				}
			}
			else
			{
				if (string.IsNullOrEmpty(txtMoreInfo.Text))
				{
					if ((txtMoreInfo.Tag != null))
					{
						txtMoreInfo.Text = txtMoreInfo.Tag.ToString();
					}
				}
			}
			txtMoreInfo.Enabled = !CheckBox1.Checked;
		}
 
		private void Form1_Load(object sender, System.EventArgs e)
		{
			ExceptionHandling.SimpleMail.SMTPClient smtp = new ExceptionHandling.SimpleMail.SMTPClient();
 
			txtSMTPDomain.Text = smtp.DefaultDomain;
			txtSMTPPort.Text = smtp.Port.ToString();
			txtSMTPServer.Text = smtp.Server.ToString();
			txtEmailTo.Text = AppSettings.GetString("UnhandledExceptionManager/EmailTo");
			txtContactInfo.Text = AppSettings.GetString("UnhandledExceptionManager/ContactInfo");
		}
	}
}
  
 /*****************************************************************************************/
/* ***************************************************************************************
 * AppSettings.cs
 * ***************************************************************************************/
/*****************************************************************************************/
//'--
//'--
//'-- Jeff Atwood
//'-- http://www.codinghorror.com
//'--

//using System;
//using System.Collections.Specialized;
//using System.Configuration;
//using System.Reflection;
//using System.Text.RegularExpressions;
 
//'-- Class for returning general settings related to our Application, such as..
//'--
//'--  ** .config file settings
//'--  ** runtime version
//'--  ** application version
//'--  ** version and build date
//'--  ** whether we're being debugged or not
//'--  ** our security zone
//'--  ** our path and filename
//'--  ** any command line arguments we have

 
namespace ExceptionHandling
{
	public class AppSettings
	{
		private static string _strAppBase;
		private static string _strConfigPath;
		private static string _strSecurityZone;
		private static string _strRuntimeVersion;
		private static System.Collections.Specialized.NameValueCollection _objCommandLineArgs = null;
 
		private static System.Collections.Specialized.NameValueCollection _objAssemblyAttribs = null;
 
		private AppSettings()
		{
			// to keep this class from being creatable as an instance.
		}
 
		#region "Properties"
 
		public static bool DebugMode
		{
			get
			{
				if (CommandLineArgs["debug"] == null)
				{
					return System.Diagnostics.Debugger.IsAttached;
				}
				else
				{
					return true;
				}
			}
		}
 
		public static string AppBuildDate
		{
			get
			{
				if (_objAssemblyAttribs == null)
				{
					_objAssemblyAttribs = GetAssemblyAttribs();
				}
				return _objAssemblyAttribs["BuildDate"];
			}
		}
 
		public static string AppProduct
		{
			get
			{
				if (_objAssemblyAttribs == null)
				{
					_objAssemblyAttribs = GetAssemblyAttribs();
				}
				return _objAssemblyAttribs["Product"];
			}
		}
 
		public static string AppCompany
		{
			get
			{
				if (_objAssemblyAttribs == null)
				{
					_objAssemblyAttribs = GetAssemblyAttribs();
				}
				return _objAssemblyAttribs["Company"];
			}
		}
 
		public static string AppCopyright
		{
			get
			{
				if (_objAssemblyAttribs == null)
				{
					_objAssemblyAttribs = GetAssemblyAttribs();
				}
				return _objAssemblyAttribs["Copyright"];
			}
		}
 
		public static string AppDescription
		{
			get
			{
				if (_objAssemblyAttribs == null)
				{
					_objAssemblyAttribs = GetAssemblyAttribs();
				}
				return _objAssemblyAttribs["Description"];
			}
		}
 
		public static string AppTitle
		{
			get
			{
				if (_objAssemblyAttribs == null)
				{
					_objAssemblyAttribs = GetAssemblyAttribs();
				}
				return _objAssemblyAttribs["Title"];
			}
		}
 
		public static string AppFileName
		{
			get { return Regex.Match(AppPath, "[^/]*.(exe|dll)", RegexOptions.IgnoreCase).ToString(); }
		}
 
		public static string AppPath
		{
			get
			{
				if (_objAssemblyAttribs == null)
				{
					_objAssemblyAttribs = GetAssemblyAttribs();
				}
				return _objAssemblyAttribs["CodeBase"];
			}
		}
 
		public static string AppFullName
		{
			get
			{
				if (_objAssemblyAttribs == null)
				{
					_objAssemblyAttribs = GetAssemblyAttribs();
				}
				return _objAssemblyAttribs["FullName"];
			}
		}
 
		public static NameValueCollection CommandLineArgs
		{
			get
			{
				if (_objCommandLineArgs == null)
				{
					_objCommandLineArgs = GetCommandLineArgs();
				}
				return _objCommandLineArgs;
			}
		}
 
		public static bool CommandLineHelpRequested
		{
			get
			{
				if (_objCommandLineArgs == null)
				{
					_objCommandLineArgs = GetCommandLineArgs();
				}
				if (!_objCommandLineArgs.HasKeys())
				{
					return false;
				}
 
				const string strHelpRegEx = "^(help|\\?)";
 
				string strKey = null;
				//foreach (string strKey_loopVariable in _objCommandLineArgs.AllKeys())
				foreach (string strKey_loopVariable in _objCommandLineArgs.AllKeys)
				{
					strKey = strKey_loopVariable;
					if (Regex.IsMatch(strKey, strHelpRegEx, RegexOptions.IgnoreCase))
					{
						return true;
					}
					if (Regex.IsMatch(_objCommandLineArgs[strKey], strHelpRegEx, RegexOptions.IgnoreCase))
					{
						return true;
					}
				}
				return false;
			}
		}
 
		public static string RuntimeVersion
		{
			get
			{
				if (_strRuntimeVersion == null)
				{
					//-- returns 1.0.3705.288; we don't want that much detail
					_strRuntimeVersion = Regex.Match(System.Environment.Version.ToString(), "\\d+.\\d+.\\d+").ToString();
				}
				return _strRuntimeVersion;
			}
		}
 
		public static string AppVersion
		{
			get
			{
				if (_objAssemblyAttribs == null)
				{
					_objAssemblyAttribs = GetAssemblyAttribs();
				}
				return _objAssemblyAttribs["Version"];
			}
		}
 
		public static string ConfigPath
		{
			get
			{
				if (_strConfigPath == null)
				{
					_strConfigPath = Convert.ToString(System.AppDomain.CurrentDomain.GetData("APP_CONFIG_FILE"));
				}
				return _strConfigPath;
			}
		}
 
		public static string AppBase
		{
			get
			{
				if (_strAppBase == null)
				{
					_strAppBase = Convert.ToString(System.AppDomain.CurrentDomain.GetData("APPBASE"));
				}
				return _strAppBase;
			}
		}
 
		public static string SecurityZone
		{
			get
			{
				if (_strSecurityZone == null)
				{
					_strSecurityZone = System.Security.Policy.Zone.CreateFromUrl(AppBase).SecurityZone.ToString();
				}
				return _strSecurityZone;
			}
		}
 
		#endregion "Properties"
 
		private static Assembly GetEntryAssembly()
		{
			if (System.Reflection.Assembly.GetEntryAssembly() == null)
			{
				return System.Reflection.Assembly.GetCallingAssembly();
			}
			else
			{
				return System.Reflection.Assembly.GetEntryAssembly();
			}
		}
 
		//--
		//-- returns string name / string value pair of all attribs
		//-- for specified assembly
		//--
		//-- note that Assembly* values are pulled from AssemblyInfo file in project folder
		//--
		//-- Product         = AssemblyProduct string
		//-- Copyright       = AssemblyCopyright string
		//-- Company         = AssemblyCompany string
		//-- Description     = AssemblyDescription string
		//-- Title           = AssemblyTitle string
		//--
		private static NameValueCollection GetAssemblyAttribs()
		{
			object[] objAttributes = null;
			object objAttribute = null;
			string strAttribName = null;
			string strAttribValue = null;
			System.Collections.Specialized.NameValueCollection objNameValueCollection = new System.Collections.Specialized.NameValueCollection();
			System.Reflection.Assembly objAssembly = GetEntryAssembly();
 
			objAttributes = objAssembly.GetCustomAttributes(false);
			foreach (object objAttribute_loopVariable in objAttributes)
			{
				objAttribute = objAttribute_loopVariable;
				strAttribName = objAttribute.GetType().ToString();
				strAttribValue = "";
				switch (strAttribName)
				{
					case "System.Reflection.AssemblyTrademarkAttribute":
						strAttribName = "Trademark";
						strAttribValue = ((AssemblyTrademarkAttribute)objAttribute).Trademark.ToString();
						break;
					case "System.Reflection.AssemblyProductAttribute":
						strAttribName = "Product";
						strAttribValue = ((AssemblyProductAttribute)objAttribute).Product.ToString();
						break;
					case "System.Reflection.AssemblyCopyrightAttribute":
						strAttribName = "Copyright";
						strAttribValue = ((AssemblyCopyrightAttribute)objAttribute).Copyright.ToString();
						break;
					case "System.Reflection.AssemblyCompanyAttribute":
						strAttribName = "Company";
						strAttribValue = ((AssemblyCompanyAttribute)objAttribute).Company.ToString();
						break;
					case "System.Reflection.AssemblyTitleAttribute":
						strAttribName = "Title";
						strAttribValue = ((AssemblyTitleAttribute)objAttribute).Title.ToString();
						break;
					case "System.Reflection.AssemblyDescriptionAttribute":
						strAttribName = "Description";
						strAttribValue = ((AssemblyDescriptionAttribute)objAttribute).Description.ToString();
						break;
					default:
						break;
					//Console.WriteLine(strAttribName)
				}
				if (!string.IsNullOrEmpty(strAttribValue))
				{
					if (string.IsNullOrEmpty(objNameValueCollection[strAttribName]))
					{
						objNameValueCollection.Add(strAttribName, strAttribValue);
					}
				}
			}
 
			//-- add some extra values that are not in the AssemblyInfo, but nice to have
			var _with1 = objNameValueCollection;
			_with1.Add("CodeBase", objAssembly.CodeBase.Replace("file:///", ""));
			_with1.Add("BuildDate", AssemblyBuildDate(objAssembly).ToString());
			_with1.Add("Version", objAssembly.GetName().Version.ToString());
			_with1.Add("FullName", objAssembly.FullName);
 
			//-- we must have certain assembly keys to proceed.
			if (objNameValueCollection["Product"] == null)
			{
				throw new MissingFieldException("The AssemblyInfo file for the assembly " + objAssembly.GetName().Name + " must have the <Assembly:AssemblyProduct()> key populated.");
			}
			if (objNameValueCollection["Company"] == null)
			{
				throw new MissingFieldException("The AssemblyInfo file for the assembly " + objAssembly.GetName().Name + " must have the <Assembly: AssemblyCompany()>  key populated.");
			}
 
			return objNameValueCollection;
		}
 
		//--
		//--
		//-- when this app is loaded via URL, it is possible to pass in "command line arguments" like so:
		//--
		//-- http://localhost/App.Website/App.Loader.exe?a=1&b=2&c=3
		//--
		//-- string[] args = {
		//--     "C:\WINDOWS\Microsoft.NET\Framework\v1.0.3705\IEExec",
		//--     "http://localhost/WebCommandLine/App.Loader.exe?a=1&b=2&c=3",
		//--     "3", "1",  "86474707A3C6F63616C686F6374710000000"};
		//--
		//--

		private static void GetURLCommandLineArgs(string strURL, ref System.Collections.Specialized.NameValueCollection objNameValueCollection)
		{
			MatchCollection objMatchCollection = null;
			Match objMatch = null;
 
			//-- http://localhost/App.Website/App.Loader.exe?a=1&b=2&c=apple
			//-- a = 1
			//-- b = 2
			//-- c = apple
			objMatchCollection = Regex.Matches(strURL, "(?<Key>[^=#&?]+)=(?<Value>[^=#&]*)");
			foreach (Match objMatch_loopVariable in objMatchCollection)
			{
				objMatch = objMatch_loopVariable;
				objNameValueCollection.Add(objMatch.Groups["Key"].ToString(), objMatch.Groups["Value"].ToString());
			}
		}
 
		private static bool IsURL(string strAny)
		{
			return strAny.IndexOf("&") > -1 || strAny.StartsWith("?") || strAny.ToLower().StartsWith("http://");
		}
 
		private static string RemoveArgPrefix(string strArg)
		{
			if (strArg.StartsWith("-") | strArg.StartsWith("/"))
			{
				return strArg.Substring(1);
			}
			else
			{
				return strArg;
			}
		}
 
		//--
		//-- breaks space delimited command line arguments into key value pairs, if they exist
		//--
		//-- App.Loader.exe -remoting=0 /sample=yes c=true
		//-- remoting = 0
		//-- sample   = yes
		//-- c        = true
		//--
		private static bool GetKeyValueCommandLineArg(string strArg, ref System.Collections.Specialized.NameValueCollection objNameValueCollection)
		{
			MatchCollection objMatchCollection = null;
			Match objMatch = null;
 
			objMatchCollection = Regex.Matches(strArg, "(?<Key>^[^=]+)=(?<Value>[^= ]*$)");
			if (objMatchCollection.Count == 0)
			{
				return false;
			}
			else
			{
				foreach (Match objMatch_loopVariable in objMatchCollection)
				{
					objMatch = objMatch_loopVariable;
					objNameValueCollection.Add(RemoveArgPrefix(objMatch.Groups["Key"].ToString()), objMatch.Groups["Value"].ToString());
				}
				return true;
			}
		}
 
		//--
		//-- parses command line arguments, handling special case when app was launched via URL
		//-- note that the default .GetCommandLineArgs is SPACE DELIMITED !
		//--
		private static NameValueCollection GetCommandLineArgs()
		{
			string[] strArgs = Environment.GetCommandLineArgs();
			System.Collections.Specialized.NameValueCollection objNameValueCollection = new System.Collections.Specialized.NameValueCollection();
 
			if (strArgs.Length > 0)
			{
				//--
				//-- handles typical case where app was launched via local .EXE
				//--
				string strArg = null;
				int intArg = 0;
				foreach (string strArg_loopVariable in strArgs)
				{
					strArg = strArg_loopVariable;
					if (IsURL(strArg))
					{
						GetURLCommandLineArgs(strArg, ref objNameValueCollection);
					}
					else
					{
						if (!GetKeyValueCommandLineArg(strArg, ref objNameValueCollection))
						{
							objNameValueCollection.Add("arg" + intArg, RemoveArgPrefix(strArg));
							intArg += 1;
						}
					}
				}
			}
 
			return objNameValueCollection;
		}
 
		//--
		//-- exception-safe file attrib retrieval; we don't care if this fails
		//--
		private static DateTime AssemblyFileTime(System.Reflection.Assembly objAssembly)
		{
			try
			{
				return System.IO.File.GetLastWriteTime(objAssembly.Location);
			}
			catch (Exception)
			{
				return DateTime.MaxValue;
			}
		}
 
		//--
		//-- returns build datetime of assembly
		//-- assumes default assembly value in AssemblyInfo:
		//-- <Assembly: AssemblyVersion("1.0.*")>
		//--
		//-- filesystem create time is used, if revision and build were overridden by user
		//--
		private static DateTime AssemblyBuildDate(System.Reflection.Assembly objAssembly, bool blnForceFileDate = false)
		{
			System.Version objVersion = objAssembly.GetName().Version;
			DateTime dtBuild = default(DateTime);
 
			if (blnForceFileDate)
			{
				dtBuild = AssemblyFileTime(objAssembly);
			}
			else
			{
				//dtBuild = ((DateTime)"01/01/2000").AddDays(objVersion.Build).AddSeconds(objVersion.Revision * 2);
				dtBuild = Convert.ToDateTime("01/01/2000").AddDays((double)objVersion.Build).AddSeconds((double)(objVersion.Revision * 2));
				if (TimeZone.IsDaylightSavingTime(dtBuild, TimeZone.CurrentTimeZone.GetDaylightChanges(dtBuild.Year)))
				{
					dtBuild = dtBuild.AddHours(1);
				}
				if (dtBuild > DateTime.Now | objVersion.Build < 730 | objVersion.Revision == 0)
				{
					dtBuild = AssemblyFileTime(objAssembly);
				}
			}
 
			return dtBuild;
		}
 
		//-- Returns the specified application value as a boolean
		//-- True values: 1, True, true
		//-- False values: anything else
		public static bool GetBoolean(string key)
		{
            string strTemp = ConfigurationManager.AppSettings.Get(key); // ConfigurationSettings.AppSettings.Get(key);
			if (strTemp == null)
			{
				return false;
			}
			else
			{
				switch (strTemp.ToLower())
				{
					case "1":
					case "true":
						return true;
					default:
						return false;
				}
			}
		}
 
		//-- Returns the specified String value from the application .config file
		public static string GetString(string key)
		{
			string strTemp = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings.Get(key));
			if (strTemp == null)
			{
				return "";
			}
			else
			{
				return strTemp;
			}
		}
 
		//--
		//-- Returns the specified Integer value from the application .config file
		//--
		public static int GetInteger(string key)
		{
            int? intTemp = Convert.ToInt32(ConfigurationManager.AppSettings.Get(key));
			if (intTemp == null)
			{
				return 0;
			}
			else
			{
				return (int) intTemp;
			}
		}
	}
}
  
 /*****************************************************************************************/
/* ***************************************************************************************
 * HandledExceptionManager.cs
 * ***************************************************************************************/
/*****************************************************************************************/
//'--
//'--
//'-- Jeff Atwood
//'-- http://www.codinghorror.com
//'--

//using System;
//using System.Diagnostics;
//using System.Drawing;
//using System.Threading;
//using System.Windows.Forms;
 
//'--
//'-- Generic HANDLED error handling class
//'--
//'-- It's like MessageBox, but specific to handled exceptions, and supports email notifications

namespace ExceptionHandling
{
	public class HandledExceptionManager
	{
		private static bool _blnHaveException = false;
		private static bool _blnEmailError = true;
		private static string _strEmailBody;
		private static string _strExceptionType;
 
		private const string _strDefaultMore = "No further information is available. If the problem persists, contact (contact).";
		public static bool EmailError
		{
			get { return _blnEmailError; }
			set { _blnEmailError = value; }
		}
 
		public enum UserErrorDefaultButton
		{
			Default = 0,
			Button1 = 1,
			Button2 = 2,
			Button3 = 3
		}
 
		//--
		//-- replace generic constants in strings with specific values
		//--
		private static string ReplaceStringVals(string strOutput)
		{
			string strTemp = null;
			if (strOutput == null)
			{
				strTemp = "";
			}
			else
			{
				strTemp = strOutput;
			}
			strTemp = strTemp.Replace("(app)", AppSettings.AppProduct);
			strTemp = strTemp.Replace("(contact)", AppSettings.GetString("UnhandledExceptionManager/ContactInfo"));
			return strTemp;
		}
 
		//--
		//-- make sure "More" text is populated with something useful
		//--
		private static string GetDefaultMore(string strMoreDetails)
		{
			if (string.IsNullOrEmpty(strMoreDetails))
			{
				System.Text.StringBuilder objStringBuilder = new System.Text.StringBuilder();
				var _with1 = objStringBuilder;
				_with1.Append(_strDefaultMore);
				_with1.Append(Environment.NewLine);
				_with1.Append(Environment.NewLine);
				_with1.Append("Basic technical information follows: " + Environment.NewLine);
				_with1.Append("---" + Environment.NewLine);
				_with1.Append(UnhandledExceptionManager.SysInfoToString(true));
				return objStringBuilder.ToString();
			}
			else
			{
				return strMoreDetails;
			}
		}
 
		//--
		//-- converts exception to a formatted "more" string
		//--
		private static string ExceptionToMore(System.Exception objException)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			var _with2 = sb;
			if (_blnEmailError)
			{
				_with2.Append("Information about this problem was automatically mailed to ");
				_with2.Append(AppSettings.GetString("UnhandledExceptionManager/EmailTo"));
				_with2.Append(Environment.NewLine + Environment.NewLine);
			}
			_with2.Append("Detailed technical information follows: " + Environment.NewLine);
			_with2.Append("---" + Environment.NewLine);
			string x = UnhandledExceptionManager.ExceptionToString(objException);
			_with2.Append(x);
			return sb.ToString();
		}
 
		//--
		//-- perform our string replacements for (app) and (contact), etc etc
		//-- also make sure More has default values if it is blank.
		//--

		private static void ProcessStrings(ref string strWhatHappened, ref string strHowUserAffected, ref string strWhatUserCanDo, ref string strMoreDetails)
		{
			strWhatHappened = ReplaceStringVals(strWhatHappened);
			strHowUserAffected = ReplaceStringVals(strHowUserAffected);
			strWhatUserCanDo = ReplaceStringVals(strWhatUserCanDo);
			strMoreDetails = ReplaceStringVals(GetDefaultMore(strMoreDetails));
		}
 
		//--
		//-- simplest possible error dialog
		//--
		public static DialogResult ShowDialog(string strWhatHappened, string strHowUserAffected, string strWhatUserCanDo)
		{
			_blnHaveException = false;
			return ShowDialogInternal(strWhatHappened, strHowUserAffected, strWhatUserCanDo, "", MessageBoxButtons.OK, MessageBoxIcon.Warning, UserErrorDefaultButton.Default);
		}
 
		//--
		//-- advanced error dialog with Exception object
		//--
		public static DialogResult ShowDialog(string strWhatHappened, string strHowUserAffected, string strWhatUserCanDo, System.Exception objException, MessageBoxButtons Buttons = MessageBoxButtons.OK, MessageBoxIcon Icon = MessageBoxIcon.Warning, UserErrorDefaultButton DefaultButton = UserErrorDefaultButton.Default)
		{
			_blnHaveException = true;
			_strExceptionType = objException.GetType().FullName;
			return ShowDialogInternal(strWhatHappened, strHowUserAffected, strWhatUserCanDo, ExceptionToMore(objException), Buttons, Icon, DefaultButton);
		}
 
		//--
		//-- advanced error dialog with More string
		//-- leave "more" string blank to get the default
		//--
		public static DialogResult ShowDialog(string strWhatHappened, string strHowUserAffected, string strWhatUserCanDo, string strMoreDetails, MessageBoxButtons Buttons = MessageBoxButtons.OK, MessageBoxIcon Icon = MessageBoxIcon.Warning, UserErrorDefaultButton DefaultButton = UserErrorDefaultButton.Default)
		{
			_blnHaveException = false;
			return ShowDialogInternal(strWhatHappened, strHowUserAffected, strWhatUserCanDo, strMoreDetails, Buttons, Icon, DefaultButton);
		}
 
		//--
		//-- internal method to show error dialog
		//--
		private static DialogResult ShowDialogInternal(string strWhatHappened, string strHowUserAffected, string strWhatUserCanDo, string strMoreDetails, MessageBoxButtons Buttons, MessageBoxIcon Icon, UserErrorDefaultButton DefaultButton)
		{
			//-- set default values, etc
			ProcessStrings(ref strWhatHappened, ref strHowUserAffected, ref strWhatUserCanDo, ref strMoreDetails);
 
			ExceptionDialog objForm = new ExceptionDialog();
			var _with3 = objForm;
			_with3.Text = ReplaceStringVals(objForm.Text);
			_with3.ErrorBox.Text = strWhatHappened;
			_with3.ScopeBox.Text = strHowUserAffected;
			_with3.ActionBox.Text = strWhatUserCanDo;
			_with3.txtMore.Text = strMoreDetails;
 
			//-- determine what button text, visibility, and defaults are
			var _with4 = objForm;
			switch (Buttons)
			{
				case MessageBoxButtons.AbortRetryIgnore:
					_with4.btn1.Text = "&Abort";
					_with4.btn2.Text = "&Retry";
					_with4.btn3.Text = "&Ignore";
					_with4.AcceptButton = objForm.btn2;
					_with4.CancelButton = objForm.btn3;
					break;
				case MessageBoxButtons.OK:
					_with4.btn3.Text = "OK";
					_with4.btn2.Visible = false;
					_with4.btn1.Visible = false;
					_with4.AcceptButton = objForm.btn3;
					break;
				case MessageBoxButtons.OKCancel:
					_with4.btn3.Text = "Cancel";
					_with4.btn2.Text = "OK";
					_with4.btn1.Visible = false;
					_with4.AcceptButton = objForm.btn2;
					_with4.CancelButton = objForm.btn3;
					break;
				case MessageBoxButtons.RetryCancel:
					_with4.btn3.Text = "Cancel";
					_with4.btn2.Text = "&Retry";
					_with4.btn1.Visible = false;
					_with4.AcceptButton = objForm.btn2;
					_with4.CancelButton = objForm.btn3;
					break;
				case MessageBoxButtons.YesNo:
					_with4.btn3.Text = "&No";
					_with4.btn2.Text = "&Yes";
					_with4.btn1.Visible = false;
					break;
				case MessageBoxButtons.YesNoCancel:
					_with4.btn3.Text = "Cancel";
					_with4.btn2.Text = "&No";
					_with4.btn1.Text = "&Yes";
					_with4.CancelButton = objForm.btn3;
					break;
			}
 
			////-- set the proper dialog icon
			//switch (Icon) {
			//    case MessageBoxIcon.Error:
			//        objForm.PictureBox1.Image = System.Drawing.SystemIcons.Error.ToBitmap();
			//        break;
			//    case MessageBoxIcon.Stop:
			//        objForm.PictureBox1.Image = System.Drawing.SystemIcons.Error.ToBitmap();
			//        break;
			//    case MessageBoxIcon.Exclamation:
			//        objForm.PictureBox1.Image = System.Drawing.SystemIcons.Exclamation.ToBitmap();
			//        break;
			//    case MessageBoxIcon.Information:
			//        objForm.PictureBox1.Image = System.Drawing.SystemIcons.Information.ToBitmap();
			//        break;
			//    case MessageBoxIcon.Question:
			//        objForm.PictureBox1.Image = System.Drawing.SystemIcons.Question.ToBitmap();
			//        break;
			//    default:
			//        objForm.PictureBox1.Image = System.Drawing.SystemIcons.Error.ToBitmap();
			//        break;
			//}

			//'-- set the proper dialog icon
			MessageBoxIcon PictureBox1 = Icon;
			if (PictureBox1 == MessageBoxIcon.Hand)
			{
				objForm.PictureBox1.Image = SystemIcons.Error.ToBitmap();
			}
			else if (PictureBox1 == MessageBoxIcon.Hand)
			{
				objForm.PictureBox1.Image = SystemIcons.Error.ToBitmap();
			}
			else if (PictureBox1 == MessageBoxIcon.Exclamation)
			{
				objForm.PictureBox1.Image = SystemIcons.Exclamation.ToBitmap();
			}
			else if (PictureBox1 == MessageBoxIcon.Asterisk)
			{
				objForm.PictureBox1.Image = SystemIcons.Information.ToBitmap();
			}
			else if (PictureBox1 == MessageBoxIcon.Question)
			{
				objForm.PictureBox1.Image = SystemIcons.Question.ToBitmap();
			}
			else
			{
				objForm.PictureBox1.Image = SystemIcons.Error.ToBitmap();
			}
 
			//-- override the default button
			switch (DefaultButton)
			{
				case UserErrorDefaultButton.Button1:
					objForm.AcceptButton = objForm.btn1;
					objForm.btn1.TabIndex = 0;
					break;
				case UserErrorDefaultButton.Button2:
					objForm.AcceptButton = objForm.btn2;
					objForm.btn2.TabIndex = 0;
					break;
				case UserErrorDefaultButton.Button3:
					objForm.AcceptButton = objForm.btn3;
					objForm.btn3.TabIndex = 0;
					break;
			}
 
			if (_blnEmailError)
			{
				SendNotificationEmail(strWhatHappened, strHowUserAffected, strWhatUserCanDo, strMoreDetails);
			}
 
			//-- show the user our error dialog
			return objForm.ShowDialog();
		}
 
		//--
		//-- this is the code that executes in the spawned thread
		//--
		private static void ThreadHandler()
		{
			SimpleMail.SMTPClient smtp = new SimpleMail.SMTPClient();
			SimpleMail.SMTPMailMessage mail = new SimpleMail.SMTPMailMessage();
			var _with5 = mail;
			_with5.To = AppSettings.GetString("UnhandledExceptionManager/EmailTo");
			if (_blnHaveException)
			{
				_with5.Subject = "Handled Exception notification - " + _strExceptionType;
			}
			else
			{
				_with5.Subject = "HandledExceptionManager notification";
			}
			_with5.Body = _strEmailBody;
			//-- try to send email, but we don't care if it succeeds (for now)
			try
			{
				smtp.SendMail(mail);
			}
			catch (Exception e)
			{
				Debug.WriteLine("** SMTP email failed to send!");
				Debug.WriteLine("** " + e.Message);
			}
		}
 
		//--
		//-- send notification about this error via e-mail
		//--

		private static void SendNotificationEmail(string strWhatHappened, string strHowUserAffected, string strWhatUserCanDo, string strMoreDetails)
		{
			//-- ignore debug exceptions (eg, development testing)?
			if (UnhandledExceptionManager.IgnoreDebugErrors)
			{
				if (AppSettings.DebugMode)
					return;
			}
 
			System.Text.StringBuilder objStringBuilder = new System.Text.StringBuilder();
			var _with6 = objStringBuilder;
			_with6.Append("What happened:");
			_with6.Append(Environment.NewLine);
			_with6.Append(strWhatHappened);
			_with6.Append(Environment.NewLine);
			_with6.Append(Environment.NewLine);
			_with6.Append("How this will affect the user:");
			_with6.Append(Environment.NewLine);
			_with6.Append(strHowUserAffected);
			_with6.Append(Environment.NewLine);
			_with6.Append(Environment.NewLine);
			_with6.Append("What the user can do about it:");
			_with6.Append(Environment.NewLine);
			_with6.Append(strWhatUserCanDo);
			_with6.Append(Environment.NewLine);
			_with6.Append(Environment.NewLine);
			_with6.Append("More information:");
			_with6.Append(Environment.NewLine);
			_with6.Append(strMoreDetails);
			_with6.Append(Environment.NewLine);
			_with6.Append(Environment.NewLine);
			SendEmail(objStringBuilder.ToString());
		}
 
		private static void SendEmail(string strEmailBody)
		{
			_strEmailBody = strEmailBody;
			//-- spawn off the email send attempt as a thread for improved throughput
			Thread objThread = new Thread(new ThreadStart(ThreadHandler));
			objThread.Name = "HandledExceptionEmail";
			objThread.Start();
		}
	}
}
  
 /*****************************************************************************************/
/* ***************************************************************************************
 * UnhandledExceptionManager
 * ***************************************************************************************/
/*****************************************************************************************/
//'--
//'--
//'-- Jeff Atwood
//'-- http://www.codinghorror.com
//'--

//using System;
//using System.Configuration;
//using System.Diagnostics;
//using System.Drawing;
//using System.Drawing.Imaging;
//using System.Reflection;
//using System.Runtime.InteropServices;
//using System.Security.Principal;
//using System.Text.RegularExpressions;
//using System.Threading;
//using System.Windows.Forms;
 
//'--
//'-- Generic UNHANDLED error handling class
//'--
//'-- Intended as a last resort for errors which crash our application, so we can get feedback on what
//'-- caused the error.
//'--
//'-- To use: UnhandledExceptionManager.AddHandler() in the STARTUP of your application
//'--
//'-- more background information on Exceptions at:
//'--   http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dnbda/html/exceptdotnet.asp

namespace ExceptionHandling
{
	public class UnhandledExceptionManager
	{
		private UnhandledExceptionManager()
		{
			// to keep this class from being creatable as an instance.
		}
 
		private static bool _blnLogToEventLog;
		private static bool _blnLogToFile;
		private static bool _blnLogToEmail;
		private static bool _blnLogToScreenshot;
 
		private static bool _blnLogToUI;
		private static bool _blnLogToFileOK;
        //private static bool _blnLogToEmailOK;
		private static bool _blnLogToScreenshotOK;
 
		private static bool _blnLogToEventLogOK;
		private static bool _blnEmailIncludeScreenshot;
		private static System.Drawing.Imaging.ImageFormat _ScreenshotImageFormat = System.Drawing.Imaging.ImageFormat.Png;
		private static string _strScreenshotFullPath;
 
		private static string _strLogFullPath;
		private static bool _blnConsoleApp;
		private static System.Reflection.Assembly _objParentAssembly = null;
		private static string _strException;
 
		private static string _strExceptionType;
		private static bool _blnIgnoreDebugErrors;
 
		private static bool _blnKillAppOnException;
		private const string _strLogName = "UnhandledExceptionLog.txt";
		private const string _strScreenshotName = "UnhandledException";
 
		private const string _strClassName = "UnhandledExceptionManager";
 
		#region "Properties"
 
		public static bool IgnoreDebugErrors
		{
			get { return _blnIgnoreDebugErrors; }
			set { _blnIgnoreDebugErrors = value; }
		}
 
		public static bool DisplayDialog
		{
			get { return _blnLogToUI; }
			set { _blnLogToUI = value; }
		}
 
		public static bool EmailScreenshot
		{
			get { return _blnEmailIncludeScreenshot; }
			set { _blnEmailIncludeScreenshot = value; }
		}
 
		public static bool KillAppOnException
		{
			get { return _blnKillAppOnException; }
			set { _blnKillAppOnException = value; }
		}
 
		public static ImageFormat ScreenshotImageFormat
		{
			get { return _ScreenshotImageFormat; }
			set { _ScreenshotImageFormat = value; }
		}
 
		public static bool LogToFile
		{
			get { return _blnLogToFile; }
			set { _blnLogToFile = value; }
		}
 
		public static bool LogToEventLog
		{
			get { return _blnLogToEventLog; }
			set { _blnLogToEventLog = value; }
		}
 
		public static bool SendEmail
		{
			get { return _blnLogToEmail; }
			set { _blnLogToEmail = value; }
		}
 
		public static bool TakeScreenshot
		{
			get { return _blnLogToScreenshot; }
			set { _blnLogToScreenshot = value; }
		}
 
		[DllImport("gdi32", EntryPoint = "BitBlt", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
 
		#endregion "Properties"
 
		#region "win32api screenshot calls"
 
		//--
		//-- Windows API calls necessary to support screen capture
		//--
		private static extern int BitBlt(int hDestDC, int x, int y, int nWidth, int nHeight, int hSrcDC, int xSrc, int ySrc, int dwRop);
 
		[DllImport("user32", EntryPoint = "GetDC", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
 
		private static extern int GetDC(int hwnd);
 
		[DllImport("user32", EntryPoint = "ReleaseDC", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
 
		private static extern int ReleaseDC(int hwnd, int hdc);
 
		#endregion "win32api screenshot calls"
 
		private static System.Reflection.Assembly ParentAssembly()
		{
			if (_objParentAssembly == null)
			{
				if (System.Reflection.Assembly.GetEntryAssembly() == null)
				{
					_objParentAssembly = System.Reflection.Assembly.GetCallingAssembly();
				}
				else
				{
					_objParentAssembly = System.Reflection.Assembly.GetEntryAssembly();
				}
			}
			return _objParentAssembly;
		}
 
		//--
		//-- load some settings that may optionally be present in our .config file
		//-- if they aren't present, we get the defaults as defined here
		//--
		private static void LoadConfigSettings()
		{
			SendEmail = GetConfigBoolean("SendEmail", true);
			TakeScreenshot = GetConfigBoolean("TakeScreenshot", true);
			EmailScreenshot = GetConfigBoolean("EmailScreenshot", true);
			LogToEventLog = GetConfigBoolean("LogToEventLog", false);
			LogToFile = GetConfigBoolean("LogToFile", true);
			DisplayDialog = GetConfigBoolean("DisplayDialog", true);
			IgnoreDebugErrors = GetConfigBoolean("IgnoreDebug", true);
			KillAppOnException = GetConfigBoolean("KillAppOnException", true);
		}
 
		//--
		//-- This *MUST* be called early in your application to set up global error handling
		//--
		public static void AddHandler(bool blnConsoleApp = false)
		{
			//-- attempt to load optional settings from .config file
			LoadConfigSettings();
 
			//-- we don't need an unhandled exception handler if we are running inside
			//-- the vs.net IDE; it is our "unhandled exception handler" in that case
			if (_blnIgnoreDebugErrors)
			{
				if (Debugger.IsAttached)
					return;
			}
 
			//-- track the parent assembly that set up error handling
			//-- need to call this NOW so we set it appropriately; otherwise
			//-- we may get the wrong assembly at exception time!
			ParentAssembly();
 
			//-- for winforms applications
			Application.ThreadException -= ThreadExceptionHandler;
			Application.ThreadException += ThreadExceptionHandler;
 
			//-- for console applications
			System.AppDomain.CurrentDomain.UnhandledException -= UnhandledExceptionHandler;
			System.AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;
 
			//-- I cannot find a good way to programatically detect a console app, so that must be specified.
			_blnConsoleApp = blnConsoleApp;
		}
 
		//--
		//-- handles Application.ThreadException event
		//--
		private static void ThreadExceptionHandler(System.Object sender, System.Threading.ThreadExceptionEventArgs e)
		{
			GenericExceptionHandler(e.Exception);
		}
 
		//--
		//-- handles AppDomain.CurrentDoamin.UnhandledException event
		//--
		private static void UnhandledExceptionHandler(System.Object sender, UnhandledExceptionEventArgs args)
		{
			Exception objException = (Exception)args.ExceptionObject;
			GenericExceptionHandler(objException);
		}
 
		//--
		//-- exception-safe file attrib retrieval; we don't care if this fails
		//--
		private static DateTime AssemblyFileTime(System.Reflection.Assembly objAssembly)
		{
			try
			{
				return System.IO.File.GetLastWriteTime(objAssembly.Location);
			}
			catch (Exception)
			{
				return DateTime.MaxValue;
			}
		}
 
		//--
		//-- returns build datetime of assembly
		//-- assumes default assembly value in AssemblyInfo:
		//-- <Assembly: AssemblyVersion("1.0.*")>
		//--
		//-- filesystem create time is used, if revision and build were overridden by user
		//--
		private static DateTime AssemblyBuildDate(System.Reflection.Assembly objAssembly, bool blnForceFileDate = false)
		{
			System.Version objVersion = objAssembly.GetName().Version;
			DateTime dtBuild = default(DateTime);
 
			if (blnForceFileDate)
			{
				dtBuild = AssemblyFileTime(objAssembly);
			}
			else
			{
				//dtBuild = ((DateTime)"01/01/2000").AddDays(objVersion.Build).AddSeconds(objVersion.Revision * 2);
				dtBuild = Convert.ToDateTime("01/01/2000").AddDays((double)objVersion.Build).AddSeconds((double)(objVersion.Revision * 2));
				if (TimeZone.IsDaylightSavingTime(DateTime.Now, TimeZone.CurrentTimeZone.GetDaylightChanges(DateTime.Now.Year)))
				{
					dtBuild = dtBuild.AddHours(1);
				}
				if (dtBuild > DateTime.Now | objVersion.Build < 730 | objVersion.Revision == 0)
				{
					dtBuild = AssemblyFileTime(objAssembly);
				}
			}
 
			return dtBuild;
		}
 
		//--
		//-- turns a single stack frame object into an informative string
		//--
		private static string StackFrameToString(StackFrame sf)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			int intParam = 0;
			MemberInfo mi = sf.GetMethod();
 
			var _with1 = sb;
			//-- build method name
			_with1.Append("   ");
			_with1.Append(mi.DeclaringType.Namespace);
			_with1.Append(".");
			_with1.Append(mi.DeclaringType.Name);
			_with1.Append(".");
			_with1.Append(mi.Name);
 
			//-- build method params
			ParameterInfo[] objParameters = sf.GetMethod().GetParameters();
			ParameterInfo objParameter = null;
			_with1.Append("(");
			intParam = 0;
			foreach (ParameterInfo objParameter_loopVariable in objParameters)
			{
				objParameter = objParameter_loopVariable;
				intParam += 1;
				if (intParam > 1)
					_with1.Append(", ");
				_with1.Append(objParameter.Name);
				_with1.Append(" As ");
				_with1.Append(objParameter.ParameterType.Name);
			}
			_with1.Append(")");
			_with1.Append(Environment.NewLine);
 
			//-- if source code is available, append location info
			_with1.Append("       ");
			if (sf.GetFileName() == null || sf.GetFileName().Length == 0)
			{
				_with1.Append(System.IO.Path.GetFileName(ParentAssembly().CodeBase));
				//-- native code offset is always available
				_with1.Append(": N ");
				_with1.Append(string.Format("{0:#00000}", sf.GetNativeOffset()));
			}
			else
			{
				_with1.Append(System.IO.Path.GetFileName(sf.GetFileName()));
				_with1.Append(": line ");
				_with1.Append(string.Format("{0:#0000}", sf.GetFileLineNumber()));
				_with1.Append(", col ");
				_with1.Append(string.Format("{0:#00}", sf.GetFileColumnNumber()));
				//-- if IL is available, append IL location info
				if (sf.GetILOffset() != StackFrame.OFFSET_UNKNOWN)
				{
					_with1.Append(", IL ");
					_with1.Append(string.Format("{0:#0000}", sf.GetILOffset()));
				}
			}
			_with1.Append(Environment.NewLine);
			return sb.ToString();
		}
 
		//--
		//-- enhanced stack trace generator
		//--
		private static string EnhancedStackTrace(StackTrace objStackTrace, string strSkipClassName = "")
		{
			int intFrame = 0;
 
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
 
			sb.Append(Environment.NewLine);
			sb.Append("---- Stack Trace ----");
			sb.Append(Environment.NewLine);
 
			for (intFrame = 0; intFrame <= objStackTrace.FrameCount - 1; intFrame++)
			{
				StackFrame sf = objStackTrace.GetFrame(intFrame);
				MemberInfo mi = sf.GetMethod();
 
				if (!string.IsNullOrEmpty(strSkipClassName) && mi.DeclaringType.Name.IndexOf(strSkipClassName) > -1)
				{
					//-- don't include frames with this name
				}
				else
				{
					sb.Append(StackFrameToString(sf));
				}
			}
			sb.Append(Environment.NewLine);
 
			return sb.ToString();
		}
 
		//--
		//-- enhanced stack trace generator (exception)
		//--
		private static string EnhancedStackTrace(Exception objException)
		{
			StackTrace objStackTrace = new StackTrace(objException, true);
			return EnhancedStackTrace(objStackTrace);
		}
 
		//--
		//-- enhanced stack trace generator (no params)
		//--
		private static string EnhancedStackTrace()
		{
			StackTrace objStackTrace = new StackTrace(true);
			return EnhancedStackTrace(objStackTrace, "ExceptionManager");
		}
 
		//--
		//-- generic exception handler; the various specific handlers all call into this sub
		//--

		private static void GenericExceptionHandler(Exception objException)
		{
			//-- turn the exception into an informative string
			try
			{
				_strException = ExceptionToString(objException);
				_strExceptionType = objException.GetType().FullName;
			}
			catch (Exception ex)
			{
				_strException = "Error '" + ex.Message + "' while generating exception string";
				_strExceptionType = "";
			}
 
			if (!_blnConsoleApp)
			{
				Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
			}
 
			//-- log this error to various locations
			try
			{
				//-- screenshot takes around 1 second
				if (_blnLogToScreenshot)
					ExceptionToScreenshot();
				//-- event logging takes < 100ms
				if (_blnLogToEventLog)
					ExceptionToEventLog();
				//-- textfile logging takes < 50ms
				if (_blnLogToFile)
					ExceptionToFile();
				//-- email takes under 1 second
				if (_blnLogToEmail)
					ExceptionToEmail();
			}
			catch (Exception)
			{
				//-- generic catch because any exceptions inside the UEH
				//-- will cause the code to terminate immediately
			}
 
			if (!_blnConsoleApp)
			{
				Cursor.Current = System.Windows.Forms.Cursors.Default;
			}
			//-- display message to the user
			if (_blnLogToUI)
				ExceptionToUI();
 
			if (_blnKillAppOnException)
			{
				//As far as the email not being send when a user closes the dialog too fast, 
				//I see that you were threading off the email to speed display of the exception 
				//dialog. Well, if the user clicks ok before the SMTP can be sent, the thread 
				//is killed immediately and the email will never make it out. To fix that, I changed 
				//the scope of the objThread to class level and right before the KillApp() call I 
				//do a objThread.Join(new TimeSpan(0, 0, 30)) to wait up to 30 seconds for it's 
				//completion. Now the email is sent reliably. Changing the scope of the objThread 
				//mandated that the class not be static (Shared) anymore, so I changed the relevant 
				//functions and I instantiate an object of the exception handler to AddHandler() with.

				//objThread.Join(new TimeSpan(0, 0, 30));	// to wait 30 seconds for completion
				KillApp();
				Application.Exit();
			}
		}
 
		//--
		//-- This is in a private routine for .NET security reasons
		//-- if this line of code is in a sub, the entire sub is tagged as full trust
		//--
		private static void KillApp()
		{
			System.Diagnostics.Process.GetCurrentProcess().Kill();
		}
 
		//--
		//-- turns exception into something an average user can hopefully
		//-- understand; still very technical
		//--
		private static string FormatExceptionForUser(bool blnConsoleApp)
		{
			System.Text.StringBuilder objStringBuilder = new System.Text.StringBuilder();
			string strBullet = null;
			if (blnConsoleApp)
			{
				strBullet = "-";
			}
			else
			{
				strBullet = "•";
			}
 
			var _with2 = objStringBuilder;
			if (!blnConsoleApp)
			{
				_with2.Append("The development team was automatically notified of this problem. ");
				_with2.Append("If you need immediate assistance, contact (contact).");
			}
			_with2.Append(Environment.NewLine);
			_with2.Append(Environment.NewLine);
			_with2.Append("The following information about the error was automatically captured: ");
			_with2.Append(Environment.NewLine);
			_with2.Append(Environment.NewLine);
			if (_blnLogToScreenshot)
			{
				_with2.Append(" ");
				_with2.Append(strBullet);
				_with2.Append(" ");
				if (_blnLogToScreenshotOK)
				{
					_with2.Append("a screenshot was taken of the desktop at:");
					_with2.Append(Environment.NewLine);
					_with2.Append("   ");
					_with2.Append(_strScreenshotFullPath);
				}
				else
				{
					_with2.Append("a screenshot could NOT be taken of the desktop.");
				}
				_with2.Append(Environment.NewLine);
			}
			if (_blnLogToEventLog)
			{
				_with2.Append(" ");
				_with2.Append(strBullet);
				_with2.Append(" ");
				if (_blnLogToEventLogOK)
				{
					_with2.Append("an event was written to the application log");
				}
				else
				{
					_with2.Append("an event could NOT be written to the application log");
				}
				_with2.Append(Environment.NewLine);
			}
			if (_blnLogToFile)
			{
				_with2.Append(" ");
				_with2.Append(strBullet);
				_with2.Append(" ");
				if (_blnLogToFileOK)
				{
					_with2.Append("details were written to a text log at:");
				}
				else
				{
					_with2.Append("details could NOT be written to the text log at:");
				}
				_with2.Append(Environment.NewLine);
				_with2.Append("   ");
				_with2.Append(_strLogFullPath);
				_with2.Append(Environment.NewLine);
			}
			if (_blnLogToEmail)
			{
				_with2.Append(" ");
				_with2.Append(strBullet);
				_with2.Append(" ");
				_with2.Append("attempted to send an email to: ");
				_with2.Append(Environment.NewLine);
				_with2.Append("   ");
				_with2.Append(GetConfigString("EmailTo"));
				_with2.Append(Environment.NewLine);
			}
			_with2.Append(Environment.NewLine);
			_with2.Append(Environment.NewLine);
			_with2.Append("Detailed error information follows:");
			_with2.Append(Environment.NewLine);
			_with2.Append(Environment.NewLine);
			_with2.Append(_strException);
			return objStringBuilder.ToString();
		}
 
		//--
		//-- display a dialog to the user; otherwise we just terminate with no alert at all!
		//--

		private static void ExceptionToUI()
		{
			const string _strWhatHappened = "There was an unexpected error in (app). This may be due to a programming bug.";
			string _strHowUserAffected = null;
			const string _strWhatUserCanDo = "Restart (app), and try repeating your last action. Try alternative methods of performing the same action.";
 
			if (UnhandledExceptionManager.KillAppOnException)
			{
				_strHowUserAffected = "When you click OK, (app) will close.";
			}
			else
			{
				_strHowUserAffected = "The action you requested was not performed.";
			}
 
			if (!_blnConsoleApp)
			{
				//-- don't send ANOTHER email if we are already doing so!
				HandledExceptionManager.EmailError = !SendEmail;
				//-- pop the dialog
				HandledExceptionManager.ShowDialog(_strWhatHappened, _strHowUserAffected, _strWhatUserCanDo, FormatExceptionForUser(false), MessageBoxButtons.OK, MessageBoxIcon.Stop);
			}
			else
			{
				//-- note that writing to console pauses for ENTER
				//-- otherwise console window just terminates immediately
				ExceptionToConsole();
			}
		}
 
		//--
		//-- for non-web hosted apps, returns:
		//--   "[path]\bin\YourAssemblyName."
		//-- for web hosted apps, returns URL with non-filesystem chars removed:
		//--   "c:\http___domain\path\YourAssemblyName."
		private static string GetApplicationPath()
		{
			if (ParentAssembly().CodeBase.StartsWith("http://"))
			{
				//return "c:\\" + Regex.Replace(ParentAssembly().CodeBase(), "[\\/\\\\\\:\\*\\?\\\"\\<\\>\\|]", "_") + ".";
				return (@"c:\" + Regex.Replace(ParentAssembly().CodeBase, "[\\/\\\\\\:\\*\\?\\\"\\<\\>\\|]", "_") + ".");
			}
			else
			{
				return System.AppDomain.CurrentDomain.BaseDirectory + System.AppDomain.CurrentDomain.FriendlyName + ".";
			}
		}
 
		//--
		//-- take a desktop screenshot of our exception
		//-- note that this fires BEFORE the user clicks on the OK dismissing the crash dialog
		//-- so the crash dialog itself will not be displayed
		//--
		private static void ExceptionToScreenshot()
		{
			//-- note that screenshotname does NOT include the file type extension
			try
			{
				TakeScreenshotPrivate(GetApplicationPath() + _strScreenshotName);
				_blnLogToScreenshotOK = true;
			}
			catch (Exception)
			{
				_blnLogToScreenshotOK = false;
			}
		}
 
		//--
		//-- write an exception to the Windows NT event log
		//--
		private static void ExceptionToEventLog()
		{
			try
			{
				System.Diagnostics.EventLog.WriteEntry(System.AppDomain.CurrentDomain.FriendlyName, Environment.NewLine + _strException, EventLogEntryType.Error);
				_blnLogToEventLogOK = true;
			}
			catch (Exception)
			{
				_blnLogToEventLogOK = false;
			}
		}
 
		//--
		//-- write an exception to the console
		//--
		private static void ExceptionToConsole()
		{
			Console.WriteLine("This application encountered an unexpected problem.");
			Console.WriteLine(FormatExceptionForUser(true));
			Console.WriteLine("The application must now terminate. Press ENTER to continue...");
			Console.ReadLine();
		}
 
		//--
		//-- write an exception to a text file
		//--
		private static void ExceptionToFile()
		{
			_strLogFullPath = GetApplicationPath() + _strLogName;
			try
			{
				System.IO.StreamWriter objStreamWriter = new System.IO.StreamWriter(_strLogFullPath, true);
				objStreamWriter.Write(_strException);
				objStreamWriter.WriteLine();
				objStreamWriter.Close();
				_blnLogToFileOK = true;
			}
			catch (Exception)
			{
				_blnLogToFileOK = false;
			}
		}
 
		//--
		//-- this is the code that executes in the spawned thread
		//--
		private static void ThreadHandler()
		{
			SimpleMail.SMTPClient objMail = new SimpleMail.SMTPClient();
			SimpleMail.SMTPMailMessage objMailMessage = new SimpleMail.SMTPMailMessage();
			var _with3 = objMailMessage;
			_with3.To = GetConfigString("EmailTo", "");
			_with3.Subject = "Unhandled Exception notification - " + _strExceptionType;
			_with3.Body = _strException;
			if (_blnLogToScreenshot & _blnEmailIncludeScreenshot)
			{
				_with3.AttachmentPath = _strScreenshotFullPath;
			}
			try
			{
				// call SendMail method in SimpleMail class
				objMail.SendMail(objMailMessage);
                //_blnLogToEmailOK = true;
			}
			catch (Exception)
			{
                //_blnLogToEmailOK = false;
				//-- don't do anything; sometimes SMTP isn't available, which generates an exception
				//-- and an exception in the unhandled exception manager.. is bad news.
				//--MsgBox("exception email failed to send:" + Environment.Newline + Environment.Newline + e.Message)
			}
		}
 
		//--
		//-- send an exception via email
		//--
		private static void ExceptionToEmail()
		{
			//-- spawn off the email send attempt as a thread for improved throughput
			Thread objThread = new Thread(new ThreadStart(ThreadHandler));
			objThread.Name = "SendExceptionEmail";
			objThread.Start();
		}
 
		//--
		//-- exception-safe WindowsIdentity.GetCurrent retrieval returns "domain\username"
		//-- per MS, this sometimes randomly fails with "Access Denied" particularly on NT4
		//--
		private static string CurrentWindowsIdentity()
		{
			try
			{
				//return System.Security.Principal.WindowsIdentity.GetCurrent().Name();
				return WindowsIdentity.GetCurrent().Name;
			}
			catch (Exception)
			{
				return "";
			}
		}
 
		//--
		//-- exception-safe "domain\username" retrieval from Environment
		//--
		private static string CurrentEnvironmentIdentity()
		{
			try
			{
				return System.Environment.UserDomainName + "\\" + System.Environment.UserName;
			}
			catch (Exception)
			{
				return "";
			}
		}
 
		//--
		//-- retrieve identity with fallback on error to safer method
		//--
		private static string UserIdentity()
		{
			string strTemp = null;
			strTemp = CurrentWindowsIdentity();
			if (string.IsNullOrEmpty(strTemp))
			{
				strTemp = CurrentEnvironmentIdentity();
			}
			return strTemp;
		}
 
		//--
		//-- gather some system information that is helpful to diagnosing
		//-- exception
		//--
		static internal string SysInfoToString(bool blnIncludeStackTrace = false)
		{
			System.Text.StringBuilder objStringBuilder = new System.Text.StringBuilder();
 
			var _with4 = objStringBuilder;
 
			_with4.Append("Date and Time:         ");
			_with4.Append(DateTime.Now);
			_with4.Append(Environment.NewLine);
 
			_with4.Append("Machine Name:          ");
			try
			{
				_with4.Append(Environment.MachineName);
			}
			catch (Exception e)
			{
				_with4.Append(e.Message);
			}
			_with4.Append(Environment.NewLine);
 
			_with4.Append("IP Address:            ");
			_with4.Append(GetCurrentIP());
			_with4.Append(Environment.NewLine);
 
			_with4.Append("Current User:          ");
			_with4.Append(UserIdentity());
			_with4.Append(Environment.NewLine);
			_with4.Append(Environment.NewLine);
 
			_with4.Append("Application Domain:    ");
			try
			{
				//_with4.Append(System.AppDomain.CurrentDomain.FriendlyName());
				_with4.Append(System.AppDomain.CurrentDomain.FriendlyName);
			}
			catch (Exception e)
			{
				_with4.Append(e.Message);
			}
 
			_with4.Append(Environment.NewLine);
			_with4.Append("Assembly Codebase:     ");
			try
			{
				//_with4.Append(ParentAssembly().CodeBase());
				_with4.Append(ParentAssembly().CodeBase);
			}
			catch (Exception e)
			{
				_with4.Append(e.Message);
			}
			_with4.Append(Environment.NewLine);
 
			_with4.Append("Assembly Full Name:    ");
			try
			{
				_with4.Append(ParentAssembly().FullName);
			}
			catch (Exception e)
			{
				_with4.Append(e.Message);
			}
			_with4.Append(Environment.NewLine);
 
			_with4.Append("Assembly Version:      ");
			try
			{
				//_with4.Append(ParentAssembly().GetName().Version().ToString);
				_with4.Append(ParentAssembly().GetName().Version.ToString());
			}
			catch (Exception e)
			{
				_with4.Append(e.Message);
			}
			_with4.Append(Environment.NewLine);
 
			_with4.Append("Assembly Build Date:   ");
			try
			{
				_with4.Append(AssemblyBuildDate(ParentAssembly()).ToString());
			}
			catch (Exception e)
			{
				_with4.Append(e.Message);
			}
			_with4.Append(Environment.NewLine);
			_with4.Append(Environment.NewLine);
 
			if (blnIncludeStackTrace)
			{
				_with4.Append(EnhancedStackTrace());
			}
 
			return objStringBuilder.ToString();
		}
 
		//--
		//-- translate exception object to string, with additional system info
		//--
		static internal string ExceptionToString(Exception objException)
		{
			System.Text.StringBuilder objStringBuilder = new System.Text.StringBuilder();
 
			if ((objException.InnerException != null))
			{
				//-- sometimes the original exception is wrapped in a more relevant outer exception
				//-- the detail exception is the "inner" exception
				//-- see http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dnbda/html/exceptdotnet.asp
				var _with5 = objStringBuilder;
				_with5.Append("(Inner Exception)");
				_with5.Append(Environment.NewLine);
				_with5.Append(ExceptionToString(objException.InnerException));
				_with5.Append(Environment.NewLine);
				_with5.Append("(Outer Exception)");
				_with5.Append(Environment.NewLine);
			}
			var _with6 = objStringBuilder;
			//-- get general system and app information
			_with6.Append(SysInfoToString());
 
			//-- get exception-specific information
			_with6.Append("Exception Source:      ");
			try
			{
				_with6.Append(objException.Source);
			}
			catch (Exception e)
			{
				_with6.Append(e.Message);
			}
			_with6.Append(Environment.NewLine);
 
			_with6.Append("Exception Type:        ");
			try
			{
				_with6.Append(objException.GetType().FullName);
			}
			catch (Exception e)
			{
				_with6.Append(e.Message);
			}
			_with6.Append(Environment.NewLine);
 
			_with6.Append("Exception Message:     ");
			try
			{
				_with6.Append(objException.Message);
			}
			catch (Exception e)
			{
				_with6.Append(e.Message);
			}
			_with6.Append(Environment.NewLine);
 
			_with6.Append("Exception Target Site: ");
			try
			{
				_with6.Append(objException.TargetSite.Name);
			}
			catch (Exception e)
			{
				_with6.Append(e.Message);
			}
			_with6.Append(Environment.NewLine);
 
			try
			{
				string x = EnhancedStackTrace(objException);
				_with6.Append(x);
			}
			catch (Exception e)
			{
				_with6.Append(e.Message);
			}
			_with6.Append(Environment.NewLine);
 
			return objStringBuilder.ToString();
		}
 
		//--
		//-- returns ImageCodecInfo for the specified MIME type
		//--
		private static ImageCodecInfo GetEncoderInfo(string strMimeType)
		{
			int j = 0;
			System.Drawing.Imaging.ImageCodecInfo[] objImageCodecInfo = null;
			objImageCodecInfo = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders();
 
			j = 0;
			while (j < objImageCodecInfo.Length)
			{
				if (objImageCodecInfo[j].MimeType == strMimeType)
				{
					return objImageCodecInfo[j];
				}
				j += 1;
			}
 
			return null;
		}
 
		//--
		//-- save bitmap object to JPEG of specified quality level
		//--
		private static void BitmapToJPEG(Bitmap objBitmap, string strFilename, long lngCompression = 75)
		{
			System.Drawing.Imaging.EncoderParameters objEncoderParameters = new System.Drawing.Imaging.EncoderParameters(1);
			System.Drawing.Imaging.ImageCodecInfo objImageCodecInfo = GetEncoderInfo("image/jpeg");
 
			objEncoderParameters.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, lngCompression);
			objBitmap.Save(strFilename, objImageCodecInfo, objEncoderParameters);
		}
 
		//--
		//-- takes a screenshot of the desktop and saves to filename and format specified
		//--
		private static void TakeScreenshotPrivate(string strFilename)
		{
			Rectangle objRectangle = Screen.PrimaryScreen.Bounds;
			Bitmap objBitmap = new Bitmap(objRectangle.Right, objRectangle.Bottom);
			Graphics objGraphics = null;
			IntPtr hdcDest = default(IntPtr);
			int hdcSrc = 0;
			const int SRCCOPY = 0xcc0020;
			string strFormatExtension = null;
 
			//objGraphics = objGraphics.FromImage(objBitmap);
			objGraphics = Graphics.FromImage(objBitmap);
 
			//-- get a device context to the windows desktop and our destination  bitmaps
			hdcSrc = GetDC(0);
			hdcDest = objGraphics.GetHdc();
			//-- copy what is on the desktop to the bitmap
			BitBlt(hdcDest.ToInt32(), 0, 0, objRectangle.Right, objRectangle.Bottom, hdcSrc, 0, 0, SRCCOPY);
			//-- release device contexts
			objGraphics.ReleaseHdc(hdcDest);
			ReleaseDC(0, hdcSrc);
 
			strFormatExtension = _ScreenshotImageFormat.ToString().ToLower();
			if (System.IO.Path.GetExtension(strFilename) != "." + strFormatExtension)
			{
				strFilename += "." + strFormatExtension;
			}
			switch (strFormatExtension)
			{
				case "jpeg":
					BitmapToJPEG(objBitmap, strFilename, 80);
					break;
				default:
					objBitmap.Save(strFilename, _ScreenshotImageFormat);
					break;
			}
            objBitmap.Dispose(); // NGE01162014 

			//-- save the complete path/filename of the screenshot for possible later use
			_strScreenshotFullPath = strFilename;
		}
 

		// The code below (TakeScreenShot and ScreenShot) is used for taking screenshots of dual monitors
		//private void TakeScreenShot(Control windowToShoot)
		//{
		//    string fileName = "c:\\temp\\ScreenShot";
		//    Screen[] screensToProcess;
 
		//    if (windowToShoot == null)
		//        screensToProcess = Screen.AllScreens;
		//    else
		//        screensToProcess = new Screen[] { Screen.FromControl(windowToShoot) };
 
		//    for (int i = 0; i < screensToProcess.Length; i++)
		//    {
		//        string thisFileName = string.Format("{0}_Screen{1}.png", fileName, (i + 1));
		//        ScreenShot(screensToProcess[i], thisFileName);
		//    }
		//}
 
		//private void ScreenShot(Screen screen, string fileName)
		//{
		//    Size imageSize = new Size(screen.WorkingArea.Width, screen.WorkingArea.Height);
 
		//    Bitmap bitmap = new Bitmap(imageSize.Width, imageSize.Height, PixelFormat.Format24bppRgb);
 
		//    Graphics g = Graphics.FromImage(bitmap);
		//    g.CopyFromScreen(screen.WorkingArea.X, screen.WorkingArea.Y,
		//    0, 0,
		//    imageSize);
 
		//    bitmap.Save(fileName, ImageFormat.Png);
		//}
 

		//--
		//-- get IP address of this machine
		//-- not an ideal method for a number of reasons (guess why!)
		//-- but the alternatives are very ugly
		//--
		private static string GetCurrentIP()
		{
			try
			{
                string strIP = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName().ToString()).ToString(); //  System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList[0].ToString();
				return strIP;
			}
			catch (Exception)
			{
				return "127.0.0.1";
			}
		}
 
		const string _strKeyNotPresent = "The key <{0}> is not present in the <appSettings> section of .config file";
 
		const string _strKeyError = "Error {0} retrieving key <{1}> from <appSettings> section of .config file";
 
		//--
		//-- Returns the specified String value from the application .config file,
		//-- with many fail-safe checks (exceptions, key not present, etc)
		//--
		//-- this is important in an *unhandled exception handler*, because any unhandled exceptions will simply exit!
		//--
		private static string GetConfigString(string strKey, string strDefault = null)
		{
			try
			{
				string strTemp = Convert.ToString(ConfigurationManager.AppSettings.Get(_strClassName + "/" + strKey));
				if (strTemp == null)
				{
					if (strDefault == null)
					{
						return string.Format(_strKeyNotPresent, _strClassName + "/" + strKey);
					}
					else
					{
						return strDefault;
					}
				}
				else
				{
					return strTemp;
				}
			}
			catch (Exception ex)
			{
				if (strDefault == null)
				{
					return string.Format(_strKeyError, ex.Message, _strClassName + "/" + strKey);
				}
				else
				{
					return strDefault;
				}
			}
		}
 
		//--
		//-- Returns the specified boolean value from the application .config file,
		//-- with many fail-safe checks (exceptions, key not present, etc)
		//--
		//-- this is important in an *unhandled exception handler*, because any unhandled exceptions will simply exit!
		//--
		//private static bool GetConfigBoolean(string strKey, bool blnDefault = null)
		private static bool GetConfigBoolean(string strKey, [Optional, DefaultParameterValue(false)] bool blnDefault)
		{
			string strTemp = null;
			try
			{
				strTemp = ConfigurationManager.AppSettings.Get(_strClassName + "/" + strKey);
			}
			catch (Exception)
			{
                //if (blnDefault == null)
                //{
                //    return false;
                //}
                //else
				{
					return blnDefault;
				}
			}
 
			if (strTemp == null)
			{
                //if (blnDefault == null)
                //{
                //    return false;
                //}
                //else
				{
					return blnDefault;
				}
			}
			else
			{
				switch (strTemp.ToLower())
				{
					case "1":
					case "true":
						return true;
					default:
						return false;
				}
			}
		}
	}
}
  
 /*****************************************************************************************/
/* ***************************************************************************************
 * SimpleMail.cs
 * ***************************************************************************************/
/*****************************************************************************************/
//'--
//'--
//'-- Jeff Atwood
//'-- http://www.codinghorror.com
//'--

//using System;
//using System.Diagnostics;
//using System.Net;
//using System.Net.Sockets;
//using System.Text;
//using System.Threading;
 
//'--
//'-- a simple class for trivial SMTP mail support
//'--
//'-- basic features:
//'--
//'--   ** trivial SMTP implementation
//'--   ** HTML body
//'--   ** plain text body
//'--   ** one file attachment
//'--   ** basic retry mechanism

namespace ExceptionHandling
{
	public class SimpleMail
	{
		public class SMTPMailMessage
		{
			public string From;
			public string To;
			public string Subject;
			public string Body;
			public string BodyHTML;
			public string AttachmentPath;
		}
 
		public class SMTPClient
		{
			private const int _intBufferSize = 1024;
			private const int _intResponseTimeExpected = 10;
			private const int _intResponseTimeMax = 750;
			private const string _strAddressSeperator = ";";
			private const int _intMaxRetries = 5;
			private const bool _blnDebugMode = true;
 
			private const bool _blnPlainTextOnly = false;
			private string _strDefaultDomain = "netgain.internal";
			private string _strServer = "blue.netgain.internal";
 
			private int _intPort = 25;
			private string _strUserName = "";
 
			private string _strUserPassword = "";
			private int _intRetries = 1;
 
			private string _strLastResponse;
 
			public string AuthUser
			{
				get { return _strUserName; }
				set { _strUserName = value; }
			}
 
			public string AuthPassword
			{
				get { return _strUserPassword; }
				set { _strUserPassword = value; }
			}
 
			public int Port
			{
				get { return _intPort; }
				set { _intPort = value; }
			}
 
			public string Server
			{
				get { return _strServer; }
				set { _strServer = value; }
			}
 
			public string DefaultDomain
			{
				get { return _strDefaultDomain; }
				set { _strDefaultDomain = value; }
			}
 
			//--
			//-- send data over the current network connection
			//--
			private void SendData(TcpClient tcp, string strData)
			{
				NetworkStream objNetworkStream = tcp.GetStream();
				byte[] bytWriteBuffer = new byte[strData.Length + 1];
				System.Text.UTF8Encoding en = new System.Text.UTF8Encoding();
 
				bytWriteBuffer = en.GetBytes(strData);
				objNetworkStream.Write(bytWriteBuffer, 0, bytWriteBuffer.Length);
			}
 
			//--
			//-- get data from the current network connection
			//--
			private string GetData(TcpClient tcp)
			{
				System.Net.Sockets.NetworkStream objNetworkStream = tcp.GetStream();
 
				if (objNetworkStream.DataAvailable)
				{
					byte[] bytReadBuffer = null;
					int intStreamSize = 0;
					bytReadBuffer = new byte[_intBufferSize + 1];
					intStreamSize = objNetworkStream.Read(bytReadBuffer, 0, bytReadBuffer.Length);
					System.Text.UTF8Encoding en = new System.Text.UTF8Encoding();
					return en.GetString(bytReadBuffer);
				}
				else
				{
					return "";
				}
			}
 
			//--
			//-- issue a required SMTP command
			//--

			private void Command(TcpClient tcp, string strCommand, string strExpectedResponse = "250")
			{
				if (!CommandInternal(tcp, strCommand, strExpectedResponse))
				{
					tcp.Close();
					throw new Exception("SMTP server at " + _strServer.ToString() + ":" + _intPort.ToString() + " was provided command '" + strCommand + "', but did not return the expected response '" + strExpectedResponse + "':" + Environment.NewLine + _strLastResponse);
				}
			}
 
			//--
			//-- issue a SMTP command
			//--
			private bool CommandInternal(TcpClient tcp, string strCommand, string strExpectedResponse = "250")
			{
				int intResponseTime = 0;
 
				//-- send the command over the socket with a trailing cr/lf
				if (strCommand.Length > 0)
				{
					SendData(tcp, strCommand + Environment.NewLine);
				}
 
				//-- wait until we get a response, or time out
				_strLastResponse = "";
				intResponseTime = 0;
				while ((string.IsNullOrEmpty(_strLastResponse)) & (intResponseTime <= _intResponseTimeMax))
				{
					intResponseTime += _intResponseTimeExpected;
					_strLastResponse = GetData(tcp);
					//Thread.CurrentThread.Sleep(_intResponseTimeExpected);
					Thread.Sleep(_intResponseTimeExpected);
				}
 
				//-- this is helpful for debugging SMTP problems
				if (_blnDebugMode)
				{
					Debug.WriteLine("SMTP >> " + strCommand + " (after " + intResponseTime.ToString() + "ms)");
					Debug.WriteLine("SMTP << " + _strLastResponse);
				}
 
				//-- if we have a response, check the first 10 characters for the expected response code
				if (string.IsNullOrEmpty(_strLastResponse))
				{
					if (_blnDebugMode)
					{
						Debug.WriteLine("** EXPECTED RESPONSE " + strExpectedResponse + " NOT RETURNED **");
					}
					return false;
				}
				else
				{
					return (_strLastResponse.IndexOf(strExpectedResponse, 0, 10) != -1);
				}
			}
 
			//--
			//-- send mail with integrated retry mechanism
			//--
			public bool SendMail(SMTPMailMessage mail)
			{
				int intRetryInterval = 333;
				try
				{
					SendMailInternal(mail);
				}
				catch (Exception)
				{
					_intRetries += 1;
					if (_intRetries <= _intMaxRetries)
					{
						//Thread.CurrentThread.Sleep(intRetryInterval);
						Thread.Sleep(intRetryInterval);
						SendMail(mail);
					}
					else
					{
						throw;
					}
				}
				//Console.WriteLine("sent after " & _intRetries.ToString)
				_intRetries = 1;
				return true;
			}
 
			//--
			//-- send an email via trivial SMTP
			//--
			private void SendMailInternal(SMTPMailMessage mail)
			{
				IPHostEntry iphost = null;
				TcpClient tcp = new TcpClient();
 
				//-- resolve server text name to an IP address
				try
				{
                    iphost = Dns.GetHostEntry(_strServer); // GetHostByName(_strServer);
				}
				catch (Exception e)
				{
					throw new Exception("Unable to resolve server name " + _strServer, e);
				}
 
				//-- attempt to connect to the server by IP address and port number
				try
				{
					tcp.Connect(iphost.AddressList[0], _intPort);
				}
				catch (Exception e)
				{
					throw new Exception("Unable to connect to SMTP server at " + _strServer.ToString() + ":" + _intPort.ToString(), e);
				}
 
				//-- make sure we get the SMTP welcome message
				//Interaction.Command(tcp, "", "220");
				//Interaction.Command(tcp, "HELO " + Environment.MachineName);
				Command(tcp, "", "220");
				Command(tcp, "HELO " + Environment.MachineName, "250");
 
				//--
				//-- authenticate if we have username and password
				//-- http://www.ietf.org/rfc/rfc2554.txt
				//--
				//if (Strings.Len(_strUserName + _strUserPassword) > 0)
				if ((_strUserName + _strUserPassword).Length > 0)
				{
					//Interaction.Command(tcp, "auth login", "334 VXNlcm5hbWU6");
					////VXNlcm5hbWU6=base64'Username:'
					//Interaction.Command(tcp, ToBase64(_strUserName), "334 UGFzc3dvcmQ6");
					////UGFzc3dvcmQ6=base64'Password:'
					//Interaction.Command(tcp, ToBase64(_strUserPassword), "235");
					//Command(tcp, "auth login", "334 VXNlcm5hbWU6");
					//Command(tcp, ToBase64(_strUserName), "334 UGFzc3dvcmQ6");
					Command(tcp, "auth login", "334 VXN");
					Command(tcp, ToBase64(_strUserName), "334 UGF");
					Command(tcp, ToBase64(_strUserPassword), "235");
				}
 
				if (string.IsNullOrEmpty(mail.From))
				{
					mail.From = System.AppDomain.CurrentDomain.FriendlyName.ToLower() + "@" + Environment.MachineName.ToLower() + "." + _strDefaultDomain;
				}
				//Interaction.Command(tcp, "MAIL FROM: <" + mail.From + ">");
				Command(tcp, "MAIL FROM: <" + mail.From + ">", "250");
 
				//-- send email to more than one recipient
				string[] arRecipients = mail.To.Split(_strAddressSeperator.ToCharArray());
				string strRecipient = null;
				foreach (string strRecipient_loopVariable in arRecipients)
				{
					strRecipient = strRecipient_loopVariable;
					//Interaction.Command(tcp, "RCPT TO: <" + strRecipient + ">");
					Command(tcp, "RCPT TO: <" + strRecipient + ">", "250");
				}
 
				//Interaction.Command(tcp, "DATA", "354");
				Command(tcp, "DATA", "354");
 
				System.Text.StringBuilder objStringBuilder = new System.Text.StringBuilder();
				var _with1 = objStringBuilder;
				//-- write common email headers
				_with1.Append("To: " + mail.To + Environment.NewLine);
				_with1.Append("From: " + mail.From + Environment.NewLine);
				_with1.Append("Subject: " + mail.Subject + Environment.NewLine);
 
                //if (_blnPlainTextOnly)
                //{
                //    //-- write plain text body
                //    _with1.Append(Environment.NewLine + mail.Body + Environment.NewLine);
                //}
                //else
				{
					string strContentType = null;
					//-- typical case; mixed content will be displayed side-by-side
					strContentType = "multipart/mixed";
					//-- unusual case; text and HTML body are both included, let the reader determine which it can handle
					if (!string.IsNullOrEmpty(mail.Body) & !string.IsNullOrEmpty(mail.BodyHTML))
					{
						strContentType = "multipart/alternative";
					}
 
					_with1.Append("MIME-Version: 1.0" + Environment.NewLine);
					_with1.Append("Content-Type: " + strContentType + "; boundary=\"NextMimePart\"" + Environment.NewLine);
					_with1.Append("Content-Transfer-Encoding: 7bit" + Environment.NewLine);
					// -- default content (for non-MIME compliant email clients, should be extremely rare)
					_with1.Append("This message is in MIME format. Since your mail reader does not understand " + Environment.NewLine);
					_with1.Append("this format, some or all of this message may not be legible." + Environment.NewLine);
					//-- handle text body (if any)
					if (!string.IsNullOrEmpty(mail.Body))
					{
						_with1.Append(Environment.NewLine + "--NextMimePart" + Environment.NewLine);
						_with1.Append("Content-Type: text/plain;" + Environment.NewLine);
						_with1.Append(Environment.NewLine + mail.Body + Environment.NewLine);
					}
					// -- handle HTML body (if any)
					if (!string.IsNullOrEmpty(mail.BodyHTML))
					{
						_with1.Append(Environment.NewLine + "--NextMimePart" + Environment.NewLine);
						_with1.Append("Content-Type: text/html; charset=iso-8859-1" + Environment.NewLine);
						_with1.Append(Environment.NewLine + mail.BodyHTML + Environment.NewLine);
					}
					//-- handle attachment (if any)
					if (!string.IsNullOrEmpty(mail.AttachmentPath))
					{
						_with1.Append(FileToMimeString(mail.AttachmentPath));
					}
				}
				//-- <crlf>.<crlf> marks end of message content
				_with1.Append(Environment.NewLine + "." + Environment.NewLine);
 
				//Interaction.Command(tcp, objStringBuilder.ToString());
				//Interaction.Command(tcp, "QUIT", "");
				Command(tcp, objStringBuilder.ToString(), "250");
				Command(tcp, "QUIT", "");
				tcp.Close();
			}
 
			//--
			//-- turn a file into a base 64 string
			//--
			private string FileToMimeString(string strFilepath)
			{
				System.IO.FileStream objFilestream = null;
				System.Text.StringBuilder objStringBuilder = new System.Text.StringBuilder();
				//-- note that chunk size is equal to maximum line width
				const int intChunkSize = 75;
				byte[] bytRead = new byte[intChunkSize + 1];
				int intRead = 0;
				string strFilename = null;
 
				//-- get just the filename out of the path
				strFilename = System.IO.Path.GetFileName(strFilepath);
 
				var _with2 = objStringBuilder;
				_with2.Append(Environment.NewLine + "--NextMimePart" + Environment.NewLine);
				_with2.Append("Content-Type: application/octet-stream; name=\"" + strFilename + "\"" + Environment.NewLine);
				_with2.Append("Content-Transfer-Encoding: base64" + Environment.NewLine);
				_with2.Append("Content-Disposition: attachment; filename=\"" + strFilename + "\"" + Environment.NewLine);
				_with2.Append(Environment.NewLine);
 
				objFilestream = new System.IO.FileStream(strFilepath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
				intRead = objFilestream.Read(bytRead, 0, intChunkSize);
				while (intRead > 0)
				{
					objStringBuilder.Append(System.Convert.ToBase64String(bytRead, 0, intRead));
					objStringBuilder.Append(Environment.NewLine);
					intRead = objFilestream.Read(bytRead, 0, intChunkSize);
				}
				objFilestream.Close();
 
				return objStringBuilder.ToString();
			}
 
			private static string ToBase64(string data)
			{
				System.Text.UTF8Encoding Encoder = new System.Text.UTF8Encoding();
				return Convert.ToBase64String(Encoder.GetBytes(data));
			}
		}
	}
}
  
 /*****************************************************************************************/
/* ***************************************************************************************
 * ExceptionDialogue.cs
 * ***************************************************************************************/
/*****************************************************************************************/
//'--
//'--
//'-- Jeff Atwood
//'-- http://www.codinghorror.com
//'--

//-- Generic user error dialog
//--
//'-- UI adapted from
//'--
//'-- Alan Cooper's "About Face: The Essentials of User Interface Design"
//'-- Chapter VII, "The End of Errors", pages 423-440

 
//using System;
//using System.Drawing;
//using System.Windows.Forms;
 
namespace ExceptionHandling
{
	internal class ExceptionDialog : System.Windows.Forms.Form
	{
		#region " Windows Form Designer generated code "
 
		public ExceptionDialog()
			: base()
		{
			Load += UserErrorDialog_Load;
 
			//This call is required by the Windows Form Designer.
			InitializeComponent();
 
			//Add any initialization after the InitializeComponent() call
		}
 
		//Form overrides dispose to clean up the component list.
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if ((components != null))
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
 
		//Required by the Windows Form Designer

		private System.ComponentModel.IContainer components = null;
		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.
		//Do not modify it using the code editor.
		private System.Windows.Forms.Button withEventsField_btn1;
 
		internal System.Windows.Forms.Button btn1
		{
			get { return withEventsField_btn1; }
			set
			{
				if (withEventsField_btn1 != null)
				{
					withEventsField_btn1.Click -= btn1_Click;
				}
				withEventsField_btn1 = value;
				if (withEventsField_btn1 != null)
				{
					withEventsField_btn1.Click += btn1_Click;
				}
			}
		}
 
		private System.Windows.Forms.Button withEventsField_btn2;
 
		internal System.Windows.Forms.Button btn2
		{
			get { return withEventsField_btn2; }
			set
			{
				if (withEventsField_btn2 != null)
				{
					withEventsField_btn2.Click -= btn2_Click;
				}
				withEventsField_btn2 = value;
				if (withEventsField_btn2 != null)
				{
					withEventsField_btn2.Click += btn2_Click;
				}
			}
		}
 
		private System.Windows.Forms.Button withEventsField_btn3;
 
		internal System.Windows.Forms.Button btn3
		{
			get { return withEventsField_btn3; }
			set
			{
				if (withEventsField_btn3 != null)
				{
					withEventsField_btn3.Click -= btn3_Click;
				}
				withEventsField_btn3 = value;
				if (withEventsField_btn3 != null)
				{
					withEventsField_btn3.Click += btn3_Click;
				}
			}
		}
 
		internal System.Windows.Forms.PictureBox PictureBox1;
		internal System.Windows.Forms.Label lblErrorHeading;
		internal System.Windows.Forms.Label lblScopeHeading;
		internal System.Windows.Forms.Label lblActionHeading;
		internal System.Windows.Forms.Label lblMoreHeading;
		internal System.Windows.Forms.TextBox txtMore;
		private System.Windows.Forms.Button withEventsField_btnMore;
 
		internal System.Windows.Forms.Button btnMore
		{
			get { return withEventsField_btnMore; }
			set
			{
				if (withEventsField_btnMore != null)
				{
					withEventsField_btnMore.Click -= btnMore_Click;
				}
				withEventsField_btnMore = value;
				if (withEventsField_btnMore != null)
				{
					withEventsField_btnMore.Click += btnMore_Click;
				}
			}
		}
 
		private System.Windows.Forms.RichTextBox withEventsField_ErrorBox;
 
		internal System.Windows.Forms.RichTextBox ErrorBox
		{
			get { return withEventsField_ErrorBox; }
			set
			{
				if (withEventsField_ErrorBox != null)
				{
					withEventsField_ErrorBox.LinkClicked -= ErrorBox_LinkClicked;
				}
				withEventsField_ErrorBox = value;
				if (withEventsField_ErrorBox != null)
				{
					withEventsField_ErrorBox.LinkClicked += ErrorBox_LinkClicked;
				}
			}
		}
 
		private System.Windows.Forms.RichTextBox withEventsField_ScopeBox;
 
		internal System.Windows.Forms.RichTextBox ScopeBox
		{
			get { return withEventsField_ScopeBox; }
			set
			{
				if (withEventsField_ScopeBox != null)
				{
					withEventsField_ScopeBox.LinkClicked -= ScopeBox_LinkClicked;
				}
				withEventsField_ScopeBox = value;
				if (withEventsField_ScopeBox != null)
				{
					withEventsField_ScopeBox.LinkClicked += ScopeBox_LinkClicked;
				}
			}
		}
 
		private System.Windows.Forms.RichTextBox withEventsField_ActionBox;
 
		internal System.Windows.Forms.RichTextBox ActionBox
		{
			get { return withEventsField_ActionBox; }
			set
			{
				if (withEventsField_ActionBox != null)
				{
					withEventsField_ActionBox.LinkClicked -= ActionBox_LinkClicked;
				}
				withEventsField_ActionBox = value;
				if (withEventsField_ActionBox != null)
				{
					withEventsField_ActionBox.LinkClicked += ActionBox_LinkClicked;
				}
			}
		}
 
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.PictureBox1 = new System.Windows.Forms.PictureBox();
			this.lblErrorHeading = new System.Windows.Forms.Label();
			this.ErrorBox = new System.Windows.Forms.RichTextBox();
			this.lblScopeHeading = new System.Windows.Forms.Label();
			this.ScopeBox = new System.Windows.Forms.RichTextBox();
			this.lblActionHeading = new System.Windows.Forms.Label();
			this.ActionBox = new System.Windows.Forms.RichTextBox();
			this.lblMoreHeading = new System.Windows.Forms.Label();
			this.btn1 = new System.Windows.Forms.Button();
			this.btn2 = new System.Windows.Forms.Button();
			this.btn3 = new System.Windows.Forms.Button();
			this.txtMore = new System.Windows.Forms.TextBox();
			this.btnMore = new System.Windows.Forms.Button();
			this.SuspendLayout();
			//
			//PictureBox1
			//
			this.PictureBox1.Location = new System.Drawing.Point(8, 8);
			this.PictureBox1.Name = "PictureBox1";
			this.PictureBox1.Size = new System.Drawing.Size(32, 32);
			this.PictureBox1.TabIndex = 0;
			this.PictureBox1.TabStop = false;
			//
			//lblErrorHeading
			//
			this.lblErrorHeading.AutoSize = true;
			this.lblErrorHeading.Font = new System.Drawing.Font("Tahoma", 8f, System.Drawing.FontStyle.Bold);
			this.lblErrorHeading.Location = new System.Drawing.Point(48, 4);
			this.lblErrorHeading.Name = "lblErrorHeading";
			this.lblErrorHeading.Size = new System.Drawing.Size(91, 16);
			this.lblErrorHeading.TabIndex = 0;
			this.lblErrorHeading.Text = "What happened";
			//
			//ErrorBox
			//
			this.ErrorBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.ErrorBox.BackColor = System.Drawing.SystemColors.Control;
			this.ErrorBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.ErrorBox.CausesValidation = false;
			this.ErrorBox.Location = new System.Drawing.Point(48, 24);
			this.ErrorBox.Name = "ErrorBox";
			this.ErrorBox.ReadOnly = true;
			this.ErrorBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.ErrorBox.Size = new System.Drawing.Size(416, 64);
			this.ErrorBox.TabIndex = 1;
			this.ErrorBox.Text = "(error message)";
			//
			//lblScopeHeading
			//
			this.lblScopeHeading.AutoSize = true;
			this.lblScopeHeading.Font = new System.Drawing.Font("Tahoma", 8f, System.Drawing.FontStyle.Bold);
			this.lblScopeHeading.Location = new System.Drawing.Point(8, 92);
			this.lblScopeHeading.Name = "lblScopeHeading";
			this.lblScopeHeading.Size = new System.Drawing.Size(134, 16);
			this.lblScopeHeading.TabIndex = 2;
			this.lblScopeHeading.Text = "How this will affect you";
			//
			//ScopeBox
			//
			this.ScopeBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.ScopeBox.BackColor = System.Drawing.SystemColors.Control;
			this.ScopeBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.ScopeBox.CausesValidation = false;
			this.ScopeBox.Location = new System.Drawing.Point(24, 112);
			this.ScopeBox.Name = "ScopeBox";
			this.ScopeBox.ReadOnly = true;
			this.ScopeBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.ScopeBox.Size = new System.Drawing.Size(440, 64);
			this.ScopeBox.TabIndex = 3;
			this.ScopeBox.Text = "(scope)";
			//
			//lblActionHeading
			//
			this.lblActionHeading.AutoSize = true;
			this.lblActionHeading.Font = new System.Drawing.Font("Tahoma", 8f, System.Drawing.FontStyle.Bold);
			this.lblActionHeading.Location = new System.Drawing.Point(8, 180);
			this.lblActionHeading.Name = "lblActionHeading";
			this.lblActionHeading.Size = new System.Drawing.Size(143, 16);
			this.lblActionHeading.TabIndex = 4;
			this.lblActionHeading.Text = "What you can do about it";
			//
			//ActionBox
			//
			this.ActionBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.ActionBox.BackColor = System.Drawing.SystemColors.Control;
			this.ActionBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.ActionBox.CausesValidation = false;
			this.ActionBox.Location = new System.Drawing.Point(24, 200);
			this.ActionBox.Name = "ActionBox";
			this.ActionBox.ReadOnly = true;
			this.ActionBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.ActionBox.Size = new System.Drawing.Size(440, 92);
			this.ActionBox.TabIndex = 5;
			this.ActionBox.Text = "(action)";
			//
			//lblMoreHeading
			//
			this.lblMoreHeading.AutoSize = true;
			this.lblMoreHeading.Font = new System.Drawing.Font("Tahoma", 8f, System.Drawing.FontStyle.Bold);
			this.lblMoreHeading.Location = new System.Drawing.Point(8, 300);
			this.lblMoreHeading.Name = "lblMoreHeading";
			this.lblMoreHeading.TabIndex = 6;
			this.lblMoreHeading.Text = "More information";
			//
			//btn1
			//
			this.btn1.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.btn1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btn1.Location = new System.Drawing.Point(220, 544);
			this.btn1.Name = "btn1";
			this.btn1.TabIndex = 9;
			this.btn1.Text = "Button1";
			//
			//btn2
			//
			this.btn2.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.btn2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btn2.Location = new System.Drawing.Point(304, 544);
			this.btn2.Name = "btn2";
			this.btn2.TabIndex = 10;
			this.btn2.Text = "Button2";
			//
			//btn3
			//
			this.btn3.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.btn3.Location = new System.Drawing.Point(388, 544);
			this.btn3.Name = "btn3";
			this.btn3.TabIndex = 11;
			this.btn3.Text = "Button3";
			//
			//txtMore
			//
			this.txtMore.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.txtMore.CausesValidation = false;
            this.txtMore.Font = new System.Drawing.Font("Lucida Console", 12.0f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.txtMore.Location = new System.Drawing.Point(8, 324);
			this.txtMore.Multiline = true;
			this.txtMore.Name = "txtMore";
			this.txtMore.ReadOnly = true;
			this.txtMore.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtMore.Size = new System.Drawing.Size(456, 212);
			this.txtMore.TabIndex = 8;
			this.txtMore.Text = "(detailed information, such as exception details)";
			//
			//btnMore
			//
			this.btnMore.Location = new System.Drawing.Point(112, 296);
			this.btnMore.Name = "btnMore";
			this.btnMore.Size = new System.Drawing.Size(28, 24);
			this.btnMore.TabIndex = 7;
			this.btnMore.Text = ">>";
			//
			//ExceptionDialog
			//
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(472, 573);
			this.Controls.Add(this.btnMore);
			this.Controls.Add(this.txtMore);
			this.Controls.Add(this.btn3);
			this.Controls.Add(this.btn2);
			this.Controls.Add(this.btn1);
			this.Controls.Add(this.lblMoreHeading);
			this.Controls.Add(this.lblActionHeading);
			this.Controls.Add(this.lblScopeHeading);
			this.Controls.Add(this.lblErrorHeading);
			this.Controls.Add(this.ActionBox);
			this.Controls.Add(this.ScopeBox);
			this.Controls.Add(this.ErrorBox);
			this.Controls.Add(this.PictureBox1);
			this.MinimizeBox = false;
			this.Name = "ExceptionDialog";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "(app) has encountered a problem";
			this.TopMost = true;
			this.ResumeLayout(false);
		}
 
		#endregion " Windows Form Designer generated code "
 
		const int _intSpacing = 10;
 
		//--
		//-- security-safe process.start wrapper
		//--
		private void LaunchLink(string strUrl)
		{
			try
			{
				System.Diagnostics.Process.Start(strUrl);
			}
			catch (System.Security.SecurityException)
			{
				//-- do nothing; we can't launch without full trust.
			}
		}
 
		private void SizeBox(System.Windows.Forms.RichTextBox ctl)
		{
			Graphics g = null;
			try
			{
				//-- note that the height is taken as MAXIMUM, so size the label for maximum desired height!
				g = Graphics.FromHwnd(ctl.Handle);
				SizeF objSizeF = g.MeasureString(ctl.Text, ctl.Font, new SizeF(ctl.Width, ctl.Height));
				g.Dispose();
				ctl.Height = Convert.ToInt32(objSizeF.Height) + 5;
			}
			catch (System.Security.SecurityException)
			{
				//-- do nothing; we can't set control sizes without full trust
			}
			finally
			{
				if ((g != null))
					g.Dispose();
			}
		}
 
		private System.Windows.Forms.DialogResult DetermineDialogResult(string strButtonText)
		{
			DialogResult returnVal = DialogResult.None;
 
			//-- strip any accelerator keys we might have
			strButtonText = strButtonText.Replace("&", "");
			switch (strButtonText.ToLower())
			{
				case "abort":
					return System.Windows.Forms.DialogResult.Abort;
                    //break;
				case "cancel":
					return System.Windows.Forms.DialogResult.Cancel;
                    //break;
				case "ignore":
					return System.Windows.Forms.DialogResult.Ignore;
                    //break;
				case "no":
					return System.Windows.Forms.DialogResult.No;
                    //break;
				case "none":
					return System.Windows.Forms.DialogResult.None;
                    //break;
				case "ok":
					return System.Windows.Forms.DialogResult.OK;
                    //break;
				case "retry":
					return System.Windows.Forms.DialogResult.Retry;
                    //break;
				case "yes":
					return System.Windows.Forms.DialogResult.Yes;
                    //break;
			}
			return returnVal;
		}
 
		private void btn1_Click(System.Object sender, System.EventArgs e)
		{
			this.Close();
			this.DialogResult = DetermineDialogResult(btn1.Text);
		}
 
		private void btn2_Click(System.Object sender, System.EventArgs e)
		{
			this.Close();
			this.DialogResult = DetermineDialogResult(btn2.Text);
		}
 
		private void btn3_Click(System.Object sender, System.EventArgs e)
		{
			this.Close();
			this.DialogResult = DetermineDialogResult(btn3.Text);
		}
 
		private void UserErrorDialog_Load(System.Object sender, System.EventArgs e)
		{
			//-- make sure our window is on top
			this.TopMost = true;
			this.TopMost = false;
 
			//-- More >> has to be expanded
			this.txtMore.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.txtMore.Visible = false;
 
			//-- size the labels' height to accommodate the amount of text in them
			SizeBox(ScopeBox);
			SizeBox(ActionBox);
			SizeBox(ErrorBox);
 
			//-- now shift everything up
			lblScopeHeading.Top = ErrorBox.Top + ErrorBox.Height + _intSpacing;
			ScopeBox.Top = lblScopeHeading.Top + lblScopeHeading.Height + _intSpacing;
 
			lblActionHeading.Top = ScopeBox.Top + ScopeBox.Height + _intSpacing;
			ActionBox.Top = lblActionHeading.Top + lblActionHeading.Height + _intSpacing;
 
			lblMoreHeading.Top = ActionBox.Top + ActionBox.Height + _intSpacing;
			btnMore.Top = lblMoreHeading.Top - 3;
 
			this.Height = btnMore.Top + btnMore.Height + _intSpacing + 45;
 
			this.CenterToScreen();
		}
 
		private void btnMore_Click(System.Object sender, System.EventArgs e)
		{
			if (btnMore.Text == ">>")
			{
				this.Height = this.Height + 300;
				var _with1 = txtMore;
				_with1.Location = new System.Drawing.Point(lblMoreHeading.Left, lblMoreHeading.Top + lblMoreHeading.Height + _intSpacing);
				_with1.Height = this.ClientSize.Height - txtMore.Top - 45;
				_with1.Width = this.ClientSize.Width - 2 * _intSpacing;
				_with1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
				_with1.Visible = true;
				btn3.Focus();
				btnMore.Text = "<<";
			}
			else
			{
				this.SuspendLayout();
				btnMore.Text = ">>";
				this.Height = btnMore.Top + btnMore.Height + _intSpacing + 45;
				txtMore.Visible = false;
				txtMore.Anchor = System.Windows.Forms.AnchorStyles.None;
				this.ResumeLayout();
			}
		}
 
		private void ErrorBox_LinkClicked(System.Object sender, System.Windows.Forms.LinkClickedEventArgs e)
		{
			LaunchLink(e.LinkText);
		}
 
		private void ScopeBox_LinkClicked(System.Object sender, System.Windows.Forms.LinkClickedEventArgs e)
		{
			LaunchLink(e.LinkText);
		}
 
		private void ActionBox_LinkClicked(System.Object sender, System.Windows.Forms.LinkClickedEventArgs e)
		{
			LaunchLink(e.LinkText);
		}
	}
}

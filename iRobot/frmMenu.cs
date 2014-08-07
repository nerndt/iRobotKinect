using System;
using System.Drawing;
using System.IO.Ports;
using System.Collections;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;

using RoombaSCI;

//TODO: My goal here, is for this to be the base form, and it will call a UI Class. This form is to be a shell only, calling single commands from the UI Class

namespace iRobotKinect
{
    public partial class frmMenu : Form
    {

        //These are to go into some kind of UI Statistics object
        //int m_iConnection_Length;
        //int m_iTotalConnection_Length;
        //TimeSpan m_tsConnectedTime;
        //TimeSpan m_tsTotalConnectionTime;
        //TimeSpan m_Read_Roomba_Sensors;
        //TimeSpan m_Draw_From_Roomba_History;
        //System.Diagnostics.Stopwatch m_Old_History;

        public frmMenu()
        {
            InitializeComponent();
        }

        #region Properties

        private RoombaSCI.Timer p_tForm_Timer;

            /// <summary>
            /// 
            /// </summary>
            public RoombaSCI.Timer Form_Timer
            {
                get
                {
                    return (this.p_tForm_Timer);
                }
                set
                {
                    this.p_tForm_Timer = value;
                }
            }

        #endregion
        #region Events

            #region Form

                private void frmMenu_Load(object sender, EventArgs e)
                {


                }
                private void frmMenu_FormClosed(object sender, FormClosedEventArgs e)
                {
                    Program.Menu_Cache.Remove(this.Handle);
                }

            #endregion
            #region MenuItems

                protected void configToolStripMenuItem_Click(object sender, EventArgs e)
                {
                    this.configToolStripMenuItem.Enabled = false;
                    Program.UI.Open_Config_Form(this, "", new Point(0,0));
                }
                protected void sensorPacketToolStripMenuItem_Click(object sender, EventArgs e)
                {
                    sensorPacketToolStripMenuItem.Enabled = false;
                    Program.UI.Open_Packet_Form(this, new Point(0, 0));
                }

                protected void driveToolStripMenuItem_Click(object sender, EventArgs e)
                {
                    driveToolStripMenuItem.Enabled = false;
                    Program.UI.Open_Drive_Form(this, new Point(0, 0));
                }
                protected void sensorsToolStripMenuItem_Click(object sender, EventArgs e)
                {
                    sensorsToolStripMenuItem.Enabled = false;
                    Program.UI.Open_Sensors_Form(this, new Point(0, 0));
                }

                //lblConnection
                protected void connectionDetailsToolStripMenuItem_Click(object sender, EventArgs e)
                {
                    frmConnection_Details Details = new frmConnection_Details();
                    Details.ShowDialog(); //we may not want this to be modal. If so, have it inherit from frmMenu and add it to the cache
                }

            #endregion
            #region labels

                private void lblConnection_Click(object sender, EventArgs e)
                {
                    //Start if stopped, Stop if running
                    
                }

            #endregion

                private void frmMenu_FormClosing(object sender, FormClosingEventArgs e)
                {
                    //Program.UI.MenuForm = null;
                }

        #endregion

    }
}
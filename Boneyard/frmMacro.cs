using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using RoombaSCI;

namespace iRobotKinect
{
    public partial class frmMacro : iRobotKinect.frmMenu
    {
        public frmMacro()
        {
            InitializeComponent();
        }
        private void frmMacro_Load(object sender, EventArgs e)
        {
            //a Macro is created in Roomba obj, but just in case someone set it to null
            if (Program.UI.CurrentRoomba.Macro == null) 
            {
                Program.UI.CurrentRoomba.Macro = new Macro(Program.UI.CurrentRoomba);
            }
        }

        #region Events

            private void rRecord_On_CheckedChanged(object sender, EventArgs e)
            {
                try
                {
                    Program.UI.CurrentRoomba.Macro.Record();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                this.SetControls();
            }
            private void rRecord_Off_CheckedChanged(object sender, EventArgs e)
            {
                this.SetControls();
            }

            #region MenuItems

                private void frmMacro_FormClosing(object sender, FormClosingEventArgs e)
                {
                    //this.macroToolStripMenuItem.Enabled = true;
                    Program.Menu_Cache.Remove(this.Handle);
                    Program.UI.CopyMyMenu(this);
                }

            #endregion

        #endregion

        protected void SetControls()
        {
            //Macro will also check the path..
            rRecord_On.Checked = Program.UI.CurrentRoomba.Macro.Recording;
        }
    }
}


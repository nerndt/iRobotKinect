using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iRobotKinect
{
    public partial class KinectCtrl : UserControl
    {
        public KinectCtrl()
        {
            InitializeComponent();
            if (!this.DesignMode)
            {
                KinectForm kinectForm = new KinectForm();
                kinectForm.TopLevel = false;
                kinectForm.Location = new Point(0, 0);
                kinectForm.FormBorderStyle = FormBorderStyle.None;
                kinectForm.Visible = true;
                this.Controls.Add(kinectForm);
            }
        }
    }
}

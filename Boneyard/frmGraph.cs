using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace iRobotKinect
{
    public partial class frmGraph : iRobotKinect.frmMenu
    {
        #region Line Chart
            //Value that Chart in tracking
            protected ArrayList m_Chart_arSensorsUpdateTime_MS = new ArrayList();

            //Represents the flow of time (number of polls)
            protected ArrayList m_Chart_arTime = new ArrayList();

            //Class that creates Chart
            protected Line2D m_Chart_line2d;

            protected int m_TimeStep = 400;

            protected List<int> m_Chart_lAvgUpdate;

        #endregion

        public frmGraph()
        {
            InitializeComponent();


            //Add a chart to RoombaUI's list of chart, set the handle to this form, and set chart:

            //m_Chart_lAvgUpdate = New List(Of Integer)
            //this.UI.Chart = new Line2D();

            //this.UI.Chart.ShowYAxis = true;
            //this.UI.Chart.XAxisText = "History";
            //this.UI.Chart.YAxisText = "MILLISECONDS";
            //this.UI.Chart.Title = "Sensor Data";
            //this.UI.Chart.TitleBackColor = Color.Transparent;
            //this.UI.Chart.TitleForeColor = Color.Black;
        }

        private void frmGraph_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.Menu_Cache.Remove(this.Handle);
        }
        
        //These need to go into a separate class
        private void Add_Point(double dLatest_Data_Point, bool bDraw)
        {
            //if ((dLatest_Data_Point > udIgnore.Value))
            //{
            //    return;
            //}
            //try
            //{
            //    if ((dLatest_Data_Point < udIgnore.Value))
            //    {
            //        if ((m_Chart_arSensorsUpdateTime_MS.Count > 0))
            //        {
            //            for (int i = 0; i <= m_Chart_arSensorsUpdateTime_MS.Count - 1; i++)
            //            {
            //                m_Chart_arSensorsUpdateTime_MS.Item(i) = m_Chart_arSensorsUpdateTime_MS.Item(i) - this.udIncrement.Value;
            //            }
            //        }
            //        if ((m_Chart_arSensorsUpdateTime_MS.Count < this.udDataPoints.Value))
            //        {
            //            m_Chart_arSensorsUpdateTime_MS.Add(m_TimeStep);
            //        }
            //        else
            //        {
            //            m_Chart_arSensorsUpdateTime_MS.RemoveAt(0);
            //        }
            //        if ((m_Chart_arTime.Count < this.udDataPoints.Value))
            //        {
            //           // m_Chart_arTime.Add(dLatest_Data_Point - this.vSubtract.Value);
            //        }
            //        else
            //        {
            //            //m_Chart_arTime.RemoveAt(0);
            //        }
            //    }
            //    else
            //    {
            //        //m_TimeStep = m_TimeStep + this.udIncrement.Value;
            //    }
            //    if ((bDraw))
            //    {
            //        this.Draw();
            //    }
            //}
            //catch (Exception ex)
            //{
            //   // Debugger.Break();
            //}
        }
        private void Draw()
        {

            //this.toolTip1.SetToolTip(pChart, "m_Chart_arTime.Count = '" + m_Chart_arTime.Count + "'");
            m_Chart_line2d.InitializeGraph();
            Application.DoEvents();

            //m_Chart_line2d.Height = this.udHeight.Value;
            //m_Chart_line2d.Width = this.udWidth.Value;
            //m_Chart_line2d.XSlice = this.udXSlice.Value;
            //m_Chart_line2d.YSlice = this.udYSlice.Value;

            m_Chart_line2d.XAxis = m_Chart_arSensorsUpdateTime_MS;
            m_Chart_line2d.YAxis = m_Chart_arTime;
            m_Chart_line2d.CreateGraph(Color.Black);
            //this.pChart.Image = m_Chart_line2d.GetGraph;
        }
    }
}


using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace iRobotKinect
{
    [Serializable]
    public class Line2D
    {
        private int m_Width = 700;
        private int m_Height = 400;
        private ArrayList m_XAxis;
        private ArrayList m_YAxis;
        private Color m_graphColor = Color.Red;
        private float m_XSlice = 1;
        private float m_YSlice = 1;
        private Graphics objGraphics;
        private Bitmap objBitmap;
        private string m_XAxisText = "X-Axis";
        private string m_YAxisText = "Y-Axis";
        private string m_Title = "Line Graph";
        private Color m_TitleBackColor = Color.Cyan;
        private Color m_TitleForeColor = Color.Green;
        private bool p_bShowXAxis;
        private bool p_bShowYAxis;

        public int Width
        {
            get
            {
                return m_Width;
            }
            set
            {
                if ((value > 100))
                {
                    m_Width = value;
                }
            }
        }
        public int Height
        {
            get
            {
                return m_Height;
            }
            set
            {
                if ((value > 100))
                {
                    m_Height = value;
                }
            }
        }
        public ArrayList XAxis
        {
            get
            {
                return m_XAxis;
            }
            set
            {
                m_XAxis = value;
            }
        }
        public ArrayList YAxis
        {
            get
            {
                return m_YAxis;
            }
            set
            {
                m_YAxis = value;
            }
        }
        public Color GraphColor
        {
            get
            {
                return m_graphColor;
            }
            set
            {
                m_graphColor = value;
            }
        }
        public float XSlice
        {
            get
            {
                return m_XSlice;
            }
            set
            {
                m_XSlice = value;
            }
        }
        public float YSlice
        {
            get
            {
                return m_YSlice;
            }
            set
            {
                m_YSlice = value;
            }
        }
        public string XAxisText
        {
            get
            {
                return m_XAxisText;
            }
            set
            {
                m_XAxisText = value;
            }
        }
        public string YAxisText
        {
            get
            {
                return m_YAxisText;
            }
            set
            {
                m_YAxisText = value;
            }
        }
        public string Title
        {
            get
            {
                return m_Title;
            }
            set
            {
                m_Title = value;
            }
        }
        public Color TitleBackColor
        {
            get
            {
                return m_TitleBackColor;
            }
            set
            {
                m_TitleBackColor = value;
            }
        }
        public Color TitleForeColor
        {
            get
            {
                return m_TitleForeColor;
            }
            set
            {
                m_TitleForeColor = value;
            }
        }
        public bool ShowXAxis
        {
            get
            {
                return p_bShowXAxis;
            }
            set
            {
                p_bShowXAxis = value;
            }
        }
        public bool ShowYAxis
        {
            get
            {
                return p_bShowYAxis;
            }
            set
            {
                p_bShowYAxis = value;
            }
        }
        public void InitializeGraph()
        {
            objBitmap = new Bitmap(Width, Height);
            objGraphics = Graphics.FromImage(objBitmap);
            objGraphics.FillRectangle(new SolidBrush(Color.LightGray), 0, 0, Width, Height);
            SetAxisText(ref objGraphics);
            CreateTitle(ref objGraphics);
        }
        public void CreateGraph(Color _GraphColor)
        {
            GraphColor = _GraphColor;
            SetPixels(ref objGraphics);
        }
        public Bitmap GetGraph()
        {
            if ((this.ShowXAxis))
            {
                SetXAxis(ref objGraphics, XSlice);
            }
            if ((this.ShowYAxis))
            {
                SetYAxis(ref objGraphics, YSlice);
            }
            return objBitmap;
        }
        
        private void PlotGraph(ref Graphics objGraphics, float x1, float y1, float x2, float y2)
        {
            objGraphics.DrawLine(new Pen(new SolidBrush(GraphColor), 2), x1 + 100, (Height - 100) - y1, x2 + 100, (Height - 100) - y2);
        }
        private void SetXAxis(ref Graphics objGraphics, float iSlices)
        {
        }
        private void SetYAxis(ref Graphics objGraphics, float iSlices)
        {
        }
        private void SetPixels(ref Graphics objGraphics)
        {
            float X1 = float.Parse(XAxis[0].ToString());
            float Y1 = float.Parse(YAxis[0].ToString());
            if (XAxis.Count == YAxis.Count)
            {
                int iXaxis = 0;
                int iYaxis = 0;
                while ((iXaxis < XAxis.Count - 1 && iYaxis < YAxis.Count - 1))
                {
                    PlotGraph(ref objGraphics, X1, Y1, float.Parse(XAxis[iXaxis + 1].ToString()), float.Parse(YAxis[iYaxis + 1].ToString()));
                    X1 = float.Parse(XAxis[iXaxis + 1].ToString());
                    Y1 = float.Parse(YAxis[iYaxis + 1].ToString());
                    System.Math.Min(System.Threading.Interlocked.Increment(ref iXaxis), iXaxis - 1);
                    System.Math.Min(System.Threading.Interlocked.Increment(ref iYaxis), iYaxis - 1);
                }
            }
            else
            {
            }
        }
        private void SetAxisText(ref Graphics objGraphics)
        {
        }
        private void CreateTitle(ref Graphics objGraphics)
        {
        }
    }
}

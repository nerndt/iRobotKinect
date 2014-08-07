using System;
using System.Runtime.InteropServices;
using System.Threading;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Platform;
using System.Drawing;

namespace iRobotKinect
{
    public class CloudExample : GameWindow
    {
        float rotation_speed = 90.0f;
        float angle;
        int vbo_id;
        int vbo_size;
        Matrix4 projection;

        const int CloudSize = 32;
        const bool HighQuality = true;

        public CloudExample()
            : base(800, 600)
        {
            Load += LoadHandler;
            Resize += ResizeHandler;
            UpdateFrame += UpdateHandler;
            RenderFrame += RenderHandler;
        }

        void LoadHandler(object sender, EventArgs e)
        {
            GL.Enable(EnableCap.DepthTest);

            // Improve visual quality at the expense of performance
            if (HighQuality)
            {
                int max_size;
                GL.GetInteger(GetPName.PointSizeMax, out max_size);
                GL.Enable(EnableCap.PointSmooth);
            }

            // Imagine that the cloud is a bool[CloudSize, CloudSize, CloudSize] array.
            // This code translates the point cloud into vertex coordinates
            var vertices = new Vector3[CloudSize * CloudSize * CloudSize];
            int index = 0;
            for (int i = 0; i < CloudSize; i++)
                for (int j = 0; j < CloudSize; j++)
                    for (int k = 0; k < CloudSize; k++)
                        if (Math.Sqrt(i * i + j * j + k * k) < CloudSize) // Point cloud shaped like a sphere
                        {
                            vertices[index++] = new Vector3(
                                -CloudSize / 2 + i,
                                -CloudSize / 2 + j,
                                -CloudSize / 2 + k);
                        }

            // Load those vertex coordinates into a VBO
            vbo_size = vertices.Length; // Necessary for rendering later on
            GL.GenBuffers(1, out vbo_id);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_id);
            GL.BufferData(BufferTarget.ArrayBuffer,
                          new IntPtr(vertices.Length * BlittableValueType.StrideOf(vertices)),
                          vertices, BufferUsageHint.StaticDraw);
        }

        void ResizeHandler(object sender, EventArgs e)
        {
            GL.Viewport(ClientRectangle);

            float aspect_ratio = Width / (float)Height;
            projection = Matrix4.CreatePerspectiveFieldOfView(OpenTK.MathHelper.PiOver4, aspect_ratio, 1, 512);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
        }

        void UpdateHandler(object sender, FrameEventArgs e)
        {
            if (Keyboard[OpenTK.Input.Key.Escape])
                this.Exit();
        }

        void RenderHandler(object sender, FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit |
                     ClearBufferMask.DepthBufferBit |
                     ClearBufferMask.StencilBufferBit);

            if (HighQuality)
            {
                // See http://www.opengl.org/discussion_boards/ubbthreads.php?ubb=showflat&Number=263583#Post263583
                GL.PointParameter(PointParameterName.PointDistanceAttenuation,
                    new float[] { 0, 0, (float)Math.Pow(1 / (projection.M11 * Width / 2), 2) });
            }

            if (!Keyboard[OpenTK.Input.Key.Space])
                angle += rotation_speed * (float)e.Time;

            Matrix4 lookat = Matrix4.LookAt(0, 128, 256, 0, 0, 0, 0, 1, 0);
            Vector3 scale = new Vector3(4, 4, 4);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);
            GL.Scale(scale);
            GL.Rotate(angle, 0.0f, 1.0f, 0.0f);

            // To draw a VBO:
            // 1) Ensure that the VertexArray client state is enabled.
            // 2) Bind the vertex and element buffer handles.
            // 3) Set up the data pointers (vertex, normal, color) according to your vertex format.

            GL.EnableClientState(ArrayCap.VertexArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_id);
            GL.VertexPointer(3, VertexPointerType.Float, Vector3.SizeInBytes, new IntPtr(0));
            GL.DrawArrays(BeginMode.Points, 0, vbo_size);

            SwapBuffers();
        }

        //public static void Main()
        //{
        //    using (var app = new CloudExample())
        //    {
        //        app.Run(30.0, 0.0);
        //    }
        //}
    }
}

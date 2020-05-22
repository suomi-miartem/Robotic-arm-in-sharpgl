using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpGL;


namespace robotic_arm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        float step = 1;

        float eyeX =5;
        float eyeY = 3;
        float eyeZ = 5;
        float centerX = 0;
        float centerY = 0;
        float centerZ = 0;
        float upX = 0;
        float upY = 1;
        float upZ = 0;
        private void openGLControl1_OpenGLInitialized(object sender, EventArgs e)
        {
            //  Возьмём OpenGL объект
            OpenGL gl = openGLControl1.OpenGL;
            //  Устанавливаем цвет заливки по умолчанию (в данном случае цвет голубой)
            gl.ClearColor(1f, 1f, 1f, 1f);
        }

        private void openGLControl1_Resized(object sender, EventArgs e)
        {
            //Возьмём OpenGL объект
            OpenGL gl = openGLControl1.OpenGL;
            //  Зададим матрицу проекции
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            //  Единичная матрица для последующих преобразований
            gl.LoadIdentity();
            //  Преобразование
            gl.Perspective(60.0f, (double)Width / (double)Height, 0.01, 100.0);

            //gl.LookAt(eyeX, eyeY, eyeZ,    // Позиция самой камеры (x, y, z)
            //             centerX, centerY, centerZ,     // Направление, куда мы смотрим
            //                upX, upY, upZ);    // Верх камеры

            //  Зададим модель отображения
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }

        private void openGLControl1_OpenGLDraw(object sender, RenderEventArgs args)
        {
            OpenGL gl = openGLControl1.OpenGL;
            //first quad
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            //  Единичная матрица для последующих преобразований
            gl.LoadIdentity();
            gl.LookAt(eyeX, eyeY, eyeZ,    // Позиция самой камеры (x, y, z)
                        centerX, centerY, centerZ,     // Направление, куда мы смотрим
                            upX, upY, upZ);    // Верх камеры
            gl.PolygonMode(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_FILL);
            DrawLine(gl);

            Drawpolygon(gl);
            DrawFinger(gl);//первый палец слева направо
            Draw_last_finger(gl);
            gl.PushMatrix();
            gl.Translate(0.0f, 0.0f, -1.0f);
            DrawFinger(gl);
            Draw_last_finger(gl);
            gl.PopMatrix();

            gl.PushMatrix();
            gl.Translate(0.0f, 0.0f, -2.0f);
            DrawFinger(gl);
            Draw_last_finger(gl);
            gl.PopMatrix();

            gl.PushMatrix();
            gl.Translate(0.0f, 0.0f, -3.0f);
            DrawFinger(gl);
            Draw_last_finger(gl);
            gl.PopMatrix();

            //крайний палец
            gl.PushMatrix();
            gl.Translate(-1.5f, 0.0f, -1.0f);
            gl.Rotate(90.0f, 0, 1, 0);
            DrawFinger(gl);
            Draw_last_finger(gl);
            gl.PopMatrix();

            gl.DrawText(5, 65, 255, 0, 0, "", 12, "eyeX : " + eyeX + ", eyeY : " + eyeY + ", eyeZ : " + eyeZ + "");
            gl.DrawText(5, 45, 255, 0, 0, "", 12, "centerX : " + centerX + ", centerY : " + centerY + ", centerZ : " + centerZ + "");
            gl.DrawText(5, 25, 255, 0, 0, "", 12, "upX : " + upX + ", upY : " + upY + ", upZ : " + upZ + "");
            gl.Flush();
        }
        
        void Draw_last_finger(OpenGL gl)
        {
            float[] x = { 3.0f, 3.0f, 4.5f, 4.5f };
            float[] z = { 2.0f, 1.0f, 1.33f, 1.67f };
            gl.Color(1.0f, 0.5f, 0.5f);
            gl.Begin(OpenGL.GL_POLYGON);//почемуто не ровно
            for (int i = 0; i < 4; ++i)
                gl.Vertex(x[i], 0.0f, z[i]);
            gl.End();
            gl.Color(0.5f, 0.0f, 0.5f);
            gl.Begin(OpenGL.GL_POLYGON);
            for (int i = 0; i < 4; ++i)
                gl.Vertex(x[i], -1.0f, z[i]);
            gl.End();

            gl.Begin(OpenGL.GL_QUADS);
            for (int i1 = 0; i1 < 4; ++i1)
            {
                //gl.Color(1.0f, 0.5f, 0.0f); 
                gl.Color(
                  i1 < 2 || i1 > 4 ? 0.0f : 1.0f,
                  i1 > 0 && i1 < 5 ? 0.0f : 1.0f,
                  i1 > 2 ? 1.0f : 0.0f,
                  1.0f
                );

                int i2 = (i1 + 1) % 4;//следующие координаты после i
                                      //нижние точки
                gl.Vertex(x[i1], 0.0f, z[i1]);
                gl.Vertex(x[i2], 0.0f, z[i2]);
                //верхние точки
                gl.Vertex(x[i2], -1.0f, z[i2]);
                gl.Vertex(x[i1], -1.0f, z[i1]);
            }
            gl.End();

        }
        void DrawFinger(OpenGL gl)
        {
            float[] x = { 1.5f,1.5f,3.0f,3.0f };
            float[] z = { 2.0f,1.0f,1.0f,2.0f };
            gl.Color(1.0f, 0.0f, 1.0f);
            gl.Begin(OpenGL.GL_POLYGON);//почемуто не ровно
            for (int i = 0; i < 4; ++i)
                gl.Vertex(x[i], 0.0f, z[i]);
            gl.End();
            gl.Color(1.0f, 0.0f, 0.5f);
            gl.Begin(OpenGL.GL_POLYGON);
            for (int i = 0; i < 4; ++i)
                gl.Vertex(x[i], -1.0f, z[i]);
            gl.End();

            gl.Begin(OpenGL.GL_QUADS);
            for (int i1 = 0; i1 < 4; ++i1)
            {
                //gl.Color(1.0f, 0.5f, 0.0f); 
                gl.Color(
                  i1 < 2 || i1 > 4 ? 1.0f : 0.0f,
                  i1 > 0 && i1 < 5 ? 1.0f : 0.0f,
                  i1 > 2 ? 1.0f : 0.0f,
                  1.0f
                );

                int i2 = (i1 + 1) % 4;//следующие координаты после i
                                      //нижние точки
                gl.Vertex(x[i1], 0.0f, z[i1]);
                gl.Vertex(x[i2], 0.0f, z[i2]);
                //верхние точки
                gl.Vertex(x[i2], -1.0f, z[i2]);
                gl.Vertex(x[i1], -1.0f, z[i1]);
            }
            gl.End();
        }
        private void Drawpolygon(OpenGL gl)
        {
            float[] x = { -1.5f, -1.5f, -0.5f, -0.5f, 0.5f, 0.5f, 1.5f, 1.5f };
            float[] z = { 2.0f, -2.0f, -2.0f, -2.5f, -2.5f, -2.0f, -2.0f, 2.0f };
            //front part
            gl.Color(1.0f, 0.0f, 1.0f);
            gl.Begin(OpenGL.GL_POLYGON);//почемуто не ровно
            for (int i = 0; i < 8; ++i)
                gl.Vertex(x[i], 0.0f, z[i]);
            gl.End();
            gl.Color(1.0f, 0.0f, 0.5f);
            gl.Begin(OpenGL.GL_POLYGON);
            for (int i = 0; i < 8; ++i)
                gl.Vertex(x[i], -1.0f, z[i]);
            gl.End();

            gl.Begin(OpenGL.GL_QUADS);
            for (int i1 = 0; i1 < 8; ++i1)
            {
                //gl.Color(1.0f, 0.5f, 0.0f); 
                gl.Color(
                  i1 < 2 || i1 > 4 ? 0.5f : 0.0f,
                  i1 > 0 && i1 < 5 ? 1.0f : 0.0f,
                  i1 > 2 ? 1.0f : 0.0f,
                  1.0f
                );

                int i2 = (i1 + 1) % 8;//следующие координаты после i
                                      //нижние точки
                gl.Vertex(x[i1], 0.0f, z[i1]);
                gl.Vertex(x[i2], 0.0f, z[i2]);
                //верхние точки
                gl.Vertex(x[i2], -1.0f, z[i2]);
                gl.Vertex(x[i1], -1.0f, z[i1]);
            }
            gl.End();
        }
        private void DrawPoint(OpenGL gl)
        {
            gl.PushMatrix();
            gl.PointSize(10.0f);
            gl.Begin(OpenGL.GL_POINTS);
            gl.Color(0.0, 0.0, 0.0);//X axis is red
            gl.Vertex(0.0, 0.0, 0.0);
            gl.End();
            gl.PopMatrix();
        }
        private void DrawLine(OpenGL gl)
        {
            gl.PushMatrix();
            gl.LineWidth(5.0f);
            gl.Begin(OpenGL.GL_LINES);
            gl.Color(1.0, 0.0, 0.0);//X axis is red
            gl.Vertex(0.0, 0.0, 0.0);
            gl.Vertex(10.0, 0.0, 0.0);
            gl.Color(0.0, 1.0, 0.0);//Y axis is green
            gl.Vertex(0.0, 0.0, 0.0);
            gl.Vertex(0.0, 10.0, 0.0);
            gl.Color(0.0, 0.0, 1.0);//Z axis is blue
            gl.Vertex(0.0, 0.0, 0.0);
            gl.Vertex(0.0, 0.0, 10.0);
            gl.End();
            gl.PopMatrix();
        }

        private void openGLControl1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape: this.Close(); break;
                //Control Eye X,Y,Z
                case Keys.Q: eyeX += step; break;
                case Keys.W: eyeX -= step; break;
                case Keys.A: eyeY += step; break;
                case Keys.S: eyeY -= step; break;
                case Keys.Z: eyeZ += step; break;
                case Keys.X: eyeZ -= step; break;
                //Control Up X,Y,Z
                case Keys.E: upX += step; break;
                case Keys.R: upX -= step; break;
                case Keys.D: upY += step; break;
                case Keys.F: upY -= step; break;
                case Keys.C: upZ += step; break;
                case Keys.V: upZ -= step; break;
                //Control Center X,Y,Z
                case Keys.T: centerX += step; break;
                case Keys.Y: centerX -= step; break;
                case Keys.G: centerY += step; break;
                case Keys.H: centerY -= step; break;
                case Keys.B: centerZ += step; break;
                case Keys.N: centerZ -= step; break;
            }
        }
    }


}

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
        float Angleofspoka = 0.0f;
        float eyeX =9;
        float eyeY = 3;
        float eyeZ = 0;
        float centerX = 0;
        float centerY = 0;
        float centerZ = 0;
        float upX = 0;
        float upY = 1;
        float upZ = 0;


        float rotobject = 0.0f;
        float[] rotsidefing = { 0.0f, 0.0f };
        float[] rotfing_1_2 = { 0.0f, 0.0f };
        float[] rotfing_3_4 = { 0.0f, 0.0f };

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
            //ладонь
            //gl.Translate()
            gl.PushMatrix();
            gl.Translate(-1.5f, 0.0f, 0.0f);
            gl.Rotate(rotobject, 0.0f, 0.0f, 1.0f);
            gl.Translate(1.5f, 0.0f, 0.0f);
            Drawpolygon(gl);



            float[] X1 = { 1.5f, 1.5f, 3.0f, 3.0f };
            float[] Z1 = { 2.0f, 1.0f, 1.0f, 2.0f };

            float[] x1 = { 3.0f, 3.0f, 4.5f, 4.5f };
            float[] z1 = { 2.0f, 1.0f, 1.0f, 1.70f };
            gl.PushMatrix();
            gl.Translate(1.5f, -1.0f, 1.25);
            gl.Rotate(rotfing_1_2[0], 0.0f, 0.0f, 1.0f);
            gl.Translate(-1.5f, 1.0f, -1.25);
            DrawFinger(gl,X1,Z1);//первый палец слева направо
            //gl.PushMatrix();
            gl.Translate(3.0f, -1.0f, 1.25);
            gl.Rotate(rotfing_1_2[1], 0.0f, 0.0f, 1.0f);
            gl.Translate(-3.0f, 1.0f, -1.25);
            Draw_last_finger(gl,x1,z1);
            //gl.PopMatrix();
            gl.PopMatrix();

            float[] X2 = { 1.5f, 1.5f, 3.0f, 3.0f };
            float[] Z2 = { 2.0f, 1.0f, 1.0f+Angleofspoka, 2.0f };

            float[] x2 = { 3.0f, 3.0f, 4.5f, 4.5f };
            float[] z2 = { 2.0f, 1.0f + Angleofspoka, 1.0f + (Angleofspoka+Angleofspoka), 2.0f };
            gl.PushMatrix();
            gl.Translate(0.0f, 0.0f, -1.0f);//перенос на начало второго пальца как будто первого
            gl.Translate(1.5f, -1.0f,1.25);
            gl.Rotate(rotfing_1_2[0], 0.0f, 0.0f, 1.0f);
            gl.Translate(-1.5f, 1.0f, -1.25);
            DrawFinger(gl,X2,Z2);
            //gl.PushMatrix();
            gl.Translate(3.0f, -1.0f, 1.25);
            gl.Rotate(rotfing_1_2[1], 0.0f, 0.0f, 1.0f);
            gl.Translate(-3.0f, 1.0f, -1.25);
            Draw_last_finger(gl,x2,z2);
            //gl.PopMatrix();
            gl.PopMatrix();

            float[] X3 = { 1.5f, 1.5f, 3.0f, 3.0f };
            float[] Z3 = { 2.0f, 1.0f, 1.0f, 2.0f-Angleofspoka };

            float[] x3 = { 3.0f, 3.0f, 4.5f, 4.5f };
            float[] z3 = { 2.0f - Angleofspoka, 1.0f, 1.0f, 2.0f - (Angleofspoka+Angleofspoka) };
            //третий палец объединен с четвертым
            gl.PushMatrix();//третий палец
            gl.Translate(0.0f, 0.0f, -2.0f);
            gl.Translate(1.5f, -1.0f, -1.25);
            gl.Rotate(rotfing_3_4[0], 0.0f, 0.0f, 1.0f);
            gl.Translate(-1.5f, 1.0f, 1.25);
            DrawFinger(gl,X3,Z3);
            //gl.PushMatrix();
            gl.Translate(3.0f, -1.0f, 1.25);
            gl.Rotate(rotfing_3_4[1], 0.0f, 0.0f, 1.0f);
            gl.Translate(-3.0f, 1.0f, -1.25);
            Draw_last_finger(gl,x3,z3);
            //gl.PopMatrix();
            gl.PopMatrix();

            float[] X4 = { 1.5f, 1.5f, 3.0f, 3.0f };
            float[] Z4 = { 2.0f, 1.0f, 1.0f, 2.0f };

            float[] x4 = { 3.0f, 3.0f, 4.5f, 4.5f };
            float[] z4 = { 2.0f, 1.0f, 1.30f, 2.00f };
            gl.PushMatrix();//четвертый палец
            gl.Translate(0.0f, 0.0f, -3.0f);
            gl.Translate(1.5f, -1.0f, -1.25);
            gl.Rotate(rotfing_3_4[0], 0.0f, 0.0f, 1.0f);
            gl.Translate(-1.5f, 1.0f, 1.25);
            DrawFinger(gl,X4,Z4);
            //gl.PushMatrix();
            gl.Translate(3.0f, -1.0f, 1.25);
            gl.Rotate(rotfing_3_4[1], 0.0f, 0.0f, 1.0f);
            gl.Translate(-3.0f, 1.0f, -1.25);
            Draw_last_finger(gl,x4,z4);
            //gl.PopMatrix();
            gl.PopMatrix();

            float[] X5 = { 1.5f, 1.5f, 3.0f, 3.0f };
            float[] Z5 = { 2.0f, 1.0f, 1.0f, 2.0f };
            float[] x5 = { 3.0f, 3.0f, 4.5f, 4.5f };
            float[] z5 = { 2.0f, 1.0f, 1.33f, 1.67f };
            //крайний палец
            gl.PushMatrix();
            gl.Translate(-1.5f, 0.0f, -1.0f);
            gl.Rotate(90.0f, 0, 1, 0);
            gl.Translate(1.5f, -1.0f, 0.5);
            gl.Rotate(rotsidefing[0], 0.0f, 0.0f, 1.0f);
            gl.Translate(-1.5f, 1.0f, -0.5);
            DrawFinger(gl,X5,Z5);
            gl.Translate(3.0f, -1.0f, 1.25);
            gl.Rotate(rotsidefing[1], 0.0f, 0.0f, 1.0f);
            gl.Translate(-3.0f, 1.0f, -1.25);
            Draw_last_finger(gl,x5,z5);
            gl.PopMatrix();

            gl.PopMatrix();
            gl.DrawText(5, 65, 255, 0, 0, "", 12, "eyeX : " + eyeX + ", eyeY : " + eyeY + ", eyeZ : " + eyeZ + "");
            gl.DrawText(5, 45, 255, 0, 0, "", 12, "centerX : " + centerX + ", centerY : " + centerY + ", centerZ : " + centerZ + "");
            gl.DrawText(5, 25, 255, 0, 0, "", 12, "upX : " + upX + ", upY : " + upY + ", upZ : " + upZ + "");
            gl.Flush();
        }
        
        void Draw_last_finger(OpenGL gl,float [] x, float [] z)//конец пальца
        {
            gl.Color(1.0f, 0.5f, 0.5f);
            gl.Begin(OpenGL.GL_POLYGON);//почемуто не ровно
            for (int i = 0; i < 4; ++i)
            {
                if(i<2)
                gl.Vertex(x[i], 0.0f, z[i]);
                else
                gl.Vertex(x[i], -0.4f, z[i]);
            }
            gl.End();
            gl.Color(0.5f, 0.0f, 0.5f);
            gl.Begin(OpenGL.GL_POLYGON);
            for (int i = 0; i < 4; ++i)
                gl.Vertex(x[i], -1.0f, z[i]);
            gl.End();

            gl.Begin(OpenGL.GL_QUADS);
            for (int i1 = 0; i1 < 4; i1++)
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
                switch (i1)
                {
                    case 0:
                        gl.Vertex(x[i1], 0.0f, z[i1]);
                        gl.Vertex(x[i2], 0.0f, z[i2]);
                        break;
                    case 1:
                        gl.Vertex(x[i1], 0.0f, z[i1]);
                        gl.Vertex(x[i2], -0.4f, z[i2]);
                        break;
                    case 2:
                        gl.Vertex(x[i1], -0.4f, z[i1]);
                        gl.Vertex(x[i2], -0.4f, z[i2]);
                        break;
                    case 3:
                        gl.Vertex(x[i1], -0.4f, z[i1]);
                        gl.Vertex(x[i2], 0.0f, z[i2]);
                        break;
                }  

                
                //верхние точки
                gl.Vertex(x[i2], -1.0f, z[i2]);
                gl.Vertex(x[i1], -1.0f, z[i1]);
            }
            gl.End();

        }
        void DrawFinger(OpenGL gl, float [] x, float []z)//палец
        {
            
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
        private void Drawpolygon(OpenGL gl)//ладонь
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
        //private void DrawPoint(OpenGL gl)
        //{
        //    gl.PushMatrix();
        //    gl.PointSize(10.0f);
        //    gl.Begin(OpenGL.GL_POINTS);
        //    gl.Color(0.0, 0.0, 0.0);//X axis is red
        //    gl.Vertex(0.0, 0.0, 0.0);
        //    gl.End();
        //    gl.PopMatrix();
        //}
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

                //rotations
                case Keys.D1:
                    rotobject += step;
                    if (rotobject >= 90)
                        rotobject = 90;
                    break;
                case Keys.D2:
                    rotobject -= step;
                    if (rotobject <= -90)
                        rotobject = -90;
                    break;
            
                //rotation of fingers
                //finger 1
                case Keys.D3:
                        rotfing_1_2[0] += step;
                        if (rotfing_1_2[0] >= 0)
                        {
                            rotfing_1_2[0] = 0;
                        }
                    break;//finger1
                case Keys.D4:
                    //if (radioButton1.Checked == true)
                    //{
                        rotfing_1_2[0] -= step;
                        if (rotfing_1_2[0] <= -90)
                        {
                            rotfing_1_2[0] = -90;
                        }
                    break;
                //finger 2
                case Keys.D5:
                    rotfing_3_4[0] += step;
                    if (rotfing_3_4[0] >= 0)
                    {
                        rotfing_3_4[0] = 0;
                    }
                    break;
                case Keys.D6:
                    rotfing_3_4[0] -= step;
                    if (rotfing_3_4[0] <= -90)
                    {
                        rotfing_3_4[0] = -90;
                    }
                    break;
                //sidefinger
                case Keys.D7:
                    rotsidefing[0] += step;
                    if (rotsidefing[0] >= 0)
                    {
                        rotsidefing[0] = 0;
                    }
                    break;//finger1
                case Keys.D8:
                    rotsidefing[0] -= step;
                    if (rotsidefing[0] <= -100)
                    {
                        rotsidefing[0] = -100;
                    }
                    break;




                //Rotation of forefingers
                case Keys.F3:
                    rotfing_1_2[1] += step;
                    if (rotfing_1_2[1] >= 0)
                    {
                        rotfing_1_2[1] = 0;
                    }
                    break;//finger1
                case Keys.F4:
                    rotfing_1_2[1] -= step;
                    if (rotfing_1_2[1] <= -90)
                    {
                        rotfing_1_2[1] = -90;
                    }
                    break;
                    //finger 2
                case Keys.F5:
                    rotfing_3_4[1] += step;
                    if (rotfing_3_4[1] >= 0)
                    {
                        rotfing_3_4[1] = 0;
                    }
                    break;
                case Keys.F6:
                    rotfing_3_4[1] -= step;
                    if (rotfing_3_4[1] <= -90)
                    {
                        rotfing_3_4[1] = -90;
                    }
                    break;
                //sidefinger
                case Keys.F7:
                    rotsidefing[1] += step;
                    if (rotsidefing[1] >= 0)
                    {
                        rotsidefing[1] = 0;
                    }
                    break;//finger1
                case Keys.F8:
                    rotsidefing[1] -= step;
                    if (rotsidefing[1] <= -100)
                    {
                        rotsidefing[1] = -100;
                    }
                    break;
                case Keys.Back:
                    rotobject = 0;
                    rotfing_1_2[0] = 0;
                    rotfing_1_2[1] = 0;
                    rotfing_3_4[0] = 0;
                    rotfing_3_4[1] = 0;
                    rotsidefing[0] = 0;
                    rotsidefing[1] = 0;
                    break;
                case Keys.U:
                    Angleofspoka += 0.1f;
                    if(Angleofspoka>0.4f)
                        Angleofspoka = 0.4f;
                    break;
                case Keys.I:
                    Angleofspoka -= 0.1f;
                    if (Angleofspoka < 0)
                        Angleofspoka = 0;
                    break;
            }
        }
    }


}

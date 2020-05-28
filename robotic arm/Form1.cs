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
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Assets;
using SharpGL.Enumerations;

namespace robotic_arm
{
    public partial class Form1 : Form
    {
        int formHeight;
        int formWidth;
        public Form1()
        {
            
            InitializeComponent();
            formHeight = this.Height;
            formWidth = this.Width;

            
        }
        bool taken = false;//объект захвачен или нет?

        Texture texture = new Texture();
        Texture texture1 = new Texture();
        float sphere_x = 0;
        float sphere_y = 0.0f;
        float sphere_z = 0.0f;

        float step = 1;
        float Angleofspoka = 0.3f;
        float eyeX =20;
        float eyeY = 3;
        float eyeZ = -15;
        float centerX = 0;
        float centerY = 0;
        float centerZ = 0;
        float upX = 0;
        float upY = 1;
        float upZ = 0;

        float posarm_x = 0.0f;
        float posarm_y = 0.0f;
        float posarm_z = 0.0f;


        float rotobjectZ = 0.0f;
        float[] rotsidefing = { 0.0f, 0.0f };
        float[] rotfing_1_2 = { 0.0f, 0.0f };
        float[] rotfing_3_4 = { 0.0f, 0.0f };

        private void openGLControl1_OpenGLInitialized(object sender, EventArgs e)
        {
            //  Возьмём OpenGL объект
            OpenGL gl = openGLControl1.OpenGL;
            //  Устанавливаем цвет заливки по умолчанию (в данном случае цвет голубой)
            gl.ClearColor(1f, 1f, 1f,1f);
            
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
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            gl.Enable(OpenGL.GL_DEPTH_TEST);
            float[] gloabalambient = { 0.5f, 0.5f, 0.5f, 1.0f };
            float[] ambient = { 0.2f, 0.2f, 0.2f, 1.0f };
            float[] diffuse = { 0.3f, 0.3f, 0.3f, 1.0f };
            float[] lpos = { Width / 2, Height / 2, 10.0f, 1.0f };
            float[] specular = { 0.8f, 0.8f, 0.8f, 0.8f };

            float[] lmodelambient = { 0.2f, 0.2f, 0.2f, 1.0f };


            //  Единичная матрица для последующих преобразований
            gl.LoadIdentity();



            gl.LightModel(OpenGL.GL_LIGHT_MODEL_AMBIENT, lmodelambient);
            gl.LightModel(OpenGL.GL_LIGHT_MODEL_AMBIENT, gloabalambient);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_AMBIENT, ambient);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_DIFFUSE, diffuse);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_POSITION, lpos);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_SPECULAR, specular);
            gl.Enable(OpenGL.GL_LIGHTING);
            gl.Enable(OpenGL.GL_LIGHT0);
            gl.Enable(OpenGL.GL_COLOR_MATERIAL);
            gl.ColorMaterial(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_AMBIENT_AND_DIFFUSE);
            gl.ShadeModel(OpenGL.GL_SMOOTH);

            gl.LookAt(eyeX, eyeY, eyeZ,    // Позиция самой камеры (x, y, z)
                        centerX, centerY, centerZ,     // Направление, куда мы смотрим
                            upX, upY, upZ);    // Верх камеры



            DrawWalls(gl, 20.0f, 20.0f, 20.0f);//стены


           
            //DrawLine(gl);
            //ладонь

            //gl.PushMatrix();
            gl.PushMatrix();
            
            if (move == false)//если кнопка переноса объекта не нажата
            {
                sphere_x = -15.0f;
                sphere_y = -3.0f;
                sphere_z = -5.0f;
                gl.Translate(sphere_x, sphere_y, sphere_z);
                Drawsphere(gl);
            }
            else
            {
                sphere_x = posarm_x;
                sphere_y = posarm_y-2.5f;
                sphere_z = posarm_z;
                gl.Translate(sphere_x, sphere_y, sphere_z);
                Drawsphere(gl);
            }

            gl.PopMatrix();

            gl.Translate(posarm_x, posarm_y, posarm_z);//move arm to above of object

            gl.Translate(-1.5f, 0.0f, 0.0f);
            gl.Rotate(rotobjectZ, 0.0f, 0.0f, 1.0f);
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
           

            gl.Flush();
            gl.DrawText(5, 65, 255, 0, 0, "", 12, "eyeX : " + eyeX + ", eyeY : " + eyeY + ", eyeZ : " + eyeZ + "");
            gl.DrawText(5, 45, 255, 0, 0, "", 12, "centerX : " + centerX + ", centerY : " + centerY + ", centerZ : " + centerZ + "");
            gl.DrawText(5, 25, 255, 0, 0, "", 12, "upX : " + upX + ", upY : " + upY + ", upZ : " + upZ + "");
            //gl.PopMatrix();
            if (movearm == true)
            {
                if (posarm_y < sphere_y)//Y надо двигать до уровня сферы +3
                {
                    posarm_y += 0.2f;
                    if (posarm_y > (sphere_y + 2.5f))
                        posarm_y = sphere_y + 2.5f;
                }
                if (posarm_y > sphere_y)
                {
                    posarm_y -= 0.2f;
                    if (posarm_y < (sphere_y + 2.5f))
                        posarm_y = sphere_y + 2.5f;
                }
                if (posarm_x < sphere_x)//X надо двигать до уровня сферы
                {
                    posarm_x += 0.2f;
                    if (posarm_x == sphere_x)
                        posarm_x = sphere_x;
                }
                if (posarm_x > sphere_x)
                {
                    posarm_x -= 0.2f;
                    if (posarm_x == sphere_x)
                        posarm_x = sphere_x;
                }
                if (posarm_z < sphere_z)//Z надо двигать до уровня сферы 
                {
                    posarm_z += 0.2f;
                    if (posarm_z == sphere_z)
                        posarm_z = sphere_z;
                }
                if (posarm_z > sphere_z)
                {
                    posarm_z -= 0.2f;
                    if (posarm_z == sphere_z)
                        posarm_z = sphere_z;
            }
        }
            else
            {
                if (posarm_y<MovetoY)//Y надо двигать до уровня сферы +3
                {
                    posarm_y += 0.2f;
                    if (posarm_y > MovetoY + 2.5f)
                        posarm_y = MovetoY + 2.5f;
                }
                if (posarm_y > MovetoY)
                {
                    posarm_y -= 0.2f;
                    if (posarm_y<MovetoY + 2.5f)
                        posarm_y = MovetoY + 2.5f;
                }
                if (posarm_x<MovetoX)//X надо двигать до уровня сферы
                {
                    posarm_x += 0.2f;
                    if (posarm_x == MovetoX)
                        posarm_x = MovetoX;
                }
                if (posarm_x > MovetoX)
                {
                    posarm_x -= 0.2f;
                    if (posarm_x == MovetoX)
                        posarm_x = MovetoX;
                }
                if (posarm_z<MovetoZ)//Z надо двигать до уровня сферы 
                {
                    posarm_z += 0.2f;
                    if (posarm_z == MovetoZ)
                        posarm_z = MovetoZ;
                }
                if (posarm_z > MovetoZ)
                {
                    posarm_z -= 0.2f;
                    if (posarm_z == MovetoZ)
                        posarm_z = MovetoZ;
                }
            }
        }
        void DrawWalls(OpenGL gl, float x, float y, float z)
        {
           //gl.Color(0.3f, 0.0f, 1.0f);
            gl.PushMatrix();
            //glRotatef(0,0,0,1);
            gl.Scale(x, y, z);
            gl.Begin(OpenGL.GL_QUADS);
            /* Floor */

            //Floor 
            gl.Color(0.0f, 0.0f, 1.0f);
            gl.Vertex(-1, -1, -1);
            gl.Vertex(1, -1, -1);
            gl.Vertex(1, -1, 1);
            gl.Vertex(-1, -1, 1);

            // Ceiling /
            gl.Color(0.7f, 0.0f, 1.0f);
            gl.Vertex(-1, 1, -1);
            gl.Vertex(1, 1, -1);
            gl.Vertex(1, 1, 1);
            gl.Vertex(-1, 1, 1);
            //// Walls
             gl.Color(0.0f, 1.0f, 1.0f);
            gl.Vertex(-1, -1, 1);
            gl.Vertex(1, -1, 1);
            gl.Vertex(1, 1, 1);
            gl.Vertex(-1, 1, 1);
            gl.Color(0.0f, 0.5f, 1.0f);
            gl.Vertex(-1, -1, -1);
            gl.Vertex(1, -1, -1);
            gl.Vertex(1, 1, -1);
            gl.Vertex(-1, 1, -1);
            gl.Color(0.0f, 0,2f, 1.0f);
            gl.Vertex(1, 1, 1);
            gl.Vertex(1, -1, 1);
            gl.Vertex(1, -1, -1);
            gl.Vertex(1, 1, -1);

            gl.Vertex(-1, 1, 1);
            gl.Vertex(-1, -1, 1);
            gl.Vertex(-1, -1, -1);
            gl.Vertex(-1, 1, -1);
            gl.End();

            gl.PopMatrix();

        }
        void Drawsphere(OpenGL gl)
        {
            gl.Color(1.0f, 0.0f, 0.0f);
            //create sphere quadric

            IntPtr quadric = gl.NewQuadric();
            gl.QuadricNormals(quadric, OpenGL.GLU_SMOOTH);
            
            gl.Sphere(quadric,1.5, 50, 50);//radis = 1.5
            gl.DeleteQuadric(quadric);
        }
        void Draw_last_finger(OpenGL gl,float [] x, float [] z)//конец пальца
        {
            gl.Enable(OpenGL.GL_TEXTURE_2D);
            texture1.Create(gl, @"C:\Users\USER\source\repos\robotic arm\robotic arm\Resources\finger_texture.bmp");
            texture1.Bind(gl);
            float[] texture_masX = { 0.0f, 1.0f, 1.0f, 0.0f };
            float[] texture_masY = { 0.0f, 0.0f, 1.0f, 1.0f };
            gl.Color(0.5f, 0.5f, 0.5f);
            gl.Begin(OpenGL.GL_POLYGON);//почемуто не ровно
            for (int i = 0; i < 4; ++i)
            {
                gl.TexCoord(texture_masX[i], texture_masY[i]);
                if (i<2)
                gl.Vertex(x[i], 0.0f, z[i]);
                else
                gl.Vertex(x[i], -0.4f, z[i]);
            }
            gl.End();
            gl.Begin(OpenGL.GL_POLYGON);
            for (int i = 0; i < 4; ++i)
            {
                gl.TexCoord(texture_masX[i], texture_masY[i]);
                gl.Vertex(x[i], -1.0f, z[i]);
            }
            gl.End();

            gl.Begin(OpenGL.GL_QUADS);
            for (int i1 = 0; i1 < 4; i1++)
            {
                int i2 = (i1 + 1) % 4;//следующие координаты после i
                                      //верхние точки
                switch (i1)
                {
                    case 0:
                        gl.TexCoord(texture_masX[0], texture_masY[0]);
                        gl.Vertex(x[i1], 0.0f, z[i1]);
                        gl.TexCoord(texture_masX[1], texture_masY[1]);
                        gl.Vertex(x[i2], 0.0f, z[i2]);
                        break;
                    case 1:
                        gl.TexCoord(texture_masX[0], texture_masY[0]);
                        gl.Vertex(x[i1], 0.0f, z[i1]);
                        gl.TexCoord(texture_masX[1], texture_masY[1]);
                        gl.Vertex(x[i2], -0.4f, z[i2]);
                        break;
                    case 2:
                        gl.TexCoord(texture_masX[0], texture_masY[0]);
                        gl.Vertex(x[i1], -0.4f, z[i1]);
                        gl.TexCoord(texture_masX[1], texture_masY[1]);
                        gl.Vertex(x[i2], -0.4f, z[i2]);
                        break;
                    case 3:
                        gl.TexCoord(texture_masX[0], texture_masY[0]);
                        gl.Vertex(x[i1], -0.4f, z[i1]);
                        gl.TexCoord(texture_masX[1], texture_masY[1]);
                        gl.Vertex(x[i2], 0.0f, z[i2]);
                        break;
                }


                //нижние точки
                gl.TexCoord(texture_masX[2], texture_masY[2]);
                gl.Vertex(x[i2], -1.0f, z[i2]);
                gl.TexCoord(texture_masX[3], texture_masY[3]);
                gl.Vertex(x[i1], -1.0f, z[i1]);
            }
            gl.End();
            gl.Disable(OpenGL.GL_TEXTURE_2D);
        }
        void DrawFinger(OpenGL gl, float [] x, float []z)//палец
        {
            gl.Enable(OpenGL.GL_TEXTURE_2D);
            texture1.Create(gl, @"C:\Users\USER\source\repos\robotic arm\robotic arm\Resources\finger_texture.bmp");
            texture1.Bind(gl);
            float[] texture_masX = { 0.0f, 1.0f, 1.0f, 0.0f };
            float[] texture_masY = { 0.0f, 0.0f, 1.0f, 1.0f };
            gl.Color(0.5f, 0.5f, 0.5f);
            gl.Begin(OpenGL.GL_POLYGON);//почемуто не ровно
            for (int i = 0; i < 4; ++i)
            {
               gl.TexCoord(texture_masX[i], texture_masY[i]);
                gl.Vertex(x[i], 0.0f, z[i]);
            }
            gl.End();
            gl.Begin(OpenGL.GL_POLYGON);
            for (int i = 0; i < 4; ++i)
            {
                gl.TexCoord(texture_masX[i], texture_masY[i]);
                gl.Vertex(x[i], -1.0f, z[i]);
            }
            gl.End();

            gl.Begin(OpenGL.GL_QUADS);
            for (int i1 = 0; i1 < 4; ++i1)
            {
                int i2 = (i1 + 1) % 4;//следующие координаты после i
                                      //верхние точки
                gl.TexCoord(texture_masX[0], texture_masY[0]);
                gl.Vertex(x[i1], 0.0f, z[i1]);
                gl.TexCoord(texture_masX[1], texture_masY[1]);
                gl.Vertex(x[i2], 0.0f, z[i2]);
                //нижние точки
                gl.TexCoord(texture_masX[2], texture_masY[2]);
                gl.Vertex(x[i2], -1.0f, z[i2]);
                gl.TexCoord(texture_masX[3], texture_masY[3]);
                gl.Vertex(x[i1], -1.0f, z[i1]);
            }
            gl.End();
            gl.Disable(OpenGL.GL_TEXTURE_2D);
        }
        
        private void Drawpolygon(OpenGL gl)//ладонь
        {
           // gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT);
            gl.Enable(OpenGL.GL_TEXTURE_2D);
            texture.Create(gl, @"C:\Users\USER\source\repos\robotic arm\robotic arm\Resources\texture.bmp");
            texture.Bind(gl);
            float[] texture_masX = { 0.0f, 1.0f,0.0f,1.0f,1.0f,0.0f,1.0f,0.0f};
            float[] texture_masY = { 0.0f, 0.0f,0.0f,0.0f,1.0f,1.0f,1.0f,1.0f};
            float[] x = { -1.5f, -1.5f, -0.5f, -0.5f, 0.5f, 0.5f, 1.5f, 1.5f };
            float[] z = { 2.0f, -2.0f, -2.0f, -2.5f, -2.5f, -2.0f, -2.0f, 2.0f };
            //front part
            gl.Color(1.0f, 1.0f, 0.0f);
            gl.Begin(OpenGL.GL_POLYGON);//почемуто не ровно
            for (int i = 0; i < 8; ++i)
            {
                gl.TexCoord(texture_masX[i], texture_masY[i]);
                gl.Vertex(x[i], 0.0f, z[i]);
            }
            gl.End();
            gl.Begin(OpenGL.GL_POLYGON);
            for (int i = 0; i < 8; ++i)
            {
                gl.TexCoord(texture_masX[i], texture_masY[i]);
                gl.Vertex(x[i], -1.0f, z[i]);
            }
            gl.End();

            gl.Begin(OpenGL.GL_QUADS);
            for (int i1 = 0; i1 < 8; ++i1)
            {
                int i2 = (i1 + 1) % 8;//следующие координаты после i
                                      //верхние точки
                gl.TexCoord(texture_masX[0], texture_masY[0]);
                gl.Vertex(x[i1], 0.0f, z[i1]);
                gl.TexCoord(texture_masX[1], texture_masY[1]);
                gl.Vertex(x[i2], 0.0f, z[i2]);
                //нижние точки
                gl.TexCoord(texture_masX[4], texture_masY[4]);
                gl.Vertex(x[i2], -1.0f, z[i2]);
                gl.TexCoord(texture_masX[5], texture_masY[5]);
                gl.Vertex(x[i1], -1.0f, z[i1]);
            }
            
            gl.End();
            gl.Disable(OpenGL.GL_TEXTURE_2D);
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
                    rotobjectZ += step;
                    if (rotobjectZ >= 90)
                        rotobjectZ = 90;
                    break;
                case Keys.D2:
                    rotobjectZ -= step;
                    if (rotobjectZ <= -90)
                        rotobjectZ = -90;
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
                        if (rotfing_1_2[0] <= -80)
                        {
                            rotfing_1_2[0] = -80;
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
                    if (rotfing_3_4[0] <= -80)
                    {
                        rotfing_3_4[0] = -80;
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
                    if (rotfing_1_2[1] <= -53)
                    {
                        rotfing_1_2[1] = -53;
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
                    if (rotfing_3_4[1] <= -53)
                    {
                        rotfing_3_4[1] = -53;
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
                    if (rotsidefing[1] <= -35)
                    {
                        rotsidefing[1] = -38;
                    }
                    break;
                case Keys.Back:
                    rotobjectZ = 0;
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
                case Keys.O:
                 
                    rotsidefing[1] -= step;
                    if (rotsidefing[1] <= -38)
                    {
                        rotsidefing[1] = -38;
                    }
                    rotfing_3_4[1] -= step;
                    if (rotfing_3_4[1] <= -53)
                    {
                        rotfing_3_4[1] = -53;
                    }
                    rotfing_1_2[1] -= step;
                    if (rotfing_1_2[1] <= -53)
                    {
                        rotfing_1_2[1] = -53;
                    }
                    rotfing_1_2[0] -= step;
                    if (rotfing_1_2[0] <= -80)
                    {
                        rotfing_1_2[0] = -80;
                    }
                    rotfing_3_4[0] -= step;
                    if (rotfing_3_4[0] <= -80)
                    {
                        rotfing_3_4[0] = -80;
                    }
                    rotsidefing[0] -= step;
                    if (rotsidefing[0] <= -100)
                    {
                        rotsidefing[0] = -100;
                    }
                    taken = true;
                    break;

                case Keys.P:
                    rotfing_1_2[0] += step;
                    if (rotfing_1_2[0] >= 0)
                    {
                        rotfing_1_2[0] = 0;
                    }
                    rotfing_3_4[0] += step;
                    if (rotfing_3_4[0] >= 0)
                    {
                        rotfing_3_4[0] = 0;
                    }
            
                    rotsidefing[0] += step;
                    if (rotsidefing[0] >= 0)
                    {
                        rotsidefing[0] = 0;
                    }
                    rotfing_1_2[1] += step;
                    if (rotfing_1_2[1] >= 0)
                    {
                        rotfing_1_2[1] = 0;
                    }
                    rotfing_3_4[1] += step;
                    if (rotfing_3_4[1] >= 0)
                    {
                        rotfing_3_4[1] = 0;
                    }
                    rotsidefing[1] += step;
                    if (rotsidefing[1] >= 0)
                    {
                        rotsidefing[1] = 0;
                    }
                    taken = false;
                   
                    break;
            }
        }
        float MovetoX = 0.0f;
        float MovetoY = 0.0f;
        float MovetoZ = 0.0f;
        bool move = false;
        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                
                MovetoX = (float)Convert.ToDouble(Xmove.Text);
                MovetoY = (float)Convert.ToDouble(Ymove.Text);
                MovetoZ = (float)Convert.ToDouble(Zmove.Text);
                if (taken == true)
                {
                    movearm = false;
                    move = true;
                }
                else
                {
                    move = false;
                    movearm = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        bool movearm = false;
        private void button2_Click(object sender, EventArgs e)
        {
            movearm = true;
        }
    }


}

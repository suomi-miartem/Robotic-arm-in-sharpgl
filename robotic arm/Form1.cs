using System;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using OpenCvSharp;

namespace robotic_arm
{
    public partial class Form1 : Form
    {
        Mat depthMap,threshold,mask;

        Window window;

        Point[][] contours;

        double[,] ranges = new double[5, 2] //массив диапазонов
        {
            { 800,4000},
            {400,2000 },
            {200,1000 },
            {100,500 },
            {50,250 }
        };

        double vAngle = 43, hAngle = 57; //Углы обзора

        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files\\");

        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            filePathCB.Items.Clear();
            DirectoryInfo dir = new DirectoryInfo(path);
            foreach (var item in dir.GetFiles())
            {
                filePathCB.Items.Add(item.Name);
            }
        }

        //Обработка изображения
        //Пороговая фильтрация ближайших по глубине пикселей
        //Вывод наименьшего контура (не стола)
        //Получение 3D координат точек внутри контура
        //Определение размеров объекта и его положения в системе координат камеры
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //Разблокировка доступных на данном этапе функций
                numThresholdMin.Enabled = true;
                numThresholdMax.Enabled = true;
                
                window = new Window("Window");

                //Чтение в глобальную матрицу и предварительная обработка
                depthMap = Cv2.ImRead(Path.Combine(path, filePathCB.SelectedItem.ToString()));

                depthMap = depthMap.CvtColor(ColorConversionCodes.BGR2GRAY);

                window.ShowImage(depthMap);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Настройка верхнего порога преобразования
        private void numThresholdMin_ValueChanged(object sender, EventArgs e)
        {
            numOfContCB.Enabled = false;
            threshold = depthMap.Clone().InRange(new Scalar((double)numThresholdMin.Value), new Scalar((double)numThresholdMax.Value)).Clone().Erode(new Mat()).Dilate(new Mat());
            Mat output = Mat.Zeros(threshold.Size(), threshold.Type());
            Cv2.BitwiseAnd(depthMap, threshold, output);
            window.ShowImage(output);
            output.Dispose();
        }

        //Настройка нижнего порога преобразования
        private void numThresholdMax_ValueChanged(object sender, EventArgs e)
        {
            numOfContCB.Enabled = false;
            threshold = depthMap.Clone().InRange(new Scalar((double)numThresholdMin.Value), new Scalar((double)numThresholdMax.Value)).Clone().Erode(new Mat()).Dilate(new Mat());
            Mat output = Mat.Zeros(threshold.Size(), threshold.Type());
            Cv2.BitwiseAnd(depthMap, threshold, output);
            window.ShowImage(output);
            output.Dispose();
        }

        //Поиск контуров
        private void findContoursBN_Click(object sender, EventArgs e)
        {
            if (filePathCB.SelectedIndex < 0) return;

            numOfContCB.Enabled = true;
            comChosenContBN.Enabled = true;
            numOfContCB.Items.Clear();

            HierarchyIndex[] HI;

            threshold.FindContours(out contours, out HI, RetrievalModes.List, ContourApproximationModes.ApproxSimple);

            for (int i = 0; i < contours.GetLength(0); i++)
            {
                numOfContCB.Items.Add(i);
            }
        }

        //Выбор контура из списка обнаруженных
        private void numOfContCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            Mat output = Mat.Zeros(threshold.Size(), threshold.Type());
            mask = Mat.Zeros(threshold.Size(), threshold.Type());

            mask.DrawContours(contours, numOfContCB.SelectedIndex, Scalar.White, -1);

            Cv2.BitwiseAnd(depthMap, mask, output);

            window.ShowImage(output);

            output.Dispose();
        }

        //Сравнение маски с картой глубины и расчет размеров и позиции объекта
        private void comChosenContBN_Click(object sender, EventArgs e)
        {
            try
            {
                if (numOfContCB.SelectedIndex >= 0 && dividerCB.SelectedIndex >= 0)
                {
                    //Выбранный диапазон
                    double min = ranges[dividerCB.SelectedIndex, 0];
                    double max = ranges[dividerCB.SelectedIndex, 1];

                    Rect rect = Cv2.BoundingRect(contours[numOfContCB.SelectedIndex].ToArray());//Определение рамки маски

                    Point[] shape = FindShape(rect);

                    //Расчет размеров и координат в пространстве
                    double[] vector1 = FindCoords(shape[0].X, shape[0].Y, new double[2] { min, max }); //Векторы крайних точек
                    double[] vector2 = FindCoords(shape[1].X, shape[1].Y, new double[2] { min, max });

                    double lX2 = (vector1[0] - vector2[0]) * (vector1[0] - vector2[0]);
                    double lY2 = (vector1[1] - vector2[1]) * (vector1[1] - vector2[1]);
                    double lZ2 = (vector1[2] - vector2[2]) * (vector1[2] - vector2[2]);

                    double radius = Math.Sqrt(lX2 + lY2 + lZ2)/2;//Диаметр сферы

                    double cX = vector2[0] + (vector1[0] - vector2[0]) / 2;
                    double cY = vector2[1] + (vector1[1] - vector2[1]) / 2;
                    double cZ = vector2[2] + (vector1[2] - vector2[2]) / 2;

                    double xMax = max * Math.Sin((hAngle / 2) * (Math.PI / 180));
                    double yMax = max * Math.Sin((vAngle / 2) * (Math.PI / 180));

                    Form2 form = new Form2((float)cX, (float)cY, (float)cZ, (float)radius, (float)xMax, (float)yMax, (float)max);
                    //form.Show();
                    infoLBL.Text = $"X = {(int)cX};\r\n" +
                        $"Y= {(int)cY};\r\n" +
                        $"Z={(int)cZ};\r\n" +
                        $"Radius = {(int)radius}";
                }
                else
                {
                    throw new Exception("Не все настройки определены");
                }
            }
             catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Поиск крайних точек объекта сверху и снизу
        Point[] FindShape(Rect rect)
        {
            bool topFound = false, bottomFound = false;

            Point top = new Point(), bottom = new Point();

            int i = 0;

            while(!topFound || !bottomFound)
            {
                if(depthMap.At<byte>(rect.Left + rect.Width/2,rect.Top+i) > 0 && !topFound)
                {
                    top = new Point(rect.Left + rect.Width / 2, rect.Top + i);

                    topFound = true;
                }

                if (depthMap.At<byte>(rect.Left + rect.Width / 2, rect.Bottom - i) > 0 && !bottomFound)
                {
                    bottom = new Point(rect.Left + rect.Width / 2, rect.Bottom - i);

                    bottomFound = true;
                }

                i++;
            }

            return new Point[2] { top, bottom };
        }

        //Возвращает координату точки в 3D пространстве с учетом параметров делителя и углов обзора камеры
        private double[] FindCoords(int x, int y, double[] range)
        {
            double intensity = (byte)(255 - depthMap.At<byte>(y, x));

            //Длина вектора = (шаг*разность+минимум)
            double vector = ((range[1] - range[0]) / 255) * intensity + range[0];

            double halfAngle = hAngle / 2, convertPix = x - depthMap.Width / 2, halfSize = depthMap.Width / 2;

            //Угол горизонтального отклонения от оси Z(в градусах)
            double horizontal = Math.Abs(halfAngle * (convertPix / halfSize));

            halfAngle = vAngle / 2; convertPix = y - depthMap.Height / 2; halfSize = depthMap.Height / 2;

            //Угол вертикального отклонения от оси Z(в градусах)
            double vertical = Math.Abs(halfAngle * (convertPix / halfSize));

            //квадраты горизонтальных/вертикальных тангенсов
            double tg2A = Math.Tan(horizontal*(Math.PI/180)) * Math.Tan(horizontal * (Math.PI / 180));
            double tg2B = Math.Tan(vertical * (Math.PI / 180)) * Math.Tan(vertical * (Math.PI / 180));

            //Расчет проекций вектора на базисы с учетом знака
            double Z = Math.Sqrt((vector * vector) / (tg2A + tg2B + 1));
            double X = Z * Math.Tan(horizontal * (Math.PI / 180)) * (x - depthMap.Width / 2) / Math.Abs(x - depthMap.Width / 2);
            double Y = Z * Math.Tan(vertical * (Math.PI / 180)) * (y - depthMap.Height / 2) / Math.Abs(y - depthMap.Height / 2);

            return new double[3] { X, Y, Z };
        }
    }
}

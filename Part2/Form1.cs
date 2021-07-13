using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Drawing.Drawing2D;

namespace Part2
{
    public partial class Form1 : Form
    {
        int osX = 100, osY = 175;
        int timeToSleep = 120;
        double InitT = 0.05, LastT = 3.1; // оборот в 360 градусов (6,28 радиан)
        double Step = 0.05, curPoint;
        int scale = 50;
        int pointsToDraw;
        int direction = 0;

        int a = 50, h = 20; double angle = 2;

        //draw styles
        Color traektColor, borderColor, fillColor; //buttons 2, 3, 4
        DashStyle styleGR = DashStyle.Solid, stylePr = DashStyle.Solid;

        //puls
        int pulsStep = 1, puls;
        bool Increase = true;


        public Form1()
        {
            InitializeComponent();            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            button2.BackColor = colorDialog1.Color;
            button3.BackColor = colorDialog2.Color;
            button4.BackColor = colorDialog3.Color;
            traektColor = colorDialog1.Color;
            borderColor = colorDialog2.Color;
            fillColor = colorDialog3.Color;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pointsToDraw = (int)Math.Round((LastT - InitT) / Step) + 1;
            Step = (double)numericUpDown5.Value;
            InitT = (double)numericUpDown6.Value;
            LastT = (double)numericUpDown7.Value;
            a = (int)(numericUpDown9.Value * scale);
            h = (int)(numericUpDown10.Value * scale);
            angle = (double)numericUpDown11.Value;
            puls = (int)numericUpDown8.Value;
            pulsStep = 1; Increase = true;

            for (int i = 0; i < numericUpDown4.Value; i++)
            switch (direction)
            {
                case 0: Paint_MoveForward(); break;
                case 1: Paint_MoveBack(); break;
                case 2: Paint_MoveForward(); Paint_MoveBack(); break;
            }
        }

        private void Paint_Osi(int x0, int y0) 
        { 
            Graphics gr = pictureBox1.CreateGraphics();
            gr.DrawLine(Pens.Black, x0, 0, x0, 1000);
            gr.DrawLine(Pens.Gray, 0, y0, 1000, y0); 
        }

        private void Paint_Parallelogr(Point center)
        {
            Graphics gr = pictureBox1.CreateGraphics();
            Point[] points = new Point[4];
            double b = h * Math.Tan(-1.57 + angle);
            Point startPoint = new Point(center.X - (int)((a - h * Math.Tan(-1.57 + angle))/2), center.Y - h / 2);

            points[0] = startPoint;
            points[1] = new Point(startPoint.X + a, startPoint.Y);
            points[2] = new Point(startPoint.X - (int)b + a, startPoint.Y + h);
            points[3] = new Point(startPoint.X - (int)b, startPoint.Y + h);

            Pen border = new Pen(borderColor);
            border.DashStyle = stylePr;
            border.Width = (float)numericUpDown2.Value/2;
            SolidBrush inside = new SolidBrush(fillColor);
            gr.FillPolygon(inside, points);
            gr.DrawPolygon(border, points);

        }

        private void Paint_FullGraphic()
        {
            Graphics gr = pictureBox1.CreateGraphics();
            gr.Clear(BackColor);
            Paint_Osi(osX, osY);
            PointF[] p = new PointF[pointsToDraw];
            double cur = InitT;
            for (int i = 0; i < pointsToDraw; i++)
            {
                double x = cur * scale;
                double y = (1.0 / Math.Tan(cur)) * scale;
                p[i] = new PointF(osX + (int)x, osY - (int)y); // расчет очередной точки траектории
                cur += Step;
                if (cur > LastT) break;
            }
            Pen pen = new Pen(traektColor);
            pen.DashStyle = styleGR;
            pen.Width = (float)numericUpDown1.Value / 2;
            gr.DrawLines(pen, p); // траектория
        }


        private void Paint_Graphic(PointF[] p)
        {
            Graphics gr = pictureBox1.CreateGraphics();
            gr.Clear(BackColor);
            Paint_Osi(osX, osY);
            Pen pen = new Pen(traektColor);
            pen.DashStyle = styleGR;
            pen.Width = (float)numericUpDown1.Value/2;
            gr.DrawLines(pen, p); // траектория
        }


        private void Paint_MoveForward()
        {
            //set parameters
            double x, y;
            int i = 0; // количество точек прорисовки
            pointsToDraw = (int)Math.Round((LastT - InitT) / Step) + 1;
            PointF[] p = new PointF[pointsToDraw]; // точки для прорисовки (LastT/Step)

            curPoint = InitT;
            int aInit = a, hInit = h;

            //draw
            while (curPoint <= LastT)
            {
                x = curPoint * scale;
                y = (1.0 / Math.Tan(curPoint)) * scale;
                p[i] = new PointF(osX + (int)x, osY - (int)y); // расчет очередной точки траектории
                Paint_FullGraphic();
                //Paint_Graphic(p);
                Pulse();
                Point point = new Point((int)p[i].X, (int)p[i].Y); 
                Paint_Parallelogr(point);
                curPoint += Step;
                Thread.Sleep(timeToSleep/(int)numericUpDown3.Value); //время приостановки прорисовки
                i++;
                
            }
        }

        private void Pulse()
        {
            //парам пам пам пульсация
            if (puls != 1)
            {
                if (Increase)
                {
                    a += 10; h += 10;
                    pulsStep++;
                }
                else
                {
                    a -= 10; h -= 10;
                    pulsStep--;
                }
                if (pulsStep > puls) Increase = false;
                else if (pulsStep == 1) Increase = true;
            }
        }

        private void Paint_MoveBack()
        {
            //set parameters
            double x, y;
            int i = 0; // количество точек прорисовки
            PointF[] p = new PointF[pointsToDraw]; // точки для прорисовки (LastT/Step)

             curPoint = LastT;
            int aInit = a, hInit = h;

            //draw
            while (curPoint >= InitT)
            {
                x = curPoint * scale;
                y = (1.0 / Math.Tan(curPoint)) * scale;
                p[i] = new PointF(osX + (int)x, osY - (int)y); // расчет очередной точки траектории
                Paint_FullGraphic();
                //Paint_Graphic(p);
                Pulse();
                Point point = new Point((int)p[i].X, (int)p[i].Y);
                Paint_Parallelogr(point);
                curPoint -= Step;
                Thread.Sleep(timeToSleep / (int)numericUpDown3.Value); //время приостановки прорисовки
                i++;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //вид линии траектории
            styleGR = (DashStyle)Enum.ToObject(typeof(DashStyle), comboBox1.SelectedIndex);
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            //вид линии объекта
            stylePr = (DashStyle)Enum.ToObject(typeof(DashStyle), comboBox4.SelectedIndex);
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //направление 0 - вперед, 1 - назад, 2 - туда обратно
            direction = comboBox2.SelectedIndex;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button2.BackColor = colorDialog1.Color;
                traektColor = colorDialog1.Color;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (colorDialog2.ShowDialog() == DialogResult.OK)
            {
                if (colorDialog3.Color != colorDialog2.Color)
                {
                    button3.BackColor = colorDialog2.Color;
                    borderColor = colorDialog2.Color;
                }
                else
                {
                    MessageBox.Show("Цвета контура и заливки должны отличаться", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (colorDialog3.ShowDialog() == DialogResult.OK)
            {
                if (colorDialog3.Color != colorDialog2.Color)
                {
                    button4.BackColor = colorDialog3.Color;
                    fillColor = colorDialog3.Color;
                }
                else MessageBox.Show("Цвета контура и заливки должны отличаться", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}

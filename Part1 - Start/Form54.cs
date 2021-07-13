using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Part1___Start
{
    public partial class Form54 : Form
    {
        public Form54()
        {
            InitializeComponent();
        }

        private void Form54_Load(object sender, EventArgs e)
        {
            Text = "Закрашивание фигур"; label1.Text = "Выберите фигуру"; 
            comboBox1.Text = "Фигуры"; 
            string[] figures = new string []{ "Прямоугольник","Эллипс","Окружность"}; 
            comboBox1.Items.AddRange(figures);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Graphics gr = pictureBox1.CreateGraphics(); 
            Brush br = new SolidBrush(Color.Orange); 
            gr.Clear(SystemColors.Control);
            switch (comboBox1.SelectedIndex)
            {
                case 0: gr.FillRectangle(br, 60, 60, 120, 180); break;
                case 1: gr.FillEllipse(br, 60, 60, 120, 180); break;
                case 2: gr.FillEllipse(br, 60, 60, 120, 120); break;
            }
        }
    }
}

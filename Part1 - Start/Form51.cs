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
    public partial class Form51 : Form
    {
        int[] m_p = new int[5];
        int index;
        public Form51()
        {
            InitializeComponent();
        }

        private void Form51_Load(object sender, EventArgs e)
        {
            this.Text = "Рисование эллипса"; label1.Text = "Введите данные"; button1.Text = "Рисовать";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_p[1] = Convert.ToInt32(textBox1.Text);
            m_p[2] = Convert.ToInt32(textBox2.Text);
            m_p[3] = Convert.ToInt32(textBox3.Text); 
            m_p[4] = Convert.ToInt32(textBox4.Text);
            index = 1;
            pictureBox1.Refresh();
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            // Рисуем линию
            if (index == 1)
            {
                Pen pen = new Pen(Color.Black); 
                e.Graphics.DrawEllipse(pen, m_p[1], m_p[2], m_p[3], m_p[4]);
            }
        }

            private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}

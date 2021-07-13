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
    public partial class Form32 : Form
    {
        public Form32()
        {
            InitializeComponent();
        }

        private void Form32_Load(object sender, EventArgs e)
        {
            this.Text = "Фото галерея"; 
            label1.Text = ""; 
            comboBox1.Text = "Список";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0: pictureBox1.Image = Image.FromFile("d:\\7.0.png"); label1.Text = "Коты"; break;
                case 1: pictureBox1.Image = Image.FromFile("d:\\7.1.png"); label1.Text = "Коала"; break;
                case 2: pictureBox1.Image = Image.FromFile("d:\\7.2.png"); label1.Text = "Море"; break;
                case 3: pictureBox1.Image = Image.FromFile("d:\\7.3.png"); label1.Text = "Горы"; break;
            }
        }
    }
}

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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            this.Text = "Доска объявлений"; 
            label1.Text = "";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            label1.Text = textBox1.Text;
        }
    }
}

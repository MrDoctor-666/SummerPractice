using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Part1___Start
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        private void Form4_Load(object sender, EventArgs e)
        {
            this.Text = "Текстовый редактор"; 
            openFileDialog1.FileName = "D:\\Text2.txt"; 
            openFileDialog1.Filter = "Текстовые файлы (*.txt)|*.txt|All files (*.*)|*.*"; 
            saveFileDialog1.Filter = "Текстовые файлы (*.txt)|*.txt|All files (*.*)|*.*";
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName == null) return;
            try
            {
                string text = File.ReadAllText(openFileDialog1.FileName);
                textBox1.Text = text;
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message + "\nФайл не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = openFileDialog1.FileName; 
            if (saveFileDialog1.ShowDialog() ==  DialogResult.OK) Save();
        }

        private void Save()
        {
            try
            {
                File.WriteAllText(saveFileDialog1.FileName, textBox1.Text);
                textBox1.Modified = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (textBox1.Modified == false) return;
            var MeBox = MessageBox.Show("Текст был изменѐн. \nСохранить изменения?", "Простой редактор", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
            if (MeBox == DialogResult.No) return;
            if (MeBox == DialogResult.Cancel) e.Cancel = true;
            if (MeBox == DialogResult.Yes)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK) { Save(); return; }
                else e.Cancel = true;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pcbaoi
{
    public partial class Addelement : Form
    {
        public Addelement(Point point)
        {
            InitializeComponent();
            textBox2.Text = point.X.ToString();
            textBox3.Text = point.Y.ToString();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Element element = new Element();
            element.Name = textBox1.Text;
            element.X = Convert.ToInt32(textBox2.Text);
            element.Y = Convert.ToInt32(textBox3.Text);
            element.Angle = Convert.ToInt32(comboBox1.SelectedItem);
            element.Banid = textBox5.Text;
            element.Elementsdb = textBox6.Text;
            element.Elementype = comboBox2.SelectedItem.ToString();
            this.Tag = element;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pcbaoi
{
    public partial class UserControl3 : UserControl
    {
        Operatorselect operatorselect = new Operatorselect();
        public UserControl3(object op)
        {
            InitializeComponent();
            operatorselect = (Operatorselect)op;

            comboBox1.SelectedIndex = 0;
            if (operatorselect.Percentageup > 0)
            {
                textBox1.Text = operatorselect.Percentageup.ToString();
                textBox2.Text = operatorselect.Percentagedown.ToString();
                comboBox1.SelectedItem = operatorselect.Codetype;
            }
            operatorselect.Percentageup = Convert.ToInt32(textBox1.Text);
            operatorselect.Percentagedown = Convert.ToInt32(textBox2.Text);
            operatorselect.Codetype = comboBox1.SelectedItem.ToString();
            this.Tag = operatorselect;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "") {
                operatorselect.Percentageup = Convert.ToInt32(textBox1.Text);
                operatorselect.Percentagedown = Convert.ToInt32(textBox2.Text);
                operatorselect.Codetype = comboBox1.SelectedItem.ToString();
                if (MyEvent != null)
                {
                    MyEvent(operatorselect, e);

                }
            }


        }

        //定义委托
        public delegate void MyEventHandle(object sender, EventArgs e);
        //定义事件
        public event MyEventHandle MyEvent;

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                operatorselect.Percentageup = Convert.ToInt32(textBox1.Text);
                operatorselect.Percentagedown = Convert.ToInt32(textBox2.Text);
                operatorselect.Codetype = comboBox1.SelectedItem.ToString();
                if (MyEvent != null)
                {
                    MyEvent(operatorselect, e);

                }
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            operatorselect.Percentageup = Convert.ToInt32(textBox1.Text);
            operatorselect.Percentagedown = Convert.ToInt32(textBox2.Text);
            operatorselect.Codetype = comboBox1.SelectedItem.ToString();
            if (MyEvent != null)
            {
                MyEvent(operatorselect, e);
            }

        }
    }
}

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
    public partial class UserControl2 : UserControl
    {
        Operatorselect operatorselect = new Operatorselect();
        public UserControl2(object op)
        {
            InitializeComponent();
            operatorselect = (Operatorselect)op;
            if (operatorselect.Percentageup > 0) {
                textBox1.Text = operatorselect.Percentageup.ToString();
                textBox2.Text = operatorselect.Percentagedown.ToString();

            
            }
            operatorselect.Percentageup = Convert.ToInt32(textBox1.Text);
            operatorselect.Percentagedown = Convert.ToInt32(textBox2.Text);
            this.Tag = operatorselect;

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
            if (textBox1.Text!=""&& Convert.ToInt32(textBox1.Text) > 100) {
                textBox1.Text = "100";
                textBox1.Select(textBox1.Text.Length,0);
            
            
            }

        }


        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
            else
            {

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text!="") {
                if ( Convert.ToInt32(textBox1.Text) > 100)
                {
                    textBox1.Text = "100";
                    textBox1.Select(textBox1.Text.Length, 0);


                }
                operatorselect.Percentageup = Convert.ToInt32(textBox1.Text);
                operatorselect.Percentagedown = Convert.ToInt32(textBox2.Text);
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
            if (textBox2.Text != "")
            {
                operatorselect.Percentageup = Convert.ToInt32(textBox1.Text);
                operatorselect.Percentagedown = Convert.ToInt32(textBox2.Text);
                if (MyEvent != null)
                {
                    MyEvent(operatorselect, e);

                }


            }

        }
    }
}

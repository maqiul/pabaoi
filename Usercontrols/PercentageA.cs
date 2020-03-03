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
    public partial class PercentageA : UserControl
    {
        OperatorSelect operatorselect = new OperatorSelect();
        public PercentageA(object op)
        {
            InitializeComponent();
            operatorselect = (OperatorSelect)op;
            if (operatorselect.Percentageup > 0) {
                Percentageuptx.Text = operatorselect.Percentageup.ToString();
                Percentagedowntx.Text = operatorselect.Percentagedown.ToString();

            
            }
            operatorselect.Percentageup = Convert.ToInt32(Percentageuptx.Text);
            operatorselect.Percentagedown = Convert.ToInt32(Percentagedowntx.Text);
            this.Tag = operatorselect;

        }

        private void Percentageuptx_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
            if (Percentageuptx.Text!=""&& Convert.ToInt32(Percentageuptx.Text) > 100) {
                Percentageuptx.Text = "100";
                Percentageuptx.Select(Percentageuptx.Text.Length,0);
            
            
            }

        }


        private void Percentagedowntx_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
            else
            {

            }
        }

        private void Percentageuptx_TextChanged(object sender, EventArgs e)
        {
            if (Percentageuptx.Text!="") {
                if ( Convert.ToInt32(Percentageuptx.Text) > 100)
                {
                    Percentageuptx.Text = "100";
                    Percentageuptx.Select(Percentageuptx.Text.Length, 0);


                }
                operatorselect.Percentageup = Convert.ToInt32(Percentageuptx.Text);
                operatorselect.Percentagedown = Convert.ToInt32(Percentagedowntx.Text);
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

        private void Percentagedowntx_TextChanged(object sender, EventArgs e)
        {
            if (Percentagedowntx.Text != "")
            {
                operatorselect.Percentageup = Convert.ToInt32(Percentageuptx.Text);
                operatorselect.Percentagedown = Convert.ToInt32(Percentagedowntx.Text);
                if (MyEvent != null)
                {
                    MyEvent(operatorselect, e);

                }


            }

        }
    }
}

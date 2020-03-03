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
    public partial class Code : UserControl
    {
        OperatorSelect operatorselect = new OperatorSelect();
        public Code(object op)
        {
            InitializeComponent();
            operatorselect = (OperatorSelect)op;

            Codetypecm.SelectedIndex = 0;
            if (operatorselect.Percentageup > 0)
            {
                PercentageOntx.Text = operatorselect.Percentageup.ToString();
                PercentageDowntx.Text = operatorselect.Percentagedown.ToString();
                Codetypecm.SelectedItem = operatorselect.Codetype;
            }
            operatorselect.Percentageup = Convert.ToInt32(PercentageOntx.Text);
            operatorselect.Percentagedown = Convert.ToInt32(PercentageDowntx.Text);
            operatorselect.Codetype = Codetypecm.SelectedItem.ToString();
            this.Tag = operatorselect;
        }

        private void PercentageOntx_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }

        }

        private void PercentageDowntx_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (PercentageOntx.Text != "") {
                operatorselect.Percentageup = Convert.ToInt32(PercentageOntx.Text);
                operatorselect.Percentagedown = Convert.ToInt32(PercentageDowntx.Text);
                operatorselect.Codetype = Codetypecm.SelectedItem.ToString();
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
            if (PercentageOntx.Text != "")
            {
                operatorselect.Percentageup = Convert.ToInt32(PercentageOntx.Text);
                operatorselect.Percentagedown = Convert.ToInt32(PercentageDowntx.Text);
                operatorselect.Codetype = Codetypecm.SelectedItem.ToString();
                if (MyEvent != null)
                {
                    MyEvent(operatorselect, e);

                }
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            operatorselect.Percentageup = Convert.ToInt32(PercentageOntx.Text);
            operatorselect.Percentagedown = Convert.ToInt32(PercentageDowntx.Text);
            operatorselect.Codetype = Codetypecm.SelectedItem.ToString();
            if (MyEvent != null)
            {
                MyEvent(operatorselect, e);
            }

        }
    }
}

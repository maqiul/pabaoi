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
    public partial class Aiadd : UserControl
    {
        Operatorselect operatorselect;
        public Aiadd(object op)
        {
            InitializeComponent();
            operatorselect = (Operatorselect)op;
            if (operatorselect.Confidence > 0) {
                textBox2.Text = operatorselect.Confidence.ToString();
                       
            }
            this.Tag = textBox2.Text;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是退格和数字，则屏蔽输入
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            {
                e.Handled = true;
            }


        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text!="") {
                if (Convert.ToDouble(textBox2.Text) >= 1)
                {
                    MessageBox.Show("请输入大于0或者小于1的数字");
                    textBox2.Text = "0.99";
                }
                if (MyEvent != null) {
                    MyEvent(textBox2.Text,e);
                
                }
            }


            
        }
        //定义委托
        public delegate void MyEventHandle(object sender, EventArgs e);
        //定义事件
        public event MyEventHandle MyEvent;
    }
}

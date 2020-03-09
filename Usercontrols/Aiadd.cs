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

        OperatorSelect operatorselect;
        public Aiadd(object op)
        {
            InitializeComponent();
            operatorselect = (OperatorSelect)op;
            if (operatorselect.Confidence!= null&& float.Parse(operatorselect.Confidence) > 0) {
                Confidencetx.Text = operatorselect.Confidence.ToString();
                       
            }
            this.Tag = Confidencetx.Text;
        }

        private void Confidencetx_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是退格和数字，则屏蔽输入
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            {
                e.Handled = true;
            }


        }

        private void Confidencetx_TextChanged(object sender, EventArgs e)
        {
            if (Confidencetx.Text!="") {
                if (Convert.ToDouble(Confidencetx.Text) >= 1)
                {
                    MessageBox.Show("请输入大于0或者小于1的数字");
                    Confidencetx.Text = "0.99";
                }
                if (MyEvent != null) {
                    MyEvent(Confidencetx.Text,e);
                
                }
            }


            
        }
        //定义委托
        public delegate void MyEventHandle(object sender, EventArgs e);
        //定义事件
        public event MyEventHandle MyEvent;
    }
}

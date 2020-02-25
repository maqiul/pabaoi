using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using pcbaoi.Properties;

namespace pcbaoi.usercontrol
{
    public partial class Savecontrol : UserControl
    {
        string path;
        FileHandler filehandler;
        public Savecontrol()
        {
            InitializeComponent();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是退格和数字，则屏蔽输入
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是退格和数字，则屏蔽输入
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            {
                e.Handled = true;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是退格和数字，则屏蔽输入
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            {
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public delegate void MyEventHandle(object sender, EventArgs e);
        //定义事件
        public event MyEventHandle MyEvent;
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || textBox7.Text == "")
            {

                MessageBox.Show("项目信息不能为空");

            }
            else {
                ProjectSetting projectSetting = new ProjectSetting();
                projectSetting.Name = textBox1.Text;
                projectSetting.Cname = textBox2.Text;
                projectSetting.Carrierplatewidth = Convert.ToInt32(textBox7.Text);
                projectSetting.Carrierplateheight = Convert.ToInt32(textBox6.Text);
                projectSetting.Width = Convert.ToInt32(textBox3.Text);
                projectSetting.Height = Convert.ToInt32(textBox4.Text);
                projectSetting.Nip = Convert.ToInt32(textBox5.Text);
                if (MyEvent != null)
                {
                    MyEvent(projectSetting, e);
                }
            }

        }
    }
}

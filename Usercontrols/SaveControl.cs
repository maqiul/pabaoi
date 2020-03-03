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

        private void Pcbwidthtx_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是退格和数字，则屏蔽输入
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            {
                e.Handled = true;
            }
        }

        private void Pcbheighttx_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是退格和数字，则屏蔽输入
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            {
                e.Handled = true;
            }
        }

        private void Niptx_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是退格和数字，则屏蔽输入
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            {
                e.Handled = true;
            }
        }

        private void Canelbt_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public delegate void MyEventHandle(object sender, EventArgs e);
        //定义事件
        public event MyEventHandle MyEvent;
        private void Okbt_Click(object sender, EventArgs e)
        {
            if (Pcbnametx.Text == "" || textBox2.Text == "" || Pcbwidthtx.Text == "" || Pcbheighttx.Text == "" || Niptx.Text == "" || Carrierplateheighttx.Text == "" || Carrierplatewidthtx.Text == "")
            {

                MessageBox.Show("项目信息不能为空");

            }
            else {
                ProjectSetting projectSetting = new ProjectSetting();
                projectSetting.Name = Pcbnametx.Text;
                projectSetting.Cname = textBox2.Text;
                projectSetting.Carrierplatewidth = Convert.ToInt32(Carrierplatewidthtx.Text);
                projectSetting.Carrierplateheight = Convert.ToInt32(Carrierplateheighttx.Text);
                projectSetting.Width = Convert.ToInt32(Pcbwidthtx.Text);
                projectSetting.Height = Convert.ToInt32(Pcbheighttx.Text);
                projectSetting.Nip = Convert.ToInt32(Niptx.Text);
                if (MyEvent != null)
                {
                    MyEvent(projectSetting, e);
                }
            }

        }

        private void Carrierplatewidthtx_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是退格和数字，则屏蔽输入
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            {
                e.Handled = true;
            }
        }

        private void Carrierplateheighttx_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是退格和数字，则屏蔽输入
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            {
                e.Handled = true;
            }
        }
    }
}

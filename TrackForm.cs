using QTing.PLC;
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
    public partial class TrackForm : Form
    {
        public TrackForm()
        {
            InitializeComponent();
            conn();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
            else
            {

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

        private void Canelbt_Click(object sender, EventArgs e)
        {
            PLCController.Instance.CloseConnection();
            this.Close();
        }

        private void Widthtest_Click(object sender, EventArgs e)
        {
            double value =  Convert.ToDouble(Widthtextbox.Text);
            int registerAddress = 2124;
            byte[] receiveData = new byte[255];
            if (value > 0xffffffff)
            {
                MessageBox.Show("超出设置范围");
                return;
            }
            byte[] writeValue = new byte[4];
            value = value * 1000;
            writeValue[0] = (byte)((value % Math.Pow(256, 2)) / 256);
            writeValue[1] = (byte)((value % Math.Pow(256, 2)) % 256);
            writeValue[2] = (byte)(value / Math.Pow(256, 3));
            writeValue[3] = (byte)((value / Math.Pow(256, 2)) % 256);
            if (PLCController.Instance.IsConnected)
                PLCController.Instance.WriteData(registerAddress, 2, writeValue, receiveData);

        }
        private void conn() {

            if (PLCController.Instance.Connection(IniFile.iniRead("PLC", "ip"), Convert.ToInt32(IniFile.iniRead("PLC", "port"))))
                Console.WriteLine("连接成功");
            else
                MessageBox.Show("连接失败");

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}

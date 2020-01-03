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
    public partial class LightsourceForm : Form
    {
        private int D2000 = 0;
        public LightsourceForm()
        {
            InitializeComponent();
            panel1_Click(lefttitlebt,null);
            conn();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            righttitlebt.BackColor = Color.Gray;
            blowbt.Hide();
            Panel panel = (Panel)sender;
            panel.BackColor = Color.White;

        }

        private void panel2_Click(object sender, EventArgs e)
        {
            lefttitlebt.BackColor = Color.Gray;
            blowbt.Show();
            Panel panel = (Panel)sender;
            panel.BackColor = Color.White;
        }

        private void WotrackBar_ValueChanged(object sender, EventArgs e)
        {

        }

        private void RtrackBar_ValueChanged(object sender, EventArgs e)
        {

        }

        private void GtrackBar_ValueChanged(object sender, EventArgs e)
        {

        }

        private void BtrackBar_ValueChanged(object sender, EventArgs e)
        {

        }

        private void WftrackBar_ValueChanged(object sender, EventArgs e)
        {

        }

        private void canelbt_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void blowbt_Click(object sender, EventArgs e)
        {
            int registerAddress = 2000;
            int registerBit = 4 ;
            int value = 1 << registerBit;
            byte[] receiveData = new byte[255];
            D2000 = D2000 ^ value;
            SendValueToRegister(registerAddress, D2000, receiveData);

        }
        private void conn()
        {

            if (PLCController.Instance.Connection(IniFile.iniRead("PLC", "ip"), Convert.ToInt32(IniFile.iniRead("PLC", "port"))))
                Console.WriteLine("连接成功");
            else
                MessageBox.Show("连接失败");

        }
        private void SendValueToRegister(int registerAddress, int value, byte[] receiveData)
        {
            try
            {
                byte[] writeValue = new byte[2] { (byte)(value / 256), (byte)(value % 256) };
                //byte[] receiveData = new byte[255];
                if (PLCController.Instance.IsConnected)
                    PLCController.Instance.WriteData(registerAddress, 1, writeValue, receiveData);
            }
            catch (Exception exp)
            {

            }
        }
    }
}

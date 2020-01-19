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
    public partial class PingbiForm : Form
    {
        private int D2000 = 0;
        private int D2001 = 0;
        private int D2002 = 0;
        private int D2003 = 0;
        private int D2004 = 0;
        public PingbiForm()
        {
            InitializeComponent();
        }

        private void Openbt_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {

                int[] registerBitall = { 0 };
                foreach (int i in registerBitall)
                {
                    int registerAddress = 2004;
                    int registerBit = i;
                    int value = 1 << registerBit;

                    int currentValue = 0;

                    byte[] receiveData = new byte[255];

                    if (registerAddress == 2004)
                    {
                        D2004 = value;
                        currentValue = D2004;
                        SendValueToRegister(2004, D2004, receiveData);
                    }
                }


            }
            catch
            {


            }
        }

        private void Openbt_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {

                int[] registerBitall = { 0 };
                foreach (int i in registerBitall)
                {
                    int registerAddress = 2004;
                    int registerBit = i;
                    int value = 0 << registerBit;

                    int currentValue = 0;

                    byte[] receiveData = new byte[255];

                    if (registerAddress == 2004)
                    {
                        D2004 = value;
                        currentValue = D2004;
                        SendValueToRegister(2004, D2004, receiveData);
                    }
                }


            }
            catch
            {


            }

        }

        private void Fengminbt_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {

                int[] registerBitall = { 10 };
                foreach (int i in registerBitall)
                {
                    int registerAddress = 2004;
                    int registerBit = i;
                    int value = 1 << registerBit;

                    int currentValue = 0;

                    byte[] receiveData = new byte[255];

                    if (registerAddress == 2004)
                    {
                        D2004 = value;
                        currentValue = D2004;
                        SendValueToRegister(2004, D2004, receiveData);
                    }
                }


            }
            catch
            {


            }
        }

        private void Fengminbt_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {

                int[] registerBitall = { 10 };
                foreach (int i in registerBitall)
                {
                    int registerAddress = 2004;
                    int registerBit = i;
                    int value = 0 << registerBit;

                    int currentValue = 0;

                    byte[] receiveData = new byte[255];

                    if (registerAddress == 2004)
                    {
                        D2004 = value;
                        currentValue = D2004;
                        SendValueToRegister(2004, D2004, receiveData);
                    }
                }


            }
            catch
            {


            }

        }

        private void Closebt_Click(object sender, EventArgs e)
        {
            this.DialogResult =DialogResult.OK;
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

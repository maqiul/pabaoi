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
    public partial class Motioncontrol : Form
    {
        string frontorside;

        private int D2000 = 0;
        private int D2001 = 0;
        private int D2002 = 0;
        private int D2003 = 0;
        private int D2004 = 0;
        public Motioncontrol()
        {
            InitializeComponent();
            conn();
            lefttitlebt_Click(lefttitlebt, null);
            //Xspeed.Text = "1234";
        }


        private void lefttitlebt_Click(object sender, EventArgs e)
        {
            righttitlebt.BackColor = Color.Gray;
           
            Panel panel = (Panel)sender;
            panel.BackColor = Color.White;
            frontorside = "front";
            Electrify();
        }

        private void righttitlebt_Click(object sender, EventArgs e)
        {
            lefttitlebt.BackColor = Color.Gray;
            Panel panel = (Panel)sender;
            panel.BackColor = Color.White;
            frontorside = "side";
            Electrify();
        }

        private void canelbt_Click(object sender, EventArgs e)
        {
            PLCController.Instance.CloseConnection();
            this.Close();
        }

        private void Restbt_Click(object sender, EventArgs e)
        {


        }


        private void conn()
        {

            if (PLCController.Instance.Connection(IniFile.iniRead("PLC", "ip"), Convert.ToInt32(IniFile.iniRead("PLC", "port"))))
                Console.WriteLine("连接成功");
            else
                MessageBox.Show("连接失败");

        }

        private void Xspeed_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
            else
            {

            }
        }

        private void Yspeed_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
            else
            {

            }
        }

        private void Xspeed_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (frontorside == "front")
                {
                    double value = Convert.ToDouble(Xspeed.Text) * 1000;
                    int registerAddress = 2050;
                    byte[] receiveData = new byte[255];

                    if (value > 0xffff)
                    {
                        MessageBox.Show("超出设置范围");
                        return;
                    }
                    byte[] writeValue = new byte[2];
                    writeValue[0] = (byte)(value / Math.Pow(256, 1));
                    writeValue[1] = (byte)((value / Math.Pow(256, 0)) % 256);
                    if (PLCController.Instance.IsConnected)
                        PLCController.Instance.WriteData(registerAddress, 1, writeValue, receiveData);


                }
                else
                {
                    double value = Convert.ToDouble(Xspeed.Text) * 1000;
                    int registerAddress = 2052;
                    byte[] receiveData = new byte[255];

                    if (value > 0xffff)
                    {
                        MessageBox.Show("超出设置范围");
                        return;
                    }
                    byte[] writeValue = new byte[2];
                    writeValue[0] = (byte)(value / Math.Pow(256, 1));
                    writeValue[1] = (byte)((value / Math.Pow(256, 0)) % 256);
                    if (PLCController.Instance.IsConnected)
                        PLCController.Instance.WriteData(registerAddress, 1, writeValue, receiveData);

                }
            }
            catch { 
            }

        }

        private void Yspeed_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (frontorside == "front")
                {
                    double value = Convert.ToDouble(Yspeed.Text) * 1000;
                    int registerAddress = 2051;
                    byte[] receiveData = new byte[255];

                    if (value > 0xffff)
                    {
                        MessageBox.Show("超出设置范围");
                        return;
                    }
                    byte[] writeValue = new byte[2];
                    writeValue[0] = (byte)(value / Math.Pow(256, 1));
                    writeValue[1] = (byte)((value / Math.Pow(256, 0)) % 256);
                    if (PLCController.Instance.IsConnected)
                        PLCController.Instance.WriteData(registerAddress, 1, writeValue, receiveData);

                }
                else
                {
                    double value = Convert.ToDouble(Yspeed.Text) * 1000;
                    int registerAddress = 2053;
                    byte[] receiveData = new byte[255];

                    if (value > 0xffff)
                    {
                        MessageBox.Show("超出设置范围");
                        return;
                    }
                    byte[] writeValue = new byte[2];
                    writeValue[0] = (byte)(value / Math.Pow(256, 1));
                    writeValue[1] = (byte)((value / Math.Pow(256, 0)) % 256);
                    if (PLCController.Instance.IsConnected)
                        PLCController.Instance.WriteData(registerAddress, 1, writeValue, receiveData);


                }
            }
            catch { 
            
            
            }

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

        private void Xdown_Click(object sender, EventArgs e)
        {
            try
            {
                if (frontorside == "front")
                {
                    int[] registerBitall = { 7 };
                    foreach (int i in registerBitall)
                    {
                        int registerAddress = 2002;
                        int registerBit = i;
                        int value = 1 << registerBit;

                        int currentValue = 0;

                        byte[] receiveData = new byte[255];

                        if (registerAddress == 2002)
                        {
                            D2002 = D2002 ^ value;
                            currentValue = D2002;
                            SendValueToRegister(2002, D2002, receiveData);
                        }
                    }

                }
                else
                {
                    int[] registerBitall = { 11 };
                    foreach (int i in registerBitall)
                    {
                        int registerAddress = 2002;
                        int registerBit = i;
                        int value = 1 << registerBit;

                        int currentValue = 0;

                        byte[] receiveData = new byte[255];

                        if (registerAddress == 2002)
                        {
                            D2002 = D2002 ^ value;
                            currentValue = D2002;
                            SendValueToRegister(2002, D2002, receiveData);
                        }
                    }


                }
            }
            catch
            {


            }

        }

        private void Xup_Click(object sender, EventArgs e)
        {
            try
            {
                if (frontorside == "front")
                {
                    int[] registerBitall = { 6 };
                    foreach (int i in registerBitall)
                    {
                        int registerAddress = 2002;
                        int registerBit = i;
                        int value = 1 << registerBit;

                        int currentValue = 0;

                        byte[] receiveData = new byte[255];

                        if (registerAddress == 2002)
                        {
                            D2002 = D2002 ^ value;
                            currentValue = D2002;
                            SendValueToRegister(2002, D2002, receiveData);
                        }
                    }

                }
                else
                {
                    int[] registerBitall = { 10 };
                    foreach (int i in registerBitall)
                    {
                        int registerAddress = 2002;
                        int registerBit = i;
                        int value = 1 << registerBit;

                        int currentValue = 0;

                        byte[] receiveData = new byte[255];

                        if (registerAddress == 2002)
                        {
                            D2002 = D2002 ^ value;
                            currentValue = D2002;
                            SendValueToRegister(2002, D2002, receiveData);
                        }
                    }


                }
            }
            catch
            {


            }

        }

        private void Yup_Click(object sender, EventArgs e)
        {
            try
            {
                if (frontorside == "front")
                {
                    int[] registerBitall = { 8 };
                    foreach (int i in registerBitall)
                    {
                        int registerAddress = 2002;
                        int registerBit = i;
                        int value = 1 << registerBit;

                        int currentValue = 0;

                        byte[] receiveData = new byte[255];

                        if (registerAddress == 2002)
                        {
                            D2002 = D2002 ^ value;
                            currentValue = D2002;
                            SendValueToRegister(2002, D2002, receiveData);
                        }
                    }

                }
                else
                {
                    int[] registerBitall = { 12 };
                    foreach (int i in registerBitall)
                    {
                        int registerAddress = 2002;
                        int registerBit = i;
                        int value = 1 << registerBit;

                        int currentValue = 0;

                        byte[] receiveData = new byte[255];

                        if (registerAddress == 2002)
                        {
                            D2002 = D2002 ^ value;
                            currentValue = D2002;
                            SendValueToRegister(2002, D2002, receiveData);
                        }
                    }


                }
            }
            catch
            {


            }
        }

        private void Ydown_Click(object sender, EventArgs e)
        {
            try
            {
                if (frontorside == "front")
                {
                    int[] registerBitall = { 9};
                    foreach (int i in registerBitall)
                    {
                        int registerAddress = 2002;
                        int registerBit = i;
                        int value = 1 << registerBit;

                        int currentValue = 0;

                        byte[] receiveData = new byte[255];

                        if (registerAddress == 2002)
                        {
                            D2002 = D2002 ^ value;
                            currentValue = D2002;
                            SendValueToRegister(2002, D2002, receiveData);
                        }
                    }

                }
                else
                {
                    int[] registerBitall = { 13 };
                    foreach (int i in registerBitall)
                    {
                        int registerAddress = 2002;
                        int registerBit = i;
                        int value = 1 << registerBit;

                        int currentValue = 0;

                        byte[] receiveData = new byte[255];

                        if (registerAddress == 2002)
                        {
                            D2002 = D2002 ^ value;
                            currentValue = D2002;
                            SendValueToRegister(2002, D2002, receiveData);
                        }
                    }


                }
            }
            catch
            {


            }
        }
        private void Electrify() {
            try
            {
                if (frontorside == "front")
                {
                    int[] registerBitall = { 10, 11 };
                    foreach (int i in registerBitall)
                    {
                        int registerAddress = 2000;
                        int registerBit = i;
                        int value = 1 << registerBit;

                        int currentValue = 0;

                        byte[] receiveData = new byte[255];

                        if (registerAddress == 2000)
                        {
                            D2000 = D2000 ^ value;
                            currentValue = D2000;
                            SendValueToRegister(2000, D2000, receiveData);
                        }
                    }

                }
                else
                {
                    int[] registerBitall = { 12, 13 };
                    foreach (int i in registerBitall)
                    {
                        int registerAddress = 2000;
                        int registerBit = i;
                        int value = 1 << registerBit;

                        int currentValue = 0;

                        byte[] receiveData = new byte[255];

                        if (registerAddress == 2000)
                        {
                            D2000 = D2000 ^ value;
                            currentValue = D2000;
                            SendValueToRegister(2000, D2000, receiveData);
                        }
                    }

                }
            }
            catch { 
            
            
            }

        
        
        }
    }
}

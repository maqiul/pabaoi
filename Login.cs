using QTing.PLC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pcbaoi
{
    public partial class Login : Form
    {
        bool isconn = false;
        private int D2000 = 0;
        private int D2001 = 0;
        private int D2002 = 0;
        private int D2003 = 0;
        private int D2004 = 0;
        public Login()
        {
            InitializeComponent();
            //conn();
            Electrify();
            Runspeed();
            Runstart();


        }

        void startLogin()
        {
            //string username = textBox1.Text;
            //string password = textBox2.Text;
            //string haveusersql = string.Format("select count(*) as count from users where username = '{0}' and password = '{1}'", username, GenerateMD5(password));
            //int usernum = MySqlHelper.GetCount(haveusersql);
            //if (usernum == 1)
            //{
            //    Loghelper.WriteLog(textBox1.Text + " 用户登录成功");
            //    this.DialogResult = DialogResult.OK;
            //    //this.Close();
            //}
            //else
            //{


            //    MessageBox.Show("用户名 密码错误");

            //}
            this.DialogResult = DialogResult.OK;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // if it is a hotkey, return true; otherwise, return false
            switch (keyData)
            {
                case Keys.Enter:
                    button1.Focus();
                    button1.PerformClick();
                    return true;
                //......
                default:
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            startLogin();
        }
        public static string GenerateMD5(string txt)
        {
            using (MD5 mi = MD5.Create())
            {
                byte[] buffer = Encoding.Default.GetBytes(txt);
                //开始加密
                byte[] newBuffer = mi.ComputeHash(buffer);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < newBuffer.Length; i++)
                {
                    sb.Append(newBuffer[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        private void conn()
        {

            if (PLCController.Instance.Connection(IniFile.iniRead("PLC", "ip"), Convert.ToInt32(IniFile.iniRead("PLC", "port"))))
            {
                isconn = true;
                Loghelper.WriteLog("连接PLC成功");
            }
            else {
                isconn = false;
                Loghelper.WriteLog("连接PLC失败");
            }

        }
        private void Electrify()
        {
            try
            {
                if (isconn) {
                    int[] registerBitall = { 10, 11, 12, 13,14,15 };
                    foreach (int i in registerBitall)
                    {
                        int registerAddress = 2000;
                        int registerBit = i;
                        int value = 1 << registerBit;

                        int currentValue = 0;

                        byte[] receiveData = new byte[255];

                        if (registerAddress == 2000)
                        {
                            D2000 =  value;
                            currentValue = D2000;
                            SendValueToRegister(2000, D2000, receiveData);
                        }
                    }
                }
                         
            }
            catch
            {


            }
        }
        private void Runspeed() {
            int[] address = new int[] { 2050, 2052, 2054 };
            for (int i = 0; i < address.Length; i++)
            {
                double value = Convert.ToDouble(IniFile.iniRead("Runspeed", address[i].ToString()));
                //double rate = (double)dataGridView6.Rows[rowIndex].Cells[2].Value;
                int registerAddress = address[i];
                //int wordBit = listMonitor_ServoConfig[e.RowIndex].WordBit;
                byte[] receiveData = new byte[255];
                if (value > 0xfffffff)
                {
                    MessageBox.Show("超出设置范围");
                    return;
                }


                byte[] writeValue = new byte[4];
                writeValue = DoubleToByte(value);
                if (PLCController.Instance.IsConnected)
                    PLCController.Instance.WriteData(registerAddress, 2, writeValue, receiveData);

            }
            address = new int[] { 2056 };
            for (int i = 0; i < address.Length; i++)
            {
                int value = Convert.ToInt32(IniFile.iniRead("Runspeed", address[i].ToString()));
                //double rate = (double)dataGridView6.Rows[rowIndex].Cells[2].Value;
                int registerAddress = address[i];
                //int wordBit = listMonitor_ServoConfig[e.RowIndex].WordBit;
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

        private byte[] DoubleToByte(double value)
        {
            byte[] obuf = new byte[4];

            if (value < 0)
            {
                value = Math.Abs(value);
                obuf[0] = (byte)~(byte)((value % Math.Pow(256, 2)) / 256);
                obuf[1] = (byte)~(byte)((value % Math.Pow(256, 2)) % 256);
                obuf[2] = (byte)~(byte)(value / Math.Pow(256, 3));
                obuf[3] = (byte)~(byte)((value / Math.Pow(256, 2)) % 256);

                obuf[1] = (byte)((byte)~(byte)((value % Math.Pow(256, 2)) % 256) + 1);
                // obuf[2] = (byte)((byte)~(byte)(value / Math.Pow(256, 3)) + 128);
            }
            else
            {
                obuf[0] = (byte)((value % Math.Pow(256, 2)) / 256);
                obuf[1] = (byte)((value % Math.Pow(256, 2)) % 256);
                obuf[2] = (byte)(value / Math.Pow(256, 3));
                obuf[3] = (byte)((value / Math.Pow(256, 2)) % 256);
            }
            return obuf;
        }
        private void Runstart() {
            try
            {
                byte[] newreceiveData = new byte[255]; //{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
                int num = PLCController.Instance.ReadData(1131, 2, newreceiveData);
                double newvalue = newreceiveData[11] * Math.Pow(256, 3) + newreceiveData[12] * Math.Pow(256, 2) + newreceiveData[9] * Math.Pow(256, 1) + newreceiveData[10];
                if (newvalue == 1.0) {
                    //byte[] receiveData = new byte[255];
                    //byte[] writeValue = new byte[4];
                    //writeValue = DoubleToByte(0);
                    //if (PLCController.Instance.IsConnected)
                    //    PLCController.Instance.WriteData(5000, 2, writeValue, receiveData);
                    //double thenewvalue = 1.00;
                    //byte[] thenewwriteValue = new byte[2];
                    //thenewwriteValue[0] = (byte)(thenewvalue / Math.Pow(256, 1));
                    //thenewwriteValue[1] = (byte)((thenewvalue / Math.Pow(256, 0)) % 256);
                    //if (PLCController.Instance.IsConnected)
                    //    PLCController.Instance.WriteData(2145, 1, thenewwriteValue, receiveData);
                    //receiveData = new byte[255];
                    //writeValue = new byte[4];
                    //writeValue = DoubleToByte(0);
                    //if (PLCController.Instance.IsConnected)
                    //    PLCController.Instance.WriteData(5002, 2, writeValue, receiveData);
                    //thenewvalue = 1.00;
                    //thenewwriteValue = new byte[2];
                    //thenewwriteValue[0] = (byte)(thenewvalue / Math.Pow(256, 1));
                    //thenewwriteValue[1] = (byte)((thenewvalue / Math.Pow(256, 0)) % 256);
                    //if (PLCController.Instance.IsConnected)
                    //    PLCController.Instance.WriteData(2147, 1, thenewwriteValue, receiveData);
                    MessageBox.Show("还有板子在设备中请先取出，然后重启软件");
                    Application.Exit();
                    //return;

                }

                int[] registerBitall = { 9 };
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
                Thread.Sleep(200);
                
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

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}

using QTing.PLC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
            conn();
            Electrify();



        }

        void startLogin()
        {
            //string username = textBox1.Text;
            //string password = textBox2.Text;
            //string haveusersql = string.Format("select count(*) as count from users where username = '{0}' and password = '{1}'", username, GenerateMD5(password));
            //int usernum = MySqlHelper.GetCount(haveusersql);
            //if (usernum == 1)
            //{
                  Loghelper.WriteLog( textBox1.Text + " 用户登录成功");
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
                    int[] registerBitall = { 10, 11, 12, 13 };
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

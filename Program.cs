﻿using pcbaoi.Properties;
using QTing.PLC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pcbaoi
{
    static class Program
    {
        static bool IsMonitor = true;
        static bool isconn = false;
        private static int D2000 = 0;

        private static int D2002 = 0;
        private static int D2004 = 0;

        static BackgroundWorker bw_MonitorMotionController;      //监控
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (System.Diagnostics.Process.GetProcessesByName(System.Diagnostics.Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                MessageBox.Show("已有另一个程序在运行");
                Application.Exit();
                Environment.Exit(0);
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bw_MonitorMotionController = new BackgroundWorker();
            bw_MonitorMotionController.DoWork += Bw_MonitorMotionController_DoWork;
            //bw_MonitorMotionController.RunWorkerCompleted += Bw_MonitorMotionController_RunWorkerCompleted;
            bw_MonitorMotionController.WorkerReportsProgress = true;
            conn();
            IsMonitor = true;
            if (!bw_MonitorMotionController.IsBusy)
            {
                bw_MonitorMotionController.WorkerSupportsCancellation = true;
                bw_MonitorMotionController.RunWorkerAsync();
            }
            RestValue();
            Login login = new Login();
            DialogResult dialogResult = login.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                login.Close();
                Application.Run(new CaptureForm(1));
            }
            substrateOut();
            Environment.Exit(0);
        }
        private static void substrateOut()
        {
            //while (true) {

            byte[] receiveData = new byte[255];
            double thenewvalue = 1.00;
            byte[] thenewwriteValue = new byte[2];
            thenewwriteValue[0] = (byte)(thenewvalue / Math.Pow(256, 1));
            thenewwriteValue[1] = (byte)((thenewvalue / Math.Pow(256, 0)) % 256);
            if (PLCController.Instance.IsConnected)
                PLCController.Instance.WriteData(2145, 1, thenewwriteValue, receiveData);
            Console.Write("A面出板信号");
            receiveData = new byte[255];
            thenewvalue = 1.00;
            thenewwriteValue = new byte[2];
            thenewwriteValue[0] = (byte)(thenewvalue / Math.Pow(256, 1));
            thenewwriteValue[1] = (byte)((thenewvalue / Math.Pow(256, 0)) % 256);
            if (PLCController.Instance.IsConnected)
                PLCController.Instance.WriteData(2147, 1, thenewwriteValue, receiveData);
            Console.Write("B面出板信号");


            //}

        }
        //转Double 转字节
        private static byte[] DoubleToByte(double value)
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

        private static void conn()
        {

            if (PLCController.Instance.Connection(IniFile.iniRead("PLC", "ip"), Convert.ToInt32(IniFile.iniRead("PLC", "port"))))
            {

                Console.WriteLine("连接成功");

            }

            else {

                MessageBox.Show("连接失败");

            }

        }
        private static void close()
        {

            PLCController.Instance.CloseConnection();
        }
        private static void Bw_MonitorMotionController_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                int precent = 0;
                while (true)
                {
                    if (IsMonitor)
                    {
                        if (PLCController.Instance.IsConnected)
                        {
                            // Console.WriteLine("服务运行中");
                            Thread.Sleep(200);
                            byte[] newreceiveData = new byte[255]; //{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
                            int num = PLCController.Instance.ReadData(1001, 1, newreceiveData);
                            int value = newreceiveData[9] * 256 + newreceiveData[10];
                            string strValue = Convert.ToString(value, 2).PadLeft(16, '0');
                            string numvalue = strValue.Substring(11, 1);
                            if (numvalue == "0")
                            {
                                Electrify();
                                Console.WriteLine("重新写了使能");
                                //Thread.Sleep(100);
                            }
                            DisplayMonitorAlarm();
                            if (precent > 100)
                            {
                                precent = 0;
                            }
                            //bw_MonitorMotionController.ReportProgress(precent);
                        }
                        else
                        {
                            Console.WriteLine("断开连接了，即将自动连接");
                            PLCController.Instance.Connection(IniFile.iniRead("PLC", "ip"), Convert.ToInt32(IniFile.iniRead("PLC", "port")));
                            Console.WriteLine("重新连接成功了");

                        }
                    }


                }
            }
            catch (Exception exp)
            {
                Loghelper.WriteLog("plc检测服务中报错了", exp);
            }
            finally
            {

            }
        }
        private static void DisplayMonitorAlarm(){
            try
            {
                //----------------------Monitor_Alarm
                List<double> listValue = new List<double>();
                int length = 2;
                byte[] receiveData = new byte[255]; //{ 1, 2, 3, 4 };
                int num = PLCController.Instance.ReadData(1147, length, receiveData);
                if (num != length * 2 + 9)
                {
                    return;
                }
                if (receiveData[8] != length * 2)
                {
                    return;
                }
                for (int i = 0; i < length; i++)
                {
                    int value = receiveData[i * 2 + 9] * 256 + receiveData[i * 2 + 10];
                    string strValue = Convert.ToString(value, 2).PadLeft(16, '0');
                    for (int j = 15; j >= 0; j--) {
                        string valuestr = strValue.Substring(j, 1);
                        if (valuestr == "1") {
                            string name = Enum.GetName(typeof(Enumall.Alarm),i*15+j);
                            MessageBox.Show(name+"出现报警了，请赶快查看");

                        }

                    }

                }
                

            }
            catch (Exception exp)
            {

            }


        }
        private static void Electrify()
        {
            try
            {
                if (isconn)
                {
                    int[] registerBitall = { 10, 11, 12, 13, 14, 15 };
                    foreach (int i in registerBitall)
                    {
                        int registerAddress = 2000;
                        int registerBit = i;
                        int value = 1 << registerBit;

                        int currentValue = 0;

                        byte[] receiveData = new byte[255];

                        if (registerAddress == 2000)
                        {
                            D2000 = value;
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
        private static void SendValueToRegister(int registerAddress, int value, byte[] receiveData)
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
        //软重置
        private static void RestValue()
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
            Thread.Sleep(500);

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


    }
}

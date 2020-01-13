using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using Microsoft.Win32;
using QTing.SingleTon;
namespace pcbaoi.COM
{
    //读取COM口
    public class COM : SingleTon<COM>
    {  //端口名称

        public bool COM_ERROR = false;
        public COM()
        {
        }
        /// <summary>
        /// 获取PC上的COM口
        /// </summary>
        /// <param name="isUseReg">true是通过注册表获取，false是通过串口获取</param>
        /// <returns></returns>
        public static List<string> GetComlist(bool isUseReg)
        {
            List<string> list = new List<string>();
            try
            {
                if (isUseReg)
                {
                    RegistryKey RootKey = Registry.LocalMachine;
                    RegistryKey Comkey = RootKey.OpenSubKey(@"HARDWARE\DEVICEMAP\SERIALCOMM");

                    String[] ComNames = Comkey.GetValueNames();

                    foreach (String ComNamekey in ComNames)
                    {
                        string TemS = Comkey.GetValue(ComNamekey).ToString();
                        list.Add(TemS);
                    }
                }
                else
                {
                    foreach (string com in System.IO.Ports.SerialPort.GetPortNames())  //自动获取串行口名称  
                        list.Add(com);
                }
            }
            catch
            {
            }
            return list;
        }

        //COM口是否存在
        public bool IsExist(string comName)
        {
            try
            {
                List<string> comNames = GetComlist(false);

                foreach (string com in comNames)
                {
                    if (com == comName.ToUpper())
                    {
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
            }
            return false;
        }
    }

    //控制板
    public class ControlBroad : SingleTon<ControlBroad>
    {

        public Action<object> SynRecivedDataCallBack;
        private SerialPort port = new SerialPort();
        private object obj = new object();

        private int _timeout;

        /// <summary>
        /// 包头
        /// </summary>
        private byte HAND = 0x55;
        /// <summary>
        /// 包尾
        /// </summary>
        private byte END = 0x00;

        /// <summary>
        /// 数据长度
        /// </summary>
        private int recLegth = 1;

        /// <summary>
        /// 接受返回数据
        /// </summary>
        private List<byte> recData = new List<byte>();

        public ControlBroad()
        {

        }
        /// <summary>
        /// 端口初始化
        /// </summary>
        /// <param name="protName"></param>
        /// <param name="portBaudRate"></param>
        /// <param name="protDataBits"></param>
        /// <param name="parity"></param>
        /// <param name="stopBits"></param>
        /// <returns></returns>
        public bool Initialize(string portName, int portBaudRate, int portDataBits, Parity parity, StopBits stopBits, int timeout)
        {
            try
            {
                port.PortName = portName;
                port.BaudRate = portBaudRate;
                port.DataBits = portDataBits;
                port.Parity = parity;
                port.StopBits = stopBits;
                this._timeout = timeout;
               if (!port.IsOpen) port.Open();
              
            }
            catch (Exception exc)
            {

                Loghelper.WriteLog("初始化com口失败",exc);
                return false;
            }
            return true;
        }

        public bool IsOpen
        {
            get { return port.IsOpen; }

        }


        public void Open()
        {
            try
            {
                if (!port.IsOpen) port.Open();
            }
            catch
            {

            }
        }

        public void Close()
        {
            try
            {
                if (port.IsOpen) port.Close();
            }
            catch
            {

            }
        }


        /// <summary>
        /// 同步发送数据字节
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="sendData"></param>
        /// <returns></returns>
        public byte[] SynSendByte(byte[] sendData)
        {
            try
            {
                //byte[] sendata = Parity(data.ToArray());
                lock (obj)
                {
                    byte[] _sendData = new byte[8];
                    port.Write(sendData, 0, sendData.Length);
                    return SynReceiveByte(this._timeout);
                }
            }

            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
          
        }


        public byte[] SynSendLightValue(byte channel, byte value)
        {
            try
            {
                //byte[] sendata = Parity(data.ToArray());
                lock (obj)
                {
                    byte[] sendData = new byte[4];                 
                    sendData[0] = 0x24;
                    sendData[1] = channel;
                    sendData[2] = value;
                    byte[] obuf = new byte[3] { sendData[0],sendData[1],sendData[2]};
                    sendData[3] = Xor(obuf);
                    port.Write(sendData, 0, sendData.Length);
                    return SynReceiveByte(this._timeout);
                }
            }

            catch (Exception exp)
            {
                Loghelper.WriteLog("发送失败",exp);
                throw new Exception(exp.Message);
            }
        }
            

      

        private byte[] SynReceiveByte(int readTimeout)
        {
            port.ReadTimeout = readTimeout;
            List<byte> readValues = new List<byte>();
            try
            {
                DateTime startTime = DateTime.Now;
                while (true)
                {
                    if (port.BytesToRead > 0)
                    {
                        //读取数据
                        byte[] data = this.ReadExists();
                        if (data != null)
                        {
                            for (int i = 0; i < data.Length; i++)
                            {
                                if (data[i] == this.HAND)
                                {
                                    readValues.Add(data[i]);

                                    if (SynRecivedDataCallBack != null)
                                        SynRecivedDataCallBack(readValues.ToArray());
                                    return readValues.ToArray();
                                }
                              //else  if (readValues.Count > 0 && readValues[0] == this.HAND)
                              //  {
                              //      readValues.Add(data[i]);
                              //      if (readValues.Count == this.recLegth)
                              //      {
                              //          if (SynRecivedDataCallBack != null)
                              //              SynRecivedDataCallBack(readValues.ToArray());
                              //          return readValues.ToArray();
                              //      }
                              //  }
                            }
                        }
                    }
                    //如果还没有收到，检查是否有必要继续等待
                    TimeSpan span = DateTime.Now.Subtract(startTime);
                    if (span.TotalMilliseconds > readTimeout)//超时
                        throw new Exception("接收数据超时");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public byte[] ReadExists()
        {
            if (port.BytesToRead < 1)
                return null;
            byte[] buffer = new byte[port.BytesToRead];
            int count = port.Read(buffer, 0, buffer.Length);
            return buffer;
        }
        /// <summary>
        /// 同步发送字符串
        /// </summary>
        /// <param name="sendData"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public string SynSendString(string sendData, string flag)
        {        
            string fcs = Fcs(sendData);
            try
            {
                port.Write(sendData + fcs.PadLeft(2, '0') + "*" + (char)13);
                // 中间可能需要加一些延时
                return SynReceiveString(this._timeout, flag);
            }
            catch (System.ArgumentNullException arg)
            {
                throw new Exception("串口通讯出错:" + arg.Message);
            }
            catch (System.InvalidOperationException inv)
            {
                throw new Exception("串口通讯出错:" + inv.Message);
            }
            catch (System.ArgumentOutOfRangeException argout)
            {
                throw new Exception("串口通讯出错:" + argout.Message);
            }
            catch (System.ArgumentException argexp)
            {
                throw new Exception("串口通讯出错:" + argexp.Message);
            }
            catch (System.TimeoutException time)
            {
                throw new Exception("串口通讯出错: " + time.Message);
            }
        }
        protected string Fcs(string s)　　//帧校验函数FCS
        {
            //获取s对应的字节数组
            byte[] b = Encoding.ASCII.GetBytes(s);
            // xorResult 存放校验结果。注意：初值去首元素值！
            byte xorResult = b[0];
            // 求xor校验和。注意：XOR运算从第二元素开始
            for (int i = 1; i < b.Length; i++)
            {
                //**进行异或运算，^=就是异或运算符，具体可查阅异或运算
                //**异或运算：两个二进制数的每一位进行比较，如果相同则为0，不同则为1,如下面2个10进制数37、     48的二进制值异或结果为21
                //**  37(10)       100101(2)
                //**  48(10)       110000(2)
                //**  21(10)       010101(2)
                //**这里的意思是：如a^=b，就是a与b先进行异或比较，得出的结果赋值给a；
                xorResult ^= b[i];
            }
            //**Convert.ToString(xorResult, 16):将当前值转换为16进制；ToUpper()：结果大写；
            //**这里的意思是：将xorResult转换成16进制并大写；
            //**（//**返回的结果为一个两个ASCII码的异或值）
            return Convert.ToString(xorResult, 16).ToUpper();
        }

        public string SynReceiveString(int readTimeout, string flag)
        {
            port.ReadTimeout = readTimeout;
            string readValues = "";
            try
            {
                DateTime startTime = DateTime.Now;
                while (true)
                {
                    if (port.BytesToRead > 0)
                    {
                        //读取数据
                        string data = port.ReadExisting();
                        if (data != null)
                        {
                            readValues += data;
                            if (readValues.Length > 3 && readValues.Contains(flag))
                                if (SynRecivedDataCallBack != null)
                                    SynRecivedDataCallBack(readValues.ToArray());
                            return readValues;
                        }
                    }
                    //如果还没有收到，检查是否有必要继续等待
                    TimeSpan span = DateTime.Now.Subtract(startTime);
                    if (span.TotalMilliseconds > readTimeout)//超时
                        throw new Exception("接收数据超时");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 设备执行PLC命令
        /// </summary>
        /// <param name="commmandRun"></param>
        /// <param name="commandReset"></param>
        /// <param name="strMessage"></param>
        public void PLC_Command(string commmandRun, string commandReset)
        {
            if (this.IsOpen)
               Open();
           SynSendString(commmandRun, "*\r");
           SynSendString(commandReset, "*\r");
        }


        private byte Xor(byte[] bt)
        {
            byte b = 0;
            try
            {
                for(int i=0;i<bt.Length;i++)
                {
                    b = (byte)(b ^ bt[i]);
                }

            }
            catch
            {

            }

            return b;
        }


        #region   计算CRC

        public byte[] CalCrc(byte[] bt)
        {
            byte[] b = new byte[2];
            int t = bt.Length;
            byte hi = 0;
            byte lo = 0;
            CalculateCRC(bt, t, out hi, out lo);
            b[0] = lo;
            b[1] = hi;
            return b;
        }

        private static void CalculateCRC(byte[] pByte, int nNumberOfBytes, out byte hi, out byte lo)
        {
            ushort sum;
            CalculateCRC(pByte, nNumberOfBytes, out sum);
            lo = (byte)(sum & 0xFF);
            hi = (byte)((sum & 0xFF00) >> 8);
        }

        private static void CalculateCRC(byte[] pByte, int nNumberOfBytes, out ushort pChecksum)
        {
            int nBit;
            ushort nShiftedBit;
            pChecksum = 0xFFFF;

            for (int nByte = 0; nByte < nNumberOfBytes; nByte++)
            {
                pChecksum ^= pByte[nByte];
                for (nBit = 0; nBit < 8; nBit++)
                {
                    if ((pChecksum & 0x1) == 1)
                    {
                        nShiftedBit = 1;
                    }
                    else
                    {
                        nShiftedBit = 0;
                    }
                    pChecksum >>= 1;
                    if (nShiftedBit != 0)
                    {
                        pChecksum ^= 0xA001;
                    }
                }
            }
        }

        #endregion
    }
}

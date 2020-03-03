using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTing.SingleTon;
using System.Net.Sockets;
using System.Threading;
namespace QTing.PLC
{
    public class PLCController : SingleTon<PLCController>
    {
        public TcpClient tcpClient;
        public NetworkStream stream;

        object obj = new object();
        public PLCController()
        {

        }
        #region 建立通讯

        public bool IsConnected { get; set; }
        public bool Connection(string ipAddress, int port)
        {
            try
            {
                // 把TCP打开提取出来放在这里
                // 创建TCP客户端
                tcpClient = new TcpClient();
                tcpClient.SendTimeout = 1000;
                tcpClient.ReceiveTimeout = 1000;            
                tcpClient.ConnectAsync(ipAddress, port);
                Thread.Sleep(2000);
                stream = tcpClient.GetStream();
                IsConnected = true;
            }
            catch (Exception exp)
            {
                IsConnected = false;
                return false;
            }
            return true;
        }
        #endregion

        #region 关闭端口
        public void CloseConnection()
        {
            try
            {
                if (tcpClient != null)
                {
                    stream.Close();
                    tcpClient.Close();
                }             
                // 把TCP关闭提取出来放在这里
            }
            catch (Exception exp)
            {
                
            }
        }
        #endregion


        #region PC-PLC读数据
        /// <summary>
        /// PC-PLC读数据
        /// </summary>
        /// <param name="dataStartAddress"></param>
        /// <param name="dataLength"></param>
        /// <param name="receiveData"></param>
        /// <returns></returns>
        public int ReadData(int dataStartAddress, int dataLength, byte[] receiveData)
        {
            try
            {
                byte[] obuf = new byte[12];
                obuf[0] = 0x19;
                obuf[1] = 0xB2;
                obuf[2] = 0x00;
                obuf[3] = 0x00;
                obuf[4] = 0x00;
                obuf[5] = 0x06;
                obuf[6] = 0x01;
                obuf[7] = 0x03;
                obuf[8] = (byte)(dataStartAddress / 256);
                obuf[9] = (byte)(dataStartAddress % 256);
                obuf[10] = (byte)(dataLength / 256);
                obuf[11] = (byte)(dataLength % 256);
                //返回长度=11+dataLength*2    [7]+[8]*256=2+dataLength*2   [9]=00 [10]=00
                return Connect(stream, obuf, receiveData);
            }
            catch (Exception exp)
            {
                IsConnected = false;
                return 0;
            }
        }

        #endregion


        #region  PC-PLC 写数据
        /// <summary>
        /// PC-PLC 写数据
        /// </summary>
        /// <param name="dataStartAddress"></param>
        /// <param name="dataLength">寄存器个数</param>
        /// <param name="writeValue">写入的值</param>
        /// <param name="receiveData">返回校验值</param>
        /// <returns></returns>
        public int WriteData(int dataStartAddress, int dataLength,byte[] writeValue, byte[] receiveData)
        {
            try
            {
                if(dataLength*2!=writeValue.Length)
                {
                    return 0;
                }
                byte[] obuf = new byte[13+dataLength*2];
                int length = 7 + dataLength * 2;
                obuf[0] = 0x97;
                obuf[1] = 0x79;
                obuf[2] = 0x00;
                obuf[3] = 0x00;              
                obuf[4] = (byte)(length / 256);
                obuf[5] = (byte)(length % 256);
                obuf[6] = 0x04;
                obuf[7] = 0x10;
                obuf[8] = (byte)(dataStartAddress / 256);
                obuf[9] = (byte)(dataStartAddress % 256);
                obuf[10] = (byte)(dataLength / 256);
                obuf[11] = (byte)(dataLength % 256);
                obuf[12] = (byte)(dataLength * 2);
                for(int i=0;i< writeValue.Length; i++)
                {
                    obuf[13 + i] = writeValue[i];   
                }
                //返回长度=12      
                return Connect(stream, obuf, receiveData);
            }
            catch (Exception exp)
            {
                IsConnected = false;
                return 0;
            }
        }
        #endregion




        #region 通讯用函数
        private int Connect(NetworkStream stream, byte[] sendData, byte[] receiveData)
        {
            lock (obj)
            {
                try
                {
                    stream.ReadTimeout = 1000;
                    stream.WriteTimeout = 1000;
                    stream.Write(sendData, 0, sendData.Length);
                    // 中间可能需要加一些延时
                    Thread.Sleep(10);
                    // 读取数据                   
                    Int32 bytes = stream.Read(receiveData, 0, receiveData.Length);
                    // 关闭端口
                    //              stream.Close();
                    //              client.Close();
                    return bytes;
                }
                catch (ArgumentNullException arg)
                {
                    IsConnected = false;
                    //          runningStatus = false;
                    //                MessageBox.Show("ArgumentNullException: " + e.Message);
                    return 0;
                }
                catch(ArgumentOutOfRangeException arg)
                {
                    IsConnected = false;
                    return -1;
                }

            }          
        }


        #endregion


    }
}

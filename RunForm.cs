using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using pcbaoi.Properties;
using QTing.PLC;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pcbaoi
{
    public partial class RunForm : Form
    {
        Snowflake snowflake = new Snowflake(2);
        string path;
        FileInfo fileInfonew;
        BackgroundWorker backgroundWorker = null;
        BackgroundWorker backgroundWorker2 = null;
        bool isrunall = false;
        int width;
        int height;

        public RunForm()
        {
            InitializeComponent();
           // conn();
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += new DoWorkEventHandler(getstatus);
            backgroundWorker2 = new BackgroundWorker();
            backgroundWorker2.WorkerReportsProgress = true;
            backgroundWorker2.WorkerSupportsCancellation = true;
            backgroundWorker2.DoWork += new DoWorkEventHandler(getstatus2);
            button1.Enabled = false;
            button2.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            #region
            //long i = snowflake.nextId();
            //CopyDir(@"f:\ftp\",@"f:\"+ i+"\\");                       
            ////本机要上传的目录的父目录
            //string localPath = @"F:\";
            ////要上传的目录名
            //string fileName = @i.ToString();
            //Ftp.UploadDirectory(localPath, fileName);
            //JsonData<Pcb> jsonData = new JsonData<Pcb>();
            //List<Result> results = new List<Result>();
            //Snowflake snowflake2 = new Snowflake(3);
            //Result result = new Result();
            //result.Id =snowflake2.nextId().ToString() ;
            //result.IsBack = 0;
            //result.PcbId = i.ToString();
            //result.Area = "";
            //result.Region = "1855";
            //result.NgType = "缺陷1";
            //result.PartImagePath = "front.jpg";
            //result.CreateTime = DateTime.Now;
            //results.Add(result);
            //Pcb pcb = new Pcb();
            //pcb.Id = i.ToString();
            //pcb.PcbNumber = "4";
            //pcb.PcbName = "testpcb";
            //pcb.PcbWidth = 123;
            //pcb.PcbHeight = 123;
            //pcb.PcbChildenNumber = 8;
            //pcb.SurfaceNumber = 1;
            //pcb.PcbPath = i.ToString();
            //pcb.IsError = 1;
            //pcb.CreateTime = DateTime.Now;
            //pcb.results = results;
            //jsonData.data = pcb;
            //string jo = JsonConvert.SerializeObject(jsonData);
            //var factory = new ConnectionFactory();
            //factory.AutomaticRecoveryEnabled = true;
            //factory.HostName = "192.168.31.157"; // RabbitMQ服务在本地运行
            //factory.UserName = "admin"; // 用户名
            //factory.Password = "admin"; // 密码
            //factory.VirtualHost = "my_vhost";

            //using (var connection = factory.CreateConnection())
            //{
            //    using (var channel = connection.CreateModel())
            //    {
            //        // 将消息标记为持久性。
            //        var properties = channel.CreateBasicProperties();
            //        properties.Persistent = true;
            //        channel.QueueDeclare("work", true, false, false, null); // 创建一个名称为hello的消息队列
            //        string message = jo; // 传递的消息内容
            //        var body = Encoding.UTF8.GetBytes(message);

            //        channel.BasicPublish("", "work", properties, body); // 开始传递
            //        //MessageBox.Show("已发送： {0}", message);
            //    }
            //}
            #endregion
            button2.Enabled = true;
            button1.Enabled = false;
            isrunall = true;
            if (!backgroundWorker.IsBusy)
            {
                backgroundWorker.RunWorkerAsync("object argument");//启动异步操作，有两种重载。将触发BackgroundWorker.DoWork事件
            }

            if (!backgroundWorker2.IsBusy)
            {
                backgroundWorker2.RunWorkerAsync("object argument");//启动异步操作，有两种重载。将触发BackgroundWorker.DoWork事件
            }


        }

        private void panel3_Click(object sender, EventArgs e)
        {
            
            panel3.BackColor = Color.DarkGray;
            panel4.BackColor = panel1.BackColor;
        }

        private void panel4_Click(object sender, EventArgs e)
        {
            panel4.BackColor = Color.DarkGray;
            panel3.BackColor = panel1.BackColor;
        }

        private void RunForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        public static void CopyDir(string srcPath, string aimPath)
        {
            try
            {
                // 检查目标目录是否以目录分割字符结束如果不是则添加
                if (aimPath[aimPath.Length - 1] != System.IO.Path.DirectorySeparatorChar)
                {
                    aimPath += System.IO.Path.DirectorySeparatorChar;
                }
                // 判断目标目录是否存在如果不存在则新建
                if (!System.IO.Directory.Exists(aimPath))
                {
                    System.IO.Directory.CreateDirectory(aimPath);
                }
                // 得到源目录的文件列表，该里面是包含文件以及目录路径的一个数组
                // 如果你指向copy目标文件下面的文件而不包含目录请使用下面的方法
                // string[] fileList = Directory.GetFiles（srcPath）；
                string[] fileList = System.IO.Directory.GetFileSystemEntries(srcPath);
                // 遍历所有的文件和目录
                foreach (string file in fileList)
                {
                    // 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
                    if (System.IO.Directory.Exists(file))
                    {
                        CopyDir(file, aimPath + System.IO.Path.GetFileName(file));
                    }
                    // 否则直接Copy文件
                    else
                    {
                        System.IO.File.Copy(file, aimPath + System.IO.Path.GetFileName(file), true);
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            //dialog.RootFolder = Environment.SpecialFolder.Windows;
            path = Setting.Projectpath.Substring(0, Setting.Projectpath.Length - 1);
            dialog.SelectedPath = @path;
            dialog.Description = "请选择保存项目文件夹";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show(this, "文件夹路径不能为空", "提示");
                    return;
                }

                path = dialog.SelectedPath + "\\";
                DirectoryInfo folder = new DirectoryInfo(path);
                
                foreach (FileInfo file in folder.GetFiles("*.db"))
                {
                    fileInfonew = file;
                    
                }
                if (fileInfonew == null)
                {
                    MessageBox.Show("请选择正确文件,保证文件夹下包含 .db文件");
                    return;
                }
                else {
                    textBox1.Text = path;
                    Settings.Default.dbpath = fileInfonew.FullName;
                    string selectsql = "select * from bad";
                    DataTable dataTable = SQLiteHelper.GetDataTable(selectsql);
                    if (dataTable.Rows.Count > 0)
                    {
                        label6.Text = "pcb名称:"+dataTable.Rows[0]["badname"].ToString();
                        label7.Text = "pcb板宽:"+dataTable.Rows[0]["badwidth"].ToString();
                        width = (int)dataTable.Rows[0]["badwidth"];
                        label10.Text = "pcb板长:" + dataTable.Rows[0]["badheight"].ToString();
                        height = (int)dataTable.Rows[0]["badheight"];
                    }
                    string zijibansql = "select *  from zijiban";
                    DataTable dataTable2 = SQLiteHelper.GetDataTable(zijibansql);
                    label9.Text = "子基板数:" + dataTable2.Rows.Count;

                    string zijibanfrontsql = "select *  from zijiban group by frontorside";
                    DataTable dataTable3 = SQLiteHelper.GetDataTable(zijibanfrontsql);
                    label8.Text = "子基板数:" + dataTable3.Rows.Count;
                }
                button1.Enabled = true;
                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (backgroundWorker.IsBusy)
                {

                    backgroundWorker.CancelAsync();
                }
                if (backgroundWorker2.IsBusy)
                {

                    backgroundWorker2.CancelAsync();
                }
            }
            catch(Exception ex) {
                Loghelper.WriteLog("报错了",ex);

            }

            CaptureForm form1 = new CaptureForm(2);
            form1.Show();
            this.Hide();
        }

        private void send(int intvalue,int address) {
            double value = Convert.ToDouble(intvalue.ToString());
            int registerAddress = address;
            byte[] receiveData = new byte[255];
            if (registerAddress < 2133)
            {
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
            else
            {
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

        private void getstatus(object sender, System.ComponentModel.DoWorkEventArgs e)
        {

            while (isrunall)
            {
                //判断是否取消操作 
                if (backgroundWorker.CancellationPending)
                {
                    e.Cancel = true; //这里才真正取消 
                    return;
                }
                //run();

            }

        }
        //private void run()
        //{
        //    int xvalue = Convert.ToInt32(Convert.ToInt32(width));
        //    int yvalue = Convert.ToInt32(Convert.ToInt32(height));
        //    List<int> ax = Xycoordinate.axcoordinate((int)Math.Ceiling((double)xvalue / (double)14), 14);
        //    List<int> ay = Xycoordinate.aycoordinate((int)Math.Ceiling((double)yvalue / (double)14), 14);
        //    byte[] receiveData = new byte[255];
        //    byte[] writeValueX = new byte[ay.Count * 4];
        //    byte[] writeValueY = new byte[ay.Count * 4];
        //    byte[] writeValue = new byte[4];
        //    bool cantak = true;
        //    while (cantak)
        //    {

        //        byte[] newreceiveData = new byte[255]; //{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
        //        int num = PLCController.Instance.ReadData(1131, 2, newreceiveData);
        //        double newvalue = newreceiveData[11] * Math.Pow(256, 3) + newreceiveData[12] * Math.Pow(256, 2) + newreceiveData[9] * Math.Pow(256, 1) + newreceiveData[10];

        //        if (newvalue == 0.00)
        //        {
        //            Thread.Sleep(100);
        //        }
        //        else
        //        {



        //            for (int i = 0; i < ax.Count; i++)
        //            {
        //                if (i == ax.Count)
        //                {
        //                    continue;
        //                }
        //                for (int n = 0; n < ay.Count; n++)
        //                {
        //                    byte[] ibuf = new byte[4];
        //                    ibuf = DoubleToByte(ax[i]);
        //                    writeValueX[n * 4] = ibuf[0];
        //                    writeValueX[n * 4 + 1] = ibuf[1];
        //                    writeValueX[n * 4 + 2] = ibuf[2];
        //                    writeValueX[n * 4 + 3] = ibuf[3];

        //                    //Thread.Sleep(50);
        //                    ibuf = DoubleToByte(ay[n]);
        //                    writeValueY[n * 4] = ibuf[0];
        //                    writeValueY[n * 4 + 1] = ibuf[1];
        //                    writeValueY[n * 4 + 2] = ibuf[2];
        //                    writeValueY[n * 4 + 3] = ibuf[3];


        //                }
        //                if (PLCController.Instance.IsConnected)
        //                    PLCController.Instance.WriteData(3000, ay.Count * 2, writeValueX, receiveData);
        //                Thread.Sleep(50);
        //                if (PLCController.Instance.IsConnected)
        //                    PLCController.Instance.WriteData(3200, ay.Count * 2, writeValueY, receiveData);
        //                writeValue = DoubleToByte(ay.Count);
        //                if (PLCController.Instance.IsConnected)
        //                    PLCController.Instance.WriteData(5000, 2, writeValue, receiveData);
        //                double value = 1.00;
        //                byte[] newwriteValue = new byte[2];
        //                newwriteValue[0] = (byte)(value / Math.Pow(256, 1));
        //                newwriteValue[1] = (byte)((value / Math.Pow(256, 0)) % 256);
        //                if (PLCController.Instance.IsConnected)
        //                    PLCController.Instance.WriteData(2144, 1, newwriteValue, receiveData);
        //                bool isrun = true;
        //                while (isrun)
        //                {

        //                    //Thread.Sleep(50);
        //                    newreceiveData = new byte[255]; //{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };

        //                    num = PLCController.Instance.ReadData(1133, 2, newreceiveData);



        //                    newvalue = newreceiveData[11] * Math.Pow(256, 3) + newreceiveData[12] * Math.Pow(256, 2) + newreceiveData[9] * Math.Pow(256, 1) + newreceiveData[10];
        //                    if (newvalue == 1.00)
        //                    {

        //                        isrun = false;
        //                    }
        //                }

        //            }
        //            cantak = false;

        //        }

        //    }
        //    double thenewvalue = 1.00;
        //    byte[] thenewwriteValue = new byte[2];
        //    thenewwriteValue[0] = (byte)(thenewvalue / Math.Pow(256, 1));
        //    thenewwriteValue[1] = (byte)((thenewvalue / Math.Pow(256, 0)) % 256);
        //    if (PLCController.Instance.IsConnected)
        //        PLCController.Instance.WriteData(2145, 1, thenewwriteValue, receiveData);
        //    Thread.Sleep(200);
        //    //Mat aa =new Mat();
        //    //if (Abitmaps.Count > 0)
        //    //{
        //    //    try
        //    //    {
        //    //        int patchWidth = Abitmaps[0].Width / 4;
        //    //        int patchHeight = Abitmaps[0].Height / 4;
        //    //        Mat aa = new Mat(ay.Count * patchWidth, ax.Count * patchHeight, DepthType.Cv8U, 3);


        //    //        //if (Abitmaps.Count == ax.Count * ay.Count)
        //    //        //{

        //    //        //    for (int i = 0; i < ax.Count; i++)
        //    //        //    {
        //    //        //        for (int j = 0; j < ay.Count; j++)
        //    //        //        {
        //    //        //            int count = i * ay.Count + j;

        //    //        //            Bitmap bitmap = Abitmaps[count];
        //    //        //            bitmap.Save("d:\\newpic\\2" + i.ToString() + ".bmp", ImageFormat.Bmp);
        //    //        //            bitmap = PicSized(bitmap, 4);
        //    //        //            bitmap.Save("d:\\newpic\\" + i.ToString() + ".bmp", ImageFormat.Bmp);
        //    //        //            //bitmap.
        //    //        //            Emgu.CV.Image<Bgr, Byte> currentFrame = new Emgu.CV.Image<Bgr, Byte>(bitmap);
        //    //        //            //mats.Add( currentFrame.Mat);
        //    //        //            Mat invert = new Mat();
        //    //        //            CvInvoke.BitwiseAnd(currentFrame, currentFrame, invert);
        //    //        //            Mat temp = aa.ToImage<Bgr, byte>().GetSubRect(new Rectangle(i * patchHeight, j * patchWidth, invert.Size.Width, invert.Size.Height)).Mat;
        //    //        //            temp.Save("d:\\newpic\\temp" + i.ToString() + "_" + j.ToString() + "jpg");
        //    //        //            invert.CopyTo(temp);
        //    //        //            aa.Save("d:\\newpic\\aa" + i.ToString() + "_" + j.ToString() + "jpg");
        //    //        //            //AoiAi.addPatch(aa.Ptr, invert.Ptr, 500 * k, 500 * n);
        //    //        //            //invert.Dispose();
        //    //        //        }

        //    //        //    }
        //    //        //}
        //    //        // Mat[] matsnew = mats.ToArray();
        //    //        //Stitcher stitcher = new Stitcher(false);
        //    //        //stitcher.Stitch(new VectorOfMat(matsnew), aa);

        //    //        //Image<Bgr, Byte> _image = aa.ToImage<Bgr, Byte>();
        //    //        //Bitmap allbitmap = _image.Bitmap;
        //    //        ////Bitmap tt = new Bitmap(aa.Cols, aa.Rows, (int)aa.Step, PixelFormat.Format24bppRgb, aa.Ptr);
        //    //        //pbMainImg.Image = allbitmap;
        //    //        //CvInvoke.NamedWindow("AJpg", NamedWindowType.Normal); //创建一个显示窗口
        //    //        //CvInvoke.Imshow("AJpg", aa); //显示图片
        //    //        //                             //mats = null;
        //    //        //aa.Dispose();
        //    //    }
        //    //    catch (Exception e)
        //    //    {
        //    //        Loghelper.WriteLog("报错了", e);


        //    //    }
        //    //}


        //    //Bitmap allbitmap = Mat
        //}
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
        private void getstatus2(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (isrunall)
            {
                //判断是否取消操作 
                if (backgroundWorker2.CancellationPending)
                {
                    e.Cancel = true; //这里才真正取消 
                    return;
                }
                //run2();

            }

        }
        //private void run2()
        //{
        //    int xvalue = Convert.ToInt32(width);
        //    int yvalue = Convert.ToInt32(height);
        //    List<int> bx = Xycoordinate.bxcoordinate((int)Math.Ceiling((double)xvalue / (double)14), 14);
        //    List<int> by = Xycoordinate.bycoordinate((int)Math.Ceiling((double)yvalue / (double)14), 14);
        //    byte[] receiveData = new byte[255];
        //    byte[] writeValueX = new byte[by.Count * 4];
        //    byte[] writeValueY = new byte[by.Count * 4];
        //    byte[] writeValue = new byte[4];
        //    bool cantak = true;
        //    while (cantak)
        //    {

        //        byte[] newreceiveData = new byte[255]; //{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
        //        int num = PLCController.Instance.ReadData(1131, 2, newreceiveData);
        //        double newvalue = newreceiveData[11] * Math.Pow(256, 3) + newreceiveData[12] * Math.Pow(256, 2) + newreceiveData[9] * Math.Pow(256, 1) + newreceiveData[10];

        //        if (newvalue == 0.00)
        //        {
        //            Thread.Sleep(100);
        //        }
        //        else
        //        {



        //            for (int i = 0; i < bx.Count; i++)
        //            {
        //                if (i == bx.Count)
        //                {
        //                    continue;
        //                }
        //                for (int n = 0; n < by.Count; n++)
        //                {
        //                    byte[] ibuf = new byte[4];
        //                    ibuf = DoubleToByte(bx[i]);
        //                    writeValueX[n * 4] = ibuf[0];
        //                    writeValueX[n * 4 + 1] = ibuf[1];
        //                    writeValueX[n * 4 + 2] = ibuf[2];
        //                    writeValueX[n * 4 + 3] = ibuf[3];

        //                    //Thread.Sleep(50);
        //                    ibuf = DoubleToByte(by[n]);
        //                    writeValueY[n * 4] = ibuf[0];
        //                    writeValueY[n * 4 + 1] = ibuf[1];
        //                    writeValueY[n * 4 + 2] = ibuf[2];
        //                    writeValueY[n * 4 + 3] = ibuf[3];


        //                }
        //                if (PLCController.Instance.IsConnected)
        //                    PLCController.Instance.WriteData(3400, by.Count * 2, writeValueX, receiveData);
        //                Thread.Sleep(50);
        //                if (PLCController.Instance.IsConnected)
        //                    PLCController.Instance.WriteData(3600, by.Count * 2, writeValueY, receiveData);
        //                writeValue = DoubleToByte(by.Count);
        //                if (PLCController.Instance.IsConnected)
        //                    PLCController.Instance.WriteData(5002, 2, writeValue, receiveData);
        //                double value = 1.00;
        //                byte[] newwriteValue = new byte[2];
        //                newwriteValue[0] = (byte)(value / Math.Pow(256, 1));
        //                newwriteValue[1] = (byte)((value / Math.Pow(256, 0)) % 256);
        //                if (PLCController.Instance.IsConnected)
        //                    PLCController.Instance.WriteData(2146, 1, newwriteValue, receiveData);
        //                bool isrun = true;
        //                while (isrun)
        //                {

        //                    //Thread.Sleep(50);
        //                    newreceiveData = new byte[255]; //{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };

        //                    num = PLCController.Instance.ReadData(1133, 2, newreceiveData);



        //                    newvalue = newreceiveData[11] * Math.Pow(256, 3) + newreceiveData[12] * Math.Pow(256, 2) + newreceiveData[9] * Math.Pow(256, 1) + newreceiveData[10];
        //                    if (newvalue == 1.00)
        //                    {

        //                        isrun = false;
        //                    }
        //                }

        //            }
        //            cantak = false;

        //        }

        //    }
        //    double thenewvalue = 1.00;
        //    byte[] thenewwriteValue = new byte[2];
        //    thenewwriteValue[0] = (byte)(thenewvalue / Math.Pow(256, 1));
        //    thenewwriteValue[1] = (byte)((thenewvalue / Math.Pow(256, 0)) % 256);
        //    if (PLCController.Instance.IsConnected)
        //        PLCController.Instance.WriteData(2147, 1, thenewwriteValue, receiveData);
        //}

        private void conn()
        {

            if (PLCController.Instance.Connection(IniFile.iniRead("PLC", "ip"), Convert.ToInt32(IniFile.iniRead("PLC", "port"))))
                Console.WriteLine("连接成功");
            else
                MessageBox.Show("连接失败");

        }
        private void close()
        {

            PLCController.Instance.CloseConnection();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                button2.Enabled = false;
                button1.Enabled = true;
                isrunall = true;
                if (!backgroundWorker.IsBusy)
                {
                    backgroundWorker.CancelAsync();//启动异步操作，有两种重载。将触发BackgroundWorker.DoWork事件
                }

                if (!backgroundWorker2.IsBusy)
                {
                    backgroundWorker2.CancelAsync();//启动异步操作，有两种重载。将触发BackgroundWorker.DoWork事件
                }


            }
            catch (Exception ex){
                Loghelper.WriteLog("停止拍照错误",ex);
            }
        }
    }
}

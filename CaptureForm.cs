using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using pcbaoi.Properties;
using pcbaoi.Tools;
using QTing.PLC;
using PylonC.NETSupportLibrary;
using System.Drawing.Imaging;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Stitching;
using Emgu.CV.Util;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing.Drawing2D;

namespace pcbaoi
{
    public enum SIDE
    {
        FRONT,
        BACK,
        DOUBle
    };

    public partial class CaptureForm : Form
    {
        //拍摄点位X和Y方向间隔，当前都为14mm
        private const float capturePointIntervalXInMM = 13.0f;
        private const float capturePointIntervalYInMM = 13.0f;
        //单张拍摄图片对应的物理宽度，当前为17mm
        private const float singleCaptureWidthInMM = 17.0f;
        //最终用于显示的大图每1mm对应的像素个数
        private const float pixelNumPerMM = 30.0f;
        private const double scale = 0.5;

        //记录拍了多少块板子
        int captureCount = 0;

        //AI检测模块
        private Aidemo aidemo;

        //AutoSizeFormClassnew asc = new AutoSizeFormClassnew();
        private int formStartX = 0;
        private int formStartY = 0;

        FormControl fc = null;
        //测量状态
        bool isrule = false;
        //划线状态
        bool drawline = false;
        int checknum = 0;
        //起点坐标
        Point startpoint;
        //结束坐标
        Point endpoint;
        Control control;
        //中心点坐标
        Point centerpoint;
        //变化框
        private PickBox pb = new PickBox();
        // 历史文件变量
        FileHandler filehandler;
        bool isSelected = false;
        Point mouseDownPoint;
        //子基板数量
        int Subsubstrate = 0;
        //pictureBox列表
        List<PictureBox> pictureBoxes = new List<PictureBox>();
        //打开次数
        int from;
        //原来的宽度
        int oldlastwidth = 0;
        //原来的长度
        int oldlastheight = 0;
        AutoSizeFormClass asc = new AutoSizeFormClass();
        Control controlnew;
        bool pulleyStop = false;
        bool pulleySearchStop = true;
        //A面相机名字
        public static string cameraAid;
        //B面相机名字
        public static string cameraBid;
        //A面照片数组
        public List<Bitmap> Abitmaps = new List<Bitmap>();
        List<Checkpic> Acheckpics = new List<Checkpic>();
        //B面照片数组
        public List<Bitmap> Bbitmaps = new List<Bitmap>();
        List<Checkpic> Bcheckpics = new List<Checkpic>();

        //A面拍摄任务
        BackgroundWorker backgroundWorkerA = null;
        //B面拍摄任务
        BackgroundWorker backgroundWorkerB = null;
        //检测运行结果
        BackgroundWorker R_backgroundWorker = null;
        BackgroundWorker Check_backgroudWorker = null;
        //A面运行标志
        bool isruna = false;
        //B面运行标志
        bool isrunb = false;
        //plc对应标志位
        private int D2004 = 0;
        //自定义板名
        Snowflake snowflake = new Snowflake(2);
        bool isdoubleside = false;
        bool Aend = false;
        bool Bend = false;
        long dicid;
        //保存图片路径
        string savepath;
        //A面队列
        public Queue<Bitmap> Amats = new Queue<Bitmap>();
        //B面队列
        public Queue<Bitmap> Bmats = new Queue<Bitmap>();
        //检测队列
        public Queue<PcbInfo> pcbinfos = new Queue<PcbInfo>();
        //A面大图
        public Mat Amat;
        //B面大图
        public Mat Bmat;
        //A面拍摄当前数量
        public int Aimagenum = 0;
        //B面拍摄当前数量
        public int Bimagenum = 0;
        //A面用作拍摄完成检测
        public int Arun = 0;
        //B面用作拍摄完成检测
        public int Brun = 0;
        //A面拼图完成检测
        public bool isAok = false;
        //B面拼图完成检测
        public bool isBok = false;
        //所有拍摄数量 用检测时数量比较
        public int Allnum = 0;
        //Ai检测是否完成
        public bool checkend = false;
        //A面使用 用作lock
        object objA = new object();
        //b面使用 用作lock
        object objB = new object();
        //拍摄的宽 用作翻转后计算坐标
        public int picwidth;
        //拍摄的长 用作翻转后计算坐标
        public int picheight;

        #region 相机使用模块 初始化两个相机
        private ImageProvider m_imageProvider = new ImageProvider(); /* Create one image provider. */
        private Bitmap m_bitmap = null;
        private ImageProvider m_imageProviderB = new ImageProvider(); /* Create one image provider. */
        private Bitmap m_bitmapB = null;
        /* Stops the image provider and handles exceptions. */
        public void Stop()
        {
            /* Stop the grabbing. */
            try
            {
                m_imageProvider.Stop();
                m_imageProviderB.Stop();
            }
            catch (Exception e)
            {
                Loghelper.WriteLog(m_imageProvider.GetLastErrorMessage(), e);
            }
        }
        public void Stop(string cameraid)
        {
            /* Stop the grabbing. */
            try
            {
                if (cameraid == cameraAid)
                {
                    m_imageProvider.Stop();
                }
                else
                {
                    m_imageProviderB.Stop();
                }

            }
            catch (Exception e)
            {
                Loghelper.WriteLog(m_imageProvider.GetLastErrorMessage(), e);
            }
        }

        /* Closes the image provider and handles exceptions. */
        private void CloseTheImageProvider()
        {
            /* Close the image provider. */
            try
            {
                m_imageProvider.Close();
                m_imageProviderB.Close();
            }
            catch (Exception e)
            {
                Loghelper.WriteLog(m_imageProvider.GetLastErrorMessage(), e);
            }
        }
        /* Closes the image provider and handles exceptions. */
        private void CloseTheImageProvider(string cameraid)
        {
            /* Close the image provider. */
            try
            {
                if (cameraid == cameraAid)
                {
                    m_imageProvider.Close();
                }
                else
                {
                    m_imageProviderB.Close();
                }

            }
            catch (Exception e)
            {
                Loghelper.WriteLog(m_imageProvider.GetLastErrorMessage(), e);
            }
        }

        /* Handles the event related to the occurrence of an error while grabbing proceeds. */
        private void OnGrabErrorEventCallback(Exception grabException, string additionalErrorMessage)
        {
            if (InvokeRequired)
            {
                /* If called from a different thread, we must use the Invoke method to marshal the call to the proper thread. */
                BeginInvoke(new ImageProvider.GrabErrorEventHandler(OnGrabErrorEventCallback), grabException, additionalErrorMessage);
                return;
            }

            Loghelper.WriteLog(additionalErrorMessage, grabException);
        }

        /* Handles the event related to the removal of a currently open device. */
        //A相机方法
        private void OnDeviceRemovedEventCallback()
        {
            if (InvokeRequired)
            {
                /* If called from a different thread, we must use the Invoke method to marshal the call to the proper thread. */
                BeginInvoke(new ImageProvider.DeviceRemovedEventHandler(OnDeviceRemovedEventCallback));
                return;
            }
            /* Stops the grabbing of images. */
            Stop(cameraAid);
            /* Close the image provider. */
            CloseTheImageProvider(cameraAid);
        }
        //B相机方法
        private void OnDeviceRemovedEventCallbackB()
        {
            if (InvokeRequired)
            {
                /* If called from a different thread, we must use the Invoke method to marshal the call to the proper thread. */
                BeginInvoke(new ImageProvider.DeviceRemovedEventHandler(OnDeviceRemovedEventCallbackB));
                return;
            }
            /* Stops the grabbing of images. */
            Stop(cameraBid);
            /* Close the image provider. */
            CloseTheImageProvider(cameraBid);
        }

        /* Handles the event related to a device being open. */
        private void OnDeviceOpenedEventCallback()
        {
            if (InvokeRequired)
            {
                /* If called from a different thread, we must use the Invoke method to marshal the call to the proper thread. */
                BeginInvoke(new ImageProvider.DeviceOpenedEventHandler(OnDeviceOpenedEventCallback));
                return;
            }
        }

        /* Handles the event related to a device being closed. */
        private void OnDeviceClosedEventCallback()
        {
            if (InvokeRequired)
            {
                /* If called from a different thread, we must use the Invoke method to marshal the call to the proper thread. */
                BeginInvoke(new ImageProvider.DeviceClosedEventHandler(OnDeviceClosedEventCallback));
                return;
            }
        }

        /* Handles the event related to the image provider executing grabbing. */
        private void OnGrabbingStartedEventCallback()
        {
            if (InvokeRequired)
            {
                /* If called from a different thread, we must use the Invoke method to marshal the call to the proper thread. */
                BeginInvoke(new ImageProvider.GrabbingStartedEventHandler(OnGrabbingStartedEventCallback));
                return;
            }

        }

        /* Handles the event related to an image having been taken and waiting for processing. A面拍照 */
        private void OnImageReadyEventCallback()
        {
            if (InvokeRequired)
            {
                /* If called from a different thread, we must use the Invoke method to marshal the call to the proper thread. */
                BeginInvoke(new ImageProvider.ImageReadyEventHandler(OnImageReadyEventCallback));
                return;
            }

            try
            {
                /* Acquire the image from the image provider. Only show the latest image. The camera may acquire images faster than images can be displayed*/
                ImageProvider.Image image = m_imageProvider.GetLatestImage();

                /* Check if the image has been removed in the meantime. */
                if (image != null)
                {
                    String directory = savepath + captureCount.ToString();
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }
                    /* Check if the image is compatible with the currently used bitmap. */
                    if (BitmapFactory.IsCompatible(m_bitmap, image.Width, image.Height, image.Color))
                    {
                        /* Update the bitmap with the image data. */
                        BitmapFactory.UpdateBitmap(m_bitmap, image.Buffer, image.Width, image.Height, image.Color);
                        /* To show the new image, request the display control to update itself. */
                        if (m_imageProvider.CameraId == cameraAid)
                        {

                            m_bitmap.Save(directory + "\\F" + Aimagenum.ToString() + ".jpg", ImageFormat.Jpeg);
                            Bitmap listbitmap;
                            //listbitmap = (Bitmap)singleCaptureCropAndResize(m_bitmap).Clone();
                            listbitmap = (Bitmap)m_bitmap.Clone();
                            Amats.Enqueue(listbitmap);
                            //Abitmaps.Add(listbitmap);
                            //listbitmap.Dispose();
                            pbFrontImg.Image = m_bitmap;
                            if (tbFrontOrBack.Text == "正面")
                            {
                                pbMainImg.Image = m_bitmap;
                            }


                            //listbitmap.Dispose();
                            //m_bitmap.Save("d:\\pic\\test" + ".bmp", ImageFormat.Bmp);
                            //Abitmaps[Abitmaps.Count - 1].Save("d:\\pic\\test2" + ".bmp", ImageFormat.Bmp);
                            //;
                        }
                        Aimagenum++;
                        lock (objA) {
                            Arun++;
                        }

                    }
                    else /* A new bitmap is required. */
                    {
                        BitmapFactory.CreateBitmap(out m_bitmap, image.Width, image.Height, image.Color);
                        BitmapFactory.UpdateBitmap(m_bitmap, image.Buffer, image.Width, image.Height, image.Color);
                        /* We have to dispose the bitmap after assigning the new one to the display control. */
                        Bitmap bitmap = pbFrontImg.Image as Bitmap;
                        /* Provide the display control with the new bitmap. This action automatically updates the display. */
                        if (m_imageProvider.CameraId == cameraAid)
                        {
                            m_bitmap.Save(directory + "\\F" + Aimagenum.ToString() + ".jpg", ImageFormat.Jpeg);
                            //保存相机拍摄的原始图片
                            Bitmap listbitmap;
                            //listbitmap = (Bitmap)singleCaptureCropAndResize(m_bitmap).Clone();
                            listbitmap = (Bitmap)m_bitmap.Clone();
                            Amats.Enqueue(listbitmap);
                            // Abitmaps.Add(listbitmap);
                            pbFrontImg.Image = m_bitmap;
                            if (tbFrontOrBack.Text == "正面")
                            {
                                pbMainImg.Image = m_bitmap;
                            }
                            //listbitmap.Dispose();

                            //listbitmap.Dispose();
                        }
                        if (bitmap != null)
                        {
                            /* Dispose the bitmap. */
                            bitmap.Dispose();
                        }
                        Aimagenum++;
                        lock (objA)
                        {
                            Arun++;
                        }
                    }
                    /* The processing of the image is done. Release the image buffer. */
                    // 
                    m_imageProvider.ReleaseImage();
                    /* The buffer can be used for the next image grabs. */
                }
            }
            catch (Exception e)
            {
                Loghelper.WriteLog(m_imageProvider.GetLastErrorMessage(), e);
            }
        }


        /* Handles the event related to an image having been taken and waiting for processing. B面拍照 */
        private void OnImageReadyEventCallbackB()
        {
            if (InvokeRequired)
            {
                /* If called from a different thread, we must use the Invoke method to marshal the call to the proper thread. */
                BeginInvoke(new ImageProvider.ImageReadyEventHandler(OnImageReadyEventCallbackB));
                return;
            }

            try
            {
                /* Acquire the image from the image provider. Only show the latest image. The camera may acquire images faster than images can be displayed*/
                ImageProvider.Image image = m_imageProviderB.GetLatestImage();

                /* Check if the image has been removed in the meantime. */
                if (image != null)
                {
                    String directory = savepath + captureCount.ToString(); ;
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }
                    /* Check if the image is compatible with the currently used bitmap. */
                    if (BitmapFactory.IsCompatible(m_bitmapB, image.Width, image.Height, image.Color))
                    {
                        /* Update the bitmap with the image data. */
                        BitmapFactory.UpdateBitmap(m_bitmapB, image.Buffer, image.Width, image.Height, image.Color);
                        /* To show the new image, request the display control to update itself. */

                        m_bitmapB.Save(directory + "\\B" + Bimagenum.ToString().ToString() + ".jpg", ImageFormat.Jpeg);
                        Bitmap listbitmap;
                        listbitmap = (Bitmap)m_bitmapB.Clone();
                        Bmats.Enqueue(listbitmap);
                        //Bbitmaps.Add(listbitmap);
                        pbBackImg.Image = m_bitmapB;
                        if (tbFrontOrBack.Text != "正面")
                        {
                            pbMainImg.Image = m_bitmapB;
                        }
                        Bimagenum++;
                        lock (objB)
                        {
                            Brun++;
                        }
                        //listbitmap.Dispose();
                        //m_bitmap.Save("d:\\pic\\" + DateTimeUtil.DateTimeToLongTimeStamp().ToString() + ".bmp", ImageFormat.Bmp);


                    }
                    else /* A new bitmap is required. */
                    {
                        BitmapFactory.CreateBitmap(out m_bitmapB, image.Width, image.Height, image.Color);
                        BitmapFactory.UpdateBitmap(m_bitmapB, image.Buffer, image.Width, image.Height, image.Color);
                        /* We have to dispose the bitmap after assigning the new one to the display control. */
                        Bitmap bitmap = pbBackImg.Image as Bitmap;
                        /* Provide the display control with the new bitmap. This action automatically updates the display. */
                        m_bitmapB.Save(directory + "\\B" + Bimagenum.ToString().ToString() + ".jpg", ImageFormat.Jpeg);
                        Bitmap listbitmap;
                        listbitmap = (Bitmap)m_bitmapB.Clone();
                        Bmats.Enqueue(listbitmap);
                        //Bbitmaps.Add(listbitmap);
                        pbBackImg.Image = m_bitmapB;
                        if (tbFrontOrBack.Text != "正面")
                        {
                            pbMainImg.Image = m_bitmapB;
                        }
                        //listbitmap.Dispose();


                        if (bitmap != null)
                        {
                            /* Dispose the bitmap. */
                            bitmap.Dispose();
                        }
                        Bimagenum++;
                        lock (objB)
                        {
                            Brun++;
                        }
                    }
                    /* The processing of the image is done. Release the image buffer. */
                    // 
                    m_imageProviderB.ReleaseImage();
                    /* The buffer can be used for the next image grabs. */
                }
            }
            catch (Exception e)
            {
                Loghelper.WriteLog(m_imageProviderB.GetLastErrorMessage(), e);
            }
        }

        /* Handles the event related to the image provider having stopped grabbing. */
        private void OnGrabbingStoppedEventCallback()
        {
            if (InvokeRequired)
            {
                /* If called from a different thread, we must use the Invoke method to marshal the call to the proper thread. */
                BeginInvoke(new ImageProvider.GrabbingStoppedEventHandler(OnGrabbingStoppedEventCallback));
                return;
            }

        }

        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"> 用作判断是否软件为刚打开</param>
        public CaptureForm(int i)
        {
            InitializeComponent();
            //List<DeviceEnumerator.Device> list = DeviceEnumerator.EnumerateDevices();
            pLeftToolbox.Hide();
            pBottomToolbox.Hide();
            pCenterXY.Hide();
            pPcbInfo.Hide();
            pFrontInfo.Hide();
            pPcbInfoTitle.Hide();
            pMain.Hide();
            //任务初始化
            backgroundWorkerA = new BackgroundWorker();
            backgroundWorkerA.WorkerReportsProgress = true;
            backgroundWorkerA.WorkerSupportsCancellation = true;
            backgroundWorkerA.DoWork += new DoWorkEventHandler(getstatusA);
            backgroundWorkerB = new BackgroundWorker();
            backgroundWorkerB.WorkerReportsProgress = true;
            backgroundWorkerB.WorkerSupportsCancellation = true;
            backgroundWorkerB.DoWork += new DoWorkEventHandler(getstatusB);
            R_backgroundWorker = new BackgroundWorker();
            R_backgroundWorker.WorkerReportsProgress = true;
            R_backgroundWorker.WorkerSupportsCancellation = true;
            R_backgroundWorker.DoWork += new DoWorkEventHandler(Getendstatus);
            Check_backgroudWorker = new BackgroundWorker();
            Check_backgroudWorker.WorkerReportsProgress = true;
            Check_backgroudWorker.WorkerSupportsCancellation = true;
            Check_backgroudWorker.DoWork += new DoWorkEventHandler(checkthread);
            from = i;
            cameraAid = IniFile.iniRead("CameraA", "SerialNumber");
            cameraBid = IniFile.iniRead("CameraB", "SerialNumber");

            //初始化AI检测SDK
            //bbox_t_container boxlist = new bbox_t_container();
            aidemo = new Aidemo();
            //int res = aidemo.sdkin();
            //if (res == -1)
            //{
            //    MessageBox.Show("AI加载失败");
            //}
            dicid = snowflake.nextId();
            //AITestSDK.detect_image_path("D:\\00.jpg", ref boxlist);
            //Bitmap bitTest = new Bitmap("D:\\3.jpg");
            //aidemo.savepic(bitTest, "D:\\3_res.jpg");

        }
        private void CaptureForm_Shown(object sender, EventArgs e)
        {
            if (from == 1)
            {
                Workspace workspace = new Workspace(this);
                workspace.ShowDialog();
            }
            filehandler = new FileHandler();
            filehandler.RecentFileMenu = this.toolStripMenuItem7;//指定 最近文件 的菜单值，方便动态创建文件菜单
            filehandler.UpdateMenu();
            pLeftToolbox.Show();
            pBottomToolbox.Show();
            pCenterXY.Show();
            pPcbInfo.Show();
            pFrontInfo.Show();
            pPcbInfoTitle.Show();
            pMain.Show();
            drawlineact();
            asc.RenewControlRect(pbMainImg);
            //相机初始化
            Camerasinitialization();
            if (!R_backgroundWorker.IsBusy)
            {
                R_backgroundWorker.RunWorkerAsync("线程启动");
            }
            savepath = "d:\\SavedPerCameraImages\\" + DateTime.Now.ToString("M.d") + "\\";
            while (Directory.Exists(savepath + captureCount.ToString()))
            {
                captureCount++;
            }
            Directory.CreateDirectory(savepath + captureCount.ToString());
            pbFrontImg_Click(pbFrontImg, null);
            asc.RenewControlRect(pbMainImg);
            //加载板长宽
            string selectsql = "select * from bad";
            DataTable dataTable = SQLiteHelper.GetDataTable(selectsql);
            if (dataTable.Rows.Count > 0)
            {
                tbPcbName.Text = dataTable.Rows[0]["badname"].ToString();
                tbWidth.Text = dataTable.Rows[0]["carrierplatewidth"].ToString();
                tbLenth.Text = dataTable.Rows[0]["carrierplateheight"].ToString();
                tbPcbWidth.Text = dataTable.Rows[0]["badwidth"].ToString();
                tbPcbLenth.Text = dataTable.Rows[0]["badheight"].ToString();
            }

            //图片滚轮缩放
            this.MouseWheel += PbMainImg_MouseWheel;

            pbMainImg.MouseWheel += PbMainImg_MouseWheel;
            this.MouseWheel += PbMainImg_MouseWheel;
            //运行轨道至固定宽度
           // Orbital();

        }
        private void CaptureForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                AITestSDK.dispose(); // 程序退出执行
            }
            catch (Exception er)
            {
                Loghelper.WriteLog("",er);

            }
            string deletesql = "delete from zijiban where isuse = 0";
            SQLiteHelper.ExecuteSql(deletesql);
            Stop();
            CloseTheImageProvider();
            Application.Exit();

        }
        private void PbMainImg_MouseWheel(object sender, MouseEventArgs e)
        {

            //让滑轮标识变成滚动
            pulleyStop = true;
            //如果是
            if (pulleySearchStop)
            {
                //让滑轮停止搜索
                pulleySearchStop = false;
                //触发方法
                PulleyPaging(e.Delta);
            }


        }
        //public void image
        private async void PulleyPaging(int i)
        {
            await Task.Run(() =>
            {
                //循环判断pulleyStop，
                while (pulleyStop)
                {
                    //让滑轮标识变得不在滚动，然后隔一段时间再检测
                    pulleyStop = false;
                    //暂停设置的时间，然后重新检测滑轮是否停止滚动
                    Thread.Sleep(100);
                }
            });


            //这里可以写你要执行的代码
            if (i > 0)
            {
                i = 72;
            }
            else
            {
                i = -72;
            }

            if (pbMainImg.Height + i > 300)
            {
                pbMainImg.Width = pbMainImg.Width + i;//增加picturebox的宽度
                pbMainImg.Height = pbMainImg.Height + i;
                // Console.WriteLine(pbMainImg.Width.ToString() + pbMainImg.Height.ToString());
                pbMainImg.Left = pbMainImg.Left - i / 2;//使picturebox的中心位于窗体的中心
                pbMainImg.Top = pbMainImg.Top - i / 2;//进而缩放时图片也位于窗体的中心
                //pbMainImg.Location = new Point(pbMainImg.Location.X- i / 2, pbMainImg.Location.Y - i / 2);
                oldlastheight = pbMainImg.Height;
                oldlastwidth = pbMainImg.Width;
            }

            //让滑轮变得可以触发方法
            pulleySearchStop = true;

        }

        private void CaptureForm_SizeChanged(object sender, EventArgs e)
        {
            Console.WriteLine(this.WindowState);
            if (this.WindowState == FormWindowState.Normal)
            {
                this.Width = this.formStartX;
                this.Height = this.formStartY;
                fc.Reset(this, fc);
            }
        }

        private void CaptureForm_Load(object sender, EventArgs e)
        {
            fc = new FormControl(this);
            fc.GetInit(this, fc);

            this.formStartX = this.Width;
            this.formStartY = this.Height;

        }
        #region pbMainImg方法组
        private void pbMainImg_MouseClick(object sender, MouseEventArgs e)
        {
            if (isrule)
            {
                if (checknum == 0)
                {
                    startpoint = e.Location;
                    drawline = true;
                    checknum = 1;
                }
                else
                {
                    endpoint = e.Location;
                    //MessageBox.Show("x:"+(endpoint.X - startpoint.X).ToString()+ "  y:" + (endpoint.Y - startpoint.Y).ToString());
                    float newx = (float)pbMainImg.Image.Width / (float)pbMainImg.Width;
                    float newy = (float)pbMainImg.Image.Height / (float)pbMainImg.Height;
                    Ruleresult ruleresult = new Ruleresult(startpoint, endpoint, newx, newy);
                    ruleresult.ShowDialog();
                    checknum = 0;
                    drawline = false;
                    isrule = false;
                    Rulerbt.BackColor = Color.Transparent;
                    drawlineact();
                    tableLayoutPanel10.Visible = true;
                    tableLayoutPanel11.Visible = true;
                    using (Graphics gc = pbMainImg.CreateGraphics())
                    using (Pen pen = new Pen(Color.Green))
                    {
                        //设置画笔的宽度
                        pen.Width = 1;
                        pen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
                        RectangleF rect = new RectangleF();
                        rect.Location = pbMainImg.Location;
                        rect.Size = pbMainImg.Size;
                        //确保在画图区域
                        pbMainImg.Refresh();
                        //画竖线
                        gc.DrawLine(pen, 0, 0, 0, 0);
                        //画横线
                        gc.DrawLine(pen, 0, 0, 0, 0);
                    }
                }

            }
            else
            {
                drawlineact();

            }

        }
        //图片移动方法
        private void pbMainImg_MouseMove(object sender, MouseEventArgs e)
        {
            if (isSelected && IsMouseInPanel())
            {
                PictureBox p = (PictureBox)sender;
                p.Refresh();
                this.pbMainImg.Left = this.pbMainImg.Left + (Cursor.Position.X - mouseDownPoint.X);
                this.pbMainImg.Top = this.pbMainImg.Top + (Cursor.Position.Y - mouseDownPoint.Y);
                mouseDownPoint.X = Cursor.Position.X;
                mouseDownPoint.Y = Cursor.Position.Y;
                drawlineact();
            }
            if (isrule && drawline)
            {
                using (Graphics gc = pbMainImg.CreateGraphics())
                using (Pen pen = new Pen(Color.Green))
                {
                    //设置画笔的宽度
                    pen.Width = 1;
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
                    RectangleF rect = new RectangleF();
                    rect.Location = pbMainImg.Location;
                    rect.Size = pbMainImg.Size;
                    //确保在画图区域
                    if (rect.Contains(pbMainImg.Location))
                    {
                        pbMainImg.Refresh();
                        //画竖线
                        gc.DrawLine(pen, startpoint.X, startpoint.Y, e.X, e.Y);
                    }
                }

            }



        }
        //显示坐标方法
        private void showponit(int x, int y)
        {

            label12.Text = "X: " + x.ToString();
            label13.Text = "Y: " + y.ToString();

        }
        private void pbMainImg_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                mouseDownPoint.X = Cursor.Position.X;  //注：全局变量mouseDownPoint前面已定义为Point类型

                mouseDownPoint.Y = Cursor.Position.Y;
                isSelected = true;
            }
        }
        private void pbMainImg_MouseUp(object sender, MouseEventArgs e)
        {
            isSelected = false;

        }
        private bool IsMouseInPanel()
        {
            if (this.pbMainImg.Left < PointToClient(Cursor.Position).X
            && PointToClient(Cursor.Position).X < this.pbMainImg.Left + this.pbMainImg.Width
            && this.pbMainImg.Top < PointToClient(Cursor.Position).Y
            && PointToClient(Cursor.Position).Y < this.pbMainImg.Top + this.pbMainImg.Height)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void drawlineact()
        {
            showponit((pbMainImg.Width - pbMainImg.Left * 2) / 2, (pbMainImg.Height - pbMainImg.Top * 2) / 2);
        }
        private void pbMainImg_Click(object sender, EventArgs e)
        {
            if (pictureBoxes.Count > 0)
            {
                foreach (PictureBox pictureBox in pictureBoxes)
                {
                    pictureBox.Refresh();
                }
                pictureBoxes.Clear();
            }

        }
        private void pbMainImg_SizeChanged(object sender, EventArgs e)
        {
            asc.ControlAutoSize(pbMainImg);

        }
        #endregion

        #region 菜单组
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "请选择项目文件夹";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show(this, "文件夹路径不能为空", "提示");
                    return;
                }
                Settings.Default.path = dialog.SelectedPath + "\\";

            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Workspace workspace = new Workspace(this);
            workspace.ShowDialog();

        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            isrule = false;
            drawline = false;
            checknum = 0;
            Addelement addelement = new Addelement(centerpoint);
            addelement.ShowDialog();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            //issave = true;
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {

        }
        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            Stop();
            CloseTheImageProvider();
            LightsourceForm lightsourceForm = new LightsourceForm();
            lightsourceForm.ShowDialog();
            if (tbFrontOrBack.Text == "正面")
            {
                #region camera

                m_imageProvider.GrabErrorEvent += new ImageProvider.GrabErrorEventHandler(OnGrabErrorEventCallback);
                m_imageProvider.DeviceRemovedEvent += new ImageProvider.DeviceRemovedEventHandler(OnDeviceRemovedEventCallback);
                m_imageProvider.DeviceOpenedEvent += new ImageProvider.DeviceOpenedEventHandler(OnDeviceOpenedEventCallback);
                m_imageProvider.DeviceClosedEvent += new ImageProvider.DeviceClosedEventHandler(OnDeviceClosedEventCallback);
                m_imageProvider.GrabbingStartedEvent += new ImageProvider.GrabbingStartedEventHandler(OnGrabbingStartedEventCallback);
                m_imageProvider.ImageReadyEvent += new ImageProvider.ImageReadyEventHandler(OnImageReadyEventCallback);
                m_imageProvider.GrabbingStoppedEvent += new ImageProvider.GrabbingStoppedEventHandler(OnGrabbingStoppedEventCallback);
                m_imageProvider.CameraId = cameraAid;
                uint id = m_imageProvider.GetDevice(cameraAid);
                if (id == 99)
                {
                    MessageBox.Show("未连接相机");
                }
                else
                {
                    m_imageProvider.Open(id);
                    m_imageProvider.ContinuousShot();
                }
                #endregion

            }
            else
            {
                #region camera

                m_imageProvider.GrabErrorEvent += new ImageProvider.GrabErrorEventHandler(OnGrabErrorEventCallback);
                m_imageProvider.DeviceRemovedEvent += new ImageProvider.DeviceRemovedEventHandler(OnDeviceRemovedEventCallback);
                m_imageProvider.DeviceOpenedEvent += new ImageProvider.DeviceOpenedEventHandler(OnDeviceOpenedEventCallback);
                m_imageProvider.DeviceClosedEvent += new ImageProvider.DeviceClosedEventHandler(OnDeviceClosedEventCallback);
                m_imageProvider.GrabbingStartedEvent += new ImageProvider.GrabbingStartedEventHandler(OnGrabbingStartedEventCallback);
                m_imageProvider.ImageReadyEvent += new ImageProvider.ImageReadyEventHandler(OnImageReadyEventCallback);
                m_imageProvider.GrabbingStoppedEvent += new ImageProvider.GrabbingStoppedEventHandler(OnGrabbingStoppedEventCallback);
                m_imageProvider.CameraId = cameraBid;
                uint id = m_imageProvider.GetDevice(cameraBid);
                if (id == 99)
                {
                    MessageBox.Show("未连接相机");
                }
                else
                {
                    m_imageProvider.Open(id);
                    m_imageProvider.ContinuousShot();
                }
                #endregion

            }
        }

        private void toolStripMenuItem14_Click_1(object sender, EventArgs e)
        {
            Motioncontrol motioncontrol = new Motioncontrol();
            motioncontrol.ShowDialog();
        }

        private void toolStripMenuItem15_Click(object sender, EventArgs e)
        {
            TrackForm trackForm = new TrackForm();
            trackForm.ShowDialog();
        }

        private void toolStripMenuItem16_Click(object sender, EventArgs e)
        {
            SaveFileForm saveFileForm = new SaveFileForm();
            saveFileForm.ShowDialog();
        }

        private void toolStripButtonStop_Click(object sender, EventArgs e)
        {
            Stop(); /* Stops the grabbing of images. */
            CloseTheImageProvider();
        }

        //屏蔽页面
        private void Shield_Click(object sender, EventArgs e)
        {
            ShieldForm pingbiForm = new ShieldForm();
            pingbiForm.ShowDialog();

        }
        #endregion

        #region 左边菜单栏
        private void btnCamera_Click(object sender, EventArgs e)
        {
            //选择拍照面弹窗
            Collectionform collectionform = new Collectionform(tbWidth.Text, tbLenth.Text, tbPcbWidth.Text, tbPcbLenth.Text);
            collectionform.ShowDialog();
            if (collectionform.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (!Check_backgroudWorker.IsBusy) {
                    Check_backgroudWorker.RunWorkerAsync("object argument");
                }
                Collection collection = new Collection();
                collection = collectionform.Tag as Collection;
                //根据选择面选择运行任务
                if (collection.Type == "正面")
                {
                    Abitmaps = new List<Bitmap>();
                    isdoubleside = false;
                    isrunb = false;
                    isruna = true;
                    if (!backgroundWorkerA.IsBusy)
                    {
                        backgroundWorkerA.RunWorkerAsync("object argument");//启动异步操作，有两种重载。将触发BackgroundWorker.DoWork事件
                    }
                }
                else if (collection.Type == "反面")
                {
                    Bbitmaps = new List<Bitmap>();
                    isdoubleside = false;
                    isruna = false;
                    isrunb = true;
                    if (!backgroundWorkerB.IsBusy)
                    {
                        backgroundWorkerB.RunWorkerAsync("object argument");//启动异步操作，有两种重载。将触发BackgroundWorker.DoWork事件
                    }
                }
                else
                {
                    isdoubleside = true;
                    Abitmaps = new List<Bitmap>();
                    isruna = true;
                    if (!backgroundWorkerA.IsBusy)
                    {
                        backgroundWorkerA.RunWorkerAsync("object argument");//启动异步操作，有两种重载。将触发BackgroundWorker.DoWork事件
                    }
                    Bbitmaps = new List<Bitmap>();
                    isrunb = true;
                    if (!backgroundWorkerB.IsBusy)
                    {
                        backgroundWorkerB.RunWorkerAsync("object argument");//启动异步操作，有两种重载。将触发BackgroundWorker.DoWork事件
                    }

                }
            }
        }

        private void Rulerbt_Click(object sender, EventArgs e)
        {
            isrule = true;
            Rulerbt.BackColor = Color.Blue;
            if (control != null)
            {
                control.BackColor = Color.Transparent;

            }
            control = Rulerbt;
            tableLayoutPanel10.Visible = false;
            tableLayoutPanel11.Visible = false;

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件夹";
            dialog.Filter = "图片(*.jpg;*.png)|*.jpg;*.png";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string file = dialog.FileName;
                pbMainImg.Image = Image.FromFile(file);
                if (tbFrontOrBack.Text == "正面")
                {
                    pbFrontImg.Image = Image.FromFile(file);
                }
                else
                {
                    pbBackImg.Image = Image.FromFile(file);

                }
            }
        }
        private void SububstrateAdd_Click(object sender, EventArgs e)
        {

            if (pbMainImg.Image == null)
            {
                MessageBox.Show("请先采集");
                return;
            }
            //画子基板框
            PictureBox pictureBox = new PictureBox();
            pictureBox.Name = "zijiban" + Subsubstrate.ToString();
            pictureBox.Location = new Point(200, 200);
            pictureBox.Width = 70;
            pictureBox.Height = 40;
            pictureBox.BorderStyle = BorderStyle.FixedSingle;
            pictureBox.SizeChanged += new EventHandler(pictureboxsizechange);
            pictureBox.Move += new EventHandler(pictureboxmove);
            pictureBox.BackColor = Color.Transparent;
            pictureBox.Parent = pbMainImg;
            pictureBox.MouseClick += pictureboxclick;
            pictureBox.PreviewKeyDown += picboxkey;
            pictureBox.Paint += picbox_Panit;
            pictureBox.BringToFront();
            this.pbMainImg.Controls.Add(pictureBox);

            pb.WireControl(pictureBox);
            controlnew = pictureBox;
            Subsubstrate++;
            showSubsubstratenum();

            asc = new AutoSizeFormClass();
            asc.RenewControlRect(pbMainImg);
            addpicturebox();
        }
        //进入加算法框界面
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (pbMainImg.Image == null)
            {
                MessageBox.Show("请先采集");
                return;
            }
            string selectsvaesql = "select * from zijiban where isuse= '0'";
            DataTable dataTable = SQLiteHelper.GetDataTable(selectsvaesql);
            if (dataTable.Rows.Count > 0)
            {
                MessageBox.Show("请先保存数据");
                return;
            }

            Platmake platmake = new Platmake(pbMainImg.Image);
            platmake.Show();
            this.Hide();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (pbMainImg.Image != null)
                {
                    oldlastheight = pbMainImg.Height;
                    oldlastwidth = pbMainImg.Width;
                    pbMainImg.Height = pbMainImg.Image.Height;
                    pbMainImg.Width = pbMainImg.Image.Width;
                    string selectall = "select zijiname from zijiban ";
                    DataTable dataTable = SQLiteHelper.GetDataTable(selectall);
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        string name = dataTable.Rows[i]["zijiname"].ToString();
                        foreach (Control control in pbMainImg.Controls)
                        {
                            if (control.Name == name)
                            {
                                string updatesql = string.Format("update zijiban set startx='{0}',starty='{1}',width='{2}',height='{3}',isuse = 1 where zijiname='{4}'", control.Location.X, control.Location.Y, control.Width, control.Height, name);
                                SQLiteHelper.ExecuteSql(updatesql);

                            }

                        }
                    }
                    pbMainImg.Height = oldlastheight;
                    pbMainImg.Width = oldlastwidth;
                    MessageBox.Show("保存成功");


                }



            }
            catch
            {


            }



        }
        private void Runbt_Click(object sender, EventArgs e)
        {
            RunForm runForm = new RunForm();
            runForm.Show();

            this.Hide();
        }
        private void Testcheckbt_Click(object sender, EventArgs e)
        {
            Testform testform = new Testform(pbMainImg.Image);
            testform.Show();
            this.Hide();
        }
        //出板按钮
        private void SubstrateOut_Click(object sender, EventArgs e)
        {
            substrateOut();
        }
        #endregion

        private void pbFrontImg_Click(object sender, EventArgs e)
        {
            try
            {
                pbBackImg.Refresh();
                PictureBox p = (PictureBox)sender;
                Pen pp = new Pen(Color.Blue);
                using (Graphics gc = p.CreateGraphics())
                {
                    Rectangle rect = new Rectangle();
                    rect.Location = new Point(0, 0);
                    rect.Size = new Size(pbFrontImg.Width - 3, pbFrontImg.Height - 3);
                    gc.DrawRectangle(pp, rect);

                }

                pbMainImg.Image = p.Image;
                tbFrontOrBack.Text = "正面";
                Settings.Default.frontorside = "front";
                foreach (Control control in pbMainImg.Controls)
                {
                    if (control is PictureBox)
                    {
                        pbMainImg.Controls.Remove(control);
                    }
                }
                if (pbMainImg.Image != null)
                {
                    drawpicboxall();
                    pbMainImg.Height = oldlastheight;
                    pbMainImg.Width = oldlastwidth;
                }

                //#region camera
                //if (m_imageProvider.CameraId == cameraBid)
                //{
                //    Stop();
                //    CloseTheImageProvider();
                //}
                //m_imageProvider.GrabErrorEvent += new ImageProvider.GrabErrorEventHandler(OnGrabErrorEventCallback);
                //m_imageProvider.DeviceRemovedEvent += new ImageProvider.DeviceRemovedEventHandler(OnDeviceRemovedEventCallback);
                //m_imageProvider.DeviceOpenedEvent += new ImageProvider.DeviceOpenedEventHandler(OnDeviceOpenedEventCallback);
                //m_imageProvider.DeviceClosedEvent += new ImageProvider.DeviceClosedEventHandler(OnDeviceClosedEventCallback);
                //m_imageProvider.GrabbingStartedEvent += new ImageProvider.GrabbingStartedEventHandler(OnGrabbingStartedEventCallback);
                //m_imageProvider.ImageReadyEvent += new ImageProvider.ImageReadyEventHandler(OnImageReadyEventCallback);
                //m_imageProvider.GrabbingStoppedEvent += new ImageProvider.GrabbingStoppedEventHandler(OnGrabbingStoppedEventCallback);
                //m_imageProvider.CameraId = cameraAid;
                //uint id = m_imageProvider.GetDevice(cameraAid);
                //if (id == 99)
                //{
                //    MessageBox.Show("未连接相机");
                //}
                //else
                //{
                //    m_imageProvider.Open(id);
                //    //Thread.Sleep(100);
                //    m_imageProvider.ContinuousShot();
                //}
                //#endregion


            }
            catch
            {



            }


        }

        private void pbBackImg_Click(object sender, EventArgs e)
        {
            try
            {

                pbFrontImg.Refresh();
                PictureBox p = (PictureBox)sender;
                Pen pp = new Pen(Color.Blue);
                using (Graphics gc = p.CreateGraphics())
                {
                    Rectangle rect = new Rectangle();
                    rect.Location = new Point(0, 0);
                    rect.Size = new Size(p.Width - 3, p.Height - 3);
                    gc.DrawRectangle(pp, rect);

                }

                pbMainImg.Image = p.Image;
                tbFrontOrBack.Text = "反面";
                Settings.Default.frontorside = "side";
                if (pbMainImg.Image != null)
                {
                    drawpicboxall();
                    pbMainImg.Height = oldlastheight;
                    pbMainImg.Width = oldlastwidth;
                }
                //#region camera

                //if (m_imageProvider.CameraId == cameraAid)
                //{
                //    Stop();
                //    /* Close the image provider. */
                //    CloseTheImageProvider();
                //}
                //m_imageProvider.GrabErrorEvent += new ImageProvider.GrabErrorEventHandler(OnGrabErrorEventCallback);
                //m_imageProvider.DeviceRemovedEvent += new ImageProvider.DeviceRemovedEventHandler(OnDeviceRemovedEventCallback);
                //m_imageProvider.DeviceOpenedEvent += new ImageProvider.DeviceOpenedEventHandler(OnDeviceOpenedEventCallback);
                //m_imageProvider.DeviceClosedEvent += new ImageProvider.DeviceClosedEventHandler(OnDeviceClosedEventCallback);
                //m_imageProvider.GrabbingStartedEvent += new ImageProvider.GrabbingStartedEventHandler(OnGrabbingStartedEventCallback);
                //m_imageProvider.ImageReadyEvent += new ImageProvider.ImageReadyEventHandler(OnImageReadyEventCallback);
                //m_imageProvider.GrabbingStoppedEvent += new ImageProvider.GrabbingStoppedEventHandler(OnGrabbingStoppedEventCallback);
                //m_imageProvider.CameraId = cameraBid;
                //uint id = m_imageProvider.GetDevice(cameraBid);
                //if (id == 99)
                //{
                //    MessageBox.Show("未连接相机");
                //}
                //else
                //{
                //    m_imageProvider.Open(id);
                //    //Thread.Sleep(100);
                //    m_imageProvider.ContinuousShot();
                //}

                //#endregion

            }
            catch
            {


            }



        }


        #region 子基板方法
        //子基板移动方法
        private void pictureboxmove(object sender, EventArgs e)
        {

            pbMainImg.Refresh();
            asc = new AutoSizeFormClass();
            asc.RenewControlRect(pbMainImg);
            drawlineact();


        }
        //子基板缩放方法
        private void pictureboxsizechange(object sender, EventArgs e)
        {
            pbMainImg.Refresh();
            asc = new AutoSizeFormClass();
            asc.RenewControlRect(pbMainImg);
            drawlineact();

        }
        public void showSubsubstratenum()
        {

            tbChildPcbNum.Text = Subsubstrate.ToString();

        }
        //子基板点击事
        private void pictureboxclick(object sender, MouseEventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            Pen pp = new Pen(Color.Blue);
            //区分control左键和左键
            if (e.Button == MouseButtons.Left && Control.ModifierKeys == Keys.Control)
            {
                using (Graphics gc = p.CreateGraphics())
                {
                    Rectangle rect = new Rectangle();
                    rect.Location = new Point(0, 0);
                    rect.Size = new Size(p.Width - 3, p.Height - 3);
                    gc.DrawRectangle(pp, rect);

                }
                pictureBoxes.Add(p);
                p.Focus();
            }
            else
            {
                using (Graphics gc = p.CreateGraphics())
                {
                    Rectangle rect = new Rectangle();
                    rect.Location = new Point(0, 0);
                    rect.Size = new Size(p.Width - 3, p.Height - 3);
                    gc.DrawRectangle(pp, rect);

                }
                if (pictureBoxes.Count > 0)
                {
                    foreach (PictureBox pictureBox in pictureBoxes)
                    {
                        pictureBox.Refresh();
                    }
                    pictureBoxes.Clear();


                }

                pictureBoxes.Add(p);
                p.Focus();
                controlnew = p;


            }


        }
        //子基板的键盘事件监控
        private void picboxkey(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyValue == 46)
            {
                if (pictureBoxes.Count > 0)
                {
                    foreach (PictureBox pictureBox in pictureBoxes)
                    {
                        foreach (Control c in pbMainImg.Controls)
                        {
                            if (c == pictureBox)
                            {
                                this.Controls.Remove(c);
                                pb.Remove();
                                c.Dispose();
                                GC.Collect();
                                string sql = string.Format("delete from zijiban where zijiname = '{0}'", c.Name);

                            }


                        }

                    }


                }
                asc = new AutoSizeFormClass();
                asc.RenewControlRect(pbMainImg);

            }

        }

        //添加子基板框
        public void addpicturebox()
        {
            oldlastheight = pbMainImg.Height;
            oldlastwidth = pbMainImg.Width;
            pbMainImg.Height = pbMainImg.Image.Height;
            pbMainImg.Width = pbMainImg.Image.Width;
            string insertsql = string.Format("INSERT INTO zijiban( zijiname, startx, starty, width, height, isuse,frontorside ) VALUES ( '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')", controlnew.Name, controlnew.Location.X, controlnew.Location.Y, controlnew.Width, controlnew.Height, 0, Settings.Default.frontorside);
            SQLiteHelper.ExecuteSql(insertsql);
            pbMainImg.Height = oldlastheight;
            pbMainImg.Width = oldlastwidth;

        }

        //加载画框
        private void drawpicboxall()
        {
            //            PropertyInfo pInfo = pbMainImg.GetType().GetProperty("ImageRectangle", BindingFlags.Instance |
            //BindingFlags.NonPublic);
            //            Rectangle rect = (Rectangle)pInfo.GetValue(pbMainImg, null);
            //            pbMainImg.Width = rect.Width;
            //            pbMainImg.Height = rect.Height;
            oldlastheight = pbMainImg.Height;
            oldlastwidth = pbMainImg.Width;
            pbMainImg.Height = pbMainImg.Image.Height;
            pbMainImg.Width = pbMainImg.Image.Width;
            string selectopsql = string.Format("select * from operato where frontorside = '{0}'", Settings.Default.frontorside);
            DataTable dataTable = SQLiteHelper.GetDataTable(selectopsql);
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.Name = dataTable.Rows[i]["outpicturename"].ToString();
                pictureBox.Location = new Point(Convert.ToInt32(dataTable.Rows[i]["outstartx"].ToString()), Convert.ToInt32(dataTable.Rows[i]["outstarty"].ToString()));
                pictureBox.Width = Convert.ToInt32(dataTable.Rows[i]["outwidth"].ToString());
                pictureBox.Height = Convert.ToInt32(dataTable.Rows[i]["outheight"].ToString()); ;
                pictureBox.BorderStyle = BorderStyle.FixedSingle;
                pictureBox.BackColor = Color.Transparent;
                pictureBox.Parent = pbMainImg;
                pictureBox.Paint += otherpicbox_Panit;
                this.pbMainImg.Controls.Add(pictureBox);
                //MessageBox.Show(pictureBox.Parent.Name);
                foreach (Control c in this.pbMainImg.Controls)
                {
                    if (c.Name == pictureBox.Name)
                    {
                        c.BringToFront();
                    }

                }
                if (dataTable.Rows[i]["intpicturename"].ToString() != "")
                {
                    PictureBox pictureBox2 = new PictureBox();
                    pictureBox2.Name = dataTable.Rows[i]["intpicturename"].ToString();
                    pictureBox2.Location = new Point(Convert.ToInt32(dataTable.Rows[i]["instartx"].ToString()), Convert.ToInt32(dataTable.Rows[i]["instarty"].ToString()));
                    pictureBox2.Width = Convert.ToInt32(dataTable.Rows[i]["inwidth"].ToString());
                    pictureBox2.Height = Convert.ToInt32(dataTable.Rows[i]["inheight"].ToString()); ;
                    pictureBox2.BorderStyle = BorderStyle.FixedSingle;
                    pictureBox2.BackColor = Color.Transparent;
                    pictureBox2.Parent = pictureBox;
                    pictureBox2.Paint += otherpicbox_Panit;
                    pictureBox.Controls.Add(pictureBox2);

                    //MessageBox.Show(pictureBox.Parent.Name);
                    foreach (Control c in pictureBox.Controls)
                    {
                        if (c.Name == pictureBox2.Name)
                        {
                            c.BringToFront();
                        }

                    }
                }

            }
            string selectopsql2 = string.Format("select * from zijiban where frontorside = '{0}'", Settings.Default.frontorside);
            DataTable dataTable2 = SQLiteHelper.GetDataTable(selectopsql2);
            for (int i = 0; i < dataTable2.Rows.Count; i++)
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.Name = dataTable2.Rows[i]["zijiname"].ToString();
                pictureBox.Location = new Point(Convert.ToInt32(dataTable2.Rows[i]["startx"].ToString()), Convert.ToInt32(dataTable2.Rows[i]["starty"].ToString()));
                pictureBox.Width = Convert.ToInt32(dataTable2.Rows[i]["width"].ToString());
                pictureBox.Height = Convert.ToInt32(dataTable2.Rows[i]["height"].ToString()); ;
                pictureBox.BorderStyle = BorderStyle.FixedSingle;
                pictureBox.BackColor = Color.Transparent;
                pictureBox.SizeChanged += new EventHandler(pictureboxsizechange);
                pictureBox.Move += new EventHandler(pictureboxmove);
                pictureBox.MouseClick += pictureboxclick;
                pictureBox.PreviewKeyDown += picboxkey;
                pictureBox.Paint += picbox_Panit;
                pictureBox.Parent = pbMainImg;
                this.pbMainImg.Controls.Add(pictureBox);
                //MessageBox.Show(pictureBox.Parent.Name);

                pb.WireControl(pictureBox);
                controlnew = pictureBox;
                Subsubstrate = Convert.ToInt32(dataTable2.Rows[i]["id"].ToString());
            }
        }
        #region 框的重绘方法
        private void picbox_Panit(object sender, PaintEventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            Pen pp = new Pen(Color.Yellow);
            e.Graphics.DrawRectangle(pp, e.ClipRectangle.X, e.ClipRectangle.Y,
 e.ClipRectangle.X + e.ClipRectangle.Width - 1,
e.ClipRectangle.Y + e.ClipRectangle.Height - 1);
        }
        private void otherpicbox_Panit(object sender, PaintEventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            Pen pp = new Pen(Color.FromArgb(0, 235, 6));
            e.Graphics.DrawRectangle(pp, e.ClipRectangle.X, e.ClipRectangle.Y,
 e.ClipRectangle.X + e.ClipRectangle.Width - 1,
e.ClipRectangle.Y + e.ClipRectangle.Height - 1);
        }
        #endregion
        #endregion



        //计算全图画布大小
        private void getCanvasSize(int xCaptureCount, int yCaptureCount, ref int canvasWidth, ref int canvasHeight)
        {
            canvasHeight = (int)((float)(xCaptureCount) * capturePointIntervalYInMM * pixelNumPerMM * 3);
            canvasWidth = (int)((float)(yCaptureCount) * capturePointIntervalXInMM * pixelNumPerMM * 3);
        }

        //A面获取状态
        private void getstatusA(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (true)
            {
                while (isruna)
                {
                    //判断是否取消操作 
                    if (backgroundWorkerA.CancellationPending)
                    {
                        e.Cancel = true; //这里才真正取消 
                        return;
                    }

                    run(SIDE.FRONT);
                }

            }
        }

        //B面获取状态
        private void getstatusB(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (true)
            {
                while (isrunb)
                {
                    //判断是否取消操作 
                    if (backgroundWorkerB.CancellationPending)
                    {
                        e.Cancel = true; //这里才真正取消 
                        return;
                    }
                    run(SIDE.BACK);
                }
            }
        }

        //转Double 转字节
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





        private void substrateOut()
        {
            byte[] receiveData = new byte[255];
            byte[] writeValue = new byte[4];
            writeValue = DoubleToByte(0);
            if (PLCController.Instance.IsConnected)
                PLCController.Instance.WriteData(5000, 2, writeValue, receiveData);
            double thenewvalue = 1.00;
            byte[] thenewwriteValue = new byte[2];
            thenewwriteValue[0] = (byte)(thenewvalue / Math.Pow(256, 1));
            thenewwriteValue[1] = (byte)((thenewvalue / Math.Pow(256, 0)) % 256);
            if (PLCController.Instance.IsConnected)
                PLCController.Instance.WriteData(2145, 1, thenewwriteValue, receiveData);
            receiveData = new byte[255];
            writeValue = new byte[4];
            writeValue = DoubleToByte(0);
            if (PLCController.Instance.IsConnected)
                PLCController.Instance.WriteData(5002, 2, writeValue, receiveData);
            thenewvalue = 1.00;
            thenewwriteValue = new byte[2];
            thenewwriteValue[0] = (byte)(thenewvalue / Math.Pow(256, 1));
            thenewwriteValue[1] = (byte)((thenewvalue / Math.Pow(256, 0)) % 256);
            if (PLCController.Instance.IsConnected)
                PLCController.Instance.WriteData(2147, 1, thenewwriteValue, receiveData);
        }



        //轨道宽度设置
        private void Orbital()
        {
            double value = Convert.ToDouble(IniFile.iniRead("Kwidth", "kwidth")) + Convert.ToDouble(tbPcbWidth.Text) * 1562.5;
            int registerAddress = 2124;
            int wordBit = 32;
            byte[] receiveData = new byte[255];
            value = value;
            if (wordBit == 32)
            {
                if (value > 0xffffffff)
                {
                    MessageBox.Show("超出设置范围");
                    return;
                }

                byte[] writeValue = new byte[4];
                writeValue = DoubleToByte(value);
                if (PLCController.Instance.IsConnected)
                    PLCController.Instance.WriteData(registerAddress, 2, writeValue, receiveData);
            }

            try
            {

                int[] registerBitall = { 5 };
                foreach (int i in registerBitall)
                {
                    registerAddress = 2004;
                    int registerBit = i;
                    int newvalue = 1 << registerBit;

                    int currentValue = 0;

                    receiveData = new byte[255];

                    if (registerAddress == 2004)
                    {
                        D2004 = newvalue;
                        currentValue = D2004;
                        SendValueToRegister(2004, D2004, receiveData);
                    }
                }

            }
            catch
            {


            }


        }

        //单面运行主要方法
        private void run(SIDE side)
        {
            int portData = 1131, portDataCaptureDone = 1133, portX = 0, portY = 0, portCaptureNumOtherSide = 0, portCaptureNum = 0, portCapture = 0, portMoveOut = 0;
            List<Bitmap> bitmapList = new List<Bitmap>();
            if (side == SIDE.FRONT)
            {
                portX = 3000;
                portY = 3200;
                portCaptureNumOtherSide = 5002;
                portCaptureNum = 5000;
                portCapture = 2144;
                portMoveOut = 2145;
                bitmapList = Abitmaps;
            }
            else
            {
                portX = 3400;
                portY = 3600;
                portCaptureNumOtherSide = 5000;
                portCaptureNum = 5002;
                portCapture = 2146;
                portMoveOut = 2147;
                bitmapList = Bbitmaps;
            }

            int xvalue = Convert.ToInt32(Convert.ToInt32(tbPcbWidth.Text));
            int yvalue = Convert.ToInt32(Convert.ToInt32(tbPcbLenth.Text));
            int xdifferencevalue = (Convert.ToInt32(tbWidth.Text) - Convert.ToInt32(tbPcbWidth.Text)) / 2 - 2;
            int ydifferencevalue = (Convert.ToInt32(tbLenth.Text) - Convert.ToInt32(tbPcbLenth.Text)) / 2 - 2;
            List<int> x = new List<int>();
            List<int> y = new List<int>();
            //计算X,Y方向的运行点位和拍摄数量
            if (side == SIDE.FRONT)
            {
                x = Xycoordinate.axcoordinate((int)Math.Ceiling((float)xvalue / capturePointIntervalXInMM), (int)(capturePointIntervalXInMM), xdifferencevalue);
                y = Xycoordinate.aycoordinate((int)Math.Ceiling((float)yvalue / capturePointIntervalYInMM), (int)(capturePointIntervalYInMM), ydifferencevalue);
            }
            else
            {
                x = Xycoordinate.bxcoordinate((int)Math.Ceiling((float)xvalue / capturePointIntervalXInMM), (int)(capturePointIntervalXInMM), xdifferencevalue);
                y = Xycoordinate.bycoordinate((int)Math.Ceiling((float)yvalue / capturePointIntervalYInMM), (int)(capturePointIntervalYInMM), ydifferencevalue);

            }

            byte[] receiveData = new byte[255];
            byte[] writeValueX = new byte[y.Count * 4];
            byte[] writeValueY = new byte[y.Count * 4];
            byte[] writeValue = new byte[4];
            bool cantak = true;
            int n_rows = x.Count;
            int n_cols = y.Count;
            Allnum = Allnum + n_rows * n_cols;
            Thread thread = new Thread(() => copypic(n_rows, n_cols, side));
            thread.IsBackground = true;
            while (cantak)
            {

                byte[] newreceiveData = new byte[255]; //{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
                int num = PLCController.Instance.ReadData(portData, 2, newreceiveData);
                //检测板子到位信号
                double newvalue = newreceiveData[11] * Math.Pow(256, 3) + newreceiveData[12] * Math.Pow(256, 2) + newreceiveData[9] * Math.Pow(256, 1) + newreceiveData[10];
                if (newvalue == 0.00)
                {
                    Thread.Sleep(100);
                }
                else
                {
                    thread.Start();
                    if (!isdoubleside)
                    {
                        //设置B面拍摄数量为0
                        writeValue = DoubleToByte(0);
                        if (PLCController.Instance.IsConnected)
                            PLCController.Instance.WriteData(portCaptureNumOtherSide, 2, writeValue, receiveData);
                    }
                    //循环写入点位 一次数量为y方向最大数量
                    try
                    {
                        for (int i = 0; i < x.Count; i++)
                        {
                            if (i == x.Count)
                            {
                                continue;
                            }
                            if (i % 2 == 1)
                            {
                                int k = 0;
                                for (int n = y.Count - 1; n >= 0; n--)
                                {
                                    byte[] ibuf = new byte[4];
                                    ibuf = DoubleToByte(x[i]);
                                    writeValueX[k * 4] = ibuf[0];
                                    writeValueX[k * 4 + 1] = ibuf[1];
                                    writeValueX[k * 4 + 2] = ibuf[2];
                                    writeValueX[k * 4 + 3] = ibuf[3];

                                    //Thread.Sleep(50);
                                    ibuf = DoubleToByte(y[n]);
                                    writeValueY[k * 4] = ibuf[0];
                                    writeValueY[k * 4 + 1] = ibuf[1];
                                    writeValueY[k * 4 + 2] = ibuf[2];
                                    writeValueY[k * 4 + 3] = ibuf[3];
                                    if (side == SIDE.FRONT)
                                    {
                                        Console.WriteLine("A 面X:" + i.ToString() + "Y:" + n.ToString());
                                    }
                                    else
                                    {
                                        Console.WriteLine("B 面X:" + i.ToString() + "Y:" + n.ToString());
                                    }
                                    k++;

                                }

                            }
                            else
                            {
                                for (int n = 0; n < y.Count; n++)
                                {
                                    byte[] ibuf = new byte[4];
                                    ibuf = DoubleToByte(x[i]);
                                    writeValueX[n * 4] = ibuf[0];
                                    writeValueX[n * 4 + 1] = ibuf[1];
                                    writeValueX[n * 4 + 2] = ibuf[2];
                                    writeValueX[n * 4 + 3] = ibuf[3];

                                    //Thread.Sleep(50);
                                    ibuf = DoubleToByte(y[n]);
                                    writeValueY[n * 4] = ibuf[0];
                                    writeValueY[n * 4 + 1] = ibuf[1];
                                    writeValueY[n * 4 + 2] = ibuf[2];
                                    writeValueY[n * 4 + 3] = ibuf[3];
                                    if (side == SIDE.FRONT)
                                    {
                                        Console.WriteLine("A 面X:" + i.ToString() + "Y:" + n.ToString());
                                    }
                                    else
                                    {
                                        Console.WriteLine("B 面X:" + i.ToString() + "Y:" + n.ToString());
                                    }

                                }

                            }

                            if (PLCController.Instance.IsConnected)
                                PLCController.Instance.WriteData(portX, y.Count * 2, writeValueX, receiveData);
                            Thread.Sleep(50);
                            if (PLCController.Instance.IsConnected)
                                PLCController.Instance.WriteData(portY, y.Count * 2, writeValueY, receiveData);
                            //设置拍摄数量
                            writeValue = DoubleToByte(y.Count);
                            if (PLCController.Instance.IsConnected)
                                PLCController.Instance.WriteData(portCaptureNum, 2, writeValue, receiveData);
                            double value = 1.00;
                            byte[] newwriteValue = new byte[2];
                            newwriteValue[0] = (byte)(value / Math.Pow(256, 1));
                            newwriteValue[1] = (byte)((value / Math.Pow(256, 0)) % 256);
                            //发送开始拍摄信号
                            if (PLCController.Instance.IsConnected)
                                PLCController.Instance.WriteData(portCapture, 1, newwriteValue, receiveData);
                            bool isrun = true;
                            while (isrun)
                            {
                                ////检测拍完信号
                                newreceiveData = new byte[255]; //{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
                                num = PLCController.Instance.ReadData(portDataCaptureDone, 2, newreceiveData);
                                newvalue = newreceiveData[11] * Math.Pow(256, 3) + newreceiveData[12] * Math.Pow(256, 2) + newreceiveData[9] * Math.Pow(256, 1) + newreceiveData[10];
                                if (newvalue == 0.00)
                                {
                                    Loghelper.WriteLog(portDataCaptureDone.ToString() + "取到0时间:" + System.DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss-ffff"));
                                    while (isrun)
                                    {
                                        ////检测拍完信号
                                        newreceiveData = new byte[255]; //{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
                                        num = PLCController.Instance.ReadData(portDataCaptureDone, 2, newreceiveData);
                                        newvalue = newreceiveData[11] * Math.Pow(256, 3) + newreceiveData[12] * Math.Pow(256, 2) + newreceiveData[9] * Math.Pow(256, 1) + newreceiveData[10];
                                        if (newvalue == 1.00)
                                        {
                                            isrun = false;
                                            Loghelper.WriteLog(portDataCaptureDone.ToString() + "取到1时间:" + System.DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss-ffff"));

                                        }
                                        Thread.Sleep(20);

                                    }

                                }
                                Thread.Sleep(20);
                            }

                        }
                    }
                    catch (Exception exp) {
                        Loghelper.WriteLog("发送位置错误", exp);

                    }

                    Console.WriteLine("拍摄完成");
                    cantak = false;

                }

            }
            //if (side == SIDE.FRONT)
            //{
            //    Aend = true;
            //    isruna = false;
            //    //Abitmaps.Clear();
            //}
            //else {
            //    Bend = true;
            //    isrunb = false;
            //    //Bbitmaps.Clear();

            //}

            while (true)
            {
                if (side == SIDE.FRONT)
                {
                    if (isAok)
                    {
                        try
                        {
                            Image<Bgr, Byte> _image = null;
                            Mat smallmat = new Mat();
                            CvInvoke.Resize(Amat, smallmat, new Size(), scale, scale);
                            _image = smallmat.ToImage<Bgr, Byte>();
                            Bitmap allbitmap = _image.Bitmap;
                            allbitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
                            allbitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);
                            allbitmap.Save(savepath + captureCount.ToString() + "\\Ftotal.jpg", ImageFormat.Jpeg);
                            Amat.Dispose();
                            //allbitmap = ImageManger.KiResizeImage(allbitmap, 2);
                            Savepic(allbitmap, side);

                            Bitmap newbitmap;
                            newbitmap = (Bitmap)allbitmap.Clone();

                            pbFrontImg.Image = allbitmap;
                            if (Settings.Default.frontorside == "front")
                            {
                                Bitmap MainImg = (Bitmap)newbitmap.Clone();
                                pbMainImg.Image = MainImg;
                                MainImg = null;
                            }


                            // Savepic(newbitmap, side);
                            //SendresulttoOther(newbitmap, captureCount.ToString(), checkpics);
                            _image.Dispose();
                            allbitmap = null;
                            newbitmap.Dispose();
                            bitmapList = null;
                            smallmat.Dispose();
                            Aend = true;
                            isruna = false;
                            Abitmaps.Clear();
                            GC.Collect();

                        }
                        catch (Exception exp)
                        {

                            Loghelper.WriteLog("结果错误:", exp);

                        }
                        break;
                    }
                }
                else
                {
                    if (isBok)
                    {
                        try
                        {
                            Image<Bgr, Byte> _image = null;
                            Mat smallmat = new Mat();
                            CvInvoke.Resize(Bmat, smallmat, new Size(), scale, scale);
                            _image = smallmat.ToImage<Bgr, Byte>();
                            Bitmap allbitmap = _image.Bitmap;
                            //allbitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);
                            allbitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
                            allbitmap.Save(savepath + captureCount.ToString() + "\\Btotal.jpg", ImageFormat.Jpeg);
                            Bmat.Dispose();
                            //allbitmap = ImageManger.KiResizeImage(allbitmap, 2);
                            Bitmap newbitmap;
                            newbitmap = (Bitmap)allbitmap.Clone();
                            pbBackImg.Image = allbitmap;
                            if (Settings.Default.frontorside == "side")
                            {
                                Bitmap MainImg = (Bitmap)newbitmap.Clone();
                                pbMainImg.Image = MainImg;
                                MainImg = null;
                            }
                            Savepic(newbitmap, side);
                            //SendresulttoOther(newbitmap, captureCount.ToString(), checkpics);
                            _image.Dispose();
                            allbitmap = null;
                            newbitmap.Dispose();
                            bitmapList = null;
                            Bend = true;
                            isrunb = false;
                            Bbitmaps.Clear();
                            GC.Collect();

                        }
                        catch (Exception exp)
                        {

                            Loghelper.WriteLog("结果错误:", exp);

                        }
                        break;
                    }
                }
                Thread.Sleep(10);
            }
            Thread.Sleep(300);
            #region 老同步拼图代码
            //根据数组进行拼图
            //if (bitmapList.Count > 0)
            //{
            //    try
            //    {
            //        Loghelper.WriteLog("拍摄数量：" + bitmapList.Count.ToString());
            //        //int cavasWidthInPixel = 0;
            //        //int cavasHeightInPixel = 0;
            //        //getCanvasSize(x.Count, y.Count, ref cavasWidthInPixel, ref cavasHeightInPixel);
            //        //List<Checkpic> checkpics = new List<Checkpic>();
            //        //老方法
            //        // Mat totalCanvas = new Mat(cavasHeightInPixel, cavasWidthInPixel, DepthType.Cv8U, 3);
            //        Mat dst = null;
            //        //拼图开始时间
            //        long intime = DateTimeUtil.DateTimeToLongTimeStamp();
            //        if (bitmapList.Count == x.Count * y.Count)
            //        {
            //            #region //拼图老方法
            //            //for (int i = 0; i < x.Count; i++)
            //            //{
            //            //    for (int j = 0; j < y.Count; j++)
            //            //    {
            //            //        int count = i * y.Count + j;

            //            //        Bitmap bitmap = bitmapList[count];
            //            //        //bitmap.Save("d:\\SavedPerCameraImages\\B" + count.ToString() + ".jpg", ImageFormat.Jpeg);

            //            //        // bitmap.Save("d:\\newpic\\" + count.ToString() + ".bmp", ImageFormat.Bmp);
            //            //        Checkpic checkpic = aidemo.savepic(bitmap, j * bitmap.Width, i * bitmap.Height);
            //            //        if (side == SIDE.FRONT)
            //            //        {
            //            //            Acheckpics.Add(checkpic);
            //            //        }
            //            //        else {
            //            //            Bcheckpics.Add(checkpic);
            //            //        }
            //            //        Emgu.CV.Image<Bgr, Byte> currentFrame = new Emgu.CV.Image<Bgr, Byte>(bitmap);
            //            //        Mat invert = new Mat();
            //            //        CvInvoke.BitwiseAnd(currentFrame, currentFrame, invert);

            //            //        AoiAi.addPatch(totalCanvas.Ptr, invert.Ptr, (float)j * bitmap.Width, (float)i * bitmap.Height);
            //            //        //totalCanvas.Save("d:\\SavedPerCameraImages\\bb" + i.ToString() + "_" + j.ToString() + ".jpg");
            //            //        invert.Dispose();
            //            //    }
            //            //}
            //            //if (side == SIDE.FRONT)
            //            //{
            //            //    totalCanvas.Save("d:\\SavedPerCameraImages\\" + captureCount.ToString() + "\\Ftotal.jpg");
            //            //}
            //            //else {
            //            //    totalCanvas.Save("d:\\SavedPerCameraImages\\" + captureCount.ToString() + "\\Btotal.jpg");
            //            //}
            //            #endregion
            //            double or_hl = 0.23; // lower bound for horizontal overlap ratio
            //            double or_hu = 0.24; // upper
            //            double or_vl = 0.065; // vertical
            //            double or_vu = 0.08;
            //            double dr_hu = 0.01; // upper bound for horizontal drift ratio
            //            double dr_vu = 0.01; // 
            //            int n_rows = x.Count;
            //            int n_cols = y.Count;

            //            Rectangle roi0 = new Rectangle(); //上一行第一张的区域
            //                                              // first row 
            //            Rectangle roi = new Rectangle(); // 左对齐的参考的区域
            //                                             // first row 

            //            for (int col = 0; col < n_cols; ++col)
            //            {

            //                Emgu.CV.Image<Bgr, Byte> currentFrame = new Emgu.CV.Image<Bgr, Byte>(bitmapList[col]);
            //                Bitmap bitmap = bitmapList[col];

            //                Mat img = new Mat();
            //                CvInvoke.BitwiseAnd(currentFrame, currentFrame, img);

            //                if (col == 0)
            //                {
            //                    roi0 = new Rectangle(Convert.ToInt32(img.Cols * (n_cols - 1) * dr_hu), Convert.ToInt32(img.Rows * (n_rows - 1) * dr_vu), img.Cols, img.Rows);
            //                    dst = new Mat(Convert.ToInt32(img.Rows * (n_rows + (n_rows - 1) * (dr_vu * 2 - or_vl))), Convert.ToInt32(img.Cols * (n_cols + (n_cols - 1) * (dr_hu * 2 - or_hl))), img.Depth, 3); // 第一张图不要0,0 最好留一些像素
            //                    roi = roi0;
            //                }
            //                else
            //                {
            //                    AoiAi.stitchv2(dst.Ptr, roi, img.Ptr, ref roi, (int)AoiAi.side.left, Convert.ToInt32(img.Cols * or_hl), Convert.ToInt32(img.Cols * or_hu), Convert.ToInt32(img.Rows * dr_vu));
            //                }
            //                //AoiAi.addPatch(dst.Ptr,img.Ptr, roi.X, roi.Y);
            //                AoiAi.copy_to(dst.Ptr, img.Ptr, roi);
            //                Checkpic checkpic = aidemo.savepic(bitmap, roi.X, roi.Y);
            //                if (side == SIDE.FRONT)
            //                {
            //                    Acheckpics.Add(checkpic);
            //                }
            //                else
            //                {
            //                    Bcheckpics.Add(checkpic);
            //                }
            //                img.Dispose();
            //                currentFrame.Dispose();
            //                bitmap.Dispose();
            //                #region 这里去掉
            //                //CvInvoke.NamedWindow("AJpg", NamedWindowType.Normal); //创建一个显示窗口
            //                //CvInvoke.Imshow("AJpg", dst);

            //                //char key = (char)CvInvoke.WaitKey(1);
            //                //if (key == 0x1b || key == 'q') continue;
            //                #endregion 这里去掉

            //            }

            //            // other rows
            //            for (int row = 1; row < n_rows; ++row)
            //            {
            //                for (int col = 0; col < n_cols; ++col)
            //                {
            //                    Emgu.CV.Image<Bgr, Byte> currentFrame = new Emgu.CV.Image<Bgr, Byte>(bitmapList[n_cols * row + col]);
            //                    Bitmap bitmap = bitmapList[n_cols * row + col];
            //                    //Checkpic checkpic = aidemo.savepic(bitmap, col * bitmap.Width, row*bitmap.Height);
            //                    //if (side == SIDE.FRONT)
            //                    //{
            //                    //    Acheckpics.Add(checkpic);
            //                    //}
            //                    //else
            //                    //{
            //                    //    Bcheckpics.Add(checkpic);
            //                    //}
            //                    Mat img = new Mat();
            //                    CvInvoke.BitwiseAnd(currentFrame, currentFrame, img);
            //                    //std::cout << n_cols * row + col << "\n";

            //                    if (col == 0)
            //                    {
            //                        AoiAi.stitchv2(dst.Ptr, roi0, img.Ptr, ref roi0, (int)AoiAi.side.up, Convert.ToInt32(img.Cols * or_vl), Convert.ToInt32(img.Cols * or_vu), Convert.ToInt32(img.Rows * dr_hu));
            //                        roi = roi0;
            //                    }
            //                    else
            //                    {
            //                        AoiAi.stitchv2(dst.Ptr, roi, img.Ptr, ref roi, (int)AoiAi.side.left, Convert.ToInt32(img.Cols * or_hl), Convert.ToInt32(img.Cols * or_hu), Convert.ToInt32(img.Rows * dr_vu), (int)AoiAi.side.up, Convert.ToInt32(img.Rows * or_vl), Convert.ToInt32(img.Rows * or_vu), Convert.ToInt32(img.Cols * dr_hu));
            //                    }
            //                    AoiAi.copy_to(dst.Ptr, img.Ptr, roi);
            //                    Checkpic checkpic = aidemo.savepic(bitmap, roi.X, roi.Y);
            //                    if (side == SIDE.FRONT)
            //                    {
            //                        Acheckpics.Add(checkpic);
            //                    }
            //                    else
            //                    {
            //                        Bcheckpics.Add(checkpic);
            //                    }
            //                    img.Dispose();
            //                    currentFrame.Dispose();
            //                    bitmap.Dispose();
            //                    #region 这里去掉
            //                    //CvInvoke.NamedWindow("AJpg", NamedWindowType.Normal); //创建一个显示窗口
            //                    //CvInvoke.Imshow("AJpg", dst);
            //                    //char key = (char)CvInvoke.WaitKey(1);
            //                    //if (key == 0x1b || key == 'q') continue;
            //                    #endregion 这里去掉
            //                }
            //            }
            //            #region 弃用代码
            //            //for (int row = 0; row < n_rows; row++) // 这里就有问题
            //            //{
            //            //    if (row == 0)
            //            //    {
            //            //        for (int col = 0; col < n_cols; ++col)
            //            //        {
            //            //            int count = row * n_cols + col;

            //            //            Bitmap bitmap = bitmapList[count];
            //            //            bitmap.Save("d:\\SavedPerCameraImages\\" + captureCount.ToString() + "\\Fa" + count.ToString() + ".jpg");
            //            //            //bitmap.Save("d:\\SavedPerCameraImages\\B" + count.ToString() + ".jpg", ImageFormat.Jpeg);

            //            //            // bitmap.Save("d:\\newpic\\" + count.ToString() + ".bmp", ImageFormat.Bmp);
            //            //            Checkpic checkpic = aidemo.savepic(bitmap, col * bitmap.Width, row * bitmap.Height);
            //            //            if (side == SIDE.FRONT)
            //            //            {
            //            //                Acheckpics.Add(checkpic);
            //            //            }
            //            //            else
            //            //            {
            //            //                Bcheckpics.Add(checkpic);
            //            //            }
            //            //            Emgu.CV.Image<Bgr, Byte> currentFrame = new Emgu.CV.Image<Bgr, Byte>(bitmap);
            //            //            Mat invert = new Mat();
            //            //            CvInvoke.BitwiseAnd(currentFrame, currentFrame, invert);
            //            //            //Mat img = new Mat(fileList[col], Emgu.CV.CvEnum.LoadImageType.AnyColor);
            //            //            if (col == 0)
            //            //            {
            //            //                roi0 = new Rectangle(Convert.ToInt32(invert.Cols * (n_cols - 1) * dr_hu), Convert.ToInt32(invert.Rows * (n_rows - 1) * dr_vu), invert.Cols, invert.Rows);
            //            //                dst = new Mat(Convert.ToInt32(invert.Rows * (n_rows + (n_rows - 1) * (dr_vu * 2 - or_vl))), Convert.ToInt32(invert.Cols * (n_cols + (n_cols - 1) * (dr_hu * 2 - or_hl))), invert.Depth, 3); // 第一张图不要0,0 最好留一些像素
            //            //                roi = roi0;
            //            //            }
            //            //            else
            //            //            {
            //            //                AoiAi.stitchv2(dst.Ptr, roi, invert.Ptr, ref roi, (int)AoiAi.side.left, Convert.ToInt32(invert.Cols * or_hl), Convert.ToInt32(invert.Cols * or_hu), Convert.ToInt32(invert.Rows * dr_vu));
            //            //            }

            //            //            AoiAi.copy_to(dst.Ptr, invert.Ptr, roi);
            //            //            //AoiAi.addPatch(totalCanvas.Ptr, invert.Ptr, (float)j * bitmap.Width, (float)i * bitmap.Height);
            //            //            //totalCanvas.Save("d:\\SavedPerCameraImages\\bb" + i.ToString() + "_" + j.ToString() + ".jpg");
            //            //            invert.Dispose();



            //            //        }


            //            //    }
            //            //    else
            //            //    {

            //            //        for (int col = 0; col < n_cols; ++col)
            //            //        {
            //            //            int count = row * n_cols + col;

            //            //            Bitmap bitmap = bitmapList[count];
            //            //            bitmap.Save("d:\\SavedPerCameraImages\\" + captureCount.ToString() + "\\Fa"+ count.ToString()+ ".jpg");
            //            //            //bitmap.Save("d:\\SavedPerCameraImages\\B" + count.ToString() + ".jpg", ImageFormat.Jpeg);

            //            //            // bitmap.Save("d:\\newpic\\" + count.ToString() + ".bmp", ImageFormat.Bmp);
            //            //            Checkpic checkpic = aidemo.savepic(bitmap, col * bitmap.Width, row * bitmap.Height);
            //            //            if (side == SIDE.FRONT)
            //            //            {
            //            //                Acheckpics.Add(checkpic);
            //            //            }
            //            //            else
            //            //            {
            //            //                Bcheckpics.Add(checkpic);
            //            //            }
            //            //            Emgu.CV.Image<Bgr, Byte> currentFrame = new Emgu.CV.Image<Bgr, Byte>(bitmap);
            //            //            Mat invert = new Mat();
            //            //            CvInvoke.BitwiseAnd(currentFrame, currentFrame, invert);
            //            //            //std::cout << n_cols * row + col << "\n";

            //            //            if (col == 0)
            //            //            {
            //            //                AoiAi.stitchv2(dst.Ptr, roi0, invert.Ptr, ref roi0, (int)AoiAi.side.up, Convert.ToInt32(invert.Cols * or_vl), Convert.ToInt32(invert.Cols * or_vu), Convert.ToInt32(invert.Rows * dr_hu));
            //            //                roi = roi0;
            //            //            }
            //            //            else
            //            //            {
            //            //                AoiAi.stitchv2(dst.Ptr, roi, invert.Ptr, ref roi, (int)AoiAi.side.left, Convert.ToInt32(invert.Cols * or_hl), Convert.ToInt32(invert.Cols * or_hu), Convert.ToInt32(invert.Rows * dr_vu), (int)AoiAi.side.up, Convert.ToInt32(invert.Rows * or_vl), Convert.ToInt32(invert.Rows * or_vu), Convert.ToInt32(invert.Cols * dr_hu));
            //            //            }
            //            //            AoiAi.copy_to(dst.Ptr, invert.Ptr, roi);
            //            //        }
            //            //    }
            //            //}
            //            #endregion

            //        }


            //        Image<Bgr, Byte> _image = dst.ToImage<Bgr, Byte>();
            //        Bitmap allbitmap = _image.Bitmap;
            //        if (side == SIDE.FRONT)
            //        {
            //            allbitmap.Save(savepath + captureCount.ToString() + "\\Ftotal.jpg",ImageFormat.Jpeg);
            //        }
            //        else
            //        {
            //            allbitmap.Save(savepath + captureCount.ToString() + "\\Btotal.jpg",ImageFormat.Jpeg);
            //        }
            //        allbitmap =KiResizeImage(allbitmap,2);
            //        Bitmap newbitmap;
            //        newbitmap = (Bitmap)allbitmap.Clone();
            //        pbMainImg.Image = allbitmap;
            //        Savepic(newbitmap, side);
            //        //SendresulttoOther(newbitmap, captureCount.ToString(), checkpics);
            //        _image.Dispose();
            //        allbitmap = null;
            //        newbitmap.Dispose();
            //        dst.Dispose();
            //        bitmapList = null;
            //        if (side == SIDE.FRONT)
            //        {
            //            Aend = true;
            //            Abitmaps.Clear();
            //        }
            //        else
            //        {
            //            Bend = true;
            //            Bbitmaps.Clear();
            //        }
            //        GC.Collect();
            //        long endtime = DateTimeUtil.DateTimeToLongTimeStamp();
            //        long gettime = endtime - intime;
            //        Console.WriteLine("拼图完成" + gettime);

            //    }
            //    catch (Exception e)
            //    {
            //        Loghelper.WriteLog("报错了", e);
            //    }
            //}
            #endregion
            if (checkend)
            {
                if (!isdoubleside)
                {
                    Console.WriteLine("发送出板信号");
                    substrateOut(); //暂时强制出板

                }
                else
                {
                    Console.WriteLine("发送出板信号");
                    //发送出出板信号
                    double thenewvalue = 1.00;
                    byte[] thenewwriteValue = new byte[2];
                    thenewwriteValue[0] = (byte)(thenewvalue / Math.Pow(256, 1));
                    thenewwriteValue[1] = (byte)((thenewvalue / Math.Pow(256, 0)) % 256);
                    if (PLCController.Instance.IsConnected)
                        PLCController.Instance.WriteData(portMoveOut, 1, thenewwriteValue, receiveData);
                }
            }
            else {

                Thread.Sleep(200);
            }

        }

        public void copypic(int n_rows, int n_cols, SIDE side)
        {
            Rectangle roi0 = new Rectangle(); //上一行第一张的区域
                                              // first row 
            Rectangle roi = new Rectangle(); // 左对齐的参考的区域
            int row = 0;
            int col = 0;
            double or_hl = 0.23; // lower bound for horizontal overlap ratio
            double or_hu = 0.24; // upper
            double or_vl = 0.065; // vertical
            double or_vu = 0.08;
            double dr_hu = 0.01; // upper bound for horizontal drift ratio
            double dr_vu = 0.01; //
            if (side == SIDE.FRONT)
            {
                while (true)
                {
                    lock (Amats)
                    {
                        try
                        {
                            if (row == n_rows)
                            {
                                isAok = true;
                                break;
                            }
                            if (Amats.Count > 0)
                            {

                                if (row == 0)
                                {
                                    Bitmap bitmap = Amats.Dequeue();
                                    Emgu.CV.Image<Bgr, Byte> currentFrame = new Emgu.CV.Image<Bgr, Byte>(bitmap);


                                    Mat img = new Mat();
                                    CvInvoke.BitwiseAnd(currentFrame, currentFrame, img);

                                    if (col == 0)
                                    {
                                        roi0 = new Rectangle(Convert.ToInt32(img.Cols * (n_cols - 1) * dr_hu), Convert.ToInt32(img.Rows * (n_rows - 1) * dr_vu), img.Cols, img.Rows);
                                        Amat = new Mat(Convert.ToInt32(img.Rows * (n_rows + (n_rows - 1) * (dr_vu * 2 - or_vl))), Convert.ToInt32(img.Cols * (n_cols + (n_cols - 1) * (dr_hu * 2 - or_hl))), img.Depth, 3); // 第一张图不要0,0 最好留一些像素
                                        roi = roi0;
                                    }
                                    else
                                    {
                                        AoiAi.stitchv2(Amat.Ptr, roi, img.Ptr, ref roi, (int)AoiAi.side.left, Convert.ToInt32(img.Cols * or_hl), Convert.ToInt32(img.Cols * or_hu), Convert.ToInt32(img.Rows * dr_vu));
                                    }
                                    //AoiAi.addPatch(dst.Ptr,img.Ptr, roi.X, roi.Y);
                                    AoiAi.copy_to(Amat.Ptr, img.Ptr, roi);

                                    PcbInfo pcbinfo = new PcbInfo();
                                    pcbinfo.Bitmap = Aidemo.Bitmap2Byte(bitmap);
                                    pcbinfo.Obj = side;
                                    pcbinfo.Rectangle = roi;
                                    lock (pcbinfos)
                                    {
                                        pcbinfos.Enqueue(pcbinfo);
                                    }
                                    //Checkpic checkpic = aidemo.savepic(bitmap, roi.X, roi.Y);
                                    //Acheckpics.Add(checkpic);
                                    img.Dispose();
                                    currentFrame.Dispose();
                                    bitmap.Dispose();
                                    roi0 = roi;
                                    col++;


                                }
                                else
                                {

                                    Bitmap bitmap = Amats.Dequeue();
                                    Emgu.CV.Image<Bgr, Byte> currentFrame = new Emgu.CV.Image<Bgr, Byte>(bitmap);


                                    Mat img = new Mat();
                                    CvInvoke.BitwiseAnd(currentFrame, currentFrame, img);
                                    if (row % 2 == 1)
                                    {
                                        if (col == 0)
                                        {
                                            AoiAi.stitchv2(Amat.Ptr, roi0, img.Ptr, ref roi0, (int)AoiAi.side.up, Convert.ToInt32(img.Cols * or_vl), Convert.ToInt32(img.Cols * or_vu), Convert.ToInt32(img.Rows * dr_hu));
                                            roi = roi0;
                                        }
                                        else
                                        {
                                            AoiAi.stitchv2(Amat.Ptr, roi, img.Ptr, ref roi, (int)AoiAi.side.right, Convert.ToInt32(img.Cols * or_hl), Convert.ToInt32(img.Cols * or_hu), Convert.ToInt32(img.Rows * dr_vu), (int)AoiAi.side.up, Convert.ToInt32(img.Rows * or_vl), Convert.ToInt32(img.Rows * or_vu), Convert.ToInt32(img.Cols * dr_hu));
                                        }
                                    }
                                    else
                                    {
                                        if (col == 0)
                                        {
                                            AoiAi.stitchv2(Amat.Ptr, roi0, img.Ptr, ref roi0, (int)AoiAi.side.up, Convert.ToInt32(img.Cols * or_vl), Convert.ToInt32(img.Cols * or_vu), Convert.ToInt32(img.Rows * dr_hu));
                                            roi = roi0;
                                        }
                                        else
                                        {
                                            AoiAi.stitchv2(Amat.Ptr, roi, img.Ptr, ref roi, (int)AoiAi.side.left, Convert.ToInt32(img.Cols * or_hl), Convert.ToInt32(img.Cols * or_hu), Convert.ToInt32(img.Rows * dr_vu), (int)AoiAi.side.up, Convert.ToInt32(img.Rows * or_vl), Convert.ToInt32(img.Rows * or_vu), Convert.ToInt32(img.Cols * dr_hu));
                                        }

                                    }
                                    //std::cout << n_cols * row + col << "\n";

                                    AoiAi.copy_to(Amat.Ptr, img.Ptr, roi);
                                    PcbInfo pcbinfo = new PcbInfo();
                                    pcbinfo.Bitmap = Aidemo.Bitmap2Byte(bitmap);
                                    pcbinfo.Obj = side;
                                    pcbinfo.Rectangle = roi;
                                    lock (pcbinfos)
                                    {
                                        pcbinfos.Enqueue(pcbinfo);
                                    }
                                    //Checkpic checkpic = aidemo.savepic(bitmap, roi.X, roi.Y);
                                    //Acheckpics.Add(checkpic);
                                    img.Dispose();
                                    currentFrame.Dispose();
                                    bitmap.Dispose();
                                    roi0 = roi;
                                    //#region 这里去掉
                                    //CvInvoke.NamedWindow("AJpg", NamedWindowType.Normal); //创建一个显示窗口
                                    //CvInvoke.Imshow("AJpg", dst);
                                    //char key = (char)CvInvoke.WaitKey(1);
                                    //if (key == 0x1b || key == 'q') continue;
                                    //#endregion 这里去掉
                                    col++;

                                }
                                if (col == n_cols)
                                {
                                    col = 0;
                                    row++;

                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            Loghelper.WriteLog("拼图检测错误:", ex);


                        }


                    }
                    Thread.Sleep(10);

                }

            }
            else
            {
                while (true)
                {
                    lock (Bmats)
                    {
                        try
                        {
                            if (row == n_rows)
                            {
                                isBok = true;
                                break;
                            }
                            if (Bmats.Count > 0)
                            {

                                if (row == 0)
                                {
                                    Bitmap bitmap = Bmats.Dequeue();
                                    bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);
                                    Emgu.CV.Image<Bgr, Byte> currentFrame = new Emgu.CV.Image<Bgr, Byte>(bitmap);


                                    Mat img = new Mat();
                                    CvInvoke.BitwiseAnd(currentFrame, currentFrame, img);

                                    if (col == 0)
                                    {
                                        roi0 = new Rectangle(Convert.ToInt32(img.Cols * (n_cols - 1) * dr_hu), Convert.ToInt32(img.Rows * (n_rows - 1) * dr_vu), img.Cols, img.Rows);
                                        Bmat = new Mat(Convert.ToInt32(img.Rows * (n_rows + (n_rows - 1) * (dr_vu * 2 - or_vl))), Convert.ToInt32(img.Cols * (n_cols + (n_cols - 1) * (dr_hu * 2 - or_hl))), img.Depth, 3); // 第一张图不要0,0 最好留一些像素
                                        picwidth = Bmat.Cols;
                                        picheight = Bmat.Rows;
                                        roi = roi0;
                                    }
                                    else
                                    {
                                        AoiAi.stitchv2(Bmat.Ptr, roi, img.Ptr, ref roi, (int)AoiAi.side.left, Convert.ToInt32(img.Cols * or_hl), Convert.ToInt32(img.Cols * or_hu), Convert.ToInt32(img.Rows * dr_vu));
                                    }
                                    if (roi.Y < 0)
                                    {
                                        roi.Y = 0;
                                    }
                                    //AoiAi.addPatch(dst.Ptr,img.Ptr, roi.X, roi.Y);
                                    AoiAi.copy_to(Bmat.Ptr, img.Ptr, roi);
                                    PcbInfo pcbinfo = new PcbInfo();
                                    pcbinfo.Bitmap = Aidemo.Bitmap2Byte(bitmap);
                                    pcbinfo.Obj = side;
                                    pcbinfo.Rectangle = roi;
                                    lock (pcbinfos)
                                    {
                                        pcbinfos.Enqueue(pcbinfo);
                                    }
                                    //Checkpic checkpic = aidemo.savepic(bitmap, roi.X, roi.Y);
                                    //Bcheckpics.Add(checkpic);
                                    img.Dispose();
                                    currentFrame.Dispose();
                                    bitmap.Dispose();
                                    roi0 = roi;
                                    col++;


                                }
                                else
                                {

                                    Bitmap bitmap = Bmats.Dequeue();
                                    bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);
                                    Emgu.CV.Image<Bgr, Byte> currentFrame = new Emgu.CV.Image<Bgr, Byte>(bitmap);


                                    Mat img = new Mat();
                                    CvInvoke.BitwiseAnd(currentFrame, currentFrame, img);
                                    //std::cout << n_cols * row + col << "\n";
                                    if (row % 2 == 1)
                                    {
                                        if (col == 0)
                                        {
                                            AoiAi.stitchv2(Bmat.Ptr, roi0, img.Ptr, ref roi0, (int)AoiAi.side.up, Convert.ToInt32(img.Cols * or_vl), Convert.ToInt32(img.Cols * or_vu), Convert.ToInt32(img.Rows * dr_hu));
                                            roi = roi0;
                                        }
                                        else
                                        {
                                            AoiAi.stitchv2(Bmat.Ptr, roi, img.Ptr, ref roi, (int)AoiAi.side.right, Convert.ToInt32(img.Cols * or_hl), Convert.ToInt32(img.Cols * or_hu), Convert.ToInt32(img.Rows * dr_vu), (int)AoiAi.side.up, Convert.ToInt32(img.Rows * or_vl), Convert.ToInt32(img.Rows * or_vu), Convert.ToInt32(img.Cols * dr_hu));
                                        }
                                    }
                                    else
                                    {
                                        if (col == 0)
                                        {
                                            AoiAi.stitchv2(Bmat.Ptr, roi0, img.Ptr, ref roi0, (int)AoiAi.side.up, Convert.ToInt32(img.Cols * or_vl), Convert.ToInt32(img.Cols * or_vu), Convert.ToInt32(img.Rows * dr_hu));
                                            roi = roi0;
                                        }
                                        else
                                        {
                                            AoiAi.stitchv2(Bmat.Ptr, roi, img.Ptr, ref roi, (int)AoiAi.side.left, Convert.ToInt32(img.Cols * or_hl), Convert.ToInt32(img.Cols * or_hu), Convert.ToInt32(img.Rows * dr_vu), (int)AoiAi.side.up, Convert.ToInt32(img.Rows * or_vl), Convert.ToInt32(img.Rows * or_vu), Convert.ToInt32(img.Cols * dr_hu));
                                        }

                                    }

                                    AoiAi.copy_to(Bmat.Ptr, img.Ptr, roi);
                                    PcbInfo pcbinfo = new PcbInfo();
                                    pcbinfo.Bitmap = Aidemo.Bitmap2Byte(bitmap);
                                    pcbinfo.Obj = side;
                                    pcbinfo.Rectangle = roi;
                                    lock (pcbinfos)
                                    {
                                        pcbinfos.Enqueue(pcbinfo);
                                    }

                                    //Checkpic checkpic = aidemo.savepic(bitmap, roi.X, roi.Y);
                                    //Bcheckpics.Add(checkpic);
                                    img.Dispose();
                                    currentFrame.Dispose();
                                    bitmap.Dispose();
                                    roi0 = roi;
                                    //#region 这里去掉
                                    //CvInvoke.NamedWindow("AJpg", NamedWindowType.Normal); //创建一个显示窗口
                                    //CvInvoke.Imshow("AJpg", dst);
                                    //char key = (char)CvInvoke.WaitKey(1);
                                    //if (key == 0x1b || key == 'q') continue;
                                    //#endregion 这里去掉
                                    col++;

                                }
                                if (col == n_cols)
                                {
                                    col = 0;
                                    row++;

                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            {
                                {
                                    Loghelper.WriteLog("拼图检测错误:", ex);

                                }
                            }
                            Thread.Sleep(10);
                        }
                    }

                }
            }
        }

        public void checkthread(object sender, System.ComponentModel.DoWorkEventArgs e) {

            while (true) {
                int thisnum = 0;
                while (!checkend) {
                    try
                    {
                        // Console.WriteLine("当前条数:" + thisnum + "  " +Allnum);
                        if (thisnum == Allnum && Allnum > 0)
                        {
                            checkend = true;
                            break;

                        }
                        lock (pcbinfos)
                        {
                            if (pcbinfos.Count > 0)
                            {
                                PcbInfo pcbinfo = pcbinfos.Dequeue();
                                if ((SIDE)pcbinfo.Obj == SIDE.FRONT)
                                {
                                    Checkpic checkpic = aidemo.savepic(pcbinfo.Bitmap, pcbinfo.Rectangle.X, pcbinfo.Rectangle.Y);
                                    Acheckpics.Add(checkpic);

                                }
                                else
                                {
                                    Checkpic checkpic = aidemo.savepic(pcbinfo.Bitmap, pcbinfo.Rectangle.X, pcbinfo.Rectangle.Y);
                                    Bcheckpics.Add(checkpic);

                                }
                                thisnum++;
                            }
                        }
                    }
                    catch (Exception ex) {
                        Loghelper.WriteLog("检测错误", ex);

                    }
                    Thread.Sleep(10);
                }
            }
        }

        //16位写入plc方法
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
        //保存图片
        private void Savepic(Bitmap bitmap, SIDE Side)
        {
            long i = dicid;
            string directory = "d:\\ftpimage\\" + i.ToString();
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            //CopyDir(@"f:\ftp\", @"f:\" + i + "\\");
            if (Side == SIDE.FRONT)
            {
                bitmap.Save("d:\\ftpimage\\" + i.ToString() + "\\front.jpg", ImageFormat.Jpeg);
            }
            else
            {
                bitmap.Save("d:\\ftpimage\\" + i.ToString() + "\\back.jpg", ImageFormat.Jpeg);
            }

        }
        private void SendresulttoOther(string path, List<Checkpic> acheckpics, List<Checkpic> bcheckpics, SIDE side)
        {
            try
            {
                long i = dicid;
                //本机要上传的目录的父目录
                string localPath = @"d:\\ftpimage\\";
                //要上传的目录名
                string fileName = @i.ToString();
                Ftp.UploadDirectory(localPath, fileName);
                //Checkpic checkpic = aidemo.savepic(bitmap);
                JsonData<Pcb> jsonData = new JsonData<Pcb>();
                List<Result> results = new List<Result>();
                Snowflake snowflake2 = new Snowflake(3);
                Pcb pcb = new Pcb();
                pcb.Id = i.ToString();
                pcb.PcbNumber = path;
                pcb.PcbName = path;
                pcb.PcbWidth = Int32.Parse(tbWidth.Text);
                pcb.PcbHeight = Int32.Parse(tbLenth.Text);

                pcb.PcbChildenNumber = 1;
                if (side == SIDE.DOUBle)
                    pcb.SurfaceNumber = 2;
                else
                    pcb.SurfaceNumber = 1;
                pcb.PcbPath = i.ToString();
                pcb.IsError = 0;
                pcb.CreateTime = DateTime.Now;
                for (int k = 0; k < acheckpics.Count; k++)
                {
                    Checkpic checkpic = acheckpics[k];
                    if (checkpic.IsNg)
                    {
                        if (Directory.Exists(savepath + captureCount.ToString())) {
                            Directory.Move(savepath + captureCount.ToString(), savepath + captureCount.ToString() + "-Ng");
                        }

                        for (int n = 0; n < checkpic.Lists.Count; n++)
                        {
                            string idstring = snowflake2.nextId().ToString();
                            Result result = new Result();
                            result.Id = idstring;
                            result.IsBack = 0;
                            result.PcbId = i.ToString();
                            result.Area = "";
                            result.Region = (picwidth - checkpic.Lists[n].x - checkpic.Lists[n].w) / 2 + "," + (picheight - checkpic.Lists[n].y - checkpic.Lists[n].h) / 2 + "," + checkpic.Lists[n].w / 2 + "," + checkpic.Lists[n].h / 2;
                            result.NgType = Ngtype.IntConvertToEnum((int)(checkpic.Lists[n].obj_id));
                            result.PartImagePath = idstring + ".jpg";
                            result.CreateTime = DateTime.Now;
                            results.Add(result);
                        }
                        pcb.IsError = 1;

                    }

                }
                for (int k = 0; k < bcheckpics.Count; k++)
                {
                    Checkpic checkpic = bcheckpics[k];
                    if (checkpic.IsNg)
                    {
                        if (Directory.Exists(savepath + captureCount.ToString()))
                        {
                            Directory.Move(savepath + captureCount.ToString(), savepath + captureCount.ToString() + "-Ng");
                        }

                        for (int n = 0; n < checkpic.Lists.Count; n++)
                        {
                            string idstring = snowflake2.nextId().ToString();
                            Result result = new Result();
                            result.Id = idstring;
                            result.IsBack = 1;
                            result.PcbId = i.ToString();
                            result.Area = "";
                            result.Region = checkpic.Lists[n].x / 2 + "," + (picheight - checkpic.Lists[n].y - checkpic.Lists[n].h) / 2 + "," + checkpic.Lists[n].w / 2 + "," + checkpic.Lists[n].h / 2;
                            result.NgType = Ngtype.IntConvertToEnum((int)(checkpic.Lists[n].obj_id));
                            result.PartImagePath = idstring + ".jpg";
                            result.CreateTime = DateTime.Now;
                            results.Add(result);
                        }
                        pcb.IsError = 1;

                    }

                }
                pcb.results = results;
                jsonData.data = pcb;
                string jo = JsonConvert.SerializeObject(jsonData);
                var factory = new ConnectionFactory();
                factory.AutomaticRecoveryEnabled = true;
                factory.HostName = @IniFile.iniRead("Factory", "hostName"); // RabbitMQ服务在本地运行
                factory.UserName = @IniFile.iniRead("Factory", "userName"); // 用户名
                factory.Password = @IniFile.iniRead("Factory", "password"); // 密码
                factory.VirtualHost = "my_vhost";

                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        // 将消息标记为持久性。
                        var properties = channel.CreateBasicProperties();
                        properties.Persistent = true;
                        channel.QueueDeclare("work", true, false, false, null); // 创建一个名称为hello的消息队列
                        string message = jo; // 传递的消息内容
                        var body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish("", "work", properties, body); // 开始传递
                                                                            //MessageBox.Show("已发送： {0}", message);
                    }
                }
                Loghelper.WriteLog("发送内容为：" + jo);

            }
            catch (Exception ex)
            {
                Loghelper.WriteLog("发送至输出端出错", ex);
            }



        }

        //所有相机初始化
        private void Camerasinitialization()
        {

            #region camera

            m_imageProvider.GrabErrorEvent += new ImageProvider.GrabErrorEventHandler(OnGrabErrorEventCallback);
            m_imageProvider.DeviceRemovedEvent += new ImageProvider.DeviceRemovedEventHandler(OnDeviceRemovedEventCallback);
            m_imageProvider.DeviceOpenedEvent += new ImageProvider.DeviceOpenedEventHandler(OnDeviceOpenedEventCallback);
            m_imageProvider.DeviceClosedEvent += new ImageProvider.DeviceClosedEventHandler(OnDeviceClosedEventCallback);
            m_imageProvider.GrabbingStartedEvent += new ImageProvider.GrabbingStartedEventHandler(OnGrabbingStartedEventCallback);
            m_imageProvider.ImageReadyEvent += new ImageProvider.ImageReadyEventHandler(OnImageReadyEventCallback);
            m_imageProvider.GrabbingStoppedEvent += new ImageProvider.GrabbingStoppedEventHandler(OnGrabbingStoppedEventCallback);
            m_imageProvider.CameraId = cameraAid;
            uint id = m_imageProvider.GetDevice(cameraAid);
            if (id == 99)
            {
                MessageBox.Show("未连接相机");
            }
            else
            {
                m_imageProvider.Open(id);
                //Thread.Sleep(100);
                m_imageProvider.ContinuousShot();
                Loghelper.WriteLog("连接相机A:" + id.ToString());
            }

            #endregion

            #region camera

            m_imageProviderB.GrabErrorEvent += new ImageProvider.GrabErrorEventHandler(OnGrabErrorEventCallback);
            m_imageProviderB.DeviceRemovedEvent += new ImageProvider.DeviceRemovedEventHandler(OnDeviceRemovedEventCallbackB);
            m_imageProviderB.DeviceOpenedEvent += new ImageProvider.DeviceOpenedEventHandler(OnDeviceOpenedEventCallback);
            m_imageProviderB.DeviceClosedEvent += new ImageProvider.DeviceClosedEventHandler(OnDeviceClosedEventCallback);
            m_imageProviderB.GrabbingStartedEvent += new ImageProvider.GrabbingStartedEventHandler(OnGrabbingStartedEventCallback);
            m_imageProviderB.ImageReadyEvent += new ImageProvider.ImageReadyEventHandler(OnImageReadyEventCallbackB);
            m_imageProviderB.GrabbingStoppedEvent += new ImageProvider.GrabbingStoppedEventHandler(OnGrabbingStoppedEventCallback);
            m_imageProviderB.CameraId = cameraBid;
            id = m_imageProviderB.GetDevice(cameraBid);
            if (id == 99)
            {
                MessageBox.Show("未连接相机");
            }
            else
            {
                m_imageProviderB.Open(id);
                //Thread.Sleep(100);
                m_imageProviderB.ContinuousShot();
                Loghelper.WriteLog("连接相机B:" + id.ToString());
            }

            #endregion

        }

        //检测结果发送
        private void Getendstatus(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (true)
            {
                Thread.Sleep(200);
                if (!R_backgroundWorker.CancellationPending)
                {

                    if (isdoubleside)
                    {
                        if (Aend && Bend && checkend)
                        {

                            SendresulttoOther(captureCount.ToString(), Acheckpics, Bcheckpics, SIDE.DOUBle);
                            Bcheckpics.Clear();
                            Acheckpics.Clear();
                            dicid = snowflake.nextId();
                            captureCount++;
                            savepath = "d:\\SavedPerCameraImages\\" + DateTime.Now.ToString("M.d") + "\\";
                            while (Directory.Exists(savepath + captureCount.ToString()) || Directory.Exists(savepath + captureCount.ToString() + "-Ng"))
                            {
                                captureCount++;
                            }
                            Directory.CreateDirectory(savepath + captureCount.ToString());
                            Loghelper.WriteLog("创建目录：" + savepath + captureCount.ToString());
                            Aend = false;
                            Bend = false;
                            Aimagenum = 0;
                            Bimagenum = 0;
                            Allnum = 0;
                            isruna = true;
                            isrunb = true;
                            checkend = false;
                            isAok = false;
                            isBok = false;

                        }
                    }
                    else
                    {
                        if (Aend && checkend)
                        {
                            SendresulttoOther(captureCount.ToString(), Acheckpics, Bcheckpics, SIDE.DOUBle);
                            Bcheckpics.Clear();
                            Acheckpics.Clear();
                            dicid = snowflake.nextId();
                            captureCount++;
                            savepath = "d:\\SavedPerCameraImages\\" + DateTime.Now.ToString("M.d") + "\\";
                            while (Directory.Exists(savepath + captureCount.ToString()) || Directory.Exists(savepath + captureCount.ToString() + "-Ng"))
                            {
                                captureCount++;
                            }
                            Directory.CreateDirectory(savepath + captureCount.ToString());
                            Loghelper.WriteLog("创建目录：" + savepath + captureCount.ToString());
                            Aend = false;
                            Bend = false;
                            Aimagenum = 0;
                            Bimagenum = 0;
                            Allnum = 0;
                            isruna = true;
                            checkend = false;
                            isAok = false;
                            isBok = false;
                        }
                        if (Bend && checkend)
                        {
                            SendresulttoOther(captureCount.ToString(), Acheckpics, Bcheckpics, SIDE.DOUBle);
                            Bcheckpics.Clear();
                            Acheckpics.Clear();
                            dicid = snowflake.nextId();
                            captureCount++;
                            savepath = "d:\\SavedPerCameraImages\\" + DateTime.Now.ToString("M.d") + "\\";
                            while (Directory.Exists(savepath + captureCount.ToString()) || Directory.Exists(savepath + captureCount.ToString() + "-Ng"))
                            {
                                captureCount++;
                            }
                            Directory.CreateDirectory(savepath + captureCount.ToString());
                            Loghelper.WriteLog("创建目录：" + savepath + captureCount.ToString());
                            Aimagenum = 0;
                            Bimagenum = 0;
                            Allnum = 0;
                            isrunb = true;
                            checkend = false;
                            isAok = false;
                            isBok = false;
                        }
                    }
                }
                else
                {
                    e.Cancel = true;
                    Aend = false;
                    Bend = false;
                    return;

                }


            }


        }

    } 
}
        
    


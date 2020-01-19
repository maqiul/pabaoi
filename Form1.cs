using System;
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
using Basler.Pylon;
using QTing.PLC;
using PylonC.NETSupportLibrary;
using System.Drawing.Imaging;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Stitching;
using Emgu.CV.Util;

namespace pcbaoi
{
    public partial class Form1 : Form
    {
        //AutoSizeFormClassnew asc = new AutoSizeFormClassnew();
        private int formStartX = 0;
        private int formStartY = 0;

        FormControl fc = null;
        bool isrule = false;
        bool drawline = false;
        int checknum = 0;
        Point startpoint;
        Point endpoint;
        Control control;
        Point centerpoint;
        int bignum = 0;
        int smallnum = 0;
        private PickBox pb = new PickBox();
        FileHandler filehandler;
        bool isSelected = false;
        Point mouseDownPoint;
        int zijibannum = 0;
        List<PictureBox> pictureBoxes = new List<PictureBox>();
        int from;
        int oldlastwidth =0;
        int oldlastheight = 0;
        AutoSizeFormClass asc = new AutoSizeFormClass();
        Control controlnew;
        bool pulleyStop =false;
        bool pulleySearchStop = true;
        public static string cameraAid;
        public static string cameraBid;
        public  List<Bitmap> Abitmaps = new List<Bitmap>();
        public  List<Bitmap> Bbitmaps =new List<Bitmap>();
        public static PictureBox pbmain;
        BackgroundWorker backgroundWorker = null;
        BackgroundWorker backgroundWorker2 = null;
        bool isruna = false;
        bool isrunb = false;
        #region camera
        private ImageProvider m_imageProvider = new ImageProvider(); /* Create one image provider. */
        private Bitmap m_bitmap = null;

        private int D2000 = 0;
        private int D2001 = 0;
        private int D2002 = 0;
        private int D2003 = 0;
        private int D2004 = 0;




        /* Stops the image provider and handles exceptions. */
        public void Stop()
        {
            /* Stop the grabbing. */
            try
            {
                m_imageProvider.Stop();
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
            }
            catch (Exception e)
            {
                Loghelper.WriteLog(m_imageProvider.GetLastErrorMessage(), e);
            }
        }

        /* Handles the click on the stop frame acquisition button. */
        private void toolStripButtonStop_Click(object sender, EventArgs e)
        {
            Stop(); /* Stops the grabbing of images. */
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
        private void OnDeviceRemovedEventCallback()
        {
            if (InvokeRequired)
            {
                /* If called from a different thread, we must use the Invoke method to marshal the call to the proper thread. */
                BeginInvoke(new ImageProvider.DeviceRemovedEventHandler(OnDeviceRemovedEventCallback));
                return;
            }
            /* Stops the grabbing of images. */
            Stop();
            /* Close the image provider. */
            CloseTheImageProvider();
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

        /* Handles the event related to an image having been taken and waiting for processing. */
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
                    /* Check if the image is compatible with the currently used bitmap. */
                    if (BitmapFactory.IsCompatible(m_bitmap, image.Width, image.Height, image.Color))
                    {
                        /* Update the bitmap with the image data. */
                        BitmapFactory.UpdateBitmap(m_bitmap, image.Buffer, image.Width, image.Height, image.Color);
                        /* To show the new image, request the display control to update itself. */
                        if (m_imageProvider.CameraId == cameraAid)
                        {
                            Bitmap listbitmap;
                            listbitmap =(Bitmap) PicSized(m_bitmap, 4).Clone();
                            pbFrontImg.Image = m_bitmap;
                            pbMainImg.Image = m_bitmap;
                            Abitmaps.Add(listbitmap);
                            //listbitmap.Dispose();
                            //m_bitmap.Save("d:\\pic\\test" + ".bmp", ImageFormat.Bmp);
                            //Abitmaps[Abitmaps.Count - 1].Save("d:\\pic\\test2" + ".bmp", ImageFormat.Bmp);
                            //;
                        }
                        else {
                            Bitmap listbitmap;
                            listbitmap = (Bitmap)PicSized(m_bitmap, 4).Clone();
                            pbBackImg.Image = m_bitmap;
                            pbMainImg.Image = m_bitmap;
                            Bbitmaps.Add(listbitmap);
                            //listbitmap.Dispose();
                            //m_bitmap.Save("d:\\pic\\" + DateTimeUtil.DateTimeToLongTimeStamp().ToString() + ".bmp", ImageFormat.Bmp);
                        }

                    }
                    else /* A new bitmap is required. */
                    {
                        BitmapFactory.CreateBitmap(out m_bitmap, image.Width, image.Height, image.Color);
                        BitmapFactory.UpdateBitmap(m_bitmap, image.Buffer, image.Width, image.Height, image.Color);
                        /* We have to dispose the bitmap after assigning the new one to the display control. */
                        Bitmap bitmap = pbMainImg.Image as Bitmap;
                        /* Provide the display control with the new bitmap. This action automatically updates the display. */
                        if (m_imageProvider.CameraId == cameraAid)
                        {
                            Bitmap listbitmap;
                            listbitmap = (Bitmap)PicSized(m_bitmap, 4).Clone();
                            pbFrontImg.Image = m_bitmap;
                            pbMainImg.Image = m_bitmap;
                            Abitmaps.Add(listbitmap);
                            //listbitmap.Dispose();
                            //m_bitmap.Save("d:\\pic\\test" + ".bmp", ImageFormat.Bmp);
                            //Abitmaps[Abitmaps.Count - 1].Save("d:\\pic\\test2" + ".bmp", ImageFormat.Bmp);

                        }
                        else
                        {
                            Bitmap listbitmap;
                            listbitmap = (Bitmap)PicSized(m_bitmap, 4).Clone();
                            pbBackImg.Image = m_bitmap;
                            pbMainImg.Image = m_bitmap;
                            Bbitmaps.Add(listbitmap);
                            //listbitmap.Dispose();
                            //m_bitmap.Save("d:\\pic\\" + DateTimeUtil.DateTimeToLongTimeStamp().ToString() + ".bmp", ImageFormat.Bmp);
                        }

                        if (bitmap != null)
                        {
                            /* Dispose the bitmap. */
                            bitmap.Dispose();
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
        public Form1(int i)
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
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += new DoWorkEventHandler(getstatus);
            backgroundWorker2 = new BackgroundWorker();
            backgroundWorker2.WorkerReportsProgress = true;
            backgroundWorker2.WorkerSupportsCancellation = true;
            backgroundWorker2.DoWork += new DoWorkEventHandler(getstatus2);
            from = i;
            cameraAid = IniFile.iniRead("CameraA", "SerialNumber");
            cameraBid = IniFile.iniRead("CameraB", "SerialNumber");
            
            pbmain = this.pbMainImg;
            
        }


        private void Form1_Shown(object sender, EventArgs e)
        {
            if (from==1) {
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
            pictureBox2_Click(pbFrontImg, null);
            asc.RenewControlRect(pbMainImg);
            string selectsql = "select * from bad";
            DataTable dataTable =SQLiteHelper.GetDataTable(selectsql);
            if (dataTable.Rows.Count > 0) {
                tbPcbName.Text = dataTable.Rows[0]["badname"].ToString();
                tbPcbWidth.Text = dataTable.Rows[0]["badwidth"].ToString();
                tbPcbLength.Text = dataTable.Rows[0]["badheight"].ToString();
            }
            //pbMainImg.Height = oldlastheight;
            //pbMainImg.Width = oldlastwidth;
            this.MouseWheel += PbMainImg_MouseWheel;

            pbMainImg.MouseWheel += PbMainImg_MouseWheel;
            this.MouseWheel += PbMainImg_MouseWheel;

            runplace();

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

        private async void PulleyPaging( int i)
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
            if (i > 0) {
                i = 72;
            }
            else {
                i = -72;            
            }

            if (pbMainImg.Height + i > 300)
            {
                pbMainImg.Width = pbMainImg.Width + i;//增加picturebox的宽度
                pbMainImg.Height = pbMainImg.Height + i;
                // Console.WriteLine(pictureBox1.Width.ToString() + pictureBox1.Height.ToString());
                pbMainImg.Left = pbMainImg.Left - i / 2;//使picturebox的中心位于窗体的中心
                pbMainImg.Top = pbMainImg.Top - i / 2;//进而缩放时图片也位于窗体的中心
                //pbMainImg.Location = new Point(pbMainImg.Location.X- i / 2, pbMainImg.Location.Y - i / 2);
                oldlastheight = pbMainImg.Height;
                oldlastwidth = pbMainImg.Width;
            }

            //让滑轮变得可以触发方法
            pulleySearchStop = true;

        }

            private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Settings.Default.path);
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal) {
                this.Width = this.formStartX;
                this.Height = this.formStartY;
                fc.Reset(this, fc);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            fc = new FormControl(this);
            fc.GetInit(this, fc);

            this.formStartX = this.Width;
            this.formStartY = this.Height;

        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (isrule) {
                if (checknum == 0)
                {
                    startpoint = e.Location;
                    drawline = true;
                    checknum = 1;
                }
                else {
                    endpoint = e.Location;
                    //MessageBox.Show("x:"+(endpoint.X - startpoint.X).ToString()+ "  y:" + (endpoint.Y - startpoint.Y).ToString());
                    float newx =  (float)pbMainImg.Image.Width/ (float)pbMainImg.Width;
                    float newy = (float)pbMainImg.Image.Height / (float)pbMainImg.Height;
                    Ruleresult ruleresult = new Ruleresult(startpoint,endpoint,newx,newy);
                    ruleresult.ShowDialog();
                    checknum = 0;
                    drawline = false;
                    isrule = false;
                    button8.BackColor = Color.Transparent;
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
            else {
                drawlineact();

            }
            
        }

        private void showponit(int x,int y) {

            label12.Text = "X: " + x.ToString(); 
            label13.Text = "Y: " + y.ToString(); 
                
        }

        private void button5_Click(object sender, EventArgs e)
        {
            isrule = true;
            //button5.BackColor = Color.Blue;
            if (control != null) {
                control.BackColor = Color.Transparent;
                
            }
            //control = button5;

            

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
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
            if (isrule && drawline) {
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

        private void button10_Click(object sender, EventArgs e)
        {
            //button10.BackColor = Color.Blue;
            if (control != null)
            {
                control.BackColor = Color.Transparent;

            }
            //control = button10;
            isrule = false;
            drawline = false;
            checknum = 0;
            Addelement addelement = new Addelement(centerpoint);
            addelement.ShowDialog();
            if (addelement.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                Element element = addelement.Tag as Element;

                PictureBox pictureBox = new PictureBox();
                pictureBox.Name = element.Name;
                pictureBox.Location = new Point(element.X - 10,element.Y - 5);
                pictureBox.Width = 20;
                pictureBox.Height = 10;
                pictureBox.BorderStyle = BorderStyle.FixedSingle;
                Rectangle rectangle = new Rectangle();
                rectangle.Location = pictureBox.Location;
                rectangle.Width = 200;
                rectangle.Height = 100;
                pictureBox.BringToFront();
                //pictureBox.Image = CropImage(bitmap, rectangle);
                ////pictureBox.Image = Image.FromFile("e:\\123.jpg");
                //pictureBox.DoubleClick += new EventHandler(pictureboxshow);
                //pictureBox.SizeChanged += new EventHandler(pictureboxsizechange);
                //pictureBox.Move += new EventHandler(pictureboxmove);
                pictureBox.BackColor = Color.Transparent;
                pictureBox.Parent = pbMainImg;
                //pictureBox1.Hide();
                this.pbMainImg.Controls.Add(pictureBox);
                //MessageBox.Show(pictureBox.Parent.Name);
                foreach (Control c in this.Controls)
                {
                    if (c.Name == element.Name)
                    {
                        c.BringToFront();
                    }

                }
                //Form1_Load(null,null);
                pb.WireControl(pictureBox);
                //MessageBox.Show(element.Name);


            }

        }

        private void button11_Click(object sender, EventArgs e)
        {
            //button11.BackColor = Color.Blue;
            if (control != null)
            {
                control.BackColor = Color.Transparent;

            }
            //control = button11;
            isrule = false;
            drawline = false;
            checknum = 0;
            Addmark addmark = new Addmark(centerpoint);
            addmark.ShowDialog();

        }



        private void button8_Click(object sender, EventArgs e)
        {
            if (smallnum == 0 && bignum > 0)
            {
                this.pbMainImg.Width = Convert.ToInt32(this.pbMainImg.Width / 2);
                this.pbMainImg.Height = Convert.ToInt32(this.pbMainImg.Height / 2);
                int x;
                int y;
                x = Convert.ToInt32(centerpoint.X / 2 + this.pbMainImg.Location.X);
                y = Convert.ToInt32(centerpoint.Y / 2 + this.pbMainImg.Location.Y);
                this.pbMainImg.Location = new Point(x, y);
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
                        gc.DrawLine(pen, Convert.ToInt32(centerpoint.X / 2), 0, Convert.ToInt32(centerpoint.X / 2), rect.Bottom);
                        //画横线
                        gc.DrawLine(pen, 0, Convert.ToInt32(centerpoint.Y / 2), rect.Right, Convert.ToInt32(centerpoint.Y / 2));


                    }
                }
                showponit(centerpoint.X / 2, centerpoint.Y / 2);
                centerpoint = new Point(Convert.ToInt32(centerpoint.X / 2), Convert.ToInt32(centerpoint.Y / 2));
                bignum--;

            }
            else if (smallnum > -2)
            {
                this.pbMainImg.Width = Convert.ToInt32(this.pbMainImg.Width / 2);
                this.pbMainImg.Height = Convert.ToInt32(this.pbMainImg.Height / 2);
                int x;
                int y;
                x = Convert.ToInt32(centerpoint.X / 2 + this.pbMainImg.Location.X);
                y = Convert.ToInt32(centerpoint.Y / 2 + this.pbMainImg.Location.Y);
                this.pbMainImg.Location = new Point(x, y);
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
                        gc.DrawLine(pen, Convert.ToInt32(centerpoint.X / 2), 0, Convert.ToInt32(centerpoint.X / 2), rect.Bottom);
                        //画横线
                        gc.DrawLine(pen, 0, Convert.ToInt32(centerpoint.Y / 2), rect.Right, Convert.ToInt32(centerpoint.Y / 2));


                    }
                }
                showponit(centerpoint.X / 2, centerpoint.Y / 2);
                centerpoint = new Point(Convert.ToInt32(centerpoint.X / 2), Convert.ToInt32(centerpoint.Y / 2));
                smallnum--;

            }
            else {
                MessageBox.Show("已经最小");
            
            
            }


        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.pbMainImg.Size = this.pMain.Size;
            this.pbMainImg.Location = new Point(0,0);
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
                    gc.DrawLine(pen, (pbMainImg.Width) / 2, 0, (pbMainImg.Width) / 2, rect.Bottom);
                    //画横线
                    gc.DrawLine(pen, 0, (pbMainImg.Height) / 2, rect.Right, (pbMainImg.Height) / 2);


                }
            }
            showponit((pbMainImg.Width) / 2, (pbMainImg.Height) / 2);
            centerpoint = new Point((pbMainImg.Width) / 2, (pbMainImg.Height) / 2);
            smallnum = 0;
            bignum = 0;

        }

        private void button4_Click(object sender, EventArgs e)
        {
             

        }

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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            string deletesql = "delete from zijiban where isuse = 0";
            SQLiteHelper.ExecuteSql(deletesql);
            Stop();
            CloseTheImageProvider();
            Application.Exit();
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                mouseDownPoint.X = Cursor.Position.X;  //注：全局变量mouseDownPoint前面已定义为Point类型

                mouseDownPoint.Y = Cursor.Position.Y;
                isSelected = true;
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isSelected = false;

        }
        //
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

        private void drawlineact() {
            //using (Graphics gc = panel7.CreateGraphics())
            //using (Pen pen = new Pen(Color.Green))
            //{
            //    //设置画笔的宽度
            //    pen.Width = 1;
            //    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
            //    RectangleF rect = new RectangleF();
            //    rect.Location = panel7.Location;
            //    rect.Size = panel7.Size;
            //    //确保在画图区域
            //    if (rect.Contains(panel7.Location))
            //    {
            //        pictureBox1.Refresh();
            //        //画竖线
            //        gc.DrawLine(pen, (panel7.Width) / 2  , 0, (panel7.Width) / 2 , rect.Bottom);
            //        //画横线
            //        gc.DrawLine(pen, 0, (panel7.Height) / 2 , rect.Right, (panel7.Height) / 2 );


            //    }
            //}
            showponit((pbMainImg.Width- pbMainImg.Left*2) /2, (pbMainImg.Height - pbMainImg.Top*2) / 2 );
            //centerpoint = new Point((pictureBox1.Width) / 2, (pictureBox1.Height) / 2);


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Collectionform collectionform = new Collectionform(tbPcbWidth.Text,tbPcbLength.Text);
            collectionform.ShowDialog();
            if (collectionform.DialogResult == System.Windows.Forms.DialogResult.OK) {
                collection collection = new collection();
                collection = collectionform.Tag as collection;
                if (collection.Type == "正面")
                {
                    Abitmaps = new List<Bitmap>();
                    isrunb = true;
                    isruna = true;
                    if (backgroundWorker2.IsBusy) {

                        backgroundWorker2.CancelAsync();
                    }
                    if (!backgroundWorker.IsBusy) {
                        backgroundWorker.RunWorkerAsync("object argument");//启动异步操作，有两种重载。将触发BackgroundWorker.DoWork事件
                    }
                }
                else {
                    Bbitmaps = new List<Bitmap>();
                    isruna = true;
                    isrunb = true;
                    if (backgroundWorker.IsBusy)
                    {
                        backgroundWorker.CancelAsync();
                    }
                    if (!backgroundWorker2.IsBusy)
                    {
                        backgroundWorker2.RunWorkerAsync("object argument");//启动异步操作，有两种重载。将触发BackgroundWorker.DoWork事件
                    }
                }
            }
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            isrule = true;
            button8.BackColor = Color.Blue;
            if (control != null)
            {
                control.BackColor = Color.Transparent;

            }
            control = button8;
            tableLayoutPanel10.Visible = false;
            tableLayoutPanel11.Visible = false;

        }

        private void button2_Click(object sender, EventArgs e)
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
                else {
                    pbBackImg.Image = Image.FromFile(file);

                }
            }
        }



        private void pictureBox2_Click(object sender, EventArgs e)
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
                if (pbMainImg.Image != null) {
                    drawpicboxall();
                    pbMainImg.Height = oldlastheight;
                    pbMainImg.Width = oldlastwidth;
                }

                #region camera
                if (m_imageProvider.CameraId == cameraBid)
                {
                    Stop();
                    /* Close the image provider. */
                    CloseTheImageProvider();
                }
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
                }
                #endregion


            }
            catch {



            }


        }

        private void pictureBox3_Click(object sender, EventArgs e)
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
                #region camera

                if (m_imageProvider.CameraId == cameraAid)
                {
                    Stop();
                    /* Close the image provider. */
                    CloseTheImageProvider();
                }
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
                    //Thread.Sleep(100);
                    m_imageProvider.ContinuousShot();
                }

                #endregion

            }
            catch {


            }



        }

        private void button7_Click_1(object sender, EventArgs e)
        {

            if (pbMainImg.Image==null) {
                MessageBox.Show("请先采集");
                return;
            }
            PictureBox pictureBox = new PictureBox();
            pictureBox.Name = "zijiban"+ zijibannum.ToString();
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
            zijibannum++;
            showzijibannum();

            asc = new AutoSizeFormClass();
            asc.RenewControlRect(pbMainImg);
            addpicturebox();
        }

        private void PictureBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void pictureboxmove(object sender, EventArgs e)
        {
            //pictureBoxnew is Control;
            //pictureBoxnew = (PictureBox)sender;
            ////this.control.Location = pictureBoxnew.Location;
            //Rectangle rectangle = new Rectangle();
            //rectangle.Location = pictureBoxnew.Location;
            //rectangle.Width = pictureBoxnew.Width;
            //rectangle.Height = pictureBoxnew.Height;
            //Console.WriteLine(pictureBoxnew.Location);
            pbMainImg.Refresh();
            asc = new AutoSizeFormClass();
            asc.RenewControlRect(pbMainImg);
            drawlineact();


        }
        private void pictureboxsizechange(object sender, EventArgs e)
        {
            //pictureBoxnew is Control;
            //pictureBoxnew = (PictureBox)sender;
            //Rectangle rectangle = new Rectangle();
            //rectangle.Location = pictureBoxnew.Location;
            //rectangle.Width = pictureBoxnew.Width;
            //rectangle.Height = pictureBoxnew.Height;
            
            pbMainImg.Refresh();
            asc = new AutoSizeFormClass();
            asc.RenewControlRect(pbMainImg);
            drawlineact();

        }
        public void showzijibannum() {

            tbChildPcbNum.Text = zijibannum.ToString();
        
        }
        private void pictureboxclick(object sender,MouseEventArgs e) {
            //if()
            PictureBox p = (PictureBox)sender;
            Pen pp = new Pen(Color.Blue);

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

        private void pictureBox1_Click(object sender, EventArgs e)
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
                                string sql = string.Format("delete from zijiban where zijiname = '{0}'",c.Name);

                            }


                        }

                    }


                }
                asc = new AutoSizeFormClass();
                asc.RenewControlRect(pbMainImg);

            }

        }

        private void button4_Click_1(object sender, EventArgs e)
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

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            asc.ControlAutoSize(pbMainImg);
            
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {


        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (pbMainImg.Image!= null){
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
            catch {


            }



        }
        public void addpicturebox() {
            oldlastheight = pbMainImg.Height;
            oldlastwidth = pbMainImg.Width;
            pbMainImg.Height = pbMainImg.Image.Height;
            pbMainImg.Width = pbMainImg.Image.Width;                     
            string insertsql = string.Format("INSERT INTO zijiban( zijiname, startx, starty, width, height, isuse,frontorside ) VALUES ( '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')", controlnew.Name, controlnew.Location.X, controlnew.Location.Y, controlnew.Width, controlnew.Height, 0,Settings.Default.frontorside);
            SQLiteHelper.ExecuteSql(insertsql);
            pbMainImg.Height = oldlastheight;
            pbMainImg.Width = oldlastwidth;

        }
        public void drawpicbox() {
            oldlastheight = pbMainImg.Height;
            oldlastwidth = pbMainImg.Width;
            pbMainImg.Height = pbMainImg.Image.Height;
            pbMainImg.Width = pbMainImg.Image.Width;
            

        }
        public void update() {
            oldlastheight = pbMainImg.Height;
            oldlastwidth = pbMainImg.Width;
            pbMainImg.Height = pbMainImg.Image.Height;
            pbMainImg.Width = pbMainImg.Image.Width;
            string updatesql = string.Format("update zijiban set startx = '{0}',starty='{1}',width = '{2}',height = '{3}' where zijiname = '{4}'",controlnew.Location.X, controlnew.Location.Y, controlnew.Width, controlnew.Height,controlnew.Name);
            SQLiteHelper.ExecuteSql(updatesql);
            pbMainImg.Height = oldlastheight;
            pbMainImg.Width = oldlastwidth;

        }

        private void button12_Click(object sender, EventArgs e)
        {
            RunForm runForm = new RunForm();
            runForm.Show();
            
            this.Hide();
        }
        private void drawpicboxall() {
//            PropertyInfo pInfo = pbMainImg.GetType().GetProperty("ImageRectangle", BindingFlags.Instance |
//BindingFlags.NonPublic);
//            Rectangle rect = (Rectangle)pInfo.GetValue(pbMainImg, null);
//            pbMainImg.Width = rect.Width;
//            pbMainImg.Height = rect.Height;
            oldlastheight = pbMainImg.Height;
            oldlastwidth = pbMainImg.Width;
            pbMainImg.Height = pbMainImg.Image.Height;
            pbMainImg.Width = pbMainImg.Image.Width;
            string selectopsql = string.Format("select * from operato where frontorside = '{0}'",Settings.Default.frontorside);
            DataTable dataTable = SQLiteHelper.GetDataTable(selectopsql);
            for (int i=0;i< dataTable.Rows.Count;i++) {
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
                if (dataTable.Rows[i]["intpicturename"].ToString() != "") {
                    PictureBox pictureBox2 = new PictureBox();
                    pictureBox2.Name = dataTable.Rows[i]["intpicturename"].ToString();
                    pictureBox2.Location = new Point(Convert.ToInt32(dataTable.Rows[i]["instartx"].ToString()), Convert.ToInt32(dataTable.Rows[i]["instarty"].ToString()));
                    pictureBox2.Width =Convert.ToInt32(dataTable.Rows[i]["inwidth"].ToString());
                    pictureBox2.Height =Convert.ToInt32(dataTable.Rows[i]["inheight"].ToString()); ;
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
                zijibannum = Convert.ToInt32(dataTable2.Rows[i]["id"].ToString());
            }
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            Testform testform = new Testform(pbMainImg.Image);
            testform.Show();
            this.Hide();
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
            else {
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
        private void picbox_Panit(object sender, PaintEventArgs e) {
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

        private void conn()
        {

            if (PLCController.Instance.Connection(IniFile.iniRead("PLC", "ip"), Convert.ToInt32(IniFile.iniRead("PLC", "port"))))
                Console.WriteLine("连接成功");
            else
                MessageBox.Show("连接失败");

        }
        private void close() {

            PLCController.Instance.CloseConnection();
        }

        private void send(int intvalue, int address)
        {
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
            while (true) {
                while (isruna)
                {
                    //判断是否取消操作 
                    if (backgroundWorker.CancellationPending)
                    {
                        e.Cancel = true; //这里才真正取消 
                        return;
                    }
                    run();

                }

            }


        }
        private void run()
        {
            int xvalue = Convert.ToInt32(Convert.ToInt32(tbPcbWidth.Text));
            int yvalue = Convert.ToInt32(Convert.ToInt32(tbPcbLength.Text));
            List<int> ax = Xycoordinate.axcoordinate((int)Math.Ceiling((double)xvalue / (double)15));
            List<int> ay = Xycoordinate.aycoordinate((int)Math.Ceiling((double)yvalue / (double)15));
            byte[] receiveData = new byte[255];
            byte[] writeValueX = new byte[ay.Count * 4];
            byte[] writeValueY = new byte[ay.Count * 4];
            byte[] writeValue = new byte[4];
            bool cantak = true;
            while (cantak)
            {

                byte[] newreceiveData = new byte[255]; //{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
                int num = PLCController.Instance.ReadData(1131, 2, newreceiveData);
                double newvalue = newreceiveData[11] * Math.Pow(256, 3) + newreceiveData[12] * Math.Pow(256, 2) + newreceiveData[9] * Math.Pow(256, 1) + newreceiveData[10];

                if (newvalue == 0.00)
                {
                    Thread.Sleep(100);
                }
                else
                {
                    writeValue = DoubleToByte(0);
                    if (PLCController.Instance.IsConnected)
                        PLCController.Instance.WriteData(5002, 2, writeValue, receiveData);

                    for (int i = 0; i < ax.Count; i++)
                    {
                        if (i == ax.Count)
                        {
                            continue;
                        }
                        for (int n = 0; n < ay.Count; n++)
                        {
                            byte[] ibuf = new byte[4];
                            ibuf = DoubleToByte(ax[i]);
                            writeValueX[n * 4] = ibuf[0];
                            writeValueX[n * 4 + 1] = ibuf[1];
                            writeValueX[n * 4 + 2] = ibuf[2];
                            writeValueX[n * 4 + 3] = ibuf[3];

                            //Thread.Sleep(50);
                            ibuf = DoubleToByte(ay[n]);
                            writeValueY[n * 4] = ibuf[0];
                            writeValueY[n * 4 + 1] = ibuf[1];
                            writeValueY[n * 4 + 2] = ibuf[2];
                            writeValueY[n * 4 + 3] = ibuf[3];


                        }
                        if (PLCController.Instance.IsConnected)
                            PLCController.Instance.WriteData(3000, ay.Count * 2, writeValueX, receiveData);
                        Thread.Sleep(50);
                        if (PLCController.Instance.IsConnected)
                            PLCController.Instance.WriteData(3200, ay.Count * 2, writeValueY, receiveData);
                        writeValue = DoubleToByte(ay.Count);
                        if (PLCController.Instance.IsConnected)
                            PLCController.Instance.WriteData(5000, 2, writeValue, receiveData);
                        double value = 1.00;
                        byte[] newwriteValue = new byte[2];
                        newwriteValue[0] = (byte)(value / Math.Pow(256, 1));
                        newwriteValue[1] = (byte)((value / Math.Pow(256, 0)) % 256);
                        if (PLCController.Instance.IsConnected)
                            PLCController.Instance.WriteData(2144, 1, newwriteValue, receiveData);
                        bool isrun = true;
                        while (isrun)
                        {

                            //Thread.Sleep(50);
                            newreceiveData = new byte[255]; //{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };

                            num = PLCController.Instance.ReadData(1133, 2, newreceiveData);



                            newvalue = newreceiveData[11] * Math.Pow(256, 3) + newreceiveData[12] * Math.Pow(256, 2) + newreceiveData[9] * Math.Pow(256, 1) + newreceiveData[10];
                            if (newvalue == 1.00)
                            {

                                isrun = false;
                            }
                        }

                    }
                    cantak = false;

                }

            }
            double thenewvalue = 1.00;
            byte[] thenewwriteValue = new byte[2];
            thenewwriteValue[0] = (byte)(thenewvalue / Math.Pow(256, 1));
            thenewwriteValue[1] = (byte)((thenewvalue / Math.Pow(256, 0)) % 256);
            if (PLCController.Instance.IsConnected)
                PLCController.Instance.WriteData(2145, 1, thenewwriteValue, receiveData);
            Thread.Sleep(200);
            //Mat aa =new Mat();
            if (Abitmaps.Count > 0) {
                try
                {
                int patchWidth = Abitmaps[0].Width ;
                int patchHeight = Abitmaps[0].Height;
                Mat aa = new Mat(ax.Count * patchHeight, ay.Count * patchWidth ,DepthType.Cv8U, 3);
                    if (Abitmaps.Count == ax.Count * ay.Count)
                    {

                        for (int i = 0; i < ax.Count; i++)
                        {
                            for (int j = 0; j < ay.Count; j++)
                            {
                                int count = i * ay.Count + j;

                                Bitmap bitmap = Abitmaps[count];
                                bitmap.Save("d:\\newpic\\2" + count.ToString() + ".bmp", ImageFormat.Bmp);

                               // bitmap.Save("d:\\newpic\\" + count.ToString() + ".bmp", ImageFormat.Bmp);
                                Emgu.CV.Image<Bgr, Byte> currentFrame = new Emgu.CV.Image<Bgr, Byte>(bitmap);
                                Mat invert = new Mat();
                                CvInvoke.BitwiseAnd(currentFrame, currentFrame, invert);
                                //Mat temp = aa.ToImage<Bgr, byte>().GetSubRect(new Rectangle(i * patchHeight, j * patchWidth, invert.Size.Width, invert.Size.Height)).Mat;
                                //temp.Save("d:\\newpic\\temp" + i.ToString() + "_" + j.ToString() + "jpg");
                                //invert.CopyTo(temp);
                               
                                AoiAi.addPatch(aa.Ptr, invert.Ptr,  (float)j * patchWidth,(float)i * patchHeight);
                                aa.Save("d:\\newpic\\aa" + i.ToString() + "_" + j.ToString() + ".jpg");
                                invert.Dispose();
                            }

                        }
                    }
                   
                    Image<Bgr, Byte> _image = aa.ToImage<Bgr, Byte>();
                    Bitmap allbitmap = _image.Bitmap;
                    pbMainImg.Image = allbitmap;
                    aa.Dispose();
                    GC.Collect();
                }
                catch (Exception e)
                {
                    Loghelper.WriteLog("报错了", e);


                }
            }


            //Bitmap allbitmap = Mat
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
        private void getstatus2(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (true) {
                while (isrunb)
                {
                    //判断是否取消操作 
                    if (backgroundWorker2.CancellationPending)
                    {
                        e.Cancel = true; //这里才真正取消 
                        return;
                    }
                    run2();

                }
            }


        }
        private void run2()
        {

            int xvalue = Convert.ToInt32(Convert.ToInt32(tbPcbWidth.Text));
            int yvalue = Convert.ToInt32(Convert.ToInt32(tbPcbLength.Text));
            List<int> bx = Xycoordinate.bxcoordinate((int)Math.Ceiling((double)xvalue / (double)15));
            List<int> by = Xycoordinate.bycoordinate((int)Math.Ceiling((double)yvalue / (double)15));
            byte[] receiveData = new byte[255];
            byte[] writeValueX = new byte[by.Count * 4];
            byte[] writeValueY = new byte[by.Count * 4];
            byte[] writeValue = new byte[4];
            bool cantak = true;
            while (cantak)
            {

                byte[] newreceiveData = new byte[255]; //{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
                int num = PLCController.Instance.ReadData(1131, 2, newreceiveData);
                double newvalue = newreceiveData[11] * Math.Pow(256, 3) + newreceiveData[12] * Math.Pow(256, 2) + newreceiveData[9] * Math.Pow(256, 1) + newreceiveData[10];

                if (newvalue == 0.00)
                {
                    Thread.Sleep(100);
                }
                else
                {
                    writeValue = DoubleToByte(0);
                    if (PLCController.Instance.IsConnected)
                        PLCController.Instance.WriteData(5000, 2, writeValue, receiveData);


                    for (int i = 0; i < bx.Count; i++)
                    {
                        if (i == bx.Count)
                        {
                            continue;
                        }
                        for (int n = 0; n < by.Count; n++)
                        {
                            byte[] ibuf = new byte[4];
                            ibuf = DoubleToByte(bx[i]);
                            writeValueX[n * 4] = ibuf[0];
                            writeValueX[n * 4 + 1] = ibuf[1];
                            writeValueX[n * 4 + 2] = ibuf[2];
                            writeValueX[n * 4 + 3] = ibuf[3];

                            //Thread.Sleep(50);
                            ibuf = DoubleToByte(by[n]);
                            writeValueY[n * 4] = ibuf[0];
                            writeValueY[n * 4 + 1] = ibuf[1];
                            writeValueY[n * 4 + 2] = ibuf[2];
                            writeValueY[n * 4 + 3] = ibuf[3];


                        }
                        if (PLCController.Instance.IsConnected)
                            PLCController.Instance.WriteData(3400, by.Count * 2, writeValueX, receiveData);
                        Thread.Sleep(50);
                        if (PLCController.Instance.IsConnected)
                            PLCController.Instance.WriteData(3600, by.Count * 2, writeValueY, receiveData);
                        writeValue = DoubleToByte(by.Count);
                        if (PLCController.Instance.IsConnected)
                            PLCController.Instance.WriteData(5002, 2, writeValue, receiveData);
                        double value = 1.00;
                        byte[] newwriteValue = new byte[2];
                        newwriteValue[0] = (byte)(value / Math.Pow(256, 1));
                        newwriteValue[1] = (byte)((value / Math.Pow(256, 0)) % 256);
                        if (PLCController.Instance.IsConnected)
                            PLCController.Instance.WriteData(2146, 1, newwriteValue, receiveData);
                        bool isrun = true;
                        while (isrun)
                        {

                            //Thread.Sleep(50);
                            newreceiveData = new byte[255]; //{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };

                            num = PLCController.Instance.ReadData(1133, 2, newreceiveData);



                            newvalue = newreceiveData[11] * Math.Pow(256, 3) + newreceiveData[12] * Math.Pow(256, 2) + newreceiveData[9] * Math.Pow(256, 1) + newreceiveData[10];
                            if (newvalue == 1.00)
                            {

                                isrun = false;
                            }
                        }

                    }
                    cantak = false;

                }

            }
            double thenewvalue = 1.00;
            byte[] thenewwriteValue = new byte[2];
            thenewwriteValue[0] = (byte)(thenewvalue / Math.Pow(256, 1));
            thenewwriteValue[1] = (byte)((thenewvalue / Math.Pow(256, 0)) % 256);
            if (PLCController.Instance.IsConnected)
                PLCController.Instance.WriteData(2147, 1, thenewwriteValue, receiveData);
            if (Bbitmaps.Count > 0)
            {
                try
                {
                    int patchWidth = Bbitmaps[0].Width / 4;
                    int patchHeight = Bbitmaps[0].Height / 4;
                    Mat aa = new Mat(by.Count * patchWidth, bx.Count * patchHeight, DepthType.Cv8U, 3);
                    if (Bbitmaps.Count == bx.Count * by.Count)
                    {

                        for (int i = 0; i < bx.Count; i++)
                        {
                            for (int j = 0; j < by.Count; j++)
                            {
                                int count = i * by.Count + j;

                                Bitmap bitmap = Bbitmaps[count];
                                bitmap.Save("d:\\newpic\\2" + i.ToString() + ".bmp", ImageFormat.Bmp);
                                bitmap = PicSized(bitmap, 4);
                                bitmap.Save("d:\\newpic\\" + i.ToString() + ".bmp", ImageFormat.Bmp);
                                //bitmap.
                                Emgu.CV.Image<Bgr, Byte> currentFrame = new Emgu.CV.Image<Bgr, Byte>(bitmap);
                                //mats.Add( currentFrame.Mat);
                                Mat invert = new Mat();
                                CvInvoke.BitwiseAnd(currentFrame, currentFrame, invert);
                                //Mat temp = aa.ToImage<Bgr, byte>().GetSubRect(new Rectangle(i * patchHeight, j * patchWidth, invert.Size.Width, invert.Size.Height)).Mat;
                                //temp.Save("d:\\newpic\\temp" + i.ToString() + "_" + j.ToString() + "jpg");
                                //invert.CopyTo(temp);
                                aa.Save("d:\\newpic\\aa" + i.ToString() + "_" + j.ToString() + "jpg");
                                AoiAi.addPatch(aa.Ptr, invert.Ptr, (float)j * patchWidth, (float)i * patchHeight);
                                invert.Dispose();
                            }

                        }
                    }


                    Image<Bgr, Byte> _image = aa.ToImage<Bgr, Byte>();
                    Bitmap allbitmap = _image.Bitmap;
                    //Bitmap tt = new Bitmap(aa.Cols, aa.Rows, (int)aa.Step, PixelFormat.Format24bppRgb, aa.Ptr);
                    pbMainImg.Image = allbitmap;
                    CvInvoke.NamedWindow("AJpg", NamedWindowType.Normal); //创建一个显示窗口
                    CvInvoke.Imshow("AJpg", aa); //显示图片
                                                 //mats = null;
                    aa.Dispose();
                }
                catch (Exception e)
                {
                    Loghelper.WriteLog("报错了", e);


                }
            }
        }

        /// <summary>
        /// 放大缩小图片尺寸
        /// </summary>
        /// <param name="picPath"></param>
        /// <param name="reSizePicPath"></param>
        /// <param name="iSize"></param>
        /// <param name="format"></param>
        public Bitmap PicSized(Bitmap bitmap , int iSize)
        {
            //Bitmap originBmp = new Bitmap(picPath);
            int w = bitmap.Width / iSize;
            int h = bitmap.Height / iSize;
            Bitmap resizedBmp = new Bitmap(w, h, PixelFormat.Format24bppRgb);
            Graphics g = Graphics.FromImage(resizedBmp);
            //设置高质量插值法  
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度  
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            //消除锯齿
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.DrawImage(bitmap, new Rectangle(0, 0, w, h), new Rectangle(0, 0, bitmap.Width, bitmap.Height), GraphicsUnit.Pixel);
            //resizedBmp.Save(reSizePicPath, format);
            g.Dispose();
            //originBmp.Dispose();
            return resizedBmp;

        }

        private void button10_Click_1(object sender, EventArgs e)
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
        
        private void runplace() {
            double value = Convert.ToDouble(IniFile.iniRead("Kwidth", "kwidth")) +Convert.ToDouble(tbPcbWidth.Text)*1562.5;
            double rate = 1000.0;
            int registerAddress = 2124;
            int wordBit = 32;
            byte[] receiveData = new byte[255];
            value = value ;
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

        private void fengmiqi_MouseDown(object sender, MouseEventArgs e)
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

        private void fengmiqi_MouseUp(object sender, MouseEventArgs e)
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

        private void Openchanle_MouseDown(object sender, MouseEventArgs e)
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

        private void Openchanle_MouseUp(object sender, MouseEventArgs e)
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

        private void pingbi_Click(object sender, EventArgs e)
        {
            PingbiForm pingbiForm = new PingbiForm();
            pingbiForm.ShowDialog();

        }
    }
}

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
        public static List<Bitmap> Abitmaps;
        public static List<Bitmap> Bbitmaps;
        public static PictureBox pbmain;
        public BaslerCamera.OnlyGetBitmapCallback cameraBitmapCallback = new BaslerCamera.OnlyGetBitmapCallback(MyCameraCallback);


        public static void MyCameraCallback(string cameraId, Bitmap bitmap)
        {
            //这里处理图片两个相机可以传入同一个函数，根据cameraId来区分正反面
            if (cameraId == cameraAid)
            {
                pbmain.Image = bitmap;
                Abitmaps.Add(bitmap);                            
            }
            else {
                Bbitmaps.Add(bitmap);
            }
        }
        BaslerCamera aa;
        #region camera
        private ImageProvider m_imageProvider = new ImageProvider(); /* Create one image provider. */
        private Bitmap m_bitmap = null;


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
                    //if (BitmapFactory.IsCompatible(m_bitmap, image.Width, image.Height, image.Color))
                    //{
                    //    /* Update the bitmap with the image data. */
                    //    BitmapFactory.UpdateBitmap(m_bitmap, image.Buffer, image.Width, image.Height, image.Color);
                    //    /* To show the new image, request the display control to update itself. */
                    //    pictureBox.Refresh();
                    //}
                    //else /* A new bitmap is required. */
                    //{
                    //    BitmapFactory.CreateBitmap(out m_bitmap, image.Width, image.Height, image.Color);
                    //    BitmapFactory.UpdateBitmap(m_bitmap, image.Buffer, image.Width, image.Height, image.Color);
                    //    /* We have to dispose the bitmap after assigning the new one to the display control. */
                    //    Bitmap bitmap = pictureBox.Image as Bitmap;
                    //    /* Provide the display control with the new bitmap. This action automatically updates the display. */
                    //    pictureBox.Image = m_bitmap;
                    //    if (bitmap != null)
                    //    {
                    //        /* Dispose the bitmap. */
                    //        bitmap.Dispose();
                    //    }
                    //}
                    /* The processing of the image is done. Release the image buffer. */
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
            pLeftToolbox.Hide();
            pBottomToolbox.Hide();
            pCenterXY.Hide();
            pPcbInfo.Hide();
            pFrontInfo.Hide();
            pPcbInfoTitle.Hide();
            pMain.Hide();
            from = i;
            cameraAid = IniFile.iniRead("CameraA", "SerialNumber");
            cameraBid = IniFile.iniRead("CameraB", "SerialNumber");
            //hidebutton();
            //asc.Initialize(this);

            aa = new BaslerCamera();
            // if (aa.RunCamera(cameraBid, cameraBitmapCallback) == 0)
            // {
            //     MessageBox.Show("打开相机失败");
            // }

            #region camera

            m_imageProvider.GrabErrorEvent += new ImageProvider.GrabErrorEventHandler(OnGrabErrorEventCallback);
            m_imageProvider.DeviceRemovedEvent += new ImageProvider.DeviceRemovedEventHandler(OnDeviceRemovedEventCallback);
            m_imageProvider.DeviceOpenedEvent += new ImageProvider.DeviceOpenedEventHandler(OnDeviceOpenedEventCallback);
            m_imageProvider.DeviceClosedEvent += new ImageProvider.DeviceClosedEventHandler(OnDeviceClosedEventCallback);
            m_imageProvider.GrabbingStartedEvent += new ImageProvider.GrabbingStartedEventHandler(OnGrabbingStartedEventCallback);
            m_imageProvider.ImageReadyEvent += new ImageProvider.ImageReadyEventHandler(OnImageReadyEventCallback);
            m_imageProvider.GrabbingStoppedEvent += new ImageProvider.GrabbingStoppedEventHandler(OnGrabbingStoppedEventCallback);
            m_imageProvider.Open(m_imageProvider.GetDevice(cameraAid));
            m_imageProvider.ContinuousShot();
            #endregion


            pbmain = this.pbMainImg;
            //aa.Dispose(); // 如果不使用相机一定需要主动调用Dispose来释放，否则等待系统回收可能要很久
        }
        public void dododo()
        {
            aa.TestGrabber();
        }
        public void closeCamera()
        {
            aa.Dispose();
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
                }
                else {
                    Abitmaps = new List<Bitmap>();
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
            pbBackImg.Refresh();
            PictureBox p = (PictureBox)sender;
            Pen pp = new Pen(Color.Blue);
            using (Graphics gc = p.CreateGraphics()) {
                Rectangle rect = new Rectangle();
                rect.Location = new Point(0,0);
                rect.Size = new Size(pbFrontImg.Width-3,pbFrontImg.Height-3);
                gc.DrawRectangle(pp, rect);

            }
            pbMainImg.Image = p.Image;
            tbFrontOrBack.Text = "正面";
            Settings.Default.frontorside = "front";
            foreach (Control control in pbMainImg.Controls) {
                if (control is PictureBox) {
                    pbMainImg.Controls.Remove(control);                
                }
            }
            
            drawpicboxall();
            pbMainImg.Height = oldlastheight;
            pbMainImg.Width = oldlastwidth;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pbFrontImg.Refresh();
            PictureBox p = (PictureBox)sender;
            Pen pp = new Pen(Color.Blue);
            using (Graphics gc = p.CreateGraphics())
            {
                Rectangle rect = new Rectangle();
                rect.Location = new Point(0,0);
                rect.Size = new Size(p.Width - 3, p.Height - 3);
                gc.DrawRectangle(pp, rect);

            }
            pbMainImg.Image = p.Image;
            tbFrontOrBack.Text = "反面";
            Settings.Default.frontorside = "side";
            drawpicboxall();
            pbMainImg.Height = oldlastheight;
            pbMainImg.Width = oldlastwidth;

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
            oldlastheight = pbMainImg.Height;
            oldlastwidth = pbMainImg.Width;
            pbMainImg.Height = pbMainImg.Image.Height;
            pbMainImg.Width = pbMainImg.Image.Width;
            string selectall = "select zijiname from zijiban ";
            DataTable dataTable = SQLiteHelper.GetDataTable(selectall);
            for (int i=0;i<dataTable.Rows.Count;i++) {
                string name = dataTable.Rows[i]["zijiname"].ToString();
                foreach (Control control in pbMainImg.Controls) {
                    if (control.Name == name) {
                        string updatesql = string.Format("update zijiban set startx='{0}',starty='{1}',width='{2}',height='{3}',isuse = 1 where zijiname='{4}'",control.Location.X,control.Location.Y,control.Width,control.Height,name);
                        SQLiteHelper.ExecuteSql(updatesql);

                    }
                
                }

            
            }
            pbMainImg.Height = oldlastheight;
            pbMainImg.Width = oldlastwidth;
            MessageBox.Show("保存成功");


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
            LightsourceForm lightsourceForm = new LightsourceForm();
            lightsourceForm.ShowDialog();
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
    }
}

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
        bool issave = false;
        int elementnum = 0;
        //int marknum = 0;
        private PickBox pb = new PickBox();
        FileHandler filehandler;
        bool isSelected = false;
        Point mouseDownPoint;
        PictureBox pictureBoxnew;
        int lastwidth = 0;
        int lastheight=0;
        int zijibannum = 0;
        List<PictureBox> pictureBoxes = new List<PictureBox>();
        int from;
        int oldlastwidth =0;
        int oldlastheight = 0;
        AutoSizeFormClass asc = new AutoSizeFormClass();
        Control controlnew;

        void OnImageGrabbed(Object sender, ImageGrabbedEventArgs e)
        {
      
        }


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
            //hidebutton();
            //asc.Initialize(this);
            BaslerCamera aa = new BaslerCamera();
            if (aa.RunCamera("正面", OnImageGrabbed) == 0) {
                MessageBox.Show("打开相机失败");
            }

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (from==1) {
                Workspace workspace = new Workspace();
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
            
            lastwidth = pbMainImg.Width;
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
            pbMainImg.Height = oldlastheight;
            pbMainImg.Width = oldlastwidth;

            pbMainImg.MouseWheel += PbMainImg_MouseWheel;
            this.MouseWheel += PbMainImg_MouseWheel;


        }

        private void PbMainImg_MouseWheel(object sender, MouseEventArgs e)
        {
            int i = e.Delta * SystemInformation.MouseWheelScrollLines / 5;
            //double num = 1.1;
            //Console.WriteLine(i);
            //foreach (Control control in pictureBox1.Controls)
            //{
            //    control.Location = new Point(Convert.ToInt32(control.Location.X * num), Convert.ToInt32(control.Location.Y * num));
            //    control.Width = Convert.ToInt32(control.Width * num);
            //    control.Height = Convert.ToInt32(control.Height * num);
            //}
            //foreach (Control control in pictureBox1.Controls)
            //{
            //    control.Location = new Point(Location.X +i/2, Location.Y +i/2);
            //    control.Width = control.Width +i;
            //    control.Height = control.Height +i;
            //}
            pbMainImg.Width = pbMainImg.Width + i;//增加picturebox的宽度
            pbMainImg.Height = pbMainImg.Height + i;
            // Console.WriteLine(pictureBox1.Width.ToString() + pictureBox1.Height.ToString());
            pbMainImg.Left = pbMainImg.Left - i / 2;//使picturebox的中心位于窗体的中心
            pbMainImg.Top = pbMainImg.Top - i / 2;//进而缩放时图片也位于窗体的中心
            oldlastheight = pbMainImg.Height;
            oldlastwidth = pbMainImg.Width;

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
                    Ruleresult ruleresult = new Ruleresult(startpoint,endpoint);
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
                //using (Graphics gc = pictureBox1.CreateGraphics())
                //using (Pen pen = new Pen(Color.Green))
                //{
                //    //设置画笔的宽度
                //    pen.Width = 1;
                //    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
                //    RectangleF rect = new RectangleF();
                //    rect.Location = pictureBox1.Location;
                //    rect.Size = pictureBox1.Size;
                //    //确保在画图区域
                //    pictureBox1.Refresh();
                //    //画竖线
                //    gc.DrawLine(pen, 0, 0, 0, 0);
                //    //画横线
                //    gc.DrawLine(pen, 0, 0, 0, 0);
                //}
                ////this.pictureBox1.Location = new Point(pictureBox1.Location.X + 0, pictureBox1.Location.Y - 200);
                //using (Graphics gc = pictureBox1.CreateGraphics())
                //using (Pen pen = new Pen(Color.Green))
                //{
                //    //设置画笔的宽度
                //    pen.Width = 1;
                //    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
                //    RectangleF rect = new RectangleF();
                //    rect.Location = pictureBox1.Location;
                //    rect.Size = pictureBox1.Size;
                //    //确保在画图区域
                //    if (rect.Contains(pictureBox1.Location))
                //    {
                //        pictureBox1.Refresh();
                //        //画竖线
                //        gc.DrawLine(pen, e.X, 0, e.X, rect.Bottom);
                //        //画横线
                //        gc.DrawLine(pen, 0, e.Y, rect.Right, e.Y);


                //    }
                //}
                //showponit(e.X, e.Y);
                //centerpoint = new Point(e.X, e.Y);
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
                this.pbMainImg.Left = this.pbMainImg.Left + (Cursor.Position.X - mouseDownPoint.X);
                this.pbMainImg.Top = this.pbMainImg.Top + (Cursor.Position.Y - mouseDownPoint.Y);
                mouseDownPoint.X = Cursor.Position.X;
                mouseDownPoint.Y = Cursor.Position.Y;
                //drawlineact();
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
            Workspace workspace = new Workspace();
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
            issave = true;
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            string deletesql = "delete from zijiban where isuse = 0";
            SQLiteHelper.ExecuteSql(deletesql);
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
        /// <summary>
        /// 放大，缩小图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            int i = e.Delta * SystemInformation.MouseWheelScrollLines / 5;
            //double num = 1.1;
            //Console.WriteLine(i);
            //foreach (Control control in pictureBox1.Controls)
            //{
            //    control.Location = new Point(Convert.ToInt32(control.Location.X * num), Convert.ToInt32(control.Location.Y * num));
            //    control.Width = Convert.ToInt32(control.Width * num);
            //    control.Height = Convert.ToInt32(control.Height * num);
            //}
            //foreach (Control control in pictureBox1.Controls)
            //{
            //    control.Location = new Point(Location.X +i/2, Location.Y +i/2);
            //    control.Width = control.Width +i;
            //    control.Height = control.Height +i;
            //}
            pbMainImg.Width = pbMainImg.Width +i;//增加picturebox的宽度
            pbMainImg.Height = pbMainImg.Height+i;
            // Console.WriteLine(pictureBox1.Width.ToString() + pictureBox1.Height.ToString());
            pbMainImg.Left = pbMainImg.Left - i/2;//使picturebox的中心位于窗体的中心
            pbMainImg.Top = pbMainImg.Top -i / 2;//进而缩放时图片也位于窗体的中心
            oldlastheight = pbMainImg.Height;
            oldlastwidth = pbMainImg.Width;



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
            Console.WriteLine(pbMainImg.Location);
            showponit((pbMainImg.Width- pbMainImg.Location.X*2) /2 , (pbMainImg.Height - pbMainImg.Location.Y*2) / 2);
            //centerpoint = new Point((pictureBox1.Width) / 2, (pictureBox1.Height) / 2);


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Collectionform collectionform = new Collectionform();
            collectionform.ShowDialog();
            if (collectionform.DialogResult == System.Windows.Forms.DialogResult.OK) {
                collection collection = new collection();
                collection = collectionform.Tag as collection;
                MessageBox.Show("111");
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
                if (pictureBoxes.Count > 0)
                {
                    foreach (PictureBox pictureBox in pictureBoxes)
                    {
                        pictureBox.Refresh();
                    }
                    pictureBoxes.Clear();
                }
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
                pictureBox.Parent = pbMainImg;
                this.pbMainImg.Controls.Add(pictureBox);
                //MessageBox.Show(pictureBox.Parent.Name);
                foreach (Control c in this.pbMainImg.Controls)
                {
                    if (c.Name == pictureBox.Name)
                    {
                        c.BringToFront();
                    }

                }
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
    }
}

using pcbaoi.Properties;
using pcbaoi.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pcbaoi
{
    public partial class Platmake : Form
    {
        Point centerpoint;
        //自定义控件
        private PickBox pb = new PickBox();
        //增加的算法框数量
        int addpicturebox = 0;
        Control thiscontrol;
        bool isSelected = false;
        Point mouseDownPoint;
        //自适应显示
        AutoSizeFormClass asc = new AutoSizeFormClass();
        Operatorselect Addopera;
        Operatorselect useroperatoe = new Operatorselect();
        int oldlastwidth = 0;
        int oldlastheight = 0;
        private bool pulleyStop = false;
        private bool pulleySearchStop = true;

        public Platmake(Image image)
        {
            InitializeComponent();
            PbMain.Image = image;
            this.MouseWheel += PbMain_MouseWheel;
            asc.RenewControlRect(PbMain);
            Rectangle rectangle = ImageManger.GetPictureBoxZoomSize(PbMain);
            PbMain.Top = rectangle.Y;
            PbMain.Left = rectangle.X;
            PbMain.Width = rectangle.Width;
            PbMain.Height = rectangle.Height;

            addnum();
            //picturestart();
        }
        #region 左边菜单栏

        private void AddAlgorithmBox_Click(object sender, EventArgs e)
        {
            Addopera = new Operatorselect();
            AlgorithmSelect algorithmSelect = new AlgorithmSelect();
            algorithmSelect.ShowDialog();
            if (algorithmSelect.DialogResult == DialogResult.OK)
            {
                Algorithmtype algorithmtype = algorithmSelect.Tag as Algorithmtype;
                //userControl1.MyEvent += usercontroler1_Myevent;
                //panel7.Controls.Add(userControl1);
                foreach (Control control in Userpanel.Controls)
                {
                    if (control is UserControl1)
                    {
                        Userpanel.Controls.Remove(control);
                    }

                }
                int i = RtDg.Rows.Count;
                RtDg.Rows.Add();
                RtDg["Column1", i].Value = algorithmtype.Typename + addpicturebox;
                RtDg["Column2", i].Value = (addpicturebox + 1).ToString();
                RtDg["Column3", i].Value = algorithmtype.Owmername;

                PictureBox pictureBox = new PictureBox();
                pictureBox.Name = (addpicturebox + 1).ToString();
                pictureBox.Location = new Point(200, 200);
                pictureBox.Width = 70;
                pictureBox.Height = 70;
                pictureBox.BorderStyle = BorderStyle.None;
                //pictureBox.DoubleClick += new EventHandler(pictureboxshow);
                pictureBox.SizeChanged += new EventHandler(pictureboxsizechange);
                pictureBox.Move += new EventHandler(pictureboxmove);
                pictureBox.BackColor = Color.Transparent;
                pictureBox.Paint += otherpicbox_Panit;
                pictureBox.Parent = PbMain;
                //pictureBox1.Hide();
                this.PbMain.Controls.Add(pictureBox);
                //MessageBox.Show(pictureBox.Parent.Name);
                foreach (Control c in PbMain.Controls)
                {
                    if (c.Name == pictureBox.Name)
                    {
                        c.BringToFront();

                    }
                    else
                    {
                        c.Visible = false;
                    }

                }
                //Form1_Load(null,null);
                pb.WireControl(pictureBox);
                pb.m_control = pictureBox;
                thiscontrol = pictureBox;
                if (algorithmtype.Typename == "BAD MARK" || algorithmtype.Typename == "MARK")
                {
                    PictureBox littlepicturebox = new PictureBox();
                    littlepicturebox.Name = pictureBox.Name + "littlebox" + addpicturebox;
                    littlepicturebox.Location = new Point(15, 15);
                    littlepicturebox.Width = 40;
                    littlepicturebox.Height = 40;
                    littlepicturebox.BorderStyle = BorderStyle.None;
                    //pictureBox.DoubleClick += new EventHandler(pictureboxshow);
                    littlepicturebox.SizeChanged += new EventHandler(pictureboxsizechange);
                    littlepicturebox.Move += new EventHandler(pictureboxmove);
                    littlepicturebox.BackColor = Color.Transparent;
                    littlepicturebox.Paint += otherpicbox_Panit;
                    littlepicturebox.Parent = pictureBox;
                    //pictureBox1.Hide();
                    pictureBox.Controls.Add(littlepicturebox);
                    //MessageBox.Show(pictureBox.Parent.Name);
                    foreach (Control c in pictureBox.Controls)
                    {
                        if (c.Name == littlepicturebox.Name)
                        {
                            c.BringToFront();
                        }
                        else
                        {
                            c.Visible = false;
                        }
                    }
                    //Form1_Load(null,null);
                    pb.WireControl(littlepicturebox);
                }
                addpicturebox++;
                Addopera.Algorithm = algorithmtype.Typename;
                UserControl1 userControl1 = new UserControl1(Addopera);
                userControl1.MyEvent += updatedatagrade;
                Addopera = userControl1.Tag as Operatorselect;
                RtDg["Column4", i].Value = Addopera;
                Userpanel.Controls.Add(userControl1);
                RtDg.CurrentCell = RtDg[0, i];
            }
            asc = new AutoSizeFormClass();
            asc.RenewControlRect(PbMain);



        }
        private void Save_Click(object sender, EventArgs e)
        {
            try
            {
                oldlastheight = PbMain.Height;
                oldlastwidth = PbMain.Width;
                PbMain.Height = PbMain.Image.Height;
                PbMain.Width = PbMain.Image.Width;
                Bitmap bigbitmap = (Bitmap)PbMain.Image.Clone();
                for (int i = 0; i < RtDg.Rows.Count; i++)
                {
                    string operatonameall = RtDg.Rows[i].Cells[0].Value.ToString();
                    string outpicturename = RtDg.Rows[i].Cells[1].Value.ToString();
                    string parent = RtDg.Rows[i].Cells[2].Value.ToString();
                    Operatorselect insertoperatorselect = (Operatorselect)RtDg.Rows[i].Cells[3].Value;
                    int outstartx = 0;
                    int outstarty = 0;
                    int outheight = 0;
                    int outwidth = 0;
                    string inpicturename = "";
                    int instartx = 0;
                    int instarty = 0;
                    int inheight = 0;
                    int inwidth = 0;
                    foreach (Control control in PbMain.Controls)
                    {

                        if (control.Name == outpicturename)
                        {
                            outstartx = control.Location.X;
                            outstarty = control.Location.Y;
                            outheight = control.Height;
                            outwidth = control.Width;
                            Rectangle rectangle = new Rectangle(outstartx, outstarty, outheight, outwidth);
                            foreach (Control control1 in control.Controls)
                            {
                                if (control1 is PictureBox)
                                {
                                    inpicturename = control1.Name;
                                    instartx = control1.Location.X;
                                    instarty = control1.Location.Y;
                                    inheight = control1.Height;
                                    inwidth = control1.Width;
                                }
                            }

                            Bitmap bitmap = ImageManger.CropImage(bigbitmap, rectangle);
                            bitmap.Save(Settings.Default.path + "template\\" + operatonameall + ".jpg", ImageFormat.Jpeg);
                            bitmap.Dispose();

                        }
                    }
                    string selectsql = string.Format("select* from operato where operatonameall = '{0}'", operatonameall);
                    DataTable selectnum = SQLiteHelper.GetDataTable(selectsql);
                    if (selectnum.Rows.Count == 0)
                    {
                        string insertsql = string.Format("INSERT INTO operato (operatonameall,outpicturename,outstartx,outstarty,outheight,outwidth,intpicturename,instartx,instarty,inheight,inwidth,parent,algorithm,operatorname,confidence,percentageup,percentagedown,codetype,luminanceon,luminancedown,rednumon,rednumdown,greennumon,greennumdown,bluenumon,bluenumdown,picname,frontorside )VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}' ,'{26}','{27}')", operatonameall, outpicturename, outstartx, outstarty, outheight, outwidth, inpicturename, instartx, instarty, inheight, inwidth, parent, insertoperatorselect.Algorithm, insertoperatorselect.Operatorname, insertoperatorselect.Confidence, insertoperatorselect.Percentageup, insertoperatorselect.Rednumdown, insertoperatorselect.Codetype, insertoperatorselect.Luminanceon, insertoperatorselect.Luminancedown, insertoperatorselect.Rednumon, insertoperatorselect.Rednumdown, insertoperatorselect.Greennumon, insertoperatorselect.Greennumdown, insertoperatorselect.Bluenumon, insertoperatorselect.Bluenumdown, operatonameall + ".jpg", Settings.Default.frontorside);
                        int num = SQLiteHelper.ExecuteSql(insertsql);

                    }
                    else
                    {
                        string updatesql = string.Format("update operato set outpicturename='{0}',outstartx='{1}',outstarty='{2}',outheight='{3}',outwidth='{4}',intpicturename='{5}',instartx='{6}',instarty='{7}',inheight='{8}',inwidth='{9}',parent='{10}',algorithm='{11}',operatorname='{12}',confidence='{13}',percentageup='{14}',percentagedown='{15}',codetype='{16}',luminanceon='{17}',luminancedown='{18}',rednumon='{19}',rednumdown='{20}',greennumon='{21}',greennumdown='{22}',bluenumon='{23}',bluenumdown='{24}',frontorside='{25}' where operatonameall = '{26}'", outpicturename, outstartx, outstarty, outheight, outwidth, inpicturename, instartx, instarty, inheight, inwidth, parent, insertoperatorselect.Algorithm, insertoperatorselect.Operatorname, insertoperatorselect.Confidence, insertoperatorselect.Percentageup, insertoperatorselect.Rednumdown, insertoperatorselect.Codetype, insertoperatorselect.Luminanceon, insertoperatorselect.Luminancedown, insertoperatorselect.Rednumon, insertoperatorselect.Rednumdown, insertoperatorselect.Greennumon, insertoperatorselect.Greennumdown, insertoperatorselect.Bluenumon, insertoperatorselect.Bluenumdown, Settings.Default.frontorside, operatonameall);
                        SQLiteHelper.ExecuteSql(updatesql);
                    }
                }
                PbMain.Height = oldlastheight;
                PbMain.Width = oldlastwidth;
                bigbitmap.Dispose();
                GC.Collect();

            }
            catch (Exception ex)
            {
                Loghelper.WriteLog("算子编辑界面---保存错误",ex);
            
            
            }

        }
        private void Exit_Click(object sender, EventArgs e)
        {
            CaptureForm form1 = new CaptureForm(2);
            form1.Show();
            this.Hide();

        }
        #endregion
        
        private void Platmake_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }


        private void Platmake_Shown(object sender, EventArgs e)
        {
            //picturestart();
        }

        private void PbMain_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void RtDg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Addopera = new Operatorselect();
                if (e.ColumnIndex != -1)
                {
                    DataGridViewColumn column = RtDg.Columns[e.ColumnIndex];
                    string picboxname = RtDg.CurrentRow.Cells[1].Value.ToString();
                    foreach (Control control in PbMain.Controls)
                    {
                        if (control.Name == picboxname)
                        {
                            control.Visible = true;
                            control.BringToFront();
                            thiscontrol = control;

                        }
                        else
                        {
                            control.Visible = false;
                        }


                    }
                    foreach (Control control1 in Userpanel.Controls)
                    {
                        if (control1 is UserControl1)
                        {
                            Userpanel.Controls.Remove(control1);

                        }

                    }
                    UserControl1 userControl1 = new UserControl1(RtDg.CurrentRow.Cells[3].Value);
                    userControl1.MyEvent += updatedatagrade;
                    Addopera = userControl1.Tag as Operatorselect;
                    Userpanel.Controls.Add(userControl1);

                }

            }
            catch (Exception ex)
            {
                Loghelper.WriteLog("Platmake界面---选择框错误",ex);


            }


        }

        private void RtDg_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //自动编号，与数据无关
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
               e.RowBounds.Location.Y,
               RtDg.RowHeadersWidth - 4,
               e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics,
                  (e.RowIndex + 1).ToString(),
                   RtDg.RowHeadersDefaultCellStyle.Font,
                   rectangle,
                   RtDg.RowHeadersDefaultCellStyle.ForeColor,
                   TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }




        private bool IsMouseInPanel()
        {
            if (this.PbMain.Left < PointToClient(Cursor.Position).X
            && PointToClient(Cursor.Position).X < this.PbMain.Left + this.PbMain.Width
            && this.PbMain.Top < PointToClient(Cursor.Position).Y
            && PointToClient(Cursor.Position).Y < this.PbMain.Top + this.PbMain.Height)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void PbMain_MouseWheel(object sender, MouseEventArgs e)
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


            if (i >= 0)
            {
                PbMain.Width = (int)(PbMain.Width * 1.1);//因为Widthh和Height都是int类型，所以要强制转换一下-_-||
                PbMain.Height = (int)(PbMain.Height * 1.1);
            }
            else
            {
                PbMain.Width = (int)(PbMain.Width * 0.9);
                PbMain.Height = (int)(PbMain.Height * 0.9);
            }


                ////这里可以写你要执行的代码
                //if (i > 0)
                //{
                //    i = 72;
                //}
                //else
                //{
                //    i = -72;
                //}

                //if (PbMain.Height + i > 300)
                //{
                //    PbMain.Width = PbMain.Width + i;//增加picturebox的宽度
                //    PbMain.Height = PbMain.Height + i;
                //    // Console.WriteLine(pictureBox1.Width.ToString() + pictureBox1.Height.ToString());
                //    PbMain.Left = PbMain.Left - i / 2;//使picturebox的中心位于窗体的中心
                //    PbMain.Top = PbMain.Top - i / 2;//进而缩放时图片也位于窗体的中心
                //    //pbMainImg.Location = new Point(pbMainImg.Location.X- i / 2, pbMainImg.Location.Y - i / 2);
                //    oldlastheight = PbMain.Height;
                //    oldlastwidth = PbMain.Width;
                //}

                //让滑轮变得可以触发方法
                pulleySearchStop = true;

        }

        private void PbMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                mouseDownPoint.X = Cursor.Position.X;  //注：全局变量mouseDownPoint前面已定义为Point类型

                mouseDownPoint.Y = Cursor.Position.Y;
                isSelected = true;
            }


        }

        private void PbMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (isSelected && IsMouseInPanel())
            {
                PictureBox p = (PictureBox)sender;
                p.Refresh();
                this.PbMain.Left = this.PbMain.Left + (Cursor.Position.X - mouseDownPoint.X);
                this.PbMain.Top = this.PbMain.Top + (Cursor.Position.Y - mouseDownPoint.Y);
                mouseDownPoint.X = Cursor.Position.X;
                mouseDownPoint.Y = Cursor.Position.Y;
                //drawlineact();
            }
        }

        private void PbMain_MouseUp(object sender, MouseEventArgs e)
        {
            isSelected = false;
        }

        private void PbMain_SizeChanged(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            control.Refresh();
            asc.ControlAutoSize(PbMain);
        }
        private void pictureboxmove(object sender, EventArgs e)
        {
            Console.WriteLine("进入移动");
            Control control = (Control)sender;
            control.Parent.Refresh();
            control.Refresh();
            //control.Parent.Refresh();
            //pictureBox1.Refresh();
            asc = new AutoSizeFormClass();
            asc.RenewControlRect(PbMain);

        }
        private void pictureboxsizechange(object sender, EventArgs e)
        {
            Console.WriteLine("进入缩放");
            Control control = (Control)sender;
            control.Parent.Refresh();
            control.Refresh();
            //control.Parent.Refresh();
            asc = new AutoSizeFormClass();
            asc.RenewControlRect(PbMain);

        }
        void updatedatagrade(object sender, EventArgs e)
        {
            Operatorselect operatorselect = (Operatorselect)sender;
            //operatorselect1 = operatorselect;
            RtDg.CurrentRow.Cells[3].Value = operatorselect;


        }


        private void addnum()
        {
            string selectsql = "select id from operato order by id desc";
            DataTable dataTable = SQLiteHelper.GetDataTable(selectsql);
            if (dataTable.Rows.Count > 0)
            {
                addpicturebox = Convert.ToInt32(dataTable.Rows[0]["id"].ToString());

            }


        }
        private void otherpicbox_Panit(object sender, PaintEventArgs e)
        {            
            Pen pp = new Pen(Color.FromArgb(0, 235, 6));
            e.Graphics.DrawRectangle(pp, e.ClipRectangle.X, e.ClipRectangle.Y,
            e.ClipRectangle.X + e.ClipRectangle.Width - 1,
            e.ClipRectangle.Y + e.ClipRectangle.Height - 1);
        }

        private void picturestart()
        {
            using (Graphics gc = PbMain.CreateGraphics())
            using (Pen pen = new Pen(Color.Green))
            {
                //设置画笔的宽度
                pen.Width = 1;
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
                RectangleF rect = new RectangleF();
                rect.Location = PbMain.Location;
                rect.Size = PbMain.Size;
                //确保在画图区域
                if (rect.Contains(PbMain.Location))
                {
                    PbMain.Refresh();
                    //画竖线
                    gc.DrawLine(pen, (PbMain.Width) / 2, 0, (PbMain.Width) / 2, rect.Bottom);
                    //画横线
                    gc.DrawLine(pen, 0, (PbMain.Height) / 2, rect.Right, (PbMain.Height) / 2);


                }
            }
            //showponit((pictureBox1.Width) / 2, (pictureBox1.Height) / 2);
            centerpoint = new Point((PbMain.Width) / 2, (PbMain.Height) / 2);

        }
    }
}

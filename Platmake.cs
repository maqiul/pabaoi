using Cyotek.Windows.Forms.Demo;
using pcbaoi.Algorithms;
using pcbaoi.Algorithms.Model;
using pcbaoi.Model;
using pcbaoi.Properties;
using pcbaoi.Tools;
using RTree;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rectangle = System.Drawing.Rectangle;
using RTRectangle = RTree.Rectangle;

namespace pcbaoi
{
    public partial class Platmake : Form
    {
        OperatorSelect Addopera;
        OperatorSelect useroperatoe = new OperatorSelect();

        bool isAi = true;
        object oldRegion;

        Snowflake snowflake = new Snowflake(14);

        Dictionary<long, AiDetectRect> _dictRect;

        RTree<long> tree = new RTree<long>();

        AiDetectRect workingAiRegion;
        AiDetectRect otheraiDetectRect = new AiDetectRect();
        ImageBoxEx imgBoxWorkSpace = new ImageBoxEx();
        private int addpicturebox;


        public Platmake(Image image)
        {
            InitializeComponent();
            _dictRect = new Dictionary<long, AiDetectRect>();

            imgBoxWorkSpace.Location = new System.Drawing.Point(0, 0);
            imgBoxWorkSpace.Width = Centerpanel.Width;
            imgBoxWorkSpace.Height = Centerpanel.Height;
            imgBoxWorkSpace.BackColor = Color.Black;
            imgBoxWorkSpace.GridColor = Color.Black;
            imgBoxWorkSpace.GridColorAlternate = Color.Black;
            Centerpanel.Controls.Add(imgBoxWorkSpace);

            imgBoxWorkSpace.SelectionMode = Cyotek.Windows.Forms.ImageBoxSelectionMode.Rectangle;
            imgBoxWorkSpace.Image = image;
            imgBoxWorkSpace.Selected += ImgBoxWorkSpace_Selected;
            imgBoxWorkSpace.Selecting += ImgBoxWorkSpace_Selecting;
            imgBoxWorkSpace.SelectionRegionChanged += ImgBoxWorkSpace_SelectionRegionChanged;
            imgBoxWorkSpace.MouseDown += ImgBoxWorkSpace_MouseDown;
            imgBoxWorkSpace.MouseUp += ImgBoxWorkSpace_MouseUp;
            imgBoxWorkSpace.MouseMove += ImgBoxWorkSpace_MouseMove;
            imgBoxWorkSpace.Paint += ImgBoxWorkSpace_Paint;

            imgBoxWorkSpace.ZoomToFit();
            addnum();
            Console.WriteLine(imgBoxWorkSpace.Image);
            //picturestart();

            //Random rnd = new Random(); //在外面生成对象

            //workingAiRegion = new AiDetectRect();
            //workingAiRegion.algorithmsName = "test";
            //workingAiRegion.operatorName = "test2222";
            //workingAiRegion.rectangle = new Rectangle(90, 90, 50, 60);
            //_listRect.Add(workingAiRegion);
            //tree.Add(workingAiRegion.getRTreeRectangle(), 1000);
            //for (int i = 0; i < 10; i++)
            //{
            //    workingAiRegion = new AiDetectRect();
            //    workingAiRegion.id = snowflake.nextId();
            //    workingAiRegion.algorithmsName = "test" + i;
            //    workingAiRegion.operatorName = "test2222" + i;
            //    workingAiRegion.rectangle = new Rectangle(rnd.Next(540, 10000), rnd.Next(800, 5000), rnd.Next(300, 900), rnd.Next(100, 1000));
            //    _dictRect.Add(workingAiRegion.id, workingAiRegion);
            //    tree.Add(workingAiRegion.getRTreeRectangle(), workingAiRegion.id);
            //}
            //workingAiRegion = null;

            //tree.Delete(workingAiRegion.getRTreeRectangle(), workingAiRegion);
        }

        private void ImgBoxWorkSpace_Selecting(object sender, Cyotek.Windows.Forms.ImageBoxCancelEventArgs e)
        {
            //otheraiDetectRect = new AiDetectRect();
            if (otheraiDetectRect.id == 0) {
                return;
            
            }
        }

        /// <summary>
        /// 主要应用于当前工作框的放大缩小和拖动操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImgBoxWorkSpace_SelectionRegionChanged(object sender, EventArgs e)
        {
            //this.Text += "1";
            //workingAiRegion = new AiDetectRect();
            //workingAiRegion.id = snowflake.nextId();
            //workingAiRegion.rectangle = imgBoxWorkSpace.SelectionRegion;
            //workingAiRegion = new AiDetectRect();
            //workingAiRegion.id = snowflake.nextId();
            //workingAiRegion.rectangle = imgBoxWorkSpace.SelectionRegion;
            //if (workingAiRegion != null) {
            //    workingAiRegion.rectangle = imgBoxWorkSpace.SelectionRegion;
            //    //workingAiRegion = null;
            //    //MessageBox.Show(workingAiRegion.id.ToString());


            //}

        }

        private void ImgBoxWorkSpace_Paint(object sender, PaintEventArgs e)
        {
            Graphics g;
            GraphicsState originalState;
            Size scaledSize;
            Size originalSize;
            Size drawSize;
            bool scaleAdornmentSize;

            scaleAdornmentSize = true;

            g = e.Graphics;

            originalState = g.Save();

            // Work out the size of the marker graphic according to the current zoom level

            foreach (var item in _dictRect)
            {
                originalSize = item.Value.rectangle.Size.ToSize();
                scaledSize = imgBoxWorkSpace.GetScaledSize(originalSize);
                drawSize = scaleAdornmentSize ? scaledSize : originalSize;

                Rectangle location;

                // Work out the location of the marker graphic according to the current zoom level and scroll offset
                location = imgBoxWorkSpace.GetOffsetRectangle(item.Value.geDrawingRectangle());
                if (imgBoxWorkSpace.IsPointInImage(location.Location))
                {
                    g.InterpolationMode = InterpolationMode.NearestNeighbor;



                    g.SetClip(imgBoxWorkSpace.GetInsideViewPort(true));
                    //using (Brush brush = new SolidBrush(Color.FromArgb(128, Color.Blue)))
                    //{
                    //    g.FillRectangle(brush, location);
                    //}

                    using (Pen pen = new Pen(Color.Green))
                    {
                        g.DrawRectangle(pen, new Rectangle(location.Location, drawSize));
                    }

                    g.ResetClip();

                }
                // adjust the location so that the image is displayed above the location and centered to it
                //location.Y -= drawSize.Height;
                //location.X -= drawSize.Width >> 1;

                // Draw the marker
                //g.DrawImage(_markerImage, new Rectangle(location.Location, drawSize), new Rectangle(Point.Empty, originalSize), GraphicsUnit.Pixel);
            }

            g.Restore(originalState);
        }

        private void ImgBoxWorkSpace_MouseDown(object sender, MouseEventArgs e)
        {
            if (imgBoxWorkSpace.IsPointInImage(e.Location))
            {
                if (e.Button == MouseButtons.Left)
                {

                    //#region 先保存老的边框
                    //if (workingAiRegion != null)
                    //{
                    //    otheraiDetectRect = new AiDetectRect();
                    //    _dictRect.Add(workingAiRegion.id, workingAiRegion);
                    //    tree.Add(workingAiRegion.getRTreeRectangle(), workingAiRegion.id);
                    //    otheraiDetectRect = workingAiRegion;
                    //    //imgBoxWorkSpace.Invalidate();
                    //    workingAiRegion = null;
                    //    Console.WriteLine(_dictRect.Count);

                    //}
                    //#endregion
                    System.Drawing.Point point = imgBoxWorkSpace.PointToImage(e.Location);
                    ////var objects = tree.Intersects(new RTRectangle(e.X, e.Y, e.X + 1, e.Y + 1, 0, 0));
                    var objects = tree.Nearest(new RTree.Point(point.X, point.Y, 0), 0);
                    if (objects.Count == 0)
                    {
                        workingAiRegion = null;
                        //otheraiDetectRect = new AiDetectRect();
                        //if (otheraiDetectRect != null)
                        //{
                        //    otheraiDetectRect = new AiDetectRect();
                        //}
                        return;
                    }
                    //这里获取不到的原因是鼠标的x,y和图像的坐标位置不一样
                    try
                    {
                        long key = objects[0];
                        workingAiRegion = _dictRect[key];
                        imgBoxWorkSpace.SelectionRegion = workingAiRegion.rectangle;
                        otheraiDetectRect = workingAiRegion;
                        tree.Delete(workingAiRegion.getRTreeRectangle(), key);
                        _dictRect.Remove(key);
                        int rowid = DataGridviewUtils.Getrowid(RtDg,0,otheraiDetectRect.algorithmsName);
                        if (rowid != -99) {
                            RtDg.CurrentCell = RtDg.Rows[rowid].Cells[0];
                            DataGridViewRow row = RtDg.Rows[rowid];
                            useroperatoe = new OperatorSelect();
                            useroperatoe = (OperatorSelect)row.Cells[3].Value;
                            foreach (Control control1 in Userpanel.Controls)
                            {
                                if (control1 is OperatorSelection)
                                {
                                    Userpanel.Controls.Remove(control1);

                                }

                            }
                            OperatorSelection userControl1 = new OperatorSelection(useroperatoe);
                            userControl1.MyEvent += updatedatagrade;
                            Addopera = userControl1.Tag as OperatorSelect;
                            Userpanel.Controls.Add(userControl1);
                        }
                        workingAiRegion = null;

                        
                        //int index = 0;
                        //imgBoxWorkSpace.SelectionRegion = _dictRect[index].rectangle;
                        //tree.Delete(_dictRect[index].getRTreeRectangle(), _dictRect[index].id);
                        //_dictRect.RemoveAt(index);
                        //imgBoxWorkSpace.Invalidate();
                        //imgBoxWorkSpace.IsPointInImage
                        //_listRect.RemoveAt(0);
                    }
                    catch (Exception er)
                    {
                    }



                } //左键
                else if (e.Button == MouseButtons.Right)
                {

                } //右键
                else { } //滚轮？
            }
        }


        private void ImgBoxWorkSpace_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                if (otheraiDetectRect != null&&otheraiDetectRect.id != 0) {
                    
                    if (_dictRect.ContainsKey(otheraiDetectRect.id))
                    {
                        if (otheraiDetectRect.rectangle != imgBoxWorkSpace.SelectionRegion) {
                            tree.Delete(otheraiDetectRect.getRTreeRectangle(), otheraiDetectRect.id);
                            _dictRect.Remove(otheraiDetectRect.id);
                            otheraiDetectRect.rectangle = imgBoxWorkSpace.SelectionRegion;
                            _dictRect.Add(otheraiDetectRect.id, otheraiDetectRect);
                            tree.Add(otheraiDetectRect.getRTreeRectangle(), otheraiDetectRect.id);
                            useroperatoe.Outstartx = (int)otheraiDetectRect.rectangle.X;
                            useroperatoe.Outstarty = (int)otheraiDetectRect.rectangle.Y;
                            useroperatoe.Outwidth = (int)otheraiDetectRect.rectangle.Width;
                            useroperatoe.Outheight = (int)otheraiDetectRect.rectangle.Height;
                            RtDg.CurrentRow.Cells[3].Value = useroperatoe;
                        }
                    }
                    else {
                        if (otheraiDetectRect.rectangle != imgBoxWorkSpace.SelectionRegion)
                        {
                            otheraiDetectRect.rectangle = imgBoxWorkSpace.SelectionRegion;

                        }
                        _dictRect.Add(otheraiDetectRect.id, otheraiDetectRect);
                        tree.Add(otheraiDetectRect.getRTreeRectangle(), otheraiDetectRect.id);
                        useroperatoe.Outstartx = (int)otheraiDetectRect.rectangle.X;
                        useroperatoe.Outstarty = (int)otheraiDetectRect.rectangle.Y;
                        useroperatoe.Outwidth = (int)otheraiDetectRect.rectangle.Width;
                        useroperatoe.Outheight = (int)otheraiDetectRect.rectangle.Height;
                        RtDg.CurrentRow.Cells[3].Value = useroperatoe;
                    }
                    //otheraiDetectRect = new AiDetectRect();                   
                    //Console.WriteLine("输出结果");
                    //imgBoxWorkSpace.Invalidate();
                    //otheraiDetectRect = null;
                    //workingAiRegion = null;
                }

            } //左键
            else if (e.Button == MouseButtons.Right) {
            } //右键
            else { } //滚轮？
        }

        private void ImgBoxWorkSpace_MouseMove(object sender,MouseEventArgs e) {

        
        
        }
        /// <summary>
        /// 保存制作的信息
        /// </summary>
        private void save()
        {
            if (!imgBoxWorkSpace.IsSelecting)
            {
                if (isAi)
                {
                    try
                    {
                        tree.Delete(workingAiRegion.getRTreeRectangle(), 0);
                    }
                    catch (Exception er)
                    {

                    }
                    tree.Add(workingAiRegion.getRTreeRectangle(), 0);
                }
            }
        }

        /// <summary>
        /// 第一次画框完成后的函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImgBoxWorkSpace_Selected(object sender, EventArgs e)
        {
            //if (otheraiDetectRect != null) {
            //    otheraiDetectRect = new AiDetectRect();
            //}
            //workingAiRegion = new AiDetectRect();
            //workingAiRegion.id = snowflake.nextId();
            //workingAiRegion.rectangle = imgBoxWorkSpace.SelectionRegion;
            //otheraiDetectRect = workingAiRegion;
            
            ////this.Text += "1";
            //if (otheraiDetectRect != null)
            //{
            //    otheraiDetectRect = new AiDetectRect();
            //}
            //_dictRect.Add(workingAiRegion.id, workingAiRegion);
            //tree.Add(workingAiRegion.getRTreeRectangle(), workingAiRegion.id);
            //imgBoxWorkSpace.Invalidate();
        }

        #region 左边菜单栏

        private void AddAlgorithmBox_Click(object sender, EventArgs e)
        {
            Addopera = new OperatorSelect();
            AlgorithmSelect algorithmSelect = new AlgorithmSelect();
            algorithmSelect.ShowDialog();
            if (algorithmSelect.DialogResult == DialogResult.OK)
            {
                Algorithmtype algorithmtype = algorithmSelect.Tag as Algorithmtype;
                //userControl1.MyEvent += usercontroler1_Myevent;
                //panel7.Controls.Add(userControl1);
                foreach (Control control in Userpanel.Controls)
                {
                    if (control is OperatorSelection)
                    {
                        Userpanel.Controls.Remove(control);
                    }

                }
                int i = RtDg.Rows.Count;
                RtDg.Rows.Add();
                RtDg["Column1", i].Value = algorithmtype.Typename + addpicturebox;
                RtDg["Column2", i].Value = (addpicturebox + 1).ToString();
                RtDg["Column3", i].Value = algorithmtype.Owmername;



                Addopera.Algorithm = algorithmtype.Typename;
                OperatorSelection operatorSelection = new OperatorSelection(Addopera);
                operatorSelection.MyEvent += updatedatagrade;
                Addopera = operatorSelection.Tag as OperatorSelect;
                RtDg["Column4", i].Value = Addopera;
                Userpanel.Controls.Add(operatorSelection);
                RtDg.CurrentCell = RtDg.Rows[this.RtDg.Rows.Count - 1].Cells[0];
                Size size = imgBoxWorkSpace.ScrollSize;
                workingAiRegion = new AiDetectRect();
                workingAiRegion.id = snowflake.nextId();
                workingAiRegion.algorithmsName = algorithmtype.Typename+addpicturebox;
                workingAiRegion.operatorName =Addopera.Operatorname;
                workingAiRegion.rectangle = new Rectangle(size.Width+540,size.Height+800,200,200);
                Addopera.Outstartx = (int)workingAiRegion.rectangle.X;
                Addopera.Outstarty = (int)workingAiRegion.rectangle.Y;
                Addopera.Outwidth = (int)workingAiRegion.rectangle.Width;
                Addopera.Outheight = (int)workingAiRegion.rectangle.Height;
                _dictRect.Add(workingAiRegion.id, workingAiRegion);
                tree.Add(workingAiRegion.getRTreeRectangle(), workingAiRegion.id);
                imgBoxWorkSpace.SelectionRegion = workingAiRegion.rectangle;
                otheraiDetectRect = workingAiRegion;
                workingAiRegion = null;
                addpicturebox++;
                //RtDg.CurrentCell = RtDg[0, i];
            }



        }
        private void Save_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    oldlastheight = PbMain.Height;
            //    oldlastwidth = PbMain.Width;
            //    PbMain.Height = PbMain.Image.Height;
            //    PbMain.Width = PbMain.Image.Width;
            //    Bitmap bigbitmap = (Bitmap)PbMain.Image.Clone();
            //    for (int i = 0; i < RtDg.Rows.Count; i++)
            //    {
            //        string operatonameall = RtDg.Rows[i].Cells[0].Value.ToString();
            //        string outpicturename = RtDg.Rows[i].Cells[1].Value.ToString();
            //        string parent = RtDg.Rows[i].Cells[2].Value.ToString();
            //        Operatorselect insertoperatorselect = (Operatorselect)RtDg.Rows[i].Cells[3].Value;
            //        int outstartx = 0;
            //        int outstarty = 0;
            //        int outheight = 0;
            //        int outwidth = 0;
            //        string inpicturename = "";
            //        int instartx = 0;
            //        int instarty = 0;
            //        int inheight = 0;
            //        int inwidth = 0;
            //        foreach (Control control in PbMain.Controls)
            //        {

            //            if (control.Name == outpicturename)
            //            {
            //                outstartx = control.Location.X;
            //                outstarty = control.Location.Y;
            //                outheight = control.Height;
            //                outwidth = control.Width;
            //                Rectangle rectangle = new Rectangle(outstartx, outstarty, outheight, outwidth);
            //                foreach (Control control1 in control.Controls)
            //                {
            //                    if (control1 is PictureBox)
            //                    {
            //                        inpicturename = control1.Name;
            //                        instartx = control1.Location.X;
            //                        instarty = control1.Location.Y;
            //                        inheight = control1.Height;
            //                        inwidth = control1.Width;
            //                    }
            //                }

            //                Bitmap bitmap = ImageManger.CropImage(bigbitmap, rectangle);
            //                bitmap.Save(Settings.Default.path + "template\\" + operatonameall + ".jpg", ImageFormat.Jpeg);
            //                bitmap.Dispose();

            //            }
            //        }
            //        string selectsql = string.Format("select* from operato where operatonameall = '{0}'", operatonameall);
            //        DataTable selectnum = SQLiteHelper.GetDataTable(selectsql);
            //        if (selectnum.Rows.Count == 0)
            //        {
            //            string insertsql = string.Format("INSERT INTO operato (operatonameall,outpicturename,outstartx,outstarty,outheight,outwidth,intpicturename,instartx,instarty,inheight,inwidth,parent,algorithm,operatorname,confidence,percentageup,percentagedown,codetype,luminanceon,luminancedown,rednumon,rednumdown,greennumon,greennumdown,bluenumon,bluenumdown,picname,frontorside )VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}' ,'{26}','{27}')", operatonameall, outpicturename, outstartx, outstarty, outheight, outwidth, inpicturename, instartx, instarty, inheight, inwidth, parent, insertoperatorselect.Algorithm, insertoperatorselect.Operatorname, insertoperatorselect.Confidence, insertoperatorselect.Percentageup, insertoperatorselect.Rednumdown, insertoperatorselect.Codetype, insertoperatorselect.Luminanceon, insertoperatorselect.Luminancedown, insertoperatorselect.Rednumon, insertoperatorselect.Rednumdown, insertoperatorselect.Greennumon, insertoperatorselect.Greennumdown, insertoperatorselect.Bluenumon, insertoperatorselect.Bluenumdown, operatonameall + ".jpg", Settings.Default.frontorside);
            //            int num = SQLiteHelper.ExecuteSql(insertsql);

            //        }
            //        else
            //        {
            //            string updatesql = string.Format("update operato set outpicturename='{0}',outstartx='{1}',outstarty='{2}',outheight='{3}',outwidth='{4}',intpicturename='{5}',instartx='{6}',instarty='{7}',inheight='{8}',inwidth='{9}',parent='{10}',algorithm='{11}',operatorname='{12}',confidence='{13}',percentageup='{14}',percentagedown='{15}',codetype='{16}',luminanceon='{17}',luminancedown='{18}',rednumon='{19}',rednumdown='{20}',greennumon='{21}',greennumdown='{22}',bluenumon='{23}',bluenumdown='{24}',frontorside='{25}' where operatonameall = '{26}'", outpicturename, outstartx, outstarty, outheight, outwidth, inpicturename, instartx, instarty, inheight, inwidth, parent, insertoperatorselect.Algorithm, insertoperatorselect.Operatorname, insertoperatorselect.Confidence, insertoperatorselect.Percentageup, insertoperatorselect.Rednumdown, insertoperatorselect.Codetype, insertoperatorselect.Luminanceon, insertoperatorselect.Luminancedown, insertoperatorselect.Rednumon, insertoperatorselect.Rednumdown, insertoperatorselect.Greennumon, insertoperatorselect.Greennumdown, insertoperatorselect.Bluenumon, insertoperatorselect.Bluenumdown, Settings.Default.frontorside, operatonameall);
            //            SQLiteHelper.ExecuteSql(updatesql);
            //        }
            //    }
            //    PbMain.Height = oldlastheight;
            //    PbMain.Width = oldlastwidth;
            //    bigbitmap.Dispose();
            //    GC.Collect();

            //}
            //catch (Exception ex)
            //{
            //    Loghelper.WriteLog("算子编辑界面---保存错误",ex);
            
            
            //}

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

       
        private void RtDg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Addopera = new OperatorSelect();
                if (e.ColumnIndex != -1)
                {
                    DataGridViewColumn column = RtDg.Columns[e.ColumnIndex];
                    string picboxname = RtDg.CurrentRow.Cells[1].Value.ToString();
                    OperatorSelect operatorselect = (OperatorSelect)RtDg.CurrentRow.Cells[3].Value;
                    //imgBoxWorkSpace.SelectionRegion = new Rectangle(operatorselect.Outstartx,operatorselect.Outstarty,operatorselect.Outwidth,operatorselect.Outheight);
                    var objects = tree.Nearest(new RTree.Point(operatorselect.Outstartx, operatorselect.Outstarty, 0), 0);
                    if (objects.Count == 0)
                    {
                        workingAiRegion = null;
                        //otheraiDetectRect = new AiDetectRect();
                        //if (otheraiDetectRect != null)
                        //{
                        //    otheraiDetectRect = new AiDetectRect();
                        //}
                        return;
                    }
                    //这里获取不到的原因是鼠标的x,y和图像的坐标位置不一样
                    try
                    {
                        long key = objects[0];
                        workingAiRegion = _dictRect[key];
                        imgBoxWorkSpace.SelectionRegion = workingAiRegion.rectangle;
                        otheraiDetectRect = new AiDetectRect();
                        otheraiDetectRect = workingAiRegion;
                        tree.Delete(workingAiRegion.getRTreeRectangle(), key);
                        _dictRect.Remove(key);
                        workingAiRegion = null;
                        //int index = 0;
                        //imgBoxWorkSpace.SelectionRegion = _dictRect[index].rectangle;
                        //tree.Delete(_dictRect[index].getRTreeRectangle(), _dictRect[index].id);
                        //_dictRect.RemoveAt(index);
                        //imgBoxWorkSpace.Invalidate();
                        //imgBoxWorkSpace.IsPointInImage
                        //_listRect.RemoveAt(0);
                    }
                    catch (Exception er)
                    {
                        Loghelper.WriteLog("获取框问题",er);
                    }

                    foreach (Control control1 in Userpanel.Controls)
                    {
                        if (control1 is OperatorSelection)
                        {
                            Userpanel.Controls.Remove(control1);

                        }

                    }
                    useroperatoe = new OperatorSelect();
                    useroperatoe = operatorselect;
                    OperatorSelection userControl1 = new OperatorSelection(operatorselect);
                    userControl1.MyEvent += updatedatagrade;
                    Addopera = userControl1.Tag as OperatorSelect;
                    Userpanel.Controls.Add(userControl1);

                }

            }
            catch (Exception ex)
            {
                Loghelper.WriteLog("Platmake界面---选择框错误", ex);


            }

        }

        private void RtDg_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {

        }


        void updatedatagrade(object sender, EventArgs e)
        {
            OperatorSelect operatorselect = (OperatorSelect)sender;
            useroperatoe = operatorselect;
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
            else {
                addpicturebox = 0;
            
            
            }


        }

    }
}

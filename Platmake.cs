using pcbaoi.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pcbaoi
{
    public partial class Platmake : Form
    {
        Point centerpoint;
        private PickBox pb = new PickBox();
        int addpicturebox = 0;
        Control thiscontrol;
        bool isSelected = false;
        Point mouseDownPoint;
        AutoSizeFormClass asc = new AutoSizeFormClass();
        Operatorselect operatorselect1;
        Operatorselect useroperatoe = new Operatorselect();
        int oldlastwidth = 0;
        int oldlastheight = 0;

        public Platmake(Image image)
        {
            InitializeComponent();
            pictureBox1.Image = image;
            pictureBox1.MouseWheel += pictureBox1_MouseWheel;
            asc.RenewControlRect(pictureBox1);
            addnum();
            //picturestart();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            operatorselect1 = new Operatorselect();
            AlgorithmSelect algorithmSelect = new AlgorithmSelect();
            algorithmSelect.ShowDialog();
            if (algorithmSelect.DialogResult == DialogResult.OK) {
                Algorithmtype algorithmtype = algorithmSelect.Tag as Algorithmtype;
                //userControl1.MyEvent += usercontroler1_Myevent;
                //panel7.Controls.Add(userControl1);
                foreach (Control control in panel7.Controls) {
                    if (control is UserControl1) {
                        panel7.Controls.Remove(control);                                   
                    }
                               
                }
                int i = dataGridView1.Rows.Count;
                dataGridView1.Rows.Add();
                dataGridView1["Column1", i].Value = algorithmtype.Typename+addpicturebox;
                dataGridView1["Column2", i].Value = (addpicturebox + 1).ToString();
                dataGridView1["Column3", i].Value = algorithmtype.Owmername;

                PictureBox pictureBox = new PictureBox();
                pictureBox.Name = (addpicturebox + 1).ToString();
                pictureBox.Location = new Point(200, 200);
                pictureBox.Width = 70;
                pictureBox.Height = 70;
                pictureBox.BorderStyle = BorderStyle.FixedSingle;
                //pictureBox.DoubleClick += new EventHandler(pictureboxshow);
                pictureBox.SizeChanged += new EventHandler(pictureboxsizechange);
                pictureBox.Move += new EventHandler(pictureboxmove);
                pictureBox.BackColor = Color.Transparent;
                pictureBox.Parent = pictureBox1;
                //pictureBox1.Hide();
                this.pictureBox1.Controls.Add(pictureBox);
                //MessageBox.Show(pictureBox.Parent.Name);
                foreach (Control c in pictureBox1.Controls)
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
                if (algorithmtype.Typename == "BAD MARK" || algorithmtype.Typename == "MARK") {
                    PictureBox littlepicturebox = new PictureBox();
                    littlepicturebox.Name = pictureBox.Name+"littlebox"+addpicturebox;
                    littlepicturebox.Location = new Point(15, 15);
                    littlepicturebox.Width = 40;
                    littlepicturebox.Height = 40;
                    littlepicturebox.BorderStyle = BorderStyle.FixedSingle;
                    //pictureBox.DoubleClick += new EventHandler(pictureboxshow);
                    pictureBox.SizeChanged += new EventHandler(pictureboxsizechange);
                    pictureBox.Move += new EventHandler(pictureboxmove);
                    littlepicturebox.BackColor = Color.Transparent;
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
                operatorselect1.Algorithm = algorithmtype.Typename;
                UserControl1 userControl1 = new UserControl1(operatorselect1);
                userControl1.MyEvent += updatedatagrade;
                operatorselect1 = userControl1.Tag as Operatorselect;
                dataGridView1["Column4", i].Value = operatorselect1;
                panel7.Controls.Add(userControl1);
                dataGridView1.CurrentCell = dataGridView1[0, i];
                


            }
            asc = new AutoSizeFormClass();
            asc.RenewControlRect(pictureBox1);



        }



        private void Platmake_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        private void picturestart()
        {
            using (Graphics gc = pictureBox1.CreateGraphics())
            using (Pen pen = new Pen(Color.Green))
            {
                //设置画笔的宽度
                pen.Width = 1;
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
                RectangleF rect = new RectangleF();
                rect.Location = pictureBox1.Location;
                rect.Size = pictureBox1.Size;
                //确保在画图区域
                if (rect.Contains(pictureBox1.Location))
                {
                    pictureBox1.Refresh();
                    //画竖线
                    gc.DrawLine(pen, (pictureBox1.Width) / 2, 0, (pictureBox1.Width) / 2, rect.Bottom);
                    //画横线
                    gc.DrawLine(pen, 0, (pictureBox1.Height) / 2, rect.Right, (pictureBox1.Height) / 2);


                }
            }
            //showponit((pictureBox1.Width) / 2, (pictureBox1.Height) / 2);
            centerpoint = new Point((pictureBox1.Width) / 2, (pictureBox1.Height) / 2);

        }

        private void Platmake_Shown(object sender, EventArgs e)
        {
            //picturestart();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {

        }
        private void dataGridView1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                operatorselect1 = new Operatorselect();
                if (e.ColumnIndex != -1)
                {
                    DataGridViewColumn column = dataGridView1.Columns[e.ColumnIndex];
                    string picboxname = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    foreach (Control control in pictureBox1.Controls)
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
                    foreach (Control control1 in panel7.Controls) {
                        if (control1 is UserControl1) {
                            panel7.Controls.Remove(control1);
                        
                        }
                                       
                    }
                    UserControl1 userControl1 = new UserControl1(dataGridView1.CurrentRow.Cells[3].Value);
                    userControl1.MyEvent += updatedatagrade;
                    operatorselect1 = userControl1.Tag as Operatorselect;
                    panel7.Controls.Add(userControl1);

                }

            }
            catch(Exception ex) { 
            
            
            }


        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //自动编号，与数据无关
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
               e.RowBounds.Location.Y,
               dataGridView1.RowHeadersWidth - 4,
               e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics,
                  (e.RowIndex + 1).ToString(),
                   dataGridView1.RowHeadersDefaultCellStyle.Font,
                   rectangle,
                   dataGridView1.RowHeadersDefaultCellStyle.ForeColor,
                   TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }





        private void button14_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1(2);
            form1.Show();
            this.Hide();
            
        }

        private bool IsMouseInPanel()
        {
            if (this.pictureBox1.Left < PointToClient(Cursor.Position).X
            && PointToClient(Cursor.Position).X < this.pictureBox1.Left + this.pictureBox1.Width
            && this.pictureBox1.Top < PointToClient(Cursor.Position).Y
            && PointToClient(Cursor.Position).Y < this.pictureBox1.Top + this.pictureBox1.Height)
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
            pictureBox1.Width = pictureBox1.Width + i;//增加picturebox的宽度
            pictureBox1.Height = pictureBox1.Height + i;
            // Console.WriteLine(pictureBox1.Width.ToString() + pictureBox1.Height.ToString());
            pictureBox1.Left = pictureBox1.Left - i / 2;//使picturebox的中心位于窗体的中心
            pictureBox1.Top = pictureBox1.Top - i / 2;//进而缩放时图片也位于窗体的中心




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

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isSelected && IsMouseInPanel())
            {
                this.pictureBox1.Left = this.pictureBox1.Left + (Cursor.Position.X - mouseDownPoint.X);
                this.pictureBox1.Top = this.pictureBox1.Top + (Cursor.Position.Y - mouseDownPoint.Y);
                mouseDownPoint.X = Cursor.Position.X;
                mouseDownPoint.Y = Cursor.Position.Y;
                //drawlineact();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isSelected = false;
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            asc.ControlAutoSize(pictureBox1);
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
            asc.RenewControlRect(pictureBox1);
            
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
            asc.RenewControlRect(pictureBox1);
            
        }
        void updatedatagrade(object sender,EventArgs e) {
            Operatorselect operatorselect = (Operatorselect)sender;
            //operatorselect1 = operatorselect;
            dataGridView1.CurrentRow.Cells[3].Value = operatorselect;


        }

        private void button16_Click(object sender, EventArgs e)
        {
            oldlastheight = pictureBox1.Height;
            oldlastwidth = pictureBox1.Width;
            pictureBox1.Height = pictureBox1.Image.Height;
            pictureBox1.Width = pictureBox1.Image.Width;
            for (int i = 0; i < dataGridView1.Rows.Count; i++) {
                string operatonameall = dataGridView1.Rows[i].Cells[0].Value.ToString();
                string outpicturename = dataGridView1.Rows[i].Cells[1].Value.ToString();
                string parent = dataGridView1.Rows[i].Cells[2].Value.ToString();
                Operatorselect insertoperatorselect = (Operatorselect)dataGridView1.Rows[i].Cells[3].Value;
                int outstartx=0;
                int outstarty=0;
                int outheight=0;
                int outwidth=0;
                string inpicturename ="";
                int instartx =0;
                int instarty =0;
                int inheight =0;
                int inwidth =0;
                foreach (Control control in pictureBox1.Controls) {

                    if (control.Name == outpicturename) {
                        outstartx = control.Location.X;
                        outstarty = control.Location.Y;
                        outheight = control.Height;
                        outwidth = control.Width;
                        foreach (Control control1 in control.Controls) {
                            if (control1 is PictureBox) {
                                inpicturename = control1.Name;
                                instartx = control1.Location.X;
                                instarty = control1.Location.Y;
                                inheight = control1.Height;
                                inwidth = control1.Width;
                            }                                                                     
                        }

                    }                               
                }
                string selectsql = string.Format("select* from operato where operatonameall = '{0}'", operatonameall);
                DataTable selectnum = SQLiteHelper.GetDataTable(selectsql);
                if (selectnum.Rows.Count == 0)
                {
                    string insertsql = string.Format("INSERT INTO operato (operatonameall,outpicturename,outstartx,outstarty,outheight,outwidth,intpicturename,instartx,instarty,inheight,inwidth,parent,algorithm,operatorname,confidence,percentageup,percentagedown,codetype,luminanceon,luminancedown,rednumon,rednumdown,greennumon,greennumdown,bluenumon,bluenumdown,frontorside )VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}' ,'{26}')", operatonameall, outpicturename, outstartx, outstarty, outheight, outwidth, inpicturename, instartx, instarty, inheight, inwidth, parent, insertoperatorselect.Algorithm, insertoperatorselect.Operatorname, insertoperatorselect.Confidence, insertoperatorselect.Percentageup, insertoperatorselect.Rednumdown, insertoperatorselect.Codetype, insertoperatorselect.Luminanceon, insertoperatorselect.Luminancedown, insertoperatorselect.Rednumon, insertoperatorselect.Rednumdown, insertoperatorselect.Greennumon, insertoperatorselect.Greennumdown, insertoperatorselect.Bluenumon, insertoperatorselect.Bluenumdown, Settings.Default.frontorside);
                    int num = SQLiteHelper.ExecuteSql(insertsql);

                }
                else {
                    string updatesql = string.Format("update operato set outpicturename='{0}',outstartx='{1}',outstarty='{2}',outheight='{3}',outwidth='{4}',intpicturename='{5}',instartx='{6}',instarty='{7}',inheight='{8}',inwidth='{9}',parent='{10}',algorithm='{11}',operatorname='{12}',confidence='{13}',percentageup='{14}',percentagedown='{15}',codetype='{16}',luminanceon='{17}',luminancedown='{18}',rednumon='{19}',rednumdown='{20}',greennumon='{21}',greennumdown='{22}',bluenumon='{23}',bluenumdown='{24}',frontorside='{25}' where operatonameall = '{26}'", outpicturename, outstartx, outstarty, outheight, outwidth, inpicturename, instartx, instarty, inheight, inwidth, parent, insertoperatorselect.Algorithm, insertoperatorselect.Operatorname, insertoperatorselect.Confidence, insertoperatorselect.Percentageup, insertoperatorselect.Rednumdown, insertoperatorselect.Codetype, insertoperatorselect.Luminanceon, insertoperatorselect.Luminancedown, insertoperatorselect.Rednumon, insertoperatorselect.Rednumdown, insertoperatorselect.Greennumon, insertoperatorselect.Greennumdown, insertoperatorselect.Bluenumon, insertoperatorselect.Bluenumdown, Settings.Default.frontorside, operatonameall);
                    SQLiteHelper.ExecuteSql(updatesql);
                }                
            }
            pictureBox1.Height = oldlastheight;
            pictureBox1.Width = oldlastwidth;
        }
        private void addnum() {
            string selectsql = "select id from operato order by id desc";
            DataTable dataTable = SQLiteHelper.GetDataTable(selectsql);
            if (dataTable.Rows.Count > 0) {
                addpicturebox = Convert.ToInt32(dataTable.Rows[0]["id"].ToString());
           
            }


        }
    }
}

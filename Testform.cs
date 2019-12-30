using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pcbaoi
{
    public partial class Testform : Form
    {

        string frontandside;
        AutoSizeFormClass asc = new AutoSizeFormClass();
        int oldlastwidth = 0;
        int oldlastheight = 0;
        Image bigimage;
        public Testform(Image image)
        {
            InitializeComponent();
            bigimage = image;

            



        }

        private void panel6_Click(object sender, EventArgs e)
        {
            frontandside = "front";
            pictureBox1.Image = bigimage;
            addpic();



        }

        private void panel7_Click(object sender, EventArgs e)
        {
            frontandside = "side";
            pictureBox1.Image = bigimage;
            foreach (Control control in pictureBox1.Controls)
            {
                if (control is PictureBox)
                {
                    pictureBox1.Controls.Remove(control);
                }
            }
            addpic();

        }
        public void addpic() {
            oldlastheight = pictureBox1.Height;
            oldlastwidth = pictureBox1.Width;
            pictureBox1.Height = pictureBox1.Image.Height;
            pictureBox1.Width = pictureBox1.Image.Width;
            string selectall = string.Format("select * from operato where frontorside = '{0}'", frontandside);
            DataTable dataTable = SQLiteHelper.GetDataTable(selectall);
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.Name = dataTable.Rows[i]["outpicturename"].ToString();
                pictureBox.Location = new Point(Convert.ToInt32(dataTable.Rows[i]["outstartx"].ToString()), Convert.ToInt32(dataTable.Rows[i]["outstarty"].ToString()));
                pictureBox.Width = Convert.ToInt32(dataTable.Rows[i]["outwidth"].ToString());
                pictureBox.Height = Convert.ToInt32(dataTable.Rows[i]["outheight"].ToString()); ;
                pictureBox.BorderStyle = BorderStyle.FixedSingle;
                pictureBox.BackColor = Color.Transparent;
                pictureBox.Parent = pictureBox1;
                pictureBox.Click += click;
                this.pictureBox1.Controls.Add(pictureBox);
                int inum = dataGridView1.Rows.Count;
                dataGridView1.Rows.Add();
                dataGridView1["Column1", inum].Value = inum;
                dataGridView1["Column2", inum].Value = dataTable.Rows[i]["operatonameall"].ToString();
                dataGridView1["Column3", inum].Value = "其他";
                dataGridView1["Column4", inum].Value = "未判定";
                dataGridView1["Column5", inum].Value = dataTable.Rows[i]["outpicturename"].ToString();
                dataGridView1["Column6", inum].Value = dataTable.Rows[i]["outstartx"].ToString();
                dataGridView1["Column7", inum].Value = dataTable.Rows[i]["outstarty"].ToString();
                dataGridView1["Column8", inum].Value = dataTable.Rows[i]["outwidth"].ToString();
                dataGridView1["Column9", inum].Value = dataTable.Rows[i]["outheight"].ToString();

                //MessageBox.Show(pictureBox.Parent.Name);
                foreach (Control c in this.pictureBox1.Controls)
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
            asc.RenewControlRect(pictureBox1);
            pictureBox1.Height = oldlastheight;
            pictureBox1.Width = oldlastwidth;

        }
        private void click(object sender,EventArgs e) {
            Rectangle rectangle = new Rectangle();
            PictureBox pp = (PictureBox)sender;
            
            foreach (DataGridViewRow each in dataGridView1.Rows) {
                if (each.Cells[4].Value.ToString() == pp.Name) {
                    each.DefaultCellStyle.BackColor = Color.Gray;
                    dataGridView1.FirstDisplayedScrollingRowIndex =Convert.ToInt32(each.Cells[4].Value.ToString())-1;
                    rectangle.Location = new Point(Convert.ToInt32(each.Cells[5].Value.ToString())-40, Convert.ToInt32(each.Cells[6].Value.ToString())-40);
                    rectangle.Width = Convert.ToInt32(each.Cells[7].Value.ToString())+80;
                    rectangle.Height = Convert.ToInt32(each.Cells[8].Value.ToString())+80;
                }
            
            }


            Image Image = AcquireRectangleImage(pictureBox1.Image,rectangle);
            Bitmap bitmap = DrawRectangleInPicture((Bitmap)Image,new Point(40,40),new Point(rectangle.Width-40, rectangle.Height - 40));
            pictureBox2.Image = bitmap;

        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            asc.ControlAutoSize(pictureBox1);

        }

        private void Testform_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Testform_Shown(object sender, EventArgs e)
        {
            asc.RenewControlRect(pictureBox1);
            panel6_Click(null, null);



        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        public static Image AcquireRectangleImage(Image source, Rectangle rect)
        {
            if (source == null || rect.IsEmpty) return null;
            Bitmap bmSmall = new Bitmap(rect.Width, rect.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            //Bitmap bmSmall = new Bitmap(rect.Width, rect.Height, source.PixelFormat);
            using (Graphics grSmall = Graphics.FromImage(bmSmall))
            {

                grSmall.DrawImage(source, new System.Drawing.Rectangle(0, 0, bmSmall.Width, bmSmall.Height), rect, GraphicsUnit.Pixel);
                grSmall.Dispose();
            }
            return bmSmall;
        }
        public static Bitmap DrawRectangleInPicture(Bitmap bmp, Point p0, Point p1)
        {
            if (bmp == null) return null;


            Graphics g = Graphics.FromImage(bmp);

            Brush brush = new SolidBrush(Color.Red);
            Pen pen = new Pen(brush, 2);
            pen.DashStyle = DashStyle.Solid;

            g.DrawRectangle(pen, new Rectangle(p0.X, p0.Y, Math.Abs(p0.X - p1.X), Math.Abs(p0.Y - p1.Y)));

            g.Dispose();

            return bmp;
        }

    }
}

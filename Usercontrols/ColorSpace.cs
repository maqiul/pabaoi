using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;

namespace pcbaoi
{
    public partial class ColorSpace : UserControl
    {
        PointF pointra;
        PointF pointrb;
        PointF pointga;
        PointF pointgb;
        PointF pointba;
        PointF pointbb;
        OperatorSelect operatorselect = new OperatorSelect();
        public ColorSpace(object op)
        {
            InitializeComponent();
            TrackBarRange range = new TrackBarRange(0, 255);

            operatorselect = (OperatorSelect)op;
            if (operatorselect.Rednumon > 0)
            {
                range = new TrackBarRange(operatorselect.Rednumdown, operatorselect.Rednumon);
                RrangTrackBar.Value = range;
                pointra = new PointF(pictureBox1.Width / 2, pictureBox1.Height - pictureBox1.Height * getnum(RrangTrackBar.Value.Minimum));
                pointrb = new PointF(pictureBox1.Width / 2, pictureBox1.Height - pictureBox1.Height * getnum(RrangTrackBar.Value.Maximum));
                range = new TrackBarRange(operatorselect.Greennumdown, operatorselect.Greennumon);
                GrangTrackBar.Value = range;
                pointga = new PointF(pictureBox1.Width - pictureBox1.Width * getnum(GrangTrackBar.Value.Minimum), pictureBox1.Height);
                pointgb = new PointF(pictureBox1.Width - pictureBox1.Width * getnum(GrangTrackBar.Value.Maximum), pictureBox1.Height);
                range = new TrackBarRange(operatorselect.Bluenumdown, operatorselect.Bluenumon);
                BrangTrackBar.Value = range;
                pointba = new PointF(pictureBox1.Width * getnum(BrangTrackBar.Value.Minimum), pictureBox1.Height);
                pointbb = new PointF(pictureBox1.Width * getnum(BrangTrackBar.Value.Maximum), pictureBox1.Height);
            }
            else {
                range = new TrackBarRange(0, 255);
                RrangTrackBar.Value = range;
                pointra = new PointF(pictureBox1.Width / 2, pictureBox1.Height - pictureBox1.Height * getnum(RrangTrackBar.Value.Minimum));
                pointrb = new PointF(pictureBox1.Width / 2, pictureBox1.Height - pictureBox1.Height * getnum(RrangTrackBar.Value.Maximum));
                GrangTrackBar.Value = range;
                pointga = new PointF(pictureBox1.Width - pictureBox1.Width * getnum(GrangTrackBar.Value.Minimum), pictureBox1.Height);
                pointgb = new PointF(pictureBox1.Width - pictureBox1.Width * getnum(GrangTrackBar.Value.Maximum), pictureBox1.Height);

                BrangTrackBar.Value = range;
                pointba = new PointF(pictureBox1.Width * getnum(BrangTrackBar.Value.Minimum), pictureBox1.Height);
                pointbb = new PointF(pictureBox1.Width * getnum(BrangTrackBar.Value.Maximum), pictureBox1.Height);

            }
            operatorselect.Rednumon = RrangTrackBar.Value.Maximum;
            operatorselect.Rednumdown = RrangTrackBar.Value.Minimum;
            operatorselect.Greennumon = GrangTrackBar.Value.Maximum;
            operatorselect.Greennumdown = GrangTrackBar.Value.Minimum;
            operatorselect.Bluenumon = BrangTrackBar.Value.Maximum;
            operatorselect.Bluenumdown = BrangTrackBar.Value.Minimum;
            this.Tag = operatorselect;
            drawpic();

        }
        //定义委托
        public delegate void MyEventHandle(object sender, EventArgs e);
        //定义事件
        public event MyEventHandle MyEvent;
        private void RrangTrackBar_ValueChanged(object sender, EventArgs e)
        {
            pointra = new PointF(pictureBox1.Width / 2, pictureBox1.Height - pictureBox1.Height * getnum(RrangTrackBar.Value.Minimum));
            pointrb = new PointF(pictureBox1.Width / 2, pictureBox1.Height - pictureBox1.Height * getnum(RrangTrackBar.Value.Maximum));
            drawpic();
            operatorselect.Rednumon = RrangTrackBar.Value.Maximum;
            operatorselect.Rednumdown = RrangTrackBar.Value.Minimum;
            if (operatorselect.Greennumon == 0) {
                operatorselect.Greennumon = GrangTrackBar.Value.Maximum;
                operatorselect.Greennumdown = GrangTrackBar.Value.Minimum;
            }
            if (operatorselect.Bluenumon == 0) {
                operatorselect.Bluenumon = BrangTrackBar.Value.Maximum;
                operatorselect.Bluenumdown = BrangTrackBar.Value.Minimum;

            }


            if (MyEvent != null) {
                MyEvent(operatorselect,e);
            
            }            


        }

        private void GrangTrackBar_ValueChanged(object sender, EventArgs e)
        {
            pointga = new PointF(pictureBox1.Width - pictureBox1.Width * getnum(GrangTrackBar.Value.Minimum), pictureBox1.Height);
            pointgb = new PointF(pictureBox1.Width - pictureBox1.Width * getnum(GrangTrackBar.Value.Maximum), pictureBox1.Height);

            drawpic();
            operatorselect.Rednumon = RrangTrackBar.Value.Maximum;
            operatorselect.Rednumdown = RrangTrackBar.Value.Minimum;
            operatorselect.Greennumon = GrangTrackBar.Value.Maximum;
            operatorselect.Greennumdown = GrangTrackBar.Value.Minimum;
            if (operatorselect.Bluenumon == 0)
            {
                operatorselect.Bluenumon = BrangTrackBar.Value.Maximum;
                operatorselect.Bluenumdown = BrangTrackBar.Value.Minimum;

            }
            if (MyEvent != null)
            {
                MyEvent(operatorselect, e);

            }
        }

        private void BrangTrackBar_ValueChanged(object sender, EventArgs e)
        {
            pointba = new PointF(pictureBox1.Width * getnum(BrangTrackBar.Value.Minimum), pictureBox1.Height);
            pointbb = new PointF(pictureBox1.Width * getnum(BrangTrackBar.Value.Maximum), pictureBox1.Height);
            drawpic();
            operatorselect.Rednumon = RrangTrackBar.Value.Maximum;
            operatorselect.Rednumdown = RrangTrackBar.Value.Minimum;
            operatorselect.Greennumon = GrangTrackBar.Value.Maximum;
            operatorselect.Greennumdown = GrangTrackBar.Value.Minimum;
            operatorselect.Bluenumon = BrangTrackBar.Value.Maximum;
            operatorselect.Bluenumdown = BrangTrackBar.Value.Minimum;
            if (MyEvent != null)
            {
                MyEvent(operatorselect, e);

            }

        }


        public void drawpic() {
            using (Graphics gc = pictureBox1.CreateGraphics()) {
                Pen pen1 = new Pen(Color.Red);
                //设置画笔的宽度
                pen1.Width = 2;
                pen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                Pen pen2 = new Pen(Color.Green);
                //设置画笔的宽度
                pen2.Width = 2;
                pen2.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                Pen pen3 = new Pen(Color.Blue);
                //设置画笔的宽度
                pen3.Width = 2;
                pen3.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                RectangleF rect = new RectangleF();
                rect.Location = pictureBox1.Location;
                rect.Size = pictureBox1.Size;
                //确保在画图区域
                if (rect.Contains(pictureBox1.Location))
                {
                    pictureBox1.Refresh();
                    //画竖线
                    //gc.DrawLine(pen1, pointra.X, 0, (pictureBox1.Width) / 2, rect.Bottom);
                    //画横线
                    gc.DrawLine(pen1,(float)(pointra.X-pointra.Y/Math.Sqrt(3.0)), pointra.Y, (float)(pointra.X + pointra.Y / Math.Sqrt(3.0)), pointra.Y);
                    gc.DrawLine(pen1, (float)(pointrb.X - pointrb.Y / Math.Sqrt(3.0)), pointrb.Y, (float)(pointrb.X + pointrb.Y / Math.Sqrt(3.0)), pointrb.Y);
                    if (pointga.X == 0)
                    {
                        gc.DrawLine(pen2, pointga.X / 2, pointga.Y, pointga.X, pointga.Y);
                    }
                    else {
                        gc.DrawLine(pen2, pointga.X / 2, getnum(GrangTrackBar.Value.Minimum) *pictureBox1.Height, pointga.X, pointga.Y);

                    }
                    

                    gc.DrawLine(pen2, pointgb.X / 2, getnum(GrangTrackBar.Value.Maximum) * pictureBox1.Height , pointgb.X, pointgb.Y);
                    if (pointba.X == 172)
                    {
                        gc.DrawLine(pen3, pointba.X, pointgb.Y, pointba.X, pointba.Y);

                    }
                    else {

                        gc.DrawLine(pen3,( pictureBox1.Width + pictureBox1.Width * getnum(BrangTrackBar.Value.Minimum))/2, getnum(BrangTrackBar.Value.Minimum) * pictureBox1.Height, pointba.X, pointba.Y);
                    }


                     gc.DrawLine(pen3, (pictureBox1.Width + pictureBox1.Width * getnum(BrangTrackBar.Value.Maximum))/2, getnum(BrangTrackBar.Value.Maximum) * pictureBox1.Height , pointbb.X, pointgb.Y);



                }

            }           
        }

        public float getnum( int value) {
            float d = (float)value / 255;
            return 1 - d * d;
                      
        }

        private void RrangTrackBar_MouseHover(object sender, EventArgs e)
        {
            RrangTrackBar.ToolTip = "最小值:" + RrangTrackBar.Value.Minimum + "最大值:" + RrangTrackBar.Value.Maximum;
        }

        private void GrangTrackBar_EditValueChanged(object sender, EventArgs e)
        {
            GrangTrackBar.ToolTip = "最小值:" + RrangTrackBar.Value.Minimum + "最大值:" + RrangTrackBar.Value.Maximum;
        }

        private void BrangTrackBar_EditValueChanged(object sender, EventArgs e)
        {
            BrangTrackBar.ToolTip = "最小值:" + RrangTrackBar.Value.Minimum + "最大值:" + RrangTrackBar.Value.Maximum;
        }

        private void UserControl6_Paint(object sender, PaintEventArgs e)
        {
            drawpic();
        }
    }
}

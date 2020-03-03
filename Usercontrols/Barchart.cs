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
    public partial class Barchart : UserControl
    {
        OperatorSelect operatorselect = new OperatorSelect();
        List<int> x = new List<int>() { 1, 2, 3, 4, 5 ,6,7,8,9,10,11,12,13,14,15,16,17};
        List<int> list1 = new List<int>() { 10, 20, 30, 40, 50,60,70,80,90,100,110,120,130,140,150,160,170 };
        public Barchart(object ob)
        {

            InitializeComponent();
            operatorselect = (OperatorSelect)ob;

            chart1.Series["new"].Points.DataBindXY(x, list1);
            if (operatorselect.Luminanceon > 0)
            {
                TrackBarRange range = new TrackBarRange(operatorselect.Luminancedown, operatorselect.Luminanceon);
                RangeTrackBar.Value = range;

            }
            operatorselect.Luminanceon = RangeTrackBar.Value.Maximum;
            operatorselect.Luminancedown = RangeTrackBar.Value.Minimum;
            this.Tag = operatorselect;
            
            draw();
        }
        //定义委托
        public delegate void MyEventHandle(object sender, EventArgs e);
        //定义事件
        public event MyEventHandle MyEvent;
        private void RangeTrackBar_ValueChanged(object sender, EventArgs e)
        {
            //rangeTrackBarControl1.Value.Minimum
            draw();
            operatorselect.Luminanceon = RangeTrackBar.Value.Maximum;
            operatorselect.Luminancedown = RangeTrackBar.Value.Minimum;
            if (MyEvent != null) {
                MyEvent(operatorselect,e);
            
            }



        }
        private void draw() {
            using (Graphics gc = chart1.CreateGraphics())
            {
                Pen pen1 = new Pen(Color.Red);

                //设置画笔的宽度
                pen1.Width = 2;
                pen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                Pen pen2 = new Pen(Color.Green);

                //设置画笔的宽度
                pen2.Width = 2;
                pen2.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                RectangleF rect = new RectangleF();
                rect.Location = chart1.Location;
                rect.Size = chart1.Size;
                //chart1.Series["new"].AxisLabel = "0";
                //确保在画图区域
                if (rect.Contains(chart1.Location))
                {
                    chart1.Refresh();
                    //画竖线
                    //gc.DrawLine(pen1, pointra.X, 0, (pictureBox1.Width) / 2, rect.Bottom);
                    //画横线
                    gc.DrawLine(pen1, RangeTrackBar.Value.Minimum * 8 + 35, 167, RangeTrackBar.Value.Minimum * 8 + 35, 0);
                    gc.DrawLine(pen2, RangeTrackBar.Value.Maximum * 8 + 35, 167, RangeTrackBar.Value.Maximum *8 + 35, 0);

                }

            }

        }

        private void Barchart_Paint(object sender, PaintEventArgs e)
        {
            //draw();
        }

        private void Barchart_Load(object sender, EventArgs e)
        {
            draw();
        }
    }
}

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
    public partial class Ruleresult : Form
    {
        public Ruleresult(Point startpoint,Point endpoint, float newx,float newy)
        {
            InitializeComponent();
            textBox1.Text = "X " + startpoint.X + " ; " + "Y " + startpoint.Y;
            textBox2.Text = "X " + endpoint.X + " ; " + "Y " + endpoint.Y;
            
            label4.Text = "长度 "+(Math.Abs(endpoint.Y - startpoint.Y)*newy).ToString();
            label5.Text = "宽度 "+ (Math.Abs(endpoint.X - startpoint.X)*newx).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

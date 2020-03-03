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
            Starttx.Text = "X " + startpoint.X + " ; " + "Y " + startpoint.Y;
            Endtx.Text = "X " + endpoint.X + " ; " + "Y " + endpoint.Y;
            
            Lenthlb.Text = "长度 "+(Math.Abs(endpoint.Y - startpoint.Y)*newy).ToString();
            Widthlb.Text = "宽度 "+ (Math.Abs(endpoint.X - startpoint.X)*newx).ToString();
        }

        private void Canelbt_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

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
        public Ruleresult(Point startpoint,Point endpoint)
        {
            InitializeComponent();
            textBox1.Text = "X " + startpoint.X + " ; " + "Y " + startpoint.Y;
            textBox2.Text = "X " + endpoint.X + " ; " + "Y " + endpoint.Y;
            label4.Text = "长度 "+(endpoint.Y - startpoint.Y).ToString();
            label5.Text = "宽度 "+ (endpoint.X - startpoint.X).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

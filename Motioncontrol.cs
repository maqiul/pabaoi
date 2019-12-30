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
    public partial class Motioncontrol : Form
    {
        public Motioncontrol()
        {
            InitializeComponent();
            lefttitlebt_Click(lefttitlebt, null);
        }

        private void XYspeed_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
            else
            {

            }
        }

        private void lefttitlebt_Click(object sender, EventArgs e)
        {
            righttitlebt.BackColor = Color.Gray;
           
            Panel panel = (Panel)sender;
            panel.BackColor = Color.White;
        }

        private void righttitlebt_Click(object sender, EventArgs e)
        {
            lefttitlebt.BackColor = Color.Gray;
            Panel panel = (Panel)sender;
            panel.BackColor = Color.White;

        }

        private void canelbt_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

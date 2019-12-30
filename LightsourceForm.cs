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
    public partial class LightsourceForm : Form
    {
        public LightsourceForm()
        {
            InitializeComponent();
            panel1_Click(lefttitlebt,null);
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            righttitlebt.BackColor = Color.Gray;
            blowbt.Hide();
            Panel panel = (Panel)sender;
            panel.BackColor = Color.White;

        }

        private void panel2_Click(object sender, EventArgs e)
        {
            lefttitlebt.BackColor = Color.Gray;
            blowbt.Show();
            Panel panel = (Panel)sender;
            panel.BackColor = Color.White;
        }

        private void WotrackBar_ValueChanged(object sender, EventArgs e)
        {

        }

        private void RtrackBar_ValueChanged(object sender, EventArgs e)
        {

        }

        private void GtrackBar_ValueChanged(object sender, EventArgs e)
        {

        }

        private void BtrackBar_ValueChanged(object sender, EventArgs e)
        {

        }

        private void WftrackBar_ValueChanged(object sender, EventArgs e)
        {

        }

        private void canelbt_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

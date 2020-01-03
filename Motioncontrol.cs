using QTing.PLC;
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
            PLCController.Instance.CloseConnection();
            this.Close();
        }

        private void Restbt_Click(object sender, EventArgs e)
        {


        }
        private void rest(){ 
        
        }

        private void conn()
        {

            if (PLCController.Instance.Connection(IniFile.iniRead("PLC", "ip"), Convert.ToInt32(IniFile.iniRead("PLC", "port"))))
                Console.WriteLine("连接成功");
            else
                MessageBox.Show("连接失败");

        }
    }
}

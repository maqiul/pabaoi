using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pcbaoi.usercontrol
{
    public partial class HistoryTab : UserControl
    {
        string path;
        public HistoryTab(string filepath, string datetime)
        {
            InitializeComponent();
            label1.Text = filepath;
            label2.Text = datetime;
            path = filepath;
        }

        public delegate void MyEventHandle(object sender, EventArgs e);
        //定义事件
        public event MyEventHandle MyEvent;
        private void label1_Click(object sender, EventArgs e)
        {
            if (MyEvent != null)
            {
                MyEvent(path, e);
            }

        }
    }
}

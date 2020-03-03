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
            Filepathlb.Text = filepath;
            Datetimelb.Text = datetime;
            path = filepath;
        }

        public delegate void MyEventHandle(object sender, EventArgs e);
        //定义事件
        public event MyEventHandle MyEvent;
        private void Filepathlb_Click(object sender, EventArgs e)
        {
            if (MyEvent != null)
            {
                MyEvent(path, e);
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Specialized;

namespace pcbaoi.usercontrol
{
    public partial class History : UserControl
    {
        private StringCollection fileList;
        private StringCollection filetimelist;
        Point point = new Point(0, 1);
        string path;
        public History()
        {
            InitializeComponent();
        }

        private void History_Load(object sender, EventArgs e)
        {
            fileList = Properties.Settings.Default.FilePaths;
            if (fileList == null)
            {
                fileList = new StringCollection();
            }
            filetimelist = Properties.Settings.Default.Filetimes;
            if (filetimelist == null)
            {
                filetimelist = new StringCollection();
            }
            for (int i = 0; i < fileList.Count - 1; i++) {
                HistoryTab historyTab = new HistoryTab(fileList[i], filetimelist[i]);
                historyTab.Location = point;
                historyTab.MyEvent += myEvent;
                panel1.Controls.Add(historyTab);
                point = new Point(point.X, point.Y + historyTab.Height);
            
            }

        }
        public void myEvent(object sender, EventArgs e) {

            if (MyEvent != null)
            {
                MyEvent(sender, e);
            }

        }

        public delegate void MyEventHandle(object sender, EventArgs e);
        //定义事件
        public event MyEventHandle MyEvent;

        private void button1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}

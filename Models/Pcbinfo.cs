using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pcbaoi
{
    public  class PcbInfo
    {
        [Description("检测")]
        byte[] bitmap;
        [Description("检测面")]
        object obj;
        [Description("检测返回矩形框")]
        Rectangle rectangle;

        public byte[] Bitmap { get => bitmap; set => bitmap = value; }
        public object Obj { get => obj; set => obj = value; }
        public Rectangle Rectangle { get => rectangle; set => rectangle = value; }
    }
}

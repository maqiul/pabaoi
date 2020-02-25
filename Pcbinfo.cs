using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pcbaoi
{
    public  class Pcbinfo
    {
        byte[] bitmap;
        object obj;
        Rectangle rectangle;

        public byte[] Bitmap { get => bitmap; set => bitmap = value; }
        public object Obj { get => obj; set => obj = value; }
        public Rectangle Rectangle { get => rectangle; set => rectangle = value; }
    }
}

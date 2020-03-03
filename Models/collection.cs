using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pcbaoi
{
    class Collection
    {
        [Description("拍摄类型 A面 B面 双面")]
        string type;
        [Description("载板长度")]
        string width;
        [Description("载板宽度")]
        string height;
        [Description("pcb长度")]
        string pcbwidth;
        [Description("pcb宽度")]
        string pcbheight;

        public string Type { get => type; set => type = value; }
        public string Width { get => width; set => width = value; }
        public string Height { get => height; set => height = value; }
        public string Pcbwidth { get => pcbwidth; set => pcbwidth = value; }
        public string Pcbheight { get => pcbheight; set => pcbheight = value; }
    }
}

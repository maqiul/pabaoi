using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pcbaoi
{
    class ProjectSetting
    {
        [Description("板名")]
        private string name;
        [Description("客户名")]
        private string cname;
        [Description("板宽")]
        private int width;
        [Description("板长")]
        private int height;
        [Description("轨道夹紧量")]
        private int nip;

        public string Name { get => name; set => name = value; }
        public string Cname { get => cname; set => cname = value; }
        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }
        public int Nip { get => nip; set => nip = value; }
    }
}

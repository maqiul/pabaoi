using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pcbaoi
{
    public static class Setting
    {
        private static string projectpath;
        private static string completepicturepath;

        public static string Projectpath { get => projectpath; set => projectpath = value; }
        public static string Completepicturepath { get => completepicturepath; set => completepicturepath = value; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pcbaoi
{
    public static class Setting
    {
        [Description("项目路径")]
        private static string projectpath;
        [Description("完整图片保存路径")]
        private static string completepicturepath;

        public static string Projectpath { get => projectpath; set => projectpath = value; }
        public static string Completepicturepath { get => completepicturepath; set => completepicturepath = value; }
    }
}

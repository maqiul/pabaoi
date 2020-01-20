using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pcbaoi
{
    class Enumall
    {
        /// <summary>
        /// 报警监控
        /// </summary>
        public enum Alarm
        {
            [Description("1147,0, ,0")]
            AL0001A面X轴伺服综合报警,
            [Description("1147,1, A面Y轴伺服综合报警,0")]
            AL0002A面Y轴伺服综合报警,
            [Description("1147,2, B面X轴伺服综合报警,0")]
            AL0003B面X轴伺服综合报警,
            [Description("1147,3, B面Y轴伺服综合报警,0")]
            AL0004B面Y轴伺服综合报警,
            [Description("1147,4, K轴伺服综合报警,0")]
            AL0005K轴伺服综合报警,
            [Description("1147,5, U轴伺服综合报警,0")]
            AL0006U轴伺服综合报警,
            [Description("1147,6, A面X轴左极限到达报警,0")]
            AL0007A面X轴左极限到达报警,
            [Description("1147,7, A面X轴右极限到达报警,0")]
            AL0008A面X轴右极限到达报警,
            [Description("1147,8, A面Y轴左极限到达报警,0")]
            AL0009A面Y轴左极限到达报警,
            [Description("1147,9, A面Y轴右极限到达报警,0")]
            AL0010A面Y轴右极限到达报警,
            [Description("1147,10, B面X轴左极限到达报警,0")]
            AL0011B面X轴左极限到达报警,
            [Description("1147,11, B面X轴右极限到达报警,0")]
            AL0012B面X轴右极限到达报警,
            [Description("1147,12, B面Y轴左极限到达报警,0")]
            AL0013B面Y轴左极限到达报警,
            [Description("1147,13, B面Y轴右极限到达报警,0")]
            AL0014B面Y轴右极限到达报警,
            [Description("1147,14, K轴左极限到达报警,0")]
            AL0015K轴左极限到达报警,
            [Description("1147,15, K轴右极限到达报警,0")]
            AL0016K轴右极限到达报警,
            [Description("1148,0, 本机急停报警,0")]
            AL0017本机急停报警,
            [Description("1148,1, 外部急停报警,0")]
            AL0018外部急停报警,
            [Description("1148,2, 安全门打开报警,0")]
            AL0019安全门打开报警,
            [Description("1148,3, 心跳交互异常报警,0")]
            AL0020心跳交互异常报警,
            [Description("1148,4, 输入信号检测异常,0")]
            AL0021输入信号检测异常,
            [Description("1148,5, 备   用,0")]
            AL0022备用,
            [Description("1148,6, 备   用,0")]
            AL0023备用,
            [Description("1148,7, 备   用,0")]
            AL0024备用,
            [Description("1148,8, 备   用,0")]
            AL0025备用,
            [Description("1148,9, 备   用,0")]
            AL0026备用,
            [Description("1148,10, 备   用,0")]
            AL0027备用,
            [Description("1148,11, 备   用,0")]
            AL0028备用,
            [Description("1148,12, 备   用,0")]
            AL0029备用,
            [Description("1148,13, 备   用,0")]
            AL0030备用,
            [Description("1148,14, 备   用,0")]
            AL0031备用,
            [Description("1148,15, 备   用,0")]
            AL0032备用,
        }
    }
}

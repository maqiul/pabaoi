using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pcbaoi
{

    public class Xycoordinate
    {
        //a面 x 限位
        static int ax = Convert.ToInt32(IniFile.iniRead("XYwidth", "ax"));
        //b面 x 限位
        static int bx = Convert.ToInt32(IniFile.iniRead("XYwidth", "bx"));
        //a面 y 限位
        static int ay = Convert.ToInt32(IniFile.iniRead("XYwidth", "ay"));
        //b面 y 限位
        static int by = Convert.ToInt32(IniFile.iniRead("XYwidth", "by"));
        //电机和物理毫米对应关系
        static int motorScale = 250;
        /// <summary>
        /// a 面 x 运行点位
        /// </summary>
        /// <param name="num">运行数量</param>
        /// <param name="xIntervalInMM">x 运行距离</param>
        /// <param name="differencevalue">载板与pcb直接差值</param>
        /// <returns></returns>
        public static List<int> axcoordinate(int num, int xIntervalInMM,int differencevalue)
        {
            List<int> xcoordinatelist = new List<int>();
            for (int i = 0; i < num; i++)
            {
                xcoordinatelist.Add(ax + differencevalue * motorScale + xIntervalInMM * i * motorScale);
            }

            return xcoordinatelist;
        }
        /// <summary>
        /// b 面 x 运行点位
        /// </summary>
        /// <param name="num">运行数量</param>
        /// <param name="xIntervalInMM">x 运行距离</param>
        /// <param name="differencevalue">载板与pcb直接差值</param>
        /// <returns></returns>
        public static List<int> bxcoordinate(int num, int xIntervalInMM, int differencevalue)
        {
            List<int> xcoordinatelist = new List<int>();
            for (int i = 0; i < num; i++)
            {
                xcoordinatelist.Add(bx + differencevalue * motorScale + xIntervalInMM * i * motorScale);
            }

            return xcoordinatelist;
        }
        /// <summary>
        /// a 面 y 运行点位
        /// </summary>
        /// <param name="num">运行数量</param>
        /// <param name="yIntervalInMM">y 运行距离</param>
        /// <param name="differencevalue">载板与pcb直接差值</param>
        /// <returns></returns>
        public static List<int> aycoordinate(int num, int yIntervalInMM, int differencevalue)
        {
            List<int> ycoordinatelist = new List<int>();
            for (int i = 0; i < num; i++)
            {

                ycoordinatelist.Add(ay + differencevalue * motorScale + yIntervalInMM * i * motorScale);

            }

            return ycoordinatelist;
        }
        /// <summary>
        /// b 面 y 运行点位
        /// </summary>
        /// <param name="num">运行数量</param>
        /// <param name="yIntervalInMM">y 运行距离</param>
        /// <param name="differencevalue">载板与pcb直接差值</param>
        /// <returns></returns>
        public static List<int> bycoordinate(int num, int yIntervalInMM, int differencevalue)
        {
            List<int> ycoordinatelist = new List<int>();
            for (int i = 0; i < num; i++)
            {
                ycoordinatelist.Add(by + differencevalue * motorScale + yIntervalInMM * i * motorScale);
            }

            return ycoordinatelist;
        }
    }
}

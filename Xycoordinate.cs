using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pcbaoi
{

    public class Xycoordinate
    {
        static int ax = Convert.ToInt32(IniFile.iniRead("XYwidth", "ax"));
        static int bx = Convert.ToInt32(IniFile.iniRead("XYwidth", "bx"));
        static int ay = Convert.ToInt32(IniFile.iniRead("XYwidth", "ay"));
        static int by = Convert.ToInt32(IniFile.iniRead("XYwidth", "by"));
        static int motorScale = 250;

        public static List<int> axcoordinate(int num, int xIntervalInMM)
        {
            List<int> xcoordinatelist = new List<int>();
            for (int i = 0; i < num; i++)
            {
                xcoordinatelist.Add(ax + xIntervalInMM * i * motorScale);
            }

            return xcoordinatelist;
        }
        public static List<int> bxcoordinate(int num, int xIntervalInMM)
        {
            List<int> xcoordinatelist = new List<int>();
            for (int i = 0; i < num; i++)
            {
                xcoordinatelist.Add(bx + xIntervalInMM * i * motorScale);
            }

            return xcoordinatelist;
        }
        public static List<int> aycoordinate(int num, int yIntervalInMM)
        {
            List<int> ycoordinatelist = new List<int>();
            for (int i = 0; i < num; i++)
            {

                ycoordinatelist.Add(ay + yIntervalInMM * i * motorScale);

            }

            return ycoordinatelist;
        }
        public static List<int> bycoordinate(int num, int yIntervalInMM)
        {
            List<int> ycoordinatelist = new List<int>();
            for (int i = 0; i < num; i++)
            {
                ycoordinatelist.Add(by + yIntervalInMM * i * motorScale);
            }

            return ycoordinatelist;
        }
    }
}

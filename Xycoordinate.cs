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
        public static List<int> axcoordinate(int num)
        {
            List<int> xcoordinatelist = new List<int>();
            for (int i = 0; i < num; i++)
            {
                xcoordinatelist.Add(ax + 15 * i * 250);
            }

            return xcoordinatelist;
        }
        public static List<int> bxcoordinate(int num)
        {
            List<int> xcoordinatelist = new List<int>();
            for (int i = 0; i < num; i++)
            {
                xcoordinatelist.Add(bx + 15 * i * 250);
            }

            return xcoordinatelist;
        }
        public static List<int> aycoordinate(int num)
        {
            List<int> ycoordinatelist = new List<int>();
            for (int i = 0; i < num; i++)
            {

                ycoordinatelist.Add(ay + 15 * i * 250);

            }

            return ycoordinatelist;
        }
        public static List<int> bycoordinate(int num)
        {
            List<int> ycoordinatelist = new List<int>();
            for (int i = 0; i < num; i++)
            {
                ycoordinatelist.Add(by + 15 * i * 250);
            }

            return ycoordinatelist;
        }
    }
}

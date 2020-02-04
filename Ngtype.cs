using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pcbaoi
{
    class Ngtype
    {
        public enum enumNgtype
     {
            aoken=0,
            huashang=1,
            jieliu=2,
            luotong=3,
            quesun=4,
            wuran=5,
            yanghua=6,
            yashang=7,
            yiwu=8,
            zhanxi=9
        }
        public static string IntConvertToEnum(int i)
        {
            return Enum.GetName(typeof(enumNgtype), i);

        }
    }
}

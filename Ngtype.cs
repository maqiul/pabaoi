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
            beiyong2=0,
            beiyong3=1,
            beiyong4=2,
            beiyong5=3,
            aoken=4,
            huashang= 5,
            jiban_duanlie= 6,
            jieliu=7,
            luotong=8,
            quesun=9,
            wuran=10,
            yanghua=11,
            yashang=12,
            yiwu=13,
            zhanxi=14
        }
        public static string IntConvertToEnum(int i)
        {
            return Enum.GetName(typeof(enumNgtype), i);

        }
    }
}

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
            aoken=2,
            huashang= 3,
            jiban_duanlie= 4,
            jieliu=5,
            luotong=6,
            quesun=7,
            wuran=8,
            xigao_wuran=9,
            yanghua=10,
            yashang=11,
            yiwu=12,
            zhanxi=13
        }
        public static string IntConvertToEnum(int i)
        {
            return Enum.GetName(typeof(enumNgtype), i);

        }
    }
}

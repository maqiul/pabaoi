using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pcbaoi
{
    class Algorithmtype
    {
        [Description("算法框名")]
        string typename;
        [Description("所属子基板")]
        string owmername;

        public string Typename { get => typename; set => typename = value; }
        public string Owmername { get => owmername; set => owmername = value; }
    }
}

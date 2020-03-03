using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pcbaoi
{
    class Checkpic
    {
        [Description("是否ng")]
        private bool isNg;
        [Description("ng框列表")]
        private List<bbox_t> lists;

        public bool IsNg { get => isNg; set => isNg = value; }
        public List<bbox_t> Lists { get => lists; set => lists = value; }
    }
}

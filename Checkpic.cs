using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pcbaoi
{
    class Checkpic
    {
        private bool isNg;
        private List<bbox_t> lists;

        public bool IsNg { get => isNg; set => isNg = value; }
        public List<bbox_t> Lists { get => lists; set => lists = value; }
    }
}

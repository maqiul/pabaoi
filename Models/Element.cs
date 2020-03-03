using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pcbaoi
{
    class Element
    {
        private string name;
        private int x;
        private int y;
        private int angle;
        private string banid;
        private string elementsdb;
        private string elementype;

        public string Name { get => name; set => name = value; }
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public int Angle { get => angle; set => angle = value; }
        public string Banid { get => banid; set => banid = value; }
        public string Elementsdb { get => elementsdb; set => elementsdb = value; }
        public string Elementype { get => elementype; set => elementype = value; }
    }
}

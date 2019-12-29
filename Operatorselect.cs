using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pcbaoi
{
    class Operatorselect
    {
        private string algorithm;
        private string operatorname;
        private float confidence;
        private int percentageup;
        private int percentagedown;
        private string codetype;
        private int luminanceon;
        private int luminancedown;
        private int rednumon;
        private int rednumdown;
        private int greennumon;
        private int greennumdown;
        private int bluenumon;
        private int bluenumdown;

        public string Algorithm { get => algorithm; set => algorithm = value; }
        public string Operatorname { get => operatorname; set => operatorname = value; }
        public float Confidence { get => confidence; set => confidence = value; }
        public int Percentageup { get => percentageup; set => percentageup = value; }
        public int Percentagedown { get => percentagedown; set => percentagedown = value; }
        public string Codetype { get => codetype; set => codetype = value; }
        public int Luminanceon { get => luminanceon; set => luminanceon = value; }
        public int Luminancedown { get => luminancedown; set => luminancedown = value; }
        public int Rednumon { get => rednumon; set => rednumon = value; }
        public int Rednumdown { get => rednumdown; set => rednumdown = value; }
        public int Greennumon { get => greennumon; set => greennumon = value; }
        public int Greennumdown { get => greennumdown; set => greennumdown = value; }
        public int Bluenumon { get => bluenumon; set => bluenumon = value; }
        public int Bluenumdown { get => bluenumdown; set => bluenumdown = value; }
    }
}

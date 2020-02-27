using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pcbaoi
{
    class Operatorselect
    {
        [Description("记录完整算法名")]
        [Column(name: "operatonameall")]
        private string operatonameall;
        [Description("定位框控件名")]
        [Column(name: "outpicturename")]
        private string outpicturename;
        [Description("定位框起点x坐标")]
        [Column(name: "outstartx")]
        private int outstartx;
        [Description("定位框起点y坐标")]
        [Column(name: "outstarty")]
        private int outstarty;
        [Description("定位框高度")]
        [Column(name: "outheight")]
        private int outheight;
        [Description("定位框宽度")]
        [Column(name: "outwidth")]
        private int outwidth;
        [Description("mark点算法内框名")]
        [Column(name: "intpicturename")]
        private string intpicturename;
        [Description("mark点算法内框起点x坐标")]
        [Column(name: "instartx")]
        private int instartx;
        [Description("mark点算法内框起点y坐标")]
        [Column(name: "instarty")]
        private int instarty;
        [Description("mark点算法内框高度")]
        [Column(name: "inheight")]
        private int inheight;
        [Description("mark点算法内框宽度")]
        [Column(name: "inwint")]
        private int inwidth;
        [Description("父控件名")]
        [Column(name: "parent")]
        private string parent;
        [Description("算法名")]
        [Column(name: "algorithm")]
        private string algorithm;
        [Description("算子名")]
        [Column(name: "operatorname")]
        private string operatorname;
        [Description("置信度")]
        [Column(name: "confidence")]
        private string confidence;
        [Description("上限")]
        [Column(name: "percentageup")]
        private int percentageup;
        [Description("下限")]
        [Column(name: "percentagedown")]
        private int percentagedown;
        [Description("条码类型")]
        [Column(name: "codetype")]
        private string codetype;
        [Description("直方图上限")]
        [Column(name: "luminanceon")]
        private int luminanceon;
        [Description("直方图下限")]
        [Column(name: "luminancedown")]
        private int luminancedown;
        [Description("r值上限")]
        [Column(name: "rednumon")]
        private int rednumon;
        [Description("r值下限")]
        [Column(name: "rednumdown")]
        private int rednumdown;
        [Description("g值上限")]
        [Column(name: "greennumon")]
        private int greennumon;
        [Description("g值下限")]
        [Column(name: "greennumdown")]
        private int greennumdown;
        [Description("b值上限")]
        [Column(name: "bluenumon")]
        private int bluenumon;
        [Description("b值下限")]
        [Column(name: "bluenumdown")]
        private int bluenumdown;
        [Description("保存图片名字存图为: 完整算法名.jpg")]
        private string picname;

        public string Algorithm { get => algorithm; set => algorithm = value; }
        public string Operatorname { get => operatorname; set => operatorname = value; }
        public string Confidence { get => confidence; set => confidence = value; }
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
        public string Operatonameall { get => operatonameall; set => operatonameall = value; }
        public string Outpicturename { get => outpicturename; set => outpicturename = value; }
        public int Outstartx { get => outstartx; set => outstartx = value; }
        public int Outstarty { get => outstarty; set => outstarty = value; }
        public int Outheight { get => outheight; set => outheight = value; }
        public int Outwidth { get => outwidth; set => outwidth = value; }
        public string Intpicturename { get => intpicturename; set => intpicturename = value; }
        public int Instartx { get => instartx; set => instartx = value; }
        public int Instarty { get => instarty; set => instarty = value; }
        public int Inheight { get => inheight; set => inheight = value; }
        public int Inwidth { get => inwidth; set => inwidth = value; }
        public string Parent { get => parent; set => parent = value; }
        public string Picname { get => picname; set => picname = value; }
    }
}

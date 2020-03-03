using pcbaoi.Algorithms.Pars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pcbaoi.Algorithms.Model
{
    public class AiDetectRect : BaseDetectRect<AiPars>
    {
        public override AiPars Run(AiPars res)
        {
            int n = detect_image_path(res.fileName, ref res.boxlist);
            //if (n == -1) Console.WriteLine("调用失败，请检测目录是否包含opencv的dll");
            return res;
            //throw new NotImplementedException();
        }
    }
}

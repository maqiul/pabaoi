using RTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pcbaoi.Algorithms
{
    public abstract class BaseDetectRect<T>: BaseAoiAlgorithms
    {
        public System.Drawing.Rectangle rectangle;
        public string algorithmsName; // 算法名
        public string operatorName; // 算子名
  
        public Rectangle getRTreeRectangle()
        {
            return new Rectangle(rectangle.X, rectangle.Y, rectangle.Width + rectangle.X, rectangle.Y + rectangle.Height, 0, 0);
        }
        public abstract T Run(T res);
    }
}

using RTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rectangle = System.Drawing.Rectangle;
using RTRectangle = RTree.Rectangle;

namespace pcbaoi.Algorithms
{
    public abstract class BaseDetectRect<T>: BaseAoiAlgorithms
    {
        public long id; // rtree key
        public System.Drawing.RectangleF rectangle;
        public string algorithmsName; // 算法名
        public string operatorName; // 算子名
  
        public RTRectangle getRTreeRectangle()
        {
            return new RTRectangle(rectangle.X, rectangle.Y, rectangle.Width + rectangle.X, rectangle.Y + rectangle.Height, 0, 0);
        }
        public Rectangle geDrawingRectangle()
        {
            return new Rectangle(Convert.ToInt32(rectangle.X), Convert.ToInt32(rectangle.Y), Convert.ToInt32(rectangle.Width + rectangle.X), Convert.ToInt32(rectangle.Y + rectangle.Height));
        }
        public abstract T Run(T res);
    }
}

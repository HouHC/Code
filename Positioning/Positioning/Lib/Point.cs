using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Templete.Positioning.Lib
{
    public class Point
    {
        public double XPosition { get; set; } 
        public double YPOsition { get; set; }

        public Point(double x = 0,double y = 0)
        {
            XPosition = x;
            YPOsition = y;
        }

        /// <summary>
        /// 获取两圆的圆心距
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static double DisOfTwoPoint(Point first,Point second)
        {
            if (first != null && second != null)
                return Math.Sqrt(Math.Pow(2, first.XPosition - second.XPosition) + Math.Pow(2, first.YPOsition - second.YPOsition));
            else
                return double.NaN;
        }

        /// <summary>
        /// 重载Equals方法
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var p = obj as Point;
            if (p == null) return false;
            return this.XPosition == p.XPosition && this.YPOsition == p.YPOsition;
        }

        /// <summary>
        /// 重载ToString方法
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Point({0},{1})",XPosition,YPOsition);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

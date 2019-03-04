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

        public static double DisOfTwoPoint(Point first,Point second)
        {
            if (first != null && second != null)
                return Math.Sqrt(Math.Pow(2, first.XPosition - second.XPosition) + Math.Pow(2, first.YPOsition - second.YPOsition));
            else
                return double.NaN;
        }

        public override bool Equals(object obj)
        {
            var p = obj as Point;
            if (p == null) return false;
            return this.XPosition == p.XPosition && this.YPOsition == p.YPOsition;
        }

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

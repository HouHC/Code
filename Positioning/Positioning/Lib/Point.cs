using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Positioning.Lib
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
            return Math.Sqrt(Math.Pow(2, first.XPosition - second.XPosition) + Math.Pow(2, first.YPOsition - second.YPOsition)); ;
        }
    }
}

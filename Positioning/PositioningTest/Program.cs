using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Templete.Positioning;
using Templete.Positioning.Lib;

namespace PositioningTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Circle p1 = new Circle(0,0,2.5);
            Circle p2 = new Circle(0,3,2.0);
            Circle p3 = new Circle(4,0,3.5);
            List<Circle> circles = new List<Circle>();
            circles.Add(p1);
            circles.Add(p2);
            circles.Add(p3);
            Point p = Positioning.GetPosition(circles);
            if(p != null)
                Console.WriteLine(p.ToString());
            Console.ReadKey();
        }
    }
}

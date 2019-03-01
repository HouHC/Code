using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Templete.Positioning.Lib
{
    public class Beacon
    {
        public Point Location { get;}

        //public double Height { get; set; }

        public int RSSI { get; set; }

        //单位距离下RSSI的绝对值
        public int A { get; set; } 

        //环境衰减因子，需要测试矫正
        public double N { get; set; }

        public Beacon(Point p, int rssi,int a,double n)
        {
            Location = p;
            RSSI = rssi;
            A = a;
            N = n;
        }

        public Beacon(int rssi, int a, double n,double x = 0, double y = 0):this(new Point(x, y), rssi, a, n)
        {
        }

        //根据RSSI信号强度计算出信标到蓝牙网关的距离
        public static double GetDis(Beacon beacon)
        {
            double p = (beacon.A - beacon.RSSI) / (10 * beacon.N);
            //平面算法求值
            return Math.Pow(p,10);
            //三维算法求值
            //return Math.Sqrt(Math.Pow(2, Math.Pow(p, 10)) - Math.Pow(2,beacon.Height));
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Templete.Positioning.Lib
{
    public class Circle
    {
        public Point Center { get;private set; }
        public double Radius { get;private set; }

        public Circle(Point p, double r)
        {
            Center = p;
            Radius = r;
        }

        public Circle(double x,double y,double r):this(new Point(x, y),r)
        {
        }

        public override string ToString()
        {
            return string.Format("Circle({0},{1},{2})",Center.XPosition,Center.YPOsition,Radius);
        }


        /// <summary>
        /// 获取两圆的交点，无交点返回null
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static List<Point> GetPointsOfIntersection(Circle first,Circle second)
        {
            List<Point> points = new List<Point>();
            //获取元组，两圆关系以及两圆之间圆心的距离
            Tuple<CircleRelationship,double> t = GetCircleRelationship(first,second);
            if (t.Item1 == CircleRelationship.相离)
                return null;
            else    //求出两圆交点的坐标，若相切则只有一个交点，相交为两个交点
            {
                if ((first.Center.XPosition - second.Center.XPosition) == 0)
                {
                    //double x1 = t.Item2 - second.Radius;
                    //double x2 = t.Item2 - first.Radius;
                    double x =  t.Item2 - (second.Radius - first.Radius) / 2.0;
                    double h = Math.Sqrt(Math.Pow(2,first.Radius) - Math.Pow(2,x));
                    //加入List
                    points.Add(new Point(first.Center.XPosition + x, h));
                    points.Add(new Point(first.Center.XPosition + x, -h));
                }
                else if ((first.Center.YPOsition - second.Center.YPOsition) == 0)
                {
                    double y = t.Item2 - (second.Radius - first.Radius) / 2.0;
                    double l = Math.Sqrt(Math.Pow(2, first.Radius) - Math.Pow(2, y));
                    //加入List
                    points.Add(new Point(first.Center.XPosition + l, y));
                    points.Add(new Point(first.Center.XPosition + l, y));
                }
                else
                {
                    //圆心之间连线的斜率
                    double k1 = (first.Center.YPOsition - second.Center.YPOsition) / (first.Center.XPosition - second.Center.XPosition);
                    //交点之间连线的斜率
                    double k2 = -(1.0 / k1);
                    //圆心之间连线与焦点之间连线的交点坐标
                    double xe = first.Center.XPosition + ((Math.Pow(2, first.Radius) - Math.Pow(2, second.Radius) + Math.Pow(2, t.Item2)) / (2 * Math.Pow(2, t.Item2))) * (second.Center.XPosition - first.Center.XPosition);
                    double ye = first.Center.YPOsition + ((Math.Pow(2, first.Radius) - Math.Pow(2, second.Radius) + Math.Pow(2, t.Item2)) / (2 * Math.Pow(2, t.Item2))) * (second.Center.YPOsition - first.Center.YPOsition);
                    if (t.Item1 == CircleRelationship.相切)
                    {
                        points.Add(new Point(xe, ye)); //两圆相切，则只有此一个交点
                    }
                    else
                    {
                        //计算出其中一个焦点到圆心之间连线与焦点之间连线的交点的距离
                        double ce = Math.Pow(2, first.Radius) - Math.Pow(2, (xe - first.Center.XPosition)) - Math.Pow(2, (ye - first.Center.YPOsition));
                        //两圆交点坐标
                        double xc = xe - ce / (Math.Sqrt(1 + Math.Pow(2, k2)));
                        double yc = ye + k2 * (xc - xe);

                        double xd = xe + ce / (Math.Sqrt(1 + Math.Pow(2, k2)));
                        double yd = ye + k2 * (xd - xe);
                        //加入List
                        points.Add(new Point(xc, yc));
                        points.Add(new Point(xd, yd));
                    }
                }
            }
            return points;
        }

        //public static CircleRelationship GetCircleRelationship(Circle first ,Circle second)
        //{
        //    double dis = Point.DisOfTwoPoint(first.Center,second.Center);
        //    CircleRelationship relationship = CircleRelationship.相离;
        //    if (dis == (first.Radius + second.Radius) || dis == Math.Abs(first.Radius - second.Radius))
        //        relationship = CircleRelationship.相切;
        //    else if (dis > Math.Abs(first.Radius - second.Radius) && dis < (first.Radius + second.Radius))
        //        relationship = CircleRelationship.相交;
        //    return relationship;
        //}

        /// <summary>
        /// 判断两圆关系，返回元组，两圆关系和圆心距
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns>返回元组，两圆关系和圆心距</returns>
        public static Tuple<CircleRelationship,double> GetCircleRelationship(Circle first, Circle second)
        {
            double dis = Point.DisOfTwoPoint(first.Center, second.Center);
            CircleRelationship relationship = CircleRelationship.相离;
            if (dis == (first.Radius + second.Radius) || dis == Math.Abs(first.Radius - second.Radius))
                relationship = CircleRelationship.相切;
            else if (dis > Math.Abs(first.Radius - second.Radius) && dis < (first.Radius + second.Radius))
                relationship = CircleRelationship.相交;
            return new Tuple<CircleRelationship, double>(relationship,dis);
        }

        /// <summary>
        /// 判断那个点离本身最近
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public Point GetNearestPoint(Point p1, Point p2)
        {
            return Point.DisOfTwoPoint(p1, this.Center) < Point.DisOfTwoPoint(p2, this.Center) ? p1 : p2;
        }

        /// <summary>
        /// 判断两点中离圆最近的一点
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="cir"></param>
        /// <returns></returns>
        public static Point GetNearestPoint(Point p1,Point p2 ,Circle cir)
        {
            return Point.DisOfTwoPoint(p1, cir.Center) < Point.DisOfTwoPoint(p2, cir.Center) ? p1 : p2;
        }
    }

    public enum CircleRelationship
    {
        相交 = 0,
        相切 = 1,
        相离 = 2
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Templete.Positioning.Lib;

namespace Templete.Positioning
{
    public static class Positioning
    {

        /// <summary>
        /// 获取定位的点
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static Point GetPosition(List<Circle> list)
        {
            if (list == null || list.Count < 3)
                return null;
            Point p = null;
            //根据半径排序
            var circles = list.OrderBy(c => c.Radius);
            List<Tuple<Point, double>> pointList = new List<Tuple<Point, double>>();
            for (int i = 0;i < list.Count - 2; i++)
            {
                //List<Circle> cs = circles.Take(3).Skip(i) as List<Circle>;
                //Tuple<Point, double> t = GetPointAndWeights(cs);
                //if(t != null)
                //    pointList.Add(t);
                for (int j = i + 1; j < list.Count - 1; j++)
                {
                    List<Circle> cs = new List<Circle>();
                    cs.Add(list[i]);
                    cs.Add(list[j]);
                    cs.Add(list[j+1]);
                    Tuple<Point, double> t = GetPointAndWeights(cs);
                    if (t != null)
                        pointList.Add(t);
                }
            }
            if (pointList.Count == 0) return null;
            double x = 0, y = 0;
            double totalWeights = 0;
            //加权计算
            for (int i = 0;i < pointList.Count;i++)
            {
                x += pointList[i].Item1.XPosition * pointList[i].Item2; 
                y += pointList[i].Item1.YPOsition * pointList[i].Item2;
                totalWeights += pointList[i].Item2;
            }
            x = x / totalWeights;
            y = y / totalWeights;
            p = new Point(x, y);
            Log.WriteInfo(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " : " + p.ToString());
            return p;
        }

        /// <summary>
        /// 获取定位，返回值为double数组
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double[] GetPosition(params double[] x)
        {
            if (x == null || x.Length <= 9)
            {
                return null;
            }
            else
            {
                if ((x.Length % 3) == 0)
                {
                    List<Circle> circles = new List<Circle>();
                    for (int i = 0; i < x.Length;)
                    {
                        circles.Add(new Circle(x[i], x[i + 1], x[i + 2]));
                        i += 3;
                    }
                    Point point = GetPosition(circles);
                    return new double[] { point.XPosition , point.YPOsition};

                }
                else
                {
                    return null;
                }
                
            }
        }

        /// <summary>
        /// 根据质心定位，求出交点坐标和权值
        /// </summary>
        /// <param name="list"></param>
        /// <returns>返回元组</returns>
        private static Tuple<Point, double> GetPointAndWeights(List<Circle> list)
        {
            if (list.Count != 3) return null;
            foreach (var item in list)
            {
                Log.WriteInfo(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " : " + item.ToString());
            }
            //获取三个圆每两个的交点，无交点则数据无效
            var plist1 = Circle.GetPointsOfIntersection(list[0], list[1]);
            var plist2 = Circle.GetPointsOfIntersection(list[1], list[2]);
            var plist3 = Circle.GetPointsOfIntersection(list[0], list[2]);
            Tuple<Point, double> t = null;
            if (plist1 != null && plist2 != null && plist3 != null)
            {
                double x = 0, y = 0;
                //权值
                double weight = 1.0 / (list[0].Radius + list[1].Radius + list[2].Radius);
                if (plist1.Count == 1)  //第0个圆与第1个圆相切
                {
                    if (plist2.Count == 1)   //第0个圆与第1个圆相切，第1个圆与第2个圆相切，
                    {
                        if (plist3.Count == 1)  //三个圆两两相切
                        {
                             x = (plist1[0].XPosition + plist2[0].XPosition + plist3[0].XPosition) / 3;
                             y = (plist1[0].YPOsition + plist2[0].YPOsition + plist3[0].YPOsition) / 3;
                        }
                        else    //第0个圆与第1个圆相切，第1个圆与第2个圆相切,第0个圆与第2个圆相交
                        {
                            //var p = Circle.GetNearestPoint(plist3[0],plist3[1],list[1]);
                            var p = list[1].GetNearestPoint(plist3[0], plist3[1]);
                            x = (plist1[0].XPosition + plist2[0].XPosition + p.XPosition) / 3;
                            y = (plist1[0].YPOsition + plist2[0].YPOsition + p.YPOsition) / 3;
                        }
                    }
                    else                     //第0个圆与第1个圆相切，第1个圆与第2个圆相交
                    {
                        if (plist3.Count == 1)
                        {
                            //var p = Circle.GetNearestPoint(plist2[0], plist2[1], list[0]);
                            var p = list[0].GetNearestPoint(plist2[0], plist2[1]);
                            x = (plist1[0].XPosition + p.XPosition + plist3[0].XPosition) / 3;
                            y = (plist1[0].YPOsition + p.YPOsition + plist3[0].YPOsition) / 3;
                        }
                        else //0、1相切，1、2相交，0、2相交
                        {
                            //var p1 = Circle.GetNearestPoint(plist2[0], plist2[1], list[0]);
                            //var p2 = Circle.GetNearestPoint(plist3[0], plist3[1], list[1]);
                            var p1 = list[0].GetNearestPoint(plist2[0], plist2[1]);
                            var p2 = list[1].GetNearestPoint(plist3[0], plist3[1]);
                            x = (plist1[0].XPosition + p1.XPosition + p2.XPosition) / 3;
                            y = (plist1[0].YPOsition + p1.YPOsition + p2.YPOsition) / 3;
                        }
                    }


                }
                else if (plist2.Count == 1) //第1个圆与第2个圆相切
                {
                    if (plist3.Count == 1) //0、1相交，1、2相切，0、2相切
                    {
                        //var p = Circle.GetNearestPoint(plist1[0], plist1[1], list[2]);
                        var p = list[2].GetNearestPoint(plist1[0], plist1[1]);
                        x = (p.XPosition + plist2[0].XPosition + plist3[0].XPosition) / 3;
                        y = (p.YPOsition + plist2[0].YPOsition + plist3[0].YPOsition) / 3;
                    }
                    else //0、1相切，1、2相交，0、2相交
                    {
                        //var p1 = Circle.GetNearestPoint(plist1[0], plist1[1], list[2]);
                        //var p2 = Circle.GetNearestPoint(plist3[0], plist3[1], list[1]);
                        var p1 = list[2].GetNearestPoint(plist1[0], plist1[1]);
                        var p2 = list[1].GetNearestPoint(plist3[0], plist3[1]);
                        x = (p1.XPosition + plist2[0].XPosition + p2.XPosition) / 3;
                        y = (p1.YPOsition + plist2[0].YPOsition + p2.YPOsition) / 3;
                    }
                }
                else if (plist3.Count == 1) //第0个圆与第2个圆相切
                {
                    //var p1 = Circle.GetNearestPoint(plist1[0], plist1[1], list[2]);
                    //var p2 = Circle.GetNearestPoint(plist2[0], plist2[1], list[0]);
                    var p1 = list[2].GetNearestPoint(plist1[0], plist1[1]);
                    var p2 = list[0].GetNearestPoint(plist2[0], plist2[1]);
                    x = (p1.XPosition +  p2.XPosition + plist3[0].XPosition ) / 3;
                    y = (p1.YPOsition +  p2.YPOsition + plist3[0].YPOsition) / 3;
                }
                else //三个圆两两相交
                {
                    //var p1 = Circle.GetNearestPoint(plist1[0], plist1[1], list[2]);
                    //var p2 = Circle.GetNearestPoint(plist2[0], plist2[1], list[0]);
                    //var p3 = Circle.GetNearestPoint(plist3[0], plist3[1], list[1]);
                    var p1 = list[2].GetNearestPoint(plist1[0], plist1[1]);
                    var p2 = list[0].GetNearestPoint(plist2[0], plist2[1]);
                    var p3 = list[1].GetNearestPoint(plist3[0], plist3[1]);
                    x = (p1.XPosition + p2.XPosition + p3.XPosition) / 3;
                    y = (p1.YPOsition + p2.YPOsition + p3.YPOsition) / 3;
                }
                Point result = new Point(x, y);
                Log.WriteInfo(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " : " + result.ToString());
                t = new Tuple<Point, double>(result, weight);
            }
            return t;
        }
    }
}

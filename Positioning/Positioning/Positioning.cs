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
        public static Point GetPosition(List<Circle> list)
        {
            if (list == null || list.Count < 3)
                return null;
            Point p = null;
            var circles = list.OrderBy(c => c.Radius);
            List<Tuple<Point, double>> pointList = new List<Tuple<Point, double>>();
            for (int i = 0;i < list.Count - 2; i++)
            {
                List<Circle> cs = circles.Take(3).Skip(i) as List<Circle>;
                pointList.Add(GetPointAndWeights(cs));
            }
            double x = 0, y = 0;
            double totalWeights = 0;
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

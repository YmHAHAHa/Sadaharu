using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Sadaharu.Shapes
{
    class Line:Shape
    {
        public Point a, b;

        public Line(Point p1, Point p2) : base()
        {
            a = p1;
            b = p2;
        }

        public override bool isSelect(Point p)
        {
            //return false;
            int x1 = a.X < b.X ? a.X : b.X;
            int x2 = a.X + b.X - x1;
            if (p.X < x1 - 5 || p.X > x2 + 5)
            {
                return false;
            }
            return disPoint2Line(a, b, p) < 5.0;
        }

        public override string showMessage()
        {
            return string.Format("a line: ({0},{1}) to ({2},{3})",
                    a.X, a.Y, b.X, b.Y);
        }

        public static double disPoint2Line(PointF a,PointF b, PointF p)
        {
            double dis = 0;
            if (a.X == b.X)
            {
                dis = Math.Abs(p.X - a.X);

                return dis;
            }
            double lineK = (b.Y - a.Y) / (b.X - a.X);

            double lineC = (b.X * a.Y - a.X * b.Y) / (b.X - a.X);

            dis = Math.Abs(lineK * p.X - p.Y + lineC) / (Math.Sqrt(lineK * lineK + 1));

            return dis;
        }

        public override void startSelect()
        {
            base.startSelect();
        }

        public override void endSelect()
        {
            base.endSelect();
        }
    }
}

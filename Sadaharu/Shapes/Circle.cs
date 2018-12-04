using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using Sadaharu.Mybuttons;
using Sadaharu.Mylibs;

namespace Sadaharu.Shapes
{
    class Circle:Shape
    {
        public Point a, b, c, d;

        Point midPoint;

        RectAdjustButton adjustButton1, adjustButton2, adjustButton3, adjustButton4;

        AdjustButton moveButton;

        public Circle(Point p1, Point p2) : base()
        {
            a = p1;
            b = new Point(p1.X, p2.Y);
            c = p2;
            d = new Point(p2.X, p1.Y);
            midPoint = new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
        }

        public override bool isSelect(Point p)
        {
            //throw new NotImplementedException();
            int minx = a.X < c.X ? a.X : c.X,
                miny = a.Y < c.Y ? a.Y : c.Y;
            int lx = a.X + c.X - minx - minx,
                ly = a.Y + c.Y - miny - miny;
            int a2 = ly;
            Point focusp1, focusp2;
            if (lx > ly)
            {
                a2 = lx;
                double c = Math.Sqrt(lx * lx - ly * ly) / 2;
                int cc = (int)c;
                focusp1 = new Point(midPoint.X - cc, midPoint.Y);
                focusp2 = new Point(midPoint.X + cc, midPoint.Y);
            }
            else
            {
                double c = Math.Sqrt(ly * ly - lx * lx) / 2;
                int cc = (int)c;
                focusp1 = new Point(midPoint.X, midPoint.Y - cc);
                focusp2 = new Point(midPoint.X, midPoint.Y + cc);
            }
            double p2f1 = Math.Sqrt((p.X - focusp1.X) * (p.X - focusp1.X)
                + (p.Y - focusp1.Y) * (p.Y - focusp1.Y));
            double p2f2 = Math.Sqrt((p.X - focusp2.X) * (p.X - focusp2.X)
                + (p.Y - focusp2.Y) * (p.Y - focusp2.Y));
            if ((p2f1 + p2f2) < (a2 + 8) && (p2f1 + p2f2) > (a2 - 8))
            {
                return true;
            }
            return false;
        }

        public override string showMessage()
        {
            return string.Format("the circle: ({0},{1}) to ({2},{3})",
                    a.X, a.Y, c.X, c.Y);
        }

        public override void startSelect()
        {
            base.startSelect();
        }

        public override void endSelect()
        {
            base.endSelect();
        }

        public override void startResize()
        {

        }

        public override void endResize()
        {

        }
    }
}

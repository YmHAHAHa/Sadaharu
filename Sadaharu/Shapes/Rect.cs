using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Sadaharu.Shapes
{
    class Rect:Shape
    {
        public Point a, b, c, d;

        public Rect(Point p1, Point p2) : base()
        {
            a = p1;
            b = new Point(p1.X, p2.Y);
            c = p2;
            d = new Point(p2.X, p1.Y);
        }

        public override bool isSelect(Point p)
        {
            Line line1 = new Line(a, b);
            if (line1.isSelect(p)) return true;
            line1.b = d;
            if (line1.isSelect(p)) return true;
            line1.a = c;
            if (line1.isSelect(p)) return true;
            line1.b = b;
            if (line1.isSelect(p)) return true;
            return false;
        }

        public override string showMessage()
        {
            return string.Format("a rectangle: ({0},{1}), ({2},{3}), ({4},{5}), ({6},{7})",
                    a.X, a.Y, b.X, b.Y, c.X, c.Y, d.X, d.Y);
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

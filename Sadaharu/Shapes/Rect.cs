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
    class Rect:Shape
    {
        public Point a, b, c, d;

        RectAdjustButton adjustButton1, adjustButton2, adjustButton3, adjustButton4;

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

            if (adjustButton1 == null)
            {
                adjustButton1 = new RectAdjustButton(Common.mainPicture,
                    new Point(a.X - 3, a.Y - 3), Cursors.SizeNS);
            }
            if (adjustButton2 == null)
            {
                adjustButton2 = new RectAdjustButton(Common.mainPicture,
                    new Point(b.X - 3, b.Y - 3), Cursors.SizeNS);
            }
            if (adjustButton3 == null)
            {
                adjustButton3 = new RectAdjustButton(Common.mainPicture,
                    new Point(c.X - 3, c.Y - 3), Cursors.SizeNS);
            }
            if (adjustButton4 == null)
            {
                adjustButton4 = new RectAdjustButton(Common.mainPicture,
                    new Point(d.X - 3, d.Y - 3), Cursors.SizeNS);
            }

            adjustButton1.setAllPoints(
                new Ref<Point>(() => a, z => { a = z; }),
                new Ref<Point>(() => b, z => { b = z; }),
                new Ref<Point>(() => d, z => { d = z; }),
                new Ref<Point>(() => adjustButton2.Location, z => { adjustButton2.Location = z; }),
                new Ref<Point>(() => adjustButton4.Location, z => { adjustButton4.Location = z; }));
            adjustButton2.setAllPoints(
                new Ref<Point>(() => b, z => { b = z; }),
                new Ref<Point>(() => a, z => { a = z; }),
                new Ref<Point>(() => c, z => { c = z; }),
                new Ref<Point>(() => adjustButton1.Location, z => { adjustButton1.Location = z; }),
                new Ref<Point>(() => adjustButton3.Location, z => { adjustButton3.Location = z; }));
            adjustButton3.setAllPoints(
                new Ref<Point>(() => c, z => { c = z; }),
                new Ref<Point>(() => d, z => { d = z; }),
                new Ref<Point>(() => b, z => { b = z; }),
                new Ref<Point>(() => adjustButton4.Location, z => { adjustButton4.Location = z; }),
                new Ref<Point>(() => adjustButton2.Location, z => { adjustButton2.Location = z; }));
            adjustButton4.setAllPoints(
                new Ref<Point>(() => d, z => { d = z; }),
                new Ref<Point>(() => c, z => { c = z; }),
                new Ref<Point>(() => a, z => { a = z; }),
                new Ref<Point>(() => adjustButton3.Location, z => { adjustButton3.Location = z; }),
                new Ref<Point>(() => adjustButton1.Location, z => { adjustButton1.Location = z; }));
        }

        public override void endSelect()
        {
            base.endSelect();
            adjustButton1.clear();
            adjustButton1 = null;
            adjustButton2.clear();
            adjustButton2 = null;
            adjustButton3.clear();
            adjustButton3 = null;
            adjustButton4.clear();
            adjustButton4 = null;
        }
    }
}

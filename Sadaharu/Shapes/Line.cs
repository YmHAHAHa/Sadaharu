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
            return false;
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

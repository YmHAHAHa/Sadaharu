using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Sadaharu.Shape
{
    class Line:Shape
    {
        public Point a, b;

        public Line(Pen pen) : base(pen)
        {

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

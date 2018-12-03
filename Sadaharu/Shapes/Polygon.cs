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
    class Polygon:Shape
    {
        public List<Point> pointList;

        public Polygon(List<Point> points) : base()
        {
            pointList = points;
        }

        public override bool isSelect(Point p)
        {
            //return false;
            if (pointList.Count < 2) return false;
            Line line = new Line(pointList[0], pointList[pointList.Count - 1]);
            if (line.isSelect(p)) return true;
            for(int i = 1; i < pointList.Count; ++i)
            {
                line = new Line(pointList[i - 1], pointList[i]);
                if (line.isSelect(p)) return true;
            }
            return false;
        }

        public override string showMessage()
        {
            string tmp = "the polygon: ";
            foreach (Point i in pointList) tmp += string.Format("({0},{1}),", i.X, i.Y);
            return tmp;
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
            base.startResize();
        }

        public override void endResize()
        {
            base.endResize();
        }
    }
}

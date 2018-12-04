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
    class Curve:Shape
    {
        public List<Point> pointList;

        List<AdjustButton> adjustButtonList;

        AdjustButton moveButton;

        public Curve(List<Point> points) : base()
        {
            pointList = points;
            adjustButtonList = new List<AdjustButton>();
        }

        public override bool isSelect(Point p)
        {
            //return false;
            //if (pointList.Count < 1) return false;
            //Line line = new Line(pointList[0], pointList[1]);
            //if (line.isSelect(p)) return true;
            //for (int i = 2; i < pointList.Count; ++i)
            //{
            //    line.a = pointList[i - 1];
            //    line.b = pointList[i];
            //    if (line.isSelect(p)) return true;
            //}
            //return false;

            Image image = new Bitmap(Common.mainPicture.Width, Common.mainPicture.Height);
            using (Graphics g = Graphics.FromImage(image))
            {
                g.Clear(Color.White);
                g.DrawCurve(Pens.Black, pointList.ToArray());
            }
            Bitmap bmp = new Bitmap(image);
            for(int i = p.X - 5; i < p.X + 5; ++i)
            {
                for(int j = p.Y - 5; j < p.Y + 5; ++j)
                {
                    Color color = bmp.GetPixel(i, j);
                    if (color.ToArgb() == Color.Black.ToArgb())
                        return true;
                }
            }
            return false;
        }

        public override string showMessage()
        {
            string tmp = "the curve: ";
            foreach (Point i in pointList) tmp += string.Format("({0},{1}),", i.X, i.Y);
            return tmp;
        }

        public override void startSelect()
        {
            base.startSelect();
            int sumx = 0, sumy = 0;
            for (int i = 0; i < pointList.Count; ++i)
            {
                AdjustButton tmp = new AdjustButton(Common.mainPicture,
                    new Point(pointList[i].X - 3, pointList[i].Y - 3), Cursors.SizeNS);
                //tmp.setAllPoints(
                //    new Ref<Point>(() => pointList[i], z => { pointList[i] = z; }));
                tmp.MouseDown += AB_MouseDown;
                tmp.MouseMove += AB_MouseMove;
                tmp.MouseUp += AB_MouseUp;
                tmp.setAllPoints();
                adjustButtonList.Add(tmp);
                sumx += pointList[i].X;
                sumy += pointList[i].Y;
            }
            if (moveButton == null)
            {
                moveButton = new AdjustButton(Common.mainPicture,
                    new Point(sumx / pointList.Count - 3, sumy / pointList.Count - 3), Cursors.SizeAll);
                moveButton.BackColor = Color.Green;
                moveButton.MouseDown += MB_MouseDown;
                moveButton.MouseMove += MB_MouseMove;
                moveButton.MouseUp += MB_MouseUp;
                moveButton.setAllPoints();
            }
        }

        public override void endSelect()
        {
            base.endSelect();
            for (int i = 0; i < adjustButtonList.Count; ++i)
            {
                adjustButtonList[i].clear();
                adjustButtonList[i] = null;
            }
            adjustButtonList.Clear();
            moveButton.clear();
            moveButton = null;
        }

        public override void startResize()
        {
            base.startResize();
        }

        public override void endResize()
        {
            base.endResize();
        }

        private void AB_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isAdjust = true;
            }
        }

        private void AB_MouseMove(object sender, MouseEventArgs e)
        {
            if (isAdjust)
            {
                AdjustButton tmp = (AdjustButton)sender;
                int index = adjustButtonList.IndexOf(tmp);
                pointList[index] = new Point(tmp.Location.X + 3, tmp.Location.Y + 3);
                int sumx = 0, sumy = 0;
                foreach (Point i in pointList)
                {
                    sumx += i.X;
                    sumy += i.Y;
                }
                moveButton.Location = new Point(sumx / pointList.Count - 3, sumy / pointList.Count - 3);
            }
        }

        private void AB_MouseUp(object sender, MouseEventArgs e)
        {
            isAdjust = false;
        }

        Point moveButtonStartLocation;

        private void MB_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMove = true;
                moveButtonStartLocation = e.Location;
            }
        }

        private void MB_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMove)
            {
                int lx = e.Location.X - moveButtonStartLocation.X;
                int ly = e.Location.Y - moveButtonStartLocation.Y;
                for (int i = 0; i < pointList.Count; ++i)
                {
                    Point tmp = pointList[i];
                    pointList[i] = new Point(tmp.X + lx, tmp.Y + ly);
                    tmp = adjustButtonList[i].Location;
                    adjustButtonList[i].Location = new Point(tmp.X + lx, tmp.Y + ly);
                }
            }
        }

        private void MB_MouseUp(object sender, MouseEventArgs e)
        {
            isMove = false;
        }
    }
}

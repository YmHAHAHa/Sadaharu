using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using Sadaharu.Mybuttons;
using Sadaharu.Mylibs;
using System.Drawing.Drawing2D;

namespace Sadaharu.Shapes
{
    class Curve:Shape
    {
        public List<Point> pointList;

        List<AdjustButton> adjustButtonList;

        PointF[] beginPoints;

        AdjustButton moveButton;
        RotateButton rotateButton;

        bool isRotate;

        Point a, b, midPoint;

        public Curve(List<Point> points) : base()
        {
            pointList = points;
            adjustButtonList = new List<AdjustButton>();
            isRotate = false;
            a = new Point(300, 300);
            b = new Point(400, 400);
            beginPoints = new PointF[pointList.Count];
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

        private void drawRB2MB()
        {
            Pen pentmp = new Pen(Color.Gray, 1);
            float[] dashp = { 2f, 3f };
            pentmp.DashPattern = dashp;
            Point m = new Point(moveButton.Location.X + 3, moveButton.Location.Y + 3),
                r = new Point(rotateButton.Location.X + 3, rotateButton.Location.Y + 3);
            using (Graphics g = Graphics.FromImage(Common.mainPicture.Image))
            {
                g.DrawLine(pentmp, m, r);
                Common.mainPicture.Image = Common.mainPicture.Image;
            }
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
            }
            if (rotateButton == null)
            {
                rotateButton = new RotateButton(Common.mainPicture,
                    new Point(sumx / pointList.Count, sumy / pointList.Count));
                rotateButton.MouseDown += RB_MouseDown;
                rotateButton.MouseMove += RB_MouseMove;
                rotateButton.MouseUp += RB_MouseUp;
            }
            midPoint = rotateButton.midPoint;
            moveButton.setAllPoints(
                new Ref<Point>(() => rotateButton.Location, z => { rotateButton.Location = z; }));
            rotateButton.setAllPoints(
                new Ref<Point>(() => a, z => { a = z; }),
                new Ref<Point>(() => b, z => { b = z; }));

            drawRB2MB();
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
            rotateButton.clear();
            rotateButton = null;
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
                rotateButton.Location = new Point(sumx / pointList.Count + 77, sumy / pointList.Count - 3);
                rotateButton.midPoint = new Point(sumx / pointList.Count, sumy / pointList.Count);
                midPoint = rotateButton.midPoint;
            }
        }

        private void AB_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isAdjust = false;
                int sumx = 0, sumy = 0;
                foreach (Point i in pointList)
                {
                    sumx += i.X;
                    sumy += i.Y;
                }
                moveButton.Location = new Point(sumx / pointList.Count - 3, sumy / pointList.Count - 3);
                rotateButton.Location = new Point(sumx / pointList.Count + 77, sumy / pointList.Count - 3);
                rotateButton.midPoint = new Point(sumx / pointList.Count, sumy / pointList.Count);
                midPoint = rotateButton.midPoint;
            }
            drawRB2MB();
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

                int sumx = 0, sumy = 0;
                foreach (Point i in pointList)
                {
                    sumx += i.X;
                    sumy += i.Y;
                }
                rotateButton.midPoint = new Point(sumx / pointList.Count, sumy / pointList.Count);
                midPoint = rotateButton.midPoint;
            }
        }

        private void MB_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMove = false;

                int sumx = 0, sumy = 0;
                foreach (Point i in pointList)
                {
                    sumx += i.X;
                    sumy += i.Y;
                }
                rotateButton.midPoint = new Point(sumx / pointList.Count, sumy / pointList.Count);
                midPoint = rotateButton.midPoint;
            }
            drawRB2MB();
        }

        Point tmpLocation;

        private void RB_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isRotate = true;
                for (int i = 0; i < pointList.Count; ++i)
                {
                    beginPoints[i] = pointList[i];
                }
                tmpLocation = e.Location;
            }
        }

        private void RB_MouseMove(object sender, MouseEventArgs e)
        {
            if (isRotate)
            {
                using (var path = new GraphicsPath())
                {
                    path.AddPolygon(beginPoints);
                    Point tmp = new Point(rotateButton.Left + (e.X - tmpLocation.X),
                        rotateButton.Top + (e.Y - tmpLocation.Y));
                    double a1 = Math.Atan2(tmp.Y - midPoint.Y, tmp.X - midPoint.X);
                    var n1 = (float)Math.Cos(a1);
                    var n2 = (float)Math.Sin(a1);
                    var n3 = -(float)Math.Sin(a1);
                    var n4 = (float)Math.Cos(a1);
                    var n5 = (float)((midPoint.X * (1 - Math.Cos(a1)) + midPoint.Y * Math.Sin(a1)));
                    var n6 = (float)((midPoint.Y * (1 - Math.Cos(a1)) - midPoint.X * Math.Sin(a1)));
                    path.Transform(new Matrix(n1, n2, n3, n4, n5, n6));

                    int sumx = 0, sumy = 0;
                    for (int i = 0; i < beginPoints.Length; ++i)
                    {
                        int xx = (int)path.PathPoints[i].X,
                            yy = (int)path.PathPoints[i].Y;
                        pointList[i] = new Point(xx, yy);
                        adjustButtonList[i].Location = new Point(xx - 3, yy - 3);
                        sumx += xx;
                        sumy += yy;
                    }
                    moveButton.Location = new Point(
                        sumx / pointList.Count - 3, sumy / pointList.Count - 3);
                }
            }
        }

        private void RB_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isRotate = false;
                int sumx = 0, sumy = 0;
                for (int i = 0; i < beginPoints.Length; ++i)
                {
                    adjustButtonList[i].Location = new Point(
                        pointList[i].X - 3, pointList[i].Y - 3);
                    sumx += pointList[i].X;
                    sumy += pointList[i].Y;
                }
                moveButton.Location = new Point(
                    sumx / pointList.Count - 3, sumy / pointList.Count - 3);
            }
        }

        ResizeRectangle resizeRectangle;

        public override void startResize()
        {
            base.startResize();
            Common.history.drawWithoutNow();
            Common.history.update();
            int x1 = 100000, x2 = -1, y1 = 100000, y2 = -1;
            for (int i = 0; i < pointList.Count; ++i)
            {
                x1 = x1 < pointList[i].X ? x1 : pointList[i].X;
                y1 = y1 < pointList[i].Y ? y1 : pointList[i].Y;
                x2 = x2 > pointList[i].X ? x2 : pointList[i].X;
                y2 = y2 > pointList[i].Y ? y2 : pointList[i].Y;
            }
            Rectangle r = new Rectangle(x1, y1, x2 - x1, y2 - y1);
            resizeRectangle = new ResizeRectangle(Common.mainPicture, r);
            resizeRectangle.rbResizeButton.MouseDown += RB_MD;
            resizeRectangle.rbResizeButton.MouseMove += RB_MM;
            resizeRectangle.rbResizeButton.MouseUp += RB_MU;
        }

        public override void endResize()
        {
            base.endResize();
            resizeRectangle.clear();
            Common.history.update();
        }

        bool isResize;
        Point rbStartLocation;

        private void RB_MD(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isResize = true;
                rbStartLocation = new Point(resizeRectangle.rbResizeButton.Location.X + 3,
                    resizeRectangle.rbResizeButton.Location.Y + 3);
                for (int i = 0; i < pointList.Count; ++i)
                {
                    beginPoints[i] = pointList[i];
                }
            }
        }

        private void RB_MM(object sender, MouseEventArgs e)
        {
            if (isResize)
            {
                Point tmp = new Point(resizeRectangle.rbResizeButton.Location.X + 3,
                    resizeRectangle.rbResizeButton.Location.Y + 3);
                double oldlx = rbStartLocation.X - resizeRectangle.ltp.X,
                    oldly = rbStartLocation.Y - resizeRectangle.ltp.Y,
                    newlx = tmp.X - resizeRectangle.ltp.X,
                    newly = tmp.Y - resizeRectangle.ltp.Y;
                double px = newlx / oldlx,
                    py = newly / oldly;
                for (int i = 0; i < pointList.Count; ++i)
                {
                    double dx = beginPoints[i].X - resizeRectangle.ltp.X,
                        dy = beginPoints[i].Y - resizeRectangle.ltp.Y;
                    double newdx = dx * px,
                        newdy = dy * py;
                    int newx = resizeRectangle.ltp.X + (int)newdx,
                        newy = resizeRectangle.ltp.Y + (int)newdy;
                    pointList[i] = new Point(newx, newy);
                }
            }
        }

        private void RB_MU(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isResize = false;
            }
        }
    }
}

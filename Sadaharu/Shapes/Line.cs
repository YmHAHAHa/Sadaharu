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
    class Line:Shape
    {
        public Point a, b;

        bool isRotate;

        AdjustButton adjustButton1, adjustButton2;
        AdjustButton moveButton;
        RotateButton rotateButton;

        public Line(Point p1, Point p2) : base()
        {
            a = p1;
            b = p2;
            isRotate = false;
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
            return string.Format("the line: ({0},{1}) to ({2},{3})",
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
            if (adjustButton1 == null)
            {
                adjustButton1 = new AdjustButton(Common.mainPicture,
                    new Point(a.X - 3, a.Y - 3), Cursors.SizeNS);
                adjustButton1.MouseDown += AB_MouseDown;
                adjustButton1.MouseMove += AB_MouseMove;
                adjustButton1.MouseUp += AB_MouseUp;
            }
            if (adjustButton2 == null)
            {
                adjustButton2 = new AdjustButton(Common.mainPicture,
                    new Point(b.X - 3, b.Y - 3), Cursors.SizeNS);
                adjustButton2.MouseDown += AB_MouseDown;
                adjustButton2.MouseMove += AB_MouseMove;
                adjustButton2.MouseUp += AB_MouseUp;
            }
            if(moveButton==null)
            {
                moveButton = new AdjustButton(Common.mainPicture,
                    new Point((a.X + b.X) / 2 - 3, (a.Y + b.Y) / 2 - 3), Cursors.SizeAll);
                moveButton.BackColor = Color.Green;
                moveButton.MouseDown += MB_MouseDown;
                moveButton.MouseMove += MB_MouseMove;
                moveButton.MouseUp += MB_MouseUp;
            }
            if (rotateButton == null)
            {
                rotateButton = new RotateButton(Common.mainPicture,
                    new Point((a.X + b.X) / 2, (a.Y + b.Y) / 2));
                rotateButton.MouseDown += RB_MouseDown;
                rotateButton.MouseMove += RB_MouseMove;
                rotateButton.MouseUp += RB_MouseUp;
            }
            adjustButton1.setAllPoints(
                new Ref<Point>(() => a, z => { a = z; }));
            adjustButton2.setAllPoints(
                new Ref<Point>(() => b, z => { b = z; }));
            moveButton.setAllPoints(
                new Ref<Point>(() => a, z => { a = z; }),
                new Ref<Point>(() => b, z => { b = z; }),
                new Ref<Point>(() => adjustButton1.Location, z => { adjustButton1.Location = z; }),
                new Ref<Point>(() => adjustButton2.Location, z => { adjustButton2.Location = z; }),
                new Ref<Point>(() => rotateButton.Location, z => { rotateButton.Location = z; }));
            rotateButton.setAllPoints(
                new Ref<Point>(() => a, z => { a = z; }),
                new Ref<Point>(() => b, z => { b = z; }));


            drawRB2MB();
        }

        public override void endSelect()
        {
            base.endSelect();
            adjustButton1.clear();
            adjustButton1 = null;
            adjustButton2.clear();
            adjustButton2 = null;
            moveButton.clear();
            moveButton = null;
            rotateButton.clear();
            rotateButton = null;
        }

        public override void startResize()
        {
            base.startResize();
            this.startSelect();
        }

        public override void endResize()
        {
            base.endResize();
            this.endSelect();
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
                moveButton.Location = new Point((a.X + b.X) / 2 - 3, (a.Y + b.Y) / 2 - 3);
                rotateButton.Location = new Point((a.X + b.X) / 2 + 77, (a.Y + b.Y) / 2 - 3);
                rotateButton.midPoint = new Point((a.X + b.X) / 2, (a.Y + b.Y) / 2);
            }
        }

        private void AB_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isAdjust = false;
                moveButton.Location = new Point((a.X + b.X) / 2 - 3, (a.Y + b.Y) / 2 - 3);
                rotateButton.Location = new Point((a.X + b.X) / 2 + 77, (a.Y + b.Y) / 2 - 3);
                rotateButton.midPoint = new Point((a.X + b.X) / 2, (a.Y + b.Y) / 2);
            }
            drawRB2MB();
        }

        private void MB_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMove = true;
            }
        }

        private void MB_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMove)
            {
                rotateButton.midPoint = new Point((a.X + b.X) / 2, (a.Y + b.Y) / 2);
            }
        }

        private void MB_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMove = false;
                rotateButton.midPoint = new Point((a.X + b.X) / 2, (a.Y + b.Y) / 2);
            }
            drawRB2MB();
        }

        private void RB_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isRotate = true;
            }
        }

        private void RB_MouseMove(object sender, MouseEventArgs e)
        {
            if (isRotate)
            {
                //rotateButton.midPoint = new Point((a.X + b.X) / 2, (a.Y + b.Y) / 2);
                adjustButton1.Location = new Point(a.X - 3, a.Y - 3);
                adjustButton2.Location = new Point(b.X - 3, b.Y - 3);
                moveButton.Location = new Point((a.X + b.X) / 2 - 3, (a.Y + b.Y) / 2 - 3);
            }
        }

        private void RB_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isRotate = false;
                adjustButton1.Location = new Point(a.X - 3, a.Y - 3);
                adjustButton2.Location = new Point(b.X - 3, b.Y - 3);
                moveButton.Location = new Point((a.X + b.X) / 2 - 3, (a.Y + b.Y) / 2 - 3);
            }
            //drawRB2MB();
        }
    }
}

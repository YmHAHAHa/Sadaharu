﻿using System;
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

        private void drawSelectRect()
        {
            Pen pentmp = new Pen(Color.Gray, 1);
            float[] dashp = { 2f, 3f };
            pentmp.DashPattern = dashp;
            using (Graphics g = Graphics.FromImage(Common.mainPicture.Image))
            {
                g.DrawLine(pentmp, a, b);
                g.DrawLine(pentmp, a, d);
                g.DrawLine(pentmp, c, b);
                g.DrawLine(pentmp, c, d);
            }
        }

        public override void startSelect()
        {
            base.startSelect();

            //drawSelectRect();

            if (adjustButton1 == null)
            {
                adjustButton1 = new RectAdjustButton(Common.mainPicture,
                    new Point(a.X - 3, a.Y - 3), Cursors.SizeNS);
                adjustButton1.MouseDown += AB_MouseDown;
                adjustButton1.MouseMove += AB_MouseMove;
                adjustButton1.MouseUp += AB_MouseUp;
            }
            if (adjustButton2 == null)
            {
                adjustButton2 = new RectAdjustButton(Common.mainPicture,
                    new Point(b.X - 3, b.Y - 3), Cursors.SizeNS);
                adjustButton2.MouseDown += AB_MouseDown;
                adjustButton2.MouseMove += AB_MouseMove;
                adjustButton2.MouseUp += AB_MouseUp;
            }
            if (adjustButton3 == null)
            {
                adjustButton3 = new RectAdjustButton(Common.mainPicture,
                    new Point(c.X - 3, c.Y - 3), Cursors.SizeNS);
                adjustButton3.MouseDown += AB_MouseDown;
                adjustButton3.MouseMove += AB_MouseMove;
                adjustButton3.MouseUp += AB_MouseUp;
            }
            if (adjustButton4 == null)
            {
                adjustButton4 = new RectAdjustButton(Common.mainPicture,
                    new Point(d.X - 3, d.Y - 3), Cursors.SizeNS);
                adjustButton4.MouseDown += AB_MouseDown;
                adjustButton4.MouseMove += AB_MouseMove;
                adjustButton4.MouseUp += AB_MouseUp;
            }
            if (moveButton == null)
            {
                moveButton = new AdjustButton(Common.mainPicture,
                    new Point((a.X + c.X) / 2 - 3, (a.Y + c.Y) / 2 - 3), Cursors.SizeAll);
                moveButton.BackColor = Color.Green;
                moveButton.MouseUp += MB_MouseUp;
                moveButton.MouseDown += MB_MouseDown;
                moveButton.MouseMove += MB_MouseMove;
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
            moveButton.setAllPoints(
                new Ref<Point>(() => a, z => { a = z; }),
                new Ref<Point>(() => b, z => { b = z; }),
                new Ref<Point>(() => c, z => { c = z; }),
                new Ref<Point>(() => d, z => { d = z; }),
                new Ref<Point>(() => adjustButton1.Location, z => { adjustButton1.Location = z; }),
                new Ref<Point>(() => adjustButton2.Location, z => { adjustButton2.Location = z; }),
                new Ref<Point>(() => adjustButton3.Location, z => { adjustButton3.Location = z; }),
                new Ref<Point>(() => adjustButton4.Location, z => { adjustButton4.Location = z; }));
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
            moveButton.clear();
            moveButton = null;
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
                moveButton.Location = new Point((a.X + c.X) / 2 - 3, (a.Y + c.Y) / 2 - 3);
            }
        }

        private void AB_MouseUp(object sender, MouseEventArgs e)
        {
            isAdjust = false;
            midPoint = new Point((a.X + c.X) / 2, (a.Y + c.Y) / 2);
            //drawSelectRect();
            //Common.mainPicture = Common.mainPicture;
            //drawSelectRect();
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
            }
        }

        private void MB_MouseUp(object sender, MouseEventArgs e)
        {
            midPoint = new Point((a.X + c.X) / 2, (a.Y + c.Y) / 2);
            //drawSelectRect();
            //Common.mainPicture = Common.mainPicture;
            //drawSelectRect();
        }
    }
}

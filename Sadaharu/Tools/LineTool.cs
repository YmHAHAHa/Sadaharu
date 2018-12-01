using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using Sadaharu.Shapes;

namespace Sadaharu.Tools
{
    interface DrawLineMethod
    {
        void drawLine(Graphics g, Pen pen, Point p1, Point p2);
    }

    class LineTool:Tool
    {
        Point startPoint;

        DrawLineMethod method;

        public LineTool(MainWin window, PictureBox pic) : base(window, pic)
        {
            //this.method = new Bresenham();
            this.method = new SystemDrawLine();
        }

        public override void reDraw(Shape s, Graphics g)
        {
            Line line = (Line)s;
            draw(g, line.drawPen, line.a, line.b);
        }

        public override void startUseTool()
        {
            base.startUseTool();
            mainPicture.Cursor = Cursors.Cross;
            mainPicture.MouseDown += MainPicture_MouseDown;
            mainPicture.MouseMove += MainPicture_MouseMove;
            mainPicture.MouseUp += MainPicture_MouseUp;
        }

        public override void endUseTool()
        {
            base.endUseTool();
            mainPicture.Cursor = Cursors.Default;
            mainPicture.MouseDown -= MainPicture_MouseDown;
            mainPicture.MouseMove -= MainPicture_MouseMove;
            mainPicture.MouseUp -= MainPicture_MouseUp;
        }

        private void MainPicture_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                startPoint = e.Location;
                isEnabled = true;
                imageTmp = (Image)mainPicture.Image.Clone();
            }
        }

        private void MainPicture_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isEnabled) return;
            mainPicture.Image.Dispose();
            mainPicture.Image = (Image)imageTmp.Clone();
            using (Graphics g = Graphics.FromImage(mainPicture.Image))
            {
                draw(g, Common.setting.nowPen, startPoint, e.Location);
            }
        }

        private void MainPicture_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isEnabled = false;
                imageTmp.Dispose();
                mainWindow.cmdPrint(string.Format("Line: ({0},{1}) to ({2},{3})",
                    startPoint.X, startPoint.Y, e.Location.X, e.Location.Y));
                /*
                 * save a line here
                 */
                Line line = new Line(startPoint, e.Location);
                Common.history.PushRecord(new Record(line, this));
            }
        }

        public void draw(Graphics g, Pen pen, Point p1, Point p2)
        {
            this.method.drawLine(g, pen, p1, p2);
        }
    }

    class SystemDrawLine:DrawLineMethod
    {
        public void drawLine(Graphics g, Pen pen, Point p1, Point p2)
        {
            g.DrawLine(pen, p1, p2);
        }
    }

    class Bresenham:DrawLineMethod
    {
        private void bresenham(Graphics g, Pen pen, Point p1, Point p2)
        {
            int x1 = p1.X, y1 = p1.Y, x2 = p2.X, y2 = p2.Y;
            int dx = Math.Abs(x2 - x1), sx = x1 < x2 ? 1 : -1;
            int dy = Math.Abs(y2 - y1), sy = y1 < y2 ? 1 : -1;
            int err = (dx > dy ? dx : -dy) / 2, e2;
            while(true)
            {
                g.DrawRectangle(pen, x1, y1, 1, 1);
                if (x1 == x2 && y1 == y2) break;
                e2 = err;
                if (e2 > -dx) { err -= dy; x1 += sx; }
                if (e2 < dy) { err += dx; y1 += sy; }
            }
        }

        public void drawLine(Graphics g, Pen pen, Point p1, Point p2)
        {
            bresenham(g, pen, p1, p2);
        }
    }
}

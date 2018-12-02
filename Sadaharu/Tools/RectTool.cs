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
    class RectTool:Tool
    {
        Point startPoint;
        DrawRectMethod method;

        public RectTool(MainWin window, PictureBox pic) : base(window, pic)
        {
            this.method = new SystemDrawRect();
            //this.method = new MyDrawRect();
        }

        public override void reDraw(Shape s, Graphics g)
        {
            Rect rect = (Rect)s;
            draw(g, rect.drawPen, rect.a, rect.c);
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
                mainWindow.cmdPrint(string.Format("Rectangle: ({0},{1}), ({2},{3}), ({4},{5}), ({6},{7})",
                    startPoint.X, startPoint.Y, startPoint.X, e.Location.Y,
                    e.Location.X, e.Location.Y, e.Location.X, startPoint.Y));
                /*
                 * save a Rect here
                 */
                Rect rect = new Rect(startPoint, e.Location);
                Common.history.PushRecord(new Record(rect, this));
            }
        }

        public void draw(Graphics g, Pen pen, Point p1, Point p2)
        {
            this.method.drawRect(g, pen, p1, p2);
        }
    }

    interface DrawRectMethod
    {
        void drawRect(Graphics g, Pen pen, Point p1, Point p2);
    }

    class SystemDrawRect:DrawRectMethod
    {
        public void drawRect(Graphics g, Pen pen, Point p1, Point p2)
        {
            int x = p1.X < p2.X ? p1.X : p2.X;
            int y = p1.Y < p2.Y ? p1.Y : p2.Y;
            int lx = p1.X + p2.X - x - x;
            int ly = p1.Y + p2.Y - y - y;
            if (Common.setting.nowColor != Color.White)
                g.FillRectangle(new SolidBrush(Common.setting.nowColor), x, y, lx, ly);
            g.DrawRectangle(pen, x, y, lx, ly);
        }
    }

    class MyDrawRect:DrawRectMethod
    {
        public void drawRect(Graphics g, Pen pen, Point p1, Point p2)
        {
            Point p3 = new Point(p1.X, p2.Y);
            Point p4 = new Point(p2.X, p1.Y);
            Common.drawtools.lineTool.draw(g, pen, p1, p3);
            Common.drawtools.lineTool.draw(g, pen, p1, p4);
            Common.drawtools.lineTool.draw(g, pen, p2, p3);
            Common.drawtools.lineTool.draw(g, pen, p2, p4);
        }
    }
}

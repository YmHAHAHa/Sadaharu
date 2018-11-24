using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Sadaharu.Tools
{
    class CircleTool:Tool
    {
        Point startPoint;

        DrawCircleMethod method;

        public CircleTool(MainWin window, PictureBox pic) : base(window, pic)
        {
            this.method = new SystemDrawCircle();
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
                mainWindow.cmdPrint(string.Format("Circle: ({0},{1}) to ({2},{3})",
                    startPoint.X, startPoint.Y, e.Location.X, e.Location.Y));
                /*
                 * save a Circle here
                 */
            }
        }

        public void draw(Graphics g, Pen pen, Point p1, Point p2)
        {
            this.method.drawCircle(g, pen, p1, p2);
        }
    }

    interface DrawCircleMethod
    {
        void drawCircle(Graphics g, Pen pen, Point p1, Point p2);
    }

    class SystemDrawCircle : DrawCircleMethod
    {
        public void drawCircle(Graphics g, Pen pen, Point p1, Point p2)
        {
            int x = p1.X < p2.X ? p1.X : p2.X;
            int y = p1.Y < p2.Y ? p1.Y : p2.Y;
            int lx = p1.X + p2.X - x - x;
            int ly = p1.Y + p2.Y - y - y;
            g.DrawEllipse(pen, x, y, lx, ly);
            if (Common.setting.nowColor != Color.White)
                g.FillEllipse(new SolidBrush(Common.setting.nowColor),
                    x + 1, y + 1, lx - 2, ly - 2);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using Sadaharu.Mylibs;

namespace Sadaharu.Mybuttons
{
    class ResizeRectangle
    {
        PictureBox mainPicture;

        Rectangle rectangle;

        public ResizeButton rbResizeButton;

        Pen pen;

        public Point ltp;

        bool isResize;

        public ResizeRectangle(PictureBox pic,Rectangle rec)
        {
            mainPicture = pic;
            rectangle = rec;
            isResize = false;
            ltp = new Point(rec.Left, rec.Top);
            pen = new Pen(Color.Gray, 1);
            float[] dashp = { 2f, 3f };
            pen.DashPattern = dashp;
            rbResizeButton = new ResizeButton(pic, new Point(rec.Right - 3, rec.Bottom - 3), Cursors.SizeNS);
            rbResizeButton.MouseDown += RB_MD;
            rbResizeButton.MouseMove += RB_MM;
            rbResizeButton.MouseUp += RB_MU;
            rbResizeButton.Show();
            using (Graphics g = Graphics.FromImage(mainPicture.Image))
            {
                g.DrawRectangle(pen, rectangle);
            }
        }

        public void clear()
        {
            rbResizeButton.clear();
        }

        private void RB_MD(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isResize = true;
            }
        }

        private void RB_MM(object sender, MouseEventArgs e)
        {
            if (isResize)
            {
                Common.history.update();
                int minx = Math.Min(ltp.X, rbResizeButton.Location.X + 3),
                    miny = Math.Min(ltp.Y, rbResizeButton.Location.Y + 3);
                int lx = ltp.X + rbResizeButton.Location.X + 3 - minx - minx,
                    ly = ltp.Y + rbResizeButton.Location.Y + 3 - miny - miny;
                rectangle = new Rectangle(minx, miny, lx, ly);
                using (Graphics g = Graphics.FromImage(mainPicture.Image))
                {
                    g.DrawRectangle(pen, rectangle);
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

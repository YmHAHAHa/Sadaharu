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
    class CutTool:Tool
    {
        Point startPoint;

        Pen pentmp;

        public CutTool(MainWin window, PictureBox pic) : base(window, pic)
        {
            pentmp = new Pen(Color.Gray, 1);
            float[] dashp = { 2f, 3f };
            pentmp.DashPattern = dashp;
        }

        public override void reDraw(Shape s, Graphics g)
        {
            return;
        }

        public override void startUseTool()
        {
            base.startUseTool();
            mainPicture.Cursor = Cursors.Cross;
            mainPicture.MouseDown += C_MD;
            mainPicture.MouseMove += C_MM;
            mainPicture.MouseUp += C_MU;
        }

        public override void endUseTool()
        {
            base.endUseTool();
            mainPicture.Cursor = Cursors.Default;
            mainPicture.MouseDown -= C_MD;
            mainPicture.MouseMove -= C_MM;
            mainPicture.MouseUp -= C_MU;
        }

        private void C_MD(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isEnabled = true;
                startPoint = e.Location;
                imageTmp = (Image)mainPicture.Image.Clone();
            }
        }

        private void C_MM(object sender, MouseEventArgs e)
        {
            if (isEnabled)
            {
                mainPicture.Image.Dispose();
                mainPicture.Image = (Image)imageTmp.Clone();
                using (Graphics g = Graphics.FromImage(mainPicture.Image))
                {
                    //draw(g, Common.setting.nowPen, startPoint, e.Location);
                    int x = Math.Min(startPoint.X, e.Location.X),
                        y = Math.Min(startPoint.Y, e.Location.Y);
                    int lx = startPoint.X + e.Location.X - x - x,
                        ly = startPoint.Y + e.Location.Y - y - y;
                    g.DrawRectangle(pentmp, x, y, lx, ly);
                }
            }
        }

        private void C_MU(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isEnabled = false;
                imageTmp.Dispose();
                int x = Math.Min(startPoint.X, e.Location.X),
                        y = Math.Min(startPoint.Y, e.Location.Y);
                int lx = startPoint.X + e.Location.X - x - x,
                    ly = startPoint.Y + e.Location.Y - y - y;
                Rectangle rect = new Rectangle(x, y, lx, ly);
                Common.history.CutAllShape(rect);
            }
        }
    }
}

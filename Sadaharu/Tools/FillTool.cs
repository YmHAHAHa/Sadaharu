using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Sadaharu.Tools
{
    class FillTool : Tool
    {
        static Queue<Point> pointQueue = new Queue<Point>(5000);

        public FillTool(MainWin window, PictureBox pic) : base(window, pic)
        {
            //pointQueue = new Queue<Point>(750000);
        }

        public override void startUseTool()
        {
            base.startUseTool();
            mainPicture.Cursor = Cursors.Hand;
            mainPicture.MouseClick += MainPicture_MouseClick;
        }

        public override void endUseTool()
        {
            base.endUseTool();
            mainPicture.Cursor = Cursors.Default;
            mainPicture.MouseClick -= MainPicture_MouseClick;
        }

        private void MainPicture_MouseClick(object sender, MouseEventArgs e)
        {
            floodfill(e.Location, Common.setting.nowColor);
            mainWindow.cmdPrint(string.Format("Fill an region with {0}", Common.setting.nowColor));
            /*
             * save a floodfill here
             */
        }

        public void floodfill(Point p, Color backcolor)
        {
            Bitmap bitmap = new Bitmap(mainPicture.Image);
            Color nowcol = bitmap.GetPixel(p.X, p.Y);
            if (nowcol == backcolor) return;
            GraphicsUnit units = GraphicsUnit.Pixel;
            RectangleF boundary = bitmap.GetBounds(ref units);
            pointQueue.Enqueue(p);
            while (pointQueue.Count > 0)
            {
                Point tmp = pointQueue.Dequeue();
                if (!boundary.Contains(tmp)) continue;
                if (bitmap.GetPixel(tmp.X, tmp.Y) == nowcol)
                {
                    bitmap.SetPixel(tmp.X, tmp.Y, backcolor);
                    pointQueue.Enqueue(new Point(tmp.X, tmp.Y - 1));
                    pointQueue.Enqueue(new Point(tmp.X, tmp.Y + 1));
                    pointQueue.Enqueue(new Point(tmp.X - 1, tmp.Y));
                    pointQueue.Enqueue(new Point(tmp.X + 1, tmp.Y));
                }
            }
            mainPicture.Image.Dispose();
            mainPicture.Image = bitmap;
            pointQueue.Clear();
        }
    }
}

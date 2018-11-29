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
        static bool[] isView = new bool[533 * 835];

        public FillTool(MainWin window, PictureBox pic) : base(window, pic)
        {
            //pointQueue = new Queue<Point>(750000);
            isView.Initialize();
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
            /*Bitmap bitmap = new Bitmap(mainPicture.Image);
            Color nowcol = bitmap.GetPixel(p.X, p.Y);
            if (nowcol.ToArgb() == backcolor.ToArgb()) return;
            GraphicsUnit units = GraphicsUnit.Pixel;
            RectangleF boundary = bitmap.GetBounds(ref units);
            pointQueue.Enqueue(p);
            while (pointQueue.Count > 0)
            {
                Point tmp = pointQueue.Dequeue();
                if (!boundary.Contains(tmp)) continue;
                if (bitmap.GetPixel(tmp.X, tmp.Y) == nowcol)
                {
                    //bitmap.SetPixel(tmp.X, tmp.Y, backcolor);
                    //pointQueue.Enqueue(new Point(tmp.X, tmp.Y - 1));
                    //pointQueue.Enqueue(new Point(tmp.X, tmp.Y + 1));
                    //pointQueue.Enqueue(new Point(tmp.X - 1, tmp.Y));
                    //pointQueue.Enqueue(new Point(tmp.X + 1, tmp.Y));
                    bitmap.SetPixel(tmp.X, tmp.Y, backcolor);
                    if (tmp.X == 0 || tmp.Y == 0 || tmp.X == 834 || tmp.Y == 532) continue;
                    if (bitmap.GetPixel(tmp.X, tmp.Y - 1) == nowcol)
                        pointQueue.Enqueue(new Point(tmp.X, tmp.Y - 1));
                    if (bitmap.GetPixel(tmp.X, tmp.Y + 1) == nowcol)
                        pointQueue.Enqueue(new Point(tmp.X, tmp.Y + 1));
                    if (bitmap.GetPixel(tmp.X - 1, tmp.Y) == nowcol)
                        pointQueue.Enqueue(new Point(tmp.X - 1, tmp.Y));
                    if (bitmap.GetPixel(tmp.X + 1, tmp.Y) == nowcol)
                        pointQueue.Enqueue(new Point(tmp.X + 1, tmp.Y));
                }
            }
            mainPicture.Image.Dispose();
            mainPicture.Image = bitmap;
            pointQueue.Clear();*/



            Bitmap bitmap = new Bitmap(mainPicture.Image);
            Color nowcol = bitmap.GetPixel(p.X, p.Y);
            if (nowcol.ToArgb() == backcolor.ToArgb()) return;
            for(int i = 0; i < 835; ++i)
            {
                for(int j = 0; j < 533; ++j)
                {
                    if (nowcol.ToArgb() == bitmap.GetPixel(i, j).ToArgb())
                        isView[835 * j + i] = true;
                }
            }
            GraphicsUnit units = GraphicsUnit.Pixel;
            RectangleF boundary = bitmap.GetBounds(ref units);
            pointQueue.Enqueue(p);
            isView[835 * p.Y + p.X] = false;
            while (pointQueue.Count > 0)
            {
                Point tmp = pointQueue.Dequeue();
                if (!boundary.Contains(tmp)) continue;

                //if (isView[835 * tmp.Y + tmp.X])
                //{
                //    bitmap.SetPixel(tmp.X, tmp.Y, backcolor);
                //    isView[835 * tmp.Y + tmp.X] = false;
                //    if (tmp.X == 0 || tmp.Y == 0 || tmp.X == 834 || tmp.Y == 532) continue;
                //    if (isView[835 * (tmp.Y - 1) + tmp.X])
                //        pointQueue.Enqueue(new Point(tmp.X, tmp.Y - 1));
                //    if (isView[835 * (tmp.Y + 1) + tmp.X])
                //        pointQueue.Enqueue(new Point(tmp.X, tmp.Y + 1));
                //    if (isView[835 * tmp.Y + tmp.X - 1]) 
                //        pointQueue.Enqueue(new Point(tmp.X - 1, tmp.Y));
                //    if (isView[835 * tmp.Y + tmp.X + 1])
                //        pointQueue.Enqueue(new Point(tmp.X + 1, tmp.Y));
                //}

                bitmap.SetPixel(tmp.X, tmp.Y, backcolor);
                if (tmp.X == 0 || tmp.Y == 0 || tmp.X == 834 || tmp.Y == 532) continue;
                if (isView[835 * (tmp.Y - 1) + tmp.X])
                    pointQueue.Enqueue(new Point(tmp.X, tmp.Y - 1));
                if (isView[835 * (tmp.Y + 1) + tmp.X])
                    pointQueue.Enqueue(new Point(tmp.X, tmp.Y + 1));
                if (isView[835 * tmp.Y + tmp.X - 1])
                    pointQueue.Enqueue(new Point(tmp.X - 1, tmp.Y));
                if (isView[835 * tmp.Y + tmp.X + 1])
                    pointQueue.Enqueue(new Point(tmp.X + 1, tmp.Y));
                isView[835 * (tmp.Y - 1) + tmp.X] = isView[835 * (tmp.Y + 1) + tmp.X] =
                    isView[835 * tmp.Y + tmp.X - 1] = isView[835 * tmp.Y + tmp.X + 1] = false;
            }
            mainPicture.Image.Dispose();
            mainPicture.Image = bitmap;
            pointQueue.Clear();
            //isView.SetValue(false, 0, 835 * 533 - 1);
            for (int i = 0; i < isView.Length; ++i)
                isView[i] = false;
        }
    }
}

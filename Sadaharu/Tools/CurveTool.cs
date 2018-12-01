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
    class CurveTool:Tool
    {
        List<Point> pointList;
        DrawCurveMethod method;

        public CurveTool(MainWin window, PictureBox pic) : base(window, pic)
        {
            pointList = new List<Point>();
            method = new SystemDrawCurve();
        }

        public override void reDraw(Shape s)
        {
            //throw new NotImplementedException();
        }

        public override void startUseTool()
        {
            base.startUseTool();
            mainPicture.Cursor = Cursors.Cross;
            mainPicture.MouseClick += MainPicture_MouseClick;
            mainPicture.MouseMove += MainPicture_MouseMove;
        }

        public override void endUseTool()
        {
            base.endUseTool();
            mainPicture.Cursor = Cursors.Default;
            mainPicture.MouseClick -= MainPicture_MouseClick;
            mainPicture.MouseMove -= MainPicture_MouseMove;
        }

        private void MainPicture_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isEnabled = true;
                pointList.Add(e.Location);
                if (pointList.Count == 1)
                {
                    imageTmp = (Image)mainPicture.Image.Clone();
                    return;
                }
                /*using (Graphics g = Graphics.FromImage(mainPicture.Image))
                {
                    draw(g, Common.setting.nowPen, pointList);
                }*/
            }
            else
            {
                if (!isEnabled) return;
                pointList.Add(e.Location);
                isEnabled = false;
                imageTmp.Dispose();
                string tmp = "Curve: ";
                foreach (Point i in pointList) tmp += string.Format("({0},{1}),", i.X, i.Y);
                mainWindow.cmdPrint(tmp);
                /*
                 * save a Curve here
                 */
                pointList.Clear();
            }
        }

        private void MainPicture_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isEnabled) return;
            mainPicture.Image.Dispose();
            mainPicture.Image = (Image)imageTmp.Clone();
            pointList.Add(e.Location);
            using (Graphics g = Graphics.FromImage(mainPicture.Image))
            {
                draw(g, Common.setting.nowPen, pointList);
            }
            pointList.RemoveAt(pointList.Count - 1);
        }

        public void draw(Graphics g, Pen pen, List<Point> pList)
        {
            this.method.drawCurve(g, pen, pList);
        }
    }

    interface DrawCurveMethod
    {
        void drawCurve(Graphics g, Pen pen, List<Point> pList);
    }

    class SystemDrawCurve:DrawCurveMethod
    {
        public void drawCurve(Graphics g, Pen pen, List<Point> pList)
        {
            g.DrawCurve(pen, pList.ToArray());
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Sadaharu.Tools
{
    class PolyTool:Tool
    {
        List<Point> pointList;

        public PolyTool(MainWin window, PictureBox pic) : base(window, pic)
        {
            pointList = new List<Point>();
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
                //imageTmp = (Image)mainPicture.Image.Clone();
                if (pointList.Count == 1)
                {
                    imageTmp = (Image)mainPicture.Image.Clone();
                    return;
                }
                using (Graphics g = Graphics.FromImage(imageTmp))
                {
                    Common.drawtools.lineTool.draw(g, Common.setting.nowPen,
                        e.Location, pointList[pointList.Count - 2]);
                }
            }
            else
            {
                if (!isEnabled) return;
                pointList.Add(e.Location);
                isEnabled = false;
                imageTmp.Dispose();
                string tmp = "Polygon: ";
                foreach (Point i in pointList) tmp += string.Format("({0},{1}),", i.X, i.Y);
                mainWindow.cmdPrint(tmp);
                /*
                 * save a Poly here
                 */
                pointList.Clear();
            }
        }

        private void MainPicture_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isEnabled) return;
            mainPicture.Image.Dispose();
            mainPicture.Image = (Image)imageTmp.Clone();
            using (Graphics g = Graphics.FromImage(mainPicture.Image))
            {
                Common.drawtools.lineTool.draw(g, Common.setting.nowPen,
                    e.Location, pointList[pointList.Count - 1]);
                Common.drawtools.lineTool.draw(g, Common.setting.nowPen,
                    e.Location, pointList[0]);
            }
        }
    }
}
using System;
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
            mainPicture.Cursor = Cursors.Cross;
            mainPicture.MouseClick -= MainPicture_MouseClick;
            mainPicture.MouseMove -= MainPicture_MouseMove;
        }

        private void MainPicture_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void MainPicture_MouseMove(object sender, MouseEventArgs e)
        {

        }
    }
}

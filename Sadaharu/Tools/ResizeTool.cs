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
    class ResizeTool:Tool
    {
        public ResizeTool(MainWin window, PictureBox pic) : base(window, pic)
        {

        }

        public override void reDraw(Shape s, Graphics g)
        {
            return;
        }

        public override void startUseTool()
        {
            base.startUseTool();
            mainPicture.Cursor = Cursors.Default;
            mainPicture.MouseClick += MainPicture_MouseClick;

            mainWindow.buttonClear.Click -= mainWindow.buttonClear_Click;
            mainWindow.buttonClear.Click += Resize_buttonClear_Click;
        }

        public override void endUseTool()
        {
            base.endUseTool();
            mainPicture.MouseClick -= MainPicture_MouseClick;

            mainWindow.buttonClear.Click -= Resize_buttonClear_Click;
            mainWindow.buttonClear.Click += mainWindow.buttonClear_Click;

            if (Common.history.nowSelect != null)
            {
                Common.history.nowSelect.shape.endResize();
                Common.history.nowSelect = null;
            }
        }

        private void Resize_buttonClear_Click(object sender, EventArgs e)
        {
            if (Common.history.nowSelect != null)
            {
                Common.history.nowSelect.shape.endResize();
                Common.history.nowSelect = null;
            }
            mainWindow.clearAll();
            Common.history.clearList();
            mainWindow.cmdPrint("Clear the canvas");
        }

        private void MainPicture_MouseClick(object sender, MouseEventArgs e)
        {
            /*
             * select a shape here
             */
            if (e.Button == MouseButtons.Left)
            {
                Common.history.SearchShape_Resize(e.Location);
            }
        }
    }
}

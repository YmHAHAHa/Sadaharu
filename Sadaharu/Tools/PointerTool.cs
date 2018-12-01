using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sadaharu.Shapes;

namespace Sadaharu.Tools
{
    class PointerTool:Tool
    {
        public PointerTool(MainWin window, PictureBox pic) : base(window, pic)
        {

        }

        public override void reDraw(Shape s)
        {
            return;
        }

        public override void startUseTool()
        {
            base.startUseTool();
            mainPicture.Cursor = Cursors.Default;
            mainPicture.MouseClick += MainPicture_MouseClick;
            mainWindow.buttonClear.Click -= mainWindow.buttonClear_Click;
            mainWindow.buttonClear.Click += Pointer_buttonClear_Click;
        }

        public override void endUseTool()
        {
            base.endUseTool();
            mainPicture.MouseClick -= MainPicture_MouseClick;
            mainWindow.buttonClear.Click -= Pointer_buttonClear_Click;
            mainWindow.buttonClear.Click += mainWindow.buttonClear_Click;
            if (Common.history.nowSelect != null)
            {
                Common.history.nowSelect.shape.endSelect();
                Common.history.nowSelect = null;
            }
        }

        private void Pointer_buttonClear_Click(object sender, EventArgs e)
        {
            //mainWindow.cmdPrint("Unfinished");
            Common.history.delNowSelect();
        }

        private void MainPicture_MouseClick(object sender, MouseEventArgs e)
        {
            /*
             * select a shape here
             */
            Common.history.SearchShape(e.Location);
        }
    }
}

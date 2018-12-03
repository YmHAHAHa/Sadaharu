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
    class PointerTool:Tool
    {
        public PointerTool(MainWin window, PictureBox pic) : base(window, pic)
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
            mainWindow.buttonClear.Click += Pointer_buttonClear_Click;

            mainWindow.buttonColor.Click -= mainWindow.buttonColor_Click;
            mainWindow.buttonColor.Click += Pointer_buttonColor_Click;

            mainWindow.buttonBackColor.Click -= mainWindow.buttonBackColor_Click;
            mainWindow.buttonBackColor.Click += Pointer_buttonBackColor_Click;
        }

        public override void endUseTool()
        {
            base.endUseTool();
            mainPicture.MouseClick -= MainPicture_MouseClick;

            mainWindow.buttonClear.Click -= Pointer_buttonClear_Click;
            mainWindow.buttonClear.Click += mainWindow.buttonClear_Click;

            mainWindow.buttonColor.Click -= Pointer_buttonColor_Click;
            mainWindow.buttonColor.Click += mainWindow.buttonColor_Click;

            mainWindow.buttonBackColor.Click -= Pointer_buttonBackColor_Click;
            mainWindow.buttonBackColor.Click += mainWindow.buttonBackColor_Click;

            if (Common.history.nowSelect != null)
            {
                Common.history.nowSelect.shape.endSelect();
                Common.history.nowSelect = null;
            }
        }

        private void Pointer_buttonColor_Click(object sender, EventArgs e)
        {
            if (mainWindow.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                mainWindow.buttonColor.BackColor = mainWindow.colorDialog1.Color;
                Common.setting.nowPen.Color = mainWindow.colorDialog1.Color;
                if (Common.history.nowSelect != null)
                {
                    Common.history.nowSelect.shape.drawPen.Color = mainWindow.colorDialog1.Color;
                    Common.history.update();
                }
            }
        }

        private void Pointer_buttonBackColor_Click(object sender, EventArgs e)
        {
            if (mainWindow.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                mainWindow.buttonBackColor.BackColor = mainWindow.colorDialog1.Color;
                Common.setting.nowColor = mainWindow.colorDialog1.Color;
                if (Common.history.nowSelect != null)
                {
                    Common.history.nowSelect.shape.backColor = mainWindow.colorDialog1.Color;
                    Common.history.update();
                }
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
            if (e.Button == MouseButtons.Left)
            {
                Common.history.SearchShape(e.Location);
            }
        }
    }
}

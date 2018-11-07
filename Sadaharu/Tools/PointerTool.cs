using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sadaharu.Tools
{
    class PointerTool:Tool
    {
        public PointerTool(MainWin window, PictureBox pic) : base(window, pic)
        {

        }

        public override void startUseTool()
        {
            base.startUseTool();
            mainPicture.Cursor = Cursors.Default;
            mainPicture.MouseClick += MainPicture_MouseClick;
        }

        public override void endUseTool()
        {
            base.endUseTool();
            mainPicture.MouseClick -= MainPicture_MouseClick;
        }

        private void MainPicture_MouseClick(object sender, MouseEventArgs e)
        {
            /*
             * select a shape here
             */
        }
    }
}

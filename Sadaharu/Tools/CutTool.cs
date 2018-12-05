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
        public CutTool(MainWin window, PictureBox pic) : base(window, pic)
        {

        }

        public override void reDraw(Shape s, Graphics g)
        {
            return;
        }

        public override void startUseTool()
        {
            base.startUseTool();
        }

        public override void endUseTool()
        {
            base.endUseTool();
        }
    }
}

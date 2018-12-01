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
    abstract class Tool
    {
        protected MainWin mainWindow;

        protected PictureBox mainPicture;

        protected bool isEnabled;

        protected Image imageTmp;

        public Tool(MainWin window, PictureBox pic)
        {
            this.mainWindow = window;
            this.mainPicture = pic;
            this.isEnabled = false;
        }

        public abstract void reDraw(Shape s);

        virtual public void startUseTool()
        {

        }

        virtual public void endUseTool()
        {

        }
    }
}

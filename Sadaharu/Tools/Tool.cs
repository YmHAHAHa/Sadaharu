using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

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

        virtual public void startUseTool()
        {

        }

        virtual public void endUseTool()
        {

        }
    }
}

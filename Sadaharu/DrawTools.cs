using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sadaharu.Tools;

namespace Sadaharu
{
    class DrawTools
    {
        PictureBox mainPicture;

        MainWin mainWindow;

        public Tool nowTool;

        public PointerTool pointerTool;

        public LineTool lineTool;

        public RectTool rectTool;

        public DrawTools(MainWin window, PictureBox pic)
        {
            this.mainWindow = window;
            this.mainPicture = pic;
            this.pointerTool = new PointerTool(mainWindow, mainPicture);
            this.lineTool = new LineTool(mainWindow, mainPicture);
            this.rectTool = new RectTool(mainWindow, mainPicture);
        }
    }
}

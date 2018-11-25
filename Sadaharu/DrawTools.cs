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

        public PolyTool polyTool;

        public CircleTool circleTool;

        public CurveTool curveTool;

        public FillTool fillTool;

        public DrawTools(MainWin window, PictureBox pic)
        {
            this.mainWindow = window;
            this.mainPicture = pic;
            this.pointerTool = new PointerTool(mainWindow, mainPicture);
            this.lineTool = new LineTool(mainWindow, mainPicture);
            this.rectTool = new RectTool(mainWindow, mainPicture);
            this.polyTool = new PolyTool(mainWindow, mainPicture);
            this.circleTool = new CircleTool(mainWindow, mainPicture);
            this.curveTool = new CurveTool(mainWindow, mainPicture);
            this.fillTool = new FillTool(mainWindow, mainPicture);
        }
    }
}

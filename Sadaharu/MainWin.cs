using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sadaharu
{
    public partial class MainWin : Form
    {

        public MainWin()
        {
            InitializeComponent();
            mainPicture.Image = new Bitmap(mainPicture.Width, mainPicture.Height);
            using (Graphics g = Graphics.FromImage(mainPicture.Image))
            {
                g.FillRectangle(Brushes.White, 0, 0, mainPicture.Image.Width, mainPicture.Image.Height);
            }

            Common.drawtools = new DrawTools(this, mainPicture);
            Common.setting = new Setting();
            Common.drawtools.nowTool = Common.drawtools.pointerTool;
            Common.drawtools.nowTool.startUseTool();
            Common.history = new History(this, mainPicture);
        }

        internal void cmdPrint(string str)
        {
            textBoxCmd.Text = str + System.Environment.NewLine;
        }

        internal void clearAll()
        {
            mainPicture.Image.Dispose();
            mainPicture.Image = new Bitmap(mainPicture.Width, mainPicture.Height);
            using (Graphics g = Graphics.FromImage(mainPicture.Image))
            {
                g.Clear(Color.White);
            }
        }

        private void trackBarXiankuan_Scroll(object sender, EventArgs e)
        {
            labelXiankuan.Text = string.Format("Thickness: {0,2}", trackBarXiankuan.Value);
            //cmdPrint(trackBarXiankuan.Value.ToString());
            Common.setting.nowPen.Width = trackBarXiankuan.Value;
            cmdPrint(string.Format("Adjust thickness to {0,2}", trackBarXiankuan.Value));
        }

        private void buttonColor_Click(object sender, EventArgs e)
        {
            //colorDialog1.ShowDialog();
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                buttonColor.BackColor = colorDialog1.Color;
                Common.setting.nowPen.Color = colorDialog1.Color;
                cmdPrint(string.Format("Adjust pen color to {0,2}", colorDialog1.Color));
            }
        }

        private void buttonBackColor_Click(object sender, EventArgs e)
        {
            //colorDialog1.ShowDialog();
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                buttonBackColor.BackColor = colorDialog1.Color;
                Common.setting.nowColor = colorDialog1.Color;
                cmdPrint(string.Format("Adjust back color to {0,2}", colorDialog1.Color));
            }
        }

        private void buttonStraight_Click(object sender, EventArgs e)
        {
            Common.drawtools.nowTool.endUseTool();
            Common.drawtools.nowTool = Common.drawtools.lineTool;
            Common.drawtools.nowTool.startUseTool();
            cmdPrint("Ready to draw a line");
        }

        private void buttonArrow_Click(object sender, EventArgs e)
        {
            Common.drawtools.nowTool.endUseTool();
            Common.drawtools.nowTool = Common.drawtools.pointerTool;
            Common.drawtools.nowTool.startUseTool();
            cmdPrint("Ready to select a shape");
        }

        public void buttonClear_Click(object sender, EventArgs e)
        {
            clearAll();
            Common.history.clearList();
            cmdPrint("Clear the canvas");
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                mainPicture.Image.Save(saveFileDialog1.FileName);
                //cmdPrint("Save the image as " + saveFileDialog1.FileName);
                cmdPrint(string.Format("Save the image as {0}", saveFileDialog1.FileName));
            }
        }

        private void buttonRect_Click(object sender, EventArgs e)
        {
            Common.drawtools.nowTool.endUseTool();
            Common.drawtools.nowTool = Common.drawtools.rectTool;
            Common.drawtools.nowTool.startUseTool();
            cmdPrint("Ready to draw a Rectangle");
        }

        private void buttonPoly_Click(object sender, EventArgs e)
        {
            Common.drawtools.nowTool.endUseTool();
            Common.drawtools.nowTool = Common.drawtools.polyTool;
            Common.drawtools.nowTool.startUseTool();
            cmdPrint("Ready to draw a Polygon");
        }

        private void buttonCircle_Click(object sender, EventArgs e)
        {
            Common.drawtools.nowTool.endUseTool();
            Common.drawtools.nowTool = Common.drawtools.circleTool;
            Common.drawtools.nowTool.startUseTool();
            cmdPrint("Ready to draw a Circle");
        }

        private void buttonCurve_Click(object sender, EventArgs e)
        {
            Common.drawtools.nowTool.endUseTool();
            Common.drawtools.nowTool = Common.drawtools.curveTool;
            Common.drawtools.nowTool.startUseTool();
            cmdPrint("Ready to draw a Curve");
        }

        private void buttonFillColor_Click(object sender, EventArgs e)
        {
            Common.drawtools.nowTool.endUseTool();
            Common.drawtools.nowTool = Common.drawtools.fillTool;
            Common.drawtools.nowTool.startUseTool();
            cmdPrint(string.Format("Ready to fill a region with {0}", Common.setting.nowColor));
        }
    }
}

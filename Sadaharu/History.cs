using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using Sadaharu.Shapes;
using Sadaharu.Tools;

namespace Sadaharu
{
    class History
    {
        MainWin mainWindow;

        PictureBox mainPicture;

        public Record nowSelect;

        List<Record> recordList;

        //List<FillTool> fillList;

        Image imageTmp;

        public History(MainWin window, PictureBox picture)
        {
            mainWindow = window;
            mainPicture = picture;
            recordList = new List<Record>();
            //fillList = new List<FillTool>();
            nowSelect = null;
            imageTmp = new Bitmap(mainPicture.Width, mainPicture.Height);
        }

        public void PushRecord(Record r)
        {
            recordList.Add(r);
        }

        public void delNowSelect()
        {
            if (nowSelect == null)
            {
                return;
            }
            nowSelect.shape.endSelect();
            mainWindow.cmdPrint("Delete " + nowSelect.shape.showMessage());
            recordList.Remove(nowSelect);
            nowSelect = null;
            update();
        }

        public void SearchShape(Point p)
        {
            foreach(Record r in recordList)
            {
                if(r.shape.isSelect(p))
                {
                    if (r == nowSelect)
                    {
                        mainWindow.cmdPrint("Select " + r.shape.showMessage());
                        return;
                    }
                    if (nowSelect != null)
                    {
                        nowSelect.shape.endSelect();
                    }
                    nowSelect = r;
                    //drawWithoutNow();
                    nowSelect.shape.startSelect();      //do drawWithoutNow() here!!!!
                    mainWindow.cmdPrint("Select " + r.shape.showMessage());
                    return;
                }
            }
            if (nowSelect != null)
            {
                nowSelect.shape.endSelect();
                nowSelect = null;
            }
            mainWindow.cmdPrint("No shape was Selected");
        }

        public void SearchShape_Resize(Point p)
        {
            foreach (Record r in recordList)
            {
                if (r.shape.isSelect(p))
                {
                    if (r == nowSelect)
                    {
                        mainWindow.cmdPrint("Resize " + r.shape.showMessage());
                        return;
                    }
                    if (nowSelect != null)
                    {
                        nowSelect.shape.endResize();
                    }
                    nowSelect = r;
                    //drawWithoutNow();
                    nowSelect.shape.startResize();      //do drawWithoutNow() here!!!!
                    mainWindow.cmdPrint("Resize " + r.shape.showMessage());
                    return;
                }
            }
            if (nowSelect != null)
            {
                nowSelect.shape.endResize();
                nowSelect = null;
            }
            mainWindow.cmdPrint("No shape was Selected");
        }

        public void update()
        {
            //mainWindow.clearAll();
            //foreach(Record r in recordList)
            //{
            //    r.tool.reDraw(r.shape);
            //}
            mainPicture.Image.Dispose();
            mainPicture.Image = (Image)imageTmp.Clone();
            using (Graphics g = Graphics.FromImage(mainPicture.Image))
            {
                if (nowSelect != null)
                {
                    nowSelect.tool.reDraw(nowSelect.shape, g);
                }
            }
        }

        public void drawWithoutNow()
        {
            if (imageTmp != null)
            {
                imageTmp.Dispose();
            }
            imageTmp = new Bitmap(mainPicture.Width, mainPicture.Height);
            using (Graphics g = Graphics.FromImage(imageTmp))
            {
                g.Clear(Color.White);
                foreach(Record r in recordList)
                {
                    if (r == nowSelect) continue;
                    r.tool.reDraw(r.shape, g);
                }
            }
        }

        public void clearList()
        {
            recordList.Clear();
        }
    }
}

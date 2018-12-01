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

        public History(MainWin window, PictureBox picture)
        {
            mainWindow = window;
            mainPicture = picture;
            recordList = new List<Record>();
            nowSelect = null;
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
                        return;
                    }
                    if (nowSelect != null)
                    {
                        nowSelect.shape.endSelect();
                    }
                    nowSelect = r;
                    nowSelect.shape.startSelect();
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

        public void update()
        {
            mainWindow.clearAll();
            foreach(Record r in recordList)
            {
                r.tool.reDraw(r.shape);
            }
        }

        public void clearList()
        {
            recordList.Clear();
        }
    }
}

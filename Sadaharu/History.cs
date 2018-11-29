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

        Record nowSelect;

        List<Record> recordList;

        public History(MainWin window, PictureBox picture)
        {
            mainWindow = window;
            mainPicture = picture;
            recordList = new List<Record>();
        }

        public void PushRecord(Record r)
        {
            recordList.Add(r);
        }
    }
}

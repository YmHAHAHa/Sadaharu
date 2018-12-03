using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Sadaharu.Shapes
{
    abstract class Shape:ISelectIt,IResizeIt
    {
        public Pen drawPen;

        public Color backColor;

        protected bool isAdjust, isMove;

        public Shape()
        {
            this.drawPen = Common.setting.nowPen.Clone() as Pen;
            this.backColor = Common.setting.nowColor;
            isAdjust = false;
            isMove = false;
        }

        public abstract bool isSelect(Point p);

        public abstract string showMessage();

        public virtual void startSelect()
        {
            Common.history.drawWithoutNow();
            Common.history.update();
        }

        public virtual void endSelect()
        {
            
        }

        public virtual void startResize()
        {

        }

        public virtual void endResize()
        {

        }
    }
}

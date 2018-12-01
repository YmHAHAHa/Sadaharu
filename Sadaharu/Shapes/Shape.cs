using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Sadaharu.Shapes
{
    abstract class Shape:ISelectIt
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

        }

        public virtual void endSelect()
        {

        }
    }
}

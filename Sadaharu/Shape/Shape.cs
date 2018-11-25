using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Sadaharu.Shape
{
    abstract class Shape:ISelectIt
    {
        public Pen drawPen;

        public Color backColor;

        protected bool isAdjust, isMove;

        public Shape(Pen pen)
        {
            this.drawPen = pen;
            this.backColor = Common.setting.nowColor;
            isAdjust = false;
            isMove = false;
        }

        public abstract bool isSelect(Point p);

        public virtual void startSelect()
        {

        }

        public virtual void endSelect()
        {

        }
    }
}

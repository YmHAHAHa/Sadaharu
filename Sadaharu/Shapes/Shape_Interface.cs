using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Sadaharu.Shapes
{
    interface ISelectIt
    {
        void startSelect();
        void endSelect();
    }

    interface IResizeIt
    {
        void startResize();
        void endResize();
    }

    interface IRotateIt
    {
        void rotateShape(Point mid, double angel);
    }
}

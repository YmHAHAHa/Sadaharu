using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sadaharu.Shape
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
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sadaharu.Shapes;
using Sadaharu.Tools;

namespace Sadaharu
{
    class Record
    {
        public Shape shape;

        public Tool tool;

        public Record(Shape s, Tool t)
        {
            shape = s;
            tool = t;
        }
    }
}

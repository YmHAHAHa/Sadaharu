using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Sadaharu
{
    class Setting
    {
        Pen pen;

        Color color;

        public Pen nowPen
        {
            get { return pen; }
            set { pen = value; }
        }

        public Color nowColor
        {
            get { return color; }
            set { color = value; }
        }

        public Setting()
        {
            nowColor = Color.White;
            nowPen = new Pen(Color.Black);
            nowPen.Width = 2;
        }

        public void setPenColor(Color colour)
        {
            nowPen.Color = colour;
        }

        public void setPenWidth(int width)
        {
            nowPen.Width = width;
        }
    }
}

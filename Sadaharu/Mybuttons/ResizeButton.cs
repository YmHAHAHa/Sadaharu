using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Sadaharu.Mylibs;
using Sadaharu.Shapes;

namespace Sadaharu.Mybuttons
{
    public partial class ResizeButton : Button
    {
        PictureBox mainPicture;

        bool isResize;

        Point tmpLocation;

        public ResizeButton(
            PictureBox pic, Point loc, Cursor cursor) : base()
        {
            this.mainPicture = pic;
            this.Location = loc;
            this.Cursor = cursor;
            this.BackColor = Color.Blue;
            this.Size = new Size(9, 9);
            this.FlatAppearance.BorderSize = 0;
            this.FlatStyle = FlatStyle.Popup;
            mainPicture.Controls.Add(this);
            isResize = false;
            this.Show();
        }

        public void clear()
        {
            this.Enabled = false;
            this.Visible = false;
            mainPicture.Controls.Remove(this);
            this.Dispose();
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);

            if (mevent.Button == MouseButtons.Left)
            {
                isResize = true;
                tmpLocation = mevent.Location;
            }
        }

        protected override void OnMouseMove(MouseEventArgs mevent)
        {
            base.OnMouseMove(mevent);
            if (isResize)
            {
                this.Location = new Point(this.Left + (mevent.X - tmpLocation.X),
                        this.Top + (mevent.Y - tmpLocation.Y));
            }
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            if (mevent.Button == MouseButtons.Left)
            {
                isResize = false;
            }
        }
    }
}

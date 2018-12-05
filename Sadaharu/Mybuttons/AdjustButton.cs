using System;
using System.Collections.Generic;
//using System.ComponentModel;
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
    public partial class AdjustButton : Button
    {
        Ref<Point>[] adjustPoints;

        Point[] beginPoints;

        PictureBox mainPicture;

        Point startLocation;

        Point tmpLocation;

        bool isAdjust;

        public AdjustButton(
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
            isAdjust = false;
            this.Show();
        }

        public void setAllPoints(params Ref<Point>[] refs)
        {
            this.adjustPoints = refs;
            beginPoints = new Point[adjustPoints.Length];

            //startLocation = this.Location;
            //for (int i = 0; i < beginPoints.Length; i++)
            //{
            //    beginPoints [i] = adjustPoints[i].Value;
            //}
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
                startLocation = this.Location;
                for (int j = 0; j < beginPoints.Length; j++)
                {
                    beginPoints[j] = adjustPoints[j].Value;
                }

                tmpLocation = mevent.Location;
                isAdjust = true;
            }
        }

        protected override void OnMouseMove(MouseEventArgs mevent)
        {
            base.OnMouseMove(mevent);
            if (isAdjust)
            {
                this.Location = new Point(this.Left + (mevent.X - tmpLocation.X),
                        this.Top + (mevent.Y - tmpLocation.Y));
                
                for (int i = 0; i < adjustPoints.Length; i++)
                {
                    adjustPoints[i].Value = new Point(
                        beginPoints[i].X + this.Location.X - startLocation.X,
                        beginPoints[i].Y + this.Location.Y - startLocation.Y);
                }
                Common.history.update();
            }
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            if (mevent.Button == MouseButtons.Left)
            {
                isAdjust = false;
            }
        }
    }
}

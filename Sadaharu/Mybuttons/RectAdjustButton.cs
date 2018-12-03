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

namespace Sadaharu.Mybuttons
{
    public partial class RectAdjustButton : Button
    {
        Ref<Point> mainAdjustPoint, blindAdjustPoint1, blindAdjustPoint2;

        Ref<Point> blindAdjustButton1, blindAdjustButton2;

        Point beginMainPoint, beginBlindPoint1, beginBlindPoint2;

        Point beginBlindButton1, beginBlindButton2;

        PictureBox mainPicture;

        Point startLocation;

        Point tmpLocation;

        bool isAdjust;

        public RectAdjustButton(
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
            this.mainAdjustPoint = refs[0];
            this.blindAdjustPoint1 = refs[1];
            this.blindAdjustPoint2 = refs[2];
            this.blindAdjustButton1 = refs[3];
            this.blindAdjustButton2 = refs[4];
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
                beginMainPoint = mainAdjustPoint.Value;
                beginBlindPoint1 = blindAdjustPoint1.Value;
                beginBlindPoint2 = blindAdjustPoint2.Value;
                beginBlindButton1 = blindAdjustButton1.Value;
                beginBlindButton2 = blindAdjustButton2.Value;
                //for (int i = 0; i < beginPoints.Length; i++)
                //{
                //    beginPoints[i] = adjustPoints[i].Value;
                //}

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

                //for (int i = 0; i < adjustPoints.Length; i++)
                //{
                //    adjustPoints[i].Value = new Point(beginPoints[i].X + Location.X - startLocation.X,
                //        beginPoints[i].Y + this.Location.Y - startLocation.Y);
                //}
                mainAdjustPoint.Value = new Point(
                    beginMainPoint.X + this.Location.X - startLocation.X,
                    beginMainPoint.Y + this.Location.Y - startLocation.Y);

                blindAdjustPoint1.Value = new Point(
                    beginBlindPoint1.X + this.Location.X - startLocation.X,
                    beginBlindPoint1.Y);

                blindAdjustPoint2.Value = new Point(
                    beginBlindPoint2.X,
                    beginBlindPoint2.Y + this.Location.Y - startLocation.Y);

                blindAdjustButton1.Value = new Point(
                    beginBlindButton1.X + this.Location.X - startLocation.X,
                    beginBlindButton1.Y);

                blindAdjustButton2.Value = new Point(
                    beginBlindButton2.X,
                    beginBlindButton2.Y + this.Location.Y - startLocation.Y);

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

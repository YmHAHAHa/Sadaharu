using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using Sadaharu.Mylibs;

namespace Sadaharu.Mybuttons
{
    public partial class RotateButton : Button
    {

        Ref<Point>[] adjustPoints;

        PointF[] beginPoints;

        PictureBox mainPicture;

        Point startLocation;            //rotateButton startLocation

        Point tmpLocation;              //cursor startPoint

        Point midPoint;

        bool isRotate;

        public RotateButton(PictureBox pic, Point mid) : base()
        {
            this.mainPicture = pic;
            midPoint = mid;
            this.Location = new Point(midPoint.X + 77, midPoint.Y - 3);
            this.Cursor = new Cursor("C:\\Users\\Mingh\\Desktop\\Sadaharu\\Sadaharu\\Resources\\ro3.cur");
            this.BackColor = Color.Red;
            this.Size = new Size(9, 9);
            this.FlatAppearance.BorderSize = 0;
            this.FlatAppearance.BorderColor = Color.Red;
            this.FlatStyle = FlatStyle.Popup;
            mainPicture.Controls.Add(this);
            isRotate = false;
            this.Show();
        }

        public void setAllPoints(params Ref<Point>[] refs)
        {
            this.adjustPoints = refs;
            beginPoints = new PointF[adjustPoints.Length + 1];
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
                //for (int j = 0; j < beginPoints.Length - 1; j++) 
                //{
                //    beginPoints[j] = adjustPoints[j].Value;
                //}
                //beginPoints[beginPoints.Length - 1] = this.Location;

                beginPoints = new PointF[] { new PointF(200, 200), new PointF(300, 400), this.Location };

                tmpLocation = mevent.Location;
                isRotate = true;
            }
        }

        protected override void OnMouseMove(MouseEventArgs mevent)
        {
            base.OnMouseMove(mevent);
            if (isRotate)
            {
                //double a = Math.Atan2(mevent.Y - midPoint.Y, mevent.X - midPoint.X);
                using (var path = new GraphicsPath())
                {
                    path.AddPolygon(beginPoints);
                    Point tmp= new Point(this.Left + (mevent.X - tmpLocation.X),
                        this.Top + (mevent.Y - tmpLocation.Y));
                    double a = Math.Atan2(tmp.Y - midPoint.Y, tmp.X - midPoint.X);
                    var n1 = (float)Math.Cos(a);
                    var n2 = (float)Math.Sin(a);
                    var n3 = -(float)Math.Sin(a);
                    var n4 = (float)Math.Cos(a);
                    var n5 = (float)((midPoint.X * (1 - Math.Cos(a)) + midPoint.Y * Math.Sin(a)));
                    var n6 = (float)((midPoint.Y * (1 - Math.Cos(a)) - midPoint.X * Math.Sin(a)));
                    path.Transform(new Matrix(n1, n2, n3, n4, n5, n6));

                    int x = (int)path.PathPoints[path.PointCount - 1].X,
                        y = (int)path.PathPoints[path.PointCount - 1].Y;
                    this.Location = new Point(x, y);
                }

                //for (int i = 0; i < adjustPoints.Length; i++)
                //{
                //    adjustPoints[i].Value = new Point(
                //        beginPoints[i].X + this.Location.X - startLocation.X,
                //        beginPoints[i].Y + this.Location.Y - startLocation.Y);
                //}
                Common.history.update();
            }
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            if (mevent.Button == MouseButtons.Left)
            {
                isRotate = false;
                this.Location = new Point(midPoint.X + 77, midPoint.Y - 3);
            }
        }
    }
}

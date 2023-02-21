using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KkutuMemo
{
    internal class MoremiPanel : Panel
    {
        private int CornerRadius = 10;

        private GraphicsPath GetCorneredPath(RectangleF rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();

            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90); // 좌측상단
            path.AddArc(rect.Width - radius, rect.Y, radius, radius, 270, 90); // 우측상단
            path.AddArc(rect.Width, rect.Height, 1, 1, 0, 90); // 우측하단
            path.AddArc(rect.X, rect.Height, 1, 1, 90, 90); // 좌측하단

            //path.AddArc(rect.Width - radius, rect.Height - radius, radius, radius, 0, 90); // 우측하단
            //path.AddArc(rect.X, rect.Height - radius, radius, radius, 90, 90); // 좌측하단

            path.CloseFigure();
            return path;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            //base.OnPaint(pevent);
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            RectangleF rect = new RectangleF(0, 0, this.Width, this.Height);
            GraphicsPath pathSurface = GetCorneredPath(rect, this.CornerRadius);
            Pen penSurface = new Pen(this.Parent.BackColor, 2);

            this.Region = new Region(pathSurface);
            pevent.Graphics.DrawPath(penSurface, pathSurface);
        }

        private void Container_BackColorChanged(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                this.Invalidate();
            }
        }
    }
}

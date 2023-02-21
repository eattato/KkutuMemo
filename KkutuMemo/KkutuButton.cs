using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Drawing.Text;
using System.Diagnostics;

namespace KkutuMemo
{
    internal class MoremiButton : Button // 버튼을 상속함
    {
        private int cornerRadius = 10;
        private bool cornerUnder = false;

        public int CornerRadius
        {
            get { return cornerRadius; }
            set {
                cornerRadius = value;
                this.Update();
            }
        }

        public bool CornerUnder
        {
            get { return cornerUnder; }
            set {
                cornerUnder = value;
                this.Update();
            }
        }

        // Init
        public MoremiButton()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.BackColor = Color.LightGray;
            this.ForeColor = Color.White;

            // Events
            //this.MouseHover += (object sender, EventArgs e) => hover = true;
            //this.MouseLeave += (object sender, EventArgs e) => hover = false;

            //fontCollection.AddFontFile("./Resources/NanumSquareNeo.ttf");
            //this.Font = new Font(fontCollection.Families[0], 8f);
        }

        // 둥근 코너를 적용한 GraphicsPath를 가져옴
        private GraphicsPath GetCorneredPath(RectangleF rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();

            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90); // 좌측상단
            path.AddArc(rect.Width - radius, rect.Y, radius, radius, 270, 90); // 우측상단

            float underCornerSize = 1;
            if (cornerUnder == true)
            {
                underCornerSize = radius;
            }
            path.AddArc(rect.Width - underCornerSize, rect.Height - underCornerSize, underCornerSize, underCornerSize, 0, 90); // 우측하단
            path.AddArc(rect.X, rect.Height - underCornerSize, underCornerSize, underCornerSize, 90, 90); // 좌측하단

            //path.AddArc(rect.Width - radius, rect.Height - radius, radius, radius, 0, 90); // 우측하단
            //path.AddArc(rect.X, rect.Height - radius, radius, radius, 90, 90); // 좌측하단

            path.CloseFigure();
            return path;
        }

        // 그려질때 사용되는 함수를 덮어씀
        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            RectangleF rect = new RectangleF(0, 0, this.Width, this.Height);
            GraphicsPath pathSurface = GetCorneredPath(rect, this.cornerRadius);
            Pen penSurface = new Pen(this.Parent.BackColor, 2);

            this.Region = new Region(pathSurface);
            pevent.Graphics.DrawPath(penSurface, pathSurface);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            this.Parent.BackColorChanged += new EventHandler(Container_BackColorChanged);
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

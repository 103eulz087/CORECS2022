using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem.Classes
{
    public class RoundedLabelControl : LabelControl
    {
        public int CornerRadius { get; set; } = 10;
        public Color BorderColor { get; set; } = Color.Gray;
        public int BorderThickness { get; set; } = 1;

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = this.ClientRectangle;
            rect.Inflate(-BorderThickness, -BorderThickness);

            using (GraphicsPath path = RoundedRect(rect, CornerRadius))
            using (Brush b = new SolidBrush(this.BackColor))
            using (Pen p = new Pen(BorderColor, BorderThickness))
            {
                e.Graphics.FillPath(b, path);
                e.Graphics.DrawPath(p, path);
            }

            // Draw the text centered
            TextRenderer.DrawText(
                e.Graphics,
                this.Text,
                this.Font,
                rect,
                this.ForeColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
            );
        }

        private GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }

    }
}

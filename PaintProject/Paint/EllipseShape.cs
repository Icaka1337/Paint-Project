using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint
{
    public class EllipseShape : Shape
    {
        public EllipseShape(Point startPoint, Point endPoint, Color color, int thickness)
            : base(startPoint, endPoint, color, thickness)
        {
        }

        public override void Draw(Graphics g)
        {
            using (Pen pen = new Pen(Color, Thickness))
            {
                g.DrawEllipse(pen, GetRectangle());
            }
        }

        public override string Serialize()
        {
            return $"Ellipse,{StartPoint.X},{StartPoint.Y},{EndPoint.X},{EndPoint.Y},{Color.ToArgb()},{Thickness}";
        }

        private Rectangle GetRectangle()
        {
            return new Rectangle(
                Math.Min(StartPoint.X, EndPoint.X),
                Math.Min(StartPoint.Y, EndPoint.Y),
                Math.Abs(EndPoint.X - StartPoint.X),
                Math.Abs(EndPoint.Y - StartPoint.Y));
        }

        public override bool IsPointInside(Point p)
        {
            Rectangle rect = GetRectangle();
            float dx = (p.X - rect.Left) / (float)rect.Width - 0.5f;
            float dy = (p.Y - rect.Top) / (float)rect.Height - 0.5f;
            return (dx * dx + dy * dy) <= 0.25f;
        }
    }
}

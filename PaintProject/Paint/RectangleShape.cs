using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint
{
    public class RectangleShape : Shape
    {
        public RectangleShape(Point startPoint, Point endPoint, Color color, int thickness, int opacity)
            : base(startPoint, endPoint, color, thickness, opacity)
        {
        }

        public override void Draw(Graphics g)
        {
            using (Pen pen = new Pen(Color.FromArgb(Opacity, Color), Thickness))
            {
                g.DrawRectangle(pen, GetRectangle());
            }
        }

        public override string Serialize()
        {
            return $"Rectangle,{StartPoint.X},{StartPoint.Y},{EndPoint.X},{EndPoint.Y},{Color.ToArgb()},{Thickness},{Opacity}";
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
            return rect.Contains(p);
        }
    }
}

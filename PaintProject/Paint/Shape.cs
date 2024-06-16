using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint
{
    public abstract class Shape
    {
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
        public Color Color { get; set; }
        public int Thickness { get; set; }

        protected Shape(Point startPoint, Point endPoint, Color color, int thickness)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            Color = color;
            Thickness = thickness;
        }

        public abstract void Draw(Graphics g);
        public abstract string Serialize();
        public abstract bool IsPointInside(Point p);
        public int Width => Math.Abs(EndPoint.X - StartPoint.X);
        public int Height => Math.Abs(EndPoint.Y - StartPoint.Y);

        public static Shape Deserialize(string data)
        {
            string[] parts = data.Split(',');
            ShapeType shapeType = (ShapeType)Enum.Parse(typeof(ShapeType), parts[0]);
            Point startPoint = new Point(int.Parse(parts[1]), int.Parse(parts[2]));
            Point endPoint = new Point(int.Parse(parts[3]), int.Parse(parts[4]));
            Color color = Color.FromArgb(int.Parse(parts[5]));
            int thickness = int.Parse(parts[6]);

            switch (shapeType)
            {
                case ShapeType.Line:
                    return new Line(startPoint, endPoint, color, thickness);
                case ShapeType.Rectangle:
                    return new RectangleShape(startPoint, endPoint, color, thickness);
                case ShapeType.Ellipse:
                    return new EllipseShape(startPoint, endPoint, color, thickness);
                default:
                    throw new ArgumentException("Unknown shape type.");
            }
        }

        public void MoveTo(Point newLocation)
        {
            int deltaX = newLocation.X - StartPoint.X;
            int deltaY = newLocation.Y - StartPoint.Y;

            StartPoint = newLocation;
            EndPoint = new Point(EndPoint.X + deltaX, EndPoint.Y + deltaY);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint
{
    public class Line : Shape
    {
        public Line(Point startPoint, Point endPoint, Color color, int thickness)
            : base(startPoint, endPoint, color, thickness)
        {
        }

        public override void Draw(Graphics g)
        {
            using (Pen pen = new Pen(Color, Thickness))
            {
                g.DrawLine(pen, StartPoint, EndPoint);
            }
        }

        public override string Serialize()
        {
            return $"Line,{StartPoint.X},{StartPoint.Y},{EndPoint.X},{EndPoint.Y},{Color.ToArgb()},{Thickness}";
        }

        public override bool IsPointInside(Point p)
        {
            double distance = DistanceFromPointToLine(StartPoint, EndPoint, p);
            return distance <= Thickness / 2.0;
        }

        private double DistanceFromPointToLine(Point a, Point b, Point p)
        {
            double ABx = b.X - a.X;
            double ABy = b.Y - a.Y;

            double APx = p.X - a.X;
            double APy = p.Y - a.Y;

            double BPx = p.X - b.X;
            double BPy = p.Y - b.Y;

            double AB_AP = ABx * APx + ABy * APy;

            double AB_AB = ABx * ABx + ABy * ABy;

            double distance;

            if (AB_AB == 0)
            {
                distance = Math.Sqrt(APx * APx + APy * APy);
            }
            else
            {
                double t = AB_AP / AB_AB;

                if (t < 0)
                {
                    distance = Math.Sqrt(APx * APx + APy * APy);
                }
                else if (t > 1)
                {
                    distance = Math.Sqrt(BPx * BPx + BPy * BPy);
                }
                else
                {
                    double projx = a.X + t * ABx;
                    double projy = a.Y + t * ABy;
                    double Dx = p.X - projx;
                    double Dy = p.Y - projy;
                    distance = Math.Sqrt(Dx * Dx + Dy * Dy);
                }
            }

            return distance;
        }

    }
}

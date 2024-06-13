using Paint;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Paint
{
    public partial class Form1 : Form
    {
        private List<Shape> shapes = new List<Shape>();
        private ShapeType currentShapeType = ShapeType.Line;
        private Color currentColor = Color.Black;
        private int currentThickness = 1;
        private Shape currentShape = null;
        private Shape lastSavedShape;
        private Shape selectedShape = null;
        private Point offset;
        private Point mouseOffset;
        private bool isMovingShape = false;

        public Form1()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            this.panelDrawing.Paint += new PaintEventHandler(this.panelDrawing_Paint);
            this.panelDrawing.MouseDown += new MouseEventHandler(this.panelDrawing_MouseDown);
            this.panelDrawing.MouseMove += new MouseEventHandler(this.panelDrawing_MouseMove);
            this.panelDrawing.MouseUp += new MouseEventHandler(this.panelDrawing_MouseUp);
        }

        private void panelDrawing_Paint(object sender, PaintEventArgs e)
        {
            foreach (var shape in shapes)
            {
                shape.Draw(e.Graphics);
            }
        }

        private void panelDrawing_MouseDown(object sender, MouseEventArgs e)
        {
            if (!isMovingShape)
            {
                if (selectedShape != null && selectedShape.IsPointInside(e.Location))
                {
                    offset = new Point(e.Location.X - selectedShape.StartPoint.X, e.Location.Y - selectedShape.StartPoint.Y);
                    return;
                }

                Shape shape = null;
                switch (currentShapeType)
                {
                    case ShapeType.Line:
                        shape = new Line(e.Location, e.Location, currentColor, currentThickness);
                        break;
                    case ShapeType.Rectangle:
                        shape = new RectangleShape(e.Location, e.Location, currentColor, currentThickness);
                        break;
                    case ShapeType.Ellipse:
                        shape = new EllipseShape(e.Location, e.Location, currentColor, currentThickness);
                        break;
                }

                if (shape != null)
                {
                    currentShape = shape;
                    lastSavedShape = shape;
                    shapes.Add(shape);
                    panelDrawing.Invalidate();
                }

                // Add your existing code for drawing shapes here
            }

            else
            {
                foreach (var s in shapes)
                {
                    if (s.IsPointInside(e.Location))
                    {
                        if (selectedShape != null)
                        {
                            mouseOffset = new Point(e.Location.X - selectedShape.StartPoint.X,
                                e.Location.Y - selectedShape.StartPoint.Y);
                        }
                        selectedShape = s;
                        mouseOffset = new Point(e.Location.X - s.StartPoint.X, e.Location.Y - s.StartPoint.Y);
                        return;
                    }
                }

                selectedShape = null;
            }
        }

        private void panelDrawing_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isMovingShape)
            {


                if (e.Button == MouseButtons.Left && currentShape != null)
                {
                    currentShape.EndPoint = e.Location;
                    panelDrawing.Invalidate();
                }
                else if (e.Button == MouseButtons.Left && selectedShape != null)
                {
                    selectedShape.StartPoint = new Point(e.Location.X - offset.X, e.Location.Y - offset.Y);
                    selectedShape.EndPoint = new Point(selectedShape.StartPoint.X + selectedShape.Width,
                        selectedShape.StartPoint.Y + selectedShape.Height);
                    panelDrawing.Invalidate();
                }

                if (e.Button == MouseButtons.Left && selectedShape != null)
                {
                    Point newLocation = new Point(e.Location.X - mouseOffset.X, e.Location.Y - mouseOffset.Y);
                    selectedShape.MoveTo(newLocation);
                    panelDrawing.Invalidate();
                }
            }
            else
            {
                if (selectedShape != null && e.Button == MouseButtons.Left)
                {
                    Point newLocation = new Point(e.Location.X - mouseOffset.X, e.Location.Y - mouseOffset.Y);
                    selectedShape.MoveTo(newLocation);
                    panelDrawing.Invalidate();
                }
            }
        }

        private void panelDrawing_MouseUp(object sender, MouseEventArgs e)
        {
            if (currentShape != null)
            {
                currentShape = null;
            }
            else if (selectedShape != null)
            {
                selectedShape = null;
            }

            selectedShape = null;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            shapes.Clear();
            panelDrawing.Invalidate();
        }


        private void resizeOrMoveShapeButton(object sender, EventArgs e)
        {
            isMovingShape = !isMovingShape; // Toggle the move
            if (isMovingShape)
            {
                // Disable drawing shapes
                currentShapeType = ShapeType.None;
                panelDrawing.Cursor = Cursors.SizeAll;
            }
            else
            {
                // Enable drawing shapes
                currentShapeType = ShapeType.Line; // Or whichever default shape you want
                panelDrawing.Cursor = Cursors.Cross;
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Shape files (*.shp)|*.shp|All files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                LoadShapesFromFile(openFileDialog.FileName);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Shape files (*.shp)|*.shp|All files (*.*)|*.*"
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveShapesToFile(saveFileDialog.FileName);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentShapeType = ShapeType.Line;
        }

        private void rectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentShapeType = ShapeType.Rectangle;
        }

        private void ellipseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentShapeType = ShapeType.Ellipse;
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                currentColor = colorDialog.Color;
            }
        }

        private void thicknessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThicknessForm thicknessForm = new ThicknessForm(currentThickness);
            if (thicknessForm.ShowDialog() == DialogResult.OK)
            {
                currentThickness = thicknessForm.Thickness;
            }
        }

        private void SaveShapesToFile(string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (var shape in shapes)
                {
                    writer.WriteLine(shape.Serialize());
                }
            }
        }

        private void LoadShapesFromFile(string filename)
        {
            shapes.Clear();
            using (StreamReader reader = new StreamReader(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    shapes.Add(Shape.Deserialize(line));
                }
            }
            panelDrawing.Invalidate();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void moveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isMovingShape = !isMovingShape; // Toggle the move
            if (isMovingShape)
            {
                // Disable drawing shapes
                currentShapeType = ShapeType.None;
                panelDrawing.Cursor = Cursors.SizeAll;
            }
            else
            {
                // Enable drawing shapes
                currentShapeType = ShapeType.Line; // Or whichever default shape you want
                panelDrawing.Cursor = Cursors.Cross;
            }
        }
    }

    public enum ShapeType
    {
        Line,
        Rectangle,
        Ellipse,
        None
    }

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
            // Simple check for line vicinity (bounding box)
            int minX = Math.Min(StartPoint.X, EndPoint.X) - Thickness;
            int maxX = Math.Max(StartPoint.X, EndPoint.X) + Thickness;
            int minY = Math.Min(StartPoint.Y, EndPoint.Y) - Thickness;
            int maxY = Math.Max(StartPoint.Y, EndPoint.Y) + Thickness;

            return p.X >= minX && p.X <= maxX && p.Y >= minY && p.Y <= maxY;
        }
    }

    public class RectangleShape : Shape
    {
        public RectangleShape(Point startPoint, Point endPoint, Color color, int thickness)
            : base(startPoint, endPoint, color, thickness)
        {
        }

        public override void Draw(Graphics g)
        {
            using (Pen pen = new Pen(Color, Thickness))
            {
                g.DrawRectangle(pen, GetRectangle());
            }
        }

        public override string Serialize()
        {
            return $"Rectangle,{StartPoint.X},{StartPoint.Y},{EndPoint.X},{EndPoint.Y},{Color.ToArgb()},{Thickness}";
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
            float dx = (p.X - rect.Left) / (float)rect.Width;
            float dy = (p.Y - rect.Top) / (float)rect.Height;
            return (dx * dx + dy * dy) <= 1;
        }
    }
}

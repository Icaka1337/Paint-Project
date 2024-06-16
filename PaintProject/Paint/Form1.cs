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
        private int currentThickness = 10;
        private Shape currentShape = null;
        private Shape lastSavedShape = null;
        private Shape selectedShape = null;
        private Point offset;
        private Point mouseOffset;
        private bool isMovingShape = false;
        private bool isResizeShape = false;

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
                    case ShapeType.Custom:
                        // Open the custom shape form
                        using (CustomShapeForm customShapeForm = new CustomShapeForm())
                        {
                            if (customShapeForm.ShowDialog() == DialogResult.OK)
                            {
                                var customShapes = customShapeForm.Shapes;
                                if (customShapes.Count > 0)
                                {
                                    shapes.AddRange(customShapes);
                                    panelDrawing.Invalidate();
                                }
                            }
                        }
                        break;
                }

                if (shape != null)
                {
                    currentShape = shape;
                    lastSavedShape = shape;
                    shapes.Add(shape);
                    panelDrawing.Invalidate();
                }
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
            isMovingShape = false;
            currentShapeType = ShapeType.Line;
            panelDrawing.Cursor = Cursors.Cross;
        }

        private void rectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isMovingShape = false;
            currentShapeType = ShapeType.Rectangle;
            panelDrawing.Cursor = Cursors.Cross;
        }

        private void ellipseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isMovingShape = false;
            currentShapeType = ShapeType.Ellipse;
            panelDrawing.Cursor = Cursors.Cross;
        }

        private void customShapeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isMovingShape = false;
            currentShapeType = ShapeType.Custom;
            panelDrawing.Cursor = Cursors.Cross;

            using (CustomShapeForm customShapeForm = new CustomShapeForm())
            {
                if (customShapeForm.ShowDialog() == DialogResult.OK)
                {
                    // Add shapes drawn in CustomShapeForm to main shapes list
                    shapes.AddRange(customShapeForm.Shapes);
                    panelDrawing.Invalidate(); // Refresh the panel to show new shapes
                }
            }
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
                currentShapeType = ShapeType.Line;
                panelDrawing.Cursor = Cursors.Cross;
            }
        }

        private void resizeShapeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isResizeShape = !isResizeShape; // Toggle resize
        }
    }
}



public enum ShapeType
{
    Line,
    Rectangle,
    Ellipse,
    Custom,
    None
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
public class CustomShape : Shape
{
    private List<Point> points;

    public CustomShape(List<Point> points, Color color, int thickness)
        : base(points.First(), points.Last(), color, thickness)
    {
        this.points = points;
    }

    public void SetPoints(List<Point> newPoints)
    {
        points = newPoints;
    }

    public override void Draw(Graphics g)
    {
        if (points.Count > 1)
        {
            using (Pen pen = new Pen(Color, Thickness))
            {
                g.DrawLines(pen, points.ToArray());
            }
        }
    }

    public override bool IsPointInside(Point p)
    {
        // Implement point-in-shape logic if needed
        return false;
    }

    public override string Serialize()
    {
        return $"CustomShape,{string.Join(";", points.Select(pt => $"{pt.X},{pt.Y}"))},{Color.ToArgb()},{Thickness}";
    }
}
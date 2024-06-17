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
        private List<int> shapesCount = new List<int>();
        private List<Point> points = new List<Point>();
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
                        shapesCount.Add(1);
                        points.Add(e.Location);
                        break;
                    case ShapeType.Rectangle:
                        shape = new RectangleShape(e.Location, e.Location, currentColor, currentThickness);
                        shapesCount.Add(2);
                        points.Add(e.Location);
                        break;
                    case ShapeType.Ellipse:
                        shape = new EllipseShape(e.Location, e.Location, currentColor, currentThickness);
                        shapesCount.Add(3);
                        points.Add(e.Location);
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
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.M))
            {
                ToggleMoveMode();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void moveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleMoveMode();
        }

        private void ToggleMoveMode()
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
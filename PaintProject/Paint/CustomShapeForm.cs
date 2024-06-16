using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Paint
{
    public partial class CustomShapeForm : Form
    {
        private List<Shape> shapes;
        private ShapeType currentShapeType = ShapeType.Line;
        private Color currentColor = Color.Black;
        private int currentThickness = 10;
        private Shape currentShape = null;

        public List<Shape> Shapes { get { return shapes; } }

        public CustomShapeForm()
        {
            InitializeComponent();
            shapes = new List<Shape>();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
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
                shapes.Add(shape);
                Invalidate();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (currentShape != null)
            {
                currentShape.EndPoint = e.Location;
                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (currentShape != null)
            {
                currentShape.EndPoint = e.Location;
                currentShape = null;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            foreach (var shape in shapes)
            {
                shape.Draw(e.Graphics);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnLine_Click(object sender, EventArgs e)
        {
            currentShapeType = ShapeType.Line;
        }

        private void btnRectangle_Click(object sender, EventArgs e)
        {
            currentShapeType = ShapeType.Rectangle;
        }

        private void btnEllipse_Click(object sender, EventArgs e)
        {
            currentShapeType = ShapeType.Ellipse;
        }
    }
}

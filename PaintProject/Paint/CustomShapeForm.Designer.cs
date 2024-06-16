namespace Paint
{
    partial class CustomShapeForm
    {
        private System.ComponentModel.IContainer components = null;
        private Button btnSave;
        private Button btnCancel;
        private Button btnLine;
        private Button btnRectangle;
        private Button btnEllipse;
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnSave = new Button();
            btnCancel = new Button();
            btnLine = new Button();
            btnRectangle = new Button();
            btnEllipse = new Button();
            SuspendLayout();
            // 
            // btnSave
            // 
            btnSave.Location = new Point(10, 10);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 35);
            btnSave.TabIndex = 0;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(100, 10);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 35);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnLine
            // 
            btnLine.Location = new Point(10, 50);
            btnLine.Name = "btnLine";
            btnLine.Size = new Size(75, 35);
            btnLine.TabIndex = 2;
            btnLine.Text = "Line";
            btnLine.UseVisualStyleBackColor = true;
            btnLine.Click += btnLine_Click;
            // 
            // btnRectangle
            // 
            btnRectangle.Location = new Point(100, 50);
            btnRectangle.Name = "btnRectangle";
            btnRectangle.Size = new Size(90, 35);
            btnRectangle.TabIndex = 3;
            btnRectangle.Text = "Rectangle";
            btnRectangle.UseVisualStyleBackColor = true;
            btnRectangle.Click += btnRectangle_Click;
            // 
            // btnEllipse
            // 
            btnEllipse.Location = new Point(203, 50);
            btnEllipse.Name = "btnEllipse";
            btnEllipse.Size = new Size(75, 35);
            btnEllipse.TabIndex = 4;
            btnEllipse.Text = "Ellipse";
            btnEllipse.UseVisualStyleBackColor = true;
            btnEllipse.Click += btnEllipse_Click;
            // 
            // CustomShapeForm
            // 
            ClientSize = new Size(900, 720);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            Controls.Add(btnLine);
            Controls.Add(btnRectangle);
            Controls.Add(btnEllipse);
            Name = "CustomShapeForm";
            Text = "CustomShape";
            ResumeLayout(false);
        }

        #endregion



    }
}
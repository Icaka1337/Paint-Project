namespace Paint
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            newToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            shapesToolStripMenuItem = new ToolStripMenuItem();
            lineToolStripMenuItem = new ToolStripMenuItem();
            rectangleToolStripMenuItem = new ToolStripMenuItem();
            ellipseToolStripMenuItem = new ToolStripMenuItem();
            customShapeToolStripMenuItem = new ToolStripMenuItem();
            colorToolStripMenuItem = new ToolStripMenuItem();
            thicknessToolStripMenuItem = new ToolStripMenuItem();
            remodelShapeToolStripMenuItem = new ToolStripMenuItem();
            moveToolStripMenuItem = new ToolStripMenuItem();
            resizeShapeToolStripMenuItem = new ToolStripMenuItem();
            opacityToolStripMenuItem = new ToolStripMenuItem();
            panelDrawing = new Panel();
            toolStrip1 = new ToolStrip();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, shapesToolStripMenuItem, colorToolStripMenuItem, thicknessToolStripMenuItem, remodelShapeToolStripMenuItem, opacityToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(5, 2, 0, 2);
            menuStrip1.Size = new Size(711, 28);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newToolStripMenuItem, openToolStripMenuItem, saveToolStripMenuItem, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(46, 24);
            fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.Size = new Size(128, 26);
            newToolStripMenuItem.Text = "New";
            newToolStripMenuItem.Click += newToolStripMenuItem_Click;
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(128, 26);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(128, 26);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(128, 26);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // shapesToolStripMenuItem
            // 
            shapesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { lineToolStripMenuItem, rectangleToolStripMenuItem, ellipseToolStripMenuItem, customShapeToolStripMenuItem });
            shapesToolStripMenuItem.Name = "shapesToolStripMenuItem";
            shapesToolStripMenuItem.Size = new Size(70, 24);
            shapesToolStripMenuItem.Text = "Shapes";
            // 
            // lineToolStripMenuItem
            // 
            lineToolStripMenuItem.Name = "lineToolStripMenuItem";
            lineToolStripMenuItem.Size = new Size(187, 26);
            lineToolStripMenuItem.Text = "Line";
            lineToolStripMenuItem.Click += lineToolStripMenuItem_Click;
            // 
            // rectangleToolStripMenuItem
            // 
            rectangleToolStripMenuItem.Name = "rectangleToolStripMenuItem";
            rectangleToolStripMenuItem.Size = new Size(187, 26);
            rectangleToolStripMenuItem.Text = "Rectangle";
            rectangleToolStripMenuItem.Click += rectangleToolStripMenuItem_Click;
            // 
            // ellipseToolStripMenuItem
            // 
            ellipseToolStripMenuItem.Name = "ellipseToolStripMenuItem";
            ellipseToolStripMenuItem.Size = new Size(187, 26);
            ellipseToolStripMenuItem.Text = "Ellipse";
            ellipseToolStripMenuItem.Click += ellipseToolStripMenuItem_Click;
            // 
            // customShapeToolStripMenuItem
            // 
            customShapeToolStripMenuItem.Name = "customShapeToolStripMenuItem";
            customShapeToolStripMenuItem.Size = new Size(187, 26);
            customShapeToolStripMenuItem.Text = "Custom Shape";
            customShapeToolStripMenuItem.Click += customShapeToolStripMenuItem_Click;
            // 
            // colorToolStripMenuItem
            // 
            colorToolStripMenuItem.Name = "colorToolStripMenuItem";
            colorToolStripMenuItem.Size = new Size(59, 24);
            colorToolStripMenuItem.Text = "Color";
            colorToolStripMenuItem.Click += colorToolStripMenuItem_Click;
            // 
            // thicknessToolStripMenuItem
            // 
            thicknessToolStripMenuItem.Name = "thicknessToolStripMenuItem";
            thicknessToolStripMenuItem.Size = new Size(85, 24);
            thicknessToolStripMenuItem.Text = "Thickness";
            thicknessToolStripMenuItem.Click += thicknessToolStripMenuItem_Click;
            // 
            // remodelShapeToolStripMenuItem
            // 
            remodelShapeToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { moveToolStripMenuItem, resizeShapeToolStripMenuItem });
            remodelShapeToolStripMenuItem.Name = "remodelShapeToolStripMenuItem";
            remodelShapeToolStripMenuItem.Size = new Size(128, 24);
            remodelShapeToolStripMenuItem.Text = "Remodel Shape";
            // 
            // moveToolStripMenuItem
            // 
            moveToolStripMenuItem.Name = "moveToolStripMenuItem";
            moveToolStripMenuItem.Size = new Size(179, 26);
            moveToolStripMenuItem.Text = "Move";
            moveToolStripMenuItem.Click += moveToolStripMenuItem_Click;
            // 
            // resizeShapeToolStripMenuItem
            // 
            resizeShapeToolStripMenuItem.Name = "resizeShapeToolStripMenuItem";
            resizeShapeToolStripMenuItem.Size = new Size(179, 26);
            resizeShapeToolStripMenuItem.Text = "Resize Shape";
            resizeShapeToolStripMenuItem.Click += resizeShapeToolStripMenuItem_Click;
            // 
            // opacityToolStripMenuItem
            // 
            opacityToolStripMenuItem.Name = "opacityToolStripMenuItem";
            opacityToolStripMenuItem.Size = new Size(74, 24);
            opacityToolStripMenuItem.Text = "Opacity";
            opacityToolStripMenuItem.Click += opacityToolStripMenuItem_Click;
            // 
            // panelDrawing
            // 
            panelDrawing.Dock = DockStyle.Fill;
            panelDrawing.Location = new Point(0, 28);
            panelDrawing.Name = "panelDrawing";
            panelDrawing.Size = new Size(711, 422);
            panelDrawing.TabIndex = 1;
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Location = new Point(0, 28);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(711, 25);
            toolStrip1.TabIndex = 2;
            toolStrip1.Text = "toolStrip1";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(711, 450);
            Controls.Add(toolStrip1);
            Controls.Add(panelDrawing);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Paint Application";
            WindowState = FormWindowState.Maximized;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem shapesToolStripMenuItem;
        private ToolStripMenuItem lineToolStripMenuItem;
        private ToolStripMenuItem rectangleToolStripMenuItem;
        private ToolStripMenuItem ellipseToolStripMenuItem;
        private ToolStripMenuItem colorToolStripMenuItem;
        private ToolStripMenuItem thicknessToolStripMenuItem;
        private Panel panelDrawing;
        private ToolStripMenuItem opacityToolStripMenuItem;
        private ToolStripMenuItem remodelShapeToolStripMenuItem;
        private ToolStripMenuItem moveToolStripMenuItem;
        private ToolStripMenuItem resizeShapeToolStripMenuItem;
        private ToolStrip toolStrip1;
        private ToolStripMenuItem customShapeToolStripMenuItem;
    }
}

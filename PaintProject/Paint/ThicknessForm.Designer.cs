namespace Paint
{
    partial class ThicknessForm
    {
        private System.ComponentModel.IContainer components = null;
        private NumericUpDown numericUpDownThickness;
        private Button buttonOK;
        private Button buttonCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            numericUpDownThickness = new NumericUpDown();
            buttonOK = new Button();
            buttonCancel = new Button();
            ((System.ComponentModel.ISupportInitialize)numericUpDownThickness).BeginInit();
            SuspendLayout();
            // 
            // numericUpDownThickness
            // 
            numericUpDownThickness.Location = new Point(42, 13);
            numericUpDownThickness.Name = "numericUpDownThickness";
            numericUpDownThickness.Size = new Size(106, 27);
            numericUpDownThickness.TabIndex = 0;
            // 
            // buttonOK
            // 
            buttonOK.Location = new Point(12, 54);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new Size(75, 35);
            buttonOK.TabIndex = 1;
            buttonOK.Text = "OK";
            buttonOK.UseVisualStyleBackColor = true;
            buttonOK.Click += buttonOK_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Location = new Point(98, 54);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(83, 35);
            buttonCancel.TabIndex = 2;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // ThicknessForm
            // 
            ClientSize = new Size(193, 98);
            Controls.Add(buttonCancel);
            Controls.Add(buttonOK);
            Controls.Add(numericUpDownThickness);
            Name = "ThicknessForm";
            Text = "Thickness";
            ((System.ComponentModel.ISupportInitialize)numericUpDownThickness).EndInit();
            ResumeLayout(false);
        }
    }
}

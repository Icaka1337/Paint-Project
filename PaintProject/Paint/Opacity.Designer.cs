namespace Paint
{
    partial class Opacity
    {
        private TrackBar trackBar1;

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
            trackBar1 = new TrackBar();
            btnOk = new Button();
            btnCancel = new Button();
            currentOpacityLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            SuspendLayout();
            // 
            // trackBar1
            // 
            trackBar1.Location = new Point(83, 37);
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(130, 56);
            trackBar1.TabIndex = 0;
            trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // btnOk
            // 
            btnOk.Location = new Point(25, 84);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(94, 29);
            btnOk.TabIndex = 1;
            btnOk.Text = "OK";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(163, 84);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(94, 29);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // currentOpacityLabel
            // 
            currentOpacityLabel.AutoSize = true;
            currentOpacityLabel.Location = new Point(132, 14);
            currentOpacityLabel.Name = "currentOpacityLabel";
            currentOpacityLabel.Size = new Size(33, 20);
            currentOpacityLabel.TabIndex = 3;
            currentOpacityLabel.Text = "255";
            // 
            // Opacity
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(281, 139);
            Controls.Add(currentOpacityLabel);
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            Controls.Add(trackBar1);
            Name = "Opacity";
            Text = "Opacity";
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnOk;
        private Button btnCancel;
        private Label currentOpacityLabel;
    }
}
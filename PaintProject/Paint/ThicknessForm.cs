using System;
using System.Windows.Forms;

namespace Paint
{
    public partial class ThicknessForm : Form
    {
        public int Thickness { get; private set; }

        public ThicknessForm(int currentThickness)
        {
            InitializeComponent();
            numericUpDownThickness.Value = currentThickness;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Thickness = (int)numericUpDownThickness.Value;
            DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
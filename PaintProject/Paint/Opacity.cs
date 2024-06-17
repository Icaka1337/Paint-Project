using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint
{
    public partial class Opacity : Form
    {
        public int newOpacity { get; private set; }
        public Opacity(int currentOpacity)
        {
            InitializeComponent();
            trackBar1.Minimum = 0;
            trackBar1.Maximum = 255;
            trackBar1.Value = currentOpacity;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            newOpacity = (int)trackBar1.Value;
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            double trackBarValue1 = trackBar1.Value;
            trackBarValue1 = (trackBarValue1 / 255) * 100;
            string trackbarValue = trackBarValue1.ToString("F1");
            currentOpacityLabel.Text = $"{trackbarValue}%";
        }
    }
}

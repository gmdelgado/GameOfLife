using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLife
{
    public partial class FromSeed : Form
    {
        int seed = 0;
        public FromSeed()
        {
            InitializeComponent();
            int seed = Properties.Settings.Default.RandFromSeed;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            FromSeed dlg = new FromSeed();
            dlg.seed = seed;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                // You only want to retrieve information
                // from the dialog if it was closed with
                // the OK button.
                seed = dlg.seed;
            }
        }

        public Int32 GetSeed()
        {
            return (Int32)numericUpDownSeed.Value;
        }

        public void SetSeed(Int32 seed)
        {
            numericUpDownSeed.Value = seed;
        }
    }
}

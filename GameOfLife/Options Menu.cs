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
    public partial class Options_Menu : Form
    {
        
        public Timer timer = new Timer();
        int _height;
        int _width;

        public Options_Menu()
        {
            InitializeComponent();

            // Reading the property
            timer.Interval = Properties.Settings.Default.TimerInterval;
            _height = Properties.Settings.Default.height;
            _width = Properties.Settings.Default.width;
        }

        public void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            //set time.interval to property and change it
            Options_Menu dlg = new Options_Menu();

            dlg.timer.Interval = timer.Interval;
            if (DialogResult.OK == dlg.ShowDialog())
            {
                // Update property
                timer.Interval = dlg.timer.Interval;
            }
        }
        private void numericUpDownWidthUniverse_ValueChanged(object sender, EventArgs e)
        {
            Options_Menu dlg = new Options_Menu();
            dlg._width = _width;

            if(DialogResult.OK == dlg.ShowDialog())
            {
                _width = dlg._width;
            }
        }
        private void numericUpDownHeightUniverse_ValueChanged(object sender, EventArgs e)
        {
            Options_Menu dlg = new Options_Menu();
            dlg._height = _height;
            if(DialogResult.OK == dlg.ShowDialog())
            {
                _height = dlg._height;
            }
        }

        public void Options_Menu_FormClosed_1(object sender, FormClosedEventArgs e)
        {
            // Update Property
            Properties.Settings.Default.TimerInterval = timer.Interval;
            Properties.Settings.Default.width = _width;
            Properties.Settings.Default.height = _height;

            Properties.Settings.Default.Save();
        }

        //accessor
        public int GetNumber()
        {
            return (int)numericUpDown1.Value;
        }

        //mutator
        public void SetNumber(int number)
        {
            numericUpDown1.Value = number;
        }

        public int GetWidth()
        {
            return (int)numericUpDownWidthUniverse.Value; 
        }

        public void SetWidth(int number)
        {
            numericUpDownWidthUniverse.Value = number;
        }

        public int GetHeight()
        {
            return (int)numericUpDownHeightUniverse.Value;
        }

        public void SetHeight(int number )
        {
            numericUpDownHeightUniverse.Value = number;
        }

    }
}

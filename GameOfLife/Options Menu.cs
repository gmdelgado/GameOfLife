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
       
        int time;
        int _height;
        int _width;

        public Options_Menu()
        {
            InitializeComponent();

            // Reading the property
            time = Properties.Settings.Default.TimerInterval;
            _height = Properties.Settings.Default.hei;
            _width = Properties.Settings.Default.wid;
        }

        public void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            //set time.interval to property and change it
            Options_Menu dlg = new Options_Menu();

            dlg.time = time;
            if (DialogResult.OK == dlg.ShowDialog())
            {
                // Update property
                time = dlg.time;
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
            Properties.Settings.Default.TimerInterval = time;
            Properties.Settings.Default.wid = _width;
            Properties.Settings.Default.hei = _height;

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

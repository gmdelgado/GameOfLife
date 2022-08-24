using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLife
{
    public partial class Form1 : Form
    {
        //static int wid = Properties.Settings.Default.wid;
        //static int hei = Properties.Settings.Default.hei;

        // The universe array
        bool[,] universe = new bool[Properties.Settings.Default.wid, Properties.Settings.Default.hei];
        bool[,] scratchPad = new bool[Properties.Settings.Default.wid, Properties.Settings.Default.hei];

        // Drawing colors
        Color gridColor = Properties.Settings.Default.gridcolor;
        Color cellColor = Properties.Settings.Default.cellcolor;

        // The Timer class
        Timer timer = new Timer();

        // Generation count
        int generations = 0;
        int cellCount = 0;
        int neighbors = 0;
        Int32 seed = 0;
        bool CountNeighbors = true;

        public Form1()
        {
            InitializeComponent();

            Color gridColor = Properties.Settings.Default.gridcolor;
            Color cellColor = Properties.Settings.Default.cellcolor;
            graphicsPanel1.BackColor = Properties.Settings.Default.PanelColor;
            seed = Properties.Settings.Default.RandFromSeed;
            // Setup the timer
            timer.Interval = Properties.Settings.Default.TimerInterval; // milliseconds
            timer.Tick += Timer_Tick;
            timer.Enabled = false; // start timer running //may need to change to false or will need to change to false
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CountNeighbors = true;
            if (checkBox1.Checked == false)
            {
                checkBox2.Checked = true;
                CountNeighbors = false;
            }
            else if (checkBox1.Checked == true)
            {
                checkBox2.Checked = false;
            }
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            //CountNeighbors = false;
            if (checkBox2.Checked == false)
            {
                checkBox1.Checked = true;
                //CountNeighbors = true;
            }
            else if (checkBox2.Checked == true)
            {
                checkBox1.Checked = false;
            }
        }


        // Calculate the next generation of cells
        private void NextGeneration()
        {


            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    if (CountNeighbors == true)
                    {
                        neighbors = CountNeighborsToroidal(x, y);
                    }
                    else
                    {
                        neighbors = CountNeighborsFinite(x, y); // send in the x and the y
                    }
                    // Apply the rules whether cell should live or die in next generation
                    if (universe[x, y] == true)
                    {
                        // cell dies with fewer than two neighbors
                        if (neighbors < 2)
                            scratchPad[x, y] = false;
                        // cell dies with more than three neighbors
                        if (neighbors > 3)
                            scratchPad[x, y] = false;
                        // any cell with 2 or 3 neighbors lives
                        if (neighbors == 2 || neighbors == 3)
                            scratchPad[x, y] = true;
                    }
                    else if (universe[x, y] == false)
                    {
                        if (neighbors == 3)
                            scratchPad[x, y] = true;
                    }

                }
            }

            // Copy from scratchPad to universe
            // clear out anything in the scratchPad that shouldnt be turned on the next time scratchPad executes
            bool[,] temp = universe;
            universe = scratchPad;
            scratchPad = temp;
            Array.Clear(scratchPad, 0, scratchPad.Length);
            // Increment generation count
            generations++;

            // Update status strip generations
            toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();

            // Invalidate the graphics panel
            graphicsPanel1.Invalidate();
        }

        // The event called by the timer every Interval milliseconds.
        private void Timer_Tick(object sender, EventArgs e)
        {
            NextGeneration();
        }

        private void Randomize()
        {
            //Create Dialog Box

            // Random rand = new Random();  Time
            // Takes a seed for seed
            Random rand = new Random();
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    // call next 
                    rand.Next();
                    // if random number == 0 turn on
                    if (rand.Next() == 0)
                    {
                        universe[x, y] = true;
                    }

                }
            }

            //validate when done
            graphicsPanel1.Invalidate();
        }

        private void graphicsPanel1_Paint(object sender, PaintEventArgs e)
        {
            int cel = 0;
            // Floats!! makes program look better
            // Calculate the width and height of each cell in pixels
            // CELL WIDTH = WINDOW WIDTH / NUMBER OF CELLS IN X
            int cellWidth = graphicsPanel1.ClientSize.Width / universe.GetLength(0);
            // CELL HEIGHT = WINDOW HEIGHT / NUMBER OF CELLS IN Y
            int cellHeight = graphicsPanel1.ClientSize.Height / universe.GetLength(1);
            Font font = new Font("Arial", 20f);
            // A Pen for drawing the grid lines (color, width)
            Pen gridPen = new Pen(gridColor, 1);

            // A Brush for filling living cells interiors (color)
            Brush cellBrush = new SolidBrush(cellColor);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            // Iterate through the universe in the y, top to bottom
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    // A rectangle to represent each cell in pixels
                    //RectangleF cellRect = RectangleF.Empty; //floats
                    Rectangle cellRect = Rectangle.Empty; //declaring rectangle

                    cellRect.X = x * cellWidth; //setting x
                    cellRect.Y = y * cellHeight; //setting y 
                    cellRect.Width = cellWidth;
                    cellRect.Height = cellHeight;
                    // Fill the cell with a brush if alive
                    if (universe[x, y] == true)
                    {
                        cel++;
                        cellCount = cel;
                        e.Graphics.FillRectangle(cellBrush, cellRect);
                        e.Graphics.DrawString(cellCount.ToString(), font, Brushes.Black, cellRect, stringFormat);
                    }

                    // Outline the cell with a pen                    
                    e.Graphics.DrawRectangle(gridPen, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);
                }
            }
            // Cleaning up pens and brushes
            toolStripStatusLabelCellCount.Text = "Alive = " + cellCount.ToString();
            gridPen.Dispose();
            cellBrush.Dispose();

            //never place invalidate inside the paint
        }

        private void graphicsPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            // If the left mouse button was clicked
            if (e.Button == MouseButtons.Left)
            {
                // Floats!!!!
                // Calculate the width and height of each cell in pixels
                int cellWidth = graphicsPanel1.ClientSize.Width / universe.GetLength(0);
                int cellHeight = graphicsPanel1.ClientSize.Height / universe.GetLength(1);

                // Calculate the cell that was clicked in
                // CELL X = MOUSE X / CELL WIDTH
                int x = e.X / cellWidth;
                // CELL Y = MOUSE Y / CELL HEIGHT
                int y = e.Y / cellHeight;

                // Toggle the cell's state
                universe[x, y] = !universe[x, y];

                // Tell Windows you need to repaint
                graphicsPanel1.Invalidate();
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetGen(ref generations);
            timer.Enabled = false;
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                for (int x = 0; x < universe.GetLength(0); x++)
                {

                    universe[x, y] = false;
                }
            }

            graphicsPanel1.Invalidate();
        }

        private void toolStripButtonNext_Click(object sender, EventArgs e)
        {
            //call next generation once
            NextGeneration();
        }

        private void toolStripButtonPause_Click(object sender, EventArgs e)
        {
            //only thing it needs to do is set timer to false
            timer.Enabled = false;
        }

        private void toolStripButtonStart_Click(object sender, EventArgs e)
        {
            //only thing it needs to do is set timer to true
            timer.Enabled = true;
        }

        private void cellColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();

            dlg.Color = cellColor;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                cellColor = dlg.Color;
                graphicsPanel1.Invalidate();
            }
        }

        public int ResetGen(ref int num)
        {
            num = 0;
            return num;
        }

        private int CountNeighborsFinite(int x, int y)
        {
            int count = 0;
            int xLen = universe.GetLength(0);
            int yLen = universe.GetLength(1);
            for (int yOffset = -1; yOffset <= 1; yOffset++)
            {
                for (int xOffset = -1; xOffset <= 1; xOffset++)
                {
                    int xCheck = x + xOffset;
                    int yCheck = y + yOffset;
                    // if xOffset and yOffset are both equal to 0 then continue
                    if (xOffset == 0 && yOffset == 0)
                    {
                        continue;
                    }
                    // if xCheck is less than 0 then continue
                    if (xCheck < 0)
                    {
                        continue;
                    }
                    // if yCheck is less than 0 then continue
                    if (yCheck < 0)
                    {
                        continue;
                    }
                    // if xCheck is greater than or equal too xLen then continue
                    if (xCheck >= xLen)
                    {
                        continue;
                    }
                    // if yCheck is greater than or equal too yLen then continue
                    if (yCheck >= yLen)
                    {
                        continue;
                    }

                    if (universe[xCheck, yCheck] == true) count++;
                }
            }
            return count;
        }

        private int CountNeighborsToroidal(int x, int y)
        {
            int count = 0;
            int xLen = universe.GetLength(0);
            int yLen = universe.GetLength(1);
            for (int yOffset = -1; yOffset <= 1; yOffset++)
            {
                for (int xOffset = -1; xOffset <= 1; xOffset++)
                {
                    int xCheck = x + xOffset;
                    int yCheck = y + yOffset;
                    // if xOffset and yOffset are both equal to 0 then continue
                    if (xOffset == 0 && yOffset == 0)
                        continue;
                    // if xCheck is less than 0 then set to xLen - 1
                    if (xCheck < 0)
                        xCheck = xLen - 1;
                    // if yCheck is less than 0 then set to yLen - 1
                    if (yCheck < 0)
                        yCheck = yLen - 1;
                    // if xCheck is greater than or equal too xLen then set to 0
                    if (xCheck >= xLen)
                        xCheck = 0;
                    // if yCheck is greater than or equal too yLen then set to 0
                    if (yCheck >= yLen)
                        yCheck = 0;

                    if (universe[xCheck, yCheck] == true) count++;
                }
            }
            return count;
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "All Files|*.*|Cells|*.cells";
            dlg.FilterIndex = 2; dlg.DefaultExt = "cells";


            if (DialogResult.OK == dlg.ShowDialog())
            {
                StreamWriter writer = new StreamWriter(dlg.FileName);

                // Write any comments you want to include first.
                // Prefix all comment strings with an exclamation point.
                // Use WriteLine to write the strings to the file. 
                // It appends a CRLF for you.
                writer.WriteLine("!This is my comment.");

                // Iterate through the universe one row at a time.
                for (int y = 0; y < universe.GetLength(1); y++)
                {
                    // Create a string to represent the current row.
                    String currentRow = string.Empty;

                    // Iterate through the current row one cell at a time.
                    for (int x = 0; x < universe.GetLength(0); x++)
                    {
                        // If the universe[x,y] is alive then append 'O' (capital O)
                        // to the row string.
                        if (universe[x, y] == true)
                        {
                            currentRow += 'O';
                        }
                        // Else if the universe[x,y] is dead then append '.' (period)
                        // to the row string.
                        else if (universe[x, y] == false)
                        {
                            currentRow += '.';
                        }
                    }

                    // Once the current row has been read through and the 
                    // string constructed then write it to the file using WriteLine.
                    writer.WriteLine(currentRow);
                }

                // After all rows and columns have been written then close the file.
                writer.Close();
            }
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "All Files|*.*|Cells|*.cells";
            dlg.FilterIndex = 2;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                StreamReader reader = new StreamReader(dlg.FileName);

                // Create a couple variables to calculate the width and height
                // of the data in the file.
                int maxWidth = 0;
                int maxHeight = 0;

                // Iterate through the file once to get its size.
                while (!reader.EndOfStream)
                {
                    // Read one row at a time.
                    string row = reader.ReadLine();

                    // If the row begins with '!' then it is a comment
                    // and should be ignored.
                    if (row[0] == '!')
                    {
                        continue;
                    }
                    // If the row is not a comment then it is a row of cells.
                    // Increment the maxHeight variable for each row read.
                    if (row[0] != '!')
                    {
                        for (int i = 0; i < row.Count<char>(); i++)
                        {
                            maxHeight++;
                        }
                    }

                    // Get the length of the current row string
                    // and adjust the maxWidth variable if necessary.
                    maxWidth = row.Length;
                }

                // Resize the current universe and scratchPad
                // to the width and height of the file calculated above.
                universe = new bool[maxWidth, maxHeight / maxWidth];
                scratchPad = new bool[maxWidth, maxHeight / maxWidth];


                // Reset the file pointer back to the beginning of the file.
                reader.BaseStream.Seek(0, SeekOrigin.Begin);

                    int yPos = 0;
                    int xPos1 = 0;
                // Iterate through the file again, this time reading in the cells.
                while (!reader.EndOfStream)
                {
                    // Read one row at a time.
                    string row = reader.ReadLine();

                    // If the row begins with '!' then
                    // it is a comment and should be ignored.
                    if (row[0] == '!')
                    {
                        continue;
                    }
                    if (xPos1 == maxWidth-1)
                    {
                        yPos++;
                    }
                    // If the row is not a comment then 
                    // it is a row of cells and needs to be iterated through.

                    for (int xPos = 0; xPos < row.Length; xPos++)
                    {
                        xPos1 = xPos;

                        // If row[xPos] is a 'O' (capital O) then
                        // set the corresponding cell in the universe to alive.
                        if (row[xPos] == 'O')
                        {
                            universe[xPos, yPos] = true;
                        }
                        // If row[xPos] is a '.' (period) then
                        // set the corresponding cell in the universe to dead.
                        if (row[xPos] == '.')
                        {
                            universe[xPos, yPos] = false;

                        }



                        graphicsPanel1.Invalidate();
                    }

                }
                // Close the file.
                reader.Close();
            }
        }

        private void gridColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();

            dlg.Color = gridColor;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                gridColor = dlg.Color;
                graphicsPanel1.Invalidate();
            }
        }

        private void optionsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int tempNum = Properties.Settings.Default.hei;
            int tempNum2 = Properties.Settings.Default.wid;

            Options_Menu dlg = new Options_Menu();
            //dlg.timer.Interval = timer.Interval;
            dlg.SetNumber(Properties.Settings.Default.TimerInterval);
            dlg.SetHeight(Properties.Settings.Default.hei);
            dlg.SetWidth(Properties.Settings.Default.wid);

            if (DialogResult.OK == dlg.ShowDialog())
            {
                // You only want to retrieve information
                // from the dialog if it was closed with
                // the OK button.
                Properties.Settings.Default.TimerInterval = dlg.GetNumber();
                Properties.Settings.Default.hei = dlg.GetHeight();
                Properties.Settings.Default.wid = dlg.GetWidth();
            }

            if (Properties.Settings.Default.hei == tempNum || Properties.Settings.Default.wid == tempNum2)
            {
            }
            else
            {
                universe = new bool[Properties.Settings.Default.wid, Properties.Settings.Default.hei];
                scratchPad = new bool[Properties.Settings.Default.wid, Properties.Settings.Default.hei];
                graphicsPanel1.Invalidate();
            }
            timer.Interval = Properties.Settings.Default.TimerInterval;
        }
        private void backColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();

            dlg.Color = graphicsPanel1.BackColor;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                graphicsPanel1.BackColor = dlg.Color;
            }
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close(); //shuts down the program
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.gridcolor = gridColor;
            Properties.Settings.Default.cellcolor = cellColor;
            Properties.Settings.Default.PanelColor = graphicsPanel1.BackColor;
            Properties.Settings.Default.Save();
        }

        private void fromTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Create Dialog Box

            // Random rand = new Random();  Time
            //Random rand = new Random(seed)
            // Takes a seed for seed
            Array.Clear(universe, 0, universe.Length);

            Random rand = new Random();
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    // call next 
                    int i = rand.Next(0, 3);
                    // if random number == 0 turn on
                    if (i == 0)
                    {
                        universe[x, y] = true;
                    }

                }
            }

            //validate when done
            graphicsPanel1.Invalidate();
        }

        private void fromSeedToolStripMenuItem_Click(object sender, EventArgs e)
        {

            FromSeed dlg = new FromSeed();
            dlg.SetSeed(Properties.Settings.Default.RandFromSeed);

            if (DialogResult.OK == dlg.ShowDialog())
            {
                // You only want to retrieve information
                // from the dialog if it was closed with
                // the OK button.
                Properties.Settings.Default.RandFromSeed = dlg.GetSeed();
            }


            //Create Dialog Box
            Random rand = new Random(seed);
            // Takes a seed for seed
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    // call next 
                    int i = rand.Next(0, 2);
                    // if random number == 0 turn on
                    if (i == 0)
                    {
                        universe[x, y] = true;
                    }

                }
            }

            //validate when done
            graphicsPanel1.Invalidate();
        }
    }
}

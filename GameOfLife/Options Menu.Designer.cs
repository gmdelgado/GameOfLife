
namespace GameOfLife
{
    partial class Options_Menu
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownWidthUniverse = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownHeightUniverse = new System.Windows.Forms.NumericUpDown();
            this.TimerInterval = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidthUniverse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeightUniverse)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(126, 128);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(207, 128);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(161, 31);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 2;
            // 
            // numericUpDownWidthUniverse
            // 
            this.numericUpDownWidthUniverse.Location = new System.Drawing.Point(161, 58);
            this.numericUpDownWidthUniverse.Name = "numericUpDownWidthUniverse";
            this.numericUpDownWidthUniverse.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownWidthUniverse.TabIndex = 3;
            this.numericUpDownWidthUniverse.ValueChanged += new System.EventHandler(this.numericUpDownWidthUniverse_ValueChanged);
            // 
            // numericUpDownHeightUniverse
            // 
            this.numericUpDownHeightUniverse.Location = new System.Drawing.Point(161, 85);
            this.numericUpDownHeightUniverse.Name = "numericUpDownHeightUniverse";
            this.numericUpDownHeightUniverse.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownHeightUniverse.TabIndex = 4;
            this.numericUpDownHeightUniverse.ValueChanged += new System.EventHandler(this.numericUpDownHeightUniverse_ValueChanged);
            // 
            // TimerInterval
            // 
            this.TimerInterval.AutoSize = true;
            this.TimerInterval.Location = new System.Drawing.Point(24, 33);
            this.TimerInterval.Name = "TimerInterval";
            this.TimerInterval.Size = new System.Drawing.Size(131, 13);
            this.TimerInterval.TabIndex = 5;
            this.TimerInterval.Text = "Timer Interval Milliseconds";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Width of Universe in Cells";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Height of Universe in Cells";
            // 
            // Options_Menu
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(307, 176);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TimerInterval);
            this.Controls.Add(this.numericUpDownHeightUniverse);
            this.Controls.Add(this.numericUpDownWidthUniverse);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Options_Menu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Options_Menu";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Options_Menu_FormClosed_1);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidthUniverse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeightUniverse)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown numericUpDownWidthUniverse;
        private System.Windows.Forms.NumericUpDown numericUpDownHeightUniverse;
        private System.Windows.Forms.Label TimerInterval;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}
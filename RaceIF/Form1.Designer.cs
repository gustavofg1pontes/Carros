namespace RaceIF
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
            this.components = new System.ComponentModel.Container();
            this.carBox = new System.Windows.Forms.PictureBox();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.carBox)).BeginInit();
            this.SuspendLayout();
            // 
            // carBox
            // 
            this.carBox.Image = global::RaceIF.Properties.Resources.TAXI_CLEAN_ALLD0000;
            this.carBox.Location = new System.Drawing.Point(50, 30);
            this.carBox.Margin = new System.Windows.Forms.Padding(0);
            this.carBox.Name = "carBox";
            this.carBox.Size = new System.Drawing.Size(97, 53);
            this.carBox.TabIndex = 0;
            this.carBox.TabStop = false;
            // 
            // gameTimer
            // 
            this.gameTimer.Enabled = true;
            this.gameTimer.Interval = 50;
            this.gameTimer.Tick += new System.EventHandler(this.MoveTimerEvent);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.carBox);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "RaceIF";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyIsDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyIsUp);
            ((System.ComponentModel.ISupportInitialize)(this.carBox)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Timer gameTimer;

        private System.Windows.Forms.PictureBox carBox;

        #endregion
    }
}
namespace PwTouchInputProvider.Forms
{
    partial class Calibration
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
            this.label1 = new System.Windows.Forms.Label();
            this.panel = new System.Windows.Forms.Panel();
            this.pbProcessedCameraFrame = new System.Windows.Forms.PictureBox();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbProcessedCameraFrame)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(238, 65);
            this.label1.TabIndex = 0;
            this.label1.Text = "Houd je vinger op het punt totdat hij groen wordt.\r\n\r\nCalibratie starten: [G]\r\nDe" +
                "tector herstarten: [B]\r\nOpslaan en afsluiten: [ESC]";
            // 
            // panel
            // 
            this.panel.Controls.Add(this.pbProcessedCameraFrame);
            this.panel.Controls.Add(this.label1);
            this.panel.Location = new System.Drawing.Point(80, 79);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(340, 350);
            this.panel.TabIndex = 1;
            // 
            // pbProcessedCameraFrame
            // 
            this.pbProcessedCameraFrame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbProcessedCameraFrame.Location = new System.Drawing.Point(10, 107);
            this.pbProcessedCameraFrame.Name = "pbProcessedCameraFrame";
            this.pbProcessedCameraFrame.Size = new System.Drawing.Size(320, 240);
            this.pbProcessedCameraFrame.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbProcessedCameraFrame.TabIndex = 1;
            this.pbProcessedCameraFrame.TabStop = false;
            // 
            // Calibration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(500, 500);
            this.Controls.Add(this.panel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Calibration";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Calibration_FormClosed);
            this.Load += new System.EventHandler(this.Calibration_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Calibration_KeyUp);
            this.Resize += new System.EventHandler(this.Calibration_Resize);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbProcessedCameraFrame)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.PictureBox pbProcessedCameraFrame;
    }
}
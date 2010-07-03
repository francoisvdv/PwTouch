namespace PwTouchApp.Forms
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.capturedImageBox = new Emgu.CV.UI.ImageBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.foregroundImageBox = new Emgu.CV.UI.ImageBox();
            this.processingTimer = new System.Windows.Forms.Timer(this.components);
            this.btnToggleProcessing = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.capturedImageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.foregroundImageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // capturedImageBox
            // 
            this.capturedImageBox.Location = new System.Drawing.Point(12, 67);
            this.capturedImageBox.Name = "capturedImageBox";
            this.capturedImageBox.Size = new System.Drawing.Size(320, 240);
            this.capturedImageBox.TabIndex = 0;
            this.capturedImageBox.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Profiel Werkstuk: Touch";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(246, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Menno Veen, Huib Donkers, Francois van der Ven";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "label3";
            // 
            // foregroundImageBox
            // 
            this.foregroundImageBox.Location = new System.Drawing.Point(338, 67);
            this.foregroundImageBox.Name = "foregroundImageBox";
            this.foregroundImageBox.Size = new System.Drawing.Size(320, 240);
            this.foregroundImageBox.TabIndex = 4;
            this.foregroundImageBox.TabStop = false;
            // 
            // processingTimer
            // 
            this.processingTimer.Interval = 1;
            this.processingTimer.Tick += new System.EventHandler(this.processingTimer_Tick);
            // 
            // btnToggleProcessing
            // 
            this.btnToggleProcessing.Location = new System.Drawing.Point(12, 313);
            this.btnToggleProcessing.Name = "btnToggleProcessing";
            this.btnToggleProcessing.Size = new System.Drawing.Size(109, 23);
            this.btnToggleProcessing.TabIndex = 5;
            this.btnToggleProcessing.Text = "Toggle Processing";
            this.btnToggleProcessing.UseVisualStyleBackColor = true;
            this.btnToggleProcessing.Click += new System.EventHandler(this.btnToggleProcessing_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 343);
            this.Controls.Add(this.btnToggleProcessing);
            this.Controls.Add(this.foregroundImageBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.capturedImageBox);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.capturedImageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.foregroundImageBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Emgu.CV.UI.ImageBox capturedImageBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private Emgu.CV.UI.ImageBox foregroundImageBox;
        private System.Windows.Forms.Timer processingTimer;
        private System.Windows.Forms.Button btnToggleProcessing;
    }
}


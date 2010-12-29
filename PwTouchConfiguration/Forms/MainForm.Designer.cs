namespace PwTouchInputProvider.Forms
{
    partial class MainForm
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
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pbProcessedCameraFrame = new System.Windows.Forms.PictureBox();
            this.pbNewCameraFrame = new System.Windows.Forms.PictureBox();
            this.pbProcessedFrame = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbDetector = new System.Windows.Forms.ComboBox();
            this.cbSave = new System.Windows.Forms.Button();
            this.cbDelete = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnRestartDetector = new System.Windows.Forms.Button();
            this.btnCalibrate = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.nudSkipFrames = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbCamera = new System.Windows.Forms.ComboBox();
            this.btnCameraSettings = new System.Windows.Forms.Button();
            this.cbCameraMode = new System.Windows.Forms.ComboBox();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbProcessedCameraFrame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbNewCameraFrame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbProcessedFrame)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSkipFrames)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer2.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer2.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer2.Size = new System.Drawing.Size(834, 520);
            this.splitContainer2.SplitterDistance = 641;
            this.splitContainer2.TabIndex = 15;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Controls.Add(this.cbDetector);
            this.splitContainer1.Panel2.Controls.Add(this.cbSave);
            this.splitContainer1.Panel2.Controls.Add(this.cbDelete);
            this.splitContainer1.Size = new System.Drawing.Size(641, 520);
            this.splitContainer1.SplitterDistance = 165;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 15;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33332F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.Controls.Add(this.pbProcessedCameraFrame, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.pbNewCameraFrame, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pbProcessedFrame, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(639, 163);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // pbProcessedCameraFrame
            // 
            this.pbProcessedCameraFrame.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pbProcessedCameraFrame.BackColor = System.Drawing.Color.Transparent;
            this.pbProcessedCameraFrame.Location = new System.Drawing.Point(428, 3);
            this.pbProcessedCameraFrame.Name = "pbProcessedCameraFrame";
            this.pbProcessedCameraFrame.Size = new System.Drawing.Size(208, 157);
            this.pbProcessedCameraFrame.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbProcessedCameraFrame.TabIndex = 4;
            this.pbProcessedCameraFrame.TabStop = false;
            // 
            // pbNewCameraFrame
            // 
            this.pbNewCameraFrame.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pbNewCameraFrame.BackColor = System.Drawing.Color.Transparent;
            this.pbNewCameraFrame.Location = new System.Drawing.Point(3, 3);
            this.pbNewCameraFrame.Name = "pbNewCameraFrame";
            this.pbNewCameraFrame.Size = new System.Drawing.Size(206, 157);
            this.pbNewCameraFrame.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbNewCameraFrame.TabIndex = 0;
            this.pbNewCameraFrame.TabStop = false;
            // 
            // pbProcessedFrame
            // 
            this.pbProcessedFrame.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pbProcessedFrame.BackColor = System.Drawing.Color.Transparent;
            this.pbProcessedFrame.Location = new System.Drawing.Point(215, 3);
            this.pbProcessedFrame.Name = "pbProcessedFrame";
            this.pbProcessedFrame.Size = new System.Drawing.Size(207, 157);
            this.pbProcessedFrame.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbProcessedFrame.TabIndex = 2;
            this.pbProcessedFrame.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Location = new System.Drawing.Point(3, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(633, 316);
            this.panel1.TabIndex = 11;
            // 
            // cbDetector
            // 
            this.cbDetector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbDetector.FormattingEnabled = true;
            this.cbDetector.Location = new System.Drawing.Point(3, 5);
            this.cbDetector.Name = "cbDetector";
            this.cbDetector.Size = new System.Drawing.Size(471, 21);
            this.cbDetector.TabIndex = 10;
            this.cbDetector.SelectedIndexChanged += new System.EventHandler(this.cbDetector_SelectedIndexChanged);
            // 
            // cbSave
            // 
            this.cbSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSave.Location = new System.Drawing.Point(480, 3);
            this.cbSave.Name = "cbSave";
            this.cbSave.Size = new System.Drawing.Size(75, 23);
            this.cbSave.TabIndex = 8;
            this.cbSave.Text = "Opslaan";
            this.cbSave.UseVisualStyleBackColor = true;
            this.cbSave.Click += new System.EventHandler(this.cbSave_Click);
            // 
            // cbDelete
            // 
            this.cbDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbDelete.Location = new System.Drawing.Point(561, 3);
            this.cbDelete.Name = "cbDelete";
            this.cbDelete.Size = new System.Drawing.Size(75, 23);
            this.cbDelete.TabIndex = 9;
            this.cbDelete.Text = "Verwijder";
            this.cbDelete.UseVisualStyleBackColor = true;
            this.cbDelete.Click += new System.EventHandler(this.cbDelete_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnRestartDetector);
            this.groupBox2.Controls.Add(this.btnCalibrate);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.nudSkipFrames);
            this.groupBox2.Location = new System.Drawing.Point(3, 121);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(183, 107);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detector";
            // 
            // btnRestartDetector
            // 
            this.btnRestartDetector.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRestartDetector.Location = new System.Drawing.Point(6, 19);
            this.btnRestartDetector.Name = "btnRestartDetector";
            this.btnRestartDetector.Size = new System.Drawing.Size(168, 23);
            this.btnRestartDetector.TabIndex = 19;
            this.btnRestartDetector.Text = "Herstarten";
            this.btnRestartDetector.UseVisualStyleBackColor = true;
            this.btnRestartDetector.Click += new System.EventHandler(this.btnRestartDetector_Click);
            // 
            // btnCalibrate
            // 
            this.btnCalibrate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCalibrate.Location = new System.Drawing.Point(6, 48);
            this.btnCalibrate.Name = "btnCalibrate";
            this.btnCalibrate.Size = new System.Drawing.Size(168, 23);
            this.btnCalibrate.TabIndex = 18;
            this.btnCalibrate.Text = "Calibreren";
            this.btnCalibrate.UseVisualStyleBackColor = true;
            this.btnCalibrate.Click += new System.EventHandler(this.btnCalibrate_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Frames overslaan:";
            // 
            // nudSkipFrames
            // 
            this.nudSkipFrames.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudSkipFrames.Location = new System.Drawing.Point(111, 77);
            this.nudSkipFrames.Name = "nudSkipFrames";
            this.nudSkipFrames.Size = new System.Drawing.Size(63, 20);
            this.nudSkipFrames.TabIndex = 22;
            this.nudSkipFrames.ValueChanged += new System.EventHandler(this.nudSkipFrames_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cbCamera);
            this.groupBox1.Controls.Add(this.btnCameraSettings);
            this.groupBox1.Controls.Add(this.cbCameraMode);
            this.groupBox1.Location = new System.Drawing.Point(3, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(183, 103);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Camera";
            // 
            // cbCamera
            // 
            this.cbCamera.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCamera.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCamera.FormattingEnabled = true;
            this.cbCamera.Location = new System.Drawing.Point(6, 19);
            this.cbCamera.Name = "cbCamera";
            this.cbCamera.Size = new System.Drawing.Size(171, 21);
            this.cbCamera.TabIndex = 20;
            this.cbCamera.SelectedIndexChanged += new System.EventHandler(this.cbCamera_SelectedIndexChanged);
            // 
            // btnCameraSettings
            // 
            this.btnCameraSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCameraSettings.Location = new System.Drawing.Point(6, 71);
            this.btnCameraSettings.Name = "btnCameraSettings";
            this.btnCameraSettings.Size = new System.Drawing.Size(171, 23);
            this.btnCameraSettings.TabIndex = 17;
            this.btnCameraSettings.Text = "Meer Camera Instellingen";
            this.btnCameraSettings.UseVisualStyleBackColor = true;
            this.btnCameraSettings.Click += new System.EventHandler(this.btnCameraSettings_Click);
            // 
            // cbCameraMode
            // 
            this.cbCameraMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCameraMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCameraMode.FormattingEnabled = true;
            this.cbCameraMode.Location = new System.Drawing.Point(6, 46);
            this.cbCameraMode.Name = "cbCameraMode";
            this.cbCameraMode.Size = new System.Drawing.Size(171, 21);
            this.cbCameraMode.TabIndex = 21;
            this.cbCameraMode.SelectedIndexChanged += new System.EventHandler(this.cbCameraMode_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(834, 520);
            this.Controls.Add(this.splitContainer2);
            this.DoubleBuffered = true;
            this.Name = "MainForm";
            this.Text = "PwTouchApp Configuratie";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbProcessedCameraFrame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbNewCameraFrame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbProcessedFrame)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSkipFrames)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pbProcessedCameraFrame;
        private System.Windows.Forms.PictureBox pbNewCameraFrame;
        private System.Windows.Forms.PictureBox pbProcessedFrame;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbDetector;
        private System.Windows.Forms.Button cbSave;
        private System.Windows.Forms.Button cbDelete;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudSkipFrames;
        private System.Windows.Forms.ComboBox cbCameraMode;
        private System.Windows.Forms.ComboBox cbCamera;
        private System.Windows.Forms.Button btnRestartDetector;
        private System.Windows.Forms.Button btnCalibrate;
        private System.Windows.Forms.Button btnCameraSettings;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;





    }
}
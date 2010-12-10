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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cbCameraMode = new System.Windows.Forms.ComboBox();
            this.cbCamera = new System.Windows.Forms.ComboBox();
            this.btnRestartDetector = new System.Windows.Forms.Button();
            this.btnCalibrate = new System.Windows.Forms.Button();
            this.btnCameraSettings = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nudSkipFrames = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.cbSave = new System.Windows.Forms.Button();
            this.cbDelete = new System.Windows.Forms.Button();
            this.cbDetector = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSkipFrames)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(834, 520);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.nudSkipFrames);
            this.tabPage1.Controls.Add(this.cbCameraMode);
            this.tabPage1.Controls.Add(this.cbCamera);
            this.tabPage1.Controls.Add(this.btnRestartDetector);
            this.tabPage1.Controls.Add(this.btnCalibrate);
            this.tabPage1.Controls.Add(this.btnCameraSettings);
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(826, 494);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Camera Configuratie";
            // 
            // cbCameraMode
            // 
            this.cbCameraMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCameraMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCameraMode.FormattingEnabled = true;
            this.cbCameraMode.Location = new System.Drawing.Point(652, 33);
            this.cbCameraMode.Name = "cbCameraMode";
            this.cbCameraMode.Size = new System.Drawing.Size(168, 21);
            this.cbCameraMode.TabIndex = 7;
            this.cbCameraMode.SelectedIndexChanged += new System.EventHandler(this.cbCameraMode_SelectedIndexChanged);
            // 
            // cbCamera
            // 
            this.cbCamera.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCamera.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCamera.FormattingEnabled = true;
            this.cbCamera.Location = new System.Drawing.Point(652, 6);
            this.cbCamera.Name = "cbCamera";
            this.cbCamera.Size = new System.Drawing.Size(168, 21);
            this.cbCamera.TabIndex = 5;
            this.cbCamera.SelectedIndexChanged += new System.EventHandler(this.cbCamera_SelectedIndexChanged);
            // 
            // btnRestartDetector
            // 
            this.btnRestartDetector.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRestartDetector.Location = new System.Drawing.Point(652, 99);
            this.btnRestartDetector.Name = "btnRestartDetector";
            this.btnRestartDetector.Size = new System.Drawing.Size(168, 23);
            this.btnRestartDetector.TabIndex = 4;
            this.btnRestartDetector.Text = "Detector Herstarten";
            this.btnRestartDetector.UseVisualStyleBackColor = true;
            this.btnRestartDetector.Click += new System.EventHandler(this.btnRestartDetector_Click);
            // 
            // btnCalibrate
            // 
            this.btnCalibrate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCalibrate.Location = new System.Drawing.Point(652, 128);
            this.btnCalibrate.Name = "btnCalibrate";
            this.btnCalibrate.Size = new System.Drawing.Size(168, 23);
            this.btnCalibrate.TabIndex = 3;
            this.btnCalibrate.Text = "Calibreren";
            this.btnCalibrate.UseVisualStyleBackColor = true;
            this.btnCalibrate.Click += new System.EventHandler(this.btnCalibrate_Click);
            // 
            // btnCameraSettings
            // 
            this.btnCameraSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCameraSettings.Location = new System.Drawing.Point(652, 70);
            this.btnCameraSettings.Name = "btnCameraSettings";
            this.btnCameraSettings.Size = new System.Drawing.Size(168, 23);
            this.btnCameraSettings.TabIndex = 2;
            this.btnCameraSettings.Text = "Meer Camera Instellingen";
            this.btnCameraSettings.UseVisualStyleBackColor = true;
            this.btnCameraSettings.Click += new System.EventHandler(this.btnCameraSettings_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(6, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(640, 480);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Controls.Add(this.cbDetector);
            this.tabPage2.Controls.Add(this.cbDelete);
            this.tabPage2.Controls.Add(this.cbSave);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(826, 494);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Detectors";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.pictureBox2);
            this.groupBox1.Controls.Add(this.pictureBox3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.Location = new System.Drawing.Point(662, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(161, 488);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Preview";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Origineel:";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(6, 35);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(150, 150);
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(6, 204);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(150, 150);
            this.pictureBox3.TabIndex = 2;
            this.pictureBox3.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 188);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Resultaat:";
            // 
            // nudSkipFrames
            // 
            this.nudSkipFrames.Location = new System.Drawing.Point(751, 157);
            this.nudSkipFrames.Name = "nudSkipFrames";
            this.nudSkipFrames.Size = new System.Drawing.Size(69, 20);
            this.nudSkipFrames.TabIndex = 8;
            this.nudSkipFrames.ValueChanged += new System.EventHandler(this.nudSkipFrames_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(652, 159);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Frames overslaan:";
            // 
            // cbSave
            // 
            this.cbSave.Location = new System.Drawing.Point(500, 4);
            this.cbSave.Name = "cbSave";
            this.cbSave.Size = new System.Drawing.Size(75, 23);
            this.cbSave.TabIndex = 8;
            this.cbSave.Text = "Opslaan";
            this.cbSave.UseVisualStyleBackColor = true;
            // 
            // cbDelete
            // 
            this.cbDelete.Location = new System.Drawing.Point(581, 4);
            this.cbDelete.Name = "cbDelete";
            this.cbDelete.Size = new System.Drawing.Size(75, 23);
            this.cbDelete.TabIndex = 9;
            this.cbDelete.Text = "Verwijder";
            this.cbDelete.UseVisualStyleBackColor = true;
            // 
            // cbDetector
            // 
            this.cbDetector.FormattingEnabled = true;
            this.cbDetector.Location = new System.Drawing.Point(8, 6);
            this.cbDetector.Name = "cbDetector";
            this.cbDetector.Size = new System.Drawing.Size(486, 21);
            this.cbDetector.TabIndex = 10;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(8, 33);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(648, 453);
            this.panel1.TabIndex = 11;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 520);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainForm";
            this.Text = "PwTouchApp Configuratie";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSkipFrames)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ComboBox cbCamera;
        private System.Windows.Forms.Button btnRestartDetector;
        private System.Windows.Forms.Button btnCalibrate;
        private System.Windows.Forms.Button btnCameraSettings;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbCameraMode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudSkipFrames;
        private System.Windows.Forms.ComboBox cbDetector;
        private System.Windows.Forms.Button cbDelete;
        private System.Windows.Forms.Button cbSave;
        private System.Windows.Forms.Panel panel1;


    }
}
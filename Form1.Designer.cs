namespace AutomationTool {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.browseBtn1 = new System.Windows.Forms.Button();
            this.ofd1 = new System.Windows.Forms.OpenFileDialog();
            this.fileLocationTextBox1 = new System.Windows.Forms.TextBox();
            this.createBtn = new System.Windows.Forms.Button();
            this.ofd2 = new System.Windows.Forms.OpenFileDialog();
            this.pkgrNameTextBox = new System.Windows.Forms.TextBox();
            this.pimsIdTextBox = new System.Windows.Forms.TextBox();
            this.appNameTextBox = new System.Windows.Forms.TextBox();
            this.pkgNameTextBox = new System.Windows.Forms.TextBox();
            this.pkgVerTextBox = new System.Windows.Forms.TextBox();
            this.appVerTextBox = new System.Windows.Forms.TextBox();
            this.pimsIdLabel = new System.Windows.Forms.Label();
            this.appNameLabel = new System.Windows.Forms.Label();
            this.appVerLabel = new System.Windows.Forms.Label();
            this.pkgNameLabel = new System.Windows.Forms.Label();
            this.pkgVerLabel = new System.Windows.Forms.Label();
            this.pkgrNameLabel = new System.Windows.Forms.Label();
            this.brwMsiLabel = new System.Windows.Forms.Label();
            this.logBox = new System.Windows.Forms.RichTextBox();
            this.customMsiCheckBox = new System.Windows.Forms.CheckBox();
            this.button3264bit = new System.Windows.Forms.RadioButton();
            this.button64bit = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.loadingAnimation = new System.Windows.Forms.PictureBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.loadingAnimation)).BeginInit();
            this.SuspendLayout();
            // 
            // browseBtn1
            // 
            this.browseBtn1.Location = new System.Drawing.Point(260, 22);
            this.browseBtn1.Name = "browseBtn1";
            this.browseBtn1.Size = new System.Drawing.Size(34, 23);
            this.browseBtn1.TabIndex = 1;
            this.browseBtn1.Text = "...";
            this.browseBtn1.UseVisualStyleBackColor = true;
            this.browseBtn1.Click += new System.EventHandler(this.browseBtn_Click);
            // 
            // ofd1
            // 
            this.ofd1.ValidateNames = false;
            // 
            // fileLocationTextBox1
            // 
            this.fileLocationTextBox1.Enabled = false;
            this.fileLocationTextBox1.Location = new System.Drawing.Point(112, 24);
            this.fileLocationTextBox1.Name = "fileLocationTextBox1";
            this.fileLocationTextBox1.Size = new System.Drawing.Size(142, 20);
            this.fileLocationTextBox1.TabIndex = 0;
            this.fileLocationTextBox1.Enter += new System.EventHandler(this.fileLocationTextBox1_Enter);
            // 
            // createBtn
            // 
            this.createBtn.Location = new System.Drawing.Point(111, 277);
            this.createBtn.Name = "createBtn";
            this.createBtn.Size = new System.Drawing.Size(182, 23);
            this.createBtn.TabIndex = 8;
            this.createBtn.Text = "Create";
            this.createBtn.UseVisualStyleBackColor = true;
            this.createBtn.Click += new System.EventHandler(this.createBtn_Click);
            // 
            // pkgrNameTextBox
            // 
            this.pkgrNameTextBox.Location = new System.Drawing.Point(111, 235);
            this.pkgrNameTextBox.Name = "pkgrNameTextBox";
            this.pkgrNameTextBox.Size = new System.Drawing.Size(182, 20);
            this.pkgrNameTextBox.TabIndex = 7;
            // 
            // pimsIdTextBox
            // 
            this.pimsIdTextBox.Location = new System.Drawing.Point(111, 105);
            this.pimsIdTextBox.Name = "pimsIdTextBox";
            this.pimsIdTextBox.Size = new System.Drawing.Size(182, 20);
            this.pimsIdTextBox.TabIndex = 2;
            // 
            // appNameTextBox
            // 
            this.appNameTextBox.Location = new System.Drawing.Point(111, 131);
            this.appNameTextBox.Name = "appNameTextBox";
            this.appNameTextBox.Size = new System.Drawing.Size(182, 20);
            this.appNameTextBox.TabIndex = 3;
            // 
            // pkgNameTextBox
            // 
            this.pkgNameTextBox.Location = new System.Drawing.Point(111, 183);
            this.pkgNameTextBox.Name = "pkgNameTextBox";
            this.pkgNameTextBox.Size = new System.Drawing.Size(182, 20);
            this.pkgNameTextBox.TabIndex = 5;
            // 
            // pkgVerTextBox
            // 
            this.pkgVerTextBox.Location = new System.Drawing.Point(111, 209);
            this.pkgVerTextBox.Name = "pkgVerTextBox";
            this.pkgVerTextBox.Size = new System.Drawing.Size(182, 20);
            this.pkgVerTextBox.TabIndex = 6;
            // 
            // appVerTextBox
            // 
            this.appVerTextBox.Location = new System.Drawing.Point(111, 157);
            this.appVerTextBox.Name = "appVerTextBox";
            this.appVerTextBox.Size = new System.Drawing.Size(182, 20);
            this.appVerTextBox.TabIndex = 4;
            // 
            // pimsIdLabel
            // 
            this.pimsIdLabel.AutoSize = true;
            this.pimsIdLabel.Location = new System.Drawing.Point(9, 108);
            this.pimsIdLabel.Name = "pimsIdLabel";
            this.pimsIdLabel.Size = new System.Drawing.Size(50, 13);
            this.pimsIdLabel.TabIndex = 11;
            this.pimsIdLabel.Text = "PIMS ID:";
            // 
            // appNameLabel
            // 
            this.appNameLabel.AutoSize = true;
            this.appNameLabel.Location = new System.Drawing.Point(9, 134);
            this.appNameLabel.Name = "appNameLabel";
            this.appNameLabel.Size = new System.Drawing.Size(93, 13);
            this.appNameLabel.TabIndex = 12;
            this.appNameLabel.Text = "Application Name:";
            // 
            // appVerLabel
            // 
            this.appVerLabel.AutoSize = true;
            this.appVerLabel.Location = new System.Drawing.Point(9, 160);
            this.appVerLabel.Name = "appVerLabel";
            this.appVerLabel.Size = new System.Drawing.Size(100, 13);
            this.appVerLabel.TabIndex = 13;
            this.appVerLabel.Text = "Application Version:";
            // 
            // pkgNameLabel
            // 
            this.pkgNameLabel.AutoSize = true;
            this.pkgNameLabel.Location = new System.Drawing.Point(9, 186);
            this.pkgNameLabel.Name = "pkgNameLabel";
            this.pkgNameLabel.Size = new System.Drawing.Size(84, 13);
            this.pkgNameLabel.TabIndex = 14;
            this.pkgNameLabel.Text = "Package Name:";
            // 
            // pkgVerLabel
            // 
            this.pkgVerLabel.AutoSize = true;
            this.pkgVerLabel.Location = new System.Drawing.Point(9, 212);
            this.pkgVerLabel.Name = "pkgVerLabel";
            this.pkgVerLabel.Size = new System.Drawing.Size(91, 13);
            this.pkgVerLabel.TabIndex = 15;
            this.pkgVerLabel.Text = "Package Version:";
            // 
            // pkgrNameLabel
            // 
            this.pkgrNameLabel.AutoSize = true;
            this.pkgrNameLabel.Location = new System.Drawing.Point(9, 238);
            this.pkgrNameLabel.Name = "pkgrNameLabel";
            this.pkgrNameLabel.Size = new System.Drawing.Size(73, 13);
            this.pkgrNameLabel.TabIndex = 16;
            this.pkgrNameLabel.Text = "Packaged by:";
            // 
            // brwMsiLabel
            // 
            this.brwMsiLabel.AutoSize = true;
            this.brwMsiLabel.Location = new System.Drawing.Point(10, 27);
            this.brwMsiLabel.Name = "brwMsiLabel";
            this.brwMsiLabel.Size = new System.Drawing.Size(81, 13);
            this.brwMsiLabel.TabIndex = 17;
            this.brwMsiLabel.Text = "Select MSI File:";
            // 
            // logBox
            // 
            this.logBox.Location = new System.Drawing.Point(12, 373);
            this.logBox.Name = "logBox";
            this.logBox.Size = new System.Drawing.Size(281, 51);
            this.logBox.TabIndex = 18;
            this.logBox.Text = "";
            // 
            // customMsiCheckBox
            // 
            this.customMsiCheckBox.AutoSize = true;
            this.customMsiCheckBox.Location = new System.Drawing.Point(112, 50);
            this.customMsiCheckBox.Name = "customMsiCheckBox";
            this.customMsiCheckBox.Size = new System.Drawing.Size(83, 17);
            this.customMsiCheckBox.TabIndex = 19;
            this.customMsiCheckBox.Text = "Custom MSI";
            this.customMsiCheckBox.UseVisualStyleBackColor = true;
            this.customMsiCheckBox.CheckedChanged += new System.EventHandler(this.customMsiCheckBox_CheckedChanged);
            // 
            // button3264bit
            // 
            this.button3264bit.AutoSize = true;
            this.button3264bit.Enabled = false;
            this.button3264bit.Location = new System.Drawing.Point(111, 75);
            this.button3264bit.Name = "button3264bit";
            this.button3264bit.Size = new System.Drawing.Size(83, 17);
            this.button3264bit.TabIndex = 20;
            this.button3264bit.TabStop = true;
            this.button3264bit.Text = "32bit + 64bit";
            this.button3264bit.UseVisualStyleBackColor = true;
            this.button3264bit.CheckedChanged += new System.EventHandler(this.button3264bit_CheckedChanged);
            // 
            // button64bit
            // 
            this.button64bit.AutoSize = true;
            this.button64bit.Enabled = false;
            this.button64bit.Location = new System.Drawing.Point(200, 75);
            this.button64bit.Name = "button64bit";
            this.button64bit.Size = new System.Drawing.Size(48, 17);
            this.button64bit.TabIndex = 21;
            this.button64bit.TabStop = true;
            this.button64bit.Text = "64bit";
            this.button64bit.UseVisualStyleBackColor = true;
            this.button64bit.CheckedChanged += new System.EventHandler(this.button64bit_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "OS Architecture:";
            // 
            // loadingAnimation
            // 
            this.loadingAnimation.Image = ((System.Drawing.Image)(resources.GetObject("loadingAnimation.Image")));
            this.loadingAnimation.Location = new System.Drawing.Point(124, 306);
            this.loadingAnimation.Name = "loadingAnimation";
            this.loadingAnimation.Size = new System.Drawing.Size(47, 50);
            this.loadingAnimation.TabIndex = 23;
            this.loadingAnimation.TabStop = false;
            this.loadingAnimation.Visible = false;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // Form1
            // 
            this.AcceptButton = this.createBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(306, 374);
            this.Controls.Add(this.loadingAnimation);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button64bit);
            this.Controls.Add(this.button3264bit);
            this.Controls.Add(this.customMsiCheckBox);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.brwMsiLabel);
            this.Controls.Add(this.pkgrNameLabel);
            this.Controls.Add(this.pkgVerLabel);
            this.Controls.Add(this.pkgNameLabel);
            this.Controls.Add(this.appVerLabel);
            this.Controls.Add(this.appNameLabel);
            this.Controls.Add(this.pimsIdLabel);
            this.Controls.Add(this.appVerTextBox);
            this.Controls.Add(this.pkgVerTextBox);
            this.Controls.Add(this.pkgNameTextBox);
            this.Controls.Add(this.appNameTextBox);
            this.Controls.Add(this.pimsIdTextBox);
            this.Controls.Add(this.pkgrNameTextBox);
            this.Controls.Add(this.createBtn);
            this.Controls.Add(this.fileLocationTextBox1);
            this.Controls.Add(this.browseBtn1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Automation Tool";
            ((System.ComponentModel.ISupportInitialize)(this.loadingAnimation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button browseBtn1;
        private System.Windows.Forms.OpenFileDialog ofd1;
        private System.Windows.Forms.TextBox fileLocationTextBox1;
        private System.Windows.Forms.Button createBtn;
        private System.Windows.Forms.OpenFileDialog ofd2;
        private System.Windows.Forms.TextBox pkgrNameTextBox;
        private System.Windows.Forms.TextBox pimsIdTextBox;
        private System.Windows.Forms.TextBox appNameTextBox;
        private System.Windows.Forms.TextBox pkgNameTextBox;
        private System.Windows.Forms.TextBox pkgVerTextBox;
        private System.Windows.Forms.TextBox appVerTextBox;
        private System.Windows.Forms.Label pimsIdLabel;
        private System.Windows.Forms.Label appNameLabel;
        private System.Windows.Forms.Label appVerLabel;
        private System.Windows.Forms.Label pkgNameLabel;
        private System.Windows.Forms.Label pkgVerLabel;
        private System.Windows.Forms.Label pkgrNameLabel;
        private System.Windows.Forms.Label brwMsiLabel;
        private System.Windows.Forms.RichTextBox logBox;
        private System.Windows.Forms.CheckBox customMsiCheckBox;
        private System.Windows.Forms.RadioButton button3264bit;
        private System.Windows.Forms.RadioButton button64bit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox loadingAnimation;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}


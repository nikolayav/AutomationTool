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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ofd1 = new System.Windows.Forms.OpenFileDialog();
            this.ofd2 = new System.Windows.Forms.OpenFileDialog();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.createBtn = new MetroFramework.Controls.MetroButton();
            this.pimsIdLabel = new MetroFramework.Controls.MetroLabel();
            this.appNameLabel = new MetroFramework.Controls.MetroLabel();
            this.appVerLabel = new MetroFramework.Controls.MetroLabel();
            this.pkgNameLabel = new MetroFramework.Controls.MetroLabel();
            this.pkgVerLabel = new MetroFramework.Controls.MetroLabel();
            this.pkgrNameLabel = new MetroFramework.Controls.MetroLabel();
            this.browseBtn1 = new MetroFramework.Controls.MetroButton();
            this.fileLocationTextBox1 = new MetroFramework.Controls.MetroTextBox();
            this.pimsIdTextBox = new MetroFramework.Controls.MetroTextBox();
            this.appNameTextBox = new MetroFramework.Controls.MetroTextBox();
            this.appVerTextBox = new MetroFramework.Controls.MetroTextBox();
            this.pkgNameTextBox = new MetroFramework.Controls.MetroTextBox();
            this.pkgVerTextBox = new MetroFramework.Controls.MetroTextBox();
            this.pkgrNameTextBox = new MetroFramework.Controls.MetroTextBox();
            this.brwMsiLabel = new MetroFramework.Controls.MetroLabel();
            this.metroStyleManager1 = new MetroFramework.Components.MetroStyleManager(this.components);
            this.customMsiCheckBox = new MetroFramework.Controls.MetroCheckBox();
            this.osComboBox = new MetroFramework.Controls.MetroComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ofd1
            // 
            this.ofd1.ValidateNames = false;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // createBtn
            // 
            this.createBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.createBtn.Location = new System.Drawing.Point(153, 415);
            this.createBtn.Name = "createBtn";
            this.createBtn.Size = new System.Drawing.Size(177, 44);
            this.createBtn.TabIndex = 10;
            this.createBtn.Text = "Create";
            this.createBtn.Theme = MetroFramework.MetroThemeStyle.Light;
            this.createBtn.UseSelectable = true;
            this.createBtn.Click += new System.EventHandler(this.createBtn_Click);
            // 
            // pimsIdLabel
            // 
            this.pimsIdLabel.AutoSize = true;
            this.pimsIdLabel.Location = new System.Drawing.Point(20, 158);
            this.pimsIdLabel.Name = "pimsIdLabel";
            this.pimsIdLabel.Size = new System.Drawing.Size(55, 19);
            this.pimsIdLabel.TabIndex = 26;
            this.pimsIdLabel.Text = "PIMS ID";
            // 
            // appNameLabel
            // 
            this.appNameLabel.AutoSize = true;
            this.appNameLabel.Location = new System.Drawing.Point(20, 187);
            this.appNameLabel.Name = "appNameLabel";
            this.appNameLabel.Size = new System.Drawing.Size(115, 19);
            this.appNameLabel.TabIndex = 27;
            this.appNameLabel.Text = "Application Name";
            // 
            // appVerLabel
            // 
            this.appVerLabel.AutoSize = true;
            this.appVerLabel.Location = new System.Drawing.Point(20, 216);
            this.appVerLabel.Name = "appVerLabel";
            this.appVerLabel.Size = new System.Drawing.Size(121, 19);
            this.appVerLabel.TabIndex = 28;
            this.appVerLabel.Text = "Application Version";
            // 
            // pkgNameLabel
            // 
            this.pkgNameLabel.AutoSize = true;
            this.pkgNameLabel.Location = new System.Drawing.Point(20, 254);
            this.pkgNameLabel.Name = "pkgNameLabel";
            this.pkgNameLabel.Size = new System.Drawing.Size(97, 19);
            this.pkgNameLabel.TabIndex = 29;
            this.pkgNameLabel.Text = "Package Name";
            // 
            // pkgVerLabel
            // 
            this.pkgVerLabel.AutoSize = true;
            this.pkgVerLabel.Location = new System.Drawing.Point(20, 287);
            this.pkgVerLabel.Name = "pkgVerLabel";
            this.pkgVerLabel.Size = new System.Drawing.Size(103, 19);
            this.pkgVerLabel.TabIndex = 30;
            this.pkgVerLabel.Text = "Package Version";
            // 
            // pkgrNameLabel
            // 
            this.pkgrNameLabel.AutoSize = true;
            this.pkgrNameLabel.Location = new System.Drawing.Point(20, 316);
            this.pkgrNameLabel.Name = "pkgrNameLabel";
            this.pkgrNameLabel.Size = new System.Drawing.Size(102, 19);
            this.pkgrNameLabel.Style = MetroFramework.MetroColorStyle.Blue;
            this.pkgrNameLabel.TabIndex = 31;
            this.pkgrNameLabel.Text = "Packager Name";
            // 
            // browseBtn1
            // 
            this.browseBtn1.Location = new System.Drawing.Point(295, 100);
            this.browseBtn1.Name = "browseBtn1";
            this.browseBtn1.Size = new System.Drawing.Size(35, 23);
            this.browseBtn1.TabIndex = 1;
            this.browseBtn1.Text = "...";
            this.browseBtn1.UseSelectable = true;
            this.browseBtn1.Click += new System.EventHandler(this.browseBtn_Click);
            // 
            // fileLocationTextBox1
            // 
            // 
            // 
            // 
            this.fileLocationTextBox1.CustomButton.Image = null;
            this.fileLocationTextBox1.CustomButton.Location = new System.Drawing.Point(114, 1);
            this.fileLocationTextBox1.CustomButton.Name = "";
            this.fileLocationTextBox1.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.fileLocationTextBox1.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.fileLocationTextBox1.CustomButton.TabIndex = 1;
            this.fileLocationTextBox1.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.fileLocationTextBox1.CustomButton.UseSelectable = true;
            this.fileLocationTextBox1.CustomButton.Visible = false;
            this.fileLocationTextBox1.Lines = new string[0];
            this.fileLocationTextBox1.Location = new System.Drawing.Point(153, 100);
            this.fileLocationTextBox1.MaxLength = 32767;
            this.fileLocationTextBox1.Name = "fileLocationTextBox1";
            this.fileLocationTextBox1.PasswordChar = '\0';
            this.fileLocationTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.fileLocationTextBox1.SelectedText = "";
            this.fileLocationTextBox1.SelectionLength = 0;
            this.fileLocationTextBox1.SelectionStart = 0;
            this.fileLocationTextBox1.ShortcutsEnabled = true;
            this.fileLocationTextBox1.Size = new System.Drawing.Size(136, 23);
            this.fileLocationTextBox1.TabIndex = 0;
            this.fileLocationTextBox1.UseSelectable = true;
            this.fileLocationTextBox1.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.fileLocationTextBox1.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // pimsIdTextBox
            // 
            this.pimsIdTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            // 
            // 
            // 
            this.pimsIdTextBox.CustomButton.FlatAppearance.BorderSize = 0;
            this.pimsIdTextBox.CustomButton.Image = null;
            this.pimsIdTextBox.CustomButton.Location = new System.Drawing.Point(155, 1);
            this.pimsIdTextBox.CustomButton.Name = "";
            this.pimsIdTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.pimsIdTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.pimsIdTextBox.CustomButton.TabIndex = 1;
            this.pimsIdTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.pimsIdTextBox.CustomButton.UseSelectable = true;
            this.pimsIdTextBox.CustomButton.Visible = false;
            this.pimsIdTextBox.Lines = new string[0];
            this.pimsIdTextBox.Location = new System.Drawing.Point(153, 154);
            this.pimsIdTextBox.MaxLength = 32767;
            this.pimsIdTextBox.Name = "pimsIdTextBox";
            this.pimsIdTextBox.PasswordChar = '\0';
            this.pimsIdTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.pimsIdTextBox.SelectedText = "";
            this.pimsIdTextBox.SelectionLength = 0;
            this.pimsIdTextBox.SelectionStart = 0;
            this.pimsIdTextBox.ShortcutsEnabled = true;
            this.pimsIdTextBox.Size = new System.Drawing.Size(177, 23);
            this.pimsIdTextBox.TabIndex = 2;
            this.pimsIdTextBox.UseSelectable = true;
            this.pimsIdTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.pimsIdTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // appNameTextBox
            // 
            // 
            // 
            // 
            this.appNameTextBox.CustomButton.Image = null;
            this.appNameTextBox.CustomButton.Location = new System.Drawing.Point(155, 1);
            this.appNameTextBox.CustomButton.Name = "";
            this.appNameTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.appNameTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.appNameTextBox.CustomButton.TabIndex = 1;
            this.appNameTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.appNameTextBox.CustomButton.UseSelectable = true;
            this.appNameTextBox.CustomButton.Visible = false;
            this.appNameTextBox.Lines = new string[0];
            this.appNameTextBox.Location = new System.Drawing.Point(153, 183);
            this.appNameTextBox.MaxLength = 32767;
            this.appNameTextBox.Name = "appNameTextBox";
            this.appNameTextBox.PasswordChar = '\0';
            this.appNameTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.appNameTextBox.SelectedText = "";
            this.appNameTextBox.SelectionLength = 0;
            this.appNameTextBox.SelectionStart = 0;
            this.appNameTextBox.ShortcutsEnabled = true;
            this.appNameTextBox.Size = new System.Drawing.Size(177, 23);
            this.appNameTextBox.TabIndex = 3;
            this.appNameTextBox.UseSelectable = true;
            this.appNameTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.appNameTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // appVerTextBox
            // 
            // 
            // 
            // 
            this.appVerTextBox.CustomButton.Image = null;
            this.appVerTextBox.CustomButton.Location = new System.Drawing.Point(155, 1);
            this.appVerTextBox.CustomButton.Name = "";
            this.appVerTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.appVerTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.appVerTextBox.CustomButton.TabIndex = 1;
            this.appVerTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.appVerTextBox.CustomButton.UseSelectable = true;
            this.appVerTextBox.CustomButton.Visible = false;
            this.appVerTextBox.Lines = new string[0];
            this.appVerTextBox.Location = new System.Drawing.Point(153, 212);
            this.appVerTextBox.MaxLength = 32767;
            this.appVerTextBox.Name = "appVerTextBox";
            this.appVerTextBox.PasswordChar = '\0';
            this.appVerTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.appVerTextBox.SelectedText = "";
            this.appVerTextBox.SelectionLength = 0;
            this.appVerTextBox.SelectionStart = 0;
            this.appVerTextBox.ShortcutsEnabled = true;
            this.appVerTextBox.Size = new System.Drawing.Size(177, 23);
            this.appVerTextBox.TabIndex = 4;
            this.appVerTextBox.UseSelectable = true;
            this.appVerTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.appVerTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // pkgNameTextBox
            // 
            // 
            // 
            // 
            this.pkgNameTextBox.CustomButton.Image = null;
            this.pkgNameTextBox.CustomButton.Location = new System.Drawing.Point(155, 1);
            this.pkgNameTextBox.CustomButton.Name = "";
            this.pkgNameTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.pkgNameTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.pkgNameTextBox.CustomButton.TabIndex = 1;
            this.pkgNameTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.pkgNameTextBox.CustomButton.UseSelectable = true;
            this.pkgNameTextBox.CustomButton.Visible = false;
            this.pkgNameTextBox.Lines = new string[0];
            this.pkgNameTextBox.Location = new System.Drawing.Point(153, 254);
            this.pkgNameTextBox.MaxLength = 32767;
            this.pkgNameTextBox.Name = "pkgNameTextBox";
            this.pkgNameTextBox.PasswordChar = '\0';
            this.pkgNameTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.pkgNameTextBox.SelectedText = "";
            this.pkgNameTextBox.SelectionLength = 0;
            this.pkgNameTextBox.SelectionStart = 0;
            this.pkgNameTextBox.ShortcutsEnabled = true;
            this.pkgNameTextBox.Size = new System.Drawing.Size(177, 23);
            this.pkgNameTextBox.TabIndex = 5;
            this.pkgNameTextBox.UseSelectable = true;
            this.pkgNameTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.pkgNameTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // pkgVerTextBox
            // 
            // 
            // 
            // 
            this.pkgVerTextBox.CustomButton.Image = null;
            this.pkgVerTextBox.CustomButton.Location = new System.Drawing.Point(155, 1);
            this.pkgVerTextBox.CustomButton.Name = "";
            this.pkgVerTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.pkgVerTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.pkgVerTextBox.CustomButton.TabIndex = 1;
            this.pkgVerTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.pkgVerTextBox.CustomButton.UseSelectable = true;
            this.pkgVerTextBox.CustomButton.Visible = false;
            this.pkgVerTextBox.Lines = new string[0];
            this.pkgVerTextBox.Location = new System.Drawing.Point(153, 283);
            this.pkgVerTextBox.MaxLength = 32767;
            this.pkgVerTextBox.Name = "pkgVerTextBox";
            this.pkgVerTextBox.PasswordChar = '\0';
            this.pkgVerTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.pkgVerTextBox.SelectedText = "";
            this.pkgVerTextBox.SelectionLength = 0;
            this.pkgVerTextBox.SelectionStart = 0;
            this.pkgVerTextBox.ShortcutsEnabled = true;
            this.pkgVerTextBox.Size = new System.Drawing.Size(177, 23);
            this.pkgVerTextBox.TabIndex = 6;
            this.pkgVerTextBox.UseSelectable = true;
            this.pkgVerTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.pkgVerTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // pkgrNameTextBox
            // 
            // 
            // 
            // 
            this.pkgrNameTextBox.CustomButton.Image = null;
            this.pkgrNameTextBox.CustomButton.Location = new System.Drawing.Point(155, 1);
            this.pkgrNameTextBox.CustomButton.Name = "";
            this.pkgrNameTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.pkgrNameTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.pkgrNameTextBox.CustomButton.TabIndex = 1;
            this.pkgrNameTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.pkgrNameTextBox.CustomButton.UseSelectable = true;
            this.pkgrNameTextBox.CustomButton.Visible = false;
            this.pkgrNameTextBox.Lines = new string[0];
            this.pkgrNameTextBox.Location = new System.Drawing.Point(153, 312);
            this.pkgrNameTextBox.MaxLength = 32767;
            this.pkgrNameTextBox.Name = "pkgrNameTextBox";
            this.pkgrNameTextBox.PasswordChar = '\0';
            this.pkgrNameTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.pkgrNameTextBox.SelectedText = "";
            this.pkgrNameTextBox.SelectionLength = 0;
            this.pkgrNameTextBox.SelectionStart = 0;
            this.pkgrNameTextBox.ShortcutsEnabled = true;
            this.pkgrNameTextBox.Size = new System.Drawing.Size(177, 23);
            this.pkgrNameTextBox.TabIndex = 7;
            this.pkgrNameTextBox.UseSelectable = true;
            this.pkgrNameTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.pkgrNameTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // brwMsiLabel
            // 
            this.brwMsiLabel.AutoSize = true;
            this.brwMsiLabel.Location = new System.Drawing.Point(20, 104);
            this.brwMsiLabel.Name = "brwMsiLabel";
            this.brwMsiLabel.Size = new System.Drawing.Size(93, 19);
            this.brwMsiLabel.TabIndex = 41;
            this.brwMsiLabel.Text = "Select MSI File";
            // 
            // metroStyleManager1
            // 
            this.metroStyleManager1.Owner = this;
            // 
            // customMsiCheckBox
            // 
            this.customMsiCheckBox.AutoSize = true;
            this.customMsiCheckBox.Location = new System.Drawing.Point(153, 350);
            this.customMsiCheckBox.Name = "customMsiCheckBox";
            this.customMsiCheckBox.Size = new System.Drawing.Size(88, 15);
            this.customMsiCheckBox.TabIndex = 8;
            this.customMsiCheckBox.Text = "Custom MSI";
            this.customMsiCheckBox.UseSelectable = true;
            this.customMsiCheckBox.CheckedChanged += new System.EventHandler(this.customMsiCheckBox_CheckedChanged);
            // 
            // osComboBox
            // 
            this.osComboBox.Enabled = false;
            this.osComboBox.FormattingEnabled = true;
            this.osComboBox.ItemHeight = 23;
            this.osComboBox.Items.AddRange(new object[] {
            "32bit + 64bit",
            "64bit"});
            this.osComboBox.Location = new System.Drawing.Point(153, 380);
            this.osComboBox.Name = "osComboBox";
            this.osComboBox.PromptText = "Select OS Architecture";
            this.osComboBox.Size = new System.Drawing.Size(177, 29);
            this.osComboBox.TabIndex = 9;
            this.osComboBox.UseSelectable = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(22, 33);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(200, 44);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 44;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AcceptButton = this.createBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(357, 499);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.osComboBox);
            this.Controls.Add(this.brwMsiLabel);
            this.Controls.Add(this.pkgrNameTextBox);
            this.Controls.Add(this.pkgVerTextBox);
            this.Controls.Add(this.pkgNameTextBox);
            this.Controls.Add(this.appVerTextBox);
            this.Controls.Add(this.appNameTextBox);
            this.Controls.Add(this.pimsIdTextBox);
            this.Controls.Add(this.fileLocationTextBox1);
            this.Controls.Add(this.browseBtn1);
            this.Controls.Add(this.customMsiCheckBox);
            this.Controls.Add(this.pkgrNameLabel);
            this.Controls.Add(this.pkgVerLabel);
            this.Controls.Add(this.pkgNameLabel);
            this.Controls.Add(this.appVerLabel);
            this.Controls.Add(this.appNameLabel);
            this.Controls.Add(this.pimsIdLabel);
            this.Controls.Add(this.createBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Style = MetroFramework.MetroColorStyle.Default;
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog ofd1;
        private System.Windows.Forms.OpenFileDialog ofd2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private MetroFramework.Controls.MetroButton createBtn;
        private MetroFramework.Controls.MetroLabel pimsIdLabel;
        private MetroFramework.Controls.MetroLabel appNameLabel;
        private MetroFramework.Controls.MetroLabel appVerLabel;
        private MetroFramework.Controls.MetroLabel pkgNameLabel;
        private MetroFramework.Controls.MetroLabel pkgVerLabel;
        private MetroFramework.Controls.MetroLabel pkgrNameLabel;
        private MetroFramework.Controls.MetroButton browseBtn1;
        private MetroFramework.Controls.MetroTextBox fileLocationTextBox1;
        private MetroFramework.Controls.MetroTextBox pimsIdTextBox;
        private MetroFramework.Controls.MetroTextBox appNameTextBox;
        private MetroFramework.Controls.MetroTextBox appVerTextBox;
        private MetroFramework.Controls.MetroTextBox pkgNameTextBox;
        private MetroFramework.Controls.MetroTextBox pkgVerTextBox;
        private MetroFramework.Controls.MetroTextBox pkgrNameTextBox;
        private MetroFramework.Controls.MetroLabel brwMsiLabel;
        private MetroFramework.Components.MetroStyleManager metroStyleManager1;
        private MetroFramework.Controls.MetroComboBox osComboBox;
        private MetroFramework.Controls.MetroCheckBox customMsiCheckBox;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}


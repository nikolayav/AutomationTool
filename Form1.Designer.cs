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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ofd1 = new System.Windows.Forms.OpenFileDialog();
            this.ofd2 = new System.Windows.Forms.OpenFileDialog();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.metroStyleManager1 = new MetroFramework.Components.MetroStyleManager(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.metroTabPage1 = new MetroFramework.Controls.MetroTabPage();
            this.editMstCheckBox = new MetroFramework.Controls.MetroCheckBox();
            this.loadMstLabel1 = new MetroFramework.Controls.MetroLabel();
            this.loadMstBtn1 = new MetroFramework.Controls.MetroButton();
            this.fileLocationMstTextBox1 = new MetroFramework.Controls.MetroTextBox();
            this.progressBar1 = new MetroFramework.Controls.MetroProgressBar();
            this.osComboBox = new MetroFramework.Controls.MetroComboBox();
            this.brwMsiLabel = new MetroFramework.Controls.MetroLabel();
            this.pkgrNameTextBox = new MetroFramework.Controls.MetroTextBox();
            this.pkgVerTextBox = new MetroFramework.Controls.MetroTextBox();
            this.pkgNameTextBox = new MetroFramework.Controls.MetroTextBox();
            this.appVerTextBox = new MetroFramework.Controls.MetroTextBox();
            this.appNameTextBox = new MetroFramework.Controls.MetroTextBox();
            this.pimsIdTextBox = new MetroFramework.Controls.MetroTextBox();
            this.fileLocationTextBox1 = new MetroFramework.Controls.MetroTextBox();
            this.browseBtn1 = new MetroFramework.Controls.MetroButton();
            this.customMsiCheckBox = new MetroFramework.Controls.MetroCheckBox();
            this.pkgrNameLabel = new MetroFramework.Controls.MetroLabel();
            this.pkgVerLabel = new MetroFramework.Controls.MetroLabel();
            this.pkgNameLabel = new MetroFramework.Controls.MetroLabel();
            this.appVerLabel = new MetroFramework.Controls.MetroLabel();
            this.appNameLabel = new MetroFramework.Controls.MetroLabel();
            this.pimsIdLabel = new MetroFramework.Controls.MetroLabel();
            this.createBtn = new MetroFramework.Controls.MetroButton();
            this.metroTabPage2 = new MetroFramework.Controls.MetroTabPage();
            this.loadMsiTextBox = new MetroFramework.Controls.MetroTextBox();
            this.loadMsiButton = new MetroFramework.Controls.MetroButton();
            this.metroGrid1 = new MetroFramework.Controls.MetroGrid();
            this.loadMstTextBox = new MetroFramework.Controls.MetroTextBox();
            this.loadMstButton = new MetroFramework.Controls.MetroButton();
            this.ofd3 = new System.Windows.Forms.OpenFileDialog();
            this.ofd4 = new System.Windows.Forms.OpenFileDialog();
            this.Property = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.metroTabControl1.SuspendLayout();
            this.metroTabPage1.SuspendLayout();
            this.metroTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.metroGrid1)).BeginInit();
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
            // metroStyleManager1
            // 
            this.metroStyleManager1.Owner = this;
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
            // metroTabControl1
            // 
            this.metroTabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.metroTabControl1.Controls.Add(this.metroTabPage1);
            this.metroTabControl1.Controls.Add(this.metroTabPage2);
            this.metroTabControl1.ItemSize = new System.Drawing.Size(70, 37);
            this.metroTabControl1.Location = new System.Drawing.Point(22, 92);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 0;
            this.metroTabControl1.Size = new System.Drawing.Size(355, 509);
            this.metroTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.metroTabControl1.TabIndex = 45;
            this.metroTabControl1.UseSelectable = true;
            // 
            // metroTabPage1
            // 
            this.metroTabPage1.Controls.Add(this.editMstCheckBox);
            this.metroTabPage1.Controls.Add(this.loadMstLabel1);
            this.metroTabPage1.Controls.Add(this.loadMstBtn1);
            this.metroTabPage1.Controls.Add(this.fileLocationMstTextBox1);
            this.metroTabPage1.Controls.Add(this.progressBar1);
            this.metroTabPage1.Controls.Add(this.osComboBox);
            this.metroTabPage1.Controls.Add(this.brwMsiLabel);
            this.metroTabPage1.Controls.Add(this.pkgrNameTextBox);
            this.metroTabPage1.Controls.Add(this.pkgVerTextBox);
            this.metroTabPage1.Controls.Add(this.pkgNameTextBox);
            this.metroTabPage1.Controls.Add(this.appVerTextBox);
            this.metroTabPage1.Controls.Add(this.appNameTextBox);
            this.metroTabPage1.Controls.Add(this.pimsIdTextBox);
            this.metroTabPage1.Controls.Add(this.fileLocationTextBox1);
            this.metroTabPage1.Controls.Add(this.browseBtn1);
            this.metroTabPage1.Controls.Add(this.customMsiCheckBox);
            this.metroTabPage1.Controls.Add(this.pkgrNameLabel);
            this.metroTabPage1.Controls.Add(this.pkgVerLabel);
            this.metroTabPage1.Controls.Add(this.pkgNameLabel);
            this.metroTabPage1.Controls.Add(this.appVerLabel);
            this.metroTabPage1.Controls.Add(this.appNameLabel);
            this.metroTabPage1.Controls.Add(this.pimsIdLabel);
            this.metroTabPage1.Controls.Add(this.createBtn);
            this.metroTabPage1.HorizontalScrollbarBarColor = true;
            this.metroTabPage1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.HorizontalScrollbarSize = 10;
            this.metroTabPage1.Location = new System.Drawing.Point(4, 41);
            this.metroTabPage1.Name = "metroTabPage1";
            this.metroTabPage1.Size = new System.Drawing.Size(347, 464);
            this.metroTabPage1.TabIndex = 0;
            this.metroTabPage1.Text = "Project";
            this.metroTabPage1.VerticalScrollbarBarColor = true;
            this.metroTabPage1.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.VerticalScrollbarSize = 10;
            // 
            // editMstCheckBox
            // 
            this.editMstCheckBox.AutoSize = true;
            this.editMstCheckBox.Location = new System.Drawing.Point(140, 90);
            this.editMstCheckBox.Name = "editMstCheckBox";
            this.editMstCheckBox.Size = new System.Drawing.Size(69, 15);
            this.editMstCheckBox.TabIndex = 66;
            this.editMstCheckBox.Text = "Edit MST";
            this.editMstCheckBox.UseSelectable = true;
            this.editMstCheckBox.CheckedChanged += new System.EventHandler(this.editMstCheckBox_CheckedChanged);
            // 
            // loadMstLabel1
            // 
            this.loadMstLabel1.AutoSize = true;
            this.loadMstLabel1.Location = new System.Drawing.Point(0, 56);
            this.loadMstLabel1.Name = "loadMstLabel1";
            this.loadMstLabel1.Size = new System.Drawing.Size(73, 19);
            this.loadMstLabel1.TabIndex = 65;
            this.loadMstLabel1.Text = "Select MST";
            // 
            // loadMstBtn1
            // 
            this.loadMstBtn1.Enabled = false;
            this.loadMstBtn1.Location = new System.Drawing.Point(299, 52);
            this.loadMstBtn1.Name = "loadMstBtn1";
            this.loadMstBtn1.Size = new System.Drawing.Size(35, 23);
            this.loadMstBtn1.TabIndex = 64;
            this.loadMstBtn1.Text = "...";
            this.loadMstBtn1.UseSelectable = true;
            this.loadMstBtn1.Click += new System.EventHandler(this.loadMstBtn1_Click);
            // 
            // fileLocationMstTextBox1
            // 
            this.fileLocationMstTextBox1.BackColor = System.Drawing.Color.Gainsboro;
            // 
            // 
            // 
            this.fileLocationMstTextBox1.CustomButton.Image = null;
            this.fileLocationMstTextBox1.CustomButton.Location = new System.Drawing.Point(131, 1);
            this.fileLocationMstTextBox1.CustomButton.Name = "";
            this.fileLocationMstTextBox1.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.fileLocationMstTextBox1.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.fileLocationMstTextBox1.CustomButton.TabIndex = 1;
            this.fileLocationMstTextBox1.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.fileLocationMstTextBox1.CustomButton.UseSelectable = true;
            this.fileLocationMstTextBox1.CustomButton.Visible = false;
            this.fileLocationMstTextBox1.Enabled = false;
            this.fileLocationMstTextBox1.Lines = new string[0];
            this.fileLocationMstTextBox1.Location = new System.Drawing.Point(140, 52);
            this.fileLocationMstTextBox1.MaxLength = 32767;
            this.fileLocationMstTextBox1.Name = "fileLocationMstTextBox1";
            this.fileLocationMstTextBox1.PasswordChar = '\0';
            this.fileLocationMstTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.fileLocationMstTextBox1.SelectedText = "";
            this.fileLocationMstTextBox1.SelectionLength = 0;
            this.fileLocationMstTextBox1.SelectionStart = 0;
            this.fileLocationMstTextBox1.ShortcutsEnabled = true;
            this.fileLocationMstTextBox1.Size = new System.Drawing.Size(153, 23);
            this.fileLocationMstTextBox1.TabIndex = 63;
            this.fileLocationMstTextBox1.UseCustomBackColor = true;
            this.fileLocationMstTextBox1.UseSelectable = true;
            this.fileLocationMstTextBox1.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.fileLocationMstTextBox1.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(0, 445);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(344, 5);
            this.progressBar1.TabIndex = 46;
            this.progressBar1.Visible = false;
            // 
            // osComboBox
            // 
            this.osComboBox.BackColor = System.Drawing.Color.Gainsboro;
            this.osComboBox.Enabled = false;
            this.osComboBox.FormattingEnabled = true;
            this.osComboBox.ItemHeight = 23;
            this.osComboBox.Items.AddRange(new object[] {
            "32bit + 64bit",
            "64bit"});
            this.osComboBox.Location = new System.Drawing.Point(140, 328);
            this.osComboBox.Name = "osComboBox";
            this.osComboBox.PromptText = "Select OS Architecture";
            this.osComboBox.Size = new System.Drawing.Size(194, 29);
            this.osComboBox.TabIndex = 51;
            this.osComboBox.UseCustomBackColor = true;
            this.osComboBox.UseSelectable = true;
            // 
            // brwMsiLabel
            // 
            this.brwMsiLabel.AutoSize = true;
            this.brwMsiLabel.Location = new System.Drawing.Point(0, 26);
            this.brwMsiLabel.Name = "brwMsiLabel";
            this.brwMsiLabel.Size = new System.Drawing.Size(69, 19);
            this.brwMsiLabel.TabIndex = 59;
            this.brwMsiLabel.Text = "Select MSI";
            // 
            // pkgrNameTextBox
            // 
            // 
            // 
            // 
            this.pkgrNameTextBox.CustomButton.Image = null;
            this.pkgrNameTextBox.CustomButton.Location = new System.Drawing.Point(172, 1);
            this.pkgrNameTextBox.CustomButton.Name = "";
            this.pkgrNameTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.pkgrNameTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.pkgrNameTextBox.CustomButton.TabIndex = 1;
            this.pkgrNameTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.pkgrNameTextBox.CustomButton.UseSelectable = true;
            this.pkgrNameTextBox.CustomButton.Visible = false;
            this.pkgrNameTextBox.Lines = new string[0];
            this.pkgrNameTextBox.Location = new System.Drawing.Point(140, 283);
            this.pkgrNameTextBox.MaxLength = 32767;
            this.pkgrNameTextBox.Name = "pkgrNameTextBox";
            this.pkgrNameTextBox.PasswordChar = '\0';
            this.pkgrNameTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.pkgrNameTextBox.SelectedText = "";
            this.pkgrNameTextBox.SelectionLength = 0;
            this.pkgrNameTextBox.SelectionStart = 0;
            this.pkgrNameTextBox.ShortcutsEnabled = true;
            this.pkgrNameTextBox.Size = new System.Drawing.Size(194, 23);
            this.pkgrNameTextBox.TabIndex = 7;
            this.pkgrNameTextBox.UseSelectable = true;
            this.pkgrNameTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.pkgrNameTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // pkgVerTextBox
            // 
            // 
            // 
            // 
            this.pkgVerTextBox.CustomButton.Image = null;
            this.pkgVerTextBox.CustomButton.Location = new System.Drawing.Point(172, 1);
            this.pkgVerTextBox.CustomButton.Name = "";
            this.pkgVerTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.pkgVerTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.pkgVerTextBox.CustomButton.TabIndex = 1;
            this.pkgVerTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.pkgVerTextBox.CustomButton.UseSelectable = true;
            this.pkgVerTextBox.CustomButton.Visible = false;
            this.pkgVerTextBox.Lines = new string[0];
            this.pkgVerTextBox.Location = new System.Drawing.Point(140, 254);
            this.pkgVerTextBox.MaxLength = 32767;
            this.pkgVerTextBox.Name = "pkgVerTextBox";
            this.pkgVerTextBox.PasswordChar = '\0';
            this.pkgVerTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.pkgVerTextBox.SelectedText = "";
            this.pkgVerTextBox.SelectionLength = 0;
            this.pkgVerTextBox.SelectionStart = 0;
            this.pkgVerTextBox.ShortcutsEnabled = true;
            this.pkgVerTextBox.Size = new System.Drawing.Size(194, 23);
            this.pkgVerTextBox.TabIndex = 6;
            this.pkgVerTextBox.UseSelectable = true;
            this.pkgVerTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.pkgVerTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // pkgNameTextBox
            // 
            // 
            // 
            // 
            this.pkgNameTextBox.CustomButton.Image = null;
            this.pkgNameTextBox.CustomButton.Location = new System.Drawing.Point(172, 1);
            this.pkgNameTextBox.CustomButton.Name = "";
            this.pkgNameTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.pkgNameTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.pkgNameTextBox.CustomButton.TabIndex = 1;
            this.pkgNameTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.pkgNameTextBox.CustomButton.UseSelectable = true;
            this.pkgNameTextBox.CustomButton.Visible = false;
            this.pkgNameTextBox.Lines = new string[0];
            this.pkgNameTextBox.Location = new System.Drawing.Point(140, 225);
            this.pkgNameTextBox.MaxLength = 32767;
            this.pkgNameTextBox.Name = "pkgNameTextBox";
            this.pkgNameTextBox.PasswordChar = '\0';
            this.pkgNameTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.pkgNameTextBox.SelectedText = "";
            this.pkgNameTextBox.SelectionLength = 0;
            this.pkgNameTextBox.SelectionStart = 0;
            this.pkgNameTextBox.ShortcutsEnabled = true;
            this.pkgNameTextBox.Size = new System.Drawing.Size(194, 23);
            this.pkgNameTextBox.TabIndex = 5;
            this.pkgNameTextBox.UseSelectable = true;
            this.pkgNameTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.pkgNameTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // appVerTextBox
            // 
            // 
            // 
            // 
            this.appVerTextBox.CustomButton.Image = null;
            this.appVerTextBox.CustomButton.Location = new System.Drawing.Point(172, 1);
            this.appVerTextBox.CustomButton.Name = "";
            this.appVerTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.appVerTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.appVerTextBox.CustomButton.TabIndex = 1;
            this.appVerTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.appVerTextBox.CustomButton.UseSelectable = true;
            this.appVerTextBox.CustomButton.Visible = false;
            this.appVerTextBox.Lines = new string[0];
            this.appVerTextBox.Location = new System.Drawing.Point(140, 181);
            this.appVerTextBox.MaxLength = 32767;
            this.appVerTextBox.Name = "appVerTextBox";
            this.appVerTextBox.PasswordChar = '\0';
            this.appVerTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.appVerTextBox.SelectedText = "";
            this.appVerTextBox.SelectionLength = 0;
            this.appVerTextBox.SelectionStart = 0;
            this.appVerTextBox.ShortcutsEnabled = true;
            this.appVerTextBox.Size = new System.Drawing.Size(194, 23);
            this.appVerTextBox.TabIndex = 4;
            this.appVerTextBox.UseSelectable = true;
            this.appVerTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.appVerTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // appNameTextBox
            // 
            // 
            // 
            // 
            this.appNameTextBox.CustomButton.Image = null;
            this.appNameTextBox.CustomButton.Location = new System.Drawing.Point(172, 1);
            this.appNameTextBox.CustomButton.Name = "";
            this.appNameTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.appNameTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.appNameTextBox.CustomButton.TabIndex = 1;
            this.appNameTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.appNameTextBox.CustomButton.UseSelectable = true;
            this.appNameTextBox.CustomButton.Visible = false;
            this.appNameTextBox.Lines = new string[0];
            this.appNameTextBox.Location = new System.Drawing.Point(140, 152);
            this.appNameTextBox.MaxLength = 32767;
            this.appNameTextBox.Name = "appNameTextBox";
            this.appNameTextBox.PasswordChar = '\0';
            this.appNameTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.appNameTextBox.SelectedText = "";
            this.appNameTextBox.SelectionLength = 0;
            this.appNameTextBox.SelectionStart = 0;
            this.appNameTextBox.ShortcutsEnabled = true;
            this.appNameTextBox.Size = new System.Drawing.Size(194, 23);
            this.appNameTextBox.TabIndex = 3;
            this.appNameTextBox.UseSelectable = true;
            this.appNameTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.appNameTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // pimsIdTextBox
            // 
            this.pimsIdTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            // 
            // 
            // 
            this.pimsIdTextBox.CustomButton.FlatAppearance.BorderSize = 0;
            this.pimsIdTextBox.CustomButton.Image = null;
            this.pimsIdTextBox.CustomButton.Location = new System.Drawing.Point(172, 1);
            this.pimsIdTextBox.CustomButton.Name = "";
            this.pimsIdTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.pimsIdTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.pimsIdTextBox.CustomButton.TabIndex = 1;
            this.pimsIdTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.pimsIdTextBox.CustomButton.UseSelectable = true;
            this.pimsIdTextBox.CustomButton.Visible = false;
            this.pimsIdTextBox.Lines = new string[0];
            this.pimsIdTextBox.Location = new System.Drawing.Point(140, 123);
            this.pimsIdTextBox.MaxLength = 32767;
            this.pimsIdTextBox.Name = "pimsIdTextBox";
            this.pimsIdTextBox.PasswordChar = '\0';
            this.pimsIdTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.pimsIdTextBox.SelectedText = "";
            this.pimsIdTextBox.SelectionLength = 0;
            this.pimsIdTextBox.SelectionStart = 0;
            this.pimsIdTextBox.ShortcutsEnabled = true;
            this.pimsIdTextBox.Size = new System.Drawing.Size(194, 23);
            this.pimsIdTextBox.TabIndex = 2;
            this.pimsIdTextBox.UseSelectable = true;
            this.pimsIdTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.pimsIdTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // fileLocationTextBox1
            // 
            this.fileLocationTextBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            // 
            // 
            // 
            this.fileLocationTextBox1.CustomButton.Image = null;
            this.fileLocationTextBox1.CustomButton.Location = new System.Drawing.Point(131, 1);
            this.fileLocationTextBox1.CustomButton.Name = "";
            this.fileLocationTextBox1.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.fileLocationTextBox1.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.fileLocationTextBox1.CustomButton.TabIndex = 1;
            this.fileLocationTextBox1.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.fileLocationTextBox1.CustomButton.UseSelectable = true;
            this.fileLocationTextBox1.CustomButton.Visible = false;
            this.fileLocationTextBox1.Lines = new string[0];
            this.fileLocationTextBox1.Location = new System.Drawing.Point(140, 22);
            this.fileLocationTextBox1.MaxLength = 32767;
            this.fileLocationTextBox1.Name = "fileLocationTextBox1";
            this.fileLocationTextBox1.PasswordChar = '\0';
            this.fileLocationTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.fileLocationTextBox1.SelectedText = "";
            this.fileLocationTextBox1.SelectionLength = 0;
            this.fileLocationTextBox1.SelectionStart = 0;
            this.fileLocationTextBox1.ShortcutsEnabled = true;
            this.fileLocationTextBox1.Size = new System.Drawing.Size(153, 23);
            this.fileLocationTextBox1.TabIndex = 0;
            this.fileLocationTextBox1.UseCustomBackColor = true;
            this.fileLocationTextBox1.UseSelectable = true;
            this.fileLocationTextBox1.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.fileLocationTextBox1.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // browseBtn1
            // 
            this.browseBtn1.Location = new System.Drawing.Point(299, 22);
            this.browseBtn1.Name = "browseBtn1";
            this.browseBtn1.Size = new System.Drawing.Size(35, 23);
            this.browseBtn1.TabIndex = 1;
            this.browseBtn1.Text = "...";
            this.browseBtn1.UseCustomBackColor = true;
            this.browseBtn1.UseSelectable = true;
            this.browseBtn1.Click += new System.EventHandler(this.browseBtn1_Click);
            // 
            // customMsiCheckBox
            // 
            this.customMsiCheckBox.AutoSize = true;
            this.customMsiCheckBox.Location = new System.Drawing.Point(246, 90);
            this.customMsiCheckBox.Name = "customMsiCheckBox";
            this.customMsiCheckBox.Size = new System.Drawing.Size(88, 15);
            this.customMsiCheckBox.TabIndex = 8;
            this.customMsiCheckBox.Text = "Custom MSI";
            this.customMsiCheckBox.UseSelectable = true;
            this.customMsiCheckBox.CheckedChanged += new System.EventHandler(this.customMsiCheckBox_CheckedChanged);
            // 
            // pkgrNameLabel
            // 
            this.pkgrNameLabel.AutoSize = true;
            this.pkgrNameLabel.Location = new System.Drawing.Point(0, 288);
            this.pkgrNameLabel.Name = "pkgrNameLabel";
            this.pkgrNameLabel.Size = new System.Drawing.Size(102, 19);
            this.pkgrNameLabel.Style = MetroFramework.MetroColorStyle.Blue;
            this.pkgrNameLabel.TabIndex = 58;
            this.pkgrNameLabel.Text = "Packager Name";
            // 
            // pkgVerLabel
            // 
            this.pkgVerLabel.AutoSize = true;
            this.pkgVerLabel.Location = new System.Drawing.Point(0, 259);
            this.pkgVerLabel.Name = "pkgVerLabel";
            this.pkgVerLabel.Size = new System.Drawing.Size(103, 19);
            this.pkgVerLabel.TabIndex = 57;
            this.pkgVerLabel.Text = "Package Version";
            // 
            // pkgNameLabel
            // 
            this.pkgNameLabel.AutoSize = true;
            this.pkgNameLabel.Location = new System.Drawing.Point(0, 226);
            this.pkgNameLabel.Name = "pkgNameLabel";
            this.pkgNameLabel.Size = new System.Drawing.Size(97, 19);
            this.pkgNameLabel.TabIndex = 56;
            this.pkgNameLabel.Text = "Package Name";
            // 
            // appVerLabel
            // 
            this.appVerLabel.AutoSize = true;
            this.appVerLabel.Location = new System.Drawing.Point(0, 186);
            this.appVerLabel.Name = "appVerLabel";
            this.appVerLabel.Size = new System.Drawing.Size(121, 19);
            this.appVerLabel.TabIndex = 55;
            this.appVerLabel.Text = "Application Version";
            // 
            // appNameLabel
            // 
            this.appNameLabel.AutoSize = true;
            this.appNameLabel.Location = new System.Drawing.Point(0, 157);
            this.appNameLabel.Name = "appNameLabel";
            this.appNameLabel.Size = new System.Drawing.Size(115, 19);
            this.appNameLabel.TabIndex = 54;
            this.appNameLabel.Text = "Application Name";
            // 
            // pimsIdLabel
            // 
            this.pimsIdLabel.AutoSize = true;
            this.pimsIdLabel.Location = new System.Drawing.Point(0, 128);
            this.pimsIdLabel.Name = "pimsIdLabel";
            this.pimsIdLabel.Size = new System.Drawing.Size(55, 19);
            this.pimsIdLabel.TabIndex = 53;
            this.pimsIdLabel.Text = "PIMS ID";
            // 
            // createBtn
            // 
            this.createBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.createBtn.Location = new System.Drawing.Point(140, 381);
            this.createBtn.Name = "createBtn";
            this.createBtn.Size = new System.Drawing.Size(194, 44);
            this.createBtn.TabIndex = 10;
            this.createBtn.Text = "Create";
            this.createBtn.Theme = MetroFramework.MetroThemeStyle.Light;
            this.createBtn.UseSelectable = true;
            this.createBtn.Click += new System.EventHandler(this.createBtn_Click);
            // 
            // metroTabPage2
            // 
            this.metroTabPage2.Controls.Add(this.loadMsiTextBox);
            this.metroTabPage2.Controls.Add(this.loadMsiButton);
            this.metroTabPage2.Controls.Add(this.metroGrid1);
            this.metroTabPage2.Controls.Add(this.loadMstTextBox);
            this.metroTabPage2.Controls.Add(this.loadMstButton);
            this.metroTabPage2.HorizontalScrollbarBarColor = true;
            this.metroTabPage2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage2.HorizontalScrollbarSize = 10;
            this.metroTabPage2.Location = new System.Drawing.Point(4, 41);
            this.metroTabPage2.Name = "metroTabPage2";
            this.metroTabPage2.Size = new System.Drawing.Size(347, 464);
            this.metroTabPage2.TabIndex = 1;
            this.metroTabPage2.Text = "MSI Checks";
            this.metroTabPage2.VerticalScrollbarBarColor = true;
            this.metroTabPage2.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage2.VerticalScrollbarSize = 10;
            // 
            // loadMsiTextBox
            // 
            // 
            // 
            // 
            this.loadMsiTextBox.CustomButton.Image = null;
            this.loadMsiTextBox.CustomButton.Location = new System.Drawing.Point(198, 1);
            this.loadMsiTextBox.CustomButton.Name = "";
            this.loadMsiTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.loadMsiTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.loadMsiTextBox.CustomButton.TabIndex = 1;
            this.loadMsiTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.loadMsiTextBox.CustomButton.UseSelectable = true;
            this.loadMsiTextBox.CustomButton.Visible = false;
            this.loadMsiTextBox.Lines = new string[0];
            this.loadMsiTextBox.Location = new System.Drawing.Point(4, 25);
            this.loadMsiTextBox.MaxLength = 32767;
            this.loadMsiTextBox.Name = "loadMsiTextBox";
            this.loadMsiTextBox.PasswordChar = '\0';
            this.loadMsiTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.loadMsiTextBox.SelectedText = "";
            this.loadMsiTextBox.SelectionLength = 0;
            this.loadMsiTextBox.SelectionStart = 0;
            this.loadMsiTextBox.ShortcutsEnabled = true;
            this.loadMsiTextBox.Size = new System.Drawing.Size(220, 23);
            this.loadMsiTextBox.TabIndex = 6;
            this.loadMsiTextBox.UseSelectable = true;
            this.loadMsiTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.loadMsiTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // loadMsiButton
            // 
            this.loadMsiButton.Location = new System.Drawing.Point(230, 25);
            this.loadMsiButton.Name = "loadMsiButton";
            this.loadMsiButton.Size = new System.Drawing.Size(97, 23);
            this.loadMsiButton.TabIndex = 5;
            this.loadMsiButton.Text = "Load MSI";
            this.loadMsiButton.UseSelectable = true;
            this.loadMsiButton.Click += new System.EventHandler(this.loadMsiButton_Click);
            // 
            // metroGrid1
            // 
            this.metroGrid1.AllowUserToResizeRows = false;
            this.metroGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.metroGrid1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.metroGrid1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.metroGrid1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.metroGrid1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.metroGrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.metroGrid1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Property,
            this.Value,
            this.Status});
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.metroGrid1.DefaultCellStyle = dataGridViewCellStyle14;
            this.metroGrid1.EnableHeadersVisualStyles = false;
            this.metroGrid1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.metroGrid1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.metroGrid1.Location = new System.Drawing.Point(4, 95);
            this.metroGrid1.Name = "metroGrid1";
            this.metroGrid1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.metroGrid1.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.metroGrid1.RowHeadersVisible = false;
            this.metroGrid1.RowHeadersWidth = 4;
            this.metroGrid1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.metroGrid1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.metroGrid1.Size = new System.Drawing.Size(323, 314);
            this.metroGrid1.TabIndex = 5;
            this.metroGrid1.UseCustomBackColor = true;
            this.metroGrid1.UseCustomForeColor = true;
            // 
            // loadMstTextBox
            // 
            // 
            // 
            // 
            this.loadMstTextBox.CustomButton.Image = null;
            this.loadMstTextBox.CustomButton.Location = new System.Drawing.Point(198, 1);
            this.loadMstTextBox.CustomButton.Name = "";
            this.loadMstTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.loadMstTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.loadMstTextBox.CustomButton.TabIndex = 1;
            this.loadMstTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.loadMstTextBox.CustomButton.UseSelectable = true;
            this.loadMstTextBox.CustomButton.Visible = false;
            this.loadMstTextBox.Lines = new string[0];
            this.loadMstTextBox.Location = new System.Drawing.Point(4, 54);
            this.loadMstTextBox.MaxLength = 32767;
            this.loadMstTextBox.Name = "loadMstTextBox";
            this.loadMstTextBox.PasswordChar = '\0';
            this.loadMstTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.loadMstTextBox.SelectedText = "";
            this.loadMstTextBox.SelectionLength = 0;
            this.loadMstTextBox.SelectionStart = 0;
            this.loadMstTextBox.ShortcutsEnabled = true;
            this.loadMstTextBox.Size = new System.Drawing.Size(220, 23);
            this.loadMstTextBox.TabIndex = 3;
            this.loadMstTextBox.UseSelectable = true;
            this.loadMstTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.loadMstTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // loadMstButton
            // 
            this.loadMstButton.Location = new System.Drawing.Point(230, 54);
            this.loadMstButton.Name = "loadMstButton";
            this.loadMstButton.Size = new System.Drawing.Size(97, 23);
            this.loadMstButton.TabIndex = 2;
            this.loadMstButton.Text = "Load MST";
            this.loadMstButton.UseSelectable = true;
            this.loadMstButton.Click += new System.EventHandler(this.metroButton1_Click);
            // 
            // ofd3
            // 
            this.ofd3.FileName = "ofd3";
            // 
            // ofd4
            // 
            this.ofd4.FileName = "ofd4";
            // 
            // Property
            // 
            this.Property.HeaderText = "Property";
            this.Property.Name = "Property";
            this.Property.Width = 140;
            // 
            // Value
            // 
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            this.Value.Width = 120;
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.Width = 50;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(391, 624);
            this.Controls.Add(this.metroTabControl1);
            this.Controls.Add(this.pictureBox1);
            this.DisplayHeader = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(20, 30, 20, 20);
            this.Style = MetroFramework.MetroColorStyle.Default;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.metroTabControl1.ResumeLayout(false);
            this.metroTabPage1.ResumeLayout(false);
            this.metroTabPage1.PerformLayout();
            this.metroTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.metroGrid1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog ofd1;
        private System.Windows.Forms.OpenFileDialog ofd2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private MetroFramework.Components.MetroStyleManager metroStyleManager1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private MetroFramework.Controls.MetroTabControl metroTabControl1;
        private MetroFramework.Controls.MetroTabPage metroTabPage1;
        private MetroFramework.Controls.MetroComboBox osComboBox;
        private MetroFramework.Controls.MetroLabel brwMsiLabel;
        private MetroFramework.Controls.MetroTextBox pkgrNameTextBox;
        private MetroFramework.Controls.MetroTextBox pkgVerTextBox;
        private MetroFramework.Controls.MetroTextBox pkgNameTextBox;
        private MetroFramework.Controls.MetroTextBox appVerTextBox;
        private MetroFramework.Controls.MetroTextBox appNameTextBox;
        private MetroFramework.Controls.MetroTextBox pimsIdTextBox;
        private MetroFramework.Controls.MetroTextBox fileLocationTextBox1;
        private MetroFramework.Controls.MetroButton browseBtn1;
        private MetroFramework.Controls.MetroCheckBox customMsiCheckBox;
        private MetroFramework.Controls.MetroLabel pkgrNameLabel;
        private MetroFramework.Controls.MetroLabel pkgVerLabel;
        private MetroFramework.Controls.MetroLabel pkgNameLabel;
        private MetroFramework.Controls.MetroLabel appVerLabel;
        private MetroFramework.Controls.MetroLabel appNameLabel;
        private MetroFramework.Controls.MetroLabel pimsIdLabel;
        private MetroFramework.Controls.MetroButton createBtn;
        private MetroFramework.Controls.MetroTabPage metroTabPage2;
        private MetroFramework.Controls.MetroTextBox loadMstTextBox;
        private MetroFramework.Controls.MetroGrid metroGrid1;
        private MetroFramework.Controls.MetroButton loadMstButton;
        private MetroFramework.Controls.MetroTextBox loadMsiTextBox;
        private MetroFramework.Controls.MetroButton loadMsiButton;
        private MetroFramework.Controls.MetroProgressBar progressBar1;
        private MetroFramework.Controls.MetroLabel loadMstLabel1;
        private MetroFramework.Controls.MetroButton loadMstBtn1;
        private MetroFramework.Controls.MetroTextBox fileLocationMstTextBox1;
        private System.Windows.Forms.OpenFileDialog ofd3;
        private System.Windows.Forms.OpenFileDialog ofd4;
        private MetroFramework.Controls.MetroCheckBox editMstCheckBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn Property;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
    }
}


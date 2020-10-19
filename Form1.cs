using System;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Drawing;

namespace AutomationTool {
    public partial class Form1 : MetroFramework.Forms.MetroForm {
        public Form1() {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        }


        private static string _fullMsiPath = "";
        private string _projectFolder = "";

        private void browseBtn_Click(object sender, EventArgs e) {

            ofd1.Filter = "MSI|*.msi";
            if (ofd1.ShowDialog() == DialogResult.OK) {
                fileLocationTextBox1.Text = ofd1.FileName;
                _fullMsiPath = ofd1.FileName;
            }
        }

        Logger logger = new Logger();
        MsiEditor msiEditor = new MsiEditor();
        Administrator adm = new Administrator();
        Creator creator = new Creator();

        private void startProcessing() {
            msiEditor.Logger = logger;
            adm.Logger = logger;
            creator.Logger = logger;

            bool isCustomMsi = customMsiCheckBox.Checked;
            logger.ConfigureLogging();

            ProjectInfo proj = new ProjectInfo();
            proj.ProductCode = "";
            proj.UpgradeCode = "";
            if (isCustomMsi) {
                proj.FolderPath = "";
                proj.MsiName = "";
            } else {
                proj.FolderPath = Path.GetDirectoryName(_fullMsiPath) + "\\";
                proj.MsiName = Path.GetFileNameWithoutExtension(_fullMsiPath);
            }

            proj.PimsId = pimsIdTextBox.Text;
            proj.AppName = appNameTextBox.Text;
            proj.AppVer = appVerTextBox.Text;
            proj.PkgName = pkgNameTextBox.Text;
            proj.PkgVer = pkgVerTextBox.Text;
            proj.AuthorName = String.Format("{0}, {1}", pkgrNameTextBox.Text, "DXC");
            proj.FeatureName = String.Format("{0}_{1}_{2}", "BMW", proj.PkgName, proj.PkgVer);
            string dateNow = DateTime.Now.ToString("d.M.yyyy");
            string[] pkgrNameArr = proj.AuthorName.Split(' ');
            char first = pkgrNameArr[0].ToCharArray()[0];
            char second = pkgrNameArr[1].ToCharArray()[0];
            proj.Comments = String.Format("{0}{1}, {2}, {3}", first.ToString().ToLowerInvariant(), second.ToString().ToLowerInvariant(), dateNow, "IS v23");

            adm.ProjectFolder = _projectFolder;
            adm.FullMsiPath = _fullMsiPath;
            creator.Adm = adm;
            creator.MsiEditor = msiEditor;

            adm.createFolders(String.Format("{0}_{1}", proj.PkgName, proj.PkgVer));

            if (!isCustomMsi) {
                creator.CreateMst(proj);
                creator.CreateXML(proj);
                adm.CreateInstallUninstallVbs(proj, false, String.Format("{0}_{1}_install.vbs", proj.PkgName, proj.PkgVer), String.Format("{0}_{1}_uninstall.vbs", proj.PkgName, proj.PkgVer));
            } else {
                creator.GenerateCustomMsi(proj, Is32bit());
                creator.CreateXML(proj);
                adm.CreateInstallUninstallVbs(proj, true, String.Format("{0}_{1}_install.vbs", proj.PkgName, proj.PkgVer), String.Format("{0}_{1}_uninstall.vbs", proj.PkgName, proj.PkgVer));
            }
        }

        private void fileLocationTextBox1_Enter(object sender, EventArgs e) {
            fileLocationTextBox1.SelectAll();
        }


        private void customMsiCheckBox_CheckedChanged(object sender, EventArgs e) {
            if (customMsiCheckBox.Checked) {
                fileLocationTextBox1.Text = "";
                fileLocationTextBox1.Enabled = false;
                browseBtn1.Enabled = false;
                osComboBox.Enabled = true;
            } else {
                fileLocationTextBox1.Enabled = true;
                browseBtn1.Enabled = true;
                osComboBox.Text = "Select OS Architecture";
                osComboBox.Enabled = false;

            }
        }


        private bool ValidateInput() {
            if (!Regex.IsMatch(pimsIdTextBox.Text, "^[a-zA-Z0-9]+$")) {
                MetroFramework.MetroMessageBox.Show(this, "\n\nPlease, enter a proper PIMS ID (e.g.P012345)", "Invalid PIMS ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                pimsIdTextBox.Focus();
                pimsIdTextBox.SelectAll();
                return false;
            }

            if (!Regex.IsMatch(appNameTextBox.Text, "^[a-zA-Z0-9_. ]*$")) {
                MetroFramework.MetroMessageBox.Show(this, "\n\nPlease, enter a proper Application Name (e.g. no special characters)", "Invalid Application Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                appNameTextBox.Focus();
                appNameTextBox.SelectAll();
                return false;
            }

            if (!Regex.IsMatch(appVerTextBox.Text, "^[a-zA-Z0-9_. ]*$")) {
                MetroFramework.MetroMessageBox.Show(this, "\n\nPlease, enter a proper Application Version (e.g. no special characters)", "Invalid Application Version", MessageBoxButtons.OK, MessageBoxIcon.Error);
                appVerTextBox.Focus();
                appNameTextBox.SelectAll();
                return false;
            }

            if (!Regex.IsMatch(pkgNameTextBox.Text, "^[a-zA-Z_]*$")) {
                MetroFramework.MetroMessageBox.Show(this, "\n\nPlease, enter a proper Package Name (e.g. bmw_sample_i_i", "Invalid Package Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                pkgNameTextBox.Focus();
                pkgNameTextBox.SelectAll();
                return false;
            }

            if (!Regex.IsMatch(pkgVerTextBox.Text, "^[\\d.\\d]*$")) {
                MetroFramework.MetroMessageBox.Show(this, "\n\nPlease, enter a proper Package Version (e.g. 1.0, 2.0...)", "Invalid Package Version", MessageBoxButtons.OK, MessageBoxIcon.Error);
                pkgVerTextBox.Focus();
                pkgVerTextBox.SelectAll();
                return false;
            }

            if (!Regex.IsMatch(pkgrNameTextBox.Text, "^[a-zA-Z]*\\s[a-zA-Z]*$")) {
                MetroFramework.MetroMessageBox.Show(this, "\n\nPlease, enter a proper Packager Name. (e.g. Firstname Lastname)", "Invalid Packager Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                pkgrNameTextBox.Focus();
                pkgrNameTextBox.SelectAll();
                return false;
            }

            if (customMsiCheckBox.Checked == false && String.IsNullOrEmpty(fileLocationTextBox1.Text)) {
                MetroFramework.MetroMessageBox.Show(this, "\n\nPlease, select a path to .msi or check the box for Custom MSI", "Transform or Custom MSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (customMsiCheckBox.Checked == true && String.IsNullOrEmpty(osComboBox.Text)) {
                MetroFramework.MetroMessageBox.Show(this, "\n\nPlease, select select the Target Platform OS Architecture", "Target OS Selection Is Empty", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show("Please, select select the Target Platform OS Architecture", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }


        private bool Is32bit() {
            if (osComboBox.Text.Equals("32bit + 64bit")) {
                return true;
            }
            return false;
        }

        private void enableControls(bool toggle) {
            osComboBox.Enabled = toggle;
            browseBtn1.Enabled = toggle;
            fileLocationTextBox1.Enabled = toggle;
            customMsiCheckBox.Enabled = toggle;
            pimsIdTextBox.Enabled = toggle;
            appNameTextBox.Enabled = toggle;
            appVerTextBox.Enabled = toggle;
            pkgNameTextBox.Enabled = toggle;
            pkgVerTextBox.Enabled = toggle;
            pkgrNameTextBox.Enabled = toggle;
            createBtn.Enabled = toggle;
        }

        private void createBtn_Click(object sender, EventArgs e) {
            if (!ValidateInput()) {
                return;
            }

            enableControls(false);
            backgroundWorker1.RunWorkerAsync();
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e) {
            startProcessing();

        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e) {

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            enableControls(true);
            if (e.Error != null) {
                logger.Log("Error:     " + e.Error.Message + "");
                MetroFramework.MetroMessageBox.Show(this, e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else {
                logger.Log("Finished!");
                MetroFramework.MetroMessageBox.Show(this, "\n\nFinished", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
    }
}

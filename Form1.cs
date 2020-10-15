using System;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace AutomationTool {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
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

            Project proj = new Project();
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
                creator.GenerateCustomMsi(proj, button3264bit.Checked, button64bit.Checked);
                creator.CreateXML(proj);
                adm.CreateInstallUninstallVbs(proj, true, String.Format("{0}_{1}_install.vbs", proj.PkgName, proj.PkgVer), String.Format("{0}_{1}_uninstall.vbs", proj.PkgName, proj.PkgVer));
            }
        }

        private void fileLocationTextBox1_Enter(object sender, EventArgs e) {
            fileLocationTextBox1.SelectAll();
        }
       

        private void customMsiCheckBox_CheckedChanged(object sender, EventArgs e) {
            if (customMsiCheckBox.Checked) {
                button3264bit.Enabled = true;
                button64bit.Enabled = true;
            } else {
                button3264bit.Enabled = false;
                button64bit.Enabled = false;
                button3264bit.Checked = false;
                button64bit.Checked = false;
            }
        }

        private void button3264bit_CheckedChanged(object sender, EventArgs e) {
            if (button3264bit.Checked) {
                button64bit.Checked = false;
            }
        }

        private void button64bit_CheckedChanged(object sender, EventArgs e) {
            if (button64bit.Checked) {
                button3264bit.Checked = false;
            }
        }

        private bool ValidateInput() {
            if (!Regex.IsMatch(pimsIdTextBox.Text, "^[a-zA-Z0-9]+$")) {
                MessageBox.Show("Please, enter a proper PIMS ID(e.g.P012345)", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                pimsIdTextBox.Focus();
                pimsIdTextBox.SelectAll();
                return false;
            }

            if (!Regex.IsMatch(appNameTextBox.Text, "^[a-zA-Z0-9_. ]*$")) {
                MessageBox.Show("Please, enter a proper Application Name (e.g. no special characters)", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                appNameTextBox.Focus();
                appNameTextBox.SelectAll();
                return false;
            }

            if (!Regex.IsMatch(appVerTextBox.Text, "^[a-zA-Z0-9_. ]*$")) {
                MessageBox.Show("Please, enter a proper Application Name (e.g. no special characters)", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                appVerTextBox.Focus();
                appNameTextBox.SelectAll();
                return false;
            }

            if (!Regex.IsMatch(appVerTextBox.Text, "^[a-zA-Z0-9_.]*$")) {
                MessageBox.Show("Please, enter a proper Application Version (e.g. no special characters)", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                appVerTextBox.Focus();
                appVerTextBox.SelectAll();
                return false;
            }

            if (!Regex.IsMatch(pkgNameTextBox.Text, "^[a-zA-Z_]*$")) {
                MessageBox.Show("Please, enter a proper Package Name (e.g. bmw_sample_i_i)", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                pkgNameTextBox.Focus();
                pkgNameTextBox.SelectAll();
                return false;
            }

            if (!Regex.IsMatch(pkgVerTextBox.Text, "^[\\d.\\d]*$")) {
                MessageBox.Show("Please, enter a proper Package Version (e.g. 1.0, 2.0...)", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                pkgVerTextBox.Focus();
                pkgVerTextBox.SelectAll();
                return false;
            }

            if (!Regex.IsMatch(pkgrNameTextBox.Text, "^[a-zA-Z]*\\s[a-zA-Z]*$")) {
                MessageBox.Show("Please, enter a proper Packager Name. (e.g. Firstname Lastname)", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                pkgrNameTextBox.Focus();
                pkgrNameTextBox.SelectAll();
                return false;
            }
            return true;
        }

        private void createBtn_Click(object sender, EventArgs e) {
            if (!ValidateInput()) {
                return;
            }
            loadingAnimation.Visible = true;
            backgroundWorker1.RunWorkerAsync();
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e) {
            startProcessing();
            
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            loadingAnimation.Visible = false;
            if (e.Error != null) {
                logger.Log("Error:     " + e.Error.Message + "");
                MessageBox.Show(e.Error.Message , this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else {
                logger.Log("Finished!");
                MessageBox.Show("Success!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        /*        private void pkgVerTextBox_KeyPress(object sender, KeyPressEventArgs e) {
                    switch (e.KeyChar) {
                        case '0':
                        case '1':
                        case '2':
                        case '3':
                        case '4':
                        case '5':
                        case '6':
                        case '7':
                        case '8':
                        case '9':
                        case '.':
                        case '\b':

                            break;
                        default:
                            e.Handled = true;
                            break;
                    }

                }*/
    }



    public class Project {
        string folderPath;
        string msiName;
        string pimsId;
        string appName;
        string appVer;
        string pkgName;
        string pkgVer;
        string authorName;
        string productCode;
        string upgradeCode;
        string featureName;
        string comments;

        public string FolderPath { get => folderPath; set => folderPath = value; }
        public string MsiName { get => msiName; set => msiName = value; }
        public string PimsId { get => pimsId; set => pimsId = value; }
        public string AppName { get => appName; set => appName = value; }
        public string AppVer { get => appVer; set => appVer = value; }
        public string PkgName { get => pkgName; set => pkgName = value; }
        public string PkgVer { get => pkgVer; set => pkgVer = value; }
        public string AuthorName { get => authorName; set => authorName = value; }
        public string ProductCode { get => productCode; set => productCode = value; }
        public string UpgradeCode { get => upgradeCode; set => upgradeCode = value; }
        public string FeatureName { get => featureName; set => featureName = value; }
        public string Comments { get => comments; set => comments = value; }
    }
}

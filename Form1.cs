using System;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Drawing;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using MetroFramework.Controls;

namespace AutomationTool {
    public partial class Form1 : MetroFramework.Forms.MetroForm {
        public Form1() {
            InitializeComponent();
            

        }

        private string _loadMsiPath = "";
        private string _loadMstPath = "";
        private static string _fullMsiPath = "";
        private string _projectFolder = "";

        private void browseBtn1_Click(object sender, EventArgs e) {

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

        private MainProcessor getMainProcessor() {
            logger.ConfigureLogging();
            msiEditor.Logger = logger;
            adm.Logger = logger;
            creator.Logger = logger;

            ProjectInfo proj = new ProjectInfo("", "", pimsIdTextBox.Text, appNameTextBox.Text, appVerTextBox.Text, pkgNameTextBox.Text, pkgVerTextBox.Text,
                                                String.Format("{0}, {1}", pkgrNameTextBox.Text, "DXC"), "", "", String.Format("{0}_{1}_{2}", "BMW", pkgNameTextBox.Text, pkgVerTextBox.Text), 
                                                "", customMsiCheckBox.Checked, Is32bit());
            //proj.isCustomMsi = customMsiCheckBox.Checked;
           //proj.ProductCode = "";
            //proj.UpgradeCode = "";
            if (proj.isCustomMsi) {
                proj.FolderPath = "";
                proj.MsiName = "";
            } else {
                proj.FolderPath = Path.GetDirectoryName(_fullMsiPath) + "\\";
                proj.MsiName = Path.GetFileNameWithoutExtension(_fullMsiPath);
            }
            //proj.is32bit = Is32bit();
            //proj.PimsId = pimsIdTextBox.Text;
            //proj.AppName = appNameTextBox.Text;
            //proj.AppVer = appVerTextBox.Text;
            //proj.PkgName = pkgNameTextBox.Text;
            //proj.PkgVer = pkgVerTextBox.Text;
            //proj.AuthorName = String.Format("{0}, {1}", pkgrNameTextBox.Text, "DXC");
            //proj.FeatureName = String.Format("{0}_{1}_{2}", "BMW", proj.PkgName, proj.PkgVer);
            string dateNow = DateTime.Now.ToString("d.M.yyyy");
            string[] pkgrNameArr = proj.AuthorName.Split(' ');
            char first = pkgrNameArr[0].ToCharArray()[0];
            char second = pkgrNameArr[1].ToCharArray()[0];
            proj.Comments = String.Format("{0}{1}, {2}, {3}", first.ToString().ToLowerInvariant(), second.ToString().ToLowerInvariant(), dateNow, "IS v23");


            adm.ProjectFolder = _projectFolder;
            adm.FullMsiPath = _fullMsiPath;
            creator.Adm = adm;
            creator.MsiEditor = msiEditor;

            MainProcessor main = new MainProcessor(msiEditor, adm, creator, logger, proj);
            return main;
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
            MainProcessor main = getMainProcessor();
            progressBar1.Value = 0;
            progressBar1.Visible = true;
            progressBar1.Maximum = main.MaxProgressValue;
            backgroundWorker1.RunWorkerAsync(main);
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e) {
            MainProcessor main = (MainProcessor)e.Argument;
            main.ProgressChanged += SetProgress;
            main.startProcessing();
        }

        private void SetProgress(object sender, MainProcessorEventArgs e) {
            backgroundWorker1.ReportProgress(e.Progress);
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e) {

            progressBar1.Value = e.ProgressPercentage;
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

        private void metroButton1_Click(object sender, EventArgs e) {
            ofd1.Filter = "MST|*.mst";
            if (ofd1.ShowDialog() == DialogResult.OK) {
                loadMstTextBox.Text = ofd1.FileName;
                _loadMstPath = ofd1.FileName;
            }

            Dictionary<string, string> propDict = new Dictionary<string, string>();
            // add summary stream info
            propDict.Add("ALLUSERS", "1");
            propDict.Add("ARPNOMODIFY", "1");
            propDict.Add("ARPNOREMOVE", "1");
            propDict.Add("ARPNOREPAIR", "1");
            propDict.Add("BMW_Package_Author", String.Format("{0}, {1}", pkgrNameTextBox.Text, "DXC"));
            propDict.Add("BMW_PackageName", pkgNameTextBox.Text);
            propDict.Add("BMW_PackageVersion", pkgVerTextBox.Text);
            propDict.Add("Manufacturer", "BMW Package Factory");
            propDict.Add("MSIRESTARTMANAGERCONTROL", "Disable");
            propDict.Add("ProductName", appNameTextBox.Text);
            propDict.Add("ProductVersion", appVerTextBox.Text);
            propDict.Add("PROMPTROLLBACKCOST", "D");
            propDict.Add("REBOOT", "ReallySuppress");
            propDict.Add("REBOOTPROMPT", "S");


            MsiEditor msiEditor = new MsiEditor();

/*            proj.FeatureName = String.Format("{0}_{1}_{2}", "BMW", proj.PkgName, proj.PkgVer);
            string dateNow = DateTime.Now.ToString("d.M.yyyy");
            string[] pkgrNameArr = proj.AuthorName.Split(' ');
            char first = pkgrNameArr[0].ToCharArray()[0];
            char second = pkgrNameArr[1].ToCharArray()[0];
            proj.Comments = String.Format("{0}{1}, {2}, {3}", first.ToString().ToLowerInvariant(), second.ToString().ToLowerInvariant(), dateNow, "IS v23");*/

            Dictionary<string, string> extract = msiEditor.checkProperties(_loadMsiPath, _loadMstPath);

            DataGridViewRow row = new DataGridViewRow();

            int rowNumber = 0;
            foreach (var pair in extract) {
                string value;
                if (propDict.TryGetValue(pair.Key, out value)) {
                    if (!value.Equals(pair.Value)) {
                        metroGrid1.Rows.Add(pair.Key, pair.Value, "Fail");
                        metroGrid1.Rows[rowNumber].Cells[0].ToolTipText = String.Format("Expected {0}", pair.Value);
                        metroGrid1.Rows[rowNumber].Cells[0].Style.ForeColor = System.Drawing.Color.Red;
                        metroGrid1.Rows[rowNumber].Cells[1].Style.ForeColor = System.Drawing.Color.Red;
                        metroGrid1.Rows[rowNumber].Cells[2].Style.ForeColor = System.Drawing.Color.Red;
                    } else {
                        metroGrid1.Rows.Add(pair.Key, pair.Value, "Pass");
                        metroGrid1.Rows[rowNumber].Cells[0].Style.ForeColor = System.Drawing.Color.Green;
                        metroGrid1.Rows[rowNumber].Cells[1].Style.ForeColor = System.Drawing.Color.Green;
                        metroGrid1.Rows[rowNumber].Cells[2].Style.ForeColor = System.Drawing.Color.Green;
                    }
                } else {
                    metroGrid1.Rows.Add(pair.Key, pair.Value, "Fail");
                    metroGrid1.Rows[rowNumber].Cells[0].ToolTipText = String.Format("Expected {0}", pair.Value);
                    metroGrid1.Rows[rowNumber].Cells[0].Style.ForeColor = System.Drawing.Color.Red;
                    metroGrid1.Rows[rowNumber].Cells[1].Style.ForeColor = System.Drawing.Color.Red;
                    metroGrid1.Rows[rowNumber].Cells[2].Style.ForeColor = System.Drawing.Color.Red;
                }
                rowNumber++;
            }
            
        }

        private void loadMsiButton_Click(object sender, EventArgs e) {
            ofd1.Filter = "MSI|*.msi";
            if (ofd1.ShowDialog() == DialogResult.OK) {
                loadMsiTextBox.Text = ofd1.FileName;
                _loadMsiPath = ofd1.FileName;
            }
        }

        private void Form1_Load(object sender, EventArgs e) {
            this.MaximumSize = new Size(this.Width, this.Height);
            this.MinimumSize = this.MaximumSize;
            this.BackColor = Color.Maroon;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        }

        private void metroGrid1_CellMouseEnter(object sender, DataGridViewCellEventArgs e) {
            if ((e.ColumnIndex == this.metroGrid1.Columns["Status"].Index)) {
                //column name
                DataGridViewCell cell =
                    this.metroGrid1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                //column id
                DataGridViewCell cell1 =
                  this.metroGrid1.Rows[e.RowIndex].Cells["Status"];

                cell.ToolTipText = "DBC";

                if (cell1.Equals("Fail")) {
                    cell.ToolTipText = "Please update NameID as required, To know more click Help icon";
                }

            }
        }
        
    }
}

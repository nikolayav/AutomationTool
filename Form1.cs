using System;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Drawing;
using System.Collections.Generic;

namespace AutomationTool {
    public partial class Form1 : MetroFramework.Forms.MetroForm {
        public Form1() {
            InitializeComponent();
        }

        private string _loadMsiPath = "";
        private string _loadMstPath = "";
        private string _fullMsiPath = "";
        private string _fullMstPath = "";
        private string _projectFolder = "";
        private bool _isQcTableLoaded = false;
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
                                                "", Is32bit(), IsCustomMsi(), editMstCheckBox.Checked, fileLocationMstTextBox1.Text);
            if (proj.isCustomMsi) {
                proj.FolderPath = "";
                proj.MsiName = "";
            } else if (!proj.isEditMst && !proj.isCustomMsi){
                proj.FolderPath = Path.GetDirectoryName(_fullMsiPath) + "\\";
                proj.MsiName = Path.GetFileNameWithoutExtension(_fullMsiPath);
            } else {
                proj.FolderPath = Path.GetDirectoryName(_fullMsiPath) + "\\";
                proj.MsiName = Path.GetFileNameWithoutExtension(_fullMsiPath);
                proj.EditMstPath = Path.GetFullPath(_fullMstPath);
            }

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
                if (editMstCheckBox.Checked) {
                    editMstCheckBox.Checked = false;
                    
                }
                fileLocationMstTextBox1.BackColor = Color.Gainsboro;
                fileLocationMstTextBox1.Enabled = false;
                fileLocationTextBox1.BackColor = Color.Gainsboro;
                fileLocationTextBox1.Text = "";
                osComboBox.BackColor = Color.White;
                fileLocationTextBox1.Enabled = false;
                browseBtn1.Enabled = false;
                osComboBox.Enabled = true;
                loadMstBtn1.Enabled = false;
            } else { 
                fileLocationTextBox1.BackColor = Color.White;
                fileLocationTextBox1.Enabled = true;
                browseBtn1.Enabled = true;
                osComboBox.BackColor = Color.Gainsboro;
                osComboBox.Text = "Select OS Architecture";
                osComboBox.Enabled = false;

            }
        }


        private bool ValidateInput(bool msiCheck) {
            if (!Regex.IsMatch(pimsIdTextBox.Text, "^[a-zA-Z0-9]+$")) {
                MessageBox.Show("Please, enter a proper PIMS ID (e.g.P012345)", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                pimsIdTextBox.Focus();
                pimsIdTextBox.SelectAll();
                return false;
            }

            if (!Regex.IsMatch(appNameTextBox.Text, "^[a-zA-Z0-9_. ]*$")) {
                MessageBox.Show("Please, enter a proper Application Name(e.g.no special characters)", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                appNameTextBox.Focus();
                appNameTextBox.SelectAll();
                return false;
            }

            if (!Regex.IsMatch(appVerTextBox.Text, "^[a-zA-Z0-9_. ]*$")) {
                MessageBox.Show("Please, enter a proper Application Version(e.g.no special characters)", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                appVerTextBox.Focus();
                appNameTextBox.SelectAll();
                return false;
            }

            if (!Regex.IsMatch(pkgNameTextBox.Text, "^[a-z_]*$")) {
                MessageBox.Show("Please, enter a proper Package Name (e.g. bmw_sample_i_i", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                pkgNameTextBox.Focus();
                pkgNameTextBox.SelectAll();
                return false;
            }

            if (!Regex.IsMatch(pkgVerTextBox.Text, "^\\d\\.\\d$")) {
                MessageBox.Show("Please, enter a proper Package Version (e.g. 1.0, 1.1...)", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                pkgVerTextBox.Focus();
                pkgVerTextBox.SelectAll();
                return false;
            }

            if (!Regex.IsMatch(pkgrNameTextBox.Text, "^[A-Z][a-z]+\\s[A-Z][a-z]+$")) {
                MessageBox.Show("Please, enter a proper Packager Name. (e.g. Firstname Lastname", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                pkgrNameTextBox.Focus();
                pkgrNameTextBox.SelectAll();
                return false;
            }

            if (!msiCheck && !customMsiCheckBox.Checked && String.IsNullOrEmpty(fileLocationTextBox1.Text) && !editMstCheckBox.Checked) {
                MessageBox.Show("Please, select MSI Path/Edit MST/Custom MSI", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!msiCheck && customMsiCheckBox.Checked && String.IsNullOrEmpty(osComboBox.Text) && !editMstCheckBox.Checked) {
                MessageBox.Show("Please, select select the Target Platform OS Architecture", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!msiCheck && editMstCheckBox.Checked && String.IsNullOrEmpty(fileLocationMstTextBox1.Text)) {
                MessageBox.Show("Please, select path to MST file", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool IsCustomMsi() {
            if (customMsiCheckBox.Checked) {
                return true;
            }
            return false;
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
            if (!ValidateInput(false)) {
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
                MessageBox.Show(e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            } else {
                logger.Log("Finished!");
                MessageBox.Show("Finished", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void metroButton1_Click(object sender, EventArgs e) {
            ofd1.Filter = "MST|*.mst";
            if (ofd1.ShowDialog() == DialogResult.OK) {
                loadMstTextBox.Text = ofd1.FileName;
                _loadMstPath = ofd1.FileName;
            }
            if (!ValidateInput(true)) {
                return;
            }
            Dictionary<string, string> propDict = new Dictionary<string, string>();
            // add summary stream info

            
            string[] pkgrNameArr = pkgrNameTextBox.Text.Split(' ');
            char first = pkgrNameArr[0].ToCharArray()[0];
            char second = pkgrNameArr[1].ToCharArray()[0];
            string comments = String.Format("{0}{1}, {2}, {3}", first.ToString().ToLowerInvariant(), second.ToString().ToLowerInvariant(), "", "IS v23");

            propDict.Add("Title", appNameTextBox.Text);
            propDict.Add("Subject", appNameTextBox.Text);
            propDict.Add("Author", String.Format("{0}, DXC", pkgrNameTextBox.Text));
            propDict.Add("Comments", comments);
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
            ProjectInfo proj = new ProjectInfo("", "", pimsIdTextBox.Text, appNameTextBox.Text, appVerTextBox.Text, pkgNameTextBox.Text, pkgVerTextBox.Text,
                                                String.Format("{0}, {1}", pkgrNameTextBox.Text, "DXC"), "", "", String.Format("{0}_{1}_{2}", "BMW", pkgNameTextBox.Text, pkgVerTextBox.Text),
                                                "", customMsiCheckBox.Checked, Is32bit(), editMstCheckBox.Checked, fileLocationMstTextBox1.Text);

            Dictionary<string, string> extract = msiEditor.checkProperties(_loadMsiPath, _loadMstPath);
            DataGridViewRow row = new DataGridViewRow();

            
            int rowNumber = 0;
            foreach (var pair in extract) {
                string value;
                if (propDict.TryGetValue(pair.Key, out value)) {
                    if (!value.Equals(pair.Value)) {
                        metroGrid1.Rows.Add(pair.Key, pair.Value, "Fail");
                        metroGrid1.Rows[rowNumber].Cells[0].Style.ForeColor = Color.Red;
                        metroGrid1.Rows[rowNumber].Cells[1].Style.ForeColor = Color.Red;
                        metroGrid1.Rows[rowNumber].Cells[2].Style.ForeColor = Color.Red;
                        metroGrid1.Rows[rowNumber].Cells[2].ToolTipText = String.Format("\"{0}\" expected", value);
                    } else {
                        metroGrid1.Rows.Add(pair.Key, pair.Value, "Pass");
                        metroGrid1.Rows[rowNumber].Cells[0].Style.ForeColor = Color.Green;
                        metroGrid1.Rows[rowNumber].Cells[1].Style.ForeColor = Color.Green;
                        metroGrid1.Rows[rowNumber].Cells[2].Style.ForeColor = Color.Green;
                    }
                } else {
                    metroGrid1.Rows.Add(pair.Key, pair.Value, "Fail");
                    metroGrid1.Rows[rowNumber].Cells[0].Style.ForeColor = Color.Red;
                    metroGrid1.Rows[rowNumber].Cells[1].Style.ForeColor = Color.Red;
                    metroGrid1.Rows[rowNumber].Cells[2].Style.ForeColor = Color.Red;
                    metroGrid1.Rows[rowNumber].Cells[2].ToolTipText = "Missing";
                }
                rowNumber++;
            }
            _isQcTableLoaded = true;
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

        private void loadMstBtn1_Click(object sender, EventArgs e) {
            ofd2.Filter = "MST|*.mst";
            if (ofd2.ShowDialog() == DialogResult.OK) {
                fileLocationMstTextBox1.Text = ofd2.FileName;
                _fullMstPath = ofd2.FileName;
            }
        }

        private void editMstCheckBox_CheckedChanged(object sender, EventArgs e) {
            if (editMstCheckBox.Checked) {
                fileLocationMstTextBox1.Enabled = true;
                fileLocationMstTextBox1.BackColor = Color.White;
                browseBtn1.Enabled = true;
                loadMstBtn1.Enabled = true;
                if (customMsiCheckBox.Checked) {
                    customMsiCheckBox.Checked = false;
                }
                
            } else {
                loadMstBtn1.Enabled = false;
                fileLocationMstTextBox1.Enabled = false;
                fileLocationMstTextBox1.BackColor = Color.Gainsboro;
            }
        }

    }
}

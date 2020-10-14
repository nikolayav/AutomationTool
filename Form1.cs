using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Deployment.WindowsInstaller;
using Excel = Microsoft.Office.Interop.Excel;
using Workbook = Microsoft.Office.Interop.Excel.Workbook;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using NLog;
using NLog.Config;
using NLog.Targets;
using NLog.Targets.Wrappers;

namespace AutomationTool {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private static string _fullMsiPath = "";
        private string _projectFolder = "";
        
        private void browseBtn_Click(object sender, EventArgs e) {

            ofd1.Filter = "MSI|*.msi";
            if (ofd1.ShowDialog() == DialogResult.OK) {
                fileLocationTextBox1.Text = ofd1.FileName;
                _fullMsiPath = ofd1.FileName;
            }
        }
        
 
        private void createBtn_Click(object sender, EventArgs e) {
            ConfigureLogging();

            try {

                if (!Regex.IsMatch(pimsIdTextBox.Text, "^[a-zA-Z0-9]+$")) {
                    MessageBox.Show("Please, enter a proper PIMS ID(e.g.P012345)", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    pimsIdTextBox.Focus();
                    pimsIdTextBox.SelectAll();
                    return;
                }

                if (!Regex.IsMatch(appNameTextBox.Text, "^[a-zA-Z0-9_. ]*$")) {
                    MessageBox.Show("Please, enter a proper Application Name (e.g. no special characters)", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    appNameTextBox.Focus();
                    appNameTextBox.SelectAll();
                    return;
                }

                if (!Regex.IsMatch(appVerTextBox.Text, "^[a-zA-Z0-9_. ]*$")) {
                    MessageBox.Show("Please, enter a proper Application Name (e.g. no special characters)", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    appVerTextBox.Focus();
                    appNameTextBox.SelectAll();
                    return;
                }

                if (!Regex.IsMatch(appVerTextBox.Text, "^[a-zA-Z0-9_.]*$")) {
                    MessageBox.Show("Please, enter a proper Application Version (e.g. no special characters)", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    appVerTextBox.Focus();
                    appVerTextBox.SelectAll();
                    return;
                }

                if (!Regex.IsMatch(pkgNameTextBox.Text, "^[a-zA-Z_]*$")) {
                    MessageBox.Show("Please, enter a proper Package Name (e.g. bmw_sample_i_i)", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    pkgNameTextBox.Focus();
                    pkgNameTextBox.SelectAll();
                    return;
                }

                if (!Regex.IsMatch(pkgVerTextBox.Text, "^[\\d.\\d]*$")) {
                    MessageBox.Show("Please, enter a proper Package Version (e.g. 1.0, 2.0...)", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    pkgVerTextBox.Focus();
                    pkgVerTextBox.SelectAll();
                    return;
                }

                if (!Regex.IsMatch(pkgrNameTextBox.Text, "^[a-zA-Z]*\\s[a-zA-Z]*$")) {
                    MessageBox.Show("Please, enter a proper Packager Name. (e.g. Firstname Lastname)", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    pkgrNameTextBox.Focus();
                    pkgrNameTextBox.SelectAll();
                    return;
                }

                Project proj = new Project();
                proj.ProductCode = "";
                proj.UpgradeCode = "";
                proj.FolderPath = Path.GetDirectoryName(_fullMsiPath) + "\\";
                proj.MsiName = Path.GetFileNameWithoutExtension(_fullMsiPath);
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

                createFolders(String.Format("{0}_{1}", proj.PkgName, proj.PkgVer), ref _projectFolder);

                GenerateMst(proj);
                createXML(proj);

            } catch (Exception ex) {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                log("Error:     " + ex.Message + "");
            }

                log("Finished!");
            
            MessageBox.Show("Success!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
       
        private void createFolders(string pkgName, ref string _projectFolder) {

            

            _projectFolder = Path.Combine(Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)), "Temp", pkgName);
            if (!Directory.Exists(_projectFolder)) {
                log("System:     Creating directory " + _projectFolder + "...");
                Directory.CreateDirectory(_projectFolder);

                string[] folders = { "Doku", "Final", "Source", "Work" };

                foreach (string folder in folders) {
                    log("System:     Creating directory " + Path.Combine(_projectFolder, folder + "..."));
                    Directory.CreateDirectory(Path.Combine(_projectFolder, folder));
                }
            }
        }
        private void GenerateMst(Project proj) {
            
            log("File:     Extracting .reg info...");

            string auditKeyPath = "";
            var regFileLines = File.ReadAllLines(@"BMWClient.reg");
            Dictionary<string, string> regDict = new Dictionary<string, string>();

            bool startReading = false;
            foreach (string line in regFileLines) {
                if (!startReading) {
                    if (line.StartsWith("[")) {
                        if (line.Contains("[HKEY_LOCAL_MACHINE\\")) {
                            auditKeyPath = line;
                            auditKeyPath = auditKeyPath.Replace("[HKEY_LOCAL_MACHINE\\", "").Replace("]]", "]");
                        }
                        startReading = true;
                    }
                } else {
                    string[] lineSplit = line.Replace("\"", "").Split('=');
                    if (lineSplit.Length == 2) {
                        regDict.Add(lineSplit[0], lineSplit[1]);
                    }
                }
            }

           
            string referenceDb = _fullMsiPath;
            string tempDb = Path.Combine(Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)), "Temp", _projectFolder, "Work", String.Format("{0}.msi_tmp1", proj.MsiName));
            string transform = Path.Combine(Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)), "Temp", _projectFolder, "Work", String.Format("{0}_{1}.mst", proj.PkgName, proj.PkgVer));
            const string auditComponentx86 = "BMW_Client_Info";
            const string auditComponentx64 = "BMW_Client_Infox64";

            checkIfFileExistsAndRenameOldFile(transform);
            checkIfFileExistsAndRenameOldFile(tempDb);

            log("System:     Creating " + Path.GetFileName(transform) + " and " + Path.GetFileName(tempDb) + " files...");

            File.Copy(referenceDb, tempDb, true);

            try {
                using (var origDatabase = new Database(referenceDb, DatabaseOpenMode.ReadOnly)) {
                    using (var db = new Database(tempDb, DatabaseOpenMode.Direct)) {
                        proj.ProductCode = GetProductAndUpgradeCodes(db, "ProductCode");
                        proj.UpgradeCode = GetProductAndUpgradeCodes(db, "UpgradeCode");

                        Feature_AddOrUpdate(db, proj.FeatureName, proj.FeatureName, "0", "1", "INSTALLDIR", "48");

                        Component_AddOrUpdate(db, auditComponentx86, GenerateUniqueGuid(db, "component"), "TARGETDIR", "4", proj.FeatureName);
                        Component_AddOrUpdate(db, auditComponentx64, GenerateUniqueGuid(db, "component"), "TARGETDIR", "260", proj.FeatureName);

                        
                        foreach (KeyValuePair<string, string> kvp in regDict) {
                            if (kvp.Key.Equals("MSIPackageName")) {
                                Reg_AddOrUpdate(db, GenerateUniqueGuid(db, "registry"), "2", auditKeyPath, kvp.Key, Path.GetFileName(_fullMsiPath), auditComponentx64);
                                Reg_AddOrUpdate(db, GenerateUniqueGuid(db, "registry"), "2", auditKeyPath, kvp.Key, Path.GetFileName(_fullMsiPath), auditComponentx86);
                            } else {
                                Reg_AddOrUpdate(db, GenerateUniqueGuid(db, "registry"), "2", auditKeyPath, kvp.Key, kvp.Value, auditComponentx64);
                                Reg_AddOrUpdate(db, GenerateUniqueGuid(db, "registry"), "2", auditKeyPath, kvp.Key, kvp.Value, auditComponentx86);
                            }
                        }

                       
                        Property_AddOrUpdate("ALLUSERS", "1", db);
                        Property_AddOrUpdate("ARPNOMODIFY", "1", db);
                        Property_AddOrUpdate("ARPNOREMOVE", "1", db);
                        Property_AddOrUpdate("ARPNOREPAIR", "1", db);
                        Property_AddOrUpdate("BMW_Package_Author", proj.AuthorName, db);
                        Property_AddOrUpdate("BMW_PackageName", proj.PkgName, db);
                        Property_AddOrUpdate("BMW_PackageVersion", proj.PkgVer, db);
                        Property_AddOrUpdate("Manufacturer", "BMW Package Factory", db);
                        Property_AddOrUpdate("MSIRESTARTMANAGERCONTROL", "Disable", db);
                        Property_AddOrUpdate("ProductName", proj.AppName, db);
                        Property_AddOrUpdate("ProductVersion", proj.AppVer, db);
                        Property_AddOrUpdate("PROMPTROLLBACKCOST", "D", db);
                        Property_AddOrUpdate("REBOOT", "ReallySuppress", db);
                        Property_AddOrUpdate("REBOOTPROMPT", "S", db);

                        log("Transform:     Editing summary information stream...");

                        // Edit summary info stream
                        db.SummaryInfo.Title = proj.AppName;
                        db.SummaryInfo.Subject = proj.AppName;
                        db.SummaryInfo.Comments = proj.Comments;
                        db.SummaryInfo.Author = proj.AuthorName;

                        log("Transform:     Saving " + Path.GetFileName(transform) + " file...");

                        db.GenerateTransform(origDatabase, transform);
                        db.Commit();
                        db.CreateTransformSummaryInfo(origDatabase, transform, TransformErrors.None, TransformValidations.None);
                    }
                }

            } catch {
                throw;
            } finally {
                File.Delete(tempDb);
            }

        }


        private void createXML(Project proj) {
            string newXLFile = Path.Combine(Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)), "Temp", _projectFolder, "Doku", String.Format("{0}_{1}_1.xlsx", proj.PkgName, proj.PkgVer));

            if (File.Exists(@"xxx_xxxxxxxx_x_x_x.x_1.xlsx")) {
                if (File.Exists(newXLFile)) {
                    bool isRenamed = false;
                    string oldFileName = String.Format("{0}_old1", newXLFile);
                    while (!isRenamed) {
                        if (File.Exists(oldFileName)) {
                            int num = Convert.ToInt32(oldFileName.Remove(0, oldFileName.Length - 1));
                            num += 1;
                            oldFileName = oldFileName.Remove(oldFileName.Length - 1) + num.ToString();
                        } else {
                            try {
                                File.Move(newXLFile, oldFileName);
                                isRenamed = true;
                            } catch (Exception ex) {
                                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                log("Error:     "+ ex.Message + "");
                                return;
                            }
                        }
                    }
                }
                log("System:     Copying " + Path.GetFileName(newXLFile) + "...");

                File.Copy(@"xxx_xxxxxxxx_x_x_x.x_1.xlsx", newXLFile);
            }

            log("Excel:     Updating cells in " + Path.GetFileName(newXLFile) + "...");

            Excel.Application excel = new Excel.Application();
            Workbook wb = excel.Workbooks.Open(newXLFile);
            Excel.Worksheet xlSheet = wb.Sheets["Universal"];
            xlSheet.Activate();
            xlSheet.Cells[5, 3].Value = pimsIdTextBox.Text;
            xlSheet.Cells[6, 3].Value = proj.PkgName;
            xlSheet.Cells[7, 3].Value = proj.PkgVer;
            xlSheet.Cells[8, 3].Value = String.Format("{0}_{1}", proj.PkgName, proj.PkgVer);
            xlSheet.Cells[9, 3].Value = proj.AppName;
            xlSheet.Cells[10, 3].Value = proj.AppVer;
            xlSheet.Cells[33, 3].Value = proj.AppName;
            xlSheet.Cells[34, 3].Value = proj.AppName;
            xlSheet.Cells[35, 3].Value = proj.AuthorName;
            xlSheet.Cells[36, 3].Value = proj.Comments;

            xlSheet.Cells[76, 3].Value = proj.ProductCode;
            xlSheet.Cells[77, 3].Value = proj.ProductCode;
            xlSheet.Cells[78, 3].Value = proj.UpgradeCode;
            xlSheet.Cells[79, 3].Value = proj.UpgradeCode;

            log("Excel:     Saving " + Path.GetFileName(newXLFile) + "...");
            
            wb.Save();
            wb.Close();

            GenerateInstallUninstallVbs(proj, String.Format("{0}_{1}_install.vbs", proj.PkgName, proj.PkgVer), String.Format("{0}_{1}_uninstall.vbs", proj.PkgName, proj.PkgVer)); 

        }

        private void GenerateInstallUninstallVbs(Project proj, string installvbs, string uninstallvbs) {
            checkIfFileExistsAndRenameOldFile(Path.Combine(_projectFolder, "Work", installvbs));
            checkIfFileExistsAndRenameOldFile(Path.Combine(_projectFolder, "Work", uninstallvbs));

            //File.Copy(@"install.vbs", Path.Combine(_projectFolder, "Work", installvbs));
            //File.Copy(@"uninstall.vbs", Path.Combine(_projectFolder, "Work", uninstallvbs));

            var vbsFileLines = File.ReadAllLines(@"install.vbs");
            string lineToWrite = "";
            using (FileStream fs = File.OpenWrite(Path.Combine(_projectFolder, "Work", installvbs))) {
                using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8)) {
                    for (int i = 0; i < vbsFileLines.Length; i++) {
                        if (vbsFileLines[i].Contains("Dim LogFileName")) {
                            string[] lineSplit = vbsFileLines[i].Replace("\"", "").Split('=');
                            lineToWrite = vbsFileLines[i].Replace(lineSplit[1].Trim(), String.Format("{0}_{1}.log", proj.PkgName, proj.PkgVer));
                        } else if (vbsFileLines[i].Contains("ProjectName = ")) {
                            string[] lineSplit = vbsFileLines[i].Replace("\"", "").Split('=');
                            lineToWrite = vbsFileLines[i].Replace(lineSplit[1].Trim(), String.Format("{0}_{1}", proj.PkgName, proj.PkgVer));
                        } else if (vbsFileLines[i].Contains("Command(0) = ")) {
                            string[] lineSplit = vbsFileLines[i].Replace("\"", "").Split('=');
                            lineToWrite = String.Format("Command(0) = \"msiexec.exe /i \"\"{0}\"\" TRANSFORMS=\"\"{1}\"\" /qn /l*v %temp%\\{2}\"", Path.GetFileName(_fullMsiPath), String.Format("{0}_{1}.mst", proj.PkgName, proj.PkgVer), String.Format("{0}_{1}.install.log", proj.PkgName, proj.PkgVer));
                        } else {
                            lineToWrite = vbsFileLines[i];
                        }
                        sw.WriteLine(lineToWrite);
                    }
                }
            }

            vbsFileLines = File.ReadAllLines(@"uninstall.vbs");

            using (FileStream fs = File.OpenWrite(Path.Combine(_projectFolder, "Work", uninstallvbs))) {
                using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8)) {
                    for (int i = 0; i < vbsFileLines.Length; i++) {
                        if (vbsFileLines[i].Contains("Dim LogFileName")) {
                            string[] lineSplit = vbsFileLines[i].Replace("\"", "").Split('=');
                            lineToWrite = vbsFileLines[i].Replace(lineSplit[1].Trim(), String.Format("{0}_{1}.log", proj.PkgName, proj.PkgVer));
                        } else if (vbsFileLines[i].Contains("ProjectName = ")) {
                            string[] lineSplit = vbsFileLines[i].Replace("\"", "").Split('=');
                            lineToWrite = vbsFileLines[i].Replace(lineSplit[1].Trim(), String.Format("{0}_{1}", proj.PkgName, proj.PkgVer));
                        } else if (vbsFileLines[i].Contains("Command(0) = ")) {
                            string[] lineSplit = vbsFileLines[i].Replace("\"", "").Split('=');
                            lineToWrite = String.Format("Command(0) = \"msiexec.exe /x \"\"{0}\"\" /qn /l*v %temp%\\{1}\"", Path.GetFileName(proj.ProductCode), String.Format("{0}_{1}.uninstall.log", proj.PkgName, proj.PkgVer));
                        } else {
                            lineToWrite = vbsFileLines[i];
                        }
                        sw.WriteLine(lineToWrite);
                    }
                }
            }
        }

        private void checkIfFileExistsAndRenameOldFile(string filePath) {
            if (File.Exists(filePath)) {
                bool isRenamed = false;
                string oldFilePath = String.Format("{0}_old1", filePath);
                while (!isRenamed) {
                    if (File.Exists(oldFilePath)) {
                        int num = Convert.ToInt32(oldFilePath.Remove(0, oldFilePath.Length - 1));
                        num += 1;
                        oldFilePath = oldFilePath.Remove(oldFilePath.Length - 1) + num.ToString();
                    } else {
                        log("File " + filePath + " already exists! Renaming to " + oldFilePath + "");
                        File.Move(filePath, oldFilePath);
                        isRenamed = true;
                    }
                }
            }
        }
        private void Feature_AddOrUpdate(Database db, string feature, string title, string display, string level, string directory_, string attributes) {
            log("Transform:     Creating feature '" + feature + "'...");

            IList featuresWithSameName = db.ExecuteQuery("SELECT * FROM Feature where Feature = '" + feature + "'");
            if (featuresWithSameName.Count > 0) {
                db.Execute("UPDATE Feature SET Feature = '" + feature + "', Title = '" + title + "', Display = '" + display + "', Level = '" + level + "', Directory_ = '" + directory_ + "', Attributes = '" + attributes + "' WHERE Feature = '" + feature + "'");
            } else {
                db.Execute("INSERT INTO `Feature` (Feature, Title, Display, Level, Directory_, Attributes) VALUES ('" + feature + "', '" + title + "', '" + display + "', '" + level + "', '" + directory_ + "', '" + attributes + "')");
            }
        }

        private string GetProductAndUpgradeCodes(Database db, string codeType) {
            string code = "";
            if (codeType.Equals("ProductCode")) {
                IList productCode = db.ExecuteQuery("SELECT Value FROM Property WHERE Property = 'ProductCode'");
                if (productCode[0] != null) {
                    code = productCode[0].ToString();
                }
            } else if (codeType.Equals("UpgradeCode")) {
                IList upgradeCode = db.ExecuteQuery("SELECT Value FROM Property WHERE Property = 'UpgradeCode'");
                if (upgradeCode[0] != null) {
                    code = upgradeCode[0].ToString();
                }
            }
            return code;
        }

        private void Component_AddOrUpdate(Database db, string component, string componentId, string directory_, string attributes, string feature) {
                log("Transform:     Creating component '" + component + "'");
            IList featuresWithSameName = db.ExecuteQuery("SELECT * FROM Component where Component = '" + component + "'");
            if (featuresWithSameName.Count > 0) {
                db.Execute("UPDATE Component SET Component = '" + component + "', ComponentId = '" + componentId + "', Directory_ = '" + directory_ + "', Attributes = '" + attributes + "' WHERE Component = '" + component + "'");

            } else {
                db.Execute("INSERT INTO `Component` (Component, ComponentId, Directory_, Attributes) VALUES ('" + component + "', '" + componentId + "', '" + directory_ + "', '" + attributes + "')");
            }

            IList associatedFeatures = db.ExecuteQuery("SELECT * FROM FeatureComponents where Component_ = '" + component + "'");
            if (associatedFeatures.Count > 0) {
                db.Execute("UPDATE Component SET Feature_ = '" + feature + "', Component_ = '" + component + "'");
            } else {
                db.Execute("INSERT INTO `FeatureComponents` (Feature_, Component_) VALUES ('" + feature + "', '" + component + "')");
            }
        }

        private void Reg_AddOrUpdate(Database db, string registry, string root, string key, string name, string value, string component) {
                log("Transform:     Creating registry value '" + value + "' in HLKM\\" + key + "");
            
            db.Execute("INSERT INTO `Registry` (`Registry`, `Root`, `Key`, `Name`, `Value`, `Component_`) VALUES ('{0}', {1}, '{2}', '{3}', '{4}', '{5}')", registry, root, key, name, value, component);
        }

        private string GenerateUniqueGuid(Database db, string type) {
            IList existingRegs = db.ExecuteQuery("SELECT Registry FROM Registry");
            string guid = "";
            if (type.Equals("registry")) {
                guid = "_" + Guid.NewGuid().ToString().ToUpperInvariant();
                guid = Regex.Replace(guid, "-", "");

                foreach (string componentId in existingRegs) {
                    if (componentId.Equals(guid, StringComparison.InvariantCultureIgnoreCase)) {
                        return GenerateUniqueGuid(db, type);
                    }
                }
            } else if (type.Equals("component")) {
                IList existingComponents = db.ExecuteQuery("SELECT ComponentId FROM Component");
                guid = "{" + Guid.NewGuid().ToString().ToUpperInvariant() + "}";

                foreach (string componentId in existingComponents) {
                    if (componentId.Equals(guid, StringComparison.InvariantCultureIgnoreCase)) {
                        return GenerateUniqueGuid(db, type);
                    }
                }
            }

            return guid;
        }

        private void Property_AddOrUpdate(string property, string value, Database db) {
            
            IList existingProperties = db.ExecuteQuery("SELECT * FROM Property where Property = '" + property + "'");
            if (existingProperties.Count > 0) {
                log("Transform:     Updating property " + property + " = " + value + "");
                db.Execute("UPDATE Property SET Value = '" + value + "' WHERE Property = '" + property + "'");
            } else {
                log("Transform:     Creating property " + property + " = " + value + "");
                db.Execute("INSERT INTO Property (Property, Value) VALUES ('" + property + "', '" + value + "')");
            }
        }

        private void fileLocationTextBox1_Enter(object sender, EventArgs e) {
            fileLocationTextBox1.SelectAll();
        }
        private void ConfigureLogging() {
            // Intialize Config Object
            LoggingConfiguration config = new LoggingConfiguration();

            // Initialize Console Target
            var consoleTarget = new ColoredConsoleTarget("Console Target") {
                Layout = @"${time} ${longdate} ${uppercase: ${level}} ${logger} ${message} ${exception: format=ToString}"
            };

            // Initialize the AsyncWrapper for the ConsoleTarget
            AsyncTargetWrapper consoleWrapper = new AsyncTargetWrapper();
            consoleWrapper.WrappedTarget = consoleTarget;
            consoleWrapper.OverflowAction = AsyncTargetWrapperOverflowAction.Block;
            consoleWrapper.QueueLimit = 5000;

            // Initialize the AsyncFlushTargetWrapper for the ConsoleWrapper
            AutoFlushTargetWrapper consoleFlushWrapper = new AutoFlushTargetWrapper();
            consoleFlushWrapper.WrappedTarget = consoleWrapper;

            // This condition controls when the log is flushed. Set the LogLevel to be equivalent to the maximum level specified in the targetRule
            consoleFlushWrapper.Condition = "level >= LogLevel.Trace";

            // Adding the target to the config
            config.AddTarget("console", consoleFlushWrapper);


            // Initialize File Target
            var fileTarget = new FileTarget("File Target") {
                FileName = "Logs\\AutomationTool.log",
                KeepFileOpen = false,
                Layout = @"[${date}] ${message} ${exception: format=ToString}"
            };

            // Initialize the AsyncWrapper for the fileTarget
            AsyncTargetWrapper fileWrapper = new AsyncTargetWrapper();
            fileWrapper.WrappedTarget = fileTarget;
            fileWrapper.QueueLimit = 5000;
            fileWrapper.OverflowAction = AsyncTargetWrapperOverflowAction.Block;

            // Initialize the AsyncFlushTargetWrapper for the FileWrapper
            AutoFlushTargetWrapper fileFlushWrapper = new AutoFlushTargetWrapper();
            fileFlushWrapper.WrappedTarget = fileWrapper;

            // This condition controls when the log is flushed. Set the LogLevel to be equivalent to the maximum level specified in the targetRule
            fileFlushWrapper.Condition = "level >= LogLevel.Trace";

            // Adding the target to the config
            config.AddTarget("file", fileFlushWrapper);

            // Creating the Log Level rules for each target and adding them to the config
            // Edit these to change what methods are logged
            var fileRule = new LoggingRule("*", fileTarget);
            fileRule.EnableLoggingForLevels(LogLevel.Trace, LogLevel.Info);
            fileRule.EnableLoggingForLevel(LogLevel.Error);
            config.LoggingRules.Add(fileRule);

            var consoleRule = new LoggingRule("*", consoleTarget);
            consoleRule.EnableLoggingForLevels(LogLevel.Trace, LogLevel.Info);
            consoleRule.EnableLoggingForLevel(LogLevel.Error);
            config.LoggingRules.Add(consoleRule);

            // Assigning the configuration to the logger
            LogManager.Configuration = config;

        }
        private void log(string logMessage) {
            string old = logBox.Text;
            logBox.Text = "";
            logBox.Text += logMessage + "\n" + old;

            logger.Info(logMessage);
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

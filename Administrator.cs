using System;
using System.IO;
using System.ServiceModel.Configuration;
using System.Text;

namespace AutomationTool {
    class Administrator {
        string projectFolder;
        string fullMsiPath;
        Logger logger;

        public string ProjectFolder { get => projectFolder; set => projectFolder = value; }
        public string FullMsiPath { get => fullMsiPath; set => fullMsiPath = value; }
        public Logger Logger { get => logger; set => logger = value; }

        public Administrator() {}
        public void CheckIfFileExistsAndRenameOldFile(string filePath) {
            if (File.Exists(filePath)) {
                bool isRenamed = false;
                string oldFilePath = String.Format("{0}_old1", filePath);
                while (!isRenamed) {
                    if (File.Exists(oldFilePath)) {
                        int num = Convert.ToInt32(oldFilePath.Remove(0, oldFilePath.Length - 1));
                        num += 1;
                        oldFilePath = oldFilePath.Remove(oldFilePath.Length - 1) + num.ToString();
                    } else {
                        Logger.Log("SYS:     File " + filePath + " already exists! Renaming to " + oldFilePath + "");
                        File.Move(filePath, oldFilePath);
                        isRenamed = true;
                    }
                }
            }
        }

        public void CreateInstallUninstallVbs(ProjectInfo proj, bool isCustomMsi, string installvbs, string uninstallvbs) {
            this.CheckIfFileExistsAndRenameOldFile(Path.Combine(this.ProjectFolder, "Work", installvbs));
            this.CheckIfFileExistsAndRenameOldFile(Path.Combine(this.ProjectFolder, "Work", uninstallvbs));
            try {
                //Copying VBS files and replacing the proper lines
                //Encoding utf8WithoutBom = new UTF8Encoding(false);
                
                var vbsFileLines = File.ReadAllLines(@"templates\template_install.vbs");
                Logger.Log(String.Format("SYS:     Creating {0}...", installvbs));
                string lineToWrite = "";
                using (FileStream fs = File.OpenWrite(Path.Combine(this.ProjectFolder, "Work", installvbs))) {
                    using (StreamWriter sw = new StreamWriter(fs, Encoding.Default)) {
                        for (int i = 0; i < vbsFileLines.Length; i++) {
                            if (vbsFileLines[i].Contains("%%PACKAGENAME%%")) {
                                lineToWrite = vbsFileLines[i].Replace("%%PACKAGENAME%%", String.Format("{0}_{1}", proj.PkgName, proj.PkgVer));
                            } else if (vbsFileLines[i].Contains("Command(0) =")) {
                                if (!proj.isCustomMsi) {
                                    lineToWrite = String.Format("Command(0) = \"msiexec.exe /i \"\"{0}\"\" TRANSFORMS=\"\"{1}\"\" /qn /l*v %temp%\\{2}\"", Path.GetFileName(this.fullMsiPath), String.Format("{0}_{1}.mst", proj.PkgName, proj.PkgVer), String.Format("{0}_{1}.install.log", proj.PkgName, proj.PkgVer));
                                    Logger.Log(String.Format("VBS:     Inserting '{0}'", lineToWrite));
                                } else {
                                    lineToWrite = String.Format("Command(0) = \"msiexec.exe /i \"\"{0}\"\" /qn /l*v %temp%\\{1}\"", String.Format("{0}_{1}.msi", proj.PkgName, proj.PkgVer), String.Format("{0}_{1}.install.log", proj.PkgName, proj.PkgVer));
                                    Logger.Log(String.Format("VBS:     Inserting '{0}'", lineToWrite));
                                }
                            } else {
                                lineToWrite = vbsFileLines[i];
                            }
                            sw.WriteLine(lineToWrite);
                        }
                    }
                    ReportProgress();
                }
                using (FileStream fs = File.OpenWrite(Path.Combine(this.ProjectFolder, "Work", uninstallvbs))) {
                    using (StreamWriter sw = new StreamWriter(fs, Encoding.Default)) {
                        vbsFileLines = File.ReadAllLines(@"templates\template_uninstall.vbs");
                        Logger.Log(String.Format("SYS:     Creating {0}...", uninstallvbs));

                        for (int i = 0; i < vbsFileLines.Length; i++) {
                            if (vbsFileLines[i].Contains("%%PACKAGENAME%%")) {
                                lineToWrite = vbsFileLines[i].Replace("%%PACKAGENAME%%", String.Format("{0}_{1}", proj.PkgName, proj.PkgVer));
                            } else if (vbsFileLines[i].Contains("Command(0) = ")) {
                                lineToWrite = String.Format("Command(0) = \"msiexec.exe /x \"\"{0}\"\" /qn /l*v %temp%\\{1}\"", Path.GetFileName(proj.ProductCode), String.Format("{0}_{1}.uninstall.log", proj.PkgName, proj.PkgVer));
                                Logger.Log(String.Format("VBS:     Inserting '{0}'", lineToWrite));
                            } else {
                                lineToWrite = vbsFileLines[i];
                            }
                            sw.WriteLine(lineToWrite);
                        }
                    }
                    ReportProgress();
                }
                string upgradeVbsPath = Path.Combine(this.ProjectFolder, "Work", "Upgrade.vbs");
                if (!File.Exists(upgradeVbsPath)) {
                    File.Copy(@"templates\Upgrade.vbs", upgradeVbsPath);
                }
                ReportProgress();
            } catch {
                throw;
            }
        }

        public void createFolders(string pkgName) {

            this.ProjectFolder = Path.Combine(Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)), "Temp", pkgName);
            if (!Directory.Exists(this.ProjectFolder)) {
                Logger.Log(String.Format("SYS:     Creating directory {0}...", this.projectFolder));
                Directory.CreateDirectory(this.ProjectFolder);

                string[] folders = { "Doku", "Final", "Source", "Work" };

                foreach (string folder in folders) {
                    Logger.Log(String.Format("SYS:     Creating directory {0}...", Path.Combine(this.ProjectFolder, folder)));
                    Directory.CreateDirectory(Path.Combine(this.ProjectFolder, folder));
                }
            }
        }
        public int MaxProgressValue { get => 3; }
        // Declare the delegate (if using non-generic pattern).
        public delegate void TickProgress(object sender, EventArgs e);
        // Declare the event.
        public event TickProgress ProgressChanged;
  
        private void ReportProgress() {
            ProgressChanged.Invoke(this, new EventArgs());
        }
    }


}

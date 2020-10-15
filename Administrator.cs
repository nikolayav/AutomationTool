using System;
using System.IO;

namespace AutomationTool {
    class Administrator {
        string projectFolder;
        string fullMsiPath;
        Logger logger;

        public Administrator() {
           
        }
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
                        Logger.Log("File " + filePath + " already exists! Renaming to " + oldFilePath + "");
                        File.Move(filePath, oldFilePath);
                        isRenamed = true;
                    }
                }
            }
        }
        public string ProjectFolder { get => projectFolder; set => projectFolder = value; }
        public string FullMsiPath { get => fullMsiPath; set => fullMsiPath = value; }
        public Logger Logger { get => logger; set => logger = value; }

        public void CreateInstallUninstallVbs(Project proj, bool isCustomMsi, string installvbs, string uninstallvbs) {
            this.CheckIfFileExistsAndRenameOldFile(Path.Combine(this.projectFolder, "Work", installvbs));
            this.CheckIfFileExistsAndRenameOldFile(Path.Combine(this.projectFolder, "Work", uninstallvbs));

            var vbsFileLines = File.ReadAllLines(@"templates\template_install.vbs");
            Logger.Log("System:     Creating " + installvbs);
            string lineToWrite = "";
            using (FileStream fs = File.OpenWrite(Path.Combine(this.projectFolder, "Work", installvbs))) {
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
                            if (!isCustomMsi) {
                                lineToWrite = String.Format("Command(0) = \"msiexec.exe /i \"\"{0}\"\" TRANSFORMS=\"\"{1}\"\" /qn /l*v %temp%\\{2}\"", Path.GetFileName(this.fullMsiPath), String.Format("{0}_{1}.mst", proj.PkgName, proj.PkgVer), String.Format("{0}_{1}.install.log", proj.PkgName, proj.PkgVer));
                            } else {
                                lineToWrite = String.Format("Command(0) = \"msiexec.exe /i \"\"{0}\"\" /qn /l*v %temp%\\{1}\"", String.Format("{0}_{1}.msi", proj.PkgName, proj.PkgVer), String.Format("{0}_{1}.install.log", proj.PkgName, proj.PkgVer));
                            }

                        } else {
                            lineToWrite = vbsFileLines[i];
                        }
                        sw.WriteLine(lineToWrite);
                    }
                }
            }

            vbsFileLines = File.ReadAllLines(@"templates\template_uninstall.vbs");
            Logger.Log("System:     Creating " + uninstallvbs);
            using (FileStream fs = File.OpenWrite(Path.Combine(this.projectFolder, "Work", uninstallvbs))) {
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
        public void createFolders(string pkgName) {

            this.projectFolder = Path.Combine(Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)), "Temp", pkgName);
            if (!Directory.Exists(this.projectFolder)) {
                Logger.Log("System:     Creating directory " + this.projectFolder + "...");
                Directory.CreateDirectory(this.projectFolder);

                string[] folders = { "Doku", "Final", "Source", "Work" };

                foreach (string folder in folders) {
                    Logger.Log("System:     Creating directory " + Path.Combine(this.projectFolder, folder + "..."));
                    Directory.CreateDirectory(Path.Combine(this.projectFolder, folder));
                }
            }
        }
    }


}

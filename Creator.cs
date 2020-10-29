﻿using Microsoft.Deployment.WindowsInstaller;
using System;
using System.Collections.Generic;
using System.IO;

namespace AutomationTool {
    class Creator {
        MsiEditor msiEditor;
        Administrator adm;
        Logger logger;
        public Creator( ) {}

        public MsiEditor MsiEditor { get => msiEditor; set => msiEditor = value; }
        public Administrator Adm { get => adm; set => adm = value; }
        public Logger Logger { get => logger; set => logger = value; }

        public void CreateXLSX(ProjectInfo proj) {
            string newXLFile = Path.Combine(Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)), "Temp", adm.ProjectFolder, "Doku", String.Format("{0}_{1}_1.xlsx", proj.PkgName, proj.PkgVer));

            if (File.Exists(@"templates\template_excel.xlsx")) {
                if (File.Exists(newXLFile)) {
                    bool isRenamed = false;
                    string oldFileName = String.Format("{0}_old1", newXLFile);
                    while (!isRenamed) {
                        if (File.Exists(oldFileName)) {
                            int num = Convert.ToInt32(oldFileName.Remove(0, oldFileName.Length - 1));
                            num += 1;
                            oldFileName = oldFileName.Remove(oldFileName.Length - 1) + num.ToString();
                        } else {
                            File.Move(newXLFile, oldFileName);
                            isRenamed = true;
                        }
                    }
                }
                Logger.Log(String.Format("System:     Copying {0}...", Path.GetFileName(newXLFile)));

                File.Copy(@"templates\template_excel.xlsx", newXLFile);
            }
            ReportProgress();
            Logger.Log(String.Format("Excel:     Updating cells in {0}...", Path.GetFileName(newXLFile)));

            OpenXMLEditor openXmlEditor = new OpenXMLEditor();
            openXmlEditor.UpdateExcelSheetData(newXLFile, "Universal", 5, "C", proj.PimsId);
            openXmlEditor.UpdateExcelSheetData(newXLFile, "Universal", 6, "C", proj.PkgName);
            openXmlEditor.UpdateExcelSheetData(newXLFile, "Universal", 7, "C", proj.PkgVer);

            openXmlEditor.UpdateExcelSheetData(newXLFile, "Universal", 9, "C", proj.AppName);
            openXmlEditor.UpdateExcelSheetData(newXLFile, "Universal", 10, "C", proj.AppVer);

            openXmlEditor.UpdateExcelSheetData(newXLFile, "Universal", 33, "C", proj.AppName);
            openXmlEditor.UpdateExcelSheetData(newXLFile, "Universal", 34, "C", proj.AppName);
            openXmlEditor.UpdateExcelSheetData(newXLFile, "Universal", 35, "C", proj.AuthorName);
            openXmlEditor.UpdateExcelSheetData(newXLFile, "Universal", 36, "C", proj.Comments);

            openXmlEditor.UpdateExcelSheetData(newXLFile, "Universal", 76, "C", proj.ProductCode);
            openXmlEditor.UpdateExcelSheetData(newXLFile, "Universal", 77, "C", proj.ProductCode);
            openXmlEditor.UpdateExcelSheetData(newXLFile, "Universal", 78, "C", proj.UpgradeCode);
            openXmlEditor.UpdateExcelSheetData(newXLFile, "Universal", 79, "C", proj.UpgradeCode);

            ReportProgress();
        }

        public void CreateMst(ProjectInfo proj) {

            Logger.Log("File:     Extracting .reg info...");

            string auditKeyPath = "";
            var regFileLines = File.ReadAllLines(@"templates\template_auditkey.reg");
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
            ReportProgress();
            string referenceDb = Adm.FullMsiPath;
            string tempDb = Path.Combine(Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)), "Temp", this.Adm.ProjectFolder, "Work", String.Format("{0}.msi_tmp1", proj.MsiName));
            string transform = Path.Combine(Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)), "Temp", this.Adm.ProjectFolder, "Work", String.Format("{0}_{1}.mst", proj.PkgName, proj.PkgVer));
            const string auditComponentx86 = "BMW_Client_Info";
            const string auditComponentx64 = "BMW_Client_Infox64";

            Adm.CheckIfFileExistsAndRenameOldFile(transform);
            Adm.CheckIfFileExistsAndRenameOldFile(tempDb);

            Logger.Log(String.Format("System:     Creating {0} and {1} files...", Path.GetFileName(transform), Path.GetFileName(tempDb)));

            File.Copy(referenceDb, tempDb, true);

            try {
                using (var origDatabase = new Database(referenceDb, DatabaseOpenMode.ReadOnly)) {
                    using (var db = new Database(tempDb, DatabaseOpenMode.Direct)) {

                        MsiEditor.Feature_AddOrUpdate(db, proj.FeatureName, proj.FeatureName, "0", "1", "INSTALLDIR", "48");
                        ReportProgress();
                        MsiEditor.Component_AddOrUpdate(db, auditComponentx86, MsiEditor.GenerateUniqueGuid(db, "component"), "TARGETDIR", "4", proj.FeatureName);
                        MsiEditor.Component_AddOrUpdate(db, auditComponentx64, MsiEditor.GenerateUniqueGuid(db, "component"), "TARGETDIR", "260", proj.FeatureName);
                        ReportProgress();

                        foreach (KeyValuePair<string, string> kvp in regDict) {
                            if (kvp.Key.Equals("MSIPackageName")) {
                                MsiEditor.Reg_Add(db, MsiEditor.GenerateUniqueGuid(db, "registry"), "2", auditKeyPath, kvp.Key, Path.GetFileName(this.adm.FullMsiPath), auditComponentx64);
                                MsiEditor.Reg_Add(db, MsiEditor.GenerateUniqueGuid(db, "registry"), "2", auditKeyPath, kvp.Key, Path.GetFileName(this.adm.FullMsiPath), auditComponentx86);
                            } else {
                                MsiEditor.Reg_Add(db, MsiEditor.GenerateUniqueGuid(db, "registry"), "2", auditKeyPath, kvp.Key, kvp.Value, auditComponentx64);
                                MsiEditor.Reg_Add(db, MsiEditor.GenerateUniqueGuid(db, "registry"), "2", auditKeyPath, kvp.Key, kvp.Value, auditComponentx86);
                            }
                        }
                        ReportProgress();
                        MsiEditor.Property_AddOrUpdate("ALLUSERS", "1", db);
                        MsiEditor.Property_AddOrUpdate("ARPNOMODIFY", "1", db);
                        MsiEditor.Property_AddOrUpdate("ARPNOREMOVE", "1", db);
                        MsiEditor.Property_AddOrUpdate("ARPNOREPAIR", "1", db);
                        MsiEditor.Property_AddOrUpdate("BMW_Package_Author", proj.AuthorName, db);
                        MsiEditor.Property_AddOrUpdate("BMW_PackageName", proj.PkgName, db);
                        MsiEditor.Property_AddOrUpdate("BMW_PackageVersion", proj.PkgVer, db);
                        //MsiEditor.Property_AddOrUpdate("Manufacturer", "BMW Package Factory", db);
                        MsiEditor.Property_AddOrUpdate("MSIRESTARTMANAGERCONTROL", "Disable", db);
                        //MsiEditor.Property_AddOrUpdate("ProductName", proj.AppName, db);
                        MsiEditor.Property_AddOrUpdate("ProductVersion", proj.AppVer, db);
                        MsiEditor.Property_AddOrUpdate("PROMPTROLLBACKCOST", "D", db);
                        MsiEditor.Property_AddOrUpdate("REBOOT", "ReallySuppress", db);
                        MsiEditor.Property_AddOrUpdate("REBOOTPROMPT", "S", db);
                        ReportProgress();
                        proj.ProductCode = MsiEditor.GetProductAndUpgradeCodes(db, "ProductCode");
                        proj.UpgradeCode = MsiEditor.GetProductAndUpgradeCodes(db, "UpgradeCode");

                        Logger.Log("Transform:     Editing summary information stream...");

                        // Edit summary info stream
                        db.SummaryInfo.Title = proj.AppName;
                        db.SummaryInfo.Subject = proj.AppName;
                        db.SummaryInfo.Comments = proj.Comments;
                        db.SummaryInfo.Author = proj.AuthorName;
                        ReportProgress();
                        Logger.Log(String.Format("Transform:     Saving {0} file...", Path.GetFileName(transform)));

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
            ReportProgress();
        }

        public void GenerateCustomMsi(ProjectInfo proj, bool is32bit) {
            Logger.Log("File:     Extracting .reg info...");

            string auditKeyPath = "";
            var regFileLines = File.ReadAllLines(@"templates\template_auditkey.reg");
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
            ReportProgress();
            string referenceDb = @"templates\template_isproject.ism";
            string ism = Path.Combine(Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)), "Temp", adm.ProjectFolder, "Work", String.Format("{0}.ism", String.Format("{0}_{1}", proj.PkgName, proj.PkgVer)));

            const string auditComponentx86 = "BMW_Client_Info";
            const string auditComponentx64 = "BMW_Client_Infox64";

            adm.CheckIfFileExistsAndRenameOldFile(ism);

            Logger.Log(String.Format("System:     Creating {0} file...", Path.GetFileName(ism)));

            File.Copy(referenceDb, ism, true);
            ReportProgress();
            try {
                using (var origDatabase = new Database(referenceDb, DatabaseOpenMode.ReadOnly)) {
                    using (var db = new Database(ism, DatabaseOpenMode.Direct)) {
                        proj.ProductCode = msiEditor.GenerateUniqueGuid(db, "ism");
                        proj.UpgradeCode = msiEditor.GenerateUniqueGuid(db, "ism");

                        msiEditor.Feature_AddOrUpdate(db, proj.FeatureName, proj.FeatureName, "0", "1", "INSTALLDIR", "48");
                        ReportProgress();
                        msiEditor.Component_AddOrUpdate(db, auditComponentx86, msiEditor.GenerateUniqueGuid(db, "component"), "TARGETDIR", "4", proj.FeatureName);
                        msiEditor.Component_AddOrUpdate(db, auditComponentx64, msiEditor.GenerateUniqueGuid(db, "component"), "TARGETDIR", "260", proj.FeatureName);
                        ReportProgress();

                        foreach (KeyValuePair<string, string> kvp in regDict) {
                            if (kvp.Key.Equals("MSIPackageName")) {
                                msiEditor.Reg_Add(db, msiEditor.GenerateUniqueGuid(db, "registry"), "2", auditKeyPath, kvp.Key, String.Format("{0}_{1}.msi", proj.PkgName, proj.PkgVer), auditComponentx64);
                                msiEditor.Reg_Add(db, msiEditor.GenerateUniqueGuid(db, "registry"), "2", auditKeyPath, kvp.Key, String.Format("{0}_{1}.msi", proj.PkgName, proj.PkgVer), auditComponentx86);
                            } else {
                                msiEditor.Reg_Add(db, msiEditor.GenerateUniqueGuid(db, "registry"), "2", auditKeyPath, kvp.Key, kvp.Value, auditComponentx64);
                                msiEditor.Reg_Add(db, msiEditor.GenerateUniqueGuid(db, "registry"), "2", auditKeyPath, kvp.Key, kvp.Value, auditComponentx86);
                            }
                        }
                        ReportProgress();
                        msiEditor.Property_AddOrUpdate("ProductCode", proj.ProductCode, db);
                        msiEditor.Property_AddOrUpdate("UpgradeCode", proj.UpgradeCode, db);
                        msiEditor.Property_AddOrUpdate("ALLUSERS", "1", db);
                        msiEditor.Property_AddOrUpdate("ARPNOMODIFY", "1", db);
                        msiEditor.Property_AddOrUpdate("ARPNOREMOVE", "1", db);
                        msiEditor.Property_AddOrUpdate("ARPNOREPAIR", "1", db);
                        msiEditor.Property_AddOrUpdate("BMW_Package_Author", proj.AuthorName, db);
                        msiEditor.Property_AddOrUpdate("BMW_PackageName", proj.PkgName, db);
                        msiEditor.Property_AddOrUpdate("BMW_PackageVersion", proj.PkgVer, db);
                        msiEditor.Property_AddOrUpdate("Manufacturer", "BMW Package Factory", db);
                        msiEditor.Property_AddOrUpdate("MSIRESTARTMANAGERCONTROL", "Disable", db);
                        msiEditor.Property_AddOrUpdate("ProductName", proj.AppName, db);
                        msiEditor.Property_AddOrUpdate("ProductVersion", proj.AppVer, db);
                        msiEditor.Property_AddOrUpdate("PROMPTROLLBACKCOST", "D", db);
                        msiEditor.Property_AddOrUpdate("REBOOT", "ReallySuppress", db);
                        msiEditor.Property_AddOrUpdate("REBOOTPROMPT", "S", db);

                        Logger.Log("ISM:     Editing summary information stream...");

                        // Edit summary info stream
                        if (is32bit) {
                            db.SummaryInfo.Template = "Intel;1033";
                        } else {
                            db.SummaryInfo.Template = "x64;1033";
                        }

                        db.SummaryInfo.Title = proj.AppName;
                        db.SummaryInfo.Subject = proj.AppName;
                        db.SummaryInfo.Comments = proj.Comments;
                        db.SummaryInfo.Author = proj.AuthorName;
                        ReportProgress();
                        Logger.Log(String.Format("ISM:     Saving {0} file...", Path.GetFileName(ism)));

                        db.Commit();
                    }
                }
            } catch {
                throw;
            } finally {
                //File.Delete(tempDb);
            }
            ReportProgress();
        }

        public void EditMst(ProjectInfo proj) {

            Logger.Log("File:     Extracting .reg info...");

            string auditKeyPath = "";
            var regFileLines = File.ReadAllLines(@"templates\template_auditkey.reg");
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

            ReportProgress();
            string referenceDb = Adm.FullMsiPath;
            string tempDb = Path.Combine(Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)), "Temp", this.Adm.ProjectFolder, "Work", String.Format("{0}.msi_tmp1", proj.MsiName));
            string transform = Path.Combine(Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)), "Temp", this.Adm.ProjectFolder, "Work", String.Format("{0}_{1}.mst", proj.PkgName, proj.PkgVer));
            const string auditComponentx86 = "BMW_Client_Info";
            const string auditComponentx64 = "BMW_Client_Infox64";

            Adm.CheckIfFileExistsAndRenameOldFile(transform);
            Adm.CheckIfFileExistsAndRenameOldFile(tempDb);

            Logger.Log(String.Format("System:     Creating {0} and {1} files...", Path.GetFileName(transform), Path.GetFileName(tempDb)));

            File.Copy(referenceDb, tempDb, true);

            try {
                using (var origDatabase = new Database(referenceDb, DatabaseOpenMode.ReadOnly)) {
                    using (var db = new Database(tempDb, DatabaseOpenMode.Direct)) {
                        db.ApplyTransform(proj.EditMstPath);

                        MsiEditor.UpdateFeatureAssociations(db, proj, proj.FeatureName, proj.FeatureName, "0", "1", "INSTALLDIR", "48");
                        string oldFeature = msiEditor.FindExistingFeature(db, proj);

                        ReportProgress();
                        MsiEditor.Component_AddOrUpdate(db, auditComponentx86, MsiEditor.GenerateUniqueGuid(db, "component"), "TARGETDIR", "4", proj.FeatureName);
                        MsiEditor.Component_AddOrUpdate(db, auditComponentx64, MsiEditor.GenerateUniqueGuid(db, "component"), "TARGETDIR", "260", proj.FeatureName);

                        ReportProgress();
                        
                        msiEditor.dropRegs(db, auditKeyPath);
                        
                       foreach (KeyValuePair<string, string> kvp in regDict) {
                            if (kvp.Key.Equals("MSIPackageName")) {
                                MsiEditor.Reg_Add(db, MsiEditor.GenerateUniqueGuid(db, "registry"), "2", auditKeyPath, kvp.Key, Path.GetFileName(this.adm.FullMsiPath), auditComponentx64);
                                MsiEditor.Reg_Add(db, MsiEditor.GenerateUniqueGuid(db, "registry"), "2", auditKeyPath, kvp.Key, Path.GetFileName(this.adm.FullMsiPath), auditComponentx86);
                            } else {
                                MsiEditor.Reg_Add(db, MsiEditor.GenerateUniqueGuid(db, "registry"), "2", auditKeyPath, kvp.Key, kvp.Value, auditComponentx64);
                                MsiEditor.Reg_Add(db, MsiEditor.GenerateUniqueGuid(db, "registry"), "2", auditKeyPath, kvp.Key, kvp.Value, auditComponentx86);
                            }
                        }

                        ReportProgress();
                        MsiEditor.Property_AddOrUpdate("ALLUSERS", "1", db);
                        MsiEditor.Property_AddOrUpdate("ARPNOMODIFY", "1", db);
                        MsiEditor.Property_AddOrUpdate("ARPNOREMOVE", "1", db);
                        MsiEditor.Property_AddOrUpdate("ARPNOREPAIR", "1", db);
                        MsiEditor.Property_AddOrUpdate("BMW_Package_Author", proj.AuthorName, db);
                        MsiEditor.Property_AddOrUpdate("BMW_PackageName", proj.PkgName, db);
                        MsiEditor.Property_AddOrUpdate("BMW_PackageVersion", proj.PkgVer, db);
                        //MsiEditor.Property_AddOrUpdate("Manufacturer", "BMW Package Factory", db);
                        MsiEditor.Property_AddOrUpdate("MSIRESTARTMANAGERCONTROL", "Disable", db);
                        //MsiEditor.Property_AddOrUpdate("ProductName", proj.AppName, db);
                        MsiEditor.Property_AddOrUpdate("ProductVersion", proj.AppVer, db);
                        MsiEditor.Property_AddOrUpdate("PROMPTROLLBACKCOST", "D", db);
                        MsiEditor.Property_AddOrUpdate("REBOOT", "ReallySuppress", db);
                        MsiEditor.Property_AddOrUpdate("REBOOTPROMPT", "S", db);
                        ReportProgress();
                        proj.ProductCode = MsiEditor.GetProductAndUpgradeCodes(db, "ProductCode");
                        proj.UpgradeCode = MsiEditor.GetProductAndUpgradeCodes(db, "UpgradeCode");

                        Logger.Log("Transform:     Editing summary information stream...");

                        // Edit summary info stream
                        db.SummaryInfo.Title = proj.AppName;
                        db.SummaryInfo.Subject = proj.AppName;
                        db.SummaryInfo.Comments = proj.Comments;
                        db.SummaryInfo.Author = proj.AuthorName;
                        ReportProgress();
                        Logger.Log(String.Format("Transform:     Saving {0} file...", Path.GetFileName(transform)));

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
            ReportProgress();
        }

        public int MaxProgressValue { get => 9; }
        // Declare the delegate (if using non-generic pattern).
        public delegate void TickProgress(object sender, EventArgs e);
        // Declare the event.
        public event TickProgress ProgressChanged;

        private void ReportProgress() {
            ProgressChanged.Invoke(this, new EventArgs());
        }
    }
}

using Microsoft.Deployment.WindowsInstaller;
using System;
using System.Collections.Generic;
using System.IO;

namespace AutomationTool {
    class Creator {
        MsiEditor msiEditor;
        Administrator adm;
        Logger logger;
        public Creator( ) {}

        string _auditKeyPath = "";
        const string _auditComponentx86 = "BMW_Client_Info";
        const string _auditComponentx64 = "BMW_Client_Infox64";

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
                Logger.Log(String.Format("SYS:     Copying {0}...", Path.GetFileName(newXLFile)));

                File.Copy(@"templates\template_excel.xlsx", newXLFile);
            }
            ReportProgress();
            Logger.Log(String.Format("EXL:     Updating cells in {0}...", Path.GetFileName(newXLFile)));

            OpenXMLEditor openXmlEditor = new OpenXMLEditor();
            openXmlEditor.UpdateExcelSheetData(newXLFile, "Universal", proj.PimsId, "%%PIMS_ID%%");
            openXmlEditor.UpdateExcelSheetData(newXLFile, "Universal", proj.PkgName, "%%PKG_NAME%%");
            openXmlEditor.UpdateExcelSheetData(newXLFile, "Universal", proj.PkgVer, "%%PKG_VER%%");

            openXmlEditor.UpdateExcelSheetData(newXLFile, "Universal", proj.AppName, "%%APP_NAME%%");
            openXmlEditor.UpdateExcelSheetData(newXLFile, "Universal", proj.AppVer, "%%APP_VER%%");

            openXmlEditor.UpdateExcelSheetData(newXLFile, "Universal", proj.AuthorName, "%%AUTHOR%%");
            openXmlEditor.UpdateExcelSheetData(newXLFile, "Universal", proj.Comments, "%%COMMENTS%%");

            openXmlEditor.UpdateExcelSheetData(newXLFile, "Universal", proj.ProductCode, "%%PRODUCT_CODE%%");
            openXmlEditor.UpdateExcelSheetData(newXLFile, "Universal", proj.UpgradeCode, "%%UPGRADE_CODE%%");

            ReportProgress();
        }

        public void CreateMst(ProjectInfo proj) {

            Dictionary<string, string> regDict = ExtractRegKeysFromFile(ref _auditKeyPath);

            ReportProgress();
            string referenceDb = Adm.FullMsiPath;
            string tempDb = Path.Combine(Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)), "Temp", this.Adm.ProjectFolder, "Work", String.Format("{0}.msi_tmp1", proj.MsiName));
            string transform = Path.Combine(Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)), "Temp", this.Adm.ProjectFolder, "Work", String.Format("{0}_{1}.mst", proj.PkgName, proj.PkgVer));


            Adm.CheckIfFileExistsAndRenameOldFile(transform);
            Adm.CheckIfFileExistsAndRenameOldFile(tempDb);

            Logger.Log(String.Format("SYS:     Creating {0} and {1} files...", Path.GetFileName(transform), Path.GetFileName(tempDb)));

            File.Copy(referenceDb, tempDb, true);

            try {
                using (var origDatabase = new Database(referenceDb, DatabaseOpenMode.ReadOnly)) {
                    using (var db = new Database(tempDb, DatabaseOpenMode.Direct)) {

                        MsiEditor.Feature_AddOrUpdate(db, proj.FeatureName, proj.FeatureName, "0", "1", "TARGETDIR", "48");
                        ReportProgress();
                        MsiEditor.Component_AddOrUpdate(db, _auditComponentx86, MsiEditor.GenerateUniqueGuid(db, "component"), "TARGETDIR", "4", proj.FeatureName);
                        MsiEditor.Component_AddOrUpdate(db, _auditComponentx64, MsiEditor.GenerateUniqueGuid(db, "component"), "TARGETDIR", "260", proj.FeatureName);
                        ReportProgress();

                        foreach (KeyValuePair<string, string> kvp in regDict) {
                            if (kvp.Key.Equals("MSIPackageName")) {
                                MsiEditor.Reg_Add(db, MsiEditor.GenerateUniqueGuid(db, "registry"), "2", _auditKeyPath, kvp.Key, Path.GetFileName(this.adm.FullMsiPath), _auditComponentx64);
                                MsiEditor.Reg_Add(db, MsiEditor.GenerateUniqueGuid(db, "registry"), "2", _auditKeyPath, kvp.Key, Path.GetFileName(this.adm.FullMsiPath), _auditComponentx86);
                            } else {
                                MsiEditor.Reg_Add(db, MsiEditor.GenerateUniqueGuid(db, "registry"), "2", _auditKeyPath, kvp.Key, kvp.Value, _auditComponentx64);
                                MsiEditor.Reg_Add(db, MsiEditor.GenerateUniqueGuid(db, "registry"), "2", _auditKeyPath, kvp.Key, kvp.Value, _auditComponentx86);
                            }
                        }
                        ReportProgress();
                        MsiEditor.ProcessProperties(db, proj);
                        ReportProgress();
                        proj.ProductCode = MsiEditor.GetProductAndUpgradeCodes(db, "ProductCode");
                        proj.UpgradeCode = MsiEditor.GetProductAndUpgradeCodes(db, "UpgradeCode");

                        Logger.Log("MSI:     Editing summary information stream...");

                        string pName = msiEditor.GetProductName(db);
                        // Edit summary info stream
                        db.SummaryInfo.Title = pName;
                        db.SummaryInfo.Subject = pName;
                        db.SummaryInfo.Comments = proj.Comments;
                        db.SummaryInfo.Author = proj.AuthorName;
                        ReportProgress();
                        Logger.Log(String.Format("MSI:     Saving {0} file...", Path.GetFileName(transform)));

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
            Dictionary<string, string> regDict = ExtractRegKeysFromFile(ref _auditKeyPath);

            ReportProgress();
            string referenceDb = @"templates\template_isproject.ism";
            string ism = Path.Combine(Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)), "Temp", adm.ProjectFolder, "Work", String.Format("{0}.ism", String.Format("{0}_{1}", proj.PkgName, proj.PkgVer)));

            adm.CheckIfFileExistsAndRenameOldFile(ism);

            Logger.Log(String.Format("SYS:     Creating {0} file...", Path.GetFileName(ism)));

            File.Copy(referenceDb, ism, true);
            ReportProgress();
            try {
                using (var origDatabase = new Database(referenceDb, DatabaseOpenMode.ReadOnly)) {
                    using (var db = new Database(ism, DatabaseOpenMode.Direct)) {
                        proj.ProductCode = msiEditor.GenerateUniqueGuid(db, "ism");
                        proj.UpgradeCode = msiEditor.GenerateUniqueGuid(db, "ism");

                        msiEditor.Feature_AddOrUpdate(db, proj.FeatureName, proj.FeatureName, "0", "1", "TARGETDIR", "48");
                        ReportProgress();
                        msiEditor.Component_AddOrUpdate(db, _auditComponentx86, msiEditor.GenerateUniqueGuid(db, "component"), "TARGETDIR", "4", proj.FeatureName);
                        msiEditor.Component_AddOrUpdate(db, _auditComponentx64, msiEditor.GenerateUniqueGuid(db, "component"), "TARGETDIR", "260", proj.FeatureName);
                        ReportProgress();

                        foreach (KeyValuePair<string, string> kvp in regDict) {
                            if (kvp.Key.Equals("MSIPackageName")) {
                                msiEditor.Reg_Add(db, msiEditor.GenerateUniqueGuid(db, "registry"), "2", _auditKeyPath, kvp.Key, String.Format("{0}_{1}.msi", proj.PkgName, proj.PkgVer), _auditComponentx64);
                                msiEditor.Reg_Add(db, msiEditor.GenerateUniqueGuid(db, "registry"), "2", _auditKeyPath, kvp.Key, String.Format("{0}_{1}.msi", proj.PkgName, proj.PkgVer), _auditComponentx86);
                            } else {
                                msiEditor.Reg_Add(db, msiEditor.GenerateUniqueGuid(db, "registry"), "2", _auditKeyPath, kvp.Key, kvp.Value, _auditComponentx64);
                                msiEditor.Reg_Add(db, msiEditor.GenerateUniqueGuid(db, "registry"), "2", _auditKeyPath, kvp.Key, kvp.Value, _auditComponentx86);
                            }
                        }
                        ReportProgress();
                        MsiEditor.ProcessProperties(db, proj);

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
            Dictionary<string, string> regDict = ExtractRegKeysFromFile(ref _auditKeyPath);

            ReportProgress();
            string referenceDb = Adm.FullMsiPath;
            string tempDb = Path.Combine(Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)), "Temp", this.Adm.ProjectFolder, "Work", String.Format("{0}.msi_tmp1", proj.MsiName));
            string transform = Path.Combine(Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)), "Temp", this.Adm.ProjectFolder, "Work", String.Format("{0}_{1}.mst", proj.PkgName, proj.PkgVer));

            Adm.CheckIfFileExistsAndRenameOldFile(transform);
            Adm.CheckIfFileExistsAndRenameOldFile(tempDb);

            Logger.Log(String.Format("SYS:     Creating {0} and {1} files...", Path.GetFileName(transform), Path.GetFileName(tempDb)));

            File.Copy(referenceDb, tempDb, true);

            try {
                using (var origDatabase = new Database(referenceDb, DatabaseOpenMode.ReadOnly)) {
                    using (var db = new Database(tempDb, DatabaseOpenMode.Direct)) {
                        db.ApplyTransform(proj.EditMstPath);

                        MsiEditor.UpdateFeatureAssociations(db, proj, proj.FeatureName, proj.FeatureName, "0", "1", "TARGETDIR", "48");
                        string oldFeature = msiEditor.FindExistingFeature(db, proj);

                        ReportProgress();
                        MsiEditor.Component_AddOrUpdate(db, _auditComponentx86, MsiEditor.GenerateUniqueGuid(db, "component"), "TARGETDIR", "4", proj.FeatureName);
                        MsiEditor.Component_AddOrUpdate(db, _auditComponentx64, MsiEditor.GenerateUniqueGuid(db, "component"), "TARGETDIR", "260", proj.FeatureName);

                        ReportProgress();
                        
                        msiEditor.dropRegs(db, _auditKeyPath);
                        
                       foreach (KeyValuePair<string, string> kvp in regDict) {
                            if (kvp.Key.Equals("MSIPackageName")) {
                                MsiEditor.Reg_Add(db, MsiEditor.GenerateUniqueGuid(db, "registry"), "2", _auditKeyPath, kvp.Key, Path.GetFileName(this.adm.FullMsiPath), _auditComponentx64);
                                MsiEditor.Reg_Add(db, MsiEditor.GenerateUniqueGuid(db, "registry"), "2", _auditKeyPath, kvp.Key, Path.GetFileName(this.adm.FullMsiPath), _auditComponentx86);
                            } else {
                                MsiEditor.Reg_Add(db, MsiEditor.GenerateUniqueGuid(db, "registry"), "2", _auditKeyPath, kvp.Key, kvp.Value, _auditComponentx64);
                                MsiEditor.Reg_Add(db, MsiEditor.GenerateUniqueGuid(db, "registry"), "2", _auditKeyPath, kvp.Key, kvp.Value, _auditComponentx86);
                            }
                        }

                        ReportProgress();

                        MsiEditor.ProcessProperties(db, proj);
                        ReportProgress();
                        proj.ProductCode = MsiEditor.GetProductAndUpgradeCodes(db, "ProductCode");
                        proj.UpgradeCode = MsiEditor.GetProductAndUpgradeCodes(db, "UpgradeCode");

                        Logger.Log("MSI:     Editing summary information stream...");

                        string pName = msiEditor.GetProductName(db);
                        // Edit summary info stream
                        db.SummaryInfo.Title = pName;
                        db.SummaryInfo.Subject = pName;
                        db.SummaryInfo.Comments = proj.Comments;
                        db.SummaryInfo.Author = proj.AuthorName;
                        ReportProgress();
                        Logger.Log(String.Format("MSI:     Saving {0} file...", Path.GetFileName(transform)));

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

        private Dictionary<string, string> ExtractRegKeysFromFile(ref string _auditKeyPath) {
            Logger.Log("SYS:     Extracting .reg info from '...templates\\template_auditkey.reg'");

            try {
                var regFileLines = File.ReadAllLines(@"templates\template_auditkey.reg");
                Dictionary<string, string> regDict = new Dictionary<string, string>();

                bool startReading = false;
                foreach (string line in regFileLines) {
                    if (!startReading) {
                        if (line.StartsWith("[")) {
                            if (line.Contains("[HKEY_LOCAL_MACHINE\\")) {
                                _auditKeyPath = line;
                                _auditKeyPath = _auditKeyPath.Replace("[HKEY_LOCAL_MACHINE\\", "").Replace("]]", "]");
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
                return regDict;
            } catch {
                throw; ;
            }
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

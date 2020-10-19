using Microsoft.Deployment.WindowsInstaller;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace AutomationTool {
    class Creator {
        MsiEditor msiEditor;
        Administrator adm;
        Logger logger;
        public Creator() {

        }

        public MsiEditor MsiEditor { get => msiEditor; set => msiEditor = value; }
        public Administrator Adm { get => adm; set => adm = value; }
        public Logger Logger { get => logger; set => logger = value; }

        public void CreateXML(ProjectInfo proj) {
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

            Logger.Log(String.Format("Excel:     Updating cells in {0}...", Path.GetFileName(newXLFile)));


            Excel.Application excel = new Excel.Application();
            Workbook wb = excel.Workbooks.Open(newXLFile);

            try {
                Excel.Worksheet xlSheet = wb.Sheets["Universal"];
                xlSheet.Activate();
                xlSheet.Cells[5, 3].Value = proj.PimsId;
                xlSheet.Cells[6, 3].Value = proj.PkgName;
                xlSheet.Cells[7, 3].Value = proj.PkgVer;
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

                Logger.Log(String.Format("Excel:     Saving {0}...", Path.GetFileName(newXLFile)));

                wb.Save();

            } catch (Exception ex) {
                throw;
            } finally {
                wb.Close();
            }
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

                        MsiEditor.Component_AddOrUpdate(db, auditComponentx86, MsiEditor.GenerateUniqueGuid(db, "component"), "TARGETDIR", "4", proj.FeatureName);
                        MsiEditor.Component_AddOrUpdate(db, auditComponentx64, MsiEditor.GenerateUniqueGuid(db, "component"), "TARGETDIR", "260", proj.FeatureName);


                        foreach (KeyValuePair<string, string> kvp in regDict) {
                            if (kvp.Key.Equals("MSIPackageName")) {
                                MsiEditor.Reg_Add(db, MsiEditor.GenerateUniqueGuid(db, "registry"), "2", auditKeyPath, kvp.Key, Path.GetFileName(this.adm.FullMsiPath), auditComponentx64);
                                MsiEditor.Reg_Add(db, MsiEditor.GenerateUniqueGuid(db, "registry"), "2", auditKeyPath, kvp.Key, Path.GetFileName(this.adm.FullMsiPath), auditComponentx86);
                            } else {
                                MsiEditor.Reg_Add(db, MsiEditor.GenerateUniqueGuid(db, "registry"), "2", auditKeyPath, kvp.Key, kvp.Value, auditComponentx64);
                                MsiEditor.Reg_Add(db, MsiEditor.GenerateUniqueGuid(db, "registry"), "2", auditKeyPath, kvp.Key, kvp.Value, auditComponentx86);
                            }
                        }

                        MsiEditor.Property_AddOrUpdate("ALLUSERS", "1", db);
                        MsiEditor.Property_AddOrUpdate("ARPNOMODIFY", "1", db);
                        MsiEditor.Property_AddOrUpdate("ARPNOREMOVE", "1", db);
                        MsiEditor.Property_AddOrUpdate("ARPNOREPAIR", "1", db);
                        MsiEditor.Property_AddOrUpdate("BMW_Package_Author", proj.AuthorName, db);
                        MsiEditor.Property_AddOrUpdate("BMW_PackageName", proj.PkgName, db);
                        MsiEditor.Property_AddOrUpdate("BMW_PackageVersion", proj.PkgVer, db);
                        MsiEditor.Property_AddOrUpdate("Manufacturer", "BMW Package Factory", db);
                        MsiEditor.Property_AddOrUpdate("MSIRESTARTMANAGERCONTROL", "Disable", db);
                        MsiEditor.Property_AddOrUpdate("ProductName", proj.AppName, db);
                        MsiEditor.Property_AddOrUpdate("ProductVersion", proj.AppVer, db);
                        MsiEditor.Property_AddOrUpdate("PROMPTROLLBACKCOST", "D", db);
                        MsiEditor.Property_AddOrUpdate("REBOOT", "ReallySuppress", db);
                        MsiEditor.Property_AddOrUpdate("REBOOTPROMPT", "S", db);

                        proj.ProductCode = MsiEditor.GetProductAndUpgradeCodes(db, "ProductCode");
                        proj.UpgradeCode = MsiEditor.GetProductAndUpgradeCodes(db, "UpgradeCode");

                        Logger.Log("Transform:     Editing summary information stream...");

                        // Edit summary info stream
                        db.SummaryInfo.Title = proj.AppName;
                        db.SummaryInfo.Subject = proj.AppName;
                        db.SummaryInfo.Comments = proj.Comments;
                        db.SummaryInfo.Author = proj.AuthorName;

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

            string referenceDb = @"templates\template_isproject.ism";
            string ism = Path.Combine(Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)), "Temp", adm.ProjectFolder, "Work", String.Format("{0}.ism", String.Format("{0}_{1}", proj.PkgName, proj.PkgVer)));

            const string auditComponentx86 = "BMW_Client_Info";
            const string auditComponentx64 = "BMW_Client_Infox64";

            adm.CheckIfFileExistsAndRenameOldFile(ism);

            Logger.Log(String.Format("System:     Creating {0} file...", Path.GetFileName(ism)));

            File.Copy(referenceDb, ism, true);

            try {
                using (var origDatabase = new Database(referenceDb, DatabaseOpenMode.ReadOnly)) {
                    using (var db = new Database(ism, DatabaseOpenMode.Direct)) {
                        proj.ProductCode = msiEditor.GenerateUniqueGuid(db, "ism");
                        proj.UpgradeCode = msiEditor.GenerateUniqueGuid(db, "ism");

                        msiEditor.Feature_AddOrUpdate(db, proj.FeatureName, proj.FeatureName, "0", "1", "INSTALLDIR", "48");

                        msiEditor.Component_AddOrUpdate(db, auditComponentx86, msiEditor.GenerateUniqueGuid(db, "component"), "TARGETDIR", "4", proj.FeatureName);
                        msiEditor.Component_AddOrUpdate(db, auditComponentx64, msiEditor.GenerateUniqueGuid(db, "component"), "TARGETDIR", "260", proj.FeatureName);


                        foreach (KeyValuePair<string, string> kvp in regDict) {
                            if (kvp.Key.Equals("MSIPackageName")) {
                                msiEditor.Reg_Add(db, msiEditor.GenerateUniqueGuid(db, "registry"), "2", auditKeyPath, kvp.Key, Path.GetFileName(this.adm.FullMsiPath), auditComponentx64);
                                msiEditor.Reg_Add(db, msiEditor.GenerateUniqueGuid(db, "registry"), "2", auditKeyPath, kvp.Key, Path.GetFileName(this.adm.FullMsiPath), auditComponentx86);
                            } else {
                                msiEditor.Reg_Add(db, msiEditor.GenerateUniqueGuid(db, "registry"), "2", auditKeyPath, kvp.Key, kvp.Value, auditComponentx64);
                                msiEditor.Reg_Add(db, msiEditor.GenerateUniqueGuid(db, "registry"), "2", auditKeyPath, kvp.Key, kvp.Value, auditComponentx86);
                            }
                        }

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

                        Logger.Log(String.Format("ISM:     Saving {0} file...", Path.GetFileName(ism)));

                        db.Commit();
                    }
                }
            } catch {
                throw;
            } finally {
                //File.Delete(tempDb);
            }
        }
    }
}

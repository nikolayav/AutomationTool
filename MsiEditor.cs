using Microsoft.Deployment.WindowsInstaller;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace AutomationTool {
    class MsiEditor {
        Logger logger;
        public Logger Logger { get => logger; set => logger = value; }
        public MsiEditor() {}

        public void Feature_AddOrUpdate(Database db, string feature, string title, string display, string level, string directory_, string attributes) {
            logger.Log(String.Format("MSI:     Creating feature '{0}'...", feature));

            IList featuresWithSameName = db.ExecuteQuery(String.Format("SELECT * FROM Feature WHERE Feature = '{0}'", feature));
            if (featuresWithSameName.Count > 0) {
                db.Execute(String.Format("UPDATE Feature SET Title = '{0}', Display = '{1}', Level = '{2}', Directory_ = '{3}', Attributes = '{4}' WHERE Feature = '{5}'", title, display, level, directory_, attributes, feature));
            } else {
                db.Execute(String.Format("INSERT INTO `Feature` (Feature, Title, Display, Level, Directory_, Attributes) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')", feature, title, display, level, directory_, attributes));
            }
        }

        public void Component_AddOrUpdate(Database db, string component, string componentId, string directory_, string attributes, string feature, string condition) {

            IList componentsWithSameName = db.ExecuteQuery(String.Format("SELECT * FROM Component WHERE Component = '{0}'", component));
            if (componentsWithSameName.Count > 0) { 
                logger.Log(String.Format("MSI:     Updating component '{0}'", component));
                db.Execute(String.Format("UPDATE Component SET ComponentId = '{0}', Directory_ = '{1}', Attributes = '{2}', Condition = '{3}' WHERE Component = '{4}'", componentId, directory_, attributes, condition, component));

            } else {
                logger.Log(String.Format("MSI:     Creating component '{0}'", component));
                db.Execute(String.Format("INSERT INTO `Component` (`Component`, `ComponentId`, `Directory_`, `Attributes`, `Condition`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}')", component, componentId, directory_, attributes, condition));
            }
            
            IList<string> associatedFeatures = db.ExecuteStringQuery(String.Format("SELECT * FROM FeatureComponents WHERE Component_ = '{0}'", component));
           
            if (associatedFeatures.Count == 0) {
                
                logger.Log(String.Format("MSI:     Attaching component '{0}' to feature '{1}'", component, feature));
                db.Execute(String.Format("INSERT INTO `FeatureComponents` (Feature_, Component_) VALUES ('{0}', '{1}')", feature, component));
            }
        }

        public void Component_SetKeyPath(Database db, string component, string keyPath) {
            if (!String.IsNullOrEmpty(keyPath)) {
                IList componentsWithSameName = db.ExecuteQuery(String.Format("SELECT * FROM Component WHERE Component = '{0}'", component));
                if (componentsWithSameName.Count > 0) {
                    db.Execute(String.Format("UPDATE Component SET KeyPath = '{0}' WHERE Component = '{1}'", keyPath, component));
                }
            }
        }

        public string Registry_GetGuid(Database db, String component) {
            string regGuid = String.Empty;
            IList registry = db.ExecuteQuery(String.Format("SELECT Registry FROM Registry WHERE Component_ = '{0}'", component));
            if (registry.Count > 0) {
                foreach (string reg in registry) {
                    if (reg[0].Equals('_')) {
                        regGuid = reg;
                        break;
                    }
                }
            }
            return regGuid;
        }

        public void dropRegs(Database db, string key) {
            if (db.Tables.Contains("Registry")) {
                db.Execute(String.Format("DELETE FROM `Registry` WHERE `Registry`.`Key` = '{0}'", key));
            }
        }

        public void Reg_Add(Database db, string registry, string root, string key, string name, string value, string component) {
            logger.Log(String.Format("MSI:     Creating registry key HKLM\\{0} with value {1} = {2}", key, name, value));

            db.Execute(String.Format("INSERT INTO `Registry` (`Registry`, `Root`, `Key`, `Name`, `Value`, `Component_`) VALUES ('{0}', {1}, '{2}', '{3}', '{4}', '{5}')", registry, root, key, name, value, component));
        }

        public void Property_AddOrUpdate(string property, string value, Database db) {

            IList existingProperties = db.ExecuteQuery("SELECT * FROM Property WHERE Property = '" + property + "'");
            if (existingProperties.Count > 0) {
                logger.Log(String.Format("MSI:     Updating property {0}={1}", property, value));
                db.Execute(String.Format("UPDATE Property SET Value = '{0}' WHERE Property = '{1}'", value, property));
            } else {
                logger.Log(String.Format("MSI:     Creating property {0}={1}", property, value));
                db.Execute(String.Format("INSERT INTO Property (Property, Value) VALUES ('{0}', '{1}')", property, value));
            }

        }

        public string GetProductName(Database db) {
            IList productName = db.ExecuteQuery("SELECT Value FROM Property WHERE Property = 'ProductName'");
            string pName = "";
            if (productName[0] != null) {
                pName = productName[0].ToString();
            }
            return pName;
        }
            public string GetProductAndUpgradeCodes(Database db, string codeType) {
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

        public string GenerateUniqueGuid(Database db, string type) {
            string guid = "";
            if (type.Equals("ism")) {
                guid = String.Format("{0}{1}{2}", "{", Guid.NewGuid().ToString().ToUpperInvariant(), "}");
            }

            if (type.Equals("registry")) {
                guid = "_" + Guid.NewGuid().ToString().ToUpperInvariant();
                guid = Regex.Replace(guid, "-", "");
                
                if (db.Tables.Contains("Registry")) {
                    IList existingRegs = db.ExecuteQuery("SELECT Registry FROM Registry");

                    foreach (string componentId in existingRegs) {
                        if (componentId.Equals(guid, StringComparison.InvariantCultureIgnoreCase)) {
                            return GenerateUniqueGuid(db, type);
                        }
                    }
                } else {
                    if (!db.Tables.Contains("Registry")) {
                        db.Import(@"templates\Registry.idt");
                    }
                    // throw new System.InvalidOperationException("'Registry' table does not exist in MSI.");
                }

            } else if (type.Equals("component")) {
                guid = "{" + Guid.NewGuid().ToString().ToUpperInvariant() + "}";

                if (db.Tables.Contains("Component")) {

                    IList existingComponents = db.ExecuteQuery("SELECT ComponentId FROM Component");

                    foreach (string componentId in existingComponents) {
                        if (componentId.Equals(guid, StringComparison.InvariantCultureIgnoreCase)) {
                            return GenerateUniqueGuid(db, type);
                        }
                    }
                } else {
                    throw new System.InvalidOperationException("'Component' table does not exist in MSI.");
                }
            }
            if (guid.Equals("{00000000-0000-0000-0000-000000000000}", StringComparison.InvariantCultureIgnoreCase) ||
                guid.Equals("{5F0C6514-2485-4FC8-8029-A1A7A2CFA768}", StringComparison.InvariantCultureIgnoreCase) ||
                guid.Equals("{E8E3F922-E7E0-46F0-99C2-2AB0FFA6BDBF}", StringComparison.InvariantCultureIgnoreCase)) {
                return GenerateUniqueGuid(db, type);
            }
            return guid;
        }

        public void ProcessProperties(Database db, ProjectInfo proj) {
            Property_AddOrUpdate("ALLUSERS", "1", db);
            Property_AddOrUpdate("ARPNOMODIFY", "1", db);
            Property_AddOrUpdate("ARPNOREMOVE", "1", db);
            Property_AddOrUpdate("ARPNOREPAIR", "1", db);
            Property_AddOrUpdate("BMW_Package_Author", proj.AuthorName, db);
            Property_AddOrUpdate("BMW_PackageName", proj.PkgName, db);
            Property_AddOrUpdate("BMW_PackageVersion", proj.PkgVer, db);
            Property_AddOrUpdate("MSIRESTARTMANAGERCONTROL", "Disable", db);
            Property_AddOrUpdate("PROMPTROLLBACKCOST", "D", db);
            Property_AddOrUpdate("REBOOT", "ReallySuppress", db);
            Property_AddOrUpdate("REBOOTPROMPT", "S", db);
            if (proj.isCustomMsi || proj.isEditMsi) {
                Property_AddOrUpdate("ProductName", proj.AppName, db);
                Property_AddOrUpdate("ProductVersion", proj.AppVer, db);
                Property_AddOrUpdate("Manufacturer", "BMW Package Factory", db);
                Property_AddOrUpdate("ProductCode", proj.ProductCode, db);
                Property_AddOrUpdate("UpgradeCode", proj.UpgradeCode, db);
            }

        }

        public Dictionary<string, string> checkProperties(string loadMsiPath, string loadMstPath) {
            string referenceDb = loadMsiPath;
            string mstDb = loadMstPath;

            Dictionary<string, string> propDictExtract = new Dictionary<string, string>();


            string[] propertiesToCheck = { "ALLUSERS", "ARPNOMODIFY", "ARPNOREMOVE", "ARPNOREPAIR", "BMW_Package_Author", "BMW_PackageName", "BMW_PackageVersion",
                "Manufacturer", "MSIRESTARTMANAGERCONTROL", "ProductName", "ProductVersion", "PROMPTROLLBACKCOST", "REBOOT", "REBOOTPROMPT"};

            try {
                using (var origDatabase = new Database(referenceDb, DatabaseOpenMode.ReadOnly)) {
                    origDatabase.ApplyTransform(loadMstPath);

                    var summaryInfo = origDatabase.SummaryInfo;

                    // Extract {Property} = {Value} from MSI based on the listed properties in propertiesToCheck
                    for (int i = 0; i < propertiesToCheck.Length; i++) { 
                            IList properties = origDatabase.ExecuteQuery(String.Format("SELECT * FROM Property where Property = '{0}'", propertiesToCheck[i]));
                            if (properties.Count > 0) {
                                propDictExtract.Add(propertiesToCheck[i], properties[1].ToString());

                            }
                        }
                    }
            } catch {
                throw;
            } finally {
                //File.Delete(tempDb);
            }

            return propDictExtract;
        }

        public void UpdateFeatureAssociations(Database db, ProjectInfo proj, string feature, string title, string display, string level, string directory_, string attributes) {
            string oldFeature = FindExistingFeature(db, proj);

            IList<string> fullComponents = findComponentsAssociatedWithOldFeature(db, oldFeature);
            List<string> components = new List<string>();
            for (int i = 0; i < fullComponents.Count; i++) {
                if (i % 2 != 0)  {
                    components.Add(fullComponents[i]);
                }
            }

            logger.Log(String.Format("MSI:     Creating feature '{0}'...", feature));

            IList featuresWithSameName = db.ExecuteQuery(String.Format("SELECT * FROM Feature WHERE Feature = '{0}'", feature));
            if (featuresWithSameName.Count == 0) {
                logger.Log(String.Format("MSI:     Creating new feature '{0}'", feature));
                db.Execute(String.Format("INSERT INTO `Feature` (Feature, Title, Display, Level, Directory_, Attributes) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')", feature, title, display, level, directory_, attributes));
            }

            foreach (string c in components) {
                logger.Log(String.Format("MSI:     Attaching component '{0}' to feature '{1}'", c, feature));
                db.Execute(String.Format("DELETE FROM FeatureComponents WHERE Component_ = '{0}'", c));
                db.Execute(String.Format("INSERT INTO `FeatureComponents` (Feature_, Component_) VALUES ('{0}', '{1}')", feature, c));

                logger.Log(String.Format("MSI:     Updateing GUID for component '{0}'", c));
                db.Execute(String.Format("UPDATE Component SET ComponentId = '{0}' WHERE Component = '{1}'", GenerateUniqueGuid(db, "component"), c));
            }
            logger.Log(String.Format("MSI:     Deleting old feature '{0}'", oldFeature));
            if (!String.IsNullOrEmpty(oldFeature)) {
                db.Execute(String.Format("DELETE FROM Feature WHERE Feature = '{0}'", oldFeature));
            }
        }

        public IList<string> findComponentsAssociatedWithOldFeature(Database db, string oldFeature) {
            IList<string> components = db.ExecuteStringQuery(String.Format("SELECT * FROM FeatureComponents WHERE Feature_ = '{0}'", oldFeature));
            return components;
        }

        public string FindExistingFeature(Database db, ProjectInfo proj) {
            string featureName = "";
            IList<string> features = db.ExecuteStringQuery("SELECT Feature FROM Feature");
            foreach (string feature in features) {
                if (feature.Contains("BMW_")) {
                    featureName = feature;
                    break;
                }
            }

            return featureName;
        }

        public int MaxProgressValue { get => 0; }
        // Declare the delegate (if using non-generic pattern).
        public delegate void TickProgress(object sender, EventArgs e);
        // Declare the event.
        public event TickProgress ProgressChanged;

        private void ReportProgress() {
            ProgressChanged.Invoke(this, new EventArgs());
        }
    }
}

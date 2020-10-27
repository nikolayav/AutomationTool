using Microsoft.Deployment.WindowsInstaller;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AutomationTool {
    class MsiEditor {
        Logger logger;
        public MsiEditor() {}

        public Logger Logger { get => logger; set => logger = value; }
        public void Feature_AddOrUpdate(Database db, string feature, string title, string display, string level, string directory_, string attributes) {
            logger.Log(String.Format("Transform:     Creating feature '{0}'...", feature));

            IList featuresWithSameName = db.ExecuteQuery(String.Format("SELECT * FROM Feature WHERE Feature = '{0}'", feature));
            if (featuresWithSameName.Count > 0) {
                db.Execute(String.Format("UPDATE Feature SET Feature = '{0}', Title = '{1}', Display = '{2}', Level = '{3}', Directory_ = '{4}', Attributes = '{5}' WHERE Feature = '{0}'", feature, title, display, level, directory_, attributes));
            } else {
                db.Execute(String.Format("INSERT INTO `Feature` (Feature, Title, Display, Level, Directory_, Attributes) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')", feature, title, display, level, directory_, attributes));
            }
        }

        public void Component_AddOrUpdate(Database db, string component, string componentId, string directory_, string attributes, string feature) {
            logger.Log(String.Format("Transform:     Creating component '{0}'", component));

            IList featuresWithSameName = db.ExecuteQuery(String.Format("SELECT * FROM Component where Component = '{0}'", component));
            if (featuresWithSameName.Count > 0) {
                db.Execute(String.Format("UPDATE Component SET Component = '{0}', ComponentId = '{1}', Directory_ = '{2}', Attributes = '{3}' WHERE Component = '{0}'", component, componentId, directory_, attributes));
            } else {
                db.Execute(String.Format("INSERT INTO `Component` (`Component`, `ComponentId`, `Directory_`, `Attributes`) VALUES ('{0}', '{1}', '{2}', '{3}')", component, componentId, directory_, attributes));
            }
            
            IList<string> associatedFeatures = db.ExecuteStringQuery(String.Format("SELECT * FROM FeatureComponents WHERE Component_ = '{0}'", component));

            if (associatedFeatures.Count > 0) {
                
                db.Execute(String.Format("UPDATE `FeatureComponents` SET `FeatureComponents`.`Feature_` = '{0}' WHERE `FeatureComponents`.`Component_` = '{1}'", feature, component));
            } else {
                db.Execute(String.Format("INSERT INTO `FeatureComponents` (Feature_, Component_) VALUES ('{0}', '{1}')", feature, component));
            }
        }

        public void dropRegs(Database db, string key) {
            db.Execute(String.Format("DELETE FROM `Registry` WHERE `Registry`.`Key` = '{0}'", key));

        }

        public void Reg_Add(Database db, string registry, string root, string key, string name, string value, string component) {
            logger.Log(String.Format("Transform:     Creating registry key HKLM\\{0} with value {1} = {2}", key, name, value));

            db.Execute(String.Format("INSERT INTO `Registry` (`Registry`, `Root`, `Key`, `Name`, `Value`, `Component_`) VALUES ('{0}', {1}, '{2}', '{3}', '{4}', '{5}')", registry, root, key, name, value, component));
        }

        public void Property_AddOrUpdate(string property, string value, Database db) {

            IList existingProperties = db.ExecuteQuery("SELECT * FROM Property where Property = '" + property + "'");
            if (existingProperties.Count > 0) {
                logger.Log(String.Format("Transform:     Updating property {0} = {1}", property, value));
                db.Execute(String.Format("UPDATE Property SET Value = '{0}' WHERE Property = '{1}'", value, property));
            } else {
                logger.Log(String.Format("Transform:     Creating property {0}={1}", property, value));
                db.Execute(String.Format("INSERT INTO Property (Property, Value) VALUES ('{0}', '{1}')", property, value));
            }

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

            IList existingRegs = db.ExecuteQuery("SELECT Registry FROM Registry");

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
            if (guid.Equals("{00000000-0000-0000-0000-000000000000}", StringComparison.InvariantCultureIgnoreCase) || guid.Equals("{5F0C6514-2485-4FC8-8029-A1A7A2CFA768}", StringComparison.InvariantCultureIgnoreCase) || guid.Equals("{E8E3F922-E7E0-46F0-99C2-2AB0FFA6BDBF}", StringComparison.InvariantCultureIgnoreCase)) {
                return GenerateUniqueGuid(db, type);
            }
            return guid;
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

            logger.Log(String.Format("Transform:     Creating feature '{0}'...", feature));

            IList featuresWithSameName = db.ExecuteQuery(String.Format("SELECT * FROM Feature where Feature = '{0}'", feature));
            if (featuresWithSameName.Count == 0) {
                db.Execute(String.Format("INSERT INTO `Feature` (Feature, Title, Display, Level, Directory_, Attributes) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')", feature, title, display, level, directory_, attributes));
            }

            foreach (string c in components) {
                db.Execute(String.Format("DELETE FROM FeatureComponents WHERE Component_ = '{0}'", c));
                db.Execute(String.Format("INSERT INTO `FeatureComponents` (Feature_, Component_) VALUES ('{0}', '{1}')", feature, c));
            }

            db.Execute(String.Format("DELETE FROM Feature WHERE Feature = '{0}'", oldFeature));
        }

        public IList<string> findComponentsAssociatedWithOldFeature(Database db, string oldFeature) {
            
            IList<string> components = db.ExecuteStringQuery(String.Format("SELECT * FROM FeatureComponents where Feature_ = '{0}'", oldFeature));


            return components;
        }

        public string FindExistingFeature(Database db, ProjectInfo proj) {
            string featureName = "";
            IList<string> features = db.ExecuteStringQuery("SELECT Feature FROM Feature");
            foreach (string feature in features) {
                if (feature.Contains(String.Format("BMW_{0}", proj.PkgName))) {
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

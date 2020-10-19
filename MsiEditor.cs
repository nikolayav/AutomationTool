using Microsoft.Deployment.WindowsInstaller;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting.Proxies;
using System.ServiceModel.Security.Tokens;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AutomationTool {
    class MsiEditor {
        Logger logger;
        public MsiEditor() { }

        public Logger Logger { get => logger; set => logger = value; }
        public void Feature_AddOrUpdate(Database db, string feature, string title, string display, string level, string directory_, string attributes) {
            logger.Log(String.Format("Transform:     Creating feature '{0}'...", feature));

            IList featuresWithSameName = db.ExecuteQuery("SELECT * FROM Feature where Feature = '" + feature + "'");
            if (featuresWithSameName.Count > 0) {
                db.Execute("UPDATE Feature SET Feature = '" + feature + "', Title = '" + title + "', Display = '" + display + "', Level = '" + level + "', Directory_ = '" + directory_ + "', Attributes = '" + attributes + "' WHERE Feature = '" + feature + "'");
            } else {
                db.Execute("INSERT INTO `Feature` (Feature, Title, Display, Level, Directory_, Attributes) VALUES ('" + feature + "', '" + title + "', '" + display + "', '" + level + "', '" + directory_ + "', '" + attributes + "')");
            }
        }

        public void Component_AddOrUpdate(Database db, string component, string componentId, string directory_, string attributes, string feature) {
            logger.Log(String.Format("Transform:     Creating component '{0}'", component));
            IList featuresWithSameName = db.ExecuteQuery("SELECT * FROM Component where Component = '" + component + "'");
            if (featuresWithSameName.Count > 0) {
                db.Execute("UPDATE Component SET Component = '" + component + "', ComponentId = '" + componentId + "', Directory_ = '" + directory_ + "', Attributes = '" + attributes + "' WHERE Component = '" + component + "'");
            } else {
                db.Execute("INSERT INTO `Component` (`Component`, `ComponentId`, `Directory_`, `Attributes`) VALUES ('{0}', '{1}', '{2}', '{3}')", component, componentId, directory_, attributes);
            }

            IList associatedFeatures = db.ExecuteQuery("SELECT * FROM FeatureComponents where Component_ = '" + component + "'");
            if (associatedFeatures.Count > 0) {
                db.Execute("UPDATE Component SET Feature_ = '" + feature + "', Component_ = '" + component + "'");
            } else {
                db.Execute("INSERT INTO `FeatureComponents` (Feature_, Component_) VALUES ('{0}', '{1}')", feature, component);
            }
        }

        public void Reg_Add(Database db, string registry, string root, string key, string name, string value, string component) {
            logger.Log(String.Format("Transform:     Creating registry key HKLM\\{0} with value {1} = {2}", key, name, value));

            db.Execute("INSERT INTO `Registry` (`Registry`, `Root`, `Key`, `Name`, `Value`, `Component_`) VALUES ('{0}', {1}, '{2}', '{3}', '{4}', '{5}')", registry, root, key, name, value, component);
        }

        public void Property_AddOrUpdate(string property, string value, Database db) {

            IList existingProperties = db.ExecuteQuery("SELECT * FROM Property where Property = '" + property + "'");
            if (existingProperties.Count > 0) {
                logger.Log(String.Format("Transform:     Updating property {0} = {1}", property, value));
                db.Execute("UPDATE Property SET Value = '" + value + "' WHERE Property = '" + property + "'");
            } else {
                logger.Log("Transform:     Creating property " + String.Format("{0}={1}", property, value));
                db.Execute("INSERT INTO Property (Property, Value) VALUES ('{0}', '{1}')", property, value);
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
            //string tempDb = String.Format(loadMsiPath, "_tmp1", proj.MsiName);
            Dictionary<string, string> propDictExtract = new Dictionary<string, string>();
            string[] propertiesToCheck = { "ALLUSERS", "ARPNOMODIFY", "ARPNOREMOVE", "ARPNOREPAIR", "BMW_Package_Author", "BMW_PackageName", "BMW_PackageVersion",
                "Manufacturer", "MSIRESTARTMANAGERCONTROL", "ProductName", "ProductVersion", "PROMPTROLLBACKCOST", "REBOOT", "REBOOTPROMPT"};

            try {
                using (var origDatabase = new Database(referenceDb, DatabaseOpenMode.ReadOnly)) {
                    origDatabase.ApplyTransform(loadMstPath);
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

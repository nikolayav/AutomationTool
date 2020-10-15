using Microsoft.Deployment.WindowsInstaller;
using System;
using System.Collections;
using System.Text.RegularExpressions;


namespace AutomationTool {
    class MsiEditor {
        Logger logger;

        

        public MsiEditor() {

        }
        public Logger Logger { get => logger; set => logger = value; }
        public void Feature_AddOrUpdate(Database db, string feature, string title, string display, string level, string directory_, string attributes) {
            logger.Log("Transform:     Creating feature '" + feature + "'...");

            IList featuresWithSameName = db.ExecuteQuery("SELECT * FROM Feature where Feature = '" + feature + "'");
            if (featuresWithSameName.Count > 0) {
                db.Execute("UPDATE Feature SET Feature = '" + feature + "', Title = '" + title + "', Display = '" + display + "', Level = '" + level + "', Directory_ = '" + directory_ + "', Attributes = '" + attributes + "' WHERE Feature = '" + feature + "'");
            } else {
                db.Execute("INSERT INTO `Feature` (Feature, Title, Display, Level, Directory_, Attributes) VALUES ('" + feature + "', '" + title + "', '" + display + "', '" + level + "', '" + directory_ + "', '" + attributes + "')");
            }
        }

        public void Component_AddOrUpdate(Database db, string component, string componentId, string directory_, string attributes, string feature) {
            logger.Log("Transform:     Creating component '" + component + "'");
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

        public void Reg_Add(Database db, string registry, string root, string key, string name, string value, string component) {
            logger.Log("Transform:     Creating registry value '" + value + "' in HLKM\\" + key + "");

            db.Execute("INSERT INTO `Registry` (`Registry`, `Root`, `Key`, `Name`, `Value`, `Component_`) VALUES ('{0}', {1}, '{2}', '{3}', '{4}', '{5}')", registry, root, key, name, value, component);
        }

        public void Property_AddOrUpdate(string property, string value, Database db) {

            IList existingProperties = db.ExecuteQuery("SELECT * FROM Property where Property = '" + property + "'");
            if (existingProperties.Count > 0) {
                logger.Log("Transform:     Updating property " + property + " = " + value + "");
                db.Execute("UPDATE Property SET Value = '" + value + "' WHERE Property = '" + property + "'");
            } else {
                logger.Log("Transform:     Creating property " + property + " = " + value + "");
                db.Execute("INSERT INTO Property (Property, Value) VALUES ('" + property + "', '" + value + "')");
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
                guid = "{" + Guid.NewGuid().ToString().ToUpperInvariant() + "}";
                if (guid.Equals("{00000000-0000-0000-0000-000000000000}")) {
                    return GenerateUniqueGuid(db, type);
                }
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

            return guid;
        }
    }
}

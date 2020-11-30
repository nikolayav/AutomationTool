namespace AutomationTool {
    public class ProjectInfo {
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
        string editMstPath;
        public bool is32bit;
        public bool isCustomMsi;
        public bool isEditMst;
        public bool isEditMsi;

        public ProjectInfo(string folderPath, string msiName, string pimsId, string appName, string appVer,
            string pkgName, string pkgVer, string authorName, string productCode, string upgradeCode, string featureName, 
            string comments, bool is32bit, bool isCustomMsi, bool isEditMst, string editMstPath, bool editMsi) {
            this.folderPath = folderPath;
            this.msiName = msiName;
            this.pimsId = pimsId;
            this.appName = appName;
            this.appVer = appVer;
            this.pkgName = pkgName;
            this.pkgVer = pkgVer;
            this.authorName = authorName;
            this.productCode = productCode;
            this.upgradeCode = upgradeCode;
            this.featureName = featureName;
            this.comments = comments;
            this.is32bit = is32bit;
            this.isCustomMsi = isCustomMsi;
            this.isEditMst = isEditMst;
            this.editMstPath = editMstPath;
            this.isEditMsi = editMsi;
        }

        public string EditMstPath { get => editMstPath; set => editMstPath = value; }
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

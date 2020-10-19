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

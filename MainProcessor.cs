using System;

namespace AutomationTool {
    class MainProcessorEventArgs : EventArgs {
        int _progress;
        public int Progress { get => _progress; }
      

        public MainProcessorEventArgs(int progress) {
            _progress = progress;
        }
    }
        class MainProcessor {
        MsiEditor _msiEditor;
        Administrator _adm;
        Creator _creator;
        Logger _logger;
        ProjectInfo _proj;


        // Declare the delegate (if using non-generic pattern).
        public delegate void GetProgress(object sender, MainProcessorEventArgs e);
        // Declare the event.
        public event GetProgress ProgressChanged;

        private int _maxProgressValue;
        public int MaxProgressValue { get => _maxProgressValue; }
        public MainProcessor(MsiEditor msiEditor, Administrator adm, Creator creator, Logger logger, ProjectInfo proj) {
            _msiEditor = msiEditor;
            _adm = adm;
            _creator = creator;
            _logger = logger;
            _proj = proj;
            _maxProgressValue = _msiEditor.MaxProgressValue + _adm.MaxProgressValue + _creator.MaxProgressValue;
            _msiEditor.ProgressChanged += SetProgress;
            _adm.ProgressChanged += SetProgress;
            _creator.ProgressChanged += SetProgress;

        }

        private int _progressValue;
        private void SetProgress(object sender, EventArgs e) {
            _progressValue += 1;
            if (_progressValue == 19) {
                int i = 0;
            }
            ProgressChanged.Invoke(this, new MainProcessorEventArgs(_progressValue));
        }

        public void startProcessing() {
            _progressValue = 0;

            _adm.createFolders(String.Format("{0}_{1}", _proj.PkgName, _proj.PkgVer));

            if (!_proj.isCustomMsi && !_proj.isEditMst && !_proj.isEditMsi) {
                _creator.CreateMst(_proj);
                _adm.CreateInstallUninstallVbs(_proj, false, String.Format("{0}_{1}_install.vbs", _proj.PkgName, _proj.PkgVer), String.Format("{0}_{1}_uninstall.vbs", _proj.PkgName, _proj.PkgVer));
            } else if (_proj.isCustomMsi) {
                _creator.GenerateCustomMsi(_proj, _proj.is32bit);
                _creator.CreateXLSX(_proj);
                _adm.CreateInstallUninstallVbs(_proj, true, String.Format("{0}_{1}_install.vbs", _proj.PkgName, _proj.PkgVer), String.Format("{0}_{1}_uninstall.vbs", _proj.PkgName, _proj.PkgVer));
            } else if (_proj.isEditMst) {
                _creator.EditMst(_proj);
                _creator.CreateXLSX(_proj);
                _adm.CreateInstallUninstallVbs(_proj, false, String.Format("{0}_{1}_install.vbs", _proj.PkgName, _proj.PkgVer), String.Format("{0}_{1}_uninstall.vbs", _proj.PkgName, _proj.PkgVer));
            } else if (_proj.isEditMsi) {
                _creator.EditMsi(_proj);
                _adm.CreateInstallUninstallVbs(_proj, true, String.Format("{0}_{1}_install.vbs", _proj.PkgName, _proj.PkgVer), String.Format("{0}_{1}_uninstall.vbs", _proj.PkgName, _proj.PkgVer));
            }
            _creator.CreateXLSX(_proj);
        }
    }
}

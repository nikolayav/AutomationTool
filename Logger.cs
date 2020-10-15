using NLog;
using NLog.Config;
using NLog.Targets;
using NLog.Targets.Wrappers;

namespace AutomationTool {
    class Logger {

        private NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public Logger() {}

        public void ConfigureLogging() {
            // Intialize Config Object
            LoggingConfiguration config = new LoggingConfiguration();

            // Initialize Console Target
            var consoleTarget = new ColoredConsoleTarget("Console Target") {
                Layout = @"${time} ${longdate} ${uppercase: ${level}} ${logger} ${message} ${exception: format=ToString}"
            };

            // Initialize the AsyncWrapper for the ConsoleTarget
            AsyncTargetWrapper consoleWrapper = new AsyncTargetWrapper();
            consoleWrapper.WrappedTarget = consoleTarget;
            consoleWrapper.OverflowAction = AsyncTargetWrapperOverflowAction.Block;
            consoleWrapper.QueueLimit = 5000;

            // Initialize the AsyncFlushTargetWrapper for the ConsoleWrapper
            AutoFlushTargetWrapper consoleFlushWrapper = new AutoFlushTargetWrapper();
            consoleFlushWrapper.WrappedTarget = consoleWrapper;

            // This condition controls when the log is flushed. Set the LogLevel to be equivalent to the maximum level specified in the targetRule
            consoleFlushWrapper.Condition = "level >= LogLevel.Trace";

            // Adding the target to the config
            config.AddTarget("console", consoleFlushWrapper);


            // Initialize File Target
            var fileTarget = new FileTarget("File Target") {
                FileName = "Logs\\AutomationTool.log",
                KeepFileOpen = false,
                Layout = @"[${date}] ${message} ${exception: format=ToString}"
            };

            // Initialize the AsyncWrapper for the fileTarget
            AsyncTargetWrapper fileWrapper = new AsyncTargetWrapper();
            fileWrapper.WrappedTarget = fileTarget;
            fileWrapper.QueueLimit = 5000;
            fileWrapper.OverflowAction = AsyncTargetWrapperOverflowAction.Block;

            // Initialize the AsyncFlushTargetWrapper for the FileWrapper
            AutoFlushTargetWrapper fileFlushWrapper = new AutoFlushTargetWrapper();
            fileFlushWrapper.WrappedTarget = fileWrapper;

            // This condition controls when the log is flushed. Set the LogLevel to be equivalent to the maximum level specified in the targetRule
            fileFlushWrapper.Condition = "level >= LogLevel.Trace";

            // Adding the target to the config
            config.AddTarget("file", fileFlushWrapper);

            // Creating the Log Level rules for each target and adding them to the config
            // Edit these to change what methods are logged
            var fileRule = new LoggingRule("*", fileTarget);
            fileRule.EnableLoggingForLevels(LogLevel.Trace, LogLevel.Info);
            fileRule.EnableLoggingForLevel(LogLevel.Error);
            config.LoggingRules.Add(fileRule);

            var consoleRule = new LoggingRule("*", consoleTarget);
            consoleRule.EnableLoggingForLevels(LogLevel.Trace, LogLevel.Info);
            consoleRule.EnableLoggingForLevel(LogLevel.Error);
            config.LoggingRules.Add(consoleRule);

            // Assigning the configuration to the logger
            LogManager.Configuration = config;

        }
        public void Log(string logMessage) {
           logger.Info(logMessage);
        }
    }
}

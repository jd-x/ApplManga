using static jdx.ApplManga.AppLogger.Core.Logger.AppLoggerBase;

namespace jdx.ApplManga.AppLogger.Core.Logger {
    public static class AppLogHelper {
        private static AppLoggerBase appLogger = null;

        public static void Log(LogTarget target, string message) {
            switch (target) {
                case LogTarget.File:
                    appLogger = new FileLogger();
                    appLogger.Log(message);
                    break;
                case LogTarget.EventLog:
                    appLogger = new EventLogger();
                    appLogger.Log(message);
                    break;
                case LogTarget.Database:
                    appLogger = new DbLogger();
                    appLogger.Log(message);
                    break;
                default:
                    return;
            }
        }
    }
}

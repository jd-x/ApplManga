namespace jdx.ApplManga.AppLogger.Core.Logger {
    public abstract class AppLoggerBase {
        public enum LogTarget {
            File, Database, EventLog
        }

        protected readonly object lockObj = new object();
        public abstract void Log(string message);
    }
}

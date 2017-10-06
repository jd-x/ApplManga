using System.Diagnostics;

namespace jdx.ApplManga.AppLogger.Core.Logger {
    public class EventLogger : AppLoggerBase {
        public override void Log(string message) {
            lock (lockObj) {
                EventLog eventLog = new EventLog("");
                eventLog.Source = "ApplEventLog";
                eventLog.WriteEntry(message);
            }
        }
    }
}

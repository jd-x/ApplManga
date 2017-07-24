using System.Diagnostics;

namespace ApplManga.Utils.AppLogger.Core.Logger {
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

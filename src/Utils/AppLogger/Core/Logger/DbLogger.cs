namespace ApplManga.Utils.AppLogger.Core.Logger {
    public class DbLogger : AppLoggerBase {
        private string _connString = string.Empty;

        public override void Log(string message) {
            // Log data to database
            lock (lockObj) {
            }
        }
    }
}

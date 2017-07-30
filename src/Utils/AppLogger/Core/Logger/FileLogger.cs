using System;
using System.IO;
using System.Linq;

namespace ApplManga.Utils.AppLogger.Core.Logger {
    public class FileLogger : AppLoggerBase {
        private string _logFileName = GetTimeStamp(DateTime.Now) + @"_AppLog.log";

        private string _logFilePath = AppDomain.CurrentDomain.BaseDirectory;
        public string LogFilePath {
            get { return Path.Combine(_logFilePath, "Logs", _logFileName); }
            set { _logFilePath = value; }
        }

        private void CreateLogDirectory() {
            string logFolderPath = Path.Combine(_logFilePath, "Logs");

            if (!Directory.Exists(logFolderPath)) {
                Directory.CreateDirectory(logFolderPath);
            }

            // Limit only to 10 log files
            foreach (var logFile in new DirectoryInfo(logFolderPath).GetFiles().OrderByDescending(x => x.LastWriteTime).Skip(10)) {
                logFile.Delete();
            }
        }

        private static string GetTimeStamp(DateTime value) {
            return value.ToString("yyyyMMddHHmmss");
        }

        public override void Log(string message) {
            lock (lockObj) {
                CreateLogDirectory();

                using (StreamWriter streamWriter = new StreamWriter(new FileStream(LogFilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))) {
                    streamWriter.WriteLine(String.Format("{0} {1} - {2}", DateTime.Now.ToShortDateString(), DateTime.Now.ToLongTimeString(), message));
                    streamWriter.Close();
                }
            }

        }
    }
}

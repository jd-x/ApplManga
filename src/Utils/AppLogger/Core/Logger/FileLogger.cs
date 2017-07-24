using System;
using System.IO;

namespace ApplManga.Utils.AppLogger.Core.Logger {
    public class FileLogger : AppLoggerBase {
        private string _logFileName = @"AppLog.txt";

        private string _logFilePath = @"D:\";
        public string LogFilePath {
            get { return _logFilePath + _logFileName; }
            set { _logFilePath = value; }
        }

        private void CreateLogFile() {
            if(!File.Exists(LogFilePath)) {
                File.Create(LogFilePath);
            }
        }

        public override void Log(string message) {
            lock(lockObj) {
                CreateLogFile();

                using(StreamWriter streamWriter = new StreamWriter(new FileStream(LogFilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))) {
                    streamWriter.WriteLine(String.Format("{0} {1} - Log info: {2}", DateTime.Now.ToShortDateString(), DateTime.Now.ToLongTimeString(), message));
                    streamWriter.Close();
                }
            }
            
        }
    }
}

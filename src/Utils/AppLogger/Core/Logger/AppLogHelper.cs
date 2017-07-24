using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ApplManga.Utils.AppLogger.Core.Logger.AppLoggerBase;

namespace ApplManga.Utils.AppLogger.Core.Logger {
    public static class AppLogHelper {
        private static AppLoggerBase appLogger = null;

        public static void Log(LogTarget target, string message) {
            switch(target) {
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

using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMLoggerBackend.Utils
{
    class Logger
    {
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public static void Config()
        {
            var config = new NLog.Config.LoggingConfiguration();

            // Targets where to log to: File and Console
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "file.txt" };
            var logconsole = new NLog.Targets.ConsoleTarget("logconsole");

            // Rules for mapping loggers to targets            
            config.AddRuleForAllLevels(logconsole);
            config.AddRuleForAllLevels(logfile);

            // Apply config           
            NLog.LogManager.Configuration = config;
        }

        public static void Debug(string msg)
        {
            _logger.Debug(msg);
        }

        public static void Debug(Exception ex, string msg)
        {
            _logger.Debug(ex, msg);
        }

        public static void Error(Exception ex, string msg)
        {
            _logger.Error(ex, msg);
        }

        public static void Warn(Exception ex, string msg)
        {
            _logger.Warn(ex, msg);
        }
    }
}

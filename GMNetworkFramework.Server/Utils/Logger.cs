﻿using NLog;
using System;

namespace GMNetworkFramework.Server.Utils
{
    public class Logger
    {
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public static void Config()
        {
            var config = new NLog.Config.LoggingConfiguration();

            // Targets where to log to: File and Console
            var logfile = new NLog.Targets.FileTarget("logfile")
            {
                FileName = "file.txt",
                Layout = "${longdate} ${uppercase:${level}} ${message} ${exception:format=Message,StackTrace,Data:maxInnerExceptionLevel=10}",
            };
            var logconsole = new NLog.Targets.ColoredConsoleTarget("logconsole")
            {
                Layout = "${longdate} ${uppercase:${level}} ${message} ${exception:format=Message,StackTrace,Data:maxInnerExceptionLevel=10}",
            };

            // Rules for mapping loggers to targets            
            config.AddRuleForAllLevels(logconsole);
            config.AddRuleForAllLevels(logfile);


            // Apply config           
            LogManager.Configuration = config;
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

        public static void Error(Exception ex)
        {
            _logger.Error(ex);
        }

        public static void Warn(Exception ex, string msg)
        {
            _logger.Warn(ex, msg);
        }

        public static void Warn(string msg)
        {
            _logger.Warn(msg);
        }
    }
}

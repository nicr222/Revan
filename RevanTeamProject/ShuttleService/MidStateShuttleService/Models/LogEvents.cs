using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace MidStateShuttleService.Models
{
    public static class LogEvents
    {
        public static void LogToFile(LogLevel level, string logMessage, IWebHostEnvironment env)
        {
            bool exists = Directory.Exists(Path.Combine(env.WebRootPath, "LogFolder"));
            if (!exists)
            {
                Directory.CreateDirectory(Path.Combine(env.WebRootPath, "LogFolder"));
            }

            string logPath = Path.Combine(env.WebRootPath, "LogFolder", $"{DateTime.Now:ddMMyyyy}.txt");

            using (StreamWriter swLog = File.AppendText(logPath))
            {
                swLog.WriteLine("Log Entry");
                swLog.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                swLog.WriteLine("Message Title : {0}", level);
                swLog.WriteLine("Message : {0}", logMessage);
                swLog.WriteLine("-------------------------------");
                swLog.WriteLine("");
            }
        }

        public static void LogSqlException(Exception ex, IWebHostEnvironment env)
        {
            LogLevel level = LogLevel.Error;
            string logMessage = "SQL Exception occurred: " + ex.Message;

            LogToFile(level, logMessage, env);
        }
    }
}

namespace MidStateShuttleService.Models
{
    public class LogEvents
    {
        public static void LogToFile(string title, string logMessage, IWebHostEnvironment env)
        {
            bool exists = Directory.Exists(env.WebRootPath + "\\" + "LogFolder");
            if (!exists)
            {
                Directory.CreateDirectory(env.WebRootPath + "\\" + "LogFolder");
            }
                
            StreamWriter swLog;
            string logPath = "";

            string FileName = DateTime.Now.ToString("ddMMyyyy") + ".txt";

            logPath = Path.Combine(env.WebRootPath + "\\" + "LogFolder", FileName);

            if (!File.Exists(logPath))
            {
                swLog = new StreamWriter(logPath);
            }
            else
            {
                swLog = File.AppendText(logPath);
            }

            swLog.WriteLine("Log Entry");
            swLog.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
            swLog.WriteLine("Message Title : {0}", title);
            swLog.WriteLine("Message : {0}", logMessage);
            swLog.WriteLine("-------------------------------");
            swLog.WriteLine("");


            swLog.Close();
        }
    }
}

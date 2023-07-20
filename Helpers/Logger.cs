using System;
using System.IO;

namespace AutoPixAiCreditClaimer.Helpers
{
    public class Logger
    {
        private string logFilePath;

        public Logger(string logFilePath)
        {
            this.logFilePath = logFilePath;
        }

        public void Log(string message)
        {
            try
            {
                string logDirectory = Path.GetDirectoryName(logFilePath);
                if (!Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }

                // Create or append to the log file
                using (StreamWriter sw = File.AppendText(logFilePath))
                {
                    string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}";
                    sw.WriteLine(logMessage);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur while writing to the log file
                Console.WriteLine("Error while writing to log file: " + ex.Message);
            }
        }
    }
}

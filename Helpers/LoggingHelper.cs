using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPixAiCreditClaimer.Helpers
{
    public class LoggingHelper
    {
        private string logFilePath;

        public LoggingHelper(string logFilePath)
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

                using (StreamWriter sw = File.AppendText(logFilePath))
                {
                    string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] - {message}";
                    sw.WriteLine(logMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while writing to log file: " + ex.Message);
            }
        }
    }
}

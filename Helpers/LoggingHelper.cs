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
            checkLogFileCount();
            deleteOldTxtLogs();
        }

        private void checkLogFileCount()
        {
            try
            {
                string logDirectory = Path.GetDirectoryName(logFilePath);
                if (Directory.Exists(logDirectory))
                {
                    var logFiles = Directory
                        .GetFiles(logDirectory, "*.log")
                        .OrderBy(File.GetCreationTime)
                        .ToList();

                    if (logFiles.Count > 30)
                    {
                        int filesToDelete = logFiles.Count - 30;
                        for (int i = 0; i < filesToDelete; i++)
                        {
                            File.Delete(logFiles[i]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while managing log files: " + ex.Message);
            }
        }

        private void deleteOldTxtLogs()
        {
            string logDirectory = Path.GetDirectoryName(logFilePath);
            if (Directory.Exists(logDirectory))
            {
                var logFiles = Directory.GetFiles(logDirectory, "*.txt").ToList();

                if (logFiles.Count > 0)
                {
                    for (int i = 0; i < logFiles.Count; i++)
                    {
                        File.Delete(logFiles[i]);
                    }
                }
            }
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

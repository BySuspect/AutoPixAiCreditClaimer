using System;
using System.IO;

namespace AutoPixAiCreditClaimer.Helpers
{
    // A simple logger class for writing log messages to a file
    public class Logger
    {
        private string logFilePath;

        // Constructor that takes the log file path as input
        public Logger(string logFilePath)
        {
            this.logFilePath = logFilePath;
        }

        // Method to log a message to the log file
        public void Log(string message)
        {
            try
            {
                // Get the directory path of the log file
                string logDirectory = Path.GetDirectoryName(logFilePath);
                // If the directory doesn't exist, create it
                if (!Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }

                // Create or append to the log file and write the log message with a timestamp
                using (StreamWriter sw = File.AppendText(logFilePath))
                {
                    string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] - {message}";
                    sw.WriteLine(logMessage);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur while writing to the log file
                // In this case, simply write the error message to the console
                Console.WriteLine("Error while writing to log file: " + ex.Message);
            }
        }
    }
}

using System;
using System.IO;

namespace AutoPixAiCreditClaimer.Helpers
{
    // A static class to define references used throughout the application
    public static class References
    {
#if DEBUG
    // Define the file path for the application files in DEBUG mode
    public static string AppFilesPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "PixaiCreditClaimerDebug");
#endif
#if !DEBUG
        // Define the file path for the application files in RELEASE mode
        public static string AppFilesPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "PixaiCreditClaimer");
#endif
    }

}

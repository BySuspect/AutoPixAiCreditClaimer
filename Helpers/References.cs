using System;
using System.IO;

namespace AutoPixAiCreditClaimer.Helpers
{
    public static class References
    {
#if DEBUG
        public static string AppFilesPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "PixaiCreditClaimerDebug");
#endif
#if !DEBUG
        public static string AppFilesPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "PixaiCreditClaimer");
#endif
    }
}

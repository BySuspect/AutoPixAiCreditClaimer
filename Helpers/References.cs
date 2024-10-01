using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPixAiCreditClaimer.Helpers
{
    public static class References
    {
        #region AppFilesPath
#if DEBUG
        public static string AppFilesPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "PixaiCreditClaimerDebug"
        );
#endif
#if !DEBUG
        public static string AppFilesPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "PixaiCreditClaimer"
        );
#endif
        #endregion
    }
}

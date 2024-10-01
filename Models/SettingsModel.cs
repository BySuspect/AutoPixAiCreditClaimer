using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPixAiCreditClaimer.Models
{
    public class SettingsModel
    {
        public bool runOnAppStartup { get; set; } = false; //(Default: false)
        public bool runOnWindowsStartup { get; set; } = false; //(Default: false)
        public bool showBrowserOnClaimProgress { get; set; } = false; //(Default: false)
        public bool AutoExitApp { get; set; } = false; //(Default: false)
    }
}

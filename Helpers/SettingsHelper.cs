using Newtonsoft.Json;
using System.IO;

namespace AutoPixAiCreditClaimer.Helpers
{
    public class SettingsHelper
    {
        // File path for JSON file, Default Value: "C:/Users/{USER}/Documents/PixaiCreditClaimer/accountlist.json"
        private static string filePath = Path.Combine(References.AppFilesPath, "settings.json");

        public static SettingsItems Settings
        {
            get
            {
                string json = ReadJsonFromFile();
                var settings = JsonConvert.DeserializeObject<SettingsItems>(json);
                return settings;
            }
            set
            {
                WriteJsonToFile(JsonConvert.SerializeObject(value, Formatting.Indented));
            }
        }

        private static void CheckFile()
        {
            if (!File.Exists(filePath))
            {
                SettingsItems items = new SettingsItems();
                File.WriteAllText(filePath, JsonConvert.SerializeObject(items, Formatting.Indented));
            }
        }

        public static void WriteJsonToFile(string jsonString)
        {
            CheckFile();
            File.WriteAllText(filePath, jsonString);
        }

        public static string ReadJsonFromFile()
        {
            CheckFile();
            return File.ReadAllText(filePath);
        }
    }
    public class SettingsItems
    {
        public bool runOnAppStartup { get; set; } = false;
        public bool runOnWindowsStartup { get; set; } = false;
    }
}

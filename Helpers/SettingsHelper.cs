using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AutoPixAiCreditClaimer.Helpers
{
    public class SettingsHelper
    {
        //(Default: "C:/Users/{USER}/Documents/PixaiCreditClaimer/settings.json")
        private static readonly string filePath = Path.Combine(
            References.AppFilesPath,
            "settings.json"
        );
        public static SettingsModel Settings
        {
            get
            {
                string json = ReadJsonFromFile();
                var settings = JsonConvert.DeserializeObject<SettingsModel>(json);
                return settings;
            }
            set { WriteJsonToFile(JsonConvert.SerializeObject(value, Formatting.Indented)); }
        }

        private static void CheckFile()
        {
            if (!File.Exists(filePath))
            {
                SettingsModel items = new SettingsModel();
                File.WriteAllText(
                    filePath,
                    JsonConvert.SerializeObject(items, Formatting.Indented)
                );
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
}

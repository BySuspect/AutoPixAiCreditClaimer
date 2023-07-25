using Newtonsoft.Json;
using System.IO;

namespace AutoPixAiCreditClaimer.Helpers
{
    // A helper class to manage application settings using JSON file operations
    public class SettingsHelper
    {
        // File path for the JSON file to store application settings, Default Value: "C:/Users/{USER}/Documents/PixaiCreditClaimer/settings.json"
        private static string filePath = Path.Combine(References.AppFilesPath, "settings.json");

        // Property to get or set the application settings as a SettingsItems object
        public static SettingsItems Settings
        {
            get
            {
                // Read the JSON data from the file and deserialize it into a SettingsItems object
                string json = ReadJsonFromFile();
                var settings = JsonConvert.DeserializeObject<SettingsItems>(json);
                return settings;
            }
            set
            {
                // Serialize the SettingsItems object to JSON format and write it back to the file
                WriteJsonToFile(JsonConvert.SerializeObject(value, Formatting.Indented));
            }
        }

        // Check if the file exists, if not, create a new JSON file with default settings
        private static void CheckFile()
        {
            // If the JSON file doesn't exist, create it and write default settings to it
            if (!File.Exists(filePath))
            {
                // Create a new SettingsItems object with default values
                SettingsItems items = new SettingsItems();
                // Serialize the object to JSON format and write it to the file
                File.WriteAllText(filePath, JsonConvert.SerializeObject(items, Formatting.Indented));
            }
        }

        // Write the provided JSON string to the file
        public static void WriteJsonToFile(string jsonString)
        {
            // Ensure that the file exists, and then write the JSON string to it
            CheckFile();
            File.WriteAllText(filePath, jsonString);
        }

        // Read the JSON string from the file
        public static string ReadJsonFromFile()
        {
            // Ensure that the file exists, and then read the JSON content from it
            CheckFile();
            return File.ReadAllText(filePath);
        }
    }

    // A class representing application settings with properties for different configuration options
    public class SettingsItems
    {
        // Whether to run the application on app startup (Default: false)
        public bool runOnAppStartup { get; set; } = false;

        // Whether to run the application on Windows startup (Default: false)
        public bool runOnWindowsStartup { get; set; } = false;

        // Whether to show the browser during the credit claim progress (Default: true)
        public bool showBrowserOnClaimProgress { get; set; } = true;
    }

}

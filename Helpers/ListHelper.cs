using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AutoPixAiCreditClaimer.Helpers
{
    // A helper class to manage the user list using JSON file operations
    public class ListHelper
    {
        // File path for the JSON file to store user data, Default Value: "C:/Users/{USER}/Documents/PixaiCreditClaimer/accountlist.json"
        private static string filePath = Path.Combine(References.AppFilesPath, "accountlist.json");

        // Property to get or set the UserList as a list of UserItems
        public static List<UserItems> UserList
        {
            get
            {
                // Read the JSON data from the file and deserialize it into a list of UserItems
                string json = ReadJsonFromFile();
                var list = JsonConvert.DeserializeObject<List<UserItems>>(json);
                // Order the user list by the 'id' property in ascending order
                list = list.OrderBy(x => x.id).ToList();
                return list;
            }
            set
            {
                // Serialize the list of UserItems to JSON format and write it back to the file
                WriteJsonToFile(JsonConvert.SerializeObject(value, Formatting.Indented));
            }
        }

        // Check if the file exists, if not, create a new JSON file with an empty array
        private static void CheckFile()
        {
            // Get the directory path of the JSON file
            string logDirectory = Path.GetDirectoryName(filePath);
            // If the directory doesn't exist, create it
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            // If the JSON file doesn't exist, create it and write an empty JSON array
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "[]");
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

    // A class representing user items with properties for id, name, email, and password
    public class UserItems
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string pass { get; set; }
    }

}

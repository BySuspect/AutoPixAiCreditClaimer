using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AutoPixAiCreditClaimer.Helpers
{
    public class ListHelper
    {
        // File path for JSON file, Default Value: "./accountlist.json"
        private static string filePath = "./accountlist.json";

        // Property to get or set the UserList
        public static List<UserItems> UserList
        {
            get
            {
                string json = ReadJsonFromFile();
                var list = JsonConvert.DeserializeObject<List<UserItems>>(json);
                list = list.OrderBy(x => x.id).ToList();
                return list;
            }
            set
            {
                WriteJsonToFile(JsonConvert.SerializeObject(value));
            }
        }

        // Check if the file exists, if not, create a new JSON file
        private static void CheckFile()
        {
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "[]");
            }
        }

        // Write the JSON string to the file
        public static void WriteJsonToFile(string jsonString)
        {
            CheckFile();
            File.WriteAllText(filePath, jsonString);
        }

        // Read the JSON string from the file
        public static string ReadJsonFromFile()
        {
            CheckFile();
            return File.ReadAllText(filePath);
        }
    }

    public class UserItems
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string pass { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AutoPixAiCreditClaimer.Helpers
{
    public class ListHelper
    {
        // (Default: "C:/Users/{USER}/Documents/PixaiCreditClaimer/accountlist.json")
        private static string filePath = Path.Combine(References.AppFilesPath, "accountlist.json");

        public static List<UserModel> UserList
        {
            get
            {
                string json = ReadJsonFromFile();
                var list = JsonConvert.DeserializeObject<List<UserModel>>(json);
                list = list.OrderBy(x => x.id).ToList();
                return list;
            }
            set { WriteJsonToFile(JsonConvert.SerializeObject(value, Formatting.Indented)); }
        }

        private static void CheckFile()
        {
            string logDirectory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "[]");
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

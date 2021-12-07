using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AccessControlProject
{
    public static class AuthorizationUsers
    {
        public static string User { get; set; }

        public static bool Authorization(string login, string pass)
        {
            using (StreamReader r = new StreamReader(Directory.GetCurrentDirectory() + @"\\Users.json"))
            {
                string json = r.ReadToEnd();


                var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                if (dict.TryGetValue(login, out var password))
                {
                    if (password == pass)
                    {
                        User = login;
                        return true;
                    }
                }
            }
            return false;
        }
        public static bool AddUser(string login, string pass)
        {
            Dictionary<string, string> dict = null;
            using (StreamReader r = new StreamReader(Directory.GetCurrentDirectory() + @"\\Users.json"))
            {
                string json = r.ReadToEnd();
                dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                dict.Add(login, pass);
            }
            AccessMatrix.AddUser(login);
            var result = JsonConvert.SerializeObject(dict);
            File.WriteAllText(Directory.GetCurrentDirectory() + @"\\Users.json", result);
            return false;

        }

        public static IEnumerable<string> GetUsers()
        {
            Dictionary<string, string> dict = null;
            using (StreamReader r = new StreamReader(Directory.GetCurrentDirectory() + @"\\Users.json"))
            {
                string json = r.ReadToEnd();
                dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            }
            return dict.Select(x => x.Key);
        }
    }
}

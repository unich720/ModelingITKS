using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AccessControlProject
{
    public static class FileSystem
    {
        private static string path => Directory.GetCurrentDirectory() + @"\\Control folder";
        public static List<FileInt> Files;

        public static Dictionary<string, string> Owners => GetOwnerFile();

        static FileSystem()
        {
            Files = GetFilesInt();
        }


        public static void CreateFile(string name)
        {
            var st = path + @"\\" + name;
            //File.Create(st);
            AccessMatrix.CreateFile(name);
            AddOwner(name);

        }

        public static void CreateFileObject(string name, string typeName, List<string> typePerents)
        {
            //var st = path + @"\\" + name;
            //File.Create(st);
            if (!Files.Any(x => x.Name == name))
            {
                AccessMatrix.CreateFile(name);
                AddOwner(name);
                TypeProcessor.AddType(typeName, typePerents);
                Files.Add(new FileInt(name, AuthorizationUsers.User, TypeProcessor.GetType(typeName)));
                Console.WriteLine("Файл {0} успешно создан", name);
            }
            else
            {
                Console.WriteLine("Файл {0} уже создан", name);
            }

        }

        public static void AddOwner(string name)
        {
            Dictionary<string, string> dict = null;
            using (StreamReader r = new StreamReader(Directory.GetCurrentDirectory() + @"\\FileOwner.json"))
            {
                string json = r.ReadToEnd();
                dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                dict.Add(name, AuthorizationUsers.User);
            }
            var result = JsonConvert.SerializeObject(dict);
            File.WriteAllText(Directory.GetCurrentDirectory() + @"\\FileOwner.json", result);
        }

        public static bool DeleteFile(string name)
        {
            if (CheckOwner(name))
            {
                var st = path + @"\\" + name;
                //File.Delete(st);
                AccessMatrix.DeleteFile(name);
                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool CheckOwner(string name)
        {
            //Dictionary<string, string> dict = null;
            //using (StreamReader r = new StreamReader(Directory.GetCurrentDirectory() + @"\\FileOwner.json"))
            //{
            //    string json = r.ReadToEnd();
            //    dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            //}
            var file = Files.FirstOrDefault(x => x.Name == name);
            if (file != default && file.Owner == AuthorizationUsers.User)
            {
                return true;
            }
            return false;
        }

        public static void GetFiles()
        {
            Dictionary<string, string> dict = null;
            using (StreamReader r = new StreamReader(Directory.GetCurrentDirectory() + @"\\FileOwner.json"))
            {
                string json = r.ReadToEnd();
                dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            }
            foreach (var item in dict)
            {
                //var split = path + @"\\";
                //var file = item.Split(path);
                Console.WriteLine(item.Key.ToString());
            }
        }

        public static List<FileInt> GetFilesInt()
        {
            Dictionary<string, string> dict = null;
            using (StreamReader r = new StreamReader(Directory.GetCurrentDirectory() + @"\\FileOwner.json"))
            {
                string json = r.ReadToEnd();
                dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            }

            //var files = Directory.GetFiles(path);
            List<FileInt> list = new List<FileInt>();
            foreach (var item in dict)
            {
                //var split = path + @"\\";
                //var file = item.Key.Split(path);
                list.Add(new FileInt(item.Key, item.Value));
            }
            return list;
        }

        public static Dictionary<string, string> GetOwnerFile()
        {
            //Dictionary<string, string> dict = null;
            //using (StreamReader r = new StreamReader(Directory.GetCurrentDirectory() + @"\\FileOwner.json"))
            //{
            //    string json = r.ReadToEnd();
            //    dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            //}

            return Files.ToDictionary(x => x.Name, y => y.Owner);
        }

        public static void OpenFile(string name)
        {
            if (AccessMatrix._acces.TryGetValue(AuthorizationUsers.User, out var file))
            {
                if (file.TryGetValue(name, out var ac))
                {
                    if (ac.Contains("e"))
                    {
                        Console.WriteLine("Вы открыли файл ", name);
                    }
                    else
                    {
                        Console.WriteLine("У вас нет доступа");
                    }
                }
            }
        }
        public static void ReadFile(string name)
        {
            if (AccessMatrix._acces.TryGetValue(AuthorizationUsers.User, out var file))
            {
                if (file.TryGetValue(name, out var ac))
                {
                    if (ac.Contains("r"))
                    {
                        Console.WriteLine("Вы открыли файл ", name);
                    }
                    else
                    {
                        Console.WriteLine("У вас нет доступа");
                    }
                }
            }
        }
        public static void WriteFile(string name)
        {
            if (AccessMatrix._acces.TryGetValue(AuthorizationUsers.User, out var file))
            {
                if (file.TryGetValue(name, out var ac))
                {
                    if (ac.Contains("w"))
                    {
                        Console.WriteLine("Вы открыли файл ", name);
                    }
                    else
                    {
                        Console.WriteLine("У вас нет доступа");
                    }
                }
            }
        }
    }
}

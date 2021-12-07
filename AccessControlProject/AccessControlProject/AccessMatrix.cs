using System;
using System.Collections.Generic;

namespace AccessControlProject
{
    public class AccessMatrix
    {
        private const string def = "r,w";

        public static Dictionary<string, Dictionary<string, string>> _acces;
        public AccessMatrix()
        {
            Dictionary<string, Dictionary<string, string>> acces = new Dictionary<string, Dictionary<string, string>>();
            var owners = FileSystem.Owners;
            foreach (var user in AuthorizationUsers.GetUsers())
            {
                Dictionary<string, string> keys = new Dictionary<string, string>();
                foreach (var item2 in FileSystem.Files)
                {
                    var asd = owners.GetValueOrDefault(item2.Name);
                    if (asd != default && user==asd)
                    {
                        keys.Add(item2.Name, def+",e");
                        continue;
                    }
                    keys.Add(item2.Name, def);
                }
                acces.Add(user, keys);
            }
            _acces = acces;
        }

        public static void AddUser(string user)
        {
            Dictionary<string, string> keys = new Dictionary<string, string>();
            foreach (var item2 in FileSystem.Files)
            {
                keys.Add(item2.Name, def);
            }
            _acces.Add(user, keys);
        }

        public static void CreateFile(string name)
        {
            foreach (var item in _acces)
            {
                if (item.Key==AuthorizationUsers.User)
                {
                    item.Value.Add(name, def+",e");
                    continue;
                }
                item.Value.Add(name, def);
            }

        }

        public static void DeleteFile(string name)
        {
            foreach (var item in _acces)
            {
                item.Value.Remove(name);
            }
        }

        public static void AcFile(string log, string name, string ac)
        {
            if (_acces.ContainsKey(log))
            {
                if (_acces[log].ContainsKey(name))
                {
                    _acces[log].Remove(name);
                    _acces[log].TryAdd(name, ac);
                }
            }
        }

        public static void AcFiles()
        {
            foreach (var item in _acces)
            {
                Console.Write("\t");
                foreach (var item2 in item.Value)
                {
                    Console.Write(item2.Key + "\t");
                }
                Console.WriteLine();
                break;
            }
            foreach (var item in _acces)
            {
                Console.Write(item.Key + "\t");
                foreach (var item2 in item.Value)
                {
                    Console.Write(item2.Value + "\t\t");
                }
                Console.WriteLine();
            }
        }
    }
}

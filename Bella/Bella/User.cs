using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bella
{
    public class User
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public int Id { get; set; }

        public AccessLevel AccessLevel { get; set; }

        public User(string name, string password, int id, AccessLevel accessLevel)
        {
            Name = name;
            Password = password;
            Id = id;
            AccessLevel = accessLevel;
        }
        public override string ToString()
        {
            return String.Format($"{Name}, AccessLevel: {AccessLevel}");
        }
    }

    public class FileInternal
    {
        public string Name { get; private set; }

        public string Owner { get; private set; }

        public AccessLevel AccessLevel { get; set; }

        public FileInternal(string name, string owner, AccessLevel access)
        {
            Name = name;
            Owner = owner;
            AccessLevel = access;
        }

        public override string ToString()
        {
            return String.Format($"{Name}, AccessLevel: {AccessLevel}");
        }
    }

    public enum AccessLevel : byte
    {
        One = 1,//low
        Two = 2,//hight
        Three = 3,
    }
}

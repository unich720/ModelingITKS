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
        
        public Roles Role { get; set; }

        public User(string name, string password, int id, AccessLevel accessLevel, Roles role)
        {
            Name = name;
            Password = password;
            Id = id;
            AccessLevel = accessLevel;
            Role = role;
        }
        public override string ToString()
        {
            return String.Format($"{Name}, AccessLevel: {AccessLevel}, Role: {Role}");
        }
    }

    public class FileInternal
    {
        public string Name { get; private set; }

        public string Owner { get; private set; }

        public DirectoryInternal Directory { get; set; }

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
    public class DirectoryInternal
    {
        public string Name { get; private set; }

        public string Owner { get; private set; }

        public List<FileInternal> Files { get; private set; }

        public List<DirectoryInternal> Directories { get; private set; }

        public AccessLevel AccessLevel { get; set; }

        public DirectoryInternal(string name, string owner, AccessLevel access)
        {
            Name = name;
            Owner = owner;
            AccessLevel = access;
            Files = new List<FileInternal>();
            Directories = new List<DirectoryInternal>();
        }

        public override string ToString()
        {
            return String.Format($"{Name}, AccessLevel: {AccessLevel}");
        }
    }


    public enum AccessLevel : byte
    {
        One = 1,//low
        Two = 2,//high
        Three = 3,//very high
    }

    public enum Roles : byte
    {
        SystemAdmin = 1,//low
        UserAdmin = 2,//high
        User = 3,//very high
    }
}

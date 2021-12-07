using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControlProject
{
    public class FileInt
    {
        public string Name { get; private set; }

        public Type Type { get; set; }

        public string Owner { get; private set; }

        public FileInt(string name, string owner)
        {
            Name = name;
            Owner = owner;
        }

        public FileInt(string name, string owner, Type type)
        {
            Name = name;
            Owner = owner;
            Type = type;
        }
    }
}

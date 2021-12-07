using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControlProject
{
    public static class TypeProcessor
    {
        private static Dictionary<string, Type> Types;

        static TypeProcessor()
        {
            Types = new();
        }

        public static void AddType(string name)
        {
            Types.Add(name, new Type());
        }

        public static void AddType(string name, List<string> perents)
        {
            Types.Add(name, new Type(name, perents));
        }

        public static void GetTypes()
        {
            foreach (var item in Types)
            {
                Console.WriteLine(item.Value.ToString());
            }
            //return Types.Keys.ToList();
        }

        public static Type GetType(string name)
        {
            _ = Types.TryGetValue(name, out var value);
            return value;
        }
    }

    public class Type
    {
        public string Name { get; set; }
        //public List<Type> ChildTypes { get; set; }
        public List<string> PerentType;
        public Type(string name, List<string> perentTypes)
        {
            Name = name;
            if (perentTypes != null)
            {
                PerentType = new();
                PerentType.AddRange(perentTypes);
            }
        }

        public Type()
        {

        }

        public override string ToString()
        {
            string st = $"Type name {Name}, perent types => ";
            if (PerentType!=null)
            {
                foreach (var item in PerentType)
                {
                    st += item + ",";
                }
            }
            else
            {
                st += "null";
            }
           
            return st;
        }
    }
}

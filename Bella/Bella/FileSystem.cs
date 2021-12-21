using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bella
{
    public class FileSystem
    {
        public List<FileInternal> Files { get; set; }

        public FileSystem()
        {
            Files = new List<FileInternal>();
        }

        public void CreateFile(string name, AccessLevel accessLevel)
        {
            Files.Add(new FileInternal(name, AuthorizationUsers.AUser.Name, accessLevel));
        }

        public void ReadFile(string name, AccessLevel accessLevel)
        {
            var file = Files.FirstOrDefault(f => f.Name == name);
            if (file != default)
            {
                if (file.AccessLevel <= accessLevel)
                {
                    Console.WriteLine("Вы прочитали файл");
                }
                else
                {
                    Console.WriteLine("Уровень доступа файла выше вашего");
                }
            }
            else
            {
                Console.WriteLine("Файл не найден");
            }
        }

        public void WriteFile(string name, AccessLevel accessLevel)
        {
            var file = Files.FirstOrDefault(f => f.Name == name);
            if (file != default)
            {
                if (file.AccessLevel == accessLevel)
                {
                    Console.WriteLine("Вы прочитали файл");
                }
                if (file.AccessLevel < accessLevel)
                {
                    Console.WriteLine("Вы прочитали файл");
                    Console.WriteLine("Уровень доступа файла увеличин {0} => {1}", file.AccessLevel, accessLevel);
                    file.AccessLevel = accessLevel;
                }
                if (file.AccessLevel > accessLevel)
                {
                    Console.WriteLine("Вы все удалили из файла");
                    Console.WriteLine("Уровень доступа файла понижен {0} => {1}", file.AccessLevel, accessLevel);
                    file.AccessLevel = accessLevel;
                }
            }
            else
            {
                Console.WriteLine("Файл не найден");
            }
        }

        public void GetFiles()
        {
            foreach (var file in Files)
            {
                Console.WriteLine(file.ToString());
            }
        }
    }
}

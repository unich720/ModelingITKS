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

        public List<DirectoryInternal> Directories { get; set; }

        public Dictionary<string,(string login, string file)> Links { get; set; }

        public FileSystem()
        {
            Files = new List<FileInternal>();
            Directories = new List<DirectoryInternal>();
            Links = new Dictionary<string,(string login,string file)>();
        }

        public void CreateFile(string name, AccessLevel accessLevel)
        {
            Files.Add(new FileInternal(name, AuthorizationUsers.AUser.Name, accessLevel));
        }

        public void CreateDir(string name, AccessLevel accessLevel)
        {
            Directories.Add(new DirectoryInternal(name, AuthorizationUsers.AUser.Name, accessLevel));
        }

        public void MoveFileToDir(string fileName, string dirName, AccessLevel accessLevel)
        {
            var file = Files.FirstOrDefault(f => f.Name == fileName);
            var dir = Directories.FirstOrDefault(f => f.Name == dirName);
            if (file != default && dir != default)
            {
                if (file.AccessLevel <= accessLevel && dir.AccessLevel <= accessLevel)
                {
                    if (file.AccessLevel >= dir.AccessLevel)
                    {
                        dir.Files.Add(file);
                        file.Directory = dir;
                    }
                    else
                    {
                        Console.WriteLine("Доступ у каталога меньше");
                    }
                }
                else
                {
                    Console.WriteLine("У вас доступ меньше чем у файла или каталога");
                }
                
            }
            else
            {
                Console.WriteLine("Файл или каталог не найден");
            }
        }

        public void MoveDirToDir(string dir1, string dirName, AccessLevel accessLevel)
        {
            var dir = Directories.FirstOrDefault(f => f.Name == dir1);
            var dirIn = Directories.FirstOrDefault(f => f.Name == dirName);
            if (dir != default && dirIn != default)
            {
                if (dir.AccessLevel <= accessLevel && dirIn.AccessLevel <= accessLevel)
                {
                    if (dir.AccessLevel >= dirIn.AccessLevel)
                    {
                        dirIn.Directories.Add(dir);
                        if (dir.AccessLevel > dirIn.AccessLevel)
                        {
                            dirIn.AccessLevel = dir.AccessLevel;
                        }
                        Console.WriteLine($"Папка {dir.Name} добавлена в папку {dirIn.Name}");
                    }
                    else
                    {
                        Console.WriteLine("Доступ у каталога меньше");
                    }
                }
                else
                {
                    Console.WriteLine("У вас доступ меньше чем у файла или каталога");
                }

            }
            else
            {
                Console.WriteLine("Каталог не найден");
            }
        }

        public void ReadDir(string name, AccessLevel accessLevel)
        {
            var directory = Directories.FirstOrDefault(f => f.Name == name);
            if (directory != default)
            {
                if (directory.AccessLevel <= accessLevel)
                {
                    Console.WriteLine("Вы открыли католог");
                    Console.WriteLine("Файлы:");
                    foreach (var item in directory.Files)
                    {
                        Console.WriteLine(item);
                    }
                    Console.WriteLine("Папки:");
                    foreach (var item in directory.Directories)
                    {
                        Console.WriteLine(item);
                    }
                }
                else
                {
                    Console.WriteLine("Уровень доступа каталога выше вашего");
                }
            }
            else
            {
                Console.WriteLine("Файл не найден");
            }
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

        public void CreateLink(string login, string file)
        {
            var link = Guid.NewGuid().ToString();
            Console.WriteLine(@"Ваша ссылка: {0}", link);
            Links.Add(link,(login,file));
        }

        public void OpenLink(string link)
        {
            var valueLink = Links.GetValueOrDefault(link);
            if (valueLink != default)
            {
                if (valueLink.login== AuthorizationUsers.AUser.Name)
                {
                    ReadFile(valueLink.file, AuthorizationUsers.AUser.AccessLevel);
                }
                else
                {
                    Console.WriteLine("Ссылка не для вас");
                }
            }
        }

        public void GetFiles()
        {
            Console.WriteLine("Файлы");
            foreach (var file in Files)
            {
                Console.WriteLine(file.ToString());
            }
        }

        public void GetDir()
        {
            Console.WriteLine("Каталоги");
            foreach (var file in Directories)
            {
                Console.WriteLine(file.ToString());
            }
        }
    }
}

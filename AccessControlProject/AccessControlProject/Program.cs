using System;
using System.Collections.Generic;

namespace AccessControlProject
{
    class Program
    {
        private const string _admin = "admin";
        static void Main(string[] args)
        {
            Console.WriteLine("Войдите в систему");
            bool result = false;
            var accesMatrix = new AccessMatrix();
            while (!result)
            {
                Console.WriteLine("Введите логин");
                var login = Console.ReadLine();
                Console.WriteLine("Введите пароль");
                var pass = Console.ReadLine();
                result = AuthorizationUsers.Authorization(login, pass);
                if (!result)
                {
                    Console.WriteLine("Неправильный логин или пароль!");
                }
                else
                {
                    Console.WriteLine("Успешный вход в систему. Пользователь {0}", login);
                }
            }
            while (true)
            {

                Console.WriteLine("Введите команду. /help список команд");
                var command = Console.ReadLine();
                var commandSplit = command.Split(" ");
                command = commandSplit[0] ?? default;
                switch (command)
                {
                    case "/createObject":
                        {
                            Console.WriteLine("Введите типы. Если нет то ");
                            var types = Console.ReadLine();
                            List<string> typesList;
                            if (String.IsNullOrWhiteSpace(types))
                            {
                                typesList = null;
                            }
                            else
                            {
                                typesList = new List<string>(types.Split(" "));
                            }
                            //var typesSplit = command.Split(" ");
                            FileSystem.CreateFileObject(commandSplit[1], commandSplit[2], typesList);
                            //Console.WriteLine("Файл {0} успешно создан", commandSplit[1]);
                            break;
                        }
                    case "/graphTypes":
                        {
                            TypeProcessor.GetTypes();
                            break;
                        }
                    case "/createSubject":
                        {
                            Console.WriteLine("Введите типы. Если нет то ");
                            var types = Console.ReadLine();
                            List<string> typesList;
                            if (String.IsNullOrWhiteSpace(types))
                            {
                                typesList = null;
                            }
                            else
                            {
                                typesList = new List<string>(types.Split(" "));
                            }
                            //var typesSplit = command.Split(" ");
                            FileSystem.CreateFileObject(commandSplit[1], commandSplit[2], typesList);
                            //Console.WriteLine("Файл {0} успешно создан", commandSplit[1]);
                            break;
                        }
                    case "/createfile":
                        {
                            FileSystem.CreateFile(commandSplit[1]);
                            Console.WriteLine("Файл {0} успешно создан", commandSplit[1]);
                            break;
                        }
                    case "/openfile":
                        {
                            FileSystem.OpenFile(commandSplit[1]);
                            Console.WriteLine("Файл {0} успешно создан", commandSplit[1]);
                            break;
                        }
                    case "/readfile":
                        {
                            FileSystem.ReadFile(commandSplit[1]);
                            Console.WriteLine("Файл {0} успешно создан", commandSplit[1]);
                            break;
                        }
                    case "/writefile":
                        {
                            FileSystem.WriteFile(commandSplit[1]);
                            Console.WriteLine("Файл {0} успешно создан", commandSplit[1]);
                            break;
                        }
                    case "/acfiles":
                        {
                            AccessMatrix.AcFiles();
                            break;
                        }
                    case "/acfile":
                        {
                            if (AuthorizationUsers.User == _admin)
                            {
                                AccessMatrix.AcFile(commandSplit[1], commandSplit[2], commandSplit[3]);
                            }
                            else
                            {
                                Console.WriteLine("Вы не админ");
                            }
                            break;
                        }
                    case "/deletefile":
                        {
                            if (FileSystem.DeleteFile(commandSplit[1]))
                            {
                                
                                Console.WriteLine("Файл {0} успешно удален", commandSplit[1]);
                            }
                            else
                            {
                                Console.WriteLine("У вас нет права удалять этот файл");
                            }
                            
                            break;
                        }
                    case "/getfiles":
                        {
                            FileSystem.GetFiles();
                            break;
                        }
                    case "/adduser":
                        {
                            if (AuthorizationUsers.User == _admin)
                            {
                                AuthorizationUsers.AddUser(commandSplit[1], commandSplit[2]);
                                Console.WriteLine("Пользователь {0} успешно создан в системе", commandSplit[1]);
                            }
                            else
                            {
                                Console.WriteLine("Вы не админ");
                            }
                            break;
                        }
                    case "/logout":
                        {
                            Console.WriteLine("Пользователь {0} вышел из системы", AuthorizationUsers.User);
                            AuthorizationUsers.User = default;
                            Console.WriteLine("Войдите в систему");
                            bool result2 = false;
                            while (!result2)
                            {
                                Console.WriteLine("Введите логин");
                                var login = Console.ReadLine();
                                Console.WriteLine("Введите пароль");
                                var pass = Console.ReadLine();
                                result2 = AuthorizationUsers.Authorization(login, pass);
                                if (!result2)
                                {
                                    Console.WriteLine("Неправильный логин или пароль!");
                                }
                                else
                                {
                                    Console.WriteLine("Успешный вход в систему. Пользователь {0}", login);
                                }
                            }
                            break;
                        }
                    case "/help":
                        {
                            Console.WriteLine("/adduser");
                            break;
                        }
                    case "/q":
                        {
                            Environment.Exit(default);
                            break;
                        }
                    default: 
                        break;
                }
            }

            //Console.ReadKey();
        }
    }
}

using System;
using System.Linq;

namespace Bella
{
    internal class Program
    {
        private const string _admin = "admin";
        static void Main(string[] args)
        {
            Console.WriteLine("Войдите в систему");
            AuthorizationUsers authorizationUsers = new AuthorizationUsers();
            FileSystem fileSystem = new FileSystem();
            authorizationUsers.AddUser("admin", "admin", AccessLevel.One);
            AuthorizationUsers.AUser = authorizationUsers.Users.Single();

            Test();

            User result = default;
            while (result == default)
            {
                Console.WriteLine("Введите логин");
                var login = Console.ReadLine();
                Console.WriteLine("Введите пароль");
                var pass = Console.ReadLine();
                result = authorizationUsers.Authorization(login, pass);
                if (result == default)
                {
                    Console.WriteLine("Неправильный логин или пароль!");
                }
                else
                {
                    Console.WriteLine("Успешный вход в систему. Пользователь {0}", login);
                    AuthorizationUsers.AUser = result;
                }
            }
            

            void Test()
            {
                fileSystem.CreateFile("test1", AccessLevel.One);
                fileSystem.CreateFile("test2", AccessLevel.Two);
                fileSystem.CreateFile("test3", AccessLevel.Three);
                authorizationUsers.AddUser("user1", "user1", AccessLevel.One);
                authorizationUsers.AddUser("user2", "user2", AccessLevel.Two);
                authorizationUsers.AddUser("user3", "user3", AccessLevel.Three);
            }

            while (true)
            {

                Console.WriteLine("Введите команду. /help список команд");
                var command = Console.ReadLine();
                var commandSplit = command.Split(" ");
                command = commandSplit[0] ?? default;

                switch (command)
                {
                    case "/createfile":
                        {
                            var ac = (AccessLevel)Convert.ToInt16(commandSplit[2]);
                            if (AuthorizationUsers.AUser.AccessLevel >= ac || AuthorizationUsers.AUser.Name == _admin)
                            {
                                fileSystem.CreateFile(commandSplit[1], ac);
                            }
                            else
                            {
                                Console.WriteLine("Доступ меньше");
                            }
                            
                            break;
                        }
                    case "/readfile":
                        {
                            fileSystem.ReadFile(commandSplit[1], AuthorizationUsers.AUser.AccessLevel);
                            break;
                        }
                    case "/writefile":
                        {
                            fileSystem.WriteFile(commandSplit[1], AuthorizationUsers.AUser.AccessLevel);
                            break;
                        }
                    case "/getfiles":
                        {
                            fileSystem.GetFiles();
                            break;
                        }
                    case "/users":
                        {
                            authorizationUsers.GetUsers();
                            break;
                        }
                    case "/adduser":
                        {
                            var ac = (AccessLevel)Convert.ToInt16(commandSplit[3]);
                            if (AuthorizationUsers.AUser.AccessLevel >= ac || AuthorizationUsers.AUser.Name == _admin)
                            {
                                authorizationUsers.AddUser(commandSplit[1], commandSplit[2], ac);
                                Console.WriteLine("Пользователь {0} успешно создан в системе", commandSplit[1]);
                            }
                            else
                            {
                                Console.WriteLine("У вас не хватает уровня доступа");
                            }
                            break;
                        }
                    case "/logout":
                        {
                            Console.WriteLine("Пользователь {0} вышел из системы", AuthorizationUsers.AUser);
                            AuthorizationUsers.AUser = default;
                            Console.WriteLine("Войдите в систему");
                            User result2 = default;
                            while (result2==default)
                            {
                                Console.WriteLine("Введите логин");
                                var login = Console.ReadLine();
                                Console.WriteLine("Введите пароль");
                                var pass = Console.ReadLine();
                                result2 = authorizationUsers.Authorization(login, pass);
                                if (result2==default)
                                {
                                    Console.WriteLine("Неправильный логин или пароль!");
                                }
                                else
                                {
                                    Console.WriteLine("Успешный вход в систему. Пользователь {0}", login);
                                    AuthorizationUsers.AUser = result2;
                                }
                            }
                            break;
                        }
                    case "/createObject":
                        {

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
        }
    }
}

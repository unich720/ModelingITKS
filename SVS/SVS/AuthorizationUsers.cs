using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bella
{
    public class AuthorizationUsers
    {
        public static User AUser { get; set; }

        public List<User> Users { get; set; }

        public AuthorizationUsers()
        {
            Users = new List<User>();
        }

        public User Authorization(string login, string pass)
        {
            return Users.FirstOrDefault(x => x.Name == login && x.Password == pass);
        }

        public void AddUser(string login, string pass, AccessLevel accessLevel, Roles role)
        {
            Users.Add(new User(login, pass, Users.Count + 1, accessLevel, role));
        }

        public void ChangeRole(string login, Roles role)
        {
            var user = Users.FirstOrDefault(x => x.Name == login);
            user.Role = role;
        }

        public void GetUsers()
        {
            foreach (var user in Users)
            {
                Console.WriteLine(user.ToString());
            }
        }
    }
}

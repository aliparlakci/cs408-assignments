using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs408_hw1_server
{
    public class CayGetirProtocol
    {
        public static string Message(string message)
        {
            return $"Cay Getir 1.0\ntype=message\nmessage={message}";
        }

        public static string Signup(string name, string surname, string username, string password)
        {
            return $"Cay Getir 1.0\ntype=signup\nname={name}\nsurname={surname}\nusername={username}\npassword={password}";
        }

        public static User ParseUser(string message)
        {
            var user = new User();
            var lines = message.Split(new char[] { '\n' });

            user.Name = lines[2].Substring(5);
            user.Surname = lines[3].Substring(8);
            user.Username = lines[4].Substring(9);
            user.Password = lines[5].Substring(9);

            return user;
        }
    }
}

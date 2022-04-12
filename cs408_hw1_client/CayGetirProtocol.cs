using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs408_hw1_client
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

        public static string ParseMessage(string message)
        {
            var lines = message.Split(new char[] { '\n' });
            var type = lines[1].Substring(5);

            if (type == "message")
            {
                return lines[2].Substring("message".Length + 1);
            }
            else
            {
                return "";
            }
        }
    }
}

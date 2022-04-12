using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs408_hw1_server
{
    public class Database
    {
        private readonly string filename = "database.txt";
        private User[] users = { };

        public Database()
        {
            if (File.Exists(filename))
            {

            }
        }

        public bool Exists(string username)
        {
            return true;
        }

        public void InsertUser(User user)
        {

        }

        private void ReadFile()
        {
            
        }
    }
}

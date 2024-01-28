using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task1.BL
{
    internal class SignUp
    {
        public SignUp(string name, string pwd, string userRole)
        {
            username = name;
            password = pwd;
            role = userRole;
        }
        public string username;
        public string password;
        public string role;
    }
}

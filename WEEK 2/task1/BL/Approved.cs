using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task1.BL
{
    internal class Approved
    {
        public Approved(string username, string password, string role, string status)
        {
            this.username = username;
            this.password = password;
            this.role = role;
            this.status = status;

        }
        public string status;
        public string role;
        public string password;
        public string username;
    }
}

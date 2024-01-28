using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task1.BL
{
    internal class Customer
    {
        public Customer(string name, string password) 
        {
            this.name = name;
            this.password = password;
        }
        public string name;
        public string password;
    }
}

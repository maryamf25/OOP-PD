using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BL
{
   
    internal class Teacher
    {
        public Teacher(string username, string pwd, int ID)
        {
            name = username;
                password = pwd;
            id = ID;
        }
        public string name;
        public string password;
        public int id;
    }
}

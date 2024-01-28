using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BL
{

    internal class Courses
    {
        public Courses(string courseName, int ID)
        {
            name = courseName;
           
            id = ID;
        }
        public string name;
        public int id;
    }
}

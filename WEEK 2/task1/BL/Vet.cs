using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task1.BL
{
    internal class Vet
    {
        public Vet(string name, string password)
        {
            this.name = name;
            this.password = password;
            
        }
        public Vet(string name, string password, string service, string email, string day, string contact, int pendingAppointments,int bookedAppointments, int totalAppointments)
        {
            this.name = name;
            this.password = password;
            this.service = service;
            this.email = email; 
            this.day = day;
            this.contact = contact;
            this.pendingAppointments = pendingAppointments;
            this.bookedAppointments = bookedAppointments;
            this.totalAppointments = totalAppointments;
        }

        public string name;
        public string password;
        public string service;
        public string email;
        public string day;
        public string contact;
        public int pendingAppointments;
        public int bookedAppointments;
        public int totalAppointments;
    }
}

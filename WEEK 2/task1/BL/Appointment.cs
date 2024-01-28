using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task1.BL
{
    internal class Appointment
    {
        public Appointment( string petOwnerName,
                            string petOwnerNumber,
                            string petOwnerEmail,
                            string petOwnerLocation,
                            string petType,
                            string petName,
                            string petAge,
                            string petGender,
                            string petWeight,
                            string doctor,
                            string day,
                            string status,
                            string BookedByCustomer,
                            string BookedByPassword)
        {
            this.petOwnerName = petOwnerName;
            this.petOwnerNumber = petOwnerNumber;
            this.petOwnerEmail = petOwnerEmail;
            this.petOwnerLocation = petOwnerLocation;
            this.petType = petType;
            this.petName = petName;
            this.petAge = petAge;
            this.petGender = petGender;
            this.petWeight = petWeight;
            this.doctor = doctor;
            this.day = day;
            this.status = status;
            this.BookedByCustomer = BookedByCustomer;
            this.BookedByPassword = BookedByPassword;
        }
        public string petOwnerName;
        public string petOwnerNumber;
        public string petOwnerEmail;
        public string petOwnerLocation;
        public string petType;
        public string petName;
        public string petAge;
        public string petGender;
        public string petWeight;
        public string doctor;
        public string day;
        public string status;
        public string BookedByCustomer;
        public string BookedByPassword;

    }
}

using task1.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Net.NetworkInformation;
using System.Diagnostics.Eventing.Reader;
using System.Timers;

namespace task1
{
    internal class Program
    {
        public static List<SignUp> signUpData = new List<SignUp>();
        public static List<Approved> approvedUsersData = new List<Approved>();
        public static List<Customer> customerData = new List<Customer>();
        public static List<Vet> vetData = new List<Vet>();
        public static List<Appointment> appointmentData = new List<Appointment>();

        // STOI function
        static public string[] stringArray = new string[10] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        static public int[] intArray = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        static int STOI(string num)
        {
            int integer = 0;
            for (int i = 0; i < 10; i++)
            {
                if (num == stringArray[i])
                {
                    integer = i;
                }
            }

            return intArray[integer];
        }
        static void Main(string[] args)
        {
            // login SCreen
            string Option;
            // signUp Variables
            int signUpIdx = 0;
            string[] ThreeUsers = { "Admin", "Customer", "Veterinarian" };
            string Username = "";
            string Password = "";
            string role = "";
            string Role = "";

            int countUser = 0;
            bool signUpValidity;
            string SignUpFile = "signUp.txt";
            
            loadSignUpDetails(signUpData, SignUpFile);

            //Sign In
            string signInUserName = "";
            string signInPassword = "";
            int idx = 0;

            // Approved Users
            int signUpcount = 0;
            int approved = 0;
            int unapproved = 0;
            int approvedIdx = 0;
            string ApprovedFile = "Approved.txt";
            loadApprovedUsers(approvedUsersData, ApprovedFile);

            // customer details
            int customeridx = 0;
            int countCustomers = 0;
            string CustomerFile = "Customer.txt";
            loadCustomerDetails(customerData, CustomerFile);
            int BookedAppointmentsTotal = 0;
            int PendingAppointmentsTotal = 0;

            // Vet Details
            string VetFile = "Vet.txt";
            string[] AvailableService = { "Dermatology", "Primary Care", "Internal Medicine" };
            string[] AvailableDays = { "Monday", "Tuesday", "Wednesesday", "Thursday", "Friday", "Saturday" };
            
            string VUserName = "";
            string VPassword = "";
            string VetEmail = "";
            string VetContact = "";
            int VetDay = 0;
            int VetService = 0;
            int countVeterinarians = 0;
            int countVets = 0;
            int vetidx = 0;
            int vetPendingAppointments = 0;
            int vetBookedAppointments = 0;
            int vetTotalAppointments = 0;
            loadVetDetails(vetData, VetFile, ref vetPendingAppointments, ref vetBookedAppointments, ref vetTotalAppointments);
            
            // Appointment Details
            string AppointmentFile = "CustomerAppointments.txt";
            loadCustomerAppointments(appointmentData, AppointmentFile);
            int AppointmentsCount = 0;
            int Appointmentidx = 0;
           
            string[] Veterinarians = { "Dermatology", "Primary Care", "Internal Medicine" };
            
            int SelectDoctorType = 0;
            int SelectDay = 0;
            int SelectedTime = 0;
            int condition = 0;
            int Bookingidx = 0; // bookingIndexNumber
            int Total = 0;      // calculating total appointments by one user
            string[] AppointmentBookOrPending = new string[100];
            
            /*loadCustomerAppointments(PetOwnerName, PetOwnerNumber, PetOwnerEmail, PetOwnerLocation,
                         PetTypeForAppointment, PetName, PetAge, PetGender, PetWeight,
                         Doctor, AppointmentBookOrPending, ref AppointmentsCount, BookedBy,
                         BookedByPassword, DoctorAppointed, DayAppointed, AppointmentFile);
            */
            //checking
            Console.Clear();    
            for (int i = 0; i < signUpData.Count; i++)
            {
                Console.WriteLine(signUpData[i].username);
                Console.WriteLine(signUpData[i].password);
                Console.WriteLine(signUpData[i].role);
            }
            Console.WriteLine(signUpData.Count + "\n");
            for (int i = 0; i < approvedUsersData.Count; i++)
            {
                Console.WriteLine(approvedUsersData[i].username);
                Console.WriteLine(approvedUsersData[i].password);
                Console.WriteLine(approvedUsersData[i].role);
                Console.WriteLine(approvedUsersData[i].status);
            }
            Console.WriteLine(approvedUsersData.Count + "\n");
            for (int i = 0; i < customerData.Count; i++)
            {
                Console.WriteLine(customerData[i].name);
                Console.WriteLine(customerData[i].password);
            }
            Console.WriteLine(customerData.Count + "\n");

            for (int i = 0; i < vetData.Count; i++)
            {
                Console.WriteLine(vetData[i].name);
                Console.WriteLine(vetData[i].password);
                Console.WriteLine(vetData[i].email);
                Console.WriteLine(vetData[i].service);
                Console.WriteLine(vetData[i].day);
                Console.WriteLine(vetData[i].contact);
                Console.WriteLine(vetData[i].pendingAppointments);
                Console.WriteLine(vetData[i].bookedAppointments);
                Console.WriteLine(vetData[i].totalAppointments);
            }
            Console.WriteLine(vetData.Count + "\n");

            for (int i = 0; i < appointmentData.Count; i++)
            {
                Console.WriteLine(appointmentData[i].petOwnerName);
                Console.WriteLine(appointmentData[i].petOwnerNumber);
                Console.WriteLine(appointmentData[i].petOwnerEmail);
                Console.WriteLine(appointmentData[i].petOwnerLocation);
                Console.WriteLine(appointmentData[i].petName);
                Console.WriteLine(appointmentData[i].petType);
                Console.WriteLine(appointmentData[i].petAge);
                Console.WriteLine(appointmentData[i].petGender);
                Console.WriteLine(appointmentData[i].petWeight);
                Console.WriteLine(appointmentData[i].doctor);
                Console.WriteLine(appointmentData[i].day);
                Console.WriteLine(appointmentData[i].status);
                Console.WriteLine(appointmentData[i].BookedByCustomer);
                Console.WriteLine(appointmentData[i].BookedByPassword);

            }
            Console.WriteLine(appointmentData.Count + " Appointments\n");
            Console.ReadKey();
            // temporary 
            string[] ValidUsername = new string[100];
            string[] ValidPassword = new string[100];
            string[] ValidRole = new string[100];
            int Roles = 0;
            int[] Rolearray = new int[100];

            int useridx = 0;
            string[] DoctorName = new string[100];
            string[] TimeAppointed = new string[100];
            string[] DoctorAppointed = new string[100];
            string[] DayAppointed = new string[100];
            string[] BookedForPet = new string[100];
            string[] PetOwnerName = new string[100];
            string[] PetOwnerNumber = new string[100];
            string[] PetOwnerEmail = new string[100];
            string[] PetOwnerLocation = new string[100];
            string[] PetTypeForAppointment = new string[100];
            string[] PetName = new string[100];
            string[] PetAge = new string[100];
            string[] PetGender = new string[100];
            string[] PetWeight = new string[100];
            string[] Doctor = new string[100];
            string[] Time = new string[100];
            string[] Day = new string[100];
            string[] BookedBy = new string[100];
            string[] BookedByPassword = new string[100];

            string[] VetDays = new string[100];
            string[] VetServices = new string[100];
            string[] VetName = new string[100];
            string[] VetPassword = new string[100];
            string[] VetEmails = new string[100];
            string[] VContact = new string[100];
            string[] VetContactNumber = new string[100];
            string VEmail = "";
            string VContactNumber = "";

            string[] CustomerName = new string[100];
            string[] CustomerPassword = new string[100];

            string[] ApprovalStatus = new string[100];
            string[] ApprovedRoles = new string[100];
            string[] ApprovedUsers = new string[100];
            string[] ApprovedPwd = new string[100];

            while (true)
            {
                printHeader();
                tagline();
                printLoginScreen();
                Console.SetCursorPosition(63, 28);
                Console.Write("Enter Your Requirement: ");
                Option = Console.ReadLine();
                while (Option != "1" && Option != "2" && Option != "3")
                {
                    Console.SetCursorPosition(63, 30);
                    Console.Write("Please Enter Correct Option! ");
                    Console.SetCursorPosition(63, 28);
                    Console.Write("                                              ");
                    Console.SetCursorPosition(63, 28);
                    Console.Write("Enter Your Requirement: ");
                    Option = Console.ReadLine();
                    Console.SetCursorPosition(63, 30);
                    Console.Write("                                               ");
                }
                int option = int.Parse(Option);
                if (option == 3)
                {
                    break;
                }
                else if (option == 1)
                {
                    printHeader();
                    printBox();
                    GetSignUpDetails(ref Username, ref Password, ref Role, ref role);
                    signUpValidity = signUp(signUpData, Username, Password, Role, countUser);
                    if (signUpValidity)
                    {
                        // saving every user except admin in arrays of useres to be approved by admin
                        if (Role != "Admin")
                        {
                            
                        }
                        // saving all users in list
                        SignUp newSignedUpUser = new SignUp(Username, Password, Role);
                        signUpData.Add(newSignedUpUser);
                        countUser++;
                       
                        // saving data of users to file
                        saveSignUpDetails(signUpData, SignUpFile);

                        Console.SetCursorPosition(50, 31);
                        Console.Write("You are signed up successfully as " + Role + "!");
                        if (Role == "Admin")
                        {
                            Console.SetCursorPosition(50, 33);
                            Console.Write("Press any key to continue......");
                            Console.ReadKey();
                        }
                        else if (Role == "Customer")
                        {
                            // saving all customers in list
                            Customer newCustomer = new Customer(Username, Password);
                            customerData.Add(newCustomer);
                            // saving data of customers to file
                            saveCustomerDetails(customerData, CustomerFile);

                            
                            Console.SetCursorPosition(50, 35);
                            Console.Write("Press any key to continue......");
                            Console.ReadKey();

                        }
                        else if (Role == "Veterinarian")
                        {
                            Approved newUnapproved = new Approved(Username, Password, Role, "Unapproved");
                            approvedUsersData.Add(newUnapproved);
                            signUpcount++;
                            unapproved++;
                            saveApprovedUsers(approvedUsersData, ApprovedFile);

                            Console.SetCursorPosition(60, 33);
                            Console.Write("Let's create a profile first!");
                            Console.SetCursorPosition(60, 35);
                            Console.Write("Press any key to continue......");
                            Console.ReadKey();

                            // creating vet profile
                            
                            VetHeader();
                            CreateProfile( ref Username, ref Password, ref VetEmail, ref VetContact, ref VetDay, ref VetService);
                            Vet newVet = new Vet(Username, Password, AvailableService[VetService-1], VetEmail,AvailableDays[VetDay-1],VetContact, vetPendingAppointments, vetBookedAppointments, vetTotalAppointments);
                            vetData.Add(newVet);
                            countVets++;

                            saveVetDetails(vetData, appointmentData, VetFile, vetidx);

                            Console.SetCursorPosition(50, 33);
                            Console.Write("Your Account will be Activated after Admin Approval!");
                            Console.SetCursorPosition(50, 35);
                            Console.Write("Press any key to continue......");
                            Console.ReadKey();
                        }
                    }
                    else if (!(signUpValidity))
                    {
                        Console.SetCursorPosition(55, 31);
                        Console.Write("This Username and Password are not available..!");
                        Console.SetCursorPosition(55, 33);
                        Console.Write("   Press any key to continue......");
                        Console.ReadKey();
                    }
                }
                else if (option == 2)
                {
                    printHeader();
                    printBox();
                    getSignInDetails(ref signInUserName, ref signInPassword);
                    string signInValidity = signIn(signInUserName, signInPassword, signUpData, idx);

                    if (signInValidity == "Invalid")
                    {
                        Console.SetCursorPosition(58, 24);
                        Console.Write("Invalid Credentials");
                        Console.SetCursorPosition(58, 26);
                        Console.Write("Press any key to continue......");
                        Console.ReadKey();
                    }
                    else if (signInValidity == "Admin")
                    {
                        Console.SetCursorPosition(58, 24);
                        Console.Write("You are logged in successfully as " + signInValidity + "!");
                        Console.SetCursorPosition(58, 26);
                        Console.Write("Press any key to continue......");
                        Console.ReadKey();

                        printHeader();
                        

                        AdminMainMenu(SignUpFile, Rolearray, ApprovedFile, CustomerFile, AppointmentFile, VetFile, useridx, ThreeUsers, ref countUser, idx,
                                      Username, ValidUsername, signInUserName, Password, ValidPassword, signInPassword, role,
                                      ValidRole, Option, ref countVets,ref  countCustomers, Roles, vetidx, customeridx,ref AppointmentsCount,
                                      ref Appointmentidx, ref BookedAppointmentsTotal, ref PendingAppointmentsTotal, ref countVeterinarians, CustomerName,
                                      CustomerPassword, TimeAppointed,
                                      DoctorAppointed, DayAppointed, DoctorName, BookedForPet, Veterinarians,
                                      PetOwnerName, PetOwnerNumber, PetOwnerEmail, PetOwnerLocation, PetTypeForAppointment,
                                      PetName, PetAge, PetGender, PetWeight, Doctor, Time, Day,
                                      BookedBy, BookedByPassword, ref Bookingidx, ref Total, AppointmentBookOrPending, AvailableService,
                                      AvailableDays, VetDays, VetServices, VetName, VetPassword, VetEmails,
                                      VetContactNumber, VUserName, VPassword, VEmail, VContactNumber, ApprovalStatus, ApprovedRoles,
                                      ApprovedUsers, ApprovedPwd, ref signUpcount, ref approved, ref unapproved);

                    }
                    else if (signInValidity == "Customer")
                    {

                        Console.SetCursorPosition(58, 24);
                        Console.Write("You are logged in successfully as " + signInValidity + "!");
                        Console.SetCursorPosition(58, 26);
                        Console.Write("Press any key to continue......");
                        Console.ReadKey();

                        checkCustomerIndex(ref customeridx, Username, Password, customerData);
                        CustomerMainMenu( appointmentData,  customerData, vetData,   ref  BookedAppointmentsTotal, ref  PendingAppointmentsTotal,  Appointmentidx,   customeridx, SelectDoctorType, AppointmentFile, VetFile, vetidx, Bookingidx, Veterinarians);
                    }
                    else if (signInValidity == "Veterinarian" )
                    {
                        checkVetIndex(vetData.Count, ref vetidx, Username, Password, vetData);
                        checkApprovedUserIndex(signUpData.Count, ref approvedIdx, Username, Password, approvedUsersData);
                        checkIndexInSignUpList( ref signUpIdx, Username, Password, signUpData);

                        if (approvedUsersData[approvedIdx].status == "Approved")
                        {
                            Console.SetCursorPosition(55, 24);
                            Console.Write("You are logged in successfully as " + signInValidity + "!");
                            Console.SetCursorPosition(56, 26);
                            Console.Write("Press any key to continue......");
                            Console.ReadKey();
                            VetMainMenu(signInUserName, CustomerName, signInPassword, ValidRole, signUpIdx, useridx, ref BookedAppointmentsTotal, ref PendingAppointmentsTotal, vetidx, ValidUsername, ValidPassword, ref countUser, idx,
                                        ThreeUsers, ref countVets, VetName, VetPassword, VetEmails, VetContactNumber, VetDays, VetServices,
                                        AvailableDays, AvailableService, DoctorAppointed, DayAppointed, DoctorName, PetOwnerName,
                                        PetOwnerNumber, PetOwnerEmail, PetOwnerLocation, PetTypeForAppointment, PetName, PetAge, PetGender,
                                        PetWeight, Doctor, Time, Day, ref AppointmentsCount, AppointmentBookOrPending, BookedBy,
                                        VetFile, ref countVeterinarians, BookedByPassword, AppointmentFile, SignUpFile, Rolearray, ApprovalStatus, ApprovedUsers,
                                        ApprovedPwd, ApprovedRoles, ref signUpcount, ApprovedFile, customeridx);

                        }
                        else
                        {
                            Console.SetCursorPosition(55, 24);
                            Console.Write("Your Account is not Approved By Admin");
                            Console.SetCursorPosition(56, 26);
                            Console.Write("Press any key to continue......");
                            Console.ReadKey();
                        }
                        
                    }
                }
            }
        }
        // loading functions///////////////////////////////////////////////////////////////////
        static void loadSignUpDetails(List<SignUp> signUpData, string SignUpFile)
        {
            string line = "";
            string role = "";
            string Username, Password;
      
            if (File.Exists(SignUpFile))
            {
                StreamReader fileVariable = new StreamReader(SignUpFile);
                while ((line = fileVariable.ReadLine()) != null)
                {
                    Username = parseData(line, 1);
                    Password = parseData(line, 2);
                    role = parseData(line, 3);
                    SignUp newSignedUp = new SignUp(Username, Password, role);
                    signUpData.Add(newSignedUp);
                   
                }

                fileVariable.Close();
            }
            else
            {
                Console.Write("File does not exist!");
            }
        }
        static void loadApprovedUsers(List<Approved> approvedUsersData, string ApprovedFile)
        {
            string line = "";
            string role = "";
            string Username, Password, status;
            if (File.Exists(ApprovedFile))
            {
                StreamReader fileVariable = new StreamReader(ApprovedFile);
                while ((line = fileVariable.ReadLine()) != null)
                {
                    Username = parseData(line, 1);
                    Password = parseData(line, 2);
                    role = parseData(line, 3);
                    status = parseData(line, 4);
                    Approved newUnapproved = new Approved(Username, Password, role, status);
                    approvedUsersData.Add(newUnapproved);
                    
                }
                fileVariable.Close();
            }
            else
            {
                Console.Write("File does not exist!");
            }
        }
        static void loadCustomerDetails(List<Customer> customerData, string CustomerFile)
        {
            string line = "";
            string role = "";
            string Username, Password, status;
            if (File.Exists(CustomerFile))
            {
                StreamReader fileVariable = new StreamReader(CustomerFile);
                while ((line = fileVariable.ReadLine()) != null)
                {
                    Username = parseData(line, 1);
                    Password = parseData(line, 2);
                    role = parseData(line, 3);
                    status = parseData(line, 4);
                    Customer newCustomer = new Customer(Username, Password);
                    customerData.Add(newCustomer);

                }
                fileVariable.Close();
            }
            else
            {
                Console.Write("File does not exist!");
            }
        }
        static void loadVetDetails(List<Vet> vetData, string VetFile,ref int vetPendingAppointments, ref int vetBookedAppointments, ref int vetTotalAppointments)
        {
            string line = "";
            
            string Username, Password, email, contact, day, service, pending, booked, total;
            if (File.Exists(VetFile))
            {
                StreamReader fileVariable = new StreamReader(VetFile);
                while ((line = fileVariable.ReadLine()) != null)
                {
                    Username = parseData(line, 1);
                    Password = parseData(line, 2);
                    email = parseData(line, 3);
                    contact = parseData(line, 4);
                    day = parseData(line, 5);
                    service = parseData(line, 6);
                    pending = parseData(line, 7);
                    booked = parseData(line, 8);
                    total = parseData(line, 9);

                    vetPendingAppointments = int.Parse(pending);
                    vetBookedAppointments = int.Parse(booked);
                    vetTotalAppointments = int.Parse(total);

                    Vet newVet = new Vet(Username, Password, service, email, day, contact, vetPendingAppointments, vetBookedAppointments, vetTotalAppointments);
                    vetData.Add(newVet);

                }
                fileVariable.Close();
            }
            else
            {
                Console.Write("File does not exist!");
            }
        }
        static void loadCustomerAppointments(List<Appointment> appointmentData, string AppointmentFile)
        {
            string line = "";
            int x = 0;
            string Name, Number, Email, Location, TypeOfPet, petName, Age, Gender, weight, Doctor, status, bookedby, password, AppointedDoctor, AppointedDay;

            if (File.Exists(AppointmentFile))
            {
                StreamReader fileVariable = new StreamReader(AppointmentFile);
                while ((line = fileVariable.ReadLine()) != null)
                {
                    Name = parseData(line, 1);
                    Number = parseData(line, 2);
                    Email = parseData(line, 3);
                    Location = parseData(line, 4);
                    TypeOfPet = parseData(line, 5);
                    petName = parseData(line, 6);
                    Age = parseData(line, 7);
                    Gender = parseData(line, 8);
                    weight = parseData(line, 9);
                    Doctor = parseData(line, 10);
                    status = parseData(line, 11);
                    bookedby = parseData(line, 12);
                    password = parseData(line, 13);
                    AppointedDay = parseData(line, 14);

                    Appointment newAppointment = new Appointment(Name, Number,Email, Location, TypeOfPet,petName,Age, Gender, weight, Doctor,AppointedDay,status, bookedby, password);
                    appointmentData.Add(newAppointment);
                }
                fileVariable.Close();
            }
            else
            {
                Console.Write("File does not exist!");
            }
        }
        // Printing Functions
        static void printHeader()
        {
            Console.Clear();
            Console.SetCursorPosition(1, 1);
            Console.Write("*************************************************************************************************************************************************************************** ");

            Console.SetCursorPosition(1, 2);
            Console.Write("___________________________________________________________________________________________________________________________________________________________________________  ");

            Console.SetCursorPosition(1, 4);
            Console.Write("                                             :::::::::       ::::::::::   :::::::::::       :::::::::           :::        :::                                 ");

            Console.SetCursorPosition(1, 5);
            Console.Write("                                            :+:    :+:      :+:              :+:           :+:    :+:        :+: :+:      :+:           (\\___/)             ");

            Console.SetCursorPosition(1, 6);
            Console.Write("                                           +:+    +:+      +:+              +:+           +:+    +:+       +:+   +:+     +:+            (_^-^ )__            ");

            Console.SetCursorPosition(1, 7);
            Console.Write("                                          +#++:++#+       +#++:++#         +#+           +#++:++#+       +#++:++#++:    +#+               /      _\\~~        ");

            Console.SetCursorPosition(1, 8);
            Console.Write("                                         +#+             +#+              +#+           +#+             +#+     +#+    +#+                \\/''\\/            ");

            Console.SetCursorPosition(1, 9);
            Console.Write("                                        #+#             #+#              #+#           #+#             #+#     #+#    #+#");

            Console.SetCursorPosition(1, 10);
            Console.Write("                                       ###             ##########       ###           ###             ###     ###    ##########");

            Console.SetCursorPosition(1, 12);
            Console.Write("___________________________________________________________________________________________________________________________________________________________________________  ");

            Console.SetCursorPosition(1, 14);
            Console.Write("***************************************************************************************************************************************************************************  ");
        }
        static void tagline()
        {
            Console.SetCursorPosition(1, 16);
            Console.Write("                                                          \"BECAUSE THE PETS DESERVE THE BEST!\"                                                                   ");
        }
        static void printBox()
        {
            Console.SetCursorPosition(50, 18);
            Console.Write("[][][][][][][][][][][][][][][][][][][][][][][][][][][]");
            Console.SetCursorPosition(50, 19);
            Console.Write("[]                                                  []");
            Console.SetCursorPosition(50, 20);
            Console.Write("[]                                                  []");
            Console.SetCursorPosition(50, 21);
            Console.Write("[]                                                  []");
            Console.SetCursorPosition(50, 22);
            Console.Write("[]                                                  []");
            Console.SetCursorPosition(50, 23);
            Console.Write("[]                                                  []");
            Console.SetCursorPosition(50, 24);
            Console.Write("[]                                                  []");
            Console.SetCursorPosition(50, 25);
            Console.Write("[]                                                  []");
            Console.SetCursorPosition(50, 26);
            Console.Write("[]                                                  []");
            Console.SetCursorPosition(50, 27);
            Console.Write("[]                                                  []");
            Console.SetCursorPosition(50, 28);
            Console.Write("[]                                                  []");
            Console.SetCursorPosition(50, 29);
            Console.Write("[]                                                  []");
            Console.SetCursorPosition(50, 30);
            Console.Write("[][][][][][][][][][][][][][][][][][][][][][][][][][][]");
        }
        static void printLoginScreen()
        {
            Console.SetCursorPosition(55, 18);
            Console.Write("  [][][][][][][][][][][][][][][][][][][][][] ");
            Console.SetCursorPosition(55, 19);
            Console.Write("  []                                      [] ");
            Console.SetCursorPosition(55, 20);
            Console.Write("  []   1. Sign Up                         [] ");
            Console.SetCursorPosition(55, 21);
            Console.Write("  []                                      [] ");
            Console.SetCursorPosition(55, 22);
            Console.Write("  []   2. Login                           [] ");
            Console.SetCursorPosition(55, 23);
            Console.Write("  []                                      [] ");
            Console.SetCursorPosition(55, 24);
            Console.Write("  []   3. Exit                            [] ");
            Console.SetCursorPosition(55, 25);
            Console.Write("  []                                      [] ");
            Console.SetCursorPosition(55, 26);
            Console.Write("  [][][][][][][][][][][][][][][][][][][][][]  ");
        }
        static void VetHeader()
        {
            printHeader();
            printVetMenuBar();
        }
        static void printVetMenuBar()
        {
            Console.SetCursorPosition(1, 15);
            Console.Write("___________________________________________________________________________________________________________________________________________________________________________");

            Console.SetCursorPosition(0, 17);
            Console.Write("                                                          ``.....    V e t e r i n a r i a n   .....``");

            Console.SetCursorPosition(1, 18);
            Console.Write("___________________________________________________________________________________________________________________________________________________________________________");
        }
        static string parseData(string line, int field)
        {

            string item = "";
            int commaCount = 1;
            int length = line.Length;
            for (int x = 0; x < length; x++)
            {
                if (line[x] == ',')
                {
                    commaCount++;
                }
                else if (field == commaCount)
                {
                    item += line[x];
                }
            }

            return item;
        }
        // Sign Up     //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        static void GetSignUpDetails( ref string username, ref string password, ref string role, ref string roleInput)
        {
            Console.SetCursorPosition(60, 20);
            Console.Write("Enter Username: ");
            username = Console.ReadLine();

            while (username.Length == 0 || username == "")
            {
                Console.SetCursorPosition(60, 24);

                Console.Write("Please Enter a valid username");


                Console.SetCursorPosition(60, 20);
                Console.Write("Enter Username: ");
                username = Console.ReadLine();
            }

            Console.SetCursorPosition(60, 22);
            Console.Write("Enter Password: ");
            password = Console.ReadLine();

            while (password.Length > 16 || password.Length < 8)
            {
                Console.SetCursorPosition(60, 24);
                Console.Write("Password must Contain 8 to 16 characters! ");
                Console.SetCursorPosition(60, 22);
                Console.Write("                                          ");
                Console.SetCursorPosition(60, 22);
                Console.Write("Enter Password: ");
                password = Console.ReadLine();
            }

            Console.SetCursorPosition(60, 24);
            Console.Write("                                         ");
            Console.SetCursorPosition(60, 24);
            Console.Write("Select Role: ");
            Console.SetCursorPosition(60, 26);
            Console.Write("1. Admin");
            Console.SetCursorPosition(60, 27);
            Console.Write("2. Customer");
            Console.SetCursorPosition(60, 28);
            Console.Write("3. Veterinarian");
            Console.SetCursorPosition(73, 24);
            roleInput = Console.ReadLine();

            while (roleInput != "1" && roleInput != "2" && roleInput != "3")
            {
                Console.SetCursorPosition(60, 31);
                Console.Write("Please Enter Correct Option");
                Console.SetCursorPosition(60, 24);
                Console.Write("                                      ");
                Console.SetCursorPosition(60, 24);
                Console.Write("Select Role: ");
                roleInput = Console.ReadLine();
            }
            if (roleInput == "1")
            {
                role = "Admin";
            }
            else if (roleInput == "2")
            {
                role = "Customer";
            }
            else if (roleInput == "3")
            {
                role = "Veterinarian";
            }

        }
        static bool signUp(List<SignUp> signUpData, string Username, string Password, string Role, int countUser)
        {
            int j = 0;
            for (int x = 0; x < signUpData.Count; x++)
            {
                
                if (((Password == signUpData[x].password) && (Username == signUpData[x].username)) || ((Role == signUpData[x].role) && (Role == "Admin")))
                {
                    j++;
                    break;
                }
            }
            if (j == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// saving data to files
        // signUp
        static void saveSignUpDetails(List<SignUp> signUpData, string SignUpFile)
        {
            StreamWriter fileVariable = new StreamWriter(SignUpFile);
            for (int i = 0; i < signUpData.Count; i++)
            {
                fileVariable.WriteLine(signUpData[i].username + "," + signUpData[i].password + "," + signUpData[i].role);
            }
            fileVariable.Flush();
            fileVariable.Close();
        }
        // Approved 
        static void saveApprovedUsers(List<Approved> approvedUsersData, string ApprovedFile)
        {
            StreamWriter fileVariable = new StreamWriter(ApprovedFile);
            for (int i = 0; i < approvedUsersData.Count; i++)
            {
                fileVariable.WriteLine(approvedUsersData[i].username + "," + approvedUsersData[i].password + "," + approvedUsersData[i].role + "," + approvedUsersData[i].status);
            }
            fileVariable.Flush();
            fileVariable.Close();
        }
        // Customer
        static void saveCustomerDetails(List<Customer> customerData, string CustomerFile)
        {
            StreamWriter fileVariable = new StreamWriter(CustomerFile);
            for (int i = 0; i < customerData.Count; i++)
            {
                fileVariable.WriteLine(customerData[i].name + "," + customerData[i].password);
            }
            fileVariable.Flush();
            fileVariable.Close();
        }
        // vet
        static void saveVetDetails(List<Vet> vetData, List<Appointment> appointmentsData, string VetFile,  int vetidx)
        {
            StreamWriter file = new StreamWriter(VetFile);
            for (int i = 0; i < vetData.Count; i++)
            {
                checkVetIndex(vetData.Count, ref vetidx, vetData[i].name, vetData[i].password, vetData);
                int pending = countVetPendingAppointments(appointmentsData, vetidx, vetData);
                int booked = countVetBookedAppointments(appointmentsData,  vetidx, vetData);
                file.Write(vetData[i].name + "," + vetData[i].password + "," + vetData[i].email + "," + vetData[i].contact + "," + vetData[i].day + "," + vetData[i].service + ",");
                if (vetData[vetidx].name == vetData[i].name)
                {
                    file.WriteLine(pending + "," + booked + "," + (pending + booked));
                }
            }
            file.Flush();
            file.Close();
        }
        // appointments
        static void saveCustomerAppointments(List<Customer> customerData, List<Appointment> appointmentData, string AppointmentFile, int customeridx)
        {
            StreamWriter file = new StreamWriter(AppointmentFile);
            int idx = 0;
            for (int i = 0; i < appointmentData.Count; i++)
            {
                int total = TotalAppointmentsByOneUser(appointmentData, appointmentData[i].BookedByCustomer);
                int booked = CountBookedAppointmentsForCustomer(appointmentData, customerData, customeridx);

                file.Write(appointmentData[i].petOwnerName + "," + appointmentData[i].petOwnerNumber + "," + appointmentData[i].petOwnerEmail + "," + 
                            appointmentData[i].petOwnerLocation + "," + appointmentData[i].petType + "," + appointmentData[i].petName + "," + 
                            appointmentData[i].petAge + "," + appointmentData[i].petGender + "," + appointmentData[i].petWeight + "," +
                            appointmentData[i].doctor + "," + appointmentData[i].status + "," + appointmentData[i].BookedByCustomer + 
                            "," + appointmentData[i].BookedByPassword  + "," + appointmentData[i].day + ",");
                if (appointmentData[customeridx].BookedByCustomer == appointmentData[i].BookedByCustomer)
                {
                    file.WriteLine((total - booked) + "," + booked + "," + total);
                }
            }
            file.Flush();
            file.Close();
        }
        // SignIn////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        static void getSignInDetails(ref string signInUserName, ref string signInPassword)
        {
            Console.SetCursorPosition(58, 20);
            Console.Write("Enter Username:  ");
            signInUserName = Console.ReadLine();
            Console.SetCursorPosition(58, 22);
            Console.Write("Enter Password:  ");
            signInPassword = Console.ReadLine();
        }
        static string signIn(string signInUserName, string signInPassword, List<SignUp> signUpData, int idx)
        {
            int k = 0;
            for (int x = 0; x < signUpData.Count; x++)
            {
                if ((signInUserName == signUpData[x].username) && (signInPassword == signUpData[x].password))
                {
                    k++;
                    idx = x;
                    break;
                }
            }
            if (k > 0)
            {
                return signUpData[idx].role;
            }
            else
            {
                return "Invalid";
            }
        }
         // ADMIN    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
         static void AdminMainMenu(string SignUpFile, int[] Roles, string ApprovedFile, string CustomerFile, string AppointmentFile, string VetFile, int useridx, string[] ThreeUsers, ref int countUser, int idx, string Username, string[] ValidUsername, string signInUserName, string Password, string[] ValidPassword, string signInPassword, string role, string[] ValidRole, string Option, ref int countVets, ref int countCustomers, int Role, int vetidx, int customeridx, ref int AppointmentsCount, ref int Appointmentidx, ref int BookedAppointmentsTotal, ref int PendingAppointmentsTotal, ref int countVeterinarians, string[] CustomerName, string[] CustomerPassword, string[] TimeAppointed, string[] DoctorAppointed, string[] DayAppointed, string[] DoctorName, string[] BookedForPet, string[] Veterinarians, string[] PetOwnerName, string[] PetOwnerNumber, string[] PetOwnerEmail, string[] PetOwnerLocation, string[] PetTypeForAppointment, string[] PetName, string[] PetAge, string[] PetGender, string[] PetWeight, string[] Doctor, string[] Time, string[] Day, string[] BookedBy, string[] BookedByPassword, ref int Bookingidx, ref int Total, string[] AppointmentBookOrPending, string[] AvailableService, string[] AvailableDays, string[] VetDays, string[] VetService, string[] VetName, string[] VetPassword, string[] VetEmail, string[] VetContact, string VUserName, string VPassword, string VEmail, string VContact, string[] ApprovalStatus, string[] ApprovedRoles, string[] ApprovedUsers, string[] ApprovedPwd, ref int signUpcount, ref int approved, ref int unapproved)
        {
            int AddUsersOption = 0;
            int ViewUsersOption = 0;
            int deleteOption = 0;
            int AdminRequirement = 0;
            int AppointmentRequirement = 0;
            string showingstatus = "";
            string show = "";
            int details = 0;

            while (true)
            {
                printHeader();
                AdminRequirement = PrintAdminMenu();

                if (AdminRequirement == 1)
                {
                    AdminHeader();
                   
                    if (signUpData.Count > 0 && unapproved > 0)
                    {
                        AddSelectedUser(ref approved, ref unapproved);
                        saveApprovedUsers(approvedUsersData, ApprovedFile);

                        if (unapproved == 0)
                        {
                            Console.SetCursorPosition(60, 34);
                            Console.Write("No more users to add");
                        }

                        Console.SetCursorPosition(60, 36);
                        Console.Write("Press any key to continue...");
                        Console.ReadKey();
                    }
                    else
                    {
                        AdminHeader();
                        Console.SetCursorPosition(60, 24);
                        Console.Write("There are no users to add");
                        Console.SetCursorPosition(60, 26);
                        Console.Write("Press any key to continue..");
                        Console.ReadKey();
                    }
                }
                else if (AdminRequirement == 2)
                {
                    while (true)
                    {
                        AdminHeader();
                        ViewUsersOption = ViewUsersMenu();

                        if (ViewUsersOption == 1)
                        {
                            show = "Customer";

                            if (customerData.Count > 0)
                            {
                                AdminHeader();
                                details = ShowUsers( show);

                                if (details == 1)
                                {
                                    AdminHeader();
                                    ViewCustomerDetails( show);

                                    Console.SetCursorPosition(60, 36);
                                    Console.Write("Press any key to continue..");
                                }
                                else if (details == 2)
                                {
                                    break;
                                }

                                Console.ReadKey();
                            }
                            else
                            {
                                AdminHeader();
                                Console.SetCursorPosition(60, 24);
                                Console.Write("There are no Customers to show");
                                Console.SetCursorPosition(60, 26);
                                Console.Write("Press any key to continue..");
                                Console.ReadKey();
                            }
                        }
                        else if (ViewUsersOption == 2)
                        {
                            show = "Veterinarian";

                            if (vetData.Count > 0)
                            {
                                AdminHeader();
                                details = ShowUsers(show);

                                if (details == 1)
                                {
                                    AdminHeader();
                                    ViewVetDetails(countVets, countUser, ValidRole, show, ValidUsername, ValidPassword, BookedBy, BookedByPassword, AppointmentsCount, AppointmentBookOrPending, VetName,
                                        VetContact,
                                        VetPassword,
                                        VetEmail,
                                        VetService,
                                        VetDays,
                                        DoctorName, DoctorAppointed);

                                    Console.SetCursorPosition(60, 36);
                                    Console.Write("Press any key to continue..");
                                    Console.ReadKey();
                                }
                                else if (details == 2)
                                {
                                    break;
                                }
                            }
                            else
                            {
                                AdminHeader();
                                Console.SetCursorPosition(60, 24);
                                Console.Write("There are no Veterinarians to show");
                                Console.SetCursorPosition(60, 26);
                                Console.Write("Press any key to continue..");
                                Console.ReadKey();
                            }
                        }
                        else if (ViewUsersOption == 3)
                        {
                            break;
                        }
                    }
                }
                else if (AdminRequirement == 3)
                {
                    while (true)
                    {
                        AdminHeader();
                        deleteOption = ViewUsersMenu();

                        if (deleteOption == 1)
                        {
                            show = "Customer";

                            if (customerData.Count > 0)
                            {
                                AdminHeader();
                                DeleteCustomerDetails(ValidRole,
                                    ValidUsername,
                                    ValidPassword, ref BookedAppointmentsTotal, ref PendingAppointmentsTotal, ref Appointmentidx,  Total, ref Bookingidx,ref countCustomers, ref countUser, ThreeUsers, TimeAppointed,
                                    DoctorAppointed,
                                    DayAppointed,
                                    BookedForPet,
                                    Veterinarians,
                                    PetOwnerName,
                                    PetOwnerNumber,
                                    PetOwnerEmail,
                                    PetOwnerLocation,
                                    PetTypeForAppointment,
                                    PetName,
                                    PetAge,
                                    PetGender,
                                    PetWeight,
                                    Doctor,
                                    DoctorName,
                                    Time,
                                    Day,
                                    ref AppointmentsCount,
                                    AppointmentBookOrPending,
                                    CustomerName,
                                    CustomerPassword, BookedBy, BookedByPassword, ApprovalStatus, ApprovedRoles, ApprovedUsers, ApprovedPwd,
                                    ref signUpcount,
                                    ref approved,
                                    ref unapproved, countVets);

                                saveCustomerDetails(customerData, CustomerFile);
                                saveCustomerAppointments(customerData ,appointmentData, AppointmentFile, customeridx);

                                saveApprovedUsers(approvedUsersData, ApprovedFile);
                                saveSignUpDetails(signUpData, SignUpFile);

                                Console.SetCursorPosition(60, 36);
                                Console.Write("Press any key to continue..");
                                Console.ReadKey();
                            }
                            else
                            {
                                AdminHeader();
                                Console.SetCursorPosition(60, 24);
                                Console.Write("There are no Customers to show");
                                Console.SetCursorPosition(60, 26);
                                Console.Write("Press any key to continue..");
                                Console.ReadKey();
                            }
                        }
                        else if (deleteOption == 2)
                        {
                            show = "Veterinarian";

                            if (vetData.Count > 0)
                            {
                                while (true)
                                {
                                    AdminHeader();
                                    details = ShowUsers( show);

                                    if (details == 1)
                                    {
                                        AdminHeader();
                                        DeleteVetDetails(ref countVeterinarians, countCustomers, vetidx, ValidUsername, ValidRole, ValidPassword, ref countUser, idx, ThreeUsers, ref countVets,
                                            VetName, VetPassword, VetEmail, VetContact, VetDays,
                                            VetService, ref signUpcount, DoctorAppointed,  AppointmentsCount, AppointmentBookOrPending,
                                            ApprovalStatus, ApprovedRoles, ApprovedUsers, ApprovedPwd);

                                        saveVetDetails(vetData, appointmentData, VetFile, vetidx);
                                        saveApprovedUsers(approvedUsersData, ApprovedFile);
                                        saveSignUpDetails(signUpData, SignUpFile);

                                        Console.SetCursorPosition(60, 36);
                                        Console.Write("Press any key to continue..");
                                        Console.ReadKey();
                                    }
                                    else if (details == 2)
                                    {
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                AdminHeader();
                                Console.SetCursorPosition(60, 24);
                                Console.Write("There are no Veterinarians to show");
                                Console.SetCursorPosition(60, 26);
                                Console.Write("Press any key to continue..");
                                Console.ReadKey();
                            }
                        }
                        else if (deleteOption == 3)
                        {
                            break;
                        }
                    }
                }
                else if (AdminRequirement == 4)
                {
                    int pendingAppointments = 0;

                    while (true)
                    {
                        AdminHeader();

                        AppointmentRequirement = ViewAppointmentsMenu();

                        if (AppointmentRequirement == 1)
                        {
                            pendingAppointments = CountPendingAppointmentsForAdmin();

                            if (pendingAppointments > 0)
                            {
                                showingstatus = "Pending";
                                AdminHeader();
                                ShowPendingAppointmentsTOAdmin(showingstatus, DoctorAppointed, DayAppointed,
                                    PetOwnerName,
                                    PetOwnerNumber,
                                    PetOwnerEmail,
                                    PetOwnerLocation,
                                    PetTypeForAppointment,
                                    PetName,
                                    PetAge,
                                    PetGender,
                                    PetWeight,
                                    Doctor,
                                    Time,
                                    Day, ref AppointmentsCount,
                                    AppointmentBookOrPending);

                                Console.SetCursorPosition(60, 36);
                                Console.Write("Press any key to continue..");
                                Console.ReadKey();
                            }
                            else
                            {
                                AdminHeader();
                                Console.SetCursorPosition(60, 24);
                                Console.Write("There are no Pending Appointments !");
                                Console.SetCursorPosition(60, 26);
                                Console.Write("Press any key to continue..");
                                Console.ReadKey();
                            }
                        }
                        else if (AppointmentRequirement == 2)
                        {
                            if ((appointmentData.Count - pendingAppointments) > 0)
                            {
                                showingstatus = "Booked";
                                AdminHeader();
                                ShowBookedAppointmentsTOAdmin(showingstatus, DoctorAppointed, DayAppointed,
                                    PetOwnerName,
                                    PetOwnerNumber,
                                    PetOwnerEmail,
                                    PetOwnerLocation,
                                    PetTypeForAppointment,
                                    PetName,
                                    PetAge,
                                    PetGender,
                                    PetWeight,
                                    Doctor,
                                    Time,
                                    Day, ref AppointmentsCount,
                                    AppointmentBookOrPending);

                                Console.SetCursorPosition(60, 36);
                                Console.Write("Press any key to continue..");
                                Console.ReadKey();
                            }
                            else
                            {
                                AdminHeader();
                                Console.SetCursorPosition(60, 24);
                                Console.Write("There are no Booked Appointments !");
                                Console.SetCursorPosition(60, 26);
                                Console.Write("Press any key to continue..");
                                Console.ReadKey();
                            }
                        }
                        else if (AppointmentRequirement == 3)
                        {
                            DeleteAppointments(ref Bookingidx, ref BookedAppointmentsTotal, ref PendingAppointmentsTotal, DoctorAppointed, DayAppointed,
                                PetOwnerName,
                                PetOwnerNumber,
                                PetOwnerEmail,
                                PetOwnerLocation,
                                PetTypeForAppointment,
                                PetName,
                                PetAge,
                                PetGender,
                                PetWeight,
                                Doctor,
                                Time,
                                Day,ref AppointmentsCount,
                                AppointmentBookOrPending, BookedBy, BookedByPassword, BookedForPet, ref Appointmentidx, DoctorName);

                            saveCustomerAppointments(customerData, appointmentData, AppointmentFile, customeridx);


                            saveVetDetails(vetData, appointmentData, VetFile, vetidx);

                            Console.SetCursorPosition(50, 36);
                            Console.Write("                                   ");
                            Console.SetCursorPosition(50, 36);
                            Console.Write("Press any key to continue..");
                            Console.ReadKey();
                        }
                        else if (AppointmentRequirement == 4)
                        {
                            break;
                        }
                    }
                }
                else if (AdminRequirement == 5)
                {
                    break;
                }
            }
        }
        static void AdminHeader()
        {
            printHeader();
            Console.SetCursorPosition(1, 15);
            Console.Write("___________________________________________________________________________________________________________________________________________________________________________");

            Console.SetCursorPosition(0, 17);
            Console.Write( "                                                                ``.....    A d m i n   .....``");

            Console.SetCursorPosition(1, 18);
            Console.Write( "___________________________________________________________________________________________________________________________________________________________________________");
        }
        static int PrintAdminMenu()
        {
            Console.SetCursorPosition(1, 17);
            Console.Write( "---------------------------------------------------------------------------------------------------------------------------------------------------------------------------" );
            Console.SetCursorPosition(71, 19);
            Console.Write( "1. Add Users");
            Console.SetCursorPosition(71, 21);
            Console.Write( "2. View Users");
            Console.SetCursorPosition(71, 23);
            Console.Write( "3. Delete Users");
            Console.SetCursorPosition(71, 25);
            Console.Write( "4. Appointments");
            Console.SetCursorPosition(71, 27);
            Console.Write( "5. Logout");
            Console.SetCursorPosition(1, 29);
            Console.Write( "---------------------------------------------------------------------------------------------------------------------------------------------------------------------------") ;
            Console.SetCursorPosition(71, 31);
            Console.Write( "Enter Your Requirement: ");
            string Option;
            Option = Console.ReadLine();
            while (Option != "1" && Option != "2" && Option != "3" && Option != "4" && Option != "5")
            {
                Console.SetCursorPosition(71, 33);

                Console.Write( "Please Enter Correct Option!");
                Console.SetCursorPosition(71, 31);
                Console.Write( "                                            ");
                Console.SetCursorPosition(71, 31);
                Console.Write( "Enter Your Requirement: ");
                Option= Console.ReadLine();
                Console.SetCursorPosition(71, 33);
                Console.Write( "                                             ");
            }
            int Admin_Requirement = int.Parse(Option);
            return Admin_Requirement;
        }
        static void AddSelectedUser(ref int approved, ref int unapproved)
        {

            string Option;
            for (int x = 0; x < approvedUsersData.Count ; x++)
            {
                if (approvedUsersData[x].status == "Unapproved" && approvedUsersData[x].role == "Veterinarian")
                {
                    AdminHeader();
                    Console.SetCursorPosition(60, 22);
                    Console.Write("User " + (x + 1) + ": " + approvedUsersData[x].role);
                    Console.SetCursorPosition(60, 24);
                    Console.Write( "Status: " + approvedUsersData[x].status);
                    Console.SetCursorPosition(60, 26);
                    Console.Write( "1. Add         2. Ignore");
                    Console.SetCursorPosition(60, 28);
                    Console.Write( "Enter Option...");
                    Option =  Console.ReadLine();
                    while (Option != "1" && Option != "2")
                    {
                        Console.SetCursorPosition(60, 30);
        
                        Console.Write("Please Enter Correct Option!");
                        Console.SetCursorPosition(60, 28);
                        Console.Write( "                                 ");
                        Console.SetCursorPosition(60, 28);
                        Console.Write( "Enter Option...");
                        Option =  Console.ReadLine();
                        Console.SetCursorPosition(60, 30);
                        Console.Write( "                                              ");
                    }
                    int option = int.Parse(Option);
                    if (option == 1)
                    {
                        approved++;
                        unapproved--;
                        approvedUsersData[x].status = "Approved";
                        Console.SetCursorPosition(60, 30);
                        
                        Console.Write( "User Added!");
                    }
                    else
                    {
                        Console.SetCursorPosition(60, 30);
        
                        Console.Write( "User Ignored!");
                    }
                    Console.SetCursorPosition(60, 32);
                    Console.Write( "Press any key to see next User...");
                    Console.ReadKey();
                    continue;
                }
                else
                {
                    continue;
                }
            }
        }
        static int ShowUsers(string show)
        {
            int z = 60;
            int y = 24;
            int i = 0;
            for (int x = 0; x < signUpData.Count ; x++)
            {
                if (signUpData[x].role == show && signUpData[x].role != "Admin")
                {
                    Console.SetCursorPosition(z, y);
                    Console.Write( show + " " + i + 1 + ": " + signUpData[x].username);
                    y += 2;
                    i++;
                }
                else
                {
                    continue;
                }
            }
            string Option;
            Console.SetCursorPosition(z, y + 2);
            Console.Write( "1. View Details           2.Exit");
            Console.SetCursorPosition(60, y + 4);
            Console.Write( "Enter Option...");
            Option= Console.ReadLine();
            while (Option != "1" && Option != "2")
            {
                Console.SetCursorPosition(60, y + 6);

                Console.Write( "Please Enter Correct Option!");
                Console.SetCursorPosition(60, y + 4);
                Console.Write( "                                          ");
                Console.SetCursorPosition(60, y + 4);
                Console.Write( "Enter Option...");
                Option = Console.ReadLine();
                Console.SetCursorPosition(60, y + 6);
                Console.Write( "                                              ");
            }
            int option = int.Parse(Option);
            return option;
        }
        static int ViewUsersMenu()
        {
            Console.SetCursorPosition(1, 19);
            Console.Write( "---------------------------------------------------------------------------------------------------------------------------------------------------------------------------" );
            Console.SetCursorPosition(71, 21);
            Console.Write( "1. Customers");
            Console.SetCursorPosition(71, 23);
            Console.Write( "2. Veterinarians");
            Console.SetCursorPosition(71, 25);
            Console.Write( "3. Exit");
            Console.SetCursorPosition(1, 27);
            Console.Write( "---------------------------------------------------------------------------------------------------------------------------------------------------------------------------") ;
            Console.SetCursorPosition(71, 29);
            Console.Write( "Enter Your Requirement: ");
            string Option;
            Option = Console.ReadLine();
            while (Option != "1" && Option != "2" && Option != "3")
            {
                Console.SetCursorPosition(71, 31);

                Console.Write( "Please Enter Correct Option!");
                Console.SetCursorPosition(71, 29);
                Console.Write( "                                              ");
                Console.SetCursorPosition(71, 29);
                Console.Write( "Enter Your Requirement: ");
                Option = Console.ReadLine();
                Console.SetCursorPosition(71, 31);
                Console.Write( "                                              ");
            }
            int Admin_Requirement = int.Parse(Option);
            return Admin_Requirement;
        }
        static void ViewCustomerDetails( string show)
        {
            int y = 0;
            int customeridx = 0;
            for (int z = 0; z < signUpData.Count ; z++)
            {
                if (signUpData[z].role == show)
                {
                    AdminHeader();
                    y++;
                    Console.SetCursorPosition(40, 20);
                    
                    Console.Write( "Customer Name: " + signUpData[z].username);
                    Console.SetCursorPosition(40, 22);
                    
                    Console.Write( "Customer Account Password: " + signUpData[z].password);
                    int total = TotalAppointmentsByOneUser(appointmentData, signUpData[z].username);
                    int booked = CountBookedAppointmentsForCustomer(appointmentData, customerData ,z);
                    Console.SetCursorPosition(40, 24);
                    
                    Console.Write( "Total Appointments : " + total);
                    Console.SetCursorPosition(40, 26);
                    
                    Console.Write( "Booked Appointments: " + booked);
                    Console.SetCursorPosition(40, 28);
                    
                    Console.Write( "Pending Appointments: " + (total - booked));
                    Console.SetCursorPosition(60, 32);
                    Console.Write( "Press any key to see next Customer Details..");
                    if (total > 0)
                    {
                        customeridx = Bookingindex( appointmentData , signUpData[z].username);
                        Console.SetCursorPosition(40, 30);
                        
                        Console.Write( "Customer Contact: " + appointmentData[customeridx].petOwnerName);
                        Console.SetCursorPosition(80, 20);
                        
                        Console.Write( "Customer Email: " + appointmentData[customeridx].petOwnerEmail);
                        Console.SetCursorPosition(80, 22);
                        
                        Console.Write( "Customer Location: " + appointmentData[customeridx].petOwnerLocation);
                        Console.SetCursorPosition(80, 24);
                        
                        Console.Write( "Customer Pet Type: " + appointmentData[customeridx].petType);
                        Console.SetCursorPosition(80, 26);
                        Console.Write( "Customer Pet Name: " + appointmentData[customeridx].petName);
                        Console.SetCursorPosition(80, 28);
                        
                        Console.Write( "Customer Pet Age: " + appointmentData[customeridx].petAge);
                        Console.SetCursorPosition(60, 32);
                        Console.Write( "Press any key to see next Customer Details..");
                    }
                    Console.ReadKey();
                }
                else
                {
                    continue;
                }
            }
            if (y == 1)
            {
                Console.SetCursorPosition(60, 34);
                Console.Write( "                                               ");
                Console.SetCursorPosition(60, 34);
                Console.Write( "There is Only " + y + " Customer");
            }
            else if (y > 1)
            {
                Console.SetCursorPosition(60, 34);
                Console.Write( "                                               ");
                Console.SetCursorPosition(60, 34);
                Console.Write( "There are Only " + y + " Customers");
            }
        }

        static void ViewVetDetails(int countVets, int countUser, string[] ValidRole, string show, string[] ValidUsername, string[] ValidPassword, string[] BookedBy, string[] BookedByPassword, int AppointmentsCount, string[] AppointmentBookOrPending, string[] VetName,
                            string[] VetContact,
                            string[] VetPassword,
                            string[] VetEmail,
                            string[] VetService,
                            string[] VetDays,
                            string[] DoctorName, string[] DoctorAppointed)
        {
            int y = 0;
            int vetidx = 0;
            for (int z = 0; z < signUpData.Count ; z++)
            {
                if (signUpData[z].role == show)
                {
                    AdminHeader();
                    y++;
                    Console.SetCursorPosition(40, 20);
                    Console.Write( "Veterinarian Name: " + signUpData[z].username);
                    Console.SetCursorPosition(40, 22);
                    Console.Write( "Veterinarian Account Password: " + signUpData[z].password);
                    checkVetIndex(countVets, ref vetidx, signUpData[z].username, signUpData[z].password, vetData);
                    int pending = CountPendingAppointmentsForVet(appointmentData, vetData, vetidx);
                    int booked = CountBookedAppointmentsForVet(appointmentData, vetData, vetidx);
                    Console.SetCursorPosition(40, 24);
                    Console.Write( "Total Appointments : " + (booked + pending));
                    Console.SetCursorPosition(40, 26);
                    Console.Write( "Booked Appointments: " + booked);
                    Console.SetCursorPosition(40, 28);
                    Console.Write( "Pending Appointments: " + pending);
                    Console.SetCursorPosition(80, 20);
                    Console.Write( "Veterinarian Contact: " + vetData[vetidx].contact);
                    Console.SetCursorPosition(80, 22);
                    Console.Write( "Veterinarian Email: " + vetData[vetidx].email);
                    Console.SetCursorPosition(80, 24);
                    Console.Write( "Offered Service: " + vetData[vetidx].service);
                    Console.SetCursorPosition(80, 26);
                    Console.Write( "Service Day: " + vetData[vetidx].day);
                    Console.SetCursorPosition(60, 32);
                    Console.Write( "Press any key to see next Veterinarian Details..");
                    Console.ReadKey();
                }
                else
                {
                    continue;
                }
            }
            if (y == 1)
            {
                Console.SetCursorPosition(60, 34);
                Console.Write( "                                               ");
                Console.SetCursorPosition(60, 34);
                Console.Write( "There is Only " + y + " Veterinarian");
            }
            else if (y > 1)
            {
                Console.SetCursorPosition(60, 34);
                Console.Write( "                                               ");
                Console.SetCursorPosition(60, 34);
                Console.Write( "There are Only " + y + " Veterinarians");
            }
        }
        static void DeleteCustomerDetails(string[] ValidRole,
                                  string[] ValidUsername,
                                  string[] ValidPassword,
                                  ref int BookedAppointmentsTotal,
                                  ref int PendingAppointmentsTotal,
                                  ref int Appointmentidx,
                                  int Total,
                                  ref int Bookingidx,
                                  ref int countCustomers,
                                  ref int countUser,
                                  string[] ThreeUsers,
                                  string[] TimeAppointed,
                                  string[] DoctorAppointed,
                                  string[] DayAppointed,
                                  string[] BookedForPet,
                                  string[] Veterinarians,
                                  string[] PetOwnerName,
                                  string[] PetOwnerNumber,
                                  string[] PetOwnerEmail,
                                  string[] PetOwnerLocation,
                                  string[] PetTypeForAppointment,
                                  string[] PetName,
                                  string[] PetAge,
                                  string[] PetGender,
                                  string[] PetWeight,
                                  string[] Doctor,
                                  string[] DoctorName,
                                  string[] Time,
                                  string[] Day,
                                  ref int AppointmentsCount,
                                  string[] AppointmentBookOrPending,
                                  string[] CustomerName,
                                  string[] CustomerPassword,
                                  string[] BookedBy,
                                  string[] BookedByPassword,
                                  string[] ApprovalStatus,
                                  string[] ApprovedRoles,
                                  string[] ApprovedUsers,
                                  string[] ApprovedPwd,
                                  ref int signUpcount,
                                  ref int approved,
                                  ref int unapproved,
                                  int countVets)
        {
            int y = 0;
            int customeridx = 0;
            string Remove = "";
            int remove = 0;

            for (int z = 0; z < signUpData.Count ; z++)
            {
                if (signUpData[z].role == "Customer")
                {
                    AdminHeader();
                    y++;
                    Console.SetCursorPosition(40, 20);
                    
                    Console.Write("Customer Name: " + signUpData[z].username);
                    Console.SetCursorPosition(40, 22);
                    
                    Console.Write("Customer Account Password: " + signUpData[z].password);
                    int total = TotalAppointmentsByOneUser(appointmentData, signUpData[z].username);
                    int booked = CountBookedAppointmentsForCustomer(appointmentData, customerData, z);
                    Console.SetCursorPosition(40, 24);
                    
                    Console.Write("Total Appointments : " + total);
                    Console.SetCursorPosition(40, 26);
                    
                    Console.Write("Booked Appointments: " + booked);
                    Console.SetCursorPosition(40, 28);
                    
                    Console.Write("Pending Appointments: " + (total - booked));

                    if (total > 0)
                    {
                        int appointmentIndex = Bookingindex(appointmentData, signUpData[z].username);
                        Console.SetCursorPosition(40, 30);
                        
                        Console.Write("Customer Contact: " + appointmentData[appointmentIndex].petOwnerName);
                        Console.SetCursorPosition(80, 20);
                        
                        Console.Write("Customer Email: " + appointmentData[appointmentIndex].petOwnerEmail);
                        Console.SetCursorPosition(80, 22);
                        
                        Console.Write("Customer Location: " + appointmentData[appointmentIndex].petOwnerLocation);
                        Console.SetCursorPosition(80, 24);
                        
                        Console.Write("Customer Pet Type: " + appointmentData[appointmentIndex].petType);
                        Console.SetCursorPosition(80, 26);
                        
                        Console.Write("Customer Pet Name: " + appointmentData[appointmentIndex].petName);
                        Console.SetCursorPosition(80, 28);
                        
                        Console.Write("Customer Pet Age: " + appointmentData[appointmentIndex].petAge);
                    }

                    Console.SetCursorPosition(80, 32);
                    Console.Write("1. Yes     2. No");
                    Console.SetCursorPosition(80, 30);
                    Console.Write("Remove this Customer...?");
                    Remove = Console.ReadLine();

                    while (Remove != "1" && Remove != "2")
                    {
                        Console.SetCursorPosition(60, 32);
        
                        Console.Write("Please Enter Correct Option! ");
                        Console.SetCursorPosition(80, 30);
                        Console.Write("                                          ");
                        Console.SetCursorPosition(80, 30);
                        Console.Write("Remove this Customer...?");
                        Remove = Console.ReadLine();
                        Console.SetCursorPosition(60, 32);
                        Console.Write("                                          ");
                    }

                    remove = int.Parse(Remove);

                    if (remove == 2)
                    {
                        continue;
                    }
                    else if (remove == 1)
                    {
                        if(total>0)
                        {
                            customeridx = Bookingindex(appointmentData, signUpData[z].username);

                            appointmentData.RemoveAt(customeridx);
                        }
                        

                        checkApprovedUserIndex(signUpcount, ref customeridx, signUpData[z].username, signUpData[z].password, approvedUsersData);

                        if (approvedUsersData[customeridx].status == "Approved")
                        {
                            approved--;
                        }
                        else if (approvedUsersData[customeridx].status == "Unapproved")
                        {
                            unapproved--;
                        }
                        approvedUsersData.RemoveAt(customeridx);

                        checkCustomerIndex( ref customeridx, signUpData[z].username, signUpData[z].password, customerData);
                        customerData.RemoveAt(customeridx);

                        signUpData.RemoveAt(z);

                        
                        
                        AdminHeader();
                        Console.SetCursorPosition(71, 24);
                        Console.Write("                                               ");
                        Console.SetCursorPosition(71, 24);
                        
                        Console.Write("Customer deleted successfully..!");
                        return;
                    }
                }
                else
                {
                    continue;
                }

                y = z;
            }

            Console.SetCursorPosition(71, 34);
            Console.Write("                                               ");
            Console.SetCursorPosition(71, 34);
            
            Console.Write("No more Customers...");
        }

        static void DeleteVetDetails(ref int countVeterinarians, int countCustomers, int vetidx, string[] ValidUsername, string[] ValidRole, string[] ValidPassword, ref int countUser, int idx, string[] ThreeUsers, ref int countVets,
                              string[] VetName, string[] VetPassword, string[] VetEmail, string[] VetContact, string[] VetDays,
                              string[] VetService, ref int signUpcount, string[] DoctorAppointed, int AppointmentsCount, string[] AppointmentBookOrPending,
                              string[] ApprovalStatus, string[] ApprovedRoles, string[] ApprovedUsers, string[] ApprovedPwd)
        {
            int y = 0;
            string Remove = "";
            int remove = 0;
            for (int z = 0; z < signUpData.Count ; z++)
            {
                if (signUpData[z].role == "Veterinarian")
                {
                    AdminHeader();
                    y++;
                    Console.SetCursorPosition(40, 20);
                    
                    Console.Write("Veterinarian Name: " + signUpData[z].username);
                    Console.SetCursorPosition(40, 22);
                    
                    Console.Write("Veterinarian Account Password: " + signUpData[z].password);
                    checkVetIndex(countVets, ref vetidx, signUpData[z].username, signUpData[z].password, vetData);
                    int pending = CountPendingAppointmentsForVet(appointmentData, vetData, vetidx);
                    int booked = CountBookedAppointmentsForVet(appointmentData, vetData, vetidx);
                    Console.SetCursorPosition(40, 24);
                    
                    Console.Write("Total Appointments : " + (booked + pending));
                    Console.SetCursorPosition(40, 26);
                    
                    Console.Write("Booked Appointments: " + booked);
                    Console.SetCursorPosition(40, 28);
                    
                    Console.Write("Pending Appointments: " + pending);
                    Console.SetCursorPosition(80, 20);
                    
                    Console.Write("Veterinarian Contact: " + vetData[vetidx].contact);
                    Console.SetCursorPosition(80, 22);
                    
                    Console.Write("Veterinarian Email: " + vetData[vetidx].email);
                    Console.SetCursorPosition(80, 24);
                    
                    Console.Write("Offered Service: " + vetData[vetidx].service);
                    Console.SetCursorPosition(80, 26);
                    
                    Console.Write("Service Day: " + vetData[vetidx].day);
                    Console.SetCursorPosition(80, 32);
                    Console.Write("1. Yes     2. No");
                    Console.SetCursorPosition(80, 30);
                    Console.Write("Remove this Veterinarian...?");
                    Remove = Console.ReadLine();
                    while (Remove != "1" && Remove != "2")
                    {
                        Console.SetCursorPosition(60, 34);
        
                        Console.Write("Please Enter Correct Option! ");
                        Console.SetCursorPosition(80, 30);
                        Console.Write("                                              ");
                        Console.SetCursorPosition(80, 30);
                        Console.Write("Remove this Veterinarian...?");
                        Remove = Console.ReadLine();
                        Console.SetCursorPosition(60, 34);
                        Console.Write("                                          ");
                    }
                    remove = int.Parse(Remove);
                    if (remove == 2)
                    {
                        continue;
                    }
                    else if (remove == 1)
                    {
                        checkApprovedUserIndex(signUpcount, ref vetidx, signUpData[z].username, signUpData[z].password,approvedUsersData);
                       
                        approvedUsersData.RemoveAt(vetidx);

                        checkVetIndex(countVets, ref vetidx, signUpData[z].username, signUpData[z].password, vetData);
                        vetData.RemoveAt(vetidx);

                        checkIndexInSignUpList(ref vetidx, signUpData[z].username, signUpData[z].password, signUpData);
                        signUpData.RemoveAt(vetidx);

                        countVeterinarians--;
                        countVets--;
                        countUser--;
                        signUpcount--;
                        Console.ReadKey();
                    }
                    continue;
                }
                else
                {
                    continue;
                }
            }
            if (y == 1)
            {
                Console.SetCursorPosition(60, 34);
                Console.Write("                                               ");
                Console.SetCursorPosition(60, 34);
                
                Console.Write("No more Veterinarians...");
            }
            else if (y > 1)
            {
                Console.SetCursorPosition(60, 34);
                Console.Write("                                               ");
                Console.SetCursorPosition(60, 34);
                
                Console.Write("No more Veterinarians...");
            }
        }
        static int ViewAppointmentsMenu()
        {
            Console.SetCursorPosition(1, 19);
            Console.Write("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------");

            Console.SetCursorPosition(71, 21);
            Console.Write("1. Pending  Appointments");
            Console.SetCursorPosition(71, 23);
            Console.Write("2. Booked Appointments");
            Console.SetCursorPosition(71, 25);
            Console.Write("3. Delete Appointments");
            Console.SetCursorPosition(71, 27);
            Console.Write("4. Exit");

            Console.SetCursorPosition(1, 29);
            Console.Write("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------");

            Console.SetCursorPosition(71, 31);
            Console.Write("Enter Your Requirement: ");
            string option = Console.ReadLine();

            while (option != "1" && option != "2" && option != "3" && option != "4")
            {
                Console.SetCursorPosition(71, 33);

                Console.Write("Please Enter Correct Option!");


                Console.SetCursorPosition(71, 31);
                Console.Write("                                                   ");

                Console.SetCursorPosition(71, 33);
                Console.Write("                                                 ");

                Console.SetCursorPosition(71, 31);
                Console.Write("Enter Your Requirement: ");
                option = Console.ReadLine();
            }

            int adminRequirement = int.Parse(option);
            return adminRequirement;
        }
        static void ShowPendingAppointmentsTOAdmin(string showingStatus, string[] DoctorAppointed, string[] DayAppointed,
        string[] PetOwnerName, string[] PetOwnerNumber, string[] PetOwnerEmail,
        string[] PetOwnerLocation, string[] PetTypeForAppointment, string[] PetName,
        string[] PetAge, string[] PetGender, string[] PetWeight, string[] Doctor,
        string[] Time, string[] Day, ref int AppointmentsCount, string[] AppointmentBookOrPending)
        {
            int y = 0;
            for (int z = 0; z < appointmentData.Count; z++)
            {
                AdminHeader();
                if ( appointmentData[z].status == "Pending")
                {
                    y++;
                    Console.SetCursorPosition(40, 20);
                    
                    Console.Write("Pet Owner Name: " +  appointmentData[z].petOwnerName);
                    Console.SetCursorPosition(40, 22);
                    
                    Console.Write("Pet Owner Contact: " +  appointmentData[z].petOwnerNumber);
                    Console.SetCursorPosition(40, 24);
                    
                    Console.Write("Pet Owner Email: " +  appointmentData[z].petOwnerEmail);
                    Console.SetCursorPosition(40, 26);
                    
                    Console.Write("Pet Owner Location: " +  appointmentData[z].petOwnerLocation);
                    Console.SetCursorPosition(40, 28);
                    
                    Console.Write("Pet Type: " +  appointmentData[z].petType);
                    Console.SetCursorPosition(80, 20);
                    
                    Console.Write("Pet Name: " +  appointmentData[z].petName);
                    Console.SetCursorPosition(80, 22);
                    
                    Console.Write("Pet Age : " +  appointmentData[z].petAge + " Months");
                    Console.SetCursorPosition(80, 24);
                    
                    Console.Write("Pet Weight: " +  appointmentData[z].petWeight + " Pounds");
                    Console.SetCursorPosition(80, 26);
                    
                    Console.Write("Pet Gender: " +  appointmentData[z].petGender);
                    Console.SetCursorPosition(80, 28);
                    
                    Console.Write("Appointment For: " +  appointmentData[z].doctor);
                    Console.SetCursorPosition(40, 30);
                    
                    Console.Write("Appointment Status: " +  appointmentData[z].status);
                    Console.SetCursorPosition(80, 30);
                    
                    Console.Write("Day For Appointment: " +  appointmentData[z].day);
                    Console.SetCursorPosition(60, 32);
                    Console.Write("Press any key to see the next Appointment..");
                    Console.ReadKey();
                }
            }

            if (y == 1)
            {
                Console.SetCursorPosition(60, 24);
                Console.Write("                                               ");
                Console.SetCursorPosition(60, 24);
                
                Console.Write("There is Only " + y + " Pending Appointment");
            }
            else if (y > 1)
            {
                Console.SetCursorPosition(60, 34);
                Console.Write("                                               ");
                Console.SetCursorPosition(60, 34);
                
                Console.Write("There are Only " + y + " Pending Appointments");
            }
        }

        static void ShowBookedAppointmentsTOAdmin(string showingStatus, string[] DoctorAppointed, string[] DayAppointed,
                                          string[] PetOwnerName, string[] PetOwnerNumber, string[] PetOwnerEmail,
                                          string[] PetOwnerLocation, string[] PetTypeForAppointment, string[] PetName,
                                          string[] PetAge, string[] PetGender, string[] PetWeight, string[] Doctor,
                                          string[] Time, string[] Day, ref int AppointmentsCount,
                                          string[] AppointmentBookOrPending)
        {
            int y = 0;

            for (int z = 0; z < appointmentData.Count; z++)
            {
                if ( appointmentData[z].status == "Booked")
                {
                    AdminHeader();
                    y++;

                    Console.SetCursorPosition(40, 20);
                    
                    Console.Write("Pet Owner Name: " +  appointmentData[z].petOwnerName);

                    Console.SetCursorPosition(40, 22);
                    
                    Console.Write("Pet Owner Contact: " +  appointmentData[z].petOwnerNumber);

                    Console.SetCursorPosition(40, 24);
                    
                    Console.Write("Pet Owner Email: " +  appointmentData[z].petOwnerEmail);

                    Console.SetCursorPosition(40, 26);
                    
                    Console.Write("Pet Owner Location: " + appointmentData[z].petOwnerLocation);

                    Console.SetCursorPosition(40, 28);
                    
                    Console.Write("Pet Type: " +  appointmentData[z].petType);

                    Console.SetCursorPosition(80, 20);
                    
                    Console.Write("Pet Name: " +  appointmentData[z].petName);

                    Console.SetCursorPosition(80, 22);
                    
                    Console.Write("Pet Age: " +  appointmentData[z].petAge + " Months");

                    Console.SetCursorPosition(80, 24);
                    
                    Console.Write("Pet Weight: " +  appointmentData[z].petWeight + " Pounds");

                    Console.SetCursorPosition(80, 26);
                    
                    Console.Write("Pet Gender: " +  appointmentData[z].petGender);

                    Console.SetCursorPosition(80, 28);
                    
                    Console.Write("Appointment For: " +  appointmentData[z].doctor);

                    Console.SetCursorPosition(40, 30);
                    
                    Console.Write("Appointment Status: " +  appointmentData[z].status);

                    Console.SetCursorPosition(80, 30);
                    
                    Console.Write("Day For Appointment: " +  appointmentData[z].day);

                    Console.SetCursorPosition(60, 32);
                    Console.Write("Press any key to see next Appointment..");
                    Console.ReadKey();
                }
            }

            if (y == 1)
            {
                Console.SetCursorPosition(60, 34);
                Console.Write("                                               ");
                Console.SetCursorPosition(60, 34);
                
                Console.Write("There is Only " + y + " Booked Appointment");
            }
            else if (y > 1)
            {
                Console.SetCursorPosition(60, 34);
                Console.Write("                                               ");
                Console.SetCursorPosition(60, 34);
                
                Console.Write("There are Only " + y + " Booked Appointments");
            }
        }

        static void DeleteAppointments(ref int Bookingidx, ref int BookedAppointmentsTotal, ref int PendingAppointmentsTotal, string[] DoctorAppointed, string[] DayAppointed,
                         string[] PetOwnerName,
                         string[] PetOwnerNumber,
                         string[] PetOwnerEmail,
                         string[] PetOwnerLocation,
                         string[] PetTypeForAppointment,
                         string[] PetName,
                         string[] PetAge,
                         string[] PetGender,
                         string[] PetWeight,
                         string[] Doctor,
                         string[] Time,
                         string[] Day, ref int AppointmentsCount,
                         string[] AppointmentBookOrPending, string[] BookedBy, string[] BookedForPet, string[] BookedByPassword, ref int Appointementdx, string[] DoctorName)
        {
            string Confirmation = "";
            int confirmation = 0;
            int x = 0;
            int z = 0;
            for (; z < appointmentData.Count; z++)
            {
                x++;
                AdminHeader();
                Console.SetCursorPosition(40, 20);
                
                Console.Write("Pet Owner Name: " +  appointmentData[z].petOwnerName);
                Console.SetCursorPosition(40, 22);
                
                Console.Write("Pet Owner Contact: " +  appointmentData[z].petOwnerNumber);
                Console.SetCursorPosition(40, 24);
                
                Console.Write("Pet Owner Email: " +  appointmentData[z].petOwnerEmail);
                Console.SetCursorPosition(40, 26);
                
                Console.Write("Pet Owner Location: " +  appointmentData[z].petOwnerLocation);
                Console.SetCursorPosition(40, 28);
                
                Console.Write("Pet Type: " +  appointmentData[z].petType);
                Console.SetCursorPosition(80, 20);
                
                Console.Write("Pet Name: " +  appointmentData[z].petName);
                Console.SetCursorPosition(80, 22);
                
                Console.Write("Pet Age : " +  appointmentData[z].petAge + " Months");
                Console.SetCursorPosition(80, 24);
                
                Console.Write("Pet Weight: " +  appointmentData[z].petWeight + " Pounds");
                Console.SetCursorPosition(80, 26);
                
                Console.Write("Pet Gender: " +  appointmentData[z].petGender);
                Console.SetCursorPosition(80, 28);
                
                Console.Write("Appointment For: " +  appointmentData[z].doctor);
                Console.SetCursorPosition(40, 30);
                
                Console.Write("Appointment Status: " +  appointmentData[z].status);
                Console.SetCursorPosition(80, 30);
                Console.SetCursorPosition(50, 34);
                Console.Write("                                                   ");
                Console.SetCursorPosition(50, 36);
                Console.Write("1. Yes    2. No");
                Console.SetCursorPosition(50, 34);
                
                Console.Write("Do You Want To Delete this Appointment? ");
                Confirmation = Console.ReadLine();
                while (Confirmation != "1" && Confirmation != "2")
                {
                    Console.SetCursorPosition(50, 38);
    
                    Console.Write("Please Enter Correct Option! ");
                    Console.SetCursorPosition(50, 34);
                    Console.Write("                                                                        ");
                    Console.SetCursorPosition(50, 34);
                    
                    Console.Write("Do You Want To Delete this Appointment? ");
                    Confirmation = Console.ReadLine();
                    Console.SetCursorPosition(50, 38);
                    Console.Write("                                                    ");
                }
                confirmation = int.Parse(Confirmation);
                if (confirmation == 2)
                {
                    continue;
                }
                else if (confirmation == 1)
                {
                    if (appointmentData[z].status == "Pending")
                    {
                        PendingAppointmentsTotal--;
                    }
                    else if (appointmentData[z].status == "Booked")
                    {
                        BookedAppointmentsTotal--;
                    }
                    appointmentData.RemoveAt(z);

                    AppointmentsCount--;
                    continue;
                }
            }
            if (x == 1)
            {
                Console.SetCursorPosition(50, 38);
                Console.Write("                                               ");
                Console.SetCursorPosition(50, 38);
                
                Console.Write("There was Only " + x + " Appointment");
            }
            else if (x > 1)
            {
                Console.SetCursorPosition(50, 38);
                Console.Write("                                               ");
                Console.SetCursorPosition(50, 38);
                
                Console.Write("There were Only " + x + " Appointments");
            }
        }

        // VETERINARIAN //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        static void VetMainMenu(string signInUserName, string[] CustomerName, string signInPassword,
       string[] ValidRole, int valididx, int useridx, ref int BookedAppointmentsTotal,
       ref int PendingAppointmentsTotal, int vetidx, string[] ValidUsername, string[] ValidPassword,
       ref int countUser, int idx, string[] ThreeUsers, ref int countVets, string[] VetName,
       string[] VetPassword, string[] VetEmail, string[] VetContact, string[] VetDays,
       string[] VetService, string[] AvailableDays, string[] AvailableService,
       string[] DoctorAppointed, string[] DayAppointed, string[] DoctorName,
       string[] PetOwnerName, string[] PetOwnerNumber, string[] PetOwnerEmail,
       string[] PetOwnerLocation, string[] PetTypeForAppointment, string[] PetName,
       string[] PetAge, string[] PetGender, string[] PetWeight, string[] Doctor,
       string[] Time, string[] Day, ref int AppointmentsCount, string[] AppointmentBookOrPending,
       string[] BookedBy, string VetFile, ref int countVeterinarians, string[] BookedByPassword,
       string AppointmentFile, string SignUpFile, int[] Roles, string[] ApprovalStatus,
       string[] ApprovedUsers, string[] ApprovedPwd, string[] ApprovedRoles, ref int signUpcount,
       string ApprovedFile, int customeridx)
        {

            int VetRequirement = 0;
            int ProfileOption = 0;
            int AppointmentOption = 0;
            int del = 0;
            int confirmation = 0;
            int BookedByCurrentVet = 0;
            string Confirmation;
            string ProfileStatus = "Exists";
            while (true)
            {
                printHeader();
                VetRequirement = PrintVetMenu();
                if (VetRequirement == 1)
                {
                    while (true)
                    {
                        VetHeader();
                        ProfileOption = VetProfileOptions();
                        if (ProfileOption == 1)
                        {
                            VetHeader();
                            ShowVetProfile(vetidx, countVets, VetName, VetPassword, VetEmail, VetContact, VetDays, VetService);
                            Console.ReadKey();
                        }
                        else if (ProfileOption == 2)
                        {
                            VetHeader();
                            EditVetProfile(useridx, vetidx, countVets, countUser, ValidUsername, ValidPassword, VetName, VetPassword, VetEmail, VetContact, VetDays, VetService, AvailableDays, AvailableService);
                            ValidPassword[valididx] = VetPassword[vetidx];
                            ValidUsername[valididx] = VetName[vetidx];
                            ApprovedUsers[useridx] = VetName[vetidx];
                            ApprovedPwd[useridx] = VetPassword[vetidx];
                            saveVetDetails(vetData, appointmentData, VetFile, vetidx);
                            saveApprovedUsers(approvedUsersData, ApprovedFile);
                            saveSignUpDetails(signUpData, SignUpFile);
                            Console.ReadKey();
                        }
                        else if (ProfileOption == 3)
                        {
                            VetHeader();
                            del = ConfirmDeletion();
                            if (del == 1)
                            {
                                DeleteVetProfile(signInUserName,
                                                 signInPassword, Roles, ValidRole, ref signUpcount, useridx, ApprovedUsers, ApprovedPwd, ApprovalStatus, ApprovedRoles,
                                                 valididx, ref countVeterinarians, vetidx, ref countVets, ref countUser, ValidUsername, ValidPassword, VetName, VetPassword, VetEmail,
                                                 VetContact, VetDays, VetService, AvailableDays, AvailableService);
                                saveVetDetails(vetData, appointmentData, VetFile, vetidx);
                                saveApprovedUsers(approvedUsersData, ApprovedFile);
                                saveSignUpDetails(signUpData, SignUpFile);
                                ProfileStatus = "Deleted";
                                break;
                            }
                            else if (del == 2)
                            {
                                continue;
                            }
                        }
                        else if (ProfileOption == 4)
                        {
                            break;
                        }
                        if (ProfileStatus == "Deleted")
                        {
                            break; // checking if the user has deleted the profile or not
                        }
                    }
                }
                if (VetRequirement == 2)
                {
                    while (true)
                    {
                        VetHeader();
                        AppointmentOption = VetAppointmentOptions();
                        if (AppointmentOption == 1)
                        {
                            VetHeader();
                            int Pending = CountPendingAppointmentsForVet(appointmentData, vetData, vetidx);
                            if (Pending > 0)
                            {
                                Console.SetCursorPosition(50, 26);
                                Console.Write("                                                   ");
                                Console.SetCursorPosition(50, 26);
                                Console.Write("1. Yes    2. No");
                                Console.SetCursorPosition(50, 22);
                                Console.Write("There are " + Pending + " Pending Appointments");
                                Console.SetCursorPosition(50, 24);
                                Console.Write("Do you want to see details? ");
                                Confirmation = Console.ReadLine();
                                while (Confirmation != "1" && Confirmation != "2")
                                {
                                    Console.SetCursorPosition(50, 38);
                                    Console.Write("Please Enter Correct Option! ");
                                    Console.SetCursorPosition(50, 34);
                                    Console.Write("                                                   ");
                                    Console.SetCursorPosition(50, 24);
                                    Console.Write("Do you want to see details? ");
                                    Confirmation = Console.ReadLine();
                                }
                                confirmation = int.Parse(Confirmation);
                                if (confirmation == 1)
                                {
                                    VetHeader();
                                    ViewPendingAppointments(CustomerName, ref BookedAppointmentsTotal, DoctorAppointed, DayAppointed,
                                                            PetOwnerName,
                                                            PetOwnerNumber,
                                                            PetOwnerEmail,
                                                            PetOwnerLocation,
                                                            PetTypeForAppointment,
                                                            PetName,
                                                            PetAge,
                                                            PetGender,
                                                            PetWeight,
                                                            Doctor,
                                                            Time,
                                                            Day, AppointmentsCount,
                                                            AppointmentBookOrPending, BookedBy, vetidx, VetService, ref BookedByCurrentVet, VetDays, VetName, DoctorName,
                                                            ValidUsername, BookedByPassword, AppointmentFile, VetPassword, VetEmail, VetContact, countVets, countVeterinarians,
                                                            VetFile, customeridx);
                                    saveCustomerAppointments(customerData, appointmentData, AppointmentFile, customeridx);
                                    saveVetDetails(vetData, appointmentData, VetFile,  vetidx);
                                }
                            }
                            else
                            {
                                Console.SetCursorPosition(50, 22);
                                Console.Write("There are no Pending Appointments");
                                Console.SetCursorPosition(50, 24);
                                Console.Write("Press any key to continue...");
                            }
                            Console.ReadKey();
                        }
                        else if (AppointmentOption == 2)
                        {
                            VetHeader();
                            int Booked = CountBookedAppointmentsForVet(appointmentData, vetData, vetidx);
                            if (Booked > 0)
                            {
                                Console.SetCursorPosition(50, 26);
                                Console.Write("                                                   ");
                                Console.SetCursorPosition(50, 26);
                                Console.Write("1. Yes    2. No");
                                Console.SetCursorPosition(50, 22);
                                Console.Write("There are " + Booked + " Booked Appointments");
                                Console.SetCursorPosition(50, 24);
                                Console.Write("Do you want to see details? ");
                                Confirmation = Console.ReadLine();
                                while (Confirmation != "1" && Confirmation != "2")
                                {
                                    Console.SetCursorPosition(50, 38);
                                    Console.Write("Please Enter Correct Option! ");
                                    Console.SetCursorPosition(50, 34);
                                    Console.Write("                                                   ");
                                    Console.SetCursorPosition(50, 24);
                                    Console.Write("Do you want to see details? ");
                                    Confirmation = Console.ReadLine();
                                }
                                confirmation = int.Parse(Confirmation);
                                if (confirmation == 1)
                                {
                                    VetHeader();
                                    ViewBookedAppointments(DoctorAppointed, DayAppointed,
                                                           PetOwnerName,
                                                           PetOwnerNumber,
                                                           PetOwnerEmail,
                                                           PetOwnerLocation,
                                                           PetTypeForAppointment,
                                                           PetName,
                                                           PetAge,
                                                           PetGender,
                                                           PetWeight,
                                                           Doctor,
                                                           Time,
                                                           Day, AppointmentsCount,
                                                           AppointmentBookOrPending, BookedBy, vetidx, VetService, ref BookedByCurrentVet, VetDays);
                                }
                            }
                            else
                            {
                                Console.SetCursorPosition(50, 22);
                                Console.Write("You have not booked any Appointment!");
                                Console.SetCursorPosition(50, 24);
                                Console.Write("Press any key to continue...");
                            }
                            Console.ReadKey();
                        }
                        else if (AppointmentOption == 3)
                        {
                            break;
                        }
                    }
                    Console.ReadKey();
                }
                else if (VetRequirement == 3)
                {
                    break;
                }
                if (ProfileStatus == "Deleted")
                {
                    break; // checking if the user has deleted the profile so go to login screen
                }
            }
        }
        
        static void CreateProfile(ref string UserName, ref string Password, ref string Email, ref string Contact, ref int VetDay, ref int VetService)
        {
            string Day = "";
            string Service = "";
            Console.SetCursorPosition(60, 20);
            Console.Write("Username: " + UserName);
            Console.SetCursorPosition(60, 22);
            Console.Write("Password: " + Password);
            Console.SetCursorPosition(60, 24);
            Console.Write("Email: ");
            Email = Console.ReadLine();
            Console.SetCursorPosition(60, 26);
            Console.Write("Contact: ");
            Contact = Console.ReadLine();
            while (Contact.Length != 11)
            {
                Console.SetCursorPosition(60, 28);
                Console.Write("Please add valid Contact Number of length 11!");
                Console.SetCursorPosition(60, 26);
                Console.Write("                                           ");
                Console.SetCursorPosition(60, 26);
                Console.Write("Contact: ");
                Contact = Console.ReadLine();
                Console.SetCursorPosition(60, 28);
                Console.Write("                                                ");
            }
            while (!(checkContactValidity(Contact)))
            {
                Console.SetCursorPosition(60, 28);
                Console.Write("Contact Number can only contain digits!");
                Console.SetCursorPosition(60, 26);
                Console.Write("                                           ");
                Console.SetCursorPosition(60, 26);
                Console.Write("Contact: ");
                Contact = Console.ReadLine();
                Console.SetCursorPosition(60, 28);
                Console.Write("                                                ");
            }
            Console.SetCursorPosition(90, 22);
            Console.Write("1. Dermatology");
            Console.SetCursorPosition(90, 24);
            Console.Write("2. Primary Care");
            Console.SetCursorPosition(90, 26);
            Console.Write("3. Internal Medicine");
            Console.SetCursorPosition(90, 20);
            Console.Write("Providing Service For: ");
            Service = Console.ReadLine();
            while (Service != "1" && Service != "2" && Service != "3")
            {
                Console.SetCursorPosition(90, 28);
                Console.Write("Please Enter Correct Option! ");
                Console.SetCursorPosition(90, 20);
                Console.Write("                                        ");
                Console.SetCursorPosition(90, 20);
                Console.Write("Providing Service For: ");
                Console.SetCursorPosition(90, 28);
                Console.Write("                                             ");
            }
            VetService = int.Parse(Service);
            Console.SetCursorPosition(60, 30);
            Console.Write("1.Monday   2.Tuesday   3.Wednesday");
            Console.SetCursorPosition(60, 32);
            Console.Write("4.Thursday 5.Friday    6.Saturday");
            Console.SetCursorPosition(60, 28);
            Console.Write("Day Availablily: ");
            Day = Console.ReadLine();
            while (Day != "1" && Day != "2" && Day != "3" && Day != "4" && Day != "5" && Day != "6")
            {
                Console.SetCursorPosition(60, 34);
                Console.Write("Please Enter Correct Option! ");
                Console.SetCursorPosition(60, 28);
                Console.Write("                                          ");
                Console.SetCursorPosition(60, 28);
                Console.Write("Day Availablily: ");
                Day = Console.ReadLine();
                Console.SetCursorPosition(60, 34);
                Console.Write("                                              ");
            }
            VetDay = int.Parse(Day);
        }
        
        static void Vet_MenuBar()
        {
            Console.SetCursorPosition(1, 15);
            Console.Write("___________________________________________________________________________________________________________________________________________________________________________");

            Console.SetCursorPosition(0, 17);
            Console.Write("                                                          ``.....    V e t e r i n a r i a n   .....``");

            Console.SetCursorPosition(1, 18);
            Console.Write("___________________________________________________________________________________________________________________________________________________________________________");
        }
        static int PrintVetMenu()
        {
            string option;
            Console.SetCursorPosition(1, 17);
            Console.Write("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------\n");

            Console.SetCursorPosition(71, 19);
            Console.Write("1. Profile Options");
            Console.SetCursorPosition(71, 21);
            Console.Write("2. Appointments");
            Console.SetCursorPosition(71, 23);
            Console.Write("3. Logout");

            Console.SetCursorPosition(1, 25);
            Console.Write("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------\n");

            Console.SetCursorPosition(71, 27);
            Console.Write("Enter Your Requirement: ");
            option = Console.ReadLine();

            while (option != "1" && option != "2" && option != "3")
            {
                Console.SetCursorPosition(71, 29);
                Console.Write("Please Enter Correct Option!");
                Console.SetCursorPosition(71, 27);
                Console.Write(new string(' ', Console.WindowWidth - 71));
                Console.SetCursorPosition(71, 27);
                Console.Write("Enter Your Requirement: ");
                option = Console.ReadLine();
            }

            int vetRequirement = int.Parse(option);
            return vetRequirement;
        }
        static int VetProfileOptions()
        {
            Console.SetCursorPosition(1, 20);
            Console.Write("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------\n");
            Console.SetCursorPosition(71, 22);
            Console.Write("1. View Profile");
            Console.SetCursorPosition(71, 24);
            Console.Write("2. Edit Profile");
            Console.SetCursorPosition(71, 26);
            Console.Write("3. Delete Profile");
            Console.SetCursorPosition(71, 28);
            Console.Write("4. Exit");
            Console.SetCursorPosition(1, 30);
            Console.Write("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------\n");

            Console.SetCursorPosition(71, 32);
            Console.Write("Enter Your Requirement: ");

            string option = Console.ReadLine();

            while (option != "1" && option != "2" && option != "3" && option != "4")
            {
                Console.SetCursorPosition(71, 34);

                Console.Write("Please Enter Correct Option! ");


                Console.SetCursorPosition(71, 32);
                Console.Write("                                                 ");
                Console.SetCursorPosition(71, 32);
                Console.Write("Enter Your Requirement: ");
                option = Console.ReadLine();
                Console.SetCursorPosition(71, 34);
                Console.Write("                                                 ");
            }

            int vetRequirement = int.Parse(option);
            return vetRequirement;
        }

        static void ShowVetProfile(int vetidx, int countVets, string[] vetName, string[] vetPassword, string[] vetEmail,
                                    string[] vetContact, string[] vetDays, string[] vetService)
        {
            Console.SetCursorPosition(60, 20);
            Console.Write($"UserName: {vetData[vetidx].name}");
            Console.SetCursorPosition(60, 22);
            Console.Write($"Password: {vetData[vetidx].password}");
            Console.SetCursorPosition(60, 24);
            Console.Write($"Email: {vetData[vetidx].email}");
            Console.SetCursorPosition(60, 26);
            Console.Write($"Contact: {vetData[vetidx].contact}");
            Console.SetCursorPosition(60, 28);
            Console.Write($"Day: {vetData[vetidx].day}");
            Console.SetCursorPosition(60, 30);
            Console.Write($"Service: {vetData[vetidx].service}");
            Console.SetCursorPosition(60, 32);
            Console.Write("Press any key to continue...");
        }
        static void EditVetProfile(int useridx, int vetidx, int countVets, int countUser, string[] ValidUsername, string[] ValidPassword, string[] VetName, string[] VetPassword, string[] VetEmail,
                    string[] VetContact, string[] VetDays, string[] VetService, string[] AvailableDays, string[] AvailableService)
        {
            string Option;
            Console.SetCursorPosition(60, 20);
            Console.Write("1. UserName");
            Console.SetCursorPosition(60, 22);
            Console.Write("2. Password");
            Console.SetCursorPosition(60, 24);
            Console.Write("3. Email");
            Console.SetCursorPosition(60, 26);
            Console.Write("4. Contact");
            Console.SetCursorPosition(60, 28);
            Console.Write("5. Day");
            Console.SetCursorPosition(60, 30);
            Console.Write("6. Service");
            Console.SetCursorPosition(60, 32);
            Console.Write("Select option you want to edit...");
            Option = Console.ReadLine();
            while (Option != "1" && Option != "2" && Option != "3" && Option != "5" && Option != "6" && Option != "4")
            {
                Console.SetCursorPosition(71, 33);
                Console.Write("Please Enter Correct Option! ");
                Console.SetCursorPosition(60, 32);
                Console.Write("                                                       ");
                Console.SetCursorPosition(60, 32);
                Console.Write("Select option you want to edit...");
                Option = Console.ReadLine();
                Console.SetCursorPosition(71, 33);
                Console.Write("                                             ");
            }
            int SelectedOption = int.Parse(Option);
            if (SelectedOption == 1)
            {
                Console.SetCursorPosition(60, 20);
                Console.Write("               ");
                Console.SetCursorPosition(60, 20);
                Console.Write("Enter Username: ");
                vetData[vetidx].name = Console.ReadLine();
                Console.SetCursorPosition(60, 32);
                Console.Write("                                       ");
                Console.SetCursorPosition(60, 32);
                Console.Write("Username Edited!");
            }
            else if (SelectedOption == 2)
            {
                Console.SetCursorPosition(60, 22);
                Console.Write("                    ");
                Console.SetCursorPosition(60, 22);
                Console.Write("Enter Old Password: ");
                string oldpassword = Console.ReadLine();
                if (oldpassword == vetData[vetidx].password)
                {
                    Console.SetCursorPosition(60, 22);
                    Console.Write("                                        ");
                    Console.SetCursorPosition(60, 22);
                    Console.Write("Enter New Password: ");
                    vetData[vetidx].password = Console.ReadLine();
                    ;
                    while (vetData[vetidx].password.Length > 16 || vetData[vetidx].password.Length < 8)
                    {
                        Console.SetCursorPosition(60, 32);
                        Console.Write("                                ");
                        Console.SetCursorPosition(60, 32);
                        Console.Write("Password must Contain 8 to 16 characters! ");
                        Console.SetCursorPosition(90, 22);
                        Console.Write("                                                    ");
                        Console.SetCursorPosition(90, 22);
                        vetData[vetidx].password = Console.ReadLine();
                        ;
                    }
                }
                else
                {
                    Console.SetCursorPosition(60, 32);
                    Console.Write("                                ");
                    Console.SetCursorPosition(60, 32);
                    Console.Write("Incorrect Password! ");
                    Console.SetCursorPosition(60, 34);
                    Console.Write("Press any key to continue..");
                    Console.ReadKey();
                }
            }
            else if (SelectedOption == 3)
            {
                Console.SetCursorPosition(60, 24);
                Console.Write("               ");
                Console.SetCursorPosition(60, 24);
                Console.Write("Enter Email: ");

                vetData[vetidx].email = Console.ReadLine();
                Console.SetCursorPosition(60, 32);
                Console.Write("                                       ");
                Console.SetCursorPosition(60, 32);
                Console.Write("Email Edited!");
            }
            else if (SelectedOption == 4)
            {
                Console.SetCursorPosition(60, 26);
                Console.Write("               ");
                Console.SetCursorPosition(60, 26);
                Console.Write("Enter Contact: ");

                vetData[vetidx].contact = Console.ReadLine();
                ;
                Console.SetCursorPosition(60, 32);
                Console.Write("                                       ");
                Console.SetCursorPosition(60, 32);
                Console.Write("Contact Edited!");
            }
            else if (SelectedOption == 5)
            {
                Console.SetCursorPosition(60, 28);
                Console.Write("               ");
                Console.SetCursorPosition(60, 28);
                Console.Write("Select Day: ");
                Console.SetCursorPosition(60, 30);
                Console.Write("                                  ");
                Console.SetCursorPosition(60, 30);
                Console.Write("1.Monday  2.Tuesday   3.Wednesday");
                Console.SetCursorPosition(60, 30);
                Console.Write("                                  ");
                Console.SetCursorPosition(60, 30);
                Console.Write("4.Thursday  5.Friday   6.Saturday");
                string Day = Console.ReadLine();

                while (Day != "1" && Day != "2" && Day != "3" && Day != "4" && Day != "5" && Day != "6")
                {
                    Console.SetCursorPosition(60, 32);
                    Console.Write("                                       ");
                    Console.SetCursorPosition(60, 32);
                    Console.Write("Please Enter Correct Option! ");
                    Console.SetCursorPosition(82, 28);
                    Console.Write("                                             ");
                    Console.SetCursorPosition(82, 28);
                    Day = Console.ReadLine();
                }
                int EditedDay = int.Parse(Day);
                vetData[vetidx].day = AvailableDays[EditedDay - 1];
                Console.SetCursorPosition(60, 32);
                Console.Write("                                       ");
                Console.SetCursorPosition(60, 32);
                Console.Write("Day Edited!");
            }
            else if (SelectedOption == 6)
            {
                Console.SetCursorPosition(60, 30);
                Console.Write("               ");
                Console.SetCursorPosition(60, 30);
                Console.Write("Select Service: ");
                Console.SetCursorPosition(60, 32);
                Console.Write("                                                   ");
                Console.SetCursorPosition(60, 32);
                Console.Write("1.Dermatology  2.Primary Care  3.Internal Medicine ");
                Console.SetCursorPosition(86, 30);
                string Service = Console.ReadLine();
                while (Service != "1" && Service != "2" && Service != "3")
                {
                    Console.SetCursorPosition(60, 34);
                    Console.Write("                                       ");
                    Console.SetCursorPosition(60, 34);
                    Console.Write("Please Enter Correct Option! ");
                    Console.SetCursorPosition(86, 30);
                    Console.Write("                                             ");
                    Console.SetCursorPosition(86, 30);
                    Service = Console.ReadLine();
                }
                int EditedService = int.Parse(Service);
                vetData[vetidx].service = AvailableService[EditedService - 1];
            }
            Console.SetCursorPosition(60, 34);
            Console.Write("Press any key to continue!");
        }
        static void DeleteVetProfile(string signInUserName, string signInPassword, int[] Roles, string[] ValidRole, ref int signUpcount, int useridx, string[] ApprovedUsers, string[] ApprovedPwd, string[] ApprovalStatus, string[] ApprovedRoles, int valididx, ref int countVeterinarians, int vetidx, ref int countVets, ref int countUser, string[] ValidUsername, string[] ValidPassword, string[] VetName, string[] VetPassword, string[] VetEmail, string[] VetContact, string[] VetDays, string[] VetService, string[] AvailableDays, string[] AvailableService)
        {
            checkVetIndex(countVets, ref vetidx, signInUserName, signInPassword, vetData);
            vetData.RemoveAt(vetidx);

            checkIndexInSignUpList( ref vetidx, signInUserName, signInPassword, signUpData);
            signUpData.RemoveAt(vetidx);

            checkApprovedUserIndex(signUpcount, ref vetidx, signInUserName, signInPassword, approvedUsersData);
            approvedUsersData.RemoveAt(vetidx);

            countVets--;
            countUser--;
            countVeterinarians--;
            signUpcount--;
        }
        static int ConfirmDeletion()
        {
            string option;
            Console.SetCursorPosition(55, 22);
            Console.Write("???????????????????????????????????????????????????");
            Console.SetCursorPosition(55, 23);
            Console.Write("|                                                 |");
            Console.SetCursorPosition(55, 24);
            Console.Write("|         Delete Your PetPal Profile?             |");
            Console.SetCursorPosition(55, 25);
            Console.Write("|                                                 |");
            Console.SetCursorPosition(55, 26);
            Console.Write("|              1. YES      2. NO                  |");
            Console.SetCursorPosition(55, 27);
            Console.Write("|                                                 |");
            Console.SetCursorPosition(55, 28);
            Console.Write("|                                                 |");
            Console.SetCursorPosition(55, 29);
            Console.Write("|                Enter Option...                  |");
            Console.SetCursorPosition(55, 30);
            Console.Write("|                                                 |");
            Console.SetCursorPosition(55, 31);
            Console.Write("???????????????????????????????????????????????????");
            Console.SetCursorPosition(87, 29);
            option = Console.ReadLine();
            while (option != "1" && option != "2")
            {
                Console.SetCursorPosition(55, 34);
                Console.Write("Please Enter Correct Option! ");
                Console.SetCursorPosition(87, 29);
                Console.Write("            ");
                Console.SetCursorPosition(87, 29);
                option = Console.ReadLine();
                Console.SetCursorPosition(55, 34);
                Console.Write("                                             ");
            }
            int Option = int.Parse(option);
            return Option;
        }
        static int VetAppointmentOptions()
        {
            Console.SetCursorPosition(1, 20);
            Console.Write("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            Console.SetCursorPosition(71, 22);
            Console.Write("1. View Pending Appointments");
            Console.SetCursorPosition(71, 24);
            Console.Write("2. View Booked Appointments");
            Console.SetCursorPosition(71, 26);
            Console.Write("3. Exit");
            Console.SetCursorPosition(1, 28);
            Console.Write("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            string Option;
            Console.SetCursorPosition(71, 30);
            Console.Write("Enter Your Requirement: ");

            Option = Console.ReadLine();
            while (Option != "1" && Option != "2" && Option != "3")
            {
                Console.SetCursorPosition(71, 32);
                Console.Write("Please Enter Correct Option!");
                Console.SetCursorPosition(71, 30);
                Console.Write("                                        ");
                Console.SetCursorPosition(71, 30);
                Console.Write("Enter Your Requirement: ");
                Option = Console.ReadLine();
                Console.SetCursorPosition(71, 32);
                Console.Write("                                        ");
            }
            int Vet_Requirement = int.Parse(Option);
            return Vet_Requirement;
        }
        static void ViewPendingAppointments(string[] CustomerName, ref int BookedAppointmentsTotal, string[] DoctorAppointed, string[] DayAppointed, string[] PetOwnerName, string[] PetOwnerNumber,
                                        string[] PetOwnerEmail, string[] PetOwnerLocation, string[] PetTypeForAppointment, string[] PetName, string[] PetAge, string[] PetGender,
                                        string[] PetWeight, string[] Doctor, string[] Time, string[] Day, int AppointmentsCount, string[] AppointmentBookOrPending, string[] BookedBy,
                                        int vetidx, string[] VetService, ref int BookedByCurrentVet, string[] VetDays, string[] VetName, string[] DoctorName, string[] ValidUsername,
                                        string[] BookedByPassword, string AppointmentFile, string[] VetPassword, string[] VetEmail, string[] VetContact, int countVets, int countVeterinarians,
                                        string VetFile, int customeridx)
        {
            string Confirmation = "";
            int confirmation = 0;
            int x = 0;
            int y = 0;

            for (int z = 0; z < appointmentData.Count; z++)
            {
                if (appointmentData[z].status == "Pending" && vetData[vetidx].service == appointmentData[z].doctor)
                {
                    x++;
                    VetHeader();

                    Console.SetCursorPosition(40, 20);

                    Console.Write("Pet Owner Name: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(appointmentData[z].petOwnerName);

                    Console.SetCursorPosition(40, 22);

                    Console.Write("Pet Owner Contact: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(appointmentData[z].petOwnerNumber);

                    Console.SetCursorPosition(40, 24);

                    Console.Write("Pet Owner Email: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(appointmentData[z].petOwnerEmail);

                    Console.SetCursorPosition(40, 26);

                    Console.Write("Pet Owner Location: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(appointmentData[z].petOwnerLocation);

                    Console.SetCursorPosition(40, 28);

                    Console.Write("Pet Type: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(appointmentData[z].petType);

                    Console.SetCursorPosition(80, 20);

                    Console.Write("Pet Name: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(appointmentData[z].petName);

                    Console.SetCursorPosition(80, 22);

                    Console.Write("Pet Age: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"{appointmentData[z].petAge} Months");

                    Console.SetCursorPosition(80, 24);

                    Console.Write("Pet Weight: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"{appointmentData[z].petWeight} Pounds");

                    Console.SetCursorPosition(80, 26);

                    Console.Write("Pet Gender: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(appointmentData[z].petGender);

                    Console.SetCursorPosition(80, 28);

                    Console.Write("Appointment For: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(appointmentData[z].doctor);

                    Console.SetCursorPosition(40, 30);

                    Console.Write("Appointment Status: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(appointmentData[z].status);

                    Console.SetCursorPosition(80, 30);

                    Console.Write("Day For Appointment: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(appointmentData[z].day);
                }
                else
                {
                    continue;
                }

                Console.SetCursorPosition(50, 34);
                Console.Write("                                                   ");
                Console.SetCursorPosition(50, 36);
                Console.Write("1. Yes    2. No");
                Console.SetCursorPosition(50, 34);
                Console.Write("Do You Want To Confirm this Appointment? ");
                Confirmation = Console.ReadLine();

                while (Confirmation != "1" && Confirmation != "2")
                {
                    Console.SetCursorPosition(50, 38);
                    Console.Write("Please Enter Correct Option! ");
                    Console.SetCursorPosition(50, 34);
                    Console.Write("                                                   ");
                    Console.SetCursorPosition(50, 34);
                    Console.Write("Do You Want To Confirm this Appointment? ");
                    Confirmation = Console.ReadLine();
                }

                confirmation = int.Parse(Confirmation);

                if (confirmation == 1)
                {
                    appointmentData[z].status = "Booked";
                    appointmentData[z].day = vetData[vetidx].day;
                    DoctorName[z] = vetData[vetidx].name;

                    saveCustomerAppointments( customerData, appointmentData,  AppointmentFile, customeridx);
                    saveVetDetails(vetData, appointmentData ,VetFile, vetidx);
                    y++;
                    BookedAppointmentsTotal++;
                }
            }

            BookedByCurrentVet = x - y;
        }
        static void ViewBookedAppointments(string[] DoctorAppointed, string[] DayAppointed, string[] PetOwnerName, string[] PetOwnerNumber, string[] PetOwnerEmail, string[] PetOwnerLocation, string[] PetTypeForAppointment, string[] PetName, string[] PetAge, string[] PetGender, string[] PetWeight, string[] Doctor, string[] Time, string[] Day, int AppointmentsCount, string[] AppointmentBookOrPending, string[] BookedBy, int vetidx, string[] VetService, ref int BookedByCurrentVet, string[] VetDays)
        {
            for (int z = 0; z < appointmentData.Count; z++)
            {
                if (appointmentData[z].status == "Booked" && vetData[vetidx].service == appointmentData[z].doctor)
                {
                    VetHeader();
                    Console.SetCursorPosition(40, 20);

                    Console.Write("Pet Owner Name: ");

                    Console.Write(appointmentData[z].petOwnerName);

                    Console.SetCursorPosition(40, 22);

                    Console.Write("Pet Owner Contact: ");

                    Console.Write(appointmentData[z].petOwnerNumber);

                    Console.SetCursorPosition(40, 24);

                    Console.Write("Pet Owner Email: ");

                    Console.Write(appointmentData[z].petOwnerEmail);

                    Console.SetCursorPosition(40, 26);

                    Console.Write("Pet Owner Location: ");

                    Console.Write(appointmentData[z].petOwnerLocation);

                    Console.SetCursorPosition(40, 28);

                    Console.Write("Pet Type: ");

                    Console.Write(appointmentData[z].petType);

                    Console.SetCursorPosition(80, 20);

                    Console.Write("Pet Name: ");

                    Console.Write(appointmentData[z].petName);

                    Console.SetCursorPosition(80, 22);

                    Console.Write("Pet Age: ");

                    Console.Write($"{appointmentData[z].petAge} Months");

                    Console.SetCursorPosition(80, 24);

                    Console.Write("Pet Weight: ");

                    Console.Write($"{appointmentData[z].petWeight} Pounds");

                    Console.SetCursorPosition(80, 26);

                    Console.Write("Pet Gender: ");

                    Console.Write(appointmentData[z].petGender);

                    Console.SetCursorPosition(80, 28);

                    Console.Write("Appointment For: ");

                    Console.Write(appointmentData[z].doctor);

                    Console.SetCursorPosition(40, 30);

                    Console.Write("Appointment Status: ");

                    Console.Write(appointmentData[z].status);

                    Console.SetCursorPosition(80, 30);

                    Console.Write("Day For Appointment: ");

                    Console.Write(appointmentData[z].day);

                    Console.SetCursorPosition(80, 32);
                    Console.Write("Press any key to see next booked appointment.. ");
                    Console.ReadKey();
                }
                else
                {
                    continue;
                }
            }

            Console.SetCursorPosition(80, 32);
            Console.Write("                                                        ");
            Console.SetCursorPosition(50, 32);
            Console.Write("Press any key to continue...");
        }
        // counting pending and booked appointments 
        static int CountPendingAppointmentsForAdmin()
        {
            int x = 0;
            for (int z = 0; z < appointmentData.Count; z++)
            {
                if (appointmentData[z].status == "Pending")
                {
                    x++;
                }
                else
                {
                    continue;
                }
            }

            return x;
        }
        static int CountPendingAppointmentsForVet(List<Appointment> appointmentData, List<Vet> vetData, int vetidx)
        {
            int x = 0;
            for (int z = 0; z < appointmentData.Count; z++)
            {
                if (appointmentData[z].status == "Pending" && vetData[vetidx].service == appointmentData[z].doctor)
                {
                    x++;
                }
                else
                {
                    continue;
                }
            }

            return x;
        }
        static int CountBookedAppointmentsForVet(List<Appointment> appointmentData, List<Vet> vetData, int vetidx)
        {
            int x = 0;
            for (int z = 0; z < appointmentData.Count; z++)
            {
                if (appointmentData[z].status == "Booked" && vetData[vetidx].service == appointmentData[z].doctor)
                {
                    x++;
                }
                else
                {
                    continue;
                }
            }

            return x;
        }
        static int CountBookedAppointmentsForCustomer(List<Appointment> appointmentData, List<Customer> customerData, int customeridx)
        {
            int x = 0;
            for (int z = 0; z < appointmentData.Count; z++)
            {
                if (appointmentData[z].status == "Booked" && customerData[customeridx].name == appointmentData[z].BookedByCustomer)
                {
                    x++;
                }
                else
                {
                    continue;
                }
            }

            return x;
        }
        static int CountPendingAppointmentsForCustomer(List<Appointment> appointmentData, List<Customer> customerData, int customeridx)
        {
            int x = 0;
            for (int z = 0; z < appointmentData.Count; z++)
            {
                if (appointmentData[z].status == "Pending" && customerData[customeridx].name == appointmentData[z].BookedByCustomer)
                {
                    x++;
                }
                else
                {
                    continue;
                }
            }

            return x;
        }
        static int TotalAppointmentsByOneUser(List<Appointment> appointmentData, string currentCustomer)
        {
            int total = 0;
            for (int v = 0; v < appointmentData.Count; v++)
            {
                if (currentCustomer == appointmentData[v].BookedByCustomer)
                {
                    total++;
                }
            }
            return total;
        }
        static int countVetPendingAppointments(List<Appointment> appointmentData, int vetidx, List<Vet> vetData)
        {
            int x = 0;
            for (int z = 0; z < appointmentData.Count; z++)
            {
                if (appointmentData[z].status == "Pending" && vetData[vetidx].service == appointmentData[z].doctor)
                {
                    x++;
                }
                else
                {
                    continue;
                }
            }

            return x;
        }
        static int countVetBookedAppointments(List<Appointment> appointmentData, int vetidx, List<Vet> vetData)
        {
            int x = 0;
            for (int z = 0; z < appointmentData.Count; z++)
            {
                if (appointmentData[z].status == "Booked" && vetData[vetidx].service == appointmentData[z].doctor)
                {
                    x++;
                }
                else
                {
                    continue;
                }
            }

            return x;
        }
        // checking index in different lists
        static int Bookingindex(List<Appointment> appointmentData, string currentCustomer)
        {
            int bookingidx = -1;
            for (int v = 0; v < appointmentData.Count; v++)
            {
                Console.WriteLine(appointmentData[v].BookedByCustomer);
                if (currentCustomer == appointmentData[v].BookedByCustomer)
                {
                    bookingidx = v;
                }
            }
            return bookingidx;
        }
        static bool checkContactValidity(string contact)
        {
            int y = 0;
            for (int x = 0; x < contact.Length; x++)
            {
                if (char.IsDigit(contact[x]))
                {
                    y++;
                }
            }
            if (y == 11)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static void checkCustomerIndex( ref int idx, string name, string password, List<Customer> customerData)
        {
            for (int v = 0; v < customerData.Count; v++)
            {
                if ((name == customerData[v].name) && (password == customerData[v].password))
                {
                    idx = v;
                    break;
                }
            }
        }
        static void checkVetIndex(int count, ref int idx, string name, string password, List<Vet> vetData)
        {
            for (int v = 0; v < vetData.Count; v++)
            {
                if ((name == vetData[v].name) && (password == vetData[v].password))
                {
                    idx = v;
                    break;
                }
            }
        }
        static void checkApprovedUserIndex(int count, ref int idx, string name, string password, List<Approved> approvedUsersData)
        {
            for (int v = 0; v < approvedUsersData.Count; v++)
            {
                if ((name == approvedUsersData[v].username) && (password == approvedUsersData[v].password))
                {
                    idx = v;
                    break;
                }
            }

        }
        static void checkIndexInSignUpList( ref int idx, string name, string password, List<SignUp> signUpData)
        {
            for (int v = 0; v < signUpData.Count; v++)
            {
                if ((name == signUpData[v].username) && (password == signUpData[v].password))
                {
                    idx = v;
                    break;
                }
            }
        }
        // CUSTOMER MAIN MENU //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        static void CustomerMainMenu(List<Appointment> appointmentData, List<Customer> customerData, List<Vet> vetData,  ref int BookedAppointmentsTotal, ref int PendingAppointmentsTotal, int Appointmentidx,  int customeridx, int SelectDoctorType,string AppointmentFile, string VetFile, int vetidx, int Bookingidx, string[] Veterinarians)
        {
            string petOwnerName = "", petOwnerNumber = "", petOnwerEmail = "", petOwnerLocation = "", petType = "", petName = "", petAge = "", petGender = "", petWeight = "", doctor = "";
            int CustomerPendingCount = 0;
            int CustomerBookedCount = 0;
            int EditChoice = 0;
            while (true)
            {
                printHeader();
                int CustomerRequirment = PrintCustomerMenu();
                if (CustomerRequirment == 5)
                {
                    break;
                }
                else if (CustomerRequirment == 1)
                {
                    CustomerHeader();
                    AboutUs();
                    Console.ReadKey();
                }
                else if (CustomerRequirment == 2)
                {
                    int SelectedService = 0;
                    while (true)
                    {
                        CustomerHeader();
                        SelectedService = Services();
                        int PetType = 0;
                        if (SelectedService == 1)
                        {
                            CustomerHeader();
                            PetType = OverNightCare();
                            if (PetType == 1)
                            {
                                CustomerHeader();
                                DogCare();
                                Console.ReadKey();
                            }
                            else if (PetType == 2)
                            {
                                CustomerHeader();
                                CatCare();
                                Console.ReadKey();
                            }
                            else if (PetType == 3)
                            {
                                CustomerHeader();
                                RabbitCare();
                                Console.ReadKey();
                            }
                        }
                        else if (SelectedService == 2)
                        {
                            CustomerHeader();
                            if (MedicalCheckup())
                            {
                                Console.SetCursorPosition(50, 31);
                                Console.Write("No need to worry! Your pet is medically fit. ");
                            }
                            else
                            {
                                Console.SetCursorPosition(50, 31);
                                Console.Write("Your pet is not medically fit. Consult a Veterinarian!");
                            }
                            Console.SetCursorPosition(71, 50);
                            Console.Write("Press any key to continue.....");
                            Console.ReadKey();
                        }
                        else if (SelectedService == 3)
                        {
                            CustomerHeader();
                            PetTraining();
                            Console.ReadKey();
                        }
                        else if (SelectedService == 4)
                        {
                            break;
                        }
                    }
                }
                else if (CustomerRequirment == 3)
                {
                    int SeeVets = 0;
                    while (true)
                    {
                        CustomerHeader();
                        SeeVets = FindVet();
                        if (SeeVets == 1)
                        {

                            CustomerHeader();
                            BookingAppointment(appointmentData, ref petOwnerName,
                            ref petOwnerNumber,
                            ref petOnwerEmail,
                            ref petOwnerLocation,
                            ref petType,
                            ref petName,
                            ref petAge,
                            ref petGender,
                            ref petWeight,
                            ref doctor, Veterinarians);

                            // creating object for new apppointment
                            Appointment newAppointment = new Appointment(petOwnerName, petOwnerNumber, petOnwerEmail, petOwnerLocation, petType, petName, petAge, petGender, petWeight, doctor, "Not decided", "Pending", customerData[customeridx].name, customerData[customeridx].password);
                            appointmentData.Add(newAppointment);

                            saveCustomerAppointments(customerData, appointmentData, AppointmentFile, customeridx);
                            saveVetDetails(vetData, appointmentData, VetFile, vetidx);
                            CustomerHeader();
                            Console.SetCursorPosition(40, 20);
                            Console.Write("Great! Your Appointment has been booked for " + appointmentData[appointmentData.Count-1].doctor);
                            int ForAppointment = OptionForAppointment();
                            if (ForAppointment == 1)
                            {
                                CustomerHeader();
                                Bookingidx = Bookingindex(appointmentData, customerData[customeridx].name);
                                CheckAppointment(Bookingidx,  appointmentData,  customerData);
                                Console.SetCursorPosition(60, 34);
                                Console.Write("Press any key to continue...");
                            }
                            else if (ForAppointment == 2)
                            {
                                break;
                            }
                            Console.ReadKey();
                        }
                        else if (SeeVets == 2)
                        {
                            break;
                        }
                    }
                }
                else if (CustomerRequirment == 4)
                {
                    while (true)
                    {
                        CustomerHeader();
                        int AppointmentOption = CustomerAppointmentOptions();
                        if (AppointmentOption == 1)
                        {
                            CustomerPendingCount =  CountPendingAppointmentsForCustomer(appointmentData, customerData,  customeridx);
                            if (CustomerPendingCount == 0)
                            {
                                CustomerHeader();
                                Console.SetCursorPosition(65, 26);
                                Console.Write("You have no pending Appointment !");
                                Console.SetCursorPosition(65, 36);
                                Console.Write("Press any key to continue...");
                                Console.ReadKey();
                            }
                            else
                            {
                                CustomerHeader();
                                ShowPendingAppointmetsToCustomer( customerData,  appointmentData, customeridx);
                                Console.SetCursorPosition(65, 36);
                                Console.Write("Press any key to continue...");
                                Console.ReadKey();
                            }
                        }
                        else if (AppointmentOption == 2)
                        {
                            CustomerBookedCount = CountBookedAppointmentsForCustomer(appointmentData,  customerData, customeridx);
                            if (CustomerBookedCount == 0)
                            {
                                CustomerHeader();
                                Console.SetCursorPosition(65, 26);
                                Console.Write("You have no booked Appointment !");
                                Console.SetCursorPosition(65, 36);
                                Console.Write("Press any key to continue...");
                                Console.ReadKey();
                            }
                            else
                            {
                                ShowBookedAppointmetsToCustomer(customeridx, appointmentData, customerData);
                                Console.SetCursorPosition(65, 36);
                                Console.Write("Press any key to continue...");
                                Console.ReadKey();
                            }
                        }
                        else if (AppointmentOption == 3)
                        {
                            CustomerPendingCount =  CountPendingAppointmentsForCustomer(appointmentData, customerData,  customeridx);
                            if (CustomerPendingCount == 0)
                            {
                                CustomerHeader();
                                Console.SetCursorPosition(65, 26);
                                Console.Write("You have no Appointment To Edit!");
                                Console.SetCursorPosition(65, 36);
                                Console.Write("Press any key to continue...");
                                Console.ReadKey();
                            }
                            else
                            {
                                CustomerHeader();
                                EditChoice = AskForEdit(ref  Appointmentidx,  customeridx,  appointmentData,  customerData);
                                Console.Clear();
                                Console.Write(EditChoice);
                                Console.Read();
                                if (EditChoice == 1)
                                {
                                    EditAppointMent(Appointmentidx,  Veterinarians,appointmentData,customerData);
                                    saveCustomerAppointments(customerData, appointmentData, AppointmentFile, customeridx);

                                }
                                Console.SetCursorPosition(65, 36);
                                Console.Write("Press any key to continue...");
                                Console.ReadKey();
                            }
                        }
                        else if (AppointmentOption == 4)
                        {
                            CustomerPendingCount =  CountPendingAppointmentsForCustomer(appointmentData, customerData,  customeridx);
                            if (CustomerPendingCount == 0)
                            {
                                CustomerHeader();
                                Console.SetCursorPosition(65, 26);
                                Console.Write("There is no Appointment to delete!");
                                Console.SetCursorPosition(65, 28);
                                Console.Write("Press any key to continue...");
                                Console.ReadKey();
                            }
                            else
                            {
                                DeleteAppointmentsByCustomer(appointmentData, customerData, customeridx, ref BookedAppointmentsTotal, ref PendingAppointmentsTotal);

                                saveCustomerAppointments(customerData, appointmentData, AppointmentFile, customeridx);
                                saveVetDetails(vetData, appointmentData, VetFile, vetidx);
                            }
                        }
                        else if (AppointmentOption == 5)
                        {
                            break;
                        }
                    }
                }
            }
        }
        static void CustomerHeader()
        {
            printHeader();
            Customer_MenuBar();
        }
        static void Customer_MenuBar()
        {
            Console.SetCursorPosition(1, 15);
            Console.Write("___________________________________________________________________________________________________________________________________________________________________________");

            Console.SetCursorPosition(0, 17);
            Console.Write("                                                               ``.....    \033[1;4mC u s t o m e r\033[0m   .....``");

            Console.SetCursorPosition(1, 18);
            Console.Write("___________________________________________________________________________________________________________________________________________________________________________");
        }
        static int PrintCustomerMenu()
        {
            string Option;
            Console.SetCursorPosition(1, 17);
            Console.Write("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            Console.SetCursorPosition(71, 19);
            Console.Write("1. About Us");
            Console.SetCursorPosition(71, 21);
            Console.Write("2. Services");
            Console.SetCursorPosition(71, 23);
            Console.Write("3. Find a Veterinarian");
            Console.SetCursorPosition(71, 25);
            Console.Write("4. Appointments Status");
            Console.SetCursorPosition(71, 27);
            Console.Write("5. Logout");
            Console.SetCursorPosition(1, 29);
            Console.Write("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            Console.SetCursorPosition(71, 31);
            Console.Write("Enter Your Requirement: ");
            Option = Console.ReadLine();
            while (Option != "1" && Option != "2" && Option != "3" && Option != "4" && Option != "5")
            {
                Console.SetCursorPosition(71, 33);

                Console.Write("Please Enter Correct Option!");
                Console.SetCursorPosition(71, 31);
                Console.Write("                                                                                                                                                                            ");
                Console.SetCursorPosition(71, 31);
                Console.Write("Enter Your Requirement: ");
                Option = Console.ReadLine();
            }
            int Customer_Requirement = int.Parse(Option);
            return Customer_Requirement;
        }
        static void AboutUs()
        {
            Console.SetCursorPosition(40, 21);
            Console.Write("Welcome to PetPal, where our passion for pets drives everything we do. Founded by a team of ");
            Console.SetCursorPosition(40, 22);
            Console.Write("dedicated animal lovers, we understand that your furry, feathered, or scaly companions are ");
            Console.SetCursorPosition(40, 23);
            Console.Write("more than just pets; they're family. With years of experience in providing top-notch pet care");
            Console.SetCursorPosition(40, 24);
            Console.Write("services, we are committed to ensuring the happiness, health, and well-being of your beloved");
            Console.SetCursorPosition(40, 25);
            Console.Write("animals.                                                                                    ");
            Console.SetCursorPosition(40, 26);
            Console.Write("                                                                                             ");
            Console.SetCursorPosition(40, 27);
            Console.Write("Our dedication to excellence extends beyond our services. We maintain a clean and safe environment,");
            Console.SetCursorPosition(40, 28);
            Console.Write("use the best products and techniques for grooming, and employ compassionate and certified staff ");
            Console.SetCursorPosition(40, 29);
            Console.Write("members who are passionate about animals. We prioritize open communication with our clients, ensuring ");
            Console.SetCursorPosition(40, 30);
            Console.Write("you are always informed and comfortable with the care your pet is receiving.");
            Console.SetCursorPosition(40, 32);
            Console.Write("Press any key to continue.....");
            Console.ReadKey();
        }
        static int Services()
        {
            Console.SetCursorPosition(71, 21);
            Console.Write("*____________________*");
            Console.SetCursorPosition(71, 22);
            Console.Write("|                    |");
            Console.SetCursorPosition(71, 23);
            Console.Write("| 1. Overnight Care  |");
            Console.SetCursorPosition(71, 24);
            Console.Write("|                    |");
            Console.SetCursorPosition(71, 25);
            Console.Write("| 2. Medical Checkup |");
            Console.SetCursorPosition(71, 26);
            Console.Write("|                    |");
            Console.SetCursorPosition(71, 27);
            Console.Write("| 3. Pet Training    |");
            Console.SetCursorPosition(71, 28);
            Console.Write("|                    |");
            Console.SetCursorPosition(71, 29);
            Console.Write("| 4. Go Back         |");
            Console.SetCursorPosition(71, 30);
            Console.Write("*____________________*");
            Console.SetCursorPosition(71, 33);
            Console.Write("Enter option to continue.....");

            string option = Console.ReadLine();

            while (option != "1" && option != "2" && option != "3" && option != "4")
            {
                Console.SetCursorPosition(71, 35);

                Console.Write("Please Enter Correct Option!");

                Console.SetCursorPosition(71, 33);
                Console.Write("                                              ");
                Console.SetCursorPosition(71, 33);
                Console.Write("Enter option to continue.....");
                option = Console.ReadLine();
                Console.SetCursorPosition(71, 35);
                Console.Write("                                              ");
            }

            return int.Parse(option);
        }
        static int OverNightCare()
        {
            Console.SetCursorPosition(71, 23);
            Console.Write("Enter Name of Your Pet: ");
            string name = Console.ReadLine();

            Console.SetCursorPosition(71, 27);
            Console.Write("1. Dog");
            Console.SetCursorPosition(71, 29);
            Console.Write("2. Cat");
            Console.SetCursorPosition(71, 31);
            Console.Write("3. Rabbit");
            Console.SetCursorPosition(71, 25);
            Console.Write("Enter type of Your Pet: ");
            string type = Console.ReadLine();

            while (type != "1" && type != "2" && type != "3")
            {
                Console.SetCursorPosition(71, 33);

                Console.Write("Please Enter Correct Option!");

                Console.SetCursorPosition(71, 25);
                Console.Write("                                                                                                                          ");
                Console.SetCursorPosition(71, 25);
                Console.Write("Enter type of Your Pet: ");
                type = Console.ReadLine();
                Console.SetCursorPosition(71, 33);
                Console.Write("                                             ");
            }
            int SelectedType = int.Parse(type);
            Console.SetCursorPosition(71, 33);
            Console.Write("Enter Sleeping Hours of your pet: ");
            string shours = Console.ReadLine();
            return SelectedType;
        }
        static void DogCare()
        {
            Console.SetCursorPosition(40, 21);
            Console.Write("1. Continue to feed your puppy a high-quality puppy formula or milk replacer specifically ");
            Console.SetCursorPosition(40, 22);
            Console.Write("designed for puppies. ");
            Console.SetCursorPosition(40, 24);
            Console.Write("2. Follow the manufacturer's instructions for feeding quantities and frequency.");
            Console.SetCursorPosition(40, 26);
            Console.Write("3. Begin gentle socialization by exposing the puppy to different people, gentle handling, ");
            Console.SetCursorPosition(40, 27);
            Console.Write("and safe, age-appropriate toys.");
            Console.SetCursorPosition(40, 29);
            Console.Write("4. Puppies are not able to regulate their body temperature well. Keep the environment warm  ");
            Console.SetCursorPosition(40, 30);
            Console.Write("(around 85-90 F or 29-32 C) using heating pads or a heat lamp. ");

            Console.SetCursorPosition(71, 40);
            Console.Write("Press any key to continue.....");
        }

        static void CatCare()
        {
            Console.SetCursorPosition(40, 21);
            Console.Write("1. Cats appreciate a quiet and comfortable sleeping area. Ensure your cat's bed or sleeping   ");
            Console.SetCursorPosition(40, 22);
            Console.Write("spot is clean, cozy, and away from noisy or high-traffic areas.  ");
            Console.SetCursorPosition(40, 24);
            Console.Write("2. Feed your cat its evening meal a few hours before bedtime. Interactive playtime is an   ");
            Console.SetCursorPosition(40, 25);
            Console.Write("excellent way to engage your cat and burn off excess energy. ");
            Console.SetCursorPosition(40, 27);
            Console.Write("3. If your cat seems active at night, consider engaging in a short play session before you  ");
            Console.SetCursorPosition(40, 28);
            Console.Write("go to bed. This can help tire them out and encourage them to rest during the night.");
            Console.SetCursorPosition(40, 30);
            Console.Write("4. If your cat goes outdoors, ensure they are safely indoors during the night to protect   ");
            Console.SetCursorPosition(40, 31);
            Console.Write(" them from night-time dangers and predators. ");
            Console.SetCursorPosition(71, 40);
            Console.Write("Press any key to continue.....");
        }

        static void RabbitCare()
        {
            Console.SetCursorPosition(40, 21);
            Console.Write("1. Rabbits need a quiet, secure, and comfortable sleeping area. Ensure their hutch or enclosure    ");
            Console.SetCursorPosition(40, 22);
            Console.Write("is clean and cozy,  with bedding or hay for them to rest on. ");
            Console.SetCursorPosition(40, 24);
            Console.Write("2. Rabbits can see in low light, but you can provide a small, dim nightlight in their sleeping ");
            Console.SetCursorPosition(40, 25);
            Console.Write(" area to help them navigate if they need to move around at night.");
            Console.SetCursorPosition(40, 27);
            Console.Write("3. Keep the noise level in your home to a minimum during the night to avoid disturbing your ");
            Console.SetCursorPosition(40, 28);
            Console.Write("rabbit's rest.");
            Console.SetCursorPosition(40, 30);
            Console.Write("4. Leave a few safe and engaging toys or chewing items in your rabbit's enclosure to keep ");
            Console.SetCursorPosition(40, 31);
            Console.Write("them entertained during their active hours. ");
            Console.SetCursorPosition(71, 40);
            Console.Write("Press any key to continue.....");
        }
        static bool MedicalCheckup()
        {
            Console.SetCursorPosition(50, 21);
            Console.Write("Provide us some information about your pet. ");

            string vac;
            string play;
            string chck;
            string eat;

            Console.SetCursorPosition(50, 23);
            Console.Write("Is your pet vaccinated?                            1. Yes  2. No   ");
            vac = Console.ReadLine();

            while (true)
            {
                if (vac == "1" || vac == "2")
                {
                    break;
                }
                else
                {
                    Console.SetCursorPosition(50, 31);
                    Console.Write("Please Enter Correct Option!");
                    Console.SetCursorPosition(50, 23);
                    Console.Write("                                                                                                                    ");
                    Console.SetCursorPosition(50, 23);
                    Console.Write("Is your pet vaccinated?                            1. Yes  2. No   ");
                    vac = Console.ReadLine();
                    Console.SetCursorPosition(50, 31);
                    Console.Write("                                              ");
                    continue;
                }
            }

            Console.SetCursorPosition(50, 25);
            Console.Write("Does your pet walk or play well?                   1. Yes  2. No   ");
            play = Console.ReadLine();

            while (true)
            {
                if (play == "1" || play == "2")
                {
                    break;
                }
                else
                {
                    Console.SetCursorPosition(50, 31);
                    Console.Write("Please Enter Correct Option!");
                    Console.SetCursorPosition(50, 25);
                    Console.Write("                                                                                                           ");
                    Console.SetCursorPosition(50, 25);
                    Console.Write("Does your pet walk or play well?                   1. Yes  2. No   ");
                    play = Console.ReadLine();
                    Console.SetCursorPosition(50, 31);
                    Console.Write("                                                    ");
                    continue;
                }
            }

            Console.SetCursorPosition(50, 27);
            Console.Write("Do you bring your pet for monthly checkup?         1. Yes  2. No   ");
            chck = Console.ReadLine();

            while (true)
            {
                if (chck == "1" || chck == "2")
                {
                    break;
                }
                else
                {
                    Console.SetCursorPosition(50, 31);
                    Console.Write("Please Enter Correct Option! ");
                    Console.SetCursorPosition(50, 27);
                    Console.Write("                                                                                                                            ");
                    Console.SetCursorPosition(50, 27);
                    Console.Write("Do you bring your pet for monthly checkup?         1. Yes  2. No   ");
                    chck = Console.ReadLine();
                    Console.SetCursorPosition(50, 31);
                    Console.Write("                                             ");
                    continue;
                }
            }

            Console.SetCursorPosition(50, 29);
            Console.Write("Does your pet eat well?                            1. Yes  2. No   ");
            eat = Console.ReadLine();

            while (true)
            {
                if (eat == "1" || eat == "2")
                {
                    break;
                }
                else
                {
                    Console.SetCursorPosition(50, 31);
                    Console.Write("Please Enter Correct Option!");
                    Console.SetCursorPosition(50, 29);
                    Console.Write("                                                                                                                     ");
                    Console.SetCursorPosition(50, 29);
                    Console.Write("Does your pet eat well?                            1. Yes  2. No   ");
                    eat = Console.ReadLine();
                    Console.SetCursorPosition(50, 31);
                    Console.Write("                                             ");
                    continue;
                }
            }

            if (vac == "1" && eat == "1" && chck == "1" && play == "1")
            {
                return true;
            }
            else if (vac == "2" || eat == "2" || chck == "2" || play == "2")
            {
                return false;
            }

            return false;
        }
        static void PetTraining()
        {
            Console.SetCursorPosition(40, 21);
            Console.Write("Need assistance? Our pet trainers are just a few taps away on the app, ready ");
            Console.SetCursorPosition(40, 22);
            Console.Write("to lend a helping hand - or paw - whenever you need it, ensuring your pet's ");
            Console.SetCursorPosition(40, 23);
            Console.Write("continuous growth and well-being ");
            Console.SetCursorPosition(40, 25);

            Console.Write("--------------------------------------------------------------------------------------------");
            Console.ResetColor();
            Console.SetCursorPosition(40, 26);
            Console.Write("|                                  TRAINING SCHEDULE                                       |");
            Console.SetCursorPosition(40, 27);
            Console.Write("--------------------------------------------------------------------------------------------");
            Console.SetCursorPosition(40, 28);
            Console.Write("|            AGE           |            WEEKLY             |            MONTHLY            |  ");
            Console.SetCursorPosition(40, 29);
            Console.Write("--------------------------------------------------------------------------------------------");
            Console.SetCursorPosition(40, 30);
            Console.Write("|       1-6 Months         |            20,000 Rs          |             50,000            |  ");
            Console.SetCursorPosition(40, 31);
            Console.Write("--------------------------------------------------------------------------------------------");
            Console.SetCursorPosition(40, 32);
            Console.Write("|       6-12 Months        |            15,000 Rs          |             40,000            |  ");
            Console.SetCursorPosition(40, 33);
            Console.Write("--------------------------------------------------------------------------------------------");
            Console.SetCursorPosition(40, 34);
            Console.Write("|   Elder than 1 Year      |            7 ,000 Rs          |             25,000            |  ");
            Console.SetCursorPosition(40, 35);
            Console.Write("--------------------------------------------------------------------------------------------");

            Console.SetCursorPosition(40, 37);
            Console.Write("We are providing 50% discount for fast learning pets!");
            Console.ResetColor();
            Console.SetCursorPosition(40, 39);
            Console.Write("Press any key to continue...");
        }
        // ///////////////////////////////////   FINDING A VET    ////////////////////////////////////////////////////////

        static int FindVet()
        {
            Console.SetCursorPosition(40, 21);
            Console.Write("Looking for a Veterinarian? We have a dedicated team of veterinarians, the heart and soul ");
            Console.SetCursorPosition(40, 22);
            Console.Write("of PetPal app. With years of experience and a deep love for animals, our veterinarians are ");
            Console.SetCursorPosition(40, 23);
            Console.Write("here to provide top-notch healthcare and guidance for your beloved furry companions. ");
            Console.SetCursorPosition(50, 28);
            Console.Write("1. Yes");
            Console.SetCursorPosition(50, 30);
            Console.Write("2. No");
            Console.SetCursorPosition(50, 26);
            Console.Write("Want to see available Veterinarians (Yes/No)?  ");
            string option = Console.ReadLine();
            while (option != "1" && option != "2")
            {

                Console.SetCursorPosition(71, 35);
                Console.Write("Please Enter Correct Option! ");

                Console.SetCursorPosition(50, 26);
                Console.Write("                                                                                                              ");
                Console.SetCursorPosition(50, 26);
                Console.Write("Want to see available Veterinarians (Yes/No)?  ");
                option = Console.ReadLine();
            }
            return int.Parse(option);
        }

        static int OptionForAppointment()
        {
            Console.SetCursorPosition(40, 22);
            Console.Write("1. Check Appointment Status    2. Go Back");
            Console.SetCursorPosition(40, 24);
            Console.Write("Select Option to continue...");
            string option = Console.ReadLine();
            while (option != "1" && option != "2")
            {

                Console.SetCursorPosition(80, 28);
                Console.Write("Please Enter Correct Option! ");

                Console.SetCursorPosition(40, 24);
                Console.Write("                                               ");
                Console.SetCursorPosition(40, 24);
                Console.Write("Select Option to continue...");
                option = Console.ReadLine();
                Console.SetCursorPosition(80, 28);
                Console.Write("                                             ");
            }
            return int.Parse(option);
        }

        static int CustomerAppointmentOptions()
        {
            Console.SetCursorPosition(1, 20);
            Console.Write("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            Console.SetCursorPosition(71, 22);
            Console.Write("1. Pending Appointments");
            Console.SetCursorPosition(71, 24);
            Console.Write("2. Booked Appointments");
            Console.SetCursorPosition(71, 26);
            Console.Write("3. Edit Appointments");
            Console.SetCursorPosition(71, 28);
            Console.Write("4. Delete Appointments");
            Console.SetCursorPosition(71, 30);
            Console.Write("5. Exit");
            Console.SetCursorPosition(1, 32);
            Console.Write("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            Console.SetCursorPosition(71, 34);
            Console.Write("Enter Your Requirement: ");
            string option = Console.ReadLine();
            while (option != "1" && option != "2" && option != "3" && option != "4" && option != "5")
            {

                Console.SetCursorPosition(71, 36);
                Console.Write("Please Enter Correct Option! ");

                Console.SetCursorPosition(71, 34);
                Console.Write("                                                                                                                       ");
                Console.SetCursorPosition(71, 34);
                Console.Write("Enter Your Requirement: ");
                option = Console.ReadLine();
            }
            return int.Parse(option);
        }
        static void BookingAppointment( List<Appointment> appointmentData, ref string petOwnerName,
                            ref string petOwnerNumber,
                            ref string petOnwerEmail,
                            ref string petOwnerLocation,
                            ref string petType,
                            ref string petName,
                            ref string petAge,
                            ref string petGender,
                            ref string petWeight,
                            ref string doctor, string[] AvailableVets)
        {
            Console.SetCursorPosition(40, 20);
            Console.Write("Enter Your Name: ");
            petOwnerName = Console.ReadLine();

            Console.SetCursorPosition(40, 22);
            Console.Write("Enter Your Phone Number: ");
            petOwnerNumber = Console.ReadLine();
            while (petOwnerNumber.Length != 11)
            {

                Console.SetCursorPosition(60, 28);
                Console.Write("Please add a valid Contact Number of length 11!");

                Console.SetCursorPosition(40, 22);
                Console.Write("                                                        ");
                Console.SetCursorPosition(40, 22);
                Console.Write("Enter Your Phone Number: ");
                petOwnerNumber = Console.ReadLine();
                Console.SetCursorPosition(60, 28);
                Console.Write("                                                    ");
            }

            while (!checkContactValidity(petOwnerNumber))
            {
                Console.SetCursorPosition(40, 24);

                Console.Write("Contact Number can only contain digits!");

                Console.SetCursorPosition(40, 22);
                Console.Write("                                     ");
                Console.SetCursorPosition(40, 22);
                Console.Write("Enter Your Phone Number: ");
                petOwnerNumber = Console.ReadLine();
                Console.SetCursorPosition(40, 24);
                Console.Write("                                           ");
            }


            Console.SetCursorPosition(40, 24);
            Console.Write("Enter Your Email: ");
            petOnwerEmail = Console.ReadLine();

            Console.SetCursorPosition(40, 26);
            Console.Write("Enter Your Location: ");
            petOwnerLocation = Console.ReadLine();

            Console.SetCursorPosition(40, 28);
            Console.Write("Enter Your Pet Type: ");
            petType = Console.ReadLine();

            Console.SetCursorPosition(40, 30);
            Console.Write("Enter Your Pet Name: ");
            petName = Console.ReadLine();

            Console.SetCursorPosition(40, 32);
            Console.Write("Enter Your Pet Age (in months): ");
            petAge = Console.ReadLine();

            Console.SetCursorPosition(40, 34);
            Console.Write("Enter Your Pet Gender: ");
            petGender = Console.ReadLine();

            Console.SetCursorPosition(40, 36);
            Console.Write("Enter Your Pet Weight (in Pounds): ");
            petWeight = Console.ReadLine();

            Console.SetCursorPosition(80, 22);
            Console.Write("1. Dermatology");
            Console.SetCursorPosition(80, 24);
            Console.Write("2. Primary Care");
            Console.SetCursorPosition(80, 26);
            Console.Write("3. Internal Medicine");
            Console.SetCursorPosition(80, 20);
            Console.Write("What is your requirement? ");
            doctor = Console.ReadLine();

            while (doctor != "1" && doctor != "2" && doctor != "3")
            {
                Console.SetCursorPosition(80, 28);

                Console.Write("Please Enter Correct Option!");

                Console.SetCursorPosition(80, 20);
                Console.Write("                                          ");
                Console.SetCursorPosition(80, 20);
                Console.Write("What is your requirement? ");
                doctor = Console.ReadLine();
                Console.SetCursorPosition(80, 20);
                Console.Write("                                          ");
                Console.SetCursorPosition(80, 28);
            }

            int SelectedDoctorType = int.Parse(doctor);
            doctor = AvailableVets[SelectedDoctorType - 1];
            
        }
        static void ShowPendingAppointmetsToCustomer(List<Customer> customerData,List<Appointment> appointmentData, int customeridx)
        {
            int y = 0;
            for (int z = 0; z < appointmentData.Count; z++)
            {
                if ((customerData[customeridx].name == appointmentData[z].BookedByCustomer) && (appointmentData[z].status == "Pending"))
                {
                    CustomerHeader();
                    y++;
                    Console.SetCursorPosition(40, 20);

                    Console.Write("Pet Owner Name: " + appointmentData[z].petOwnerName);

                    Console.SetCursorPosition(40, 22);

                    Console.Write("Pet Owner Contact: " + appointmentData[z].petOwnerNumber);

                    Console.SetCursorPosition(40, 24);

                    Console.Write("Pet Owner Email: " + appointmentData[z].petOwnerEmail);

                    Console.SetCursorPosition(40, 26);

                    Console.Write("Pet Owner Location: " + appointmentData[z].petOwnerLocation);

                    Console.SetCursorPosition(40, 28);

                    Console.Write("Pet Type: " + appointmentData[z].petType);

                    Console.SetCursorPosition(80, 20);

                    Console.Write("Pet Name: " + appointmentData[z].petName);

                    Console.SetCursorPosition(80, 22);

                    Console.Write("Pet Age: " + appointmentData[z].petAge + " Months");

                    Console.SetCursorPosition(80, 24);

                    Console.Write("Pet Weight: " + appointmentData[z].petWeight + " Pounds");

                    Console.SetCursorPosition(80, 26);

                    Console.Write("Pet Gender: " + appointmentData[z].petGender);

                    Console.SetCursorPosition(80, 28);

                    Console.Write("Appointment For: " + appointmentData[z].doctor);

                    Console.SetCursorPosition(40, 30);

                    Console.Write("Appointment Status: " + appointmentData[z].status);

                    Console.SetCursorPosition(80, 30);

                    Console.Write("Day For Appointment: " + appointmentData[z].day);

                    Console.SetCursorPosition(60, 32);
                    Console.Write("Press any key to see next Appointment..");

                    Console.ReadKey();
                }
                else
                {
                    continue;
                }
            }
            if (y == 1)
            {
                Console.SetCursorPosition(60, 34);
                Console.Write("                                               ");
                Console.SetCursorPosition(60, 34);

                Console.Write($"There is Only {y} Pending Appointment");
            }
            else if (y > 1)
            {
                Console.SetCursorPosition(60, 34);
                Console.Write("                                               ");
                Console.SetCursorPosition(60, 34);

                Console.Write($"There are Only {y} Pending Appointments");
            }

            Console.ResetColor();

        }
        static void ShowBookedAppointmetsToCustomer( int customeridx, List<Appointment> appointmentData, List<Customer> customerData)
        {
            int y = 0;
            for (int z = 0; z < appointmentData.Count; z++)
            {
                if ((customerData[customeridx].name == appointmentData[z].BookedByCustomer) && (appointmentData[z].status == "Booked"))
                {
                    CustomerHeader();
                    y++;
                    Console.SetCursorPosition(40, 20);
                    Console.Write("Pet Owner Name: " + appointmentData[z].petOwnerName);
                    Console.SetCursorPosition(40, 22);
                    Console.Write("Pet Owner Contact: " + appointmentData[z].petOwnerNumber);
                    Console.SetCursorPosition(40, 24);
                    Console.Write("Pet Owner Email: " + appointmentData[z].petOwnerEmail);
                    Console.SetCursorPosition(40, 26);
                    Console.Write("Pet Owner Location: " + appointmentData[z].petOwnerLocation);
                    Console.SetCursorPosition(40, 28);
                    Console.Write("Pet Type: " + appointmentData[z].petType);
                    Console.SetCursorPosition(80, 20);
                    Console.Write("Pet Name: " + appointmentData[z].petName);
                    Console.SetCursorPosition(80, 22);
                    Console.Write("Pet Age: " + appointmentData[z].petAge + " Months");
                    Console.SetCursorPosition(80, 24);
                    Console.Write("Pet Weight: " + appointmentData[z].petWeight + " Pounds");
                    Console.SetCursorPosition(80, 26);
                    Console.Write("Pet Gender: " + appointmentData[z].petGender);
                    Console.SetCursorPosition(80, 28);
                    Console.Write("Appointment For: " + appointmentData[z].doctor);
                    Console.SetCursorPosition(40, 30);
                    Console.Write("Appointment Status: " + appointmentData[z].status);
                    Console.SetCursorPosition(80, 30);
                    Console.Write("Day For Appointment: " + appointmentData[z].day);
                    Console.SetCursorPosition(60, 32);
                    Console.Write("Press any key to see the next Appointment..");
                    Console.ReadKey();
                }
                else
                {
                    continue;
                }
            }
            if (y == 1)
            {
                Console.SetCursorPosition(60, 34);
                Console.Write("                                               ");
                Console.SetCursorPosition(60, 34);
                Console.Write("You Have Only " + y + " Booked Appointment");
            }
            else if (y > 1)
            {
                Console.SetCursorPosition(60, 34);
                Console.Write("                                               ");
                Console.SetCursorPosition(60, 34);
                Console.Write("You Have " + y + " Booked Appointments");
            }
        }
        static void CheckAppointment(int bookingIdx, List<Appointment> appointmentData, List<Customer> customerData)
        {
            Console.SetCursorPosition(40, 20);
            Console.Write("Pet Owner Name: " + appointmentData[bookingIdx].petOwnerName);
            Console.SetCursorPosition(40, 22);
            Console.Write("Pet Owner Contact: " + appointmentData[bookingIdx].petOwnerNumber);
            Console.SetCursorPosition(40, 24);
            Console.Write("Pet Owner Email: " + appointmentData[bookingIdx].petOwnerEmail);
            Console.SetCursorPosition(40, 26);
            Console.Write("Pet Owner Location: " + appointmentData[bookingIdx].petOwnerLocation);
            Console.SetCursorPosition(40, 28);
            Console.Write("Pet Type: " + appointmentData[bookingIdx].petType);
            Console.SetCursorPosition(80, 20);
            Console.Write("Pet Name: " + appointmentData[bookingIdx].petName);
            Console.SetCursorPosition(80, 22);
            Console.Write("Pet Age : " + appointmentData[bookingIdx].petAge + " Months");
            Console.SetCursorPosition(80, 24);
            Console.Write("Pet Weight: " + appointmentData[bookingIdx].petWeight + " Pounds");
            Console.SetCursorPosition(80, 26);
            Console.Write("Pet Gender: " + appointmentData[bookingIdx].petGender);
            Console.SetCursorPosition(80, 28);
            Console.Write("Appointment For: " + appointmentData[bookingIdx].doctor);
            Console.SetCursorPosition(40, 30);
            Console.Write("Appointment Status: " + appointmentData[bookingIdx].status);
            Console.SetCursorPosition(80, 30);
            Console.Write("Day For Appointment: " + appointmentData[bookingIdx].day);
        }
        static void DeleteAppointmentsByCustomer(List<Appointment> appointmentData, List<Customer> customerData,  int customeridx, ref int BookedAppointmentsTotal,
        ref int PendingAppointmentsTotal)
        {
            int x = 0;
            string Confirmation = "";
            for (int z = 0; z < appointmentData.Count; z++)
            {
                if ((customerData[customeridx].name == appointmentData[z].BookedByCustomer) && (appointmentData[z].status == "Pending"))
                {
                    x++;
                    CustomerHeader();
                    Console.SetCursorPosition(40, 20);
                    Console.Write("Pet Owner Name: " + appointmentData[z].petOwnerName);
                    Console.SetCursorPosition(40, 22);
                    Console.Write("Pet Owner Contact: " + appointmentData[z].petOwnerNumber);
                    Console.SetCursorPosition(40, 24);
                    Console.Write("Pet Owner Email: " + appointmentData[z].petOwnerEmail);
                    Console.SetCursorPosition(40, 26);
                    Console.Write("Pet Owner Location: " + appointmentData[z].petOwnerLocation);
                    Console.SetCursorPosition(40, 28);
                    Console.Write("Pet Type: " + appointmentData[z].petType);
                    Console.SetCursorPosition(80, 20);
                    Console.Write("Pet Name: " + appointmentData[z].petName);
                    Console.SetCursorPosition(80, 22);
                    Console.Write("Pet Age : " + appointmentData[z].petAge + " Months");
                    Console.SetCursorPosition(80, 24);
                    Console.Write("Pet Weight: " + appointmentData[z].petWeight + " Pounds");
                    Console.SetCursorPosition(80, 26);
                    Console.Write("Pet Gender: " + appointmentData[z].petGender);
                    Console.SetCursorPosition(80, 28);
                    Console.Write("Appointment For: " + appointmentData[z].doctor);
                    Console.SetCursorPosition(40, 30);
                    Console.Write("Appointment Status: " + appointmentData[z].status);
                    Console.SetCursorPosition(80, 30);
                    Console.SetCursorPosition(50, 34);
                    Console.Write("                                                   ");
                    Console.SetCursorPosition(50, 36);
                    Console.Write("1. Yes    2. No");
                    Console.SetCursorPosition(50, 34);
                    Console.Write("Do You Want To Delete this Appointment? ");
                    Confirmation = Console.ReadLine();
                    while (Confirmation != "1" && Confirmation != "2")
                    {
                        Console.SetCursorPosition(50, 38);
                        Console.Write("Please Enter Correct Option! ");
                        Console.SetCursorPosition(50, 34);
                        Console.Write("                                                   ");
                        Console.SetCursorPosition(50, 34);
                        Console.Write("Do You Want To Delete this Appointment? ");
                        Confirmation = Console.ReadLine();
                    }
                    int confirmation = STOI(Confirmation);
                    if (confirmation == 1)
                    {
                        if (appointmentData[z].status == "Pending")
                        {
                            PendingAppointmentsTotal--;
                        }
                        else if (appointmentData[z].status == "Booked")
                        {
                            BookedAppointmentsTotal--;
                        }
                        appointmentData.RemoveAt(z);
                        continue;
                    }
                    else if (confirmation == 2)
                    {
                        continue;
                    }
                }
                if (x == 1)
                {
                    Console.SetCursorPosition(50, 34);
                    Console.Write("                                               ");
                    Console.SetCursorPosition(50, 34);
                    Console.Write("There was Only " + x + " Appointment");
                }
                else if (x > 1)
                {
                    Console.SetCursorPosition(50, 34);
                    Console.Write("                                               ");
                    Console.SetCursorPosition(50, 34);
                    Console.Write("There were Only " + x + " Appointments");
                }
            }
        }
        static int AskForEdit(ref int Appointmentidx,  int customeridx, List<Appointment> appointmentData, List<Customer> customerData)
        {
            string option = "";
            int SelectedOption = 0;
            for (int z = 0; z < appointmentData.Count; z++)
            {
                if ((customerData[customeridx].name == appointmentData[z].BookedByCustomer) && (appointmentData[z].status == "Pending"))
                {
                    CustomerHeader();
                    Appointmentidx = z;
                    Console.SetCursorPosition(40, 20);
                    Console.Write("Pet Owner Name: " + appointmentData[z].petOwnerName);
                    Console.SetCursorPosition(40, 22);
                    Console.Write("Pet Owner Contact: " + appointmentData[z].petOwnerNumber);
                    Console.SetCursorPosition(40, 24);
                    Console.Write("Pet Owner Email: " + appointmentData[z].petOwnerEmail);
                    Console.SetCursorPosition(40, 26);
                    Console.Write("Pet Owner Location: " + appointmentData[z].petOwnerNumber); // Assuming it's a mistake in the original code and it should be  appointmentData[z].petOwnerLocation
                    Console.SetCursorPosition(40, 28);
                    Console.Write("Pet Type: " + appointmentData[z].petType);
                    Console.SetCursorPosition(80, 20);
                    Console.Write("Pet Name: " + appointmentData[z].petName);
                    Console.SetCursorPosition(80, 22);
                    Console.Write("Pet Age : " + appointmentData[z].petAge + " Months");
                    Console.SetCursorPosition(80, 24);
                    Console.Write("Pet Weight: " + appointmentData[z].petWeight + " Pounds");
                    Console.SetCursorPosition(80, 26);
                    Console.Write("Pet Gender: " + appointmentData[z].petGender);
                    Console.SetCursorPosition(80, 28);
                    Console.Write("Appointment For: " + appointmentData[z].doctor);
                    Console.SetCursorPosition(40, 30);
                    Console.Write("Appointment Status: " + appointmentData[z].status);
                    Console.SetCursorPosition(80, 30);
                    Console.Write("Day For Appointment: " + appointmentData[z].day);
                    Console.SetCursorPosition(60, 34);
                    Console.Write("1. Yes          2. No");
                    Console.SetCursorPosition(60, 32);
                    Console.Write("Edit This Appointment? ");
                    option = Console.ReadLine();

                    while (option != "1" && option != "2")
                    {
                        Console.SetCursorPosition(60, 36);
                        Console.Write("\u001b[0;31mPlease Enter Correct Option! ");
                        Console.SetCursorPosition(60, 32);
                        Console.Write("                                               ");
                        Console.SetCursorPosition(60, 32);
                        Console.Write("Edit This Appointment? ");
                        option = Console.ReadLine();
                        Console.SetCursorPosition(60, 36);
                        Console.Write("                                             ");
                    }

                    
                    if (option == "1")
                    {
                        break;
                    }
                    else if (option == "2")
                    {
                        continue;
                    }
                }
                else
                {
                    continue;
                }
            }
            int selectedOption = int.Parse(option);
            return SelectedOption;
        }
        static void EditAppointMent(int appointmentIdx, string[] veterinarians, List<Appointment> appointmentData, List<Customer> customerData)
        {
            CustomerHeader();
            Console.SetCursorPosition(40, 20);
            Console.Write("1. Edit Pet Owner Name ");
            Console.SetCursorPosition(40, 22);
            Console.Write("2. Edit Pet Owner Contact ");
            Console.SetCursorPosition(40, 24);
            Console.Write("3. Edit Pet Owner Email ");
            Console.SetCursorPosition(40, 26);
            Console.Write("4. Edit Pet Owner Location ");
            Console.SetCursorPosition(40, 28);
            Console.Write("5. Edit Pet Type ");
            Console.SetCursorPosition(80, 20);
            Console.Write("6. Edit Pet Name ");
            Console.SetCursorPosition(80, 22);
            Console.Write("7. Edit Pet Age  ");
            Console.SetCursorPosition(80, 24);
            Console.Write("8. Edit Pet Weight ");
            Console.SetCursorPosition(80, 26);
            Console.Write("9. Edit Pet Gender ");
            Console.SetCursorPosition(80, 28);
            Console.Write("10. Edit Appointment Requirement ");
            Console.SetCursorPosition(60, 32);
            Console.Write("Select Option You Want to Edit... ");
            string option = Console.ReadLine();

            while (option != "1" && option != "2" && option != "3" && option != "4" && option != "5" && option != "6" && option != "7" && option != "8" && option != "9" && option != "10")
            {
                Console.SetCursorPosition(60, 34);
                Console.Write("Please Enter Correct Option! ");
                Console.SetCursorPosition(60, 32);
                Console.Write("                                                        ");
                Console.SetCursorPosition(60, 32);
                Console.Write("Select Option You Want to Edit... ");
                option = Console.ReadLine();
                Console.SetCursorPosition(60, 34);
                Console.Write("                                             ");
            }

            CustomerHeader();
            int selectedOption = int.Parse(option);

            if (selectedOption == 1)
            {
                Console.SetCursorPosition(60, 24);
                Console.Write("                                   ");
                Console.SetCursorPosition(60, 24);
                Console.Write("Enter Pet Owner Name: ");
                appointmentData[appointmentIdx].petOwnerName = Console.ReadLine();
                Console.SetCursorPosition(60, 26);
                Console.Write("Pet Owner Name Edited!");
            }
            else if (selectedOption == 2)
            {
                Console.SetCursorPosition(60, 24);
                Console.Write("                                   ");
                Console.SetCursorPosition(60, 24);
                Console.Write("Enter Pet Owner Contact: ");
                appointmentData[appointmentIdx].petOwnerNumber = Console.ReadLine();
                Console.SetCursorPosition(60, 26);
                Console.Write("Pet Owner Contact Edited!");
            }
            else if (selectedOption == 3)
            {
                Console.SetCursorPosition(60, 24);
                Console.Write("                                   ");
                Console.SetCursorPosition(60, 24);
                Console.Write("Enter Pet Owner Email: ");
                appointmentData[appointmentIdx].petOwnerEmail = Console.ReadLine();
                Console.SetCursorPosition(60, 26);
                Console.Write("Pet Owner Email Edited!");
            }
            else if (selectedOption == 4)
            {
                Console.SetCursorPosition(60, 24);
                Console.Write("                                   ");
                Console.SetCursorPosition(60, 24);
                Console.Write("Enter Pet Owner Location: ");
                appointmentData[appointmentIdx].petOwnerLocation = Console.ReadLine();
                Console.SetCursorPosition(60, 26);
                Console.Write("Pet Owner Location Edited!");
            }
            else if (selectedOption == 5)
            {
                Console.SetCursorPosition(60, 24);
                Console.Write("                                   ");
                Console.SetCursorPosition(60, 24);
                Console.Write("Enter Pet Type: ");
                appointmentData[appointmentIdx].petType = Console.ReadLine();
                Console.SetCursorPosition(60, 26);
                Console.Write("Pet Type Edited!");
            }
            else if (selectedOption == 6)
            {
                Console.SetCursorPosition(60, 24);
                Console.Write("                                   ");
                Console.SetCursorPosition(60, 24);
                Console.Write("Enter Pet Name: ");
                appointmentData[appointmentIdx].petName = Console.ReadLine();
                Console.SetCursorPosition(60, 26);
                Console.Write("Pet Name Edited!");
            }
            else if (selectedOption == 7)
            {
                Console.SetCursorPosition(60, 24);
                Console.Write("                                   ");
                Console.SetCursorPosition(60, 24);
                Console.Write("Enter Pet Age: ");
                appointmentData[appointmentIdx].petAge = Console.ReadLine();
                Console.SetCursorPosition(60, 26);
                Console.Write("Pet Age Edited!");
            }
            else if (selectedOption == 8)
            {
                Console.SetCursorPosition(60, 24);
                Console.Write("                                   ");
                Console.SetCursorPosition(60, 24);
                Console.Write("Enter Pet Weight: ");
                appointmentData[appointmentIdx].petWeight = Console.ReadLine();
                Console.SetCursorPosition(60, 26);
                Console.Write("Pet Weight Edited!");
            }
            else if (selectedOption == 9)
            {
                Console.SetCursorPosition(60, 24);
                Console.Write("                                   ");
                Console.SetCursorPosition(60, 24);
                Console.Write("Enter Pet Gender: ");
                appointmentData[appointmentIdx].petGender = Console.ReadLine();
                Console.SetCursorPosition(60, 26);
                Console.Write("Pet Gender Edited!");
            }
            else if (selectedOption == 10)
            {
                Console.Write("1. Dermatology");
                Console.Write("2. Primary Care");
                Console.Write("3. Internal Medicine");
                Console.SetCursorPosition(60, 22);
                Console.Write("                                   ");
                Console.SetCursorPosition(60, 22);
                Console.Write("Select Requirement For Appointment: ");
                string selectedDoctor = Console.ReadLine();

                while (selectedDoctor != "1" && selectedDoctor != "2" && selectedDoctor != "3")
                {
                    Console.SetCursorPosition(60, 30);
                    Console.Write("Please Enter Correct Doctor! ");
                    Console.SetCursorPosition(60, 22);
                    Console.Write("                                                                      ");
                    Console.SetCursorPosition(60, 22);
                    Console.Write("Select Requirement For Appointment: ");
                    selectedDoctor = Console.ReadLine();
                    Console.SetCursorPosition(60, 30);
                    Console.Write("                                             ");
                }

                int doctorIdx = int.Parse(selectedDoctor);
                appointmentData[appointmentIdx].doctor = veterinarians[doctorIdx - 1];
                Console.SetCursorPosition(60, 30);
                Console.Write("Requirement Edited!");

            }
        }
    }

    
}

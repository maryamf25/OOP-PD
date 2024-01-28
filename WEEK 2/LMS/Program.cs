using LMS.BL;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LMS
{
    internal class Program
    {

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
            //sign up
            List<Teacher> teacher = new List<Teacher>();
            List<Student> student = new List<Student>();
            List<Courses> courses = new List<Courses>();
            List<string> registeredCourseName = new List<string>();
            List<int> registeredCourseId = new List<int>();
            int registerCount = 0;

            string Username = "";
            string Password = "";
            string courseName = "";
            int courseID = 0;
            int signInId = 0;
            string signInPassword = "";
            string Role = "";
            string role = "";
            int teacherCount = 0;
            int courseCount = 0;
            int studentCount = 0;
            while (true)
            {
                Console.Clear();

                Console.WriteLine("---------------------------------");
                Console.WriteLine("| < Learning Management System >|");
                Console.WriteLine("---------------------------------");
                Console.WriteLine("|      1. Register              |");
                Console.WriteLine("|      2. Login                 |");
                Console.WriteLine("|      3. Exit                  |");
                Console.WriteLine("---------------------------------");

                Console.Write("Enter Option to continue...");
                string option = Console.ReadLine();
                

                if (option == "1")
                {
                    GetSignUpDetails(ref Username, ref Password, ref Role, ref role);
                    
                    if(Role == "Teacher" )
                    {
                       
                        
                        teacherCount++;
                        Teacher newTeacher = new Teacher(Username, Password, teacherCount);
                        teacher.Add(newTeacher);
                        Console.WriteLine("Your ID is: " + teacherCount);


                    }
                    else if (Role == "Student" )
                    {
                       
                        studentCount++;
                        Student newStudent = new Student(Username, Password, studentCount);
                        student.Add(newStudent);
                        Console.WriteLine("Your ID is: " + studentCount);

                    }
                    Console.WriteLine("You are registered successfully as " + Role);
                    Console.Write("Press any key to continue...");
                    Console.ReadKey();
                }
                else if (option == "2")
                {
                    Console.WriteLine(option);
                    getSignInDetails(ref signInId, ref signInPassword);
                    
                    string signInValidity = signIn(signInId, signInPassword, teacher, student);
                   
                    if (signInValidity == "Invalid")
                    {
                        
                        Console.WriteLine("Invalid Credentials");
                       
                        Console.Write("Press any key to continue......");
                        Console.ReadKey();
                    }
                    else if (signInValidity == "Teacher")
                    {
                       while(true)
                        {
                            Console.WriteLine("You are logged in successfully as " + signInValidity + "!");
                            string Option = teacherMenu();
                            if (Option == "1")
                            {
                                Courses newCourse = addCourse(ref courseName, ref courseID);

                                courses.Add(newCourse);
                                courseCount++;
                                
                                Console.Write("Press any key to continue......");
                                Console.ReadKey();
                            }
                            else if (Option == "2")
                            {
                                showCourses(courses, ref courseCount);
                            }
                            else if (Option == "3")
                            {
                                break;
                            }
                        }
                       
                    }
                    else if (signInValidity == "Student")
                    {

                        
                        Console.WriteLine("You are logged in successfully as " + signInValidity + "!");
                        string Option = studentMenu();
                        while (true)
                        {
                            Console.WriteLine("You are logged in successfully as " + signInValidity + "!");
                            string stuOption = studentMenu();
                            if (stuOption == "1")
                            {
                                registerCourse(courses, ref courseCount, registeredCourseName, registeredCourseId, ref registerCount);
                                
                                Console.Write("Press any key to continue......");
                                Console.ReadKey();
                            }
                            else if (stuOption == "2")
                            {
                                showRegisterCourse(courses, ref courseCount, registeredCourseName, registeredCourseId, ref registerCount);

                                Console.Write("Press any key to continue......");
                                Console.ReadKey();
                            }
                            else if (stuOption == "3")
                            {
                                break;
                            }
                        }
                        


                    }
                    Console.Write("Press any key to continue......");
                    Console.ReadKey();
                }
                else if (option == "3")
                {
                    break;
                }
               
            }
        }
        static void GetSignUpDetails(ref string username, ref string password, ref string role, ref string roleInput)
        {
            Console.Clear ();
            Console.WriteLine("---------------------------------");
            Console.WriteLine("| <        Register           > |");
            Console.WriteLine("---------------------------------");
            Console.Write("Enter Username: ");
            username = Console.ReadLine();
            Console.Write("Enter Password: ");
            password = Console.ReadLine();

            Console.WriteLine("Select Role: ");
            Console.WriteLine("1. Teacher");
            Console.WriteLine("2. Student");
            roleInput = Console.ReadLine();

            if (roleInput == "1")
            {
                role = "Teacher";
            }
            else if (roleInput == "2")
            {
                role = "Student";
            }

        }
        static void getSignInDetails(ref int username, ref string password)
        {
            Console.Clear();
            Console.WriteLine("---------------------------------");
            Console.WriteLine("| <        LogIn              > |");
            Console.WriteLine("---------------------------------");
            Console.Write("Enter ID: ");
            username = int.Parse(Console.ReadLine());
            Console.Write("Enter Password: ");
            password = Console.ReadLine();

        }
        static string signIn(int signInId, string signInPassword, List<Teacher> teacher, List<Student> student)
        {
            int check = 0;
 
            string role = "Invalid";
            for (int x = 0; x < teacher.Count; x++)
            {
               
                if ((signInId == teacher[x].id) && (signInPassword == teacher[x].password) )
                {
                    check++;
                    
                    break;
                }
            }
            
            if (check> 0)
            {
                return "Teacher";
            }
            else

            {
                check = 0;
                for (int i = 0; i < student.Count; i++)
                {
                   
                    if ((signInId == student[i].id) && (signInPassword == student[i].password))
                    {
                        check++;
                        break;
                    }
                }

                if (check > 0)
                {
                    return "Student";
                    
                }
            }
            return role;

        }
        static string teacherMenu()
        {
            Console.Clear();
            Console.WriteLine("---------------------------------");
            Console.WriteLine("|          < Teacher Menu      > |");
            Console.WriteLine("---------------------------------");
            Console.WriteLine("|      1. Add Course            |");
            Console.WriteLine("|      2. Show Courses          |");
            Console.WriteLine("|      3. Exit                  |");
            Console.WriteLine("---------------------------------");
            Console.Write("Enter Option to continue...");
            string option = Console.ReadLine();
            return option;

        }
        static Courses addCourse(ref string name, ref int id)
        {
            Console.Clear();

            Console.WriteLine("---------------------------------");
            Console.WriteLine("| <        Add Courses       > |");
            Console.WriteLine("---------------------------------");
            Console.Write("Enter Course ID: ");
            string ID = Console.ReadLine();
            id = int.Parse(ID);
            Console.Write("Enter Course Name: ");
            name = Console.ReadLine();

            Courses course = new Courses(name, id);
           
            return course;
        }
        static void showCourses(List<Courses> course, ref int countCourses)
        {
            Console.Clear();
            Console.WriteLine("-------------------------");
            Console.WriteLine("|   <  Show Courses  >  |");
            Console.WriteLine("-------------------------");
            if (countCourses > 0)
            {
                Console.WriteLine("---------------------------");
                Console.WriteLine("| Course ID  |   Name    |");
                Console.WriteLine("---------------------------");


                for (int x = 0; x < countCourses; x++)
                {
                  
                    Console.WriteLine($"| {course[x].id,-11} | {course[x].name,-9} |");
                    Console.WriteLine("-------------------------");
                }

               
            }
            else
            {
                Console.WriteLine("There are no Courses to show");

            }
            Console.Write("Press any key to continue...");
            Console.Read();
        }
        static void registerCourse(List<Courses> course, ref int countCourses, List<string> registeredCourseName, List<int> registeredCourseId, ref int registerCount)
        {
            Console.Clear();
            Console.WriteLine("-------------------------");
            Console.WriteLine("|   <  Show Courses  >  |");
            Console.WriteLine("-------------------------");
            if (countCourses > 0)
            {
                Console.WriteLine("---------------------------------");
                Console.WriteLine("| NO.  |   Name    |   Name    |");
                Console.WriteLine("---------------------------------");


                for (int x = 0; x < countCourses; x++)
                {

                    Console.WriteLine($"| {x+1,-3} |  {course[x].id,-11} | {course[x].name,-9} |");
                    Console.WriteLine("---------------------------------");
                }

                Console.Write("Enter Course NUMBER to register...");
                string Number = Console.ReadLine();
                int courseNumber = int.Parse(Number);
                registeredCourseName.Add(course[courseNumber - 1].name);
                registeredCourseId.Add(course[courseNumber - 1].id);
                registerCount++;
               
                Console.WriteLine("Course added successfully!");
            }
            else
            {
                Console.WriteLine("There are no Courses to show");

            }
            
           
        }
        static void showRegisterCourse(List<Courses> course, ref int countCourses, List<string> registeredCourseName, List<int> registeredCourseId, ref int registerCount)
        {
            Console.Clear();
            Console.WriteLine("-------------------------");
            Console.WriteLine("|   <  Show Courses  >  |");
            Console.WriteLine("-------------------------");
            if (countCourses > 0)
            {
                Console.WriteLine("---------------------------------");
                Console.WriteLine("|    Course Id  |   Name    |");
                Console.WriteLine("---------------------------------");

                for (int x = 0; x < registerCount; x++)
                {

                    Console.WriteLine($"|{registeredCourseId[x],-11} | {registeredCourseName[x],-9} |");
                    Console.WriteLine("---------------------------------");
                }

              
            }
            else
            {
                Console.WriteLine("There are no Courses to show");

            }


        }
        static string studentMenu()
        {
            Console.Clear();
            Console.WriteLine("---------------------------------");
            Console.WriteLine("|          < Teacher Menu      > |");
            Console.WriteLine("---------------------------------");
            Console.WriteLine("|      1. Register Course       |");
            Console.WriteLine("|      2. Show Courses          |");
            Console.WriteLine("|      3. Exit                  |");
            Console.WriteLine("---------------------------------");
            Console.Write("Enter Option to continue...");
            string option = Console.ReadLine();
            return option;

        }
    }
    }


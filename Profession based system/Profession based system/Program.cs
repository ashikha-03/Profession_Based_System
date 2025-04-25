using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Database database = new Database();

        Teacher teacher = new Teacher { Name = "Dharani", ID = "T001", Age = 30 };
        await database.AddAsync(teacher.Name, "teacher", teacher.ID, teacher.Age);

        Student student = new Student { Name = "Bharu", ID = "S001", Age = 22 };
        await database.AddAsync(student.Name, "student", student.ID, student.Age);

        LabAssistant labAssistant = new LabAssistant { Name = "Geetha", ID = "L001", Age = 28 };
        await database.AddAsync(labAssistant.Name, "labassistant", labAssistant.ID, labAssistant.Age);

        SecurityGuard securityGuard = new SecurityGuard { Name = "Shri", ID = "SG001", Age = 35 };
        await database.AddAsync(securityGuard.Name, "securityguard", securityGuard.ID, securityGuard.Age);

        string adminUsername = "admin";
        string adminPassword = "admin123";
        string userUsername = "user";
        string userPassword = "user123";

        Console.WriteLine("Welcome to the system!");
        Console.WriteLine("Are you an Admin or a User? (Type 'admin' or 'user')");

        string role = Console.ReadLine().ToLower();

        string username, password;
        bool isValidUser = false;
        bool isAdmin = false;

        if (role == "admin")
        {
            Console.WriteLine("Enter admin username:");
            username = Console.ReadLine();
            Console.WriteLine("Enter admin password:");
            password = Console.ReadLine();

            if (username == adminUsername && password == adminPassword)
            {
                isAdmin = true;
                Console.WriteLine("Admin login successful.");
                isValidUser = true;
            }
            else
            {
                Console.WriteLine("Invalid admin credentials.");
            }
        }
        else if (role == "user")
        {
            Console.WriteLine("Enter user username:");
            username = Console.ReadLine();
            Console.WriteLine("Enter user password:");
            password = Console.ReadLine();

            if (username == userUsername && password == userPassword)
            {
                isAdmin = false;
                Console.WriteLine("User login successful.");
                isValidUser = true;
            }
            else
            {
                Console.WriteLine("Invalid user credentials.");
            }
        }
        else
        {
            Console.WriteLine("Invalid role. Exiting.");
            return;
        }

        if (!isValidUser)
        {
            return;
        }

        while (true)
        {
            if (isAdmin)
            {
                Console.WriteLine("\nAdmin Menu:");
                Console.WriteLine("1. Add People");
                Console.WriteLine("2. Remove People");
                Console.WriteLine("3. View All People");
                Console.WriteLine("4. Exit");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter name:");
                        string name = Console.ReadLine();
                        Console.WriteLine("Enter profession (Teacher, Student, LabAssistant, SecurityGuard):");
                        string professionType = Console.ReadLine();
                        Console.WriteLine("Enter ID:");
                        string id = Console.ReadLine();
                        Console.WriteLine("Enter age:");
                        int age = int.Parse(Console.ReadLine());

                        await database.AddAsync(name, professionType, id, age);
                        break;

                    case 2:
                        Console.WriteLine("Enter ID to remove:");
                        string idToRemove = Console.ReadLine();
                        Profession professionToRemove = database.GetPersonById(idToRemove);

                        if (professionToRemove != null)
                        {
                            await database.RemoveAsync(professionToRemove);
                        }
                        else
                        {
                            Console.WriteLine("Person not found.");
                        }
                        break;

                    case 3:
                        database.ViewAll();
                        break;

                    case 4:
                        Console.WriteLine("Exiting admin menu...");
                        return;

                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("\nUser Menu:");
                Console.WriteLine("1. View Your Details");
                Console.WriteLine("2. View Schedule");
                Console.WriteLine("3. Exit");

                
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter your ID:");
                        string userId = Console.ReadLine();

                        Profession userPerson = database.GetPersonById(userId);

                        if (userPerson != null)
                        {
                            Console.WriteLine($"Name: {userPerson.Name}, ID: {userPerson.ID}, Age: {userPerson.Age}");
                            if (userPerson is IPaidProfession paidProfession)
                            {
                                var salary = paidProfession.GetSalary().Result;
                                var bonus = paidProfession.GetBonus().Result;
                                Console.WriteLine($"Salary: {salary}, Bonus: {bonus}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("User not found.");
                        }
                        break;

                    case 2:
                        Console.WriteLine("Enter your ID:");
                        string scheduleUserId = Console.ReadLine();

                        Profession scheduleUser = database.GetPersonById(scheduleUserId);

                        if (scheduleUser != null)
                        {
                            database.ShowSchedule(scheduleUser);
                        }
                        else
                        {
                            Console.WriteLine("User not found.");
                        }
                        break;

                    case 3:
                        Console.WriteLine("Exiting...");
                        return;

                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }
    }
}

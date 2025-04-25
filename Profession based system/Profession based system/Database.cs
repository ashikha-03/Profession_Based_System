using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Database
{
    private List<Profession> people = new List<Profession>();

    public async Task AddAsync(string name, string professionType, string id, int age)
    {
        await Task.Delay(200); // Simulate async operation
        Profession profession = null;

        // Create the appropriate profession based on input
        switch (professionType.ToLower())
        {
            case "teacher":
                profession = new Teacher { Name = name, ID = id, Age = age };
                break;
            case "student":
                profession = new Student { Name = name, ID = id, Age = age };
                break;
            case "labassistant":
                profession = new LabAssistant { Name = name, ID = id, Age = age };
                break;
            case "securityguard":
                profession = new SecurityGuard { Name = name, ID = id, Age = age };
                break;
            default:
                Console.WriteLine("Unknown profession type.");
                return;
        }

        people.Add(profession);
        Console.WriteLine($"{name} has been added as a {professionType}.");
    }

    public async Task RemoveAsync(Profession profession)
    {
        await Task.Delay(200); // Simulate async operation
        people.Remove(profession);
        Console.WriteLine($"{profession.Name} has been removed from the database.");
    }

    public List<Profession> GetPeople()
    {
        return people;
    }

    public void ViewAll()
    {
        Console.WriteLine("Listing all people:");
        foreach (var person in people)
        {
            Console.WriteLine($"ID: {person.ID}, Name: {person.Name}, Profession: {person.GetType().Name}");
            if (person is IPaidProfession paidProfession)
            {
                var salary = paidProfession.GetSalary().Result;
                var bonus = paidProfession.GetBonus().Result;
                Console.WriteLine($"Salary: {salary}, Bonus: {bonus}");
            }
        }
    }

    public Profession GetPersonById(string id)
    {
        return people.Find(p => p.ID == id);
    }

    public void ShowSchedule(Profession profession)
    {
        if (profession is Teacher)
        {
            Console.WriteLine($"{profession.Name}'s Schedule: Teaching classes from 9 AM to 3 PM.");
        }
        else if (profession is LabAssistant)
        {
            Console.WriteLine($"{profession.Name}'s Schedule: Assisting in lab from 8 AM to 4 PM.");
        }
        else if (profession is SecurityGuard)
        {
            Console.WriteLine($"{profession.Name}'s Schedule: Guarding premises from 6 AM to 6 PM.");
        }
        else if (profession is Student)
        {
            Console.WriteLine($"{profession.Name}'s Schedule: Attending classes from 10 AM to 3 PM.");
        }
        else
        {
            Console.WriteLine($"{profession.Name} does not have a defined schedule.");
        }
    }
}

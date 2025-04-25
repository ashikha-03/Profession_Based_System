using System;
using System.Threading.Tasks;

public class Teacher : Profession, IPaidProfession
{
    public async Task<double> GetSalary()
    {
        await Task.Delay(100); 
        return 50000; 
    }

    public async Task<double> GetBonus()
    {
        var salary = await GetSalary();
        return salary * 0.10; 
    }

    public void Teach()
    {
        Console.WriteLine("Teaching the students.");
    }
}

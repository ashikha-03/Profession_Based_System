using System;
using System.Threading.Tasks;

public class LabAssistant : Profession, IPaidProfession
{
    public async Task<double> GetSalary()
    {
        await Task.Delay(100); 
        return 35000; 
    }

    public async Task<double> GetBonus()
    {
        var salary = await GetSalary();
        return salary * 0.05; 
    }

    public void AssistInLab()
    {
        Console.WriteLine("Assisting in the lab.");
    }
}

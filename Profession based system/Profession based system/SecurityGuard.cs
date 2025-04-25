using System;
using System.Threading.Tasks;

public class SecurityGuard : Profession, IPaidProfession
{
    public async Task<double> GetSalary()
    {
        await Task.Delay(100); 
        return 30000; 
    }

    public async Task<double> GetBonus()
    {
        var salary = await GetSalary();
        return salary * 0.03; // 3% Bonus
    }

    public void Guard()
    {
        Console.WriteLine("Guarding the premises.");
    }
}

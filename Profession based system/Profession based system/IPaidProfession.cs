using System.Threading.Tasks;

public interface IPaidProfession
{
    Task<double> GetSalary();
    Task<double> GetBonus();
}

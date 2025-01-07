using SeasonalBite.Data;
using SeasonalBite.Models;

namespace SeasonalBite.Services;

public class DatabaseService
{
    public DatabaseService(CockroachDb db)
    {
        Console.WriteLine("inject cockroachdb");
    }
    
    public async Task<IEnumerable<Aliment>> GetAlimentsAsync()
    {
        var aliments = new List<Aliment>();

        Console.WriteLine("Get all aliments");

        return aliments;
    }
}
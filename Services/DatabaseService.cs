using SeasonalBite.Data;
using SeasonalBite.Models;

namespace SeasonalBite.Services;

public class DatabaseService
{
    public DatabaseService(CockroachDB db)
    {
        Console.WriteLine("establish db connection");
    }
    
    public async Task<IEnumerable<Aliment>> GetAlimentsAsync()
    {
        var aliments = new List<Aliment>();
        
        Console.WriteLine("Get all aliments");
        
        return aliments;
    }
}
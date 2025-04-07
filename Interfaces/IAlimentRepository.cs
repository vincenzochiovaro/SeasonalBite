using SeasonalBite.Models;

namespace SeasonalBite.Interfaces;

public interface IAlimentRepository
{
    Task<IEnumerable<Aliment>> GetAlimentsAsync(int currentMonth);
}
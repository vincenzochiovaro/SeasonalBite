using SeasonalBite.Models;

namespace SeasonalBite.Interfaces;

public interface IAlimentHelper
{
    Task<IEnumerable<Aliment>> FilterAlimentsInSeason(int currentMonth);
}
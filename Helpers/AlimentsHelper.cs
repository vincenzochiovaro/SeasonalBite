using SeasonalBite.Interfaces;
using SeasonalBite.Models;

namespace SeasonalBite.Helpers;

public class AlimentsHelper : IAlimentHelper
{
    private readonly IAlimentRepository _alimentRepository;

    public AlimentsHelper(IAlimentRepository alimentRepository)
    {
        _alimentRepository = alimentRepository;
    }

    public async Task<IEnumerable<Aliment>> FilterAlimentsInSeason(int currentMonth)
    {
        var allAliments = await _alimentRepository.GetAlimentsAsync();

        var alimentsInSeason = new List<Aliment>();

        foreach (var aliment in allAliments)
        {
            if (aliment.FromMonthInt <= aliment.ToMonthInt)
            {
                if (currentMonth >= aliment.FromMonthInt && currentMonth <= aliment.ToMonthInt)
                {
                    alimentsInSeason.Add(aliment);
                }
            }
            else
            {
                if (currentMonth >= aliment.FromMonthInt || currentMonth <= aliment.ToMonthInt)
                {
                    alimentsInSeason.Add(aliment);
                }
            }
        }

        return alimentsInSeason;
    }
}
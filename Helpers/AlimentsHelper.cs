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
        var allAliments = await _alimentRepository.GetAlimentsAsync(currentMonth);
        return allAliments;
    }
}
using SeasonalBite.Interfaces;
using SeasonalBite.Models;

namespace SeasonalBite.Services;

public class CockroachDbRepository : IAlimentRepository
{
    private IDbManager _db;

    public CockroachDbRepository(IDbManager db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Aliment>> GetAlimentsAsync(int currentMonth)
    {
        try
        {
            var query = $@"
                SELECT aliment_seeds.name
                FROM seed_sowing_months
                JOIN aliment_seeds ON seed_sowing_months.seed_id = aliment_seeds.id
                WHERE seed_sowing_months.sowing_month = {currentMonth};
            ";

            var reader = await _db.ExecuteReader(query);

            var aliments = new List<Aliment>();

            while (await reader.ReadAsync())
            {
                var aliment = new Aliment
                {
                    Name = reader.GetString(reader.GetOrdinal("name"))
                };

                aliments.Add(aliment);
            }

            return aliments;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Critical Error while retrieving aliments.", ex);
        }
    }
}
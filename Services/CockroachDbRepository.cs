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

    public async Task<IEnumerable<Aliment>> GetAlimentsAsync()
    {
        try
        {
            var reader = await _db.ExecuteReader("SELECT * FROM Aliments");

            var aliments = new List<Aliment>();

            while (await reader.ReadAsync())
            {
                var aliment = new Aliment
                {
                    Name = reader.GetString(reader.GetOrdinal("name")),
                    Category = reader.GetString(reader.GetOrdinal("category")),
                    FromMonth = reader.GetString(reader.GetOrdinal("from_month")),
                    ToMonth = reader.GetString(reader.GetOrdinal("to_month")),
                    FromMonthInt = reader.GetInt32(reader.GetOrdinal("from__month")),
                    ToMonthInt = reader.GetInt32(reader.GetOrdinal("to__month")),
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
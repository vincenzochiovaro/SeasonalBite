using Npgsql;

namespace SeasonalBite.Interfaces;

public interface IDbManager
{
    Task<NpgsqlDataReader> ExecuteReader(string query);
}
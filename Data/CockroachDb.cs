using Npgsql;
using SeasonalBite.Interfaces;

namespace SeasonalBite.Data;

public class CockroachDb : IDbManager
{
    private string _connectionString;

    public CockroachDb()
    {
        _connectionString = Environment.GetEnvironmentVariable("COCKROACH_CONN_STR");

        if (string.IsNullOrEmpty(_connectionString))
        {
            throw new Exception("Connection string not found.");
        }
    }

    public async Task<NpgsqlDataReader> ExecuteReader(string query)
    {
        var dataSource = NpgsqlDataSource.Create(_connectionString);
        await using var command = dataSource.CreateCommand(query);
        return await command.ExecuteReaderAsync();
    }
}
using Npgsql;
using SeasonalBite.Interfaces;

namespace SeasonalBite.Data;

public class CockroachDb : IDbManager
{
    private string _connectionString;

    public CockroachDb()
    {
        // _connectionString = Environment.GetEnvironmentVariable("COCKROACH_CONN_STR");
        var foo =
            "Host=seasonalbite-cluster-6350.j77.aws-eu-west-1.cockroachlabs.cloud;Username=vincenzo;Password=fYqicPFDBUdDs-Ni_LPJ5Q;Database=SeasonalBiteDB;Port=26257;";
        
        _connectionString = foo;
        
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
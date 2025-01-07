using Npgsql;

namespace SeasonalBite.Data;

public class CockroachDb
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
    
    public NpgsqlConnection GetConnection()
    {
        var connection = new NpgsqlConnection(_connectionString);
        return connection;
    }
}
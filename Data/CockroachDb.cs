using System.Text.Json;
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
            try
            {
                using var stream = GetType().Assembly.GetManifestResourceStream("SeasonalBite.secrets.json");
                if (stream != null)
                {
                    using var reader = new StreamReader(stream);
                    var json = reader.ReadToEnd();
                    var secrets = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
                    _connectionString = secrets["COCKROACH_CONN_STR"];
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading secrets: {ex.Message}");
            }
        }
    }

    public async Task<NpgsqlDataReader> ExecuteReader(string query)
    {
        var dataSource = NpgsqlDataSource.Create(_connectionString);
        await using var command = dataSource.CreateCommand(query);
        return await command.ExecuteReaderAsync();
    }
}
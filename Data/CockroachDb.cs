using System.Text.Json;
using Npgsql;
using SeasonalBite.Interfaces;

namespace SeasonalBite.Data;

public class CockroachDb : IDbManager
{
    private string _connectionString;

    public CockroachDb()
    {
        /// <summary>
        /// Tries to get the connection string from environment variables first (used for Windows and iOS development).
        /// If not found, attempts to load it from the 'secrets.json' file (used for Android development).
        /// </summary>

        _connectionString = Environment.GetEnvironmentVariable("COCKROACH_CONN_STR");

        if (string.IsNullOrEmpty(_connectionString))
        {
            try
            {
                var secretsPath = Path.Combine(FileSystem.AppDataDirectory, "secrets.json");
                if (File.Exists(secretsPath))
                {
                    var json = File.ReadAllText(secretsPath);
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
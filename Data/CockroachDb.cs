using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Npgsql;
using SeasonalBite.Interfaces;

namespace SeasonalBite.Data;

public class CockroachDb : IDbManager
{
    private string _connectionString;

    public CockroachDb()
    {
        var keyVaultUri = "https://seasonalbitevault.vault.azure.net/";

        var credential = new DefaultAzureCredential();
        var client = new SecretClient(new Uri(keyVaultUri), credential);
        var keyVaultDbConnString = client.GetSecret("CockroachDbConnectionString").Value;

        _connectionString = keyVaultDbConnString.Value;

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
using FluentAssertions;
using SeasonalBite.Data;
using SeasonalBite.Interfaces;
using SeasonalBite.Models;
using SeasonalBite.Services;

namespace SeasonalBiteUnitTests.IntegrationTests;

public class CockroachDbRepositoryTests
{
    private readonly IDbManager _db; 
    private readonly CockroachDbRepository _repository;
    public CockroachDbRepositoryTests()
    {
        _db = new CockroachDb();
        _repository = new CockroachDbRepository(_db);
    }
    [Fact]
    public async void GivenValidDatabaseConn_WhenGetAlimentCalled_ThenReturnAlimentList()
    {
        // When
        var result = await _repository.GetAlimentsAsync();
        
        // Then
        var firstAliment = result.First();
        firstAliment.Should().NotBeNull();
        result.Should().BeOfType<List<Aliment>>();
        result.Should().NotBeEmpty();
    }
}
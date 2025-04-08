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
    private const string AllYearRoundAliment = "Prezzemolo";
    private const string AprilMayAliment = "Zucca";
    private const string JuneJulyAliment = "Porro";

    public CockroachDbRepositoryTests()
    {
        _db = new CockroachDb();
        _repository = new CockroachDbRepository(_db);
    }

    [Fact]
    public async void GivenValidDatabaseConn_WhenGetAlimentCalled_ThenReturnAlimentList()
    {
        // When
        var result = await _repository.GetAlimentsAsync(1);

        // Then
        result.Select(x => x.Name).Should().Contain(AllYearRoundAliment);
        result.Should().BeOfType<List<Aliment>>();
        result.Should().NotBeEmpty();
    }

    [Fact]
    public async void GivenValidDatabaseConn_WhenGetAlimentCalledWithSpecificMonth_ThenReturnExpectedAliment()
    {
        // When
        var result = await _repository.GetAlimentsAsync(4);

        var foo = result.Select(x => x.Name);
        // Then
        result.Select(x => x.Name).Should().Contain(AprilMayAliment);
        result.Select(x => x.Name).Should().Contain(AllYearRoundAliment);
        result.Select(x => x.Name).Should().NotContain(JuneJulyAliment);

        result.Should().BeOfType<List<Aliment>>();
        result.Should().NotBeEmpty();
    }
}
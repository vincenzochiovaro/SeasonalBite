using System.Text.Json;
using FluentAssertions;
using Moq;
using SeasonalBite.Helpers;
using SeasonalBite.Interfaces;
using SeasonalBite.Models;

namespace SeasonalBiteUnitTests.UnitTests;

public class AlimentHelperTests
{
    Mock<IAlimentRepository> _alimentRepositoryMock;

    public AlimentHelperTests()
    {
        _alimentRepositoryMock = new Mock<IAlimentRepository>();
    }

    [Theory]
    [InlineData(1, "Parsnip")]
    [InlineData(2, "Parsnip")]
    [InlineData(10, "Parsnip")]
    [InlineData(11, "Parsnip")]
    [InlineData(12, "Parsnip")]
    public async void given_valid_month_when_filtering_aliments_then_successfully_return_aliments_in_season(int monthUnderTest, string alimentUnderTest)
    {
        // Given
        _alimentRepositoryMock.Setup(x => x.GetAlimentsAsync()).ReturnsAsync(AlimentsTestData());

        // When
        var sut = new AlimentsHelper(_alimentRepositoryMock.Object);

        var result = await sut.FilterAlimentsInSeason(monthUnderTest);

        // Then
        result.Any(aliment => aliment.Name == "Parsnip").Should().BeTrue();
        result.Should().BeOfType<List<Aliment>>();

        var assertAliment = result.FirstOrDefault(aliment => aliment.Name == alimentUnderTest);
        assertAliment.Should().NotBeNull();
        assertAliment.Category.Should().Be("Vegetable");
    }

    [Theory]
    [InlineData(3, "Parsnip")]
    [InlineData(4, "Parsnip")]
    [InlineData(5, "Parsnip")]
    [InlineData(6, "Parsnip")]
    [InlineData(7, "Parsnip")]
    [InlineData(8, "Parsnip")]
    [InlineData(9, "Parsnip")]
    public async void given_invalid_month_when_filtering_aliments_then_exclude_aliments_not_in_season(int monthUnderTest, string alimentUnderTest)
    {
        // Given
        _alimentRepositoryMock.Setup(x => x.GetAlimentsAsync()).ReturnsAsync(AlimentsTestData());

        // When
        var sut = new AlimentsHelper(_alimentRepositoryMock.Object);

        var result = await sut.FilterAlimentsInSeason(monthUnderTest);

        // Then
        result.Any(aliment => aliment.Name == "Parsnip").Should().BeFalse();
        result.Should().BeOfType<List<Aliment>>();
        result.FirstOrDefault(aliment => aliment.Name == alimentUnderTest).Should().BeNull();
    }

    [Theory]
    [InlineData(8, "Plums")]
    [InlineData(9, "Plums")]
    public async void given_month_for_fruit_aliment_when_filtering_aliments_then_category_should_be_fruit(int monthUnderTest, string alimentUnderTest)
    {
        // Given
        _alimentRepositoryMock.Setup(x => x.GetAlimentsAsync()).ReturnsAsync(AlimentsTestData());

        // When
        var sut = new AlimentsHelper(_alimentRepositoryMock.Object);

        var result = await sut.FilterAlimentsInSeason(monthUnderTest);

        // Then
        result.Any(aliment => aliment.Name == "Plums").Should().BeTrue();
        result.Should().BeOfType<List<Aliment>>();
        var assertAliment = result.FirstOrDefault(aliment => aliment.Name == alimentUnderTest);
        assertAliment.Should().NotBeNull();
        assertAliment.Category.Should().Be("Fruit");
    }

    private IEnumerable<Aliment> AlimentsTestData()
    {
        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AlimentsTestData.json");
        var jsonAliments = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<IEnumerable<Aliment>>(jsonAliments)!;
    }
}
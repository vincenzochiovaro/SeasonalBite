namespace SeasonalBite.Models;

public class Aliment
{
    public required string Name { get; set; }
    public string ImageUrl => $"{Name.ToLower().Replace(" ", "")}.png";
}
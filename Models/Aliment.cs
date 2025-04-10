namespace SeasonalBite.Models;

public class Aliment
{
    public required string Name { get; set; }
    public string ImageUrl => $"https://raw.githubusercontent.com/vincenzochiovaro/SeasonalBite-assets/refs/heads/main/images/{Name.ToLower().Replace(" ", "")}.png";
}
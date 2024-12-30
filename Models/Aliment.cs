namespace SeasonalBite.Models;

public class Aliment
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Category { get; set; }
    public string? FromMonth { get; set; }
    public string? ToMonth { get; set; }
    public int FromMonthInt { get; set; }
    public int ToMontInt { get; set; }
    
    public string ImageUrl => $"{Name.ToLower().Replace(" ", "")}.png";
    public string DateRange => $"From {FromMonth} To {ToMonth}";
}
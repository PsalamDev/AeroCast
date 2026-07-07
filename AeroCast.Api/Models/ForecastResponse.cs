public class ForecastResponse
{
    public List<ForecastItem> List { get; set; } = [];

    public City City { get; set; } = new();
}
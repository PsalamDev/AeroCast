public class ForecastDto
{
    public string City { get; set; } = string.Empty;

    public string Country { get; set; } = string.Empty;

    public List<ForecastItemDto> Forecasts { get; set; } = [];
}
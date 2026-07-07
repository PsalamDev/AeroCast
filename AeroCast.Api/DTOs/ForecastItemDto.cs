public class ForecastItemDto
{
    public DateTime Date { get; set; }

    public double Temperature { get; set; }

    public double FeelsLike { get; set; }

    public double MinTemperature { get; set; }

    public double MaxTemperature { get; set; }

    public int Humidity { get; set; }

    public double WindSpeed { get; set; }

    public string Description { get; set; } = string.Empty;

    public string Icon { get; set; } = string.Empty;
}
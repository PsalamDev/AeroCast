
public class CurrentWeatherDto
{
    public string City { get; set; } = string.Empty;

    public string Country { get; set; } = string.Empty;

    public double Temperature { get; set; }

    public double FeelsLike { get; set; }

    public double MinTemperature { get; set; }

    public double MaxTemperature { get; set; }

    public int Humidity { get; set; }

    public int Pressure { get; set; }

    public double WindSpeed { get; set; }

    public string Description { get; set; } = string.Empty;

    public string Icon { get; set; } = string.Empty;

    public long Sunrise { get; set; }

    public long Sunset { get; set; }

    public int Visibility { get; set; }
}
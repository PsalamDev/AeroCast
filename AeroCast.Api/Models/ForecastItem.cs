
public class ForecastItem
{
    public MainInfo Main { get; set; } = new();

    public List<WeatherInfo> Weather { get; set; } = [];

    public WindInfo Wind { get; set; } = new();

    public string Dt_Txt { get; set; } = string.Empty;
}
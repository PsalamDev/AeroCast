public class CurrentWeatherResponse
{

  public Coordinates Coord { get; set; } = new();

    public List<WeatherInfo> Weather { get; set; } = [];

    public MainInfo Main { get; set; } = new();

    public WindInfo Wind { get; set; } = new();

    public SysInfo Sys { get; set; } = new();

    public string Name { get; set; } = string.Empty;

    public int Visibility { get; set; }
}
using System.Text.Json;

namespace AeroCast.Api.Services;

public class WeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public WeatherService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<CurrentWeatherDto?> GetCurrentWeatherAsync(string city)
    {
        var apiKey = _configuration["OpenWeather:ApiKey"];

        var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&appid={apiKey}";

        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            return null;

        var json = await response.Content.ReadAsStringAsync();

        var weather = JsonSerializer.Deserialize<CurrentWeatherResponse>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        if (weather is null)
            return null;

        return new CurrentWeatherDto
        {
            City = weather.Name,
            Country = weather.Sys.Country,
            Temperature = weather.Main.Temp,
            FeelsLike = weather.Main.FeelsLike,
            MinTemperature = weather.Main.TempMin,
            MaxTemperature = weather.Main.TempMax,
            Humidity = weather.Main.Humidity,
            Pressure = weather.Main.Pressure,
            WindSpeed = weather.Wind.Speed,
            Description = weather.Weather.FirstOrDefault()?.Description ?? "",
            Icon = weather.Weather.FirstOrDefault()?.Icon ?? "",
            Sunrise = weather.Sys.Sunrise,
            Sunset = weather.Sys.Sunset,
            Visibility = weather.Visibility
        };
    }

    public async Task<ForecastDto?> GetForecastAsync(string city)
    {
        var apiKey = _configuration["OpenWeather:ApiKey"];

        var url = $"https://api.openweathermap.org/data/2.5/forecast?q={city}&units=metric&appid={apiKey}";

        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            return null;

        var json = await response.Content.ReadAsStringAsync();

        var forecast = JsonSerializer.Deserialize<ForecastResponse>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        if (forecast is null)
            return null;

        return new ForecastDto
        {
            City = forecast.City.Name,
            Country = forecast.City.Country,

            Forecasts = forecast.List.Select(item => new ForecastItemDto
            {
                Date = DateTime.Parse(item.Dt_Txt),
                Temperature = item.Main.Temp,
                FeelsLike = item.Main.FeelsLike,
                MinTemperature = item.Main.TempMin,
                MaxTemperature = item.Main.TempMax,
                Humidity = item.Main.Humidity,
                WindSpeed = item.Wind.Speed,
                Description = item.Weather.FirstOrDefault()?.Description ?? "",
                Icon = item.Weather.FirstOrDefault()?.Icon ?? ""
            }).ToList()
        };
    }
}
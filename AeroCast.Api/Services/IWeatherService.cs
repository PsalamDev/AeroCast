
namespace AeroCast.Api.Services;

public interface IWeatherService
{
Task<CurrentWeatherDto?> GetCurrentWeatherAsync(string city);

   Task<ForecastDto?> GetForecastAsync(string city);

   Task<CurrentWeatherDto?> GetWeatherByLocationAsync(
    double lat,
    double lon);
}
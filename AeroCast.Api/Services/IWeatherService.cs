
namespace AeroCast.Api.Services;

public interface IWeatherService
{
Task<CurrentWeatherDto?> GetCurrentWeatherAsync(string city);

   Task<ForecastDto?> GetForecastAsync(string city);
}
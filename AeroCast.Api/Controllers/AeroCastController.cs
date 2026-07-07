using AeroCast.Api.Services;
using Microsoft.AspNetCore.Mvc;
namespace AeroCast.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AeroCastController : ControllerBase
{
    private readonly IWeatherService _weatherService;

    public AeroCastController(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    [HttpGet("current")]
    public async Task<IActionResult> GetCurrentWeather([FromQuery] string city)
    {
        if (string.IsNullOrWhiteSpace(city))
        {
            return BadRequest(new
            {
                Message = "City is required."
            });
        }

        var weather = await _weatherService.GetCurrentWeatherAsync(city);

        if (weather is null)
        {
            return NotFound(new
            {
                Message = $"Weather data for '{city}' was not found."
            });
        }

        return Ok(weather);
    }

    [HttpGet("forecast")]
    public async Task<IActionResult> GetForecast([FromQuery] string city)
    {
        if (string.IsNullOrWhiteSpace(city))
        {
            return BadRequest(new
            {
                Message = "City is required."
            });
        }

        var forecast = await _weatherService.GetForecastAsync(city);

        if (forecast is null)
        {
            return NotFound(new
            {
                Message = $"Forecast data for '{city}' was not found."
            });
        }

        return Ok(forecast);
    }


[HttpGet("current/location")]
public async Task<IActionResult> GetWeatherByLocation(
    [FromQuery] double lat,
    [FromQuery] double lon)
{
    var weather = await _weatherService.GetWeatherByLocationAsync(lat, lon);

    if (weather is null)
    {
        return NotFound(new
        {
            Message = "Weather data not found."
        });
    }

    return Ok(weather);
}


}
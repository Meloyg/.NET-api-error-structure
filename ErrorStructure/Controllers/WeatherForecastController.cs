using ErrorStructure.Exceptions;
using ErrorStructure.Helpers;
using ErrorStructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace ErrorStructure.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IWeatherService _weatherService;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherService weatherService)
    {
        _logger = logger;
        _weatherService = weatherService;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<ApiResponse<IEnumerable<WeatherForecast>>> GetAllWeatherForecasts()
    {
        try
        {
            _logger.LogInformation("Getting all weather forecasts...");
            var weatherForecasts = _weatherService.GetAllWeatherForecasts();

            return new ApiResponse<IEnumerable<WeatherForecast>>
            {
                Success = true,
                Message = "Successfully retrieved weather forecasts.",
                Data = weatherForecasts
            };
        }
        catch (Exception e)
        {
            throw new Exception("An error occurred while processing your request.", e);
        }
    }
}
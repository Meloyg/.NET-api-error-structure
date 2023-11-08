using ErrorStructure.Helpers;
using ErrorStructure.Models;
using ErrorStructure.Services;
using ErrorStructure.Validations;
using Microsoft.AspNetCore.Mvc;

namespace ErrorStructure.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IWeatherService _weatherService;
    private readonly IWeatherForecastValidator _validator;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherService weatherService,
        IWeatherForecastValidator validator)
    {
        _logger = logger;
        _weatherService = weatherService;
        _validator = validator;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public ActionResult<ApiResponse<IEnumerable<WeatherForecast>>> GetAllWeatherForecasts()
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

    [HttpGet("{summary}", Name = "GetWeatherForecastBySummary")]
    public ActionResult<ApiResponse<WeatherForecast>> GetWeatherForecastBySummary(string summary)
    {
        _logger.LogInformation($"Getting weather forecast with summary '{summary}'...");
        bool isValid = _validator.ValidateSummary(summary);

        var weatherForecast = _weatherService.GetWeatherForecastBySummary(summary);

        return new ApiResponse<WeatherForecast>
        {
            Success = true,
            Message = $"Successfully retrieved weather forecast with summary '{summary}'.",
            Data = weatherForecast
        };
    }
}
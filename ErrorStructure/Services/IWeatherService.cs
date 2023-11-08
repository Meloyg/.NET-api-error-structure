using ErrorStructure.Models;

namespace ErrorStructure.Services;

public interface IWeatherService
{
    IEnumerable<WeatherForecast> GetAllWeatherForecasts();
    WeatherForecast GetWeatherForecastBySummary(string summary);
}
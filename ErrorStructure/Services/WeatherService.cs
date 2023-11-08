using ErrorStructure.Exceptions;
using ErrorStructure.Models;

namespace ErrorStructure.Services;

public class WeatherService : IWeatherService
{
    public static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public WeatherService()
    {
    }

    public IEnumerable<WeatherForecast> GetAllWeatherForecasts()
    {
        var weatherForecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

        if (weatherForecasts.Length == 0)
        {
            throw new NotFoundException("No weather forecasts found.");
        }

        return weatherForecasts;
    }
    
    public WeatherForecast GetWeatherForecastBySummary(string summary)
    {
        var weatherForecast = GetAllWeatherForecasts()
            .FirstOrDefault(wf => wf.Summary.Equals(summary, StringComparison.OrdinalIgnoreCase));

        if (weatherForecast == null)
        {
            throw new NotFoundException($"No weather forecast found with summary '{summary}'.");
        }

        return weatherForecast;
    }
}
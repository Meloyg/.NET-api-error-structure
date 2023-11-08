using ErrorStructure.Models;
using ErrorStructure.Services;

namespace ErrorStructure.Validations;

public class WeatherForecastValidator : IWeatherForecastValidator
{
    public WeatherForecastValidator()
    {
    }

    public bool ValidateSummary(string summary)
    {
        summary = summary.Trim();
        if (string.IsNullOrWhiteSpace(summary))
        {
            throw new ArgumentException("Summary is required");
        }

        if (!WeatherService.Summaries.Contains(summary, StringComparer.OrdinalIgnoreCase))
        {
            throw new ArgumentException("Summary must be one of the following: " +
                                        $"{string.Join(", ", WeatherService.Summaries)}");
        }
        
        return true;
    }

    public bool Validate(WeatherForecast weatherForecast)
    {
        if (weatherForecast.Date == default)
        {
            throw new ArgumentException("Date is required");
        }

        if (weatherForecast.TemperatureC < -50 || weatherForecast.TemperatureC > 50)
        {
            throw new ArgumentException("Temperature must be between -50 and 50");
        }

        if (string.IsNullOrWhiteSpace(weatherForecast.Summary))
        {
            throw new ArgumentException("Summary is required");
        }

        return true;
    }
}
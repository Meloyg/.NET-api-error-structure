using ErrorStructure.Models;

namespace ErrorStructure.Validations;

public interface IWeatherForecastValidator
{
    bool ValidateSummary(string summary);
    bool Validate(WeatherForecast weatherForecast);
}
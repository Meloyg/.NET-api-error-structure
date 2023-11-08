namespace ErrorStructure.Services;

public interface IWeatherService
{
    IEnumerable<WeatherForecast> GetAllWeatherForecasts();
}
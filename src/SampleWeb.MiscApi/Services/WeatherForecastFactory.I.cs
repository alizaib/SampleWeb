namespace SampleWeb.MiscApi.Services
{
    public interface IWeatherForecastFactory
    {
        IWeatherForecastPrivider CreateWeatherForecast(string providerName);
    }
}

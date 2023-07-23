namespace SampleWeb.MiscApi.Services
{
    public interface IWeatherForecastFactory
    {
        IWeatherForecastProvider CreateWeatherForecast(string providerName);
    }
}

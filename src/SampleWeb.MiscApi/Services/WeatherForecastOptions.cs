namespace SampleWeb.MiscApi.Services
{
    public class WeatherForecastOptions
    {
        public IDictionary<string, Type> Types { get; } = new Dictionary<string, Type>();

        public void Register<T>(string name) where T : IWeatherForecastProvider
        {
            Types.Add(name, typeof(T));
        }
    }
}

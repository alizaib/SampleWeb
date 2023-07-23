using Microsoft.Extensions.Options;

namespace SampleWeb.MiscApi.Services
{
    public class DefaultWeatherForecastFactory : IWeatherForecastFactory
    {
        private readonly IServiceProvider _sp;
        private readonly IDictionary<string, Type> _types;

        public DefaultWeatherForecastFactory(IServiceProvider sp, IOptions<WeatherForecastOptions> options)
        {
            _sp = sp;
            _types = options.Value.Types;
        }
        public IWeatherForecastPrivider CreateWeatherForecast(string providerName) { 
            if(_types.TryGetValue(providerName, out var type))
            {
                return (IWeatherForecastPrivider)_sp.GetRequiredService(type);
            }
            throw new ArgumentOutOfRangeException(providerName);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using SampleWeb.MiscApi.Services;

namespace SampleWeb.MiscApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastFactory _factory;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(IWeatherForecastFactory factory, ILogger<WeatherForecastController> logger)
        {
            _factory = factory;
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public string Get([FromQuery]string providerName)
        {
            var provider = _factory.CreateWeatherForecast(providerName);
            return provider.GetWeatherForecast();
        }
    }
}
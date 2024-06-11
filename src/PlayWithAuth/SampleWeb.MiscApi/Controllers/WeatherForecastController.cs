using Microsoft.AspNetCore.Mvc;
using SampleWeb.MiscApi.Services;

namespace SampleWeb.MiscApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {   
        private readonly IServiceProvider _serviceProvider;

        public WeatherForecastController(IServiceProvider serviceProvider)
        {   
            _serviceProvider = serviceProvider;
        }

        [HttpGet]
        public string GetWeatherForecast([FromQuery]string providerName)
        {
            var provider = _serviceProvider.GetRequiredKeyedService<IWeatherForecastProvider>(providerName);
            return provider.GetWeatherForecast();
        }

        [HttpGet("IsLive")]
        public ActionResult IsLive()
        {
            return Ok("WeatherForecast is live");
        }
    }
}
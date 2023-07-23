using Microsoft.Extensions.DependencyInjection.Extensions;
using SampleWeb.MiscApi.Services;

namespace SampleWeb.MiscApi
{
    public static class ServicesCollectionExtension
    {
        public static IServiceCollection 
            RegisterWeatherForecaseProvider<TServiceImplemenation>(this IServiceCollection services, string providerName)
            where TServiceImplemenation : class, IWeatherForecastPrivider
        {            
            services.TryAddTransient<TServiceImplemenation>();
            services.Configure<WeatherForecastOptions>(options => options.Register<TServiceImplemenation>(providerName));
            return services;
        }
    }
}

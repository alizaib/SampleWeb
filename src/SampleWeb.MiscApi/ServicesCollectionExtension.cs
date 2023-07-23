using Microsoft.Extensions.DependencyInjection.Extensions;
using SampleWeb.MiscApi.Services;

namespace SampleWeb.MiscApi
{
    public static class ServicesCollectionExtension
    {
        public static IServiceCollection 
            RegisterWeatherProvider<TServiceImplemenation>(this IServiceCollection services, string providerName)
            where TServiceImplemenation : class, IWeatherForecastProvider
        {            
            services.TryAddTransient<TServiceImplemenation>();
            services.Configure<WeatherForecastOptions>(options => options.Register<TServiceImplemenation>(providerName));
            return services;
        }
    }
}

using Microsoft.Extensions.DependencyInjection.Extensions;
using SampleWeb.MiscApi;
using SampleWeb.MiscApi.Services;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.TryAddTransient<IWeatherForecastFactory, DefaultWeatherForecastFactory>();

services.RegisterWeatherProvider<WeatherForecastProviderOne>("one");
services.RegisterWeatherProvider<WeatherForecastProviderTwo>("two");    

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

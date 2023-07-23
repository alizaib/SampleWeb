using Microsoft.Extensions.DependencyInjection.Extensions;
using SampleWeb.MiscApi;
using SampleWeb.MiscApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.TryAddTransient<IWeatherForecastFactory, DefaultWeatherForecastFactory>();
builder.Services.RegisterWeatherForecaseProvider<WeatherForecastProviderOne>("one");
builder.Services.RegisterWeatherForecaseProvider<WeatherForecastProviderTwo>("two");    

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


using SampleWeb.MiscAuth;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
//services.AddCustomAuth();
//services.AddMicrosoftAuth();
services.AddIdentityAuth();


var app = builder.Build();

//app.UseCustomAuthCookie();
//app.UseMicrosoftAuthCookie();
app.UseIdentityAuth();

app.Run();

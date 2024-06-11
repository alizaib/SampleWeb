using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace SampleWeb.MiscAuth;

public static partial class WebApplicationExtensions
{
    public static void AddIdentityAuth(this IServiceCollection services)
    {
        services.AddAuthentication("cookie")                
                .AddCookie("cookie");
    }

    public static void UseIdentityAuth(this WebApplication app)
    {
        app.UseAuthentication();

        app.MapGet("/username", (HttpContext ctx) => {
            return ctx.User.FindFirstValue("user");
        });

        app.MapGet("/login", (HttpContext ctx) => {
            var claims = new List<Claim>
            {
                new Claim("user", "Ali-Identity")
            };

            var identity = new ClaimsIdentity(claims, "cookie");
            var user = new ClaimsPrincipal(identity);

            ctx.SignInAsync("cookie", user);

            return "ok";
        });
    }
}

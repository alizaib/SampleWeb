using Microsoft.AspNetCore.DataProtection;

namespace SampleWeb.MiscAuth;

public static partial class WebApplicationExtensions
{
    public static void AddCustomAuth(this IServiceCollection services)
    {
        services.AddDataProtection();
        services.AddHttpContextAccessor();
    }

    public static void UseCustomAuth(this WebApplication app)
    {
        app.MapGet("/username", (HttpContext ctx, IDataProtectionProvider idp) => {
            var protector = idp.CreateProtector("auth-cookie");

            var authCookie = ctx.Request.Headers.Cookie.FirstOrDefault(x => x.StartsWith("auth="));
            var protectedPayload = authCookie!.Split("=").Last();
            var payload = protector.Unprotect(protectedPayload);
            var parts = payload.Split(':');
            var key = parts[0];
            var value = parts[1];

            return value;
        });

        app.MapGet("/login", (HttpContext ctx, IDataProtectionProvider idp) => {
            var protector = idp.CreateProtector("auth-cookie");
            var encryptedAuthCookie = protector.Protect("usr:ali-Custom");

            ctx.Response.Headers["set-cookie"] = $"auth={encryptedAuthCookie}";

            return "ok";
        });
    }
}    

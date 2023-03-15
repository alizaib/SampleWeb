using Microsoft.AspNetCore.DataProtection;
using System.Security.Claims;

namespace SampleWeb.MiscAuth
{
    public static partial class WebApplicationExtensions
    {
        public static void AddMicrosoftAuth(this IServiceCollection services)
        {
            services.AddDataProtection();
            services.AddHttpContextAccessor();
            services.AddScoped<AuthService>();
        }
        public static void UseMicrosoftAuth(this WebApplication app)
        {

            app.Use((ctx, next) => {
                var idp = ctx.RequestServices.GetRequiredService<IDataProtectionProvider>();

                var protector = idp.CreateProtector("auth-cookie");

                var authCookie = ctx.Request.Headers.Cookie.FirstOrDefault(x => x.StartsWith("auth="));
                var protectedPayload = authCookie!.Split("=").Last();
                var payload = protector.Unprotect(protectedPayload);
                var parts = payload.Split(':');
                var key = parts[0];
                var value = parts[1];

                var claims = new List<Claim>
                {
                    new Claim(key, value)
                };
                var identity = new ClaimsIdentity(claims);
                ctx.User = new ClaimsPrincipal(identity);

                return next();
            });

            app.MapGet("/username", (HttpContext ctx) => {
                return ctx.User.FindFirstValue("usr");
            });

            app.MapGet("/login", (AuthService auth) => {
                auth.SignIn();

                return "ok";
            });
        }
    }

    public class AuthService
    {
        private readonly IDataProtectionProvider _idp;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IDataProtectionProvider idp, IHttpContextAccessor httpContextAccessor)
        {
            _idp = idp;
            _httpContextAccessor = httpContextAccessor;
        }

        public void SignIn()
        {
            var protector = _idp.CreateProtector("auth-cookie");
            var encryptedAuthCookie = protector.Protect("usr:ali-Microsoft");

            _httpContextAccessor.HttpContext!.Response.Headers["set-cookie"] = $"auth={encryptedAuthCookie}";
        }
    }
}

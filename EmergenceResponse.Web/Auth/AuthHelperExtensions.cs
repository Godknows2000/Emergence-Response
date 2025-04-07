using EmergenceResponse.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Security.Claims;
using wCyber.Helpers.Identity.Auth;

namespace EmergenceResponse.Web.Auth
{
    public static class AuthHelperExtensions
    {
        public static async Task InitUser(this CookieSigningInContext context)
        {
            var _currentUserId = Guid.Parse(context.Principal.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var _db = (EmergenceContext)context.Request.HttpContext.RequestServices.GetService(typeof(EmergenceContext));
            var user = await _db.Users.FirstOrDefaultAsync(c => c.IsActive && c.Id == _currentUserId);

            var claims = new List<Claim>();

            if (user != null)
            {
                claims.Add(new Claim(ClaimTypes.Role, user.RoleId.ToString()));
            }

            if (claims.Any())
            {
                var appIdentity = new ClaimsIdentity(claims);
                context.Principal.AddIdentity(appIdentity);
            }
        }

        public static void ConfigureAuth(WebApplicationBuilder builder)
        {
            var authOptions = builder.Configuration.GetSection("Auth").Get<IdentityAuthOptions>();
            builder.Services.Configure<IdentityAuthOptions>(builder.Configuration.GetSection("Auth"));
            var HttpHandler = new HttpClientHandler();
            var environment = builder.Environment;
            if (environment.IsDevelopment())
            {
                HttpHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            }
            builder.Services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });



        }
    }
}

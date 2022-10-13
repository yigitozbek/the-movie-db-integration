using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TheMovieDb.Domain.Helpers.Security.JWT;

namespace TheMovieDb.Domain.Extensions;

public static class IdentityExtension
{
    public static void AddIdentityService(this IServiceCollection services,
        IConfiguration configuration)
    {

        var tokenOptions = configuration.GetSection("TokenOptions").Get<JwtHelper.TokenOptions>();
        var sessionOption = configuration.GetSection("SessionOption").Get<JwtHelper.SessionOption>();

        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(sessionOption.IdleTimeout);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
            options.Cookie.Name = sessionOption.CookieName;
        });

        services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidAudience = tokenOptions.Audience,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey)),

                    ValidateIssuer = true,
                    ValidIssuer = tokenOptions.Issuer,
                };
            });

        services.Configure<CookiePolicyOptions>(options =>
        {
            options.CheckConsentNeeded = _ => true;
            options.MinimumSameSitePolicy = SameSiteMode.None;
        });

    }


    public static void AddIdentityConfigure(this IApplicationBuilder app)
    {
        app.UseCookiePolicy();

        app.UseSession();

        //app.UseIdentity();

        app.UseAuthentication();

        app.UseAuthorization();
    }
}
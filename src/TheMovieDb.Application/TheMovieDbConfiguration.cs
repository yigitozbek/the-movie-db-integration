using Autofac.Extensions.DependencyInjection;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Yella.Dapper;
using Yella.EntityFrameworkCore.IoC.DependencyResolvers;
using TheMovieDb.Domain.Context;
using Yella.Utilities.Extensions;
using Microsoft.Extensions.Configuration;
using TheMovieDb.Domain.Helpers.Security.JWT;
using TheMovieDb.Application.Middlewares;
using TheMovieDb.Movie.Application;

namespace TheMovieDb.Application;

public static class TheMovieDbConfiguration
{

    public static IServiceCollection AddMovieApplicationService(this IServiceCollection services)
    {

        #region cors
        services.AddCors(options =>
        {
            options.AddPolicy("AllCors", policy =>
            {
                policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            });
        });
        #endregion

        #region AutoMapper
        services.AddAutoMapper(typeof(TheMovieDbAutoMapper).Assembly);
        #endregion

        services.AddHttpContextAccessor();

        return services;
    }

    public static IServiceCollection AddIdentityService(this IServiceCollection services, WebApplicationBuilder builder)
    {
        #region Identity


        var tokenOptions = builder.Configuration.GetSection("TokenOptions")
            .Get<JwtHelper.TokenOptions>();

        var sessionOption = builder.Configuration.GetSection("SessionOption")
            .Get<JwtHelper.SessionOption>();

        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(sessionOption.IdleTimeout);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = false,
                    ValidateAudience = true,
                    ValidAudience = tokenOptions.Audience,
                    ValidIssuer = tokenOptions.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey)),
                    ValidateIssuer = true,
                };
            });

        #endregion

        return services;
    }

    public static IServiceCollection AddDapperService(this IServiceCollection services, string connectionString)
    {
        services.AddSingleton<IDapperRepository, DapperRepository>();
        var dapper = services.BuildServiceProvider().GetService(typeof(IDapperRepository)) as IDapperRepository;
        dapper?.Connection(connectionString);
        return services;
    }

    public static IServiceCollection AddContextService(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<TheMovieDbContext>(x => x.UseSqlServer(connectionString));
        return services;
    }

    public static ConfigureHostBuilder AddMovieApplicationHost(this ConfigureHostBuilder host)
    {

        #region Autofac

        host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

        host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
            containerBuilder.RegisterModule(new TheMovieDbAutofacApplicationModule()));

        host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
            containerBuilder.RegisterModule(new AutofacEntityFrameworkCoreModule()));

        host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
            containerBuilder.RegisterModule(new AutofacContextModule<TheMovieDbContext>()));

        #endregion

        return host;
    }

    public static WebApplication UseMovieApplication(this WebApplication app)
    {
        ServiceActivator.Configure(app.Services);

        app.UseMiddleware<FluentValidationExceptionMiddleware>();

        app.UseMiddleware<AuthenticationExceptionMiddleware>();

        app.UseMiddleware<GeneralExceptionMiddleware>();

        return app;
    }

}
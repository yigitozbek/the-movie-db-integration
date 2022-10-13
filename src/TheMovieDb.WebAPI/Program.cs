using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using TheMovieDb.Application;
using TheMovieDb.Application.Middlewares;
using TheMovieDb.Domain.Context;
using NLog;
using NLog.Web;
using Yella.Utilities.Routing;
using TheMovieDb.BackgroundWorker;

var builder = WebApplication.CreateBuilder(args);

var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

try
{

    // Add services to the container.

    builder.Services.AddControllers(options =>
    {
        options.Conventions.Add(new RouteTokenTransformerConvention(new CamelCaseParameterTransformer()));
    }).AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

    builder.Services.AddHostedService<RepaitingService>();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen(options =>
    {

        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "TheMovieDb Movie",
            Description = "a project to learn",
            TermsOfService = new Uri("https://example.com/terms"),
            Contact = new OpenApiContact
            {
                Name = "Contact",
                Url = new Uri("https://example.com/contact")
            },
            License = new OpenApiLicense
            {
                Name = "License",
                Url = new Uri("https://example.com/license")
            }
        });

        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

    });

    builder.Services.AddMovieApplicationService();

    var connectionString = builder.Configuration["ConnectionStrings:SqlServer"];

    builder.Services.AddDapperService(connectionString);

    builder.Services.AddContextService(connectionString);

    builder.Host.AddMovieApplicationHost();

    builder.Services.AddIdentityService(builder);

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger(options =>
        {
            options.SerializeAsV2 = true;
        });
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        });
    }

    app.UseMovieApplication();

    app.UseHttpsRedirection();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}
catch (Exception e)
{
    logger.Error(e);
    throw;
}
finally
{
    LogManager.Shutdown();
}
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yella.Utilities.Results;

namespace TheMovieDb.Application.Middlewares;

public class GeneralExceptionMiddleware
{
    readonly ILogger<GeneralExceptionMiddleware> _logger;

    public GeneralExceptionMiddleware(RequestDelegate next, ILogger<GeneralExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    protected readonly RequestDelegate _next;


    public async Task InvokeAsync(HttpContext context)
    {

        try
        {
            await _next(context);
        }
        catch (System.Exception ex)
        {
            var guid = Guid.NewGuid();

            _logger.Log(LogLevel.Error, ex.ToString());

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorResult($"Bir hata oluştu - {guid.ToString()}")));
        }

    }

}

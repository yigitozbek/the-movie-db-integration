using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using Yella.Utilities.Results;

namespace TheMovieDb.Application.Middlewares;

public class AuthenticationExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public AuthenticationExceptionMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }

        catch (AuthenticationException)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorResult("Authentication Exception")));
        }
        catch (UnauthorizedAccessException)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorResult("Unauthorized Exception")));
        }
    }
}
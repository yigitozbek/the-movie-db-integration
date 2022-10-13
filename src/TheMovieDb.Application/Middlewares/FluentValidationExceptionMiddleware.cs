using FluentValidation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yella.CrossCuttingConcern.Validations.FluentValidation;
using Yella.Utilities.Results;

namespace TheMovieDb.Application.Middlewares;

public class FluentValidationExceptionMiddleware
{
    private readonly RequestDelegate _next;
    public FluentValidationExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 400;
            var tt = JsonConvert.DeserializeObject<List<ValidationError>>(ex.Message);
            var message = (tt ?? new List<ValidationError>()).Aggregate("", (current, m) => current + m.Message + "<br>");
            await context.Response.WriteAsJsonAsync(new ErrorResult(message));
        }


    }
}

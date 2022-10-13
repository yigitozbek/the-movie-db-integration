using Microsoft.AspNetCore.Mvc;
using TheMovieDb.Application.Contract.Identities;
using TheMovieDb.Application.Contract.Identities.Dtos;
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using TheMovieDb.Domain.Shared.Identities;
using Yella.Utilities.Results;

namespace TheMovieDb.WebAPI.Controllers;


[ApiController]
[Route("api/[controller]/[action]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }


    /// <summary>
    /// This method allows you to register.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     Post /api​/auth​/register
    ///     {
    ///         "userName": "ygtozbk",
    ///         "password": "123456789A.",
    ///         "email": "yigitozbek17@outlook.com",
    ///         "name": "Yiğit",
    ///         "surname": "Özbek",
    ///         "roleIds": [
    ///         "0edcb04c-8fd6-4c34-8044-8d178fe4b7c9"
    ///             ]
    ///     }
    /// 
    /// </remarks>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Yella.Utilities.Results.IResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
    public async Task<IActionResult> Register(RequestRegister input)
    {
        var result = await _authService.RegisterAsync(input);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }


    /// <summary>
    /// This method allows you to login.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     Post /api​/auth​/login
    ///     {
    ///         "username": "ygtozbk",
    ///         "password": "123456789A."
    ///     }
    /// 
    /// </remarks>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Yella.Utilities.Results.IResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
    public async Task<IActionResult> Login(RequestLogin input)
    {
        var result = await _authService.LoginAsync(input);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }



}
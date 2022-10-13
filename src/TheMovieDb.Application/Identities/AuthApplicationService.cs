using TheMovieDb.Application.Base;
using TheMovieDb.Application.Contract.Identities;
using TheMovieDb.Application.Contract.Identities.Validators;
using TheMovieDb.Domain.Modules.Identities.Managers;
using TheMovieDb.Domain.Shared.Const;
using TheMovieDb.Domain.Shared.Identities;
using TheMovieDb.Domain.Shared.Models;
using Yella.Aspect.PostSharp.Validations;
using Yella.Utilities.Results;
using IResult = Yella.Utilities.Results.IResult;

namespace TheMovieDb.Application.Identities;

public class AuthApplicationService : ApplicationService<AuthApplicationService>, IAuthService
{

    private readonly AuthManager _authManager;

    public AuthApplicationService(AuthManager authManager)
    {
        _authManager = authManager;
    }

    /// <summary>
    /// This method allows it to be registered to the User table.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    [FluentValidationAspect(typeof(AuthRegisterValidator), AspectPriority = 2)]
    public async Task<IResult> RegisterAsync(RequestRegister input)
    {
        var result = await _authManager.RegisterAsync(input);

        if (!result.Success)
        {
            return new ErrorResult(result.Message);
        }

        return new SuccessResult(result.Message);
    }

    /// <summary>
    /// This method allows it to be login
    /// </summary>
    /// <param name="input"></param>
    /// <returns>Return value Token returns</returns>
    /// <exception cref="ArgumentNullException"></exception>
    [FluentValidationAspect(typeof(AuthLoginValidator), AspectPriority = 2)]
    public async Task<IDataResult<AccessToken>> LoginAsync(RequestLogin input)
    {
        var result = await _authManager.LoginAsync(input);

        if (!result.Success)
        {
            return new ErrorDataResult<AccessToken>(result.Message);
        }

        return new SuccessDataResult<AccessToken>(result.Data, IdentityMessages.Successful);
    }

    /// <summary>
    /// This method is used for resetting the password.
    /// </summary>
    /// <param name="input"></param>
    /// <exception cref="ArgumentNullException"></exception>
    [FluentValidationAspect(typeof(AuthChangePasswordValidator), AspectPriority = 2)]
    public async Task<IResult> ChangePasswordAsync(RequestChangePasswordAsync input)
    {
        var result = await _authManager.ChangePasswordAsync(input);

        if (!result.Success)
        {
            return new ErrorResult(result.Message);
        }

        return new SuccessResult(result.Message);
    }


}
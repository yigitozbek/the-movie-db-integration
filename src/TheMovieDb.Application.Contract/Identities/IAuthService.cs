using TheMovieDb.Application.Contract.Identities.Dtos;
using TheMovieDb.Domain.Shared.Models;
using System.Security.Claims;
using TheMovieDb.Domain.Shared.Identities;
using Yella.Utilities.Results;

namespace TheMovieDb.Application.Contract.Identities;

public interface IAuthService
{
    /// <summary>
    /// This method allows it to be registered to the User table.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    Task<IResult> RegisterAsync(RequestRegister input);

    /// <summary>
    /// This method allows it to be login
    /// </summary>
    /// <returns>Return value Token returns</returns>
    /// <exception cref="ArgumentNullException"></exception>
    Task<IDataResult<AccessToken>> LoginAsync(RequestLogin input);

    /// <summary>
    /// This method is used for resetting the password.
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    Task<IResult> ChangePasswordAsync(RequestChangePasswordAsync input);
}

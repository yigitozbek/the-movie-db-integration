using System.Security.Claims;
using TheMovieDb.Domain.Modules.Identities;
using TheMovieDb.Domain.Shared.Models;

namespace TheMovieDb.Domain.Helpers.Security.JWT;

public interface ITokenHelper
{
    AccessToken CreateToken(IdentityUser user, IEnumerable<IdentityRole> roles, IEnumerable<IdentityPermission> permissions, IEnumerable<Claim>? claims);
}
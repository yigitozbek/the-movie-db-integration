using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TheMovieDb.Domain.Extensions;
using TheMovieDb.Domain.Modules.Identities;
using TheMovieDb.Domain.Shared.Models;

namespace TheMovieDb.Domain.Helpers.Security.JWT;

public class JwtHelper : ITokenHelper
{
    private readonly TokenOptions _tokenOptions;
    private readonly DateTime _accessTokenExpiration;

    public JwtHelper(IConfiguration configuration)
    {
        _tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
        _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
    }

    public AccessToken CreateToken(IdentityUser user, IEnumerable<IdentityRole> roles, IEnumerable<IdentityPermission> permissions, IEnumerable<Claim>? claims)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, roles, permissions, claims);
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var token = jwtSecurityTokenHandler.WriteToken(jwt);
        return new AccessToken(token, _accessTokenExpiration);
    }

    private JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, IdentityUser user,
        SigningCredentials signingCredentials, IEnumerable<IdentityRole> roles,
        IEnumerable<IdentityPermission> permissions, IEnumerable<Claim>? claims)
    {
        if (signingCredentials == null)
            throw new ArgumentNullException(nameof(signingCredentials));

        var jwt = new JwtSecurityToken(
            issuer: tokenOptions.Issuer,
            audience: tokenOptions.Audience,
            expires: DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration),
            claims: SetClaims(user, roles, permissions, claims),
            signingCredentials: signingCredentials
        );

        return jwt;
    }


    private static IEnumerable<Claim> SetClaims(IdentityUser user, IEnumerable<IdentityRole> roles,
        IEnumerable<IdentityPermission> permissions, IEnumerable<Claim>? list)
    {
        var claims = new List<Claim>();
        claims.AddNameIdentifier(user.Id.ToString());

        if (roles.Any())
            claims.AddRoles(roles.Select(c => c.Name).ToArray());

        if (permissions.Any())
            claims.AddPermissions(permissions.Select(c => c.Name).ToArray());

        claims.AddFullName($"{user.Name} {user.Surname}");
        claims.AddUsername($"{user.Username}");
        claims.AddEmail($"{user.Email}");

        if (list == null) return claims;

        if (claims is { Count: > 0 }) claims.AddRange(list);

        return claims;
    }

    public class TokenOptions
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int AccessTokenExpiration { get; set; }
        public string SecurityKey { get; set; }
    }

    public class SessionOption
    {
        public string CookieName { get; set; }
        public int IdleTimeout { get; set; }

    }
}
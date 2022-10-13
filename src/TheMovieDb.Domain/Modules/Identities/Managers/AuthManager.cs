using System.Security.Claims;
using TheMovieDb.Domain.Helpers.Security.JWT;
using TheMovieDb.Domain.Shared.Const;
using TheMovieDb.Domain.Shared.Identities;
using TheMovieDb.Domain.Shared.Models;
using Yella.Aspect.PostSharp.Transactions;
using Yella.EntityFrameworkCore;
using Yella.EntityFrameworkCore.Extensions;
using Yella.Utilities.Results;
using Yella.Utilities.Security.Hashing;

namespace TheMovieDb.Domain.Modules.Identities.Managers;

public class AuthManager : DomainService<AuthManager>
{
    private readonly IRepository<IdentityUser, Guid> _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IRepository<IdentityUserRole, Guid> _userRoleRepository;
    private readonly IRepository<IdentityPermission, short> _permissionRepository;
    private readonly ITokenHelper _tokenHelper;

    public AuthManager(IRepository<IdentityUser, Guid> userRepository, IPasswordHasher passwordHasher, IRepository<IdentityUserRole, Guid> userRoleRepository, IRepository<IdentityPermission, short> permissionRepository, ITokenHelper tokenHelper)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _userRoleRepository = userRoleRepository;
        _permissionRepository = permissionRepository;
        _tokenHelper = tokenHelper;
    }

    [TransactionAspect(AspectPriority = 1)]
    public async Task<IResult> RegisterAsync(RequestRegister input)
    {

        var isUserExit = await _userRepository.FirstOrDefaultAsync(x => x.Username == input.Username || x.Email == input.Email);

        if (isUserExit != null)
        {
            return new ErrorResult("there is a user with a username or email");
        }

        _passwordHasher.CreatePasswordHash(input.Password, out var passwordHash, out var passwordSalt);

        var user = new IdentityUser(input.Username, input.Email, passwordSalt, passwordHash, null, DateTime.Now, 0, input.Name, input.Surname);

        var userResult = await _userRepository.AddAsync(user);

        var userRoleList = input.RoleIds.Select(registerDtoRoleId => new IdentityUserRole(userResult.Data.Id, registerDtoRoleId)).ToList();

        var result = await _userRoleRepository.AddRangeAsync(userRoleList);

        if (!result.Success)
        {
            return new ErrorResult(result.Message);
        }

        return new SuccessResult(result.Message);
    }

    public async Task<IDataResult<AccessToken>> LoginAsync(RequestLogin input)
    {
        if (input == null) throw new ArgumentNullException(nameof(input));

        var user = await _userRepository.FirstOrDefaultAsync(x => x.Username == input.Username);

        if (user == null)
        {
            return new ErrorDataResult<AccessToken>(IdentityMessages.UserNotFound);
        }

        if (!VerifyPasswordHash(user, input.Password))
        {
            return new ErrorDataResult<AccessToken>(IdentityMessages.PasswordError);
        }

        var accessToken = await CreateTokenAsync(user.Id, null);

        return new SuccessDataResult<AccessToken>(accessToken, IdentityMessages.Successful);

    }


    public async Task<IResult> ChangePasswordAsync(RequestChangePasswordAsync input)
    {
        if (input == null) throw new ArgumentNullException(nameof(input));

        var user = await _userRepository.FirstOrDefaultAsync(x => x.Id == input.Id);

        if (user == null)
        {
            return new ErrorResult("there is no such user");
        }

        if (!_passwordHasher.VerifyPasswordHash(input.CurrentPassword, user.PasswordHash, user.PasswordSalt))
        {
            return new ErrorResult(IdentityMessages.ThisPasswordIsWrong);
        }

        _passwordHasher.CreatePasswordHash(input.NewPassword, out var passwordHash, out var passwordSalt);

        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        var result = await _userRepository.UpdateAsync(user);

        if (!result.Success)
        {
            return new ErrorResult(result.Message);
        }

        return new SuccessResult(result.Message);
    }


    /// <summary>
    /// This method checks the correctness of the password.
    /// </summary>
    /// <param name="user"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    private bool VerifyPasswordHash(IdentityUser user, string password) => _passwordHasher.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt);



    /// <summary>
    /// This method is a Private method. It is used to create tokens.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="claims"></param>
    /// <returns></returns>
    private async Task<AccessToken> CreateTokenAsync(Guid userId, IEnumerable<Claim>? claims)
    {
        var user = await _userRepository.GetAsync(userId);

        var roles = (await _userRoleRepository.WithIncludeAsync(x => x.IdentityRole)).Where(x => x.IdentityUserId == user.Id).Select(x => x.IdentityRole);

        var permissions = (await _permissionRepository.WithIncludeAsync(x => x.IdentityPermissionRoles,
                x => ((IdentityPermissionRole)x.IdentityPermissionRoles).IdentityRole,
                x => ((IdentityPermissionRole)x.IdentityPermissionRoles).IdentityRole.UserRoles))
            .Where(x => x.IdentityPermissionRoles.Any(pRole => pRole.IdentityRole.UserRoles.Any(uRole => uRole.IdentityUserId == userId)));

        var accessToken = _tokenHelper.CreateToken(user, roles, permissions, claims);

        return accessToken;
    }

}
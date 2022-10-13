using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using TheMovieDb.Application.Base;
using TheMovieDb.Application.Contract.Identities;
using TheMovieDb.Application.Contract.Identities.Dtos;
using TheMovieDb.Application.Contract.Identities.Validators;
using TheMovieDb.Domain.Modules.Identities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yella.Aspect.PostSharp.Authorizations;
using Yella.Aspect.PostSharp.Validations;
using Yella.AutoMapper.Extensions;
using Yella.EntityFrameworkCore;
using Yella.Utilities.Results;

namespace TheMovieDb.Application.Identities;

[Authorize]
public class RoleApplicationService : ApplicationService<RoleApplicationService>, IRoleService
{
    private readonly IRepository<IdentityRole, Guid> _identityRoleRepository;
    private readonly IRepository<IdentityUserRole, Guid> _identityUserRoleRepository;
    private readonly IRepository<IdentityPermissionRole, Guid> _identityPermissionRoleRepository;

    public RoleApplicationService(IRepository<IdentityRole, Guid> identityRoleRepository, IRepository<IdentityUserRole, Guid> identityUserRoleRepository, IRepository<IdentityPermissionRole, Guid> identityPermissionRoleRepository)
    {
        _identityRoleRepository = identityRoleRepository;
        _identityUserRoleRepository = identityUserRoleRepository;
        _identityPermissionRoleRepository = identityPermissionRoleRepository;
    }

    [AuthorizationAspect(TheMovieDbPermission.Identities.Roles.Get, AspectPriority = 1)]
    public async Task<IDataResult<List<ResponseRoleGetList>>> GetListAsync()
    {
        var query = await _identityRoleRepository.QueryableAsync();
        var map = (await query.ToListAsync()).ToMapper<List<ResponseRoleGetList>>();
        return new SuccessDataResult<List<ResponseRoleGetList>>(map);
    }

    [FluentValidationAspect(typeof(RoleAddValidator), AspectPriority = 2)]
    [AuthorizationAspect(TheMovieDbPermission.Identities.Roles.Add, AspectPriority = 1)]
    public async Task<IResult> AddAsync(RequestRoleAdd input)
    {
        if (input == null) throw new ArgumentNullException(nameof(input));

        var map = input.ToMapper<IdentityRole>();

        var result = await _identityRoleRepository.AddAsync(map);

        if (!result.Success)
        {
            return new ErrorResult(result.Message);
        }

        return new SuccessResult(result.Message);
    }

    [FluentValidationAspect(typeof(RoleUpdateValidator), AspectPriority = 2)]
    [AuthorizationAspect(TheMovieDbPermission.Identities.Roles.Update, AspectPriority = 1)]
    public async Task<IResult> UpdateAsync(RequestRoleUpdate input)
    {
        if (input == null) throw new ArgumentNullException(nameof(input));

        var map = input.ToMapper<IdentityRole>();

        var result = await _identityRoleRepository.UpdateAsync(map);

        if (!result.Success)
        {
            return new ErrorResult(result.Message);
        }

        return new SuccessResult(result.Message);
    }

    [FluentValidationAspect(typeof(RoleDeleteValidator), AspectPriority = 2)]
    [AuthorizationAspect(TheMovieDbPermission.Identities.Roles.Delete, AspectPriority = 1)]
    public async Task<IResult> DeleteAsync(RequestRoleDelete input)
    {
        if (input == null) throw new ArgumentNullException(nameof(input));

        var result = await _identityRoleRepository.DeleteAsync(input.Id);

        if (!result.Success)
        {
            return new ErrorResult(result.Message);
        }

        return new SuccessResult(result.Message);
    }
    
    [FluentValidationAspect(typeof(RoleAddRolePermissionValidator), AspectPriority = 2)]
    [AuthorizationAspect(TheMovieDbPermission.Identities.Roles.Add, AspectPriority = 1)]
    public async Task<IResult> AddRolePermissionAsync(RequestAddRolePermission input)
    {
        var map = input.ToMapper<IdentityPermissionRole>();

        var result = await _identityPermissionRoleRepository.AddAsync(map);

        if (!result.Success)
        {
            return new ErrorResult(result.Message);
        }

        return new SuccessResult(result.Message);
    }

    [FluentValidationAspect(typeof(RoleAddRolePermissionValidator), AspectPriority = 2)]
    [AuthorizationAspect(TheMovieDbPermission.Identities.Roles.Delete, AspectPriority = 1)]
    public async Task<IResult> DeleteRolePermissionAsync(RequestDeleteRolePermission input)
    {

        var result = await _identityPermissionRoleRepository.DeleteAsync(input.Id);

        if (!result.Success)
        {
            return new ErrorResult(result.Message);
        }

        return new SuccessResult(result.Message);
    }

    [FluentValidationAspect(typeof(RoleAddValidator), AspectPriority = 2)]
    [AuthorizationAspect(TheMovieDbPermission.Identities.Roles.Add, AspectPriority = 1)]
    public async Task<IResult> AddUserRoleAsync(RequestRoleAddUserRole input)
    {

        var map = input.ToMapper<IdentityUserRole>();

        var result = await _identityUserRoleRepository.AddAsync(map);

        if (!result.Success)
        {
            return new ErrorResult(result.Message);
        }

        return new SuccessResult(result.Message);
    }

    [FluentValidationAspect(typeof(RoleDeleteUserRoleValidator), AspectPriority = 2)]
    [AuthorizationAspect(TheMovieDbPermission.Identities.Roles.Delete, AspectPriority = 1)]
    public async Task<IResult> DeleteUserRoleAsync(RequestDeleteUserRole input)
    {
        var result = await _identityUserRoleRepository.DeleteAsync(x => x.IdentityRoleId == input.RoleId && x.IdentityUserId == input.UserId);

        if (!result.Success)
        {
            return new ErrorResult(result.Message);
        }

        return new SuccessResult(result.Message);
    }
}

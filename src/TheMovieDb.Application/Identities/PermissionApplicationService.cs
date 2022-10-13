using Microsoft.AspNetCore.Authorization;
using TheMovieDb.Application.Base;
using TheMovieDb.Application.Contract.Identities;
using TheMovieDb.Application.Contract.Identities.Dtos;
using TheMovieDb.Domain.Modules.Identities;
using Yella.Aspect.PostSharp.Authorizations;
using Yella.AutoMapper.Extensions;
using Yella.EntityFrameworkCore;
using Yella.EntityFrameworkCore.Extensions;
using Yella.Utilities.Results;

namespace TheMovieDb.Application.Identities;

[Authorize]
public class PermissionApplicationService : ApplicationService<PermissionApplicationService>, IPermissionService
{
    private readonly IRepository<IdentityPermission, short> _permissionRepository;

    public PermissionApplicationService(IRepository<IdentityPermission, short> permissionRepository)
    {
        _permissionRepository = permissionRepository;
    }

    [AuthorizationAspect(TheMovieDbPermission.Identities.Permissions.GetList, AspectPriority = 1)]
    public async Task<IDataResult<List<ResponsePermissionGetList>>> GetListAsync()
    {
        var query = await _permissionRepository.GetListAsync();
        var map = query.ToMapper<List<ResponsePermissionGetList>>();
        return new SuccessDataResult<List<ResponsePermissionGetList>>(map);
    }
}

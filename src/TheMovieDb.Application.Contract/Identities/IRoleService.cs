using TheMovieDb.Application.Contract.Identities.Dtos;
using Yella.Utilities.Results;

namespace TheMovieDb.Application.Contract.Identities;

public interface IRoleService
{

    Task<IDataResult<List<ResponseRoleGetList>>> GetListAsync();
    Task<IResult> AddAsync(RequestRoleAdd input);
    Task<IResult> DeleteAsync(RequestRoleDelete input);
    Task<IResult> UpdateAsync(RequestRoleUpdate input);

    Task<IResult> AddUserRoleAsync(RequestRoleAddUserRole input);
    Task<IResult> DeleteUserRoleAsync(RequestDeleteUserRole input);
    Task<IResult> AddRolePermissionAsync(RequestAddRolePermission input);
    Task<IResult> DeleteRolePermissionAsync(RequestDeleteRolePermission input);

}
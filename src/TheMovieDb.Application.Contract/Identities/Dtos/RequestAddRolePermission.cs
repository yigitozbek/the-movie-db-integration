using Yella.Domain.Dto;

namespace TheMovieDb.Application.Contract.Identities.Dtos;

public class RequestAddRolePermission : EntityDto
{
    public Guid RoleId { get; set; }
    public short PermissionId { get; set; }
}

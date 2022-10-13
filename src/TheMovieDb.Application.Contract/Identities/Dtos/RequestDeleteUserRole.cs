using Yella.Domain.Dto;

namespace TheMovieDb.Application.Contract.Identities.Dtos;

public class RequestDeleteUserRole : EntityDto
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}
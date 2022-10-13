using Yella.Domain.Dto;

namespace TheMovieDb.Application.Contract.Identities.Dtos;

public class RequestRoleUpdate : EntityDto<Guid>
{
    public string Name { get; set; }

    public string Description { get; set; }
}

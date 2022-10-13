using Yella.Domain.Dto;

namespace TheMovieDb.Application.Contract.Identities.Dtos;

public class RequestRoleAdd : EntityDto
{
    public string Name { get; set; }

    public string Description { get; set; }
}

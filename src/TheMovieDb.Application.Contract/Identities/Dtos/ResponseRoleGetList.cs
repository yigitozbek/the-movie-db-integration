using Yella.Domain.Dto;

namespace TheMovieDb.Application.Contract.Identities.Dtos;

public class ResponseRoleGetList : EntityDto<Guid>
{
    public string Name { get; set; }

    public string Description { get; set; }
}
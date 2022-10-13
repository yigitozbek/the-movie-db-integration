using Yella.Domain.Dto;

namespace TheMovieDb.Application.Contract.Identities.Dtos;

public class ResponseUserGetList : EntityDto<Guid>
{
    public string Username { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Surname { get; set; } = string.Empty;
}



public class ResponsePermissionGetList : EntityDto<Guid>
{
    public string Name { get; set; }

    public string Description { get; set; }

    public string Module { get; set; }
}

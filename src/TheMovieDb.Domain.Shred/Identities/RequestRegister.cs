using Yella.Domain.Dto;

namespace TheMovieDb.Domain.Shared.Identities;

public class RequestRegister : EntityDto
{
    public RequestRegister(string username, string password, string email, string name, string surname, List<Guid> roleIds)
    {
        Username = username;
        Password = password;
        Email = email;
        Name = name;
        Surname = surname;
        RoleIds = roleIds;
    }

    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public List<Guid> RoleIds { get; set; }
}
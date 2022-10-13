using Yella.Domain.Dto;

namespace TheMovieDb.Domain.Shared.Identities;

public class RequestLogin : EntityDto
{
    public RequestLogin(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public string Username { get; set; }
    public string Password { get; set; }

}
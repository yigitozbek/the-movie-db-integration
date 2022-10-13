using Yella.Domain.Dto;

namespace TheMovieDb.Domain.Shared.Identities;

public class RequestChangePasswordAsync : EntityDto<Guid>
{
    protected RequestChangePasswordAsync(string username, string currentPassword, string confirmPassword, string newPassword)
    {
        Username = username;
        CurrentPassword = currentPassword;
        ConfirmPassword = confirmPassword;
        NewPassword = newPassword;
    }

    protected RequestChangePasswordAsync(Guid id, string username, string currentPassword, string confirmPassword, string newPassword) : base(id)
    {
        Username = username;
        CurrentPassword = currentPassword;
        ConfirmPassword = confirmPassword;
        NewPassword = newPassword;
    }

    public string Username { get; set; }

    public string CurrentPassword { get; set; }

    public string ConfirmPassword { get; set; }

    public string NewPassword { get; set; }
}
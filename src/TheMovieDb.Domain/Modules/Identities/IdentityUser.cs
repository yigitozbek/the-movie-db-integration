using System.ComponentModel.DataAnnotations;
using Yella.Domain.Entities;

namespace TheMovieDb.Domain.Modules.Identities;

public class IdentityUser : FullAuditedEntity<Guid>
{
    public IdentityUser(Guid id, string username, string email, byte[] passwordSalt, byte[] passwordHash, DateTime? lastLogin, DateTime? joined, short incorrectPasswordAttempts, ICollection<IdentityUserRole> identityUserRoles) : base(id)
    {
        Username = username;
        Email = email;
        PasswordSalt = passwordSalt;
        PasswordHash = passwordHash;
        LastLogin = lastLogin;
        Joined = joined;
        IncorrectPasswordAttempts = incorrectPasswordAttempts;
        IdentityUserRoles = identityUserRoles;
    }

    public IdentityUser(string username, string email, byte[] passwordSalt, byte[] passwordHash, DateTime? lastLogin, DateTime? joined, short incorrectPasswordAttempts, string name, string surname)
    {
        Username = username;
        Email = email;
        PasswordSalt = passwordSalt;
        PasswordHash = passwordHash;
        LastLogin = lastLogin;
        Joined = joined;
        IncorrectPasswordAttempts = incorrectPasswordAttempts;
        Name = name;
        Surname = surname;
    }

    [Required, MinLength(3), MaxLength(20)]
    public string Username { get; set; }

    [Required, MinLength(5)]
    public string Email { get; set; }

    public byte[] PasswordSalt { get; set; }

    public byte[] PasswordHash { get; set; }


    [Required, MinLength(5), MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [Required, MinLength(5), MaxLength(50)]
    public string Surname { get; set; } = string.Empty;

    public DateTime? LastLogin { get; set; }

    public DateTime? Joined { get; set; }

    public short IncorrectPasswordAttempts { get; set; }

    public virtual ICollection<IdentityUserRole> IdentityUserRoles { get; set; }

}

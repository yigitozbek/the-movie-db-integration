using System.ComponentModel.DataAnnotations;
using Yella.Domain.Entities;

namespace TheMovieDb.Domain.Modules.Identities;

public class IdentityPermission : Entity<short>
{
    [Required, MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [Required, MaxLength(150)]
    public string Description { get; set; } = string.Empty;

    [Required, MaxLength(50)]
    public string Module { get; set; } = string.Empty;

    public virtual ICollection<IdentityPermissionRole> IdentityPermissionRoles { get; set; }
}
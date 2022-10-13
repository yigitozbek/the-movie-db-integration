
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Yella.Domain.Entities;

namespace TheMovieDb.Domain.Modules.Identities;

public class IdentityRole : FullAuditedEntity<Guid>
{
    [StringLength(50)]
    public string Name { get; set; }

    [StringLength(250)]
    public string Description { get; set; }

    public virtual ICollection<IdentityUserRole> UserRoles { get; set; }
}
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TheMovieDb.Domain.Modules.Identities;
using TheMovieDb.Domain.Modules.Movies;
using Yella.EntityFrameworkCore;
using Yella.EntityFrameworkCore.Context;

namespace TheMovieDb.Domain.Context;

public class TheMovieDbContext : YellaDbContext<TheMovieDbContext>, IApplicationDbContext
{

    public TheMovieDbContext(DbContextOptions<TheMovieDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options, httpContextAccessor)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TheMovieDbContext).Assembly);

        GetEntityWithoutDeleted(modelBuilder);

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.NavigationBaseIncludeIgnored));
    }

    #region Identities
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityUserRole> UserRoles { get; set; }
    public DbSet<IdentityPermission> Permissions { get; set; }
    public DbSet<IdentityPermissionRole> PermissionRoles { get; set; }
    #endregion

    #region Movies

    public DbSet<Modules.Movies.Movie> Movies { get; set; }
    public DbSet<SpokenLanguage> SpokenLanguages { get; set; }
    public DbSet<BelongsToCollection> BelongsToCollections { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<ProductionCompany> ProductionCompanies { get; set; }
    public DbSet<ProductionCountry> ProductionCountries { get; set; }
    public DbSet<MovieSpokenLanguage> MovieSpokenLanguages { get; set; }
    public DbSet<MovieProductionCountry> MovieProductionCountries { get; set; }
    public DbSet<MovieProductionCompany> MovieProductionCompanies { get; set; }
    public DbSet<MovieGenre> MovieGenres { get; set; }
    public DbSet<MovieRate> MovieRates { get; set; }
    #endregion
}
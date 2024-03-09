using Entities.Models.Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Entities.Models.DbContexts;

public class ModelsDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public ModelsDbContext(DbContextOptions<ModelsDbContext> options) : base(options)
    {
    }

    protected ModelsDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ApplyCurrentLayerEntityConfigurations(modelBuilder);
    }

    /// <summary>
    /// Apply entity type configurations for Entities' entities.
    /// </summary>
    private static void ApplyCurrentLayerEntityConfigurations(ModelBuilder modelBuilder)
    {
        EnumToStringConverter<ReadingEnum> readingEnumConverter = new();
        modelBuilder.Entity<Reading>()
            .Property(r => r.ReadingEnum)
            .HasConversion(readingEnumConverter)
            .HasMaxLength(64);

        modelBuilder.Entity<Category>()
            .HasQueryFilter(c => c.Deleted);

        modelBuilder.Entity<Content>()
            .HasQueryFilter(c => c.Deleted);

        modelBuilder.Entity<Reading>()
            .HasQueryFilter(c => c.Deleted);

        #region Identity
        modelBuilder.Entity<IdentityRole<Guid>>().HasData(
        Constants.Authentication.Roles.GetAllRoles().Select(role => new IdentityRole<Guid>() { Id = Guid.NewGuid(), Name = role, NormalizedName = role.ToUpper() })
        #endregion
    );
    }
}
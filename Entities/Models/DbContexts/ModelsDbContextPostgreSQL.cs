using Microsoft.EntityFrameworkCore;

namespace Entities.Models.DbContexts;

/// <summary>
/// Context for the ModelsDbContext with PostgreSQL provider.
/// </summary>
public class ModelsDbContextPostgreSQL(DbContextOptions<ModelsDbContextPostgreSQL> options) : ModelsDbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Add custom provider settings here.
    }
}
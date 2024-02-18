using Microsoft.EntityFrameworkCore;

namespace Entities.Models.DbContexts;

/// <summary>
/// Adds additional configurations to the ModelsDbContext for SqlServer & Sqlite providers.
/// </summary>
public class ModelsDbContextSQL(DbContextOptions<ModelsDbContextSQL> options) : ModelsDbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Add custom provider settings here.
    }
}
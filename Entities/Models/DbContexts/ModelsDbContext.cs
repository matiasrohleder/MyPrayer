using Microsoft.EntityFrameworkCore;

namespace Entities.Models.DbContexts;

public class ModelsDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }

    public ModelsDbContext(DbContextOptions<ModelsDbContext> options) : base(options)
    {
    }

    protected ModelsDbContext(DbContextOptions options) : base(options)
    {
    }
}
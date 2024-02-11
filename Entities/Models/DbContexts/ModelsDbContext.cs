// Entities/AppDbContext.cs
using Microsoft.EntityFrameworkCore;

namespace Entities.Models.DbContexts;

public class ModelsDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }

    public ModelsDbContext()
    {
    }

    public ModelsDbContext(DbContextOptions<ModelsDbContext> options) : base(options)
    {
    }
}
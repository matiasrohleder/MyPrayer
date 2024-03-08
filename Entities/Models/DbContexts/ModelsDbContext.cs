using Entities.Models.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Entities.Models.DbContexts;

public class ModelsDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Content> Contents { get; set; }
    public DbSet<Reading> Readings { get; set; }

    public ModelsDbContext(DbContextOptions<ModelsDbContext> options) : base(options)
    {
    }

    protected ModelsDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        EnumToStringConverter<ReadingEnum> readingEnumConverter = new EnumToStringConverter<ReadingEnum>();
        modelBuilder.Entity<Reading>()
            .Property(r => r.ReadingEnum)
            .HasConversion(readingEnumConverter)
            .HasMaxLength(64);

        base.OnModelCreating(modelBuilder);
    }
}
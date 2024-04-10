using Entities.Seeds.EntitySeeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Entities.Seeds;

public class SeedManager
{
    private readonly ModelBuilder modelBuilder;
    private readonly DatabaseFacade database;

    public SeedManager(ModelBuilder modelBuilder, DatabaseFacade database)
    {
        this.modelBuilder = modelBuilder;
        this.database = database;
    }

    public void ExecuteSeed()
    {
        Dictionary<Type, IList<object>> addedElements = [];
        foreach (SeedBase seedClass in GetSeedFixture())
        {
            seedClass.ModelBuilder = modelBuilder;
            seedClass.AddedElements = addedElements;

            seedClass.Process();
        }
    }

    private static SeedBase[] GetSeedFixture() =>
    [
        new RoleSeed(),
        new UserSeed()
    ];
}
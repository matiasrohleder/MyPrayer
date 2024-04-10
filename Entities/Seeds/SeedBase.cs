using Microsoft.EntityFrameworkCore;

namespace Entities.Seeds;

public abstract class SeedBase
{
    public ModelBuilder ModelBuilder { get; set; }
    public Dictionary<Type, IList<object>> AddedElements { get; set; }

    public void Process()
    {
        Execute();
    }

    protected abstract void Execute();
}
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Interceptors;

public class LocalToUtcInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData? eventData, InterceptionResult<int> result)
    {
        IEnumerable<EntityEntry> entries = eventData.Context.ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var entry in entries)
            ConvertDatesToUtc(entry);

        return base.SavingChanges(eventData, result);
    }

    private static void ConvertDatesToUtc(EntityEntry entry)
    {
        IEnumerable<PropertyEntry> properties = entry.Properties
            .Where(p => p.Metadata.ClrType == typeof(DateTime) || p.Metadata.ClrType == typeof(DateTime?));

        foreach (PropertyEntry prop in properties)
        {
            if (prop.CurrentValue != null)
            {
                DateTime date = (DateTime)prop.CurrentValue;

                if (date.Kind == DateTimeKind.Unspecified || date.Kind == DateTimeKind.Local)
                    prop.CurrentValue = date.ToUniversalTime();
            }
        }
    }
}
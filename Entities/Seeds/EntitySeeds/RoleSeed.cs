using Entities.Constants.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Entities.Seeds.EntitySeeds;

public class RoleSeed : SeedBase
{
    protected override void Execute()
    {
        IdentityRole<Guid>[] elementsToAdd = Roles.GetAllRoles()
            .Select(role =>
            {
                Guid id = Roles.GetId(role);
                return new IdentityRole<Guid>()
                {
                    Id = id,
                    Name = role,
                    NormalizedName = role.ToUpper(),
                    ConcurrencyStamp = role
                };
            }).ToArray();

        AddedElements.Add(typeof(IdentityRole<Guid>), elementsToAdd);
        ModelBuilder.Entity<IdentityRole<Guid>>().HasData(elementsToAdd);
    }
}
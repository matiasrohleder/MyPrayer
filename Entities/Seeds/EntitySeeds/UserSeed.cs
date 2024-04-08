using Entities.Constants.Authentication;
using Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace Entities.Seeds.EntitySeeds;

public class UserSeed : SeedBase
{
    protected override void Execute()
    {
        PasswordHasher<ApplicationUser> userHasher = new();
        var adminGuid = Users.AdminId;
        string adminUser = "admin@myprayer.com";
        string adminPass = "qweQWE123!#";

        DateTime createdDate = new(2019, 5, 7, 3, 20, 00);

        ApplicationUser[] usersToAdd =
        [
                new()
                {
                    CreatedDate = createdDate,
                    Email = adminUser,
                    EmailConfirmed = true,
                    Id = adminGuid,
                    LastName = "MyPrayer",
                    Name = "Admin",
                    NormalizedEmail = adminUser.ToUpper(),
                    NormalizedUserName = adminUser.ToUpper(),
                    PasswordHash = userHasher.HashPassword(null, adminPass),
                    SecurityStamp = "3AA6005F-8E18-4A00-B9E2-C2539C60A8C1",
                    ConcurrencyStamp = adminUser,
                    UserName = adminUser,
                }
        ];

        AddedElements.Add(typeof(ApplicationUser), usersToAdd);
        ModelBuilder.Entity<ApplicationUser>().HasData(usersToAdd);

        // Add roles.
        List<IdentityRole<Guid>> roles = AddedElements[typeof(IdentityRole<Guid>)].ToList().Cast<IdentityRole<Guid>>().ToList();

        IdentityRole<Guid>? roleAdmin = roles.FirstOrDefault(r => r.Name == Roles.Admin);
        if (roleAdmin != null)
        {
            IdentityUserRole<Guid>[] userRolesToAdd =
            [
                    new()
                    {
                        RoleId = Roles.GetId(Roles.Admin),
                        UserId = adminGuid
                    },
            ];

            AddedElements.Add(typeof(IdentityUserRole<Guid>), userRolesToAdd);
            ModelBuilder.Entity<IdentityUserRole<Guid>>().HasData(userRolesToAdd);
        }
    }
}
using BusinessLayer.Interfaces;
using DataLayer.Interfaces;
using DataLayer.Services;
using Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace BusinessLayer.Services;

#region Properties and constructor
public class ApplicationUserService(
    IUnitOfWork unitOfWork,
    UserManager<ApplicationUser> userManager
    ) : Service<ApplicationUser>(unitOfWork), IApplicationUserService
{
    private readonly UserManager<ApplicationUser> userManager = userManager;
    #endregion

    /// <inheritdoc/>
    public async Task AddAsync(ApplicationUser user, IEnumerable<string> roles)
    {
        // Set a sequential id.
        user.Id = Guid.NewGuid(); //sarasa sacar

        user.CreatedDate = DateTime.UtcNow;
        user.LastEditedDate = user.CreatedDate;
        user.CreatorId = Guid.NewGuid();
        user.LastEditorId = user.CreatorId;

        IdentityResult createResult = await userManager.CreateAsync(user);

        if (!createResult.Succeeded)
            throw new Exception("Error al crear usuario");

        // Set user roles
        var addRolesResult = await userManager.AddToRolesAsync(user, roles);

        if (!addRolesResult.Succeeded)
            throw new Exception("Error al añadir roles");

        // this.RegisterOperation(user, DALOperations.Create); sarasa sacar
    }
}
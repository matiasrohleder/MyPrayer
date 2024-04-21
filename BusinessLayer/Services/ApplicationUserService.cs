using BusinessLayer.Interfaces;
using DataLayer.Interfaces;
using DataLayer.Services;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

        user.CreatedDate = DateTime.Now;
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
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(ApplicationUser user, IEnumerable<string> roles)
    {
        ApplicationUser dbUser = await base.GetAll()
                    .Where(u => u.Id == user.Id)
                    .FirstAsync();

        dbUser.Name = user.Name;
        dbUser.LastName = user.LastName;
        dbUser.Email = user.Email;
        dbUser.UserName = user.Email;
        dbUser.LastEditedDate = DateTime.Now;

        IdentityResult updateResult = await userManager.UpdateAsync(dbUser);

        if (!updateResult.Succeeded)
            throw new Exception("Error al actualizar usuario");

        // Update user roles
        IList<string> currentRoles = await userManager.GetRolesAsync(dbUser);

        // Calculate roles to remove.
        IEnumerable<string> rolesToRemove = currentRoles.Except(roles);

        // Remove user from roles.
        IdentityResult removeRolesResult = await userManager.RemoveFromRolesAsync(dbUser, rolesToRemove);

        if (!removeRolesResult.Succeeded)
            throw new Exception("Error al eliminar roles");

        // Calculate roles to add.
        IEnumerable<string> rolesToAdd = roles.Except(currentRoles);

        // Add user to their new roles.
        IdentityResult addRolesResult = await userManager.AddToRolesAsync(dbUser, rolesToAdd);

        if (!addRolesResult.Succeeded)
            throw new Exception("Error al agregar nuevos roles");
    }
}
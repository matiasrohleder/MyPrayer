using DataLayer.Interfaces;
using Entities.Models;

namespace BusinessLayer.Interfaces;

public interface IApplicationUserService : IService<ApplicationUser>
{
    /// <summary>
    /// Creates a new user.
    /// </summary>
    Task AddAsync(ApplicationUser user, IEnumerable<string> roles);

    /// <summary>
    /// Updates an existing user.
    /// </summary>
    Task UpdateAsync(ApplicationUser user, IEnumerable<string> roles);
}
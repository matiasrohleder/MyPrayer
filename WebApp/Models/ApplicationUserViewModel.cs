using Entities.Models;

namespace WebApp.Models;

public class ApplicationUserViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public List<string> Roles { get; set; }

    public ApplicationUserViewModel()
    {
        Id = Guid.NewGuid();
        Name = string.Empty;
        Email = string.Empty;
        Roles = [];
    }

    public ApplicationUserViewModel(ApplicationUser applicationUser)
    {
        Id = applicationUser.Id;
        Name = applicationUser.Name;
        Email = applicationUser.Email ?? string.Empty;
        Roles = [];
    }

    public ApplicationUser ToEntity()
    {
        ApplicationUser applicationUser = new()
        {
            Id = Id,
            Name = Name,
            Email = Email,

            UserName = Email,
            EmailConfirmed = true,
            PhoneNumberConfirmed = true
        };

        return applicationUser;
    }
}
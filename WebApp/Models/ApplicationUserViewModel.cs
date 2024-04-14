using Entities.Models;

namespace WebApp.Models;

public class ApplicationUserViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<string> Roles { get; set; }

    public ApplicationUserViewModel()
    {
        Id = Guid.NewGuid();
        Name = string.Empty;
        Roles = [];
    }

    public ApplicationUserViewModel(ApplicationUser applicationUser)
    {
        Id = applicationUser.Id;
        Name = applicationUser.Name;
    }

    public ApplicationUser ToEntity()
    {
        ApplicationUser applicationUser = new()
        {
            Id = Id,
            Name = Name,
        };

        return applicationUser;
    }
}
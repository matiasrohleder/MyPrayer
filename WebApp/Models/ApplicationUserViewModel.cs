using Entities.Models;

namespace WebApp.Models;

public class ApplicationUserViewModel
{
    public bool Active { get; set; }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Order { get; set; }

    public ApplicationUserViewModel()
    {
        Id = Guid.NewGuid();
        Name = string.Empty;
        Active = true;
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
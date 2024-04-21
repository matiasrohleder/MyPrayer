using System.ComponentModel.DataAnnotations;
using Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Models;

public class ApplicationUserViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public List<string> Roles { get; set; }

    [DataType(DataType.Password)]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
    public string ConfirmPassword { get; set; }

    public ApplicationUserViewModel()
    {
        Id = Guid.NewGuid();
        Name = string.Empty;
        LastName = string.Empty;
        Email = string.Empty;
        Roles = [];
        Password = string.Empty;
        ConfirmPassword = string.Empty;
    }

    public ApplicationUserViewModel(ApplicationUser applicationUser, List<string> roles)
    {
        Id = applicationUser.Id;
        Name = applicationUser.Name;
        LastName = applicationUser.LastName;
        Email = applicationUser.Email ?? string.Empty;
        Roles = roles;
        Password = "";
        ConfirmPassword = "";
    }

    public ApplicationUser ToEntity()
    {
        PasswordHasher<ApplicationUser> userHasher = new();
        ApplicationUser applicationUser = new()
        {
            Id = Id,
            Name = Name,
            LastName = LastName,
            Email = Email,

            UserName = Email,
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            PasswordHash = userHasher.HashPassword(null, Password),
        };

        return applicationUser;
    }
}
using System.Reflection;

namespace Entities.Constants.Authentication;

public static class Roles
{
    public const string Admin = "Admin";
    public const string User = "User";

    public static ICollection<string> GetAllRoles() =>
        typeof(Roles)
        .GetFields(BindingFlags.Public)
        .Where(f => f.IsLiteral && !f.IsInitOnly)
        .Select(f => (f.GetValue(null) as string) ?? string.Empty) //sarasa, si falla ver bi
        .ToList();
}
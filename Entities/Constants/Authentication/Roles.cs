using System.Reflection;

namespace Entities.Constants.Authentication;

public static class Roles
{
    public const string Admin = "Admin";
    private static readonly Guid adminId = Guid.Parse("3f3dd5b3-b480-4ea2-a477-101010101010");

    public static ICollection<string> GetAllRoles() =>
        typeof(Roles)
        .GetFields(BindingFlags.Public)
        .Where(f => f.IsLiteral && !f.IsInitOnly)
        .Select(f => (f.GetValue(null) as string) ?? string.Empty)
        .ToList();

    /// <summary>
    /// Get the id from the current role
    /// </summary>
    /// <param name="role">Name of the role you want to get the id</param>
    public static Guid GetId(string role)
    {
        FieldInfo[] publicFields = typeof(Roles).GetFields(BindingFlags.Static | BindingFlags.Public);
        string rolName = publicFields.First(f => f.GetValue(null).ToString() == role).Name;

        string idFieldName = $"{char.ToLowerInvariant(rolName[0])}{rolName[1..]}Id";
        FieldInfo[] privateFields = typeof(Roles).GetFields(BindingFlags.Static | BindingFlags.NonPublic);
        return Guid.Parse(privateFields.First(f => f.Name == idFieldName).GetValue(null).ToString());
    }
}
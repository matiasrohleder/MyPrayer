using System.Reflection;

namespace Entities.Constants.Authentication;

public static class Roles
{
    public const string Admin = "Admin";
    private static readonly Guid adminId = Guid.Parse("3f3dd5b3-b480-4ea2-a477-101010101010");

    public static ICollection<string> GetAllRoles() => typeof(Roles).GetFields(BindingFlags.Public | BindingFlags.Static)
        .Where(f => f.IsLiteral && !f.IsInitOnly)
        .Select(f => f.GetValue(null) as string)
        .ToList();

    /// <summary>
    /// Get the id from the current role
    /// </summary>
    /// <param name="role">Name of the role you want to get the id</param>
    public static Guid GetId(string role)
    {
        string idFieldName = $"{char.ToLowerInvariant(role[0])}{role[1..]}Id";
        FieldInfo idField = typeof(Roles).GetField(idFieldName, BindingFlags.Static | BindingFlags.NonPublic);
        return (Guid)idField!.GetValue(null);
    }
}

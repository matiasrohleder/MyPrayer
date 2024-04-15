using System.Reflection;

namespace Entities.Constants.Authentication;

public static class Roles
{
    #region Roles
    public const string Admin = "Admin";
    private static readonly Guid adminId = Guid.Parse("3f3dd5b3-b480-4ea2-a477-101010101010");

    public const string CategoryAdmin = "CategoryAdmin";
    private static readonly Guid categoryAdminId = Guid.Parse("8d02c502-afbb-4e9b-9552-6c2cabbd6864");

    public const string ContentAdmin = "ContentAdmin";
    private static readonly Guid contentAdminId = Guid.Parse("0d6490bf-fdfd-4a68-99ab-c0bb135b28f7");

    public const string DailyQuoteAdmin = "DailyQuoteAdmin";
    private static readonly Guid dailyQuoteAdminId = Guid.Parse("166800F3-0CB9-4B19-9465-D63DEDD2608B");

    public const string ReadingAdmin = "ReadingAdmin";
    private static readonly Guid readingAdminId = Guid.Parse("f6c0b642-2ddb-42f5-8ba8-0ce9faeb3a4d");

    public const string UserAdmin = "UserAdmin";
    private static readonly Guid userAdminId = Guid.Parse("b8be6280-b3e8-48a3-9b01-7a361961595a");
    #endregion

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

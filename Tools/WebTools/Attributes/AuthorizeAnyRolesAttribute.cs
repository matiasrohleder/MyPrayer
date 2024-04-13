using Microsoft.AspNetCore.Authorization;

namespace Tools.WebTools.Attributes;

public class AuthorizeAnyRolesAttribute : AuthorizeAttribute
{
    public AuthorizeAnyRolesAttribute(params string[] roles)
    {
        Roles = string.Join(",", roles);
    }
}
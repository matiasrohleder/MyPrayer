using Microsoft.AspNetCore.Authentication;
using Microsoft.Net.Http.Headers;

namespace WebAPI.Authentication
{
    public class ApiKeySchemeOptions : AuthenticationSchemeOptions
    {
        public const string ApiKeySectionName = "Authentication:ApiKey";
        public const string Scheme = "ApiKeyScheme";
        public string HeaderName { get; set; } = HeaderNames.Authorization;
    }
}
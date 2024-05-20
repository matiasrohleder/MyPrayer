using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace WebAPI.Authentication
{
    public class ApiKeySchemeHandler : AuthenticationHandler<ApiKeySchemeOptions>
    {
        private readonly IConfiguration _configuration;

        public ApiKeySchemeHandler(IConfiguration configuration, IOptionsMonitor<ApiKeySchemeOptions> options,

            ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
            _configuration = configuration;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey(Options.HeaderName))
            {
                return AuthenticateResult.Fail("Header Not Found.");
            }

            var apiKey = _configuration.GetValue<string>(ApiKeySchemeOptions.ApiKeySectionName);

            if (Request.Headers.TryGetValue(Options.HeaderName, out var extractedApiKey))
            {
                if (!apiKey.Equals(extractedApiKey))
                    return AuthenticateResult.Fail("Invalid Api Key.");
            }

            var claims = new Claim[]
            {
            new Claim(ClaimTypes.NameIdentifier, $"{apiKey}"),
            new Claim(ClaimTypes.Name, "apiKey")
            };

            var identiy = new ClaimsIdentity(claims, nameof(ApiKeySchemeHandler));
            var principal = new ClaimsPrincipal(identiy);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}
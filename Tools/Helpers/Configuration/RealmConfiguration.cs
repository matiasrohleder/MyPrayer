using Microsoft.Extensions.Configuration;
using Tools.Interfaces.Configuration;

namespace Tools.Helpers.Configuration;

public class RealmConfiguration : IRealmConfiguration
{
    public string Name { get; set; }
    public bool AllowSelfSignUp { get; set; }
    public bool AllowApiKeyAuthorization { get; set; }
    public int IdentityTokenExpirationMinutes { get; set; }
    public bool RequireEmailConfirmation { get; set; }

    public RealmConfiguration(IConfiguration configuration)
    {
        Bind(configuration);
    }

    public RealmConfiguration Bind(IConfiguration configuration)
    {
        configuration.GetSection("Realm").Bind(this);
        return this;
    }
}